// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuContentItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public class RadMenuContentItem : RadMenuItemBase
  {
    public static RadProperty ShowImageOffsetProperty = RadProperty.Register(nameof (ShowImageOffset), typeof (bool), typeof (RadMenuContentItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private ImagePrimitive imagePrimitive;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private RadElement contentElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (RadMenuContentItem);
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("ShowImageOffset", typeof (RadMenuContentItem))]
    [Browsable(true)]
    [Description("Gets or sets if the image is shown along with content element or not.")]
    public virtual bool ShowImageOffset
    {
      get
      {
        return (bool) this.GetValue(RadMenuContentItem.ShowImageOffsetProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuContentItem.ShowImageOffsetProperty, (object) value);
      }
    }

    [Browsable(true)]
    [Description("Provides a reference to the content element in the menu item.")]
    [Category("Behavior")]
    public RadElement ContentElement
    {
      get
      {
        return this.contentElement;
      }
      set
      {
        if (this.contentElement == value)
          return;
        if (this.contentElement != null)
          this.Children.Remove(this.contentElement);
        this.contentElement = value;
        this.Children.Add(this.contentElement);
        this.contentElement.NotifyParentOnMouseInput = true;
      }
    }

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.BackColor = Color.Empty;
      this.fillPrimitive.GradientStyle = GradientStyles.Solid;
      this.fillPrimitive.Class = "RadMenuItemFillPrimitive";
      this.fillPrimitive.Name = "MenuContentItemFill";
      this.Children.Add((RadElement) this.fillPrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "RadMenuItemBorderPrimitive";
      this.borderPrimitive.Name = "MenuContentItemBorder";
      int num1 = (int) this.borderPrimitive.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.imagePrimitive = new ImagePrimitive();
      int num2 = (int) this.imagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      int num3 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadButtonItem.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadButtonItem.ImageProperty, PropertyBindingOptions.TwoWay);
      int num5 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadButtonItem.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      this.imagePrimitive.Class = "RadMenuItemImagePrimitive";
      this.Children.Add((RadElement) this.imagePrimitive);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RadDropDownMenuLayout parent = this.Parent as RadDropDownMenuLayout;
      int num1 = 0;
      if (parent != null)
        num1 = (int) parent.LeftColumnWidth;
      foreach (RadElement child in this.Children)
      {
        if (child == this.imagePrimitive)
        {
          RectangleF rectangleF = new RectangleF(clientRectangle.Left, clientRectangle.Top, (float) num1, clientRectangle.Height);
          if (this.RightToLeft)
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, clientRectangle);
          child.Arrange(rectangleF);
        }
        else if (child == this.contentElement)
        {
          int num2 = 0;
          int num3 = 0;
          if (parent != null)
          {
            num2 = (int) parent.LeftColumnMaxPadding;
            num3 = (int) parent.RightColumnWidth;
          }
          RectangleF rectangleF = new RectangleF(clientRectangle.Left + (float) num2 + (float) num1, clientRectangle.Top, clientRectangle.Width - (float) (num2 + num3), clientRectangle.Height);
          if (this.RightToLeft)
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, clientRectangle);
          child.Arrange(rectangleF);
        }
        else if (child.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
          child.Arrange(new RectangleF((PointF) Point.Empty, finalSize));
        else if (child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
          child.Arrange(new RectangleF((float) this.BorderThickness.Left, (float) this.BorderThickness.Top, finalSize.Width - (float) this.BorderThickness.Horizontal, finalSize.Height - (float) this.BorderThickness.Vertical));
        else
          child.Arrange(clientRectangle);
      }
      return finalSize;
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "Class")
        return new bool?(this.Class != nameof (RadMenuContentItem));
      return base.ShouldSerializeProperty(property);
    }
  }
}
