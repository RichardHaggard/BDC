// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardListViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI
{
  public class CardListViewElement : IconListViewElement
  {
    private int uniqueItemId = 1;
    private static IList<RadProperty> itemSynchronizationProperties;
    internal int containerVersionNumber;
    internal int synchronizationPropertiesVersionNumber;
    private ComponentXmlSerializationInfo xmlSerializationInfo;
    private RadDropDownMenu initialDropDownMenu;
    private RadDropDownMenu dropDownMenu;
    private RadMenuItem hideMenuItem;
    private IDictionary<string, string> boundItems;
    private IList<LayoutControlItemBase> cardTemplateItems;
    private IDictionary<ListViewDataItem, List<string>> collapsedGroupsDictionary;

    public CardListViewElement(RadListViewElement owner)
      : base(owner)
    {
      owner.BindingCompleted += new EventHandler(this.Owner_BindingCompleted);
      owner.Columns.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Columns_CollectionChanged);
      this.WireCardTemplateEvents();
      this.CardTemplate.Size = this.ItemSize;
      this.ItemSpacing = 10;
      this.cardTemplateItems = (IList<LayoutControlItemBase>) new List<LayoutControlItemBase>();
      this.CollapsedGroupsDictionary = (IDictionary<ListViewDataItem, List<string>>) new Dictionary<ListViewDataItem, List<string>>();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.dropDownMenu = new RadDropDownMenu();
      this.initialDropDownMenu = this.dropDownMenu;
      this.InitializeDropDownMenu();
      this.ItemSize = new Size(200, 250);
    }

    protected override IVirtualizedElementProvider<ListViewDataItem> CreateElementProvider()
    {
      return (IVirtualizedElementProvider<ListViewDataItem>) new CardViewVirtualizedElementProvider((BaseListViewElement) this);
    }

    protected override void DisposeManagedResources()
    {
      this.Owner.BindingCompleted -= new EventHandler(this.Owner_BindingCompleted);
      this.Owner.Columns.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.Columns_CollectionChanged);
      this.UnwireCardTemplateEvents();
      this.hideMenuItem.Dispose();
      this.dropDownMenu.Dispose();
      base.DisposeManagedResources();
    }

    public static IList<RadProperty> ItemSynchronizationProperties
    {
      get
      {
        if (CardListViewElement.itemSynchronizationProperties == null)
        {
          CardListViewElement.itemSynchronizationProperties = (IList<RadProperty>) new List<RadProperty>();
          CardListViewElement.InitializeItemSynchronizationProperties();
        }
        return CardListViewElement.itemSynchronizationProperties;
      }
    }

    public RadLayoutControl CardTemplate
    {
      get
      {
        if (this.CardViewElement != null)
          return this.CardViewElement.CardTemplate;
        return (RadLayoutControl) null;
      }
    }

    public RadDropDownMenu DropDownMenu
    {
      get
      {
        return this.dropDownMenu;
      }
      set
      {
        if (this.dropDownMenu == value)
          return;
        if (this.initialDropDownMenu != null)
        {
          this.initialDropDownMenu.Dispose();
          this.initialDropDownMenu = (RadDropDownMenu) null;
        }
        this.dropDownMenu = value;
        this.OnNotifyPropertyChanged(nameof (DropDownMenu));
      }
    }

    public RadCardViewElement CardViewElement
    {
      get
      {
        return this.Owner as RadCardViewElement;
      }
    }

    public override Size ItemSize
    {
      get
      {
        return base.ItemSize;
      }
      set
      {
        base.ItemSize = value;
        this.UpdateCardTemplateSize();
      }
    }

    public override bool AllowArbitraryItemHeight
    {
      get
      {
        return base.AllowArbitraryItemHeight;
      }
      set
      {
        base.AllowArbitraryItemHeight = value;
        this.UpdateCardTemplateSize();
      }
    }

    public override bool AllowArbitraryItemWidth
    {
      get
      {
        return base.AllowArbitraryItemWidth;
      }
      set
      {
        base.AllowArbitraryItemWidth = value;
        this.UpdateCardTemplateSize();
      }
    }

    protected internal IDictionary<string, string> BoundItems
    {
      get
      {
        return this.boundItems;
      }
      private set
      {
        this.boundItems = value;
      }
    }

    protected internal IDictionary<ListViewDataItem, List<string>> CollapsedGroupsDictionary
    {
      get
      {
        return this.collapsedGroupsDictionary;
      }
      private set
      {
        this.collapsedGroupsDictionary = value;
      }
    }

    public virtual void UpdateItemsLayout()
    {
      if (this.IsAtDesignTime())
        return;
      if (this.Owner != null && this.Owner.ViewElement != null)
        this.Owner.SynchronizeVisualItems();
      this.ViewElement.InvalidateMeasure(true);
      this.ViewElement.UpdateLayout();
    }

    public void ShowCustomizeDialog()
    {
      RadCardView control = this.ElementTree.Control as RadCardView;
      if (control == null)
        return;
      BaseListViewVisualItem selectedListViewItem = this.GetSelectedListViewItem(control);
      this.CardTemplate.ThemeName = control.ThemeName;
      this.CardTemplate.Show();
      Padding padding = Padding.Add(LightVisualElement.GetBorderThickness((LightVisualElement) selectedListViewItem, true), selectedListViewItem.Padding);
      Size sz = new Size(padding.Left, padding.Top);
      this.CardTemplate.Location = Point.Add(selectedListViewItem.ControlBoundingRectangle.Location, sz);
      this.CardTemplate.ShowCustomizeDialog();
    }

    public void CloseCustomizeDialog()
    {
      if (this.CardTemplate == null)
        return;
      this.CardTemplate.CustomizeDialog.Hide();
      this.CardTemplate.Hide();
      this.UpdateCardTemplateSize();
      this.UpdateCardTemplateLayout(true);
      this.UpdateItemsLayout();
    }

    public virtual List<LayoutControlItemBase> GetAllChildItems(
      RadItemCollection items)
    {
      List<LayoutControlItemBase> layoutControlItemBaseList = new List<LayoutControlItemBase>();
      Queue<LayoutControlItemBase> layoutControlItemBaseQueue = new Queue<LayoutControlItemBase>();
      foreach (LayoutControlItemBase layoutControlItemBase in items)
        layoutControlItemBaseQueue.Enqueue(layoutControlItemBase);
      while (layoutControlItemBaseQueue.Count > 0)
      {
        LayoutControlItemBase layoutControlItemBase1 = layoutControlItemBaseQueue.Dequeue();
        layoutControlItemBaseList.Add(layoutControlItemBase1);
        LayoutControlGroupItem controlGroupItem = layoutControlItemBase1 as LayoutControlGroupItem;
        LayoutControlTabbedGroup controlTabbedGroup = layoutControlItemBase1 as LayoutControlTabbedGroup;
        if (controlGroupItem != null)
        {
          foreach (LayoutControlItemBase layoutControlItemBase2 in controlGroupItem.Items)
            layoutControlItemBaseQueue.Enqueue(layoutControlItemBase2);
        }
        else if (controlTabbedGroup != null)
        {
          foreach (LayoutControlItemBase itemGroup in (RadItemCollection) controlTabbedGroup.ItemGroups)
            layoutControlItemBaseQueue.Enqueue(itemGroup);
        }
      }
      return layoutControlItemBaseList;
    }

    public Dictionary<string, System.Type> GetFieldNames()
    {
      Dictionary<string, System.Type> dictionary = new Dictionary<string, System.Type>();
      RadListViewElement owner = this.Owner;
      if (owner.IsDataBound)
      {
        if (owner.ListSource == null || owner.ListSource.BoundProperties.Count == 0)
          return dictionary;
        foreach (PropertyDescriptor boundProperty in owner.ListSource.BoundProperties)
        {
          if ((object) boundProperty.PropertyType != (object) typeof (IBindingList))
            dictionary.Add(boundProperty.Name, boundProperty.PropertyType);
        }
      }
      else
      {
        foreach (ListViewDetailColumn column in (Collection<ListViewDetailColumn>) owner.Columns)
          dictionary.Add(column.Name, typeof (string));
      }
      return dictionary;
    }

    public virtual void SaveCardTemplateLayout(XmlWriter xmlWriter)
    {
      this.CardTemplate.XmlSerializationInfo = this.XmlSerializationInfo;
      this.CardTemplate.SaveLayout(xmlWriter);
    }

    public virtual void SaveCardTemplateLayout(Stream stream)
    {
      this.CardTemplate.XmlSerializationInfo = this.XmlSerializationInfo;
      this.CardTemplate.SaveLayout(stream);
    }

    public virtual void SaveCardTemplateLayout(string fileName)
    {
      this.CardTemplate.XmlSerializationInfo = this.XmlSerializationInfo;
      this.CardTemplate.SaveLayout(fileName);
    }

    public virtual void LoadCardTemplateLayout(XmlReader xmlReader)
    {
      this.CardTemplate.XmlSerializationInfo = this.XmlSerializationInfo;
      this.CardTemplate.LoadLayout(xmlReader);
      this.UpdateCardTemplateSize();
      this.UpdateItemsLayout();
    }

    public virtual void LoadCardTemplateLayout(Stream stream)
    {
      this.CardTemplate.XmlSerializationInfo = this.XmlSerializationInfo;
      this.CardTemplate.LoadLayout(stream);
      this.UpdateCardTemplateSize();
      this.UpdateItemsLayout();
    }

    public virtual void LoadCardTemplateLayout(string fileName)
    {
      this.CardTemplate.XmlSerializationInfo = this.XmlSerializationInfo;
      this.CardTemplate.LoadLayout(fileName);
      this.UpdateCardTemplateSize();
      this.UpdateItemsLayout();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ComponentXmlSerializationInfo XmlSerializationInfo
    {
      get
      {
        if (this.xmlSerializationInfo == null)
          this.xmlSerializationInfo = this.GetDefaultXmlSerializationInfo();
        return this.xmlSerializationInfo;
      }
      set
      {
        this.xmlSerializationInfo = value;
      }
    }

    protected virtual ComponentXmlSerializationInfo GetDefaultXmlSerializationInfo()
    {
      return new ComponentXmlSerializationInfo(new PropertySerializationMetadataCollection() { { typeof (RadLayoutControl), "Name", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadLayoutControl), "Visible", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadLayoutControl), "AllowResize", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadControl), "ThemeName", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Controls", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "DataBindings", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadComponentElement), "DataBindings", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadElement), "Style", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (LayoutControlItem), "Text", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible) } }, { typeof (LayoutControlItem), "DrawText", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible) } }, { typeof (Control), "Tag", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (RadControl), "RootElement", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Size", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Location", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Dock", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } }, { typeof (Control), "Anchor", new Attribute[1]{ (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden) } } });
    }

    private static void InitializeItemSynchronizationProperties()
    {
      CardListViewElement.ItemSynchronizationProperties.Add(VisualElement.BackColorProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.BackColor2Property);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.BackColor3Property);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.BackColor4Property);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.NumberOfColorsProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.DrawFillProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(VisualElement.ForeColorProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(VisualElement.FontProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.TextWrapProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.DrawTextProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.TextAlignmentProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.ImageAlignmentProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.TextImageRelationProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.BorderColorProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.DrawBorderProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.BorderWidthProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.GradientStyleProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.GradientPercentageProperty);
      CardListViewElement.ItemSynchronizationProperties.Add(LightVisualElement.GradientPercentage2Property);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (!this.Owner.IsDataBound && !this.Owner.IsLayoutSuspended)
        this.ResetCardTemplate();
      this.UpdateCardTemplateLayoutRefresh();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right && this.CardTemplate.AllowCustomize && this.CardTemplate.Site == null)
      {
        this.dropDownMenu.ThemeName = this.CardTemplate.ThemeName;
        this.hideMenuItem.Text = LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCustomizeText");
        this.dropDownMenu.Show((RadItem) this, e.Location);
      }
      else
        base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right && this.CardTemplate.AllowCustomize && this.CardTemplate.Site == null)
        return;
      base.OnMouseUp(e);
    }

    protected virtual void InitializeDropDownMenu()
    {
      if (this.hideMenuItem == null)
        this.hideMenuItem = new RadMenuItem(LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCustomizeText"));
      this.hideMenuItem.Image = LayoutControlIcons.Customize;
      this.hideMenuItem.Click += new EventHandler(this.CustomizeItem_Click);
      this.dropDownMenu.Items.Add((RadItem) this.hideMenuItem);
    }

    protected virtual void ResetCardTemplate()
    {
      RadListViewElement owner = this.Owner;
      if (owner == null)
        return;
      IDesignerHost designerHost = this.GetDesignerHost(owner);
      if (!this.HasListViewColumns(owner))
      {
        this.ClearCardTemplateItems(designerHost);
      }
      else
      {
        if (this.AreCardFieldsSame())
          return;
        this.BoundItems = (IDictionary<string, string>) new Dictionary<string, string>();
        this.ClearCardTemplateItems(designerHost);
        if (designerHost != null)
        {
          Dictionary<string, System.Type> fieldNames = this.GetFieldNames();
          if (fieldNames.Count == 0)
            return;
          this.CreateCardTemplateItemsDesignTime(designerHost, owner, fieldNames);
        }
        else
          this.CreateCardTemplateItems(owner);
        this.RebuildCardTemplateLayoutTree(true);
      }
    }

    private void ClearCardTemplateItems(IDesignerHost host)
    {
      if (host != null)
        this.DestroyDesignTimeCardTemplateItems(host);
      this.CardTemplate.Items.Clear();
    }

    private void CreateCardTemplateItems(RadListViewElement listViewElement)
    {
      CardViewGroupItem cardViewGroupItem = new CardViewGroupItem();
      cardViewGroupItem.Name = cardViewGroupItem.GetType().Name + (object) this.uniqueItemId++;
      this.CardTemplate.Items.Add((RadItem) cardViewGroupItem);
      int y = 0;
      int width = listViewElement.ItemSize.Width;
      int val2 = listViewElement.ItemSize.Height / listViewElement.Columns.Count;
      string name = typeof (CardViewItem).Name;
      foreach (ListViewDetailColumn column in (Collection<ListViewDetailColumn>) listViewElement.Columns)
      {
        CardViewItem cardViewItem = new CardViewItem();
        cardViewItem.Text = column.HeaderText;
        cardViewItem.CardField = column;
        cardViewItem.FieldName = column.Name;
        cardViewItem.Name = name + (object) this.uniqueItemId++;
        int height = Math.Max(cardViewItem.MinSize.Height, val2);
        cardViewItem.Bounds = new Rectangle(0, y, width, height);
        y += height;
        cardViewGroupItem.Items.Add((RadItem) cardViewItem);
        this.BoundItems.Add(cardViewItem.Name, column.Name);
      }
    }

    private void CreateCardTemplateItemsDesignTime(
      IDesignerHost host,
      RadListViewElement listViewElement,
      Dictionary<string, System.Type> fieldNameTypeDictionary)
    {
      CardViewGroupItem component1 = host.CreateComponent(typeof (CardViewGroupItem)) as CardViewGroupItem;
      component1.Name = component1.GetType().Name + (object) this.uniqueItemId++;
      this.CardTemplate.Items.Add((RadItem) component1);
      int y = 0;
      int width = listViewElement.ItemSize.Width;
      int val2 = listViewElement.ItemSize.Height / fieldNameTypeDictionary.Count;
      string name = typeof (CardViewItem).Name;
      foreach (KeyValuePair<string, System.Type> fieldNameType in fieldNameTypeDictionary)
      {
        string key = fieldNameType.Key;
        System.Type type = fieldNameType.Value;
        CardViewItem component2 = host.CreateComponent(typeof (CardViewItem)) as CardViewItem;
        component2.Text = key;
        component2.EditorItem.Text = type.Name;
        component2.FieldName = key;
        component2.Name = name + (object) this.uniqueItemId++;
        int height = Math.Max(component2.MinSize.Height, val2);
        component2.Bounds = new Rectangle(0, y, width, height);
        y += height;
        component1.Items.Add((RadItem) component2);
        this.BoundItems.Add(component2.Name, key);
      }
    }

    private IDesignerHost GetDesignerHost(RadListViewElement listViewElement)
    {
      if (this.IsAtDesignTime())
        return listViewElement.ElementTree.Control.Site.GetService(typeof (IDesignerHost)) as IDesignerHost;
      return (IDesignerHost) null;
    }

    private bool HasListViewColumns(RadListViewElement listViewElement)
    {
      if (!listViewElement.IsDataBound)
        return listViewElement.Columns.Count > 0;
      if (listViewElement.ListSource != null)
        return listViewElement.ListSource.BoundProperties.Count > 0;
      return false;
    }

    private void DestroyDesignTimeCardTemplateItems(IDesignerHost host)
    {
      if (host == null)
        return;
      List<LayoutControlItemBase> cardTemplateItems = this.GetCardTemplateItems(true);
      cardTemplateItems.Reverse();
      foreach (LayoutControlItemBase layoutControlItemBase in cardTemplateItems)
        host.DestroyComponent((IComponent) layoutControlItemBase);
    }

    protected virtual bool AreCardFieldsSame()
    {
      List<LayoutControlItemBase> cardTemplateItems = this.GetCardTemplateItems(true);
      if (cardTemplateItems.Count == 0)
        return false;
      return this.AreItemFieldNamesEqualToColumnNames((IList<LayoutControlItemBase>) cardTemplateItems);
    }

    private bool AreItemFieldNamesEqualToColumnNames(IList<LayoutControlItemBase> items)
    {
      bool flag1 = false;
      bool flag2 = true;
      this.BoundItems = (IDictionary<string, string>) new Dictionary<string, string>();
      IDictionary<string, System.Type> fieldNames = (IDictionary<string, System.Type>) this.GetFieldNames();
      foreach (LayoutControlItemBase layoutControlItemBase in (IEnumerable<LayoutControlItemBase>) items)
      {
        string itemFieldName = this.GetItemFieldName(layoutControlItemBase);
        if (!string.IsNullOrEmpty(itemFieldName))
        {
          if (fieldNames.ContainsKey(itemFieldName))
          {
            flag1 = true;
            if (string.IsNullOrEmpty(layoutControlItemBase.Name) && layoutControlItemBase.Site != null)
              layoutControlItemBase.Name = layoutControlItemBase.Site.Name;
            this.BoundItems.Add(layoutControlItemBase.Name, itemFieldName);
          }
          else
          {
            flag2 = false;
            break;
          }
        }
      }
      if (flag1 && flag2)
        return true;
      this.BoundItems.Clear();
      return false;
    }

    private string GetItemFieldName(LayoutControlItemBase item)
    {
      ICardViewBoundItem cardViewBoundItem = item as ICardViewBoundItem;
      if (cardViewBoundItem != null)
        return cardViewBoundItem.FieldName;
      return string.Empty;
    }

    private bool IsAtDesignTime()
    {
      RadListViewElement owner = this.Owner;
      return owner != null && owner.ElementTree != null && (owner.ElementTree.Control != null && owner.ElementTree.Control.Site != null);
    }

    private List<LayoutControlItemBase> GetCardTemplateItems(
      bool includeHidden)
    {
      List<LayoutControlItemBase> allChildItems1 = this.GetAllChildItems((RadItemCollection) this.CardTemplate.Items);
      if (includeHidden)
      {
        List<LayoutControlItemBase> allChildItems2 = this.GetAllChildItems(this.CardTemplate.HiddenItems);
        allChildItems1.AddRange((IEnumerable<LayoutControlItemBase>) allChildItems2);
      }
      return allChildItems1;
    }

    private void WireCardTemplateEvents()
    {
      this.CardTemplate.ContainerElement.Items.ItemsChanged += new ItemChangedDelegate(this.Items_ItemsChanged);
      this.CardTemplate.StructureChanged += new EventHandler(this.CardTemplate_StructureChanged);
      this.CardTemplate.SizeChanged += new EventHandler(this.CardTemplate_SizeChanged);
      this.CardTemplate.CustomizeDialog.FormClosing += new FormClosingEventHandler(this.CustomizeDialog_FormClosing);
      this.CardTemplate.HandleDropCompleted += new EventHandler(this.CardTemplate_HandleDropCompleted);
    }

    private void UnwireCardTemplateEvents()
    {
      this.CardTemplate.ContainerElement.Items.ItemsChanged -= new ItemChangedDelegate(this.Items_ItemsChanged);
      this.CardTemplate.StructureChanged -= new EventHandler(this.CardTemplate_StructureChanged);
      this.CardTemplate.SizeChanged -= new EventHandler(this.CardTemplate_SizeChanged);
      this.CardTemplate.CustomizeDialog.FormClosing -= new FormClosingEventHandler(this.CustomizeDialog_FormClosing);
      this.CardTemplate.HandleDropCompleted -= new EventHandler(this.CardTemplate_HandleDropCompleted);
    }

    private void UpdateCardTemplateLayout(bool rebuildLayoutTree)
    {
      if (rebuildLayoutTree)
        this.RebuildCardTemplateLayoutTree();
      this.CardTemplate.UpdateControlsLayout();
      this.CardTemplate.UpdateScrollbars();
    }

    private void RebuildCardTemplateLayoutTree()
    {
      this.RebuildCardTemplateLayoutTree(true);
    }

    private void RebuildCardTemplateLayoutTree(bool performLayout)
    {
      if (this.Owner.IsLayoutSuspended)
        return;
      if (this.CardTemplate.Size != this.CardTemplate.ContainerElement.Size)
        this.CardTemplate.ContainerElement.ElementTree.PerformLayout();
      this.CardTemplate.ContainerElement.RebuildLayoutTree(performLayout);
    }

    private void UpdateCardTemplateLayoutRefresh()
    {
      if (this.IsAtDesignTime())
        return;
      this.UpdateCardTemplateSize();
      this.UpdateCardTemplateLayout(true);
      this.UpdateItemsLayout();
    }

    private LayoutControlContainerElement GetCardTemplateContainer()
    {
      return this.CardTemplate.ContainerElement;
    }

    private void UpdateCardTemplateSize()
    {
      if (this.CardTemplate == null)
        return;
      Size itemSize = this.ItemSize;
      if (this.AllowArbitraryItemHeight && this.CardTemplate.ContainerElement.LayoutTree.Root != null)
        itemSize.Height = (int) this.CardTemplate.ContainerElement.LayoutTree.Root.Bounds.Height;
      if (this.AllowArbitraryItemWidth && this.CardTemplate.ContainerElement.LayoutTree.Root != null)
        itemSize.Width = (int) this.CardTemplate.ContainerElement.LayoutTree.Root.Bounds.Width;
      this.CardTemplate.Size = itemSize;
      this.RebuildCardTemplateLayoutTree(true);
    }

    protected internal virtual void CompareContainers(
      CardViewContainerElement cardViewContainer,
      CardListViewVisualItem visualItem)
    {
      if (this.IsAtDesignTime())
        return;
      LayoutControlContainerElement templateContainer = this.GetCardTemplateContainer();
      if (cardViewContainer.Size != templateContainer.Size)
        cardViewContainer.Size = templateContainer.Size;
      if (visualItem.ContainerVersionNumber != this.containerVersionNumber)
      {
        this.CompareItems((RadItemCollection) templateContainer.Items, (RadItemCollection) cardViewContainer.Items, visualItem);
        cardViewContainer.RebuildLayoutTree();
        visualItem.ContainerVersionNumber = this.containerVersionNumber;
      }
      for (int index = 0; index < templateContainer.Items.Count; ++index)
        this.UpdateItemProperties(templateContainer.Items[index] as LayoutControlItemBase, cardViewContainer.Items[index] as LayoutControlItemBase, visualItem);
      visualItem.SynchronizationPropertiesVersionNumber = this.synchronizationPropertiesVersionNumber;
      cardViewContainer.RebuildLayoutTree();
    }

    protected virtual void CompareItems(
      RadItemCollection layoutContainerItems,
      RadItemCollection cardViewContainerItems,
      CardListViewVisualItem visualItem)
    {
      foreach (LayoutControlItemBase layoutContainerItem in layoutContainerItems)
      {
        bool flag = false;
        int index = layoutContainerItems.IndexOf((RadItem) layoutContainerItem);
        if (string.IsNullOrEmpty(layoutContainerItem.Name))
        {
          layoutContainerItem.Name = layoutContainerItem.GetType().Name + (object) this.uniqueItemId;
          ++this.uniqueItemId;
        }
        string name = layoutContainerItem.Name;
        foreach (LayoutControlItemBase layoutControlItemBase in new RadItemCollection(cardViewContainerItems))
        {
          if (name + layoutControlItemBase.GetType().Name == layoutControlItemBase.Name)
          {
            flag = true;
            if (flag && index != cardViewContainerItems.IndexOf((RadItem) layoutControlItemBase))
            {
              cardViewContainerItems.Remove((RadItem) layoutControlItemBase);
              if (index >= cardViewContainerItems.Count)
                cardViewContainerItems.Add((RadItem) layoutControlItemBase);
              else
                cardViewContainerItems.Insert(index, (RadItem) layoutControlItemBase);
            }
            LayoutControlGroupItem controlGroupItem = layoutContainerItem as LayoutControlGroupItem;
            LayoutControlTabbedGroup controlTabbedGroup = layoutContainerItem as LayoutControlTabbedGroup;
            if (controlGroupItem != null)
            {
              this.CompareItems(controlGroupItem.Items, (layoutControlItemBase as LayoutControlGroupItem).Items, visualItem);
              break;
            }
            if (controlTabbedGroup != null)
            {
              this.CompareItems((RadItemCollection) controlTabbedGroup.ItemGroups, (RadItemCollection) (layoutControlItemBase as LayoutControlTabbedGroup).ItemGroups, visualItem);
              break;
            }
            break;
          }
        }
        if (!flag)
        {
          LayoutControlItemBase newItem = this.CreateNewItem(layoutContainerItem, visualItem);
          if (index >= cardViewContainerItems.Count)
            cardViewContainerItems.Add((RadItem) newItem);
          else
            cardViewContainerItems.Insert(index, (RadItem) newItem);
        }
      }
      while (layoutContainerItems.Count < cardViewContainerItems.Count)
        cardViewContainerItems.RemoveAt(cardViewContainerItems.Count - 1);
    }

    protected virtual void UpdateItemProperties(
      LayoutControlItemBase originalItem,
      LayoutControlItemBase item,
      CardListViewVisualItem visualItem)
    {
      item.SuspendPropertyNotifications();
      if (originalItem is LayoutControlGroupItem)
      {
        LayoutControlGroupItem controlGroupItem = originalItem as LayoutControlGroupItem;
        CardViewGroupItem cardViewGroupItem = item as CardViewGroupItem;
        for (int index = 0; index < controlGroupItem.Items.Count; ++index)
        {
          LayoutControlItemBase originalItem1 = controlGroupItem.Items[index] as LayoutControlItemBase;
          LayoutControlItemBase layoutControlItemBase = cardViewGroupItem.Items[index] as LayoutControlItemBase;
          this.UpdateItemProperties(originalItem1, layoutControlItemBase, visualItem);
          if (visualItem.SynchronizationPropertiesVersionNumber != this.synchronizationPropertiesVersionNumber)
          {
            this.SuspendLayout();
            this.SynchronizeItemProperties(originalItem1, layoutControlItemBase);
            this.ResumeLayout(false);
          }
        }
        if (visualItem != null && visualItem.Data != null && (this.CollapsedGroupsDictionary.ContainsKey(visualItem.Data) && this.CollapsedGroupsDictionary[visualItem.Data].Contains(cardViewGroupItem.Name)))
          cardViewGroupItem.IsExpanded = false;
      }
      else if (originalItem is LayoutControlTabbedGroup)
      {
        LayoutControlTabbedGroup controlTabbedGroup1 = originalItem as LayoutControlTabbedGroup;
        LayoutControlTabbedGroup controlTabbedGroup2 = item as LayoutControlTabbedGroup;
        for (int index = 0; index < controlTabbedGroup1.ItemGroups.Count; ++index)
        {
          LayoutControlGroupItem itemGroup1 = controlTabbedGroup1.ItemGroups[index] as LayoutControlGroupItem;
          CardViewGroupItem itemGroup2 = controlTabbedGroup2.ItemGroups[index] as CardViewGroupItem;
          this.UpdateItemProperties((LayoutControlItemBase) itemGroup1, (LayoutControlItemBase) itemGroup2, visualItem);
          if (visualItem.SynchronizationPropertiesVersionNumber != this.synchronizationPropertiesVersionNumber)
          {
            this.SuspendLayout();
            this.SynchronizeItemProperties((LayoutControlItemBase) itemGroup1, (LayoutControlItemBase) itemGroup2);
            this.ResumeLayout(false);
          }
        }
      }
      if (visualItem.SynchronizationPropertiesVersionNumber != this.synchronizationPropertiesVersionNumber)
      {
        this.SuspendLayout();
        this.SynchronizeItemProperties(originalItem, item);
        this.ResumeLayout(false);
      }
      if (item.MinSize != originalItem.MinSize)
        item.MinSize = originalItem.MinSize;
      if (item.MaxSize != originalItem.MaxSize)
        item.MaxSize = originalItem.MaxSize;
      if (item.Bounds != originalItem.Bounds)
        item.Bounds = originalItem.Bounds;
      item.ResumePropertyNotifications();
    }

    protected virtual void SynchronizeItemProperties(
      LayoutControlItemBase originalItem,
      LayoutControlItemBase item)
    {
      foreach (RadProperty synchronizationProperty in (IEnumerable<RadProperty>) CardListViewElement.ItemSynchronizationProperties)
      {
        if (item.GetValueSource(synchronizationProperty) < ValueSource.Local)
        {
          if (originalItem.GetValueSource(synchronizationProperty) < ValueSource.Local)
            item.ResetThemeValueOverride(synchronizationProperty);
          else
            item.SetThemeValueOverride(synchronizationProperty, originalItem.GetValue(synchronizationProperty), "");
        }
      }
    }

    protected virtual LayoutControlItemBase CreateNewItem(
      LayoutControlItemBase originalItem,
      CardListViewVisualItem visualItem)
    {
      LayoutControlItemBase newItem = (LayoutControlItemBase) null;
      if (originalItem is CardViewItem)
        newItem = (LayoutControlItemBase) this.CreateCardViewItem(originalItem as CardViewItem);
      else if (originalItem is LayoutControlLabelItem)
        newItem = (LayoutControlItemBase) this.CreateLabelItem(originalItem as LayoutControlLabelItem);
      else if (originalItem is LayoutControlSplitterItem)
        newItem = (LayoutControlItemBase) this.CreateSplitterItem(originalItem as LayoutControlSplitterItem);
      else if (originalItem is LayoutControlSeparatorItem)
        newItem = (LayoutControlItemBase) this.CreateSeparatorItem(originalItem as LayoutControlSeparatorItem);
      else if (originalItem is LayoutControlGroupItem)
        newItem = (LayoutControlItemBase) this.CreateGroupItem(originalItem as LayoutControlGroupItem, visualItem);
      else if (originalItem is LayoutControlTabbedGroup)
        newItem = (LayoutControlItemBase) this.CreateTabbedGroup(originalItem as LayoutControlTabbedGroup, visualItem);
      if (newItem != null)
      {
        newItem.NotifyParentOnMouseInput = true;
        newItem.ShouldHandleMouseInput = false;
        newItem.Text = originalItem.Text;
        newItem.Image = originalItem.Image;
      }
      if (!this.cardTemplateItems.Contains(originalItem))
      {
        originalItem.RadPropertyChanged += new RadPropertyChangedEventHandler(this.OriginalItem_RadPropertyChanged);
        originalItem.Disposing += new EventHandler(this.OriginalItem_Disposing);
        this.cardTemplateItems.Add(originalItem);
      }
      LayoutControlItemBase layoutControlItemBase = this.ProcessItemCreatingEvent(originalItem, newItem, visualItem);
      if (layoutControlItemBase != null)
        layoutControlItemBase.Name = originalItem.Name + layoutControlItemBase.GetType().Name;
      return layoutControlItemBase;
    }

    protected virtual CardViewItem CreateCardViewItem(CardViewItem originalItem)
    {
      CardViewItem cardViewItem = new CardViewItem();
      cardViewItem.EditorItem.DrawText = originalItem.EditorItem.DrawText;
      cardViewItem.EditorItem.NotifyParentOnMouseInput = true;
      cardViewItem.EditorItem.ShouldHandleMouseInput = true;
      cardViewItem.CardField = originalItem.CardField;
      cardViewItem.FieldName = originalItem.FieldName;
      cardViewItem.TextPosition = originalItem.TextPosition;
      cardViewItem.TextProportionalSize = originalItem.TextProportionalSize;
      cardViewItem.TextFixedSize = originalItem.TextFixedSize;
      cardViewItem.TextMinSize = originalItem.TextMinSize;
      cardViewItem.TextMaxSize = originalItem.TextMaxSize;
      cardViewItem.TextSizeMode = originalItem.TextSizeMode;
      return cardViewItem;
    }

    protected virtual LayoutControlLabelItem CreateLabelItem(
      LayoutControlLabelItem originalItem)
    {
      return new LayoutControlLabelItem();
    }

    protected virtual LayoutControlSeparatorItem CreateSeparatorItem(
      LayoutControlSeparatorItem originalItem)
    {
      return new LayoutControlSeparatorItem() { Thickness = originalItem.Thickness };
    }

    protected virtual LayoutControlSplitterItem CreateSplitterItem(
      LayoutControlSplitterItem originalItem)
    {
      LayoutControlSplitterItem controlSplitterItem = new LayoutControlSplitterItem();
      controlSplitterItem.HorizontalImage = originalItem.HorizontalImage;
      controlSplitterItem.VerticalImage = originalItem.VerticalImage;
      controlSplitterItem.Thickness = originalItem.Thickness;
      return controlSplitterItem;
    }

    protected virtual LayoutControlGroupItem CreateGroupItem(
      LayoutControlGroupItem originalItem,
      CardListViewVisualItem visualItem)
    {
      CardViewGroupItem cardViewGroupItem1 = new CardViewGroupItem();
      cardViewGroupItem1.TextAlignment = originalItem.TextAlignment;
      cardViewGroupItem1.TextWrap = originalItem.TextWrap;
      cardViewGroupItem1.ShowHorizontalLine = originalItem.ShowHorizontalLine;
      cardViewGroupItem1.ShowHeaderLine = originalItem.ShowHeaderLine;
      int num = (int) cardViewGroupItem1.SetDefaultValueOverride(LayoutControlGroupItem.IsExpandedProperty, (object) originalItem.IsExpanded);
      cardViewGroupItem1.HeaderHeight = originalItem.HeaderHeight;
      CardViewGroupItem cardViewGroupItem2 = originalItem as CardViewGroupItem;
      if (cardViewGroupItem2 != null)
      {
        cardViewGroupItem1.CardField = cardViewGroupItem2.CardField;
        cardViewGroupItem1.FieldName = cardViewGroupItem2.FieldName;
      }
      foreach (LayoutControlItemBase originalItem1 in originalItem.Items)
      {
        LayoutControlItemBase newItem = this.CreateNewItem(originalItem1, visualItem);
        cardViewGroupItem1.Items.Add((RadItem) newItem);
      }
      cardViewGroupItem1.RadPropertyChanged += new RadPropertyChangedEventHandler(this.GroupItem_RadPropertyChanged);
      return (LayoutControlGroupItem) cardViewGroupItem1;
    }

    protected virtual LayoutControlTabbedGroup CreateTabbedGroup(
      LayoutControlTabbedGroup originalItem,
      CardListViewVisualItem visualItem)
    {
      LayoutControlTabbedGroup controlTabbedGroup = new LayoutControlTabbedGroup();
      LayoutControlGroupItem controlGroupItem = (LayoutControlGroupItem) null;
      foreach (LayoutControlGroupItem itemGroup in (RadItemCollection) originalItem.ItemGroups)
      {
        CardViewGroupItem newItem = this.CreateNewItem((LayoutControlItemBase) itemGroup, visualItem) as CardViewGroupItem;
        controlTabbedGroup.ItemGroups.Add((RadItem) newItem);
        if (originalItem.SelectedGroup == itemGroup)
          controlGroupItem = (LayoutControlGroupItem) newItem;
      }
      controlTabbedGroup.SelectedGroup = controlGroupItem;
      return controlTabbedGroup;
    }

    private LayoutControlItemBase ProcessItemCreatingEvent(
      LayoutControlItemBase originalItem,
      LayoutControlItemBase newItem,
      CardListViewVisualItem visualItem)
    {
      CardViewItemCreatingEventArgs args = new CardViewItemCreatingEventArgs(originalItem, newItem, visualItem);
      this.CardViewElement.OnCardViewItemCreating(args);
      return args.NewItem;
    }

    private BaseListViewVisualItem GetSelectedListViewItem(
      RadCardView cardView)
    {
      BaseListViewVisualItem listViewVisualItem = (BaseListViewVisualItem) null;
      if (cardView.SelectedItem == null)
      {
        listViewVisualItem = this.ViewElement.Children[0] as BaseListViewVisualItem;
      }
      else
      {
        ListViewDataItem selectedItem = cardView.SelectedItem;
        this.EnsureItemVisible(selectedItem);
        foreach (BaseListViewVisualItem child in this.ViewElement.Children)
        {
          if (child.Data == selectedItem)
            listViewVisualItem = child;
        }
        if (listViewVisualItem == null)
          listViewVisualItem = this.ViewElement.Children[0] as BaseListViewVisualItem;
      }
      return listViewVisualItem;
    }

    private void CardTemplate_StructureChanged(object sender, EventArgs e)
    {
      ++this.containerVersionNumber;
    }

    private void Items_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      ++this.containerVersionNumber;
      if (operation != ItemsChangeOperation.Inserted)
        return;
      LayoutControlItemBase layoutControlItemBase = target as LayoutControlItemBase;
      if (layoutControlItemBase == null || !string.IsNullOrEmpty(layoutControlItemBase.Name))
        return;
      layoutControlItemBase.Name = layoutControlItemBase.GetType().Name + (object) this.uniqueItemId;
      ++this.uniqueItemId;
    }

    private void CardTemplate_HandleDropCompleted(object sender, EventArgs e)
    {
      if (this.CardTemplate.CustomizeDialog.Visible)
      {
        this.UpdateCardTemplateSize();
        this.UpdateCardTemplateLayout(true);
      }
      else
        this.UpdateCardTemplateLayout(false);
      ++this.containerVersionNumber;
      this.UpdateItemsLayout();
    }

    private void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      RadListViewElement owner = this.Owner;
      if (owner == null || owner.IsDataBound || owner.IsLayoutSuspended)
        return;
      if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove || (e.Action == NotifyCollectionChangedAction.Replace || e.Action == NotifyCollectionChangedAction.Reset))
        this.ClearCardTemplateItems(this.GetDesignerHost(owner));
      this.ResetCardTemplate();
    }

    private void Owner_BindingCompleted(object sender, EventArgs e)
    {
      this.ResetCardTemplate();
    }

    private void CustomizeDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.CloseCustomizeDialog();
    }

    private void CardTemplate_SizeChanged(object sender, EventArgs e)
    {
      if (this.CardTemplate.CustomizeDialog.Visible)
        return;
      this.CardTemplate.ContainerElement.Size = this.CardTemplate.Size;
      this.UpdateCardTemplateLayout(true);
      this.UpdateItemsLayout();
    }

    private void CustomizeItem_Click(object sender, EventArgs e)
    {
      if (!this.CardTemplate.AllowCustomize)
        return;
      this.ShowCustomizeDialog();
    }

    private void OriginalItem_Disposing(object sender, EventArgs e)
    {
      (sender as LayoutControlItemBase).RadPropertyChanged -= new RadPropertyChangedEventHandler(this.OriginalItem_RadPropertyChanged);
      (sender as LayoutControlItemBase).Disposing -= new EventHandler(this.OriginalItem_Disposing);
    }

    private void OriginalItem_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      LayoutControlItemBase layoutControlItemBase = sender as LayoutControlItemBase;
      if (layoutControlItemBase == null || layoutControlItemBase.GetValueSource(e.Property) < ValueSource.Local || !CardListViewElement.ItemSynchronizationProperties.Contains(e.Property))
        return;
      ++this.synchronizationPropertiesVersionNumber;
      this.Owner.SynchronizeVisualItems();
      this.Owner.Update(RadListViewElement.UpdateModes.RefreshLayout);
    }

    private void GroupItem_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != LayoutControlGroupItem.IsExpandedProperty)
        return;
      this.UpdateCollapsedGroupsDictionary(sender as LayoutControlGroupItem);
    }

    private void UpdateCollapsedGroupsDictionary(LayoutControlGroupItem groupItem)
    {
      if (groupItem == null)
        return;
      CardListViewVisualItem ancestor = groupItem.FindAncestor<CardListViewVisualItem>();
      if (ancestor == null || ancestor.Data == null)
        return;
      bool isExpanded = groupItem.IsExpanded;
      if (this.CollapsedGroupsDictionary.ContainsKey(ancestor.Data))
      {
        List<string> collapsedGroups = this.CollapsedGroupsDictionary[ancestor.Data];
        if (collapsedGroups.Contains(groupItem.Name))
        {
          if (isExpanded)
          {
            collapsedGroups.Remove(groupItem.Name);
            if (collapsedGroups.Count == 0)
              this.CollapsedGroupsDictionary.Remove(ancestor.Data);
          }
        }
        else if (!isExpanded)
          collapsedGroups.Add(groupItem.Name);
      }
      else if (!isExpanded)
      {
        this.CollapsedGroupsDictionary.Add(ancestor.Data, new List<string>());
        this.CollapsedGroupsDictionary[ancestor.Data].Add(groupItem.Name);
      }
      if (this.Owner.isSynchronizing)
        return;
      ancestor.InvalidateMeasure();
      ancestor.UpdateLayout();
    }
  }
}
