using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity
{
    public class CodeModel : TwinsItem
    {
        public UInt32 Header;
        public class AgentLabAdditions
        {
            public int scriptCommandsAmount;
            public Script.MainScript.ScriptCommand scriptCommand = null;

            public AgentLabAdditions(int ver)
            {
                scriptCommand = new Script.MainScript.ScriptCommand(ver);
            }
        }
        public List<AgentLabAdditions> agentLabAdditionsList = new List<AgentLabAdditions>();
        public List<UInt16> scriptIds = new List<UInt16>();
        public Script.MainScript.ScriptCommand scriptCommand = null;
        public int scriptGameVersion;
        private uint arraySize;
        public uint ArraySize
        {
            set
            {
                if (value >= 255)
                {
                    throw new Exception("Array can't be bigger than the max number in a byte");
                }
                Header = (Header & 0xFF00FFFF) | (value << 16);
                arraySize = value;
            }
            get
            {
                return arraySize;
            }
        }

        protected override int GetSize()
        {
            var totalSize = 4; // Header
            foreach (var agentLabAddition in agentLabAdditionsList)
            {
                totalSize += 4;
                if (agentLabAddition.scriptCommandsAmount > 0)
                {
                    totalSize += agentLabAddition.scriptCommand.GetLength();
                }
                totalSize += 2;
            }
            totalSize += scriptCommand.GetLength();
            return totalSize;
        }

        public override void Save(BinaryWriter writer)
        {
            var sk = writer.BaseStream.Position;
            writer.Write(Header);
            for (var i = 0; i < arraySize; ++i)
            {
                writer.Write(agentLabAdditionsList[i].scriptCommandsAmount);
                if (agentLabAdditionsList[i].scriptCommandsAmount > 0)
                {
                    agentLabAdditionsList[i].scriptCommand.Write(writer);
                }
                writer.Write(scriptIds[i]);
            }
            scriptCommand.Write(writer);
        }

        public override void Load(BinaryReader reader, int size)
        {
            if (ParentType == SectionType.CodeModelX)
            {
                scriptGameVersion = 1;
            }
            else if (ParentType == SectionType.CodeModelDemo)
            {
                scriptGameVersion = 2;
            }
            else
            {
                scriptGameVersion = 0;
            }

            Header = reader.ReadUInt32();
            arraySize = (Header >> 16 & 0xFF);
            for (var i = 0; i < arraySize; ++i)
            {
                var agentLabAddition = new AgentLabAdditions(scriptGameVersion);
                agentLabAddition.scriptCommandsAmount = reader.ReadInt32();
                if (agentLabAddition.scriptCommandsAmount > 0)
                {
                    agentLabAddition.scriptCommand = new Script.MainScript.ScriptCommand(reader, scriptGameVersion);
                }
                agentLabAdditionsList.Add(agentLabAddition);
                scriptIds.Add(reader.ReadUInt16());
            }
            scriptCommand = new Script.MainScript.ScriptCommand(reader, scriptGameVersion);
        }
    }
}
