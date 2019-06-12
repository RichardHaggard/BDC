// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public abstract class RadPageViewItem : RadPageViewElementBase
  {
    private int row = -1;
    public static RadProperty ButtonsAlignmentProperty = RadProperty.Register(nameof (ButtonsAlignment), typeof (PageViewItemButtonsAlignment), typeof (RadPageViewItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) PageViewItemButtonsAlignment.ContentBeforeButtons, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty AutoFlipMarginProperty = RadProperty.Register(nameof (AutoFlipMargin), typeof (bool), typeof (RadPageViewItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsMeasure));
    public static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (RadPageViewItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty TitleProperty = RadProperty.Register(nameof (Title), typeof (string), typeof (RadPageViewItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty DescriptionProperty = RadProperty.Register(nameof (Description), typeof (string), typeof (RadPageViewItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty IsPinnedProperty = RadProperty.Register(nameof (IsPinned), typeof (bool), typeof (RadPageViewItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.Cancelable));
    public static RadProperty IsPreviewProperty = RadProperty.Register(nameof (IsPreview), typeof (bool), typeof (RadPageViewItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private SizeF forcedLayoutSize;
    private SizeF currentSize;
    private Point dragStart;
    private RadPageViewItemButtonsPanel buttonsPanel;
    private bool isSystemItem;
    private RadPageViewPage page;
    private RadPageViewElement owner;
    private RadElement content;

    static RadPageViewItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadPageViewItemStateManager(), typeof (RadPageViewItem));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrag = true;
      this.MinSize = new Size(8, 8);
      this.Padding = new Padding(4);
      this.ImageAlignment = ContentAlignment.MiddleLeft;
      this.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.AutoEllipsis = true;
      this.ClipText = true;
      this.UseMnemonic = true;
      this.ShowKeyboardCues = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.buttonsPanel = this.CreateButtonsPanel();
      this.Children.Add((RadElement) this.buttonsPanel);
      int num1 = (int) this.ButtonsPanel.CloseButton.BindProperty(RadPageViewButtonElement.IsSelectedProperty, (RadObject) this, RadPageViewItem.IsSelectedProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.ButtonsPanel.PinButton.BindProperty(RadPageViewButtonElement.IsSelectedProperty, (RadObject) this, RadPageViewItem.IsSelectedProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.ButtonsPanel.PinButton.BindProperty(RadPageViewPinButtonElement.IsPinnedProperty, (RadObject) this, RadPageViewItem.IsPinnedProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.ButtonsPanel.PinButton.BindProperty(RadPageViewPinButtonElement.IsPreviewProperty, (RadObject) this, RadPageViewItem.IsPreviewProperty, PropertyBindingOptions.TwoWay);
    }

    protected virtual RadPageViewItemButtonsPanel CreateButtonsPanel()
    {
      return new RadPageViewItemButtonsPanel(this);
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "AutoEllipsis")
        return new bool?(!this.AutoEllipsis);
      return base.ShouldSerializeProperty(property);
    }

    protected override void DisposeManagedResources()
    {
      if (this.page != null)
        this.Detach();
      this.owner = (RadPageViewElement) null;
      base.DisposeManagedResources();
    }

    public virtual void Attach(RadPageViewPage page)
    {
      this.page = page;
      if (this.page == null)
        return;
      this.page.Item = this;
      this.Text = this.page.Text;
      this.Title = this.page.Title;
      this.Description = this.page.Description;
      this.ToolTipText = this.page.ToolTipText;
      this.Enabled = this.page.Enabled;
      this.PageLength = this.page.PageLength;
      this.IsContentVisible = this.page.IsContentVisible;
      this.page.UpdateItemStyle();
      this.page.CallBackColorChanged();
    }

    public virtual void Detach()
    {
      if (this.page == null)
        return;
      this.page.Item = (RadPageViewItem) null;
      this.page = (RadPageViewPage) null;
    }

    public override string ToString()
    {
      return this.GetType().Name + "; Text: " + this.Text;
    }

    public bool IsPinned
    {
      get
      {
        return (bool) this.GetValue(RadPageViewItem.IsPinnedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewItem.IsPinnedProperty, (object) value);
      }
    }

    public bool IsPreview
    {
      get
      {
        return (bool) this.GetValue(RadPageViewItem.IsPreviewProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewItem.IsPreviewProperty, (object) value);
      }
    }

    internal virtual int PageLength
    {
      get
      {
        return -1;
      }
      set
      {
      }
    }

    internal virtual bool IsContentVisible
    {
      get
      {
        return this.IsSelected;
      }
      set
      {
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsSystemItem
    {
      get
      {
        return this.isSystemItem;
      }
      internal set
      {
        this.isSystemItem = value;
      }
    }

    [Browsable(false)]
    public RadPageViewItemButtonsPanel ButtonsPanel
    {
      get
      {
        return this.buttonsPanel;
      }
    }

    [System.ComponentModel.Description("Gets or sets the alignment of item's associated buttons.")]
    [Category("Appearance")]
    public PageViewItemButtonsAlignment ButtonsAlignment
    {
      get
      {
        return (PageViewItemButtonsAlignment) this.GetValue(RadPageViewItem.ButtonsAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewItem.ButtonsAlignmentProperty, (object) value);
      }
    }

    [DefaultValue(true)]
    [System.ComponentModel.Description("Gets or sets a boolean value that determines whether the item margin will be automatically flipped according to the orientation of the items in the control.")]
    public bool AutoFlipMargin
    {
      get
      {
        return (bool) this.GetValue(RadPageViewItem.AutoFlipMarginProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewItem.AutoFlipMarginProperty, (object) value);
      }
    }

    [System.ComponentModel.Description("Gets or sets the title of the item. Title is visualized in the Header area of the owning view element.")]
    public string Title
    {
      get
      {
        string str = (string) this.GetValue(RadPageViewItem.TitleProperty);
        if (string.IsNullOrEmpty(str))
          return this.Text;
        return str;
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewItem.TitleProperty, (object) value);
      }
    }

    [System.ComponentModel.Description("Gets or sets the description of the item. Description is visualized in the Footer area of the owning view element.")]
    public string Description
    {
      get
      {
        string str = (string) this.GetValue(RadPageViewItem.DescriptionProperty);
        if (string.IsNullOrEmpty(str))
          return this.Text;
        return str;
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewItem.DescriptionProperty, (object) value);
      }
    }

    [System.ComponentModel.Description("Gets or sets the RadElement instance that represents the content of this item. The content is used when item is not bound to a RadPageViewPage instance.")]
    [DefaultValue(null)]
    public RadElement Content
    {
      get
      {
        return this.content;
      }
      set
      {
        if (this.content == value || !this.OnContentChanging(value))
          return;
        this.SetContentCore(value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public SizeF ForcedLayoutSize
    {
      get
      {
        if (this.forcedLayoutSize == SizeF.Empty)
          return this.DesiredSize;
        return this.forcedLayoutSize;
      }
      internal set
      {
        this.forcedLayoutSize = value;
      }
    }

    [Browsable(false)]
    public SizeF CurrentSize
    {
      get
      {
        return this.currentSize;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(RadPageViewItem.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewItem.IsSelectedProperty, (object) value);
      }
    }

    [Browsable(false)]
    public RadPageViewPage Page
    {
      get
      {
        return this.page;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadPageViewElement Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
      }
    }

    [DefaultValue(-1)]
    public int Row
    {
      get
      {
        return this.row;
      }
      set
      {
        this.row = value;
      }
    }

    [DefaultValue(true)]
    public override bool UseMnemonic
    {
      get
      {
        if (this.Owner != null && !this.Owner.UseMnemonic)
          return false;
        return base.UseMnemonic;
      }
      set
      {
        base.UseMnemonic = value;
      }
    }

    [DefaultValue(true)]
    public override bool ShowKeyboardCues
    {
      get
      {
        if (!this.UseMnemonic || this.Owner != null && !this.Owner.UseMnemonic)
          return false;
        return base.ShowKeyboardCues;
      }
      set
      {
        base.ShowKeyboardCues = value;
      }
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      this.owner.OnItemClick(this, e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.Owner != null && this.Owner.Owner != null)
      {
        RadPageViewPage selectedPage = this.Owner.Owner.SelectedPage;
        if (selectedPage != null && selectedPage.HasFocusedChildControl())
          return;
      }
      this.dragStart = e.Location;
      this.owner.OnItemMouseDown(this, e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.owner.OnItemMouseUp(this, e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.Capture || e.Button != this.owner.ActionMouseButton || (!this.ControlBoundingRectangle.Contains(e.Location) || !RadDragDropService.ShouldBeginDrag(e.Location, this.dragStart)))
        return;
      this.owner.OnItemDrag(this, e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = this.MeasureItem(availableSize);
      if (this.page != null && this.page.Owner != null && (this.page.Owner.ItemSizeMode == PageViewItemSizeMode.Individual && (double) this.forcedLayoutSize.Width != 0.0) && (double) this.forcedLayoutSize.Height != 0.0)
      {
        if (!float.IsInfinity(this.forcedLayoutSize.Width) && !float.IsInfinity(this.forcedLayoutSize.Height))
          sizeF = this.forcedLayoutSize;
      }
      else if (this.page != null && this.page.Owner != null && this.page.Owner.ItemSizeMode == PageViewItemSizeMode.EqualSize && this.page.Owner.ItemSize.Width != 0 && this.page.Owner.ItemSize.Height != 0)
        sizeF = (SizeF) this.page.Owner.ItemSize;
      else
        this.forcedLayoutSize = !(availableSize == LayoutUtils.InfinitySize) ? availableSize : SizeF.Empty;
      return sizeF;
    }

    private SizeF MeasureItem(SizeF availableSize)
    {
      SizeF size = this.GetClientRectangle(availableSize).Size;
      SizeF childSize = this.MeasureChildren(size);
      SizeF contentSize = this.MeasureContent(size);
      this.ButtonsPanel.Measure(size);
      switch (this.ContentOrientation)
      {
        case PageViewContentOrientation.Horizontal:
        case PageViewContentOrientation.Horizontal180:
          childSize.Width += this.ButtonsPanel.DesiredSize.Width;
          childSize.Height = Math.Max(childSize.Height, this.ButtonsPanel.DesiredSize.Height);
          break;
        case PageViewContentOrientation.Vertical90:
        case PageViewContentOrientation.Vertical270:
          childSize.Width += this.ButtonsPanel.DesiredSize.Height;
          childSize.Height = Math.Max(childSize.Height, this.ButtonsPanel.DesiredSize.Width);
          break;
      }
      SizeF sizeF = this.ApplyMinMaxSize(this.ApplyClientOffset(this.CalculateMeasuredSize(contentSize, childSize)));
      switch (this.ContentOrientation)
      {
        case PageViewContentOrientation.Vertical90:
        case PageViewContentOrientation.Vertical270:
          sizeF = new SizeF(sizeF.Height, sizeF.Width);
          break;
      }
      return sizeF;
    }

    protected override bool ShouldMeasureChild(RadElement child)
    {
      if (child == this.buttonsPanel)
        return false;
      return base.ShouldMeasureChild(child);
    }

    protected override SizeF CalculateMeasuredSize(SizeF contentSize, SizeF childSize)
    {
      if (this.buttonsPanel.DesiredSize == SizeF.Empty)
        return base.CalculateMeasuredSize(contentSize, childSize);
      Padding margin = this.buttonsPanel.Margin;
      SizeF sizeF = SizeF.Empty;
      switch (this.ButtonsAlignment)
      {
        case PageViewItemButtonsAlignment.ButtonsBeforeContent:
        case PageViewItemButtonsAlignment.ContentBeforeButtons:
          sizeF.Width = contentSize.Width + childSize.Width + (float) margin.Horizontal;
          sizeF.Height = Math.Max(contentSize.Height, childSize.Height + (float) margin.Vertical);
          break;
        case PageViewItemButtonsAlignment.ButtonsAboveContent:
        case PageViewItemButtonsAlignment.ContentAboveButtons:
          sizeF.Width = Math.Max(contentSize.Width, childSize.Width + (float) margin.Horizontal);
          sizeF.Height = contentSize.Height + childSize.Height + (float) margin.Vertical;
          break;
        default:
          sizeF = base.CalculateMeasuredSize(contentSize, childSize);
          break;
      }
      return sizeF;
    }

    protected override void ArrangeChildren(SizeF available)
    {
      int count = this.Children.Count;
      RectangleF buttonsClientRect = this.GetButtonsClientRect(available);
      float x = buttonsClientRect.X;
      float y = buttonsClientRect.Y;
      for (int index = 0; index < count; ++index)
      {
        if (this.Children[index] != this.buttonsPanel)
          this.Children[index].Arrange(buttonsClientRect);
      }
      SizeF desiredSize = this.buttonsPanel.DesiredSize;
      if (desiredSize == SizeF.Empty)
        return;
      Padding padding = this.RotatePadding(this.buttonsPanel.Margin);
      PageViewItemButtonsAlignment alignment = this.RotateButtonsAlignment(this.ButtonsAlignment);
      if (this.RightToLeft)
        alignment = this.RTLTransformButtonsAlignment(alignment);
      switch (alignment)
      {
        case PageViewItemButtonsAlignment.ButtonsBeforeContent:
          x = buttonsClientRect.X + (float) padding.Left;
          y = (float) ((double) buttonsClientRect.Y + (double) padding.Top + ((double) buttonsClientRect.Height - (double) desiredSize.Height) / 2.0);
          break;
        case PageViewItemButtonsAlignment.ContentBeforeButtons:
          x = buttonsClientRect.Right - (float) padding.Right - desiredSize.Width;
          y = (float) ((double) buttonsClientRect.Y + (double) padding.Top + ((double) buttonsClientRect.Height - (double) desiredSize.Height) / 2.0);
          break;
        case PageViewItemButtonsAlignment.ButtonsAboveContent:
          x = buttonsClientRect.X + (float) (((double) buttonsClientRect.Width - (double) desiredSize.Width) / 2.0) + (float) padding.Left;
          y = buttonsClientRect.Y + (float) padding.Top;
          break;
        case PageViewItemButtonsAlignment.ContentAboveButtons:
          x = buttonsClientRect.X + (float) (((double) buttonsClientRect.Width - (double) desiredSize.Width) / 2.0) + (float) padding.Left;
          y = buttonsClientRect.Bottom - (float) padding.Bottom - desiredSize.Height;
          break;
      }
      this.buttonsPanel.Arrange(new RectangleF(x, y, desiredSize.Width, desiredSize.Height));
    }

    private RectangleF GetButtonsClientRect(SizeF available)
    {
      Padding padding1 = this.RotatePadding(this.Padding);
      Padding padding2 = this.RotatePadding(this.GetBorderThickness(true));
      return new RectangleF((float) (padding1.Left + padding2.Left), (float) (padding1.Top + padding2.Top), available.Width - (float) padding1.Horizontal - (float) padding2.Horizontal, available.Height - (float) padding1.Vertical - (float) padding2.Vertical);
    }

    protected virtual Padding RotatePadding(Padding margin)
    {
      if (this.ContentOrientation == PageViewContentOrientation.Horizontal)
        return margin;
      switch (this.ContentOrientation)
      {
        case PageViewContentOrientation.Horizontal180:
          margin = LayoutUtils.RotateMargin(margin, 180);
          break;
        case PageViewContentOrientation.Vertical90:
          margin = LayoutUtils.RotateMargin(margin, 90);
          break;
        case PageViewContentOrientation.Vertical270:
          margin = LayoutUtils.RotateMargin(margin, 270);
          break;
      }
      return margin;
    }

    protected virtual PageViewItemButtonsAlignment RTLTransformButtonsAlignment(
      PageViewItemButtonsAlignment alignment)
    {
      switch (alignment)
      {
        case PageViewItemButtonsAlignment.ButtonsBeforeContent:
          alignment = PageViewItemButtonsAlignment.ContentBeforeButtons;
          break;
        case PageViewItemButtonsAlignment.ContentBeforeButtons:
          alignment = PageViewItemButtonsAlignment.ButtonsBeforeContent;
          break;
        case PageViewItemButtonsAlignment.ButtonsAboveContent:
          alignment = PageViewItemButtonsAlignment.ContentAboveButtons;
          break;
        case PageViewItemButtonsAlignment.ContentAboveButtons:
          alignment = PageViewItemButtonsAlignment.ButtonsAboveContent;
          break;
      }
      return alignment;
    }

    protected virtual PageViewItemButtonsAlignment RotateButtonsAlignment(
      PageViewItemButtonsAlignment alignment)
    {
      if (alignment == PageViewItemButtonsAlignment.Overlay || this.ContentOrientation == PageViewContentOrientation.Horizontal)
        return alignment;
      switch (this.ContentOrientation)
      {
        case PageViewContentOrientation.Horizontal180:
          switch (alignment)
          {
            case PageViewItemButtonsAlignment.ButtonsBeforeContent:
              alignment = PageViewItemButtonsAlignment.ContentBeforeButtons;
              break;
            case PageViewItemButtonsAlignment.ContentBeforeButtons:
              alignment = PageViewItemButtonsAlignment.ButtonsBeforeContent;
              break;
            case PageViewItemButtonsAlignment.ButtonsAboveContent:
              alignment = PageViewItemButtonsAlignment.ContentAboveButtons;
              break;
            case PageViewItemButtonsAlignment.ContentAboveButtons:
              alignment = PageViewItemButtonsAlignment.ButtonsAboveContent;
              break;
          }
        case PageViewContentOrientation.Vertical90:
          switch (alignment)
          {
            case PageViewItemButtonsAlignment.ButtonsBeforeContent:
              alignment = PageViewItemButtonsAlignment.ButtonsAboveContent;
              break;
            case PageViewItemButtonsAlignment.ContentBeforeButtons:
              alignment = PageViewItemButtonsAlignment.ContentAboveButtons;
              break;
            case PageViewItemButtonsAlignment.ButtonsAboveContent:
              alignment = PageViewItemButtonsAlignment.ContentBeforeButtons;
              break;
            case PageViewItemButtonsAlignment.ContentAboveButtons:
              alignment = PageViewItemButtonsAlignment.ButtonsBeforeContent;
              break;
          }
        case PageViewContentOrientation.Vertical270:
          switch (alignment)
          {
            case PageViewItemButtonsAlignment.ButtonsBeforeContent:
              alignment = PageViewItemButtonsAlignment.ContentAboveButtons;
              break;
            case PageViewItemButtonsAlignment.ContentBeforeButtons:
              alignment = PageViewItemButtonsAlignment.ButtonsAboveContent;
              break;
            case PageViewItemButtonsAlignment.ButtonsAboveContent:
              alignment = PageViewItemButtonsAlignment.ButtonsBeforeContent;
              break;
            case PageViewItemButtonsAlignment.ContentAboveButtons:
              alignment = PageViewItemButtonsAlignment.ContentBeforeButtons;
              break;
          }
      }
      return alignment;
    }

    protected internal override void SetContentOrientation(
      PageViewContentOrientation orientation,
      bool recursive)
    {
      this.currentSize = SizeF.Empty;
      this.buttonsPanel.SetContentOrientation(orientation, false);
      base.SetContentOrientation(orientation, recursive);
    }

    protected virtual void SetContentCore(RadElement value)
    {
      this.content = value;
      if (this.owner == null)
        return;
      this.owner.OnItemContentChanged(this);
    }

    protected virtual bool OnContentChanging(RadElement value)
    {
      if (this.owner != null)
        return this.owner.OnItemContentChanging(this, value);
      return true;
    }

    protected override void OnUnloaded(ComponentThemableElementTree oldTree)
    {
      base.OnUnloaded(oldTree);
      this.currentSize = SizeF.Empty;
      this.forcedLayoutSize = SizeF.Empty;
    }

    protected override void SetBoundsCore(Rectangle bounds)
    {
      base.SetBoundsCore(bounds);
      this.currentSize = (SizeF) bounds.Size;
    }

    public override bool ProcessMnemonic(char charCode)
    {
      if (!this.UseMnemonic || !Control.IsMnemonic(charCode, this.Text) || (this.Owner == null || !this.Owner.UseMnemonic) || this.Owner.Owner == null)
        return false;
      this.Owner.Owner.Focus();
      this.Owner.Owner.SelectedPage = this.Page;
      return true;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (this.page != null)
        this.UpdatePage(e);
      if (this.owner != null)
        this.owner.OnItemPropertyChanged(this, e);
      if (e.Property == RadPageViewItem.IsPinnedProperty && this.IsInValidState(true) && this.Owner != null)
        this.Owner.InvalidateMeasure(true);
      if (!(this is RadPageViewStripItem) || !(e.Property.Name == "IsSelected") || (!(bool) e.NewValue || (this as RadPageViewStripItem).Page == null))
        return;
      this.FindAncestor<StripViewItemLayout>()?.ReArrangeRows((this as RadPageViewStripItem).Page.Item.Row);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      if (this.Size.Width <= 0 || (this.Size.Height <= 0 || !this.IsSelected))
        return;
      RadControl componentTreeHandler = this.ElementTree.ComponentTreeHandler as RadControl;
      if (componentTreeHandler == null || !componentTreeHandler.ContainsFocus || !componentTreeHandler.AllowShowFocusCues)
        return;
      Padding borderThickness = this.GetBorderThickness(true);
      Rectangle rectangle = new Rectangle(borderThickness.Left, borderThickness.Top, this.Size.Width - borderThickness.Horizontal - 1, this.Size.Height - borderThickness.Vertical - 1);
      Color color = Color.Black;
      RadControl radControl = this.owner.ElementTree == null || this.owner.ElementTree.Control == null ? (RadControl) null : this.owner.ElementTree.Control as RadControl;
      if (TelerikHelper.IsDarkTheme(radControl != null ? radControl.ThemeName : string.Empty))
        color = Color.White;
      this.DrawRectangle(graphics, rectangle, color, 1f);
    }

    private void DrawRectangle(IGraphics graphics, Rectangle rectangle, Color color, float width)
    {
      graphics.ChangeSmoothingMode(SmoothingMode.None);
      graphics.DrawRectangle((RectangleF) rectangle, color, PenAlignment.Center, width, DashStyle.Dot);
      graphics.RestoreSmoothingMode();
    }

    private void UpdatePage(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadItem.TextProperty)
        this.page.Text = (string) e.NewValue;
      else if (e.Property == LightVisualElement.ImageProperty && this.GetValueSource(e.Property) == ValueSource.Local)
        this.page.Image = (Image) e.NewValue;
      else if (e.Property == RadPageViewItem.TitleProperty)
        this.page.Title = (string) e.NewValue;
      else if (e.Property == RadPageViewItem.DescriptionProperty)
        this.page.Description = (string) e.NewValue;
      else if (e.Property == LightVisualElement.ImageAlignmentProperty && this.GetValueSource(e.Property) == ValueSource.Local)
        this.page.ImageAlignment = (ContentAlignment) e.NewValue;
      else if (e.Property == LightVisualElement.TextAlignmentProperty && this.GetValueSource(e.Property) == ValueSource.Local)
        this.page.TextAlignment = (ContentAlignment) e.NewValue;
      else if (e.Property == LightVisualElement.TextImageRelationProperty && this.GetValueSource(e.Property) == ValueSource.Local)
      {
        this.page.TextImageRelation = (TextImageRelation) e.NewValue;
      }
      else
      {
        if (e.Property != RadElement.EnabledProperty)
          return;
        bool newValue = (bool) e.NewValue;
        if (this.page.Enabled == newValue)
          return;
        this.page.Enabled = newValue;
      }
    }
  }
}
