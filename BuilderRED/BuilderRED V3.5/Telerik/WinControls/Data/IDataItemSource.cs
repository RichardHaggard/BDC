// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.IDataItemSource
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.Data
{
  public interface IDataItemSource
  {
    IDataItem NewItem();

    void Initialize();

    void BindingComplete();

    void MetadataChanged(PropertyDescriptor pd);

    BindingContext BindingContext { get; }

    event EventHandler BindingContextChanged;
  }
}
