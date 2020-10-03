using System;
using System.IO;
using System.Diagnostics;

namespace CrateModLoader.ModPipelines
{

    public class ModPipeline_XBOX : ModPipeline
    {

        public override ModPipelineInfo Metadata => new ModPipelineInfo()
        {
            Console = ConsoleMode.XBOX,
            Layer = 0,
            NeedsDetection = true,
        };

        public ModPipeline_XBOX()
        {

        }

        public override bool DetectROM(string inputPath, out string titleID, out uint regionID)
        {
            titleID = null;
            regionID = 0;
            
            string args = "-i -x ";
            args += "\"" + inputPath + "\"";

            //Modified extract-xiso only extracts the executables to check

            Process ExtractorProcess = new Process();
            ExtractorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "extract-xiso.exe";
            ExtractorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            ExtractorProcess.StartInfo.Arguments = args;
            //ExtractorProcess.Start();

            ExtractorProcess.StartInfo.UseShellExecute = false;
            //ExtractorProcess.StartInfo.RedirectStandardOutput = true;
            ExtractorProcess.StartInfo.CreateNoWindow = true;
            ExtractorProcess.Start();
            //string outputMessage = ExtractorProcess.StandardOutput.ReadToEnd();
            //Console.WriteLine(outputMessage);

            ExtractorProcess.WaitForExit();

            if (Directory.Exists(ModLoaderGlobals.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(inputPath)))
            {
                Directory.Move(ModLoaderGlobals.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(inputPath), ModLoaderGlobals.TempPath);
            }

            if (Directory.Exists(ModLoaderGlobals.TempPath) && File.Exists(ModLoaderGlobals.TempPath + @"default.xbe"))
            {
                //Based on OpenXDK
                using (FileStream fileStream = new FileStream(ModLoaderGlobals.TempPath + @"default.xbe", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    fileStream.Seek(0x0118, SeekOrigin.Begin);
                    BinaryReader reader = new BinaryReader(fileStream);
                    uint CertOffset = reader.ReadUInt16();
                    fileStream.Seek(CertOffset, SeekOrigin.Begin);
                    fileStream.Seek(CertOffset + 0x0008, SeekOrigin.Begin);
                    uint CertID = reader.ReadUInt32();
                    fileStream.Seek(CertOffset + 0x000C, SeekOrigin.Begin);
                    byte[] CertNameUnicode = new byte[2];
                    string TitleName = "";
                    for (int i = 0; i < 40; i++)
                    {
                        CertNameUnicode[0] = reader.ReadByte();
                        CertNameUnicode[1] = reader.ReadByte();
                        TitleName += System.Text.Encoding.Unicode.GetString(CertNameUnicode);
                    }
                    fileStream.Seek(CertOffset + 0x00A0, SeekOrigin.Begin);
                    uint CertRegion = reader.ReadUInt32();
                    fileStream.Seek(CertOffset + 0x00AC, SeekOrigin.Begin);
                    uint CertVersion = reader.ReadUInt32();

                    /*
                    Console.WriteLine("Cert offset: " + CertOffset.ToString("X"));
                    Console.WriteLine("Cert Title ID: " + CertID);
                    Console.WriteLine("Cert Region: " + CertRegion);
                    Console.WriteLine("Cert Version: " + CertVersion);
                    Console.WriteLine("Cert Name: " + TitleName);
                    */

                    titleID = TitleName;
                    regionID = CertRegion;
                    return true;
                }
            }
            return false;
        }

        public override bool DetectFolder(string inputPath, out string titleID, out uint regionID)
        {
            regionID = 0;
            titleID = null;
            if (File.Exists(inputPath + @"default.xbe"))
            {
                //Based on OpenXDK
                using (FileStream fileStream = new FileStream(inputPath + @"default.xbe", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    fileStream.Seek(0x0118, SeekOrigin.Begin);
                    BinaryReader reader = new BinaryReader(fileStream);
                    uint CertOffset = reader.ReadUInt16();
                    fileStream.Seek(CertOffset, SeekOrigin.Begin);
                    fileStream.Seek(CertOffset + 0x0008, SeekOrigin.Begin);
                    uint CertID = reader.ReadUInt32();
                    fileStream.Seek(CertOffset + 0x000C, SeekOrigin.Begin);
                    byte[] CertNameUnicode = new byte[2];
                    string TitleName = "";
                    for (int i = 0; i < 40; i++)
                    {
                        CertNameUnicode[0] = reader.ReadByte();
                        CertNameUnicode[1] = reader.ReadByte();
                        TitleName += System.Text.Encoding.Unicode.GetString(CertNameUnicode);
                    }
                    fileStream.Seek(CertOffset + 0x00A0, SeekOrigin.Begin);
                    uint CertRegion = reader.ReadUInt32();
                    fileStream.Seek(CertOffset + 0x00AC, SeekOrigin.Begin);
                    uint CertVersion = reader.ReadUInt32();

                    /*
                    Console.WriteLine("Cert offset: " + CertOffset.ToString("X"));
                    Console.WriteLine("Cert Title ID: " + CertID);
                    Console.WriteLine("Cert Region: " + CertRegion);
                    Console.WriteLine("Cert Version: " + CertVersion);
                    Console.WriteLine("Cert Name: " + TitleName);
                    */

                    titleID = TitleName;
                    regionID = CertRegion;
                    return true;
                }
            }
            return false;
        }

        public override void Build(string inputPath, string outputPath)
        {
            //Use extract-xiso
            string args = "-c ";
            args += inputPath + " ";
            args += "\"" + outputPath + "\" ";

            Process ISOcreatorProcess = new Process();
            ISOcreatorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "extract-xiso.exe";
            ISOcreatorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            ISOcreatorProcess.StartInfo.Arguments = args;
            ISOcreatorProcess.Start();

            ISOcreatorProcess.WaitForExit();
        }

        public override void Extract(string inputPath, string outputPath)
        {
            // TODO: add free space checks
            string args = "-x ";
            args += "\"" + inputPath + "\" ";

            Process ExtractorProcess = new Process();
            ExtractorProcess.StartInfo.FileName = ModLoaderGlobals.ToolsPath + "extract-xiso.exe";
            ExtractorProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            ExtractorProcess.StartInfo.Arguments = args;
            ExtractorProcess.Start();
            ExtractorProcess.WaitForExit();

            // this may not work with an arbitrary output path, since it assumes it's on the same drive
            Directory.Move(ModLoaderGlobals.BaseDirectory + @"\" + Path.GetFileNameWithoutExtension(inputPath), outputPath);
        }

    }
}