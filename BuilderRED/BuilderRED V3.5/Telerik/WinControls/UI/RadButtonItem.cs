// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadButtonItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadButtonItemDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadButtonItem : RadItem, IButtonControl, IImageElement
  {
    public static RadProperty ImageProperty = RadProperty.Register(nameof (Image), typeof (Image), typeof (RadButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DisplayStyleProperty = RadProperty.Register(nameof (DisplayStyle), typeof (DisplayStyle), typeof (RadButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DisplayStyle.ImageAndText, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty ImageIndexProperty = RadProperty.Register(nameof (ImageIndex), typeof (int), typeof (RadButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageKeyProperty = RadProperty.Register(nameof (ImageKey), typeof (string), typeof (RadButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageAlignmentProperty = RadProperty.Register(nameof (ImageAlignment), typeof (ContentAlignment), typeof (RadButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty TextAlignmentProperty = RadProperty.Register(nameof (TextAlignment), typeof (ContentAlignment), typeof (RadButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleCenter, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsParentArrange));
    public static RadProperty TextImageRelationProperty = RadProperty.Register(nameof (TextImageRelation), typeof (TextImageRelation), typeof (RadButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextImageRelation.Overlay, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty IsDefaultProperty = RadProperty.Register(nameof (IsDefault), typeof (bool), typeof (RadButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue));
    public static RadProperty IsPressedProperty = RadProperty.Register(nameof (IsPressed), typeof (bool), typeof (RadButtonItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    internal const long IsSharedImageStateKey = 8796093022208;
    internal const long CaptureOnMouseDownStateKey = 17592186044416;
    internal const long DoPressOnMouseEnterStateKey = 35184372088832;
    internal const long ClickCancelStateKey = 70368744177664;
    internal const long RadButtonItemLastStateKey = 70368744177664;
    private DialogResult dialogResult;
    private static Dictionary<RadProperty, RadProperty> propertiesForMapping;

    static RadButtonItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ButtonItemStateManagerFactory(), typeof (RadButtonItem));
      RadButtonItem.propertiesForMapping = new Dictionary<RadProperty, RadProperty>(1);
      RadButtonItem.propertiesForMapping.Add(ImagePrimitive.ImageProperty, RadButtonItem.ImageProperty);
    }

    public RadButtonItem()
    {
    }

    public RadButtonItem(string text)
    {
      this.Text = text;
    }

    public RadButtonItem(string text, Image image)
    {
      this.Text = text;
      this.Image = image;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BitState[8796093022208L] = true;
      this.BitState[17592186044416L] = true;
    }

    protected override void DisposeManagedResources()
    {
      if (!this.GetBitState(8796093022208L))
        this.Image?.Dispose();
      base.DisposeManagedResources();
    }

    [Localizable(true)]
    [RadPropertyDefaultValue("Image", typeof (RadButtonItem))]
    [Category("Appearance")]
    [Description("Gets or sets the image that is displayed on a button element.")]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter(typeof (ImageTypeConverter))]
    public virtual Image Image
    {
      get
      {
        return (Image) this.GetValue(RadButtonItem.ImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonItem.ImageProperty, (object) value);
      }
    }

    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RadPropertyDefaultValue("ImageIndex", typeof (RadButtonItem))]
    [Category("Appearance")]
    [Localizable(true)]
    [Description("Gets or sets the image list index value of the image displayed on the button control.")]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual int ImageIndex
    {
      get
      {
        return (int) this.GetValue(RadButtonItem.ImageIndexProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonItem.ImageIndexProperty, (object) value);
      }
    }

    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadPropertyDefaultValue("ImageKey", typeof (RadButtonItem))]
    [Description("Gets or sets the key accessor for the image in the ImageList.")]
    [RefreshProperties(RefreshProperties.All)]
    [Category("Appearance")]
    [Localizable(true)]
    public virtual string ImageKey
    {
      get
      {
        return (string) this.GetValue(RadButtonItem.ImageKeyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonItem.ImageKeyProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TextImageRelation", typeof (RadButtonItem))]
    [Description("Gets or sets the position of text and image relative to each other.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public virtual TextImageRelation TextImageRelation
    {
      get
      {
        return (TextImageRelation) this.GetValue(RadButtonItem.TextImageRelationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonItem.TextImageRelationProperty, (object) value);
      }
    }

    [Description("Gets or sets the alignment of image content on the drawing surface.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [RadPropertyDefaultValue("ImageAlignment", typeof (RadButtonItem))]
    [Category("Appearance")]
    public virtual ContentAlignment ImageAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(RadButtonItem.ImageAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonItem.ImageAlignmentProperty, (object) value);
      }
    }

    [Description("Gets or sets the alignment of text content on the drawing surface.")]
    [Localizable(true)]
    [RadPropertyDefaultValue("TextAlignment", typeof (RadButtonItem))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public virtual ContentAlignment TextAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(RadButtonItem.TextAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonItem.TextAlignmentProperty, (object) value);
      }
    }

    [Browsable(true)]
    [Description("Specifies the options for display of image and text primitives in the element.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("DisplayStyle", typeof (RadButtonItem))]
    public virtual DisplayStyle DisplayStyle
    {
      get
      {
        return (DisplayStyle) this.GetValue(RadButtonItem.DisplayStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonItem.DisplayStyleProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [ReadOnly(true)]
    [Browsable(false)]
    [Description("Gets a value indicating whether the button item is in the pressed state.")]
    public virtual bool IsPressed
    {
      get
      {
        return (bool) this.GetValue(RadButtonItem.IsPressedProperty);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual bool IsDefault
    {
      get
      {
        return (bool) this.GetValue(RadButtonItem.IsDefaultProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadButtonItem.IsDefaultProperty, (object) value);
      }
    }

    [DefaultValue(true)]
    [Description("Determines whether the Image value of the current item is shared (reused by other items). This flag is true by default; if it is set to false, then the item itselft will dispose the Image upon its disposal.")]
    [Category("Behavior")]
    public virtual bool IsSharedImage
    {
      get
      {
        return this.GetBitState(8796093022208L);
      }
      set
      {
        this.BitState[8796093022208L] = value;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.ClickMode != ClickMode.Hover && e.Button == MouseButtons.Left)
      {
        if (this.GetBitState(17592186044416L))
          this.Capture = true;
        if (!this.IsPressed)
        {
          int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
        }
      }
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.ClickMode != ClickMode.Hover)
      {
        int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
        if (this.GetBitState(17592186044416L))
          this.Capture = false;
      }
      this.SetBitState(35184372088832L, false);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      if (this.ClickMode != ClickMode.Hover && !this.GetBitState(35184372088832L))
        return;
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
      this.SetBitState(35184372088832L, false);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.IsPressed)
      {
        int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
        this.SetBitState(35184372088832L, true);
      }
      if (this.Capture)
        return;
      int num1 = (int) this.ResetValue(RadElement.IsMouseDownProperty);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (e.KeyCode == Keys.Escape && this.IsPressed)
      {
        int num1 = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
      }
      else
      {
        if (!this.Enabled)
          return;
        if (e.KeyCode == Keys.Space)
        {
          int num2 = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
        }
        else
        {
          if (e.KeyCode != Keys.Return)
            return;
          int num3 = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) true);
          try
          {
            this.PerformClick();
          }
          finally
          {
            int num4 = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
          }
        }
      }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
      if (!this.Enabled || e.KeyCode != Keys.Space)
        return;
      if (!this.IsPressed)
        return;
      try
      {
        this.PerformClick();
      }
      finally
      {
        int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
      }
    }

    protected override void OnClick(EventArgs e)
    {
      if (this.GetBitState(70368744177664L))
      {
        this.SetBitState(70368744177664L, false);
      }
      else
      {
        if (this.IsInValidState(true))
        {
          Control control = this.ElementTree.Control;
          IButtonControl buttonControl = control as IButtonControl;
          if (buttonControl != null)
          {
            Form form = control.FindForm();
            if (form != null)
              form.DialogResult = buttonControl.DialogResult;
          }
        }
        base.OnClick(e);
      }
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      this.OnClick(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.EnabledProperty || (bool) e.NewValue)
        return;
      int num = (int) this.SetValue(RadButtonItem.IsPressedProperty, (object) false);
    }

    DialogResult IButtonControl.DialogResult
    {
      get
      {
        return this.dialogResult;
      }
      set
      {
        this.dialogResult = value;
      }
    }

    void IButtonControl.NotifyDefault(bool value)
    {
      this.IsDefault = value;
    }

    void IButtonControl.PerformClick()
    {
      this.OnClick(EventArgs.Empty);
    }

    internal static Dictionary<RadProperty, RadProperty> PropertiesForMapping
    {
      get
      {
        return RadButtonItem.propertiesForMapping;
      }
    }

    public override RadProperty MapStyleProperty(
      RadProperty propertyToMap,
      string settingType)
    {
      RadProperty radProperty;
      RadButtonItem.PropertiesForMapping.TryGetValue(propertyToMap, out radProperty);
      return radProperty;
    }
  }
}
