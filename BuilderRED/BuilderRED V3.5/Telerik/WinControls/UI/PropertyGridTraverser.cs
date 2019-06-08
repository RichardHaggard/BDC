// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class PropertyGridTraverser : ITraverser<PropertyGridItemBase>, IEnumerator<PropertyGridItemBase>, IDisposable, IEnumerator, IEnumerable
  {
    private PropertyGridTableElement propertyGridElement;
    private PropertyGridTraverser enumerator;
    private PropertyGridGroupItem group;
    private PropertyGridItemBase item;
    private int index;
    private int groupIndex;

    public PropertyGridTraverser(PropertyGridTableElement propertyGridElement)
    {
      this.propertyGridElement = propertyGridElement;
    }

    public event PropertyGridTraversingEventHandler Traversing;

    protected virtual void OnTraversing(PropertyGridTraversingEventArgs e)
    {
      PropertyGridTraversingEventHandler traversing = this.Traversing;
      if (traversing == null)
        return;
      traversing((object) this, e);
    }

    protected bool OnTraversing()
    {
      PropertyGridTraversingEventArgs e = new PropertyGridTraversingEventArgs(this.item);
      this.OnTraversing(e);
      return e.Process;
    }

    object ITraverser<PropertyGridItemBase>.Position
    {
      get
      {
        return (object) new PropertyGridTraverserState(this.group, this.item, this.index, this.groupIndex);
      }
      set
      {
        PropertyGridTraverserState gridTraverserState = (PropertyGridTraverserState) value;
        this.group = gridTraverserState.Group;
        this.item = gridTraverserState.Item;
        this.index = gridTraverserState.Index;
        this.groupIndex = gridTraverserState.GroupIndex;
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

    public bool MoveNext()
    {
      while (this.MoveNextCore())
      {
        if (this.OnTraversing())
          return true;
      }
      return false;
    }

    public bool MoveToEnd()
    {
      do
        ;
      while (this.MoveNext());
      return true;
    }

    protected virtual PropertyGridItemBase GetLastChild(
      PropertyGridItemBase currentItem)
    {
      if (!currentItem.Expandable || !currentItem.Expanded)
        return currentItem;
      this.index = currentItem.GridItems.Count - 1;
      return this.GetLastChild((PropertyGridItemBase) currentItem.GridItems[this.index]);
    }

    public bool MoveToFirst()
    {
      if (this.propertyGridElement.CollectionView.Groups.Count > 0)
      {
        this.group = ((PropertyGridGroup) this.propertyGridElement.CollectionView.Groups[0]).GroupItem;
        this.groupIndex = 0;
        this.item = (PropertyGridItemBase) this.group;
        this.index = 0;
        return true;
      }
      if (this.propertyGridElement.CollectionView.Count <= 0)
        return false;
      this.index = 0;
      this.item = (PropertyGridItemBase) this.propertyGridElement.CollectionView[0];
      return true;
    }

    protected virtual bool MovePreviousCore()
    {
      if (this.item == null)
        return false;
      if (this.item is PropertyGridGroupItem)
        return this.MovePreviousFromGroupItem(this.item as PropertyGridGroupItem);
      return this.MovePreviousFromDataItem((PropertyGridItemBase) (this.item as PropertyGridItem));
    }

    protected virtual bool MoveNextCore()
    {
      if (this.item == null)
        return this.MoveToFirst();
      if (this.item is PropertyGridGroupItem)
        return this.MoveNextFromGroupItem(this.item as PropertyGridGroupItem);
      return this.MoveNextFromDataItem((PropertyGridItemBase) (this.item as PropertyGridItem), true, false);
    }

    protected virtual bool MoveNextFromGroupItem(PropertyGridGroupItem currentGroup)
    {
      if ((currentGroup.Expanded || this.TraverseHirarchy) && currentGroup.GridItems.Count > 0)
      {
        this.item = (PropertyGridItemBase) currentGroup.GridItems[0];
        this.index = 0;
        return true;
      }
      if (this.groupIndex + 1 >= this.propertyGridElement.CollectionView.Groups.Count)
        return false;
      this.group = ((PropertyGridGroup) this.propertyGridElement.CollectionView.Groups[++this.groupIndex]).GroupItem;
      this.item = (PropertyGridItemBase) this.group;
      return true;
    }

    protected virtual bool MoveNextFromDataItem(
      PropertyGridItemBase currentItem,
      bool checkIfExpandable,
      bool resetIndex)
    {
      if (currentItem.Expandable && checkIfExpandable && currentItem.GridItems.Count > 0 && (currentItem.Expanded || this.TraverseHirarchy))
      {
        this.item = (PropertyGridItemBase) currentItem.GridItems[0];
        this.index = 0;
        return true;
      }
      if (currentItem.Parent != null)
      {
        if (resetIndex)
          this.index = currentItem.Parent.GridItems.IndexOf(currentItem as PropertyGridItem);
        if (this.index + 1 >= currentItem.Parent.GridItems.Count)
          return this.MoveNextFromDataItem(currentItem.Parent, false, true);
        this.item = (PropertyGridItemBase) currentItem.Parent.GridItems[++this.index];
        return true;
      }
      if (currentItem.Parent == null && this.propertyGridElement.CollectionView.Groups.Count == 0)
      {
        if (resetIndex)
          this.index = this.propertyGridElement.CollectionView.IndexOf(currentItem as PropertyGridItem);
        if (this.index + 1 >= this.propertyGridElement.CollectionView.Count)
          return false;
        this.item = (PropertyGridItemBase) this.propertyGridElement.CollectionView[++this.index];
        return true;
      }
      if (currentItem.Parent == null && this.propertyGridElement.CollectionView.Groups.Count != 0)
      {
        if (resetIndex)
          this.index = this.propertyGridElement.CollectionView.Groups[this.groupIndex].IndexOf(currentItem as PropertyGridItem);
        if (this.index + 1 < this.propertyGridElement.CollectionView.Groups[this.groupIndex].GetItems().Count)
        {
          this.item = (PropertyGridItemBase) this.propertyGridElement.CollectionView.Groups[this.groupIndex].GetItems()[++this.index];
          return true;
        }
        if (this.groupIndex + 1 < this.propertyGridElement.CollectionView.Groups.Count)
        {
          this.group = ((PropertyGridGroup) this.propertyGridElement.CollectionView.Groups[++this.groupIndex]).GroupItem;
          this.item = (PropertyGridItemBase) this.group;
          this.index = -1;
          return true;
        }
      }
      return false;
    }

    protected virtual bool MovePreviousFromGroupItem(PropertyGridGroupItem currentGroup)
    {
      if (this.groupIndex > 0)
      {
        PropertyGridGroupItem groupItem = ((PropertyGridGroup) this.propertyGridElement.CollectionView.Groups[--this.groupIndex]).GroupItem;
        if (groupItem.Expanded || this.TraverseHirarchy)
        {
          this.index = groupItem.GridItems.Count - 1;
          this.item = this.GetLastChild((PropertyGridItemBase) groupItem.GridItems[this.index]);
          return true;
        }
        this.group = groupItem;
        this.item = (PropertyGridItemBase) this.group;
        this.index = -1;
        return true;
      }
      if (this.groupIndex != 0)
        return false;
      this.Reset();
      return true;
    }

    protected virtual bool MovePreviousFromDataItem(PropertyGridItemBase currentItem)
    {
      if (currentItem.Parent != null && currentItem.Parent.GridItems.Count > 0)
      {
        if (this.index > 0)
        {
          PropertyGridItemBase currentItem1 = (PropertyGridItemBase) currentItem.Parent.GridItems[--this.index];
          if (currentItem1.Expandable && (currentItem1.Expanded || this.TraverseHirarchy))
            currentItem1 = this.GetLastChild(currentItem1);
          this.item = currentItem1;
          return true;
        }
        this.item = currentItem.Parent;
        this.index = -1;
        this.index = currentItem.Parent.Parent == null ? (this.propertyGridElement.CollectionView.Groups.Count != 0 ? this.propertyGridElement.CollectionView.Groups[this.groupIndex].IndexOf(currentItem.Parent as PropertyGridItem) : this.propertyGridElement.CollectionView.IndexOf(currentItem.Parent as PropertyGridItem)) : currentItem.Parent.Parent.GridItems.IndexOf(currentItem.Parent as PropertyGridItem);
        return true;
      }
      if (currentItem.Parent == null && this.propertyGridElement.CollectionView.Groups.Count != 0 && (this.index > 0 && this.index < this.propertyGridElement.CollectionView.Groups[this.groupIndex].GetItems().Count) && this.groupIndex < this.propertyGridElement.CollectionView.Groups.Count)
      {
        this.item = (PropertyGridItemBase) this.propertyGridElement.CollectionView.Groups[this.groupIndex].GetItems()[--this.index];
        return true;
      }
      if (currentItem.Parent == null && this.propertyGridElement.CollectionView.Groups.Count != 0 && this.index == 0)
      {
        this.group = ((PropertyGridGroup) this.propertyGridElement.CollectionView.Groups[this.groupIndex]).GroupItem;
        this.item = (PropertyGridItemBase) this.group;
        this.index = -1;
        return true;
      }
      if (currentItem.Parent == null && this.propertyGridElement.CollectionView.Groups.Count == 0)
      {
        if (this.index > 0 && this.index < this.propertyGridElement.CollectionView.Count)
        {
          PropertyGridItemBase lastChild = (PropertyGridItemBase) this.propertyGridElement.CollectionView[--this.index];
          if (lastChild != null)
            lastChild = this.GetLastChild(lastChild);
          this.item = lastChild;
          return true;
        }
        if (this.index == 0)
        {
          this.Reset();
          return true;
        }
      }
      return false;
    }

    public void Reset()
    {
      this.item = (PropertyGridItemBase) null;
      this.group = (PropertyGridGroupItem) null;
      this.index = -1;
      this.groupIndex = -1;
    }

    public PropertyGridItemBase Current
    {
      get
      {
        return this.item;
      }
    }

    public void Dispose()
    {
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.item;
      }
    }

    public IEnumerator GetEnumerator()
    {
      if (this.enumerator == null)
        this.enumerator = new PropertyGridTraverser(this.propertyGridElement);
      this.enumerator.item = this.item;
      this.enumerator.group = this.group;
      this.enumerator.index = this.index;
      this.enumerator.groupIndex = this.groupIndex;
      return (IEnumerator) this.enumerator;
    }

    public void MoveTo(int index)
    {
      this.Reset();
      while (index != 0 && this.MoveNext())
        ++index;
    }

    public int MoveTo(PropertyGridItemBase item)
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

    public int GetIndex(PropertyGridItemBase item)
    {
      object position = ((ITraverser<PropertyGridItemBase>) this).Position;
      int num = this.MoveTo(item);
      ((ITraverser<PropertyGridItemBase>) this).Position = position;
      return num;
    }

    [DefaultValue(false)]
    public bool TraverseHirarchy { get; set; }
  }
}
