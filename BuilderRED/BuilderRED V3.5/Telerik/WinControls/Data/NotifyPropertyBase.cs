// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.NotifyPropertyBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.Data
{
  [Serializable]
  public class NotifyPropertyBase : INotifyPropertyChangingEx, INotifyPropertyChanged
  {
    private PropertyChangedEventArgs tempStore;
    private int suspendCount;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual bool IsSuspended
    {
      get
      {
        return this.suspendCount != 0;
      }
    }

    public virtual bool SuspendNotifications()
    {
      return 0 == this.suspendCount++;
    }

    public virtual bool ResumeNotifications(bool notifyChanges)
    {
      if (this.suspendCount <= 0)
        return false;
      bool flag = 0 == --this.suspendCount;
      if (flag && this.tempStore != null && notifyChanges)
        this.SignalPropertyChanged();
      this.tempStore = (PropertyChangedEventArgs) null;
      return flag;
    }

    public bool ResumeNotifications()
    {
      return this.ResumeNotifications(true);
    }

    public virtual event PropertyChangedEventHandler PropertyChanged;

    public virtual event PropertyChangingEventHandlerEx PropertyChanging;

    private void SignalPropertyChanged()
    {
      this.ProcessPropertyChanged(this.tempStore);
      if (this.PropertyChanged != null)
        this.PropertyChanged((object) this, this.tempStore);
      this.tempStore = (PropertyChangedEventArgs) null;
    }

    protected void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    internal void CallOnPropertyChanged(PropertyChangedEventArgs e)
    {
      this.OnPropertyChanged(e);
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      this.tempStore = this.tempStore == null ? e : new PropertyChangedEventArgs("this");
      if (this.IsSuspended)
        return;
      this.SignalPropertyChanged();
    }

    protected virtual void ProcessPropertyChanged(PropertyChangedEventArgs e)
    {
    }

    protected PropertyChangingEventArgsEx OnPropertyChanging(
      string propertyName,
      object originalValue,
      object value)
    {
      PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(propertyName, originalValue, value, false);
      this.OnPropertyChanging(e);
      return e;
    }

    protected bool OnPropertyChanging(string propertyName)
    {
      PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(propertyName, (object) null, (object) null, false);
      this.OnPropertyChanging(e);
      return e.Cancel;
    }

    protected virtual void OnPropertyChanging(PropertyChangingEventArgsEx e)
    {
      if (this.IsSuspended)
        return;
      this.ProcessPropertyChanging(e);
      if (this.PropertyChanging == null)
        return;
      this.PropertyChanging((object) this, e);
    }

    protected virtual void ProcessPropertyChanging(PropertyChangingEventArgsEx e)
    {
    }

    protected virtual bool SetProperty<T>(string propertyName, ref T propertyField, T value)
    {
      if (object.Equals((object) propertyField, (object) value))
        return false;
      PropertyChangingEventArgsEx changingEventArgsEx = this.OnPropertyChanging(propertyName, (object) propertyField, (object) value);
      if (changingEventArgsEx.Cancel || object.Equals((object) propertyField, changingEventArgsEx.NewValue))
        return false;
      propertyField = (T) changingEventArgsEx.NewValue;
      this.OnPropertyChanged(propertyName);
      return true;
    }
  }
}
