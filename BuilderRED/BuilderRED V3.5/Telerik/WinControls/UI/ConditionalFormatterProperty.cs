// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ConditionalFormatterProperty
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Telerik.WinControls.UI
{
  public class ConditionalFormatterProperty
  {
    private Color rowForeColor = Color.Empty;
    private Color rowBackColor = Color.Empty;
    private Color cellForeColor = Color.Empty;
    private Color cellBackColor = Color.Empty;
    private ContentAlignment textAlignment = ContentAlignment.MiddleLeft;
    private ContentAlignment rowTextAlignment = ContentAlignment.MiddleLeft;
    private bool enabled = true;
    private bool applyOnSelectedRows = true;
    private Font cellFont;
    private Font rowFont;
    private bool caseSensitive;

    public void CopyFrom(BaseFormattingObject format)
    {
      this.rowForeColor = format.RowForeColor;
      this.rowBackColor = format.RowBackColor;
      this.cellForeColor = format.CellForeColor;
      this.cellBackColor = format.CellBackColor;
      this.textAlignment = format.TextAlignment;
      this.rowTextAlignment = format.RowTextAlignment;
      this.cellFont = format.CellFont;
      this.rowFont = format.RowFont;
      this.enabled = format.Enabled;
      this.applyOnSelectedRows = format.ApplyOnSelectedRows;
      ConditionalFormattingObject formattingObject = format as ConditionalFormattingObject;
      if (formattingObject == null)
        return;
      this.caseSensitive = formattingObject.CaseSensitive;
    }

    public void CopyTo(BaseFormattingObject format)
    {
      format.RowForeColor = this.rowForeColor;
      format.RowBackColor = this.rowBackColor;
      format.CellForeColor = this.cellForeColor;
      format.CellBackColor = this.cellBackColor;
      format.TextAlignment = this.textAlignment;
      format.RowTextAlignment = this.rowTextAlignment;
      format.CellFont = this.cellFont;
      format.RowFont = this.rowFont;
      format.Enabled = this.enabled;
      ConditionalFormattingObject formattingObject = format as ConditionalFormattingObject;
      if (formattingObject == null)
        return;
      formattingObject.CaseSensitive = this.caseSensitive;
    }

    [Category("Design")]
    [DesignOnly(false)]
    [Browsable(true)]
    [Description("Enter the alignment to be used for the cell values")]
    [Bindable(false)]
    [ReadOnly(false)]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
      }
    }

    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Browsable(true)]
    [Bindable(false)]
    [DesignOnly(false)]
    [Category("Design")]
    [Description("Enter the alignment to be used for the cell values, when ApplyToRow is true.")]
    [ReadOnly(false)]
    public ContentAlignment RowTextAlignment
    {
      get
      {
        return this.rowTextAlignment;
      }
      set
      {
        this.rowTextAlignment = value;
      }
    }

    [DesignOnly(false)]
    [DefaultValue(false)]
    [Category("Design")]
    [Description("Determines whether case-sensitive comparisons will be made when evaluating string values.")]
    [Browsable(true)]
    [ReadOnly(false)]
    [Bindable(false)]
    public bool CaseSensitive
    {
      get
      {
        return this.caseSensitive;
      }
      set
      {
        this.caseSensitive = value;
      }
    }

    [ReadOnly(false)]
    [DefaultValue(true)]
    [DesignOnly(false)]
    [Description("Determines whether the condition is enabled (can be evaluated and applied).")]
    [Browsable(true)]
    [Category("Design")]
    [Bindable(false)]
    public bool Enabled
    {
      get
      {
        return this.enabled;
      }
      set
      {
        this.enabled = value;
      }
    }

    [ReadOnly(false)]
    [DesignOnly(false)]
    [Description("Enter the font to be used for the cell.")]
    [Browsable(true)]
    [Category("Design")]
    [Bindable(false)]
    [DefaultValue(null)]
    public Font CellFont
    {
      get
      {
        return this.cellFont;
      }
      set
      {
        this.cellFont = value;
      }
    }

    [Description("Enter the font to be used for the entire row.")]
    [DesignOnly(false)]
    [Category("Design")]
    [Browsable(true)]
    [ReadOnly(false)]
    [Bindable(false)]
    [DefaultValue(null)]
    public Font RowFont
    {
      get
      {
        return this.rowFont;
      }
      set
      {
        this.rowFont = value;
      }
    }

    [Browsable(true)]
    [ReadOnly(false)]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Design")]
    [Bindable(false)]
    [DefaultValue("")]
    [DesignOnly(false)]
    [Description("Enter the foreground color to be used for the entire row")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public Color RowForeColor
    {
      get
      {
        return this.rowForeColor;
      }
      set
      {
        this.rowForeColor = value;
      }
    }

    [DefaultValue("")]
    [Description("Enter the background color to be used for the entire row")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Bindable(false)]
    [DesignOnly(false)]
    [ReadOnly(false)]
    [Category("Design")]
    [Browsable(true)]
    public Color RowBackColor
    {
      get
      {
        return this.rowBackColor;
      }
      set
      {
        this.rowBackColor = value;
      }
    }

    [Description("Enter the foreground color to be used for the cell")]
    [DefaultValue("")]
    [DesignOnly(false)]
    [Category("Design")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Browsable(true)]
    [ReadOnly(false)]
    [Bindable(false)]
    public Color CellForeColor
    {
      get
      {
        return this.cellForeColor;
      }
      set
      {
        this.cellForeColor = value;
      }
    }

    [Browsable(true)]
    [Bindable(false)]
    [DefaultValue("")]
    [DesignOnly(false)]
    [Description("Enter the background color to be used for the cell")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Design")]
    [ReadOnly(false)]
    public Color CellBackColor
    {
      get
      {
        return this.cellBackColor;
      }
      set
      {
        this.cellBackColor = value;
      }
    }

    [Browsable(true)]
    [Bindable(false)]
    [DefaultValue(true)]
    [ReadOnly(false)]
    [Description("Determines whether to apply this condition on selected rows.")]
    [DesignOnly(false)]
    [Category("Design")]
    public bool ApplyOnSelectedRows
    {
      get
      {
        return this.applyOnSelectedRows;
      }
      set
      {
        this.applyOnSelectedRows = value;
      }
    }
  }
}
