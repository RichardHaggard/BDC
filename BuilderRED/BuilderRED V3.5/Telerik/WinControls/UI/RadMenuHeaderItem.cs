// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuHeaderItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadMenuHeaderItem : RadMenuItemBase
  {
    private ImagePrimitive imagePrimitive;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private TextPrimitive textPrimitive;

    public RadMenuHeaderItem()
      : this("")
    {
    }

    public RadMenuHeaderItem(string text)
    {
      this.Text = text;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = nameof (RadMenuHeaderItem);
      this.ShouldHandleMouseInput = false;
    }

    public override bool Selectable
    {
      get
      {
        return false;
      }
    }

    [RelatedImageList("MenuElement.ElementTree.Control.ImageList")]
    [Category("Appearance")]
    [Description("Gets or sets the index value of the image that is displayed on the item.")]
    [Browsable(true)]
    public override int ImageIndex
    {
      get
      {
        return base.ImageIndex;
      }
      set
      {
        base.ImageIndex = value;
      }
    }

    [Browsable(true)]
    [RelatedImageList("MenuElement.ElementTree.Control.ImageList")]
    [Category("Appearance")]
    [Description("Gets or sets the key accessor for the image in the ImageList.")]
    public override string ImageKey
    {
      get
      {
        return base.ImageKey;
      }
      set
      {
        base.ImageKey = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the ImagePrimitive of this RadMenuHeaderItem.")]
    public ImagePrimitive ImagePrimitive
    {
      get
      {
        return this.imagePrimitive;
      }
    }

    [Description("Gets the FillPrimitive of this RadMenuHeaderItem.")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public FillPrimitive FillPrimitive
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Description("Gets the BorderPrimitive of this RadMenuHeaderItem.")]
    public BorderPrimitive BorderPrimitive
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    [Description("Gets the TextPrimitive of this RadMenuHeaderItem.")]
    public TextPrimitive TextPrimitive
    {
      get
      {
        return this.textPrimitive;
      }
    }

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "RadMenuHeaderItemFillPrimitive";
      this.fillPrimitive.BackColor = Color.Empty;
      this.fillPrimitive.GradientStyle = GradientStyles.Solid;
      this.fillPrimitive.Name = "MenuHeaderItemFill";
      this.Children.Add((RadElement) this.fillPrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "RadMenuHeaderItemBorderPrimitive";
      this.borderPrimitive.Name = "MenuHeaderItemBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.imagePrimitive = new ImagePrimitive();
      int num1 = (int) this.imagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      int num2 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageIndexProperty, (RadObject) this, RadButtonItem.ImageIndexProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageProperty, (RadObject) this, RadButtonItem.ImageProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.imagePrimitive.BindProperty(ImagePrimitive.ImageKeyProperty, (RadObject) this, RadButtonItem.ImageKeyProperty, PropertyBindingOptions.TwoWay);
      this.imagePrimitive.Class = "RadMenuHeaderItemImagePrimitive";
      this.Children.Add((RadElement) this.imagePrimitive);
      this.textPrimitive = new TextPrimitive();
      this.textPrimitive.Class = "RadMenuHeaderItemText";
      this.Children.Add((RadElement) this.textPrimitive);
      int num5 = (int) this.textPrimitive.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num6 = (int) this.textPrimitive.BindProperty(VisualElement.FontProperty, (RadObject) this, VisualElement.FontProperty, PropertyBindingOptions.TwoWay);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RadDropDownMenuLayout parent = this.Parent as RadDropDownMenuLayout;
      float num1 = 0.0f;
      float num2 = 0.0f;
      if (parent != null)
      {
        num1 = parent.LeftColumnMaxPadding + parent.LeftColumnWidth;
        num2 = parent.LeftColumnMaxPadding + parent.RightColumnWidth;
      }
      foreach (RadElement child in this.Children)
      {
        if (child == this.imagePrimitive)
        {
          float width = parent != null ? parent.LeftColumnWidth : 0.0f;
          RectangleF rectangleF = new RectangleF(clientRectangle.Left, clientRectangle.Top, width, clientRectangle.Height);
          if (this.RightToLeft)
            rectangleF = LayoutUtils.RTLTranslateNonRelative(rectangleF, clientRectangle);
          child.Arrange(rectangleF);
        }
        else if (child == this.textPrimitive)
        {
          RectangleF rectangleF = LayoutUtils.Align(this.textPrimitive.DesiredSize, new RectangleF(clientRectangle.Left + num1, clientRectangle.Top, clientRectangle.Width - num2, clientRectangle.Height), this.textPrimitive.Alignment);
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
        return new bool?(this.Class != nameof (RadMenuHeaderItem));
      return base.ShouldSerializeProperty(property);
    }
  }
}
