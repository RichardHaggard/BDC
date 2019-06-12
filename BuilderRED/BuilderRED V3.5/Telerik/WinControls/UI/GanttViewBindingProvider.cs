// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewBindingProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  internal class GanttViewBindingProvider : GanttViewDataItemProvider
  {
    private bool postponeSelection;
    private PropertyDescriptorCollection boundProperties;
    private GanttViewRelationBinding root;
    private GanttViewBindingProvider.TaskMetadata taskMetadata;
    private GanttViewBindingProvider.LinkMetadata linkMetadata;
    private WeakReference addRef;

    public GanttViewBindingProvider(RadGanttViewElement ganttView)
      : base(ganttView)
    {
      this.root = new GanttViewRelationBinding();
      this.root.PropertyChanged += new PropertyChangedEventHandler(this.root_PropertyChanged);
    }

    public GanttViewRelationBinding Root
    {
      get
      {
        return this.root;
      }
    }

    public GanttTaskDescriptor Descriptor
    {
      get
      {
        return this.taskMetadata.TaskDescriptor;
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
        if (value == null)
        {
          this.GanttView.Links.Clear();
          this.GanttView.Root.items.Clear();
        }
        this.root.DataSource = value;
      }
    }

    public string TaskDataMember
    {
      get
      {
        return this.root.TaskDataMember;
      }
      set
      {
        this.root.TaskDataMember = value;
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

    public string TitleMember
    {
      get
      {
        return this.root.TitleMember;
      }
      set
      {
        this.root.TitleMember = value;
      }
    }

    public string StartMember
    {
      get
      {
        return this.root.StartMember;
      }
      set
      {
        this.root.StartMember = value;
      }
    }

    public string EndMember
    {
      get
      {
        return this.root.EndMember;
      }
      set
      {
        this.root.EndMember = value;
      }
    }

    public string ProgressMember
    {
      get
      {
        return this.root.ProgressMember;
      }
      set
      {
        this.root.ProgressMember = value;
      }
    }

    public string LinkDataMember
    {
      get
      {
        return this.root.LinkDataMember;
      }
      set
      {
        this.root.LinkDataMember = value;
      }
    }

    public string LinkStartMember
    {
      get
      {
        return this.root.LinkStartMember;
      }
      set
      {
        this.root.LinkStartMember = value;
      }
    }

    public string LinkEndMember
    {
      get
      {
        return this.root.LinkEndMember;
      }
      set
      {
        this.root.LinkEndMember = value;
      }
    }

    public string LinkTypeMember
    {
      get
      {
        return this.root.LinkTypeMember;
      }
      set
      {
        this.root.LinkTypeMember = value;
      }
    }

    public bool IsDataBound
    {
      get
      {
        return this.root.DataSource != null;
      }
    }

    public PropertyDescriptorCollection BoundProperties
    {
      get
      {
        return this.boundProperties;
      }
    }

    public int GetTasksVersion()
    {
      if (this.taskMetadata != null)
        return this.taskMetadata.Version;
      return -1;
    }

    public int GetLinksVersion()
    {
      if (this.linkMetadata != null)
        return this.linkMetadata.Version;
      return -1;
    }

    public override IList<GanttViewDataItem> GetItems(GanttViewDataItem parent)
    {
      if (parent == null)
        parent = this.GanttView.Root;
      int boundIndex = parent.BoundIndex;
      this.BuildTasksIndex();
      IList<GanttViewDataItem> ganttViewDataItemList = this.BuildDataItems(parent, boundIndex);
      if (ganttViewDataItemList != null && ganttViewDataItemList.Count > 0 && this.root.DataSource != null)
        return ganttViewDataItemList;
      return (IList<GanttViewDataItem>) NotifyCollection<GanttViewDataItem>.Empty;
    }

    public IList<GanttViewLinkDataItem> GetLinkItems()
    {
      this.BuildTasksIndex();
      IList<GanttViewLinkDataItem> viewLinkDataItemList = this.BuildLinkDataItems();
      if (viewLinkDataItemList != null && viewLinkDataItemList.Count > 0 && this.DataSource != null)
        return viewLinkDataItemList;
      return (IList<GanttViewLinkDataItem>) NotifyCollection<GanttViewLinkDataItem>.Empty;
    }

    public override void Reset()
    {
      if (this.IsSuspended)
        return;
      if (this.root.DataSource != null)
        this.RegisterMetadata(this.root);
      this.SuspendUpdate();
      this.GanttView.Root.Items.ResetVersion();
      this.GanttView.Links.ResetVersion();
      this.GanttView.Update(RadGanttViewElement.UpdateActions.Reset);
      this.ResumeUpdate();
    }

    public override void Dispose()
    {
      this.root.PropertyChanged -= new PropertyChangedEventHandler(this.root_PropertyChanged);
      this.UnwireTaskEvents(this.taskMetadata.CurrencyManager);
      this.UnwireLinkEvents(this.linkMetadata.CurrencyManager);
      this.root = (GanttViewRelationBinding) null;
      base.Dispose();
    }

    public bool PreProcess(
      GanttViewDataItem parent,
      GanttViewDataItem item,
      params object[] metadata)
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

    public bool PostProcess(
      GanttViewDataItem parent,
      GanttViewDataItem item,
      params object[] metadata)
    {
      if (this.IsSuspended || metadata == null || metadata.Length == 0)
        return true;
      string str = (string) metadata[0];
      if (str == "Remove")
        return this.RemoveFromSource(parent, item);
      if (str == "EndEdit")
      {
        this.SuspendUpdate();
        try
        {
          (((IDataItem) item).DataBoundItem as IEditableObject)?.EndEdit();
        }
        catch (Exception ex)
        {
          this.GanttView.OnItemDataError(new GanttViewItemDataErrorEventArgs(item, ex.Message, new object[1]
          {
            (object) ex
          }));
        }
        this.ResumeUpdate();
      }
      return true;
    }

    public bool PreProcess(GanttViewLinkDataItem item, params object[] metadata)
    {
      if (this.IsSuspended || metadata == null || metadata.Length == 0)
        return true;
      string str = (string) metadata[0];
      if (str == "Add" || str == "Insert")
        return this.AddLinkToSource(item);
      if (str == "Remove")
        return this.RemoveLinkFromSource(item);
      return true;
    }

    public bool PostProcess(GanttViewLinkDataItem item, params object[] metadata)
    {
      if (this.IsSuspended || metadata == null || metadata.Length == 0)
        return true;
      string str = (string) metadata[0];
      if (str == "Remove")
        return this.RemoveLinkFromSource(item);
      if (str == "EndEdit")
      {
        this.SuspendUpdate();
        try
        {
          (((IDataItem) item).DataBoundItem as IEditableObject)?.EndEdit();
        }
        catch (Exception ex)
        {
          this.GanttView.OnLinkDataError(new GanttViewLinkDataErrorEventArgs(item, ex.Message, new object[1]
          {
            (object) ex
          }));
        }
        this.ResumeUpdate();
      }
      return true;
    }

    public object GetBoundValue(object dataBoundItem, string propertyName)
    {
      if (!this.IsDataBound)
        return (object) null;
      PropertyDescriptor propertyDescriptor = this.BoundProperties.Find(propertyName, true);
      if (propertyDescriptor == null)
        throw new ArgumentException("There is no property descriptor corresponding to property name: " + propertyName);
      return propertyDescriptor.GetValue(dataBoundItem);
    }

    public bool SetBoundValue(IDataItem dataItem, string propertyName, object value, string path)
    {
      return this.SetBoundValue(dataItem, propertyName, propertyName, value, path);
    }

    public bool SetBoundValue(
      IDataItem dataItem,
      string propertyName,
      string columnName,
      object value,
      string path)
    {
      if (!this.IsDataBound)
        return false;
      object dataBoundItem = dataItem.DataBoundItem;
      if (string.IsNullOrEmpty(path))
      {
        PropertyDescriptor propertyDescriptor = this.BoundProperties.Find(propertyName, true);
        if (value == null)
        {
          try
          {
            propertyDescriptor.SetValue(dataBoundItem, value);
          }
          catch
          {
            propertyDescriptor.SetValue(dataBoundItem, (object) DBNull.Value);
          }
        }
        else
        {
          System.Type underlyingType = Nullable.GetUnderlyingType(propertyDescriptor.PropertyType);
          if (propertyDescriptor.Converter != null && (object) underlyingType != null && underlyingType.IsGenericType)
            value = propertyDescriptor.Converter.ConvertFromInvariantString(value.ToString());
          propertyDescriptor.SetValue(dataBoundItem, value);
        }
      }
      else
        this.SetSubPropertyValue(path, dataBoundItem, value);
      if (propertyName == this.taskMetadata.TaskDescriptor.StartDescriptor.Name || propertyName == this.taskMetadata.TaskDescriptor.EndDescriptor.Name)
      {
        GanttViewDataItem ganttViewDataItem = dataItem as GanttViewDataItem;
        if (propertyName == this.taskMetadata.TaskDescriptor.StartDescriptor.Name && ganttViewDataItem.Start > ganttViewDataItem.End)
          this.SetBoundValue(dataItem, this.taskMetadata.TaskDescriptor.EndDescriptor.Name, (object) ganttViewDataItem.Start.AddDays(1.0), path);
        else if (propertyName == this.taskMetadata.TaskDescriptor.EndDescriptor.Name && ganttViewDataItem.End < ganttViewDataItem.Start)
          this.SetBoundValue(dataItem, this.taskMetadata.TaskDescriptor.StartDescriptor.Name, (object) ganttViewDataItem.End.AddDays(-1.0), path);
      }
      return true;
    }

    public override void SetCurrent(GanttViewDataItem item)
    {
      if (item == null || this.IsSuspended || (item.BoundIndex < 0 || this.taskMetadata == null) || this.taskMetadata.CurrencyManager == null)
        return;
      this.SuspendUpdate();
      CurrencyManager currencyManager = this.taskMetadata.CurrencyManager;
      int num = currencyManager.List.IndexOf(((IDataItem) item).DataBoundItem);
      if (num >= 0)
      {
        try
        {
          currencyManager.Position = num;
        }
        catch (Exception ex)
        {
          this.GanttView.OnItemDataError(new GanttViewItemDataErrorEventArgs(item, ex.Message, new object[1]
          {
            (object) ex
          }));
        }
      }
      this.ResumeUpdate();
    }

    private void SetSubPropertyValue(string propertyPath, object dataObject, object value)
    {
      PropertyDescriptor innerDescriptor = (PropertyDescriptor) null;
      object innerObject = (object) null;
      this.GetSubPropertyByPath(propertyPath, dataObject, out innerDescriptor, out innerObject);
      innerDescriptor?.SetValue(innerObject, value);
    }

    private void GetSubPropertyByPath(
      string propertyPath,
      object dataObject,
      out PropertyDescriptor innerDescriptor,
      out object innerObject)
    {
      string[] strArray = propertyPath.Split('.');
      innerDescriptor = this.boundProperties[strArray[0]];
      innerObject = innerDescriptor.GetValue(dataObject);
      for (int index = 1; index < strArray.Length && innerDescriptor != null; ++index)
      {
        innerDescriptor = innerDescriptor.GetChildProperties()[strArray[index]];
        if (index + 1 != strArray.Length)
          innerObject = innerDescriptor.GetValue(innerObject);
      }
    }

    private CurrencyManager GetTasksCurrencyManager(GanttViewRelationBinding relation)
    {
      CurrencyManager currencyManager = (CurrencyManager) null;
      if (relation.DataSource is BindingSource)
        currencyManager = string.IsNullOrEmpty(relation.TaskDataMember) || this.GanttView.BindingContext == null ? ((BindingSource) relation.DataSource).CurrencyManager : this.GanttView.BindingContext[((BindingSource) relation.DataSource).DataSource, relation.TaskDataMember] as CurrencyManager;
      if (currencyManager == null && relation.DataSource != null && this.GanttView.BindingContext != null)
        currencyManager = this.GanttView.BindingContext[relation.DataSource, relation.TaskDataMember] as CurrencyManager;
      return currencyManager;
    }

    private CurrencyManager GetLinksCurrencyManager(GanttViewRelationBinding relation)
    {
      CurrencyManager currencyManager = (CurrencyManager) null;
      if (relation.DataSource is BindingSource)
        currencyManager = string.IsNullOrEmpty(relation.LinkDataMember) || this.GanttView.BindingContext == null ? ((BindingSource) relation.DataSource).CurrencyManager : this.GanttView.BindingContext[((BindingSource) relation.DataSource).DataSource, relation.LinkDataMember] as CurrencyManager;
      if (currencyManager == null && relation.DataSource != null && this.GanttView.BindingContext != null)
        currencyManager = this.GanttView.BindingContext[relation.DataSource, relation.LinkDataMember] as CurrencyManager;
      return currencyManager;
    }

    private void RegisterMetadata(GanttViewRelationBinding relation)
    {
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      CurrencyManager tasksCurrencyManager = this.GetTasksCurrencyManager(relation);
      if (tasksCurrencyManager == null)
        return;
      foreach (PropertyDescriptor property in this.GetProperties(tasksCurrencyManager))
        propertyDescriptorList.Add(property);
      this.boundProperties = new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
      this.taskMetadata = new GanttViewBindingProvider.TaskMetadata();
      this.UnwireTaskEvents(tasksCurrencyManager);
      this.WireTaskEvents(tasksCurrencyManager);
      this.taskMetadata.CurrencyManager = tasksCurrencyManager;
      this.taskMetadata.TaskDescriptor = this.GetTaskDescriptor(relation, tasksCurrencyManager);
      ++this.taskMetadata.Version;
      CurrencyManager linksCurrencyManager = this.GetLinksCurrencyManager(relation);
      if (linksCurrencyManager == null)
        return;
      foreach (PropertyDescriptor property in this.GetProperties(linksCurrencyManager))
        propertyDescriptorList.Add(property);
      this.boundProperties = new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
      this.linkMetadata = new GanttViewBindingProvider.LinkMetadata();
      if (linksCurrencyManager != tasksCurrencyManager)
      {
        this.UnwireLinkEvents(linksCurrencyManager);
        this.WireLinkEvents(linksCurrencyManager);
      }
      this.linkMetadata.CurrencyManager = linksCurrencyManager;
      this.linkMetadata.LinkDescriptor = this.GetLinkDescriptor(relation, linksCurrencyManager);
      ++this.linkMetadata.Version;
    }

    private void WireTaskEvents(CurrencyManager tasksCurrencyManager)
    {
      tasksCurrencyManager.ListChanged += new ListChangedEventHandler(this.tasksCurrencyManager_ListChanged);
      tasksCurrencyManager.PositionChanged += new EventHandler(this.tasksCurrencyManager_PositionChanged);
    }

    private void UnwireTaskEvents(CurrencyManager tasksCurrencyManager)
    {
      tasksCurrencyManager.ListChanged -= new ListChangedEventHandler(this.tasksCurrencyManager_ListChanged);
      tasksCurrencyManager.PositionChanged -= new EventHandler(this.tasksCurrencyManager_PositionChanged);
    }

    private void WireLinkEvents(CurrencyManager linkCurrencyManager)
    {
      linkCurrencyManager.ListChanged += new ListChangedEventHandler(this.linkCurrencyManager_ListChanged);
    }

    private void UnwireLinkEvents(CurrencyManager linkCurrencyManager)
    {
      linkCurrencyManager.ListChanged -= new ListChangedEventHandler(this.linkCurrencyManager_ListChanged);
    }

    private GanttTaskDescriptor GetTaskDescriptor(
      GanttViewRelationBinding relation,
      CurrencyManager tcm)
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
      if (!string.IsNullOrEmpty(relation.TitleMember))
        str3 = relation.TitleMember.Split('\\')[0];
      else
        str3 = (string) null;
      string path3 = str3;
      string str4;
      if (!string.IsNullOrEmpty(relation.StartMember))
        str4 = relation.StartMember.Split('\\')[0];
      else
        str4 = (string) null;
      string path4 = str4;
      string str5;
      if (!string.IsNullOrEmpty(relation.EndMember))
        str5 = relation.EndMember.Split('\\')[0];
      else
        str5 = (string) null;
      string path5 = str5;
      string str6;
      if (!string.IsNullOrEmpty(relation.ProgressMember))
        str6 = relation.ProgressMember.Split('\\')[0];
      else
        str6 = (string) null;
      string path6 = str6;
      GanttTaskDescriptor ganttTaskDescriptor = new GanttTaskDescriptor();
      if (path1 != null)
      {
        string[] strArray = path1.Split('.');
        ganttTaskDescriptor.ChildDescriptor = this.boundProperties.Find(strArray[0], true);
        if (ganttTaskDescriptor.ChildDescriptor != null && path1.Length > 1)
          ganttTaskDescriptor.SetChildDescriptor(ganttTaskDescriptor.ChildDescriptor, path1);
      }
      if (path2 != null)
      {
        string[] strArray = path2.Split('.');
        ganttTaskDescriptor.ParentDescriptor = this.boundProperties.Find(strArray[0], true);
        if (ganttTaskDescriptor.ParentDescriptor != null && path2.Length > 1)
          ganttTaskDescriptor.SetParentDescriptor(ganttTaskDescriptor.ParentDescriptor, path2);
      }
      if (path3 != null)
      {
        string[] strArray = path3.Split('.');
        ganttTaskDescriptor.TitleDescriptor = this.boundProperties.Find(strArray[0], true);
        if (ganttTaskDescriptor.TitleDescriptor != null && path3.Length > 1)
          ganttTaskDescriptor.SetTitleDescriptor(ganttTaskDescriptor.TitleDescriptor, path3);
      }
      if (path4 != null)
      {
        string[] strArray = path4.Split('.');
        ganttTaskDescriptor.StartDescriptor = this.boundProperties.Find(strArray[0], true);
        if (ganttTaskDescriptor.StartDescriptor != null && path4.Length > 1)
          ganttTaskDescriptor.SetStartDescriptor(ganttTaskDescriptor.StartDescriptor, path4);
      }
      if (path5 != null)
      {
        string[] strArray = path5.Split('.');
        ganttTaskDescriptor.EndDescriptor = this.boundProperties.Find(strArray[0], true);
        if (ganttTaskDescriptor.EndDescriptor != null && path5.Length > 1)
          ganttTaskDescriptor.SetEndDescriptor(ganttTaskDescriptor.EndDescriptor, path5);
      }
      if (path6 != null)
      {
        string[] strArray = path6.Split('.');
        ganttTaskDescriptor.ProgressDescriptor = this.boundProperties.Find(strArray[0], true);
        if (ganttTaskDescriptor.ProgressDescriptor != null && path6.Length > 1)
          ganttTaskDescriptor.SetProgressDescriptor(ganttTaskDescriptor.ProgressDescriptor, path6);
      }
      return ganttTaskDescriptor;
    }

    private GanttLinkDescriptor GetLinkDescriptor(
      GanttViewRelationBinding relation,
      CurrencyManager lcm)
    {
      string str1;
      if (!string.IsNullOrEmpty(relation.LinkStartMember))
        str1 = relation.LinkStartMember.Split('\\')[0];
      else
        str1 = (string) null;
      string path1 = str1;
      string str2;
      if (!string.IsNullOrEmpty(relation.LinkEndMember))
        str2 = relation.LinkEndMember.Split('\\')[0];
      else
        str2 = (string) null;
      string path2 = str2;
      string str3;
      if (!string.IsNullOrEmpty(relation.LinkTypeMember))
        str3 = relation.LinkTypeMember.Split('\\')[0];
      else
        str3 = (string) null;
      string path3 = str3;
      GanttLinkDescriptor ganttLinkDescriptor = new GanttLinkDescriptor();
      if (path1 != null)
      {
        string[] strArray = path1.Split('.');
        ganttLinkDescriptor.StartItemDescriptor = this.boundProperties.Find(strArray[0], true);
        if (ganttLinkDescriptor.StartItemDescriptor != null && path1.Length > 1)
          ganttLinkDescriptor.SetStartItemDescriptor(ganttLinkDescriptor.StartItemDescriptor, path1);
      }
      if (path2 != null)
      {
        string[] strArray = path2.Split('.');
        ganttLinkDescriptor.EndItemDescriptor = this.boundProperties.Find(strArray[0], true);
        if (ganttLinkDescriptor.EndItemDescriptor != null && path2.Length > 1)
          ganttLinkDescriptor.SetEndItemDescriptor(ganttLinkDescriptor.EndItemDescriptor, path2);
      }
      if (path3 != null)
      {
        string[] strArray = path3.Split('.');
        ganttLinkDescriptor.LinkTypeDescriptor = this.boundProperties.Find(strArray[0], true);
        if (ganttLinkDescriptor.LinkTypeDescriptor != null && path3.Length > 1)
          ganttLinkDescriptor.SetLinkTypeDescriptor(ganttLinkDescriptor.LinkTypeDescriptor, path3);
      }
      return ganttLinkDescriptor;
    }

    private PropertyDescriptorCollection GetProperties(
      CurrencyManager cm)
    {
      System.Type listItemType = ListBindingHelper.GetListItemType((object) cm.List);
      if ((object) listItemType != null && listItemType.IsInterface && cm.List.Count > 0)
        return TypeDescriptor.GetProperties(cm.List[0]);
      if (this.GanttView.DataProvider != null && TelerikHelper.IsCompatibleDataSource())
        return TypeDescriptor.GetProperties(listItemType);
      return cm.GetItemProperties();
    }

    private void BuildTasksIndex()
    {
      if (this.taskMetadata.GanttTasks != null && this.taskMetadata.GanttTasks.Count > 0 && (this.linkMetadata.GanttTasks != null && this.linkMetadata.GanttTasks.Count > 0) || (object) this.taskMetadata.TaskDescriptor.ChildDescriptor == null)
        return;
      if ((object) this.taskMetadata.TaskDescriptor.ParentDescriptor == null)
      {
        object maxValue = (object) int.MaxValue;
      }
      GanttViewBindingProvider.GanttTaskList ganttTaskList1 = new GanttViewBindingProvider.GanttTaskList(this, this.taskMetadata.CurrencyManager.Count);
      GanttViewBindingProvider.GanttTaskList ganttTaskList2 = new GanttViewBindingProvider.GanttTaskList(this, this.taskMetadata.CurrencyManager.Count);
      for (int index = 0; index < this.taskMetadata.CurrencyManager.Count; ++index)
      {
        object obj = this.taskMetadata.CurrencyManager.List[index];
        object parentKey = this.GetParentKey(obj);
        object childKey = this.GetChildKey(obj);
        ganttTaskList1.AddTask(parentKey, obj);
        ganttTaskList2.AddTask(childKey, obj);
      }
      this.taskMetadata.GanttTasks = ganttTaskList1;
      this.linkMetadata.GanttTasks = ganttTaskList2;
    }

    private IList<GanttViewDataItem> BuildDataItems(
      GanttViewDataItem parent,
      int index)
    {
      bool flag = parent == this.GanttView.Root;
      GanttViewBindingProvider.GanttTaskList ganttTasks = this.taskMetadata.GanttTasks;
      if (ganttTasks == null || ganttTasks.Count <= 0)
        return (IList<GanttViewDataItem>) NotifyCollection<GanttViewDataItem>.Empty;
      if (flag)
        return (IList<GanttViewDataItem>) ganttTasks.GetGanttDataItems(0, index);
      object childKey = this.GetChildKey(((IDataItem) parent).DataBoundItem);
      if (this.GetParentKey(((IDataItem) parent).DataBoundItem) == childKey)
        return (IList<GanttViewDataItem>) NotifyCollection<GanttViewDataItem>.Empty;
      if (parent.Level > 0 && (childKey == null || childKey == DBNull.Value))
        return (IList<GanttViewDataItem>) NotifyCollection<GanttViewDataItem>.Empty;
      return (IList<GanttViewDataItem>) ganttTasks.GetGanttDataItems(childKey, index);
    }

    private IList<GanttViewLinkDataItem> BuildLinkDataItems()
    {
      if (this.linkMetadata.GanttTasks == null || this.linkMetadata.LinkDescriptor.StartItemDescriptor == null || (this.linkMetadata.LinkDescriptor.EndItemDescriptor == null || this.linkMetadata.LinkDescriptor.LinkTypeDescriptor == null))
        return (IList<GanttViewLinkDataItem>) null;
      List<GanttViewLinkDataItem> viewLinkDataItemList = new List<GanttViewLinkDataItem>();
      foreach (object obj in (IEnumerable) this.linkMetadata.CurrencyManager.List)
      {
        GanttViewLinkDataItem newLink = this.GanttView.CreateNewLink();
        ((IDataItem) newLink).DataBoundItem = obj;
        object linkStartKey = this.GetLinkStartKey(obj);
        object linkEndKey = this.GetLinkEndKey(obj);
        if (this.linkMetadata.GanttTasks.Contains(linkStartKey) && this.linkMetadata.GanttTasks.Contains(linkEndKey))
        {
          newLink.StartItem = this.linkMetadata.GanttTasks[linkStartKey][0] as GanttViewDataItem;
          newLink.EndItem = this.linkMetadata.GanttTasks[linkEndKey][0] as GanttViewDataItem;
          newLink.LinkType = this.GanttView.LinkTypeConverter.ConvertToLinkType(this.linkMetadata.LinkDescriptor.LinkTypeDescriptor.GetValue(obj));
          viewLinkDataItemList.Add(newLink);
        }
      }
      return (IList<GanttViewLinkDataItem>) viewLinkDataItemList;
    }

    private object GetChildKey(object dataBoundItem)
    {
      if (this.taskMetadata.TaskDescriptor.ChildDescriptor == null)
        return (object) null;
      try
      {
        return this.taskMetadata.TaskDescriptor.ChildDescriptor.GetValue(dataBoundItem);
      }
      catch
      {
        return (object) null;
      }
    }

    private object GetParentKey(object dataBoundItem)
    {
      if (this.taskMetadata.TaskDescriptor.ParentDescriptor == null)
        return (object) null;
      try
      {
        return this.taskMetadata.TaskDescriptor.ParentDescriptor.GetValue(dataBoundItem);
      }
      catch
      {
        return (object) null;
      }
    }

    private object GetLinkStartKey(object dataBoundItem)
    {
      if (this.linkMetadata.LinkDescriptor.StartItemDescriptor == null)
        return (object) null;
      return this.linkMetadata.LinkDescriptor.StartItemDescriptor.GetValue(dataBoundItem);
    }

    private object GetLinkEndKey(object dataBoundItem)
    {
      if (this.linkMetadata.LinkDescriptor.EndItemDescriptor == null)
        return (object) null;
      return this.linkMetadata.LinkDescriptor.EndItemDescriptor.GetValue(dataBoundItem);
    }

    private void SetChildKey(object dataBoundItem, object value)
    {
      if (this.taskMetadata.TaskDescriptor.ChildDescriptor == null)
        return;
      this.taskMetadata.TaskDescriptor.ChildDescriptor.SetValue(dataBoundItem, value);
    }

    private void SetParentKey(object dataBoundItem, object value)
    {
      if (this.taskMetadata.TaskDescriptor.ParentDescriptor == null)
        return;
      this.taskMetadata.TaskDescriptor.ParentDescriptor.SetValue(dataBoundItem, value);
    }

    private void SetLinkStartKey(object dataBoundItem, object value)
    {
      if (this.linkMetadata.LinkDescriptor.StartItemDescriptor == null)
        return;
      this.linkMetadata.LinkDescriptor.StartItemDescriptor.SetValue(dataBoundItem, value);
    }

    private void SetLinkEndKey(object dataBoundItem, object value)
    {
      if (this.linkMetadata.LinkDescriptor.EndItemDescriptor == null)
        return;
      this.linkMetadata.LinkDescriptor.EndItemDescriptor.SetValue(dataBoundItem, value);
    }

    private void tasksCurrencyManager_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (this.IsSuspended)
        return;
      switch (e.ListChangedType)
      {
        case ListChangedType.Reset:
        case ListChangedType.PropertyDescriptorAdded:
        case ListChangedType.PropertyDescriptorDeleted:
        case ListChangedType.PropertyDescriptorChanged:
          this.Reset();
          break;
        case ListChangedType.ItemAdded:
          this.AddTaskByCM(sender as CurrencyManager, e.NewIndex);
          break;
        case ListChangedType.ItemDeleted:
          this.DeleteTaskByCM(sender as CurrencyManager, e.NewIndex);
          break;
        case ListChangedType.ItemChanged:
          this.EditTaskByCM(sender as CurrencyManager, e.NewIndex, e.PropertyDescriptor);
          break;
      }
    }

    private void tasksCurrencyManager_PositionChanged(object sender, EventArgs e)
    {
      if (this.IsSuspended)
        return;
      CurrencyManager tasksCurrencyManager = this.GetTasksCurrencyManager(this.root);
      if (tasksCurrencyManager == null)
        return;
      this.SuspendUpdate();
      IList<GanttViewDataItem> items = (IList<GanttViewDataItem>) null;
      if (tasksCurrencyManager.Position < 0 && tasksCurrencyManager.Count > 0)
        this.GanttView.SelectedItem = (GanttViewDataItem) null;
      else if (tasksCurrencyManager.Position < tasksCurrencyManager.Count && tasksCurrencyManager.Count > 0)
        items = this.GetItems(tasksCurrencyManager.Current);
      if (items != null)
      {
        this.postponeSelection = items.Count > tasksCurrencyManager.Count;
        if (!this.postponeSelection)
          this.SetSelectedTask(tasksCurrencyManager.Current, items);
      }
      this.ResumeUpdate();
    }

    private void linkCurrencyManager_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (this.IsSuspended)
        return;
      switch (e.ListChangedType)
      {
        case ListChangedType.Reset:
        case ListChangedType.PropertyDescriptorAdded:
        case ListChangedType.PropertyDescriptorDeleted:
        case ListChangedType.PropertyDescriptorChanged:
          this.Reset();
          break;
        case ListChangedType.ItemAdded:
          this.AddLinkByCM(sender as CurrencyManager, e.NewIndex);
          break;
        case ListChangedType.ItemDeleted:
          this.DeleteLinkByCM(sender as CurrencyManager, e.OldIndex);
          break;
        case ListChangedType.ItemChanged:
          this.EditLinkByCM(sender as CurrencyManager, e.NewIndex, e.PropertyDescriptor);
          break;
      }
    }

    private void AddTaskByCM(CurrencyManager cm, int index)
    {
      if (cm == null || index >= 0 && index < cm.Count && cm.List.Count == index)
        return;
      object obj = cm.List[index];
      if (this.addRef != null && this.addRef.IsAlive && this.addRef.Target == obj)
        return;
      this.addRef = new WeakReference(obj);
      object parentKey = this.GetParentKey(obj);
      object childKey = this.GetChildKey(obj);
      if (this.taskMetadata.GanttTasks != null)
      {
        this.taskMetadata.GanttTasks.AddTask(parentKey, obj);
        this.linkMetadata.GanttTasks.AddTask(childKey, obj);
        List<object> ganttTask = this.taskMetadata.GanttTasks[parentKey];
        if (ganttTask != null && ganttTask.Count > 0 && (ganttTask[0] is GanttViewDataItem && ((GanttViewDataItem) ganttTask[0]).parent != null))
        {
          ((GanttViewDataItem) ganttTask[0]).parent.items.ResetVersion();
          this.GanttView.Update(RadGanttViewElement.UpdateActions.Resume);
          return;
        }
      }
      this.taskMetadata.GanttTasks = (GanttViewBindingProvider.GanttTaskList) null;
      ++this.taskMetadata.Version;
      this.GanttView.Update(RadGanttViewElement.UpdateActions.Reset);
    }

    private void EditTaskByCM(CurrencyManager cm, int index, PropertyDescriptor pd)
    {
      if (cm == null || pd == null)
        return;
      object obj = cm.List[index];
      PropertyDescriptor parentDescriptor = this.taskMetadata.TaskDescriptor.ParentDescriptor;
      if (parentDescriptor != null && parentDescriptor.Name == pd.Name)
      {
        this.taskMetadata.GanttTasks = (GanttViewBindingProvider.GanttTaskList) null;
        ++this.taskMetadata.Version;
        this.GanttView.Update(RadGanttViewElement.UpdateActions.Reset);
      }
      else
      {
        if (this.GanttView.IsEditing)
          return;
        this.GanttView.Update(RadGanttViewElement.UpdateActions.Resume);
      }
    }

    private void DeleteTaskByCM(CurrencyManager cm, int index)
    {
      if (cm == null)
        return;
      this.taskMetadata.GanttTasks = (GanttViewBindingProvider.GanttTaskList) null;
      ++this.taskMetadata.Version;
      this.GanttView.Update(RadGanttViewElement.UpdateActions.Reset);
      if (!this.postponeSelection)
        return;
      this.postponeSelection = false;
      IList<GanttViewDataItem> items = this.GetItems(cm.Current);
      this.SetSelectedTask(cm.Current, items);
    }

    private void AddLinkByCM(CurrencyManager cm, int index)
    {
      if (cm == null || index >= 0 && index < cm.Count && cm.List.Count == index)
        return;
      object obj = cm.List[index];
      if (this.addRef != null && this.addRef.IsAlive && this.addRef.Target == obj)
        return;
      this.addRef = new WeakReference(obj);
      this.GetLinkStartKey(obj);
      this.GetLinkEndKey(obj);
      if (this.linkMetadata.GanttTasks != null && this.GanttView.HasLinks)
      {
        this.GanttView.Links.ResetVersion();
        this.GanttView.Update(RadGanttViewElement.UpdateActions.Resume);
      }
      else
      {
        this.linkMetadata.GanttTasks = (GanttViewBindingProvider.GanttTaskList) null;
        ++this.linkMetadata.Version;
        this.GanttView.Update(RadGanttViewElement.UpdateActions.Reset);
      }
    }

    private void EditLinkByCM(CurrencyManager cm, int index, PropertyDescriptor pd)
    {
      if (cm == null || pd == null)
        return;
      if (this.GanttView.HasLinks)
      {
        this.GanttView.Links.ResetVersion();
        this.GanttView.Update(RadGanttViewElement.UpdateActions.Resume);
      }
      else
      {
        this.linkMetadata.GanttTasks = (GanttViewBindingProvider.GanttTaskList) null;
        ++this.linkMetadata.Version;
        this.GanttView.Update(RadGanttViewElement.UpdateActions.Reset);
      }
    }

    private void DeleteLinkByCM(CurrencyManager cm, int index)
    {
      if (cm == null)
        return;
      if (this.GanttView.HasLinks)
      {
        this.GanttView.Links.ResetVersion();
        this.GanttView.Update(RadGanttViewElement.UpdateActions.Resume);
      }
      else
      {
        this.linkMetadata.GanttTasks = (GanttViewBindingProvider.GanttTaskList) null;
        ++this.linkMetadata.Version;
        this.GanttView.Update(RadGanttViewElement.UpdateActions.Resume);
      }
    }

    private void SetSelectedTask(object dataItem, IList<GanttViewDataItem> items)
    {
      if (items == null || items.Count == 0)
        return;
      for (int index = 0; index < items.Count; ++index)
      {
        GanttViewDataItem ganttViewDataItem = items[index];
        if (((IDataItem) ganttViewDataItem).DataBoundItem == dataItem)
        {
          this.GanttView.SelectedItem = ganttViewDataItem;
          this.ExpandEnsureVisible();
          break;
        }
      }
    }

    private bool AddToSource(GanttViewDataItem parent, GanttViewDataItem item)
    {
      int boundIndex = parent.BoundIndex;
      if (this.taskMetadata.CurrencyManager == null)
        return true;
      bool flag = true;
      this.SuspendUpdate();
      try
      {
        CurrencyManager currencyManager = this.taskMetadata.CurrencyManager;
        currencyManager.AddNew();
        object obj = currencyManager.List[currencyManager.Count - 1];
        ((IDataItem) item).DataBoundItem = obj;
        item.BoundIndex = boundIndex;
        if (this.taskMetadata.TaskDescriptor.TitleDescriptor != null)
          this.taskMetadata.TaskDescriptor.TitleDescriptor.SetValue(obj, (object) item.Title);
        if (this.taskMetadata.TaskDescriptor.StartDescriptor != null)
          this.taskMetadata.TaskDescriptor.StartDescriptor.SetValue(obj, (object) item.Start);
        if (this.taskMetadata.TaskDescriptor.EndDescriptor != null)
          this.taskMetadata.TaskDescriptor.EndDescriptor.SetValue(obj, (object) item.End);
        if (this.taskMetadata.TaskDescriptor.ProgressDescriptor != null)
          this.taskMetadata.TaskDescriptor.ProgressDescriptor.SetValue(obj, (object) item.Progress);
        object key = !(parent is RadGanttViewElement.RootDataItem) || parent.items.Count <= 0 ? this.GetChildKey(((IDataItem) parent).DataBoundItem) : this.GetParentKey(((IDataItem) parent.items[0]).DataBoundItem);
        this.SetParentKey(obj, key);
        if (this.taskMetadata.GanttTasks != null)
          this.taskMetadata.GanttTasks.AddTask(key, (object) item);
        if (this.linkMetadata.GanttTasks != null)
        {
          GanttViewItemChildIdNeededEventArgs e = new GanttViewItemChildIdNeededEventArgs(item);
          this.GanttView.OnItemChildIdNeeded(e);
          object childId = e.ChildId;
          if (childId == null)
            throw new ArgumentException("You must provide a valid Id for new items. You can use the ItemChildIdNeeded event of RadGanttView for this purpose.");
          this.SetChildKey(item.DataBoundItem, childId);
          this.linkMetadata.GanttTasks.AddTask(childId, (object) item);
        }
      }
      catch (Exception ex)
      {
        if (ex is ArgumentException && ex.Message.Contains("ItemChildIdNeeded"))
          throw ex;
        this.GanttView.OnItemDataError(new GanttViewItemDataErrorEventArgs(item, ex.Message, new object[1]
        {
          (object) ex
        }));
        flag = false;
      }
      this.ResumeUpdate();
      return flag;
    }

    private bool RemoveFromSource(GanttViewDataItem parent, GanttViewDataItem item)
    {
      CurrencyManager currencyManager = this.taskMetadata.CurrencyManager;
      object parentKey = this.GetParentKey(((IDataItem) item).DataBoundItem);
      object childKey = this.GetChildKey(((IDataItem) item).DataBoundItem);
      if (this.taskMetadata.GanttTasks != null)
        this.taskMetadata.GanttTasks.RemoveTask(parentKey, (object) item);
      if (this.linkMetadata.GanttTasks != null)
        this.linkMetadata.GanttTasks.RemoveTask(childKey, (object) item);
      int count = currencyManager.Count;
      int index1 = currencyManager.List.IndexOf(((IDataItem) item).DataBoundItem);
      if (index1 >= 0)
      {
        this.SuspendUpdate();
        currencyManager.List.RemoveAt(index1);
        this.ResumeUpdate();
      }
      this.SuspendUpdate();
      int index2 = 0;
      while (index2 < this.linkMetadata.CurrencyManager.List.Count)
      {
        object dataBoundItem = this.linkMetadata.CurrencyManager.List[index2];
        if (childKey != null && (childKey.Equals(this.GetLinkStartKey(dataBoundItem)) || childKey.Equals(this.GetLinkEndKey(dataBoundItem))))
          this.linkMetadata.CurrencyManager.List.RemoveAt(index2);
        else
          ++index2;
      }
      this.ResumeUpdate();
      parent?.items.ResetVersion();
      this.GanttView.Links.ResetVersion();
      return count != currencyManager.Count;
    }

    private bool AddLinkToSource(GanttViewLinkDataItem item)
    {
      if (this.linkMetadata.CurrencyManager == null)
        return true;
      bool flag = true;
      this.SuspendUpdate();
      try
      {
        CurrencyManager currencyManager = this.linkMetadata.CurrencyManager;
        currencyManager.AddNew();
        object obj1 = currencyManager.List[currencyManager.Count - 1];
        ((IDataItem) item).DataBoundItem = obj1;
        object childKey1 = this.GetChildKey(((IDataItem) item.StartItem).DataBoundItem);
        this.SetLinkStartKey(obj1, childKey1);
        object childKey2 = this.GetChildKey(((IDataItem) item.EndItem).DataBoundItem);
        this.SetLinkEndKey(obj1, childKey2);
        if (this.linkMetadata.LinkDescriptor.LinkTypeDescriptor != null)
        {
          object obj2 = this.GanttView.LinkTypeConverter.ConvertFromLinkType(item.LinkType);
          this.linkMetadata.LinkDescriptor.LinkTypeDescriptor.SetValue(obj1, obj2);
        }
      }
      catch (Exception ex)
      {
        this.GanttView.OnLinkDataError(new GanttViewLinkDataErrorEventArgs(item, ex.Message, new object[1]
        {
          (object) ex
        }));
        flag = false;
      }
      this.ResumeUpdate();
      return flag;
    }

    private bool RemoveLinkFromSource(GanttViewLinkDataItem item)
    {
      CurrencyManager currencyManager = this.linkMetadata.CurrencyManager;
      int count = currencyManager.Count;
      int index = currencyManager.List.IndexOf(((IDataItem) item).DataBoundItem);
      if (index >= 0)
      {
        this.SuspendUpdate();
        currencyManager.List.RemoveAt(index);
        this.ResumeUpdate();
      }
      this.GanttView.Links.ResetVersion();
      return count != currencyManager.Count;
    }

    private IList<GanttViewDataItem> GetItems(object dataItem)
    {
      return this.taskMetadata.GanttTasks == null ? (IList<GanttViewDataItem>) this.GanttView.Root.items : (IList<GanttViewDataItem>) this.taskMetadata.GanttTasks.GetGanttDataItems(this.GetParentKey(dataItem), 0);
    }

    private void root_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Reset();
      if (!(e.PropertyName == "DataSource"))
        return;
      foreach (GanttViewTextViewColumn column in (Collection<GanttViewTextViewColumn>) this.GanttView.Columns)
        column.Initialize();
    }

    private void ExpandEnsureVisible()
    {
      if (this.GanttView.SelectedItem == null)
        return;
      this.GanttView.BeginUpdate();
      for (GanttViewDataItem parent = this.GanttView.SelectedItem.Parent; parent != null && !parent.Expanded; parent = parent.Parent)
        parent.Expand();
      this.GanttView.EndUpdate(true, RadGanttViewElement.UpdateActions.Resume);
      this.GanttView.EnsureVisible(this.GanttView.SelectedItem);
    }

    private class TaskMetadata
    {
      public int Version;
      public GanttViewBindingProvider.GanttTaskList GanttTasks;
      public CurrencyManager CurrencyManager;
      public GanttTaskDescriptor TaskDescriptor;
    }

    private class LinkMetadata
    {
      public int Version;
      public GanttViewBindingProvider.GanttTaskList GanttTasks;
      public CurrencyManager CurrencyManager;
      public GanttLinkDescriptor LinkDescriptor;
    }

    private class GanttTaskList
    {
      private GanttViewBindingProvider.GanttTaskList.TaskComparer comparer = new GanttViewBindingProvider.GanttTaskList.TaskComparer();
      private List<GanttViewBindingProvider.GanttTaskList.GanttTask> tasks;
      private GanttViewBindingProvider owner;

      public GanttTaskList(GanttViewBindingProvider owner, int capacity)
      {
        this.owner = owner;
        this.tasks = new List<GanttViewBindingProvider.GanttTaskList.GanttTask>(capacity);
      }

      public List<object> this[object key]
      {
        get
        {
          int index = this.IndexOf(key);
          if (index >= 0)
            return this.tasks[index].Tasks;
          return (List<object>) null;
        }
      }

      public List<object> this[int index]
      {
        get
        {
          return this.tasks[index].Tasks;
        }
      }

      public List<object> GetOrAdd(object key)
      {
        int index = this.tasks.BinarySearch(new GanttViewBindingProvider.GanttTaskList.GanttTask(key), (IComparer<GanttViewBindingProvider.GanttTaskList.GanttTask>) this.comparer);
        if (index >= 0)
          return this.tasks[index].Tasks;
        GanttViewBindingProvider.GanttTaskList.GanttTask ganttTask = new GanttViewBindingProvider.GanttTaskList.GanttTask(key, new List<object>());
        this.tasks.Insert(~index, ganttTask);
        return ganttTask.Tasks;
      }

      public bool Contains(object key)
      {
        return this.IndexOf(key) >= 0;
      }

      public int IndexOf(object key)
      {
        return this.tasks.BinarySearch(new GanttViewBindingProvider.GanttTaskList.GanttTask(key), (IComparer<GanttViewBindingProvider.GanttTaskList.GanttTask>) this.comparer);
      }

      public void AddTask(object key, object task)
      {
        this.GetOrAdd(key).Add(task);
      }

      public bool RemoveTask(object key, object task)
      {
        List<object> objectList = this[key];
        if (objectList == null)
          return false;
        bool flag = objectList.Remove(task);
        for (int index = 0; index < objectList.Count; ++index)
        {
          if (objectList[index] == task)
          {
            objectList.RemoveAt(index);
            break;
          }
          object obj = task;
          if (obj is GanttViewDataItem)
            obj = ((IDataItem) obj).DataBoundItem;
          object dataBoundItem = objectList[index];
          if (dataBoundItem is GanttViewDataItem)
            dataBoundItem = ((IDataItem) dataBoundItem).DataBoundItem;
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
        this.tasks.RemoveAt(index);
        return true;
      }

      public List<GanttViewDataItem> GetGanttDataItems(
        object key,
        int boundIndex)
      {
        object index1 = key;
        if (this.tasks.Count > 0)
        {
          object obj = (object) null;
          for (int index2 = this.tasks.Count - 1; index2 >= 0; --index2)
          {
            obj = this.tasks[this.tasks.Count - 1].Key;
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
          return (List<GanttViewDataItem>) null;
        List<GanttViewDataItem> ganttViewDataItemList = new List<GanttViewDataItem>(objectList.Count);
        for (int index2 = 0; index2 < objectList.Count; ++index2)
        {
          object dataBoundItem = objectList[index2];
          GanttViewDataItem ganttViewDataItem = dataBoundItem as GanttViewDataItem;
          if (ganttViewDataItem == null)
          {
            object childKey = this.owner.GetChildKey(dataBoundItem);
            ganttViewDataItem = this.owner.GanttView.CreateNewTask();
            ((IDataItem) ganttViewDataItem).DataBoundItem = dataBoundItem;
            this.owner.linkMetadata.GanttTasks[childKey][0] = (object) ganttViewDataItem;
            ganttViewDataItem.BoundIndex = boundIndex;
            objectList[index2] = (object) ganttViewDataItem;
          }
          ganttViewDataItemList.Add(ganttViewDataItem);
        }
        return ganttViewDataItemList;
      }

      public List<GanttViewDataItem> GetGanttDataItems(
        int index,
        int boundIndex)
      {
        List<object> tasks = this.tasks[index].Tasks;
        if (tasks == null)
          return (List<GanttViewDataItem>) null;
        List<GanttViewDataItem> ganttViewDataItemList = new List<GanttViewDataItem>(tasks.Count);
        for (int index1 = 0; index1 < tasks.Count; ++index1)
        {
          object dataBoundItem = tasks[index1];
          GanttViewDataItem ganttViewDataItem = dataBoundItem as GanttViewDataItem;
          if (ganttViewDataItem == null)
          {
            object childKey = this.owner.GetChildKey(dataBoundItem);
            ganttViewDataItem = this.owner.GanttView.CreateNewTask();
            ((IDataItem) ganttViewDataItem).DataBoundItem = dataBoundItem;
            this.owner.linkMetadata.GanttTasks[childKey][0] = (object) ganttViewDataItem;
            ganttViewDataItem.BoundIndex = boundIndex;
            tasks[index1] = (object) ganttViewDataItem;
          }
          ganttViewDataItemList.Add(ganttViewDataItem);
        }
        return ganttViewDataItemList;
      }

      public GanttViewDataItem GetTask(object key, object dataBoundItem)
      {
        List<object> objectList = this[key];
        if (objectList != null)
        {
          for (int index = 0; index < objectList.Count; ++index)
          {
            GanttViewDataItem ganttViewDataItem = objectList[index] as GanttViewDataItem;
            if (ganttViewDataItem != null && ((IDataItem) ganttViewDataItem).DataBoundItem == dataBoundItem)
              return ganttViewDataItem;
          }
        }
        return (GanttViewDataItem) null;
      }

      public int Count
      {
        get
        {
          return this.tasks.Count;
        }
      }

      private struct GanttTask
      {
        public object Key;
        public List<object> Tasks;

        public GanttTask(object key)
        {
          this = new GanttViewBindingProvider.GanttTaskList.GanttTask(key, (List<object>) null);
        }

        public GanttTask(object key, List<object> tasks)
        {
          this.Key = key;
          this.Tasks = tasks;
        }
      }

      private class TaskComparer : IComparer<GanttViewBindingProvider.GanttTaskList.GanttTask>
      {
        public int Compare(
          GanttViewBindingProvider.GanttTaskList.GanttTask x,
          GanttViewBindingProvider.GanttTaskList.GanttTask y)
        {
          if (x.Key == y.Key)
            return 0;
          if (x.Key == null || x.Key == DBNull.Value)
            return -1;
          if (y.Key == null || y.Key == DBNull.Value)
            return 1;
          IComparable key = x.Key as IComparable;
          if (key == null)
            throw new ArgumentException(string.Format("Argument {0} must implement IComparable", x.Key));
          return key.CompareTo(y.Key);
        }
      }
    }
  }
}
