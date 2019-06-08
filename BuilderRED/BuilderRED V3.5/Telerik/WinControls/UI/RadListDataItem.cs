// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class RadListDataItem : RadObject, IDataItem
  {
    private string cachedText = "";
    internal bool measureDirty = true;
    public static readonly RadProperty HeightProperty = RadProperty.Register(nameof (Height), typeof (int), typeof (RadListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1));
    public static readonly RadProperty ActiveProperty = RadProperty.Register(nameof (Active), typeof (bool), typeof (RadListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static readonly RadProperty SelectedProperty = RadProperty.Register(nameof (Selected), typeof (bool), typeof (RadListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static readonly RadProperty ValueProperty = RadProperty.Register(nameof (Value), typeof (object), typeof (RadListDataItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.None));
    private object dataBoundItem;
    protected ListDataLayer dataLayer;
    protected RadListElement ownerElement;
    internal ListGroup group;
    private bool changingOwner;
    private SizeF measuredSize;
    private object tag;

    static RadListDataItem()
    {
      ElementPropertyOptions options = ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay;
      System.Type forType = typeof (RadListDataItem);
      LightVisualElement.TextImageRelationProperty.OverrideMetadata(forType, (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextImageRelation.ImageBeforeText, options));
      LightVisualElement.ImageAlignmentProperty.OverrideMetadata(forType, (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, options));
      LightVisualElement.TextAlignmentProperty.OverrideMetadata(forType, (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, options));
      LightVisualElement.TextWrapProperty.OverrideMetadata(forType, (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, options));
    }

    public RadListDataItem(string text)
      : this()
    {
      this.Text = text;
    }

    public RadListDataItem(string text, object value)
      : this(text)
    {
      this.Value = value;
    }

    public RadListDataItem()
    {
    }

    [Category("Data")]
    [DefaultValue(null)]
    [TypeConverter(typeof (StringConverter))]
    [Description("Tag object that can be used to store user data, corresponding to the element")]
    [Bindable(true)]
    [Localizable(false)]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        this.tag = value;
      }
    }

    internal virtual ListGroup Group
    {
      get
      {
        return this.group ?? this.ownerElement.groupFactory.DefaultGroup;
      }
      set
      {
        if (this.group == value)
          return;
        this.group = value;
        this.ownerElement.UpdateItemTraverser();
        this.ownerElement.Scroller.UpdateScrollRange();
        this.ownerElement.InvalidateMeasure(true);
      }
    }

    [Browsable(false)]
    public bool IsDataBound
    {
      get
      {
        if (this.dataLayer != null)
          return this.dataLayer.DataSource != null;
        return false;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal ListDataLayer DataLayer
    {
      get
      {
        return this.dataLayer;
      }
      set
      {
        if (this.DataLayer == value)
          return;
        this.dataLayer = value;
        this.OnNotifyPropertyChanged(nameof (DataLayer));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual RadListElement Owner
    {
      get
      {
        return this.ownerElement;
      }
      internal set
      {
        if (this.ownerElement == value || this.changingOwner)
          return;
        this.changingOwner = true;
        if (this.ownerElement != null && value != null)
          this.ownerElement.Items.Remove(this);
        this.ownerElement = value;
        if (this.ownerElement != null)
          this.DataLayer = this.ownerElement.DataLayer;
        this.OnNotifyPropertyChanged(nameof (Owner));
        this.changingOwner = false;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Control OwnerControl
    {
      get
      {
        return this.ownerElement.ElementTree.Control;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the visual height of this item. This property can be set only when AutoSizeItems of the parent RadListControl is true.")]
    [Browsable(true)]
    [DefaultValue(-1)]
    public int Height
    {
      get
      {
        return (int) this.GetValue(RadListDataItem.HeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadListDataItem.HeightProperty, (object) value);
        if (this.Owner == null)
          return;
        this.Owner.Scroller.UpdateScrollRange();
        this.Owner.ViewElement.UpdateItems();
      }
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Localizable(true)]
    [RadPropertyDefaultValue("TextWrap", typeof (RadListDataItem))]
    [Category("Appearance")]
    [Description("Determines whether text wrap is enabled.")]
    public bool TextWrap
    {
      get
      {
        return (bool) this.GetValue(LightVisualElement.TextWrapProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.TextWrapProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int RowIndex
    {
      get
      {
        if (this.dataLayer == null)
          return -1;
        return this.dataLayer.GetRowIndex(this);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public object DisplayValue
    {
      get
      {
        if (this.dataLayer == null)
          return (object) "";
        return this.dataLayer.GetDisplayValue(this);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual object Value
    {
      get
      {
        if (!this.IsDataBound)
          return this.GetUnboundValue();
        try
        {
          return this.GetBoundValue();
        }
        catch (Exception ex)
        {
          return this.GetUnboundValue();
        }
      }
      set
      {
        if (this.IsDataBound)
          throw new InvalidOperationException("The Value property can not be set while in bound mode.");
        this.SetUnboundValue(value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool Active
    {
      get
      {
        return (bool) this.GetValue(RadListDataItem.ActiveProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadListDataItem.ActiveProperty, (object) value);
      }
    }

    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Description("Gets or sets a value that indicates if this item is selected. Setting this property will cause the selection events of the owner list control to fire if there is one.")]
    public virtual bool Selected
    {
      get
      {
        return (bool) this.GetValue(RadListDataItem.SelectedProperty);
      }
      set
      {
        if (value == this.Selected)
          return;
        int num = (int) this.SetValue(RadListDataItem.SelectedProperty, (object) value);
      }
    }

    [Description("Gets or sets whether this item responds to GUI events.")]
    [RadPropertyDefaultValue("Enabled", typeof (RadElement))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public bool Enabled
    {
      get
      {
        return (bool) this.GetValue(RadElement.EnabledProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadElement.EnabledProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the text for this RadListDataItem instance.")]
    public virtual string Text
    {
      get
      {
        if (this.GetValueSource(RadItem.TextProperty) == ValueSource.Local || this.dataBoundItem == null)
          return (string) this.GetValue(RadItem.TextProperty);
        object displayValue = this.DisplayValue;
        if (displayValue == null)
          return this.dataBoundItem.ToString();
        return displayValue.ToString();
      }
      set
      {
        int num = (int) this.SetValue(RadItem.TextProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal string CachedText
    {
      get
      {
        return this.cachedText;
      }
      set
      {
        if (this.cachedText == value)
          return;
        this.cachedText = value;
        this.OnNotifyPropertyChanged(nameof (CachedText));
      }
    }

    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Description("Gets or sets an image for this RadListDataItem instance.")]
    public Image Image
    {
      get
      {
        return (Image) this.GetValue(LightVisualElement.ImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.ImageProperty, (object) value);
      }
    }

    [DefaultValue(TextImageRelation.ImageBeforeText)]
    [Description("Gets or sets the text-image relation for this RadListDataItem instance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public TextImageRelation TextImageRelation
    {
      get
      {
        return (TextImageRelation) this.GetValue(LightVisualElement.TextImageRelationProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.TextImageRelationProperty, (object) value);
      }
    }

    [Description("Gets or sets the image alignment for this RadListDataItem instance.")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(LightVisualElement.ImageAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.ImageAlignmentProperty, (object) value);
      }
    }

    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Browsable(true)]
    [Description("Gets or sets the text alignment for this RadListDataItem instance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ContentAlignment TextAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(LightVisualElement.TextAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.TextAlignmentProperty, (object) value);
      }
    }

    [Description("Gets or sets the text orientation for this RadListDataItem instance.")]
    [DefaultValue(Orientation.Horizontal)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public Orientation TextOrientation
    {
      get
      {
        return (Orientation) this.GetValue(RadItem.TextOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadItem.TextOrientationProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RadPropertyDefaultValue("Font", typeof (VisualElement))]
    [Description("Gets or sets the font for this RadListDataItem instance.")]
    public Font Font
    {
      get
      {
        return (Font) this.GetValue(VisualElement.FontProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.FontProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the text color for this RadListDataItem instance.")]
    [RadPropertyDefaultValue("ForeColor", typeof (VisualElement))]
    [Browsable(true)]
    public Color ForeColor
    {
      get
      {
        return (Color) this.GetValue(VisualElement.ForeColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.ForeColorProperty, (object) value);
      }
    }

    [Browsable(false)]
    [Description("Gets a value that indicates if this item is currently visible.")]
    public bool IsVisible
    {
      get
      {
        return this.VisualItem != null;
      }
    }

    [Browsable(false)]
    public RadListVisualItem VisualItem
    {
      get
      {
        if (this.Owner != null)
        {
          foreach (RadElement child in this.Owner.ViewElement.Children)
          {
            if (child is RadListVisualItem)
            {
              RadListVisualItem radListVisualItem = child as RadListVisualItem;
              if (radListVisualItem.Data == this)
                return radListVisualItem;
            }
          }
        }
        return (RadListVisualItem) null;
      }
    }

    [Description("Gets or sets the preferred size for the element which will present this item.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SizeF MeasuredSize
    {
      get
      {
        return this.measuredSize;
      }
      set
      {
        if (this.measuredSize != value)
          this.measuredSize = value;
        this.measureDirty = false;
      }
    }

    public virtual int Index
    {
      get
      {
        if (this.Owner != null)
          return this.Owner.Items.IndexOf(this);
        return -1;
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int FieldCount
    {
      get
      {
        return 1;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public object this[int index]
    {
      get
      {
        if (this.dataLayer == null)
          return (object) null;
        return this.dataLayer.GetDisplayValue(this);
      }
      set
      {
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object this[string name]
    {
      get
      {
        if (this.dataLayer == null)
          return (object) null;
        if (!this.dataLayer.ListSource.IsDataBound || name.Equals(this.dataLayer.DisplayMember, StringComparison.InvariantCultureIgnoreCase))
          return this.dataLayer.GetDisplayValue(this);
        if (name.Split('.').Length > 1)
          return this.dataLayer.GetSubPropertyValue(name, this.DataBoundItem);
        return this.dataLayer.ListSource.GetBoundValue(this.DataBoundItem, name);
      }
      set
      {
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public int IndexOf(string name)
    {
      return 0;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      this.measureDirty = true;
      if (e.Property == RadItem.TextProperty)
      {
        this.cachedText = (string) e.NewValue;
        if (this.ownerElement != null && this.dataLayer != null)
        {
          int selectedIndex = this.ownerElement.SelectedIndex;
          RadListDataItem selectedItem = this.ownerElement.SelectedItem;
          this.ownerElement.SuspendSelectionEvents = true;
          this.dataLayer.ListSource.Refresh();
          this.ownerElement.SelectedItem = selectedItem;
          this.ownerElement.SuspendSelectionEvents = false;
          if (selectedIndex != this.ownerElement.SelectedIndex)
            this.ownerElement.OnSelectedIndexChanged(this.ownerElement.SelectedIndex);
        }
      }
      if (this.Owner != null)
        this.Owner.OnDataItemPropertyChanged((object) this, e);
      base.OnPropertyChanged(e);
    }

    public override string ToString()
    {
      return this.Text;
    }

    protected virtual object GetBoundValue()
    {
      if (this.dataLayer.ValueMember == "")
        return this.dataBoundItem;
      return this.dataLayer.GetValue(this);
    }

    protected virtual object GetUnboundValue()
    {
      return this.GetValue(RadListDataItem.ValueProperty);
    }

    protected virtual void SetUnboundValue(object value)
    {
      int num = (int) this.SetValue(RadListDataItem.ValueProperty, value);
    }

    protected internal virtual void SetDataBoundItem(bool dataBinding, object value)
    {
      this.dataBoundItem = value;
      if (this.Owner != null && this.Owner.DataSource != null)
        this.Owner.OnListItemDataBound(this);
      this.cachedText = this.dataLayer.GetUnformattedValue(this);
      this.OnNotifyPropertyChanged("DataBoundItem");
    }
  }
}
