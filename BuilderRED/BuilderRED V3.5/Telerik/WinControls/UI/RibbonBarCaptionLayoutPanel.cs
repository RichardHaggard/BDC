// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonBarCaptionLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class RibbonBarCaptionLayoutPanel : LayoutPanel
  {
    public static RadProperty CaptionTextProperty = RadProperty.Register("CaptionText", typeof (bool), typeof (RibbonBarCaptionLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private bool showTabGroups = true;
    private const int RIGHTMOSTGROUP_VISIBILITY_TRESHOLD = 15;
    private float rightEmptySpace;
    private bool isShrinking;
    private RadElement captionTextElement;
    private ContextualTabGroup rightMostGroup;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = true;
    }

    public RadElement CaptionTextElement
    {
      get
      {
        this.InitializeElements();
        return this.captionTextElement;
      }
    }

    public RadRibbonBarElement RibbonBarElement
    {
      get
      {
        return this.FindAncestor<RadRibbonBarElement>();
      }
    }

    private void InitializeElements()
    {
      if (this.captionTextElement != null)
        return;
      foreach (RadElement child in this.Children)
      {
        if ((bool) child.GetValue(RibbonBarCaptionLayoutPanel.CaptionTextProperty))
          this.captionTextElement = child;
      }
    }

    private Point TransformXToClient(Point point)
    {
      float num = 0.0f;
      float x1;
      if (!this.RibbonBarElement.QuickAccessToolbarBelowRibbon)
        x1 = this.RightToLeft ? (float) point.X - this.RibbonBarElement.RibbonCaption.SystemButtons.DesiredSize.Width : (float) (point.X - this.RibbonBarElement.QuickAccessToolBar.ControlBoundingRectangle.Right);
      else if (!this.RightToLeft)
      {
        x1 = (float) point.X;
      }
      else
      {
        double x2 = (double) point.X;
        double width = (double) this.RibbonBarElement.RibbonCaption.SystemButtons.DesiredSize.Width;
        x1 = num = (float) (x2 - width);
      }
      return Point.Round(new PointF(x1, (float) point.Y));
    }

    private float GetLeftCaptionSpace(SizeF availableSize)
    {
      ContextualTabGroup leftMostGroup = this.GetLeftMostGroup(this.IsDesignMode);
      if (!this.RightToLeft)
      {
        if (leftMostGroup.TabItems.Count > 0)
          return (float) this.TransformXToClient(leftMostGroup.TabItems[0].ControlBoundingRectangle.Location).X;
        return (float) new Rectangle(leftMostGroup.BoundingRectangle.Location, Size.Add(leftMostGroup.BoundingRectangle.Size, leftMostGroup.Margin.Size)).X;
      }
      if (leftMostGroup.TabItems.Count > 0)
      {
        int index = leftMostGroup.TabItems.Count - 1;
        float groupWidth = this.CalculateGroupWidth(leftMostGroup);
        return availableSize.Width - ((float) this.TransformXToClient(leftMostGroup.TabItems[index].ControlBoundingRectangle.Location).X + groupWidth);
      }
      Rectangle rectangle = new Rectangle(leftMostGroup.BoundingRectangle.Location, Size.Add(leftMostGroup.BoundingRectangle.Size, leftMostGroup.Margin.Size));
      return availableSize.Width - ((float) rectangle.X + leftMostGroup.DesiredSize.Width);
    }

    private float GetRightCaptionSpace(SizeF availableSize)
    {
      ContextualTabGroup rightMostGroup = this.GetRightMostGroup(this.IsDesignMode);
      if (!this.RightToLeft)
      {
        if (rightMostGroup.TabItems.Count <= 0)
          return availableSize.Width - (float) (rightMostGroup.BoundingRectangle.X + rightMostGroup.BoundingRectangle.Width);
        float groupWidth = this.CalculateGroupWidth(rightMostGroup);
        return availableSize.Width - ((float) this.TransformXToClient(rightMostGroup.TabItems[0].ControlBoundingRectangle.Location).X + groupWidth);
      }
      if (rightMostGroup.TabItems.Count <= 0)
        return (float) rightMostGroup.BoundingRectangle.X;
      int index = rightMostGroup.TabItems.Count - 1;
      return (float) this.TransformXToClient(rightMostGroup.TabItems[index].ControlBoundingRectangle.Location).X;
    }

    private bool ShouldResetContextTabs()
    {
      int num1 = this.IsAddNewTabItemInTabStrip() ? this.RibbonBarElement.TabStripElement.Items.Count - 2 : this.RibbonBarElement.TabStripElement.Items.Count - 1;
      for (int index1 = this.RibbonBarElement.ContextualTabGroups.Count - 1; index1 > -1; --index1)
      {
        ContextualTabGroup contextualTabGroup = this.RibbonBarElement.ContextualTabGroups[index1] as ContextualTabGroup;
        if (contextualTabGroup != null && contextualTabGroup.TabItems.Count > 0)
        {
          for (int index2 = contextualTabGroup.TabItems.Count - 1; index2 > -1; --index2)
          {
            int num2 = this.RibbonBarElement.TabStripElement.Items.IndexOf(contextualTabGroup.TabItems[index2] as RadPageViewItem);
            if (num1 - num2 != 0)
              return true;
            --num1;
          }
        }
      }
      return false;
    }

    private bool IsAddNewTabItemInTabStrip()
    {
      for (int index = 0; index < this.RibbonBarElement.TabStripElement.Items.Count; ++index)
      {
        if ((bool) (this.RibbonBarElement.TabStripElement.Items[index] as RibbonTab).GetValue(RadItem.IsAddNewItemProperty))
          return true;
      }
      return false;
    }

    private int GetEmptyGroupsCount()
    {
      int num = 0;
      for (int index = 0; index < this.RibbonBarElement.ContextualTabGroups.Count; ++index)
      {
        ContextualTabGroup contextualTabGroup = this.RibbonBarElement.ContextualTabGroups[index] as ContextualTabGroup;
        if (contextualTabGroup != null && contextualTabGroup.TabItems.Count == 0)
          ++num;
      }
      return num;
    }

    private void ResetLayoutContext(SizeF availableSize)
    {
      if (this.ShouldResetContextTabs())
        this.ResetAssociatedTabItems();
      else if (this.RibbonBarElement.ContextualTabGroups.Count == 0 || this.RibbonBarElement.ContextualTabGroups.Count == 1 && this.IsDesignMode)
        this.ResetTabPositions();
      int index = this.RibbonBarElement.TabStripElement.Items.Count - 1;
      if (index == -1)
        return;
      RadPageViewItem radPageViewItem = this.RibbonBarElement.TabStripElement.Items[index];
      float x = (float) this.TransformXToClient(radPageViewItem.ControlBoundingRectangle.Location).X;
      this.rightEmptySpace = x;
      if (this.RightToLeft)
        return;
      float num = x + (float) radPageViewItem.ControlBoundingRectangle.Width;
      this.rightEmptySpace = availableSize.Width - num;
    }

    internal ContextualTabGroup GetLeftMostGroup(bool considerEmpty)
    {
      ContextualTabGroup contextualTabGroup1 = (ContextualTabGroup) null;
      int num = -1;
      for (int index = 0; index < this.RibbonBarElement.ContextualTabGroups.Count; ++index)
      {
        ContextualTabGroup contextualTabGroup2 = this.RibbonBarElement.ContextualTabGroups[index] as ContextualTabGroup;
        if (contextualTabGroup2 != null)
        {
          if (contextualTabGroup2.TabItems.Count == 0 && considerEmpty)
          {
            if (contextualTabGroup1 == null)
            {
              contextualTabGroup1 = contextualTabGroup2;
              num = this.RibbonBarElement.ContextualTabGroups.IndexOf((RadItem) contextualTabGroup1);
            }
            else if (index < num)
            {
              contextualTabGroup1 = contextualTabGroup2;
              num = this.RibbonBarElement.ContextualTabGroups.IndexOf((RadItem) contextualTabGroup1);
            }
          }
          else if (contextualTabGroup2.TabItems.Count > 0 && contextualTabGroup2.Visibility == ElementVisibility.Visible)
          {
            if (contextualTabGroup1 == null)
            {
              contextualTabGroup1 = contextualTabGroup2;
              num = this.RibbonBarElement.ContextualTabGroups.IndexOf((RadItem) contextualTabGroup1);
            }
            else
            {
              if (contextualTabGroup1.TabItems.Count == 0)
              {
                contextualTabGroup1 = contextualTabGroup2;
                num = this.RibbonBarElement.ContextualTabGroups.IndexOf((RadItem) contextualTabGroup1);
              }
              if (index < num && contextualTabGroup1.TabItems.Count > 0)
              {
                contextualTabGroup1 = contextualTabGroup2;
                num = this.RibbonBarElement.ContextualTabGroups.IndexOf((RadItem) contextualTabGroup1);
              }
            }
          }
        }
      }
      return contextualTabGroup1;
    }

    internal ContextualTabGroup GetRightMostGroup(bool considerEmpty)
    {
      ContextualTabGroup contextualTabGroup1 = (ContextualTabGroup) null;
      int num = -1;
      for (int index = 0; index < this.RibbonBarElement.ContextualTabGroups.Count; ++index)
      {
        ContextualTabGroup contextualTabGroup2 = this.RibbonBarElement.ContextualTabGroups[index] as ContextualTabGroup;
        if (contextualTabGroup2 != null)
        {
          if (contextualTabGroup2.TabItems.Count == 0 && considerEmpty)
          {
            if (contextualTabGroup1 == null)
            {
              contextualTabGroup1 = contextualTabGroup2;
              num = this.RibbonBarElement.ContextualTabGroups.IndexOf((RadItem) contextualTabGroup1);
            }
            else if (index > num)
            {
              contextualTabGroup1 = contextualTabGroup2;
              num = this.RibbonBarElement.ContextualTabGroups.IndexOf((RadItem) contextualTabGroup1);
            }
          }
          else if (contextualTabGroup2.TabItems.Count > 0 && contextualTabGroup2.Visibility == ElementVisibility.Visible)
          {
            if (contextualTabGroup1 == null)
            {
              contextualTabGroup1 = contextualTabGroup2;
              num = this.RibbonBarElement.ContextualTabGroups.IndexOf((RadItem) contextualTabGroup1);
            }
            else if (index > num && contextualTabGroup1.TabItems.Count > 0)
            {
              contextualTabGroup1 = contextualTabGroup2;
              num = this.RibbonBarElement.ContextualTabGroups.IndexOf((RadItem) contextualTabGroup1);
            }
          }
        }
      }
      return contextualTabGroup1;
    }

    private void ResetAssociatedTabItems()
    {
      RibbonTab selectedItem = this.RibbonBarElement.TabStripElement.SelectedItem as RibbonTab;
      List<RadItem> radItemList = new List<RadItem>();
      this.ResetTabPositions();
      for (int index1 = 0; index1 < this.RibbonBarElement.ContextualTabGroups.Count; ++index1)
      {
        ContextualTabGroup contextualTabGroup = this.RibbonBarElement.ContextualTabGroups[index1] as ContextualTabGroup;
        for (int index2 = 0; index2 < contextualTabGroup.TabItems.Count; ++index2)
        {
          RadPageViewItem tabItem = contextualTabGroup.TabItems[index2] as RadPageViewItem;
          for (int index3 = 0; index3 < this.RibbonBarElement.TabStripElement.Items.Count; ++index3)
          {
            if (((RibbonTab) this.RibbonBarElement.TabStripElement.Items[index3]).obsoleteTab == tabItem)
            {
              tabItem = this.RibbonBarElement.TabStripElement.Items[index3];
              contextualTabGroup.TabItems[index2] = (RadItem) tabItem;
            }
          }
          if (this.RibbonBarElement.TabStripElement.Items.Contains(tabItem) && !(bool) tabItem.GetValue(RadItem.IsAddNewItemProperty) && tabItem.Parent != null)
          {
            this.RibbonBarElement.TabStripElement.RemoveItem(tabItem);
            if (this.IsDesignMode)
            {
              if (this.RibbonBarElement.TabStripElement.Items.Count == 0)
                this.RibbonBarElement.TabStripElement.InsertItem(this.RibbonBarElement.TabStripElement.Items.Count, tabItem);
              else
                this.RibbonBarElement.TabStripElement.InsertItem(this.RibbonBarElement.TabStripElement.Items.Count - 1, tabItem);
            }
            else
              this.RibbonBarElement.TabStripElement.InsertItem(this.RibbonBarElement.TabStripElement.Items.Count, tabItem);
          }
        }
      }
      this.RibbonBarElement.TabStripElement.SelectedItem = (RadPageViewItem) selectedItem;
    }

    private void ResetTabPositions()
    {
      for (int index1 = 0; index1 < this.RibbonBarElement.CommandTabs.Count; ++index1)
      {
        int index2 = this.RibbonBarElement.TabStripElement.Items.IndexOf((RadPageViewItem) (this.RibbonBarElement.TabStripElement.TabItems[index1] as RibbonTab));
        if (index2 != -1 && index2 != index1 && index1 < this.RibbonBarElement.TabStripElement.Items.Count)
        {
          RibbonTab ribbonTab = this.RibbonBarElement.TabStripElement.Items[index2] as RibbonTab;
          bool isSelected = ribbonTab.IsSelected;
          this.RibbonBarElement.TabStripElement.RemoveItem((RadPageViewItem) ribbonTab);
          this.RibbonBarElement.TabStripElement.InsertItem(index1, (RadPageViewItem) ribbonTab);
          if (isSelected)
            this.RibbonBarElement.TabStripElement.SelectItem((RadPageViewItem) ribbonTab);
        }
      }
    }

    private float CalculateGroupWidth(ContextualTabGroup tabGroup)
    {
      float num = 0.0f;
      if (tabGroup.TabItems.Count > 0)
      {
        for (int index1 = 0; index1 < tabGroup.TabItems.Count; ++index1)
        {
          RadPageViewItem tabItem = tabGroup.TabItems[index1] as RadPageViewItem;
          for (int index2 = 0; index2 < this.RibbonBarElement.TabStripElement.Items.Count; ++index2)
          {
            if ((RibbonTab) this.RibbonBarElement.TabStripElement.Items[index2] == tabItem)
            {
              tabItem = this.RibbonBarElement.TabStripElement.Items[index2];
              tabGroup.TabItems[index1] = (RadItem) tabItem;
            }
          }
          if (tabItem != null && tabItem.Visibility != ElementVisibility.Collapsed)
          {
            Rectangle rectangle = new Rectangle(tabItem.BoundingRectangle.Location, Size.Add(tabItem.BoundingRectangle.Size, tabItem.Margin.Size));
            num += (float) rectangle.Width;
          }
        }
      }
      return num;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.InitializeElements();
      foreach (RadElement child in this.Children)
      {
        if (!(child is ContextualTabGroup) && child != this.CaptionTextElement)
          child.Measure(availableSize);
      }
      this.ResetLayoutContext(availableSize);
      SizeF empty = SizeF.Empty;
      SizeF sizeF1 = this.ContextGroupsMeasure(availableSize);
      empty.Width += sizeF1.Width;
      empty.Height = sizeF1.Height;
      SizeF sizeF2 = this.CaptionMeasure(availableSize);
      empty.Width += sizeF2.Width;
      empty.Height = Math.Max(sizeF1.Height, sizeF2.Height);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.InitializeElements();
      foreach (RadElement child in this.Children)
      {
        if (!(child is ContextualTabGroup) && child != this.CaptionTextElement)
          child.Arrange(new RectangleF((PointF) new Point(0, 0), finalSize));
      }
      this.ContextGroupsArrange(finalSize);
      this.CaptionArrange(finalSize);
      return finalSize;
    }

    private SizeF CaptionMeasure(SizeF availableSize)
    {
      RadItemCollection contextualTabGroups = (RadItemCollection) this.RibbonBarElement.ContextualTabGroups;
      SizeF empty = (SizeF) Size.Empty;
      if (contextualTabGroups.Count > 0)
      {
        if (this.GetLeftMostGroup(this.IsDesignMode) == null || this.GetRightMostGroup(this.IsDesignMode) == null)
          this.captionTextElement.Measure(availableSize);
        else if (!this.showTabGroups)
        {
          this.captionTextElement.Measure(availableSize);
        }
        else
        {
          float leftCaptionSpace = this.GetLeftCaptionSpace(availableSize);
          float rightCaptionSpace = this.GetRightCaptionSpace(availableSize);
          if ((double) leftCaptionSpace > (double) rightCaptionSpace)
          {
            empty.Width = leftCaptionSpace;
            empty.Height = availableSize.Height;
            this.captionTextElement.Measure(empty);
          }
          else
          {
            empty.Width = rightCaptionSpace;
            empty.Height = availableSize.Height;
            this.captionTextElement.Measure(empty);
          }
        }
      }
      else
        this.captionTextElement.Measure(availableSize);
      return this.captionTextElement.DesiredSize;
    }

    private void CaptionArrange(SizeF finalSize)
    {
      RadItemCollection contextualTabGroups = (RadItemCollection) this.RibbonBarElement.ContextualTabGroups;
      RectangleF finalRect = new RectangleF();
      finalRect.Width = this.captionTextElement.DesiredSize.Width;
      finalRect.Height = this.captionTextElement.DesiredSize.Height;
      if (contextualTabGroups.Count > 0 && this.showTabGroups)
      {
        ContextualTabGroup leftMostGroup;
        ContextualTabGroup rightMostGroup;
        if ((leftMostGroup = this.GetLeftMostGroup(this.IsDesignMode)) == null || (rightMostGroup = this.GetRightMostGroup(this.IsDesignMode)) == null)
        {
          this.captionTextElement.Arrange(new RectangleF((float) (((double) finalSize.Width - (double) this.captionTextElement.DesiredSize.Width) / 2.0), (float) (((double) finalSize.Height - (double) this.captionTextElement.DesiredSize.Height) / 2.0), this.captionTextElement.DesiredSize.Width, this.captionTextElement.DesiredSize.Height));
        }
        else
        {
          float leftCaptionSpace = this.GetLeftCaptionSpace(finalSize);
          float rightCaptionSpace = this.GetRightCaptionSpace(finalSize);
          finalRect.Y = (float) (((double) finalSize.Height - (double) this.captionTextElement.DesiredSize.Height) / 2.0);
          if ((double) leftCaptionSpace > (double) rightCaptionSpace)
          {
            if (!this.RightToLeft)
            {
              finalRect.X = (float) ((double) leftCaptionSpace / 2.0 - (double) this.captionTextElement.DesiredSize.Width / 2.0);
              this.captionTextElement.Arrange(finalRect);
            }
            else
            {
              Rectangle rectangle = new Rectangle(leftMostGroup.BoundingRectangle.Location, Size.Add(leftMostGroup.BoundingRectangle.Size, leftMostGroup.Margin.Size));
              finalRect.X = (float) ((double) rectangle.X + (double) leftMostGroup.DesiredSize.Width + (double) leftCaptionSpace / 2.0 - (double) this.captionTextElement.DesiredSize.Width / 2.0);
            }
            this.captionTextElement.Arrange(finalRect);
          }
          else
          {
            if (!this.RightToLeft)
            {
              float num = (float) new Rectangle(rightMostGroup.BoundingRectangle.Location, Size.Add(rightMostGroup.BoundingRectangle.Size, rightMostGroup.Margin.Size)).X + rightMostGroup.DesiredSize.Width;
              finalRect.X = (float) ((double) num + (double) rightCaptionSpace / 2.0 - (double) this.captionTextElement.DesiredSize.Width / 2.0);
            }
            else
              finalRect.X = (float) ((double) rightCaptionSpace / 2.0 - (double) this.captionTextElement.DesiredSize.Width / 2.0);
            this.captionTextElement.Arrange(finalRect);
          }
        }
      }
      else
      {
        finalRect.X = (float) (((double) finalSize.Width - (double) this.captionTextElement.DesiredSize.Width) / 2.0);
        finalRect.Y = (float) (((double) finalSize.Height - (double) this.captionTextElement.DesiredSize.Height) / 2.0);
        this.captionTextElement.Arrange(finalRect);
      }
    }

    private float GetCalculatedGroupXCoord(ContextualTabGroup tabGroup)
    {
      float x;
      if (!this.RightToLeft)
      {
        x = (float) this.TransformXToClient(tabGroup.TabItems[0].ControlBoundingRectangle.Location).X;
      }
      else
      {
        int index = tabGroup.TabItems.Count - 1;
        x = (float) this.TransformXToClient(tabGroup.TabItems[index].ControlBoundingRectangle.Location).X;
      }
      return x;
    }

    private bool ShouldShrinkGroup(
      ContextualTabGroup tabGroup,
      float desiredGroupWidth,
      SizeF availableSize)
    {
      float calculatedGroupXcoord = this.GetCalculatedGroupXCoord(tabGroup);
      return this.RightToLeft ? (double) calculatedGroupXcoord <= 0.0 : (double) calculatedGroupXcoord + (double) desiredGroupWidth - (double) availableSize.Width > 0.0;
    }

    private SizeF PerformMeasureWithShrink(SizeF availableSize, ContextualTabGroup tabGroup)
    {
      RadRibbonBarElement ribbonBarElement = this.RibbonBarElement;
      float calculatedGroupXcoord = this.GetCalculatedGroupXCoord(tabGroup);
      float groupWidth = this.CalculateGroupWidth(tabGroup);
      if (this.ShouldShrinkGroup(tabGroup, groupWidth, availableSize) && !this.isShrinking)
      {
        this.isShrinking = true;
        float width;
        if (!this.RightToLeft)
        {
          width = availableSize.Width - calculatedGroupXcoord;
        }
        else
        {
          float x = (float) (tabGroup.TabItems[tabGroup.TabItems.Count - 1] as RadPageViewItem).ControlBoundingRectangle.X;
          float right = (float) this.RibbonBarElement.RibbonCaption.SystemButtons.ControlBoundingRectangle.Right;
          width = (double) x - (double) right >= 0.0 ? groupWidth : groupWidth - Math.Abs(x - right);
        }
        if ((double) width < 15.0)
        {
          width = 0.0f;
          this.showTabGroups = false;
        }
        else if ((double) width > 25.0)
          this.showTabGroups = true;
        return new SizeF(width, availableSize.Height);
      }
      if (this.isShrinking)
      {
        float width;
        if (!this.RightToLeft)
        {
          width = availableSize.Width - calculatedGroupXcoord;
        }
        else
        {
          float x = (float) (tabGroup.TabItems[tabGroup.TabItems.Count - 1] as RadPageViewItem).ControlBoundingRectangle.X;
          float right = (float) this.RibbonBarElement.RibbonCaption.SystemButtons.ControlBoundingRectangle.Right;
          width = (double) x - (double) right >= 0.0 ? groupWidth : groupWidth - Math.Abs(x - right);
        }
        if ((double) width < 15.0)
        {
          width = 0.0f;
          this.showTabGroups = false;
        }
        else if ((double) width > 25.0)
          this.showTabGroups = true;
        if ((double) width >= (double) groupWidth)
        {
          width = groupWidth;
          this.showTabGroups = true;
          this.isShrinking = false;
        }
        return new SizeF(width, availableSize.Height);
      }
      if (!this.showTabGroups)
        this.showTabGroups = true;
      return new SizeF(groupWidth, availableSize.Height);
    }

    private RectangleF PerformArrangeWithShrink(
      SizeF finalSize,
      ContextualTabGroup tabGroup)
    {
      RadPageViewItem tabItem = tabGroup.TabItems[tabGroup.TabItems.Count - 1] as RadPageViewItem;
      RectangleF rectangleF = new RectangleF();
      rectangleF.Height = finalSize.Height;
      rectangleF.Y = 0.0f;
      if (this.isShrinking)
      {
        rectangleF.Width = tabGroup.DesiredSize.Width;
        rectangleF.X = 0.0f;
      }
      else
      {
        rectangleF.Width = tabGroup.DesiredSize.Width;
        rectangleF.X = (float) this.TransformXToClient(tabItem.ControlBoundingRectangle.Location).X;
      }
      return rectangleF;
    }

    private SizeF ContextGroupsMeasure(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      SizeF availableSize1 = new SizeF();
      ContextualTabGroup rightMostGroup = this.GetRightMostGroup(this.IsDesignMode);
      if (rightMostGroup != this.rightMostGroup)
      {
        this.rightMostGroup = rightMostGroup;
        this.showTabGroups = true;
      }
      int emptyGroupsCount = this.GetEmptyGroupsCount();
      for (int index = this.RibbonBarElement.ContextualTabGroups.Count - 1; index > -1; --index)
      {
        ContextualTabGroup contextualTabGroup = this.RibbonBarElement.ContextualTabGroups[index] as ContextualTabGroup;
        if (contextualTabGroup != null)
        {
          if (contextualTabGroup.TabItems.Count == 0)
          {
            if (this.IsDesignMode)
            {
              float width = 100f;
              if (emptyGroupsCount > 0 && (double) this.rightEmptySpace / (double) emptyGroupsCount < 100.0)
                width = this.rightEmptySpace / (float) emptyGroupsCount;
              if ((double) width < 20.0)
                width = 0.0f;
              availableSize1 = new SizeF(width, availableSize.Height);
              empty.Width += Math.Max(contextualTabGroup.DesiredSize.Width, (float) contextualTabGroup.MinSize.Width);
              empty.Height = Math.Max(contextualTabGroup.DesiredSize.Height, (float) contextualTabGroup.MinSize.Height);
            }
            else
            {
              availableSize1 = (SizeF) Size.Empty;
              empty.Width += Math.Max(contextualTabGroup.DesiredSize.Width, (float) contextualTabGroup.MinSize.Width);
              empty.Height = Math.Max(contextualTabGroup.DesiredSize.Height, (float) contextualTabGroup.MinSize.Height);
            }
          }
          else if (contextualTabGroup == rightMostGroup)
          {
            SizeF sizeF = this.PerformMeasureWithShrink(availableSize, contextualTabGroup);
            availableSize1 = this.showTabGroups ? sizeF : (SizeF) Size.Empty;
            empty.Width += contextualTabGroup.DesiredSize.Width;
            empty.Height = contextualTabGroup.DesiredSize.Height;
          }
          else if (this.showTabGroups)
          {
            availableSize1 = new SizeF(this.CalculateGroupWidth(contextualTabGroup), availableSize.Height);
            empty.Width += Math.Max(contextualTabGroup.DesiredSize.Width, (float) contextualTabGroup.MinSize.Width);
            empty.Height = Math.Max(contextualTabGroup.DesiredSize.Height, (float) contextualTabGroup.MinSize.Height);
          }
          else
          {
            availableSize1 = SizeF.Empty;
            empty.Width += Math.Max(contextualTabGroup.DesiredSize.Width, (float) contextualTabGroup.MinSize.Width);
          }
        }
        if (contextualTabGroup.Visibility == ElementVisibility.Visible)
          contextualTabGroup.Measure(availableSize1);
        else
          contextualTabGroup.Measure(SizeF.Empty);
      }
      return empty;
    }

    private Rectangle GetFullBoundingRect(RadElement element)
    {
      return new Rectangle(element.BoundingRectangle.Location, Size.Add(element.Size, element.Margin.Size));
    }

    private void ContextGroupsArrange(SizeF endSize)
    {
      float num = 0.0f;
      ContextualTabGroup rightMostGroup = this.GetRightMostGroup(this.IsDesignMode);
      for (int index = 0; index < this.RibbonBarElement.ContextualTabGroups.Count; ++index)
      {
        ContextualTabGroup contextualTabGroup = this.RibbonBarElement.ContextualTabGroups[index] as ContextualTabGroup;
        float y = 0.0f;
        if (contextualTabGroup != null)
        {
          if (contextualTabGroup.TabItems.Count == 0)
          {
            if (this.IsDesignMode)
            {
              if (this.RibbonBarElement.TabStripElement.Items.Count > 0)
              {
                RadPageViewItem radPageViewItem = this.RibbonBarElement.TabStripElement.Items[this.RibbonBarElement.TabStripElement.Items.Count - 1];
                Point point = this.TransformXToClient(radPageViewItem.ControlBoundingRectangle.Location);
                if (!this.RightToLeft)
                {
                  float x = num + (float) point.X + (float) this.GetFullBoundingRect((RadElement) radPageViewItem).Width;
                  contextualTabGroup.Arrange(new RectangleF(x, y, Math.Max(contextualTabGroup.DesiredSize.Width, (float) contextualTabGroup.MinSize.Width), contextualTabGroup.DesiredSize.Height));
                  num += Math.Max(contextualTabGroup.DesiredSize.Width, (float) contextualTabGroup.MinSize.Width);
                }
                else
                {
                  float x1 = (float) point.X;
                  num += Math.Max(contextualTabGroup.DesiredSize.Width, (float) contextualTabGroup.MinSize.Width);
                  float x2 = x1 - num;
                  contextualTabGroup.Arrange(new RectangleF(x2, y, Math.Max(contextualTabGroup.DesiredSize.Width, (float) contextualTabGroup.MinSize.Width), endSize.Height));
                }
              }
            }
          }
          else
          {
            RadPageViewItem tabItem1 = contextualTabGroup.TabItems[0] as RadPageViewItem;
            RadPageViewItem tabItem2 = contextualTabGroup.TabItems[contextualTabGroup.TabItems.Count - 1] as RadPageViewItem;
            if (tabItem1 != null && tabItem2 != null)
            {
              if (contextualTabGroup == rightMostGroup && this.RightToLeft)
              {
                contextualTabGroup.Arrange(this.PerformArrangeWithShrink(endSize, contextualTabGroup));
              }
              else
              {
                Point point = this.RightToLeft ? this.TransformXToClient(tabItem2.ControlBoundingRectangle.Location) : this.TransformXToClient(tabItem1.ControlBoundingRectangle.Location);
                contextualTabGroup.Arrange(new RectangleF((float) point.X, y, Math.Max(contextualTabGroup.DesiredSize.Width, (float) contextualTabGroup.MinSize.Width), endSize.Height));
              }
            }
          }
        }
      }
    }
  }
}
