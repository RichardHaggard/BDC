// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadBitVector64
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class RadBitVector64
  {
    private ulong data;

    public RadBitVector64(RadBitVector64 source)
    {
      this.data = source.data;
    }

    public RadBitVector64(long data)
    {
      this.data = (ulong) data;
    }

    public bool this[long key]
    {
      get
      {
        return ((long) this.data & key) == key;
      }
      set
      {
        if (value)
          this.data |= (ulong) key;
        else
          this.data &= (ulong) ~key;
      }
    }

    public void Reset()
    {
      this.data = 0UL;
    }

    public override bool Equals(object obj)
    {
      if ((object) (obj as RadBitVector64) == null)
        return false;
      return (RadBitVector64) obj == this;
    }

    public override int GetHashCode()
    {
      return this.data.GetHashCode();
    }

    public static bool operator ==(RadBitVector64 vector1, RadBitVector64 vector2)
    {
      return (long) vector1.data == (long) vector2.data;
    }

    public static bool operator !=(RadBitVector64 vector1, RadBitVector64 vector2)
    {
      return (long) vector1.data != (long) vector2.data;
    }
  }
}
