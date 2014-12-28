using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
/// <summary>
/// Klasse, um Dateien im Ini-Format
/// zu verwalten.
/// </summary>
public class ClsIni
{
	/// <summary>
	/// Inhalt der Datei
	/// </summary>
	private List<String> m_lines = new List<string>();
	
	/// <summary>
	/// Voller Pfad und Name der Datei
	/// </summary>
	private String m_fileName = "";

	/// <summary>
	/// Gibt an, welche Zeichen als Kommentarbeginn
	/// gewertet werden sollen. Dabei wird das erste 
	/// Zeichen defaultmaessig fuer neue Kommentare
	/// verwendet.
	/// </summary>
	private String m_commentCharacters = "#;";

	/// <summary>
	/// Regulaerer Ausdruck fuer einen Kommentar in einer Zeile
	/// </summary>
	private String m_regCommentStr = "";

	/// <summary>
	/// Regulaerer Ausdruck fuer einen Eintrag
	/// </summary>
	private Regex m_regEntry = null;

	/// <summary>
	/// Regulaerer Ausdruck fuer einen Bereichskopf
	/// </summary>
	private Regex m_regCaption = null;

	/// <summary>
	/// Leerer Standard-Konstruktor
	/// </summary>
	public ClsIni()
	{
        this.m_regCommentStr = @"(\s*[" + this.m_commentCharacters + "](?<comment>.*))?";
        this.m_regEntry = new Regex(@"^[ \t]*(?<entry>([^=])+)=(?<value>([^=" + this.m_commentCharacters + "])+)" + this.m_regCommentStr + "$");
        this.m_regCaption = new Regex(@"^[ \t]*(\[(?<caption>([^\]])+)\]){1}" + this.m_regCommentStr + "$");
	}

	/// <summary>
	/// Konstruktor, welcher sofort eine Datei einliest
	/// </summary>
	/// <param name="fileName">Name der einzulesenden Datei</param>
	public ClsIni(string fileName) : this ()
	{
		if (!File.Exists(fileName)) throw new IOException("File " + fileName + "  not found");
        this.m_fileName = fileName;
        using (StreamReader sr = new StreamReader(this.m_fileName))
            while (!sr.EndOfStream) this.m_lines.Add(sr.ReadLine().TrimEnd());
	}

	/// <summary>
	/// Datei sichern
	/// </summary>
	/// <returns></returns>
	public Boolean Save()
	{
        if (this.m_fileName == "") return false;
		try
		{
            using (StreamWriter sw = new StreamWriter(this.m_fileName))
                foreach (String line in this.m_lines)
					sw.WriteLine(line);
		}
		catch (IOException ex)
		{
			throw new IOException("Fehler beim Schreiben der Datei " + FileName, ex);
		}
		catch
		{
			throw new IOException("Fehler beim Schreiben der Datei " + FileName);
		}
		return true;
	}

	/// <summary>
	/// Voller Name der Datei
	/// </summary>
	/// <returns></returns>
	public String FileName
	{
        get { return this.m_fileName; }
        set { this.m_fileName = value; }
	}

	/// <summary>
	/// Verzeichnis der Datei
	/// </summary>
	/// <returns></returns>
	public String GetDirectory()
	{
		return Path.GetDirectoryName(this.m_fileName);
	}

	/// <summary>
	/// Sucht die Zeilennummer (nullbasiert) 
	/// eines gewuenschten Eintrages
	/// </summary>
	/// <param name="Caption">Name des Bereiches</param>
	/// <param name="CaseSensitive">true = Gross-/Kleinschreibung beachten</param>
	/// <returns>Nummer der Zeile, sonst -1</returns>
	private int SearchCaptionLine(String Caption, Boolean CaseSensitive=true)
	{
		if (!CaseSensitive) Caption = Caption.ToLower();
        for (int i = 0; i < this.m_lines.Count; i++)
		{
            String line = this.m_lines[i].Trim();
			if (line == "") continue;
			if (!CaseSensitive) line = line.ToLower();
			// Erst den gewuenschten Abschnitt suchen
			if (line == "[" + Caption + "]")
				return i;
		}
		return -1;// Bereich nicht gefunden
	}

