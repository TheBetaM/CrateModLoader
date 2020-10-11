using RadcoreCementFile;
using System;
using System.Collections.Generic;
using System.IO;
using CrateModLoader.GameSpecific.CrashTTR;
using CrateModLoader.ModProperties;
//RCF API by NeoKesha
/* Mod Layers:
 * 1: Default.RCF contents (only replace files)
 */

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public sealed class Modder_Titans : Modder
    {

        public override Game Game => new Game()
        {
            Name = "Crash of the Titans",
            ShortName = "CrashTitans",
            Consoles = new List<ConsoleMode>
                {
                    ConsoleMode.PS2,
                    ConsoleMode.PSP,
                    ConsoleMode.WII,
                    ConsoleMode.XBOX360,
                },
            API_Credit = "API by NeoKesha and BetaM",
            API_Link = string.Empty,
            RegionID = new Dictionary<ConsoleMode, RegionCode[]>()
            {
                [ConsoleMode.PS2] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = @"SLUS_215.83",
                    Region = RegionType.NTSC_U,
                    ExecName = "SLUS_215.83",
                    CodeName = "SLUS_21583", },
                    new RegionCode() {
                    Name = @"SLES_548.41",
                    Region = RegionType.PAL,
                    ExecName = "SLES_548.41",
                    CodeName = "SLES_54841", },
                },
                [ConsoleMode.PSP] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "ULUS-10304",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "ULES-00917",
                    Region = RegionType.PAL },
                },
                [ConsoleMode.WII] = new RegionCode[]
                {
                    new RegionCode() {
                    Name = "RQJE7D",
                    Region = RegionType.NTSC_U },
                    new RegionCode() {
                    Name = "RQJP7D",
                    Region = RegionType.PAL },
                    new RegionCode() {
                    Name = "RQJX7D",
                    Region = RegionType.PAL },
                },
                [ConsoleMode.XBOX360] = new RegionCode[]
                {
                    new RegionCode() {
                        Name = "Crash Of The Titans",
                        Region = RegionType.Global, }
                },
            },
            PropertyCategories = new Dictionary<int, string>()
            {
                [0] = "Options",
            },
        };

        public static ModPropOption Option_TestMod = new ModPropOption("Test Mod: Wide camera angle in Episode 1", "");

        public Modder_Titans()
        {

        }

        private string basePath = "";

        public override void StartModProcess()
        {
            string path_RCF_frontend = "DEFAULT.RCF";
            basePath = ConsolePipeline.ExtractedPath;
            RCF_Manager.cachedRCF = null;

            if (ConsolePipeline.Metadata.Console == ConsoleMode.WII)
            {
                path_RCF_frontend = "default.rcf";
            }
            else if (ConsolePipeline.Metadata.Console == ConsoleMode.PSP)
            {
                path_RCF_frontend = "default.rcf";
            }
            else if  (ConsolePipeline.Metadata.Console == ConsoleMode.XBOX360)
            {
                path_RCF_frontend = "default.rcf";
            }

            string path_extr = basePath + @"cml_extr\";
            
            RCF_Manager.Extract(basePath + path_RCF_frontend, path_extr);

            ModCrates.InstallLayerMods(path_extr, 1);

            if (Option_TestMod.Enabled)
            {
                // Proof of concept mod increasing gameplay FOV in Episode 1
                string[] frontend_lines = File.ReadAllLines(path_extr + @"levels\L1_E1\cameraoverrides.blua");
                frontend_lines[6] = "cameraManager:SetCameraVolumeFOV( 0, 90.000000 )";
                frontend_lines[11] = "cameraManager:SetCameraVolumeFOV( 2, 90.000000 )";
                frontend_lines[14] = "cameraManager:SetCameraVolumeFOV( 3, 90.000000 )";
                frontend_lines[68] = "cameraManager:SetCameraVolumeFOV( 78, 90.000000 )";
                frontend_lines[71] = "cameraManager:SetCameraVolumeFOV( 79, 90.000000 )";
                frontend_lines[73] = "cameraManager:SetCameraVolumeFOV( 80, 90.000000 )";
                frontend_lines[76] = "cameraManager:SetCameraVolumeFOV( 81, 90.000000 )";
                frontend_lines[79] = "cameraManager:SetCameraVolumeFOV( 82, 90.000000 )";
                frontend_lines[81] = "cameraManager:SetCameraVolumeFOV( 83, 90.000000 )";
                frontend_lines[83] = "cameraManager:SetCameraVolumeFOV( 84, 90.000000 )";
                frontend_lines[85] = "cameraManager:SetCameraVolumeFOV( 85, 90.000000 )";
                frontend_lines[87] = "cameraManager:SetCameraVolumeFOV( 86, 90.000000 )";
                frontend_lines[89] = "cameraManager:SetCameraVolumeFOV( 87, 90.000000 )";
                frontend_lines[91] = "cameraManager:SetCameraVolumeFOV( 88, 90.000000 )";
                frontend_lines[94] = "cameraManager:SetCameraVolumeFOV( 89, 90.000000 )";
                frontend_lines[97] = "cameraManager:SetCameraVolumeFOV( 90, 90.000000 )";
                File.WriteAllLines(path_extr + @"levels\L1_E1\cameraoverrides.blua", frontend_lines);
            }

            RCF_Manager.Pack(basePath + path_RCF_frontend, path_extr);
        }
    }
}
