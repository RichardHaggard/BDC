// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.ICollectionView`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.Data
{
  public interface ICollectionView<T> where T : IDataItem
  {
    bool CanFilter { get; }

    bool CanGroup { get; }

    bool CanSort { get; }

    Predicate<T> Filter { get; set; }

    SortDescriptorCollection SortDescriptors { get; }

    GroupDescriptorCollection GroupDescriptors { get; }

    GroupCollection<T> Groups { get; }

    IComparer<Group<T>> GroupComparer { get; set; }

    IEnumerable<T> SourceCollection { get; }

    void Refresh();

    event NotifyCollectionChangedEventHandler CollectionChanged;

    T CurrentItem { get; }

    int CurrentPosition { get; }

    event EventHandler CurrentChanged;

    event CancelEventHandler CurrentChanging;

    bool MoveCurrentTo(T item);

    bool MoveCurrentToFirst();

    bool MoveCurrentToLast();

    bool MoveCurrentToNext();

    bool MoveCurrentToPosition(int position);

    bool MoveCurrentToPrevious();
  }
}
