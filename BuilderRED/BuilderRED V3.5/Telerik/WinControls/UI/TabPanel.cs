// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabPanel
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
  public class TabPanel : ContainerControl, INotifyPropertyChanged
  {
    private BorderStyle borderStyle;
    private TabStripItem tabItem;
    private string toolTip;
    private Image image;

    public TabPanel()
    {
      this.toolTip = string.Empty;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
    }

    [DefaultValue(typeof (BorderStyle), "None")]
    public BorderStyle BorderStyle
    {
      get
      {
        return this.borderStyle;
      }
      set
      {
        if (this.borderStyle == value)
          return;
        if (!Telerik.WinControls.ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 2))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (BorderStyle));
        this.borderStyle = value;
        this.UpdateStyles();
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return new Size(200, 200);
      }
    }

    [DefaultValue(null)]
    [Localizable(true)]
    [Description("Gets or sets the Image associated with the panel.")]
    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        if (this.image == value)
          return;
        this.image = value;
        this.OnImageChanged(EventArgs.Empty);
      }
    }

    protected virtual void OnImageChanged(EventArgs e)
    {
      if (this.tabItem == null)
        return;
      this.tabItem.Image = this.image;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public TabStripItem TabStripItem
    {
      get
      {
        return this.tabItem;
      }
      set
      {
        if (this.tabItem == value)
          return;
        if (this.tabItem != null)
        {
          if (this.tabItem.TabPanel.TabStrip != null)
          {
            lock (this.tabItem.TabPanel.TabStrip.TabStripElement)
            {
              if (this.tabItem.TabPanel.TabStrip.TabStripElement.Items.Contains((RadPageViewItem) this.tabItem))
                this.tabItem.TabPanel.TabStrip.TabStripElement.RemoveItem((RadPageViewItem) this.tabItem);
            }
          }
          if (!this.tabItem.IsDisposed)
            this.tabItem.Dispose();
        }
        this.tabItem = value;
        if (this.tabItem == null)
          return;
        this.tabItem.ToolTipText = this.toolTip;
      }
    }

    [Description("Gets or sets the tooltip to be displayed when the mouse hovers the tabitem of this panel.")]
    public string ToolTipText
    {
      get
      {
        return this.toolTip;
      }
      set
      {
        if (value == null)
          value = string.Empty;
        if (value == this.toolTip)
          return;
        this.toolTip = value;
        this.OnToolTipTextChanged(EventArgs.Empty);
      }
    }

    protected virtual void OnToolTipTextChanged(EventArgs e)
    {
      if (this.tabItem == null)
        return;
      this.tabItem.ToolTipText = this.toolTip;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeToolTipText()
    {
      return !string.IsNullOrEmpty(this.toolTip);
    }

    [Browsable(false)]
    public TabStripPanel TabStrip
    {
      get
      {
        return this.Parent as TabStripPanel;
      }
    }

    public T GetTabStrip<T>() where T : TabStripPanel
    {
      return this.Parent as T;
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new bool Enabled
    {
      get
      {
        return base.Enabled;
      }
      set
      {
        base.Enabled = value;
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DefaultValue(typeof (Size), "0, 0")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event EventHandler EnabledChanged
    {
      add
      {
        base.EnabledChanged += value;
      }
      remove
      {
        base.EnabledChanged -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event EventHandler LocationChanged
    {
      add
      {
        base.LocationChanged += value;
      }
      remove
      {
        base.LocationChanged -= value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public new event EventHandler TextChanged
    {
      add
      {
        base.TextChanged += value;
      }
      remove
      {
        base.TextChanged -= value;
      }
    }

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

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
