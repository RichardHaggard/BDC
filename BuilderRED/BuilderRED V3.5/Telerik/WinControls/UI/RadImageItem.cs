// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadImageItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadImageItem : RadItem
  {
    public static RadProperty BorderVisibleProperty = RadProperty.Register(nameof (BorderVisible), typeof (bool), typeof (RadImageItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageProperty = RadProperty.Register(nameof (Image), typeof (Image), typeof (RadImageItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageIndexProperty = RadProperty.Register(nameof (ImageIndex), typeof (int), typeof (RadImageItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageKeyProperty = RadProperty.Register(nameof (ImageKey), typeof (string), typeof (RadImageItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    private ImagePrimitive imagePrimitive;
    private BorderPrimitive borderPrimitive;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
    }

    [RadPropertyDefaultValue("BorderVisible", typeof (RadImageItem))]
    public bool BorderVisible
    {
      get
      {
        return (bool) this.GetValue(RadImageItem.BorderVisibleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageItem.BorderVisibleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Image", typeof (RadImageItem))]
    [RefreshProperties(RefreshProperties.All)]
    [TypeConverter(typeof (ImageTypeConverter))]
    public Image Image
    {
      get
      {
        return (Image) this.GetValue(RadImageItem.ImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageItem.ImageProperty, (object) value);
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("ImageIndex", typeof (RadImageItem))]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Editor("Telerik.WinControls.UI.Design.ThemeNameEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual int ImageIndex
    {
      get
      {
        return (int) this.GetValue(RadImageItem.ImageIndexProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageItem.ImageIndexProperty, (object) value);
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("ImageKey", typeof (RadImageItem))]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public virtual string ImageKey
    {
      get
      {
        return (string) this.GetValue(RadImageItem.ImageKeyProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageItem.ImageKeyProperty, (object) value);
      }
    }

    protected override void CreateChildElements()
    {
      this.imagePrimitive = new ImagePrimitive();
      this.imagePrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      int num = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadImageItem.ImageProperty, PropertyBindingOptions.TwoWay);
      this.Children.Add((RadElement) this.imagePrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Visibility = ElementVisibility.Hidden;
      this.borderPrimitive.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.Children.Add((RadElement) this.borderPrimitive);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadImageItem.BorderVisibleProperty)
        this.borderPrimitive.Visibility = (bool) e.NewValue ? ElementVisibility.Visible : ElementVisibility.Hidden;
      base.OnPropertyChanged(e);
    }
  }
}
