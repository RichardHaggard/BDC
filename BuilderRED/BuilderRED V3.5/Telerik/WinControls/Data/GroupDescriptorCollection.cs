// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.GroupDescriptorCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using Telerik.Collections.Generic;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.Data
{
  public class GroupDescriptorCollection : NotifyCollection<GroupDescriptor>
  {
    private bool useCaseSensitiveFieldNames;

    public bool UseCaseSensitiveFieldNames
    {
      get
      {
        return this.useCaseSensitiveFieldNames;
      }
      set
      {
        this.useCaseSensitiveFieldNames = value;
      }
    }

    public virtual string Expression
    {
      get
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index1 = 0; index1 < this.Count; ++index1)
        {
          for (int index2 = 0; index2 < this[index1].GroupNames.Count; ++index2)
          {
            SortDescriptor groupName = this[index1].GroupNames[index2];
            string str1 = "ASC";
            if (groupName.Direction == ListSortDirection.Descending)
              str1 = "DESC";
            string str2 = "";
            if (this[index1].GroupNames.Count > 1 && index2 < this[index1].GroupNames.Count - 1)
              str2 = ",";
            stringBuilder.Append(string.Format("{0} {1}{2}", (object) groupName.PropertyName, (object) str1, (object) str2));
          }
          if (this.Count > 1 && index1 < this.Count - 1)
            stringBuilder.Append(" > ");
        }
        return stringBuilder.ToString();
      }
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          this.Clear();
        }
        else
        {
          this.ParseGroupString(value);
          this.OnPropertyChanged(new PropertyChangedEventArgs(nameof (Expression)));
        }
      }
    }

    public void Add(string propertyName, ListSortDirection direction)
    {
      this.Add(new GroupDescriptor(new SortDescriptor[1]
      {
        new SortDescriptor(propertyName, direction)
      }));
    }

    public bool Remove(string propertyName)
    {
      bool flag = false;
      int index1 = 0;
      while (index1 < this.Count)
      {
        int index2 = 0;
        while (index2 < this[index1].GroupNames.Count)
        {
          if (this[index1].GroupNames[index2].PropertyName.Equals(propertyName, this.UseCaseSensitiveFieldNames ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
          {
            this[index1].GroupNames.RemoveAt(index2);
            flag = true;
          }
          else
            ++index2;
        }
        if (this[index1].GroupNames.Count == 0)
        {
          this.RemoveAt(index1);
          flag = true;
        }
        else
          ++index1;
      }
      return flag;
    }

    public bool Contains(string propertyName, bool caseSensitive)
    {
      CompareOptions compareOpations = this.GetCompareOpations(caseSensitive);
      CultureInfo cultureInfo = CultureInfo.CurrentCulture;
      if (cultureInfo.IsNeutralCulture)
        cultureInfo = CultureInfo.InvariantCulture;
      CompareInfo compareInfo = cultureInfo.CompareInfo;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        for (int index2 = 0; index2 < this[index1].GroupNames.Count; ++index2)
        {
          if (!string.IsNullOrEmpty(this[index1].GroupNames[index2].PropertyName) && compareInfo.Compare(this[index1].GroupNames[index2].PropertyName, propertyName, compareOpations) == 0)
            return true;
        }
      }
      return false;
    }

    protected CompareOptions GetCompareOpations(bool caseSensitive)
    {
      CompareOptions compareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;
      if (caseSensitive)
        compareOptions = CompareOptions.None;
      return compareOptions;
    }

    public bool Contains(string propertyName)
    {
      return this.Contains(propertyName, false);
    }

    public ReadOnlyCollection<SortDescriptor> FindAllAssociatedSortDescriptors(
      string propertyName)
    {
      return this.FindAllAssociatedSortDescriptors(propertyName, false);
    }

    public ReadOnlyCollection<SortDescriptor> FindAllAssociatedSortDescriptors(
      string propertyName,
      bool caseSensitive)
    {
      List<SortDescriptor> sortDescriptorList = new List<SortDescriptor>();
      CompareOptions compareOpations = this.GetCompareOpations(caseSensitive);
      CultureInfo cultureInfo = CultureInfo.CurrentCulture;
      if (cultureInfo.IsNeutralCulture)
        cultureInfo = CultureInfo.InvariantCulture;
      CompareInfo compareInfo = cultureInfo.CompareInfo;
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        for (int index2 = 0; index2 < this[index1].GroupNames.Count; ++index2)
        {
          if (!string.IsNullOrEmpty(this[index1].GroupNames[index2].PropertyName) && compareInfo.Compare(this[index1].GroupNames[index2].PropertyName, propertyName, compareOpations) == 0)
            sortDescriptorList.Add(this[index1].GroupNames[index2]);
        }
      }
      return sortDescriptorList.AsReadOnly();
    }

    protected override void InsertItem(int index, GroupDescriptor item)
    {
      base.InsertItem(index, item);
      item.Owner = this;
      item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
    }

    protected override void SetItem(int index, GroupDescriptor item)
    {
      GroupDescriptor groupDescriptor = this[index];
      groupDescriptor.Owner = (GroupDescriptorCollection) null;
      groupDescriptor.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
      base.SetItem(index, item);
      item.Owner = this;
      item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
    }

    protected override void RemoveItem(int index)
    {
      GroupDescriptor groupDescriptor = this[index];
      base.RemoveItem(index);
      groupDescriptor.Owner = (GroupDescriptorCollection) null;
      groupDescriptor.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
    }

    protected override void ClearItems()
    {
      foreach (GroupDescriptor groupDescriptor in (Collection<GroupDescriptor>) this)
      {
        groupDescriptor.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
        groupDescriptor.Owner = (GroupDescriptorCollection) null;
      }
      base.ClearItems();
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      int index = this.IndexOf(sender as GroupDescriptor);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, sender, index));
    }

    private void ParseGroupString(string groupExpression)
    {
      this.BeginUpdate();
      this.Clear();
      this.AddRange(DataStorageHelper.ParseGroupString(groupExpression).ToArray());
      this.EndUpdate();
    }
  }
}
