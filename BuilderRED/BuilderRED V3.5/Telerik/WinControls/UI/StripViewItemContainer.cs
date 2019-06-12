// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StripViewItemContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class StripViewItemContainer : RadPageViewElementBase
  {
    private StripViewItemLayout itemLayout;
    private StripViewButtonsPanel buttonsPanel;

    static StripViewItemContainer()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new StripViewElementStateManager(), typeof (StripViewItemContainer));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = false;
      this.ClipDrawing = false;
    }

    protected override void CreateChildElements()
    {
      this.itemLayout = new StripViewItemLayout();
      this.Children.Add((RadElement) this.itemLayout);
      this.buttonsPanel = new StripViewButtonsPanel();
      this.Children.Add((RadElement) this.buttonsPanel);
    }

    [Browsable(false)]
    public StripViewButtonsPanel ButtonsPanel
    {
      get
      {
        return this.buttonsPanel;
      }
    }

    [Browsable(false)]
    public StripViewItemLayout ItemLayout
    {
      get
      {
        return this.itemLayout;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF measured = this.ApplyClientOffset(this.PerformMeasure(this.GetClientRectangle(availableSize).Size));
      if (this.StretchHorizontally && !float.IsPositiveInfinity(availableSize.Width))
        measured.Width = availableSize.Width;
      if (this.StretchVertically && !float.IsPositiveInfinity(availableSize.Height))
        measured.Height = availableSize.Height;
      return this.ApplyMinMaxSize(measured);
    }

    private SizeF PerformMeasure(SizeF availableSize)
    {
      this.buttonsPanel.Measure(availableSize);
      SizeF desiredSize1 = this.buttonsPanel.DesiredSize;
      StripViewAlignment stripViewAlignment = (StripViewAlignment) this.GetValue(RadPageViewStripElement.StripAlignmentProperty);
      switch (stripViewAlignment)
      {
        case StripViewAlignment.Top:
        case StripViewAlignment.Bottom:
          availableSize.Width -= desiredSize1.Width + (float) this.buttonsPanel.Margin.Horizontal;
          this.buttonsPanel.Measure(availableSize);
          break;
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          availableSize.Height -= desiredSize1.Height + (float) this.buttonsPanel.Margin.Vertical;
          break;
      }
      this.itemLayout.Measure(availableSize);
      SizeF desiredSize2 = this.itemLayout.DesiredSize;
      SizeF empty = SizeF.Empty;
      switch (stripViewAlignment)
      {
        case StripViewAlignment.Top:
        case StripViewAlignment.Bottom:
          empty.Width = desiredSize1.Width + (float) this.buttonsPanel.Margin.Horizontal + desiredSize2.Width + (float) this.itemLayout.Margin.Horizontal;
          empty.Height = Math.Max(desiredSize1.Height + (float) this.buttonsPanel.Margin.Vertical, desiredSize2.Height + (float) this.itemLayout.Margin.Vertical);
          break;
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          empty.Width = Math.Max(desiredSize1.Width + (float) this.buttonsPanel.Margin.Horizontal, desiredSize2.Width + (float) this.itemLayout.Margin.Horizontal);
          empty.Height = desiredSize1.Height + (float) this.buttonsPanel.Margin.Vertical + desiredSize2.Height + (float) this.itemLayout.Margin.Vertical;
          break;
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.itemLayout.Arrange(this.ArrangeButtons(finalSize));
      return finalSize;
    }

    private RectangleF ArrangeButtons(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      SizeF desiredSize = this.buttonsPanel.DesiredSize;
      StripViewAlignment stripViewAlignment = (StripViewAlignment) this.GetValue(RadPageViewStripElement.StripAlignmentProperty);
      RectangleF finalRect = RectangleF.Empty;
      switch (stripViewAlignment)
      {
        case StripViewAlignment.Top:
        case StripViewAlignment.Bottom:
          finalRect = new RectangleF(clientRectangle.Right - desiredSize.Width - (float) this.buttonsPanel.Margin.Right, clientRectangle.Y, desiredSize.Width, clientRectangle.Height);
          clientRectangle.Width = finalRect.Left - clientRectangle.Left - (float) this.buttonsPanel.Margin.Left;
          if (this.RightToLeft)
          {
            clientRectangle.X = clientRectangle.X - (float) this.Padding.Left + (float) this.Padding.Right;
            finalRect.X = clientRectangle.X;
            clientRectangle.X += finalRect.Width;
            break;
          }
          break;
        case StripViewAlignment.Right:
        case StripViewAlignment.Left:
          float y = clientRectangle.Bottom - desiredSize.Height - (float) this.buttonsPanel.Margin.Bottom;
          finalRect = new RectangleF(clientRectangle.X, y, clientRectangle.Width, desiredSize.Height);
          clientRectangle.Height = finalRect.Top - clientRectangle.Top - (float) this.buttonsPanel.Margin.Top;
          break;
      }
      this.buttonsPanel.Arrange(finalRect);
      return clientRectangle;
    }

    internal void UpdateButtonsEnabledState()
    {
      if (this.itemLayout.LayoutInfo == null)
      {
        foreach (RadElement child in this.buttonsPanel.Children)
          child.Enabled = false;
      }
      else
      {
        int nonSystemItemCount = this.itemLayout.LayoutInfo.nonSystemItemCount;
        this.buttonsPanel.CloseButton.Enabled = nonSystemItemCount > 0;
        this.buttonsPanel.ItemListButton.Enabled = nonSystemItemCount > 0;
        this.buttonsPanel.ScrollLeftButton.Enabled = this.itemLayout.CanScroll(StripViewButtons.LeftScroll);
        this.buttonsPanel.ScrollRightButton.Enabled = this.itemLayout.CanScroll(StripViewButtons.RightScroll);
      }
    }

    internal void OnStripButtonClicked(RadPageViewStripButtonElement button)
    {
      RadPageViewStripElement ancestor = this.FindAncestor<RadPageViewStripElement>();
      if (ancestor == null || ancestor.SelectedItem != null && ancestor.SelectedItem.Page != null && ancestor.SelectedItem.Page.HasFocusedChildControl())
        return;
      switch ((StripViewButtons) button.Tag)
      {
        case StripViewButtons.LeftScroll:
          this.itemLayout.Scroll(StripViewButtons.LeftScroll);
          break;
        case StripViewButtons.RightScroll:
          this.itemLayout.Scroll(StripViewButtons.RightScroll);
          break;
        case StripViewButtons.Close:
          ancestor.CloseItem(ancestor.SelectedItem);
          break;
        case StripViewButtons.ItemList:
          this.DisplayItemListMenu(ancestor);
          break;
      }
    }

    private void DisplayItemListMenu(RadPageViewStripElement view)
    {
      RadPageViewElementBase itemListButton = (RadPageViewElementBase) this.buttonsPanel.ItemListButton;
      HorizontalPopupAlignment hAlign = this.RightToLeft ? HorizontalPopupAlignment.RightToRight : HorizontalPopupAlignment.LeftToLeft;
      VerticalPopupAlignment vAlign = VerticalPopupAlignment.TopToBottom;
      switch (view.StripAlignment)
      {
        case StripViewAlignment.Right:
          hAlign = HorizontalPopupAlignment.RightToLeft;
          vAlign = VerticalPopupAlignment.TopToTop;
          break;
        case StripViewAlignment.Left:
          hAlign = HorizontalPopupAlignment.LeftToRight;
          vAlign = VerticalPopupAlignment.TopToTop;
          break;
      }
      view.DisplayItemListMenu(itemListButton, hAlign, vAlign);
    }
  }
}