	/// <summary>
	/// Sucht die Zeilennummer (nullbasiert) 
	/// eines gewuenschten Eintrages
	/// </summary>
	/// <param name="Caption">Name des Bereiches</param>
	/// <param name="Entry">Name des Eintrages</param>
	/// <param name="CaseSensitive">true = Gross-/Kleinschreibung beachten</param>
	/// <returns>Nummer der Zeile, sonst -1</returns>
	private int SearchEntryLine(String Caption, String Entry, Boolean CaseSensitive=true)
	{
		Caption = Caption.ToLower();
		if (!CaseSensitive) Entry = Entry.ToLower();
		int CaptionStart = SearchCaptionLine(Caption, false);
		if (CaptionStart < 0) return -1;
        for (int i = CaptionStart + 1; i < this.m_lines.Count; i++)
		{
            String line = this.m_lines[i].Trim();
			if (line == "") continue;
			if (!CaseSensitive) line = line.ToLower();
			if (line.StartsWith("[")) 
				return -1;// Ende, wenn der naechste Abschnitt beginnt
            if (Regex.IsMatch(line, @"^[ \t]*[" + this.m_commentCharacters + "]"))
				continue; // Kommentar
			if (line.StartsWith(Entry)) 
				return i;// Eintrag gefunden
		}
		return -1;// Eintrag nicht gefunden
	}

	/// <summary>
	/// Kommentiert einen Wert aus
	/// </summary>
	/// <param name="caption">Name des Bereiches</param>
	/// <param name="entry">Name des Eintrages</param>
	/// <param name="caseSensitive">true = Gross-/Kleinschreibung beachten</param>
	/// <returns>true = Eintrag gefunden und auskommentiert</returns>
	public Boolean CommentValue(string caption, string entry, bool caseSensitive=true)
	{
		int line = SearchEntryLine(caption, entry, caseSensitive);
		if (line < 0) return false;
        this.m_lines[line] = this.m_commentCharacters[0] + this.m_lines[line];
		return true;
	}

	/// <summary>
	/// Loescht einen Wert
	/// </summary>
	/// <param name="caption">Name des Bereiches</param>
	/// <param name="entry">Name des Eintrages</param>
	/// <param name="caseSensitive">true = Gross-/Kleinschreibung beachten</param>
	/// <returns>true = Eintrag gefunden und geloescht</returns>
	public Boolean DeleteValue(string caption, string entry, bool caseSensitive=true)
	{
		int line = SearchEntryLine(caption, entry, caseSensitive);
		if (line < 0) return false;
        this.m_lines.RemoveAt(line);
		return true;
	}

	/// <summary>
	/// Liest den Wert eines Eintrages aus
	/// (Erweiterung: case sensitive)
	/// </summary>
	/// <param name="caption">Name des Bereiches</param>
	/// <param name="entry">Name des Eintrages</param>
	/// <param name="caseSensitive">true = Gross-/Kleinschreibung beachten</param>
	/// <returns>Wert des Eintrags oder leer</returns>
	public String GetValue(string caption, string entry, bool caseSensitive=true)
	{
		int line = SearchEntryLine(caption, entry, caseSensitive);
		if (line < 0) return "";
        int pos = this.m_lines[line].IndexOf("=");
		if (pos < 0) return "";
        return this.m_lines[line].Substring(pos + 1).Trim();
		// Evtl. noch abschliessende Kommentarbereiche entfernen
	}

