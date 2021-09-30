using System;
using System.Collections.Generic;
using Twinsanity;

namespace TwinsaityEditor.Controllers
{
    class CodeModelController : ItemController
    {
        public new CodeModel Data { get; set; }

        public CodeModelController(MainForm topform, CodeModel item) : base(topform, item)
        {
            Data = item;
        }

        protected override void GenText()
        {
            List<string> text = new List<string>();
            text.Add($"ID: {Data.ID}");
            text.Add($"Size: {Data.Size}");
            text.Add($"Header: 0x{Data.Header:X}");
            text.Add($"AgentLab additions: {Data.ArraySize}");
            for (var i = 0; i < Data.ArraySize; ++i)
            {
                text.Add($"Addition {i} - Script ID: {Data.scriptIds[i]}");
                var agentLabAddition = Data.agentLabAdditionsList[i];
                text.Add($"\tCommands amount: {agentLabAddition.scriptCommandsAmount}");
                if (agentLabAddition.scriptCommandsAmount > 0)
                {
                    var command = agentLabAddition.scriptCommand;
                    do
                    {
                        if (Enum.IsDefined(typeof(DefaultEnums.CommandID), command.VTableIndex))
                        {
                            text.Add($"\t{(DefaultEnums.CommandID)command.VTableIndex}: {command.VTableIndex}");
                        }
                        else
                        {
                            text.Add($"\t{command.VTableIndex}");
                        }
                        command = command.nextCommand;
                    } while (command != null);
                }
            }
            text.Add($"Unk AgentLab addition");
            var cmd = Data.scriptCommand;
            do
            {
                if (Enum.IsDefined(typeof(DefaultEnums.CommandID), cmd.VTableIndex))
                {
                    text.Add($"{(DefaultEnums.CommandID)cmd.VTableIndex}: {cmd.VTableIndex}");
                }
                else
                {
                    text.Add($"{cmd.VTableIndex}");
                }
                cmd = cmd.nextCommand;
            } while (cmd != null);
            TextPrev = text.ToArray();
        }

        protected override string GetName()
        {
            return $"Code Model [ID {Data.ID}]";
        }
    }
}
