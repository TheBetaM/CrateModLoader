using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public class Parser_XOM : ModParser<XOM_File>
    {
        private WormsGame targetGame;

        public Parser_XOM(Modder mod, WormsGame game) : base(mod)
        {
            targetGame = game;
        }

        public override List<string> Extensions => new List<string>() { ".XOM", ".BDL", ".KEV" };
        public override bool SecondarySkip => false;
        public override List<string> SecondaryList => new List<string>()
        {
            
            "American.xom",
            "Canadian.xom",
            "Czech.xom",
            "English.xom",
            "French.xom",
            "German.xom",
            "Italian.xom",
            "Japanese.xom",
            "Polish.xom",
            "Slovak.xom",
            "Spanish.xom",
            "Local.xom",

            //WormsForts

            "Building.XOM",
            "Weapons.XOM",
            "Control.XOM",

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

            //Worms3D

            "dday.xom",
            "crate britain.xom",
            "graveyard.xom",
            "leek.xom",
            "ice.xom",
            "collide.xom",
            "rum.xom",
            "crust.xom",
            "applecore.xom",
            "helterskelter.xom",
            "cherry.xom",
            "clean.xom",
            "timbers.xom",
            "falling.xom",
            "cropcircle.xom",
            "treevillage.xom",
            "landing.xom",
            "beanstalk.xom",
            "schools.xom",
            "highstakes.xom",
            "notpc.xom",
            "cooped.xom",
            "Trial.xom",
            "showdown.xom",
            "plaice.xom",
            "hookline.xom",
            "funfair.xom",
            "pegasus.xom",
            "boldly.xom",
            "balloon.xom",
            "countingsheep.xom",
            "breakfast.xom",
            "holiday.xom",
            "pack.xom",
            "alien.xom",

            "tutorial1.xom",
            "tutorial2.xom",
            "tutorial3.xom",
            "Tutorial4.xom",
            "tutorial5.xom",

            "Deathmatch1.xom",
            "Deathmatch2.xom",
            "Deathmatch3.xom",
            "Deathmatch4.xom",
            "Deathmatch5.xom",
            "Deathmatch6.xom",
            "Deathmatch7.xom",
            "Deathmatch8.xom",
            "Deathmatch9.xom",
            "Deathmatch10.xom",

            //Worms4 

            "AmeFE.xom",
            "CzeFE.xom",
            "EngFE.xom",
            "FreFE.xom",
            "GerFE.xom",
            "ItaFE.xom",
            "JapFE.xom",
            "PolFE.xom",
            "RusFE.xom",
            "SloFE.xom",
            "SpaFE.xom",

            "SNEAKYBRIDGETHIEVES.XOM",
            "CARPETCAPERS.XOM",
            "CHUTETOVICTORY.XOM",
            "THECRATEESCAPE.XOM",
            "DESTRUCTANDSERVE.XOM",
            "DINERMIGHT.XOM",
            "DOOMCANYON.XOM",
            "ESCAPEFROMTREEREX.XOM",
            "FASTFOODDINO.XOM",
            "GHOSTHILLGRAVEYARD.XOM",
            "GIBBONTAKE.XOM",
            "HIGHNOONHIJINX.XOM",
            "JOUSTABOUTIT.XOM",
            "THELANDTHATWORMSFORGOT.XOM",
            "MINEALLMINE.XOM",
            "NICETOSIEGEYOU.XOM",
            "NOROOMFORERROR.XOM",
            "RIVERBOATHARBOUR.XOM",
            "ROBINTHEHOOD.XOM",
            "BUILDINGSITESABOTEURS.XOM",
            "STORMTHECASTLE.XOM",
            "THISGAMEDOESDRAGON.XOM",
            "TINCANWALLY.XOM",
            "TRAITOROUSWATERS.XOM",
            "TURKISHDELIGHTS.XOM",
            "VALLEYOFTHEDINO.XOM",
            "THEWINDYWIZARD.XOM",
            "WORMWIGSBIGRIGJIG.XOM",

            "TUTORIAL1.XOM",
            "TUTORIAL2.XOM",
            "TUTORIAL3.XOM",

            "DEATHMATCH1.xom",
            "DEATHMATCH2.xom",
            "DEATHMATCH3.xom",
            "DEATHMATCH4.xom",
            "DEATHMATCH5.xom",
            "DEATHMATCH6.xom",
            "DEATHMATCH7.xom",
            "DEATHMATCH8.xom",
            "DEATHMATCH9.xom",
            "DEATHMATCH10.xom",

            //WormsOW

            "AmericanFE.xom",
            "CzechFE.xom",
            "EnglishFE.xom",
            "FrenchFE.xom",
            "GermanFE.xom",
            "ItalianFE.xom",
            "JapaneseFE.xom",
            "PolishFE.xom",
            "Russian.xom",
            "RussianFE.xom",
            "SpanishFE.xom",

            //WormsOW2

            //WormsBI

        };
        public override bool DisableAsync => true; //too resource-intensive atm

        public override XOM_File LoadObject(string filePath)
        {
            XOM_File file = new XOM_File();
            file.FileGame = targetGame;
            file.Read(filePath);
            return file;
        }

        public override void SaveObject(XOM_File thing, string filePath)
        {
            thing.Write(filePath);
        }
    }
}
