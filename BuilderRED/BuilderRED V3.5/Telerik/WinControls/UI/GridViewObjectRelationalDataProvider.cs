// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewObjectRelationalDataProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using Telerik.Data.Expressions;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewObjectRelationalDataProvider : GridViewHierarchyDataProvider
  {
    private readonly HybridDictionary cachedRows = new HybridDictionary();
    private Dictionary<IBindingList, GridViewInfo> rowsBindingLists = new Dictionary<IBindingList, GridViewInfo>();
    private GridViewRelation relation;

    public GridViewObjectRelationalDataProvider(GridViewTemplate template)
      : base(template)
    {
      this.relation = template.MasterTemplate.Relations.Find(template.Parent, template);
    }

    public override GridViewRelation Relation
    {
      get
      {
        return this.relation;
      }
    }

    public override bool IsVirtual
    {
      get
      {
        return true;
      }
    }

    public GridViewRowInfo AddNewRow(GridViewNewRowInfo newRow)
    {
      GridViewRowInfo parent = newRow.Parent as GridViewRowInfo;
      PropertyDescriptor descriptor = ListBindingHelper.GetListItemProperties(parent.DataBoundItem).Find(this.relation.ChildColumnNames[0], true);
      List<GridViewRowInfo> cachedChildRows = this.GetCachedChildRows(parent.DataBoundItem, descriptor);
      if (descriptor == null)
        return (GridViewRowInfo) null;
      IList list = descriptor.GetValue(parent.DataBoundItem) as IList;
      if (list != null)
      {
        this.SuspendNotifications();
        object instance = Activator.CreateInstance(ListBindingHelper.GetListItemType((object) list));
        GridViewRowInfo gridViewRowInfo = this.Template.Rows.NewRow();
        ((IDataItem) gridViewRowInfo).DataBoundItem = instance;
        gridViewRowInfo.SetParent(parent);
        foreach (GridViewColumn column in (Collection<GridViewDataColumn>) newRow.ViewTemplate.Columns)
        {
          if (column.IsVisible && !column.ReadOnly && newRow.Cells[column.FieldName].Value != null)
            gridViewRowInfo[column] = newRow.Cells[column.FieldName].Value;
        }
        list.Add(instance);
        cachedChildRows.Add(gridViewRowInfo);
        this.ResumeNotifications();
        return gridViewRowInfo;
      }
      object obj = descriptor.GetValue(parent.DataBoundItem);
      if (obj == null)
        return (GridViewRowInfo) null;
      System.Type type = obj.GetType();
      if (!type.IsGenericType || type.IsGenericTypeDefinition)
        return (GridViewRowInfo) null;
      MethodInfo method = type.GetMethod("Add");
      if ((object) method == null)
        return (GridViewRowInfo) null;
      this.SuspendNotifications();
      object instance1 = Activator.CreateInstance(ListBindingHelper.GetListItemType((object) list));
      GridViewRowInfo gridViewRowInfo1 = this.Template.Rows.NewRow();
      ((IDataItem) gridViewRowInfo1).DataBoundItem = instance1;
      gridViewRowInfo1.SetParent(parent);
      foreach (GridViewColumn column in (Collection<GridViewDataColumn>) newRow.ViewTemplate.Columns)
      {
        if (column.IsVisible && !column.ReadOnly && newRow.Cells[column.FieldName].Value != null)
          gridViewRowInfo1[column] = newRow.Cells[column.FieldName].Value;
      }
      method.Invoke(obj, new object[1]{ instance1 });
      cachedChildRows.Add(gridViewRowInfo1);
      this.ResumeNotifications();
      return gridViewRowInfo1;
    }

    public bool RemoveRow(GridViewRowInfo row)
    {
      if (row.Parent is GridViewRowInfo)
      {
        GridViewRowInfo parent = row.Parent as GridViewRowInfo;
        PropertyDescriptor descriptor = ListBindingHelper.GetListItemProperties(parent.DataBoundItem).Find(this.relation.ChildColumnNames[0], true);
        if (descriptor != null)
        {
          List<GridViewRowInfo> cachedChildRows = this.GetCachedChildRows(parent.DataBoundItem, descriptor);
          IList list = descriptor.GetValue(parent.DataBoundItem) as IList;
          if (list != null)
          {
            this.SuspendNotifications();
            if (list.Contains(row.DataBoundItem))
              list.Remove(row.DataBoundItem);
            if (cachedChildRows.Contains(row))
              cachedChildRows.Remove(row);
            this.ResumeNotifications();
            return true;
          }
          object obj = descriptor.GetValue(parent.DataBoundItem);
          if (obj == null)
            return true;
          System.Type type = obj.GetType();
          if (!type.IsGenericType || type.IsGenericTypeDefinition)
            return true;
          MethodInfo method1 = type.GetMethod("Remove");
          MethodInfo method2 = type.GetMethod("Contains");
          if ((object) method1 == null || (object) method2 == null)
            return true;
          this.SuspendNotifications();
          if ((bool) method2.Invoke(obj, new object[1]
          {
            row.DataBoundItem
          }))
            method1.Invoke(obj, new object[1]
            {
              row.DataBoundItem
            });
          if (cachedChildRows.Contains(row))
            cachedChildRows.Remove(row);
          this.ResumeNotifications();
          return true;
        }
      }
      return false;
    }

    public override IList<GridViewRowInfo> GetChildRows(
      GridViewRowInfo parentRow,
      GridViewInfo view)
    {
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      PropertyDescriptor descriptor = ListBindingHelper.GetListItemProperties(parentRow.DataBoundItem).Find(this.relation.ChildColumnNames[0], true);
      if (descriptor != null)
      {
        List<GridViewRowInfo> cachedChildRows = this.GetCachedChildRows(parentRow.DataBoundItem, descriptor);
        IEnumerable enumerable = descriptor.GetValue(parentRow.DataBoundItem) as IEnumerable;
        if (enumerable != null)
        {
          PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties((object) enumerable);
          int num = 0;
          foreach (object obj in enumerable)
          {
            GridViewRowInfo childRowInfo = this.GetChildRowInfo(cachedChildRows, num++);
            ((IDataItem) childRowInfo).DataBoundItem = obj;
            for (int index = 0; index < this.Template.Columns.Count; ++index)
            {
              if (this.Template.Columns[index].IsFieldNamePath)
                childRowInfo.Cache[(GridViewColumn) this.Template.Columns[index]] = DataUtils.GetValue(listItemProperties, this.Template.Columns[index].FieldName, obj);
              else if (!string.IsNullOrEmpty(this.Template.Columns[index].FieldName))
              {
                PropertyDescriptor propertyDescriptor = listItemProperties.Find(this.Template.Columns[index].FieldName, true);
                if (propertyDescriptor != null)
                  childRowInfo.Cache[(GridViewColumn) this.Template.Columns[index]] = propertyDescriptor.GetValue(obj);
              }
            }
            gridViewRowInfoList.Add(childRowInfo);
          }
        }
        IBindingList index1 = enumerable as IBindingList;
        if (index1 != null)
        {
          index1.ListChanged -= new ListChangedEventHandler(this.bindableChildren_ListChanged);
          index1.ListChanged += new ListChangedEventHandler(this.bindableChildren_ListChanged);
          this.rowsBindingLists[index1] = view;
        }
      }
      return (IList<GridViewRowInfo>) gridViewRowInfoList;
    }

    public override void Dispose()
    {
      foreach (KeyValuePair<IBindingList, GridViewInfo> rowsBindingList in this.rowsBindingLists)
        rowsBindingList.Key.ListChanged -= new ListChangedEventHandler(this.bindableChildren_ListChanged);
      base.Dispose();
    }

    private void bindableChildren_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (this.IsSuspendedNotifications)
        return;
      IBindingList key = sender as IBindingList;
      if (key != null && this.rowsBindingLists.ContainsKey(key))
        this.rowsBindingLists[key].Refresh();
      this.Refresh();
    }

    private List<GridViewRowInfo> GetCachedChildRows(
      object dataBoundItem,
      PropertyDescriptor descriptor)
    {
      string str = dataBoundItem.GetHashCode().ToString() + descriptor.Name + descriptor.ComponentType.FullName + descriptor.PropertyType.GetType().FullName;
      List<GridViewRowInfo> gridViewRowInfoList = this.cachedRows[(object) str] as List<GridViewRowInfo>;
      if (gridViewRowInfoList == null)
      {
        gridViewRowInfoList = new List<GridViewRowInfo>();
        this.cachedRows.Add((object) str, (object) gridViewRowInfoList);
      }
      return gridViewRowInfoList;
    }

    private GridViewRowInfo GetChildRowInfo(List<GridViewRowInfo> cache, int index)
    {
      GridViewRowInfo gridViewRowInfo = index < cache.Count ? cache[index] : (GridViewRowInfo) null;
      if (gridViewRowInfo == null)
      {
        gridViewRowInfo = this.Template.Rows.NewRow();
        cache.Add(gridViewRowInfo);
      }
      return gridViewRowInfo;
    }

    public override void Refresh()
    {
      this.DispatchDataViewChangedEvent(new DataViewChangedEventArgs(ViewChangedAction.Reset));
    }

    private void DispatchDataViewChangedEvent(DataViewChangedEventArgs args)
    {
      GridViewEventInfo eventInfo = new GridViewEventInfo(KnownEvents.ViewChanged, GridEventType.Both, GridEventDispatchMode.Send);
      GridViewSynchronizationService.DispatchEvent(this.Template, new GridViewEvent((object) this.Template, (object) this.Template, new object[1]
      {
        (object) args
      }, eventInfo), false);
    }

    public override GridViewHierarchyRowInfo GetParent(
      GridViewRowInfo gridViewRowInfo)
    {
      return (GridViewHierarchyRowInfo) null;
    }
  }
}
