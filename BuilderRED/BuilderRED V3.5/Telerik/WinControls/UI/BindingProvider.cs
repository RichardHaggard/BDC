// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BindingProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Telerik.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class BindingProvider : TreeNodeProvider
  {
    private const int InitializedDescriptorState = 1;
    private const int InitializedDisplayState = 2;
    private const int SelfReferenceState = 4;
    private bool postponeSelection;
    private RelationBinding root;
    private BindingContext bindingContext;
    private RelationBindingCollection relationBindings;
    private BindingProvider.MetadataList map;
    private bool objectRelationalMode;
    private TypeConverter toggleStateConverter;
    private WeakReference addRef;

    public BindingProvider(RadTreeViewElement treeView)
      : base(treeView)
    {
      this.root = new RelationBinding();
      this.root.PropertyChanged += new PropertyChangedEventHandler(this.root_PropertyChanged);
      this.relationBindings = new RelationBindingCollection();
      this.relationBindings.CollectionChanged += new Telerik.WinControls.Data.NotifyCollectionChangedEventHandler(this.RelationBindings_CollectionChanged);
      this.map = new BindingProvider.MetadataList();
    }

    public bool PreProcess(RadTreeNode parent, RadTreeNode item, params object[] metadata)
    {
      if (this.IsSuspended || metadata == null || metadata.Length == 0)
        return true;
      string str = (string) metadata[0];
      if (str == "Add" || str == "Insert")
        return this.AddToSource(parent, item);
      if (str == "Remove")
        return this.RemoveFromSource(parent, item);
      return true;
    }

    public bool PostProcess(RadTreeNode parent, RadTreeNode item, params object[] metadata)
    {
      if (this.IsSuspended || metadata == null || (metadata.Length == 0 || !((string) metadata[0] == "Remove")))
        return true;
      return this.RemoveFromSource(parent, item);
    }

    public override IList<RadTreeNode> GetNodes(RadTreeNode parent)
    {
      if (this.IsDataBound)
      {
        if (this.objectRelationalMode)
          return this.GetObjectNodes(parent);
        if (parent == null)
          parent = this.TreeView.Root;
        int boundIndex = parent.BoundIndex;
        this.BuildIndex(boundIndex);
        IList<RadTreeNode> radTreeNodeList = this.BuildNodes(parent, boundIndex);
        if (radTreeNodeList != null && radTreeNodeList.Count > 0)
          return radTreeNodeList;
        int index = boundIndex + 1;
        if (index < this.map.Count && parent != this.TreeView.Root)
        {
          this.BuildIndex(index);
          return this.BuildNodes(parent, index);
        }
      }
      return (IList<RadTreeNode>) NotifyCollection<RadTreeNode>.Empty;
    }

    public override void Reset()
    {
      if (this.IsSuspended)
        return;
      this.objectRelationalMode = this.ContainsObjectRelation;
      if (this.objectRelationalMode)
      {
        this.RegisterObjectRelation();
        this.SuspendUpdate();
        this.TreeView.Root.nodes.ResetVersion();
        this.TreeView.Update(RadTreeViewElement.UpdateActions.Reset);
        this.ResumeUpdate();
      }
      else
      {
        if (this.root.DataSource != null)
        {
          this.RegisterMetadata(this.root, 0);
          for (int index = 0; index < this.relationBindings.Count; ++index)
            this.RegisterMetadata(this.relationBindings[index], -1);
        }
        this.SuspendUpdate();
        this.TreeView.Root.Nodes.ResetVersion();
        this.TreeView.Update(RadTreeViewElement.UpdateActions.Reset);
        this.ResumeUpdate();
      }
    }

    public override void Dispose()
    {
      this.root.PropertyChanged -= new PropertyChangedEventHandler(this.root_PropertyChanged);
      this.relationBindings.CollectionChanged -= new Telerik.WinControls.Data.NotifyCollectionChangedEventHandler(this.RelationBindings_CollectionChanged);
      for (int index = 0; index < this.map.Count; ++index)
        this.UnwireEvents(this.map[index].Source);
      this.map = (BindingProvider.MetadataList) null;
      this.root = (RelationBinding) null;
      this.bindingContext = (BindingContext) null;
      this.relationBindings = (RelationBindingCollection) null;
      base.Dispose();
    }

    public int GetLevelVersion(int level)
    {
      if (level >= 0 && level < this.map.Count)
        return this.map[level].Version;
      return -1;
    }

    public override void SetCurrent(RadTreeNode node)
    {
      if (node == null || this.IsSuspended || (node.BoundIndex < 0 || node.BoundIndex >= this.map.Count) || this.map[node.BoundIndex].Source == null)
        return;
      this.SuspendUpdate();
      CurrencyManager source = this.map[node.BoundIndex].Source;
      int num = source.List.IndexOf(node.DataBoundItem);
      if (num >= 0)
      {
        if (source.Position != num)
        {
          try
          {
            source.Position = num;
          }
          catch (Exception ex)
          {
            this.TreeView.OnDataError(new TreeNodeDataErrorEventArgs(ex.Message, node, new object[1]
            {
              (object) ex
            }));
          }
        }
      }
      this.ResumeUpdate();
    }

    public void DropNodes(RadTreeNode parent, List<RadTreeNode> draggedNodes)
    {
      if (parent == null)
        parent = this.TreeView.Root;
      if (this.objectRelationalMode)
        this.DropObjectNodes(parent, draggedNodes);
      else
        this.DropRelationNodes(parent, draggedNodes);
      if (draggedNodes.Count <= 0)
        return;
      this.TreeView.Update(RadTreeViewElement.UpdateActions.Resume);
    }

    private void DropObjectNodes(RadTreeNode parent, List<RadTreeNode> draggedNodes)
    {
      for (int index = 0; index < draggedNodes.Count; ++index)
      {
        RadTreeNode draggedNode = draggedNodes[index];
        if (draggedNode.treeView != parent.treeView)
          break;
        BindingProvider.Metadata metadata = this.map[draggedNode.BoundIndex];
        if (parent.Level != draggedNode.Level - 1 || index > 0 && draggedNodes[index - 1].Level != draggedNode.Level)
          break;
        draggedNode.parent.Nodes.Remove(draggedNode);
        parent.Nodes.Add(draggedNode);
      }
    }

    private void DropRelationNodes(RadTreeNode parent, List<RadTreeNode> draggedNodes)
    {
      for (int index = 0; index < draggedNodes.Count; ++index)
      {
        RadTreeNode draggedNode = draggedNodes[index];
        if (draggedNode.treeView != parent.treeView || draggedNode.BoundIndex >= this.map.Count)
          break;
        BindingProvider.Metadata metadata = this.map[draggedNode.BoundIndex];
        if (index > 0 && draggedNodes[index - 1].Level != draggedNode.Level || !metadata.State[4] && draggedNode.BoundIndex - 1 != parent.BoundIndex || !metadata.State[4] && draggedNode.BoundIndex - 1 == 0 && this.TreeView.Root == parent)
          break;
        if (draggedNode.parent != null)
          draggedNode.parent.nodes.ResetVersion();
        object key1;
        object key2;
        if (metadata.State[4])
        {
          key1 = this.GetParentKey(draggedNode.BoundIndex, draggedNode.DataBoundItem);
          object dataBoundItem1 = parent.DataBoundItem;
          if (parent == this.TreeView.Root)
          {
            if (this.TreeView.Root.nodes.Count != 0)
            {
              object dataBoundItem2 = parent.nodes[0].DataBoundItem;
              key2 = this.GetParentKey(draggedNode.BoundIndex, dataBoundItem2);
            }
            else
              continue;
          }
          else
            key2 = this.GetChildKey(draggedNode.BoundIndex, dataBoundItem1);
        }
        else
        {
          key1 = this.GetChildKey(draggedNode.BoundIndex, draggedNode.DataBoundItem);
          key2 = this.GetParentKey(draggedNode.BoundIndex, parent.DataBoundItem);
        }
        metadata.Tree.RemoveNode(key1, (object) draggedNode);
        if (metadata.State[4])
          this.SetParentKey(draggedNode.BoundIndex, draggedNode.DataBoundItem, key2);
        else
          this.SetChildKey(draggedNode.BoundIndex, draggedNode.DataBoundItem, key2);
        metadata.Tree.AddNode(key2, (object) draggedNode);
        parent.nodes.ResetVersion();
      }
    }

    public TreeNodeDescriptor this[int index]
    {
      get
      {
        if (index >= 0 && index < this.map.Count)
          return this.map[index].Descriptor;
        return (TreeNodeDescriptor) null;
      }
    }

    public bool CanDrop
    {
      get
      {
        return this.IsDataBound;
      }
    }

    public int BoundLevels
    {
      get
      {
        return this.map.Count;
      }
    }

    public bool IsDataBound
    {
      get
      {
        return this.map.Count > 0;
      }
    }

    public RelationBindingCollection RelationBindings
    {
      get
      {
        return this.relationBindings;
      }
    }

    public BindingContext BindingContext
    {
      get
      {
        return this.bindingContext;
      }
      set
      {
        if (this.bindingContext == value)
          return;
        this.bindingContext = value;
        if (!this.NeedsRefresh)
          return;
        this.Reset();
      }
    }

    public object DataSource
    {
      get
      {
        return this.root.DataSource;
      }
      set
      {
        if (this.root.DataSource == value)
          return;
        this.TreeView.SuspendPropertyNotifications();
        this.TreeView.SelectedNodes.Clear();
        this.TreeView.SelectedNode = (RadTreeNode) null;
        this.TreeView.ResumePropertyNotifications();
        if (value == null && this.map.Count > 0)
        {
          this.map.Clear();
          this.TreeView.Root.nodes.Clear();
        }
        this.root.DataSource = value;
      }
    }

    public string DataMember
    {
      get
      {
        return this.root.DataMember;
      }
      set
      {
        this.root.DataMember = value;
      }
    }

    public string ValueMember
    {
      get
      {
        return this.root.ValueMember;
      }
      set
      {
        this.root.ValueMember = value;
      }
    }

    public string CheckedMember
    {
      get
      {
        return this.root.CheckedMember;
      }
      set
      {
        this.root.CheckedMember = value;
      }
    }

    public string ChildMember
    {
      get
      {
        return this.root.ChildMember;
      }
      set
      {
        this.root.ChildMember = value;
      }
    }

    public string DisplayMember
    {
      get
      {
        return this.root.DisplayMember;
      }
      set
      {
        this.root.DisplayMember = value;
      }
    }

    public string ParentMember
    {
      get
      {
        return this.root.ParentMember;
      }
      set
      {
        this.root.ParentMember = value;
      }
    }

    public TypeConverter ToggleStateConverter
    {
      get
      {
        return this.toggleStateConverter;
      }
      set
      {
        this.toggleStateConverter = value;
      }
    }

    private object GetParentKey(int metadataIndex, object dataBoundItem)
    {
      if (this.map[metadataIndex].Descriptor.ParentDescriptor == null)
        return (object) null;
      return this.map[metadataIndex].Descriptor.ParentDescriptor.GetValue(dataBoundItem);
    }

    private void SetParentKey(int metadataIndex, object dataBoundItem, object value)
    {
      if (this.map[metadataIndex].Descriptor.ParentDescriptor == null)
        return;
      this.map[metadataIndex].Descriptor.ParentDescriptor.SetValue(dataBoundItem, value);
    }

    private object GetChildKey(int metadataIndex, object dataBoundItem)
    {
      if (this.map[metadataIndex].Descriptor.ChildDescriptor == null)
        return (object) null;
      return this.map[metadataIndex].Descriptor.ChildDescriptor.GetValue(dataBoundItem);
    }

    private void SetChildKey(int metadataIndex, object dataBoundItem, object value)
    {
      if (this.map[metadataIndex].Descriptor.ChildDescriptor == null)
        return;
      this.map[metadataIndex].Descriptor.ChildDescriptor.SetValue(dataBoundItem, value);
    }

    private bool ContainsObjectRelation
    {
      get
      {
        if (string.IsNullOrEmpty(this.root.ChildMember))
          return false;
        return this.root.ChildMember.Split('\\').Length > 1;
      }
    }

    private RadTreeNode CreateNode(object dataBounditem, int boundIndex)
    {
      RadTreeNode newNode = this.TreeView.CreateNewNode();
      ((IDataItem) newNode).DataBoundItem = dataBounditem;
      newNode.BoundIndex = boundIndex;
      return newNode;
    }

    private IList<RadTreeNode> GetObjectNodes(RadTreeNode parent)
    {
      if (parent == this.TreeView.Root)
      {
        List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>(this.map[0].Source.Count);
        for (int index = 0; index < this.map[0].Source.Count; ++index)
        {
          RadTreeNode node = this.CreateNode(this.map[0].Source.List[index], 0);
          node.TreeViewElement = this.TreeView;
          node.parent = parent;
          radTreeNodeList.Add(node);
          this.TreeView.OnNodeDataBound(new RadTreeViewEventArgs(node));
        }
        return (IList<RadTreeNode>) radTreeNodeList;
      }
      int index1 = parent.BoundIndex + 1;
      if ((index1 >= this.map.Count || !this.map[index1].State[1]) && !this.ResolveDescriptors(parent.BoundIndex + 1, parent.DataBoundItem))
        return (IList<RadTreeNode>) NotifyCollection<RadTreeNode>.Empty;
      List<RadTreeNode> radTreeNodeList1 = new List<RadTreeNode>();
      IEnumerable enumerable = this[index1].ChildDescriptor.GetValue(parent.DataBoundItem) as IEnumerable;
      if (enumerable != null)
      {
        if (!this.map[index1].State[2])
        {
          string[] strArray = this.root.DisplayMember.Split('\\');
          if (index1 < strArray.Length)
          {
            PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties((object) enumerable);
            this[index1].DisplayDescriptor = listItemProperties.Find(strArray[index1], true);
            this.map[index1].State[2] = true;
          }
        }
        foreach (object obj in enumerable)
        {
          RadTreeNode newNode = this.TreeView.CreateNewNode();
          ((IDataItem) newNode).DataBoundItem = obj;
          newNode.Parent = parent;
          newNode.BoundIndex = index1;
          radTreeNodeList1.Add(newNode);
          this.TreeView.OnNodeDataBound(new RadTreeViewEventArgs(newNode));
        }
        IBindingList bindingList = enumerable as IBindingList;
        if (bindingList != null && this.ReflectInnerObjectRelationChanges)
          parent.SubscribeToChildCollection(bindingList);
      }
      return (IList<RadTreeNode>) radTreeNodeList1;
    }

    private IList<RadTreeNode> BuildNodes(RadTreeNode parent, int index)
    {
      bool flag1 = parent == this.TreeView.Root;
      if (flag1 && this.map[index].Tree == null)
      {
        List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>(this.map[index].Source.Count);
        for (int index1 = 0; index1 < this.map[index].Source.Count; ++index1)
        {
          RadTreeNode node = this.CreateNode(this.map[index].Source.List[index1], index);
          node.TreeViewElement = this.TreeView;
          radTreeNodeList.Add(node);
          this.TreeView.OnNodeDataBound(new RadTreeViewEventArgs(node));
        }
        return (IList<RadTreeNode>) radTreeNodeList;
      }
      if (index > this.map.Count)
        return (IList<RadTreeNode>) NotifyCollection<RadTreeNode>.Empty;
      BindingProvider.TreeNodeList tree = this.map[index].Tree;
      if (tree == null || tree.Count <= 0)
        return (IList<RadTreeNode>) NotifyCollection<RadTreeNode>.Empty;
      if (flag1)
        return (IList<RadTreeNode>) tree.GetTreeNodes(0, index);
      bool flag2 = this.map[index].State[4];
      if (!flag2 && parent.BoundIndex == index)
        return (IList<RadTreeNode>) NotifyCollection<RadTreeNode>.Empty;
      object key;
      if (flag2)
      {
        key = this.GetChildKey(index, parent.DataBoundItem);
        if (this.GetParentKey(index, parent.DataBoundItem) == key)
          return (IList<RadTreeNode>) NotifyCollection<RadTreeNode>.Empty;
        if (parent.Level > 0 && (key == null || key == DBNull.Value))
          return (IList<RadTreeNode>) NotifyCollection<RadTreeNode>.Empty;
      }
      else
        key = this.GetParentKey(index, parent.DataBoundItem);
      return (IList<RadTreeNode>) tree.GetTreeNodes(key, index);
    }

    private void BuildIndex(int index)
    {
      if (this.map[index].Tree != null && this.map[index].Tree.Count > 0)
        return;
      BindingProvider.Metadata metadata = this.map[index];
      bool flag = metadata.State[4];
      if ((flag ? (object) metadata.Descriptor.ParentDescriptor : (object) metadata.Descriptor.ChildDescriptor) == null)
        return;
      BindingProvider.TreeNodeList treeNodeList = new BindingProvider.TreeNodeList(this, metadata.Source.Count);
      for (int index1 = 0; index1 < metadata.Source.Count; ++index1)
      {
        object obj = metadata.Source.List[index1];
        object key = flag ? this.GetParentKey(index, obj) : this.GetChildKey(index, obj);
        treeNodeList.AddNode(key, obj);
      }
      this.map[index].Tree = treeNodeList;
    }

    private bool ResolveDescriptors(int level, object dataBoundItem)
    {
      if (dataBoundItem == null)
        return false;
      bool flag = false;
      PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(dataBoundItem);
      string[] strArray1 = this.root.ChildMember.Split('\\');
      if (level < strArray1.Length)
      {
        PropertyDescriptor propertyDescriptor = listItemProperties.Find(strArray1[level], true);
        if (propertyDescriptor != null)
        {
          this.map[level].Descriptor.ChildDescriptor = propertyDescriptor;
          this.map[level].State[1] = true;
          flag = true;
        }
      }
      string[] strArray2 = this.root.DisplayMember.Split('\\');
      if (level < strArray2.Length)
      {
        PropertyDescriptor propertyDescriptor = listItemProperties.Find(strArray2[level], true);
        if (propertyDescriptor != null)
          this.map[level].Descriptor.DisplayDescriptor = propertyDescriptor;
      }
      return flag;
    }

    private void ResetBindingContext()
    {
      for (int index = 0; index < this.map.Count; ++index)
        this.UnwireEvents(this.map[index].Source);
      if (this.root != null)
        this.root.PropertyChanged -= new PropertyChangedEventHandler(this.root_PropertyChanged);
      this.root = new RelationBinding();
      if (this.relationBindings != null)
        this.relationBindings.CollectionChanged -= new Telerik.WinControls.Data.NotifyCollectionChangedEventHandler(this.RelationBindings_CollectionChanged);
      this.relationBindings = new RelationBindingCollection();
      this.relationBindings.CollectionChanged += new Telerik.WinControls.Data.NotifyCollectionChangedEventHandler(this.RelationBindings_CollectionChanged);
      this.map.Clear();
    }

    private void ExpandEnsureVisible()
    {
      if (this.TreeView.SelectedNode == null)
        return;
      this.TreeView.BeginUpdate();
      for (RadTreeNode parent = this.TreeView.SelectedNode.Parent; parent != null && !parent.Expanded; parent = parent.Parent)
        parent.Expand();
      this.TreeView.EndUpdate(true, RadTreeViewElement.UpdateActions.Resume);
      this.TreeView.EnsureVisible(this.TreeView.SelectedNode);
    }

    private void RelationBindings_CollectionChanged(
      object sender,
      Telerik.WinControls.Data.NotifyCollectionChangedEventArgs e)
    {
      this.Reset();
    }

    private void cm_PositionChanged(object sender, EventArgs e)
    {
      if (this.IsSuspended)
        return;
      CurrencyManager cm = sender as CurrencyManager;
      int level = this.map.IndexOf(cm);
      if (level < 0 || cm.Position < 0)
        return;
      DataRowView current = cm.Current as DataRowView;
      if (current != null && current.IsEdit && current.IsNew)
        return;
      this.SuspendUpdate();
      IList<RadTreeNode> nodes = (IList<RadTreeNode>) null;
      if (cm.Position >= 0 && cm.Position < cm.Count)
        nodes = this.GetItems(level, cm.Current);
      if (nodes != null)
      {
        this.postponeSelection = nodes.Count > cm.Count;
        if (!this.postponeSelection)
          this.SetSelectedNode(cm.Current, nodes);
      }
      this.ResumeUpdate();
    }

    private IList<RadTreeNode> GetItems(int level, object dataItem)
    {
      IList<RadTreeNode> radTreeNodeList;
      if (this.map[level].Tree != null)
      {
        object key = this.map[level].State[4] ? this.GetParentKey(level, dataItem) : this.GetChildKey(level, dataItem);
        radTreeNodeList = (IList<RadTreeNode>) this.map[level].Tree.GetTreeNodes(key, level);
      }
      else
        radTreeNodeList = (IList<RadTreeNode>) this.TreeView.Root.nodes;
      return radTreeNodeList;
    }

    private void SetSelectedNode(object dataItem, IList<RadTreeNode> nodes)
    {
      if (nodes == null || nodes.Count == 0)
        return;
      for (int index = 0; index < nodes.Count; ++index)
      {
        RadTreeNode node = nodes[index];
        if (node.DataBoundItem == dataItem)
        {
          this.TreeView.SelectedNode = node;
          this.ExpandEnsureVisible();
          break;
        }
      }
    }

    private void cm_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (this.IsSuspended)
      {
        if (e.ListChangedType != ListChangedType.ItemAdded)
          return;
        this.UpdateAddRef(sender as CurrencyManager, e.NewIndex);
      }
      else
      {
        switch (e.ListChangedType)
        {
          case ListChangedType.Reset:
          case ListChangedType.PropertyDescriptorAdded:
          case ListChangedType.PropertyDescriptorDeleted:
          case ListChangedType.PropertyDescriptorChanged:
            this.Reset();
            break;
          case ListChangedType.ItemAdded:
            this.AddNodeByCM(sender as CurrencyManager, e.NewIndex);
            break;
          case ListChangedType.ItemDeleted:
            this.DeleteNodeByCM(sender as CurrencyManager, e.OldIndex);
            break;
          case ListChangedType.ItemChanged:
            this.EditNodeByCM(sender as CurrencyManager, e.NewIndex, e.PropertyDescriptor);
            break;
        }
      }
    }

    private void UpdateAddRef(CurrencyManager cm, int index)
    {
      if (this.map.IndexOf(cm) < 0 || index >= 0 && index < cm.Count && cm.List.Count == index)
        return;
      this.addRef = new WeakReference(cm.List[index]);
    }

    private void AddNodeByCM(CurrencyManager cm, int index)
    {
      int index1 = this.map.IndexOf(cm);
      if (index1 < 0 || index >= 0 && index < cm.Count && cm.List.Count == index)
        return;
      object obj = cm.List[index];
      if (this.addRef != null && this.addRef.IsAlive && this.addRef.Target == obj)
        return;
      this.addRef = new WeakReference(obj);
      object key = this.map[index1].State[4] ? this.GetParentKey(index1, obj) : this.GetChildKey(index1, obj);
      if (this.map[index1].Tree != null)
      {
        this.map[index1].Tree.AddNode(key, obj);
        List<object> objectList = this.map[index1].Tree[key];
        if (objectList != null && objectList.Count > 0 && (objectList[0] is RadTreeNode && ((RadTreeNode) objectList[0]).parent != null))
        {
          ((RadTreeNode) objectList[0]).parent.nodes.ResetVersion();
          this.TreeView.Update(RadTreeViewElement.UpdateActions.Resume);
          return;
        }
      }
      Dictionary<object, bool> expandedStates = this.SaveExpandedStates(index1);
      this.map[index1].Tree = (BindingProvider.TreeNodeList) null;
      ++this.map[index1].Version;
      if (index1 > 0)
      {
        ++this.map[index1 - 1].Version;
        this.map[index1 - 1].Tree = (BindingProvider.TreeNodeList) null;
      }
      this.TreeView.Update(RadTreeViewElement.UpdateActions.Reset);
      this.TreeView.BeginUpdate();
      this.RestoreExpandedStates(index1, expandedStates);
      expandedStates.Clear();
      if (cm.Position >= 0)
      {
        IList<RadTreeNode> items = this.GetItems(index1, cm.Current);
        this.SetSelectedNode(cm.Current, items);
      }
      this.TreeView.EndUpdate(true, RadTreeViewElement.UpdateActions.Resume);
    }

    private void RestoreExpandedStates(int cmIndex, Dictionary<object, bool> expandedStates)
    {
      if (this.map[cmIndex].Tree == null)
        return;
      foreach (RadTreeNode node in this.TreeView.GetNodes())
      {
        if (node != null && node.DataBoundItem != null && expandedStates.ContainsKey(node.DataBoundItem))
          node.Expanded = expandedStates[node.DataBoundItem];
      }
    }

    private Dictionary<object, bool> SaveExpandedStates(int cmIndex)
    {
      Dictionary<object, bool> dictionary = new Dictionary<object, bool>();
      if (this.map[cmIndex].Tree == null)
        return dictionary;
      for (int index = 0; index < this.map[cmIndex].Tree.Count; ++index)
      {
        foreach (object obj in this.map[cmIndex].Tree[index])
        {
          RadTreeNode radTreeNode = obj as RadTreeNode;
          if (radTreeNode != null && radTreeNode.DataBoundItem != null)
            dictionary.Add(radTreeNode.DataBoundItem, radTreeNode.Expanded);
        }
      }
      return dictionary;
    }

    private void EditNodeByCM(CurrencyManager cm, int index, PropertyDescriptor pd)
    {
      int index1 = this.map.IndexOf(cm);
      if (index1 < 0)
        return;
      object obj = cm.List[index];
      bool flag = this.map[index1].State[4];
      if (flag && this.map[index1].Descriptor.ParentDescriptor == pd || !flag && this.map[index1].Descriptor.ChildDescriptor == pd)
      {
        this.map[index1].Tree = (BindingProvider.TreeNodeList) null;
        ++this.map[index1].Version;
        if (index1 > 0)
        {
          ++this.map[index1 - 1].Version;
          this.map[index1 - 1].Tree = (BindingProvider.TreeNodeList) null;
        }
        this.TreeView.Update(RadTreeViewElement.UpdateActions.Reset);
      }
      else
      {
        if (this.TreeView.IsEditing)
          return;
        this.TreeView.Update(RadTreeViewElement.UpdateActions.Resume);
      }
    }

    private void DeleteNodeByCM(CurrencyManager cm, int index)
    {
      int index1 = this.map.IndexOf(cm);
      if (index1 < 0)
        return;
      Dictionary<object, bool> expandedStates = this.SaveExpandedStates(index1);
      this.map[index1].Tree = (BindingProvider.TreeNodeList) null;
      ++this.map[index1].Version;
      if (index1 > 0)
      {
        ++this.map[index1 - 1].Version;
        this.map[index1 - 1].Tree = (BindingProvider.TreeNodeList) null;
      }
      this.TreeView.Update(RadTreeViewElement.UpdateActions.Reset);
      this.TreeView.BeginUpdate();
      if (cm.Position >= 0)
      {
        this.postponeSelection = false;
        IList<RadTreeNode> items = this.GetItems(index1, cm.Current);
        this.SetSelectedNode(cm.Current, items);
      }
      this.RestoreExpandedStates(index1, expandedStates);
      expandedStates.Clear();
      this.TreeView.EndUpdate(true, RadTreeViewElement.UpdateActions.Resume);
    }

    private bool RemoveFromSource(RadTreeNode parent, RadTreeNode item)
    {
      if (this.objectRelationalMode)
        return this.RemoveFromORM(parent, item);
      if (item.BoundIndex < 0 || item.BoundIndex >= this.map.Count || this.map[item.BoundIndex].Source == null)
        return true;
      CurrencyManager source = this.map[item.BoundIndex].Source;
      object key = this.map[item.BoundIndex].State[item.BoundIndex] ? this.GetParentKey(item.BoundIndex, item.DataBoundItem) : this.GetChildKey(item.BoundIndex, item.DataBoundItem);
      if (this.map[item.BoundIndex].Tree != null)
        this.map[item.BoundIndex].Tree.RemoveNode(key, (object) item);
      int count = source.Count;
      int index = source.List.IndexOf(item.DataBoundItem);
      if (index >= 0)
      {
        this.SuspendUpdate();
        source.List.RemoveAt(index);
        this.ResumeUpdate();
      }
      parent?.nodes.ResetVersion();
      return count != source.Count;
    }

    private bool RemoveFromORM(RadTreeNode parent, RadTreeNode item)
    {
      int index = parent.BoundIndex + 1;
      if (index < this.map.Count)
      {
        if (this.map[index].State[1])
          goto label_3;
      }
      if (!this.ResolveDescriptors(parent.BoundIndex + 1, parent.DataBoundItem))
        goto label_13;
label_3:
      try
      {
        IList list = this[index].ChildDescriptor.GetValue(parent.DataBoundItem) as IList;
        if (list != null)
        {
          this.SuspendUpdate();
          list.Remove(item.DataBoundItem);
          this.ResumeUpdate();
          return true;
        }
        object obj = this[index].ChildDescriptor.GetValue(parent.DataBoundItem);
        if (obj == null)
          return true;
        System.Type type = obj.GetType();
        if (!type.IsGenericType || type.IsGenericTypeDefinition)
          return true;
        System.Type[] genericArguments = obj.GetType().GetGenericArguments();
        MethodInfo method = type.GetMethod("Remove");
        if ((object) method == null || genericArguments.Length == 0 || (item.DataBoundItem == null || (object) genericArguments[0] != (object) item.DataBoundItem.GetType()))
          return true;
        this.SuspendUpdate();
        method.Invoke(obj, new object[1]
        {
          item.DataBoundItem
        });
        this.ResumeUpdate();
        return true;
      }
      catch (Exception ex)
      {
        this.TreeView.OnDataError(new TreeNodeDataErrorEventArgs(ex.Message, item, new object[1]
        {
          (object) ex
        }));
      }
label_13:
      return false;
    }

    private bool AddToSource(RadTreeNode parent, RadTreeNode item)
    {
      if (this.objectRelationalMode)
        return this.AddToORM(parent, item);
      if (parent.BoundIndex < 0 || parent.BoundIndex >= this.map.Count)
        return true;
      int boundIndex = parent.BoundIndex;
      if (!this.map[parent.BoundIndex].State[4] && this.map.Count > 1)
      {
        ++boundIndex;
        if (boundIndex >= this.map.Count)
          return false;
      }
      if (this.map[boundIndex].Source == null)
        return true;
      bool flag1 = true;
      this.SuspendUpdate();
      try
      {
        bool flag2 = this.map[boundIndex].State[4];
        CurrencyManager source = this.map[boundIndex].Source;
        source.AddNew();
        object obj = source.List[source.Count - 1];
        ((IDataItem) item).DataBoundItem = obj;
        item.BoundIndex = boundIndex;
        this.TreeView.OnNodeDataBound(new RadTreeViewEventArgs(item));
        if (this.map[boundIndex].Descriptor.DisplayDescriptor != null && !string.IsNullOrEmpty(item.Text))
          this.map[boundIndex].Descriptor.DisplayDescriptor.SetValue(obj, (object) item.Text);
        if (this.map[boundIndex].Descriptor.ValueDescriptor != null && item.Value != null)
          this.map[boundIndex].Descriptor.ValueDescriptor.SetValue(obj, item.Value);
        object key = !(parent is RadTreeViewElement.RootTreeNode) || parent.Nodes.Count <= 0 ? (flag2 ? this.GetChildKey(boundIndex, parent.DataBoundItem) : this.GetParentKey(boundIndex, parent.DataBoundItem)) : this.GetParentKey(boundIndex, parent.Nodes[0].DataBoundItem);
        if (flag2)
          this.SetParentKey(boundIndex, obj, key);
        else
          this.SetChildKey(boundIndex, obj, key);
        (source.List as ICancelAddNew)?.EndNew(source.List.Count - 1);
        if (this.map[boundIndex].Tree != null)
          this.map[boundIndex].Tree.AddNode(key, (object) item);
      }
      catch (Exception ex)
      {
        this.TreeView.OnDataError(new TreeNodeDataErrorEventArgs(ex.Message, item, new object[0]));
        flag1 = false;
      }
      this.ResumeUpdate();
      return flag1;
    }

    private bool AddToORM(RadTreeNode parent, RadTreeNode item)
    {
      int index = parent.BoundIndex + 1;
      if (index < this.map.Count)
      {
        if (this.map[index].State[1])
          goto label_3;
      }
      if (!this.ResolveDescriptors(parent.BoundIndex + 1, parent.DataBoundItem))
        goto label_20;
label_3:
      try
      {
        IList list = this[index].ChildDescriptor.GetValue(parent.DataBoundItem) as IList;
        if (list != null)
        {
          System.Type listItemType = ListBindingHelper.GetListItemType((object) list);
          if (item.DataBoundItem != null && (object) listItemType == (object) item.DataBoundItem.GetType())
          {
            this.SuspendUpdate();
            list.Add(item.DataBoundItem);
            this.ResumeUpdate();
            return true;
          }
          ConstructorInfo constructor = listItemType.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, new System.Type[0], (ParameterModifier[]) null);
          if ((object) constructor != null)
          {
            this.SuspendUpdate();
            object obj = constructor.Invoke((object[]) null);
            list.Add(obj);
            ((IDataItem) item).DataBoundItem = obj;
            this.UpdateMap(parent, item);
            this.ResumeUpdate();
          }
        }
        else
        {
          object obj1 = this[index].ChildDescriptor.GetValue(parent.DataBoundItem);
          if (obj1 == null)
            return true;
          System.Type type = obj1.GetType();
          if (!type.IsGenericType || type.IsGenericTypeDefinition)
            return true;
          System.Type[] genericArguments = obj1.GetType().GetGenericArguments();
          MethodInfo method = type.GetMethod("Add");
          if ((object) method == null || genericArguments.Length == 0)
            return true;
          if (item.DataBoundItem != null && (object) genericArguments[0] == (object) item.DataBoundItem.GetType())
          {
            this.SuspendUpdate();
            method.Invoke(obj1, new object[1]
            {
              item.DataBoundItem
            });
            this.ResumeUpdate();
            return true;
          }
          ConstructorInfo constructor = genericArguments[0].GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, new System.Type[0], (ParameterModifier[]) null);
          if ((object) constructor != null)
          {
            this.SuspendUpdate();
            object obj2 = constructor.Invoke((object[]) null);
            method.Invoke(obj1, new object[1]{ obj2 });
            ((IDataItem) item).DataBoundItem = obj2;
            this.UpdateMap(parent, item);
            this.ResumeUpdate();
          }
        }
        return true;
      }
      catch (Exception ex)
      {
        this.TreeView.OnDataError(new TreeNodeDataErrorEventArgs(ex.Message, item, new object[1]
        {
          (object) ex
        }));
      }
