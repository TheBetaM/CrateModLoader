using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.CrashTitans
{
    public class Titans_TestMod : ModStruct<string>
    {
        public override string Name => "Test Mod: Wide camera angle in Episode 1";

        public override void ModPass(string path_extr)
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
    }
}
