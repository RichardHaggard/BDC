// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class ListViewTraverser : ITraverser<ListViewDataItem>, IEnumerator<ListViewDataItem>, IDisposable, IEnumerator, IEnumerable
  {
    private int currentGroupIndex = -1;
    private int currentItemIndex = -1;
    private RadListViewElement owner;
    private ListViewDataItem position;

    public ListViewTraverser(RadListViewElement owner)
    {
      this.owner = owner;
      this.Position = (object) null;
    }

    public event ListViewTraversingEventHandler Traversing;

    public bool IsGroupingEnabled
    {
      get
      {
        if (!this.owner.ShowGroups || this.owner.Groups.Count <= 0)
          return false;
        if (!this.owner.EnableGrouping || this.owner.IsDesignMode)
          return this.owner.EnableCustomGrouping;
        return true;
      }
    }

    public object Position
    {
      get
      {
        return (object) this.position;
      }
      set
      {
        this.position = value as ListViewDataItem;
        if (this.position == null)
        {
          if (this.IsGroupingEnabled)
          {
            this.currentItemIndex = -2;
            this.currentGroupIndex = -1;
          }
          else
          {
            this.currentItemIndex = -1;
            this.currentGroupIndex = -1;
          }
        }
        else
          this.SetPositionCore(value);
      }
    }

    public ListViewDataItem Current
    {
      get
      {
        return this.position;
      }
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.Current;
      }
    }

    public bool MovePrevious()
    {
      while (this.MovePreviousCore())
      {
        if (this.OnTraversing())
          return true;
      }
      return false;
    }

    public bool MoveToEnd()
    {
      if (!this.MoveNext())
        return false;
      do
        ;
      while (this.MoveNext());
      return true;
    }

    public void Dispose()
    {
    }

    public bool MoveNext()
    {
      while (this.MoveNextCore())
      {
        if (this.OnTraversing())
          return true;
      }
      return false;
    }

    public void Reset()
    {
      this.position = (ListViewDataItem) null;
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) new ListViewTraverser(this.owner) { Position = (object) this.position, currentItemIndex = this.currentItemIndex, currentGroupIndex = this.currentGroupIndex };
    }

    public void MoveTo(int index)
    {
      this.Reset();
      while (index != 0 && this.MoveNext())
        ++index;
    }

    public int MoveTo(ListViewDataItem item)
    {
      int num = -1;
      this.Reset();
      while (this.MoveNext())
      {
        ++num;
        if (this.Current == item)
          return num;
      }
      return -1;
    }

    protected virtual void OnTraversing(ListViewTraversingEventArgs e)
    {
      ListViewTraversingEventHandler traversing = this.Traversing;
      if (traversing == null)
        return;
      traversing((object) this, e);
    }

    protected virtual bool OnTraversing()
    {
      if (this.IsGroupingEnabled && this.position != null && (this.position.Group != null && !this.position.Group.Expanded) || this.position != null && !this.position.Visible)
        return false;
      ListViewTraversingEventArgs e = new ListViewTraversingEventArgs(this.position);
      this.OnTraversing(e);
      return e.Process;
    }

    protected virtual bool MovePreviousCore()
    {
      if (this.position == null)
        return false;
      if (this.IsGroupingEnabled)
      {
        if (this.currentGroupIndex == -1)
        {
          this.Position = (object) null;
          return false;
        }
        --this.currentItemIndex;
        if (this.currentItemIndex == -1)
        {
          this.position = (ListViewDataItem) this.owner.Groups[this.currentGroupIndex];
          return true;
        }
        if (this.currentItemIndex == -2)
        {
          --this.currentGroupIndex;
          if (this.currentGroupIndex == -1)
          {
            this.position = (ListViewDataItem) null;
            return true;
          }
          if (this.owner.Groups.Count <= this.currentGroupIndex)
            return false;
          this.currentItemIndex = this.owner.Groups[this.currentGroupIndex].Items.Count - 1;
          this.position = this.currentItemIndex < 0 ? (ListViewDataItem) this.owner.Groups[this.currentGroupIndex] : this.owner.Groups[this.currentGroupIndex].Items[this.currentItemIndex];
          return true;
        }
        if (this.owner.Groups[this.currentGroupIndex].Items.Count <= this.currentItemIndex)
          return false;
        this.position = this.owner.Groups[this.currentGroupIndex].Items[this.currentItemIndex];
        return true;
      }
      if (this.owner.Items.Count > this.currentItemIndex && this.currentItemIndex > 0)
      {
        --this.currentItemIndex;
        this.position = this.owner.Items[this.currentItemIndex];
        return true;
      }
      this.position = (ListViewDataItem) null;
      return false;
    }

    protected virtual bool MoveNextCore()
    {
      if (this.position == null)
      {
        if (this.IsGroupingEnabled)
        {
          this.position = (ListViewDataItem) this.owner.Groups[0];
          this.currentGroupIndex = 0;
          this.currentItemIndex = -1;
          return true;
        }
        if (this.owner.Items.Count <= 0)
          return false;
        this.position = this.owner.Items[0];
        this.currentItemIndex = 0;
        return true;
      }
      if (this.IsGroupingEnabled)
      {
        if (this.currentGroupIndex >= this.owner.Groups.Count)
        {
          this.currentItemIndex = -1;
          this.currentGroupIndex = -1;
          return false;
        }
        if (this.currentGroupIndex == this.owner.Groups.Count - 1 && this.currentItemIndex == this.owner.Groups[this.currentGroupIndex].Items.Count - 1)
          return false;
        ++this.currentItemIndex;
        if (this.owner.Groups[this.currentGroupIndex].Items.Count > this.currentItemIndex)
        {
          this.position = this.owner.Groups[this.currentGroupIndex].Items[this.currentItemIndex];
          return true;
        }
        this.currentItemIndex = -1;
        ++this.currentGroupIndex;
        if (this.owner.Groups.Count <= this.currentGroupIndex)
          return false;
        this.position = (ListViewDataItem) this.owner.Groups[this.currentGroupIndex];
        return true;
      }
      if (this.currentItemIndex >= this.owner.Items.Count - 1)
        return false;
      ++this.currentItemIndex;
      this.position = this.owner.Items[this.currentItemIndex];
      return true;
    }

    protected virtual void SetPositionCore(object value)
    {
      if (this.IsGroupingEnabled)
      {
        ListViewDataItemGroup viewDataItemGroup = value as ListViewDataItemGroup;
        if (viewDataItemGroup != null)
        {
          this.currentGroupIndex = this.owner.Groups.IndexOf(viewDataItemGroup);
          this.currentItemIndex = -1;
        }
        else
        {
          int count = this.owner.Groups.Count;
          for (int index = 0; index < count; ++index)
          {
            if (this.owner.Groups[index].Items.Contains(this.position))
            {
              this.currentGroupIndex = index;
              this.currentItemIndex = this.owner.Groups[index].Items.IndexOf(this.position);
              break;
            }
          }
        }
      }
      else
      {
        this.currentGroupIndex = 0;
        this.currentItemIndex = this.owner.Items.IndexOf(this.position);
      }
    }
  }
}
