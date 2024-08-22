using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Reflectis.SDK.Utilities
{
    public static class BinaryWriterReaderExtension
    {
        #region Int32 Array

        public static void Write(this BinaryWriter binaryWriter, int[] values)
        {
            binaryWriter.Write(values.Length);
            foreach (var value in values)
            {
                binaryWriter.Write(value);
            }
        }

        public static int[] ReadInt32s(this BinaryReader binaryReader)
        {
            int length = binaryReader.ReadInt32();

            int[] array = new int[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = binaryReader.ReadInt32();
            }

            return array;
        }
        #endregion



        public enum SupportedType
        {
            Bool = 0,
            Byte = 1,
            SByte = 2,
            Char = 3,
            Short = 4,
            UShort = 5,
            Int = 6,
            UInt = 7,
            Long = 8,
            ULong = 9,
            Float = 10,
            Double = 11,
            Decimal = 12,
            String = 13,
            //Max supported types for current implementation
            //Custom1 = 14,  
            Unsupported = 15
        }

        public static void Write(this BinaryWriter writer, object[] values)
        {
            writer.Write(values.Length);
            writer.WriteTypes(values);
            foreach (var value in values)
            {
                writer.Write(value);
            }
        }



        public static void WriteTypes(this BinaryWriter writer, object[] values)
        {
            for (int i = 0; i < values.Length; i += 2)
            {
                // Get the next 2 types
                SupportedType type1 = GetSupportedType(values[i]);
                // Type 2 is present only if we do not exceed the array count
                SupportedType type2 = (i + 1 < values.Length) ? GetSupportedType(values[i + 1]) : SupportedType.Unsupported;

                WriteTypes(writer, type1, type2);
            }
        }

        public static void WriteTypes(this BinaryWriter writer, SupportedType type1, SupportedType type2)
        {
            // Pack the two SupportedType values into a single byte
            byte packedByte = 0;
            packedByte |= (byte)type1;
            packedByte |= (byte)((byte)type2 << 4); // Insert type2 in the last 4 bit

            writer.Write(packedByte);
        }

        private static SupportedType GetSupportedType(object obj)
        {
            return obj switch
            {
                bool => SupportedType.Bool,
                byte => SupportedType.Byte,
                sbyte => SupportedType.SByte,
                char => SupportedType.Char,
                short => SupportedType.Short,
                ushort => SupportedType.UShort,
                int => SupportedType.Int,
                uint => SupportedType.UInt,
                long => SupportedType.Long,
                ulong => SupportedType.ULong,
                float => SupportedType.Float,
                double => SupportedType.Double,
                decimal => SupportedType.Decimal,
                string => SupportedType.String,
                _ => SupportedType.Unsupported
            };
        }

        public static SupportedType[] ReadTypes(this BinaryReader reader, int objectCount)
        {
            SupportedType[] types = new SupportedType[objectCount];
            int index = 0;

            while (index < objectCount)
            {
                // Legge un byte dal flusso
                byte packedByte = reader.ReadByte();

                //Get the first 4 bit for the first type
                SupportedType type1 = (SupportedType)(packedByte & 0x0F);
                types[index++] = type1;

                if (index < objectCount)
                {
                    // Get the last 4 bit for the last type
                    SupportedType type2 = (SupportedType)((packedByte >> 4) & 0x0F);
                    types[index++] = type2;
                }
            }

            return types;
        }

        public static object[] ReadObjects(this BinaryReader binaryReader)
        {
            int length = binaryReader.ReadInt32();

            SupportedType[] types = binaryReader.ReadTypes(length);
            object[] array = new object[length];

            for (int i = 0; i < length; i++)
            {
                array[i] = ReadObject(binaryReader, types[i]);
            }

            return array;
        }

        public static object ReadObject(this BinaryReader binaryReader, SupportedType type)
        {
            // Leggi il valore corrispondente in base al tipo
            switch (type)
            {
                case SupportedType.Bool:
                    return binaryReader.ReadBoolean();
                case SupportedType.Byte:
                    return binaryReader.ReadByte();
                case SupportedType.SByte:
                    return binaryReader.ReadSByte();
                case SupportedType.Char:
                    return binaryReader.ReadChar();
                case SupportedType.Short:
                    return binaryReader.ReadInt16();
                case SupportedType.UShort:
                    return binaryReader.ReadUInt16();
                case SupportedType.Int:
                    return binaryReader.ReadInt32();
                case SupportedType.UInt:
                    return binaryReader.ReadUInt32();
                    break;
                case SupportedType.Long:
                    return binaryReader.ReadInt64();
                case SupportedType.ULong:
                    return binaryReader.ReadUInt64();
                case SupportedType.Float:
                    return binaryReader.ReadSingle();
                case SupportedType.Double:
                    return binaryReader.ReadDouble();
                case SupportedType.Decimal:
                    return binaryReader.ReadDecimal();
                case SupportedType.String:
                    return binaryReader.ReadString();
                default:
                    UnityEngine.Debug.LogError("Found unsupported type " + type);
                    return null;
            }
        }


        public static void Write(this BinaryWriter writer, object value)
        {
            if (value == null)
            {
                UnityEngine.Debug.LogError("Cannot serialize null value");
                return;
            }

            if (value is bool typedValueBool)
            {
                writer.Write(typedValueBool);
            }
            else if (value is byte typedValueByte)
            {
                writer.Write(typedValueByte);
            }
            else if (value is sbyte typedValueSByte)
            {
                writer.Write(typedValueSByte);
            }
            else if (value is char typedValueChar)
            {
                writer.Write(typedValueChar);
            }
            else if (value is short typedValueShort)
            {
                writer.Write(typedValueShort);
            }
            else if (value is ushort typedValueUShort)
            {
                writer.Write(typedValueUShort);
            }
            else if (value is int typedValueInt)
            {
                writer.Write(typedValueInt);
            }
            else if (value is uint typedValueUInt)
            {
                writer.Write(typedValueUInt);
            }
            else if (value is long typedValueLong)
            {
                writer.Write(typedValueLong);
            }
            else if (value is ulong typedValueULong)
            {
                writer.Write(typedValueULong);
            }
            else if (value is float typedValueFloat)
            {
                writer.Write(typedValueFloat);
            }
            else if (value is double typedValueDouble)
            {
                writer.Write(typedValueDouble);
            }
            else if (value is decimal typedValueDecimal)
            {
                writer.Write(typedValueDecimal);
            }
            else if (value is string typedValueString)
            {
                writer.Write(typedValueString);
            }
            else
            {
                UnityEngine.Debug.LogError("Trying to write an unsupported value");
            }
        }


        public static void Write(this BinaryWriter binaryWriter, Dictionary<int, object> dictionary)
        {
            binaryWriter.Write(dictionary.Count);
            var keys = dictionary.Keys.ToArray();
            for (int i = 0; i < keys.Length; i += 2)
            {
                SupportedType type1 = GetSupportedType(dictionary[keys[i]]);
                if ((i + 1 < keys.Length))
                {
                    // Type 2 is present only if we do not exceed the array count
                    binaryWriter.Write(keys[i]);
                    binaryWriter.Write(keys[i + 1]);
                    SupportedType type2 = GetSupportedType(dictionary[keys[i + 1]]);
                    binaryWriter.WriteTypes(type1, type2);
                    binaryWriter.Write(dictionary[keys[i]]);
                    binaryWriter.Write(dictionary[keys[i + 1]]);
                }
                else
                {
                    binaryWriter.Write(keys[i]);
                    binaryWriter.WriteTypes(type1, SupportedType.Unsupported);
                    binaryWriter.Write(dictionary[keys[i]]);
                }
            }
        }

        public static Dictionary<int, object> ReadDictionaryInt32Object(this BinaryReader binaryReader)
        {
            int count = binaryReader.ReadInt32();

            Dictionary<int, object> dictionary = new Dictionary<int, object>();

            for (int i = 0; i < count; i += 2)
            {
                if ((i + 1 < count))
                {
                    int key1 = binaryReader.ReadInt32();
                    int key2 = binaryReader.ReadInt32();
                    SupportedType[] types = binaryReader.ReadTypes(2);
                    dictionary[key1] = binaryReader.ReadObject(types[0]);
                    dictionary[key2] = binaryReader.ReadObject(types[1]);
                }
                else
                {
                    int key = binaryReader.ReadInt32();
                    SupportedType[] types = binaryReader.ReadTypes(1);
                    dictionary[key] = binaryReader.ReadObject(types[0]);
                }
            }
            return dictionary;

        }
    }
}
