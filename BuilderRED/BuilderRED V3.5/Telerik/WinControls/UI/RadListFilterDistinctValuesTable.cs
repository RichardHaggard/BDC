// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListFilterDistinctValuesTable
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Globalization;

namespace Telerik.WinControls.UI
{
  public class RadListFilterDistinctValuesTable : IDictionary, ICollection, IEnumerable
  {
    private string formatString;
    private IDataConversionInfoProvider conversionInfoProvider;
    private Hashtable innerTable;

    public string FormatString
    {
      set
      {
        this.formatString = value;
      }
    }

    public IDataConversionInfoProvider DataConversionInfoProvider
    {
      get
      {
        return this.conversionInfoProvider;
      }
      set
      {
        this.conversionInfoProvider = value;
      }
    }

    public RadListFilterDistinctValuesTable()
    {
      this.innerTable = new Hashtable();
    }

    public void Add(object value)
    {
      string str = this.conversionInfoProvider == null ? (!string.IsNullOrEmpty(this.formatString) ? string.Format(this.formatString, value) : Convert.ToString(value, (IFormatProvider) CultureInfo.CurrentCulture)) : RadDataConverter.Instance.Format(value, typeof (string), this.conversionInfoProvider) as string;
      GridViewDataColumn conversionInfoProvider = this.DataConversionInfoProvider as GridViewDataColumn;
      if (TelerikHelper.StringIsNullOrWhiteSpace(str) || conversionInfoProvider != null && str.Equals(conversionInfoProvider.NullValue))
        str = string.Empty;
      this.Add(str, value);
    }

    public void Add(string key, ArrayList valueList)
    {
      this.innerTable.Add((object) key, (object) valueList);
    }

    public void Add(string key, object filterValue)
    {
      if (this.ContainsStringKey(key))
        this[key].Add(filterValue);
      else
        this.Add(key, new ArrayList() { filterValue });
    }

    public bool ContainsStringKey(string key)
    {
      return this.innerTable.ContainsKey((object) key);
    }

    public bool ContainsFilterValue(object value)
    {
      string str = Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture);
      if (this.innerTable.ContainsKey((object) str) && ((ArrayList) this.innerTable[(object) str]).Contains(value))
        return true;
      foreach (ArrayList arrayList in (IEnumerable) this.innerTable.Values)
      {
        if (arrayList.Contains((object) str) || arrayList.Contains(value))
          return true;
        if (arrayList.Count > 0 && value != null && (arrayList[0] != null && (object) arrayList[0].GetType() != (object) value.GetType()))
        {
          for (int index = 0; index < arrayList.Count; ++index)
          {
            if (string.Concat(arrayList[index]) == string.Concat(value))
              return true;
          }
        }
      }
      return false;
    }

    public ArrayList this[string key]
    {
      get
      {
        return (ArrayList) this.innerTable[(object) key];
      }
      set
      {
        this.innerTable[(object) key] = (object) value;
      }
    }

    public void Add(object key, object value)
    {
      this.Add((string) key, (ArrayList) value);
    }

    public void Clear()
    {
      this.innerTable.Clear();
    }

    public bool Contains(object key)
    {
      return this.ContainsStringKey((string) key);
    }

    public IDictionaryEnumerator GetEnumerator()
    {
      return this.innerTable.GetEnumerator();
    }

    public bool IsFixedSize
    {
      get
      {
        return this.innerTable.IsFixedSize;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.innerTable.IsReadOnly;
      }
    }

    public ICollection Keys
    {
      get
      {
        return this.innerTable.Keys;
      }
    }

    public void Remove(object key)
    {
      this.innerTable.Remove((object) (string) key);
    }

    public ICollection Values
    {
      get
      {
        return this.innerTable.Values;
      }
    }

    public object this[object key]
    {
      get
      {
        return (object) this[(string) key];
      }
      set
      {
        this[(string) key] = (ArrayList) value;
      }
    }

    public void CopyTo(Array array, int index)
    {
      this.innerTable.CopyTo(array, index);
    }

    public int Count
    {
      get
      {
        return this.innerTable.Count;
      }
    }

    public bool IsSynchronized
    {
      get
      {
        return this.innerTable.IsSynchronized;
      }
    }

    public object SyncRoot
    {
      get
      {
        return this.innerTable.SyncRoot;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
