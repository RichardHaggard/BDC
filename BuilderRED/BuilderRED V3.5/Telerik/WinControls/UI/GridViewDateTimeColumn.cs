// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewDateTimeColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.UI
{
  public class GridViewDateTimeColumn : GridViewDataColumn
  {
    public static RadProperty FormatProperty = RadProperty.Register(nameof (Format), typeof (DateTimePickerFormat), typeof (GridViewDateTimeColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DateTimePickerFormat.Long));
    public static RadProperty CustomFormatProperty = RadProperty.Register(nameof (CustomFormat), typeof (string), typeof (GridViewDateTimeColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null));
    public static RadProperty GridViewDateTimeEditorTypeProperty = RadProperty.Register(nameof (EditorType), typeof (GridViewDateTimeEditorType), typeof (GridViewDateTimeColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GridViewDateTimeEditorType.DateTimePicker));
    public static RadProperty DateTimeKindProperty = RadProperty.Register(nameof (DateTimeKind), typeof (DateTimeKind), typeof (GridViewDateTimeColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DateTimeKind.Unspecified));
    private GridViewTimeFilteringMode filteringMode = GridViewTimeFilteringMode.All;

    public GridViewDateTimeColumn()
    {
      this.InitProperties();
    }

    protected override TypeConverter GetDefaultDataTypeConverter(System.Type type)
    {
      if ((object) type == (object) typeof (DateTime))
        return (TypeConverter) new RadGridDateTimeConverter(this);
      return base.GetDefaultDataTypeConverter(type);
    }

    public GridViewDateTimeColumn(string fieldName)
      : base(fieldName)
    {
      this.InitProperties();
    }

    public GridViewDateTimeColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
      this.InitProperties();
    }

    private void InitProperties()
    {
      int num = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeProperty, (object) typeof (DateTime));
    }

    [DefaultValue(typeof (DateTime))]
    public override System.Type DataType
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

    [Description("Specifies the editor type of the column.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(GridViewDateTimeEditorType.DateTimePicker)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public GridViewDateTimeEditorType EditorType
    {
      get
      {
        return (GridViewDateTimeEditorType) this.GetValue(GridViewDateTimeColumn.GridViewDateTimeEditorTypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDateTimeColumn.GridViewDateTimeEditorTypeProperty, (object) value);
      }
    }

    [DefaultValue(GridViewTimeFilteringMode.All)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Specifies the filtering mode of the column.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public GridViewTimeFilteringMode FilteringMode
    {
      get
      {
        return this.filteringMode;
      }
      set
      {
        if (this.filteringMode == value)
          return;
        this.filteringMode = value;
        this.OnNotifyPropertyChanged(nameof (FilteringMode));
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the format of the date and time displayed in the control.")]
    [DefaultValue(typeof (DateTimePickerFormat), "Long")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public DateTimePickerFormat Format
    {
      get
      {
        return (DateTimePickerFormat) this.GetValue(GridViewDateTimeColumn.FormatProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDateTimeColumn.FormatProperty, (object) value);
      }
    }

    [DefaultValue(null)]
    [Category("Behavior")]
    [Description("Gets or sets the custom date/time format string.")]
    [Localizable(true)]
    public string CustomFormat
    {
      get
      {
        return this.GetValue(GridViewDateTimeColumn.CustomFormatProperty) as string;
      }
      set
      {
        int num = (int) this.SetValue(GridViewDateTimeColumn.CustomFormatProperty, (object) value);
      }
    }

    [DefaultValue(DateTimeKind.Unspecified)]
    [Category("Behavior")]
    [Description("Gets or sets the column date/time representation.")]
    public DateTimeKind DateTimeKind
    {
      get
      {
        return (DateTimeKind) this.GetValue(GridViewDateTimeColumn.DateTimeKindProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewDateTimeColumn.DateTimeKindProperty, (object) value);
      }
    }

    [DefaultValue(DisplayFormatType.GeneralDate)]
    [Description("Gets or sets the type of the excel export.")]
    [Category("Behavior")]
    public override DisplayFormatType ExcelExportType
    {
      get
      {
        if (this.excelFormat.HasValue)
          return base.ExcelExportType;
        return this.Format == DateTimePickerFormat.Time ? DisplayFormatType.ShortTime : DisplayFormatType.GeneralDate;
      }
      set
      {
        base.ExcelExportType = value;
      }
    }

    public override IInputEditor GetDefaultEditor()
    {
      if (this.EditorType == GridViewDateTimeEditorType.DateTimePicker || this.EditorType == GridViewDateTimeEditorType.DateTimePickerSpinMode)
        return (IInputEditor) new RadDateTimeEditor();
      return (IInputEditor) new GridTimePickerEditor();
    }

    public override System.Type GetDefaultEditorType()
    {
      if (this.EditorType == GridViewDateTimeEditorType.DateTimePicker || this.EditorType == GridViewDateTimeEditorType.DateTimePickerSpinMode)
        return typeof (RadDateTimeEditor);
      return typeof (GridTimePickerEditor);
    }

    public override void InitializeEditor(IInputEditor editor)
    {
      RadDateTimeEditor radDateTimeEditor = editor as RadDateTimeEditor;
      if (radDateTimeEditor != null)
      {
        RadDateTimePickerElement editorElement = (RadDateTimePickerElement) radDateTimeEditor.EditorElement;
        editorElement.Format = this.Format;
        editorElement.CustomFormat = this.CustomFormat;
        editorElement.Culture = this.FormatInfo;
        editorElement.ShowUpDown = this.EditorType == GridViewDateTimeEditorType.DateTimePickerSpinMode;
      }
      GridTimePickerEditor timePickerEditor = editor as GridTimePickerEditor;
      if (timePickerEditor == null)
        return;
      RadTimePickerElement editorElement1 = timePickerEditor.EditorElement as RadTimePickerElement;
      editorElement1.Format = this.Format != DateTimePickerFormat.Custom ? this.FormatString : this.CustomFormat;
      editorElement1.Culture = this.FormatInfo;
    }

    public override System.Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewDataRowInfo || row is GridViewNewRowInfo)
        return typeof (GridDateTimeCellElement);
      return base.GetCellType(row);
    }

    internal override object GetValue(GridViewRowInfo row, GridViewDataOperation operation)
    {
      object obj = base.GetValue(row, operation);
      if (operation == GridViewDataOperation.Filtering && !Convert.IsDBNull(obj) && obj != null)
      {
        if (this.DataTypeConverter != null && this.DataTypeConverter.CanConvertTo((ITypeDescriptorContext) this, typeof (DateTime)))
          obj = this.DataTypeConverter.ConvertTo((ITypeDescriptorContext) this, this.FormatInfo, obj, typeof (DateTime));
        obj = (object) GridViewHelper.ClampDateTime(Convert.ToDateTime(obj), this.filteringMode);
      }
      return obj;
    }
  }
}
