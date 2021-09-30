using System;
using System.Collections.Generic;
using System.IO;

namespace Twinsanity
{
    public class GameObjectDemo : TwinsItem
    {
        private int size;
        public uint UnkBitfield { get; set; }
        public List<Byte> ScriptSlots { get; set; } = new List<Byte>(); // Pairs;Scripts;GameObjects;UInt32s;Sounds;00;00;00 (last 3 are potentially a side effect of needing object name's length to be word aligned)
        public List<UInt32> UI32 { get; set; } = new List<UInt32>();
        public List<UInt16> OGIs { get; set; } = new List<UInt16>();
        public List<UInt16> Anims { get; set; } = new List<UInt16>();
        public List<UInt16> Scripts { get; set; } = new List<UInt16>();
        public List<UInt16> Objects { get; set; } = new List<UInt16>();
        public List<UInt16> Sounds { get; set; } = new List<UInt16>();
        //public uint PHeader { get; set; } // Inst;Pos;Path;00
        public uint PUI32 { get; set; }
        //private int pUi321.Length = 0;
        //private int pUi322.Length = 1;
        //private int pUi323.Length = 2;
        public List<UInt32> instFlagsList = new List<UInt32>();
        public List<Single> instFloatsList = new List<Single>();
        public List<UInt32> instIntegerList = new List<UInt32>();
        public uint flag;
        public List<UInt16> cObjects = new List<UInt16>();
        public List<UInt16> cOGIs = new List<UInt16>();
        public List<UInt16> cAnims = new List<UInt16>();
        public List<UInt16> cCM = new List<UInt16>();
        public List<UInt16> cScripts = new List<UInt16>();
        public List<UInt16> cUnk = new List<UInt16>();
        public List<UInt16> cSounds = new List<UInt16>();
        public int scriptCommandsAmount;
        public List<UInt16> scriptParams = new List<UInt16>();
        public int scriptGameVersion = 0;
        public Script.MainScript.ScriptCommand scriptCommand = null;
        public List<Script.MainScript.ScriptCommand> scriptCommands = new List<Script.MainScript.ScriptCommand>();

        public string Name { get; set; }
        public GameObjectDemo()
        {
            while (ScriptSlots.Count < 8)
            {
                ScriptSlots.Add(0);
            }
        }
        private void UpdateSlots()
        {
            ScriptSlots[0] = (Byte)OGIs.Count;
            ScriptSlots[1] = (Byte)Scripts.Count;
            ScriptSlots[2] = (Byte)Objects.Count;
            ScriptSlots[3] = (Byte)UI32.Count;
            ScriptSlots[4] = (Byte)Sounds.Count;
            ScriptSlots[5] = 0;
            ScriptSlots[6] = 0;
            ScriptSlots[7] = 0;
        }
        public override void Save(BinaryWriter writer)
        {
            var sk = writer.BaseStream.Position;
            UpdateSlots();
            writer.Write(UnkBitfield);
            /*
            for (int i = 0; i < 8; ++i)
            {
                writer.Write(ScriptSlots[i]);
            }
            */
            writer.Write((byte)OGIs.Count);
            //writer.Write(Anims.Count);
            writer.Write((byte)Scripts.Count);
            writer.Write((byte)Objects.Count);
            writer.Write((byte)UI32.Count);
            writer.Write(Sounds.Count);

            writer.Write(Name.Length);
            writer.Write(Name.ToCharArray());
            
            for (int i = 0; i < UI32.Count; ++i)
                writer.Write(UI32[i]);
            
            for (int i = 0; i < OGIs.Count; ++i)
                writer.Write(OGIs[i]);
            
            for (int i = 0; i < Anims.Count; ++i)
                writer.Write(Anims[i]);
            
            for (int i = 0; i < Scripts.Count; ++i)
                writer.Write(Scripts[i]);
            
            for (int i = 0; i < Objects.Count; ++i)
                writer.Write(Objects[i]);
            
            for (int i = 0; i < Sounds.Count; ++i)
                writer.Write(Sounds[i]);

            if ((UnkBitfield & 0x20000000) != 0x0)
            {
                //PHeader = (uint)((byte)instFlagsList.Count
                //| (instFloatsList.Count << 8)
                //| (instIntegerList.Count << 16));
                writer.Write((byte)instFlagsList.Count);
                writer.Write((byte)instFloatsList.Count);
                writer.Write((byte)instIntegerList.Count);
                writer.Write((byte)0);
                //writer.Write(PHeader);
                writer.Write(PUI32);
                for (int i = 0; i < instFlagsList.Count; ++i)
                    writer.Write(instFlagsList[i]);
                for (int i = 0; i < instFloatsList.Count; ++i)
                    writer.Write(instFloatsList[i]);
                for (int i = 0; i < instIntegerList.Count; ++i)
                    writer.Write(instIntegerList[i]);
            }

            if ((UnkBitfield & 0x40000000) != 0x0)
            {
                updateFlag();
                writer.Write(flag);
                if ((flag & 0x01) != 0)
                {
                    writer.Write(cObjects.Count);
                    for (int i = 0; i < cObjects.Count; ++i)
                        writer.Write(cObjects[i]);
                }
                if ((flag & 0x02) != 0)
                {
                    writer.Write(cOGIs.Count);
                    for (int i = 0; i < cOGIs.Count; ++i)
                        writer.Write(cOGIs[i]);
                }
                if ((flag & 0x04) != 0)
                {
                    writer.Write(cAnims.Count);
                    for (int i = 0; i < cAnims.Count; ++i)
                        writer.Write(cAnims[i]);
                }
                if ((flag & 0x08) != 0)
                {
                    writer.Write(cCM.Count);
                    for (int i = 0; i < cCM.Count; ++i)
                        writer.Write(cCM[i]);
                }
                if ((flag & 0x10) != 0)
                {
                    writer.Write(cScripts.Count);
                    for (int i = 0; i < cScripts.Count; ++i)
                        writer.Write(cScripts[i]);
                }
                if ((flag & 0x20) != 0)
                {
                    writer.Write(cUnk.Count);
                    for (int i = 0; i < cUnk.Count; ++i)
                        writer.Write(cUnk[i]);
                }
                if ((flag & 0x40) != 0)
                {
                    writer.Write(cSounds.Count);
                    for (int i = 0; i < cSounds.Count; ++i)
                        writer.Write(cSounds[i]);
                }
            }
            writer.Write(scriptCommandsAmount);
            if (scriptCommand != null)
            {
                scriptCommand.Write(writer);
            }
            size = (int)(writer.BaseStream.Position - sk);
        }

