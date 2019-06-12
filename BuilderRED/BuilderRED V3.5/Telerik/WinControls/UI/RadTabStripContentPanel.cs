// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTabStripContentPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadTabStripContentPanel : Panel
  {
    private RadControl owner;
    private RadItem associatedItem;

    public RadTabStripContentPanel()
      : this((RadControl) null)
    {
    }

    public RadTabStripContentPanel(RadControl owner)
    {
      this.owner = owner;
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    internal void SetAssociatedItem(RadItem associatedItem)
    {
      this.associatedItem = associatedItem;
    }

    public RadItem AssociatedItem
    {
      get
      {
        return this.associatedItem;
      }
    }

    internal int HeightInternal
    {
      get
      {
        return base.Height;
      }
      set
      {
        base.Height = value;
      }
    }

    internal RadControl Owner
    {
      get
      {
        return this.owner;
      }
    }

    internal int WidthInternal
    {
      get
      {
        return base.Width;
      }
      set
      {
        base.Width = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event EventHandler AutoSizeChanged
    {
      add
      {
        base.AutoSizeChanged += value;
      }
      remove
      {
        base.AutoSizeChanged -= value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler DockChanged
    {
      add
      {
        base.DockChanged += value;
      }
      remove
      {
        base.DockChanged -= value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event EventHandler TabIndexChanged
    {
      add
      {
        base.TabIndexChanged += value;
      }
      remove
      {
        base.TabIndexChanged -= value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler TabStopChanged
    {
      add
      {
        base.TabStopChanged += value;
      }
      remove
      {
        base.TabStopChanged -= value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler VisibleChanged
    {
      add
      {
        base.VisibleChanged += value;
      }
      remove
      {
        base.VisibleChanged -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override AnchorStyles Anchor
    {
      get
      {
        return base.Anchor;
      }
      set
      {
        base.Anchor = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override AutoSizeMode AutoSizeMode
    {
      get
      {
        return AutoSizeMode.GrowOnly;
      }
      set
      {
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new BorderStyle BorderStyle
    {
      get
      {
        return base.BorderStyle;
      }
      set
      {
        base.BorderStyle = value;
      }
    }

    protected override Padding DefaultMargin
    {
      get
      {
        return new Padding(0, 0, 0, 0);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        base.Dock = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override Size MaximumSize
    {
      get
      {
        return base.MaximumSize;
      }
      set
      {
        base.MaximumSize = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Size MinimumSize
    {
      get
      {
        return base.MinimumSize;
      }
      set
      {
        base.MinimumSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new string Name
    {
      get
      {
        return base.Name;
      }
      set
      {
        base.Name = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new int TabIndex
    {
      get
      {
        return base.TabIndex;
      }
      set
      {
        base.TabIndex = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new bool TabStop
    {
      get
      {
        return base.TabStop;
      }
      set
      {
        base.TabStop = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new bool Visible
    {
      get
      {
        return base.Visible;
      }
      set
      {
        base.Visible = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new int Height
    {
      get
      {
        return base.Height;
      }
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new int Width
    {
      get
      {
        return base.Width;
      }
      set
      {
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new Size Size
    {
      get
      {
        return base.Size;
      }
      set
      {
        base.Size = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new Point Location
    {
      get
      {
        return base.Location;
      }
      set
      {
        base.Location = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    public new bool CausesValidation
    {
      get
      {
        return base.CausesValidation;
      }
      set
      {
        base.CausesValidation = value;
      }
    }

    [DefaultValue("Transparent")]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }
  }
}
