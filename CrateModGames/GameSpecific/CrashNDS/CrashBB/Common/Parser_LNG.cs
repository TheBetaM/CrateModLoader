using System;
using System.Collections.Generic;

namespace CrateModLoader.GameSpecific.CrashBB
{
    public class Parser_LNG : ModParser<LNG_File>
    {
        public Parser_LNG(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".LNG" };

        public override LNG_File LoadObject(string filePath)
        {
            LNG_File lng = new LNG_File();
            try
            {
                //lng.Read(filePath);
            }
            catch
            {
                Console.WriteLine("HELP " + filePath);
            }

            return lng;
        }

        public override void SaveObject(LNG_File thing, string filePath)
        {
            try
            {
                //thing.Write(filePath);
            }
            catch
            {
                Console.WriteLine("SAVE ME " + filePath);
            }
        }
    }
}
