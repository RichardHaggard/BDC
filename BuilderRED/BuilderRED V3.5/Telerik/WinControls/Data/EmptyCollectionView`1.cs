// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.EmptyCollectionView`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.Data
{
  internal class EmptyCollectionView<T> : RadCollectionView<T> where T : IDataItem
  {
    private static List<T> Empty = new List<T>();
    private bool canFilter = true;
    private bool canSort = true;
    private bool canGroup = true;
    private IComparer<T> comparer;

    public EmptyCollectionView(IEnumerable<T> collection)
      : base(collection)
    {
    }

    protected override void InitializeSource(IEnumerable<T> collection)
    {
      base.InitializeSource(collection);
      this.comparer = (IComparer<T>) new DataItemComparer<T>(this.SortDescriptors);
    }

    public override IComparer<T> Comparer
    {
      get
      {
        return this.comparer;
      }
      set
      {
        this.comparer = value;
        this.RefreshOverride();
      }
    }

    public override GroupCollection<T> Groups
    {
      get
      {
        return GroupCollection<T>.Empty;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool CanFilter
    {
      get
      {
        return this.canFilter;
      }
      set
      {
        if (this.canFilter == value)
          return;
        this.canFilter = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("Filter"));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool CanGroup
    {
      get
      {
        return this.canGroup;
      }
      set
      {
        if (this.CanGroup == value)
          return;
        this.canGroup = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("GroupDescriptors"));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool CanSort
    {
      get
      {
        return this.canSort;
      }
      set
      {
        if (this.canSort == value)
          return;
        this.canSort = value;
        this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("SortDescriptors"));
      }
    }

    public override int IndexOf(T item)
    {
      return -1;
    }

    public override T Find(int itemIndex, object dataBoundItem)
    {
      return default (T);
    }

    protected override void RefreshOverride()
    {
      base.RefreshOverride();
    }

    protected override IList<T> Items
    {
      get
      {
        return (IList<T>) EmptyCollectionView<T>.Empty;
      }
    }

    protected override void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnNotifyPropertyChanged(e);
      if (!(e.PropertyName == "FilterExpression") && !(e.PropertyName == "Filter") && (!(e.PropertyName == "SortDescriptors") && !(e.PropertyName == "GroupDescriptors")))
        return;
      this.RefreshOverride();
    }
  }
}
