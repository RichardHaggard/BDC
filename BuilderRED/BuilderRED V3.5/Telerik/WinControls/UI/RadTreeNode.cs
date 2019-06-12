// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Commands;
using Telerik.WinControls.Data;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadTreeNode : IDataItem, ICloneable, INotifyPropertyChanged
  {
    protected BitVector32 state = new BitVector32();
    private CheckType? checkType = new CheckType?();
    private int itemHeight = -1;
    private int imageIndex = -1;
    internal bool UpdateActualSizeOnExpand = true;
    internal const int DefaultItemHeight = -1;
    protected const int SuspendNotificationsState = 1;
    protected const int IsExpandedState = 2;
    protected const int IsSelectedState = 4;
    protected const int IsCurrentState = 8;
    protected const int IsVisibleState = 16;
    protected const int IsEnableState = 32;
    protected const int IsAllowDropState = 64;
    protected const int UpdateParentSizeOnExpandedChangedState = 128;
    private string toolTipText;
    private string name;
    private string imageKey;
    private string text;
    private Telerik.WinControls.Enumerations.ToggleState checkState;
    private object dataBoundItem;
    private object tag;
    private RadContextMenu contextMenu;
    private TreeNodeStyle style;
    private Image image;
    private int boundIndex;
    private Size actualSize;
    private Size childrenSize;
    private object value;
    private WeakReference matches;
    private bool? isInDesignMode;
    internal RadTreeNode parent;
    internal RadTreeNodeCollection nodes;
    internal RadTreeViewElement treeView;
    private IBindingList boundChildrenCollection;

    public RadTreeNode()
    {
      this.state[16] = true;
      this.state[32] = true;
      this.state[64] = true;
    }

    public RadTreeNode(string text)
      : this()
    {
      this.Text = text;
      this.name = text;
    }

    public RadTreeNode(string text, RadTreeNode[] children)
      : this()
    {
      this.Text = text;
      this.name = text;
      if (children == null || children.Length <= 0)
        return;
      this.Nodes.AddRange(children);
    }

    public RadTreeNode(string text, bool expanded)
      : this(text)
    {
      this.Expanded = expanded;
    }

    public RadTreeNode(string text, Image image)
      : this(text)
    {
      this.Image = image;
    }

    public RadTreeNode(string text, Image image, bool expanded)
      : this(text, expanded)
    {
      this.Image = image;
    }

    internal virtual bool CanPerformFind
    {
      get
      {
        return true;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IEnumerator<RadTreeNode> Matches
    {
      get
      {
        if (this.matches != null && this.matches.IsAlive)
        {
          List<RadTreeNode> target = this.matches.Target as List<RadTreeNode>;
          if (target != null)
            return (IEnumerator<RadTreeNode>) target.GetEnumerator();
        }
        return (IEnumerator<RadTreeNode>) new List<RadTreeNode>().GetEnumerator();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [DefaultValue(null)]
    public TreeNodeStyle Style
    {
      get
      {
        if (this.style == null)
        {
          this.style = new TreeNodeStyle();
          this.style.PropertyChanged += new PropertyChangedEventHandler(this.Style_PropertyChanged);
        }
        return this.style;
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HasStyle
    {
      get
      {
        return this.style != null;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the node can respond to user interaction.")]
    [Category("Behavior")]
    public bool Enabled
    {
      get
      {
        return this.state[32];
      }
      set
      {
        if (value == this.state[32])
          return;
        this.state[32] = value;
        this.Update(RadTreeViewElement.UpdateActions.StateChanged);
      }
    }

    [Description("Gets the root parent node for this instance.")]
    [DefaultValue(null)]
    [Category("Behavior")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadTreeNode RootNode
    {
      get
      {
        RadTreeNode radTreeNode = this;
        while (radTreeNode.Parent != null)
          radTreeNode = radTreeNode.Parent;
        return radTreeNode;
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets the parent tree view that the tree node is assigned to.")]
    public RadTreeView TreeView
    {
      get
      {
        RadTreeViewElement treeViewElement = this.TreeViewElement;
        if (treeViewElement != null && treeViewElement.ElementTree != null && treeViewElement.ElementTree.Control != null)
          return treeViewElement.ElementTree.Control as RadTreeView;
        return (RadTreeView) null;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [NotifyParentProperty(true)]
    [Description("Gets or set a value indicating whether the check box displayed by the tree node is in the checked state.")]
    public bool Checked
    {
      get
      {
        if (this.dataBoundItem != null && this.TreeViewElement != null && (!string.IsNullOrEmpty(this.TreeViewElement.CheckedMember) && this.boundIndex < this.TreeViewElement.bindingProvider.BoundLevels))
        {
          TreeNodeDescriptor treeNodeDescriptor = this.TreeViewElement.bindingProvider[this.boundIndex];
          if (treeNodeDescriptor.CheckedDescriptor != null)
            return Convert.ToBoolean(treeNodeDescriptor.GetChecked(this));
        }
        return this.CheckState == Telerik.WinControls.Enumerations.ToggleState.On;
      }
      set
      {
        if (value == this.Checked)
          return;
        this.CheckState = !value ? Telerik.WinControls.Enumerations.ToggleState.Off : Telerik.WinControls.Enumerations.ToggleState.On;
        this.OnNotifyPropertyChanged(nameof (Checked));
      }
    }

    [DefaultValue(Telerik.WinControls.Enumerations.ToggleState.Off)]
    [Category("Behavior")]
    [Description("Gets or sets the check state of the RadTreeNode.")]
    public virtual Telerik.WinControls.Enumerations.ToggleState CheckState
    {
      get
      {
        if (this.dataBoundItem != null && this.TreeViewElement != null && !string.IsNullOrEmpty(this.TreeViewElement.CheckedMember))
        {
          TreeNodeDescriptor treeNodeDescriptor = this.TreeViewElement.bindingProvider[this.boundIndex];
          object obj = treeNodeDescriptor.GetChecked(this);
          if (treeNodeDescriptor.CheckedDescriptor != null)
          {
            if (treeNodeDescriptor.Converter.CanConvertFrom(treeNodeDescriptor.CheckedDescriptor.PropertyType))
              return (Telerik.WinControls.Enumerations.ToggleState) treeNodeDescriptor.Converter.ConvertFrom(obj);
            return !Convert.ToBoolean(obj) ? Telerik.WinControls.Enumerations.ToggleState.Off : Telerik.WinControls.Enumerations.ToggleState.On;
          }
        }
        return this.checkState;
      }
      set
      {
        if (!this.SetCheckStateCore(value))
          return;
        bool expanded = this.Expanded;
        if (this.TreeViewElement != null)
          this.TreeViewElement.BeginUpdate();
        this.UpdateChildrenCheckState();
        if (this.checkState == Telerik.WinControls.Enumerations.ToggleState.On)
          this.InvalidateOnStateInternal(true);
        this.UpdateParentCheckState();
        this.OnCheckStateChanged();
        if (this.TreeViewElement == null)
          return;
        this.TreeViewElement.EndUpdate(true, RadTreeViewElement.UpdateActions.StateChanged);
        if (expanded == this.Expanded)
          return;
        this.TreeViewElement.Update(RadTreeViewElement.UpdateActions.ExpandedChanged);
      }
    }

    public void InvalidateOnState()
    {
      this.InvalidateOnStateInternal(false);
    }

    public void InvalidateOnState(bool recursive)
    {
      if (!recursive)
        this.InvalidateOnStateInternal(false);
      else
        this.InvalidateOnStateRecursive(this);
    }

    private void InvalidateOnStateRecursive(RadTreeNode radTreeNode)
    {
      if (radTreeNode.nodes == null)
        return;
      foreach (RadTreeNode node in (Collection<RadTreeNode>) radTreeNode.nodes)
        this.InvalidateOnStateRecursive(node);
      radTreeNode.InvalidateOnStateInternal(false);
    }

    private void InvalidateOnStateInternal(bool indeterminateOnly)
    {
      if (this.TreeViewElement != null && !this.TreeViewElement.TriStateMode)
        return;
      int num1 = 0;
      int num2 = 0;
      Stack<RadTreeNodeCollection> treeNodeCollectionStack = new Stack<RadTreeNodeCollection>();
      treeNodeCollectionStack.Push(this.Nodes);
      while (treeNodeCollectionStack.Count > 0)
      {
        foreach (RadTreeNode radTreeNode in (Collection<RadTreeNode>) treeNodeCollectionStack.Pop())
        {
          if (radTreeNode.CheckType != CheckType.None && radTreeNode.Visible)
          {
            ++num2;
            Telerik.WinControls.Enumerations.ToggleState checkState = radTreeNode.CheckState;
            if (checkState == Telerik.WinControls.Enumerations.ToggleState.On)
              ++num1;
            else if (num1 > 0 || checkState == Telerik.WinControls.Enumerations.ToggleState.Indeterminate && this.TreeViewElement != null && this.TreeViewElement.TriStateMode)
            {
              this.SetCheckStateCore(Telerik.WinControls.Enumerations.ToggleState.Indeterminate);
              return;
            }
          }
        }
      }
      if (indeterminateOnly)
      {
        if (num2 == num1 || num2 == 0)
          return;
        this.SetCheckStateCore(Telerik.WinControls.Enumerations.ToggleState.Indeterminate);
      }
      else if (num1 == 0 && num2 != 0)
        this.SetCheckStateCore(Telerik.WinControls.Enumerations.ToggleState.Off);
      else if (num2 != num1)
      {
        this.SetCheckStateCore(Telerik.WinControls.Enumerations.ToggleState.Indeterminate);
      }
      else
      {
        if (num2 == 0)
          return;
        this.SetCheckStateCore(Telerik.WinControls.Enumerations.ToggleState.On);
      }
    }

    [DefaultValue(CheckType.None)]
    [Category("Behavior")]
    [Description("Gets or sets the check type of the RadTreeNode.")]
    public CheckType CheckType
    {
      get
      {
        if (this.checkType.HasValue)
          return this.checkType.Value;
        return this.TreeViewElement != null && this.TreeViewElement.CheckBoxes ? CheckType.CheckBox : CheckType.None;
      }
      set
      {
        CheckType? checkType1 = this.checkType;
        CheckType checkType2 = value;
        if ((checkType1.GetValueOrDefault() != checkType2 ? 1 : (!checkType1.HasValue ? 1 : 0)) == 0)
          return;
        this.checkType = new CheckType?(value);
        this.OnNotifyPropertyChanged(nameof (CheckType));
      }
    }

    [DefaultValue(null)]
    [Category("Behavior")]
    [Description("Gets or sets the shortcut menu associated to the node.")]
    public virtual RadContextMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        if (this.contextMenu == value)
          return;
        this.contextMenu = value;
        this.OnNotifyPropertyChanged(nameof (ContextMenu));
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the tree node is visible.")]
    public bool Visible
    {
      get
      {
        return this.state[16];
      }
      set
      {
        this.SetBooleanProperty(nameof (Visible), 16, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int Index
    {
      get
      {
        if (this.parent == null)
          return -1;
        return this.parent.Nodes.IndexOf(this);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsEditing
    {
      get
      {
        if (this.TreeViewElement != null && this.TreeViewElement.IsEditing)
          return this.TreeViewElement.SelectedNode == this;
        return false;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets a value indicating whether the tree node is in the selected state.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(false)]
    public bool Selected
    {
      get
      {
        return this.state[4];
      }
      set
      {
        this.SetBooleanProperty(nameof (Selected), 4, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets or sets a value indicating whether the tree node is in the current state.")]
    [DefaultValue(false)]
    public bool Current
    {
      get
      {
        return this.state[8];
      }
      set
      {
        this.SetBooleanProperty(nameof (Current), 8, value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTreeViewElement TreeViewElement
    {
      get
      {
        if (this.treeView == null)
          this.treeView = this.FindTreeView();
        return this.treeView;
      }
      internal set
      {
        if (value == this.treeView)
          return;
        this.treeView = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether this instance is expanded.")]
    [Browsable(false)]
    public bool Expanded
    {
      get
      {
        return this.state[2];
      }
      set
      {
        if (this.Expanded == value || this.TreeViewElement != null && !this.TreeViewElement.OnNodeExpandedChanging(this))
          return;
        if (value && this.treeView != null && this.treeView.IsLazyLoading)
        {
          int count = this.Nodes.Count;
        }
        this.SetBooleanProperty(nameof (Expanded), 2, value);
        this.OnExpandedChanged(value);
        this.NotifyExpandedChanged(this);
        if (this.TreeViewElement == null)
          return;
        this.TreeViewElement.OnNodeExpandedChanged(new RadTreeViewEventArgs(this));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTreeNode Parent
    {
      get
      {
        if (this.parent is RadTreeViewElement.RootTreeNode)
          return (RadTreeNode) null;
        return this.parent;
      }
      internal set
      {
        if (this.parent == value)
          return;
        if (value == null)
        {
          this.Current = false;
          this.Selected = false;
          this.treeView = (RadTreeViewElement) null;
          this.SubscribeToChildCollection((IBindingList) null);
        }
        this.parent = value;
        if (this.parent == null)
        {
          this.ClearChildrenState();
        }
        else
        {
          if (this.parent is RadTreeViewElement.RootTreeNode || this.TreeViewElement == null)
            return;
          int num = this.TreeViewElement.IsLazyLoading ? 1 : 0;
        }
      }
    }

    [DefaultValue(null)]
    [Category("Appearance")]
    [Description("Gets or sets the text displayed in the label of the tree node.")]
    public string Text
    {
      get
      {
        if (this.dataBoundItem != null && this.parent != null && (this.TreeViewElement != null && this.boundIndex < this.TreeViewElement.bindingProvider.BoundLevels))
        {
          TreeNodeDescriptor treeNodeDescriptor = this.TreeViewElement.bindingProvider[this.boundIndex];
          if (treeNodeDescriptor.DisplayDescriptor != null)
            return Convert.ToString(treeNodeDescriptor.GetDisplay(this));
        }
        return this.text;
      }
      set
      {
        if (!(this.Text != value))
          return;
        if (this.dataBoundItem != null && this.parent != null && this.TreeViewElement != null)
        {
          if (this.boundIndex < this.TreeViewElement.bindingProvider.BoundLevels)
          {
            TreeNodeDescriptor treeNodeDescriptor = this.TreeViewElement.bindingProvider[this.boundIndex];
            if (treeNodeDescriptor.DisplayDescriptor != null)
              treeNodeDescriptor.SetDisplay(this, (object) value);
          }
        }
        else
        {
          this.text = value;
          if (this.TreeViewElement != null && this.TreeViewElement.EditMode == TreeNodeEditMode.TextAndValue)
            this.value = (object) this.text;
        }
        this.OnNotifyPropertyChanged(nameof (Text));
        this.UpdateParent();
        this.Update(RadTreeViewElement.UpdateActions.ItemEdited);
      }
    }

    private void UpdateParent()
    {
      if (this.parent == null || this.parent.nodes == null || (this.treeView == null || this.treeView.bindingProvider.IsDataBound) || this.treeView.SortDescriptors.Count <= 0 && this.treeView.FilterDescriptors.Count <= 0)
        return;
      this.parent.nodes.UpdateView();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    [Description("Gets or sets the node value.")]
    [Category("Data")]
    public virtual object Value
    {
      get
      {
        if (this.dataBoundItem != null && this.TreeViewElement != null)
        {
          if (this.boundIndex < this.TreeViewElement.bindingProvider.BoundLevels)
          {
            TreeNodeDescriptor treeNodeDescriptor = this.TreeViewElement.bindingProvider[this.boundIndex];
            if (treeNodeDescriptor.ValueDescriptor != null)
              return treeNodeDescriptor.GetValue(this);
            if (treeNodeDescriptor.DisplayDescriptor != null)
              return treeNodeDescriptor.GetDisplay(this);
          }
          return this.dataBoundItem;
        }
        if (this.value != null)
          return this.value;
        return (object) null;
      }
      set
      {
        if (this.dataBoundItem != null && this.TreeViewElement != null)
        {
          if (this.boundIndex >= this.TreeViewElement.bindingProvider.BoundLevels)
            return;
          this.TreeViewElement.SuspendProvider();
          TreeNodeDescriptor treeNodeDescriptor = this.TreeViewElement.bindingProvider[this.boundIndex];
          if (treeNodeDescriptor.ValueDescriptor != null)
          {
            treeNodeDescriptor.SetValue(this, value);
            this.OnNotifyPropertyChanged(nameof (Value));
          }
          else if (treeNodeDescriptor.DisplayDescriptor != null)
          {
            treeNodeDescriptor.SetDisplay(this, value);
            this.OnNotifyPropertyChanged(nameof (Value));
            this.OnNotifyPropertyChanged("Text");
          }
          this.TreeViewElement.ResumeProvider();
        }
        else
        {
          this.value = value;
          if (this.TreeViewElement != null && this.TreeViewElement.EditMode == TreeNodeEditMode.TextAndValue)
            this.text = value.ToString();
          this.OnNotifyPropertyChanged(nameof (Value));
        }
      }
    }

    [Browsable(true)]
    [ListBindable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadTreeNodeCollection Nodes
    {
      get
      {
        if (this.nodes == null)
          this.nodes = new RadTreeNodeCollection(this);
        if (this.TreeViewElement != null)
        {
          if (this.nodes.NeedsRefresh)
          {
            this.TreeViewElement.BeginUpdate();
            this.TreeViewElement.bindingProvider.SuspendUpdate();
            this.nodes = new RadTreeNodeCollection(this);
            this.nodes.BeginUpdate();
            IList<RadTreeNode> nodes = this.treeView.TreeNodeProvider.GetNodes(this);
            if (nodes != null)
              this.nodes.AddRange((IEnumerable<RadTreeNode>) nodes);
            this.nodes.UpdateView();
            this.nodes.EndUpdate(false);
            this.nodes.SyncVersion();
            this.TreeViewElement.bindingProvider.ResumeUpdate();
            this.TreeViewElement.EndUpdate(false, RadTreeViewElement.UpdateActions.Resume);
          }
          else if (this.treeView.IsLazyLoading && this.nodes.IsEmpty)
          {
            this.nodes.BeginUpdate();
            if (this.TreeViewElement.OnNodesNeeded(new NodesNeededEventArgs(this == this.TreeViewElement.Root ? (RadTreeNode) null : this, (IList<RadTreeNode>) this.nodes)))
              this.nodes.UpdateView();
            this.nodes.EndUpdate(false);
            if (!this.nodes.IsEmpty)
              this.TreeViewElement.UpdateScrollersOnNodesNeeded(this);
          }
        }
        return this.nodes;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool HasNodes
    {
      get
      {
        if (this.treeView != null && (this.treeView.HasNodeProvider || this.treeView.IsLazyLoading))
          return this.Nodes.Count > 0;
        if (this.nodes != null)
          return this.Nodes.Count > 0;
        return false;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int Level
    {
      get
      {
        int num = 0;
        for (RadTreeNode parent = this.Parent; parent != null; parent = parent.Parent)
        {
          if (num > (int) short.MaxValue)
            throw new OverflowException(string.Format("RadTreeNode can contains {0} level of items.", (object) short.MaxValue));
          ++num;
        }
        return num;
      }
    }

    [Category("Appearance")]
    [DefaultValue("")]
    [Description("Gets or sets the name of the RadTreeNode.")]
    [Browsable(true)]
    [NotifyParentProperty(true)]
    public string Name
    {
      get
      {
        if (this.name != null)
          return this.name;
        if (this.dataBoundItem != null && this.parent != null && this.boundIndex < this.TreeViewElement.bindingProvider.BoundLevels)
        {
          TreeNodeDescriptor treeNodeDescriptor = this.TreeViewElement.bindingProvider[this.boundIndex];
          if (treeNodeDescriptor.DisplayDescriptor != null)
            return treeNodeDescriptor.DisplayDescriptor.GetValue(this.dataBoundItem).ToString();
        }
        return "";
      }
      set
      {
        if (!(this.name != value))
          return;
        this.name = value;
        this.OnNotifyPropertyChanged(nameof (Name));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTreeNode FirstNode
    {
      get
      {
        if (this.nodes == null || this.nodes.Count == 0)
          return (RadTreeNode) null;
        return this.nodes[0];
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTreeNode LastNode
    {
      get
      {
        if (this.nodes == null || this.nodes.Count == 0)
          return (RadTreeNode) null;
        return this.nodes[this.Nodes.Count - 1];
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTreeNode NextNode
    {
      get
      {
        if (this.parent == null)
          return (RadTreeNode) null;
        int num = this.parent.Nodes.IndexOf(this);
        if (num < 0)
          return (RadTreeNode) null;
        int index = num + 1;
        if (index < this.parent.Nodes.Count)
          return this.parent.Nodes[index];
        return (RadTreeNode) null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadTreeNode NextVisibleNode
    {
      get
      {
        int index1 = 0;
        if (this.Expanded)
        {
          for (; index1 < this.Nodes.Count; ++index1)
          {
            if (this.Nodes[index1].Visible || this.IsInDesignMode)
              return this.Nodes[index1];
          }
        }
        RadTreeNode radTreeNode = this;
        for (RadTreeNode parent = this.parent; parent != null; parent = parent.parent)
        {
          int num = parent.Nodes.IndexOf(radTreeNode);
          if (num < 0)
            return (RadTreeNode) null;
          for (int index2 = num + 1; index2 >= 0 && index2 < parent.Nodes.Count; ++index2)
          {
            if (parent.Nodes[index2].Visible || this.IsInDesignMode)
              return parent.Nodes[index2];
          }
          radTreeNode = parent;
        }
        return (RadTreeNode) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTreeNode PrevNode
    {
      get
      {
        if (this.parent == null)
          return (RadTreeNode) null;
        int index = this.parent.Nodes.IndexOf(this) - 1;
        if (index >= 0 && index < this.parent.Nodes.Count)
          return this.parent.Nodes[index];
        return (RadTreeNode) null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadTreeNode PrevVisibleNode
    {
      get
      {
        if (this.parent == null || this.treeView == null)
          return (RadTreeNode) null;
        for (int index = this.parent.Nodes.IndexOf(this) - 1; index >= 0; --index)
        {
          RadTreeNode node = this.parent.Nodes[index];
          if (node.Visible || this.IsInDesignMode)
          {
            if (node.Expanded)
            {
              RadTreeNode lastVisibleNode = this.GetLastVisibleNode(node);
              if (lastVisibleNode != null)
                return lastVisibleNode;
            }
            return node;
          }
        }
        if (this.parent == this.TreeViewElement.Root)
          return (RadTreeNode) null;
        return this.parent;
      }
    }

    [Description("Tag object that can be used to store user data, corresponding to the tree node")]
    [Localizable(false)]
    [TypeConverter(typeof (StringConverter))]
    [Category("Data")]
    [Bindable(true)]
    [DefaultValue(null)]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        if (this.tag == value)
          return;
        this.tag = value;
        this.OnNotifyPropertyChanged(nameof (Tag));
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the text that appears when the mouse pointer hovers over a tree node.")]
    [DefaultValue(null)]
    public string ToolTipText
    {
      get
      {
        return this.toolTipText;
      }
      set
      {
        if (!(this.toolTipText != value))
          return;
        this.toolTipText = value;
        this.OnNotifyPropertyChanged(nameof (ToolTipText));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FullPath
    {
      get
      {
        RadTreeViewElement treeViewElement = this.TreeViewElement;
        if (treeViewElement == null)
          throw new InvalidOperationException("FullPath cannot be accessed when the node is not added in Nodes collection.");
        StringBuilder path = new StringBuilder();
        this.GetFullPath(path, treeViewElement.PathSeparator);
        return path.ToString();
      }
    }

    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [Description("Gets or sets the image of the node.")]
    [Category("Appearance")]
    public virtual Image Image
    {
      get
      {
        if (this.image != null)
          return this.image;
        if (this.TreeViewElement == null)
          return (Image) null;
        int index = this.ImageIndex >= 0 ? this.ImageIndex : this.TreeViewElement.ImageIndex;
        if (index >= 0 && string.IsNullOrEmpty(this.ImageKey))
        {
          RadControl control = this.TreeViewElement.ElementTree.Control as RadControl;
          if (control != null)
          {
            ImageList imageList = control.ImageList;
            if (imageList != null && index < imageList.Images.Count)
              return imageList.Images[index];
          }
        }
        string str = (string) null;
        if (this.TreeViewElement != null)
          str = this.TreeViewElement.ImageKey;
        string key = string.IsNullOrEmpty(this.ImageKey) ? str : this.ImageKey;
        if (!string.IsNullOrEmpty(key))
        {
          RadControl control = this.TreeViewElement.ElementTree.Control as RadControl;
          if (control != null)
          {
            ImageList imageList = control.ImageList;
            if (imageList != null && imageList.Images.Count > 0 && imageList.Images.ContainsKey(key))
              return imageList.Images[key];
          }
        }
        return (Image) null;
      }
      set
      {
        if (this.image == value)
          return;
        this.image = value;
        this.OnNotifyPropertyChanged(nameof (Image));
        this.Update(RadTreeViewElement.UpdateActions.StateChanged);
      }
    }

    [Description("Gets or sets the left image list index value of the image displayed when the tree node is in the unselected state.")]
    [NotifyParentProperty(true)]
    [RelatedImageList("TreeView.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Category("Appearance")]
    [DefaultValue(-1)]
    public virtual int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        if (this.imageIndex == value)
          return;
        this.imageIndex = value;
        this.OnNotifyPropertyChanged(nameof (ImageIndex));
        this.Update(RadTreeViewElement.UpdateActions.StateChanged);
      }
    }

    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RelatedImageList("TreeView.ImageList")]
    [DefaultValue(null)]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets or sets the key for the left image associated with this tree node when the node is in an unselected state.")]
    [NotifyParentProperty(true)]
    [Category("Appearance")]
    public string ImageKey
    {
      get
      {
        return this.imageKey;
      }
      set
      {
        if (!(this.imageKey != value))
          return;
        this.imageKey = value;
        this.imageIndex = -1;
        this.OnNotifyPropertyChanged(nameof (ImageKey));
        this.Update(RadTreeViewElement.UpdateActions.StateChanged);
      }
    }

    [Description("Gets or sets the height of the tree node in the tree view control.")]
    [Browsable(true)]
    [NotifyParentProperty(true)]
    [DefaultValue(-1)]
    [Category("Layout")]
    public int ItemHeight
    {
      get
      {
        return this.itemHeight;
      }
      set
      {
        if (this.itemHeight == value)
          return;
        this.itemHeight = value;
        this.OnNotifyPropertyChanged(nameof (ItemHeight));
        this.Update(RadTreeViewElement.UpdateActions.Resume);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public Size ActualSize
    {
      get
      {
        return this.actualSize;
      }
      internal set
      {
        if (!(this.actualSize != value))
          return;
        this.OnActualSizeChanged(value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Size ChildrenSize
    {
      get
      {
        return this.childrenSize;
      }
      internal set
      {
        if (!(this.childrenSize != value))
          return;
        this.OnChildrenSizeChanged(value);
      }
    }

    [Browsable(false)]
    [Description("Gets or sets a value indicating whether [allow drop].")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowDrop
    {
      get
      {
        return this.state[64];
      }
      set
      {
        if (this.state[64] == value)
          return;
        this.state[64] = value;
        this.OnNotifyPropertyChanged(nameof (AllowDrop));
      }
    }

    [Description("Gets or  a value indicating whether the control is in design mode.")]
    [Browsable(false)]
    public bool IsInDesignMode
    {
      get
      {
        if (this.isInDesignMode.HasValue)
          return this.isInDesignMode.Value;
        this.isInDesignMode = new bool?(this.TreeViewElement != null && this.TreeViewElement.IsInDesignMode);
        return this.isInDesignMode.Value;
      }
    }

    public RadTreeNode Find(Predicate<RadTreeNode> match)
    {
      List<RadTreeNode> all = this.FindAll(match);
      if (all.Count == 0)
        return (RadTreeNode) null;
      if (all.Count == 1)
        return all[0];
      RadTreeNode radTreeNode = all[0];
      all.RemoveAt(0);
      radTreeNode.CacheLastFind(all);
      return radTreeNode;
    }

    public RadTreeNode Find<T>(FindAction<T> match, T arg)
    {
      List<RadTreeNode> all = this.FindAll<T>(match, arg);
      if (all.Count == 0)
        return (RadTreeNode) null;
      if (all.Count == 1)
        return all[0];
      RadTreeNode radTreeNode = all[0];
      all.RemoveAt(0);
      radTreeNode.CacheLastFind(all);
      return radTreeNode;
    }

    public RadTreeNode[] FindNodes(Predicate<RadTreeNode> match)
    {
      return this.FindAll(match).ToArray();
    }

    public RadTreeNode[] FindNodes<T>(FindAction<T> match, T arg)
    {
      return this.FindAll<T>(match, arg).ToArray();
    }

    public object Execute(ICommand command, params object[] settings)
    {
      return this.Execute(true, command, settings);
    }

    public object Execute(bool includeSubTrees, ICommand command, params object[] settings)
    {
      if (includeSubTrees)
      {
        IEnumerator<RadTreeNode> enumerator = this.Nodes.GetEnumerator();
        while (enumerator.MoveNext())
        {
          object obj = enumerator.Current.Execute(true, command, settings);
          if (obj != null)
            return obj;
        }
      }
      if (!command.CanExecute((object) this))
        return (object) null;
      return command.Execute((object) this, (object) settings);
    }

    public bool BeginEdit()
    {
      if (this.TreeViewElement == null)
        return false;
      this.TreeViewElement.SelectedNode = this;
      return this.TreeViewElement.BeginEdit();
    }

    public bool EndEdit()
    {
      if (this.TreeViewElement != null && this.TreeViewElement.IsEditing && this.TreeViewElement.SelectedNode == this)
        return this.TreeViewElement.EndEdit();
      return false;
    }

    public bool CancelEdit()
    {
      if (this.TreeViewElement == null || !this.TreeViewElement.IsEditing || this.TreeViewElement.SelectedNode != this)
        return false;
      this.TreeViewElement.CancelEdit();
      return true;
    }

    public void Collapse()
    {
      this.Collapse(false);
    }

    public void Collapse(bool ignoreChildren)
    {
      if (!ignoreChildren)
      {
        this.TreeViewElement.BeginUpdate();
        this.Expanded = false;
        IEnumerator<RadTreeNode> enumerator = this.Nodes.GetEnumerator();
        while (enumerator.MoveNext())
          enumerator.Current.Collapse(ignoreChildren);
        this.TreeViewElement.EndUpdate(true, RadTreeViewElement.UpdateActions.Resume);
      }
      else
        this.Expanded = false;
    }

    public void EnsureVisible()
    {
      this.TreeViewElement.EnsureVisible(this);
    }

    public void Expand()
    {
      this.Expanded = true;
      this.NotifyExpandedChanged((RadTreeNode) null);
    }

    public void ExpandAll()
    {
      this.Expand();
      IEnumerator<RadTreeNode> enumerator = this.Nodes.GetEnumerator();
      while (enumerator.MoveNext())
        enumerator.Current.ExpandAll();
      this.NotifyExpandedChanged((RadTreeNode) null);
    }

    public int GetNodeCount(bool includeSubTrees)
    {
      int count = this.Nodes.Count;
      if (includeSubTrees)
      {
        IEnumerator<RadTreeNode> enumerator = this.Nodes.GetEnumerator();
        while (enumerator.MoveNext())
          count += enumerator.Current.GetNodeCount(true);
      }
      return count;
    }

    public virtual void Remove()
    {
      if (this.parent == null)
        return;
      this.parent.nodes.Remove(this);
    }

    public void Toggle()
    {
      if (this.Expanded)
        this.Collapse(true);
      else
        this.Expand();
    }

    public override string ToString()
    {
      return "RadTreeNode: " + (this.Text == null ? "" : this.Text);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void RemoveTreeView()
    {
      this.TreeViewElement = (RadTreeViewElement) null;
    }

    private void ClearChildrenState()
    {
      Queue<RadTreeNodeCollection> treeNodeCollectionQueue = new Queue<RadTreeNodeCollection>();
      if (this.nodes != null)
        treeNodeCollectionQueue.Enqueue(this.nodes);
      if (this.parent == null)
      {
        this.ActualSize = Size.Empty;
        this.ChildrenSize = Size.Empty;
      }
      while (treeNodeCollectionQueue.Count > 0)
      {
        foreach (RadTreeNode radTreeNode in (Collection<RadTreeNode>) treeNodeCollectionQueue.Dequeue())
        {
          if (this.parent == null)
          {
            radTreeNode.ActualSize = Size.Empty;
            radTreeNode.ChildrenSize = Size.Empty;
          }
          radTreeNode.Current = false;
          radTreeNode.Selected = false;
          radTreeNode.treeView = (RadTreeViewElement) null;
          radTreeNode.SubscribeToChildCollection((IBindingList) null);
          if (radTreeNode.nodes != null && radTreeNode.nodes.Count > 0)
            treeNodeCollectionQueue.Enqueue(radTreeNode.nodes);
        }
      }
    }

    protected internal void CacheLastFind(List<RadTreeNode> nodes)
    {
      this.matches = new WeakReference((object) nodes);
    }

    private List<RadTreeNode> FindAll(Predicate<RadTreeNode> match)
    {
      List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>();
      if (this.CanPerformFind && match(this))
        radTreeNodeList.Add(this);
      IEnumerator<RadTreeNode> enumerator = this.Nodes.GetEnumerator();
      while (enumerator.MoveNext())
      {
        List<RadTreeNode> all = enumerator.Current.FindAll(match);
        for (int index = 0; index < all.Count; ++index)
          radTreeNodeList.Add(all[index]);
      }
      return radTreeNodeList;
    }

    private List<RadTreeNode> FindAll<T>(FindAction<T> match, T arg)
    {
      List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>();
      if (this.CanPerformFind && match(this, arg))
        radTreeNodeList.Add(this);
      IEnumerator<RadTreeNode> enumerator = this.Nodes.GetEnumerator();
      while (enumerator.MoveNext())
      {
        List<RadTreeNode> all = enumerator.Current.FindAll<T>(match, arg);
        for (int index = 0; index < all.Count; ++index)
          radTreeNodeList.Add(all[index]);
      }
      return radTreeNodeList;
    }

    public void ForEach(Action<RadTreeNode> action)
    {
      Stack<RadTreeNode> radTreeNodeStack = new Stack<RadTreeNode>();
      radTreeNodeStack.Push(this);
      this.Nodes.GetEnumerator();
      while (radTreeNodeStack.Count > 0)
      {
        RadTreeNode radTreeNode = radTreeNodeStack.Pop();
        action(radTreeNode);
        for (int index = 0; index < radTreeNode.Nodes.Count; ++index)
          radTreeNodeStack.Push(radTreeNode.Nodes[index]);
      }
    }

    [Browsable(false)]
    [Description("Gets a value if the node is root node")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool IsRootNode
    {
      get
      {
        return this.parent == null || this.parent is RadTreeViewElement.RootTreeNode;
      }
    }

    private void Update(RadTreeViewElement.UpdateActions updateAction)
    {
      if (this.state[1] || this.treeView == null)
        return;
      this.treeView.Update(updateAction, this);
    }

    private RadTreeNode GetLastVisibleNode(RadTreeNode parent)
    {
      for (int index = parent.Nodes.Count - 1; index >= 0; --index)
      {
        RadTreeNode node = parent.Nodes[index];
        if (node.Visible || this.IsInDesignMode)
        {
          if (node.Expanded && node.Nodes.Count > 0)
            return this.GetLastVisibleNode(node);
          return node;
        }
      }
      return (RadTreeNode) null;
    }

    private RadTreeViewElement FindTreeView()
    {
      RadTreeNode radTreeNode = this;
      while (radTreeNode.parent != null)
        radTreeNode = radTreeNode.parent;
      return radTreeNode.treeView;
    }

    internal int BoundIndex
    {
      get
      {
        return this.boundIndex;
      }
      set
      {
        this.boundIndex = value;
      }
    }

    private void GetFullPath(StringBuilder path, string pathSeparator)
    {
      if (this.parent == null)
        return;
      this.parent.GetFullPath(path, pathSeparator);
      if (this.parent.parent != null)
        path.Append(pathSeparator);
      path.Append(this.Text);
    }

    protected virtual bool SetCheckStateCore(Telerik.WinControls.Enumerations.ToggleState value)
    {
      if (this.CheckState == value || this.TreeViewElement != null && !this.TreeViewElement.OnNodeCheckedChanging(this))
        return false;
      if (this.dataBoundItem != null && this.TreeViewElement != null && (this.parent != null && this.boundIndex < this.TreeViewElement.bindingProvider.BoundLevels))
      {
        TreeNodeDescriptor treeNodeDescriptor = this.TreeViewElement.bindingProvider[this.boundIndex];
        if (treeNodeDescriptor.CheckedDescriptor != null)
          treeNodeDescriptor.SetChecked(this, (object) value);
      }
      this.checkState = value;
      return true;
    }

    protected virtual void OnCheckStateChanged()
    {
      this.OnNotifyPropertyChanged("CheckState");
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeCheckedChanged(this, CheckedMode.Normal);
    }

    protected virtual void OnCheckStateChanged(CheckedMode checkedMode)
    {
      this.OnNotifyPropertyChanged("CheckState");
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.OnNodeCheckedChanged(this, checkedMode);
    }

    protected internal virtual void UpdateParentCheckState()
    {
      if (!this.Visible || this.CheckType == CheckType.None || this.Parent == null && this.CheckType == CheckType.CheckBox)
        return;
      if (this.TreeViewElement != null)
        this.TreeViewElement.BeginUpdate();
      int num = 0;
      if (this.CheckType == CheckType.RadioButton)
      {
        if (this.parent != null)
        {
          foreach (RadTreeNode node in (Collection<RadTreeNode>) this.parent.Nodes)
          {
            if (node != this && node.CheckType == this.CheckType && node.SetCheckStateCore(Telerik.WinControls.Enumerations.ToggleState.Off))
            {
              node.OnCheckStateChanged(CheckedMode.ParentChild);
              ++num;
            }
          }
        }
      }
      else
        num += this.UpdateParentCheckBoxes();
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.EndUpdate(num > 0, RadTreeViewElement.UpdateActions.StateChanged);
    }

    private void UpdateParentStateOnPropertyChange()
    {
      if (this.TreeViewElement == null || !this.TreeViewElement.TriStateMode || this.Parent == null)
        return;
      RadTreeNode parent = this.Parent;
      bool flag = true;
      foreach (RadTreeNode node in (Collection<RadTreeNode>) parent.Nodes)
      {
        if (node.Visible && node.CheckType == parent.CheckType)
          flag &= node.CheckState == Telerik.WinControls.Enumerations.ToggleState.On;
      }
      if (!flag)
        return;
      parent.SetCheckStateCore(Telerik.WinControls.Enumerations.ToggleState.On);
      parent.UpdateParentCheckState();
    }

    private int UpdateParentCheckBoxes()
    {
      if (this.TreeViewElement != null && !this.TreeViewElement.TriStateMode)
        return 0;
      int num = 0;
      Stack<RadTreeNode> radTreeNodeStack = new Stack<RadTreeNode>();
      radTreeNodeStack.Push(this.parent);
      while (radTreeNodeStack.Count > 0)
      {
        RadTreeNode radTreeNode = radTreeNodeStack.Pop();
        if (radTreeNode.CheckType == this.CheckType)
        {
          bool flag = true;
          foreach (RadTreeNode node in (Collection<RadTreeNode>) radTreeNode.Nodes)
          {
            if (node.CheckType != CheckType.None && node.Visible && node.CheckState != this.CheckState)
            {
              flag = false;
              break;
            }
          }
          Telerik.WinControls.Enumerations.ToggleState toggleState = this.CheckState;
          if (!flag)
            toggleState = Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
          if (radTreeNode.SetCheckStateCore(toggleState))
          {
            radTreeNode.OnCheckStateChanged(CheckedMode.ParentChild);
            ++num;
          }
          if (radTreeNode.Parent != null)
            radTreeNodeStack.Push(radTreeNode.Parent);
        }
      }
      return num;
    }

    protected virtual void UpdateChildrenCheckState()
    {
      if (this.CheckType != CheckType.CheckBox || this.TreeViewElement != null && !this.TreeViewElement.TriStateMode || this.TreeViewElement != null && !this.TreeViewElement.AutoCheckChildNodes)
        return;
      if (this.TreeViewElement != null)
        this.TreeViewElement.BeginUpdate();
      int num = 0;
      Stack<RadTreeNodeCollection> treeNodeCollectionStack = new Stack<RadTreeNodeCollection>();
      treeNodeCollectionStack.Push(this.Nodes);
      while (treeNodeCollectionStack.Count > 0)
      {
        foreach (RadTreeNode radTreeNode in (Collection<RadTreeNode>) treeNodeCollectionStack.Pop())
        {
          if (radTreeNode.CheckType != CheckType.None && radTreeNode.Visible)
          {
            if (radTreeNode.SetCheckStateCore(this.CheckState))
            {
              radTreeNode.OnCheckStateChanged(CheckedMode.ParentChild);
              ++num;
            }
            if (radTreeNode.Nodes.Count > 0)
              treeNodeCollectionStack.Push(radTreeNode.nodes);
          }
        }
      }
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.EndUpdate(num > 0, RadTreeViewElement.UpdateActions.StateChanged);
    }

    private void OnChildrenSizeChanged(Size newChildrenSize)
    {
      if (this.parent != null && !this.state[1])
        this.parent.ChildrenSize = this.UpdateChildrenSize(this.parent.childrenSize, newChildrenSize, this.childrenSize);
      this.childrenSize = newChildrenSize;
      if (!(newChildrenSize == Size.Empty))
        return;
      this.UpdateActualSizeOnExpand = true;
    }

    private void OnActualSizeChanged(Size newSize)
    {
      if (this.parent != null && !this.state[1])
      {
        Size newSize1 = newSize;
        if (this.TreeViewElement != null)
        {
          newSize1.Height += this.treeView.NodeSpacing;
          if (this.actualSize.Height != 0)
            this.actualSize.Height += this.treeView.NodeSpacing;
        }
        this.parent.ChildrenSize = this.UpdateChildrenSize(this.parent.childrenSize, newSize1, this.actualSize);
      }
      this.actualSize = newSize;
    }

    private void OnExpandedChanged(bool expanded)
    {
      if (!this.state[128])
        this.state[128] = true;
      else if (expanded)
      {
        if (this.parent == null || this.childrenSize.Height == 0)
          return;
        Size childrenSize = this.parent.ChildrenSize;
        if (childrenSize.Height != 0)
          childrenSize.Height += this.childrenSize.Height;
        childrenSize.Width = Math.Max(this.childrenSize.Width, childrenSize.Width);
        this.parent.ChildrenSize = childrenSize;
      }
      else
      {
        if (this.parent == null)
          return;
        Size childrenSize = this.parent.ChildrenSize;
        if (this.childrenSize.Height != 0 && childrenSize.Height != 0)
          childrenSize.Height -= this.childrenSize.Height;
        if (this.childrenSize.Width != 0 && childrenSize.Width == this.childrenSize.Width)
        {
          int val1 = 0;
          foreach (RadTreeNode node in (Collection<RadTreeNode>) this.parent.Nodes)
          {
            if ((node.Visible || this.IsInDesignMode) && (node.Expanded && node != this))
              val1 = Math.Max(val1, node.ChildrenSize.Width);
            val1 = Math.Max(val1, node.ActualSize.Width);
          }
          childrenSize.Width = val1;
        }
        this.parent.ChildrenSize = childrenSize;
      }
    }

    protected virtual void OnDataBoundItemChanged(object oldItem, object newItem)
    {
    }

    private Size UpdateChildrenSize(Size childrenSize, Size newSize, Size oldSize)
    {
      if (newSize.Width > childrenSize.Width)
        childrenSize.Width = newSize.Width;
      else if (newSize.Width < childrenSize.Width && oldSize.Width == childrenSize.Width)
      {
        childrenSize.Width = newSize.Width;
        foreach (RadTreeNode node in (Collection<RadTreeNode>) this.parent.Nodes)
        {
          if ((node.Visible || this.IsInDesignMode) && (node.Expanded && node != this))
            childrenSize.Width = Math.Max(childrenSize.Width, node.ChildrenSize.Width);
          if (node != this)
            childrenSize.Width = Math.Max(childrenSize.Width, node.ActualSize.Width);
        }
      }
      if (newSize.Height != oldSize.Height)
      {
        if (oldSize.Height != 0)
          childrenSize.Height += newSize.Height - oldSize.Height;
        else
          childrenSize.Height += newSize.Height;
      }
      childrenSize.Height = Math.Max(0, childrenSize.Height);
      return childrenSize;
    }

    public object Clone()
    {
      System.Type type = this.GetType();
      RadTreeNode radTreeNode = (object) type != (object) typeof (RadTreeNode) ? (RadTreeNode) Activator.CreateInstance(type) : new RadTreeNode(this.Text);
      radTreeNode.Text = this.Text;
      radTreeNode.Name = this.name;
      radTreeNode.ToolTipText = this.toolTipText;
      radTreeNode.ContextMenu = this.contextMenu;
      radTreeNode.ItemHeight = this.ItemHeight;
      radTreeNode.Visible = this.Visible;
      radTreeNode.Image = this.Image;
      radTreeNode.ImageKey = this.ImageKey;
      radTreeNode.ImageIndex = this.ImageIndex;
      radTreeNode.Tag = this.Tag;
      radTreeNode.ToolTipText = this.ToolTipText;
      radTreeNode.Enabled = this.Enabled;
      if (this.style != null)
      {
        radTreeNode.ForeColor = this.Style.ForeColor;
        radTreeNode.BackColor = this.Style.BackColor;
        radTreeNode.BackColor2 = this.Style.BackColor2;
        radTreeNode.BackColor3 = this.Style.BackColor3;
        radTreeNode.BackColor4 = this.Style.BackColor4;
        radTreeNode.GradientAngle = this.Style.GradientAngle;
        radTreeNode.GradientStyle = this.Style.GradientStyle;
        radTreeNode.GradientPercentage = this.Style.GradientPercentage;
        radTreeNode.GradientPercentage2 = this.Style.GradientPercentage2;
        radTreeNode.NumberOfColors = this.Style.NumberOfColors;
        radTreeNode.TextAlignment = this.Style.TextAlignment;
        radTreeNode.Font = this.Style.Font;
        radTreeNode.BorderColor = this.Style.BorderColor;
      }
      foreach (RadTreeNode node in (Collection<RadTreeNode>) this.Nodes)
        radTreeNode.Nodes.Add((RadTreeNode) node.Clone());
      radTreeNode.Expanded = this.Expanded;
      radTreeNode.CheckState = this.CheckState;
      radTreeNode.CheckType = this.CheckType;
      radTreeNode.Tag = this.Tag;
      return (object) radTreeNode;
    }

    [Browsable(false)]
    public object DataBoundItem
    {
      get
      {
        return ((IDataItem) this).DataBoundItem;
      }
    }

    object IDataItem.DataBoundItem
    {
      get
      {
        return this.dataBoundItem;
      }
      set
      {
        if (value == this.dataBoundItem)
          return;
        object dataBoundItem = this.dataBoundItem;
        this.dataBoundItem = value;
        this.OnDataBoundItemChanged(dataBoundItem, this.dataBoundItem);
      }
    }

    int IDataItem.FieldCount
    {
      get
      {
        return 1;
      }
    }

    object IDataItem.this[string name]
    {
      get
      {
        return (object) this.Text;
      }
      set
      {
        this.Text = value.ToString();
      }
    }

    object IDataItem.this[int index]
    {
      get
      {
        return (object) this.Text;
      }
      set
      {
        this.Text = value.ToString();
      }
    }

    int IDataItem.IndexOf(string name)
    {
      return 0;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void SuspendPropertyNotifications()
    {
      this.state[1] = true;
    }

    public void ResumePropertyNotifications()
    {
      this.state[1] = false;
    }

    protected virtual bool SetBooleanProperty(string propertyName, int propertyKey, bool value)
    {
      if (this.state[propertyKey] == value)
        return false;
      this.state[propertyKey] = value;
      if (!this.state[1])
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
      return true;
    }

    protected virtual void OnNotifyPropertyChanged(string name)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(name));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs args)
    {
      if (this.TreeViewElement != null)
      {
        if (args.PropertyName == "CheckType")
        {
          this.UpdateParentStateOnPropertyChange();
          this.TreeViewElement.Update(RadTreeViewElement.UpdateActions.StateChanged, this);
        }
        else if (args.PropertyName == "Visible")
        {
          this.UpdateParentStateOnPropertyChange();
          this.TreeViewElement.Update(RadTreeViewElement.UpdateActions.Resume);
        }
        else if (args.PropertyName == "Current")
        {
          if (!this.TreeViewElement.ProcessCurrentNode(this.Current ? this : (RadTreeNode) null, true, RadTreeViewAction.Unknown))
          {
            this.state[8] = !this.Current;
            return;
          }
          if (this.TreeViewElement != null && !this.TreeViewElement.MultiSelect)
            this.Selected = this.Current;
        }
        else if (args.PropertyName == "Selected")
        {
          if (this.TreeViewElement != null && !this.TreeViewElement.MultiSelect)
          {
            this.Current = this.Selected;
            if (this.Current != this.Selected)
            {
              this.state[4] = !this.Selected;
              return;
            }
          }
          this.TreeViewElement.SelectedNodes.ProcessSelectedNode(this);
        }
      }
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, args);
    }

    protected virtual void NotifyExpandedChanged(RadTreeNode node)
    {
      RadTreeViewElement.UpdateActions updateAction = RadTreeViewElement.UpdateActions.ExpandedChanged;
      RadTreeViewElement treeViewElement = this.TreeViewElement;
      if (treeViewElement == null)
        return;
      if (node != null)
        treeViewElement.Update(updateAction, node);
      else
        treeViewElement.Update(updateAction);
    }

    private void Style_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (this.TreeViewElement == null)
        return;
      this.TreeViewElement.Update(RadTreeViewElement.UpdateActions.StateChanged, this);
    }

    [Category("Appearance")]
    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    [Description("Gets or sets the font of the node text.")]
    public Font Font
    {
      get
      {
        if (this.HasStyle)
          return this.Style.Font;
        return (Font) null;
      }
      set
      {
        if (this.Font == value)
          return;
        this.Style.Font = value;
      }
    }

    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "")]
    [Description("Gets or sets the foreground color of the tree node. This color is applied to the text label.")]
    public virtual Color ForeColor
    {
      get
      {
        if (this.HasStyle)
          return this.Style.ForeColor;
        return Color.Empty;
      }
      set
      {
        if (!(this.ForeColor != value))
          return;
        this.Style.ForeColor = value;
      }
    }

    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "")]
    [Description("Gets or sets the first back color.")]
    public Color BackColor
    {
      get
      {
        if (this.HasStyle)
          return this.Style.BackColor;
        return Color.Empty;
      }
      set
      {
        if (!(this.BackColor != value))
          return;
        this.Style.BackColor = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "")]
    [Description("Gets or sets the backcolor of the tree node.")]
    public Color BackColor2
    {
      get
      {
        if (this.HasStyle)
          return this.Style.BackColor2;
        return Color.Empty;
      }
      set
      {
        if (!(this.BackColor2 != value))
          return;
        this.Style.BackColor2 = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [Category("Appearance")]
    [NotifyParentProperty(true)]
    [DefaultValue(typeof (Color), "")]
    [Description("Gets or sets the backcolor of the tree node.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BackColor3
    {
      get
      {
        if (this.HasStyle)
          return this.Style.BackColor3;
        return Color.Empty;
      }
      set
      {
        if (!(this.BackColor3 != value))
          return;
        this.Style.BackColor3 = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [Category("Appearance")]
    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof (Color), "")]
    [Description("Gets or sets the backcolor of the tree node.")]
    public Color BackColor4
    {
      get
      {
        if (this.HasStyle)
          return this.Style.BackColor4;
        return Color.Empty;
      }
      set
      {
        if (!(this.BackColor4 != value))
          return;
        this.Style.BackColor4 = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "")]
    [Description("Gets or sets the border color of the tree node.")]
    public Color BorderColor
    {
      get
      {
        if (this.HasStyle)
          return this.Style.BorderColor;
        return Color.Empty;
      }
      set
      {
        if (!(this.BorderColor != value))
          return;
        this.Style.BorderColor = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [Category("Appearance")]
    [NotifyParentProperty(true)]
    [DefaultValue(90f)]
    [Description("Gets or sets gradient angle for linear gradient.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float GradientAngle
    {
      get
      {
        if (this.HasStyle)
          return this.Style.GradientAngle;
        return 90f;
      }
      set
      {
        if ((double) this.GradientAngle == (double) value)
          return;
        this.Style.GradientAngle = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [DefaultValue(0.5f)]
    [Description("Gets or sets GradientPercentage for linear, glass, office glass, gel, vista and radial gradients.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float GradientPercentage
    {
      get
      {
        if (this.HasStyle)
          return this.Style.GradientPercentage;
        return 0.5f;
      }
      set
      {
        if ((double) this.GradientPercentage == (double) value)
          return;
        this.Style.GradientPercentage = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [DefaultValue(0.5f)]
    [Category("Appearance")]
    [NotifyParentProperty(true)]
    [Description("Gets or sets GradientPercentage for office glass, vista, and radial gradients.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public float GradientPercentage2
    {
      get
      {
        if (this.HasStyle)
          return this.Style.GradientPercentage2;
        return 0.5f;
      }
      set
      {
        if ((double) this.GradientPercentage2 == (double) value)
          return;
        this.Style.GradientPercentage2 = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [DefaultValue(GradientStyles.Linear)]
    [Category("Appearance")]
    [NotifyParentProperty(true)]
    [Description("Gets or sets the gradient angle.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public GradientStyles GradientStyle
    {
      get
      {
        if (this.HasStyle)
          return this.Style.GradientStyle;
        return GradientStyles.Linear;
      }
      set
      {
        if (this.GradientStyle == value)
          return;
        this.Style.GradientStyle = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [DefaultValue(4)]
    [Description("Gets or sets the number of used colors in the gradient effect.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int NumberOfColors
    {
      get
      {
        if (this.HasStyle)
          return this.Style.NumberOfColors;
        return 4;
      }
      set
      {
        if (this.NumberOfColors == value)
          return;
        this.Style.NumberOfColors = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Description("Gets or sets the text alignment.")]
    public ContentAlignment TextAlignment
    {
      get
      {
        if (this.HasStyle)
          return this.Style.TextAlignment;
        return ContentAlignment.MiddleLeft;
      }
      set
      {
        if (this.TextAlignment == value)
          return;
        this.Style.TextAlignment = value;
        this.OnNotifyPropertyChanged("Style");
      }
    }

    internal void SubscribeToChildCollection(IBindingList bindingList)
    {
      this.SetBoundChildNodesList(bindingList);
    }

    protected virtual void SetBoundChildNodesList(IBindingList bindingList)
    {
      if (bindingList == this.boundChildrenCollection)
        return;
      if (this.boundChildrenCollection != null)
        this.boundChildrenCollection.ListChanged -= new ListChangedEventHandler(this.childCollection_ListChanged);
      this.boundChildrenCollection = bindingList;
      if (this.boundChildrenCollection == null)
        return;
      this.boundChildrenCollection.ListChanged += new ListChangedEventHandler(this.childCollection_ListChanged);
    }

    private void childCollection_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (this.treeView == null || this.treeView.bindingProvider == null || this.treeView.bindingProvider.IsSuspended)
        return;
      this.Nodes.Refresh();
    }
  }
}
