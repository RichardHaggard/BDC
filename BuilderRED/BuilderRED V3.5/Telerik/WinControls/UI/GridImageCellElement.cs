// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridImageCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridImageCellElement : GridDataCellElement
  {
    private const int OleHeaderSize = 78;

    public GridImageCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    public override bool IsEditable
    {
      get
      {
        return false;
      }
    }

    protected override void SetContentCore(object value)
    {
      object result = (object) null;
      this.Text = string.Empty;
      RadDataConverter.Instance.TryFormat(value, typeof (Image), (IDataConversionInfoProvider) this, out result);
      this.Image = result as Image;
      if (this.Image == null)
        return;
      GridViewImageColumn columnInfo = this.ColumnInfo as GridViewImageColumn;
      this.ImageLayout = columnInfo != null ? columnInfo.ImageLayout : ImageLayout.Stretch;
      this.ImageAlignment = columnInfo != null ? columnInfo.ImageAlignment : ContentAlignment.MiddleCenter;
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      return data is GridViewImageColumn;
    }
  }
}
