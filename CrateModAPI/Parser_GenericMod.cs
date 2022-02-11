using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CrateModLoader
{
    public class Parser_GenericMod : ModParser<GenericModStruct>
    {
        public Parser_GenericMod(Modder mod) : base(mod) { }

        public override List<string> Extensions => null;

        public override GenericModStruct LoadObject(string filePath)
        {
            return ExecutionSource.GenericModStruct;
        }

        public override void SaveObject(GenericModStruct thing, string filePath)
        {
            return;
        }

        public override async Task StartPass(ModPass pass = ModPass.Mod)
        {
            await Task.Run(
                () =>
                {
                    ExecutionSource.StartPass(ExecutionSource.GenericModStruct, pass);
                }
                );

            SkipParser = true; //So that it only gets executed once
        }
    }
}
