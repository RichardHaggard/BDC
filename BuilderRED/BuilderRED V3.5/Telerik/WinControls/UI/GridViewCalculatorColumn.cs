// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCalculatorColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.UI
{
  public class GridViewCalculatorColumn : GridViewDataColumn
  {
    public GridViewCalculatorColumn()
      : this(string.Empty, string.Empty)
    {
    }

    public GridViewCalculatorColumn(string fieldName)
      : this(string.Empty, fieldName)
    {
    }

    public GridViewCalculatorColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
      int num = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeProperty, (object) typeof (Decimal));
      this.TextAlignment = ContentAlignment.MiddleRight;
    }

    [DefaultValue(typeof (Decimal))]
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

    [DefaultValue(ContentAlignment.MiddleRight)]
    public override ContentAlignment TextAlignment
    {
      get
      {
        return base.TextAlignment;
      }
      set
      {
        base.TextAlignment = value;
      }
    }

    [DefaultValue(DisplayFormatType.Fixed)]
    [Description("Gets or sets the type of the excel export.")]
    [Category("Behavior")]
    public override DisplayFormatType ExcelExportType
    {
      get
      {
        if (!this.excelFormat.HasValue)
          return DisplayFormatType.Fixed;
        return base.ExcelExportType;
      }
      set
      {
        base.ExcelExportType = value;
      }
    }

    public override IInputEditor GetDefaultEditor()
    {
      return (IInputEditor) new RadCalculatorEditor();
    }

    public override Type GetDefaultEditorType()
    {
      return typeof (RadCalculatorEditor);
    }

    public override Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewDataRowInfo || row is GridViewNewRowInfo)
        return typeof (GridCalculatorCellElement);
      return base.GetCellType(row);
    }
  }
}
