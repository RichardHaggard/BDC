// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.SortDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.Data
{
  public class SortDescriptor : INotifyPropertyChanged, INotifyPropertyChangingEx, ICloneable
  {
    private string propertyName = string.Empty;
    private ListSortDirection direction;
    private SortDescriptorCollection owner;
    private int propertyIndex;

    public SortDescriptor()
    {
    }

    public SortDescriptor(string propertyName, ListSortDirection direction)
    {
      this.propertyName = propertyName;
      this.direction = direction;
    }

    public SortDescriptor(
      string propertyName,
      ListSortDirection direction,
      SortDescriptorCollection owner)
      : this(propertyName, direction)
    {
      this.owner = owner;
    }

    [DefaultValue("")]
    public string PropertyName
    {
      get
      {
        return this.propertyName;
      }
      set
      {
        if (!this.OnPropertyChanging(nameof (PropertyName), (object) this.propertyName, (object) value))
          return;
        this.propertyName = value;
        this.OnPropertyChanged(nameof (PropertyName));
      }
    }

    [DefaultValue(ListSortDirection.Ascending)]
    public ListSortDirection Direction
    {
      get
      {
        return this.direction;
      }
      set
      {
        if (!this.OnPropertyChanging(nameof (Direction), (object) this.direction, (object) value))
          return;
        this.direction = value;
        this.OnPropertyChanged(nameof (Direction));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public SortDescriptorCollection Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
      }
    }

    public int PropertyIndex
    {
      get
      {
        return this.propertyIndex;
      }
      internal set
      {
        this.propertyIndex = value;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    public event PropertyChangingEventHandlerEx PropertyChanging;

    protected virtual bool OnPropertyChanging(
      string propertyName,
      object oldValue,
      object newValue)
    {
      PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(propertyName, oldValue, newValue);
      this.OnPropertyChanging(e);
      return !e.Cancel;
    }

    protected virtual void OnPropertyChanging(PropertyChangingEventArgsEx e)
    {
      PropertyChangingEventHandlerEx propertyChanging = this.PropertyChanging;
      if (propertyChanging == null)
        return;
      propertyChanging((object) this, e);
    }

    public virtual object Clone()
    {
      return (object) new SortDescriptor(this.propertyName, this.direction);
    }
  }
}
