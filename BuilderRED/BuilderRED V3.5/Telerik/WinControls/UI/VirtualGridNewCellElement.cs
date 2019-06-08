// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridNewCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridNewCellElement : VirtualGridCellElement
  {
    protected override void UpdateInfo(VirtualGridCellValueNeededEventArgs args)
    {
      this.Value = this.ViewInfo.NewRowValues.ContainsKey(this.ColumnIndex) ? this.ViewInfo.NewRowValues[this.ColumnIndex] : (object) null;
      this.FieldName = args.FieldName;
      this.FormatString = args.FormatString;
      this.Text = string.Format(this.FormatString, this.Value);
      this.IsPinned = true;
    }

    public override bool IsCompatible(int data, object context)
    {
      if (data >= 0)
        return context is VirtualGridNewRowElement;
      return false;
    }
  }
}