        public override void Load(BinaryReader reader, int size)
        {
            scriptGameVersion = 2;

            var sk = reader.BaseStream.Position;

            UnkBitfield = reader.ReadUInt32();
            /*
            for (int i = 0; i < 8; ++i)
            {
                ScriptSlots[i] = reader.ReadByte();
            }
            */

            var Count_OGI = reader.ReadByte();
            var Count_Anim = Count_OGI;
            var Count_Script = reader.ReadByte();
            var Count_GameObject = reader.ReadByte();
            var Count_UnkI32 = reader.ReadByte();
            //reader.ReadUInt32();
            var Count_Sound = reader.ReadUInt32();

            //Class1 = reader.ReadUInt32();
            //Class2 = reader.ReadUInt32();
            //Class3 = reader.ReadUInt32();
            var len = reader.ReadInt32();
            Name = new string(reader.ReadChars(len));

            // Read UInt32 script slots
            UI32.Clear();
            for (int i = 0; i < Count_UnkI32; ++i)
                UI32.Add(reader.ReadUInt32());

            // Read OGI script slots
            OGIs.Clear();
            for (int i = 0; i < Count_OGI; ++i)
                OGIs.Add(reader.ReadUInt16());

            // Read Animation script slots
            Anims.Clear();
            for (int i = 0; i < Count_Anim; ++i)
                Anims.Add(reader.ReadUInt16());

            // Read Script script slots
            Scripts.Clear();
            for (int i = 0; i < Count_Script; ++i)
                Scripts.Add(reader.ReadUInt16());

            // Read Object script slots
            Objects.Clear();
            for (int i = 0; i < Count_GameObject; ++i)
                Objects.Add(reader.ReadUInt16());

            // Read Sound script slots
            Sounds.Clear();
            for (int i = 0; i < Count_Sound; ++i)
                Sounds.Add(reader.ReadUInt16());

            // Read instance properties
            if ((UnkBitfield & 0x20000000) != 0x0)
            {
                //PHeader = reader.ReadUInt32();
                //reader.BaseStream.Position -= 4;
                var Count_Flags = reader.ReadByte();
                var Count_Floats = reader.ReadByte();
                var Count_Ints = reader.ReadByte();
                reader.ReadByte();
                PUI32 = reader.ReadUInt32();

                instFlagsList.Clear();
                for (int i = 0; i < Count_Flags; ++i)
                    instFlagsList.Add(reader.ReadUInt32());

                instFloatsList.Clear();
                for (int i = 0; i < Count_Floats; ++i)
                    instFloatsList.Add(reader.ReadSingle());

                instIntegerList.Clear();
                for (int i = 0; i < Count_Ints; ++i)
                    instIntegerList.Add(reader.ReadUInt32());
            }
            else
            {
                //PHeader = 0;
                PUI32 = 0;
                instFlagsList.Clear();
                instFloatsList.Clear();
                instIntegerList.Clear();
            }

            // Read IDs needed for instance creation
            if ((UnkBitfield & 0x40000000) != 0x0)
            {
                flag = reader.ReadUInt32();
                if ((flag & 0x00000001) != 0)
                {
                    var cnt = reader.ReadInt32();
                    cObjects.Clear();
                    for (int i = 0; i < cnt; ++i)
                        cObjects.Add(reader.ReadUInt16());
                }
                if ((flag & 0x00000002) != 0)
                {
                    var cnt = reader.ReadInt32();
                    cOGIs.Clear();
                    for (int i = 0; i < cnt; ++i)
                        cOGIs.Add(reader.ReadUInt16());
                }
                if ((flag & 0x00000004) != 0)
                {
                    var cnt = reader.ReadInt32();
                    cAnims.Clear();
                    for (int i = 0; i < cnt; ++i)
                        cAnims.Add(reader.ReadUInt16());
                }
                if ((flag & 0x00000008) != 0)
                {
                    var cnt = reader.ReadInt32();
                    cCM.Clear();
                    for (int i = 0; i < cnt; ++i)
                        cCM.Add(reader.ReadUInt16());
                }
                if ((flag & 0x00000010) != 0)
                {
                    var cnt = reader.ReadInt32();
                    cScripts.Clear();
                    for (int i = 0; i < cnt; ++i)
                        cScripts.Add(reader.ReadUInt16());
                }
                if ((flag & 0x00000020) != 0)
                {
                    var cnt = reader.ReadInt32();
                    cUnk.Clear();
                    for (int i = 0; i < cnt; ++i)
                        cUnk.Add(reader.ReadUInt16());
                }
                if ((flag & 0x00000040) != 0)
                {
                    var cnt = reader.ReadInt32();
                    cSounds.Clear();
                    for (int i = 0; i < cnt; ++i)
                        cSounds.Add(reader.ReadUInt16());
                }
            }

            scriptCommandsAmount = (int)reader.ReadUInt32();
            if (scriptCommandsAmount != 0)
            {
                scriptCommand = new Script.MainScript.ScriptCommand(reader, scriptGameVersion);
                var command = scriptCommand;
                do
                {
                    scriptCommands.Add(command);
                    command = command.nextCommand;
                } while (command != null);
            }
            else
            {
                scriptCommand = null;
            }
            this.size = size;
        }

