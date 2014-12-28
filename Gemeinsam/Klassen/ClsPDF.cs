using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace Helper
{
    class ClsPDF
    {


        public ClsPDF()
        {

            int leftborder = 0;
            int upperborder = 0;

            PdfDocument document = new PdfDocument();

            // Create a font
            XFont font = new XFont("Arial", 22, XFontStyle.Bold);
            XFont font1 = new XFont("Arial", 14, XFontStyle.Bold);
            XFont font2 = new XFont("Arial", 10, XFontStyle.Regular);
            XFont font3 = new XFont("Arial", 10, XFontStyle.Bold);
            XFont font4 = new XFont("Arial", 8, XFontStyle.Regular);
            XFont font5 = new XFont("Arial", 8, XFontStyle.Bold);


            PdfPage page = document.AddPage();
            page.Size = PageSize.A4;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XImage image = XImage.FromFile(@"Yoko.jpg");
            //TODO: Bilder auswählen und hinzufügen
            //XImage image2 = XImage.FromFile(@"DPHarp EJX.jpg");

            System.Drawing.Pen pen = new Pen(Color.Black, 4);
            gfx.DrawString("Calibration", font, XBrushes.Black, leftborder + 20, upperborder + 50);
            System.Drawing.Pen pen1 = new Pen(Color.Black, 1);
            string filename = "pdftest"+ ".pdf";

            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);


        }
        ~ClsPDF()
        {
           
        }


    }
}
