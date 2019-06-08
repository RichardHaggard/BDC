// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabPanelCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TabPanelCollection : IList, ICollection, IEnumerable
  {
    private TabStripPanel owner;
    private int lastAccessedIndex;

    public TabPanelCollection(TabStripPanel owner)
    {
      this.lastAccessedIndex = -1;
      if (owner == null)
        throw new ArgumentNullException(nameof (owner));
      this.owner = owner;
    }

    public void Add(string text)
    {
      TabPanel tabPanel = new TabPanel();
      tabPanel.Text = text;
      this.Add(tabPanel);
    }

    public void Add(TabPanel value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      this.owner.Controls.Add((Control) value);
    }

    public void Add(string key, string text)
    {
      TabPanel tabPanel = new TabPanel();
      tabPanel.Name = key;
      tabPanel.Text = text;
      this.Add(tabPanel);
    }

    public void Add(string key, string text, int imageIndex)
    {
      TabPanel tabPanel = new TabPanel();
      tabPanel.Name = key;
      tabPanel.Text = text;
      this.Add(tabPanel);
    }

    public void Add(string key, string text, string imageKey)
    {
      TabPanel tabPanel = new TabPanel();
      tabPanel.Name = key;
      tabPanel.Text = text;
      this.Add(tabPanel);
    }

    public void AddRange(TabPanel[] panels)
    {
      if (panels == null)
        throw new ArgumentNullException(nameof (panels));
      foreach (TabPanel panel in panels)
        this.Add(panel);
    }

    public bool Contains(TabPanel panel)
    {
      if (panel == null)
        throw new ArgumentNullException(nameof (panel));
      return this.IndexOf(panel) != -1;
    }

    public virtual bool ContainsKey(string key)
    {
      return this.IsValidIndex(this.IndexOfKey(key));
    }

    public int IndexOf(TabPanel panel)
    {
      if (panel == null)
        throw new ArgumentNullException(nameof (panel));
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] == panel)
          return index;
      }
      return -1;
    }

    public virtual int IndexOfKey(string key)
    {
      if (!string.IsNullOrEmpty(key))
      {
        if (this.IsValidIndex(this.lastAccessedIndex) && Telerik.WinControls.WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
          return this.lastAccessedIndex;
        for (int index = 0; index < this.Count; ++index)
        {
          if (Telerik.WinControls.WindowsFormsUtils.SafeCompareStrings(this[index].Name, key, true))
          {
            this.lastAccessedIndex = index;
            return index;
          }
        }
        this.lastAccessedIndex = -1;
      }
      return -1;
    }

    public void Insert(int index, string text)
    {
      TabPanel tabPanel = new TabPanel();
      tabPanel.Text = text;
      this.Insert(index, tabPanel);
    }

    public void Insert(int index, TabPanel tabPanel)
    {
      this.owner.Controls.Add((Control) tabPanel);
      this.owner.Controls.SetChildIndex((Control) tabPanel, index);
      this.owner.SelectedIndex = index;
    }

    public void Insert(int index, string key, string text)
    {
      TabPanel tabPanel = new TabPanel();
      tabPanel.Name = key;
      tabPanel.Text = text;
      this.Insert(index, tabPanel);
    }

    public void Insert(int index, string key, string text, int imageIndex)
    {
      TabPanel tabPanel = new TabPanel();
      tabPanel.Name = key;
      tabPanel.Text = text;
      this.Insert(index, tabPanel);
    }

    public void Insert(int index, string key, string text, string imageKey)
    {
      TabPanel tabPanel = new TabPanel();
      tabPanel.Name = key;
      tabPanel.Text = text;
      this.Insert(index, tabPanel);
    }

    private bool IsValidIndex(int index)
    {
      if (index >= 0)
        return index < this.Count;
      return false;
    }

    public void Remove(TabPanel value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      this.owner.Controls.Remove((Control) value);
    }

    public void RemoveAt(int index)
    {
      this.owner.Controls.RemoveAt(index);
    }

    public virtual void RemoveByKey(string key)
    {
      int index = this.IndexOfKey(key);
      if (!this.IsValidIndex(index))
        return;
      this.RemoveAt(index);
    }

    public virtual TabPanel this[int index]
    {
      get
      {
        return (TabPanel) this.owner.Controls[index];
      }
    }

    public virtual TabPanel this[string key]
    {
      get
      {
        if (!string.IsNullOrEmpty(key))
        {
          int index = this.IndexOfKey(key);
          if (this.IsValidIndex(index))
            return this[index];
        }
        return (TabPanel) null;
      }
    }

    int IList.Add(object value)
    {
      if (!(value is TabPanel))
        throw new ArgumentException(nameof (value));
      this.Add((TabPanel) value);
      return this.IndexOf((TabPanel) value);
    }

    public void Clear()
    {
      this.owner.Controls.Clear();
    }

    bool IList.Contains(object value)
    {
      if (value is TabPanel)
        return this.Contains((TabPanel) value);
      return false;
    }

    int IList.IndexOf(object value)
    {
      if (value is TabPanel)
        return this.IndexOf((TabPanel) value);
      return -1;
    }

    void IList.Insert(int index, object value)
    {
      if (!(value is TabPanel))
        throw new ArgumentException("tabPanel");
      this.Insert(index, (TabPanel) value);
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
      if (!(value is TabPanel))
        return;
      this.Remove((TabPanel) value);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this[index];
      }
      set
      {
        if (!(value is TabPanel))
          throw new ArgumentException(nameof (value));
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      if (this.Count <= 0)
        return;
      this.owner.Controls.CopyTo(array, index);
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

    public IEnumerator GetEnumerator()
    {
      return this.owner.Controls.GetEnumerator();
    }
  }
}
