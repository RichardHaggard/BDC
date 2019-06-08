// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.CollectionViewProvider`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.Data
{
  public class CollectionViewProvider<T> where T : IDataItem, INotifyPropertyChanged
  {
    private SortDescriptorCollection sortDescriptors;
    private GroupDescriptorCollection groupDescriptors;
    private ICollectionView<T> collectionView;
    private Predicate<T> filter;

    public CollectionViewProvider()
    {
      this.collectionView = (ICollectionView<T>) null;
      this.sortDescriptors = new SortDescriptorCollection();
      this.groupDescriptors = new GroupDescriptorCollection();
      this.filter = (Predicate<T>) null;
    }

    public CollectionViewProvider(ICollectionView<T> sourceCollectionView)
    {
      this.collectionView = sourceCollectionView;
      this.sortDescriptors = this.collectionView.SortDescriptors;
      this.groupDescriptors = this.collectionView.GroupDescriptors;
      this.filter = this.collectionView.Filter;
    }

    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.sortDescriptors;
      }
      set
      {
        if (this.collectionView != null)
          throw new InvalidOperationException("The property can not be set when source CollectionView is used");
        this.sortDescriptors = value;
        this.OnNotifyPropertyChanged(nameof (SortDescriptors));
      }
    }

    public GroupDescriptorCollection GroupDescriptors
    {
      get
      {
        return this.groupDescriptors;
      }
      set
      {
        if (this.groupDescriptors == value)
          return;
        if (this.collectionView != null)
          throw new InvalidOperationException("The property can not be set when source CollectionView is used");
        this.groupDescriptors = value;
        this.OnNotifyPropertyChanged(nameof (GroupDescriptors));
      }
    }

    public Predicate<T> Filter
    {
      get
      {
        return this.filter;
      }
      set
      {
        if (!(this.filter != value))
          return;
        if (this.collectionView != null)
          throw new InvalidOperationException("The property can not be set when source CollectionView is used");
        this.filter = value;
        this.OnNotifyPropertyChanged(nameof (Filter));
      }
    }

    public ISnapshotCollectionView<T> GetView(IEnumerable<T> source)
    {
      return (ISnapshotCollectionView<T>) null;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }
  }
}
