// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BindingCreatedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BindingCreatedEventArgs : EventArgs
  {
    private Control control;
    private string propertyName;
    private object propertyDataSource;
    private string dataMember;
    private Binding binding;

    public BindingCreatedEventArgs(
      Control control,
      string propertyName,
      object propertyDataSource,
      string dataMember,
      Binding binding)
    {
      this.control = control;
      this.propertyName = propertyName;
      this.propertyDataSource = propertyDataSource;
      this.dataMember = dataMember;
      this.binding = binding;
    }

    public string DataMember
    {
      get
      {
        return this.dataMember;
      }
    }

    public object PropertyDataSource
    {
      get
      {
        return this.propertyDataSource;
      }
    }

    public string PropertyName
    {
      get
      {
        return this.propertyName;
      }
    }

    public Control Control
    {
      get
      {
        return this.control;
      }
    }

    public Binding Binding
    {
      get
      {
        return this.binding;
      }
    }
  }
}
