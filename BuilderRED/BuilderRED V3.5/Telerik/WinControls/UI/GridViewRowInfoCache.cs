﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRowInfoCache
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Specialized;

namespace Telerik.WinControls.UI
{
  internal class GridViewRowInfoCache
  {
    private HybridDictionary dictionary = new HybridDictionary();

    public object this[GridViewColumn column]
    {
      get
      {
        if (column.IsDataBound && !string.IsNullOrEmpty(column.FieldName))
          return this.dictionary[(object) column.FieldName];
        return this.dictionary[(object) column];
      }
      set
      {
        if (column.IsDataBound && !string.IsNullOrEmpty(column.FieldName))
          this.dictionary[(object) column.FieldName] = value;
        this.dictionary[(object) column] = value;
      }
    }

    public void Clear()
    {
      this.dictionary.Clear();
    }

    public void Remove(GridViewColumn key)
    {
      if (!this.dictionary.Contains((object) key))
        return;
      this.dictionary.Remove((object) key);
    }

    public void ReplaceKey(GridViewDataColumn oldKey, GridViewDataColumn newKey)
    {
      if (!this.dictionary.Contains((object) oldKey))
        return;
      object obj = this.dictionary[(object) oldKey];
      this.dictionary.Remove((object) oldKey);
      this.dictionary.Add((object) newKey, obj);
    }

    public bool ContainsKey(object key)
    {
      return this.dictionary.Contains(key);
    }
  }
}
