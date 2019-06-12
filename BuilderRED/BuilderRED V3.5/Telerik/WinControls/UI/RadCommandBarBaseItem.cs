// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarBaseItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [DefaultEvent("Click")]
  public class RadCommandBarBaseItem : RadCommandBarVisualElement
  {
    public static RoutedEvent ClickEvent = RadElement.RegisterRoutedEvent(nameof (ClickEvent), typeof (RadCommandBarBaseItem));
    public static RoutedEvent VisibleInStripChangedEvent = RadElement.RegisterRoutedEvent(nameof (VisibleInStripChangedEvent), typeof (RadCommandBarBaseItem));
    public static RoutedEvent VisibleInStripChangingEvent = RadElement.RegisterRoutedEvent(nameof (VisibleInStripChangingEvent), typeof (RadCommandBarBaseItem));
    public static RadProperty VisibleInStripProperty = RadProperty.Register(nameof (VisibleInStrip), typeof (bool), typeof (RadCommandBarBaseItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    private bool visibleInStrip = true;
    private bool inheritsParentOrientation = true;
    private bool visibleInOverflowMenu = true;
    private Size localMinSize;

    public event EventHandler OrientationChanged;

    public event CancelEventHandler OrientationChanging;

    public event EventHandler VisibleInStripChanged;

    [Description("Indicates whether the item should be drawn in the overflow menu.")]
    [DefaultValue(true)]
    [Browsable(true)]
    [Category("Appearance")]
    public bool VisibleInOverflowMenu
    {
      get
      {
        return this.visibleInOverflowMenu;
      }
      set
      {
        this.visibleInOverflowMenu = value;
      }
    }

    [DefaultValue(Orientation.Horizontal)]
    [Description("Gets or sets the orientation of the item.")]
    [Browsable(true)]
    [Category("Appearance")]
    public override Orientation Orientation
    {
      get
      {
        return base.Orientation;
      }
      set
      {
        if (this.Orientation == value || this.OnOrientationChanging(new CancelEventArgs()))
          return;
        this.SetOrientationCore(value);
        this.OnOrientationChanged(new EventArgs());
      }
    }

    private void SetOrientationCore(Orientation value)
    {
      this.cachedOrientation = value;
      bool stretchHorizontally = this.StretchHorizontally;
      int num1 = (int) this.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) this.StretchVertically);
      int num2 = (int) this.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) stretchHorizontally);
    }

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Indicates whether the item should be drawn in the strip.")]
    [DefaultValue(true)]
    public virtual bool VisibleInStrip
    {
      get
      {
        return this.visibleInStrip;
      }
      set
      {
        if (this.visibleInStrip == value)
          return;
        CancelEventArgs cancelEventArgs = new CancelEventArgs();
        this.RaiseBubbleEvent((RadElement) this, new RoutedEventArgs((EventArgs) cancelEventArgs, RadCommandBarBaseItem.VisibleInStripChangingEvent));
        if (cancelEventArgs.Cancel)
          return;
        this.visibleInStrip = value;
        int num1 = (int) this.SetValue(RadElement.MinSizeProperty, (object) (value ? this.localMinSize : Size.Empty));
        int num2 = (int) this.SetValue(RadCommandBarBaseItem.VisibleInStripProperty, (object) value);
        this.OnVisibleInStripChanged(new EventArgs());
      }
    }

    [Browsable(true)]
    [Description("Indicates whether the item's orientation will be affected by parent's orientation.")]
    [Category("Appearance")]
    [DefaultValue(true)]
    public virtual bool InheritsParentOrientation
    {
      get
      {
        return this.inheritsParentOrientation;
      }
      set
      {
        this.inheritsParentOrientation = value;
      }
    }

    protected virtual void OnOrientationChanged(EventArgs e)
    {
      if (this.OrientationChanged == null)
        return;
      this.OrientationChanged((object) this, e);
    }

    protected virtual bool OnOrientationChanging(CancelEventArgs e)
    {
      if (this.OrientationChanging == null)
        return false;
      this.OrientationChanging((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnVisibleInStripChanged(EventArgs e)
    {
      if (this.VisibleInStripChanged != null)
        this.VisibleInStripChanged((object) this, e);
      this.RaiseBubbleEvent((RadElement) this, new RoutedEventArgs(new EventArgs(), RadCommandBarBaseItem.VisibleInStripChangedEvent));
    }

    public override Size MinSize
    {
      get
      {
        return base.MinSize;
      }
      set
      {
        this.localMinSize = value;
        if (!this.VisibleInStrip)
          return;
        base.MinSize = value;
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DoubleClickEnabled = false;
    }

    [Browsable(true)]
    [Category("Action")]
    [Description("Occurs when the element is double-clicked.")]
    public override event EventHandler DoubleClick
    {
      add
      {
        this.DoubleClickEnabled = true;
        base.DoubleClick += value;
      }
      remove
      {
        base.DoubleClick -= value;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Alignment = ContentAlignment.MiddleCenter;
      int num1 = (int) this.SetDefaultValueOverride(RadElement.StretchHorizontallyProperty, (object) false);
      int num2 = (int) this.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      this.localMinSize = this.MinSize;
    }

    protected override SizeF MeasureCore(SizeF availableSize)
    {
      SizeF baseSize = base.MeasureCore(availableSize);
      if (!this.VisibleInStrip && this.Site == null)
        return SizeF.Empty;
      return this.DpiScale(baseSize);
    }

    protected virtual SizeF DpiScale(SizeF baseSize)
    {
      return RadControl.GetDpiScaledSize(baseSize);
    }

    public override bool ShouldPaint
    {
      get
      {
        if (!this.VisibleInStrip && this.Site == null)
          return false;
        return base.ShouldPaint;
      }
      set
      {
        base.ShouldPaint = value;
      }
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      this.RaiseBubbleEvent((RadElement) this, new RoutedEventArgs(new EventArgs(), RadCommandBarBaseItem.ClickEvent));
    }

    protected virtual SizeF ExcludeBorders(SizeF elementSize)
    {
      elementSize.Height -= this.BorderBottomWidth + this.BorderTopWidth;
      elementSize.Width -= this.BorderRightWidth + this.BorderLeftWidth;
      return elementSize;
    }

    protected virtual SizeF IncludeBorders(SizeF elementSize)
    {
      elementSize.Height += this.BorderBottomWidth + this.BorderTopWidth;
      elementSize.Width += this.BorderRightWidth + this.BorderLeftWidth;
      return elementSize;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
    }
  }
}
