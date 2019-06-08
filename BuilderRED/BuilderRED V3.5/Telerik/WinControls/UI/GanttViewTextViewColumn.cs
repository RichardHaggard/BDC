// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewColumn : INotifyPropertyChanged
  {
    private bool visible = true;
    private int width = 100;
    private string name;
    private string headerText;
    private string fieldName;
    private string formatString;
    private Type dataType;
    private object tag;
    private bool current;
    private RadGanttViewElement owner;
    private GanttViewDataItemAccessor accessor;

    public GanttViewTextViewColumn()
      : this(string.Empty, string.Empty, string.Empty)
    {
    }

    public GanttViewTextViewColumn(string name)
      : this(name, name, name)
    {
    }

    public GanttViewTextViewColumn(string name, string headerText)
      : this(name, headerText, name)
    {
    }

    public GanttViewTextViewColumn(string name, string headerText, string fieldName)
    {
      this.name = name;
      this.headerText = headerText;
      this.fieldName = fieldName;
    }

    protected internal virtual void Initialize()
    {
      if (this.IsDataBound)
        this.Accessor = (GanttViewDataItemAccessor) new GanttViewDataItemBoundAccessor(this);
      else
        this.Accessor = new GanttViewDataItemAccessor(this);
    }

    [DefaultValue(null)]
    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        if (!(this.name != value))
          return;
        this.name = value;
        this.OnNotifyPropertyChanged(nameof (Name));
      }
    }

    public Type DataType
    {
      get
      {
        return this.dataType;
      }
      set
      {
        this.dataType = value;
      }
    }

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

    [DefaultValue(null)]
    public string HeaderText
    {
      get
      {
        return this.headerText;
      }
      set
      {
        if (!(this.headerText != value))
          return;
        this.headerText = value;
        this.OnNotifyPropertyChanged(nameof (HeaderText));
      }
    }

    [DefaultValue(null)]
    public string FieldName
    {
      get
      {
        return this.fieldName;
      }
      set
      {
        if (!(this.fieldName != value))
          return;
        this.fieldName = value;
        this.OnNotifyPropertyChanged(nameof (FieldName));
      }
    }

    [DefaultValue(null)]
    public string FormatString
    {
      get
      {
        return this.formatString;
      }
      set
      {
        if (!(this.formatString != value))
          return;
        this.formatString = value;
        this.OnNotifyPropertyChanged(nameof (FormatString));
      }
    }

    [DefaultValue(true)]
    public bool Visible
    {
      get
      {
        return this.visible;
      }
      set
      {
        if (this.visible == value)
          return;
        this.visible = value;
        this.OnNotifyPropertyChanged(nameof (Visible));
        if (this.Owner == null)
          return;
        this.Owner.TextViewElement.ViewElement.InvalidateMeasure(true);
        this.owner.TextViewElement.LastVisibleColumn = (GanttViewTextViewColumn) null;
        this.owner.TextViewElement.FirstVisibleColumn = (GanttViewTextViewColumn) null;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool Current
    {
      get
      {
        return this.current;
      }
      set
      {
        if (this.current == value)
          return;
        this.current = value;
        if (this.owner != null)
          this.Owner.CurrentColumn = !value ? (GanttViewTextViewColumn) null : this;
        this.OnNotifyPropertyChanged(nameof (Current));
      }
    }

    public RadGanttViewElement Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        if (value == this.owner)
          return;
        this.owner = value;
        this.Initialize();
      }
    }

    [DefaultValue(100)]
    public int Width
    {
      get
      {
        return (int) ((double) this.width * (double) this.Owner.DpiScaleFactor.Width);
      }
      set
      {
        if (this.width == value)
          return;
        this.width = value;
        this.UpdateWidth();
        this.OnNotifyPropertyChanged(nameof (Width));
      }
    }

    public bool IsDataBound
    {
      get
      {
        return this.Owner.IsDataBound;
      }
    }

    internal GanttViewDataItemAccessor Accessor
    {
      get
      {
        return this.accessor;
      }
      set
      {
        this.accessor = value;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void UpdateWidth()
    {
      if (this.Owner == null)
        return;
      this.Owner.TextViewElement.ColumnScroller.UpdateScrollRange();
      this.Owner.TextViewElement.ViewElement.InvalidateMeasure();
    }
  }
}
