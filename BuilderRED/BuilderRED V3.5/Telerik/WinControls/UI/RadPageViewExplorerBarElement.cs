// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewExplorerBarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [Description("Represents the element that implements the ExplorerBar view of the RadPageView control. This view allows for multiple visible pages, whereby items can be expanded/collapsed to show their content in an associated page.")]
  public class RadPageViewExplorerBarElement : RadPageViewStackElement
  {
    private Padding ncMetrics = Padding.Empty;
    public static readonly RadProperty ContentSizeModeProperty = RadProperty.Register(nameof (ContentSizeMode), typeof (ExplorerBarContentSizeMode), typeof (RadPageViewExplorerBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ExplorerBarContentSizeMode.FixedLength, ElementPropertyOptions.AffectsMeasure));
    private const int DefaultYOffset = 5;
    private RadScrollBarElement scrollbar;
    private int initialLayoutOffset;
    private bool allowNCCALCSIZEProcessing;

    static RadPageViewExplorerBarElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new StackViewElementStateManager(), typeof (RadPageViewExplorerBarElement));
    }

    public event EventHandler<RadPageViewExpandedChangedEventArgs> ExpandedChanged;

    [Browsable(false)]
    public RadScrollBarElement Scrollbar
    {
      get
      {
        return this.scrollbar;
      }
    }

    [Description("Gets or sets a value that defines how the content areas for each item are sized.")]
    [DefaultValue(typeof (ExplorerBarContentSizeMode), "FixedLength")]
    public ExplorerBarContentSizeMode ContentSizeMode
    {
      get
      {
        return (ExplorerBarContentSizeMode) this.GetValue(RadPageViewExplorerBarElement.ContentSizeModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewExplorerBarElement.ContentSizeModeProperty, (object) value);
      }
    }

    protected override ValueUpdateResult SetValueCore(
      RadPropertyValue propVal,
      object propModifier,
      object newValue,
      ValueSource source)
    {
      RadProperty property = propVal.Property;
      if (property == RadPageViewStackElement.ItemSelectionModeProperty && (StackViewItemSelectionMode) newValue != StackViewItemSelectionMode.ContentAfterSelected || property == RadPageViewStackElement.StackPositionProperty && ((StackViewPosition) newValue == StackViewPosition.Bottom || (StackViewPosition) newValue == StackViewPosition.Right))
        return ValueUpdateResult.Canceled;
      return base.SetValueCore(propVal, propModifier, newValue, source);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Header.RadPropertyChanged += new RadPropertyChangedEventHandler(this.OnNCElement_PropertyChanged);
      this.Footer.RadPropertyChanged += new RadPropertyChangedEventHandler(this.OnNCElement_PropertyChanged);
      this.scrollbar = new RadScrollBarElement();
      this.scrollbar.Visibility = ElementVisibility.Collapsed;
      this.scrollbar.ScrollType = ScrollType.Vertical;
      this.scrollbar.Scroll += new ScrollEventHandler(this.OnScrollbar_Scroll);
      this.scrollbar.SmallChange = 5;
      this.scrollbar.ZIndex = 3;
      this.Children.Add((RadElement) this.scrollbar);
    }

    protected override void DisposeManagedResources()
    {
      this.scrollbar.Scroll -= new ScrollEventHandler(this.OnScrollbar_Scroll);
      this.Header.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.OnNCElement_PropertyChanged);
      this.Footer.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.OnNCElement_PropertyChanged);
      base.DisposeManagedResources();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ItemSelectionMode = StackViewItemSelectionMode.ContentAfterSelected;
      this.StackPosition = StackViewPosition.Top;
    }

    protected override void OnLoaded()
    {
      foreach (RadPageViewExplorerBarItem viewExplorerBarItem in (IEnumerable<RadPageViewItem>) this.Items)
        this.SetPageVisibility(viewExplorerBarItem);
    }

    protected internal override void OnSelectedPageChanged(RadPageViewEventArgs e)
    {
      base.OnSelectedPageChanged(e);
      this.RefreshNCArea();
    }

    protected override void SetSelectedContent(RadPageViewItem item)
    {
    }

    public override RadPageViewContentAreaElement GetContentAreaForItem(
      RadPageViewItem item)
    {
      if (item == null)
        return this.ContentArea as RadPageViewContentAreaElement;
      return (item as RadPageViewExplorerBarItem).AssociatedContentAreaElement;
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
      if (handledMouseEventArgs == null || handledMouseEventArgs.Handled)
        return;
      int num1 = Math.Max(1, e.Delta / SystemInformation.MouseWheelScrollDelta);
      int num2 = Math.Sign(e.Delta) * num1 * SystemInformation.MouseWheelScrollLines;
      handledMouseEventArgs.Handled = true;
      int num3 = this.scrollbar.Value - num2 * this.scrollbar.SmallChange;
      if (num3 > this.scrollbar.Maximum - this.scrollbar.LargeChange + 1)
        num3 = this.scrollbar.Maximum - this.scrollbar.LargeChange + 1;
      if (num3 < this.scrollbar.Minimum)
      {
        num3 = 0;
        handledMouseEventArgs.Handled = false;
      }
      else if (num3 > this.scrollbar.Maximum)
      {
        num3 = this.scrollbar.Maximum;
        handledMouseEventArgs.Handled = false;
      }
      this.scrollbar.Value = num3;
      this.InvalidateMeasure();
    }

    public virtual bool ScrollToItem(RadPageViewExplorerBarItem item)
    {
      if (this.layoutInfo == null)
        return false;
      RectangleF scrollbarClientRectangle = this.GetCorrectedScrollbarClientRectangle((RectangleF) this.BoundingRectangle);
      scrollbarClientRectangle.Height -= (float) (this.Header.BoundingRectangle.Height + this.Footer.BoundingRectangle.Height);
      this.UpdateScrollbarMetrics(scrollbarClientRectangle);
      RectangleF clientRectangle = this.GetClientRectangle((SizeF) this.Size);
      Padding ncMetrics = this.GetNCMetrics();
      clientRectangle.Y += (float) ncMetrics.Top;
      clientRectangle.X += (float) ncMetrics.Left;
      clientRectangle.Width -= (float) ncMetrics.Horizontal;
      clientRectangle.Height -= (float) ncMetrics.Vertical;
      RectangleF boundingRectangle = (RectangleF) item.BoundingRectangle;
      RectangleF rectangleF = RectangleF.Intersect(clientRectangle, boundingRectangle);
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          if ((double) rectangleF.Width == (double) boundingRectangle.Width)
            return false;
          break;
        case StackViewPosition.Top:
          if ((double) rectangleF.Height == (double) boundingRectangle.Height)
            return false;
          break;
      }
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          this.initialLayoutOffset += (int) ((double) clientRectangle.Left - (double) boundingRectangle.Left);
          break;
        case StackViewPosition.Top:
          this.initialLayoutOffset += (int) ((double) clientRectangle.Top - (double) boundingRectangle.Top);
          break;
      }
      this.initialLayoutOffset = this.CorrectLayoutOffset();
      this.scrollbar.Value = -this.initialLayoutOffset;
      this.InvalidateMeasure();
      return true;
    }

    protected override float GetInitialItemsOffset(RectangleF clientRect)
    {
      return (float) this.initialLayoutOffset;
    }

    private void OnScrollbar_Scroll(object sender, ScrollEventArgs e)
    {
      this.InvalidateMeasure();
    }

    private int GetOffsetValueAccordingToStackPosition()
    {
      if (this.layoutInfo == null)
        return 0;
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
        case StackViewPosition.Top:
          return -this.scrollbar.Value;
        case StackViewPosition.Right:
        case StackViewPosition.Bottom:
          return this.scrollbar.Value - (this.scrollbar.Maximum - this.scrollbar.LargeChange + 1);
        default:
          return 0;
      }
    }

    private void SetInitialScrollbarParameters(StackViewPosition stackPosition)
    {
      this.scrollbar.ScrollType = stackPosition == StackViewPosition.Top || stackPosition == StackViewPosition.Bottom ? ScrollType.Vertical : ScrollType.Horizontal;
      this.scrollbar.Visibility = ElementVisibility.Visible;
      this.ResetLayoutOffset(true);
      this.InvalidateMeasure();
    }

    protected virtual void UpdateAndArrangeScrollbar(RectangleF clientRect)
    {
      if (this.CheckShowScrollbar(clientRect))
      {
        if (this.scrollbar.Visibility != ElementVisibility.Visible)
          this.SetInitialScrollbarParameters(this.layoutInfo.position);
        this.ArrangeScrollbar(clientRect);
        this.initialLayoutOffset = this.GetOffsetValueAccordingToStackPosition();
        int num = this.CorrectLayoutOffset();
        if (num == this.initialLayoutOffset)
          return;
        this.initialLayoutOffset = num;
        this.ArrangeContentAndItems(clientRect);
      }
      else
      {
        if (this.scrollbar.Visibility == ElementVisibility.Collapsed)
          return;
        this.ResetLayoutOffset(false);
        this.scrollbar.Visibility = ElementVisibility.Collapsed;
        this.InvalidateMeasure();
      }
    }

    protected virtual RectangleF ArrangeScrollbar(RectangleF clientRect)
    {
      RectangleF finalRect = (RectangleF) Rectangle.Empty;
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          finalRect = new RectangleF(clientRect.Left, clientRect.Bottom, clientRect.Width, Math.Max((float) RadScrollBarElement.HorizontalScrollBarHeight, this.scrollbar.DesiredSize.Height));
          break;
        case StackViewPosition.Top:
          finalRect = this.RightToLeft ? new RectangleF(clientRect.Left - (float) RadScrollBarElement.VerticalScrollBarWidth, clientRect.Top, Math.Max((float) RadScrollBarElement.VerticalScrollBarWidth, this.scrollbar.DesiredSize.Width), clientRect.Height) : new RectangleF(clientRect.Right, clientRect.Top, Math.Max((float) RadScrollBarElement.VerticalScrollBarWidth, this.scrollbar.DesiredSize.Width), clientRect.Height);
          break;
      }
      this.scrollbar.Arrange(finalRect);
      this.UpdateScrollbarMetrics(clientRect);
      return clientRect;
    }

    protected internal override void OnContentBoundsChanged()
    {
      if (this.Owner == null)
        return;
      foreach (RadPageViewPage page in this.Owner.Pages)
      {
        if (page.IsContentVisible)
        {
          RadPageViewContentAreaElement contentAreaForItem = this.GetContentAreaForItem((RadPageViewItem) (page.Item as RadPageViewExplorerBarItem));
          page.Bounds = this.GetClientRectangleFromContentElement(contentAreaForItem);
        }
      }
      this.Owner.Update();
    }

    protected override bool IsChildElementExternal(RadElement element)
    {
      if (element != this.scrollbar)
        return base.IsChildElementExternal(element);
      return false;
    }

    internal override StackViewLayoutInfo CreateLayoutInfo(SizeF availableSize)
    {
      return (StackViewLayoutInfo) new ExplorerBarLayoutInfo(this, availableSize);
    }

    internal override SizeF GetContentSizeForItem(
      PageViewItemSizeInfo sizeInfo,
      RectangleF clientRect)
    {
      if (sizeInfo == null)
        return SizeF.Empty;
      ExplorerBarItemSizeInfo itemSizeInfo = sizeInfo as ExplorerBarItemSizeInfo;
      if (!itemSizeInfo.IsExpanded)
        return SizeF.Empty;
      SizeF empty = SizeF.Empty;
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          empty.Width = this.GetContentLength(itemSizeInfo, clientRect.Width);
          empty.Height = clientRect.Height;
          break;
        case StackViewPosition.Top:
          empty.Width = clientRect.Width;
          empty.Height = this.GetContentLength(itemSizeInfo, clientRect.Height);
          break;
      }
      return empty;
    }

    internal virtual float GetContentLength(
      ExplorerBarItemSizeInfo itemSizeInfo,
      float availableLength)
    {
      switch (this.ContentSizeMode)
      {
        case ExplorerBarContentSizeMode.FixedLength:
          return (float) itemSizeInfo.item.PageLength;
        case ExplorerBarContentSizeMode.AutoSizeToBestFit:
          if (this.Owner == null)
          {
            switch (this.layoutInfo.position)
            {
              case StackViewPosition.Left:
                return itemSizeInfo.contentSize.Width;
              case StackViewPosition.Top:
                return itemSizeInfo.contentSize.Height;
            }
          }
          else
          {
            Size itemContentMetrics = this.GetItemContentMetrics(itemSizeInfo);
            switch (this.layoutInfo.position)
            {
              case StackViewPosition.Left:
                return (float) itemContentMetrics.Width;
              case StackViewPosition.Top:
                return (float) itemContentMetrics.Height;
            }
          }
        case ExplorerBarContentSizeMode.EqualLength:
          float layoutLength = this.layoutInfo.layoutLength;
          ExplorerBarLayoutInfo layoutInfo = this.layoutInfo as ExplorerBarLayoutInfo;
          return (availableLength - layoutLength) / (float) layoutInfo.expandedItemsCount;
      }
      return availableLength;
    }

    internal virtual Size GetItemContentMetrics(ExplorerBarItemSizeInfo itemSizeInfo)
    {
      RadPageViewPage page = itemSizeInfo.item.Page;
      RadPageViewExplorerBarItem viewExplorerBarItem = itemSizeInfo.item as RadPageViewExplorerBarItem;
      Padding padding = viewExplorerBarItem.AssociatedContentAreaElement.Padding;
      Padding borderThickness = LightVisualElement.GetBorderThickness((LightVisualElement) viewExplorerBarItem.AssociatedContentAreaElement, false);
      Size minSize = viewExplorerBarItem.AssociatedContentAreaElement.MinSize;
      if (page != null)
      {
        foreach (Control control in (ArrangedElementCollection) page.Controls)
        {
          if (minSize.Width < control.Left)
            minSize.Width = control.Left;
          if (minSize.Height < control.Bottom)
            minSize.Height = control.Bottom;
        }
      }
      minSize.Width += padding.Horizontal + borderThickness.Horizontal;
      minSize.Height += padding.Vertical + borderThickness.Vertical;
      return minSize;
    }

    internal override float GetItemOffset(
      RectangleF clientRect,
      PageViewItemSizeInfo sizeInfo,
      float proposedOffset)
    {
      int itemSpacing = this.layoutInfo.itemSpacing;
      ExplorerBarItemSizeInfo explorerBarItemSizeInfo = sizeInfo as ExplorerBarItemSizeInfo;
      SizeF contentSize = explorerBarItemSizeInfo.contentSize;
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Top:
        case StackViewPosition.Bottom:
          proposedOffset += contentSize.Height;
          break;
        default:
          proposedOffset += contentSize.Width;
          break;
      }
      if (explorerBarItemSizeInfo.IsExpanded)
        proposedOffset -= (float) itemSpacing;
      (this.layoutInfo as ExplorerBarLayoutInfo).fullLayoutLength = (float) (int) proposedOffset;
      return proposedOffset;
    }

    private RectangleF GetCorrectedScrollbarClientRectangle(RectangleF currentRectangle)
    {
      if (this.scrollbar.Visibility != ElementVisibility.Visible)
        return currentRectangle;
      if (this.scrollbar.ScrollType == ScrollType.Horizontal)
      {
        currentRectangle.Height -= Math.Max((float) RadScrollBarElement.HorizontalScrollBarHeight, this.scrollbar.DesiredSize.Height);
      }
      else
      {
        currentRectangle.Width -= Math.Max((float) RadScrollBarElement.VerticalScrollBarWidth, this.scrollbar.DesiredSize.Width);
        if (this.RightToLeft)
          currentRectangle.X += Math.Max((float) RadScrollBarElement.VerticalScrollBarWidth, this.scrollbar.DesiredSize.Width);
      }
      return currentRectangle;
    }

    protected override SizeF MeasureItems(SizeF availableSize)
    {
      availableSize = this.GetCorrectedScrollbarClientRectangle(new RectangleF((PointF) Point.Empty, availableSize)).Size;
      SizeF sizeF = base.MeasureItems(availableSize);
      if (this.Owner == null)
        availableSize.Height -= sizeF.Height;
      foreach (ExplorerBarItemSizeInfo explorerBarItemSizeInfo in this.layoutInfo.items)
      {
        if (this.Owner == null)
        {
          availableSize.Height = float.PositiveInfinity;
          RadPageViewExplorerBarItem viewExplorerBarItem = (RadPageViewExplorerBarItem) explorerBarItemSizeInfo.item;
          if (viewExplorerBarItem.AssociatedContentAreaElement != null && viewExplorerBarItem.IsExpanded)
          {
            viewExplorerBarItem.AssociatedContentAreaElement.Measure(availableSize);
            if (this.StackPosition == StackViewPosition.Top)
            {
              sizeF.Height += viewExplorerBarItem.AssociatedContentAreaElement.DesiredSize.Height;
              sizeF.Width = Math.Max(sizeF.Width, viewExplorerBarItem.AssociatedContentAreaElement.DesiredSize.Width);
            }
            else
            {
              sizeF.Width += viewExplorerBarItem.AssociatedContentAreaElement.DesiredSize.Width;
              sizeF.Height = Math.Max(sizeF.Height, viewExplorerBarItem.AssociatedContentAreaElement.DesiredSize.Height);
            }
            explorerBarItemSizeInfo.contentSize = viewExplorerBarItem.AssociatedContentAreaElement.DesiredSize;
          }
        }
        else
          explorerBarItemSizeInfo.contentSize = this.GetContentSizeForItem((PageViewItemSizeInfo) explorerBarItemSizeInfo, new RectangleF((PointF) Point.Empty, availableSize));
      }
      return sizeF;
    }

    protected override SizeF MeasureContentArea(ref SizeF availableSize)
    {
      this.scrollbar.Measure(availableSize);
      return SizeF.Empty;
    }

    protected virtual bool CheckShowScrollbar(RectangleF clientRect)
    {
      ExplorerBarLayoutInfo layoutInfo = this.layoutInfo as ExplorerBarLayoutInfo;
      switch (layoutInfo.position)
      {
        case StackViewPosition.Left:
          if ((double) clientRect.Width < (double) layoutInfo.fullLayoutLength)
            return true;
          break;
        case StackViewPosition.Top:
          if ((double) clientRect.Height < (double) layoutInfo.fullLayoutLength)
            return true;
          break;
      }
      return false;
    }

    public override RectangleF GetItemsRect()
    {
      RectangleF clientRectangle = this.GetClientRectangle(true, (SizeF) this.Size);
      clientRectangle.Y += this.Header.DesiredSize.Height + (float) this.Header.Margin.Vertical;
      clientRectangle.Height -= this.Header.DesiredSize.Height + this.Footer.DesiredSize.Height + (float) this.Header.Margin.Vertical + (float) this.Footer.Margin.Vertical;
      return clientRectangle;
    }

    protected virtual void UpdateScrollbarMetrics(RectangleF clientRect)
    {
      ExplorerBarLayoutInfo layoutInfo = this.layoutInfo as ExplorerBarLayoutInfo;
      switch (layoutInfo.position)
      {
        case StackViewPosition.Left:
          this.scrollbar.LargeChange = (int) clientRect.Width;
          break;
        case StackViewPosition.Top:
          this.scrollbar.LargeChange = (int) clientRect.Height;
          break;
      }
      this.scrollbar.Minimum = 0;
      if ((double) layoutInfo.fullLayoutLength == (double) this.scrollbar.Maximum)
        return;
      this.scrollbar.Maximum = (int) layoutInfo.fullLayoutLength;
    }

    private void ResetLayoutOffset(bool scrollbarVisible)
    {
      if (!scrollbarVisible)
        this.initialLayoutOffset = 0;
      else
        this.initialLayoutOffset = this.GetOffsetValueAccordingToStackPosition();
    }

    private int CorrectLayoutOffset()
    {
      int num1 = Math.Max(Math.Min(this.scrollbar.Maximum - this.scrollbar.LargeChange + 1, this.scrollbar.Maximum), this.scrollbar.Minimum);
      int num2 = -1;
      int num3 = this.initialLayoutOffset;
      if (num3 * num2 > num1)
      {
        this.scrollbar.Value = num1;
        num3 = num2 * num1;
      }
      else if (num3 * num2 < 0)
        num3 = 0;
      return num3;
    }

    protected override RectangleF PerformArrange(RectangleF clientRect)
    {
      clientRect = this.GetCorrectedScrollbarClientRectangle(clientRect);
      this.ArrangeContentAndItems(clientRect);
      this.UpdateAndArrangeScrollbar(clientRect);
      return clientRect;
    }

    private void ArrangeContentAndItems(RectangleF clientRect)
    {
      this.ArrangeItems(clientRect);
      this.ArrangeContent(clientRect);
    }

    protected override RectangleF ArrangeContent(RectangleF clientRect)
    {
      List<ContentAreaLayoutInfo> contentAreaLayoutInfos = this.GetContentAreaLayoutInfos(clientRect);
      ExplorerBarLayoutInfo layoutInfo = this.layoutInfo as ExplorerBarLayoutInfo;
      layoutInfo.fullLayoutLength = layoutInfo.layoutLength;
      foreach (ContentAreaLayoutInfo contentAreaLayoutInfo in contentAreaLayoutInfos)
      {
        (contentAreaLayoutInfo.AssociatedItem as RadPageViewExplorerBarItem).AssociatedContentAreaElement.Arrange(contentAreaLayoutInfo.ContentAreaRectangle);
        switch (layoutInfo.position)
        {
          case StackViewPosition.Left:
            layoutInfo.fullLayoutLength += contentAreaLayoutInfo.ContentAreaRectangle.Width;
            continue;
          case StackViewPosition.Top:
            layoutInfo.fullLayoutLength += contentAreaLayoutInfo.ContentAreaRectangle.Height;
            continue;
          default:
            continue;
        }
      }
      this.OnContentBoundsChanged();
      if (layoutInfo.position == StackViewPosition.Top)
      {
        if (this.Header.Visibility == ElementVisibility.Visible)
          layoutInfo.fullLayoutLength += (float) this.Header.Margin.Bottom;
        if (this.Footer.Visibility == ElementVisibility.Visible)
          layoutInfo.fullLayoutLength += (float) this.Footer.Margin.Top;
      }
      layoutInfo.fullLayoutLength -= (float) (layoutInfo.expandedItemsCount * layoutInfo.itemSpacing);
      return clientRect;
    }

    protected List<ContentAreaLayoutInfo> GetContentAreaLayoutInfos(
      RectangleF clientRect)
    {
      RectangleF empty = (RectangleF) Rectangle.Empty;
      ExplorerBarLayoutInfo layoutInfo = this.layoutInfo as ExplorerBarLayoutInfo;
      List<ContentAreaLayoutInfo> contentAreaLayoutInfoList = new List<ContentAreaLayoutInfo>();
      foreach (ExplorerBarItemSizeInfo explorerBarItemSizeInfo in layoutInfo.items)
      {
        RectangleF contentRectangle = this.GetContentWithSelectedContentRectangle((PageViewItemSizeInfo) explorerBarItemSizeInfo, clientRect);
        contentAreaLayoutInfoList.Add(new ContentAreaLayoutInfo()
        {
          AssociatedItem = explorerBarItemSizeInfo.item,
          ContentAreaRectangle = contentRectangle
        });
      }
      return contentAreaLayoutInfoList;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.ResetLayoutOffset(this.scrollbar.Visibility == ElementVisibility.Visible);
      return base.ArrangeOverride(finalSize);
    }

    protected internal override void ProcessKeyDown(KeyEventArgs e)
    {
      base.ProcessKeyDown(e);
      if (!(this.SelectedItem is RadPageViewExplorerBarItem))
        return;
      RadPageViewExplorerBarItem selectedItem = this.SelectedItem as RadPageViewExplorerBarItem;
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          if (e.KeyData == Keys.Up)
          {
            selectedItem.IsExpanded = true;
            break;
          }
          if (e.KeyData != Keys.Down)
            break;
          selectedItem.IsExpanded = false;
          break;
        case StackViewPosition.Top:
          if (e.KeyData == Keys.Right)
          {
            selectedItem.IsExpanded = true;
            break;
          }
          if (e.KeyData != Keys.Left)
            break;
          selectedItem.IsExpanded = false;
          break;
      }
    }

    public virtual void ScrollToControl(Control control)
    {
      RadPageViewPage parent = control.Parent as RadPageViewPage;
      if (parent == null || !(parent.Item is RadPageViewExplorerBarItem))
        return;
      ((RadPageViewExplorerBarItem) parent.Item).IsExpanded = true;
      this.ProcessAutoScroll(control, true);
      control.Focus();
    }

    protected internal virtual void ProcessAutoScroll(Control activeControl, bool ensureVisibility)
    {
      if (activeControl == null)
        return;
      int parentOffset = this.FindParentOffset(activeControl);
      if (parentOffset > 5 && (parentOffset < this.Owner.Bounds.Height && !ensureVisibility))
        return;
      int num = parentOffset + this.Scrollbar.Value;
      if (num < this.Scrollbar.Minimum || num > this.scrollbar.Maximum)
        return;
      this.Scrollbar.Value = num - 5;
      this.InvalidateMeasure(true);
      this.UpdateLayout();
      this.Owner.Invalidate();
    }

    protected virtual int FindParentOffset(Control activeControl)
    {
      Control control = activeControl;
      int num = 0;
      do
      {
        num += control.Location.Y;
        control = control.Parent;
      }
      while (control != null && this.ElementTree.Control != control);
      return num;
    }

    protected virtual Control FindParentControl(Control activeControl)
    {
      for (Control parent = activeControl.Parent; parent != null; parent = parent.Parent)
      {
        if (parent != null && !(parent is RadPageViewPage))
          return parent;
      }
      return activeControl;
    }

    protected virtual RadPageViewItem FindParentItem(Control activeControl)
    {
      for (Control parent = activeControl.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is RadPageViewPage)
          return (parent as RadPageViewPage).Item;
      }
      return (RadPageViewItem) null;
    }

    protected virtual RadPageViewStackItem PageWithFocusedControl()
    {
      foreach (RadPageViewStackItem pageViewStackItem in (IEnumerable<RadPageViewItem>) this.Items)
      {
        if (this.GetFocusedControl(pageViewStackItem.Page.Controls) != null)
          return pageViewStackItem;
      }
      return (RadPageViewStackItem) null;
    }

    protected virtual Control GetFocusedControl(Control.ControlCollection controls)
    {
      foreach (Control control in (ArrangedElementCollection) controls)
      {
        if (control.ContainsFocus)
          return control;
        Control focusedControl = this.GetFocusedControl(control.Controls);
        if (focusedControl != null)
          return focusedControl;
      }
      return (Control) null;
    }

    protected virtual Control GetNextFocusedControl(
      Control.ControlCollection controls,
      int currentTabIndex,
      int step)
    {
      Control control1 = (Control) null;
      Control control2 = (Control) null;
      foreach (Control control3 in (ArrangedElementCollection) controls)
      {
        if (control3.TabIndex == 0)
          control1 = control3;
        if (control2 == null || control2.TabIndex < control3.TabIndex)
          control2 = control3;
        if (control3.TabIndex - step == currentTabIndex)
          return control3;
      }
      if (step <= 0)
        return control2;
      return control1;
    }

    protected internal override bool IsNextKey(Keys key)
    {
      switch (this.layoutInfo.position)
      {
        case StackViewPosition.Left:
          return key == Keys.Left;
        case StackViewPosition.Top:
          return key == Keys.Up;
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
          return key == Keys.Down;
        default:
          return base.IsPreviousKey(key);
      }
    }

    protected override bool EnsureItemVisibleCore(RadPageViewItem item)
    {
      return this.ScrollToItem(item as RadPageViewExplorerBarItem);
    }

    protected internal virtual bool OnItemExpanding(RadPageViewExplorerBarItem item)
    {
      if (this.Owner == null)
        return false;
      RadPageViewCancelEventArgs e = new RadPageViewCancelEventArgs(item.Page);
      this.Owner.OnPageExpanding(e);
      return e.Cancel;
    }

    protected internal virtual bool OnItemCollapsing(RadPageViewExplorerBarItem item)
    {
      if (this.Owner == null)
        return false;
      RadPageViewCancelEventArgs e = new RadPageViewCancelEventArgs(item.Page);
      this.Owner.OnPageCollapsing(e);
      return e.Cancel;
    }

    internal void ExpandItem(RadPageViewExplorerBarItem item)
    {
      this.SynchronizeItemContentWithExpandedState(item);
      if (this.Owner != null)
        this.Owner.OnPageExpanded(new RadPageViewEventArgs(item.Page));
      this.OnExpandedChanged(new RadPageViewExpandedChangedEventArgs(item));
      if (!item.IsExpanded)
        return;
      this.EnsureVisible(item);
    }

    protected virtual void EnsureVisible(RadPageViewExplorerBarItem item)
    {
      this.UpdateLayout();
      RectangleF clientRectangle = this.GetClientRectangle((SizeF) this.Size);
      Padding ncMetrics = this.GetNCMetrics();
      clientRectangle.Y += (float) ncMetrics.Top;
      clientRectangle.X += (float) ncMetrics.Left;
      clientRectangle.Width -= (float) ncMetrics.Horizontal;
      clientRectangle.Height -= (float) ncMetrics.Vertical;
      RectangleF boundingRectangle = (RectangleF) item.BoundingRectangle;
      if (item.Page != null)
        boundingRectangle.Height += (float) item.Page.Height;
      RectangleF.Intersect(clientRectangle, boundingRectangle);
      this.initialLayoutOffset += (int) ((double) clientRectangle.Top - (double) boundingRectangle.Top);
      this.initialLayoutOffset = this.CorrectLayoutOffset();
      this.Scrollbar.Value = -this.initialLayoutOffset;
      this.InvalidateMeasure(true);
      this.UpdateLayout();
    }

    internal void CollapseItem(RadPageViewExplorerBarItem item)
    {
      this.SynchronizeItemContentWithExpandedState(item);
      if (this.Owner != null)
        this.Owner.OnPageCollapsed(new RadPageViewEventArgs(item.Page));
      this.OnExpandedChanged(new RadPageViewExpandedChangedEventArgs(item));
    }

    protected internal override void OnItemMouseUp(RadPageViewItem sender, MouseEventArgs e)
    {
      base.OnItemMouseUp(sender, e);
      if (e.Button != MouseButtons.Left)
        return;
      RadPageViewExplorerBarItem viewExplorerBarItem = sender as RadPageViewExplorerBarItem;
      viewExplorerBarItem.IsExpanded = !viewExplorerBarItem.IsExpanded;
    }

    private void SynchronizeItemContentWithExpandedState(RadPageViewExplorerBarItem item)
    {
      RadPageViewContentAreaElement contentAreaElement = item.AssociatedContentAreaElement;
      if (this.Owner != null && this.Owner.IsHandleCreated)
        this.Owner.BeginInvoke((Delegate) new RadPageViewExplorerBarElement.PageViewExplorerBarDelegate(this.SetPageVisibility), (object) item);
      else
        this.SetPageVisibility(item);
    }

    private void SetPageVisibility(RadPageViewExplorerBarItem item)
    {
      if (this.Owner == null && item.AssociatedContentAreaElement != null)
      {
        if (item.IsExpanded)
          item.AssociatedContentAreaElement.Visibility = ElementVisibility.Visible;
        else
          item.AssociatedContentAreaElement.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        if (item.Page == null || this.Owner == null || !this.Owner.IsHandleCreated)
          return;
        item.Page.Visible = item.IsExpanded;
      }
    }

    protected internal override void OnPageAdded(RadPageViewEventArgs e)
    {
      RadPageViewItem radPageViewItem1 = this.CreateItem();
      RadPageViewItem radPageViewItem2 = this.OnItemCreating(new RadPageViewItemCreatingEventArgs(e.Page));
      if (radPageViewItem2 != null)
      {
        if (!radPageViewItem1.GetType().IsAssignableFrom(radPageViewItem2.GetType()))
          throw new ArgumentException(string.Format("The type of the custom item must inherit from {0}", (object) radPageViewItem1.GetType()), "Item");
        radPageViewItem1 = radPageViewItem2;
      }
      if (this.Items.Count == 0)
      {
        ((RadPageViewExplorerBarItem) radPageViewItem1).AssociatedContentAreaElement = this.ContentArea as RadPageViewContentAreaElement;
      }
      else
      {
        RadPageViewContentAreaElement contentAreaElement = new RadPageViewContentAreaElement();
        contentAreaElement.Visibility = ElementVisibility.Collapsed;
        ((RadPageViewExplorerBarItem) radPageViewItem1).AssociatedContentAreaElement = contentAreaElement;
        contentAreaElement.Owner = (RadPageViewElement) this;
      }
      radPageViewItem1.Attach(e.Page);
      this.AddItemCore(radPageViewItem1);
      e.Page.Dock = DockStyle.None;
      if (this.Items.Count <= 1)
        return;
      this.Children.Add((RadElement) ((RadPageViewExplorerBarItem) radPageViewItem1).AssociatedContentAreaElement);
    }

    protected override void AddItemCore(RadPageViewItem item)
    {
      base.AddItemCore(item);
      RadPageView owner = this.Owner;
    }

    protected internal override void OnPageRemoved(RadPageViewEventArgs e)
    {
      RadPageViewExplorerBarItem viewExplorerBarItem = e.Page.Item as RadPageViewExplorerBarItem;
      if (this.Items.Count > 1)
      {
        viewExplorerBarItem.AssociatedContentAreaElement.Dispose();
        viewExplorerBarItem.AssociatedContentAreaElement = (RadPageViewContentAreaElement) null;
      }
      else
        this.ContentArea.Visibility = ElementVisibility.Collapsed;
      base.OnPageRemoved(e);
    }

    protected override RadPageViewItem CreateItem()
    {
      return (RadPageViewItem) new RadPageViewExplorerBarItem();
    }

    protected override void SetItemIndex(int currentIndex, int newIndex)
    {
      RadPageViewExplorerBarItem viewExplorerBarItem1 = this.Items[currentIndex] as RadPageViewExplorerBarItem;
      RadPageViewExplorerBarItem viewExplorerBarItem2 = this.Items[newIndex] as RadPageViewExplorerBarItem;
      RadPageViewItem radPageViewItem = this.Items[currentIndex];
      RadPageViewReadonlyCollection<RadPageViewItem> items = this.Items as RadPageViewReadonlyCollection<RadPageViewItem>;
      items.RemoveAt(currentIndex);
      items.Insert(newIndex, radPageViewItem);
      this.ItemsParent.Children.Move(this.ItemsParent.Children.IndexOf((RadElement) viewExplorerBarItem1), this.ItemsParent.Children.IndexOf((RadElement) viewExplorerBarItem2));
      this.ItemsParent.InvalidateMeasure();
    }

    internal void RefreshNCArea()
    {
      if (this.Owner == null || !this.Owner.IsHandleCreated)
        return;
      Telerik.WinControls.NativeMethods.SetWindowPos(new HandleRef((object) null, this.Owner.Handle), new HandleRef((object) null, IntPtr.Zero), 0, 0, 0, 0, 547);
    }

    protected internal override bool EnableNCModification
    {
      get
      {
        return true;
      }
    }

    protected internal override bool EnableNCPainting
    {
      get
      {
        return true;
      }
    }

    protected internal override Padding GetNCMetrics()
    {
      if (!this.allowNCCALCSIZEProcessing)
        return this.ncMetrics;
      return this.ncMetrics = this.CalculateNCMetrics();
    }

    private Padding CalculateNCMetrics()
    {
      return Padding.Add(this.GetBorderThickness(false), new Padding(0, (int) this.Header.DesiredSize.Height + (this.Header.Visibility == ElementVisibility.Visible ? this.Header.Margin.Top : 0), 0, (int) this.Footer.DesiredSize.Height + (this.Footer.Visibility == ElementVisibility.Visible ? this.Footer.Margin.Bottom : 0)));
    }

    protected override void PaintBorder(IGraphics graphics, float angle, SizeF scale)
    {
    }

    private Bitmap PaintTopNCArea(Padding ncMetrics)
    {
      int width = this.DesiredSize.ToSize().Width;
      SizeF scaleTransform = this.ScaleTransform;
      float angleTransform = this.AngleTransform;
      int num = width;
      int top = ncMetrics.Top;
      if (num <= 0 || top <= 0)
        return (Bitmap) null;
      Padding borderThickness = this.GetBorderThickness(false);
      Bitmap bitmap = new Bitmap(width, ncMetrics.Top);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      using (graphics)
      {
        Padding margin = this.Header.Margin;
        RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
        {
          Bitmap asBitmap = this.Header.GetAsBitmap((Brush) solidBrush, angleTransform, scaleTransform);
          if (asBitmap != null)
            graphics.DrawImage((Image) asBitmap, new Point(borderThickness.Left + margin.Left, borderThickness.Top + margin.Top));
        }
        this.BorderPrimitiveImpl.PaintBorder((IGraphics) radGdiGraphics, angleTransform, scaleTransform);
      }
      return bitmap;
    }

    private Bitmap PaintBottomNCArea(Padding ncMetrics)
    {
      Size size = this.DesiredSize.ToSize();
      int width = size.Width;
      int height = size.Height;
      SizeF scaleTransform = this.ScaleTransform;
      float angleTransform = this.AngleTransform;
      int num = width;
      int bottom = ncMetrics.Bottom;
      if (num <= 0 || bottom <= 0)
        return (Bitmap) null;
      Padding borderThickness = this.GetBorderThickness(false);
      Bitmap bitmap = new Bitmap(width, ncMetrics.Bottom);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      using (graphics)
      {
        Padding margin = this.Footer.Margin;
        RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
        {
          Bitmap asBitmap = this.Footer.GetAsBitmap((Brush) solidBrush, angleTransform, scaleTransform);
          if (asBitmap != null)
            graphics.DrawImage((Image) asBitmap, new Point(borderThickness.Left + margin.Left, -margin.Bottom - borderThickness.Bottom));
        }
        radGdiGraphics.TranslateTransform(0, -(height - ncMetrics.Bottom));
        this.BorderPrimitiveImpl.PaintBorder((IGraphics) radGdiGraphics, angleTransform, scaleTransform);
      }
      return bitmap;
    }

    private Bitmap PaintLeftNCArea(Padding ncMetrics)
    {
      int height = this.DesiredSize.ToSize().Height;
      SizeF scaleTransform = this.ScaleTransform;
      float angleTransform = this.AngleTransform;
      int left = ncMetrics.Left;
      int num = height - ncMetrics.Vertical;
      if (left <= 0 || num <= 0)
        return (Bitmap) null;
      Bitmap bitmap = new Bitmap(ncMetrics.Left, height - ncMetrics.Vertical);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      using (graphics)
      {
        RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
        radGdiGraphics.TranslateTransform(0, -ncMetrics.Top);
        this.BorderPrimitiveImpl.PaintBorder((IGraphics) radGdiGraphics, angleTransform, scaleTransform);
      }
      return bitmap;
    }

    private Bitmap PaintRightNCArea(Padding ncMetrics)
    {
      Size size = this.DesiredSize.ToSize();
      int width = size.Width;
      int height = size.Height;
      SizeF scaleTransform = this.ScaleTransform;
      float angleTransform = this.AngleTransform;
      int right = ncMetrics.Right;
      int num = height - ncMetrics.Vertical;
      if (right <= 0 || num <= 0)
        return (Bitmap) null;
      Bitmap bitmap = new Bitmap(ncMetrics.Right, height - ncMetrics.Vertical);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      using (graphics)
      {
        RadGdiGraphics radGdiGraphics = new RadGdiGraphics(graphics);
        radGdiGraphics.TranslateTransform(-(width - ncMetrics.Right), -ncMetrics.Top);
        this.BorderPrimitiveImpl.PaintBorder((IGraphics) radGdiGraphics, angleTransform, scaleTransform);
      }
      return bitmap;
    }

    protected internal override void OnNCPaint(Graphics g)
    {
      base.OnNCPaint(g);
      Padding ncMetrics = this.CalculateNCMetrics();
      Size size = this.DesiredSize.ToSize();
      int width = size.Width;
      int height = size.Height;
      if (width <= 0 || height <= 0)
        return;
      Bitmap bitmap1 = this.PaintTopNCArea(ncMetrics);
      if (bitmap1 != null)
      {
        g.DrawImage((Image) bitmap1, new Rectangle(Point.Empty, bitmap1.Size));
        bitmap1.Dispose();
      }
      Bitmap bitmap2 = this.PaintLeftNCArea(ncMetrics);
      if (bitmap2 != null)
      {
        g.DrawImage((Image) bitmap2, new Rectangle(new Point(0, ncMetrics.Top), bitmap2.Size));
        bitmap2.Dispose();
      }
      Bitmap bitmap3 = this.PaintRightNCArea(ncMetrics);
      if (bitmap3 != null)
      {
        g.DrawImage((Image) bitmap3, new Rectangle(new Point(width - ncMetrics.Right, ncMetrics.Top), bitmap3.Size));
        bitmap3.Dispose();
      }
      Bitmap bitmap4 = this.PaintBottomNCArea(ncMetrics);
      if (bitmap4 == null)
        return;
      g.DrawImage((Image) bitmap4, new Rectangle(new Point(0, height - ncMetrics.Bottom), bitmap4.Size));
      bitmap4.Dispose();
    }

    protected override Padding GetBorderThickness(bool checkDrawBorder)
    {
      if (checkDrawBorder)
        return Padding.Empty;
      return base.GetBorderThickness(checkDrawBorder);
    }

    protected override RectangleF GetClientRectangle(SizeF finalSize)
    {
      Padding ncMetrics = this.CalculateNCMetrics();
      RectangleF clientRectangle = base.GetClientRectangle(finalSize);
      if (this.Owner != null)
      {
        clientRectangle.X -= (float) ncMetrics.Left;
        clientRectangle.Y -= (float) ncMetrics.Top;
      }
      return clientRectangle;
    }

    private void OnNCElement_PropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      RadElementPropertyMetadata metadata = e.Metadata as RadElementPropertyMetadata;
      if (metadata == null || !metadata.AffectsDisplay && e.Property != RadElement.BoundsProperty)
        return;
      this.allowNCCALCSIZEProcessing = e.Property == RadElement.BoundsProperty;
      this.RefreshNCArea();
      this.allowNCCALCSIZEProcessing = false;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadPageViewStackElement.StackPositionProperty)
        this.SetInitialScrollbarParameters((StackViewPosition) e.NewValue);
      base.OnPropertyChanged(e);
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      base.OnBoundsChanged(e);
      if (!(this.ncMetrics != this.CalculateNCMetrics()))
        return;
      this.allowNCCALCSIZEProcessing = true;
      this.RefreshNCArea();
      this.allowNCCALCSIZEProcessing = false;
    }

    protected virtual void OnExpandedChanged(RadPageViewExpandedChangedEventArgs e)
    {
      EventHandler<RadPageViewExpandedChangedEventArgs> expandedChanged = this.ExpandedChanged;
      if (expandedChanged == null)
        return;
      expandedChanged((object) this, e);
    }

    public delegate void PageViewExplorerBarDelegate(RadPageViewExplorerBarItem item);
  }
}
