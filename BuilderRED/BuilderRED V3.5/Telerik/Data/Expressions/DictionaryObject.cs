// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.DictionaryObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Data.Expressions
{
  public class DictionaryObject : DynamicObject, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
  {
    private Dictionary<string, object> properties;

    public object this[string name]
    {
      get
      {
        return this.GetValue(name);
      }
      set
      {
        this.SetValue(name, value);
      }
    }

    public DictionaryObject()
      : this(new Dictionary<string, object>())
    {
    }

    public DictionaryObject(Dictionary<string, object> properties)
    {
      this.properties = properties;
    }

    protected override IEnumerator<KeyValuePair<string, object>> GetEnumeratorInternal()
    {
      return (IEnumerator<KeyValuePair<string, object>>) this.properties.GetEnumerator();
    }

    protected override object GetValue(string name)
    {
      if (this.properties.ContainsKey(name))
        return this.properties[name];
      throw new ArgumentOutOfRangeException(string.Format("Property {0} does not exist.", (object) name));
    }

    protected override void SetValue(string name, object value)
    {
      this.properties[name] = value;
    }

    public bool ContainsKey(string key)
    {
      return this.properties.ContainsKey(key);
    }

    public virtual void Add(string key, object value)
    {
      this.properties.Add(key, value);
    }

    public bool Remove(string key)
    {
      return this.properties.Remove(key);
    }

    public bool TryGetValue(string key, out object value)
    {
      return this.properties.TryGetValue(key, out value);
    }

    public ICollection<string> Keys
    {
      get
      {
        return (ICollection<string>) this.properties.Keys;
      }
    }

    public ICollection<object> Values
    {
      get
      {
        return (ICollection<object>) this.properties.Values;
      }
    }

    public void Add(KeyValuePair<string, object> item)
    {
      ((ICollection<KeyValuePair<string, object>>) this.properties).Add(item);
    }

    public virtual void Clear()
    {
      this.properties.Clear();
    }

    public bool Contains(KeyValuePair<string, object> item)
    {
      return ((ICollection<KeyValuePair<string, object>>) this.properties).Contains(item);
    }

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      ((ICollection<KeyValuePair<string, object>>) this.properties).CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, object> item)
    {
      return ((ICollection<KeyValuePair<string, object>>) this.properties).Remove(item);
    }

    public int Count
    {
      get
      {
        return this.properties.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return ((ICollection<KeyValuePair<string, object>>) this.properties).IsReadOnly;
      }
    }
  }
}
