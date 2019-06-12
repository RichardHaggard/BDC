// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BindingCreatingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BindingCreatingEventArgs : CancelEventArgs
  {
    private Control control;
    private string propertyName;
    private object propertyDataSource;
    private string dataMember;
    private bool formattingEnabled;

    public BindingCreatingEventArgs(
      Control control,
      string propertyName,
      object propertyDataSource,
      string dataMember)
    {
      this.control = control;
      this.propertyName = propertyName;
      this.propertyDataSource = propertyDataSource;
      this.dataMember = dataMember;
    }

    public BindingCreatingEventArgs(
      Control control,
      string propertyName,
      object propertyDataSource,
      string dataMember,
      bool formattingEnabled)
    {
      this.control = control;
      this.propertyName = propertyName;
      this.propertyDataSource = propertyDataSource;
      this.dataMember = dataMember;
      this.formattingEnabled = formattingEnabled;
    }

    public string DataMember
    {
      get
      {
        return this.dataMember;
      }
      set
      {
        this.dataMember = value;
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
      set
      {
        this.propertyName = value;
      }
    }

    public Control Control
    {
      get
      {
        return this.control;
      }
    }

    public bool FormattingEnabled
    {
      get
      {
        return this.formattingEnabled;
      }
      set
      {
        this.formattingEnabled = value;
      }
    }
  }
}
