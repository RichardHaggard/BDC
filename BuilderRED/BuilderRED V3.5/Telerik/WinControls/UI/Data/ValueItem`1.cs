// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Data.ValueItem`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI.Data
{
  public class ValueItem<T> : INotifyPropertyChanged
  {
    private T item;

    public ValueItem()
    {
    }

    public ValueItem(T item)
    {
      this.item = item;
    }

    public T Value
    {
      get
      {
        return this.item;
      }
      set
      {
        this.item = value;
        this.OnPropertyChanged(nameof (Value));
      }
    }

    public static explicit operator T(ValueItem<T> value)
    {
      return value.Value;
    }

    public static implicit operator ValueItem<T>(T value)
    {
      return new ValueItem<T>(value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
