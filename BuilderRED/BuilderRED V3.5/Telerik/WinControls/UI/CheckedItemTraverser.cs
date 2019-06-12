// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CheckedItemTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class CheckedItemTraverser : ItemsTraverser<RadListDataItem>
  {
    private bool checkAll;
    private CheckAllDataItem checkAllItem;

    public CheckedItemTraverser(
      IList<RadListDataItem> collection,
      CheckAllDataItem checkAllItem,
      bool checkAll)
      : base(collection)
    {
      this.checkAllItem = checkAllItem;
      this.checkAll = checkAll;
    }

    public bool CheckAll
    {
      get
      {
        return this.checkAll;
      }
      set
      {
        this.checkAll = value;
        this.enumerator = (ItemsTraverser<RadListDataItem>) null;
      }
    }

    public CheckAllDataItem CheckAllItem
    {
      get
      {
        return this.checkAllItem;
      }
      set
      {
        this.checkAllItem = value;
      }
    }

    protected override bool MovePreviousCore()
    {
      if (!this.checkAll)
        return base.MovePreviousCore();
      if (this.InternalPosition >= 0)
      {
        if (this.InternalPosition == 0)
        {
          this.InternalCurrent = (RadListDataItem) this.checkAllItem;
          --this.InternalPosition;
        }
        else
        {
          --this.InternalPosition;
          this.InternalCurrent = this.Collection[(int) this.Position];
        }
        return true;
      }
      this.InternalPosition = -1;
      this.InternalCurrent = (RadListDataItem) null;
      return false;
    }

    public override IEnumerator GetEnumerator()
    {
      if (!this.checkAll)
        return base.GetEnumerator();
      if (this.enumerator == null)
      {
        this.enumerator = (ItemsTraverser<RadListDataItem>) new CheckedItemTraverser(this.Collection, this.checkAllItem, this.checkAll);
        this.enumerator.ItemsNavigating += new ItemsNavigatingEventHandler<RadListDataItem>(((ItemsTraverser<RadListDataItem>) this).Enumerator_ItemsNavigating);
      }
      this.enumerator.Position = this.Position;
      return (IEnumerator) this.enumerator;
    }

    protected override bool MoveNextCore()
    {
      if (!this.checkAll)
        return base.MoveNextCore();
      if (this.InternalPosition >= this.Collection.Count)
        return false;
      if (this.InternalPosition < 0)
      {
        ++this.InternalPosition;
        this.InternalCurrent = (RadListDataItem) this.checkAllItem;
      }
      else
      {
        ++this.InternalPosition;
        this.InternalCurrent = this.Collection[this.InternalPosition - 1];
      }
      return true;
    }
  }
}
