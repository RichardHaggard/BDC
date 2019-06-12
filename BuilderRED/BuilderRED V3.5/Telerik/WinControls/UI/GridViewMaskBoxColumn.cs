// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewMaskBoxColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.UI
{
  public class GridViewMaskBoxColumn : GridViewDataColumn
  {
    public static RadProperty MaxLengthProperty = RadProperty.Register("MaxLength", typeof (int), typeof (GridViewMaskBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) (int) short.MaxValue));
    public static RadProperty MaskTypeProperty = RadProperty.Register(nameof (MaskType), typeof (MaskType), typeof (GridViewMaskBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) MaskType.Numeric));
    public static RadProperty MaskProperty = RadProperty.Register(nameof (Mask), typeof (string), typeof (GridViewMaskBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ""));
    public static RadProperty TextMaskFormatProperty = RadProperty.Register(nameof (TextMaskFormat), typeof (MaskFormat), typeof (GridViewMaskBoxColumn), new RadPropertyMetadata((object) MaskFormat.IncludePromptAndLiterals));
    public static RadProperty PromptCharProperty = RadProperty.Register(nameof (PromptChar), typeof (char), typeof (GridViewMaskBoxColumn), new RadPropertyMetadata((object) '_'));
    private bool enableNullValueInput;

    public GridViewMaskBoxColumn()
    {
    }

    public GridViewMaskBoxColumn(string fieldName)
      : base(fieldName)
    {
    }

    public GridViewMaskBoxColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
    }

    [Browsable(true)]
    [DefaultValue('_')]
    [Description("Gets or sets the PromptChar for the GridViewMaskBoxColumn.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    public char PromptChar
    {
      get
      {
        return (char) this.GetValue(GridViewMaskBoxColumn.PromptCharProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewMaskBoxColumn.PromptCharProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [DefaultValue(typeof (MaskType), "MaskType.Numeric")]
    [Description("Gets or sets the mask type for the GridViewMaskBoxColumn.")]
    public MaskType MaskType
    {
      get
      {
        return (MaskType) this.GetValue(GridViewMaskBoxColumn.MaskTypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewMaskBoxColumn.MaskTypeProperty, (object) value);
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value that determines whether literals and prompt characters are included in the Value")]
    [DefaultValue(MaskFormat.IncludePromptAndLiterals)]
    [Category("Behavior")]
    public MaskFormat TextMaskFormat
    {
      get
      {
        return (MaskFormat) this.GetValue(GridViewMaskBoxColumn.TextMaskFormatProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewMaskBoxColumn.TextMaskFormatProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the mask for the GridViewMaskBoxColumn.")]
    [Browsable(true)]
    public string Mask
    {
      get
      {
        return (string) this.GetValue(GridViewMaskBoxColumn.MaskProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewMaskBoxColumn.MaskProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [DefaultValue(DisplayFormatType.Fixed)]
    [Description("Gets or sets the type of the excel export.")]
    public override DisplayFormatType ExcelExportType
    {
      get
      {
        if (this.excelFormat.HasValue)
          return base.ExcelExportType;
        switch (this.MaskType)
        {
          case MaskType.DateTime:
            return DisplayFormatType.GeneralDate;
          case MaskType.Numeric:
            return DisplayFormatType.Fixed;
          default:
            return DisplayFormatType.None;
        }
      }
      set
      {
        base.ExcelExportType = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or set a value indicating whether end users can set the value to NULL. This can be achieved by pressing Ctrl + Del or Ctrl + 0 key combinations.")]
    [Browsable(true)]
    public bool EnableNullValueInput
    {
      get
      {
        return this.enableNullValueInput;
      }
      set
      {
        this.enableNullValueInput = value;
      }
    }

    public override IInputEditor GetDefaultEditor()
    {
      return (IInputEditor) new RadMaskedEditBoxEditor();
    }

    public override void InitializeEditor(IInputEditor editor)
    {
      RadMaskedEditBoxEditor maskedEditBoxEditor = editor as RadMaskedEditBoxEditor;
      if (maskedEditBoxEditor == null)
        return;
      maskedEditBoxEditor.MaskTextBox.Mask = this.Mask;
      maskedEditBoxEditor.MaskTextBox.MaskType = this.MaskType;
      maskedEditBoxEditor.MaskTextBox.TextMaskFormat = this.TextMaskFormat;
      maskedEditBoxEditor.NullValue = RadDataConverter.Instance.Format(this.NullValue, typeof (string), (IDataConversionInfoProvider) this) as string;
      maskedEditBoxEditor.MaskTextBox.PromptChar = this.PromptChar;
      maskedEditBoxEditor.MaskTextBox.EnableNullValueInput = this.EnableNullValueInput;
    }

    public override System.Type GetDefaultEditorType()
    {
      return typeof (RadMaskedEditBoxEditor);
    }
  }
}
