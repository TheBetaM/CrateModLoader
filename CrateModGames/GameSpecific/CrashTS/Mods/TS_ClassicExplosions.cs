using System;
using System.Collections.Generic;
using Twinsanity;
using CrateModGames.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.CrashTS.Mods
{
    //todo needs testing
    public class TS_ClassicExplosions : ModStruct<ChunkInfoRM>
    {
        public override string Name => Twins_Text.Mod_ClassicExplosionDaamge;
        public override string Description => Twins_Text.Mod_ClassicExplosionDamageDesc;
        public override bool NeedsCachePass => true;

        private Script GenericCrateExplode = null;

        public override void CachePass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            if (GenericCrateExplode == null)
            {
                if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
                {
                    TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);
                    if (code_section.ContainsItem((uint)RM_Code_Sections.Script))
                    {
                        TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
                        if (script_section.Records.Count > 0)
                        {
                            if (script_section.ContainsItem((uint)ScriptID.COM_GENERIC_CRATE_EXPLODE))
                            {
                                Script scr = script_section.GetItem<Script>((int)ScriptID.COM_GENERIC_CRATE_EXPLODE);
                                GenericCrateExplode = scr;

                                Script.MainScript.ScriptState state = GenericCrateExplode.Main.scriptState1;
                                for (int s = 0; s < 3; s++)
                                {
                                    state = state.nextState;
                                }
                                Script.MainScript.ScriptCommand command = state.scriptStateBody.command;
                                while (command.VTableIndex != (ushort)DefaultEnums.CommandID.CreateDamageZone)
                                {
                                    command = command.nextCommand;
                                }
                                command.arguments[7] = 8;
                            }
                        }
                    }
                }
            }
        }

        public override void ModPass(ChunkInfoRM info)
        {
            TwinsFile RM_Archive = info.File;

            if (RM_Archive.ContainsItem((uint)RM_Sections.Code))
            {
                TwinsSection code_section = RM_Archive.GetItem<TwinsSection>((uint)RM_Sections.Code);

                if (code_section.ContainsItem((uint)RM_Code_Sections.Script))
                {
                    TwinsSection script_section = code_section.GetItem<TwinsSection>((uint)RM_Code_Sections.Script);
                    if (script_section.Records.Count > 0)
                    {
                        if (script_section.ContainsItem((uint)ScriptID.COM_GENERIC_CRATE_EXPLODE))
                        {
                            script_section.RemoveItem((uint)ScriptID.COM_GENERIC_CRATE_EXPLODE);
                            script_section.Records.Add(GenericCrateExplode);
                        }
                    }
                }
            }
        }
    }
}
