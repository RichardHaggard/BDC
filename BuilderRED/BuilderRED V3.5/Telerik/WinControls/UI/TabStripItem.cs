// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabStripItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TabStripItem : RadPageViewStripItem
  {
    public static RadProperty CloseButtonOffsetProperty = RadProperty.Register(nameof (CloseButtonOffset), typeof (int), typeof (TabStripItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 5, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    private TabPanel tabPanel;
    private TabStripButtonItem closeButton;
    private TabStripPinButtonItem pinButton;
    private StackLayoutElement buttonsPanel;
    private bool showCloseButton;
    private bool showPinButton;
    private RadPageViewStripElement owner;

    public TabStripItem(TabPanel tabPanel)
      : base(tabPanel.Text)
    {
      this.Image = tabPanel.Image;
      this.tabPanel = tabPanel;
      this.showCloseButton = false;
      this.showPinButton = false;
      this.ToolTipText = tabPanel.ToolTipText;
      if (!(tabPanel is IPinnable))
        return;
      this.IsPinned = ((IPinnable) tabPanel).IsPinned;
      ((IPinnable) tabPanel).IsPinnedChanged += new EventHandler(this.IPinnable_IsPinnedChanged);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ClipDrawing = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.CreateCloseButton();
      this.CreatePinButton();
      this.buttonsPanel = new StackLayoutElement();
      this.buttonsPanel.Orientation = Orientation.Horizontal;
      this.buttonsPanel.ElementSpacing = 3;
      this.buttonsPanel.Children.Add((RadElement) this.pinButton);
      this.buttonsPanel.Children.Add((RadElement) this.closeButton);
      this.Children.Add((RadElement) this.buttonsPanel);
    }

    protected override void DisposeManagedResources()
    {
      if (this.closeButton != null)
        this.closeButton.Click -= new EventHandler(this.OnCloseButtonClick);
      if (this.pinButton != null)
        this.pinButton.Click -= new EventHandler(this.OnPinButtonClick);
      if (this.owner != null)
        this.owner.PropertyChanged -= new PropertyChangedEventHandler(this.owner_PropertyChanged);
      base.DisposeManagedResources();
    }

    private TabStripPanel TabStrip
    {
      get
      {
        if (this.tabPanel != null)
          return this.tabPanel.Parent as TabStripPanel;
        return (TabStripPanel) null;
      }
    }

    public TabPanel TabPanel
    {
      get
      {
        return this.tabPanel;
      }
    }

    public TabStripButtonItem CloseButton
    {
      get
      {
        return this.closeButton;
      }
    }

    public TabStripPinButtonItem PinButton
    {
      get
      {
        return this.pinButton;
      }
    }

    public bool ShowCloseButton
    {
      get
      {
        return this.showCloseButton;
      }
      set
      {
        if (this.showCloseButton == value)
          return;
        this.showCloseButton = value;
        if (this.ElementState != ElementState.Loaded)
          return;
        this.UpdateCloseButton(this.TabStrip);
      }
    }

    public bool ShowPinButton
    {
      get
      {
        return this.showPinButton;
      }
      set
      {
        if (this.showPinButton == value)
          return;
        this.showPinButton = value;
        if (this.ElementState != ElementState.Loaded)
          return;
        this.UpdatePinButton(this.TabStrip);
      }
    }

    public int CloseButtonOffset
    {
      get
      {
        return (int) this.GetValue(TabStripItem.CloseButtonOffsetProperty);
      }
      set
      {
        int num = (int) this.SetValue(TabStripItem.CloseButtonOffsetProperty, (object) value);
      }
    }

    private void CreateCloseButton()
    {
      this.closeButton = new TabStripButtonItem();
      this.closeButton.Class = "TabCloseButton";
      this.closeButton.BorderElement.Visibility = ElementVisibility.Collapsed;
      this.closeButton.ButtonFillElement.Visibility = ElementVisibility.Hidden;
      this.closeButton.StretchHorizontally = false;
      this.closeButton.StretchVertically = false;
      this.closeButton.Click += new EventHandler(this.OnCloseButtonClick);
      this.closeButton.Visibility = ElementVisibility.Collapsed;
      int num = (int) this.closeButton.BindProperty(TabStripButtonItem.IsSelectedProperty, (RadObject) this, RadPageViewItem.IsSelectedProperty, PropertyBindingOptions.TwoWay);
      this.closeButton.SetAllLocalValuesAsDefault(true);
    }

    private void CreatePinButton()
    {
      this.pinButton = new TabStripPinButtonItem();
      this.pinButton.Class = "TabPinButton";
      this.pinButton.BorderElement.Visibility = ElementVisibility.Collapsed;
      this.pinButton.ButtonFillElement.Visibility = ElementVisibility.Hidden;
      this.pinButton.StretchHorizontally = false;
      this.pinButton.StretchVertically = false;
      this.pinButton.Click += new EventHandler(this.OnPinButtonClick);
      this.pinButton.Visibility = ElementVisibility.Collapsed;
      int num1 = (int) this.pinButton.BindProperty(TabStripButtonItem.IsSelectedProperty, (RadObject) this, RadPageViewItem.IsSelectedProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.pinButton.BindProperty(TabStripPinButtonItem.IsPinnedProperty, (RadObject) this, RadPageViewItem.IsPinnedProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.pinButton.BindProperty(TabStripPinButtonItem.IsPreviewProperty, (RadObject) this, RadPageViewItem.IsPreviewProperty, PropertyBindingOptions.TwoWay);
      this.pinButton.SetAllLocalValuesAsDefault(true);
    }

    internal void UpdateTabButtoms(TabStripPanel owner)
    {
      this.UpdateCloseButton(owner);
      this.UpdatePinButton(owner);
    }

    private void UpdateCloseButton(TabStripPanel owner)
    {
      if (!this.showCloseButton)
      {
        this.closeButton.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        this.closeButton.AngleTransform = 360f - this.AngleTransform;
        this.closeButton.Visibility = ElementVisibility.Visible;
        if (owner == null)
          return;
        int closeButtonOffset = this.CloseButtonOffset;
        if (this.TextOrientation == Orientation.Vertical)
          this.closeButton.Alignment = ContentAlignment.TopLeft;
        else
          this.closeButton.Alignment = ContentAlignment.TopRight;
      }
    }

    private void UpdatePinButton(TabStripPanel owner)
    {
      if (!this.showPinButton)
      {
        this.pinButton.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        this.pinButton.AngleTransform = 360f - this.AngleTransform;
        this.pinButton.Visibility = ElementVisibility.Visible;
        if (owner == null)
          return;
        int closeButtonOffset = this.CloseButtonOffset;
        if (this.TextOrientation == Orientation.Vertical)
          this.pinButton.Alignment = ContentAlignment.TopLeft;
        else
          this.pinButton.Alignment = ContentAlignment.TopRight;
      }
    }

    private void UpdateItemContentOrientation()
    {
      if (!(this.owner is RadPageViewTabStripElement))
        return;
      if (this.owner.TextOrientation == Orientation.Vertical)
      {
        if (this.owner.StripAlignment == StripViewAlignment.Right || this.owner.StripAlignment == StripViewAlignment.Left)
        {
          this.owner.ItemContentOrientation = PageViewContentOrientation.Vertical270;
          this.buttonsPanel.Orientation = Orientation.Horizontal;
        }
        else
        {
          this.owner.ItemContentOrientation = PageViewContentOrientation.Horizontal180;
          this.buttonsPanel.Orientation = Orientation.Vertical;
        }
      }
      else if (this.owner.StripAlignment == StripViewAlignment.Right)
      {
        this.owner.ItemContentOrientation = PageViewContentOrientation.Vertical90;
        this.buttonsPanel.Orientation = Orientation.Vertical;
      }
      else if (this.owner.StripAlignment == StripViewAlignment.Left)
      {
        this.owner.ItemContentOrientation = PageViewContentOrientation.Vertical270;
        this.buttonsPanel.Orientation = Orientation.Vertical;
      }
      else
      {
        this.owner.ItemContentOrientation = PageViewContentOrientation.Horizontal;
        this.buttonsPanel.Orientation = Orientation.Horizontal;
      }
      this.SetContentOrientation(this.owner.ItemContentOrientation, false);
      if (this.owner.StripAlignment == StripViewAlignment.Left || this.buttonsPanel.Orientation == Orientation.Vertical && this.owner.StripAlignment == StripViewAlignment.Top || this.buttonsPanel.Orientation == Orientation.Horizontal && this.owner.StripAlignment == StripViewAlignment.Bottom)
      {
        this.buttonsPanel.Children.Remove((RadElement) this.pinButton);
        this.buttonsPanel.Children.Insert(1, (RadElement) this.pinButton);
      }
      else
      {
        this.buttonsPanel.Children.Remove((RadElement) this.pinButton);
        this.buttonsPanel.Children.Insert(0, (RadElement) this.pinButton);
      }
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.owner = this.Owner as RadPageViewStripElement;
      if (this.owner != null)
        this.owner.PropertyChanged += new PropertyChangedEventHandler(this.owner_PropertyChanged);
      this.UpdateTabButtoms(this.TabStrip);
      this.UpdateItemContentOrientation();
    }

    private void owner_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == RadPageViewStripElement.StripAlignmentProperty.Name) && !(e.PropertyName == RadItem.TextOrientationProperty.Name))
        return;
      this.UpdateItemContentOrientation();
    }

    private void OnCloseButtonClick(object sender, EventArgs e)
    {
      if (this.tabPanel == null || this.tabPanel.Site != null)
        return;
      (this.tabPanel.Parent as TabStripPanel)?.OnTabCloseButtonClicked(this);
    }

    private void OnPinButtonClick(object sender, EventArgs e)
    {
      if (this.tabPanel == null || this.tabPanel.Site != null)
        return;
      if (this.IsPreview)
      {
        this.owner.PreviewItem = (RadPageViewItem) null;
      }
      else
      {
        TabStripItem tabStripItem = this;
        tabStripItem.IsPinned = !tabStripItem.IsPinned;
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.AngleTransformProperty || e.Property == TabStripItem.CloseButtonOffsetProperty || e.Property == RadItem.TextOrientationProperty)
        this.UpdateTabButtoms(this.TabStrip);
      if (e.Property == RadPageViewItem.IsPinnedProperty && this.tabPanel is IPinnable)
      {
        IPinnable tabPanel = (IPinnable) this.tabPanel;
        tabPanel.IsPinnedChanged -= new EventHandler(this.IPinnable_IsPinnedChanged);
        tabPanel.IsPinned = this.IsPinned;
        tabPanel.IsPinnedChanged += new EventHandler(this.IPinnable_IsPinnedChanged);
      }
      if (e.Property != RadItem.TextProperty || !this.IsInValidState(true) || this.Owner == null)
        return;
      this.Owner.InvalidateMeasure(true);
    }

    private void IPinnable_IsPinnedChanged(object sender, EventArgs e)
    {
      if (!(this.tabPanel is IPinnable))
        return;
      this.IsPinned = ((IPinnable) this.tabPanel).IsPinned;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF size = base.MeasureOverride(availableSize);
      if (!this.showCloseButton && !this.showPinButton)
        return size;
      return this.owner.StripAlignment == StripViewAlignment.Top || this.owner.StripAlignment == StripViewAlignment.Bottom ? (this.owner.TextOrientation != Orientation.Horizontal ? this.IncreaseFinalHeight(size) : this.IncreaseFinalWidth(size)) : (this.owner.TextOrientation != Orientation.Horizontal ? this.IncreaseFinalWidth(size) : this.IncreaseFinalHeight(size));
    }

    private SizeF IncreaseFinalWidth(SizeF size)
    {
      float height = (double) size.Height > (double) this.buttonsPanel.DesiredSize.Height ? size.Height : this.buttonsPanel.DesiredSize.Height;
      return new SizeF(size.Width + this.buttonsPanel.DesiredSize.Width + (float) this.CloseButtonOffset, height);
    }

    private SizeF IncreaseFinalHeight(SizeF size)
    {
      return new SizeF((double) size.Width > (double) this.buttonsPanel.DesiredSize.Width ? size.Width : this.buttonsPanel.DesiredSize.Width, size.Height + this.buttonsPanel.DesiredSize.Height + (float) this.CloseButtonOffset);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      Padding padding = this.RotateTabItemPadding(this.Padding);
      int offsetTop = padding.Top + this.BorderThickness.Top;
      int offsetBottom = padding.Bottom + this.BorderThickness.Bottom;
      int offsetLeft = padding.Left + this.BorderThickness.Left;
      int offsetRight = padding.Right + this.BorderThickness.Right;
      this.ArrangeChildren(finalSize);
      if (this.owner.TextOrientation == Orientation.Horizontal)
        this.ArrangeHorizontally(finalSize, offsetTop, offsetBottom, offsetLeft, offsetRight);
      else
        this.ArrangeVertically(finalSize, offsetTop, offsetBottom, offsetLeft, offsetRight);
      return finalSize;
    }

    private void ArrangeHorizontally(
      SizeF finalSize,
      int offsetTop,
      int offsetBottom,
      int offsetLeft,
      int offsetRight)
    {
      int num1 = offsetLeft + offsetRight;
      int num2 = offsetTop + offsetBottom;
      RectangleF finalRect = new RectangleF();
      RectangleF bounds = new RectangleF();
      switch (this.owner.StripAlignment)
      {
        case StripViewAlignment.Top:
          if (this.showCloseButton || this.showPinButton)
          {
            finalRect = new RectangleF(finalSize.Width - this.buttonsPanel.DesiredSize.Width - (float) offsetRight, (float) ((double) finalSize.Height / 2.0 - (double) this.buttonsPanel.DesiredSize.Height / 2.0), this.buttonsPanel.DesiredSize.Width, this.buttonsPanel.DesiredSize.Height);
            bounds = new RectangleF((float) offsetLeft, (float) offsetTop, finalSize.Width - finalRect.Width - (float) num1, finalSize.Height - (float) num2);
            if (this.RightToLeft)
            {
              finalRect.X = (float) offsetRight;
              bounds.X = (float) offsetRight + this.buttonsPanel.DesiredSize.Width;
              break;
            }
            break;
          }
          bounds = new RectangleF((float) offsetLeft, (float) offsetTop, finalSize.Width - (float) num1, finalSize.Height - (float) num2);
          if (this.RightToLeft)
          {
            bounds.X = (float) offsetRight;
            break;
          }
          break;
        case StripViewAlignment.Right:
          if (this.showCloseButton || this.showPinButton)
          {
            finalRect = new RectangleF((float) ((double) finalSize.Width / 2.0 - (double) this.buttonsPanel.DesiredSize.Width / 2.0), finalSize.Height - this.buttonsPanel.DesiredSize.Height - (float) offsetBottom, this.buttonsPanel.DesiredSize.Height, this.buttonsPanel.DesiredSize.Width);
            bounds = new RectangleF((float) offsetTop, (float) offsetRight, finalSize.Height - finalRect.Height - (float) num2, finalSize.Width - (float) num1);
            if (this.RightToLeft)
            {
              finalRect.Y = (float) offsetBottom;
              bounds.X = (float) offsetBottom + this.buttonsPanel.DesiredSize.Width;
              break;
            }
            break;
          }
          bounds = new RectangleF((float) offsetTop, (float) offsetRight, finalSize.Height - (float) num2, finalSize.Width - (float) num1);
          if (this.RightToLeft)
          {
            bounds.X = (float) offsetBottom;
            break;
          }
          break;
        case StripViewAlignment.Bottom:
          if (this.showCloseButton || this.showPinButton)
          {
            finalRect = new RectangleF((float) offsetLeft, (float) ((double) finalSize.Height / 2.0 - (double) this.buttonsPanel.DesiredSize.Height / 2.0), this.buttonsPanel.DesiredSize.Width, this.buttonsPanel.DesiredSize.Height);
            bounds = new RectangleF((float) offsetLeft + this.buttonsPanel.DesiredSize.Width + (float) this.CloseButtonOffset, (float) offsetTop, finalSize.Width - finalRect.Width - (float) num1, finalSize.Height - (float) num2);
            if (this.RightToLeft)
            {
              finalRect.X = finalSize.Width - (float) offsetLeft - this.buttonsPanel.DesiredSize.Width;
              bounds.X = (float) (offsetRight - this.CloseButtonOffset);
              break;
            }
            break;
          }
          bounds = new RectangleF((float) offsetLeft, (float) offsetTop, finalSize.Width - (float) num1, finalSize.Height - (float) num2);
          if (this.RightToLeft)
          {
            bounds.X = (float) offsetRight;
            break;
          }
          break;
        case StripViewAlignment.Left:
          if (this.showCloseButton || this.showPinButton)
          {
            finalRect = new RectangleF((float) ((double) finalSize.Width / 2.0 - (double) this.buttonsPanel.DesiredSize.Width / 2.0), (float) offsetTop, this.buttonsPanel.DesiredSize.Height, this.buttonsPanel.DesiredSize.Width);
            bounds = new RectangleF((float) offsetBottom, (float) offsetRight, finalSize.Height - finalRect.Height - (float) num2, finalSize.Width - (float) num1);
            if (this.RightToLeft)
            {
              finalRect.Y = finalSize.Height - this.buttonsPanel.DesiredSize.Height - (float) offsetTop - (float) (this.CloseButtonOffset / 2);
              bounds.X = (float) offsetTop + this.buttonsPanel.DesiredSize.Width;
              break;
            }
            break;
          }
          bounds = new RectangleF((float) offsetBottom, (float) offsetRight, finalSize.Height - (float) num2, finalSize.Width - (float) num1);
          if (this.RightToLeft)
          {
            bounds.X = (float) offsetTop;
            break;
          }
          break;
      }
      if (this.showCloseButton || this.showPinButton)
        this.buttonsPanel.Arrange(finalRect);
      if (this.owner.TextOrientation == Orientation.Vertical)
      {
        this.Layout.LeftPart.Arrange(bounds);
        this.Layout.RightPart.Arrange(bounds);
      }
      else
        this.Layout.Arrange(bounds);
    }

    private void ArrangeVertically(
      SizeF finalSize,
      int offsetTop,
      int offsetBottom,
      int offsetLeft,
      int offsetRight)
    {
      int num1 = offsetLeft + offsetRight;
      int num2 = offsetTop + offsetBottom;
      RectangleF finalRect = new RectangleF();
      RectangleF bounds = new RectangleF();
      switch (this.owner.StripAlignment)
      {
        case StripViewAlignment.Top:
          if (this.showCloseButton || this.showPinButton)
          {
            finalRect = new RectangleF(finalSize.Width - this.buttonsPanel.DesiredSize.Width - (float) offsetRight, (float) offsetTop, this.buttonsPanel.DesiredSize.Width, this.buttonsPanel.DesiredSize.Height);
            bounds = new RectangleF((float) offsetTop, (float) offsetRight, finalSize.Width - (float) num1, finalSize.Height - finalRect.Width - (float) num2);
            break;
          }
          bounds = new RectangleF((float) offsetTop, (float) offsetRight, finalSize.Width - (float) num1, finalSize.Height - (float) num2);
          break;
        case StripViewAlignment.Right:
          if (this.showCloseButton || this.showPinButton)
          {
            finalRect = new RectangleF(finalSize.Width - this.buttonsPanel.DesiredSize.Width - (float) offsetRight, (float) offsetTop, this.buttonsPanel.DesiredSize.Height, this.buttonsPanel.DesiredSize.Width);
            bounds = new RectangleF((float) offsetBottom, (float) offsetRight, finalSize.Height - (float) num2, finalSize.Width - finalRect.Height - (float) num1);
            break;
          }
          bounds = new RectangleF((float) offsetBottom, (float) offsetRight, finalSize.Height - (float) num2, finalSize.Width - (float) num1);
          break;
        case StripViewAlignment.Bottom:
          if (this.showCloseButton || this.showPinButton)
          {
            finalRect = new RectangleF(finalSize.Width - this.buttonsPanel.DesiredSize.Width - (float) offsetRight, finalSize.Height - this.buttonsPanel.DesiredSize.Height - (float) offsetBottom, this.buttonsPanel.DesiredSize.Width, this.buttonsPanel.DesiredSize.Height);
            bounds = new RectangleF((float) offsetLeft, (float) offsetTop + this.buttonsPanel.DesiredSize.Width + (float) this.CloseButtonOffset, finalSize.Width, finalSize.Height - finalRect.Width - (float) num2);
            break;
          }
          bounds = new RectangleF(finalSize.Width - (float) offsetLeft - this.Layout.DesiredSize.Width, (float) offsetTop, finalSize.Width - (float) num1, finalSize.Height - (float) num2);
          break;
        case StripViewAlignment.Left:
          if (this.showCloseButton || this.showPinButton)
          {
            finalRect = new RectangleF((float) offsetLeft, (float) offsetTop, this.buttonsPanel.DesiredSize.Height, this.buttonsPanel.DesiredSize.Width);
            bounds = new RectangleF((float) offsetTop, (float) offsetRight + this.buttonsPanel.DesiredSize.Width + (float) this.CloseButtonOffset, finalSize.Height, finalSize.Width - finalRect.Height - (float) num1);
            break;
          }
          bounds = new RectangleF(finalSize.Height - (float) offsetTop - this.Layout.DesiredSize.Width, (float) offsetLeft, finalSize.Height - (float) num2, finalSize.Width - (float) num1);
          break;
      }
      if (this.showCloseButton || this.showPinButton)
        this.buttonsPanel.Arrange(finalRect);
      if (this.owner.TextOrientation == Orientation.Vertical)
      {
        this.Layout.LeftPart.Arrange(bounds);
        this.Layout.RightPart.Arrange(bounds);
      }
      else
        this.Layout.Arrange(bounds);
    }

    private Padding RotateTabItemPadding(Padding padding)
    {
      switch (this.owner.StripAlignment)
      {
        case StripViewAlignment.Right:
          padding = new Padding(padding.Bottom, padding.Left, padding.Top, padding.Right);
          break;
        case StripViewAlignment.Bottom:
          padding = new Padding(padding.Right, padding.Bottom, padding.Left, padding.Top);
          break;
        case StripViewAlignment.Left:
          padding = new Padding(padding.Top, padding.Right, padding.Bottom, padding.Left);
          break;
      }
      return padding;
    }
  }
}
