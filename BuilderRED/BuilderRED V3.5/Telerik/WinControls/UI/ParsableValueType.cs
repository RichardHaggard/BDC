// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ParsableValueType
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public struct ParsableValueType
  {
    private string expression;
    private object value;
    private bool parseCompleted;
    private Type convertToType;

    public ParsableValueType(string text, Type convertTo)
    {
      this.parseCompleted = false;
      this.value = (object) null;
      this.expression = text;
      this.convertToType = convertTo;
    }

    public object GetValue(out bool parsed)
    {
      if (!this.parseCompleted)
      {
        this.value = this.ParseValue();
        this.parseCompleted = true;
      }
      parsed = this.value != null;
      if (this.value != null)
        return this.value;
      return (object) this.expression;
    }

    private object ParseValue()
    {
      bool result1;
      if (this.convertToType.IsAssignableFrom(typeof (bool)) && bool.TryParse(this.expression, out result1))
        return (object) result1;
      byte result2;
      if (this.convertToType.IsAssignableFrom(typeof (byte)) && byte.TryParse(this.expression, out result2))
        return (object) result2;
      short result3;
      if (this.convertToType.IsAssignableFrom(typeof (short)) && short.TryParse(this.expression, out result3))
        return (object) result3;
      int result4;
      if (this.convertToType.IsAssignableFrom(typeof (int)) && int.TryParse(this.expression, out result4))
        return (object) result4;
      long result5;
      if (this.convertToType.IsAssignableFrom(typeof (long)) && long.TryParse(this.expression, out result5))
        return (object) result5;
      uint result6;
      if (this.convertToType.IsAssignableFrom(typeof (uint)) && uint.TryParse(this.expression, out result6))
        return (object) result6;
      ushort result7;
      if (this.convertToType.IsAssignableFrom(typeof (ushort)) && ushort.TryParse(this.expression, out result7))
        return (object) result7;
      uint result8;
      if (this.convertToType.IsAssignableFrom(typeof (uint)) && uint.TryParse(this.expression, out result8))
        return (object) result8;
      ushort result9;
      if (this.convertToType.IsAssignableFrom(typeof (ushort)) && ushort.TryParse(this.expression, out result9))
        return (object) result9;
      Decimal result10;
      if (this.convertToType.IsAssignableFrom(typeof (Decimal)) && Decimal.TryParse(this.expression, out result10))
        return (object) result10;
      float result11;
      if (this.convertToType.IsAssignableFrom(typeof (float)) && float.TryParse(this.expression, out result11))
        return (object) result11;
      double result12;
      if (this.convertToType.IsAssignableFrom(typeof (double)) && double.TryParse(this.expression, out result12))
        return (object) result12;
      DateTime result13;
      if (this.convertToType.IsAssignableFrom(typeof (DateTime)) && DateTime.TryParse(this.expression, out result13))
        return (object) result13;
      if (this.convertToType.IsAssignableFrom(typeof (string)))
        return (object) this.expression;
      return (object) null;
    }

    public string StringRepresentation
    {
      get
      {
        return this.expression;
      }
    }

    public Type ConvertToType
    {
      get
      {
        return this.convertToType;
      }
    }
  }
}
