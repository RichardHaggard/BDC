// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.ParameterValuePair
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.Data
{
  public class ParameterValuePair : NotifyPropertyBase
  {
    private string name;
    private object value;

    public ParameterValuePair()
    {
    }

    public ParameterValuePair(string name, object value)
    {
      this.name = name;
      this.value = value;
    }

    [DefaultValue(null)]
    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    [DefaultValue(null)]
    public object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.SetProperty<object>(nameof (Value), ref this.value, value);
      }
    }
  }
}
