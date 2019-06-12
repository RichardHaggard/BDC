// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewColorColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridViewColorColumn : GridViewDataColumn
  {
    public GridViewColorColumn()
      : this(string.Empty, string.Empty)
    {
    }

    public GridViewColorColumn(string fieldName)
      : this(string.Empty, fieldName)
    {
    }

    public GridViewColorColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
      int num = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeProperty, (object) typeof (Color));
    }

    public override IInputEditor GetDefaultEditor()
    {
      return (IInputEditor) new GridColorPickerEditor();
    }

    public override Type GetDefaultEditorType()
    {
      return typeof (GridColorPickerEditor);
    }

    public override Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewDataRowInfo || row is GridViewNewRowInfo)
        return typeof (GridColorCellElement);
      return base.GetCellType(row);
    }

    protected override TypeConverter GetDefaultDataTypeConverter(Type type)
    {
      if ((object) type == (object) typeof (string))
        type = typeof (Color);
      return base.GetDefaultDataTypeConverter(type);
    }

    [DefaultValue(typeof (Color))]
    public override Type DataType
    {
      get
      {
        return base.DataType;
      }
      set
      {
        base.DataType = value;
      }
    }
  }
}
