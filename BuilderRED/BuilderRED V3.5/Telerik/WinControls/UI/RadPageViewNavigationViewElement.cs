// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewNavigationViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadPageViewNavigationViewElement : RadPageViewStripElement
  {
    public static RadProperty DisplayModeProperty = RadProperty.Register(nameof (DisplayMode), typeof (NavigationViewDisplayModes), typeof (RadPageViewNavigationViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) NavigationViewDisplayModes.Expanded, ElementPropertyOptions.AffectsLayout));
    public static RadProperty HeaderHeightProperty = RadProperty.Register(nameof (HeaderHeight), typeof (int), typeof (RadPageViewNavigationViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 40, ElementPropertyOptions.AffectsLayout));
    public static RadProperty ExpandedPaneWidthProperty = RadProperty.Register(nameof (ExpandedPaneWidth), typeof (int), typeof (RadPageViewNavigationViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 280, ElementPropertyOptions.AffectsLayout));
    public static RadProperty CollapsedPaneWidthProperty = RadProperty.Register(nameof (CollapsedPaneWidth), typeof (int), typeof (RadPageViewNavigationViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 40, ElementPropertyOptions.AffectsLayout));
    public static RadProperty CompactModeThresholdWidthProperty = RadProperty.Register(nameof (CompactModeThresholdWidth), typeof (int), typeof (RadPageViewNavigationViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 641, ElementPropertyOptions.AffectsLayout));
    public static RadProperty ExpandedModeThresholdWidthProperty = RadProperty.Register(nameof (ExpandedModeThresholdWidth), typeof (int), typeof (RadPageViewNavigationViewElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1008, ElementPropertyOptions.AffectsLayout));
    private NavigationViewDisplayModes displayMode = NavigationViewDisplayModes.Expanded;
    private bool allowAnimation = true;
    private bool isCollapsed = true;
    private int animationInterval = 5;
    private int animationFrames = 10;
    private NavigationViewDisplayModes displayModeCache;
    private NavigationViewHeaderElement headerElement;
    private HamburgerButtonElement hamburgerButton;
    private RadPopupControlBase popup;
    private StackLayoutElement popupStack;
    private bool programmaticallyClosingPopup;

    public static void ApplyThemeToPopup(RadElementTree elementTree, RadPopupControlBase popup)
    {
      string str = "ControlDefault";
      if (elementTree != null && elementTree.ComponentTreeHandler != null && !string.IsNullOrEmpty(elementTree.ComponentTreeHandler.ThemeName))
        str = elementTree.ComponentTreeHandler.ThemeName;
      if (!(popup.ThemeName != str))
        return;
      popup.ThemeName = str;
      if (popup.RootElement.ElementState != ElementState.Loaded)
        return;
      popup.RootElement.UpdateLayout();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.headerElement = this.CreateHeaderElement();
      this.Children.Add((RadElement) this.headerElement);
      this.hamburgerButton = this.CreateHamburgerButton();
      this.Children.Add((RadElement) this.hamburgerButton);
      this.StripAlignment = this.RightToLeft ? StripViewAlignment.Right : StripViewAlignment.Left;
      (this.ItemsParent as RadPageViewElementBase).SetBorderAndFillOrientation(this.RightToLeft ? PageViewContentOrientation.Horizontal180 : PageViewContentOrientation.Horizontal, false);
      this.OnPropertyChanged(new RadPropertyChangedEventArgs(RadPageViewStripElement.StripAlignmentProperty, RadPageViewStripElement.StripAlignmentProperty.GetMetadata((RadObject) this), (object) StripViewAlignment.Top, (object) StripViewAlignment.Left));
      this.ItemContentOrientation = PageViewContentOrientation.Horizontal;
      this.ItemFitMode |= StripViewItemFitMode.FillHeight;
      this.ContentArea.Shape = (ElementShape) null;
      this.ContentArea.Padding = new Padding(0);
      this.ItemContainer.Padding = new Padding(0);
      this.ItemContainer.Children.Remove((RadElement) this.ItemContainer.ButtonsPanel);
      this.ItemContainer.MinSize = new Size(this.CollapsedPaneWidth, 0);
      int num = (int) this.ItemContainer.ItemLayout.SetValue(RadPageViewStripElement.ItemFitModeProperty, (object) StripViewItemFitMode.FillHeight);
      this.Children.Remove((RadElement) this.Header);
      this.Children.Remove((RadElement) this.Footer);
    }

    public RadPageViewNavigationViewElement()
    {
      this.SetDisplayMode(this.DisplayMode);
    }

    protected override StripViewItemContainer CreateItemContainer()
    {
      return (StripViewItemContainer) new NavigationViewItemContainer();
    }

    protected override RadPageViewItem CreateItem()
    {
      return (RadPageViewItem) new RadPageViewNavigationViewItem();
    }

    protected virtual NavigationViewHeaderElement CreateHeaderElement()
    {
      return new NavigationViewHeaderElement();
    }

    protected virtual HamburgerButtonElement CreateHamburgerButton()
    {
      HamburgerButtonElement hamburgerButtonElement = new HamburgerButtonElement(this);
      hamburgerButtonElement.StretchHorizontally = true;
      hamburgerButtonElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      hamburgerButtonElement.TextAlignment = ContentAlignment.MiddleLeft;
      hamburgerButtonElement.Click += new EventHandler(this.Element_Click);
      return hamburgerButtonElement;
    }

    protected virtual RadPopupControlBase CreatePopup()
    {
      RadPopupControlBase popupControlBase = new RadPopupControlBase((RadElement) this);
      this.popupStack = this.CreatePopupStack();
      popupControlBase.RootElement.Children.Add((RadElement) this.popupStack);
      popupControlBase.AnimationEnabled = true;
      popupControlBase.DropDownAnimationDirection = RadDirection.Right;
      popupControlBase.AnimationType = PopupAnimationTypes.Easing;
      popupControlBase.PopupClosing += new RadPopupClosingEventHandler(this.Popup_PopupClosing);
      return popupControlBase;
    }

    protected virtual StackLayoutElement CreatePopupStack()
    {
      return new StackLayoutElement() { Orientation = Orientation.Vertical };
    }

    public RadButtonElement HamburgerButton
    {
      get
      {
        return (RadButtonElement) this.hamburgerButton;
      }
    }

    public NavigationViewHeaderElement HeaderElement
    {
      get
      {
        return this.headerElement;
      }
    }

    public virtual RadPopupControlBase Popup
    {
      get
      {
        if (this.popup == null)
          this.popup = this.CreatePopup();
        return this.popup;
      }
    }

    public virtual StackLayoutElement PopupStack
    {
      get
      {
        if (this.popup == null)
          this.popup = this.CreatePopup();
        return this.popupStack;
      }
    }

    public bool IsCollapsed
    {
      get
      {
        return this.isCollapsed;
      }
      private set
      {
        this.isCollapsed = value;
      }
    }

    [DefaultValue(StripViewNewItemVisibility.Hidden)]
    public override StripViewNewItemVisibility NewItemVisibility
    {
      get
      {
        return StripViewNewItemVisibility.Hidden;
      }
      set
      {
        base.NewItemVisibility = value;
      }
    }

    [DefaultValue(typeof (NavigationViewDisplayModes), "Expanded")]
    public NavigationViewDisplayModes DisplayMode
    {
      get
      {
        return (NavigationViewDisplayModes) this.GetValue(RadPageViewNavigationViewElement.DisplayModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewNavigationViewElement.DisplayModeProperty, (object) value);
      }
    }

    [DefaultValue(40)]
    public int HeaderHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadPageViewNavigationViewElement.HeaderHeightProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewNavigationViewElement.HeaderHeightProperty, (object) value);
      }
    }

    [DefaultValue(280)]
    public int ExpandedPaneWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadPageViewNavigationViewElement.ExpandedPaneWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewNavigationViewElement.ExpandedPaneWidthProperty, (object) value);
      }
    }

    [DefaultValue(40)]
    public int CollapsedPaneWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadPageViewNavigationViewElement.CollapsedPaneWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewNavigationViewElement.CollapsedPaneWidthProperty, (object) value);
      }
    }

    [DefaultValue(641)]
    public int CompactModeThresholdWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadPageViewNavigationViewElement.CompactModeThresholdWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewNavigationViewElement.CompactModeThresholdWidthProperty, (object) value);
      }
    }

    [DefaultValue(1008)]
    public int ExpandedModeThresholdWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadPageViewNavigationViewElement.ExpandedModeThresholdWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewNavigationViewElement.ExpandedModeThresholdWidthProperty, (object) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override RadPageViewLabelElement Header
    {
      get
      {
        return base.Header;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override RadPageViewLabelElement Footer
    {
      get
      {
        return base.Footer;
      }
    }

    [Description("Gets or sets a value indicating whether to use animation when collapsing and expanding the menu.")]
    [DefaultValue(true)]
    public bool AllowAnimation
    {
      get
      {
        return this.allowAnimation;
      }
      set
      {
        this.allowAnimation = value;
      }
    }

    [Description("Gets or sets the animation interval.")]
    [DefaultValue(10)]
    public int AnimationInterval
    {
      get
      {
        return this.animationInterval;
      }
      set
      {
        this.animationInterval = value;
      }
    }

    [Description("Gets or sets the animation frames.")]
    [DefaultValue(20)]
    public int AnimationFrames
    {
      get
      {
        return this.animationFrames;
      }
      set
      {
        this.animationFrames = value;
      }
    }

    public virtual void Expand()
    {
      if (!this.IsCollapsed)
        return;
      CancelEventArgs e = new CancelEventArgs();
      this.OnExpanding(e);
      if (e.Cancel)
        return;
      this.IsCollapsed = false;
      this.ItemContainer.Visibility = ElementVisibility.Visible;
      if (this.GetEffectiveDisplayMode((float) this.Size.Width) == NavigationViewDisplayModes.Expanded)
      {
        if (this.AllowAnimation)
        {
          Size size1 = new Size(TelerikDpiHelper.ScaleInt(this.CollapsedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          Size size2 = new Size(TelerikDpiHelper.ScaleInt(this.ExpandedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          AnimatedPropertySetting animatedPropertySetting = new AnimatedPropertySetting(RadElement.MinSizeProperty, (object) size1, (object) size2, this.AnimationFrames, this.AnimationInterval);
          animatedPropertySetting.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) =>
          {
            foreach (LightVisualElement lightVisualElement in (IEnumerable<RadPageViewItem>) this.Items)
              lightVisualElement.DrawText = true;
            this.OnExpanded(EventArgs.Empty);
          });
          animatedPropertySetting.ApplyValue((RadObject) this.ItemContainer);
        }
        else
        {
          this.ItemContainer.MinSize = new Size(TelerikDpiHelper.ScaleInt(this.ExpandedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          this.OnExpanded(EventArgs.Empty);
        }
      }
      else
      {
        this.PopupStack.Children.Insert(0, (RadElement) this.HamburgerButton);
        if (!this.PopupStack.Children.Contains((RadElement) this.ItemContainer))
          this.PopupStack.Children.Add((RadElement) this.ItemContainer);
        this.HamburgerButton.Alignment = this.RightToLeft ? ContentAlignment.TopRight : ContentAlignment.TopLeft;
        this.HamburgerButton.StretchHorizontally = true;
        int num1 = (int) this.ItemContainer.ItemLayout.SetValue(RadPageViewStripElement.StripAlignmentProperty, (object) StripViewAlignment.Right);
        int num2 = (int) this.ItemContainer.ResetValue(RadElement.MinSizeProperty, ValueResetFlags.Animation);
        this.ItemContainer.MinSize = new Size(TelerikDpiHelper.ScaleInt(this.ExpandedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
        this.ItemContainer.MaxSize = Size.Empty;
        foreach (LightVisualElement lightVisualElement in (IEnumerable<RadPageViewItem>) this.Items)
          lightVisualElement.DrawText = true;
        if (this.Popup.ElementTree.RootElement.ElementState != ElementState.Loaded)
          this.Popup.LoadElementTree(new Size(this.ExpandedPaneWidth, (int) this.DesiredSize.Height));
        RadPageViewNavigationViewElement.ApplyThemeToPopup((RadElementTree) this.ElementTree, this.Popup);
        this.Popup.Size = new Size(this.ExpandedPaneWidth, (int) this.DesiredSize.Height);
        this.Popup.DropDownAnimationDirection = this.RightToLeft ? RadDirection.Left : RadDirection.Right;
        this.Popup.HorizontalPopupAlignment = this.RightToLeft ? HorizontalPopupAlignment.RightToRight : HorizontalPopupAlignment.LeftToLeft;
        this.Popup.VerticalPopupAlignment = VerticalPopupAlignment.TopToBottom;
        this.Popup.RootElement.BackColor = this.BackColor;
        this.Popup.Show(this.PointToScreen(this.RightToLeft ? new Point(this.Bounds.Right, this.Bounds.Top) : new Point(this.Bounds.Left, this.Bounds.Top)));
        this.OnExpanded(EventArgs.Empty);
      }
    }

    public virtual void Collapse()
    {
      if (this.IsCollapsed)
        return;
      CancelEventArgs e = new CancelEventArgs();
      this.OnCollapsing(e);
      if (e.Cancel)
        return;
      this.IsCollapsed = true;
      foreach (LightVisualElement lightVisualElement in (IEnumerable<RadPageViewItem>) this.Items)
        lightVisualElement.DrawText = false;
      NavigationViewDisplayModes effectiveDisplayMode = this.GetEffectiveDisplayMode((float) this.Size.Width);
      if (effectiveDisplayMode == NavigationViewDisplayModes.Expanded)
      {
        if (this.AllowAnimation)
        {
          Size size1 = new Size(TelerikDpiHelper.ScaleInt(this.ExpandedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          Size size2 = new Size(TelerikDpiHelper.ScaleInt(this.CollapsedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          AnimatedPropertySetting animatedPropertySetting = new AnimatedPropertySetting(RadElement.MinSizeProperty, (object) size1, (object) size2, this.AnimationFrames, this.AnimationInterval);
          animatedPropertySetting.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) => this.OnCollapsed(EventArgs.Empty));
          animatedPropertySetting.ApplyValue((RadObject) this.ItemContainer);
        }
        else
        {
          this.ItemContainer.MinSize = new Size(TelerikDpiHelper.ScaleInt(this.CollapsedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          this.OnCollapsed(EventArgs.Empty);
        }
      }
      else
      {
        this.programmaticallyClosingPopup = true;
        this.Popup.ClosePopup(RadPopupCloseReason.Mouse);
        this.HamburgerButton.StretchHorizontally = false;
        this.programmaticallyClosingPopup = false;
        if (effectiveDisplayMode == NavigationViewDisplayModes.Compact)
        {
          this.ItemContainer.MinSize = new Size(TelerikDpiHelper.ScaleInt(this.CollapsedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          this.ItemContainer.MaxSize = new Size(TelerikDpiHelper.ScaleInt(this.CollapsedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          if (!this.Children.Contains((RadElement) this.ItemContainer))
          {
            int num = (int) this.Popup.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
            this.Children.Insert(0, (RadElement) this.ItemContainer);
          }
        }
        if (!this.Children.Contains((RadElement) this.HamburgerButton))
        {
          int num = (int) this.HamburgerButton.ResetValue(RadElement.AlignmentProperty, ValueResetFlags.Local);
          this.Children.Add((RadElement) this.HamburgerButton);
        }
        this.OnCollapsed(EventArgs.Empty);
      }
    }

    protected virtual void SetDisplayMode(NavigationViewDisplayModes mode)
    {
      this.Collapse();
      this.displayMode = mode;
      mode = this.GetEffectiveDisplayMode((float) this.Size.Width);
      this.EnsureDisplayMode(mode);
    }

    protected virtual void EnsureDisplayMode(NavigationViewDisplayModes mode)
    {
      switch (mode)
      {
        case NavigationViewDisplayModes.Minimal:
          this.ItemContainer.MaxSize = Size.Empty;
          this.HamburgerButton.StretchHorizontally = false;
          if (!this.PopupStack.Children.Contains((RadElement) this.ItemContainer))
            this.PopupStack.Children.Add((RadElement) this.ItemContainer);
          if (this.Children.Contains((RadElement) this.HamburgerButton))
            break;
          this.Children.Add((RadElement) this.HamburgerButton);
          break;
        case NavigationViewDisplayModes.Compact:
          foreach (LightVisualElement lightVisualElement in (IEnumerable<RadPageViewItem>) this.Items)
            lightVisualElement.DrawText = false;
          this.HamburgerButton.StretchHorizontally = false;
          this.ItemContainer.MinSize = new Size(TelerikDpiHelper.ScaleInt(this.CollapsedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          this.ItemContainer.MaxSize = new Size(TelerikDpiHelper.ScaleInt(this.CollapsedPaneWidth, new SizeF(1f / this.DpiScaleFactor.Width, 0.0f)), 0);
          if (!this.Children.Contains((RadElement) this.ItemContainer))
            this.Children.Insert(0, (RadElement) this.ItemContainer);
          if (this.Children.Contains((RadElement) this.HamburgerButton))
            break;
          this.Children.Add((RadElement) this.HamburgerButton);
          break;
        case NavigationViewDisplayModes.Expanded:
          foreach (LightVisualElement lightVisualElement in (IEnumerable<RadPageViewItem>) this.Items)
            lightVisualElement.DrawText = !this.IsCollapsed;
          this.ItemContainer.MaxSize = Size.Empty;
          this.HamburgerButton.StretchHorizontally = true;
          if (!this.Children.Contains((RadElement) this.ItemContainer))
            this.Children.Insert(0, (RadElement) this.ItemContainer);
          if (this.Children.Contains((RadElement) this.ItemContainer))
            break;
          this.Children.Add((RadElement) this.HamburgerButton);
          break;
      }
    }

    protected virtual NavigationViewDisplayModes GetEffectiveDisplayMode(
      float width)
    {
      if (this.DisplayMode != NavigationViewDisplayModes.Auto)
        return this.DisplayMode;
      if ((double) width < (double) this.CompactModeThresholdWidth)
        return NavigationViewDisplayModes.Minimal;
      return (double) width < (double) this.ExpandedModeThresholdWidth ? NavigationViewDisplayModes.Compact : NavigationViewDisplayModes.Expanded;
    }

    protected internal override void CloseItem(RadPageViewItem item)
    {
    }

    protected internal override void SetSelectedItem(RadPageViewItem item)
    {
      base.SetSelectedItem(item);
      switch (this.GetEffectiveDisplayMode((float) this.Size.Width))
      {
        case NavigationViewDisplayModes.Minimal:
        case NavigationViewDisplayModes.Compact:
          this.Collapse();
          break;
      }
      this.HeaderElement.Text = item != null ? item.Text : string.Empty;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.RightToLeftProperty)
      {
        RadPageViewElementBase itemsParent = this.ItemsParent as RadPageViewElementBase;
        if (this.RightToLeft)
        {
          int num = (int) this.SetDefaultValueOverride(RadPageViewStripElement.StripAlignmentProperty, (object) StripViewAlignment.Right);
          itemsParent.SetBorderAndFillOrientation(PageViewContentOrientation.Horizontal180, false);
        }
        else
        {
          int num = (int) this.SetDefaultValueOverride(RadPageViewStripElement.StripAlignmentProperty, (object) StripViewAlignment.Left);
          itemsParent.SetBorderAndFillOrientation(PageViewContentOrientation.Horizontal, false);
        }
      }
      else
      {
        if (e.Property != RadPageViewNavigationViewElement.CollapsedPaneWidthProperty)
          return;
        this.EnsureDisplayMode(this.DisplayMode);
      }
    }

    public event CancelEventHandler Collapsing;

    protected virtual void OnCollapsing(CancelEventArgs e)
    {
      if (this.Collapsing == null)
        return;
      this.Collapsing((object) this, e);
    }

    public event CancelEventHandler Expanding;

    protected virtual void OnExpanding(CancelEventArgs e)
    {
      if (this.Expanding == null)
        return;
      this.Expanding((object) this, e);
    }

    public event EventHandler Collapsed;

    protected virtual void OnCollapsed(EventArgs e)
    {
      if (this.Collapsed == null)
        return;
      this.Collapsed((object) this, e);
    }

    public event EventHandler Expanded;

    protected virtual void OnExpanded(EventArgs e)
    {
      if (this.Expanded == null)
        return;
      this.Expanded((object) this, e);
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (!(e.PropertyName == RadPageViewNavigationViewElement.DisplayModeProperty.Name))
        return;
      this.SetDisplayMode(this.DisplayMode);
    }

    private void Element_Click(object sender, EventArgs e)
    {
      if (this.IsCollapsed)
        this.Expand();
      else
        this.Collapse();
    }

    private void Popup_PopupClosing(object sender, RadPopupClosingEventArgs args)
    {
      if (this.programmaticallyClosingPopup || args.Cancel)
        return;
      this.Collapse();
    }

    protected override SizeF MeasureItems(SizeF availableSize)
    {
      availableSize.Width = Math.Max((float) this.CollapsedPaneWidth, Math.Min((float) this.ExpandedPaneWidth, availableSize.Width));
      return base.MeasureItems(availableSize);
    }

    protected override RectangleF ArrangeItems(RectangleF itemsRect)
    {
      RectangleF rectangleF = itemsRect;
      NavigationViewDisplayModes effectiveDisplayMode = this.GetEffectiveDisplayMode((float) this.Size.Width);
      if (this.ItemContainer.Visibility != ElementVisibility.Collapsed)
      {
        SizeF desiredSize = this.ItemContainer.DesiredSize;
        Padding margin = this.ItemContainer.Margin;
        RectangleF finalRect = new RectangleF(itemsRect.X + (float) margin.Left, itemsRect.Y + (float) margin.Top + this.HamburgerButton.DesiredSize.Height, desiredSize.Width, desiredSize.Height - (float) margin.Vertical);
        if (this.RightToLeft)
        {
          switch (effectiveDisplayMode)
          {
            case NavigationViewDisplayModes.Compact:
              if (!this.IsCollapsed)
                break;
              goto case NavigationViewDisplayModes.Expanded;
            case NavigationViewDisplayModes.Expanded:
              finalRect.X = itemsRect.Right - (float) margin.Horizontal - desiredSize.Width;
              break;
          }
        }
        this.ItemContainer.Arrange(finalRect);
        switch (effectiveDisplayMode - 1)
        {
          case NavigationViewDisplayModes.Auto:
            rectangleF = new RectangleF(this.RightToLeft ? itemsRect.X : itemsRect.X + (float) margin.Horizontal, itemsRect.Y, itemsRect.Width, itemsRect.Height);
            break;
          case NavigationViewDisplayModes.Minimal:
            rectangleF = new RectangleF(this.RightToLeft ? itemsRect.X : (float) this.CollapsedPaneWidth + itemsRect.X + (float) margin.Horizontal, itemsRect.Y, itemsRect.Width - (float) this.CollapsedPaneWidth, itemsRect.Height);
            break;
          case NavigationViewDisplayModes.Compact:
            rectangleF = new RectangleF(this.RightToLeft ? itemsRect.X : finalRect.Right + (float) margin.Right, itemsRect.Y, itemsRect.Width - finalRect.Width - (float) margin.Horizontal, itemsRect.Height);
            break;
        }
      }
      if (this.HeaderElement.Visibility != ElementVisibility.Collapsed)
      {
        RectangleF finalRect = new RectangleF(rectangleF.Location, new SizeF(rectangleF.Width, (float) this.HeaderHeight));
        if (effectiveDisplayMode == NavigationViewDisplayModes.Minimal)
        {
          if (this.RightToLeft)
            finalRect.Width -= this.HamburgerButton.DesiredSize.Width;
          else
            finalRect.X += (float) (this.CollapsedPaneWidth + this.ItemContainer.Margin.Horizontal);
        }
        this.HeaderElement.Arrange(finalRect);
        rectangleF.Y += (float) this.HeaderHeight;
        rectangleF.Height -= (float) this.HeaderHeight;
      }
      return rectangleF;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      NavigationViewDisplayModes effectiveDisplayMode = this.GetEffectiveDisplayMode(availableSize.Width);
      if (this.displayModeCache != effectiveDisplayMode)
      {
        this.EnsureDisplayMode(effectiveDisplayMode);
        this.displayModeCache = effectiveDisplayMode;
      }
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.GetEffectiveDisplayMode(availableSize.Width) == NavigationViewDisplayModes.Expanded)
      {
        if (this.IsCollapsed)
          this.HamburgerButton.Measure(new SizeF((float) this.CollapsedPaneWidth, (float) this.HeaderHeight));
        else
          this.HamburgerButton.Measure(new SizeF(this.ItemContainer.DesiredSize.Width, (float) this.HeaderHeight));
      }
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      if (this.GetEffectiveDisplayMode((float) this.Size.Width) == NavigationViewDisplayModes.Expanded)
      {
        RectangleF clientRectangle = this.GetClientRectangle(finalSize);
        if (this.RightToLeft)
          this.HamburgerButton.Arrange(new RectangleF(clientRectangle.Right - this.ItemContainer.DesiredSize.Width, clientRectangle.Y, this.ItemContainer.DesiredSize.Width, (float) this.HeaderHeight));
        else
          this.HamburgerButton.Arrange(new RectangleF(clientRectangle.X, clientRectangle.Y, this.ItemContainer.DesiredSize.Width, (float) this.HeaderHeight));
      }
      return sizeF;
    }
  }
}
