// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadPropertyChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  public class RadPropertyChangingEventArgs : CancelEventArgs
  {
    private RadProperty property;
    private object oldValue;
    private object newValue;
    private RadPropertyMetadata metadata;
    private bool isNewValueSet;

    public RadPropertyChangingEventArgs(
      RadProperty property,
      object oldValue,
      object newValue,
      RadPropertyMetadata metadata)
    {
      this.property = property;
      this.oldValue = oldValue;
      this.newValue = newValue;
      this.metadata = metadata;
    }

    public RadProperty Property
    {
      get
      {
        return this.property;
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
        this.isNewValueSet = true;
        this.newValue = value;
      }
    }

    public object OldValue
    {
      get
      {
        return this.oldValue;
      }
    }

    public RadPropertyMetadata Metadata
    {
      get
      {
        return this.metadata;
      }
    }

    internal bool IsNewValueSet
    {
      get
      {
        return this.isNewValueSet;
      }
    }
  }
}