label_20:
      return false;
    }

    private void UpdateMap(RadTreeNode parent, RadTreeNode item)
    {
      int boundIndex = parent.BoundIndex;
      if (!this.map[parent.BoundIndex].State[4] && this.map.Count > 1)
      {
        ++boundIndex;
        if (boundIndex >= this.map.Count)
          return;
      }
      item.BoundIndex = boundIndex;
      this.TreeView.OnNodeDataBound(new RadTreeViewEventArgs(item));
      if (this.map[boundIndex].Descriptor.DisplayDescriptor != null && !string.IsNullOrEmpty(item.Text))
        this.map[boundIndex].Descriptor.DisplayDescriptor.SetValue(item.DataBoundItem, (object) item.Text);
      if (this.map[boundIndex].Descriptor.ValueDescriptor == null || item.Value == null)
        return;
      this.map[boundIndex].Descriptor.ValueDescriptor.SetValue(item.DataBoundItem, item.Value);
    }

    private void WireEvents(CurrencyManager cm)
    {
      cm.ListChanged += new ListChangedEventHandler(this.cm_ListChanged);
      cm.PositionChanged += new EventHandler(this.cm_PositionChanged);
    }

    private void UnwireEvents(CurrencyManager cm)
    {
      cm.ListChanged -= new ListChangedEventHandler(this.cm_ListChanged);
      cm.PositionChanged -= new EventHandler(this.cm_PositionChanged);
    }

    private void root_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Reset();
    }

    private void RegisterMetadata(RelationBinding relation, int level)
    {
      CurrencyManager currencyManager = this.GetCurrencyManager(relation);
      if (currencyManager == null)
        return;
      int index = this.map.IndexOf(currencyManager);
      if (level == 0 && this.map.Count == 1 && index < 0)
      {
        this.WireEvents(currencyManager);
        index = 0;
        this.map[0].Source = currencyManager;
        this.map[0].Tree = (BindingProvider.TreeNodeList) null;
        if (this.TreeView.Root.nodes != null)
          this.TreeView.Root.nodes.Clear();
        this.TreeView.Root.nodes = (RadTreeNodeCollection) null;
      }
      if (index >= 0)
      {
        TreeNodeDescriptor descriptor = this.GetDescriptor(relation, currencyManager);
        this.map[index].State[4] = this.IsSefReference(descriptor, index);
        if (!this.map[index].State[4] && index > 0)
        {
          CurrencyManager source = this.map[index - 1].Source;
          if (source != null)
          {
            PropertyDescriptorCollection properties = this.GetProperties(source);
            descriptor.ParentDescriptor = properties.Find(relation.ParentMember, true);
          }
        }
        this.map[index].Descriptor = descriptor;
        this.map[index].Tree = (BindingProvider.TreeNodeList) null;
        ++this.map[index].Version;
      }
      else
        this.AddNewMetadata(relation, currencyManager, level);
    }

    private CurrencyManager GetCurrencyManager(RelationBinding relation)
    {
      CurrencyManager currencyManager = (CurrencyManager) null;
      if (relation.DataSource is BindingSource)
        currencyManager = ((BindingSource) relation.DataSource).CurrencyManager;
      if (currencyManager == null && relation.DataSource != null && this.TreeView.BindingContext != null)
        currencyManager = this.TreeView.BindingContext[relation.DataSource, relation.DataMember] as CurrencyManager;
      return currencyManager;
    }

    private bool NeedsRefresh
    {
      get
      {
        List<CurrencyManager> bindings = new List<CurrencyManager>();
        for (int index = 0; index < this.map.Count; ++index)
          bindings.Add(this.map[index].Source);
        byte[] metadataHash1 = this.ComputeMetadataHash(bindings);
        bindings.Clear();
        bindings.Add(this.GetCurrencyManager(this.root));
        for (int index = 0; index < this.relationBindings.Count; ++index)
          bindings.Add(this.GetCurrencyManager(this.relationBindings[index]));
        byte[] metadataHash2 = this.ComputeMetadataHash(bindings);
        if (metadataHash1 == null && metadataHash2 == null)
          return true;
        return !Telerik.WinControls.ClientUtils.ArraysEqual(metadataHash1, metadataHash2);
      }
    }

    private byte[] ComputeMetadataHash(List<CurrencyManager> bindings)
    {
      StringBuilder stringBuilder = new StringBuilder(1024);
      for (int index1 = 0; index1 < bindings.Count; ++index1)
      {
        CurrencyManager binding = bindings[index1];
        if (binding != null)
        {
          PropertyDescriptorCollection itemProperties = binding.GetItemProperties();
          for (int index2 = 0; index2 < itemProperties.Count; ++index2)
            stringBuilder.Append(itemProperties[index2].Name);
          stringBuilder.Append(binding.Count.ToString());
        }
      }
      if (stringBuilder.Length == 0)
        return (byte[]) null;
      return new SHA1CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(stringBuilder.ToString()));
    }

    private PropertyDescriptorCollection GetProperties(
      CurrencyManager cm)
    {
      System.Type listItemType = ListBindingHelper.GetListItemType((object) cm.List);
      if ((object) listItemType != null && listItemType.IsInterface && cm.List.Count > 0)
        return TypeDescriptor.GetProperties(cm.List[0]);
      return cm.GetItemProperties();
    }

    private PropertyDescriptorCollection GetProperties(
      CurrencyManager cm,
      string childMember,
      int level)
    {
      System.Type listItemType = ListBindingHelper.GetListItemType((object) cm.List);
      if ((object) listItemType != null && listItemType.IsInterface && cm.List.Count > 0)
        return TypeDescriptor.GetProperties(cm.List[0]);
      if ((object) listItemType == null || level <= 0)
        return cm.GetItemProperties();
      string[] strArray = childMember.Split('\\');
      int index = 1;
      PropertyDescriptorCollection descriptorCollection = cm.List.Count > 0 ? TypeDescriptor.GetProperties(cm.List[0]) : cm.GetItemProperties();
      for (; index <= level; ++index)
      {
        PropertyDescriptor propertyDescriptor = descriptorCollection[strArray[index]];
        if (propertyDescriptor == null)
          throw new ArgumentException("ChildMember property was not found in the data source objects. Please verify that your ChildMember property is correctly set. If you are changing the DataSource, consider setting the DataSource property to null first.");
        System.Type[] genericArguments = propertyDescriptor.PropertyType.GetGenericArguments();
        descriptorCollection = !propertyDescriptor.PropertyType.IsArray ? (genericArguments == null || genericArguments.Length <= 0 ? TypeDescriptor.GetProperties(propertyDescriptor.PropertyType) : TypeDescriptor.GetProperties(genericArguments[0])) : TypeDescriptor.GetProperties(propertyDescriptor.PropertyType.GetElementType());
      }
      return descriptorCollection;
    }

    private void RegisterObjectRelation()
    {
      CurrencyManager cm = (CurrencyManager) null;
      if (this.root.DataSource is BindingSource)
        cm = ((BindingSource) this.root.DataSource).CurrencyManager;
      if (cm == null && this.root.DataSource != null && this.TreeView.BindingContext != null)
        cm = this.TreeView.BindingContext[this.root.DataSource, this.root.DataMember] as CurrencyManager;
      if (cm == null)
        return;
      this.map.Clear();
      string[] strArray1;
      if (!string.IsNullOrEmpty(this.root.ChildMember))
        strArray1 = this.root.ChildMember.Split('\\');
      else
        strArray1 = (string[]) null;
      string[] strArray2 = strArray1;
      if (strArray2 == null)
        return;
      for (int level = 0; level < strArray2.Length; ++level)
      {
        BindingProvider.Metadata metadata = new BindingProvider.Metadata();
        metadata.Source = cm;
        metadata.Tree = (BindingProvider.TreeNodeList) null;
        ++metadata.Version;
        metadata.Descriptor = this.GetObjectRelationDescriptor(this.root, cm, level);
        this.map.Add(metadata);
      }
    }

    private void AddNewMetadata(RelationBinding relation, CurrencyManager cm, int level)
    {
      BindingProvider.Metadata metadata = new BindingProvider.Metadata();
      this.WireEvents(cm);
      metadata.Source = cm;
      metadata.Descriptor = this.GetDescriptor(relation, cm);
      ++metadata.Version;
      int index;
      if (level < 0)
      {
        index = this.map.Count > 0 ? this.map.Count : -1;
        this.map.Add(metadata);
      }
      else
      {
        index = level - 1 >= 0 ? level : -1;
        this.map.Insert(level, metadata);
      }
      metadata.State[4] = this.IsSefReference(metadata.Descriptor, index);
      if (metadata.State[4] || index <= 0)
        return;
      CurrencyManager source = this.map[index - 1].Source;
      if (source == null)
        return;
      PropertyDescriptorCollection properties = this.GetProperties(source);
      metadata.Descriptor.ParentDescriptor = properties.Find(relation.ParentMember, true);
    }

    private bool IsSefReference(TreeNodeDescriptor descriptor, int index)
    {
      bool flag = descriptor.ParentDescriptor != null && descriptor.ChildDescriptor != null && descriptor.ChildDescriptor != descriptor.ParentDescriptor;
      if (flag && index > 0 && this.map.Count > 1)
      {
        CurrencyManager source = this.map[index - 1].Source;
        if (source != null)
          flag = this.GetProperties(source).Find(descriptor.ParentDescriptor.Name, true) == null;
      }
      return flag;
    }

    private TreeNodeDescriptor GetDescriptor(
      RelationBinding relation,
      CurrencyManager cm)
    {
      string str1;
      if (!string.IsNullOrEmpty(relation.ChildMember))
        str1 = relation.ChildMember.Split('\\')[0];
      else
        str1 = (string) null;
      string path1 = str1;
      string str2;
      if (!string.IsNullOrEmpty(relation.ParentMember))
        str2 = relation.ParentMember.Split('\\')[0];
      else
        str2 = (string) null;
      string path2 = str2;
      string str3;
      if (!string.IsNullOrEmpty(relation.DisplayMember))
        str3 = relation.DisplayMember.Split('\\')[0];
      else
        str3 = (string) null;
      string path3 = str3;
      string str4;
      if (!string.IsNullOrEmpty(relation.ValueMember))
        str4 = relation.ValueMember.Split('\\')[0];
      else
        str4 = (string) null;
      string path4 = str4;
      string str5;
      if (!string.IsNullOrEmpty(relation.CheckedMember))
        str5 = relation.CheckedMember.Split('\\')[0];
      else
        str5 = (string) null;
      string path5 = str5;
      TreeNodeDescriptor treeNodeDescriptor = new TreeNodeDescriptor();
      PropertyDescriptorCollection properties = this.GetProperties(cm);
      if (path2 != null)
      {
        string[] strArray = path2.Split('.');
        treeNodeDescriptor.ParentDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.ParentDescriptor != null && path2.Length > 1)
          treeNodeDescriptor.SetParentDescriptor(treeNodeDescriptor.ParentDescriptor, path2);
      }
      if (path1 != null)
      {
        string[] strArray = path1.Split('.');
        treeNodeDescriptor.ChildDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.ChildDescriptor != null && path1.Length > 1)
          treeNodeDescriptor.SetChildDescriptor(treeNodeDescriptor.ChildDescriptor, path1);
      }
      if (path3 != null)
      {
        string[] strArray = path3.Split('.');
        treeNodeDescriptor.DisplayDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.DisplayDescriptor != null && path3.Length > 1)
          treeNodeDescriptor.SetDisplayDescriptor(treeNodeDescriptor.DisplayDescriptor, path3);
      }
      if (path4 != null)
      {
        string[] strArray = path4.Split('.');
        treeNodeDescriptor.ValueDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.ValueDescriptor != null && path4.Length > 1)
          treeNodeDescriptor.SetValueDescriptor(treeNodeDescriptor.ValueDescriptor, path4);
      }
      if (path5 != null)
      {
        string[] strArray = path5.Split('.');
        treeNodeDescriptor.CheckedDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.CheckedDescriptor != null && path5.Length > 1)
          treeNodeDescriptor.SetCheckedDescriptor(treeNodeDescriptor.CheckedDescriptor, path5);
      }
      if (this.ToggleStateConverter != null)
        treeNodeDescriptor.Converter = this.ToggleStateConverter;
      return treeNodeDescriptor;
    }

    private TreeNodeDescriptor GetObjectRelationDescriptor(
      RelationBinding relation,
      CurrencyManager cm,
      int level)
    {
      string memberPath1 = this.GetMemberPath(relation.ChildMember, level);
      string memberPath2 = this.GetMemberPath(relation.ValueMember, level);
      string memberPath3 = this.GetMemberPath(relation.CheckedMember, level);
      string memberPath4 = this.GetMemberPath(relation.ParentMember, level);
      string memberPath5 = this.GetMemberPath(relation.DisplayMember, level);
      TreeNodeDescriptor treeNodeDescriptor = new TreeNodeDescriptor();
      PropertyDescriptorCollection properties = this.GetProperties(cm, relation.ChildMember, level);
      if (memberPath4 != null)
      {
        string[] strArray = memberPath4.Split('.');
        treeNodeDescriptor.ParentDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.ParentDescriptor != null && memberPath4.Length > 1)
          treeNodeDescriptor.SetParentDescriptor(treeNodeDescriptor.ParentDescriptor, memberPath4);
      }
      if (memberPath1 != null)
      {
        string[] strArray = memberPath1.Split('.');
        treeNodeDescriptor.ChildDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.ChildDescriptor != null && memberPath1.Length > 1)
          treeNodeDescriptor.SetChildDescriptor(treeNodeDescriptor.ChildDescriptor, memberPath1);
      }
      if (memberPath5 != null)
      {
        string[] strArray = memberPath5.Split('.');
        treeNodeDescriptor.DisplayDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.DisplayDescriptor != null && memberPath5.Length > 1)
          treeNodeDescriptor.SetDisplayDescriptor(treeNodeDescriptor.DisplayDescriptor, memberPath5);
      }
      if (memberPath2 != null)
      {
        string[] strArray = memberPath2.Split('.');
        treeNodeDescriptor.ValueDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.ValueDescriptor != null && memberPath2.Length > 1)
          treeNodeDescriptor.SetValueDescriptor(treeNodeDescriptor.ValueDescriptor, memberPath2);
      }
      if (memberPath3 != null)
      {
        string[] strArray = memberPath3.Split('.');
        treeNodeDescriptor.CheckedDescriptor = properties.Find(strArray[0], true);
        if (treeNodeDescriptor.CheckedDescriptor != null && memberPath3.Length > 1)
          treeNodeDescriptor.SetCheckedDescriptor(treeNodeDescriptor.CheckedDescriptor, memberPath3);
      }
      if (this.ToggleStateConverter != null)
        treeNodeDescriptor.Converter = this.ToggleStateConverter;
      return treeNodeDescriptor;
    }

    private string GetMemberPath(string member, int level)
    {
      if (string.IsNullOrEmpty(member))
        return (string) null;
      string[] strArray = member.Split('\\');
      if (strArray.Length > level)
        return strArray[level];
      return (string) null;
    }

    private class Metadata
    {
      public int Version;
      public BitVector32 State;
      public BindingProvider.TreeNodeList Tree;
      public CurrencyManager Source;
      public TreeNodeDescriptor Descriptor;
    }

    private class MetadataList : List<BindingProvider.Metadata>
    {
      public int IndexOf(CurrencyManager cm)
      {
        if (cm == null)
          return -1;
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Source == cm)
            return index;
        }
        return -1;
      }
    }

    private class TreeNodeList
    {
      private BindingProvider.TreeNodeList.NodeComparer comparer = new BindingProvider.TreeNodeList.NodeComparer();
      private List<BindingProvider.TreeNodeList.Node> nodes;
      private BindingProvider owner;

      public TreeNodeList(BindingProvider owner, int capacity)
      {
        this.owner = owner;
        this.nodes = new List<BindingProvider.TreeNodeList.Node>(capacity);
      }

      public List<object> this[object key]
      {
        get
        {
          int index = this.IndexOf(key);
          if (index >= 0)
            return this.nodes[index].Nodes;
          return (List<object>) null;
        }
      }

      public List<object> this[int index]
      {
        get
        {
          return this.nodes[index].Nodes;
        }
      }

      public List<object> GetOrAdd(object key)
      {
        int index = this.nodes.BinarySearch(new BindingProvider.TreeNodeList.Node(key), (IComparer<BindingProvider.TreeNodeList.Node>) this.comparer);
        if (index >= 0)
          return this.nodes[index].Nodes;
        BindingProvider.TreeNodeList.Node node = new BindingProvider.TreeNodeList.Node(key, new List<object>());
        this.nodes.Insert(index * -1 - 1, node);
        return node.Nodes;
      }

      public bool Contains(object key)
      {
        return this.IndexOf(key) >= 0;
      }

      public int IndexOf(object key)
      {
        return this.nodes.BinarySearch(new BindingProvider.TreeNodeList.Node(key), (IComparer<BindingProvider.TreeNodeList.Node>) this.comparer);
      }

      public void AddNode(object key, object node)
      {
        this.GetOrAdd(key).Add(node);
      }

      public bool RemoveNode(object key, object node)
      {
        List<object> objectList = this[key];
        if (objectList == null)
          return false;
        bool flag = objectList.Remove(node);
        for (int index = 0; index < objectList.Count; ++index)
        {
          if (objectList[index] == node)
          {
            objectList.RemoveAt(index);
            break;
          }
          object obj = node;
          if (obj is RadTreeNode)
            obj = ((RadTreeNode) obj).DataBoundItem;
          object dataBoundItem = objectList[index];
          if (dataBoundItem is RadTreeNode)
            dataBoundItem = ((RadTreeNode) dataBoundItem).DataBoundItem;
          if (obj == dataBoundItem)
          {
            objectList.RemoveAt(index);
            break;
          }
        }
        if (objectList.Count == 0)
          return this.RemoveBranch(key);
        return flag;
      }

      public bool RemoveBranch(object key)
      {
        int index = this.IndexOf(key);
        if (index < 0)
          return false;
        this.nodes.RemoveAt(index);
        return true;
      }

      public List<RadTreeNode> GetTreeNodes(object key, int boundIndex)
      {
        object index1 = key;
        if (this.nodes.Count > 0)
        {
          object obj = (object) null;
          for (int index2 = this.nodes.Count - 1; index2 >= 0; --index2)
          {
            obj = this.nodes[this.nodes.Count - 1].Key;
            if (obj != DBNull.Value)
              break;
          }
          System.Type type1 = obj == null ? (System.Type) null : obj.GetType();
          System.Type type2 = key == null ? (System.Type) null : key.GetType();
          if (obj != DBNull.Value && obj != null && (key != null && (object) type1 != (object) type2))
          {
            TypeConverter converter1 = TypeDescriptor.GetConverter(type1);
            if (converter1 != null && converter1.CanConvertFrom(type2))
            {
              index1 = converter1.ConvertFrom(key);
            }
            else
            {
              TypeConverter converter2 = TypeDescriptor.GetConverter(type2);
              if (converter2 != null && converter2.CanConvertTo(type1))
                index1 = converter2.ConvertTo(key, type1);
            }
          }
        }
        List<object> objectList = this[index1];
        if (objectList == null)
          return (List<RadTreeNode>) null;
        List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>(objectList.Count);
        for (int index2 = 0; index2 < objectList.Count; ++index2)
        {
          RadTreeNode newNode = objectList[index2] as RadTreeNode;
          if (newNode == null)
          {
            newNode = this.owner.TreeView.CreateNewNode();
            ((IDataItem) newNode).DataBoundItem = objectList[index2];
            newNode.BoundIndex = boundIndex;
            objectList[index2] = (object) newNode;
          }
          radTreeNodeList.Add(newNode);
        }
        return radTreeNodeList;
      }

      public List<RadTreeNode> GetTreeNodes(int index, int boundIndex)
      {
        List<object> nodes = this.nodes[index].Nodes;
        if (nodes == null)
          return (List<RadTreeNode>) null;
        List<RadTreeNode> radTreeNodeList = new List<RadTreeNode>(nodes.Count);
        for (int index1 = 0; index1 < nodes.Count; ++index1)
        {
          RadTreeNode newNode = nodes[index1] as RadTreeNode;
          if (newNode == null)
          {
            newNode = this.owner.TreeView.CreateNewNode();
            ((IDataItem) newNode).DataBoundItem = nodes[index1];
            newNode.BoundIndex = boundIndex;
            nodes[index1] = (object) newNode;
          }
          radTreeNodeList.Add(newNode);
        }
        return radTreeNodeList;
      }

      public RadTreeNode GetNode(object key, object dataBoundItem)
      {
        List<object> objectList = this[key];
        if (objectList != null)
        {
          for (int index = 0; index < objectList.Count; ++index)
          {
            RadTreeNode radTreeNode = objectList[index] as RadTreeNode;
            if (radTreeNode != null && radTreeNode.DataBoundItem == dataBoundItem)
              return radTreeNode;
          }
        }
        return (RadTreeNode) null;
      }

      public int Count
      {
        get
        {
          return this.nodes.Count;
        }
      }

      private struct Node
      {
        public object Key;
        public List<object> Nodes;

        public Node(object key)
        {
          this.Key = key;
          this.Nodes = (List<object>) null;
        }

        public Node(object key, List<object> nodes)
        {
          this.Key = key;
          this.Nodes = nodes;
        }
      }

      private class NodeComparer : IComparer<BindingProvider.TreeNodeList.Node>
      {
        public int Compare(BindingProvider.TreeNodeList.Node x, BindingProvider.TreeNodeList.Node y)
        {
          if (x.Key == y.Key)
            return 0;
          if (x.Key == null || x.Key == DBNull.Value)
            return -1;
          if (y.Key == null || y.Key == DBNull.Value)
            return 1;
          IComparable key = x.Key as IComparable;
          if (key == null)
            throw new ArgumentException("Argument_ImplementIComparable");
          return key.CompareTo(y.Key);
        }
      }
    }
  }
}
