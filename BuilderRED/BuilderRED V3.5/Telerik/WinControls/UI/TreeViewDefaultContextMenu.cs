// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewDefaultContextMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class TreeViewDefaultContextMenu : RadContextMenu
  {
    private RadTreeViewElement treeView;
    private TreeViewMenuItem expandCollapseMenuItem;
    private TreeViewMenuItem editMenuItem;
    private TreeViewMenuItem addMenuItem;
    private TreeViewMenuItem deleteMenuItem;
    private TreeViewMenuItem copyMenuItem;
    private TreeViewMenuItem pasteMenuItem;
    private TreeViewMenuItem cutMentuItem;
    private WeakReference copyRef;

    public TreeViewDefaultContextMenu(RadTreeViewElement treeView)
    {
      this.treeView = treeView;
      this.editMenuItem = new TreeViewMenuItem("Edit", "&Edit");
      this.Items.Add((RadItem) this.editMenuItem);
      this.expandCollapseMenuItem = new TreeViewMenuItem("Expand", LocalizationProvider<TreeViewLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuExpand"));
      this.Items.Add((RadItem) this.expandCollapseMenuItem);
      this.addMenuItem = new TreeViewMenuItem("Add", "&Add");
      this.Items.Add((RadItem) this.addMenuItem);
      this.deleteMenuItem = new TreeViewMenuItem("Delete", "&Delete");
      this.Items.Add((RadItem) this.deleteMenuItem);
      this.cutMentuItem = new TreeViewMenuItem("Cut", "Cu&t");
      this.Items.Add((RadItem) this.cutMentuItem);
      this.copyMenuItem = new TreeViewMenuItem("Copy", "&Copy");
      this.Items.Add((RadItem) this.copyMenuItem);
      this.pasteMenuItem = new TreeViewMenuItem("Paste", "&Paste");
      this.Items.Add((RadItem) this.pasteMenuItem);
      for (int index = 0; index < this.Items.Count; ++index)
        this.Items[index].Click += new EventHandler(this.menuItem_Click);
    }

    public TreeViewMenuItem ExpandCollapseMenuItem
    {
      get
      {
        return this.expandCollapseMenuItem;
      }
    }

    public TreeViewMenuItem EditMenuItem
    {
      get
      {
        return this.editMenuItem;
      }
    }

    public TreeViewMenuItem AddMenuItem
    {
      get
      {
        return this.addMenuItem;
      }
    }

    public TreeViewMenuItem DeleteMenuItem
    {
      get
      {
        return this.deleteMenuItem;
      }
    }

    public TreeViewMenuItem CutMenuItem
    {
      get
      {
        return this.cutMentuItem;
      }
    }

    public TreeViewMenuItem CopyMenuItem
    {
      get
      {
        return this.copyMenuItem;
      }
    }

    public TreeViewMenuItem PasteMenuItem
    {
      get
      {
        return this.pasteMenuItem;
      }
    }

    internal RadTreeNode NodeUnderMouse { get; set; }

    private void menuItem_Click(object sender, EventArgs e)
    {
      TreeViewMenuItem treeViewMenuItem = sender as TreeViewMenuItem;
      if (treeViewMenuItem == null)
        return;
      switch (treeViewMenuItem.Command)
      {
        case "Edit":
          this.EditNode();
          break;
        case "Expand":
        case "Collapse":
          this.ExpandNode();
          break;
        case "Add":
          this.AddNode(this.treeView.CreateNewNode("New Node"), true, this.NodeUnderMouse);
          break;
        case "Delete":
          this.DeleteNode();
          break;
        case "Cut":
          this.CutNode();
          break;
        case "Copy":
          this.CopyNode(true);
          break;
        case "Paste":
          this.PasteNode();
          break;
      }
    }

    private void PasteNode()
    {
      object data = Clipboard.GetData("RadTreeViewTreeNode");
      if (data != null && data is int && (this.copyRef != null && this.copyRef.IsAlive) && (int) data == this.copyRef.Target.GetHashCode())
      {
        this.AddNode((RadTreeNode) this.copyRef.Target, false, this.treeView.SelectedNode);
      }
      else
      {
        string text = Clipboard.GetText();
        if (string.IsNullOrEmpty(text))
          return;
        this.AddNode(new RadTreeNode(text), false, this.treeView.SelectedNode);
      }
    }

    private void CopyNode(bool clone)
    {
      RadTreeNode selectedNode = this.treeView.SelectedNode;
      if (selectedNode == null)
        return;
      this.copyRef = new WeakReference(clone ? selectedNode.Clone() : (object) selectedNode);
      DataObject dataObject = new DataObject();
      dataObject.SetData("RadTreeViewTreeNode", (object) selectedNode.GetHashCode());
      dataObject.SetText(selectedNode.Text);
      Clipboard.SetDataObject((object) dataObject);
    }

    private void CutNode()
    {
      this.CopyNode(false);
      this.DeleteNode();
    }

    private void DeleteNode()
    {
      if (this.treeView.IsEditing || !this.treeView.AllowRemove)
        return;
      RadTreeNode selectedNode = this.treeView.SelectedNode;
      if (selectedNode == null)
        return;
      RadTreeViewElement.RemoveAllNodes(this.treeView, selectedNode);
    }

    private void AddNode(RadTreeNode newNode, bool beginEdit, RadTreeNode node)
    {
      if (this.treeView.IsEditing || !this.treeView.AllowAdd)
        return;
      if (node == null && this.treeView != null)
        this.treeView.Nodes.Add(newNode);
      else
        node.Nodes.Add(newNode);
      if (this.treeView == null)
        return;
      if (node != null)
        node.Expanded = true;
      if (this.treeView.bindingProvider.IsDataBound)
        return;
      this.treeView.SelectedNode = newNode;
      this.treeView.UpdateLayout();
      if (!beginEdit)
        return;
      this.treeView.BeginEdit();
    }

    private void ExpandNode()
    {
      RadTreeNode selectedNode = this.treeView.SelectedNode;
      if (selectedNode == null)
        return;
      if (selectedNode.Expanded)
        selectedNode.Collapse(true);
      else
        selectedNode.Expand();
    }

    private void EditNode()
    {
      this.treeView.BeginEdit();
    }

    protected override void Dispose(bool disposing)
    {
      for (int index = 0; index < this.Items.Count; ++index)
        this.Items[index].Click -= new EventHandler(this.menuItem_Click);
      this.editMenuItem.Dispose();
      this.expandCollapseMenuItem.Dispose();
      this.addMenuItem.Dispose();
      this.deleteMenuItem.Dispose();
      this.cutMentuItem.Click -= new EventHandler(this.menuItem_Click);
      this.cutMentuItem.Dispose();
      this.copyMenuItem.Click -= new EventHandler(this.menuItem_Click);
      this.copyMenuItem.Dispose();
      this.pasteMenuItem.Click -= new EventHandler(this.menuItem_Click);
      this.pasteMenuItem.Dispose();
      base.Dispose(disposing);
    }
  }
}
