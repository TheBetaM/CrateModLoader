using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Twinsanity;
//Twinsanity API by NeoKesha

namespace CrateModLoader
{
    class Modder_Twins
    {
        public string gameName = "Twinsanity";
        public string apiCredit = "API by NeoKesha";
        public System.Drawing.Image gameIcon = Properties.Resources.icon_twins;
        public string[] modOptions = { "Randomize Crate Types" };

        public bool Twins_Randomize_CrateTypes = false;
        private string bdPath = "";

        public void StartModProcess()
        {
            Directory.CreateDirectory(Program.ModProgram.extractedPath + "/cml_extr/");
            bdPath = Program.ModProgram.extractedPath + "/cml_extr/";
            BDArchive mainBD = new BDArchive();
            File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD;1", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD");
            File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH;1", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH");
            //mainBD.LoadArchive(Program.ModProgram.extractedPath + "/CRASH6/", "CRASH.BD");
            mainBD.ExtractOnce(bdPath, Program.ModProgram.extractedPath + "/CRASH6/", "CRASH");
            //mainBD.Dispose();
            File.Delete(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD");
            File.Delete(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH");

            RM2 mainArchive = new RM2();
            mainArchive.LoadRM2(bdPath + "/Startup/Default.rm2"); // load rm2

            mainArchive.Item[1].Item[0].Item[3].ID = 5;
            mainArchive.Item[1].Item[0].Item[4].ID = 4;

            mainArchive.Recalculate();
            mainArchive.Save(bdPath + "/Startup/Default.rm2");

            mainBD = new BDArchive();
            mainBD.CreateTable(bdPath);
            mainBD.SaveTable(Program.ModProgram.extractedPath + "/CRASH6/", "CRASH");
            mainBD.SaveArchive(Program.ModProgram.extractedPath + "/CRASH6/", "CRASH");
            mainBD.Dispose();

            if (Directory.Exists(bdPath))
            {
                DirectoryInfo di = new DirectoryInfo(bdPath);

                foreach (FileInfo file in di.EnumerateFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.EnumerateDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(bdPath);
            }

            File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BD;1");
            File.Move(Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH", Program.ModProgram.extractedPath + "/CRASH6/CRASH.BH;1");
        }
    }
}
