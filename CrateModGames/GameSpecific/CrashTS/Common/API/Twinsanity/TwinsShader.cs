using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity
{
    public class TwinsShader
    {
        public UInt32 ShaderType { get; set; }
        public UInt32 IntParam { get; set; }
        public Single[] FloatParam { get; private set; }
        public AlphaBlending ABlending;
        byte AlphaRegSettingsIndex;
        public AlphaTest ATest;
        public AlphaTestMethod ATestMethod;
        public byte AlphaValueToBeComparedTo;
        public ProcessAfterAlphaTestFailed ProcessMethodWhenAlphaTestFailed;
        public DestinationAlphaTest DAlphaTest;
        public DestinationAlphaTestMode DAlphaTestMode;
        public DepthTestMethod DepthTest;
        public byte UnkVal1;
        public ShadingMethod ShdMethod;
        public TextureMapping TxtMapping;
        public TextureCoordinatesSpecification MethodOfSpecifyingTextureCoordinates;
        public Fogging Fog;
        public Context ContextNum;
        public byte UnkVal2;
        public byte UnkVal3;
        public bool UsePresetAlphaRegSettings;
        public ColorSpecMethod SpecOfColA;
        public ColorSpecMethod SpecOfColB;
        public AlphaSpecMethod SpecOfAlphaC;
        public ColorSpecMethod SpecOfColD;
        public byte FixedAlphaValue;
        public TextureFilter TextureFilterWhenTextureIsExpanded;
        public bool AlphaCorrectionValue;
        public bool UnkFlag1;
        public bool UnkFlag2;
        public ZValueDrawMask ZValueDrawingMask;
        public bool UnkFlag3;
        public bool BlobFlag; // Technically blobs have never been encountered before yet
        // For LOD level calculation the following formula is used: LOD = (log2(1/Q)<<L)+K, applicable only to STQ TextureCoordinatesSpecification
        public UInt16 LodParamK { get; set; }
        public UInt16 LodParamL { get; set; }
        public TwinsVector4 UnkVector1 { get; private set; }
        public TwinsVector4 UnkVector2 { get; private set; }
        public TwinsVector4 UnkVector3 { get; private set; }
        public UInt32 TextureId { get; set; }
        public bool isDemo { get; set; }
        
        public TwinsShader()
        {
            FloatParam = new float[4];
            UnkVector1 = new TwinsVector4();
            UnkVector2 = new TwinsVector4();
            UnkVector3 = new TwinsVector4();
        }
        public int GetLength()
        {
            int paramLen = (ShaderType == 23) ? 12 :
                            (ShaderType == 26) ? 20 :
                            (ShaderType == 16 || ShaderType == 17) ? 4 :
                            0;
            if (isDemo)
            {
                paramLen -= 2;
            }
            return 4 + paramLen + 30 + 4 + 16 * 3 + 8;
        }

        public void Read(BinaryReader reader, int length, bool Demo = false)
        {
            ShaderType = reader.ReadUInt32();
            switch (ShaderType)
            {
                case 23:
                    IntParam = reader.ReadUInt32();
                    FloatParam[0] = reader.ReadSingle();
                    FloatParam[1] = reader.ReadSingle();
                    break;
                case 26:
                    IntParam = reader.ReadUInt32();
                    FloatParam[0] = reader.ReadSingle();
                    FloatParam[1] = reader.ReadSingle();
                    FloatParam[2] = reader.ReadSingle();
                    FloatParam[3] = reader.ReadSingle();
                    break;
                case 16:
                case 17:
                    FloatParam[0] = reader.ReadSingle();
                    break;
                default:
                    break;
            }
            ABlending = (AlphaBlending)reader.ReadByte();
            AlphaRegSettingsIndex = reader.ReadByte();
            ATest = (AlphaTest)reader.ReadByte();
            ATestMethod = (AlphaTestMethod)reader.ReadByte();
            AlphaValueToBeComparedTo = reader.ReadByte();
            ProcessMethodWhenAlphaTestFailed = (ProcessAfterAlphaTestFailed)reader.ReadByte();
            DAlphaTest = (DestinationAlphaTest)reader.ReadByte();
            DAlphaTestMode = (DestinationAlphaTestMode)reader.ReadByte();
            DepthTest = (DepthTestMethod)reader.ReadByte();
            UnkVal1 = reader.ReadByte();
            ShdMethod = (ShadingMethod)reader.ReadByte();
            TxtMapping = (TextureMapping)reader.ReadByte();
            MethodOfSpecifyingTextureCoordinates = (TextureCoordinatesSpecification)reader.ReadByte();
            Fog = (Fogging)reader.ReadByte();
            ContextNum = (Context)reader.ReadByte();
            UnkVal2 = reader.ReadByte();
            UnkVal3 = reader.ReadByte();
            UsePresetAlphaRegSettings = reader.ReadBoolean();
            SpecOfColA = (ColorSpecMethod)reader.ReadByte();
            SpecOfColB = (ColorSpecMethod)reader.ReadByte();
            SpecOfAlphaC = (AlphaSpecMethod)reader.ReadByte();
            SpecOfColD = (ColorSpecMethod)reader.ReadByte();
            FixedAlphaValue = reader.ReadByte();
            TextureFilterWhenTextureIsExpanded = (TextureFilter)reader.ReadByte();
            AlphaCorrectionValue = reader.ReadBoolean();
            UnkFlag1 = reader.ReadBoolean();
            UnkFlag2 = reader.ReadBoolean();
            ZValueDrawingMask = (ZValueDrawMask)reader.ReadByte();
            if (!Demo)
            {
                UnkFlag3 = reader.ReadBoolean();
                BlobFlag = reader.ReadBoolean();
                isDemo = false;
            }
            else
            {
                UnkFlag3 = false;
                BlobFlag = false;
                isDemo = true;
            }
            LodParamK = reader.ReadUInt16();
            LodParamL = reader.ReadUInt16();
            UnkVector1.Load(reader, 16);
            UnkVector2.Load(reader, 16);
            UnkVector3.Load(reader, 16);
            TextureId = reader.ReadUInt32();
            reader.ReadUInt32(); // Shader type
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(ShaderType);
            switch (ShaderType)
            {
                case 23:
                    writer.Write(IntParam);
                    writer.Write(FloatParam[0]);
                    writer.Write(FloatParam[1]);
                    break;
                case 26:
                    writer.Write(IntParam);
                    writer.Write(FloatParam[0]);
                    writer.Write(FloatParam[1]);
                    writer.Write(FloatParam[2]);
                    writer.Write(FloatParam[3]);
                    break;
                case 16:
                case 17:
                    writer.Write(FloatParam[0]);
                    break;
                default:
                    break;
            }
            writer.Write((byte)ABlending);
            writer.Write(AlphaRegSettingsIndex);
            writer.Write((byte)ATest);
            writer.Write((byte)ATestMethod);
            writer.Write(AlphaValueToBeComparedTo);
            writer.Write((byte)ProcessMethodWhenAlphaTestFailed);
            writer.Write((byte)DAlphaTest);
            writer.Write((byte)DAlphaTestMode);
            writer.Write((byte)DepthTest);
            writer.Write(UnkVal1);
            writer.Write((byte)ShdMethod);
            writer.Write((byte)TxtMapping);
            writer.Write((byte)MethodOfSpecifyingTextureCoordinates);
            writer.Write((byte)Fog);
            writer.Write((byte)ContextNum);
            writer.Write(UnkVal2);
            writer.Write(UnkVal3);
            writer.Write(UsePresetAlphaRegSettings);
            writer.Write((byte)SpecOfColA);
            writer.Write((byte)SpecOfColB);
            writer.Write((byte)SpecOfAlphaC);
            writer.Write((byte)SpecOfColD);
            writer.Write(FixedAlphaValue);
            writer.Write((byte)TextureFilterWhenTextureIsExpanded);
            writer.Write(AlphaCorrectionValue);
            writer.Write(UnkFlag1);
            writer.Write(UnkFlag2);
            writer.Write((byte)ZValueDrawingMask);
            if (!isDemo)
            {
                writer.Write(UnkFlag3);
                writer.Write(BlobFlag);
            }
            writer.Write(LodParamK);
            writer.Write(LodParamL);
            UnkVector1.Save(writer);
            UnkVector2.Save(writer);
            UnkVector3.Save(writer);
            writer.Write(TextureId);
            writer.Write(ShaderType);
        }

        // These are mostly helper enums to help keep stuff type safe
        #region Enums
        public enum AlphaBlending
        {
            OFF,
            ON
        }
        public enum AlphaTest
        {
            OFF,
            ON
        }
        public enum AlphaTestMethod
        {
            NEVER       = 0b000,
            ALWAYS      = 0b001,
            LESS        = 0b010,
            LEQUAL      = 0b011,
            EQUAL       = 0b100,
            GEQUAL      = 0b101,
            GREATER     = 0b110,
            NOTEQUAL    = 0b111
        }
        public enum ProcessAfterAlphaTestFailed
        {
            KEEP        = 0b00,
            FB_ONLY     = 0b01,
            ZB_ONLY     = 0b10,
            RGB_ONLY    = 0b11
        }
        public enum DestinationAlphaTest
        {
            OFF,
            ON
        }
        public enum DestinationAlphaTestMode
        {
            Alpha0Pass = 0,
            Alpha1Pass = 1
        }
        public enum DepthTestMethod
        {
            NEVER   = 0b00,
            ALWAYS  = 0b01,
            GEQUAL  = 0b10,
            GREATER = 0b11
        }
        public enum ShadingMethod
        {
            FLAT    = 0,
            GOURAND = 1
        }
        public enum TextureMapping
        {
            OFF,
            ON
        }
        public enum TextureCoordinatesSpecification
        {
            UV,
            STQ
        }
        public enum Fogging
        {
            OFF,
            ON
        }
        public enum Context
        {
            FIRST,
            SECOND
        }
        public enum ColorSpecMethod
        {
            SOURCE   = 0b00,
            FB       = 0b01,
            ZERO     = 0b10,
            RESERVED = 0b11
        }
        public enum AlphaSpecMethod
        {
            SOURCE = 0b00,
            FB = 0b01,
            FIX = 0b10,
            RESERVED = 0b11
        }
        public enum TextureFilter
        {
            NEAREST,
            LINEAR
        }
        public enum ZValueDrawMask
        {
            UPDATE,
            NOT_UPDATE
        }
        #endregion
    }
}
