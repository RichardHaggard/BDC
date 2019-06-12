// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseFormattingObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  [Browsable(false)]
  public class BaseFormattingObject : ICloneable, INotifyPropertyChanged, INotifyPropertyChangingEx, IEquatable<BaseFormattingObject>
  {
    internal string name = "";
    internal bool enabled = true;
    internal ContentAlignment textAlignment = ContentAlignment.MiddleLeft;
    internal ContentAlignment rowTextAlignment = ContentAlignment.MiddleLeft;
    internal Color rowBackColor = Color.Empty;
    internal Color rowForeColor = Color.Empty;
    internal Color cellBackColor = Color.Empty;
    internal Color cellForeColor = Color.Empty;
    internal bool applyOnSelectedRows = true;
    internal bool applyToRow;
    internal Font cellFont;
    internal Font rowFont;

    public BaseFormattingObject()
      : this("NewCondition", false)
    {
    }

    public BaseFormattingObject(string name, bool applyToRow)
    {
      this.name = name;
      this.applyToRow = applyToRow;
    }

    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        if (this.textAlignment == value)
          return;
        this.textAlignment = value;
        this.OnPropertyChanged(nameof (TextAlignment));
      }
    }

    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Description("Determines what TextAlignment to apply to child cells when ApplyToRow is true.")]
    public ContentAlignment RowTextAlignment
    {
      get
      {
        return this.rowTextAlignment;
      }
      set
      {
        if (this.rowTextAlignment == value)
          return;
        this.rowTextAlignment = value;
        this.OnPropertyChanged(nameof (RowTextAlignment));
      }
    }

    [DefaultValue(true)]
    public bool Enabled
    {
      get
      {
        return this.enabled;
      }
      set
      {
        if (this.enabled == value)
          return;
        this.enabled = value;
        this.OnPropertyChanged(nameof (Enabled));
      }
    }

    [DefaultValue(typeof (Color), "0")]
    public Color CellForeColor
    {
      get
      {
        return this.cellForeColor;
      }
      set
      {
        if (!(this.cellForeColor != value))
          return;
        this.cellForeColor = value;
        this.OnPropertyChanged(nameof (CellForeColor));
      }
    }

    [DefaultValue(typeof (Color), "0")]
    public Color CellBackColor
    {
      get
      {
        return this.cellBackColor;
      }
      set
      {
        if (!(this.cellBackColor != value))
          return;
        this.cellBackColor = value;
        this.OnPropertyChanged(nameof (CellBackColor));
      }
    }

    [DefaultValue(typeof (Color), "0")]
    public Color RowForeColor
    {
      get
      {
        return this.rowForeColor;
      }
      set
      {
        if (!(this.rowForeColor != value))
          return;
        this.rowForeColor = value;
        this.OnPropertyChanged(nameof (RowForeColor));
      }
    }

    [DefaultValue(typeof (Color), "0")]
    public Color RowBackColor
    {
      get
      {
        return this.rowBackColor;
      }
      set
      {
        if (!(this.rowBackColor != value))
          return;
        this.rowBackColor = value;
        this.OnPropertyChanged(nameof (RowBackColor));
      }
    }

    [DefaultValue(null)]
    public Font CellFont
    {
      get
      {
        return this.cellFont;
      }
      set
      {
        if (this.cellFont == value)
          return;
        this.cellFont = value;
        this.OnPropertyChanged(nameof (CellFont));
      }
    }

    [DefaultValue(null)]
    public Font RowFont
    {
      get
      {
        return this.rowFont;
      }
      set
      {
        if (this.rowFont == value)
          return;
        this.rowFont = value;
        this.OnPropertyChanged(nameof (RowFont));
      }
    }

    [DefaultValue("")]
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
        this.OnPropertyChanged(nameof (Name));
      }
    }

    [DefaultValue(false)]
    public bool ApplyToRow
    {
      get
      {
        return this.applyToRow;
      }
      set
      {
        if (this.applyToRow == value)
          return;
        this.applyToRow = value;
        this.OnPropertyChanged(nameof (ApplyToRow));
      }
    }

    [DefaultValue(true)]
    public bool ApplyOnSelectedRows
    {
      get
      {
        return this.applyOnSelectedRows;
      }
      set
      {
        this.applyOnSelectedRows = value;
      }
    }

    public virtual bool Evaluate(GridViewRowInfo row, GridViewColumn column)
    {
      return false;
    }

    public override bool Equals(object obj)
    {
      BaseFormattingObject format = obj as BaseFormattingObject;
      if (format == null)
        return false;
      return this.Equals(format);
    }

    public override int GetHashCode()
    {
      int num = base.GetHashCode() ^ this.cellBackColor.GetHashCode() ^ this.cellForeColor.GetHashCode() ^ this.rowBackColor.GetHashCode() ^ this.rowForeColor.GetHashCode() ^ this.textAlignment.GetHashCode();
      if (this.cellFont != null)
        num ^= this.cellFont.GetHashCode();
      if (this.rowFont != null)
        num ^= this.rowFont.GetHashCode();
      return num;
    }

    public bool IsValueSet(string propName)
    {
      switch (propName)
      {
        case "TextAlignment":
          return this.textAlignment != ContentAlignment.MiddleLeft;
        case "RowTextAlignment":
          return this.rowTextAlignment != ContentAlignment.MiddleLeft;
        case "CellForeColor":
          return this.cellForeColor != Color.Empty;
        case "CellBackColor":
          return this.cellBackColor != Color.Empty;
        case "RowForeColor":
          return this.rowForeColor != Color.Empty;
        case "RowBackColor":
          return this.rowBackColor != Color.Empty;
        case "CellFont":
          return this.cellFont != null;
        case "RowFont":
          return this.rowFont != null;
        default:
          return false;
      }
    }

    public virtual object Clone()
    {
      return (object) ReflectionHelper.Clone<BaseFormattingObject>(this);
    }

    public virtual void Copy(BaseFormattingObject source)
    {
      ReflectionHelper.CopyFields<BaseFormattingObject>(this, source);
      this.OnPropertyChanged("CellBackColor");
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public event PropertyChangingEventHandlerEx PropertyChanging;

    protected virtual bool OnPropertyChanging(string propertyName)
    {
      PropertyChangingEventArgsEx e = new PropertyChangingEventArgsEx(propertyName);
      if (this.PropertyChanging != null)
        this.PropertyChanging((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    public bool Equals(BaseFormattingObject format)
    {
      if (format == null || !(format.cellBackColor == this.cellBackColor) || (!(format.cellForeColor == this.cellForeColor) || !(format.rowBackColor == this.rowBackColor)) || (!(format.rowForeColor == this.rowForeColor) || format.textAlignment != this.textAlignment || format.cellFont != this.cellFont))
        return false;
      return format.rowFont == this.rowFont;
    }
  }
}
