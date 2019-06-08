// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewStackElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadPageViewStackElement : RadPageViewElement
  {
    public static readonly RadProperty StackPositionProperty = RadProperty.Register(nameof (StackPosition), typeof (StackViewPosition), typeof (RadPageViewStackElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StackViewPosition.Bottom, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty ItemSelectionModeProperty = RadProperty.Register(nameof (ItemSelectionMode), typeof (StackViewItemSelectionMode), typeof (RadPageViewStackElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StackViewItemSelectionMode.Standard, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    internal StackViewLayoutInfo layoutInfo;

    static RadPageViewStackElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new StackViewElementStateManager(), typeof (RadPageViewStackElement));
    }

    internal override PageViewLayoutInfo ItemLayoutInfo
    {
      get
      {
        return (PageViewLayoutInfo) this.layoutInfo;
      }
    }

    protected override RadElement ItemsParent
    {
      get
      {
        return (RadElement) this;
      }
    }

    [RadPropertyDefaultValue("StackPosition", typeof (RadPageViewStackElement))]
    [Description("Gets or sets a value that determines the location of the items in relation to the content area.")]
    [Category("Appearance")]
    public StackViewPosition StackPosition
    {
      get
      {
        return (StackViewPosition) this.GetValue(RadPageViewStackElement.StackPositionProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStackElement.StackPositionProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value that determines how items in the stack view are selected and positioned.")]
    [RadPropertyDefaultValue("ItemSelectionMode", typeof (RadPageViewStackElement))]
    public StackViewItemSelectionMode ItemSelectionMode
    {
      get
      {
        return (StackViewItemSelectionMode) this.GetValue(RadPageViewStackElement.ItemSelectionModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewStackElement.ItemSelectionModeProperty, (object) value);
      }
    }

    protected internal override bool IsNextKey(Keys key)
    {
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          return key == Keys.Left;
        case StackViewPosition.Top:
        case StackViewPosition.Bottom:
          return key == Keys.Down;
        case StackViewPosition.Right:
          return key == Keys.Right;
        default:
          return base.IsNextKey(key);
      }
    }

    protected internal override bool IsPreviousKey(Keys key)
    {
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          return key == Keys.Right;
        case StackViewPosition.Top:
        case StackViewPosition.Bottom:
          return key == Keys.Up;
        case StackViewPosition.Right:
          return key == Keys.Left;
        default:
          return base.IsPreviousKey(key);
      }
    }

    protected internal override void SetSelectedItem(RadPageViewItem item)
    {
      this.InvalidateMeasure();
      this.InvalidateArrange();
      base.SetSelectedItem(item);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ItemSizeMode = PageViewItemSizeMode.EqualSize;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ContentArea.ThemeRole = "StackViewContentArea";
      this.Header.Visibility = ElementVisibility.Visible;
    }

    protected override RadPageViewItem CreateItem()
    {
      return (RadPageViewItem) new RadPageViewStackItem();
    }

    protected internal override PageViewContentOrientation GetAutomaticItemOrientation(
      bool content)
    {
      switch (this.StackPosition)
      {
        case StackViewPosition.Left:
          return PageViewContentOrientation.Vertical270;
        case StackViewPosition.Right:
          return PageViewContentOrientation.Vertical90;
        default:
          return PageViewContentOrientation.Horizontal;
      }
    }

    public override RectangleF GetItemsRect()
    {
      RectangleF clientRectangle = this.GetClientRectangle(true, (SizeF) this.Size);
      clientRectangle.Y += this.Header.DesiredSize.Height + (float) this.Header.Margin.Vertical;
      clientRectangle.Height -= this.Header.DesiredSize.Height + this.Footer.DesiredSize.Height + (float) this.Header.Margin.Vertical + (float) this.Footer.Margin.Vertical;
      return clientRectangle;
    }

    private SizeF GetAvailableSizeForContent(RectangleF clientRect)
    {
      SizeF sizeF = SizeF.Empty;
      float num = this.CorrectOffsetBasedOnSelectionContext(this.layoutInfo.layoutLength);
      switch (this.StackPosition)
      {
        case StackViewPosition.Left:
        case StackViewPosition.Right:
          sizeF = new SizeF(clientRect.Width - num, clientRect.Height);
          break;
        case StackViewPosition.Top:
        case StackViewPosition.Bottom:
          sizeF = new SizeF(clientRect.Width, clientRect.Height - num);
          break;
      }
      return sizeF;
    }

    private RectangleF GetStandardContentRectangle(
      PageViewItemSizeInfo item,
      RectangleF clientRect)
    {
      RectangleF rectangleF = (RectangleF) Rectangle.Empty;
      SizeF contentSizeForItem = this.GetContentSizeForItem(item, clientRect);
      float layoutLength = this.layoutInfo.layoutLength;
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          rectangleF = new RectangleF(clientRect.X + layoutLength, clientRect.Y, contentSizeForItem.Width, contentSizeForItem.Height);
          break;
        case StackViewPosition.Top:
          rectangleF = new RectangleF(clientRect.X, clientRect.Y + layoutLength, contentSizeForItem.Width, contentSizeForItem.Height);
          break;
        case StackViewPosition.Right:
        case StackViewPosition.Bottom:
          rectangleF = new RectangleF(clientRect.X, clientRect.Y, contentSizeForItem.Width, contentSizeForItem.Height);
          break;
      }
      return rectangleF;
    }

    private RectangleF GetTopBottomContentWithSelectedRectangle(
      bool isTop,
      PageViewItemSizeInfo selectedItem,
      SizeF contentAreaSize,
      RectangleF clientRect)
    {
      RectangleF itemRectangle = selectedItem.itemRectangle;
      if (!isTop && this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentWithSelected || isTop && this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentAfterSelected)
        return new RectangleF(clientRect.Left, itemRectangle.Bottom + (float) selectedItem.item.Margin.Bottom, contentAreaSize.Width, contentAreaSize.Height);
      if (!isTop && this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentAfterSelected || isTop && this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentWithSelected)
        return new RectangleF(clientRect.Left, itemRectangle.Top - (float) selectedItem.item.Margin.Top - contentAreaSize.Height, contentAreaSize.Width, contentAreaSize.Height);
      return (RectangleF) Rectangle.Empty;
    }

    private RectangleF GetLeftRightContentWithSelectedRectangle(
      bool isLeft,
      PageViewItemSizeInfo selectedItem,
      SizeF contentAreaSize,
      RectangleF clientRect)
    {
      RectangleF itemRectangle = selectedItem.itemRectangle;
      if (!isLeft && this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentWithSelected || isLeft && this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentAfterSelected)
        return new RectangleF(itemRectangle.Right + (float) selectedItem.item.Margin.Right, clientRect.Y, contentAreaSize.Width, contentAreaSize.Height);
      if (!isLeft && this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentAfterSelected || isLeft && this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentWithSelected)
        return new RectangleF(itemRectangle.Left - (float) selectedItem.item.Margin.Left - contentAreaSize.Width, clientRect.Y, contentAreaSize.Width, contentAreaSize.Height);
      return (RectangleF) Rectangle.Empty;
    }

    internal RectangleF GetContentWithSelectedContentRectangle(
      PageViewItemSizeInfo item,
      RectangleF clientRect)
    {
      PageViewItemSizeInfo selectedItem = item;
      if (selectedItem == null)
        return clientRect;
      RectangleF rectangleF = (RectangleF) Rectangle.Empty;
      SizeF contentSizeForItem = this.GetContentSizeForItem(item, clientRect);
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          rectangleF = this.GetLeftRightContentWithSelectedRectangle(true, selectedItem, contentSizeForItem, clientRect);
          break;
        case StackViewPosition.Top:
          rectangleF = this.GetTopBottomContentWithSelectedRectangle(true, selectedItem, contentSizeForItem, clientRect);
          break;
        case StackViewPosition.Right:
          rectangleF = this.GetLeftRightContentWithSelectedRectangle(false, selectedItem, contentSizeForItem, clientRect);
          break;
        case StackViewPosition.Bottom:
          rectangleF = this.GetTopBottomContentWithSelectedRectangle(false, selectedItem, contentSizeForItem, clientRect);
          break;
      }
      return rectangleF;
    }

    protected virtual RectangleF GetContentAreaRectangle(RectangleF clientRect)
    {
      RectangleF empty = (RectangleF) Rectangle.Empty;
      PageViewItemSizeInfo selectedItem = this.layoutInfo.selectedItem;
      return this.layoutInfo.selectionMode == StackViewItemSelectionMode.Standard ? this.GetStandardContentRectangle(selectedItem, clientRect) : this.GetContentWithSelectedContentRectangle(selectedItem, clientRect);
    }

    private float GetItemWidth(PageViewItemSizeInfo item)
    {
      float num1 = item.desiredSize.Width;
      float num2 = this.layoutInfo.availableSize.Width - (float) item.marginLength;
      if ((double) num1 > (double) num2)
        num1 = num2;
      return num1;
    }

    private float GetItemHeight(PageViewItemSizeInfo item)
    {
      float num1 = item.desiredSize.Height;
      float num2 = this.layoutInfo.availableSize.Height - (float) item.marginLength;
      if ((double) num1 > (double) num2)
        num1 = num2;
      return num1;
    }

    internal virtual StackViewLayoutInfo CreateLayoutInfo(SizeF availableSize)
    {
      return new StackViewLayoutInfo(this, availableSize);
    }

    protected virtual SizeF MeasureContentArea(ref SizeF availableSize)
    {
      this.ContentArea.Measure(availableSize);
      if (this.StackPosition == StackViewPosition.Top || this.StackPosition == StackViewPosition.Bottom)
      {
        if (!this.StretchHorizontally)
          availableSize.Width = this.ContentArea.DesiredSize.Width;
        availableSize.Height -= this.ContentArea.DesiredSize.Height;
      }
      else
      {
        availableSize.Width -= this.ContentArea.DesiredSize.Width;
        if (!this.StretchVertically)
          availableSize.Height = this.ContentArea.DesiredSize.Height;
      }
      return this.ContentArea.DesiredSize;
    }

    protected override SizeF MeasureItems(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      this.layoutInfo = this.CreateLayoutInfo(availableSize);
      StackViewPosition position = this.layoutInfo.position;
      switch (position)
      {
        case StackViewPosition.Left:
        case StackViewPosition.Right:
          this.ApplyItemMetricsVertical();
          break;
        case StackViewPosition.Top:
        case StackViewPosition.Bottom:
          this.ApplyItemMetricsHorizontal();
          break;
      }
      this.MeasureItemsCore();
      if (position == StackViewPosition.Top || position == StackViewPosition.Bottom)
      {
        availableSize.Height -= this.layoutInfo.layoutLength;
        empty.Height = this.layoutInfo.layoutLength;
        empty.Width = this.layoutInfo.maxWidth;
      }
      else
      {
        availableSize.Width -= this.layoutInfo.layoutLength;
        empty.Width = this.layoutInfo.layoutLength;
        empty.Height = this.layoutInfo.maxHeight;
      }
      SizeF sizeF = this.MeasureContentArea(ref availableSize);
      if (position == StackViewPosition.Top || position == StackViewPosition.Bottom)
      {
        empty.Height += sizeF.Height;
        empty.Width = Math.Max(empty.Width, sizeF.Width);
      }
      else
      {
        empty.Width += sizeF.Width;
        empty.Height = Math.Max(empty.Height, sizeF.Height);
      }
      return empty;
    }

    private void MeasureItemsCore()
    {
      StackViewPosition position = this.layoutInfo.position;
      this.layoutInfo.layoutLength += (float) (this.layoutInfo.itemSpacing * (this.layoutInfo.itemCount - 1));
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        Padding itemMargin = this.GetItemMargin(viewItemSizeInfo);
        if (viewItemSizeInfo.layoutSize != viewItemSizeInfo.desiredSize)
        {
          SizeF layoutSize = viewItemSizeInfo.layoutSize;
          viewItemSizeInfo.item.Measure(layoutSize);
          if (this.Owner == null)
          {
            SizeF desiredSize = viewItemSizeInfo.item.DesiredSize;
            if ((position == StackViewPosition.Top || position == StackViewPosition.Bottom) && (viewItemSizeInfo.item.StretchHorizontally && !float.IsInfinity(viewItemSizeInfo.layoutSize.Width)))
              desiredSize.Width = viewItemSizeInfo.layoutSize.Width;
            if ((position == StackViewPosition.Left || position == StackViewPosition.Right) && (viewItemSizeInfo.item.StretchVertically && !float.IsInfinity(viewItemSizeInfo.layoutSize.Height)))
              desiredSize.Height = viewItemSizeInfo.layoutSize.Height;
            viewItemSizeInfo.desiredSize = desiredSize;
          }
          else
            viewItemSizeInfo.desiredSize = viewItemSizeInfo.layoutSize;
        }
        switch (position)
        {
          case StackViewPosition.Left:
          case StackViewPosition.Right:
            this.layoutInfo.layoutLength += viewItemSizeInfo.desiredSize.Width + (float) itemMargin.Horizontal;
            continue;
          case StackViewPosition.Top:
          case StackViewPosition.Bottom:
            this.layoutInfo.layoutLength += viewItemSizeInfo.desiredSize.Height + (float) itemMargin.Vertical;
            continue;
          default:
            continue;
        }
      }
    }

    private void ApplyItemMetricsVertical()
    {
      switch (this.layoutInfo.sizeMode)
      {
        case PageViewItemSizeMode.Individual:
          this.MeasureItemsVerticalIndividual();
          break;
        case PageViewItemSizeMode.EqualWidth:
          this.MeasureItemsVerticalEqualWidth();
          break;
        case PageViewItemSizeMode.EqualHeight:
          this.MeasureItemsVerticalEqualHeight();
          break;
        case PageViewItemSizeMode.EqualSize:
          this.MeasureItemsEqualSizeVertical();
          break;
      }
    }

    private void MeasureItemsEqualSizeVertical()
    {
      SizeF availableSize = this.layoutInfo.availableSize;
      float maxWidth = this.layoutInfo.maxWidth;
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
        viewItemSizeInfo.SetLayoutSize(new SizeF(maxWidth, availableSize.Height - (this.Owner != null ? (float) viewItemSizeInfo.marginLength : 0.0f)));
    }

    private void MeasureItemsVerticalEqualHeight()
    {
      SizeF availableSize = this.layoutInfo.availableSize;
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
        viewItemSizeInfo.SetLayoutSize(new SizeF(viewItemSizeInfo.desiredSize.Width, availableSize.Height - (float) viewItemSizeInfo.marginLength));
    }

    private void MeasureItemsVerticalEqualWidth()
    {
      float maxWidth = this.layoutInfo.maxWidth;
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        float itemHeight = this.GetItemHeight(viewItemSizeInfo);
        viewItemSizeInfo.SetLayoutSize(new SizeF(maxWidth, itemHeight));
      }
    }

    private void MeasureItemsVerticalIndividual()
    {
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        float itemHeight = this.GetItemHeight(viewItemSizeInfo);
        viewItemSizeInfo.SetLayoutSize(new SizeF(viewItemSizeInfo.desiredSize.Width, itemHeight));
      }
    }

    private void ApplyItemMetricsHorizontal()
    {
      switch (this.layoutInfo.sizeMode)
      {
        case PageViewItemSizeMode.Individual:
          this.MeasureItemsHorizontalIndividualSize();
          break;
        case PageViewItemSizeMode.EqualWidth:
          this.MeasureItemsHorizontalEqualWidth();
          break;
        case PageViewItemSizeMode.EqualHeight:
          this.MeasureItemsHorizontalEqualHeight();
          break;
        case PageViewItemSizeMode.EqualSize:
          this.MeasureItemsEqualSizeHorizontal();
          break;
      }
    }

    private void MeasureItemsEqualSizeHorizontal()
    {
      SizeF availableSize = this.layoutInfo.availableSize;
      float maxHeight = this.layoutInfo.maxHeight;
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
        viewItemSizeInfo.SetLayoutSize(new SizeF(availableSize.Width - (float) viewItemSizeInfo.marginLength, maxHeight));
    }

    private void MeasureItemsHorizontalEqualWidth()
    {
      float width = this.layoutInfo.availableSize.Width;
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
        viewItemSizeInfo.SetLayoutSize(new SizeF(width - (float) viewItemSizeInfo.marginLength, viewItemSizeInfo.desiredSize.Height));
    }

    private void MeasureItemsHorizontalEqualHeight()
    {
      float maxHeight = this.layoutInfo.maxHeight;
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        float itemWidth = this.GetItemWidth(viewItemSizeInfo);
        viewItemSizeInfo.SetLayoutSize(new SizeF(itemWidth, maxHeight));
      }
    }

    private void MeasureItemsHorizontalIndividualSize()
    {
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        float itemWidth = this.GetItemWidth(viewItemSizeInfo);
        viewItemSizeInfo.SetLayoutSize(new SizeF(itemWidth, viewItemSizeInfo.desiredSize.Height));
      }
    }

    internal Padding GetItemMargin(RadPageViewStackItem item)
    {
      StackViewPosition position = this.layoutInfo.position;
      Padding margin = item.Margin;
      if (!item.AutoFlipMargin)
        return margin;
      switch (position)
      {
        case StackViewPosition.Left:
          return new Padding(margin.Top, margin.Right, margin.Bottom, margin.Left);
        case StackViewPosition.Right:
          return new Padding(margin.Bottom, margin.Left, margin.Top, margin.Right);
        default:
          return margin;
      }
    }

    internal Padding GetItemMargin(PageViewItemSizeInfo item)
    {
      return this.GetItemMargin(item.item as RadPageViewStackItem);
    }

    private PointF GetItemLocation(
      ref float currentItemOffset,
      PageViewItemSizeInfo item,
      RectangleF clientRect)
    {
      PointF pointF = PointF.Empty;
      Padding itemMargin = this.GetItemMargin(item);
      SizeF desiredSize = item.desiredSize;
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          currentItemOffset += (float) itemMargin.Left;
          pointF = new PointF(clientRect.X + currentItemOffset, clientRect.Y + (this.Owner == null ? 0.0f : (float) itemMargin.Top));
          currentItemOffset += (float) itemMargin.Right + desiredSize.Width;
          break;
        case StackViewPosition.Top:
          currentItemOffset += (float) itemMargin.Top;
          pointF = new PointF(clientRect.X + (float) itemMargin.Left, clientRect.Y + currentItemOffset);
          currentItemOffset += desiredSize.Height + (float) itemMargin.Bottom;
          break;
        case StackViewPosition.Right:
          currentItemOffset += desiredSize.Width + (float) itemMargin.Right;
          pointF = new PointF(clientRect.Right - currentItemOffset, clientRect.Y + (this.Owner == null ? 0.0f : (float) itemMargin.Top));
          currentItemOffset += (float) itemMargin.Left;
          break;
        case StackViewPosition.Bottom:
          currentItemOffset += desiredSize.Height + (float) itemMargin.Bottom;
          pointF = new PointF(clientRect.X + (float) itemMargin.Left, clientRect.Bottom - currentItemOffset);
          currentItemOffset += (float) itemMargin.Top;
          break;
      }
      return pointF;
    }

    internal virtual SizeF GetContentSizeForItem(
      PageViewItemSizeInfo sizeInfo,
      RectangleF clientRect)
    {
      return this.GetAvailableSizeForContent(clientRect);
    }

    internal virtual float GetItemOffset(
      RectangleF clientRect,
      PageViewItemSizeInfo sizeInfo,
      float proposedOffset)
    {
      if (object.ReferenceEquals((object) sizeInfo, (object) this.layoutInfo.selectedItem) && this.layoutInfo.selectionMode != StackViewItemSelectionMode.Standard)
      {
        SizeF contentSizeForItem = this.GetContentSizeForItem(sizeInfo, clientRect);
        switch (this.layoutInfo.position)
        {
          case StackViewPosition.Top:
          case StackViewPosition.Bottom:
            proposedOffset += contentSizeForItem.Height;
            break;
          default:
            proposedOffset += contentSizeForItem.Width;
            break;
        }
        proposedOffset = this.CorrectOffsetBasedOnSelectionContext(proposedOffset);
      }
      return proposedOffset;
    }

    internal virtual float CorrectOffsetBasedOnSelectionContext(float proposedOffset)
    {
      if (this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentWithSelected)
      {
        if (this.layoutInfo.selectedItem.itemIndex < this.Items.Count - 1)
          proposedOffset -= (float) this.layoutInfo.itemSpacing;
      }
      else if (this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentAfterSelected && this.layoutInfo.selectedItem.itemIndex > 0)
        proposedOffset -= (float) this.layoutInfo.itemSpacing;
      return proposedOffset;
    }

    protected virtual float GetInitialItemsOffset(RectangleF clientRect)
    {
      return 0.0f;
    }

    protected override RectangleF ArrangeItems(RectangleF clientRect)
    {
      float currentItemOffset = this.GetInitialItemsOffset(clientRect);
      PointF empty = PointF.Empty;
      int itemSpacing = this.layoutInfo.itemSpacing;
      for (int index = this.layoutInfo.items.Count - 1; index > -1; --index)
      {
        PageViewItemSizeInfo sizeInfo = this.layoutInfo.items[index];
        PointF itemLocation;
        float itemOffset;
        if (this.layoutInfo.selectionMode == StackViewItemSelectionMode.ContentAfterSelected)
        {
          itemLocation = this.GetItemLocation(ref currentItemOffset, sizeInfo, clientRect);
          itemOffset = this.GetItemOffset(clientRect, sizeInfo, currentItemOffset);
        }
        else
        {
          itemOffset = this.GetItemOffset(clientRect, sizeInfo, currentItemOffset);
          itemLocation = this.GetItemLocation(ref itemOffset, sizeInfo, clientRect);
        }
        currentItemOffset = itemOffset + (float) itemSpacing;
        sizeInfo.itemRectangle = new RectangleF(itemLocation, sizeInfo.desiredSize);
        if (this.Owner == null)
        {
          if (this.StackPosition == StackViewPosition.Left)
            sizeInfo.itemRectangle.Height = clientRect.Height;
          if (this.StackPosition == StackViewPosition.Left || this.StackPosition == StackViewPosition.Right)
            sizeInfo.itemRectangle.Height -= (float) (this.layoutInfo.items[index].marginLength * 2);
        }
        sizeInfo.item.Arrange(sizeInfo.itemRectangle);
      }
      return this.GetContentAreaRectangle(clientRect);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadPageViewStackElement.StackPositionProperty && this.ItemContentOrientation == PageViewContentOrientation.Auto)
        this.UpdateItemOrientation((IEnumerable) this.Items);
      base.OnPropertyChanged(e);
    }
  }
}
