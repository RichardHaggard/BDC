// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewDecimalColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.UI
{
  public class GridViewDecimalColumn : GridViewDataColumn
  {
    public static RadProperty DecimalPlacesProperty = RadProperty.Register(nameof (DecimalPlaces), typeof (int), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2));
    public static RadProperty StepProperty = RadProperty.Register(nameof (Step), typeof (Decimal), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Decimal(1)));
    public static RadProperty MinimumProperty = RadProperty.Register(nameof (Minimum), typeof (Decimal), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Decimal(-1, -1, -1, true, (byte) 0)));
    public static RadProperty MaximumProperty = RadProperty.Register(nameof (Maximum), typeof (Decimal), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) new Decimal(-1, -1, -1, false, (byte) 0)));
    public static RadProperty ThousandsSeparatorProperty = RadProperty.Register(nameof (ThousandsSeparator), typeof (bool), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty ShowUpDownButtonsProperty = RadProperty.Register(nameof (ShowUpDownButtons), typeof (bool), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));

    public GridViewDecimalColumn()
      : this(string.Empty)
    {
    }

    public GridViewDecimalColumn(string fieldName)
      : this(string.Empty, fieldName)
    {
    }

    public GridViewDecimalColumn(string uniqueName, string fieldName)
      : this(typeof (Decimal), uniqueName, fieldName)
    {
    }

    public GridViewDecimalColumn(Type numericType, string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
      int num = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeProperty, (object) typeof (Decimal));
      if ((object) numericType != (object) typeof (Decimal))
        this.DataType = numericType;
      this.TextAlignment = ContentAlignment.MiddleRight;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(ContentAlignment.MiddleRight)]
    [Description("Gets or sets a value indicating the position of the cell content within a column's cells.")]
    [Browsable(true)]
    [Category("Appearance")]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Data")]
    [DefaultValue(2)]
    [Description("Gets or sets the number of decimal places to display in the GridSpinEditor.")]
    public int DecimalPlaces
    {
      get
      {
        return (int) this.GetValue(GridViewDecimalColumn.DecimalPlacesProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDecimalColumn.DecimalPlacesProperty, (object) value);
      }
    }

    [DefaultValue(typeof (Decimal), "1")]
    [Browsable(true)]
    [Category("Data")]
    [Description("Gets or sets the amount to increment or decrement from the current value of the GridSpinEditor")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Decimal Step
    {
      get
      {
        return (Decimal) this.GetValue(GridViewDecimalColumn.StepProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDecimalColumn.StepProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Data")]
    [DefaultValue(typeof (Decimal), "79228162514264337593543950335")]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets a value indicating the maximum value for the GridSpinEditor.")]
    public Decimal Maximum
    {
      get
      {
        return (Decimal) this.GetValue(GridViewDecimalColumn.MaximumProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDecimalColumn.MaximumProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Data")]
    [DefaultValue(typeof (Decimal), "-79228162514264337593543950335")]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets a value indicating the minimum value for the GridSpinEditor.")]
    public Decimal Minimum
    {
      get
      {
        return (Decimal) this.GetValue(GridViewDecimalColumn.MinimumProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDecimalColumn.MinimumProperty, (object) value);
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Data")]
    [Description("Gets or sets a value indicating whether the thousands separator will be displayed in the GridSpinEditor.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool ThousandsSeparator
    {
      get
      {
        return (bool) this.GetValue(GridViewDecimalColumn.ThousandsSeparatorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDecimalColumn.ThousandsSeparatorProperty, (object) value);
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the whether GridSpinEditor will be used as a numeric textbox.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool ShowUpDownButtons
    {
      get
      {
        return (bool) this.GetValue(GridViewDecimalColumn.ShowUpDownButtonsProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDecimalColumn.ShowUpDownButtonsProperty, (object) value);
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
      return (IInputEditor) new GridSpinEditor();
    }

    public override void InitializeEditor(IInputEditor editor)
    {
      GridSpinEditor gridSpinEditor = editor as GridSpinEditor;
      if (gridSpinEditor == null)
        return;
      RadSpinElement editorElement = (RadSpinElement) gridSpinEditor.EditorElement;
      editorElement.DecimalPlaces = GridViewHelper.IsInteger(this.DataType) || GridViewHelper.IsIntegerSql(this.DataType) ? 0 : this.DecimalPlaces;
      gridSpinEditor.ValueType = this.DataType;
      editorElement.ShowUpDownButtons = this.ShowUpDownButtons;
      editorElement.Step = this.Step;
      editorElement.MinValue = this.Minimum;
      editorElement.MaxValue = this.Maximum;
      editorElement.ThousandsSeparator = this.ThousandsSeparator;
    }

    public override Type GetDefaultEditorType()
    {
      return typeof (GridSpinEditor);
    }
  }
}
