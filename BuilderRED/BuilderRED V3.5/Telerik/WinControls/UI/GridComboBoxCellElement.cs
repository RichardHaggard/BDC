// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridComboBoxCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridComboBoxCellElement : GridDataCellElement
  {
    public GridComboBoxCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    public override void SetContent()
    {
      object lookupValue = this.Value;
      GridViewComboBoxColumn columnInfo = (GridViewComboBoxColumn) this.ColumnInfo;
      if (columnInfo.HasLookupValue)
        lookupValue = columnInfo.GetLookupValue(lookupValue);
      this.SetContentCore(lookupValue);
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      return data is GridViewComboBoxColumn;
    }

    protected override string ApplyFormatString(object value)
    {
      GridDataConversionInfo dataConversionInfo = new GridDataConversionInfo((IDataConversionInfoProvider) this);
      GridViewComboBoxColumn columnInfo = (GridViewComboBoxColumn) this.ColumnInfo;
      dataConversionInfo.DataType = columnInfo.DisplayMemberDataType;
      dataConversionInfo.DataTypeConverter = TypeDescriptor.GetConverter(columnInfo.DisplayMemberDataType);
      return RadDataConverter.Instance.Format(value, typeof (string), (IDataConversionInfoProvider) dataConversionInfo) as string;
    }
  }
}
