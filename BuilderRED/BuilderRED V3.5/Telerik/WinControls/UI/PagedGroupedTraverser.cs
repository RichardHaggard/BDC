// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PagedGroupedTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class PagedGroupedTraverser : IEnumerator
  {
    private bool isOnGroupRow = true;
    private Stack<int> currentGroupIndex;
    private Stack<DataGroup> parentGroup;
    private Stack<GroupRowTraverser> currentTraverser;
    private IList<Group<GridViewRowInfo>> groups;

    public PagedGroupedTraverser(IList<Group<GridViewRowInfo>> groups)
    {
      this.groups = (IList<Group<GridViewRowInfo>>) new List<Group<GridViewRowInfo>>((IEnumerable<Group<GridViewRowInfo>>) groups);
    }

    public object Current
    {
      get
      {
        if (this.isOnGroupRow)
        {
          if (this.groups == null || this.groups.Count == 0)
            return (object) null;
          DataGroup dataGroup = this.parentGroup.Peek();
          if (dataGroup == null)
            return (object) ((DataGroup) this.groups[this.currentGroupIndex.Peek()]).GroupRow;
          return (object) dataGroup.GroupRow;
        }
        if (this.currentTraverser.Peek() != null)
          return (object) this.currentTraverser.Peek().Current;
        return (object) null;
      }
    }

    public bool MoveNext()
    {
      if (this.groups == null || this.groups.Count == 0)
        return false;
      if (this.currentTraverser == null)
      {
        this.currentTraverser = new Stack<GroupRowTraverser>();
        this.currentTraverser.Push(new GroupRowTraverser(((DataGroup) this.groups[0]).GroupRow));
        this.parentGroup = new Stack<DataGroup>();
        this.parentGroup.Push((DataGroup) null);
        this.currentGroupIndex = new Stack<int>();
        this.currentGroupIndex.Push(0);
        this.isOnGroupRow = true;
        return true;
      }
      if (this.parentGroup.Peek() == null && this.groups[this.currentGroupIndex.Peek()].Groups.Count > 0)
      {
        DataGroup group = (DataGroup) this.groups[this.currentGroupIndex.Peek()];
        this.parentGroup.Push(group);
        this.currentGroupIndex.Push(0);
        this.currentTraverser.Push(new GroupRowTraverser(group.GroupRow));
        this.isOnGroupRow = true;
        return true;
      }
      if (this.parentGroup.Peek() != null && this.parentGroup.Peek().Groups.Count > 0)
      {
        DataGroup group = this.parentGroup.Peek().Groups[this.currentGroupIndex.Peek()];
        this.parentGroup.Push(group);
        this.currentGroupIndex.Push(0);
        this.currentTraverser.Push(new GroupRowTraverser(group.GroupRow));
        this.isOnGroupRow = true;
        return true;
      }
      if (this.parentGroup.Peek() == null && this.currentGroupIndex.Peek() < this.groups.Count && this.currentGroupIndex.Peek() > 0)
      {
        DataGroup group = (DataGroup) this.groups[this.currentGroupIndex.Peek()];
        this.parentGroup.Push(group);
        this.currentGroupIndex.Push(0);
        this.currentTraverser.Push(new GroupRowTraverser(group.GroupRow));
        this.isOnGroupRow = true;
        return true;
      }
      if (this.currentTraverser.Peek().MoveNext())
      {
        this.isOnGroupRow = false;
        return true;
      }
      if (this.parentGroup.Peek() != null)
      {
        this.parentGroup.Pop();
        this.currentTraverser.Pop();
        this.currentGroupIndex.Pop();
      }
      this.currentGroupIndex.Push(this.currentGroupIndex.Pop() + 1);
      for (int index = this.parentGroup.Peek() == null ? this.groups.Count : this.parentGroup.Peek().Groups.Count; this.currentGroupIndex.Count > 0 && this.currentGroupIndex.Peek() >= index; index = this.parentGroup.Peek() == null ? this.groups.Count : this.parentGroup.Peek().Groups.Count)
      {
        this.parentGroup.Pop();
        this.currentTraverser.Pop();
        this.currentGroupIndex.Pop();
        if (this.currentGroupIndex.Count != 0)
          this.currentGroupIndex.Push(this.currentGroupIndex.Pop() + 1);
        else
          break;
      }
      if (this.parentGroup.Count > 0)
      {
        if (this.parentGroup.Peek() == null && (this.groups[this.currentGroupIndex.Peek()].Groups.Count > 0 || this.currentGroupIndex.Peek() < this.groups.Count))
        {
          DataGroup group = (DataGroup) this.groups[this.currentGroupIndex.Peek()];
          this.parentGroup.Push(group);
          this.currentGroupIndex.Push(0);
          this.currentTraverser.Push(new GroupRowTraverser(group.GroupRow));
          this.isOnGroupRow = true;
          return true;
        }
        if (this.parentGroup.Peek() != null && this.parentGroup.Peek().Groups.Count > 0)
        {
          DataGroup group = this.parentGroup.Peek().Groups[this.currentGroupIndex.Peek()];
          this.parentGroup.Push(group);
          this.currentGroupIndex.Push(0);
          this.currentTraverser.Push(new GroupRowTraverser(group.GroupRow));
          this.isOnGroupRow = true;
          return true;
        }
      }
      return false;
    }

    public void Reset()
    {
      this.currentGroupIndex = (Stack<int>) null;
      this.currentTraverser = (Stack<GroupRowTraverser>) null;
      this.parentGroup = (Stack<DataGroup>) null;
      this.isOnGroupRow = true;
    }
  }
}
