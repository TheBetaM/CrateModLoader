/* API by BetaM.
 * Mod Layers:
 * 1: Zipdat.zip contents
 * Mod Passes:
 * lua -> LUA scripts (plain text) (Lua 5.0) (ps2/gcn) 
 * xom -> data containers
 * tga -> textures
 */
using CrateModLoader.GameSpecific.WormsForts;
namespace CrateModLoader.GameSpecific.Worms3D
{
    public sealed class Modder_Worms3D : Modder
    {
        public override async void StartModProcess()
        {
            if (!ModderHasPreloaded)
            {
                XOM_TYPE.GetSupported();
                FindArchives(new Pipeline_ZIP(this));
                await StartPipelines(PipelinePass.Extract);
            }

            FindFiles(new Parser_LUA(this), new Parser_XOM(this, WormsGame.W3D), new Parser_TGA(this, ModderIsPreloading));
            await StartNewPass();

            if (!ModderIsPreloading)
            {
                await StartPipelines(PipelinePass.Build);
            }

            ProcessBusy = false;
        }
    }
}
