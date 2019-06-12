// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CellDefinition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class CellDefinition : NotifyPropertyBase
  {
    private string uniqueName = "";
    private int width = -1;
    private int colSpan = 1;
    private int rowSpan = 1;
    private Color backColor = Color.Empty;
    private int border = -1;
    private RowDefinition row;
    private int columnIndex;

    public CellDefinition()
      : this("", 50, 1, 1)
    {
    }

    public CellDefinition(string uniqueName)
      : this(uniqueName, 50, 1, 1)
    {
    }

    public CellDefinition(string uniqueName, int width, int colSpan, int rowSpan)
    {
      this.uniqueName = uniqueName;
      this.width = width;
      this.colSpan = colSpan;
      this.rowSpan = rowSpan;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the RowDefinition that owns this CellDefinition.")]
    public RowDefinition Row
    {
      get
      {
        return this.row;
      }
      internal set
      {
        this.row = value;
      }
    }

    [Description("Gets the row index of this CellDefinition.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int RowIndex
    {
      get
      {
        return this.Row.RowTemplate.Rows.IndexOf(this.Row);
      }
    }

    [Description("Gets the column index of this CellDefinition.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int ColumnIndex
    {
      get
      {
        return this.columnIndex;
      }
      internal set
      {
        this.columnIndex = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the unique name of the column related with this CellDefinition.")]
    [DefaultValue("")]
    [Category("Appearance")]
    public string UniqueName
    {
      get
      {
        return this.uniqueName;
      }
      set
      {
        this.SetProperty<string>(nameof (UniqueName), ref this.uniqueName, value);
      }
    }

    [Description("Gets or sets the desired width of this CellDefinition.")]
    [Browsable(true)]
    [DefaultValue(-1)]
    [Category("Appearance")]
    public int Width
    {
      get
      {
        return this.width;
      }
      set
      {
        this.SetProperty<int>(nameof (Width), ref this.width, value);
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the cell span value of this CellDefinition.")]
    [DefaultValue(1)]
    [Category("Appearance")]
    public int ColSpan
    {
      get
      {
        return this.colSpan;
      }
      set
      {
        this.SetProperty<int>(nameof (ColSpan), ref this.colSpan, value);
      }
    }

    [Category("Appearance")]
    [DefaultValue(1)]
    [Browsable(true)]
    [Description("Gets or sets the row span value of this CellDefinition.")]
    public int RowSpan
    {
      get
      {
        return this.rowSpan;
      }
      set
      {
        this.SetProperty<int>(nameof (RowSpan), ref this.rowSpan, value);
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the background color of this CellDefinition.")]
    [DefaultValue(typeof (Color), "")]
    [Category("Appearance")]
    public Color BackColor
    {
      get
      {
        return this.backColor;
      }
      set
      {
        this.SetProperty<Color>(nameof (BackColor), ref this.backColor, value);
      }
    }

    [Description("Gets or sets the border width for this CellDefinition.")]
    [DefaultValue(-1)]
    [Category("Appearance")]
    [Browsable(true)]
    public int Border
    {
      get
      {
        return this.border;
      }
      set
      {
        this.SetProperty<int>(nameof (Border), ref this.border, value);
      }
    }
  }
}
