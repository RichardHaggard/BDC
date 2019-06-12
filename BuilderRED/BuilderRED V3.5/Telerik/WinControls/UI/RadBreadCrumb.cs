// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadBreadCrumb
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadBreadCrumbDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadBreadCrumb : RadControl
  {
    private RadTreeView defaultTreeView;
    private RadBreadCrumbElement breadCrumbElement;
    private bool dropDownOpened;
    private RadSplitButtonElement currentExpandedSplitButton;
    private bool selectTreeNodeOnClick;

    protected override void CreateChildItems(RadElement parent)
    {
      this.breadCrumbElement = this.CraeteBreadCrumbElement();
      parent.Children.Add((RadElement) this.breadCrumbElement);
    }

    [Category("Behavior")]
    [Description("Associates a TreeView to the bread crumb control")]
    [DefaultValue(null)]
    public RadTreeView DefaultTreeView
    {
      get
      {
        return this.defaultTreeView;
      }
      set
      {
        if (this.defaultTreeView != null)
        {
          this.defaultTreeView.SelectedNodeChanged -= new RadTreeView.RadTreeViewEventHandler(this.value_Selected);
          this.defaultTreeView.NodeExpandedChanged -= new RadTreeView.TreeViewEventHandler(this.value_NodeExpand);
        }
        this.defaultTreeView = value;
        if (this.defaultTreeView != null && this.defaultTreeView.SelectedNode != null)
          this.UpdateBreadCrumb(this.defaultTreeView.SelectedNode);
        else
          this.ClearCurrentItems();
        if (value == null)
          return;
        value.SelectedNodeChanged += new RadTreeView.RadTreeViewEventHandler(this.value_Selected);
        value.NodeExpandedChanged += new RadTreeView.TreeViewEventHandler(this.value_NodeExpand);
      }
    }

    [Category("Behavior")]
    [Description("Determines whether a tree node will be selected upon clicking on the action part of the split button element. The default value is false and click on the action part will result in opening the popup with the menu items.")]
    [DefaultValue(false)]
    public bool SelectTreeNodeOnClick
    {
      get
      {
        return this.selectTreeNodeOnClick;
      }
      set
      {
        if (this.selectTreeNodeOnClick == value)
          return;
        this.selectTreeNodeOnClick = value;
        if (this.DefaultTreeView == null)
          return;
        this.UpdateBreadCrumb(this.DefaultTreeView.SelectedNode);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual RadBreadCrumbElement BreadCrumbElement
    {
      get
      {
        return this.breadCrumbElement;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    private void value_NodeExpand(object sender, RadTreeViewEventArgs tvea)
    {
      if (!tvea.Node.Selected)
        return;
      this.UpdateBreadCrumb(tvea.Node);
    }

    private void value_Selected(object sender, RadTreeViewEventArgs e)
    {
      this.UpdateBreadCrumb(this.defaultTreeView.SelectedNode);
    }

    private void currentSplitButton_MouseEnter(object sender, EventArgs e)
    {
      if (!this.dropDownOpened)
        return;
      RadSplitButtonElement splitButtonElement = sender as RadSplitButtonElement;
      if (splitButtonElement == null || splitButtonElement == this.currentExpandedSplitButton)
        return;
      if (this.currentExpandedSplitButton != null)
        this.currentExpandedSplitButton.HideDropDown();
      this.currentExpandedSplitButton = splitButtonElement;
      if (splitButtonElement.IsDropDownShown)
        return;
      splitButtonElement.ShowDropDown();
    }

    private void currentSplitButton_DropDownClosed(object sender, EventArgs e)
    {
      int num = 0;
      foreach (RadDropDownButtonElement downButtonElement in (RadItemCollection) this.breadCrumbElement.Items)
      {
        if (!downButtonElement.IsDropDownShown)
          ++num;
      }
      if (num != this.breadCrumbElement.Items.Count)
        return;
      this.dropDownOpened = false;
    }

    private void currentSplitButton_DropDownOpened(object sender, EventArgs e)
    {
      this.dropDownOpened = true;
    }

    private void item_Click(object sender, EventArgs e)
    {
      RadBreadCrumb.AssociatedMenuItem associatedMenuItem = sender as RadBreadCrumb.AssociatedMenuItem;
      for (RadTreeNode radTreeNode = associatedMenuItem.AssociatedNode; radTreeNode != null; radTreeNode = radTreeNode.Parent)
        radTreeNode.Expand();
      this.defaultTreeView.SelectedNode = associatedMenuItem.AssociatedNode;
      this.defaultTreeView.BringIntoView(associatedMenuItem.AssociatedNode);
    }

    protected virtual RadBreadCrumbElement CraeteBreadCrumbElement()
    {
      return new RadBreadCrumbElement();
    }

    public void UpdateBreadCrumb(RadTreeNode node)
    {
      this.ClearCurrentItems();
      Stack<RadSplitButtonElement> stack = new Stack<RadSplitButtonElement>();
      RadTreeNode lastNode = (RadTreeNode) null;
      if (node == null)
        return;
      Image image = node.Image;
      for (; node != null; node = node.Parent)
      {
        RadSplitButtonElement ddButton = new RadSplitButtonElement();
        ddButton.Text = node.Text;
        ddButton.TextImageRelation = TextImageRelation.ImageBeforeText;
        if (node.Nodes.Count == 0)
          ddButton.ShowArrow = false;
        this.SetDropDownItems(node, lastNode, ddButton);
        if (this.SelectTreeNodeOnClick)
        {
          RadBreadCrumb.AssociatedMenuItem associatedMenuItem = new RadBreadCrumb.AssociatedMenuItem(node);
          associatedMenuItem.Click += new EventHandler(this.item_Click);
          associatedMenuItem.Visibility = ElementVisibility.Collapsed;
          ddButton.Items.Add((RadItem) associatedMenuItem);
          ddButton.DefaultItem = (RadItem) associatedMenuItem;
        }
        ddButton.Text = node.Text;
        stack.Push(ddButton);
        lastNode = node;
      }
      this.AddBreadCrumbsChildren(stack, image);
    }

    public int GetNodesCount(RadTreeNodeCollection nodes)
    {
      using (IEnumerator<RadTreeNode> enumerator = nodes.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          RadTreeNode current = enumerator.Current;
          return nodes.Count + this.GetNodesCount(current.Nodes);
        }
      }
      return nodes.Count;
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      if (TelerikHelper.IsMaterialTheme(this.ThemeName))
      {
        this.RootElement.SetThemeValueOverride(RadItem.EnableElementShadowProperty, (object) true, "");
        this.RootElement.SetThemeValueOverride(RootRadElement.ControlDefaultSizeProperty, (object) new Size(120, 36), "");
      }
      else
      {
        this.RootElement.ResetThemeValueOverride(RadItem.EnableElementShadowProperty);
        this.RootElement.ResetThemeValueOverride(RootRadElement.ControlDefaultSizeProperty);
      }
    }

    private void ClearCurrentItems()
    {
      while (this.breadCrumbElement.Items.Count > 0)
      {
        RadSplitButtonElement splitButtonElement = this.breadCrumbElement.Items[0] as RadSplitButtonElement;
        splitButtonElement.DropDownOpened -= new EventHandler(this.currentSplitButton_DropDownOpened);
        splitButtonElement.DropDownClosed -= new EventHandler(this.currentSplitButton_DropDownClosed);
        splitButtonElement.MouseEnter -= new EventHandler(this.currentSplitButton_MouseEnter);
        foreach (RadElement radElement in (RadItemCollection) splitButtonElement.Items)
          radElement.Click -= new EventHandler(this.item_Click);
        this.breadCrumbElement.Items[0].Dispose();
      }
    }

    private void SetDropDownItems(
      RadTreeNode node,
      RadTreeNode lastNode,
      RadSplitButtonElement ddButton)
    {
      for (int index = 0; index < node.Nodes.Count; ++index)
      {
        RadBreadCrumb.AssociatedMenuItem associatedMenuItem = new RadBreadCrumb.AssociatedMenuItem(node.Nodes[index]);
        associatedMenuItem.Click += new EventHandler(this.item_Click);
        if (lastNode == node.Nodes[index])
          associatedMenuItem.Font = new Font(associatedMenuItem.Font, FontStyle.Bold);
        associatedMenuItem.Image = node.Nodes[index].Image;
        associatedMenuItem.Text = node.Nodes[index].Text;
        ddButton.Items.Add((RadItem) associatedMenuItem);
      }
    }

    private void AddBreadCrumbsChildren(Stack<RadSplitButtonElement> stack, Image firstNodeImage)
    {
      Size empty = Size.Empty;
      Graphics graphics = this.CreateGraphics();
      bool flag = false;
      while (stack.Count > 0)
      {
        RadSplitButtonElement splitButtonElement = stack.Pop();
        if (!flag)
        {
          Size size = Size.Ceiling(graphics.MeasureString(splitButtonElement.Text, splitButtonElement.Font));
          if (!size.IsEmpty && firstNodeImage != null)
            splitButtonElement.Image = (Image) new Bitmap(firstNodeImage, new Size(Math.Min(size.Height, firstNodeImage.Width), Math.Min(size.Height, firstNodeImage.Width)));
          flag = true;
        }
        splitButtonElement.DropDownOpened += new EventHandler(this.currentSplitButton_DropDownOpened);
        splitButtonElement.DropDownClosed += new EventHandler(this.currentSplitButton_DropDownClosed);
        splitButtonElement.MouseEnter += new EventHandler(this.currentSplitButton_MouseEnter);
        this.breadCrumbElement.Items.Add((RadItem) splitButtonElement);
      }
      graphics.Dispose();
    }

    private class AssociatedMenuItem : RadMenuItem
    {
      private RadTreeNode associatedNode;

      public AssociatedMenuItem(RadTreeNode associatedNode)
      {
        this.associatedNode = associatedNode;
      }

      protected override System.Type ThemeEffectiveType
      {
        get
        {
          return typeof (RadMenuItem);
        }
      }

      public RadTreeNode AssociatedNode
      {
        get
        {
          return this.associatedNode;
        }
      }
    }
  }
}