	/// <summary>
	/// Setzt einen Wert in einem Bereich. Wenn der Wert
	/// (und der Bereich) noch nicht existiert, werden die
	/// entsprechenden Eintraege erstellt.
	/// </summary>
	/// <param name="caption">Name des Bereichs</param>
	/// <param name="entry">name des Eintrags</param>
	/// <param name="value">Wert des Eintrags</param>
	/// <param name="caseSensitive">true = Gross-/Kleinschreibung beachten</param>
	/// <returns>true = Eintrag erfolgreich gesetzt</returns>
	public Boolean SetValue(string caption, string entry, string value, bool caseSensitive=true)
	{
		caption = caption.ToLower();
		if (!caseSensitive) entry = entry.ToLower();
		int lastCommentedFound = -1;
		int CaptionStart = SearchCaptionLine(caption, false);
		if (CaptionStart < 0)
		{
            this.m_lines.Add("[" + caption + "]");
            this.m_lines.Add(entry + "=" + value);
			return true;
		}
		int EntryLine = SearchEntryLine(caption, entry, caseSensitive);
        for (int i = CaptionStart + 1; i < this.m_lines.Count; i++)
		{
            String line = this.m_lines[i].Trim();
			if (!caseSensitive) line = line.ToLower();
			if (line == "") continue;
			// Ende, wenn der naechste Abschnitt beginnt
			if (line.StartsWith("["))
			{
                this.m_lines.Insert(i, entry + "=" + value);
				return true;
			}
			// Suche aukommentierte, aber gesuchte Eintraege
			// (evtl. per Parameter bestimmen koennen?), falls
			// der Eintrag noch nicht existiert.
			if (EntryLine<0)
                if (Regex.IsMatch(line, @"^[ \t]*[" + this.m_commentCharacters + "]"))
				{
					String tmpLine = line.Substring(1).Trim();
					if (tmpLine.StartsWith(entry))
					{
						// Werte vergleichen, wenn gleich,
						// nur Kommentarzeichen loeschen
						int pos = tmpLine.IndexOf("=");
						if (pos > 0)
						{
							if (value == tmpLine.Substring(pos + 1).Trim())
							{
                                this.m_lines[i] = tmpLine;
								return true;
							}
						}
						lastCommentedFound = i;
					}
					continue;// Kommentar
				}
			if (line.StartsWith(entry))
			{
                this.m_lines[i] = entry + "=" + value;
				return true;
			}
		}
		if (lastCommentedFound > 0)
            this.m_lines.Insert(lastCommentedFound + 1, entry + "=" + value);
		else
            this.m_lines.Insert(CaptionStart + 1, entry + "=" + value);
		return true;
	}

	/// <summary>
	/// Liest alle Eintraege uns deren Werte eines Bereiches aus
	/// </summary>
	/// <param name="caption">Name des Bereichs</param>
	/// <returns>Sortierte Liste mit Eintraegen und Werten</returns>
	public SortedList<String, String> GetCaption(string caption)
	{
		SortedList<String, String> result = new SortedList<string,string>();
		Boolean CaptionFound = false;
        for (int i = 0; i < this.m_lines.Count; i++)
		{
            String line = this.m_lines[i].Trim();
			if (line == "") continue;
			// Erst den gewuenschten Abschnitt suchen
			if (!CaptionFound)
				if (line.ToLower() != "[" + caption + "]") continue;
				else
				{
					CaptionFound = true;
					continue;
				}
			// Ende, wenn der naechste Abschnitt beginnt
			if (line.StartsWith("[")) break;
            if (Regex.IsMatch(line, @"^[ \t]*[" + this.m_commentCharacters + "]")) continue; // Kommentar
			int pos = line.IndexOf("=");
			if (pos < 0)
				result.Add(line, "");
			else
				result.Add(line.Substring(0,pos).Trim(),line.Substring(pos + 1).Trim());
		}
		return result;
	}

	/// <summary>
	/// Erstellt eine Liste aller enthaltenen Bereiche
	/// </summary>
	/// <returns>Liste mit gefundenen Bereichen</returns>
	public List<string> GetAllCaptions()
	{
		List<string> result = new List<string>();
        for (int i = 0; i < this.m_lines.Count; i++)
		{
            String line = this.m_lines[i];
            Match mCaption = this.m_regCaption.Match(this.m_lines[i]);
			if (mCaption.Success)
				result.Add(mCaption.Groups["caption"].Value.Trim());
		}
		return result;
	}

	/// <summary>
	/// Exportiert saemtliche Bereiche und deren Werte
	/// in ein XML-Dokument
	/// </summary>
	/// <returns>XML-Dokument</returns>
	public XmlDocument ExportToXml()
	{
		XmlDocument doc = new XmlDocument();
		XmlElement root = doc.CreateElement(Path.GetFileNameWithoutExtension(this.FileName));
		doc.AppendChild(root);
		XmlElement Caption = null;
        for (int i = 0; i < this.m_lines.Count; i++)
		{
            Match mEntry = this.m_regEntry.Match(this.m_lines[i]);
            Match mCaption = this.m_regCaption.Match(this.m_lines[i]);
			if (mCaption.Success)
			{
				Caption = doc.CreateElement(mCaption.Groups["caption"].Value.Trim());
				root.AppendChild(Caption);
				continue;
			}
			if (mEntry.Success)
			{
				XmlElement xe = doc.CreateElement(mEntry.Groups["entry"].Value.Trim());
				xe.InnerXml = mEntry.Groups["value"].Value.Trim();
				if (Caption == null)
					root.AppendChild(xe);
				else
					Caption.AppendChild(xe);
			}
		}
		return doc;
	}
}
 
