// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollViewer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadScrollViewer : RadItem, IScrollViewer
  {
    internal const ulong RadScrollViewerLastStateKey = 4398046511104;
    private RadScrollLayoutPanel scrollPanel;
    private BorderPrimitive border;
    private FillPrimitive fillPrimitive;

    public event RadScrollPanelHandler Scroll;

    protected virtual void OnScroll(ScrollPanelEventArgs args)
    {
      if (this.Scroll == null)
        return;
      this.Scroll((object) this, args);
    }

    public event ScrollNeedsHandler ScrollNeedsChanged;

    protected virtual void OnScrollNeedsChanged(ScrollNeedsEventArgs args)
    {
      if (this.ScrollNeedsChanged == null)
        return;
      this.ScrollNeedsChanged((object) this, args);
    }

    public event RadPanelScrollParametersHandler ScrollParametersChanged;

    protected virtual void OnScrollParametersChanged(RadPanelScrollParametersEventArgs args)
    {
      if (this.ScrollParametersChanged == null)
        return;
      this.ScrollParametersChanged((object) this, args);
    }

    public RadScrollLayoutPanel ScrollLayoutPanel
    {
      get
      {
        return this.scrollPanel;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool CanHorizontalScroll
    {
      get
      {
        return this.scrollPanel.CanHorizontalScroll;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool CanVerticalScroll
    {
      get
      {
        return this.scrollPanel.CanVerticalScroll;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual RadElement Viewport
    {
      get
      {
        return this.scrollPanel.Viewport;
      }
      set
      {
        this.scrollPanel.Viewport = value;
      }
    }

    [Browsable(false)]
    public RadScrollBarElement VerticalScrollBar
    {
      get
      {
        return this.scrollPanel.VerticalScrollBar;
      }
    }

    [Browsable(false)]
    public RadScrollBarElement HorizontalScrollBar
    {
      get
      {
        return this.scrollPanel.HorizontalScrollBar;
      }
    }

    [RadDescription("HorizontalScrollState", typeof (RadScrollLayoutPanel))]
    [Browsable(true)]
    [Category("Behavior")]
    [RadDefaultValue("HorizontalScrollState", typeof (RadScrollLayoutPanel))]
    public ScrollState HorizontalScrollState
    {
      get
      {
        return this.scrollPanel.HorizontalScrollState;
      }
      set
      {
        this.scrollPanel.HorizontalScrollState = value;
      }
    }

    [RadDefaultValue("VerticalScrollState", typeof (RadScrollLayoutPanel))]
    [Browsable(true)]
    [Category("Behavior")]
    [RadDescription("VerticalScrollState", typeof (RadScrollLayoutPanel))]
    public ScrollState VerticalScrollState
    {
      get
      {
        return this.scrollPanel.VerticalScrollState;
      }
      set
      {
        this.scrollPanel.VerticalScrollState = value;
      }
    }

    [RadDescription("UsePhysicalScrolling", typeof (RadScrollLayoutPanel))]
    [Browsable(true)]
    [Category("Behavior")]
    [RadDefaultValue("UsePhysicalScrolling", typeof (RadScrollLayoutPanel))]
    public bool UsePhysicalScrolling
    {
      get
      {
        return this.scrollPanel.UsePhysicalScrolling;
      }
      set
      {
        this.scrollPanel.UsePhysicalScrolling = value;
      }
    }

    [RadDefaultValue("PixelsPerLineScroll", typeof (RadScrollLayoutPanel))]
    [Browsable(true)]
    [Category("Behavior")]
    [RadDescription("PixelsPerLineScroll", typeof (RadScrollLayoutPanel))]
    public Point PixelsPerLineScroll
    {
      get
      {
        return this.scrollPanel.PixelsPerLineScroll;
      }
      set
      {
        this.scrollPanel.PixelsPerLineScroll = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [RadDefaultValue("MinValue", typeof (RadScrollLayoutPanel))]
    [RadDescription("MinValue", typeof (RadScrollLayoutPanel))]
    public Point MinValue
    {
      get
      {
        return this.scrollPanel.MinValue;
      }
    }

    [Browsable(true)]
    [RadDefaultValue("MaxValue", typeof (RadScrollLayoutPanel))]
    [Category("Behavior")]
    [RadDescription("MaxValue", typeof (RadScrollLayoutPanel))]
    public Point MaxValue
    {
      get
      {
        return this.scrollPanel.MaxValue;
      }
    }

    [RadDescription("Value", typeof (RadScrollLayoutPanel))]
    [Browsable(true)]
    [Category("Behavior")]
    [RadDefaultValue("Value", typeof (RadScrollLayoutPanel))]
    public Point Value
    {
      get
      {
        return this.scrollPanel.Value;
      }
      set
      {
        this.scrollPanel.Value = value;
      }
    }

    [Description("Gets or sets a value indicating whether the border is shown.")]
    [Category("Behavior")]
    [Browsable(true)]
    public bool ShowBorder
    {
      get
      {
        return this.border.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.border.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the fill is shown.")]
    public bool ShowFill
    {
      get
      {
        return this.fillPrimitive.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.fillPrimitive.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadElement FillElement
    {
      get
      {
        return (RadElement) this.fillPrimitive;
      }
    }

    [RadDefaultValue("ScrollThickness", typeof (RadScrollLayoutPanel))]
    [Browsable(true)]
    [Category("Behavior")]
    [RadDescription("ScrollThickness", typeof (RadScrollLayoutPanel))]
    public int ScrollThickness
    {
      get
      {
        return this.scrollPanel.ScrollThickness;
      }
      set
      {
        this.scrollPanel.ScrollThickness = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ForceViewportWidth
    {
      get
      {
        return this.scrollPanel.ForceViewportWidth;
      }
      set
      {
        this.scrollPanel.ForceViewportWidth = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool ForceViewportHeight
    {
      get
      {
        return this.scrollPanel.ForceViewportHeight;
      }
      set
      {
        this.scrollPanel.ForceViewportHeight = value;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ClipDrawing = true;
    }

    public RadScrollViewer()
    {
    }

    public RadScrollViewer(RadElement viewport)
    {
      this.Viewport = viewport;
    }

    [DefaultValue(true)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets whether RadScrollViewer uses UI virtualization")]
    public virtual bool Virtualized
    {
      get
      {
        IVirtualViewport viewport = this.Viewport as IVirtualViewport;
        if (viewport != null)
          return viewport.Virtualized;
        return true;
      }
      set
      {
        throw new InvalidOperationException("There is no base implementation for setting Virtualized property of RadScrollViewer. Inherit it and override the setter.");
      }
    }

    public void LineDown()
    {
      this.scrollPanel.ScrollWith(0, this.scrollPanel.SmallVerticalChange);
    }

    public void LineLeft()
    {
      this.scrollPanel.ScrollWith(-this.scrollPanel.SmallHorizontalChange, 0);
    }

    public void LineRight()
    {
      this.scrollPanel.ScrollWith(this.scrollPanel.SmallHorizontalChange, 0);
    }

    public void LineUp()
    {
      this.scrollPanel.ScrollWith(0, -this.scrollPanel.SmallVerticalChange);
    }

    public void PageDown()
    {
      this.scrollPanel.ScrollWith(0, this.scrollPanel.LargeVerticalChange);
    }

    public void PageLeft()
    {
      this.scrollPanel.ScrollWith(0, -this.scrollPanel.LargeHorizontalChange);
    }

    public void PageRight()
    {
      this.scrollPanel.ScrollWith(0, this.scrollPanel.LargeHorizontalChange);
    }

    public void PageUp()
    {
      this.scrollPanel.ScrollWith(0, -this.scrollPanel.LargeVerticalChange);
    }

    public void ScrollToTop()
    {
      this.scrollPanel.ScrollTo(this.scrollPanel.Value.X, 0);
    }

    public void ScrollToBottom()
    {
      this.scrollPanel.ScrollTo(this.scrollPanel.Value.X, this.scrollPanel.MaxValue.Y);
    }

    public void ScrollToLeftEnd()
    {
      this.scrollPanel.ScrollTo(0, this.scrollPanel.Value.Y);
    }

    public void ScrollToRightEnd()
    {
      this.scrollPanel.ScrollTo(this.scrollPanel.MaxValue.X, this.scrollPanel.Value.Y);
    }

    public void ScrollToHome()
    {
      this.scrollPanel.ScrollTo(0, 0);
    }

    public void ScrollToEnd()
    {
      this.scrollPanel.ScrollTo(this.scrollPanel.MaxValue.X, this.scrollPanel.MaxValue.Y);
    }

    public void ScrollElementIntoView(RadElement element)
    {
      if (this.scrollPanel == null)
        return;
      this.scrollPanel.ScrollElementIntoView(element);
    }

    protected override void CreateChildElements()
    {
      this.scrollPanel = this.CreateScrollLayoutPanel();
      this.scrollPanel.Scroll += (RadScrollPanelHandler) ((sender, args) => this.OnScroll(args));
      this.scrollPanel.ScrollNeedsChanged += (ScrollNeedsHandler) ((sender, args) => this.OnScrollNeedsChanged(args));
      this.scrollPanel.ScrollParametersChanged += (RadPanelScrollParametersHandler) ((sender, args) => this.OnScrollParametersChanged(args));
      this.border = new BorderPrimitive();
      this.border.Class = "RadScrollViewBorder";
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "RadScrollViewFill";
      this.fillPrimitive.GradientAngle = 45f;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.border);
      this.Children.Add((RadElement) this.scrollPanel);
      this.scrollPanel.AutoSize = this.AutoSize;
      this.scrollPanel.AutoSizeMode = this.AutoSizeMode;
      int num1 = (int) this.scrollPanel.BindProperty(RadElement.AutoSizeProperty, (RadObject) this, RadElement.AutoSizeProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.scrollPanel.BindProperty(RadElement.AutoSizeModeProperty, (RadObject) this, RadElement.AutoSizeModeProperty, PropertyBindingOptions.OneWay);
    }

    protected virtual RadScrollLayoutPanel CreateScrollLayoutPanel()
    {
      return new RadScrollLayoutPanel();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.MaxSizeProperty)
      {
        this.scrollPanel.MaxSize = this.SubtractBorderSize((Size) e.NewValue);
      }
      else
      {
        if (e.Property != RadElement.MinSizeProperty)
          return;
        this.scrollPanel.MinSize = this.SubtractBorderSize((Size) e.NewValue);
      }
    }

    private Size SubtractBorderSize(Size size)
    {
      size.Width = size.Width;
      size.Height = size.Height;
      return size;
    }
  }
}
