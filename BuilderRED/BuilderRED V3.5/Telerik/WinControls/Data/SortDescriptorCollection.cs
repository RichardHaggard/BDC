// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.SortDescriptorCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Telerik.Collections.Generic;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.Data
{
  public class SortDescriptorCollection : NotifyCollection<SortDescriptor>
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
        return this.GetExpression();
      }
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          this.Clear();
        }
        else
        {
          this.ParseSortString(value);
          this.OnPropertyChanged(new PropertyChangedEventArgs(nameof (Expression)));
        }
      }
    }

    public void Add(string propertyName, ListSortDirection direction)
    {
      this.Add(new SortDescriptor(propertyName, direction));
    }

    public bool Contains(string propertyName)
    {
      return this.IndexOf(propertyName) >= 0;
    }

    public int IndexOf(string propertyName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (string.IsNullOrEmpty(this[index].PropertyName))
        {
          if (string.IsNullOrEmpty(propertyName))
            return index;
        }
        else if (this[index].PropertyName.Equals(propertyName, this.UseCaseSensitiveFieldNames ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
          return index;
      }
      return -1;
    }

    public override string ToString()
    {
      if (this.Count < 1)
        return base.ToString();
      return this.GetExpression();
    }

    public bool Remove(string propertyName)
    {
      bool flag = false;
      int index = 0;
      while (index < this.Count)
      {
        if (this[index].PropertyName.Equals(propertyName, this.UseCaseSensitiveFieldNames ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
        {
          this.RemoveAt(index);
          flag = true;
        }
        else
          ++index;
      }
      return flag;
    }

    protected override NotifyCollectionChangedEventArgs CreateEventArguments(
      NotifyCollectionChangedAction action,
      object item,
      int index)
    {
      NotifyCollectionChangedEventArgs eventArguments = base.CreateEventArguments(action, item, index);
      SortDescriptor sortDescriptor = item as SortDescriptor;
      if (sortDescriptor != null)
        eventArguments.PropertyName = sortDescriptor.PropertyName;
      return eventArguments;
    }

    protected override NotifyCollectionChangedEventArgs CreateEventArguments(
      NotifyCollectionChangedAction action,
      object oldItem,
      object item,
      int index)
    {
      NotifyCollectionChangedEventArgs eventArguments = base.CreateEventArguments(action, oldItem, item, index);
      SortDescriptor sortDescriptor = item as SortDescriptor;
      if (sortDescriptor != null)
        eventArguments.PropertyName = sortDescriptor.PropertyName;
      return eventArguments;
    }

    protected override void InsertItem(int index, SortDescriptor item)
    {
      int index1 = this.IndexOf(item.PropertyName);
      if (index1 >= 0 && this[index1].Direction == item.Direction)
        return;
      base.InsertItem(index, item);
      item.Owner = this;
      item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
    }

    private void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "PropertyName") && !(e.PropertyName == "Direction"))
        return;
      int index = this.IndexOf(sender as SortDescriptor);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, sender, index));
    }

    protected override void RemoveItem(int index)
    {
      SortDescriptor sortDescriptor = this[index];
      sortDescriptor.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
      sortDescriptor.Owner = (SortDescriptorCollection) null;
      base.RemoveItem(index);
    }

    protected override void SetItem(int index, SortDescriptor item)
    {
      SortDescriptor sortDescriptor = this[index];
      sortDescriptor.Owner = (SortDescriptorCollection) null;
      sortDescriptor.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
      base.SetItem(index, item);
      item.Owner = this;
      item.PropertyChanged += new PropertyChangedEventHandler(this.item_PropertyChanged);
    }

    protected override void ClearItems()
    {
      foreach (SortDescriptor sortDescriptor in (Collection<SortDescriptor>) this)
      {
        sortDescriptor.PropertyChanged -= new PropertyChangedEventHandler(this.item_PropertyChanged);
        sortDescriptor.Owner = (SortDescriptorCollection) null;
      }
      base.ClearItems();
    }

    private void ParseSortString(string sortString)
    {
      this.BeginUpdate();
      this.Clear();
      this.AddRange(DataStorageHelper.ParseSortString(sortString).ToArray());
      this.EndUpdate();
    }

    private string GetExpression()
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < this.Count; ++index)
      {
        string str1 = "ASC";
        if (this[index].Direction == ListSortDirection.Descending)
          str1 = "DESC";
        string str2 = "";
        if (this.Count > 1 && index < this.Count - 1)
          str2 = ",";
        stringBuilder.Append(string.Format("{0} {1}{2}", (object) this[index].PropertyName, (object) str1, (object) str2));
      }
      return stringBuilder.ToString();
    }
  }
}
