// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewTextBoxColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.UI
{
  public class GridViewTextBoxColumn : GridViewDataColumn
  {
    public static RadProperty CharacterCasingProperty = RadProperty.Register("CharacterCasing", typeof (CharacterCasing), typeof (GridViewTextBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) CharacterCasing.Normal));
    public static RadProperty MaxLengthProperty = RadProperty.Register(nameof (MaxLength), typeof (int), typeof (GridViewTextBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) (int) short.MaxValue));
    public static RadProperty MultilineProperty = RadProperty.Register(nameof (Multiline), typeof (bool), typeof (GridViewTextBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty AcceptsTabProperty = RadProperty.Register(nameof (AcceptsTab), typeof (bool), typeof (GridViewTextBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty AcceptsReturnProperty = RadProperty.Register(nameof (AcceptsReturn), typeof (bool), typeof (GridViewTextBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));

    public GridViewTextBoxColumn()
    {
    }

    public GridViewTextBoxColumn(string fieldName)
      : base(fieldName)
    {
    }

    public GridViewTextBoxColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
    }

    [Category("Behavior")]
    [Description("Gets or sets the maximum length of the text that can be entered.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(32767)]
    [Browsable(true)]
    public int MaxLength
    {
      get
      {
        return (int) this.GetValue(GridViewTextBoxColumn.MaxLengthProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewTextBoxColumn.MaxLengthProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the text in the column cells can span more than one line.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool Multiline
    {
      get
      {
        return (bool) this.GetValue(GridViewTextBoxColumn.MultilineProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewTextBoxColumn.MultilineProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating the character casing of the editor.")]
    [Category("Behavior")]
    [DefaultValue(CharacterCasing.Normal)]
    [Browsable(true)]
    public CharacterCasing ColumnCharacterCasing
    {
      get
      {
        return (CharacterCasing) this.GetValue(GridViewTextBoxColumn.CharacterCasingProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewTextBoxColumn.CharacterCasingProperty, (object) value);
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the editor accepts the tab key in multiline mode")]
    [Browsable(true)]
    public bool AcceptsTab
    {
      get
      {
        return (bool) this.GetValue(GridViewTextBoxColumn.AcceptsTabProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewTextBoxColumn.AcceptsTabProperty, (object) value);
      }
    }

    [Description("Gets or sets whether the editor accepts the ENTER key in multiline mode")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool AcceptsReturn
    {
      get
      {
        return (bool) this.GetValue(GridViewTextBoxColumn.AcceptsReturnProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewTextBoxColumn.AcceptsReturnProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the type of the excel export.")]
    [DefaultValue(DisplayFormatType.Text)]
    public override DisplayFormatType ExcelExportType
    {
      get
      {
        if (!this.excelFormat.HasValue)
          return DisplayFormatType.Text;
        return base.ExcelExportType;
      }
      set
      {
        base.ExcelExportType = value;
      }
    }

    public override IInputEditor GetDefaultEditor()
    {
      return (IInputEditor) new RadTextBoxEditor();
    }

    public override void InitializeEditor(IInputEditor editor)
    {
      RadTextBoxEditor radTextBoxEditor = editor as RadTextBoxEditor;
      if (radTextBoxEditor != null)
      {
        radTextBoxEditor.MaxLength = this.MaxLength;
        radTextBoxEditor.Multiline = this.Multiline;
        radTextBoxEditor.AcceptsTab = this.AcceptsTab;
        radTextBoxEditor.AcceptsReturn = this.AcceptsReturn;
        radTextBoxEditor.CharacterCasing = this.ColumnCharacterCasing;
        radTextBoxEditor.NullValue = this.NullValue != null ? RadDataConverter.Instance.Format(this.NullValue, typeof (string), (IDataConversionInfoProvider) this) as string : (string) null;
      }
      else
      {
        RadTextBoxControlEditor boxControlEditor = editor as RadTextBoxControlEditor;
        if (boxControlEditor == null)
          return;
        boxControlEditor.MaxLength = this.MaxLength;
        boxControlEditor.Multiline = this.Multiline;
        boxControlEditor.AcceptsTab = this.AcceptsTab;
        boxControlEditor.AcceptsReturn = this.AcceptsReturn;
        boxControlEditor.CharacterCasing = this.ColumnCharacterCasing;
        boxControlEditor.NullText = this.NullValue != null ? RadDataConverter.Instance.Format(this.NullValue, typeof (string), (IDataConversionInfoProvider) this) as string : (string) null;
      }
    }

    public override System.Type GetDefaultEditorType()
    {
      return typeof (RadTextBoxEditor);
    }
  }
}
