using System;
using System.Collections.Generic;

namespace MusicApp
{
    //každý akord má svoji třídu
    //akordy jsou seřazeny vzestupně- nejdřív durové potom molové
    // každý akord se skládá ze tří základních tónů tzv. tercie a potom má každý akord pole, kde jsou uložené pozice tónů na Kytarovém grafu (KytaraNoty)
    static public class Akordy
    {
        public static List<Akord> AkordsList = new List<Akord>()
        { 
            new Akord(){Name = "C",Tony=(Nota.c,Nota.e,Nota.g), KytaraPozice=new int[] {-1,3,2,0,1,0}},
            new Akord(){Name = "C#",Tony=(Nota.cis,Nota.f,Nota.gis), KytaraPozice=new int[] {-1,-1,3,1,2,1}},
            new Akord(){Name = "D",Tony=(Nota.d,Nota.fis,Nota.a), KytaraPozice=new int[] {-1,-1,0,2,3,2}},
            new Akord(){Name = "Eb",Tony=(Nota.dis,Nota.g,Nota.b), KytaraPozice=new int[] {-1,-1,-1,3,4,3}},
            new Akord(){Name = "E",Tony=(Nota.e,Nota.gis,Nota.h), KytaraPozice=new int[] {0,2,2,1,0,0}},
            new Akord(){Name = "F",Tony=(Nota.f,Nota.a,Nota.c), KytaraPozice=new int[] {0,0,3,2,1,1}},
            new Akord(){Name = "F#",Tony=(Nota.fis,Nota.b,Nota.cis), KytaraPozice=new int[] {2,0,0,2,3,0}},
            new Akord(){Name = "G",Tony=(Nota.g,Nota.h,Nota.d), KytaraPozice=new int[] {3,2,0,0,0,3}},
            new Akord(){Name = "Ab",Tony=(Nota.gis,Nota.c,Nota.dis), KytaraPozice=new int[] {-1,1,3,3,3,1}},
            new Akord(){Name = "A",Tony=(Nota.a,Nota.cis,Nota.e), KytaraPozice=new int[] {-1,0,2,2,2,0}},
            new Akord(){Name = "Hb",Tony=(Nota.b,Nota.d,Nota.f), KytaraPozice=new int[] {-1,1,3,3,3,1}},
            new Akord(){Name = "H",Tony=(Nota.h,Nota.dis,Nota.fis), KytaraPozice=new int[] {-1,2,4,4,4,2}},
            new Akord(){Name = "Cm",Tony=(Nota.c,Nota.dis,Nota.g), KytaraPozice=new int[] {-1,3,5,5,4,3}},
            new Akord(){Name = "C#m",Tony=(Nota.cis,Nota.e,Nota.gis), KytaraPozice=new int[] {-1,-1,2,1,2,-1}},
            new Akord(){Name = "Dm",Tony=(Nota.d,Nota.f,Nota.a), KytaraPozice=new int[] {-1,-1,0,2,3,1}},
            new Akord(){Name = "Ebm",Tony=(Nota.dis,Nota.fis,Nota.b), KytaraPozice=new int[] {-1,-1,1,3,4,2}},
            new Akord(){Name = "Em",Tony=(Nota.e,Nota.g,Nota.h), KytaraPozice=new int[] {0,2,2,0,0,0}},
            new Akord(){Name = "Fm",Tony=(Nota.f,Nota.gis,Nota.c), KytaraPozice=new int[] {1,3,3,1,1,1}},
            new Akord(){Name = "F#m",Tony=(Nota.fis,Nota.a,Nota.cis), KytaraPozice=new int[] {2,4,4,2,2,2}},
            new Akord(){Name = "Gm",Tony=(Nota.g,Nota.b,Nota.d), KytaraPozice=new int[] {3,5,5,3,3,3}},
            new Akord(){Name = "Abm",Tony=(Nota.gis,Nota.h,Nota.dis), KytaraPozice=new int[] {4,2,1,1,-1,-1}},
            new Akord(){Name = "Am",Tony=(Nota.a,Nota.c,Nota.e), KytaraPozice=new int[] {-1,0,2,2,1,0}},
            new Akord(){Name = "Hbm",Tony=(Nota.b,Nota.cis,Nota.f), KytaraPozice=new int[] {-1,-1,3,3,2,1}},
            new Akord(){Name = "Hm",Tony=(Nota.h,Nota.d,Nota.fis), KytaraPozice=new int[] {-1,2,4,4,3,-1 }}
            

        };
    }

    public class Akord
    {
        public string Name;
        public (Nota, Nota, Nota) Tony;
        public int[] KytaraPozice;
        
    }

    //všechny tóny na klávesách- prvních sedm určuje název bílích klavírních kláves, dalších pět půltónů jsou názvem černých kláves
    public enum Nota
    {
        c,
        d,
        e,
        f,
        g,
        a,
        h,
        cis,
        dis,
        fis,
        gis,
        b
    }

}