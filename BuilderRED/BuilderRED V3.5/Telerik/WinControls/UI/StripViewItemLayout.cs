// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StripViewItemLayout
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class StripViewItemLayout : RadPageViewElementBase
  {
    public static RadProperty MultiLineItemFitModeProperty = RadProperty.Register(nameof (MultiLineItemFitMode), typeof (MultiLineItemFitMode), typeof (StripViewItemLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) MultiLineItemFitMode.Reflow, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    internal static RadProperty ScrollOffsetProperty = RadProperty.Register("StripButtons", typeof (int), typeof (StripViewItemLayout), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private const int PartialItemOffset = 15;
    private StripViewLayoutInfo layoutInfo;
    private AnimatedPropertySetting scrollAnimation;
    private bool enableScrolling;
    private RadPageViewItem itemToEnsureVisible;
    private Dictionary<int, SizeF> rowsSize;

    static StripViewItemLayout()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new StripViewElementStateManager(), typeof (StripViewItemLayout));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.enableScrolling = true;
      this.scrollAnimation = new AnimatedPropertySetting();
      this.scrollAnimation.RemoveAfterApply = true;
      this.scrollAnimation.NumFrames = 5;
      this.scrollAnimation.ApplyEasingType = RadEasingType.InOutQuad;
      this.scrollAnimation.Property = StripViewItemLayout.ScrollOffsetProperty;
      int num;
      this.scrollAnimation.AnimationFinished += (AnimationFinishedEventHandler) ((param0, param1) => num = (int) this.SetValue(StripViewItemLayout.ScrollOffsetProperty, this.scrollAnimation.EndValue));
    }

    [Browsable(false)]
    public int ScrollOffset
    {
      get
      {
        return (int) this.GetValue(StripViewItemLayout.ScrollOffsetProperty);
      }
      private set
      {
        int num = (int) this.SetValue(StripViewItemLayout.ScrollOffsetProperty, (object) value);
      }
    }

    internal StripViewLayoutInfo LayoutInfo
    {
      get
      {
        return this.layoutInfo;
      }
    }

    public AnimatedPropertySetting ScrollAnimation
    {
      get
      {
        return this.scrollAnimation;
      }
    }

    [RadPropertyDefaultValue("MultiLineItemFitMode", typeof (StripViewItemLayout))]
    [Description("Gets or sets the MultiLineItemFitMode.This mode determines how the multiLine layout will behave when control is resizing.")]
    [Category("Appearance")]
    public MultiLineItemFitMode MultiLineItemFitMode
    {
      get
      {
        return (MultiLineItemFitMode) this.GetValue(StripViewItemLayout.MultiLineItemFitModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(StripViewItemLayout.MultiLineItemFitModeProperty, (object) value);
      }
    }

    protected override void OnUnloaded(ComponentThemableElementTree oldTree)
    {
      base.OnUnloaded(oldTree);
      this.itemToEnsureVisible = (RadPageViewItem) null;
      this.layoutInfo = (StripViewLayoutInfo) null;
      this.scrollAnimation.Stop((RadObject) this);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (this.ElementState != ElementState.Loaded)
        return;
      if (RadPageViewStripElement.PropertyInvalidatesScrollOffset(e.Property))
      {
        this.ResetScrollOffset();
      }
      else
      {
        if (e.Property != RadElement.BoundsProperty)
          return;
        this.FindAncestor<RadPageViewElement>()?.OnContentBoundsChanged();
      }
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
    }

    protected override void PaintFocusCues(IGraphics graphics, Rectangle clipRectange)
    {
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      float prevItemLength = -1f;
      if (this.layoutInfo != null)
        prevItemLength = this.layoutInfo.layoutLength;
      this.layoutInfo = (StripViewLayoutInfo) null;
      if ((double) availableSize.Width <= 0.0 || (double) availableSize.Height <= 0.0)
      {
        this.ResetScrollOffset();
        return SizeF.Empty;
      }
      this.layoutInfo = new StripViewLayoutInfo((LightVisualElement) this, availableSize);
      if (this.layoutInfo.itemCount == 0)
      {
        this.ResetScrollOffset();
        return SizeF.Empty;
      }
      this.ApplyItemSizeMode();
      this.ApplyItemFitMode();
      if (this.layoutInfo == null)
        return SizeF.Empty;
      if (this.layoutInfo.fitMode == StripViewItemFitMode.MultiLine)
        this.MeasureItemsInMultiLine(availableSize);
      else
        this.MeasureItems();
      this.UpdateScrollOffset(prevItemLength, availableSize);
      return this.ApplyMinMaxSize(this.ApplyClientOffset(this.layoutInfo.measuredSize));
    }

    private void MeasureItemsInMultiLine(SizeF availableSize)
    {
      SizeF empty1 = SizeF.Empty;
      this.rowsSize = new Dictionary<int, SizeF>();
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        if (viewItemSizeInfo.layoutSize != viewItemSizeInfo.desiredSize)
          viewItemSizeInfo.item.Measure(viewItemSizeInfo.layoutSize);
      }
      this.ReArrangeTabs(availableSize);
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        Padding margin = viewItemSizeInfo.item.Margin;
        if (this.layoutInfo.vertical)
        {
          if (this.rowsSize.ContainsKey(viewItemSizeInfo.item.Row))
          {
            SizeF sizeF = this.rowsSize[viewItemSizeInfo.item.Row];
            sizeF.Width = Math.Max(sizeF.Width, viewItemSizeInfo.layoutSize.Width + (float) margin.Horizontal);
            sizeF.Height += viewItemSizeInfo.layoutSize.Height + (float) margin.Vertical + (float) this.layoutInfo.itemSpacing;
            this.rowsSize[viewItemSizeInfo.item.Row] = sizeF;
          }
          else
          {
            SizeF empty2 = SizeF.Empty;
            empty2.Width = viewItemSizeInfo.layoutSize.Width + (float) margin.Horizontal;
            empty2.Height = viewItemSizeInfo.layoutSize.Height + (float) margin.Vertical + (float) this.layoutInfo.itemSpacing;
            this.rowsSize.Add(viewItemSizeInfo.item.Row, empty2);
          }
        }
        else if (this.rowsSize.ContainsKey(viewItemSizeInfo.item.Row))
        {
          SizeF sizeF = this.rowsSize[viewItemSizeInfo.item.Row];
          sizeF.Width += viewItemSizeInfo.layoutSize.Width + (float) margin.Horizontal + (float) this.layoutInfo.itemSpacing;
          sizeF.Height = Math.Max(sizeF.Height, viewItemSizeInfo.layoutSize.Height + (float) margin.Vertical);
          this.rowsSize[viewItemSizeInfo.item.Row] = sizeF;
        }
        else
        {
          SizeF empty2 = SizeF.Empty;
          empty2.Width = viewItemSizeInfo.layoutSize.Width + (float) margin.Horizontal + (float) this.layoutInfo.itemSpacing;
          empty2.Height = viewItemSizeInfo.layoutSize.Height + (float) margin.Vertical;
          this.rowsSize.Add(viewItemSizeInfo.item.Row, empty2);
        }
      }
      if (this.layoutInfo.vertical)
      {
        foreach (KeyValuePair<int, SizeF> keyValuePair in this.rowsSize)
        {
          empty1.Width += keyValuePair.Value.Width;
          empty1.Height = Math.Max(empty1.Height, keyValuePair.Value.Height);
        }
      }
      else
      {
        foreach (KeyValuePair<int, SizeF> keyValuePair in this.rowsSize)
        {
          empty1.Height += keyValuePair.Value.Height;
          empty1.Width = Math.Max(empty1.Width, keyValuePair.Value.Width);
        }
      }
      this.layoutInfo.layoutLength = empty1.Width;
      this.layoutInfo.measuredSize = empty1;
    }

    private void MeasureItems()
    {
      SizeF empty = SizeF.Empty;
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        if (viewItemSizeInfo.layoutSize != viewItemSizeInfo.desiredSize)
          viewItemSizeInfo.item.Measure(viewItemSizeInfo.layoutSize);
        Padding margin = viewItemSizeInfo.item.Margin;
        if (this.layoutInfo.vertical)
        {
          empty.Width = Math.Max(empty.Width, viewItemSizeInfo.layoutSize.Width + (float) margin.Horizontal);
          empty.Height += viewItemSizeInfo.layoutSize.Height + (float) margin.Vertical;
        }
        else
        {
          empty.Width += viewItemSizeInfo.layoutSize.Width + (float) margin.Horizontal;
          empty.Height = Math.Max(empty.Height, viewItemSizeInfo.layoutSize.Height + (float) margin.Vertical);
        }
        this.layoutInfo.layoutLength += viewItemSizeInfo.layoutLength + (float) viewItemSizeInfo.marginLength;
      }
      int num = (this.layoutInfo.itemCount - 1) * this.layoutInfo.itemSpacing;
      if (this.layoutInfo.vertical)
        empty.Height += (float) num;
      else
        empty.Width += (float) num;
      this.layoutInfo.layoutLength += (float) num;
      this.layoutInfo.measuredSize = empty;
    }

    private void ApplyItemSizeMode()
    {
      if (this.layoutInfo.sizeMode == PageViewItemSizeMode.Individual)
        return;
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        SizeF layoutSize = viewItemSizeInfo.layoutSize;
        switch (this.layoutInfo.align)
        {
          case StripViewAlignment.Top:
          case StripViewAlignment.Bottom:
            if ((this.layoutInfo.sizeMode & PageViewItemSizeMode.EqualWidth) == PageViewItemSizeMode.EqualWidth)
              layoutSize.Width = this.layoutInfo.maxWidth;
            if ((this.layoutInfo.sizeMode & PageViewItemSizeMode.EqualHeight) == PageViewItemSizeMode.EqualHeight)
            {
              layoutSize.Height = this.layoutInfo.maxHeight;
              break;
            }
            break;
          case StripViewAlignment.Right:
          case StripViewAlignment.Left:
            if ((this.layoutInfo.sizeMode & PageViewItemSizeMode.EqualWidth) == PageViewItemSizeMode.EqualWidth)
              layoutSize.Height = this.layoutInfo.maxHeight;
            if ((this.layoutInfo.sizeMode & PageViewItemSizeMode.EqualHeight) == PageViewItemSizeMode.EqualHeight)
            {
              layoutSize.Width = this.layoutInfo.maxWidth;
              break;
            }
            break;
        }
        viewItemSizeInfo.SetLayoutSize(layoutSize);
      }
    }

    private void ApplyItemFitMode()
    {
      if (this.layoutInfo.fitMode == StripViewItemFitMode.MultiLine)
        return;
      if ((this.layoutInfo.fitMode & StripViewItemFitMode.Fill) == StripViewItemFitMode.Fill)
        new StripViewItemExpandStrategy(this.layoutInfo).Execute();
      if ((this.layoutInfo.fitMode & StripViewItemFitMode.Shrink) != StripViewItemFitMode.Shrink)
        return;
      new StripViewItemShrinkStrategy(this.layoutInfo).Execute();
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.layoutInfo == null)
        return finalSize;
      RectangleF rectangleF1 = this.GetAlignedClientRectangle(finalSize, true);
      RectangleF rectangleF2 = this.GetAlignedClientRectangle(finalSize, false);
      if (this.RightToLeft && this.layoutInfo.align != StripViewAlignment.Left && this.layoutInfo.align != StripViewAlignment.Right)
      {
        rectangleF1 = LayoutUtils.RTLTranslateNonRelative(rectangleF1, new RectangleF(PointF.Empty, finalSize));
        rectangleF2 = LayoutUtils.RTLTranslateNonRelative(rectangleF2, new RectangleF(PointF.Empty, finalSize));
      }
      if ((double) rectangleF1.Width > 0.0 && (double) rectangleF1.Height > 0.0)
      {
        if (this.layoutInfo.fitMode == StripViewItemFitMode.MultiLine)
          this.ArrangeItemsMultiLine(rectangleF1);
        else
          this.ArrangeItems(rectangleF1, rectangleF2);
      }
      this.FindAncestor<StripViewItemContainer>()?.UpdateButtonsEnabledState();
      if (this.itemToEnsureVisible != null)
      {
        this.ScrollToItem(this.itemToEnsureVisible);
        this.itemToEnsureVisible = (RadPageViewItem) null;
      }
      return finalSize;
    }

    private void ArrangeItemsMultiLine(RectangleF client)
    {
      PointF location = (PointF) Point.Empty;
      SortedDictionary<int, float> sortedDictionary1 = new SortedDictionary<int, float>();
      foreach (RadElement child in this.Children)
      {
        RadPageViewItem radPageViewItem = child as RadPageViewItem;
        if (radPageViewItem != null && !sortedDictionary1.ContainsKey(radPageViewItem.Row))
          sortedDictionary1.Add(radPageViewItem.Row, 0.0f);
      }
      foreach (RadElement child in this.Children)
      {
        RadPageViewItem radPageViewItem = child as RadPageViewItem;
        if (radPageViewItem != null && radPageViewItem.Visibility != ElementVisibility.Collapsed)
        {
          SizeF size = radPageViewItem.ForcedLayoutSize;
          if ((this.layoutInfo.fitMode & StripViewItemFitMode.FillHeight) != StripViewItemFitMode.None)
          {
            switch (this.layoutInfo.align)
            {
              case StripViewAlignment.Top:
              case StripViewAlignment.Bottom:
                size = new SizeF(size.Width, size.Height);
                break;
              case StripViewAlignment.Right:
              case StripViewAlignment.Left:
                size = new SizeF(size.Width, size.Height);
                break;
            }
          }
          Padding margin = radPageViewItem.Margin;
          switch (this.layoutInfo.align)
          {
            case StripViewAlignment.Top:
              if (radPageViewItem.Row == 0 || radPageViewItem.Row == -1)
              {
                location = new PointF(sortedDictionary1[radPageViewItem.Row], client.Bottom - size.Height - (float) margin.Vertical);
                SortedDictionary<int, float> sortedDictionary2;
                int row;
                (sortedDictionary2 = sortedDictionary1)[row = radPageViewItem.Row] = sortedDictionary2[row] + (size.Width + (float) margin.Horizontal + (float) this.layoutInfo.itemSpacing);
                break;
              }
              float num1 = 0.0f;
              for (int key = radPageViewItem.Row - 1; key >= -1; --key)
              {
                if (this.rowsSize.ContainsKey(key))
                  num1 += this.rowsSize[key].Height;
              }
              location = new PointF(sortedDictionary1[radPageViewItem.Row], Math.Max(0.0f, client.Bottom - num1 - radPageViewItem.DesiredSize.Height - (float) margin.Vertical));
              SortedDictionary<int, float> sortedDictionary3;
              int row1;
              (sortedDictionary3 = sortedDictionary1)[row1 = radPageViewItem.Row] = sortedDictionary3[row1] + (size.Width + (float) margin.Horizontal + (float) this.layoutInfo.itemSpacing);
              break;
            case StripViewAlignment.Right:
              if (radPageViewItem.Row == 0 || radPageViewItem.Row == -1)
              {
                location = new PointF(0.0f, sortedDictionary1[radPageViewItem.Row] + (float) margin.Vertical);
                SortedDictionary<int, float> sortedDictionary2;
                int row2;
                (sortedDictionary2 = sortedDictionary1)[row2 = radPageViewItem.Row] = sortedDictionary2[row2] + (size.Height + (float) margin.Vertical + (float) this.layoutInfo.itemSpacing);
                break;
              }
              float x = 0.0f;
              for (int key = radPageViewItem.Row - 1; key >= -1; --key)
              {
                if (this.rowsSize.ContainsKey(key))
                  x += this.rowsSize[key].Width;
              }
              location = new PointF(x, sortedDictionary1[radPageViewItem.Row] + (float) margin.Vertical);
              SortedDictionary<int, float> sortedDictionary4;
              int row3;
              (sortedDictionary4 = sortedDictionary1)[row3 = radPageViewItem.Row] = sortedDictionary4[row3] + (size.Height + (float) margin.Vertical + (float) this.layoutInfo.itemSpacing);
              break;
            case StripViewAlignment.Bottom:
              if (radPageViewItem.Row == 0 || radPageViewItem.Row == -1)
              {
                location = new PointF(sortedDictionary1[radPageViewItem.Row], client.Top + (float) margin.Vertical);
                SortedDictionary<int, float> sortedDictionary2;
                int row2;
                (sortedDictionary2 = sortedDictionary1)[row2 = radPageViewItem.Row] = sortedDictionary2[row2] + (size.Width + (float) margin.Horizontal + (float) this.layoutInfo.itemSpacing);
                break;
              }
              float num2 = 0.0f;
              for (int key = radPageViewItem.Row - 1; key >= -1; --key)
              {
                if (this.rowsSize.ContainsKey(key))
                  num2 += this.rowsSize[key].Height;
              }
              location = new PointF(sortedDictionary1[radPageViewItem.Row], Math.Max(0.0f, client.Top + num2 + (float) margin.Vertical));
              SortedDictionary<int, float> sortedDictionary5;
              int row4;
              (sortedDictionary5 = sortedDictionary1)[row4 = radPageViewItem.Row] = sortedDictionary5[row4] + (size.Width + (float) margin.Horizontal + (float) this.layoutInfo.itemSpacing);
              break;
            case StripViewAlignment.Left:
              if (radPageViewItem.Row == 0 || radPageViewItem.Row == -1)
              {
                location = new PointF(client.Right - size.Width - (float) margin.Horizontal, sortedDictionary1[radPageViewItem.Row] + (float) margin.Vertical);
                SortedDictionary<int, float> sortedDictionary2;
                int row2;
                (sortedDictionary2 = sortedDictionary1)[row2 = radPageViewItem.Row] = sortedDictionary2[row2] + (size.Height + (float) margin.Vertical + (float) this.layoutInfo.itemSpacing);
                break;
              }
              float num3 = 0.0f;
              for (int key = radPageViewItem.Row - 1; key >= -1; --key)
              {
                if (this.rowsSize.ContainsKey(key))
                  num3 += this.rowsSize[key].Width;
              }
              location = new PointF(client.Right - size.Width - (float) margin.Horizontal - num3, sortedDictionary1[radPageViewItem.Row] + (float) margin.Vertical);
              SortedDictionary<int, float> sortedDictionary6;
              int row5;
              (sortedDictionary6 = sortedDictionary1)[row5 = radPageViewItem.Row] = sortedDictionary6[row5] + (size.Height + (float) margin.Vertical + (float) this.layoutInfo.itemSpacing);
              break;
          }
          RectangleF rectangleF = new RectangleF(location, size);
          if (this.RightToLeft && (this.layoutInfo.align == StripViewAlignment.Top || this.layoutInfo.align == StripViewAlignment.Bottom))
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, client);
          radPageViewItem.Arrange(rectangleF);
        }
      }
    }

    private void ArrangeItems(RectangleF client, RectangleF clientNoOffset)
    {
      float x1 = client.X;
      float y1 = client.Y;
      float x2 = Math.Max(clientNoOffset.X, 0.0f);
      float y2 = Math.Max(clientNoOffset.Y, 0.0f);
      foreach (RadElement child in this.Children)
      {
        RadPageViewItem radPageViewItem = child as RadPageViewItem;
        if (radPageViewItem != null && radPageViewItem.Visibility != ElementVisibility.Collapsed && (radPageViewItem.IsPinned && !radPageViewItem.IsPreview))
        {
          SizeF sizeF = this.ArrangeItem(radPageViewItem, client, clientNoOffset, new PointF(x2, y2));
          x2 += sizeF.Width;
          y2 += sizeF.Height;
        }
      }
      this.ArrangePreviewItem(client, clientNoOffset);
      this.layoutInfo.pinnedItemsOffset = new SizeF(x2 - Math.Max(clientNoOffset.X, 0.0f), y2 - Math.Max(clientNoOffset.Y, 0.0f));
      float x3 = x1 + this.layoutInfo.pinnedItemsOffset.Width;
      float y3 = y1 + this.layoutInfo.pinnedItemsOffset.Height;
      foreach (RadElement child in this.Children)
      {
        RadPageViewItem radPageViewItem = child as RadPageViewItem;
        if (radPageViewItem != null && radPageViewItem.Visibility != ElementVisibility.Collapsed && (!radPageViewItem.IsPinned && !radPageViewItem.IsPreview))
        {
          SizeF sizeF = this.ArrangeItem(radPageViewItem, client, clientNoOffset, new PointF(x3, y3));
          x3 += sizeF.Width;
          y3 += sizeF.Height;
        }
      }
    }

    private void ArrangePreviewItem(RectangleF client, RectangleF clientNoOffset)
    {
      foreach (RadElement child in this.Children)
      {
        RadPageViewItem radPageViewItem = child as RadPageViewItem;
        if (radPageViewItem != null && radPageViewItem.Visibility != ElementVisibility.Collapsed && radPageViewItem.IsPreview)
        {
          SizeF size = radPageViewItem.ForcedLayoutSize;
          if ((this.layoutInfo.fitMode & StripViewItemFitMode.FillHeight) != StripViewItemFitMode.None)
          {
            switch (this.layoutInfo.align)
            {
              case StripViewAlignment.Top:
              case StripViewAlignment.Bottom:
                size = new SizeF(size.Width, client.Height);
                break;
              case StripViewAlignment.Right:
              case StripViewAlignment.Left:
                size = new SizeF(client.Width, size.Height);
                break;
            }
          }
          Padding margin = radPageViewItem.Margin;
          PointF location = PointF.Empty;
          switch (this.layoutInfo.align)
          {
            case StripViewAlignment.Top:
              location = new PointF(clientNoOffset.Right - size.Width - (float) margin.Left, client.Bottom - size.Height - (float) margin.Vertical);
              break;
            case StripViewAlignment.Right:
              location = new PointF(client.X + (float) margin.Left, clientNoOffset.Bottom - size.Height - (float) margin.Bottom);
              break;
            case StripViewAlignment.Bottom:
              location = new PointF(clientNoOffset.Right - size.Width - (float) margin.Left, client.Y + (float) margin.Top);
              break;
            case StripViewAlignment.Left:
              location = new PointF(client.Right - size.Width - (float) margin.Horizontal, clientNoOffset.Bottom - size.Height - (float) margin.Bottom);
              break;
          }
          RectangleF rectangleF = new RectangleF(location, size);
          if (this.RightToLeft && (this.layoutInfo.align == StripViewAlignment.Top || this.layoutInfo.align == StripViewAlignment.Bottom))
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, clientNoOffset);
          radPageViewItem.Arrange(rectangleF);
          this.layoutInfo.previewItemSize = size;
        }
      }
    }

    private SizeF ArrangeItem(
      RadPageViewItem item,
      RectangleF clientRect,
      RectangleF clientNoOffset,
      PointF currentLocation)
    {
      SizeF size = item.ForcedLayoutSize;
      if ((this.layoutInfo.fitMode & StripViewItemFitMode.FillHeight) != StripViewItemFitMode.None)
      {
        switch (this.layoutInfo.align)
        {
          case StripViewAlignment.Top:
          case StripViewAlignment.Bottom:
            size = new SizeF(size.Width, clientRect.Height);
            break;
          case StripViewAlignment.Right:
          case StripViewAlignment.Left:
            size = new SizeF(clientRect.Width, size.Height);
            break;
        }
      }
      Padding margin = item.Margin;
      PointF location = PointF.Empty;
      SizeF empty = SizeF.Empty;
      switch (this.layoutInfo.align)
      {
        case StripViewAlignment.Top:
          location = new PointF(currentLocation.X + (float) margin.Left, clientRect.Bottom - size.Height - (float) margin.Vertical);
          empty.Width += size.Width + (float) margin.Horizontal + (float) this.layoutInfo.itemSpacing;
          break;
        case StripViewAlignment.Right:
          location = new PointF(clientRect.X + (float) margin.Left, currentLocation.Y + (float) margin.Top);
          empty.Height += size.Height + (float) margin.Vertical + (float) this.layoutInfo.itemSpacing;
          break;
        case StripViewAlignment.Bottom:
          location = new PointF(currentLocation.X + (float) margin.Left, clientRect.Y + (float) margin.Top);
          empty.Width += size.Width + (float) margin.Horizontal + (float) this.layoutInfo.itemSpacing;
          break;
        case StripViewAlignment.Left:
          location = new PointF(clientRect.Right - size.Width - (float) margin.Horizontal, currentLocation.Y + (float) margin.Top);
          empty.Height += size.Height + (float) margin.Vertical + (float) this.layoutInfo.itemSpacing;
          break;
      }
      RectangleF rectangleF = new RectangleF(location, size);
      if (this.RightToLeft && (this.layoutInfo.align == StripViewAlignment.Top || this.layoutInfo.align == StripViewAlignment.Bottom))
        rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, item.IsPinned ? clientNoOffset : clientRect);
      item.Arrange(rectangleF);
      return empty;
    }

    private RectangleF GetAlignedClientRectangle(SizeF finalSize, bool addScrollOffset)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if ((double) clientRectangle.Width <= 0.0 || (double) clientRectangle.Height <= 0.0)
        return RectangleF.Empty;
      RectangleF rectangleF = clientRectangle;
      switch (this.layoutInfo.itemAlign)
      {
        case StripViewItemAlignment.Center:
          rectangleF = this.GetCenterClientRect(clientRectangle);
          break;
        case StripViewItemAlignment.Far:
          rectangleF = this.GetFarClientRect(clientRectangle);
          break;
      }
      if (!addScrollOffset)
      {
        rectangleF.Width = clientRectangle.Right - rectangleF.X;
        rectangleF.Height = clientRectangle.Bottom - rectangleF.Y;
      }
      RectangleF client = rectangleF;
      if (!addScrollOffset)
        return client;
      return this.AddStripAndScrollOffset(client);
    }

    private RectangleF AddStripAndScrollOffset(RectangleF client)
    {
      switch (this.layoutInfo.align)
      {
        case StripViewAlignment.Top:
        case StripViewAlignment.Bottom:
          switch (this.layoutInfo.itemAlign)
          {
            case StripViewItemAlignment.Near:
            case StripViewItemAlignment.Center:
              client.X -= (float) this.ScrollOffset;
              break;
            case StripViewItemAlignment.Far:
              client.X += (float) this.ScrollOffset;
              break;
          }
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          switch (this.layoutInfo.itemAlign)
          {
            case StripViewItemAlignment.Near:
            case StripViewItemAlignment.Center:
              client.Y -= (float) this.ScrollOffset;
              break;
            case StripViewItemAlignment.Far:
              client.Y += (float) this.ScrollOffset;
              break;
          }
      }
      return client;
    }

    private RectangleF GetCenterClientRect(RectangleF client)
    {
      SizeF measuredSize = this.layoutInfo.measuredSize;
      switch (this.layoutInfo.align)
      {
        case StripViewAlignment.Top:
        case StripViewAlignment.Bottom:
          if ((double) measuredSize.Width < (double) client.Width)
          {
            client.X += (float) (int) (((double) client.Width - (double) measuredSize.Width) / 2.0 + 0.5);
            break;
          }
          break;
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          if ((double) measuredSize.Height < (double) client.Height)
          {
            client.Y += (float) (int) (((double) client.Height - (double) measuredSize.Height) / 2.0 + 0.5);
            break;
          }
          break;
      }
      return client;
    }

    private RectangleF GetFarClientRect(RectangleF client)
    {
      SizeF measuredSize = this.layoutInfo.measuredSize;
      switch (this.layoutInfo.align)
      {
        case StripViewAlignment.Top:
        case StripViewAlignment.Bottom:
          client.X += client.Width - measuredSize.Width;
          break;
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          client.Y += client.Height - measuredSize.Height;
          break;
      }
      return client;
    }

    internal void EnableScrolling(bool enable)
    {
      this.enableScrolling = enable;
      if (this.layoutInfo == null)
        return;
      int scrollOffset = this.ScrollOffset;
      if (!this.enableScrolling)
      {
        this.scrollAnimation.UnapplyValue((RadObject) this);
        this.scrollAnimation.EndValue = (object) 0;
        int num = (int) this.ResetValue(StripViewItemLayout.ScrollOffsetProperty, ValueResetFlags.Animation);
        this.ScrollOffset = scrollOffset;
      }
      else
        this.scrollAnimation.EndValue = (object) scrollOffset;
    }

    internal void SetScrollAnimation(RadEasingType type)
    {
      this.scrollAnimation.ApplyEasingType = type;
    }

    internal bool EnsureVisible(RadPageViewItem item)
    {
      if (item.Visibility == ElementVisibility.Collapsed || item.IsPinned || item.IsPreview)
        return false;
      if (this.layoutInfo == null || !this.IsMeasureValid || !this.IsArrangeValid)
      {
        this.itemToEnsureVisible = item;
        return false;
      }
      this.ScrollToItem(item);
      return true;
    }

    private void ScrollToItem(RadPageViewItem item)
    {
      RectangleF clientRectangle = this.GetClientRectangle(this.layoutInfo.availableSize);
      RectangleF boundingRectangle = (RectangleF) item.BoundingRectangle;
      switch (this.layoutInfo.align)
      {
        case StripViewAlignment.Top:
        case StripViewAlignment.Bottom:
          if (!this.ShouldUseRightToLeft())
            clientRectangle.X += this.layoutInfo.pinnedItemsOffset.Width;
          clientRectangle.Width -= this.layoutInfo.pinnedItemsOffset.Width + this.layoutInfo.previewItemSize.Width;
          break;
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          clientRectangle.Y += this.layoutInfo.pinnedItemsOffset.Height;
          clientRectangle.Height -= this.layoutInfo.pinnedItemsOffset.Height + this.layoutInfo.previewItemSize.Height;
          break;
      }
      this.ScrollToBounds(clientRectangle, boundingRectangle);
      this.InvalidateArrange();
    }

    private bool ShouldUseRightToLeft()
    {
      if (this.RightToLeft && this.layoutInfo.align != StripViewAlignment.Left)
        return this.layoutInfo.align != StripViewAlignment.Right;
      return false;
    }

    private void ScrollToBounds(RectangleF client, RectangleF itemBounds)
    {
      int num1 = this.layoutInfo.itemSpacing + 15;
      float num2;
      float num3;
      if (this.layoutInfo.vertical)
      {
        num2 = itemBounds.Bottom - client.Bottom + (float) num1;
        num3 = client.Y - itemBounds.Y + (float) num1;
      }
      else
      {
        num2 = itemBounds.Right - client.Right + (float) num1;
        num3 = client.X - itemBounds.X + (float) num1;
      }
      if (this.layoutInfo.itemAlign == StripViewItemAlignment.Far ^ this.ShouldUseRightToLeft())
      {
        float num4 = num3;
        num3 = num2;
        num2 = num4;
      }
      if ((double) num3 > 0.0)
      {
        this.SetScrollOffset((int) ((double) this.ScrollOffset - (double) num3), true);
      }
      else
      {
        if ((double) num2 <= 0.0)
          return;
        this.SetScrollOffset((int) ((double) this.ScrollOffset + (double) num2), true);
      }
    }

    internal bool CanScroll(StripViewButtons button)
    {
      if (this.layoutInfo == null || this.layoutInfo.itemCount <= 1)
        return false;
      if (this.layoutInfo.itemAlign == StripViewItemAlignment.Far ^ this.ShouldUseRightToLeft())
        button = this.FlipScrollButtons(button);
      return this.GetScrollStep(this.PreviousConstraint, button) > 0;
    }

    private StripViewButtons FlipScrollButtons(StripViewButtons buttons)
    {
      buttons = buttons != StripViewButtons.LeftScroll ? StripViewButtons.LeftScroll : StripViewButtons.RightScroll;
      return buttons;
    }

    internal void Scroll(StripViewButtons button)
    {
      if (this.layoutInfo == null)
        return;
      if (this.layoutInfo.itemAlign == StripViewItemAlignment.Far ^ this.ShouldUseRightToLeft())
        button = this.FlipScrollButtons(button);
      switch (button)
      {
        case StripViewButtons.LeftScroll:
          this.SetScrollOffset(this.ScrollOffset - this.GetScrollStep(this.PreviousConstraint, StripViewButtons.LeftScroll), true);
          break;
        case StripViewButtons.RightScroll:
          this.SetScrollOffset(this.ScrollOffset + this.GetScrollStep(this.PreviousConstraint, StripViewButtons.RightScroll), true);
          break;
      }
    }

    private void UpdateScrollOffset(float prevItemLength, SizeF availableSize)
    {
      if (this.ScrollOffset == 0)
        return;
      SizeF previousConstraint = this.PreviousConstraint;
      if ((double) previousConstraint.Width <= 0.0 || (double) previousConstraint.Height <= 0.0 || ((double) previousConstraint.Width == double.PositiveInfinity || (double) previousConstraint.Height == double.PositiveInfinity))
      {
        this.ResetScrollOffset();
      }
      else
      {
        float num = 0.0f;
        switch (this.layoutInfo.align)
        {
          case StripViewAlignment.Top:
          case StripViewAlignment.Bottom:
            num = availableSize.Width - previousConstraint.Width;
            break;
          case StripViewAlignment.Right:
          case StripViewAlignment.Left:
            num = availableSize.Height - previousConstraint.Height;
            break;
        }
        int scrollOffset = this.ScrollOffset;
        if ((double) num > 0.0)
          scrollOffset -= (int) num;
        this.SetScrollOffset(Math.Max(0, scrollOffset), false);
      }
    }

    private void ResetScrollOffset()
    {
      bool enableScrolling = this.enableScrolling;
      this.EnableScrolling(false);
      this.SetScrollOffset(0, false);
      this.EnableScrolling(enableScrolling);
    }

    private void SetScrollOffset(int offset, bool arrange)
    {
      if (this.layoutInfo != null)
      {
        float num = this.layoutInfo.availableLength - this.layoutInfo.borderLength - this.layoutInfo.paddingLength;
        if ((double) offset + (double) num > (double) this.layoutInfo.layoutLength)
          offset = (int) ((double) this.layoutInfo.layoutLength - (double) num);
        offset = Math.Max(0, offset);
      }
      if (this.ScrollOffset == offset)
        return;
      if (this.enableScrolling)
      {
        if (this.scrollAnimation.IsAnimating((RadObject) this))
          this.scrollAnimation.Stop((RadObject) this);
        this.scrollAnimation.StartValue = this.scrollAnimation.EndValue;
        this.scrollAnimation.EndValue = (object) offset;
        this.scrollAnimation.ApplyValue((RadObject) this);
      }
      else
        this.ScrollOffset = offset;
      if (!arrange)
        return;
      this.InvalidateArrange();
    }

    private int GetScrollStep(SizeF available, StripViewButtons scrollButtons)
    {
      if (this.layoutInfo == null)
        return 0;
      RectangleF clientRectangle = this.GetClientRectangle(available);
      float num = 0.0f;
      float val1 = 0.0f;
      float layoutLength = this.layoutInfo.layoutLength;
      switch (this.layoutInfo.align)
      {
        case StripViewAlignment.Top:
        case StripViewAlignment.Bottom:
          if ((double) layoutLength > (double) clientRectangle.Width)
            num = layoutLength - clientRectangle.Width;
          val1 = clientRectangle.Width;
          break;
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          if ((double) layoutLength > (double) clientRectangle.Height)
            num = layoutLength - clientRectangle.Height;
          val1 = clientRectangle.Height;
          break;
      }
      float val2 = scrollButtons != StripViewButtons.LeftScroll ? num - (float) this.ScrollOffset : (float) this.ScrollOffset;
      return (int) Math.Min(val1, val2);
    }

    private void ReArrangeTabs(SizeF availableSize)
    {
      if (this.MultiLineItemFitMode == MultiLineItemFitMode.None)
        return;
      float num1 = (float) (-1 * this.layoutInfo.itemSpacing);
      bool flag = true;
      for (int index = 0; index < this.layoutInfo.items.Count; ++index)
      {
        if (this.layoutInfo.vertical)
          num1 += this.layoutInfo.items[index].layoutSize.Height + (float) this.layoutInfo.itemSpacing;
        else
          num1 += this.layoutInfo.items[index].layoutSize.Width + (float) this.layoutInfo.itemSpacing;
        if (this.layoutInfo.items[index].item.Row != 0)
          flag = false;
      }
      if (this.layoutInfo.vertical && (double) num1 < (double) availableSize.Height && flag || !this.layoutInfo.vertical && (double) num1 < (double) availableSize.Width && flag)
        return;
      List<PageViewItemSizeInfo> tabs = new List<PageViewItemSizeInfo>();
      float num2 = 0.0f;
      for (int index = this.layoutInfo.items.Count - 1; index >= 0 && !this.layoutInfo.items[index].item.IsSelected && ((!this.layoutInfo.vertical || (double) num1 - (double) num2 >= (double) availableSize.Height) && (this.layoutInfo.vertical || (double) num1 - (double) num2 >= (double) availableSize.Width)); --index)
      {
        if (this.layoutInfo.vertical)
          num2 += this.layoutInfo.items[index].layoutSize.Height;
        else
          num2 += this.layoutInfo.items[index].layoutSize.Width;
        tabs.Add(this.layoutInfo.items[index]);
      }
      for (int index = 0; index < this.layoutInfo.items.Count && !this.layoutInfo.items[index].item.IsSelected && ((!this.layoutInfo.vertical || (double) num1 - (double) num2 >= (double) availableSize.Height) && (this.layoutInfo.vertical || (double) num1 - (double) num2 >= (double) availableSize.Width)); ++index)
      {
        if (this.layoutInfo.vertical)
          num2 += this.layoutInfo.items[index].layoutSize.Height;
        else
          num2 += this.layoutInfo.items[index].layoutSize.Width;
        tabs.Add(this.layoutInfo.items[index]);
      }
      for (int index = 0; index < this.layoutInfo.items.Count; ++index)
      {
        if (!tabs.Contains(this.layoutInfo.items[index]))
          this.layoutInfo.items[index].item.Row = 0;
      }
      this.ReArrangeFloatingTabs(tabs, availableSize);
    }

    private void ReArrangeFloatingTabs(List<PageViewItemSizeInfo> tabs, SizeF availableSize)
    {
      int num1 = 1;
      float num2 = 0.0f;
      List<PageViewItemSizeInfo> viewItemSizeInfoList = new List<PageViewItemSizeInfo>();
      for (int index = 0; index < this.layoutInfo.items.Count; ++index)
      {
        if (tabs.Contains(this.layoutInfo.items[index]))
          viewItemSizeInfoList.Add(this.layoutInfo.items[index]);
      }
      for (int index = 0; index < viewItemSizeInfoList.Count; ++index)
      {
        if (this.layoutInfo.vertical && (double) num2 + (double) viewItemSizeInfoList[index].layoutSize.Height > (double) availableSize.Height || !this.layoutInfo.vertical && (double) num2 + (double) viewItemSizeInfoList[index].layoutSize.Width > (double) availableSize.Width)
        {
          ++num1;
          num2 = 0.0f;
        }
        if (this.layoutInfo.vertical)
          num2 += viewItemSizeInfoList[index].layoutSize.Height + (float) this.layoutInfo.itemSpacing;
        else
          num2 += viewItemSizeInfoList[index].layoutSize.Width + (float) this.layoutInfo.itemSpacing;
        viewItemSizeInfoList[index].item.Row = num1;
      }
    }

    internal void ReArrangeRows(int oldDesiredRow)
    {
      if (this.layoutInfo == null || oldDesiredRow == 0 || (this.rowsSize == null || this.layoutInfo.fitMode != StripViewItemFitMode.MultiLine))
        return;
      Dictionary<int, SizeF> dictionary = new Dictionary<int, SizeF>();
      foreach (PageViewItemSizeInfo viewItemSizeInfo in this.layoutInfo.items)
      {
        if (viewItemSizeInfo.item.Row == oldDesiredRow)
        {
          if (!dictionary.ContainsKey(0))
            dictionary.Add(0, this.rowsSize[oldDesiredRow]);
          viewItemSizeInfo.item.Row = 0;
        }
        else
        {
          int row = viewItemSizeInfo.item.Row;
          if (viewItemSizeInfo.item.Row == 0 || viewItemSizeInfo.item.Row == -1)
            viewItemSizeInfo.item.Row = 1;
          else if (viewItemSizeInfo.item.Row < oldDesiredRow)
            ++viewItemSizeInfo.item.Row;
          if (!dictionary.ContainsKey(viewItemSizeInfo.item.Row))
            dictionary.Add(viewItemSizeInfo.item.Row, this.rowsSize[row]);
        }
      }
      this.rowsSize = dictionary;
    }
  }
}
