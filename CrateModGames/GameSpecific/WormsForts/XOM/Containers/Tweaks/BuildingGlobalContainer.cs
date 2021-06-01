using System;
using System.Collections.Generic;
using System.IO;

namespace CrateModLoader.GameSpecific.WormsForts.XOM
{
    [XOM_TypeName("BuildingGlobalContainer")]
    public class BuildingGlobalContainer : Container
    {
        public float LinkHeightDistance = 3f;
        public float DistanceFromBuildingToDropCrates = 2400f;
        public ushort AdditionalStrongholdHealthPerVictoryPoint = 300;
        public byte BuildDistance = 1;
        public byte NumTurnsEarthRemainsScorched = 2;
        public byte NumTurnsToHoldWonder = 2;
        public byte MaxCratesToHoldAtRefinery = 10;
        public byte NumCratesToSpawnAtRefineryPerTurn = 3;
        public byte NumTurnsSuperScorchedMode = 3;

        public override void Read(BinaryReader reader)
        {
            LinkHeightDistance = reader.ReadSingle();
            DistanceFromBuildingToDropCrates = reader.ReadSingle();
            AdditionalStrongholdHealthPerVictoryPoint = reader.ReadUInt16();
            BuildDistance = reader.ReadByte();
            NumTurnsEarthRemainsScorched = reader.ReadByte();
            NumTurnsToHoldWonder = reader.ReadByte();
            MaxCratesToHoldAtRefinery = reader.ReadByte();
            NumCratesToSpawnAtRefineryPerTurn = reader.ReadByte();
            NumTurnsSuperScorchedMode = reader.ReadByte();
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(LinkHeightDistance);
            writer.Write(DistanceFromBuildingToDropCrates);
            writer.Write(AdditionalStrongholdHealthPerVictoryPoint);
            writer.Write(BuildDistance);
            writer.Write(NumTurnsEarthRemainsScorched);
            writer.Write(NumTurnsToHoldWonder);
            writer.Write(MaxCratesToHoldAtRefinery);
            writer.Write(NumCratesToSpawnAtRefineryPerTurn);
            writer.Write(NumTurnsSuperScorchedMode);
        }
    }
}
