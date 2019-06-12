// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTokenizedTextItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class RadTokenizedTextItem : IComparable<RadTokenizedTextItem>, INotifyPropertyChanged
  {
    private string text;
    private object value;

    public RadTokenizedTextItem(string text, object value)
    {
      this.text = text;
      this.value = value;
    }

    public string Text
    {
      get
      {
        return this.text;
      }
      protected internal set
      {
        if (!(this.text != value))
          return;
        this.text = value;
        this.OnPropertyChanged(nameof (Text));
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
      protected internal set
      {
        if (this.value == value)
          return;
        this.value = value;
        this.OnPropertyChanged(nameof (Value));
      }
    }

    public virtual int CompareTo(RadTokenizedTextItem other)
    {
      return string.Compare(this.text, other.text);
    }

    public override string ToString()
    {
      return string.Format("Text = {0} Value = {1}", (object) this.text, this.value);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, e);
    }
  }
}
