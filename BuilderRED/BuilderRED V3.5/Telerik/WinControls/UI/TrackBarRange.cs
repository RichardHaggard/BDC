// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarRange
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [DesignTimeVisible(false)]
  [ToolboxItem(false)]
  public class TrackBarRange : INotifyPropertyChanged, ICloneable, IComponent, IDisposable
  {
    private string text = string.Empty;
    private string toolTipText = string.Empty;
    private float start;
    private float end;
    private bool selected;
    private object tag;
    private TrackBarRangeCollection owner;
    private ISite site;

    public TrackBarRange()
    {
    }

    public TrackBarRange(float start, float end)
      : this(start, end, "")
    {
      if ((double) start > (double) end)
        throw new ArgumentException();
    }

    public TrackBarRange(float start, float end, string text)
    {
      this.Start = start;
      this.End = end;
      this.Text = text;
    }

    public TrackBarRange(float value)
    {
      this.End = value;
    }

    public TrackBarRange(float value, string text)
    {
      this.End = value;
      this.Text = text;
    }

    [Category("Behavior")]
    [DefaultValue(0.0f)]
    public float Start
    {
      get
      {
        if (this.Owner != null && this.Owner.Mode != TrackBarRangeMode.Range)
          return this.Owner.Minimum;
        return this.start;
      }
      set
      {
        if ((double) this.start == (double) value)
          return;
        if (this.Site != null && (double) value > (double) this.End)
          this.end = value;
        if (this.Owner != null)
        {
          if (!this.Owner.CheckThumbMove(value, true, this))
            return;
          if (this.Owner.Mode == TrackBarRangeMode.Range && (double) value > (double) this.End)
            this.end = value;
        }
        this.start = value;
        if (this.Owner != null)
          this.owner.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) this));
        this.OnNotifyPropertyChanged(nameof (Start));
      }
    }

    [DefaultValue(0.0f)]
    [Category("Behavior")]
    public float End
    {
      get
      {
        return this.end;
      }
      set
      {
        if ((double) this.end == (double) value)
          return;
        if (this.Site != null && (double) value < (double) this.Start)
          this.Start = value;
        if (this.Owner != null)
        {
          if (!this.Owner.CheckThumbMove(value, false, this))
            return;
          if (this.Owner.Mode == TrackBarRangeMode.Range && (double) this.Start > (double) value)
            this.start = value;
        }
        this.end = value;
        if (this.Owner != null)
          this.owner.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) this));
        this.OnNotifyPropertyChanged(nameof (End));
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool IsSelected
    {
      get
      {
        return this.selected;
      }
      internal set
      {
        if (this.selected == value)
          return;
        foreach (TrackBarRange trackBarRange in this.Owner)
          trackBarRange.selected = false;
        this.selected = value;
        this.OnNotifyPropertyChanged(nameof (IsSelected));
        this.owner.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.ItemChanged, (object) this));
      }
    }

    [Browsable(false)]
    public TrackBarRangeCollection Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
        if (this.owner == null)
          return;
        this.CheckForOutOfRange(this.start);
        this.CheckForOutOfRange(this.end);
      }
    }

    [DefaultValue("")]
    [Category("Appearance")]
    public string Text
    {
      get
      {
        return this.text;
      }
      set
      {
        if (this.text == value)
          return;
        this.text = value;
        this.OnNotifyPropertyChanged(nameof (Text));
      }
    }

    [DefaultValue("")]
    [Category("Appearance")]
    public string ToolTipText
    {
      get
      {
        return this.toolTipText;
      }
      set
      {
        if (!(this.toolTipText != value))
          return;
        this.toolTipText = value;
        this.OnNotifyPropertyChanged(nameof (ToolTipText));
      }
    }

    [Browsable(false)]
    [DefaultValue(null)]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        if (this.tag == value)
          return;
        this.tag = value;
        this.OnNotifyPropertyChanged(nameof (Tag));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    private void CheckForOutOfRange(float value)
    {
      if (this.Site != null)
      {
        if ((double) value >= (double) this.Owner.Minimum && (double) value <= (double) this.Owner.Maximum)
          return;
        this.start = this.owner.Minimum;
        this.end = this.owner.Minimum;
      }
      else
      {
        if ((double) value >= (double) this.Owner.Minimum && (double) value <= (double) this.Owner.Maximum)
          return;
        bool flag = false;
        if (this.Owner.Owner.ElementTree != null)
        {
          RadControl control = this.Owner.Owner.ElementTree.Control as RadControl;
          if (control != null)
            flag = control.IsInitializing;
        }
        if (!flag)
          throw new ArgumentOutOfRangeException(string.Format("'{0}' is not valid. Provide value between {1} and {2}", (object) value, (object) this.Owner.Minimum, (object) this.Owner.Maximum));
      }
    }

    public object Clone()
    {
      return (object) new TrackBarRange() { start = this.start, end = this.end, text = this.text, toolTipText = this.toolTipText, selected = this.selected };
    }

    public event EventHandler Disposed;

    [Browsable(false)]
    public ISite Site
    {
      get
      {
        return this.site;
      }
      set
      {
        this.site = value;
      }
    }

    public void Dispose()
    {
      EventHandler disposed = this.Disposed;
      if (disposed == null)
        return;
      disposed((object) this, EventArgs.Empty);
    }
  }
}
