using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Parser_XOM : ModParser<XOM_File>
    {
        public Parser_XOM(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".XOM" };
        public override bool SecondarySkip => false;
        public override List<string> SecondaryList => new List<string>()
        {
            
            "American.xom",
            "Canadiam.xom",
            "Czech.xom",
            "English.xom",
            "French.xom",
            "German.xom",
            "Italian.xom",
            "Japanese.xom",
            "Polish.xom",
            "Slovak.xom",
            "Spanish.xom",

            "Building.XOM",
            "Weapons.XOM",
            "Control.XOM",
            "Local.xom",

            "DM01.xom",
            "DM02.xom",
            "DM03.xom",
            "DM04.xom",
            "DM05.xom",
            "DM06.xom",
            "DM07.xom",
            "DM08.xom",
            "DM09.xom",
            "DM10.xom",

            "ME1.xom",
            "ME2.xom",
            "ME3.xom",
            "MM1.xom",
            "MM2.xom",
            "MM3.xom",
            "MO1.xom",
            "MO2.xom",
            "MO3.xom",
            "MR1.xom",
            "MR2.xom",
            "MR3.xom",

            "E1.xom",
            "E2.xom",
            "E3.xom",
            "E4.xom",
            "E5.xom",
            "G1.xom",
            "G2.xom",
            "G3.xom",
            "G4.xom",
            "G5.xom",
            "M1.xom",
            "M2.xom",
            "M3.xom",
            "M4.xom",
            "M5.xom",
            "O1.xom",
            "O2.xom",
            "O3.xom",
            "O4.xom",
            "O5.xom",
            
            "T.xom",
            "T2.xom",
            "T3.xom",

        };
        public override bool DisableAsync => true; //too resource-intensive atm

        public override XOM_File LoadObject(string filePath)
        {
            XOM_File file = new XOM_File();
            file.Read(filePath);
            return file;
        }

        public override void SaveObject(XOM_File thing, string filePath)
        {
            thing.Write(filePath);
        }
    }
}
