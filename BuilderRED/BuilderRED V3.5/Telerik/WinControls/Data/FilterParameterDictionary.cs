// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterParameterDictionary
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.Data
{
  public class FilterParameterDictionary : ObservableCollection<ParameterValuePair>
  {
    private PropertyChangedEventHandler pceh;
    private PropertyChangingEventHandlerEx pceh2;

    public FilterParameterDictionary()
    {
      this.pceh = new PropertyChangedEventHandler(this.ItemPropertyChanged);
      this.pceh2 = new PropertyChangingEventHandlerEx(this.ItemPropertyChanging);
    }

    public void Add(string key, object value)
    {
      this.Add(new ParameterValuePair(key, value));
    }

    public bool Contains(string key)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Name.Equals(key, StringComparison.InvariantCultureIgnoreCase))
          return true;
      }
      return false;
    }

    public void Remove(string key)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (string.Equals(this[index].Name, key, StringComparison.InvariantCultureIgnoreCase))
          this.Remove(this[index]);
      }
    }

    public object this[string key]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Name.Equals(key, StringComparison.InvariantCultureIgnoreCase))
            return this[index].Value;
        }
        return (object) null;
      }
      set
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Name.Equals(key, StringComparison.InvariantCultureIgnoreCase))
          {
            if (this[index].Value == value)
              return;
            this[index].Value = value;
            return;
          }
        }
        this.Add(key, value);
      }
    }

    protected override void InsertItem(int index, ParameterValuePair item)
    {
      this.SetUpItem(item, true);
      base.InsertItem(index, item);
    }

    protected override void RemoveItem(int index)
    {
      this.SetUpItem(this[index], false);
      base.RemoveItem(index);
    }

    protected override void ClearItems()
    {
      for (int index = 0; index < this.Count; ++index)
        this.SetUpItem(this[index], false);
      base.ClearItems();
    }

    protected override void SetItem(int index, ParameterValuePair item)
    {
      this.SetUpItem(this[index], false);
      this.SetUpItem(item, true);
      base.SetItem(index, item);
    }

    private void SetUpItem(ParameterValuePair item, bool attach)
    {
      if (attach)
      {
        item.PropertyChanged += this.pceh;
        item.PropertyChanging += this.pceh2;
      }
      else
      {
        item.PropertyChanged -= this.pceh;
        item.PropertyChanging -= this.pceh2;
      }
    }

    private void ItemPropertyChanging(object sender, PropertyChangingEventArgsEx e)
    {
      ParameterValuePair parameterValuePair = sender as ParameterValuePair;
      if (parameterValuePair == null)
        throw new Exception("Invalid sender of PropertyChanged event.");
      e.Cancel = this.OnCollectionChanging(NotifyCollectionChangedAction.ItemChanged, (object) parameterValuePair, this.IndexOf(parameterValuePair));
    }

    private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      ParameterValuePair parameterValuePair = sender as ParameterValuePair;
      if (parameterValuePair == null)
        throw new Exception("Invalid sender of PropertyChanged event.");
      this.OnCollectionChanged(NotifyCollectionChangedAction.ItemChanged, (object) parameterValuePair, this.IndexOf(parameterValuePair));
    }
  }
}
