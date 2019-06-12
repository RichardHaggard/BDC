// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardListViewVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class CardListViewVisualItem : IconListViewVisualItem
  {
    private CardViewContainerElement layoutContainer;
    private CardViewItem editingItem;
    private RadScrollBarElement horizontalScrollBar;
    private RadScrollBarElement verticalScrollBar;
    private int containerVersionNumber;
    private int synchronizationPropertiesVersionNumber;

    protected override void CreateChildElements()
    {
      this.containerVersionNumber = -1;
      this.synchronizationPropertiesVersionNumber = -1;
      base.CreateChildElements();
      this.DrawText = false;
      this.DrawImage = false;
      this.ClipDrawing = true;
      this.Padding = new Padding(10);
      this.layoutContainer = this.CreateLayoutConatiner();
      this.Children.Add((RadElement) this.layoutContainer);
      this.horizontalScrollBar = this.CreateScrollBarElement();
      this.horizontalScrollBar.ScrollType = ScrollType.Horizontal;
      this.horizontalScrollBar.Minimum = 0;
      this.horizontalScrollBar.ZIndex = 1000;
      this.horizontalScrollBar.ThemeRole = "RadScrollBarElement";
      this.horizontalScrollBar.MinSize = new Size(0, RadScrollBarElement.HorizontalScrollBarHeight);
      this.horizontalScrollBar.ScrollTimerDelay = 1;
      this.horizontalScrollBar.Visibility = ElementVisibility.Collapsed;
      this.horizontalScrollBar.ValueChanged += new EventHandler(this.HorizontalScrollBar_ValueChanged);
      this.Children.Add((RadElement) this.horizontalScrollBar);
      this.verticalScrollBar = this.CreateScrollBarElement();
      this.verticalScrollBar.ScrollType = ScrollType.Vertical;
      this.verticalScrollBar.Minimum = 0;
      this.verticalScrollBar.ZIndex = 1000;
      this.verticalScrollBar.ThemeRole = "RadScrollBarElement";
      this.verticalScrollBar.MinSize = new Size(RadScrollBarElement.VerticalScrollBarWidth, 0);
      this.verticalScrollBar.ScrollTimerDelay = 1;
      this.verticalScrollBar.Visibility = ElementVisibility.Collapsed;
      this.verticalScrollBar.ValueChanged += new EventHandler(this.VerticalScrollBar_ValueChanged);
      this.Children.Add((RadElement) this.verticalScrollBar);
    }

    protected virtual CardViewContainerElement CreateLayoutConatiner()
    {
      return new CardViewContainerElement();
    }

    protected virtual RadScrollBarElement CreateScrollBarElement()
    {
      return new RadScrollBarElement();
    }

    public CardViewContainerElement CardContainer
    {
      get
      {
        return this.layoutContainer;
      }
    }

    public RadScrollBarElement HorizontalScrollBar
    {
      get
      {
        return this.horizontalScrollBar;
      }
    }

    public RadScrollBarElement VerticalScrollBar
    {
      get
      {
        return this.verticalScrollBar;
      }
    }

    protected internal virtual CardViewItem EditingItem
    {
      get
      {
        return this.editingItem;
      }
      set
      {
        this.editingItem = value;
      }
    }

    protected internal virtual int ContainerVersionNumber
    {
      get
      {
        return this.containerVersionNumber;
      }
      set
      {
        this.containerVersionNumber = value;
      }
    }

    protected internal virtual int SynchronizationPropertiesVersionNumber
    {
      get
      {
        return this.synchronizationPropertiesVersionNumber;
      }
      set
      {
        this.synchronizationPropertiesVersionNumber = value;
      }
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (CardListViewVisualItem);
      }
    }

    public override bool IsCompatible(ListViewDataItem data, object context)
    {
      return !(data is ListViewDataItemGroup) && data.Owner is RadCardViewElement;
    }

    public override void AddEditor(IInputEditor editor)
    {
      if (editor == null || this.EditingItem == null || this.EditingItem.EditorItem == null)
        return;
      this.EditingItem.EditorItem.AddEditor(editor);
      this.Data.Owner.ViewElement.ViewElement.UpdateItems();
      this.UpdateLayout();
    }

    public override void RemoveEditor(IInputEditor editor)
    {
      if (this.EditingItem == null || this.EditingItem.EditorItem == null)
        return;
      this.EditingItem.EditorItem.RemoveEditor();
      this.EditingItem = (CardViewItem) null;
      this.Synchronize();
    }

    public virtual void UpdateScrollbars(SizeF desiredSize)
    {
      if (this.layoutContainer.LayoutTree == null || this.layoutContainer.LayoutTree.Root == null)
        return;
      this.verticalScrollBar.LargeChange = Math.Max(0, (int) desiredSize.Height - (this.horizontalScrollBar.Visibility == ElementVisibility.Visible ? this.horizontalScrollBar.Size.Height : 0));
      this.verticalScrollBar.Maximum = (int) this.layoutContainer.LayoutTree.Root.Bounds.Height;
      this.verticalScrollBar.SmallChange = 20;
      this.horizontalScrollBar.LargeChange = Math.Max(0, (int) desiredSize.Width - (this.verticalScrollBar.Visibility == ElementVisibility.Visible ? this.verticalScrollBar.Size.Width : 0));
      this.horizontalScrollBar.Maximum = (int) this.layoutContainer.LayoutTree.Root.Bounds.Width;
      this.horizontalScrollBar.SmallChange = 20;
      this.verticalScrollBar.Visibility = this.verticalScrollBar.LargeChange < this.verticalScrollBar.Maximum ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      this.horizontalScrollBar.Visibility = this.horizontalScrollBar.LargeChange < this.horizontalScrollBar.Maximum ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      this.verticalScrollBar.Value = this.verticalScrollBar.Visibility == ElementVisibility.Visible ? Math.Min(this.verticalScrollBar.Value, this.verticalScrollBar.Maximum - this.verticalScrollBar.LargeChange + 1) : 0;
      this.horizontalScrollBar.Value = this.horizontalScrollBar.Visibility == ElementVisibility.Visible ? Math.Min(this.horizontalScrollBar.Value, this.horizontalScrollBar.Maximum - this.horizontalScrollBar.LargeChange + 1) : 0;
    }

    protected override RadItem GetEditorElement(IValueEditor editor)
    {
      if (this.EditingItem != null && this.EditingItem.EditorItem != null)
      {
        BaseInputEditor editor1 = this.EditingItem.EditorItem.Editor as BaseInputEditor;
        if (editor1 != null)
          return editor1.EditorElement as RadItem;
      }
      return editor as RadItem;
    }

    protected override void SynchronizeProperties()
    {
      base.SynchronizeProperties();
      CardListViewElement viewElement = this.Data.Owner.ViewElement as CardListViewElement;
      if (viewElement == null)
        return;
      viewElement.CompareContainers(this.layoutContainer, this);
      this.UpdateGroupItemsExpandedState(viewElement);
      List<LayoutControlItemBase> allChildItems = viewElement.GetAllChildItems((RadItemCollection) this.CardContainer.Items);
      this.SynchronizeCardViewItems(viewElement, allChildItems);
      foreach (LayoutControlItemBase layoutControlItemBase in allChildItems)
        ((RadCardViewElement) this.Data.Owner).OnCardViewItemFormatting(new CardViewItemFormattingEventArgs(layoutControlItemBase, this));
    }

    private void UpdateGroupItemsExpandedState(CardListViewElement cardElement)
    {
      List<LayoutControlItemBase> allChildItems = cardElement.GetAllChildItems((RadItemCollection) this.CardContainer.Items);
      List<string> collapsedItemNames = this.GetCollapsedItemNames(cardElement);
      foreach (LayoutControlItemBase layoutControlItemBase in allChildItems)
      {
        LayoutControlGroupItem controlGroupItem = layoutControlItemBase as LayoutControlGroupItem;
        if (controlGroupItem != null)
        {
          bool flag = true;
          if (collapsedItemNames.Contains(controlGroupItem.Name))
            flag = false;
          controlGroupItem.isAttaching = true;
          controlGroupItem.IsExpanded = flag;
          controlGroupItem.isAttaching = false;
        }
      }
    }

    private List<string> GetCollapsedItemNames(CardListViewElement cardElement)
    {
      if (cardElement.CollapsedGroupsDictionary.ContainsKey(this.Data))
        return cardElement.CollapsedGroupsDictionary[this.Data];
      return new List<string>();
    }

    private void SynchronizeCardViewItems(
      CardListViewElement cardElement,
      List<LayoutControlItemBase> containerItems)
    {
      IDictionary<string, string> boundItems = cardElement.BoundItems;
      if (boundItems == null || boundItems.Count == 0)
        return;
      foreach (LayoutControlItemBase containerItem in containerItems)
      {
        ICardViewBoundItem cardViewBoundItem = containerItem as ICardViewBoundItem;
        if (cardViewBoundItem != null)
        {
          string cardItemFieldName = this.GetCardItemFieldName(containerItem, boundItems);
          if (!string.IsNullOrEmpty(cardItemFieldName))
          {
            ListViewDetailColumn column = cardElement.Owner.Columns[cardItemFieldName];
            if (column != null && (cardViewBoundItem.CardField == null || cardViewBoundItem.CardField.Name != column.Name))
            {
              cardViewBoundItem.FieldName = column.Name;
              cardViewBoundItem.SetCardField(column);
            }
            cardViewBoundItem.Synchronize();
          }
        }
      }
    }

    private string GetCardItemFieldName(
      LayoutControlItemBase item,
      IDictionary<string, string> boundItems)
    {
      string empty = string.Empty;
      int num = item.Name.LastIndexOf(item.GetType().Name);
      string key = item.Name.Substring(0, num >= 0 ? num : 0);
      boundItems.TryGetValue(key, out empty);
      return empty;
    }

    private LayoutControlItemBase FindCardViewItemByName(
      string name,
      RadItemCollection layoutContainerItems)
    {
      LayoutControlItemBase layoutControlItemBase = (LayoutControlItemBase) null;
      foreach (LayoutControlItemBase layoutContainerItem in layoutContainerItems)
      {
        if (layoutContainerItem.Name == name + layoutContainerItem.GetType().Name || layoutContainerItem.Name == name)
          return layoutContainerItem;
        if (layoutContainerItem is LayoutControlGroupItem)
          return this.FindCardViewItemByName(name, (layoutContainerItem as LayoutControlGroupItem).Items);
        if (layoutContainerItem is LayoutControlTabbedGroup)
          return this.FindCardViewItemByName(name, (RadItemCollection) (layoutContainerItem as LayoutControlTabbedGroup).ItemGroups);
      }
      return layoutControlItemBase;
    }

    private IEnumerable<LayoutControlItemBase> FindCardViewItemsByType(
      System.Type itemType,
      RadItemCollection layoutContainerItems)
    {
      foreach (LayoutControlItemBase layoutContainerItem in layoutContainerItems)
      {
        if ((object) layoutContainerItem.GetType() == (object) itemType)
          yield return layoutContainerItem;
        LayoutControlGroupItem groupItem = layoutContainerItem as LayoutControlGroupItem;
        LayoutControlTabbedGroup tabbedGroup = layoutContainerItem as LayoutControlTabbedGroup;
        if (groupItem != null)
          this.FindCardViewItemsByType(itemType, groupItem.Items);
        else if (tabbedGroup != null)
          this.FindCardViewItemsByType(itemType, (RadItemCollection) tabbedGroup.ItemGroups);
      }
    }

    private bool HasItemCollapsedGroups(CardListViewElement cardElement)
    {
      if (cardElement.CollapsedGroupsDictionary.ContainsKey(this.Data))
        return cardElement.CollapsedGroupsDictionary[this.Data].Count > 0;
      return false;
    }

    private void HorizontalScrollBar_ValueChanged(object sender, EventArgs e)
    {
      this.layoutContainer.PositionOffset = new SizeF((float) -this.horizontalScrollBar.Value, this.layoutContainer.PositionOffset.Height);
    }

    private void VerticalScrollBar_ValueChanged(object sender, EventArgs e)
    {
      this.layoutContainer.PositionOffset = new SizeF(this.layoutContainer.PositionOffset.Width, (float) -this.verticalScrollBar.Value);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.Data == null)
        return SizeF.Empty;
      SizeF size = this.GetClientRectangle(availableSize).Size;
      foreach (RadElement child in this.Children)
      {
        if (child != this.CardContainer)
          child.Measure(size);
      }
      SizeF itemSize = (SizeF) this.Data.Owner.ItemSize;
      return this.MeasureContentCore(availableSize, itemSize);
    }

    protected override SizeF MeasureContentCore(SizeF availableSize, SizeF desiredSize)
    {
      if (this.Data == null || this.IsDesignMode)
        return SizeF.Empty;
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (this.Data.Owner.ShowCheckBoxes)
      {
        switch (this.Data.Owner.CheckBoxesPosition)
        {
          case CheckBoxesPosition.Left:
          case CheckBoxesPosition.Right:
            num1 = this.ToggleElement.DesiredSize.Width;
            break;
          default:
            num2 = this.ToggleElement.DesiredSize.Height;
            break;
        }
      }
      desiredSize.Width += num1;
      desiredSize.Height += num2;
      RadListViewElement owner = this.Data.Owner;
      if (owner == null)
        return desiredSize;
      bool arbitraryItemWidth = owner.AllowArbitraryItemWidth;
      bool arbitraryItemHeight = owner.AllowArbitraryItemHeight;
      CardListViewElement viewElement = owner.ViewElement as CardListViewElement;
      SizeF itemSize = (SizeF) owner.ItemSize;
      if (viewElement.CardTemplate.ContainerElement.LayoutTree.Root != null)
      {
        if (arbitraryItemWidth)
          itemSize.Width = viewElement.CardTemplate.ContainerElement.LayoutTree.Root.Bounds.Width;
        if (arbitraryItemHeight)
          itemSize.Height = viewElement.CardTemplate.ContainerElement.LayoutTree.Root.Bounds.Height;
      }
      desiredSize = itemSize;
      if (!arbitraryItemHeight && !arbitraryItemWidth)
      {
        this.layoutContainer.Measure(desiredSize);
        this.UpdateScrollbars(desiredSize);
      }
      else if (arbitraryItemHeight && !arbitraryItemWidth)
      {
        desiredSize.Width = itemSize.Width + num1;
        this.layoutContainer.Measure(desiredSize);
        if (this.HasItemCollapsedGroups(viewElement) && this.layoutContainer.LayoutTree.Root != null)
        {
          this.layoutContainer.Measure((SizeF) new Size(1, 1));
          desiredSize.Height = this.layoutContainer.LayoutTree.Root.Bounds.Height;
          this.layoutContainer.Measure(desiredSize);
        }
        if (this.HorizontalScrollBar.Visibility != ElementVisibility.Collapsed)
          desiredSize.Height += (float) this.HorizontalScrollBar.MinSize.Height;
        this.UpdateScrollbars(desiredSize);
        this.VerticalScrollBar.Visibility = ElementVisibility.Collapsed;
        desiredSize.Height += num2;
        if (this.Data.Size.Height > 0)
          desiredSize.Height = (float) this.Data.Size.Height;
      }
      else if (!arbitraryItemHeight && arbitraryItemWidth)
      {
        desiredSize.Height = itemSize.Height + num2;
        this.layoutContainer.Measure(desiredSize);
        if (this.HasItemCollapsedGroups(viewElement) && this.layoutContainer.LayoutTree.Root != null)
        {
          desiredSize.Width = this.layoutContainer.LayoutTree.Root.Bounds.Width + (this.VerticalScrollBar.Visibility != ElementVisibility.Collapsed ? (float) this.VerticalScrollBar.MinSize.Width : 0.0f);
          this.layoutContainer.Measure(desiredSize);
        }
        this.UpdateScrollbars(desiredSize);
        this.HorizontalScrollBar.Visibility = ElementVisibility.Collapsed;
        if (this.VerticalScrollBar.Visibility != ElementVisibility.Collapsed)
          desiredSize.Width += (float) this.VerticalScrollBar.MinSize.Width;
        desiredSize.Width += num1;
        if (this.Data.Size.Width > 0)
          desiredSize.Width = (float) this.Data.Size.Width;
      }
      else
      {
        this.VerticalScrollBar.Visibility = ElementVisibility.Collapsed;
        this.HorizontalScrollBar.Visibility = ElementVisibility.Collapsed;
        this.layoutContainer.Measure(desiredSize);
        if (this.HasItemCollapsedGroups(viewElement) && this.layoutContainer.LayoutTree.Root != null)
          desiredSize = this.layoutContainer.LayoutTree.Root.Bounds.Size;
        this.layoutContainer.Measure(desiredSize);
        desiredSize.Width += num1;
        desiredSize.Height += num2;
        if (this.Data.Size.Width > 0)
          desiredSize.Width = (float) this.Data.Size.Width;
        if (this.Data.Size.Height > 0)
          desiredSize.Height = (float) this.Data.Size.Height;
      }
      Padding padding = Padding.Add(this.GetBorderThickness(true), this.Padding);
      desiredSize = SizeF.Add(desiredSize, new SizeF((float) padding.Horizontal, (float) padding.Vertical));
      this.Data.ActualSize = desiredSize.ToSize();
      return desiredSize;
    }

    protected override void ArrangeContentCore(RectangleF clientRect)
    {
      if (this.IsDesignMode)
        return;
      base.ArrangeContentCore(clientRect);
      RectangleF toggleRectangle;
      RectangleF layoutManagerRect;
      this.GetArrangeRectangles(clientRect, out toggleRectangle, out layoutManagerRect);
      if (this.RightToLeft)
      {
        toggleRectangle = LayoutUtils.RTLTranslateNonRelative(toggleRectangle, clientRect);
        layoutManagerRect = LayoutUtils.RTLTranslateNonRelative(layoutManagerRect, clientRect);
      }
      if (this.VerticalScrollBar.Visibility == ElementVisibility.Visible)
      {
        RectangleF rectangleF = new RectangleF(layoutManagerRect.X + layoutManagerRect.Width - (float) this.VerticalScrollBar.MinSize.Width, layoutManagerRect.Y, (float) this.VerticalScrollBar.MinSize.Width, layoutManagerRect.Height);
        if (this.RightToLeft)
          rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, clientRect);
        this.VerticalScrollBar.Arrange(rectangleF);
        layoutManagerRect.Width -= rectangleF.Width;
      }
      if (this.HorizontalScrollBar.Visibility == ElementVisibility.Visible)
      {
        RectangleF finalRect = new RectangleF(layoutManagerRect.X, layoutManagerRect.Y + layoutManagerRect.Height - (float) this.HorizontalScrollBar.MinSize.Height, layoutManagerRect.Width, (float) this.HorizontalScrollBar.MinSize.Height);
        this.HorizontalScrollBar.Arrange(finalRect);
        layoutManagerRect.Height -= finalRect.Height;
      }
      if (this.ToggleElement.Visibility != ElementVisibility.Collapsed)
        this.ToggleElement.Arrange(toggleRectangle);
      this.layoutContainer.Arrange(layoutManagerRect);
    }
  }
}
