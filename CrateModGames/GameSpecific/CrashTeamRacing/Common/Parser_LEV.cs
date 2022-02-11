using System.Collections.Generic;
using System.IO;
using CTRFramework;
using CTRFramework.Shared;

namespace CrateModLoader.GameSpecific.CrashTeamRacing
{
    public class Parser_LEV : ModParser<CtrScene>
    {
        public Parser_LEV(Modder mod) : base(mod) { }

        public override List<string> Extensions => new List<string>() { ".LEV" };

        public override CtrScene LoadObject(string filePath)
        {
            return CtrScene.FromFile(filePath);
        }

        public override void SaveObject(CtrScene thing, string filePath)
        {
            WriteLEV(thing, filePath);
            thing.Dispose();
        }

        public void WriteLEV(CtrScene scn, string path)
        {

            using (var bw = new BinaryWriterEx(File.OpenWrite(path)))
            {
                bw.Flush();
                bw.Jump(4);

                scn.header.Write(bw);

                bw.Jump(scn.header.ptrRestartPts.Address + 4);

                foreach (var pose in scn.restartPts)
                    pose.Write(bw);

                bw.Jump(scn.header.ptrInstances.Address + 4);

                foreach (PickupHeader ph in scn.pickups)
                    ph.Write(bw);

                bw.Jump(scn.mesh.ptrVertices + 4);

                foreach (var vert in scn.verts)
                    vert.Write(bw);

                bw.Jump(scn.mesh.ptrQuadBlocks + 4);

                foreach (var qb in scn.quads)
                    qb.Write(bw);

                /*

                bw.Jump(scn.header.ptrVcolAnim.Address + 4);

                foreach (VertexAnim vc in scn.vertanims)
                    vc.Write(bw);
                */

                bw.Jump(scn.mesh.ptrVisData + 4);

                foreach (var vis in scn.visdata)
                    vis.Write(bw);

                //bw.Jump(scn.header.ptrAiNav.Address + 4);

                //scn.nav.Write(bw);


            }

        }

    }
}
