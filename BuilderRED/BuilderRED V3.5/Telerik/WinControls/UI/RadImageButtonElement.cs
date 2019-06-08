// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadImageButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadImageButtonElement : RadButtonElement
  {
    public static RadProperty ImageHoveredProperty = RadProperty.Register(nameof (ImageHovered), typeof (Image), typeof (RadImageButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageIndexHoveredProperty = RadProperty.Register(nameof (ImageIndexHovered), typeof (int), typeof (RadImageButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageKeyHoveredProperty = RadProperty.Register(nameof (ImageKeyHovered), typeof (string), typeof (RadImageButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    public static RadProperty ImageClickedProperty = RadProperty.Register(nameof (ImageClicked), typeof (Image), typeof (RadImageButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageIndexClickedProperty = RadProperty.Register(nameof (ImageIndexClicked), typeof (int), typeof (RadImageButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ImageKeyClickedProperty = RadProperty.Register(nameof (ImageKeyClicked), typeof (string), typeof (RadImageButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.None));
    private int cachedImageIndex = -1;
    private Image normalStateImage;
    private ValueSource normalStateSource;
    private bool isPreviousStateClicked;
    private bool chaingingImageInternally;

    [Description("Gets or sets the image that is displayed on a button element when it is hovered.")]
    [Editor("Telerik.WinControls.UI.Design.RadImageTypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Appearance")]
    [Localizable(true)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("ImageHovered", typeof (RadButtonItem))]
    public virtual Image ImageHovered
    {
      get
      {
        return (Image) this.GetValue(RadImageButtonElement.ImageHoveredProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageButtonElement.ImageHoveredProperty, (object) value);
      }
    }

    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RadPropertyDefaultValue("ImageIndexHovered", typeof (RadButtonItem))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Gets or sets the image list index value of the image displayed on the button control when it is hovered.")]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual int ImageIndexHovered
    {
      get
      {
        return (int) this.GetValue(RadImageButtonElement.ImageIndexHoveredProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageButtonElement.ImageIndexHoveredProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("ImageKeyHovered", typeof (RadButtonItem))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the key accessor for the image for hovered state in the ImageList.")]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Localizable(true)]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public virtual string ImageKeyHovered
    {
      get
      {
        return (string) this.GetValue(RadImageButtonElement.ImageKeyHoveredProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageButtonElement.ImageKeyHoveredProperty, (object) value);
      }
    }

    [Description("Gets or sets the image that is displayed on a button element when it is clicked.")]
    [Category("Appearance")]
    [Localizable(true)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [RadPropertyDefaultValue("ImageClicked", typeof (RadButtonItem))]
    [Editor("Telerik.WinControls.UI.Design.RadImageTypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual Image ImageClicked
    {
      get
      {
        return (Image) this.GetValue(RadImageButtonElement.ImageClickedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageButtonElement.ImageClickedProperty, (object) value);
      }
    }

    [Description("Gets or sets the image list index value of the image displayed on the button control when it is clicked.")]
    [RadPropertyDefaultValue("ImageIndexClicked", typeof (RadButtonItem))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public virtual int ImageIndexClicked
    {
      get
      {
        return (int) this.GetValue(RadImageButtonElement.ImageIndexClickedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageButtonElement.ImageIndexClickedProperty, (object) value);
      }
    }

    [RelatedImageList("ElementTree.Control.ImageList")]
    [RadPropertyDefaultValue("ImageKeyClicked", typeof (RadButtonItem))]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Gets or sets the key accessor for the image for clicked state in the ImageList.")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Localizable(true)]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public virtual string ImageKeyClicked
    {
      get
      {
        return (string) this.GetValue(RadImageButtonElement.ImageKeyClickedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadImageButtonElement.ImageKeyClickedProperty, (object) value);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DisplayStyle = DisplayStyle.Image;
      this.ImageAlignment = ContentAlignment.MiddleCenter;
    }

    private void EnsureNormalStateImageCached()
    {
      if (this.normalStateImage != null)
        return;
      this.normalStateSource = this.GetValueSource(RadButtonItem.ImageProperty);
      this.normalStateImage = this.Image;
    }

    private void SetNewStateImage(Image newImage, bool isFromImageHoveredOrImageClicked)
    {
      this.chaingingImageInternally = true;
      if (newImage != null)
      {
        if (isFromImageHoveredOrImageClicked)
          this.Image = newImage;
        else if (this.normalStateSource < ValueSource.Local)
        {
          int num = (int) this.ResetValue(RadButtonItem.ImageProperty, ValueResetFlags.Local);
          new PropertySetting(RadButtonItem.ImageProperty, (object) newImage).ApplyValue((RadObject) this);
        }
        else
          this.Image = newImage;
      }
      this.chaingingImageInternally = false;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
      this.SetNewStateImage(this.normalStateImage, false);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.IsMouseOverProperty)
      {
        this.EnsureNormalStateImageCached();
        this.SetImageIfHovered((bool) e.NewValue);
      }
      else if (e.Property == RadElement.IsMouseDownProperty)
      {
        this.EnsureNormalStateImageCached();
        if ((bool) e.NewValue)
        {
          this.SetNewStateImage(this.ImageClicked, true);
          if (this.ImageIndexClicked != -1)
            this.ImageIndex = this.ImageIndexClicked;
          this.isPreviousStateClicked = true;
        }
        else
          this.SetImageIfHovered(this.IsMouseOver);
      }
      else
      {
        if (e.Property != RadButtonItem.ImageProperty || this.chaingingImageInternally)
          return;
        this.normalStateImage = (Image) null;
        this.EnsureNormalStateImageCached();
      }
    }

    private void SetImageIfHovered(bool hovered)
    {
      if (hovered)
      {
        this.SetNewStateImage(this.ImageHovered, true);
        if (this.ImageIndexHovered == -1)
          return;
        if (!this.isPreviousStateClicked)
          this.cachedImageIndex = this.ImageIndex;
        this.isPreviousStateClicked = false;
        this.ImageIndex = this.ImageIndexHovered;
      }
      else
      {
        this.SetNewStateImage(this.normalStateImage, false);
        this.ImageIndex = this.cachedImageIndex;
      }
    }

    [DefaultValue(DisplayStyle.Image)]
    public override DisplayStyle DisplayStyle
    {
      get
      {
        return base.DisplayStyle;
      }
      set
      {
        base.DisplayStyle = value;
      }
    }
  }
}
