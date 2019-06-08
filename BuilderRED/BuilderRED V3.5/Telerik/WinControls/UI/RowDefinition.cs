// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RowDefinition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class RowDefinition : NotifyPropertyBase
  {
    private CellDefinitionsCollection cells = new CellDefinitionsCollection();
    private int height = 20;
    private Color backColor = Color.Empty;
    private RowTemplate template;

    public RowDefinition()
      : this(20)
    {
    }

    public RowDefinition(int height)
    {
      this.height = height;
      this.cells.Owner = this;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the RowTemplate that owns this RowDefinition.")]
    public RowTemplate RowTemplate
    {
      get
      {
        return this.template;
      }
      internal set
      {
        this.template = value;
      }
    }

    [Description("Gets a collection that contains all the cell definitions in the RowDefinition.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public CellDefinitionsCollection Cells
    {
      get
      {
        return this.cells;
      }
    }

    [Description("Gets the CellDefinition at the specified index.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public CellDefinition this[int index]
    {
      get
      {
        return this.cells[index];
      }
    }

    [DefaultValue(20)]
    [Category("Appearance")]
    [Description("Gets or sets the desired height of this RowDefinition.")]
    [Browsable(true)]
    public int Height
    {
      get
      {
        return this.height;
      }
      set
      {
        this.SetProperty<int>(nameof (Height), ref this.height, value);
      }
    }

    [DefaultValue(typeof (Color), "")]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the background color of this RowDefinition.")]
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
  }
}
