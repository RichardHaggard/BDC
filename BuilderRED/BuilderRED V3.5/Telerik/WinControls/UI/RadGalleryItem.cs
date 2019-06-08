// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGalleryItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadGalleryItem : RadButtonItem, ICloneable
  {
    private static RadProperty ActiveProperty = RadProperty.Register(nameof (Active), typeof (bool), typeof (RadGalleryItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DescriptionFontProperty = RadProperty.Register(nameof (DescriptionFont), typeof (Font), typeof (RadGalleryItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Control.DefaultFont, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    internal static RadProperty IsSelectedProperty = RadProperty.Register(nameof (IsSelected), typeof (bool), typeof (RadGalleryItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private RadGalleryElement owner;
    private FillPrimitive fillPrimitive;
    private TextPrimitive textPrimitive;
    private BorderPrimitive borderPrimitive;
    private ImageAndTextLayoutPanel layoutPanel;
    private ImagePrimitive imagePrimitive;
    private TextPrimitive descriptionTextPrimitive;
    private Font descriptionFontCache;

    static RadGalleryItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GalleryItemStateManager(), typeof (RadGalleryItem));
    }

    public RadGalleryItem()
    {
    }

    public RadGalleryItem(string text)
      : base(text)
    {
    }

    public RadGalleryItem(string text, Image image)
      : base(text, image)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.NotifyParentOnMouseInput = true;
    }

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "GalleryItemSelectionFill";
      this.fillPrimitive.Visibility = ElementVisibility.Hidden;
      this.fillPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "GalleryItemSelectionBorder";
      this.borderPrimitive.Visibility = ElementVisibility.Hidden;
      this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.borderPrimitive.ZIndex = 100;
      this.imagePrimitive = new ImagePrimitive();
      int num1 = (int) this.imagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      int num2 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadButtonItem.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadButtonItem.ImageProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadButtonItem.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      this.textPrimitive = new TextPrimitive();
      this.textPrimitive.Class = "GalleryItemText";
      int num5 = (int) this.textPrimitive.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.OneWay);
      int num6 = (int) this.textPrimitive.BindProperty(RadElement.AlignmentProperty, (RadObject) this, RadButtonItem.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      this.descriptionTextPrimitive = new TextPrimitive();
      this.descriptionTextPrimitive.Class = "GalleryItemDescriptionText";
      int num7 = (int) this.descriptionTextPrimitive.BindProperty(VisualElement.FontProperty, (RadObject) this, RadGalleryItem.DescriptionFontProperty, PropertyBindingOptions.OneWay);
      StackLayoutPanel stackLayoutPanel = new StackLayoutPanel();
      stackLayoutPanel.Orientation = Orientation.Vertical;
      stackLayoutPanel.EqualChildrenWidth = true;
      int num8 = (int) stackLayoutPanel.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      stackLayoutPanel.Children.Add((RadElement) this.textPrimitive);
      stackLayoutPanel.Children.Add((RadElement) this.descriptionTextPrimitive);
      this.layoutPanel = new ImageAndTextLayoutPanel();
      int num9 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.DisplayStyleProperty, (RadObject) this, RadButtonItem.DisplayStyleProperty, PropertyBindingOptions.OneWay);
      int num10 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.ImageAlignmentProperty, (RadObject) this, RadButtonItem.ImageAlignmentProperty, PropertyBindingOptions.OneWay);
      int num11 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.TextAlignmentProperty, (RadObject) this, RadButtonItem.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      int num12 = (int) this.layoutPanel.BindProperty(ImageAndTextLayoutPanel.TextImageRelationProperty, (RadObject) this, RadButtonItem.TextImageRelationProperty, PropertyBindingOptions.OneWay);
      this.layoutPanel.Children.Add((RadElement) this.imagePrimitive);
      this.layoutPanel.Children.Add((RadElement) stackLayoutPanel);
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.layoutPanel);
    }

    [RadPropertyDefaultValue("DescriptionText", typeof (RadGalleryItem))]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the description text associated with this item. ")]
    [Bindable(true)]
    [SettingsBindable(true)]
    [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string DescriptionText
    {
      get
      {
        return this.descriptionTextPrimitive.Text;
      }
      set
      {
        this.descriptionTextPrimitive.Text = value;
      }
    }

    [RadDefaultValue("AngleTransform", typeof (ImagePrimitive))]
    [Category("Behavior")]
    [Description("Angle of rotation for the button image")]
    public float ImagePrimitiveAngleTransform
    {
      get
      {
        return this.imagePrimitive.AngleTransform;
      }
      set
      {
        this.imagePrimitive.AngleTransform = value;
      }
    }

    [Category("Appearance")]
    [Description("DescriptionFont - ex. of the description text of an RadGalleryItem. The property is inheritable through the element tree.")]
    [RadPropertyDefaultValue("DescriptionFont", typeof (RadGalleryItem))]
    public virtual Font DescriptionFont
    {
      get
      {
        if (this.descriptionFontCache == null)
          this.descriptionFontCache = (Font) this.GetValue(RadGalleryItem.DescriptionFontProperty);
        return this.descriptionFontCache;
      }
      set
      {
        int num = (int) this.SetValue(RadGalleryItem.DescriptionFontProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Returns whether the gallery item is currently selected.")]
    public bool IsSelected
    {
      get
      {
        return (bool) this.GetValue(RadGalleryItem.IsSelectedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGalleryItem.IsSelectedProperty, (object) value);
      }
    }

    internal bool Active
    {
      get
      {
        return (bool) this.GetValue(RadGalleryItem.ActiveProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadGalleryItem.ActiveProperty, (object) value);
      }
    }

    internal RadGalleryElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    public object Clone()
    {
      return (object) new RadGalleryItem(this.Text, this.Image);
    }

    public bool IsZoomShown()
    {
      PropertyChangeBehaviorCollection behaviors = this.GetBehaviors();
      if (behaviors.Count == 0)
        return false;
      GalleryMouseOverBehavior mouseOverBehavior = behaviors[0] as GalleryMouseOverBehavior;
      if (mouseOverBehavior == null)
        return false;
      return mouseOverBehavior.IsPopupShown;
    }

    public void ZoomHide()
    {
      PropertyChangeBehaviorCollection behaviors = this.GetBehaviors();
      if (behaviors.Count == 0)
        return;
      (behaviors[0] as GalleryMouseOverBehavior)?.ClosePopup();
    }

    public override string ToString()
    {
      if (this.Site != null)
        return this.Site.Name;
      return base.ToString();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadGalleryItem.IsSelectedProperty)
        return;
      foreach (RadObject radObject in this.ChildrenHierarchy)
      {
        int num = (int) radObject.SetValue(RadGalleryItem.IsSelectedProperty, e.NewValue);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      this.layoutPanel.Measure(this.GetClientRectangle(availableSize).Size);
      return this.layoutPanel.DesiredSize + (SizeF) this.Padding.Size + new SizeF(this.borderPrimitive.Width * 2f, this.borderPrimitive.Width * 2f);
    }
  }
}
