// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRangeAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [AttributeUsage(AttributeTargets.Property)]
  [Description("The RadRangeAttribute is an attribute which specifies the allowed range of values.")]
  public class RadRangeAttribute : Attribute
  {
    private long minValue;
    private long maxValue;

    public long MinValue
    {
      get
      {
        return this.minValue;
      }
    }

    public long MaxValue
    {
      get
      {
        return this.maxValue;
      }
    }

    public RadRangeAttribute(long minValue, long maxValue)
    {
      this.minValue = minValue;
      this.maxValue = maxValue;
    }
  }
}
