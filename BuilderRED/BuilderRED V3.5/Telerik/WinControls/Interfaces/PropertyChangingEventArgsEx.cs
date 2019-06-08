// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Interfaces.PropertyChangingEventArgsEx
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.Interfaces
{
  public class PropertyChangingEventArgsEx : CancelEventArgs
  {
    private readonly string propertyName;
    private object oldValue;
    private object newValue;

    public PropertyChangingEventArgsEx(
      string propertyName,
      object oldValue,
      object newValue,
      bool cancel)
      : base(cancel)
    {
      this.propertyName = propertyName;
      this.oldValue = oldValue;
      this.newValue = newValue;
    }

    public PropertyChangingEventArgsEx(string propertyName, object oldValue, object newValue)
      : this(propertyName, oldValue, newValue, false)
    {
    }

    public PropertyChangingEventArgsEx(string propertyName, bool cancel)
      : this(propertyName, (object) null, (object) null, cancel)
    {
    }

    public PropertyChangingEventArgsEx(string propertyName)
      : this(propertyName, (object) null, (object) null, false)
    {
    }

    public object OldValue
    {
      get
      {
        return this.oldValue;
      }
    }

    public object NewValue
    {
      get
      {
        return this.newValue;
      }
      set
      {
        this.newValue = value;
      }
    }

    public virtual string PropertyName
    {
      get
      {
        return this.propertyName;
      }
    }
  }
}
