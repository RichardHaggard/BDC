// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadLayoutControlCustomizeDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadLayoutControlCustomizeDialog : RadForm
  {
    private Point mouseDownPoint = new Point(-1, -1);
    private RadLayoutControl layoutControl;
    private ListViewDataItemGroup hiddenItemsGroup;
    private ListViewDataItemGroup newItemsGroup;
    private ListViewDataItem emptySpaceItem;
    private ListViewDataItem labelItem;
    private ListViewDataItem separatorItem;
    private ListViewDataItem splitterItem;
    private ListViewDataItem groupItem;
    private ListViewDataItem tabbedGroupItem;
    private IContainer components;
    private Panel panel1;
    private RadButton btnSaveLayout;
    private RadButton btnLoadLayout;
    private RadListView itemsListView;
    private RadPageView radPageView1;
    private RadPageViewPage radPageViewPage1;
    private RadPageViewPage radPageViewPage2;
    private RadTreeView structureTreeView;

    protected ListViewDataItem SplitterItem
    {
      get
      {
        return this.splitterItem;
      }
    }

    protected ListViewDataItemGroup HiddenItemsGroup
    {
      get
      {
        return this.hiddenItemsGroup;
      }
    }

    public RadLayoutControlCustomizeDialog(RadLayoutControl layoutControl)
      : this()
    {
      this.layoutControl = layoutControl;
    }

    public RadLayoutControlCustomizeDialog()
    {
      this.InitializeComponent();
      this.Icon = Telerik.WinControls.ResFinder.ProgressIcon;
      this.Text = this.GetString("CustomizeDialogText");
      this.radPageViewPage1.Text = this.GetString("CustomizeDialogPageItems");
      this.radPageViewPage2.Text = this.GetString("CustomizeDialogPageStructure");
      this.btnSaveLayout.Image = LayoutControlIcons.SaveLayout;
      this.btnSaveLayout.ImageAlignment = ContentAlignment.MiddleCenter;
      this.btnSaveLayout.ToolTipTextNeeded += (ToolTipTextNeededEventHandler) ((sender, e) => e.ToolTipText = this.GetString("CustomizeDialogSaveLayout"));
      this.btnLoadLayout.Image = LayoutControlIcons.LoadLayout;
      this.btnLoadLayout.ImageAlignment = ContentAlignment.MiddleCenter;
      this.btnLoadLayout.ToolTipTextNeeded += (ToolTipTextNeededEventHandler) ((sender, e) => e.ToolTipText = this.GetString("CustomizeDialogLoadLayout"));
      this.Icon = Icon.FromHandle(new Bitmap(LayoutControlIcons.Customize).GetHicon());
      this.hiddenItemsGroup = new ListViewDataItemGroup(this.GetString("CustomizeDialogHiddenItems"));
      this.newItemsGroup = new ListViewDataItemGroup();
      this.itemsListView.Groups.Add(this.hiddenItemsGroup);
      this.itemsListView.Groups.Add(this.newItemsGroup);
      this.emptySpaceItem = new ListViewDataItem(this.GetString("CustomizeDialogNewItemsEmptySpace"));
      this.emptySpaceItem.Image = LayoutControlIcons.EmptySpaceItem;
      this.emptySpaceItem.Tag = (object) typeof (LayoutControlLabelItem);
      this.itemsListView.Items.Add(this.emptySpaceItem);
      this.emptySpaceItem.Group = this.newItemsGroup;
      this.labelItem = new ListViewDataItem(this.GetString("CustomizeDialogNewItemsLabel"));
      this.labelItem.Image = LayoutControlIcons.LabelItem;
      this.labelItem.Tag = (object) typeof (LayoutControlLabelItem);
      this.itemsListView.Items.Add(this.labelItem);
      this.labelItem.Group = this.newItemsGroup;
      this.separatorItem = new ListViewDataItem(this.GetString("CustomizeDialogNewItemsSeparator"));
      this.separatorItem.Image = LayoutControlIcons.SeparatorItem;
      this.separatorItem.Tag = (object) typeof (LayoutControlSeparatorItem);
      this.itemsListView.Items.Add(this.separatorItem);
      this.separatorItem.Group = this.newItemsGroup;
      this.splitterItem = new ListViewDataItem(this.GetString("CustomizeDialogNewItemsSplitter"));
      this.splitterItem.Image = LayoutControlIcons.SplitterItem;
      this.splitterItem.Tag = (object) typeof (LayoutControlSplitterItem);
      this.itemsListView.Items.Add(this.splitterItem);
      this.splitterItem.Group = this.newItemsGroup;
      this.groupItem = new ListViewDataItem(this.GetString("CustomizeDialogNewItemsGroup"));
      this.groupItem.Image = LayoutControlIcons.GroupItem;
      this.groupItem.Tag = (object) typeof (LayoutControlGroupItem);
      this.itemsListView.Items.Add(this.groupItem);
      this.groupItem.Group = this.newItemsGroup;
      this.tabbedGroupItem = new ListViewDataItem(this.GetString("CustomizeDialogNewItemsTabbedGroup"));
      this.tabbedGroupItem.Image = LayoutControlIcons.TabbedGroup;
      this.tabbedGroupItem.Tag = (object) typeof (LayoutControlTabbedGroup);
      this.itemsListView.Items.Add(this.tabbedGroupItem);
      this.tabbedGroupItem.Group = this.newItemsGroup;
      this.newItemsGroup.Text = string.Format(this.GetString("CustomizeDialogNewItems"), (object) this.newItemsGroup.Items.Count);
      this.itemsListView.ListViewElement.DragDropService.PreviewDragOver += new EventHandler<RadDragOverEventArgs>(this.OnDragServiceDragOver);
      this.itemsListView.ListViewElement.DragDropService.PreviewDragDrop += new EventHandler<RadDropEventArgs>(this.OnDragServiceDragDrop);
      this.itemsListView.ListViewElement.DragDropService.Stopped += new EventHandler(this.OnDragServiceStopped);
      this.structureTreeView.TreeViewElement.EditMode = TreeNodeEditMode.Value;
      LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.UpdateLocalizableStrings);
      this.Disposed += (EventHandler) ((sender, e) => LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.UpdateLocalizableStrings));
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
        return;
      this.Width += 100;
      this.Height += 150;
      this.btnSaveLayout.Size = new Size(36, 36);
      this.btnLoadLayout.Size = new Size(36, 36);
      this.btnLoadLayout.Left = this.btnSaveLayout.Right + 6;
    }

    protected virtual void UpdateLocalizableStrings(object sender, EventArgs e)
    {
      this.UpdateHiddenItems();
      this.UpdateItemsStructureTreeView();
      this.Text = this.GetString("CustomizeDialogText");
      this.radPageViewPage1.Text = this.GetString("CustomizeDialogPageItems");
      this.radPageViewPage2.Text = this.GetString("CustomizeDialogPageStructure");
      this.emptySpaceItem.Text = this.GetString("CustomizeDialogNewItemsEmptySpace");
      this.labelItem.Text = this.GetString("CustomizeDialogNewItemsLabel");
      this.separatorItem.Text = this.GetString("CustomizeDialogNewItemsSeparator");
      this.splitterItem.Text = this.GetString("CustomizeDialogNewItemsSplitter");
      this.groupItem.Text = this.GetString("CustomizeDialogNewItemsGroup");
      this.tabbedGroupItem.Text = this.GetString("CustomizeDialogNewItemsTabbedGroup");
      this.newItemsGroup.Text = string.Format(this.GetString("CustomizeDialogNewItems"), (object) this.newItemsGroup.Items.Count);
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (!this.Visible || this.layoutControl == null)
        return;
      this.UpdateHiddenItems();
      this.UpdateItemsStructureTreeView();
      this.layoutControl.HiddenItems.ItemsChanged += new ItemChangedDelegate(this.HiddenItems_ItemsChanged);
      this.layoutControl.StructureChanged += new EventHandler(this.layoutControl_StructureChanged);
    }

    protected string GetString(string id)
    {
      return LocalizationProvider<LayoutControlLocalizationProvider>.CurrentProvider.GetLocalizedString(id);
    }

    public virtual void UpdateHiddenItems()
    {
      this.itemsListView.BeginUpdate();
      foreach (ListViewDataItem listViewDataItem in new List<ListViewDataItem>((IEnumerable<ListViewDataItem>) this.hiddenItemsGroup.Items))
        this.itemsListView.Items.Remove(listViewDataItem);
      foreach (LayoutControlItemBase hiddenItem in this.layoutControl.HiddenItems)
      {
        ListViewDataItem listViewDataItem = new ListViewDataItem(this.GetItemText(hiddenItem)) { Tag = (object) hiddenItem };
        listViewDataItem.Image = this.GetItemImage(hiddenItem);
        this.itemsListView.Items.Add(listViewDataItem);
        listViewDataItem.Group = this.hiddenItemsGroup;
      }
      this.itemsListView.EndUpdate();
      this.hiddenItemsGroup.Text = string.Format(this.GetString("CustomizeDialogHiddenItems"), (object) this.hiddenItemsGroup.Items.Count);
    }

    public virtual void UpdateItemsStructureTreeView()
    {
      RadTreeNode radTreeNode = new RadTreeNode(this.GetString("CustomizeDialogRootItem"));
      foreach (LayoutControlItemBase layoutControlItemBase in (RadItemCollection) this.layoutControl.Items)
      {
        RadTreeNode node = new RadTreeNode(this.GetItemText(layoutControlItemBase)) { Tag = (object) layoutControlItemBase, Value = (object) layoutControlItemBase.Text, Image = this.GetItemImage(layoutControlItemBase) };
        radTreeNode.Nodes.Add(node);
        this.InitializeChildNodes(layoutControlItemBase, node);
      }
      this.structureTreeView.Nodes.Clear();
      this.structureTreeView.Nodes.Add(radTreeNode);
      this.structureTreeView.ExpandAll();
    }

    private void InitializeChildNodes(LayoutControlItemBase item, RadTreeNode node)
    {
      if (item is LayoutControlGroupItem)
      {
        foreach (LayoutControlItemBase layoutControlItemBase in (item as LayoutControlGroupItem).Items)
        {
          RadTreeNode node1 = new RadTreeNode(this.GetItemText(layoutControlItemBase)) { Tag = (object) layoutControlItemBase, Value = (object) layoutControlItemBase.Text, Image = this.GetItemImage(layoutControlItemBase) };
          node.Nodes.Add(node1);
          this.InitializeChildNodes(layoutControlItemBase, node1);
        }
      }
      else
      {
        if (!(item is LayoutControlTabbedGroup))
          return;
        foreach (LayoutControlItemBase itemGroup in (RadItemCollection) (item as LayoutControlTabbedGroup).ItemGroups)
        {
          RadTreeNode node1 = new RadTreeNode(this.GetItemText(itemGroup)) { Tag = (object) itemGroup, Value = (object) itemGroup.Text, Image = this.GetItemImage(itemGroup) };
          node.Nodes.Add(node1);
          this.InitializeChildNodes(itemGroup, node1);
        }
      }
    }

    protected virtual string GetItemText(LayoutControlItemBase item)
    {
      if (!string.IsNullOrEmpty(item.Text))
        return item.Text;
      if (item is LayoutControlItem)
        return this.GetString("CustomizeDialogLayoutItem");
      if (item is LayoutControlSplitterItem)
        return this.GetString("CustomizeDialogSplitterItem");
      if (item is LayoutControlSeparatorItem)
        return this.GetString("CustomizeDialogSeparatorItem");
      if (item is LayoutControlLabelItem)
        return this.GetString("CustomizeDialogLabelItem");
      if (item is LayoutControlGroupItem)
        return this.GetString("CustomizeDialogGroupItem");
      if (item is LayoutControlTabbedGroup)
        return this.GetString("CustomizeDialogTabbedGroup");
      return this.GetString("CustomizeDialogLayoutItem");
    }

    protected virtual Image GetItemImage(LayoutControlItemBase item)
    {
      if (item is LayoutControlItem)
        return LayoutControlIcons.ControlItem;
      if (item is LayoutControlSplitterItem)
        return LayoutControlIcons.SplitterItem;
      if (item is LayoutControlSeparatorItem)
        return LayoutControlIcons.SeparatorItem;
      if (item is LayoutControlLabelItem)
        return LayoutControlIcons.LabelItem;
      if (item is LayoutControlGroupItem)
        return LayoutControlIcons.GroupItem;
      if (item is LayoutControlTabbedGroup)
        return LayoutControlIcons.TabbedGroup;
      return (Image) null;
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      this.RightToLeftTranslate();
    }

    protected virtual void RightToLeftTranslate()
    {
      this.SuspendLayout();
      this.SuspendUpdate();
      foreach (Control control in (ArrangedElementCollection) this.panel1.Controls)
        control.Location = new Point(this.panel1.Bounds.Width - control.Location.X - control.Width, control.Location.Y);
      this.ResumeUpdate();
      this.ResumeLayout();
    }

    protected virtual void OnDragServiceStopped(object sender, EventArgs e)
    {
      this.layoutControl.DragOverlay.SetPreviewRectangle(Rectangle.Empty);
      this.layoutControl.DragOverlay.UpdatePreview();
    }

    protected virtual void OnDragServiceDragDrop(object sender, RadDropEventArgs e)
    {
      e.Handled = true;
      this.layoutControl.DragOverlay.SetPreviewRectangle(Rectangle.Empty);
      DraggableLayoutControlItem hitTarget1 = e.HitTarget as DraggableLayoutControlItem;
      DraggableLayoutControlOverlayElement hitTarget2 = e.HitTarget as DraggableLayoutControlOverlayElement;
      BaseListViewVisualItem dragInstance = e.DragInstance as BaseListViewVisualItem;
      if (hitTarget2 != null)
      {
        this.HandleDropOnEmptyContainer(hitTarget2, dragInstance);
      }
      else
      {
        if (hitTarget1 == null)
          return;
        this.HandleItemDrop(dragInstance, hitTarget1);
      }
    }

    protected virtual void HandleItemDrop(
      BaseListViewVisualItem draggedItem,
      DraggableLayoutControlItem target)
    {
      if (draggedItem.Data.Group == this.hiddenItemsGroup)
      {
        LayoutControlItemBase tag = draggedItem.Data.Tag as LayoutControlItemBase;
        if (tag == null)
          return;
        this.layoutControl.HiddenItems.Remove((RadItem) tag);
        this.layoutControl.Items.Add((RadItem) tag);
        target.AssociatedItem.FindAncestor<LayoutControlContainerElement>().LayoutTree.HandleDrop(target, tag, this.layoutControl.PointToClient(Control.MousePosition));
      }
      else
      {
        LayoutControlItemBase newItem = this.CreateNewItem(draggedItem);
        if (newItem == null)
          return;
        target.AssociatedItem.FindAncestor<LayoutControlContainerElement>().LayoutTree.HandleDrop(target, newItem, this.layoutControl.PointToClient(Control.MousePosition));
      }
    }

    protected virtual void HandleDropOnEmptyContainer(
      DraggableLayoutControlOverlayElement overlayElement,
      BaseListViewVisualItem draggedItem)
    {
      LayoutControlDraggableOverlay control = overlayElement.ElementTree.Control as LayoutControlDraggableOverlay;
      LayoutControlItemBase layoutControlItemBase;
      if (draggedItem.Data.Group == this.hiddenItemsGroup)
      {
        LayoutControlItemBase tag = draggedItem.Data.Tag as LayoutControlItemBase;
        if (tag == null)
          return;
        this.layoutControl.HiddenItems.Remove((RadItem) tag);
        layoutControlItemBase = tag;
      }
      else
        layoutControlItemBase = this.CreateNewItem(draggedItem);
      control.Owner.Items.Add((RadItem) layoutControlItemBase);
      control.Owner.ContainerElement.RebuildLayoutTree();
    }

    protected virtual LayoutControlItemBase CreateNewItem(
      BaseListViewVisualItem draggedItem)
    {
      LayoutControlItemBase layoutControlItemBase = (LayoutControlItemBase) null;
      if (draggedItem.Data == this.groupItem)
      {
        LayoutControlGroupItem controlGroupItem = new LayoutControlGroupItem();
        controlGroupItem.Text = this.GetString("NewGroupDefaultText");
        layoutControlItemBase = (LayoutControlItemBase) controlGroupItem;
      }
      else if (draggedItem.Data == this.separatorItem)
        layoutControlItemBase = (LayoutControlItemBase) new LayoutControlSeparatorItem();
      else if (draggedItem.Data == this.splitterItem)
        layoutControlItemBase = (LayoutControlItemBase) new LayoutControlSplitterItem();
      else if (draggedItem.Data == this.labelItem)
      {
        LayoutControlLabelItem controlLabelItem = new LayoutControlLabelItem();
        controlLabelItem.Text = this.GetString("NewLabelDefaultText");
        layoutControlItemBase = (LayoutControlItemBase) controlLabelItem;
      }
      else if (draggedItem.Data == this.emptySpaceItem)
      {
        LayoutControlLabelItem controlLabelItem = new LayoutControlLabelItem();
        controlLabelItem.DrawText = false;
        layoutControlItemBase = (LayoutControlItemBase) controlLabelItem;
      }
      else if (draggedItem.Data == this.tabbedGroupItem)
        layoutControlItemBase = (LayoutControlItemBase) new LayoutControlTabbedGroup();
      if (layoutControlItemBase != null)
        layoutControlItemBase.AllowDelete = true;
      return layoutControlItemBase;
    }

    protected virtual void OnDragServiceDragOver(object sender, RadDragOverEventArgs e)
    {
      if (e.HitTarget is DraggableLayoutControlItem)
      {
        e.CanDrop = true;
        LayoutControlContainerElement ancestor = ((DraggableLayoutControlItem) e.HitTarget).AssociatedItem.FindAncestor<LayoutControlContainerElement>();
        LayoutControlDropTargetInfo dropTargetNode = ancestor.LayoutTree.GetDropTargetNode(e.HitTarget as DraggableLayoutControlItem, this.layoutControl.PointToClient(Control.MousePosition), this.GetDragContext(e.DragInstance));
        dropTargetNode?.TargetBounds.Offset(ancestor.ControlBoundingRectangle.Location);
        this.layoutControl.DragOverlay.SetPreviewRectangle(dropTargetNode != null ? dropTargetNode.TargetBounds : Rectangle.Empty);
      }
      else if (e.HitTarget is DraggableLayoutControlOverlayElement)
      {
        DraggableLayoutControlOverlayElement hitTarget = e.HitTarget as DraggableLayoutControlOverlayElement;
        if (hitTarget.Parent is RootRadElement && hitTarget.Items.Count == 0)
        {
          e.CanDrop = true;
          this.layoutControl.DragOverlay.SetPreviewRectangle(new Rectangle(Point.Empty, this.layoutControl.Size));
        }
        else
          e.CanDrop = false;
      }
      else
      {
        e.CanDrop = false;
        this.layoutControl.DragOverlay.SetPreviewRectangle(Rectangle.Empty);
      }
    }

    protected virtual System.Type GetDragContext(ISupportDrag dragInstance)
    {
      BaseListViewVisualItem listViewVisualItem = dragInstance as BaseListViewVisualItem;
      if (listViewVisualItem == null)
        return (System.Type) null;
      if ((object) (listViewVisualItem.Data.Tag as System.Type) != null)
        return (System.Type) listViewVisualItem.Data.Tag;
      if (listViewVisualItem.Data.Tag != null)
        return listViewVisualItem.Data.Tag.GetType();
      return (System.Type) null;
    }

    protected virtual void OnStructureTreeViewNodeRemoving(
      object sender,
      RadTreeViewCancelEventArgs e)
    {
      LayoutControlItemBase tag = e.Node.Tag as LayoutControlItemBase;
      if (tag != null && tag.AllowDelete)
      {
        this.layoutControl.RemoveItem(tag);
        tag.Dispose();
        if (this.layoutControl.DragOverlay == null)
          return;
        this.layoutControl.DragOverlay.UpdatePreview(true);
      }
      else
        e.Cancel = true;
    }

    private void layoutControl_StructureChanged(object sender, EventArgs e)
    {
      this.UpdateItemsStructureTreeView();
    }

    private void HiddenItems_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      this.UpdateHiddenItems();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      this.layoutControl.HideDragOverlay();
      this.layoutControl.HiddenItems.ItemsChanged -= new ItemChangedDelegate(this.HiddenItems_ItemsChanged);
      this.layoutControl.StructureChanged -= new EventHandler(this.layoutControl_StructureChanged);
      e.Cancel = true;
      this.Owner = (Form) null;
      this.Hide();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData != Keys.Escape)
        return base.ProcessCmdKey(ref msg, keyData);
      this.Close();
      return true;
    }

    protected virtual void OnSaveLayoutButtonClick(object sender, EventArgs e)
    {
      using (SaveFileDialog saveFileDialog = new SaveFileDialog())
      {
        saveFileDialog.DefaultExt = ".xml";
        saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        if (saveFileDialog.ShowDialog() != DialogResult.OK)
          return;
        this.layoutControl.SaveLayout(saveFileDialog.FileName);
      }
    }

    protected virtual void OnLoadLayoutButtonClick(object sender, EventArgs e)
    {
      using (OpenFileDialog openFileDialog = new OpenFileDialog())
      {
        openFileDialog.DefaultExt = ".xml";
        openFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() != DialogResult.OK)
          return;
        this.layoutControl.LoadLayout(openFileDialog.FileName);
      }
    }

    protected virtual void OnStructureTreeNodeEdited(object sender, TreeNodeEditedEventArgs e)
    {
      LayoutControlItemBase tag = e.Node.Tag as LayoutControlItemBase;
      if (tag != null)
      {
        tag.Text = Convert.ToString(e.Node.Value);
        e.Node.Text = this.GetItemText(tag);
      }
      this.layoutControl.DragOverlay.UpdatePreview();
    }

    protected virtual void OnStructureTreeViewSelectedNodeChanged(
      object sender,
      RadTreeViewEventArgs e)
    {
      if (e.Node == null || !e.Node.Selected)
        return;
      LayoutControlItemBase tag = e.Node.Tag as LayoutControlItemBase;
      if (tag == null)
        return;
      this.layoutControl.DragOverlay.SelectLayoutItem(tag);
    }

    protected virtual void OnStructureTreeViewMouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left || this.mouseDownPoint.X < 0 || Math.Abs(e.Location.X - this.mouseDownPoint.X) <= SystemInformation.DragSize.Width && Math.Abs(e.Location.Y - this.mouseDownPoint.Y) <= SystemInformation.DragSize.Height)
        return;
      TreeNodeElement elementAtPoint = this.structureTreeView.ElementTree.GetElementAtPoint(e.Location) as TreeNodeElement;
      if (elementAtPoint == null || elementAtPoint.Data == null || !(elementAtPoint.Data.Tag is LayoutControlItemBase))
        return;
      this.layoutControl.DragOverlay.StartDrag((LayoutControlItemBase) elementAtPoint.Data.Tag);
    }

    protected virtual void OnStructureTreeViewMouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
        this.mouseDownPoint = e.Location;
      else
        this.mouseDownPoint = new Point(-1, -1);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panel1 = new Panel();
      this.btnSaveLayout = new RadButton();
      this.btnLoadLayout = new RadButton();
      this.itemsListView = new RadListView();
      this.radPageView1 = new RadPageView();
      this.radPageViewPage1 = new RadPageViewPage();
      this.radPageViewPage2 = new RadPageViewPage();
      this.structureTreeView = new RadTreeView();
      this.btnSaveLayout.BeginInit();
      this.btnLoadLayout.BeginInit();
      this.itemsListView.BeginInit();
      this.radPageView1.BeginInit();
      this.panel1.SuspendLayout();
      this.radPageView1.SuspendLayout();
      this.radPageViewPage1.SuspendLayout();
      this.radPageViewPage2.SuspendLayout();
      this.structureTreeView.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.btnSaveLayout.Location = new Point(7, 12);
      this.btnSaveLayout.Name = "btnSaveLayout";
      this.btnSaveLayout.Size = new Size(24, 24);
      this.btnSaveLayout.TabIndex = 0;
      this.btnSaveLayout.Click += new EventHandler(this.OnSaveLayoutButtonClick);
      this.btnLoadLayout.Location = new Point(34, 12);
      this.btnLoadLayout.Name = "btnLoadLayout";
      this.btnLoadLayout.Size = new Size(24, 24);
      this.btnLoadLayout.TabIndex = 0;
      this.btnLoadLayout.Click += new EventHandler(this.OnLoadLayoutButtonClick);
      this.panel1.Controls.Add((Control) this.btnSaveLayout);
      this.panel1.Controls.Add((Control) this.btnLoadLayout);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Margin = new Padding(3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(150, 50);
      this.panel1.TabIndex = 0;
      this.itemsListView.AllowDragDrop = true;
      this.itemsListView.AllowEdit = false;
      this.itemsListView.AllowRemove = false;
      this.itemsListView.Dock = DockStyle.Fill;
      this.itemsListView.EnableCustomGrouping = true;
      this.itemsListView.Location = new Point(0, 0);
      this.itemsListView.Name = "itemsListView";
      this.itemsListView.ShowGroups = true;
      this.itemsListView.Size = new Size(250, 277);
      this.itemsListView.TabIndex = 0;
      this.itemsListView.Text = "radListView1";
      this.radPageView1.Controls.Add((Control) this.radPageViewPage1);
      this.radPageView1.Controls.Add((Control) this.radPageViewPage2);
      this.radPageView1.DefaultPage = this.radPageViewPage1;
      this.radPageView1.Dock = DockStyle.Fill;
      this.radPageView1.Location = new Point(0, 45);
      this.radPageView1.Name = "radPageView1";
      this.radPageView1.SelectedPage = this.radPageViewPage1;
      this.radPageView1.Size = new Size(271, 325);
      this.radPageView1.TabIndex = 2;
      this.radPageView1.Text = "radPageView1";
      ((RadPageViewStripElement) this.radPageView1.GetChildAt(0)).StripButtons = StripViewButtons.None;
      this.radPageViewPage1.Controls.Add((Control) this.itemsListView);
      this.radPageViewPage1.ItemSize = new SizeF(44f, 28f);
      this.radPageViewPage1.Location = new Point(10, 37);
      this.radPageViewPage1.Name = "radPageViewPage1";
      this.radPageViewPage1.Size = new Size(250, 277);
      this.radPageViewPage1.Text = "Items";
      this.radPageViewPage2.Controls.Add((Control) this.structureTreeView);
      this.radPageViewPage2.ItemSize = new SizeF(62f, 28f);
      this.radPageViewPage2.Location = new Point(10, 37);
      this.radPageViewPage2.Name = "radPageViewPage2";
      this.radPageViewPage2.Size = new Size(362, 282);
      this.radPageViewPage2.Text = "Structure";
      this.structureTreeView.AllowEdit = true;
      this.structureTreeView.AllowRemove = true;
      this.structureTreeView.Dock = DockStyle.Fill;
      this.structureTreeView.Location = new Point(0, 0);
      this.structureTreeView.Name = "structureTreeView";
      this.structureTreeView.Size = new Size(362, 282);
      this.structureTreeView.SpacingBetweenNodes = -1;
      this.structureTreeView.TabIndex = 0;
      this.structureTreeView.Text = "radTreeView1";
      this.structureTreeView.Edited += new TreeNodeEditedEventHandler(this.OnStructureTreeNodeEdited);
      this.structureTreeView.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(this.OnStructureTreeViewSelectedNodeChanged);
      this.structureTreeView.MouseDown += new MouseEventHandler(this.OnStructureTreeViewMouseDown);
      this.structureTreeView.MouseMove += new MouseEventHandler(this.OnStructureTreeViewMouseMove);
      this.structureTreeView.NodeRemoving += new RadTreeView.RadTreeViewCancelEventHandler(this.OnStructureTreeViewNodeRemoving);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.ClientSize = new Size(267, 360);
      this.Controls.Add((Control) this.radPageView1);
      this.Controls.Add((Control) this.panel1);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (RadLayoutControlCustomizeDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Customize";
      this.TopMost = true;
      this.btnSaveLayout.EndInit();
      this.btnLoadLayout.EndInit();
      this.itemsListView.EndInit();
      this.radPageView1.EndInit();
      this.radPageView1.ResumeLayout(false);
      this.radPageViewPage1.ResumeLayout(false);
      this.radPageViewPage2.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.structureTreeView.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