        protected override int GetSize()
        {
            int oldSize = size;
            size = 0;

            size += 4;
            size += 8;
            size += 4;
            size += Name.Length;

            //size += 4;
            size += UI32.Count * 4;

            //size += 4;
            size += OGIs.Count * 2;

            //size += 4;
            size += Anims.Count * 2;

            //size += 4;
            size += Scripts.Count * 2;

            //size += 4;
            size += Objects.Count * 2;

            //size += 4;
            size += Sounds.Count * 2;

            if ((UnkBitfield & 0x20000000) != 0x0)
            {
                size += 4; // PHeader
                size += 4; // UnkInt

                //size += 4;
                size += instFlagsList.Count * 4;

                //size += 4;
                size += instFloatsList.Count * 4;

                //size += 4;
                size += instIntegerList.Count * 4;
            }
            updateFlag();
            if ((UnkBitfield & 0x40000000) != 0x0)
            {
                size += 4; // Flag

                if ((flag & 0x00000001) != 0)
                {
                    size += 4;
                    size += cObjects.Count * 2;
                }
                if ((flag & 0x00000002) != 0)
                {
                    size += 4;
                    size += cOGIs.Count * 2;
                }
                if ((flag & 0x00000004) != 0)
                {
                    size += 4;
                    size += cAnims.Count * 2;
                }
                if ((flag & 0x00000008) != 0)
                {
                    size += 4;
                    size += cCM.Count * 2;
                }
                if ((flag & 0x00000010) != 0)
                {
                    size += 4;
                    size += cScripts.Count * 2;
                }
                if ((flag & 0x00000020) != 0)
                {
                    size += 4;
                    size += cUnk.Count * 2;
                }
                if ((flag & 0x00000040) != 0)
                {
                    size += 4;
                    size += cSounds.Count * 2;
                }
            }
            size += 4; // Amount of script commands
            if (scriptCommand != null)
            {
                size += scriptCommand.GetLength();
            }
            return size;
        }
        private void updateFlag()
        {
            flag = 0;
            if (cObjects.Count > 0) flag |= 0x01;
            if (cOGIs.Count > 0) flag |= 0x02;
            if (cAnims.Count > 0) flag |= 0x04;
            if (cCM.Count > 0) flag |= 0x08;
            if (cScripts.Count > 0) flag |= 0x10;
            if (cUnk.Count > 0) flag |= 0x20;
            if (cSounds.Count > 0) flag |= 0x40;
        }
    }
}
