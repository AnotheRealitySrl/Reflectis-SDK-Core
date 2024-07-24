using System;
using System.IO;


namespace Reflectis.SDK.Utilities
{
    [Serializable]
    public abstract class DirtyVariable<T>
    {
        private T value;
        private bool isDirty;

        public DirtyVariable(T value, bool isDirty)
        {
            this.value = value;
            this.isDirty = isDirty;
        }

        public T Value { get => value; set => this.value = value; }
        public bool IsDirty { get => isDirty; set => isDirty = value; }

        public void Write(BinaryWriter bw)
        {
            bw.Write(isDirty);
            WriteValue(bw);
        }

        public virtual void WriteValue(BinaryWriter bw)
        {
            bw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(Value));
        }

        public void Read(BinaryReader br)
        {
            IsDirty = br.ReadBoolean();
            ReadValue(br);
        }

        public virtual void ReadValue(BinaryReader br)
        {
            Value = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(br.ReadString());
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class DirtyBoolVariable : DirtyVariable<bool>
    {
        public DirtyBoolVariable(bool value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadBoolean();
        }
    }

    public class DirtyByteVariable : DirtyVariable<byte>
    {
        public DirtyByteVariable(byte value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadByte();
        }
    }

    public class DirtySByteVariable : DirtyVariable<sbyte>
    {
        public DirtySByteVariable(sbyte value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadSByte();
        }
    }

    public class DirtyCharVariable : DirtyVariable<char>
    {
        public DirtyCharVariable(char value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadChar();
        }
    }

    public class DirtyDecimalVariable : DirtyVariable<decimal>
    {
        public DirtyDecimalVariable(decimal value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadDecimal();
        }
    }

    public class DirtyDoubleVariable : DirtyVariable<double>
    {
        public DirtyDoubleVariable(double value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadDouble();
        }
    }

    public class DirtyShortVariable : DirtyVariable<short>
    {
        public DirtyShortVariable(short value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadInt16();
        }
    }

    public class DirtyIntVariable : DirtyVariable<int>
    {
        public DirtyIntVariable(int value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadInt32();
        }
    }

    public class DirtyLongVariable : DirtyVariable<long>
    {
        public DirtyLongVariable(long value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadInt64();
        }
    }

    public class DirtyFloatVariable : DirtyVariable<float>
    {
        public DirtyFloatVariable(float value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadSingle();
        }
    }

    public class DirtyUShortVariable : DirtyVariable<ushort>
    {
        public DirtyUShortVariable(ushort value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadUInt16();
        }
    }

    public class DirtyUIntVariable : DirtyVariable<uint>
    {
        public DirtyUIntVariable(uint value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadUInt32();
        }
    }

    public class DirtyULongVariable : DirtyVariable<ulong>
    {
        public DirtyULongVariable(ulong value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadUInt64();
        }
    }

    public class DirtyStringVariable : DirtyVariable<string>
    {
        public DirtyStringVariable(string value, bool isDirty) : base(value, isDirty) { }

        public override void WriteValue(BinaryWriter bw)
        {
            bw.Write(Value);
        }

        public override void ReadValue(BinaryReader br)
        {
            Value = br.ReadString();
        }
    }
}