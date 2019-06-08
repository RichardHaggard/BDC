// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridTableViewState
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class VirtualGridTableViewState : INotifyPropertyChanged
  {
    private int defaultItemSize = 21;
    private int itemSpacing = -1;
    private SizeF dpiScale = new SizeF(1f, 1f);
    private int pageSize = 100;
    private List<int> topPinnedItems = new List<int>();
    private List<int> bottomPinnedItems = new List<int>();
    private Dictionary<int, int> itemSizes = new Dictionary<int, int>();
    private Dictionary<int, int> expandedSizes = new Dictionary<int, int>();
    private List<KeyValuePair<int, int>> partialItemSizes = new List<KeyValuePair<int, int>>();
    private int itemCount;
    private bool enablePaging;
    private int pageIndex;
    private bool supportsPaging;
    private bool isUpdating;

    public VirtualGridTableViewState(
      int itemCount,
      int defaultItemSize,
      int itemSpacing,
      bool supportsPaging)
    {
      this.supportsPaging = supportsPaging;
      this.itemCount = itemCount;
      this.defaultItemSize = defaultItemSize;
      this.itemSpacing = itemSpacing;
    }

    public bool SupportsPaging
    {
      get
      {
        return this.supportsPaging;
      }
    }

    public int TotalPages
    {
      get
      {
        return this.itemCount / this.pageSize + (this.itemCount % this.pageSize > 0 ? 1 : 0);
      }
    }

    [DefaultValue(false)]
    public bool EnablePaging
    {
      get
      {
        if (this.enablePaging)
          return this.supportsPaging;
        return false;
      }
      set
      {
        if (!this.supportsPaging || this.enablePaging == value)
          return;
        this.enablePaging = value;
        this.OnPropertyChanged(nameof (EnablePaging));
      }
    }

    [DefaultValue(100)]
    public int PageSize
    {
      get
      {
        return this.pageSize;
      }
      set
      {
        if (!this.supportsPaging || this.pageSize == value)
          return;
        this.pageSize = value;
        this.OnPropertyChanged(nameof (PageSize));
      }
    }

    [DefaultValue(0)]
    public int PageIndex
    {
      get
      {
        return this.pageIndex;
      }
      set
      {
        if (!this.supportsPaging || this.pageIndex == value || (value < 0 || value >= this.TotalPages) || !this.OnPageIndexChanging(this.pageIndex, value))
          return;
        this.pageIndex = value;
        this.OnPropertyChanged(nameof (PageIndex));
        this.OnPageIndexChanged();
      }
    }

    public ReadOnlyCollection<int> TopPinnedItems
    {
      get
      {
        return this.topPinnedItems.AsReadOnly();
      }
    }

    public ReadOnlyCollection<int> BottomPinnedItems
    {
      get
      {
        return this.bottomPinnedItems.AsReadOnly();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(0)]
    public int ItemCount
    {
      get
      {
        return this.itemCount;
      }
      set
      {
        if (this.itemCount == value)
          return;
        this.itemCount = value;
        this.Reset();
        this.OnPropertyChanged(nameof (ItemCount));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(21)]
    public int DefaultItemSize
    {
      get
      {
        return (int) Math.Round((double) this.defaultItemSize * (double) this.DpiScale.Height);
      }
      set
      {
        if (this.defaultItemSize == value)
          return;
        this.defaultItemSize = value;
        this.OnPropertyChanged(nameof (DefaultItemSize));
      }
    }

    [DefaultValue(-1)]
    public int ItemSpacing
    {
      get
      {
        return this.itemSpacing;
      }
      set
      {
        if (this.itemSpacing == value)
          return;
        this.itemSpacing = value;
        this.OnPropertyChanged(nameof (ItemSpacing));
      }
    }

    public SizeF DpiScale
    {
      get
      {
        return this.dpiScale;
      }
    }

    public void BeginUpdate()
    {
      this.isUpdating = true;
    }

    public void EndUpdate()
    {
      this.isUpdating = false;
      this.UpdateOnItemSizeChanged();
    }

    public void SetItemSize(int itemIndex, int size)
    {
      if (this.itemSizes.ContainsKey(itemIndex) && this.itemSizes[itemIndex] == size)
        return;
      this.itemSizes[itemIndex] = size;
      if (this.isUpdating)
        return;
      this.UpdateOnItemSizeChanged();
    }

    public ReadOnlyCollection<KeyValuePair<int, int>> GetItemSizes()
    {
      return new List<KeyValuePair<int, int>>((IEnumerable<KeyValuePair<int, int>>) this.itemSizes).AsReadOnly();
    }

    public int GetItemSize(int itemIndex)
    {
      return this.GetItemSize(itemIndex, true);
    }

    public int GetItemSize(int itemIndex, bool checkPinned)
    {
      return this.GetItemSize(itemIndex, checkPinned, true);
    }

    public int GetItemSize(int itemIndex, bool checkPinned, bool checkExpanded)
    {
      if (checkPinned && (this.topPinnedItems.Contains(itemIndex) || this.bottomPinnedItems.Contains(itemIndex)))
        return 0;
      int num = !this.itemSizes.ContainsKey(itemIndex) ? this.DefaultItemSize : this.itemSizes[itemIndex];
      if (checkExpanded && this.expandedSizes.ContainsKey(itemIndex))
        num += this.expandedSizes[itemIndex];
      return num;
    }

    public void SetExpandedSize(int itemIndex, int size)
    {
      this.expandedSizes[itemIndex] = size;
      if (!this.itemSizes.ContainsKey(itemIndex))
        this.itemSizes.Add(itemIndex, this.DefaultItemSize);
      if (this.isUpdating)
        return;
      this.UpdateOnItemSizeChanged();
    }

    public void ResetExpandedSize(int itemIndex)
    {
      if (!this.expandedSizes.ContainsKey(itemIndex))
        return;
      this.expandedSizes.Remove(itemIndex);
      if (this.isUpdating)
        return;
      this.UpdateOnItemSizeChanged();
    }

    public void UpdateOnItemSizeChanged()
    {
      if (this.isUpdating)
        return;
      List<KeyValuePair<int, int>> keyValuePairList = new List<KeyValuePair<int, int>>((IEnumerable<KeyValuePair<int, int>>) this.itemSizes);
      keyValuePairList.Sort((Comparison<KeyValuePair<int, int>>) ((A, B) => A.Key.CompareTo(B.Key)));
      this.partialItemSizes = keyValuePairList;
      if (this.partialItemSizes.Count > 0)
        this.partialItemSizes[0] = new KeyValuePair<int, int>(this.partialItemSizes[0].Key, this.partialItemSizes[0].Value + this.ItemSpacing + (this.expandedSizes.ContainsKey(this.partialItemSizes[0].Key) ? this.expandedSizes[this.partialItemSizes[0].Key] : 0) + this.partialItemSizes[0].Key * (this.DefaultItemSize + this.ItemSpacing));
      for (int index = 1; index < this.partialItemSizes.Count; ++index)
        this.partialItemSizes[index] = new KeyValuePair<int, int>(this.partialItemSizes[index].Key, this.partialItemSizes[index].Value + this.partialItemSizes[index - 1].Value + this.ItemSpacing + (this.expandedSizes.ContainsKey(this.partialItemSizes[index].Key) ? this.expandedSizes[this.partialItemSizes[index].Key] : 0) + (this.partialItemSizes[index].Key - this.partialItemSizes[index - 1].Key - 1) * (this.DefaultItemSize + this.ItemSpacing));
      this.OnPropertyChanged("ItemSizes[]");
    }

    public int GetTotalItemSize()
    {
      if (this.partialItemSizes.Count > 0)
        return this.partialItemSizes[this.partialItemSizes.Count - 1].Value + (this.itemCount - this.partialItemSizes[this.partialItemSizes.Count - 1].Key - 1) * (this.DefaultItemSize + this.ItemSpacing);
      int num = 0;
      foreach (KeyValuePair<int, int> expandedSize in this.expandedSizes)
        num += expandedSize.Value;
      return num + this.itemCount * this.DefaultItemSize + (this.itemCount - 1) * this.ItemSpacing;
    }

    public int GetScrollDownTarget(int startIndex, int scrollOffset, out int targetIndex)
    {
      if (scrollOffset < 0)
        return this.GetScrollUpTarget(startIndex, -scrollOffset, out targetIndex);
      int partialArrayIndex;
      int nextResizedItem = this.FindNextResizedItem(startIndex, out partialArrayIndex);
      int num1 = (nextResizedItem - startIndex - 1) * (this.DefaultItemSize + this.ItemSpacing);
      if (nextResizedItem < 0 || num1 >= scrollOffset)
      {
        int num2 = scrollOffset / (this.DefaultItemSize + this.ItemSpacing);
        targetIndex = startIndex + num2;
        return scrollOffset - num2 * (this.DefaultItemSize + this.ItemSpacing);
      }
      scrollOffset -= num1;
      if (scrollOffset < this.GetItemSize(nextResizedItem) - this.ItemSpacing)
      {
        targetIndex = nextResizedItem - 1;
        return scrollOffset;
      }
      this.FindNextResizedItemWithinRange(partialArrayIndex, ref scrollOffset, ref startIndex);
      if (scrollOffset < this.GetItemSize(startIndex + 1) - this.ItemSpacing)
      {
        targetIndex = startIndex;
        return scrollOffset;
      }
      if (scrollOffset > 0)
      {
        int num2 = scrollOffset / (this.DefaultItemSize + this.ItemSpacing);
        targetIndex = startIndex + num2;
        return scrollOffset - num2 * (this.DefaultItemSize + this.ItemSpacing);
      }
      targetIndex = startIndex;
      return 0;
    }

    public int GetScrollUpTarget(int startIndex, int scrollOffset, out int targetIndex)
    {
      if (scrollOffset < 0)
        return this.GetScrollUpTarget(startIndex, -scrollOffset, out targetIndex);
      int num1 = this.GetItemSize(startIndex) + this.ItemSpacing;
      if (scrollOffset <= num1)
      {
        targetIndex = startIndex - 1;
        return num1 - scrollOffset;
      }
      scrollOffset -= num1;
      int partialArrayIndex;
      int prevResizedItem = this.FindPrevResizedItem(startIndex, out partialArrayIndex);
      int num2 = (startIndex - prevResizedItem - 1) * (this.DefaultItemSize + this.ItemSpacing);
      if (prevResizedItem < 0 || num2 >= scrollOffset)
      {
        int num3 = scrollOffset / (this.DefaultItemSize + this.ItemSpacing);
        targetIndex = startIndex - num3 - 1;
        int num4 = scrollOffset - num3 * (this.DefaultItemSize + this.ItemSpacing);
        if (num4 <= 0)
          return 0;
        --targetIndex;
        return this.GetItemSize(targetIndex + 1) + this.ItemSpacing - num4;
      }
      scrollOffset -= num2;
      if (scrollOffset < this.GetItemSize(prevResizedItem) - this.ItemSpacing)
      {
        targetIndex = prevResizedItem - 1;
        return this.GetItemSize(prevResizedItem) + this.ItemSpacing - scrollOffset;
      }
      startIndex = prevResizedItem;
      scrollOffset -= this.GetItemSize(prevResizedItem) + this.ItemSpacing;
      this.FindPrevResizedItemWithinRange(partialArrayIndex, ref scrollOffset, ref startIndex);
      if (scrollOffset < this.GetItemSize(startIndex - 1) - this.ItemSpacing)
      {
        targetIndex = startIndex - 2;
        return this.GetItemSize(startIndex - 1) + this.ItemSpacing - scrollOffset;
      }
      if (scrollOffset > 0)
      {
        int num3 = scrollOffset / (this.DefaultItemSize + this.ItemSpacing);
        targetIndex = startIndex - 1 - num3;
        int num4 = scrollOffset - num3 * (this.DefaultItemSize + this.ItemSpacing);
        if (num4 <= 0)
          return 0;
        --targetIndex;
        return this.GetItemSize(targetIndex + 1) + this.ItemSpacing - num4;
      }
      targetIndex = startIndex - 1;
      return 0;
    }

    private int FindNextResizedItem(int startIndex, out int partialArrayIndex)
    {
      int index1 = 0;
      int num = this.partialItemSizes.Count;
      while (index1 < num)
      {
        int index2 = (index1 + num) / 2;
        if (this.partialItemSizes[index2].Key <= startIndex)
          index1 = index2 + 1;
        else
          num = index2;
      }
      if (index1 == this.partialItemSizes.Count)
      {
        partialArrayIndex = -1;
        return -1;
      }
      partialArrayIndex = index1;
      return this.partialItemSizes[index1].Key;
    }

    private int FindPrevResizedItem(int startIndex, out int partialArrayIndex)
    {
      int num1 = 0;
      int num2 = this.partialItemSizes.Count;
      while (num1 < num2)
      {
        int index = (num1 + num2) / 2;
        if (this.partialItemSizes[index].Key >= startIndex)
          num2 = index;
        else
          num1 = index + 1;
      }
      int index1 = num1 - 1;
      if (index1 == this.partialItemSizes.Count || num2 == 0)
      {
        partialArrayIndex = -1;
        return -1;
      }
      partialArrayIndex = index1;
      return this.partialItemSizes[index1].Key;
    }

    private void FindNextResizedItemWithinRange(
      int startIndexInArray,
      ref int scrollOffset,
      ref int startIndex)
    {
      int index1 = startIndexInArray;
      int num1 = this.partialItemSizes.Count;
      int num2 = this.GetItemSize(this.partialItemSizes[startIndexInArray].Key) + this.ItemSpacing;
      int num3 = this.partialItemSizes[startIndexInArray].Value - num2;
      while (index1 < num1)
      {
        int index2 = (index1 + num1) / 2;
        if (this.partialItemSizes[index2].Value - num3 > scrollOffset)
          num1 = index2;
        else
          index1 = index2 + 1;
      }
      startIndex = this.partialItemSizes[index1 - 1].Key;
      scrollOffset -= this.partialItemSizes[index1 - 1].Value - num3;
      if (index1 >= this.partialItemSizes.Count)
        return;
      int num4 = this.partialItemSizes[index1].Value - (this.GetItemSize(this.partialItemSizes[index1].Key) + this.ItemSpacing) - this.partialItemSizes[index1 - 1].Value;
      if (scrollOffset <= num4)
        return;
      startIndex = this.partialItemSizes[index1].Key - 1;
      scrollOffset -= num4;
    }

    private void FindPrevResizedItemWithinRange(
      int startIndexInArray,
      ref int scrollOffset,
      ref int startIndex)
    {
      int index1 = 0;
      int num1 = startIndexInArray;
      int num2 = this.GetItemSize(this.partialItemSizes[startIndexInArray].Key) + this.ItemSpacing;
      int num3 = this.partialItemSizes[startIndexInArray].Value - num2;
      while (index1 < num1)
      {
        int index2 = (index1 + num1) / 2;
        int num4 = this.partialItemSizes[index2].Value - (this.GetItemSize(this.partialItemSizes[index2].Key) + this.ItemSpacing);
        if (num3 - num4 > scrollOffset)
          index1 = index2 + 1;
        else
          num1 = index2;
      }
      if (startIndex != this.partialItemSizes[index1].Key)
      {
        scrollOffset -= num3 - this.partialItemSizes[index1].Value;
        scrollOffset -= this.GetItemSize(this.partialItemSizes[index1].Key) + this.ItemSpacing;
      }
      startIndex = this.partialItemSizes[index1].Key;
      if (index1 <= 0)
        return;
      int num5 = this.partialItemSizes[index1].Value - (this.GetItemSize(this.partialItemSizes[index1].Key) + this.ItemSpacing) - this.partialItemSizes[index1 - 1].Value;
      if (scrollOffset <= num5)
        return;
      startIndex = this.partialItemSizes[index1 - 1].Key + 1;
      scrollOffset -= num5;
    }

    public bool IsPinned(int itemIndex)
    {
      if (!this.topPinnedItems.Contains(itemIndex))
        return this.bottomPinnedItems.Contains(itemIndex);
      return true;
    }

    public void SetPinPosition(int itemIndex, PinnedRowPosition position)
    {
      if (this.bottomPinnedItems.Contains(itemIndex))
      {
        this.bottomPinnedItems.Remove(itemIndex);
        if (position == PinnedRowPosition.None)
          this.OnPropertyChanged("BottomPinnedItems[]");
      }
      if (this.topPinnedItems.Contains(itemIndex))
      {
        this.topPinnedItems.Remove(itemIndex);
        if (position == PinnedRowPosition.None)
          this.OnPropertyChanged("TopPinnedItems[]");
      }
      if (position == PinnedRowPosition.Top)
      {
        this.topPinnedItems.Add(itemIndex);
        this.topPinnedItems.Sort();
        this.OnPropertyChanged("TopPinnedItems[]");
      }
      if (position != PinnedRowPosition.Bottom)
        return;
      this.bottomPinnedItems.Add(itemIndex);
      this.bottomPinnedItems.Sort();
      this.OnPropertyChanged("BottomPinnedItems[]");
    }

    public PinnedRowPosition GetPinPosition(int itemIndex)
    {
      if (this.topPinnedItems.Contains(itemIndex))
        return PinnedRowPosition.Top;
      return this.bottomPinnedItems.Contains(itemIndex) ? PinnedRowPosition.Bottom : PinnedRowPosition.None;
    }

    public int GetItemOffset(int itemIndex)
    {
      if (itemIndex == 0)
        return 0;
      int partialArrayIndex;
      this.FindPrevResizedItem(itemIndex, out partialArrayIndex);
      if (partialArrayIndex < 0)
        return itemIndex * (this.DefaultItemSize + this.ItemSpacing);
      int num = this.partialItemSizes[partialArrayIndex].Value;
      return itemIndex == this.partialItemSizes[partialArrayIndex].Key ? num - (this.GetItemSize(this.partialItemSizes[partialArrayIndex].Key) - this.ItemSpacing) : num + (itemIndex - this.partialItemSizes[partialArrayIndex].Key - 1) * (this.DefaultItemSize + this.ItemSpacing);
    }

    public int GetItemScrollOffset(int itemIndex)
    {
      int itemIndex1 = 0;
      if (this.EnablePaging)
        itemIndex1 = Math.Min(this.ItemCount - 1, this.PageIndex * this.PageSize);
      return this.GetItemOffset(itemIndex) - this.GetItemOffset(itemIndex1);
    }

    public void Reset()
    {
      this.ItemSizes.Clear();
      this.partialItemSizes.Clear();
      this.ExpandedSizes.Clear();
    }

    public virtual void DpiScaleChanged(SizeF scale)
    {
      this.dpiScale = scale;
      this.Reset();
    }

    public event VirtualGridPageChangingEventHandler PageIndexChanging;

    protected virtual bool OnPageIndexChanging(int oldValue, int newValue)
    {
      if (this.PageIndexChanging == null)
        return false;
      VirtualGridPageChangingEventArgs e = new VirtualGridPageChangingEventArgs(oldValue, newValue, (VirtualGridViewInfo) null);
      this.PageIndexChanging((object) this, e);
      return !e.Cancel;
    }

    public event EventHandler PageIndexChanged;

    protected virtual void OnPageIndexChanged()
    {
      if (this.PageIndexChanged == null)
        return;
      this.PageIndexChanged((object) this, EventArgs.Empty);
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<int> TopPinnedItemsList
    {
      get
      {
        return this.topPinnedItems;
      }
      set
      {
        this.topPinnedItems = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public List<int> BottomPinnedItemsList
    {
      get
      {
        return this.bottomPinnedItems;
      }
      set
      {
        this.bottomPinnedItems = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Dictionary<int, int> ItemSizes
    {
      get
      {
        return this.itemSizes;
      }
      set
      {
        this.itemSizes = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Dictionary<int, int> ExpandedSizes
    {
      get
      {
        return this.expandedSizes;
      }
      set
      {
        this.expandedSizes = value;
      }
    }
  }
}
