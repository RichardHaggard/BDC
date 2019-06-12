// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCellValueNeededEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridCellValueNeededEventArgs : VirtualGridCellEventArgs
  {
    private string formatString = "{0}";
    private string fieldName = string.Empty;
    private object value;

    public VirtualGridCellValueNeededEventArgs(
      int rowIndex,
      int columnIndex,
      VirtualGridViewInfo viewInfo)
      : base(rowIndex, columnIndex, viewInfo)
    {
    }

    public string FieldName
    {
      get
      {
        return this.fieldName;
      }
      set
      {
        this.fieldName = value;
      }
    }

    public string FormatString
    {
      get
      {
        return this.formatString;
      }
      set
      {
        this.formatString = value;
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }
  }
}
