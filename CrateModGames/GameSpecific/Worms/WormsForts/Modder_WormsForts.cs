/* API by BetaM.
 * Mod Layers:
 * 1: Data.zip contents
 * Mod Passes:
 * lua -> LUA scripts (plain text) (ps2) (Lua 5.0.1)
 * lub -> compiled LUA scripts (xbox) (standard Lua 5.0.1 compiler) (not yet implemented) 
 * xom -> data containers
 * tga -> textures
 * csv -> bitmap configs
 */
using System.Collections.Generic;
using CrateModLoader.GameSpecific.CrashNitroKart;

namespace CrateModLoader.GameSpecific.WormsForts
{
    public sealed class Modder_WormsForts : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                XOM_TYPE.GetSupported();
                FindArchives(new Pipeline_ZIP(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles(new Parser_LUA(this), new Parser_XOM(this, WormsGame.Forts, ModderIsPreloading), new Parser_CSV(this), new Parser_TGA(this, ModderIsPreloading));
            await StartNewPass();

            if (!ModderIsPreloading)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
