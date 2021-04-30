using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CrateModLoader.GameSpecific.CrashTTR
{
    public class Pipeline_RCF : ModPipeline
    {
        private Dictionary<string, RCF_Manager> Managers;

        public Pipeline_RCF(Modder mod) : base(mod)
        {
            Managers = new Dictionary<string, RCF_Manager>();
        }

        public override List<string> Extensions => new List<string>() { ".RCF" };

        public override async Task ExtractObject(string filePath)
        {
            RCF_Manager rcf = new RCF_Manager(ExecutionSource, filePath);
            string fileName = Path.GetFileName(filePath);
            string dirPath = filePath.Substring(0, (filePath.Length - 4)) + @"\";

            await rcf.ExtractAsync(ExecutionSource, filePath, dirPath);

            Managers.Add(filePath, rcf);
        }

        public override async Task BuildObject(string filePath)
        {
            RCF_Manager rcf = Managers[filePath];
            string fileName = Path.GetFileName(filePath);
            string dirPath = filePath.Substring(0, (filePath.Length - 4)) + @"\";

            await rcf.PackAsync(filePath, dirPath);
        }
    }
}
