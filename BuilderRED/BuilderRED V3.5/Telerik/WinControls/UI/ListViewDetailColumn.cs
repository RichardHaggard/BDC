// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDetailColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ListViewColumnTypeConverter))]
  public class ListViewDetailColumn : RadObject, INotifyPropertyChanged
  {
    public static RadProperty AutoSizeModeProperty = RadProperty.Register(nameof (AutoSizeMode), typeof (ListViewBestFitColumnMode), typeof (ListViewDetailColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ListViewBestFitColumnMode.DataCells));
    private float width = 200f;
    private float minWidth = 20f;
    private string fieldName = string.Empty;
    private string headerText = string.Empty;
    private bool visible = true;
    private RadListViewElement owner;
    private ListViewAccessor accessor;
    private string name;
    private float maxWidth;
    private bool current;

    public ListViewDetailColumn(string name)
      : this(name, name)
    {
    }

    public ListViewDetailColumn(string name, string headerText)
    {
      this.name = name;
      this.HeaderText = headerText;
      this.accessor = new ListViewAccessor(this);
    }

    protected internal virtual void Initialize()
    {
      if (this.Owner.IsDataBound)
        this.Accessor = (ListViewAccessor) new ListViewBoundAccessor(this);
      else
        this.Accessor = new ListViewAccessor(this);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets the RadListViewElement that owns this column.")]
    public RadListViewElement Owner
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetOwner(RadListViewElement listElement)
    {
      this.Owner = listElement;
    }

    [Browsable(true)]
    [Description("Gets the maximum width that the column can be resized to.")]
    [DefaultValue(0.0f)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Layout")]
    public float MaxWidth
    {
      get
      {
        return this.maxWidth * (this.Owner != null ? this.Owner.DpiScaleFactor.Width : 1f);
      }
      set
      {
        if ((double) value == (double) this.maxWidth)
          return;
        this.maxWidth = value;
        this.OnNotifyPropertyChanged(nameof (MaxWidth));
        this.UpdateWidth(true);
      }
    }

    [Description("Gets the minimum width that the column can be resized to.")]
    [Browsable(true)]
    [DefaultValue(20f)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Layout")]
    public float MinWidth
    {
      get
      {
        return this.minWidth * (this.Owner != null ? this.Owner.DpiScaleFactor.Width : 1f);
      }
      set
      {
        if ((double) value == (double) this.minWidth)
          return;
        this.minWidth = value;
        this.OnNotifyPropertyChanged(nameof (MinWidth));
        this.UpdateWidth(true);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(200f)]
    [Category("Layout")]
    [Description("Gets the current width of the column.")]
    public float Width
    {
      get
      {
        return this.width * (this.Owner != null ? this.Owner.DpiScaleFactor.Width : 1f);
      }
      set
      {
        if ((double) value == (double) this.width)
          return;
        this.width = value / (this.Owner != null ? this.Owner.DpiScaleFactor.Width : 1f);
        this.UpdateWidth(false);
      }
    }

    [Browsable(false)]
    [Description("Gets the name of the field of the bound item corresponding to this column.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FieldName
    {
      get
      {
        return this.fieldName;
      }
      internal set
      {
        this.fieldName = value;
      }
    }

    [Browsable(true)]
    [Description("Gets the name of the column. Must be unique for each column in the same RadListViewElement")]
    public string Name
    {
      get
      {
        return this.name;
      }
    }

    [Description("Gets or sets the text that will be displayed in the header cells.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [Browsable(true)]
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

    [Description("Gets a value indicating whether the column is in bound mode.")]
    [Browsable(false)]
    public bool IsDataBound
    {
      get
      {
        return this.accessor is ListViewBoundAccessor;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether this column is current.")]
    public bool Current
    {
      get
      {
        return this.current;
      }
      set
      {
        if (this.current == value)
          return;
        if (this.owner != null)
          this.owner.CurrentColumn = !value ? (ListViewDetailColumn) null : this;
        this.OnNotifyPropertyChanged(nameof (Current));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether this column will be visible in DetailsView.")]
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
        if (this.Owner != null)
        {
          DetailListViewElement viewElement = this.Owner.ViewElement as DetailListViewElement;
          if (viewElement != null)
          {
            viewElement.InvalidateMeasure();
            viewElement.UpdateLayout();
          }
        }
        this.OnNotifyPropertyChanged(nameof (Visible));
      }
    }

    internal ListViewAccessor Accessor
    {
      get
      {
        return this.accessor;
      }
      set
      {
        if (object.Equals((object) this.accessor, (object) value))
          return;
        if (this.accessor != null)
          this.accessor.Dispose();
        this.accessor = value;
      }
    }

    [Browsable(false)]
    [Category("Layout")]
    [DefaultValue(ListViewBestFitColumnMode.DataCells)]
    [Description("Gets or sets the mode by which the column automatically adjusts its width after BestFit is executed.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ListViewBestFitColumnMode AutoSizeMode
    {
      get
      {
        return (ListViewBestFitColumnMode) this.GetValue(ListViewDetailColumn.AutoSizeModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(ListViewDetailColumn.AutoSizeModeProperty, (object) value);
      }
    }

    public void BestFit()
    {
      if (this.Owner == null)
        return;
      DetailListViewElement viewElement = this.Owner.ViewElement as DetailListViewElement;
      if (viewElement == null)
        return;
      viewElement.BestFitHelper.BestFitQueue.EnqueueBestFitColumn(this);
      viewElement.BestFitHelper.ProcessRequests();
    }

    private void UpdateWidth(bool checkOldWidth)
    {
      float width = this.Width;
      if ((double) this.maxWidth > 0.0)
        this.width = Math.Min(this.width, this.maxWidth);
      this.width = Math.Max(this.width, this.minWidth);
      if ((double) width == (double) this.width && checkOldWidth)
        return;
      if (this.Owner != null)
      {
        DetailListViewElement viewElement = this.Owner.ViewElement as DetailListViewElement;
        if (viewElement != null)
        {
          viewElement.ColumnScroller.UpdateScrollRange();
          viewElement.ViewElement.InvalidateMeasure();
        }
      }
      this.OnNotifyPropertyChanged("Width");
    }

    internal void SetCurrent(bool value)
    {
      if (this.current == value)
        return;
      this.current = value;
      this.OnNotifyPropertyChanged("Current");
    }
  }
}
