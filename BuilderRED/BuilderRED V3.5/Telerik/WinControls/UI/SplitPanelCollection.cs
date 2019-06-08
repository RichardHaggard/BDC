// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitPanelCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class SplitPanelCollection : IList<SplitPanel>, ICollection<SplitPanel>, IEnumerable<SplitPanel>, IList, ICollection, IEnumerable
  {
    private RadSplitContainer owner;

    public SplitPanelCollection(RadSplitContainer owner)
    {
      this.owner = owner;
    }

    public virtual SplitPanel this[int index]
    {
      get
      {
        return (SplitPanel) this.owner.Controls[index];
      }
      set
      {
        this.owner.Controls.RemoveAt(index);
        this.owner.Controls.Add((Control) value);
        this.owner.Controls.SetChildIndex((Control) value, index);
      }
    }

    public virtual SplitPanel this[string key]
    {
      get
      {
        if (!string.IsNullOrEmpty(key))
        {
          int index = this.IndexOfKey(key);
          if (this.IsValidIndex(index))
            return this[index];
        }
        return (SplitPanel) null;
      }
    }

    private bool IsValidIndex(int index)
    {
      if (index >= 0)
        return index < this.Count;
      return false;
    }

    public virtual int IndexOfKey(string key)
    {
      if (!string.IsNullOrEmpty(key))
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (Telerik.WinControls.WindowsFormsUtils.SafeCompareStrings(this[index].Name, key, true))
            return index;
        }
      }
      return -1;
    }

    public void Add(SplitPanel value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      this.owner.Controls.Add((Control) value);
    }

    public void AddRange(IEnumerable<SplitPanel> splitPanels)
    {
      if (splitPanels == null)
        throw new ArgumentNullException("SplitPanels");
      foreach (SplitPanel splitPanel in splitPanels)
        this.Add(splitPanel);
    }

    public int IndexOf(SplitPanel splitPanel)
    {
      if (splitPanel == null)
        throw new ArgumentNullException("value");
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] == splitPanel)
          return index;
      }
      return -1;
    }

    public bool Contains(SplitPanel spliPanel)
    {
      if (spliPanel == null)
        throw new ArgumentNullException("value");
      return this.IndexOf(spliPanel) != -1;
    }

    public virtual bool ContainsKey(string key)
    {
      return this.IsValidIndex(this.IndexOfKey(key));
    }

    public void Insert(int index, string text)
    {
      SplitPanel splitPanel = new SplitPanel();
      splitPanel.Text = text;
      this.Insert(index, splitPanel);
    }

    public void Insert(int index, SplitPanel splitPanel)
    {
      this.owner.Controls.Add((Control) splitPanel);
      this.owner.Controls.SetChildIndex((Control) splitPanel, index);
    }

    public void Insert(int index, string key, string text)
    {
      SplitPanel splitPanel = new SplitPanel();
      splitPanel.Name = key;
      splitPanel.Text = text;
      this.Insert(index, splitPanel);
    }

    public bool Remove(SplitPanel value)
    {
      if (value == null)
        return false;
      this.owner.Controls.Remove((Control) value);
      return true;
    }

    public virtual void RemoveByKey(string key)
    {
      int index = this.IndexOfKey(key);
      if (!this.IsValidIndex(index))
        return;
      this.RemoveAt(index);
    }

    int IList.Add(object value)
    {
      if (!(value is SplitPanel))
        throw new ArgumentException(nameof (value));
      this.Add((SplitPanel) value);
      return this.IndexOf((SplitPanel) value);
    }

    public void Clear()
    {
      this.owner.Controls.Clear();
    }

    bool IList.Contains(object value)
    {
      if (value is SplitPanel)
        return this.Contains((SplitPanel) value);
      return false;
    }

    int IList.IndexOf(object value)
    {
      if (value is SplitPanel)
        return this.IndexOf((SplitPanel) value);
      return -1;
    }

    void IList.Insert(int index, object value)
    {
      if (!(value is SplitPanel))
        throw new ArgumentException(nameof (value));
      this.Insert(index, (SplitPanel) value);
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    void IList.Remove(object value)
    {
      if (!(value is SplitPanel))
        return;
      this.Remove((SplitPanel) value);
    }

    public void RemoveAt(int index)
    {
      this.owner.Controls.RemoveAt(index);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this.owner.Controls[index];
      }
      set
      {
        if (!(value is SplitPanel))
          throw new ArgumentException(nameof (value));
      }
    }

    void ICollection<SplitPanel>.CopyTo(SplitPanel[] dest, int index)
    {
      if (this.Count <= 0)
        return;
      this.owner.Controls.CopyTo((Array) dest, index);
    }

    void ICollection.CopyTo(Array dest, int index)
    {
      if (this.Count <= 0)
        return;
      this.owner.Controls.CopyTo(dest, index);
    }

    public int Count
    {
      get
      {
        return this.owner.Controls.Count;
      }
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return false;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    IEnumerator<SplitPanel> IEnumerable<SplitPanel>.GetEnumerator()
    {
      SplitPanel[] splitPanelArray = new SplitPanel[this.Count];
      this.owner.Controls.CopyTo((Array) splitPanelArray, 0);
      List<SplitPanel> splitPanelList = new List<SplitPanel>(this.Count);
      splitPanelList.AddRange((IEnumerable<SplitPanel>) splitPanelArray);
      return (IEnumerator<SplitPanel>) splitPanelList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      SplitPanel[] splitPanelArray = new SplitPanel[this.Count];
      this.owner.Controls.CopyTo((Array) splitPanelArray, 0);
      return splitPanelArray.GetEnumerator();
    }
  }
}
