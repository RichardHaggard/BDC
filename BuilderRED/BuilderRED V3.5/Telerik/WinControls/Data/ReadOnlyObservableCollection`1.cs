// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.ReadOnlyObservableCollection`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Telerik.WinControls.Data
{
  public class ReadOnlyObservableCollection<T> : ReadOnlyCollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
  {
    protected event NotifyCollectionChangedEventHandler CollectionChanged;

    protected event PropertyChangedEventHandler PropertyChanged;

    event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
    {
      add
      {
        this.CollectionChanged += value;
      }
      remove
      {
        this.CollectionChanged -= value;
      }
    }

    event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
    {
      add
      {
        this.PropertyChanged += value;
      }
      remove
      {
        this.PropertyChanged -= value;
      }
    }

    public ReadOnlyObservableCollection(ObservableCollection<T> list)
      : base((IList<T>) list)
    {
      ((INotifyCollectionChanged) this.Items).CollectionChanged += new NotifyCollectionChangedEventHandler(this.HandleCollectionChanged);
      ((INotifyPropertyChanged) this.Items).PropertyChanged += new PropertyChangedEventHandler(this.HandlePropertyChanged);
    }

    private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.OnCollectionChanged(e);
    }

    private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnPropertyChanged(e);
    }

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      if (this.CollectionChanged == null)
        return;
      this.CollectionChanged((object) this, args);
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, args);
    }
  }
}
