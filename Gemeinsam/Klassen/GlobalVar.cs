using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GlobalVar
{
    public enum Language
    {
        De=0,
        En,
        Fr,
        Sp,
        Ru
    }
    public static bool Glb_Passwort_ok;

    //public static Language Glb_Language;

    public static string Glb_SQLConnecton;






    //Konstanten fuer automatische Generierung des Serviceformulars


    public const int Const_Service_TextHeigt = 39;
    public const int Const_Service_TextBreite = 400;
    public const int Const_Service_LblSpacing = 2;


    //Konstanten fuer automatische Generierung des Freigabeformulars
    public const int Const_ControlSpacing = 5;
    public const int Const_LedSpacing = 2;
    public const int Const_LedHeigt = 25;
    public const int Const_TextHeigt = 25;
    public const int Const_GroupBoxWidth = 428;
    public const int Const_GroupBoxOffset = 20;
    public const int Const_GroupBoxOffsetRest = 50;



}

