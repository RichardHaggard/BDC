// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewPageCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [Editor("Telerik.WinControls.UI.Design.RadPageViewPageCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  public class RadPageViewPageCollection : RadPageViewObject, IList<RadPageViewPage>, ICollection<RadPageViewPage>, IEnumerable<RadPageViewPage>, IList, ICollection, IEnumerable
  {
    private const string PageNameBase = "Page";
    private RadPageViewControlCollection controls;

    public RadPageViewPageCollection(RadPageView owner)
    {
      this.Owner = owner;
      this.controls = owner.Controls as RadPageViewControlCollection;
    }

    public void Add(RadPageViewPage item)
    {
      this.InitNameAndText(item);
      this.controls.Add((Control) item);
    }

    private void InitNameAndText(RadPageViewPage item)
    {
      if (!string.IsNullOrEmpty(item.Text))
        return;
      if (string.IsNullOrEmpty(item.Name))
        item.Text = item.Name = this.GetUniqueName("Page");
      else
        item.Text = item.Name;
    }

    public void Clear()
    {
      this.controls.Clear();
    }

    public bool Contains(RadPageViewPage item)
    {
      return this.controls.Contains((Control) item);
    }

    public bool Contains(string name)
    {
      return this[name] != null;
    }

    public void CopyTo(RadPageViewPage[] array, int arrayIndex)
    {
      this.controls.CopyTo((Array) array, arrayIndex);
    }

    public int Count
    {
      get
      {
        return this.controls.Count;
      }
    }

    bool ICollection<RadPageViewPage>.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool Remove(RadPageViewPage item)
    {
      this.controls.Remove((Control) item);
      return true;
    }

    public int IndexOf(RadPageViewPage item)
    {
      return this.controls.IndexOf((Control) item);
    }

    public void Insert(int index, RadPageViewPage item)
    {
      this.InitNameAndText(item);
      this.controls.Add((Control) item);
      this.ChangeIndex(item, index);
    }

    public void Swap(RadPageViewPage page1, RadPageViewPage page2)
    {
      if (page1.Owner != this.Owner || page2.Owner != this.Owner)
        throw new ArgumentException("Pages do not belong to this PageView.");
      int newIndex1 = this.controls.IndexOf((Control) page1);
      int newIndex2 = this.controls.IndexOf((Control) page2);
      if (newIndex1 > newIndex2)
      {
        this.ChangeIndex(page1, newIndex2);
        this.ChangeIndex(page2, newIndex1);
      }
      else
      {
        this.ChangeIndex(page2, newIndex1);
        this.ChangeIndex(page1, newIndex2);
      }
    }

    public void ChangeIndex(RadPageViewPage page, int newIndex)
    {
      this.Owner.EnablePageIndexChange();
      this.controls.SetChildIndex((Control) page, newIndex);
    }

    public void RemoveAt(int index)
    {
      this.controls.RemoveAt(index);
    }

    public RadPageViewPage this[int index]
    {
      get
      {
        return this.controls[index] as RadPageViewPage;
      }
      set
      {
        throw new InvalidOperationException("Indexer is read-only.");
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.controls.GetEnumerator();
    }

    public IEnumerator<RadPageViewPage> GetEnumerator()
    {
      IEnumerator enumerator = this.controls.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Control ctrl = (Control) enumerator.Current;
          if (ctrl is RadPageViewPage)
            yield return ctrl as RadPageViewPage;
        }
      }
      finally
      {
        IDisposable disposable = enumerator as IDisposable;
        disposable?.Dispose();
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    void ICollection.CopyTo(Array array, int index)
    {
      this.controls.CopyTo(array, index);
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
        return (object) null;
      }
    }

    int IList.Add(object value)
    {
      RadPageViewPage radPageViewPage = value as RadPageViewPage;
      this.Add(radPageViewPage);
      return this.controls.IndexOf((Control) radPageViewPage);
    }

    bool IList.Contains(object value)
    {
      return this.controls.Contains((Control) (value as RadPageViewPage));
    }

    int IList.IndexOf(object value)
    {
      return this.controls.IndexOf((Control) (value as RadPageViewPage));
    }

    void IList.Insert(int index, object value)
    {
      this.Insert(index, value as RadPageViewPage);
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    bool IList.IsReadOnly
    {
      get
      {
        return false;
      }
    }

    void IList.Remove(object value)
    {
      this.Remove(value as RadPageViewPage);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this[index];
      }
      set
      {
        throw new InvalidOperationException("Indexer is read-only.");
      }
    }

    public RadPageViewPage this[string name]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Name == name)
            return this[index];
        }
        return (RadPageViewPage) null;
      }
    }

    private string GetUniqueName(string baseName)
    {
      if (string.IsNullOrEmpty(baseName))
        return string.Empty;
      if (this[baseName] == null && char.IsNumber(baseName[baseName.Length - 1]))
        return baseName;
      int num = 1;
      while (this.Contains(string.Format("{0}{1}", (object) baseName, (object) num)))
        ++num;
      return baseName + (object) num;
    }
  }
}
