// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Interfaces;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ListViewDataItemTypeConverter))]
  public class ListViewDataItem : IDataItem, INotifyPropertyChanged, INotifyPropertyChangingEx, IDisposable
  {
    protected BitVector32 bitState = new BitVector32();
    private Size? customSize = new Size?();
    private int imageIndex = -1;
    protected const int IsSelectedState = 1;
    protected const int IsEnabledState = 2;
    protected const int IsVisibleState = 4;
    protected const int IsLastInRowState = 8;
    protected const int IsCurrentState = 16;
    protected const int IsMeasureValidState = 32;
    protected const int MajorBitState = 32;
    private object dataBoundItem;
    private RadListViewElement owner;
    private ListViewDataItemStyle style;
    private Image image;
    private object tag;
    private object key;
    private object unboundValue;
    private ListViewDataItemGroup group;
    private ListViewDetailsCache cache;
    private Size actualSize;
    private string imageKey;
    private Telerik.WinControls.Enumerations.ToggleState checkState;
    private ListViewSubDataItemCollection subItems;

    public ListViewDataItem()
    {
      this.cache = new ListViewDetailsCache();
      this.Visible = true;
      this.Enabled = true;
    }

    public ListViewDataItem(string text)
      : this()
    {
      this.Text = text;
    }

    public ListViewDataItem(object value)
      : this()
    {
      this.Value = value;
    }

    public ListViewDataItem(string text, string[] values)
      : this(text)
    {
      foreach (object obj in values)
        this.SubItems.Add(obj);
    }

    [Description("Key object that is used by the FindByKey method of RadListView. By default this property holds a reference to the ListViewDataItem.")]
    [Category("Data")]
    [Localizable(false)]
    [DefaultValue(null)]
    [TypeConverter(typeof (StringConverter))]
    public object Key
    {
      get
      {
        return this.key;
      }
      set
      {
        if (this.key == value || this.OnNotifyPropertyChanging(nameof (Key)))
          return;
        this.key = value;
        this.OnNotifyPropertyChanged(nameof (Key));
      }
    }

    [Category("Appearance")]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [NotifyParentProperty(true)]
    [RelatedImageList("ListView.ImageList")]
    [DefaultValue(null)]
    public string ImageKey
    {
      get
      {
        return this.imageKey;
      }
      set
      {
        if (!(this.imageKey != value) || this.OnNotifyPropertyChanging(nameof (ImageKey)))
          return;
        this.imageKey = value;
        this.imageIndex = -1;
        this.OnNotifyPropertyChanged(nameof (ImageKey));
      }
    }

    [Description("Gets or sets the left image list index value of the image displayed.")]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [DefaultValue(-1)]
    [RelatedImageList("ListView.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        if (this.imageIndex == value || this.OnNotifyPropertyChanging(nameof (ImageIndex)))
          return;
        this.imageIndex = value;
        this.OnNotifyPropertyChanged(nameof (ImageIndex));
      }
    }

    [DefaultValue(typeof (Size), "0, 0")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Size Size
    {
      get
      {
        return this.customSize ?? Size.Empty;
      }
      set
      {
        if (!(value != this.Size))
          return;
        this.customSize = new Size?(value);
        this.IsMeasureValid = false;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value that indicates if this item is current.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool Current
    {
      get
      {
        return this.bitState[16];
      }
      internal set
      {
        if (this.Current == value || this.OnNotifyPropertyChanging(nameof (Current)))
          return;
        this.bitState[16] = value;
        this.OnNotifyPropertyChanged(nameof (Current));
      }
    }

    internal bool IsMeasureValid
    {
      get
      {
        if (!this.bitState[32])
          return false;
        if (this.owner != null)
          return this.owner.IsItemsMeasureValid;
        return true;
      }
      set
      {
        this.bitState[32] = value;
      }
    }

    internal bool IsLastInRow
    {
      get
      {
        return this.bitState[8];
      }
      set
      {
        this.bitState[8] = value;
      }
    }

    [Browsable(false)]
    public Size ActualSize
    {
      get
      {
        return this.actualSize;
      }
      internal set
      {
        if (this.actualSize != value && !this.OnNotifyPropertyChanging(nameof (ActualSize)))
        {
          if (this.owner != null)
            this.owner.ViewElement.Scroller.UpdateScrollRange(this.owner.ViewElement.Scroller.Scrollbar.Maximum + (value.Height - this.actualSize.Height), false);
          this.actualSize = value;
          this.OnNotifyPropertyChanged(nameof (ActualSize));
        }
        this.IsMeasureValid = true;
      }
    }

    internal ListViewDetailsCache Cache
    {
      get
      {
        return this.cache;
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HasStyle
    {
      get
      {
        return this.style != null;
      }
    }

    private ListViewDataItemStyle ItemStyle
    {
      get
      {
        if (this.style == null)
          this.style = new ListViewDataItemStyle();
        return this.style;
      }
    }

    [Browsable(true)]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual ListViewDataItemGroup Group
    {
      get
      {
        return this.group;
      }
      set
      {
        if (this.group == value)
          return;
        this.SetGroupCore(value);
      }
    }

    [DefaultValue(null)]
    [TypeConverter(typeof (StringConverter))]
    [Description("Tag object that can be used to store user data, corresponding to the element")]
    [Localizable(false)]
    [Bindable(true)]
    [Category("Data")]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        if (this.tag == value || this.OnNotifyPropertyChanging(nameof (Tag)))
          return;
        this.tag = value;
        this.OnNotifyPropertyChanged(nameof (Tag));
      }
    }

    [Browsable(false)]
    public virtual bool IsDataBound
    {
      get
      {
        if (this.owner != null)
          return this.owner.DataSource != null;
        return false;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadListView ListView
    {
      get
      {
        if (this.owner != null)
          return this.owner.ElementTree.Control as RadListView;
        return (RadListView) null;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadListViewElement Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        if (this.owner == value)
          return;
        this.owner = value;
        this.OnNotifyPropertyChanged(nameof (Owner));
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual object Value
    {
      get
      {
        if (this.IsDataBound)
          return this.GetBoundValue();
        return this.GetUnboundValue();
      }
      set
      {
        if (this.owner != null && this.owner.OnValueChanging(new ListViewItemValueChangingEventArgs(this, value, this.Value)))
          return;
        if (this.IsDataBound)
          this.SetBoundValue(value);
        else
          this.SetUnboundValue(value);
        if (this.owner == null)
          return;
        this.owner.OnValueChanged(new ListViewItemValueChangedEventArgs(this));
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the text displayed in the label of the list item.")]
    [DefaultValue(null)]
    public virtual string Text
    {
      get
      {
        if (this.dataBoundItem == null)
          return Convert.ToString(this.Value);
        object displayValue = this.GetDisplayValue();
        if (displayValue == null)
          return Convert.ToString(this.dataBoundItem);
        return Convert.ToString(displayValue);
      }
      set
      {
        if (!(this.Text != value) || this.OnNotifyPropertyChanging(nameof (Text)))
          return;
        this.Value = (object) value;
        this.OnNotifyPropertyChanged(nameof (Text));
      }
    }

    [DefaultValue(false)]
    [Description("Gets a value that indicates if this item is selected.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual bool Selected
    {
      get
      {
        return this.bitState[1];
      }
      internal set
      {
        if (value == this.Selected || this.OnNotifyPropertyChanging(nameof (Selected)))
          return;
        this.bitState[1] = value;
        this.OnNotifyPropertyChanged(nameof (Selected));
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [RadPropertyDefaultValue("Enabled", typeof (RadElement))]
    [Description("Gets or sets whether this item responds to GUI events.")]
    public bool Enabled
    {
      get
      {
        return this.bitState[2];
      }
      set
      {
        if (value == this.Enabled || this.OnNotifyPropertyChanging(nameof (Enabled)))
          return;
        this.bitState[2] = value;
        this.OnNotifyPropertyChanged(nameof (Enabled));
      }
    }

    [Browsable(true)]
    [Description("Gets a value that indicates if this item is currently visible.")]
    [DefaultValue(true)]
    public bool Visible
    {
      get
      {
        return this.bitState[4];
      }
      set
      {
        if (this.Visible == value || this.OnNotifyPropertyChanging(nameof (Visible)))
          return;
        this.bitState[4] = value;
        this.OnNotifyPropertyChanged(nameof (Visible));
      }
    }

    [Description("Gets a value that indicating the current check state of the item.")]
    [Browsable(true)]
    [DefaultValue(Telerik.WinControls.Enumerations.ToggleState.Off)]
    public virtual Telerik.WinControls.Enumerations.ToggleState CheckState
    {
      get
      {
        if (this.Owner != null && this.Owner.IsDataBound && !string.IsNullOrEmpty(this.Owner.CheckedMember))
          return this.GetBoundCheckedValue();
        return this.checkState;
      }
      set
      {
        if (this.CheckState == value || this.OnNotifyPropertyChanging(nameof (CheckState)))
          return;
        if (this.Owner != null && this.Owner.IsDataBound && !string.IsNullOrEmpty(this.Owner.CheckedMember))
        {
          PropertyDescriptor targetField = this.Owner.ListSource.BoundProperties.Find(this.Owner.CheckedMember, !this.Owner.ListSource.UseCaseSensitiveFieldNames);
          this.Owner.ListSource.SetBoundValue((IDataItem) this, this.Owner.CheckedMember, this.FormatToggleStateValue(value, targetField));
        }
        this.checkState = value;
        this.OnNotifyPropertyChanged(nameof (CheckState));
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public Image Image
    {
      get
      {
        if (this.image != null)
          return this.image;
        if (this.owner == null)
          return (Image) null;
        int index = this.ImageIndex >= 0 ? this.ImageIndex : this.owner.ImageIndex;
        if (index >= 0 && string.IsNullOrEmpty(this.ImageKey))
        {
          RadControl control = this.owner.ElementTree.Control as RadControl;
          if (control != null)
          {
            ImageList imageList = control.ImageList;
            if (imageList != null && index < imageList.Images.Count)
              return imageList.Images[index];
          }
        }
        string str = (string) null;
        if (this.owner != null)
          str = this.owner.ImageKey;
        string key = string.IsNullOrEmpty(this.ImageKey) ? str : this.ImageKey;
        if (!string.IsNullOrEmpty(key))
        {
          RadControl control = this.owner.ElementTree.Control as RadControl;
          if (control != null)
          {
            ImageList imageList = control.ImageList;
            if (imageList != null && imageList.Images.Count > 0 && imageList.Images.ContainsKey(key))
              return imageList.Images[key];
          }
        }
        return (Image) null;
      }
      set
      {
        if (this.image == value || this.OnNotifyPropertyChanging(nameof (Image)))
          return;
        this.image = value;
        this.OnNotifyPropertyChanged(nameof (Image));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Description("This collection is used for adding items at design time. It should not be used in runtime.")]
    [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    public virtual ListViewSubDataItemCollection SubItems
    {
      get
      {
        if (this.subItems == null)
          this.subItems = new ListViewSubDataItemCollection(this);
        return this.subItems;
      }
    }

    [Browsable(true)]
    [DefaultValue(TextImageRelation.ImageBeforeText)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public TextImageRelation TextImageRelation
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.TextImageRelation;
        return ListViewDataItemStyle.DefaultTextImageRelation;
      }
      set
      {
        if (this.TextImageRelation == value || this.OnNotifyPropertyChanging(nameof (TextImageRelation)))
          return;
        this.ItemStyle.TextImageRelation = value;
        this.OnNotifyPropertyChanged(nameof (TextImageRelation));
      }
    }

    [DefaultValue(ContentAlignment.MiddleLeft)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public ContentAlignment TextAlignment
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.TextAlignment;
        return ListViewDataItemStyle.DefaultTextAlignment;
      }
      set
      {
        if (this.TextAlignment == value || this.OnNotifyPropertyChanging(nameof (TextAlignment)))
          return;
        this.ItemStyle.TextAlignment = value;
        this.OnNotifyPropertyChanged(nameof (TextAlignment));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment ImageAlignment
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.ImageAlignment;
        return ListViewDataItemStyle.DefaultImageAlignment;
      }
      set
      {
        if (this.ImageAlignment == value || this.OnNotifyPropertyChanging(nameof (ImageAlignment)))
          return;
        this.ItemStyle.ImageAlignment = value;
        this.OnNotifyPropertyChanged(nameof (ImageAlignment));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(null)]
    [Browsable(true)]
    public Font Font
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.Font;
        return ListViewDataItemStyle.DefaultFont;
      }
      set
      {
        if (this.Font == value || this.OnNotifyPropertyChanging(nameof (Font)))
          return;
        this.ItemStyle.Font = value;
        this.OnNotifyPropertyChanged(nameof (Font));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(typeof (Color), "")]
    [Browsable(true)]
    public Color ForeColor
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.ForeColor;
        return ListViewDataItemStyle.DefaultForeColor;
      }
      set
      {
        if (!(this.ForeColor != value) || this.OnNotifyPropertyChanging(nameof (ForeColor)))
          return;
        this.ItemStyle.ForeColor = value;
        this.OnNotifyPropertyChanged(nameof (ForeColor));
      }
    }

    [DefaultValue(typeof (Color), "")]
    [Browsable(true)]
    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [Description("Gets or sets the first back color.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BackColor
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.BackColor;
        return ListViewDataItemStyle.DefaultBackColor;
      }
      set
      {
        if (!(this.BackColor != value) || this.OnNotifyPropertyChanging(nameof (BackColor)))
          return;
        this.ItemStyle.BackColor = value;
        this.OnNotifyPropertyChanged(nameof (BackColor));
      }
    }

    [Browsable(true)]
    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "")]
    [Description("Gets or sets the backcolor of the list item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BackColor2
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.BackColor2;
        return ListViewDataItemStyle.DefaultBackColor2;
      }
      set
      {
        if (!(this.BackColor2 != value) || this.OnNotifyPropertyChanging(nameof (BackColor2)))
          return;
        this.ItemStyle.BackColor2 = value;
        this.OnNotifyPropertyChanged(nameof (BackColor2));
      }
    }

    [DefaultValue(typeof (Color), "")]
    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [Description("Gets or sets the backcolor of the list item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public Color BackColor3
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.BackColor3;
        return ListViewDataItemStyle.DefaultBackColor3;
      }
      set
      {
        if (!(this.BackColor3 != value) || this.OnNotifyPropertyChanging(nameof (BackColor3)))
          return;
        this.ItemStyle.BackColor3 = value;
        this.OnNotifyPropertyChanged(nameof (BackColor3));
      }
    }

    [DefaultValue(typeof (Color), "")]
    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [Browsable(true)]
    [Description("Gets or sets the backcolor of the list item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BackColor4
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.BackColor4;
        return ListViewDataItemStyle.DefaultBackColor4;
      }
      set
      {
        if (!(this.BackColor4 != value) || this.OnNotifyPropertyChanging(nameof (BackColor4)))
          return;
        this.ItemStyle.BackColor4 = value;
        this.OnNotifyPropertyChanged(nameof (BackColor4));
      }
    }

    [Browsable(true)]
    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "")]
    [Description("Gets or sets the border color of the list item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color BorderColor
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.BorderColor;
        return ListViewDataItemStyle.DefaultBorderColor;
      }
      set
      {
        if (!(this.BorderColor != value) || this.OnNotifyPropertyChanging(nameof (BorderColor)))
          return;
        this.ItemStyle.BorderColor = value;
        this.OnNotifyPropertyChanged(nameof (BorderColor));
      }
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [NotifyParentProperty(true)]
    [DefaultValue(90f)]
    [Description("Gets or sets gradient angle for linear gradient.")]
    public float GradientAngle
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.GradientAngle;
        return ListViewDataItemStyle.DefaultGradientAngle;
      }
      set
      {
        if ((double) this.GradientAngle == (double) value || this.OnNotifyPropertyChanging(nameof (GradientAngle)))
          return;
        this.ItemStyle.GradientAngle = value;
        this.OnNotifyPropertyChanged(nameof (GradientAngle));
      }
    }

    [Category("Appearance")]
    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(0.5f)]
    [Description("Gets or sets GradientPercentage for linear, glass, office glass, gel, vista and radial gradients.")]
    public float GradientPercentage
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.GradientPercentage;
        return ListViewDataItemStyle.DefaultGradientPercentage;
      }
      set
      {
        if ((double) this.GradientPercentage == (double) value || this.OnNotifyPropertyChanging(nameof (GradientPercentage)))
          return;
        this.ItemStyle.GradientPercentage = value;
        this.OnNotifyPropertyChanged(nameof (GradientPercentage));
      }
    }

    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [DefaultValue(0.5f)]
    [Description("Gets or sets GradientPercentage for office glass, vista, and radial gradients.")]
    public float GradientPercentage2
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.GradientPercentage2;
        return ListViewDataItemStyle.DefaultGradientPercentage2;
      }
      set
      {
        if ((double) this.GradientPercentage2 == (double) value || this.OnNotifyPropertyChanging(nameof (GradientPercentage2)))
          return;
        this.ItemStyle.GradientPercentage2 = value;
        this.OnNotifyPropertyChanged(nameof (GradientPercentage2));
      }
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [NotifyParentProperty(true)]
    [DefaultValue(GradientStyles.Linear)]
    [Description("Gets or sets the gradient angle.")]
    public GradientStyles GradientStyle
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.GradientStyle;
        return ListViewDataItemStyle.DefaultGradientStyle;
      }
      set
      {
        if (this.GradientStyle == value || this.OnNotifyPropertyChanging(nameof (GradientStyle)))
          return;
        this.ItemStyle.GradientStyle = value;
        this.OnNotifyPropertyChanged(nameof (GradientStyle));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the number of used colors in the gradient effect.")]
    [NotifyParentProperty(true)]
    [Category("Appearance")]
    [DefaultValue(4)]
    public int NumberOfColors
    {
      get
      {
        if (this.HasStyle)
          return this.ItemStyle.NumberOfColors;
        return ListViewDataItemStyle.DefaultNumberOfColors;
      }
      set
      {
        if (this.NumberOfColors == value || this.OnNotifyPropertyChanging(nameof (NumberOfColors)))
          return;
        this.ItemStyle.NumberOfColors = value;
        this.OnNotifyPropertyChanged(nameof (NumberOfColors));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public object DataBoundItem
    {
      get
      {
        return this.dataBoundItem;
      }
      set
      {
        this.SetDataBoundItem(false, value);
      }
    }

    [Browsable(false)]
    public int FieldCount
    {
      get
      {
        if (this.owner.ViewType == ListViewType.DetailsView)
          return this.owner.Columns.Count;
        return 1;
      }
    }

    public object this[string name]
    {
      get
      {
        if (this.owner != null && this.owner.Columns.Contains(name))
          return this[this.owner.Columns[name]];
        return this.Value;
      }
      set
      {
        if (this.owner != null && this.owner.Columns.Contains(name))
          this[this.owner.Columns[name]] = value;
        else
          this.Value = value;
      }
    }

    public object this[int index]
    {
      get
      {
        if (this.owner == null)
        {
          if (this.SubItems.Count <= index)
            return this.Value;
          return this.SubItems[index];
        }
        if (this.owner != null && this.owner.Columns.Count > index)
          return this[this.owner.Columns[index]];
        return this.Value;
      }
      set
      {
        if (this.owner == null)
        {
          while (this.SubItems.Count <= index)
            this.SubItems.Add(new object());
          this.SubItems[index] = value;
        }
        else if (this.owner != null && this.owner.Columns.Count > index)
          this[this.owner.Columns[index]] = value;
        else
          this.Value = value;
      }
    }

    public object this[ListViewDetailColumn column]
    {
      get
      {
        if (column.Accessor[this] == null && this.subItems != null && this.owner.Columns.IndexOf(column) < this.subItems.Count)
        {
          if (this.owner.IsDesignMode)
            return this.subItems[this.owner.Columns.IndexOf(column)];
          column.Accessor.SuspendItemNotifications();
          column.Accessor[this] = this.subItems[this.owner.Columns.IndexOf(column)];
          column.Accessor.ResumeItemNotifications();
        }
        return column.Accessor[this];
      }
      set
      {
        column.Accessor[this] = value;
      }
    }

    public int IndexOf(string name)
    {
      if (this.owner != null && this.owner.Columns.Contains(name))
        return this.owner.Columns.IndexOf(name);
      return 0;
    }

    protected virtual object GetBoundValue()
    {
      if (!string.IsNullOrEmpty(this.owner.ValueMember))
        return this.GetValue(this.owner.ValueMember);
      if (string.IsNullOrEmpty(this.owner.DisplayMember))
        return this.dataBoundItem;
      return this.GetValue(this.owner.DisplayMember);
    }

    protected virtual object GetUnboundValue()
    {
      return this.unboundValue;
    }

    protected virtual void SetUnboundValue(object value)
    {
      if (this.unboundValue == value || this.OnNotifyPropertyChanging("Value"))
        return;
      if (this.Owner != null)
        this.Owner.ListSource.NotifyItemChanging(this);
      this.unboundValue = value;
      this.OnNotifyPropertyChanged("Value");
      if (this.Owner == null)
        return;
      this.Owner.ListSource.NotifyItemChanged(this);
    }

    protected virtual void SetBoundValue(object value)
    {
      if (!string.IsNullOrEmpty(this.owner.ValueMember))
      {
        this.owner.ListSource.SetBoundValue((IDataItem) this, this.owner.ValueMember, value);
      }
      else
      {
        if (string.IsNullOrEmpty(this.owner.DisplayMember))
          return;
        this.owner.ListSource.SetBoundValue((IDataItem) this, this.owner.DisplayMember, value);
      }
    }

    protected internal virtual void SetDataBoundItem(bool dataBinding, object value)
    {
      this.dataBoundItem = value;
      if (this.Owner != null && this.Owner.DataSource != null)
        this.Owner.OnItemDataBound(this);
      this.OnNotifyPropertyChanged("DataBoundItem");
    }

    internal void SetGroupCore(ListViewDataItemGroup value, bool changeGroupCollection)
    {
      if (changeGroupCollection)
        this.SetGroupCore(value);
      else
        this.group = value;
    }

    private void SetGroupCore(ListViewDataItemGroup value)
    {
      if (this.OnNotifyPropertyChanging("Group"))
        return;
      if (this.group != null)
        this.group.Items.InnerList.Remove(this);
      this.group = value;
      if (this.group != null)
        this.group.Items.InnerList.Add(this);
      this.OnNotifyPropertyChanged("Group");
    }

    protected object GetValue(string propertyName)
    {
      object obj;
      try
      {
        obj = propertyName.Split('.').Length <= 1 ? this.owner.ListSource.GetBoundValue(this.DataBoundItem, propertyName) : this.GetSubPropertyValue(propertyName, this.DataBoundItem);
      }
      catch (ArgumentException ex)
      {
        obj = this.DataBoundItem;
        this.owner.DisplayMember = "";
        this.owner.ValueMember = "";
      }
      return obj;
    }

    private object GetSubPropertyValue(string propertyPath, object dataObject)
    {
      PropertyDescriptor innerDescriptor = (PropertyDescriptor) null;
      object innerObject = (object) null;
      this.GetSubPropertyByPath(propertyPath, dataObject, out innerDescriptor, out innerObject);
      return innerDescriptor?.GetValue(innerObject);
    }

    private void GetSubPropertyByPath(
      string propertyPath,
      object dataObject,
      out PropertyDescriptor innerDescriptor,
      out object innerObject)
    {
      string[] strArray = propertyPath.Split('.');
      innerDescriptor = this.owner.ListSource.BoundProperties[strArray[0]];
      innerObject = innerDescriptor.GetValue(dataObject);
      for (int index = 1; index < strArray.Length && innerDescriptor != null; ++index)
      {
        innerDescriptor = innerDescriptor.GetChildProperties()[strArray[index]];
        if (index + 1 != strArray.Length)
          innerObject = innerDescriptor.GetValue(innerObject);
      }
    }

    protected object GetDisplayValue()
    {
      if (string.IsNullOrEmpty(this.owner.DisplayMember))
        return (object) this.GetFormattedValue(this.DataBoundItem);
      object obj;
      try
      {
        obj = this.owner.DisplayMember.Split('.').Length <= 1 ? this.owner.ListSource.GetBoundValue(this.DataBoundItem, this.owner.DisplayMember) : this.GetSubPropertyValue(this.owner.DisplayMember, this.DataBoundItem);
      }
      catch (ArgumentException ex)
      {
        obj = this.DataBoundItem;
        this.owner.DisplayMember = "";
        this.owner.ValueMember = "";
        this.owner.CheckedMember = "";
      }
      if (obj == null)
        obj = (object) "";
      return (object) this.GetFormattedValue(obj);
    }

    protected virtual string GetFormattedValue(object value)
    {
      return value.ToString();
    }

    protected Telerik.WinControls.Enumerations.ToggleState GetBoundCheckedValue()
    {
      object obj;
      try
      {
        obj = this.Owner.CheckedMember.Split('.').Length <= 1 ? this.Owner.ListSource.GetBoundValue(this.DataBoundItem, this.Owner.CheckedMember) : this.GetSubPropertyValue(this.Owner.CheckedMember, this.DataBoundItem);
      }
      catch (ArgumentException ex)
      {
        obj = this.dataBoundItem;
        this.owner.DisplayMember = "";
        this.owner.ValueMember = "";
        this.owner.CheckedMember = "";
      }
      if (obj == null)
        obj = (object) "False";
      return this.FormatToggleStateDataSourceValue(obj);
    }

    protected virtual Telerik.WinControls.Enumerations.ToggleState FormatToggleStateDataSourceValue(
      object value)
    {
      Telerik.WinControls.Enumerations.ToggleState toggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
      if (value == null)
        return toggleState;
      System.Type type = value.GetType();
      if ((object) type == (object) typeof (Telerik.WinControls.Enumerations.ToggleState))
        return (Telerik.WinControls.Enumerations.ToggleState) value;
      if ((object) type == (object) typeof (bool) || (object) type == (object) typeof (int) || (object) type == (object) typeof (Decimal))
        toggleState = Convert.ToInt32(value) != 0 ? (Convert.ToInt32(value) != 2 ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Indeterminate) : Telerik.WinControls.Enumerations.ToggleState.Off;
      else if ((object) type == (object) typeof (string) && value != null)
      {
        string lower = value.ToString().ToLower();
        if (lower == "indeterminate")
          ;
        toggleState = lower == "true" || lower == "t" || lower == "on" ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      }
      else if ((object) type == (object) typeof (System.Windows.Forms.CheckState))
      {
        switch ((System.Windows.Forms.CheckState) value)
        {
          case System.Windows.Forms.CheckState.Unchecked:
            toggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            break;
          case System.Windows.Forms.CheckState.Checked:
            toggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            break;
          case System.Windows.Forms.CheckState.Indeterminate:
            toggleState = Telerik.WinControls.Enumerations.ToggleState.Indeterminate;
            break;
        }
      }
      return toggleState;
    }

    protected virtual object FormatToggleStateValue(
      Telerik.WinControls.Enumerations.ToggleState value,
      PropertyDescriptor targetField)
    {
      if ((object) targetField.PropertyType == (object) typeof (Telerik.WinControls.Enumerations.ToggleState))
        return (object) value;
      if ((object) targetField.PropertyType == (object) typeof (bool))
        return (object) (value == Telerik.WinControls.Enumerations.ToggleState.On);
      if ((object) targetField.PropertyType == (object) typeof (int) || (object) targetField.PropertyType == (object) typeof (Decimal))
        return (object) Convert.ToInt32((object) value);
      if ((object) targetField.PropertyType == (object) typeof (System.Windows.Forms.CheckState))
      {
        switch (value)
        {
          case Telerik.WinControls.Enumerations.ToggleState.Off:
            return (object) System.Windows.Forms.CheckState.Unchecked;
          case Telerik.WinControls.Enumerations.ToggleState.On:
            return (object) System.Windows.Forms.CheckState.Checked;
          case Telerik.WinControls.Enumerations.ToggleState.Indeterminate:
            return (object) System.Windows.Forms.CheckState.Indeterminate;
        }
      }
      else
      {
        if (targetField.Converter.CanConvertFrom(typeof (Telerik.WinControls.Enumerations.ToggleState)))
          return targetField.Converter.ConvertFrom((object) value);
        if (targetField.Converter.CanConvertFrom(typeof (string)))
          return targetField.Converter.ConvertFromInvariantString(value.ToString());
      }
      return (object) null;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public event PropertyChangingEventHandlerEx PropertyChanging;

    protected void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs args)
    {
      if (args.PropertyName == "Text" || args.PropertyName == "Value")
        this.IsMeasureValid = false;
      if (args.PropertyName == "Selected" && this.owner != null)
      {
        if (!(this is ListViewDataItemGroup))
          this.owner.SelectedItems.ProcessSelectedItem(this);
        if (this.group != null)
          this.group.OnItemSelectedChanged();
      }
      if (args.PropertyName == "CheckState" && this.owner != null)
      {
        this.owner.CheckedItems.ProcessCheckedItem(this);
        this.owner.OnItemCheckedChanged(new ListViewItemEventArgs(this));
      }
      if (args.PropertyName == "Owner" && this.group != null && this.owner != this.group.owner)
        this.Group = (ListViewDataItemGroup) null;
      if (this.PropertyChanged != null)
        this.PropertyChanged((object) this, args);
      if ((args.PropertyName == "Visible" || args.PropertyName == "Group") && this.owner != null)
      {
        this.owner.ViewElement.ViewElement.InvalidateMeasure();
        this.owner.ViewElement.ViewElement.UpdateLayout();
        this.owner.ViewElement.Scroller.UpdateScrollRange();
      }
      if (!(args.PropertyName == "Expanded") || this.owner == null)
        return;
      this.owner.ViewElement.ViewElement.Invalidate();
    }

    protected bool OnNotifyPropertyChanging(string propertyName)
    {
      return this.OnNotifyPropertyChanging(new PropertyChangingEventArgsEx(propertyName));
    }

    protected virtual bool OnNotifyPropertyChanging(PropertyChangingEventArgsEx args)
    {
      if (args.PropertyName == "CheckState" && this.owner != null)
        args.Cancel = this.owner.OnItemCheckedChanging(new ListViewItemCancelEventArgs(this));
      if (this.PropertyChanging == null)
        return args.Cancel;
      this.PropertyChanging((object) this, args);
      return args.Cancel;
    }

    public virtual void Dispose()
    {
    }
  }
}
