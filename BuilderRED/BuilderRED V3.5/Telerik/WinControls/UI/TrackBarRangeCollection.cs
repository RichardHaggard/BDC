// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarRangeCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [Editor("Telerik.WinControls.UI.Design.RadTrackBarControlCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
  [Serializable]
  public class TrackBarRangeCollection : IList, ICollection, IList<TrackBarRange>, ICollection<TrackBarRange>, IEnumerable<TrackBarRange>, IEnumerable, INotifyPropertyChanged
  {
    private List<TrackBarRange> rangeList = new List<TrackBarRange>();
    private float maximum = 20f;
    private float minimum;
    private TrackBarRangeMode mode;
    private RadTrackBarElement owner;
    private int notificationsSuspended;

    public TrackBarRangeCollection(RadTrackBarElement owner)
    {
      this.owner = owner;
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a maximum value for the trackbar position")]
    [DefaultValue(20f)]
    public float Maximum
    {
      get
      {
        return this.maximum;
      }
      set
      {
        if ((double) this.maximum == (double) value)
          return;
        this.maximum = value;
        this.OnNotifyPropertyChanged(nameof (Maximum));
      }
    }

    [DefaultValue(0.0f)]
    [Browsable(true)]
    [Description("Gets or sets a minimum value for the trackbar position")]
    [Category("Behavior")]
    public float Minimum
    {
      get
      {
        return this.minimum;
      }
      set
      {
        if ((double) this.minimum == (double) value)
          return;
        this.minimum = value;
        this.OnNotifyPropertyChanged(nameof (Minimum));
      }
    }

    [DefaultValue(TrackBarRangeMode.SingleThumb)]
    [Browsable(true)]
    [Description("Indicates the Mode of the TrackBar")]
    public TrackBarRangeMode Mode
    {
      get
      {
        return this.mode;
      }
      set
      {
        if (value == this.mode)
          return;
        if (this.Mode == TrackBarRangeMode.StartFromTheBeginning)
          this.Clear();
        this.mode = value;
        this.OnNotifyPropertyChanged(nameof (Mode));
      }
    }

    public RadTrackBarElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    public event NotifyCollectionChangedEventHandler CollectionChanged;

    public event PropertyChangedEventHandler PropertyChanged;

    public bool CheckThumbMove(float value, bool isStart, TrackBarRange range)
    {
      return this.CheckThumbMove(value) && (this.owner.ElementTree == null || this.owner.ElementTree.Control.Site != null || this.PerformThumbMove(value, isStart, range));
    }

    public bool PerformThumbMove(float value)
    {
      if (!this.CheckThumbMove(value))
        return false;
      switch (this.mode)
      {
        case TrackBarRangeMode.SingleThumb:
          if (this.rangeList.Count > 0)
          {
            this.rangeList[0].End = value;
            break;
          }
          break;
      }
      return true;
    }

    public virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      if (this.notificationsSuspended > 0)
        return;
      NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
      if (collectionChanged == null)
        return;
      collectionChanged((object) this, e);
    }

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      if (this.notificationsSuspended > 0)
        return;
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    public IEnumerator<TrackBarRange> GetEnumerator()
    {
      return (IEnumerator<TrackBarRange>) this.rangeList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    public int IndexOf(TrackBarRange item)
    {
      if (item == null)
        return -1;
      return this.rangeList.IndexOf(item);
    }

    public void Insert(int index, TrackBarRange item)
    {
      if (item == null)
        return;
      item.Owner = this;
      this.rangeList.Insert(index, item);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
    }

    public void RemoveAt(int index)
    {
      if (this.Count <= 1 || index >= this.Count)
        return;
      TrackBarRange range = this.rangeList[index];
      this.rangeList.RemoveAt(index);
      range.Owner = (TrackBarRangeCollection) null;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) range, index));
    }

    public TrackBarRange this[int index]
    {
      get
      {
        return this.rangeList[index];
      }
      set
      {
        if (this.rangeList[index] == value)
          return;
        this.rangeList[index] = value;
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged));
      }
    }

    public TrackBarRange this[string text]
    {
      get
      {
        foreach (TrackBarRange range in this.rangeList)
        {
          if (range.Text == text)
            return range;
        }
        return (TrackBarRange) null;
      }
    }

    public void Add(TrackBarRange item)
    {
      if (item == null)
        return;
      if (this.owner != null && this.owner.ElementTree != null && (this.owner.ElementTree.Control.Site != null && item.Text == "Default"))
      {
        foreach (TrackBarRange range in this.rangeList)
        {
          if (range.Text == "Default")
            return;
        }
      }
      bool flag = false;
      if (this.owner.ElementTree != null)
      {
        RadControl control = this.owner.ElementTree.Control as RadControl;
        if (control != null)
          flag = control.IsInitializing;
      }
      if ((this.Mode == TrackBarRangeMode.SingleThumb || this.Mode == TrackBarRangeMode.Range) && !flag)
      {
        foreach (TrackBarRange range in this.rangeList)
        {
          if ((double) range.Start >= (double) item.Start && (double) range.Start <= (double) item.End || (double) range.End >= (double) item.Start && (double) range.End <= (double) item.End || ((double) range.Start <= (double) item.Start && (double) range.End >= (double) item.Start || (double) range.Start <= (double) item.End && (double) range.End >= (double) item.End))
            return;
        }
      }
      item.Owner = this;
      this.rangeList.Add(item);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
    }

    public void Clear()
    {
      if (this.rangeList.Count == 0)
        return;
      TrackBarRange range = this.rangeList[0];
      for (int index = 0; index < this.rangeList.Count; ++index)
        this.rangeList[index].Owner = (TrackBarRangeCollection) null;
      this.rangeList.Clear();
      range.Owner = this;
      if (range != null)
        this.rangeList.Add(range);
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public bool Contains(TrackBarRange item)
    {
      return this.rangeList.Contains(item);
    }

    public void CopyTo(TrackBarRange[] array, int arrayIndex)
    {
      foreach (TrackBarRange trackBarRange in array)
        trackBarRange.Owner = this;
      this.rangeList.CopyTo(array, arrayIndex);
      if (array.Length <= 0)
        return;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
    }

    public int Count
    {
      get
      {
        return this.rangeList.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public bool Remove(TrackBarRange item)
    {
      if (this.Count <= 1 || !this.rangeList.Remove(item))
        return false;
      item.Owner = (TrackBarRangeCollection) null;
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (object) item, this.IndexOf(item)));
      return true;
    }

    int IList.Add(object value)
    {
      TrackBarRange trackBarRange = value as TrackBarRange;
      if (trackBarRange == null)
        return -1;
      this.Add(trackBarRange);
      return this.IndexOf(trackBarRange);
    }

    bool IList.Contains(object value)
    {
      return this.Contains(value as TrackBarRange);
    }

    int IList.IndexOf(object value)
    {
      return this.IndexOf(value as TrackBarRange);
    }

    void IList.Insert(int index, object value)
    {
      this.Insert(index, value as TrackBarRange);
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    void IList.Remove(object value)
    {
      this.Remove(value as TrackBarRange);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this.rangeList[index];
      }
      set
      {
        this[index] = value as TrackBarRange;
      }
    }

    void ICollection.CopyTo(Array array, int index)
    {
      this.CopyTo(array as TrackBarRange[], index);
    }

    bool ICollection.IsSynchronized
    {
      get
      {
        return true;
      }
    }

    object ICollection.SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    public void SuspendNotifications()
    {
      ++this.notificationsSuspended;
    }

    public void ResumeNotifications()
    {
      if (this.notificationsSuspended <= 0)
        return;
      --this.notificationsSuspended;
    }

    private bool PerformThumbMove(float value, bool isStart, TrackBarRange range)
    {
      switch (this.mode)
      {
        case TrackBarRangeMode.Range:
          bool flag = false;
          if (this.owner.ElementTree != null)
          {
            RadControl control = this.owner.ElementTree.Control as RadControl;
            if (control != null)
              flag = control.IsInitializing;
          }
          if (!flag)
          {
            if (isStart)
            {
              using (List<TrackBarRange>.Enumerator enumerator = this.rangeList.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  TrackBarRange current = enumerator.Current;
                  if (!current.Equals((object) range) && ((double) range.Start == (double) range.End && ((double) value == (double) current.Start || (double) value == (double) current.End) || (double) current.Start <= (double) range.Start && (double) value <= (double) current.End || (double) current.End >= (double) range.End && (double) value >= (double) current.Start))
                    return false;
                }
                break;
              }
            }
            else
            {
              using (List<TrackBarRange>.Enumerator enumerator = this.rangeList.GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  TrackBarRange current = enumerator.Current;
                  if (!current.Equals((object) range) && ((double) range.Start == (double) range.End && ((double) value == (double) current.Start || (double) value == (double) current.End) || (double) current.End >= (double) range.End && (double) value >= (double) current.Start || (double) current.Start <= (double) range.Start && (double) value <= (double) current.End))
                    return false;
                }
                break;
              }
            }
          }
          else
            break;
      }
      return true;
    }

    private bool CheckThumbMove(float value)
    {
      return (double) value >= (double) this.Minimum && (double) value <= (double) this.Maximum;
    }
  }
}
