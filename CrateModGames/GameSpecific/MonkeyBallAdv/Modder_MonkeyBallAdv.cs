using System;
using System.Collections.Generic;
using CrateModLoader.GameSpecific.CrashTS;

namespace CrateModLoader.GameSpecific.MonkeyBallAdv
{
    public sealed class Modder_MonkeyBallAdv : Modder
    {
        public override async void StartModProcess()
        {
            string bdPath = ConsolePipeline.ExtractedPath;

            #region Extract BD
            // Extract BD (PS2/PSP only)
            if (ConsolePipeline.Metadata.Console != ConsoleMode.GCN)
            {
                UpdateProcessMessage("Extracting MB.BD...", 0);
                bdPath = System.IO.Path.Combine(ConsolePipeline.ExtractedPath, @"MB\");

                FindArchives(new Pipeline_BD(this));
                await StartPipelines(PipelinePass.Extract);
            }
            #endregion

            StartModPass(bdPath);
            if (ConsolePipeline.Metadata.Console == ConsoleMode.PSP) //PS2 level editing not functional yet
                FindFiles(new Parser_XML(this), 
                    new Parser_RM(this, ConsolePipeline.Metadata.Console)
                    ,new Parser_SM(this, ConsolePipeline.Metadata.Console));
            else
                FindFiles(new Parser_XML(this));
            await StartNewPass();

            #region Build BD
            // Build BD
            if (ConsolePipeline.Metadata.Console != ConsoleMode.GCN)
            {
                UpdateProcessMessage("Building MB.BD...", 95);
                await StartPipelines(PipelinePass.Build);
            }
            #endregion

            ProcessBusy = false;
        }
    }
}
