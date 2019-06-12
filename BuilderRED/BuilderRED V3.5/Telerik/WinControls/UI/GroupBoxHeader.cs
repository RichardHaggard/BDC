// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupBoxHeader
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class GroupBoxHeader : GroupBoxVisualElement
  {
    public static RadProperty HeaderPositionProperty = RadProperty.Register(nameof (HeaderPosition), typeof (HeaderPosition), typeof (GroupBoxHeader), (RadPropertyMetadata) new RadElementPropertyMetadata((object) HeaderPosition.Top, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GroupBoxStyleProperty = RadProperty.Register(nameof (GroupBoxStyle), typeof (RadGroupBoxStyle), typeof (GroupBoxHeader), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadGroupBoxStyle.Standard, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    private TextPrimitive textPrimitive;
    private ImagePrimitive imagePrimitive;
    private ImageAndTextLayoutPanel imageAndTextLayout;

    public HeaderPosition HeaderPosition
    {
      get
      {
        return (HeaderPosition) this.GetValue(GroupBoxHeader.HeaderPositionProperty);
      }
      set
      {
        if ((HeaderPosition) this.GetValue(GroupBoxHeader.HeaderPositionProperty) == value)
          return;
        switch (value)
        {
          case HeaderPosition.Left:
            this.AngleTransform = -90f;
            break;
          case HeaderPosition.Top:
            this.AngleTransform = 0.0f;
            break;
          case HeaderPosition.Right:
            this.AngleTransform = 90f;
            break;
          case HeaderPosition.Bottom:
            this.AngleTransform = 0.0f;
            break;
        }
        int num = (int) this.SetValue(GroupBoxHeader.HeaderPositionProperty, (object) value);
        ++((RadGroupBoxElement) this.Parent).InvalidateMeasureInMainLayout;
      }
    }

    public RadGroupBoxStyle GroupBoxStyle
    {
      get
      {
        return (RadGroupBoxStyle) this.GetValue(GroupBoxHeader.GroupBoxStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GroupBoxHeader.GroupBoxStyleProperty, (object) value);
      }
    }

    public TextPrimitive TextPrimitive
    {
      get
      {
        return this.textPrimitive;
      }
      set
      {
        this.textPrimitive = value;
      }
    }

    public ImagePrimitive ImagePrimitive
    {
      get
      {
        return this.imagePrimitive;
      }
      set
      {
        this.imagePrimitive = value;
      }
    }

    public ImageAndTextLayoutPanel ImageAndTextLayout
    {
      get
      {
        return this.imageAndTextLayout;
      }
      set
      {
        this.imageAndTextLayout = value;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.textPrimitive = new TextPrimitive();
      this.imagePrimitive = new ImagePrimitive();
      this.imageAndTextLayout = new ImageAndTextLayoutPanel();
      int num1 = (int) this.BindProperty(RadItem.TextProperty, (RadObject) this.textPrimitive, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.textPrimitive.SetValue(ImageAndTextLayoutPanel.IsTextPrimitiveProperty, (object) true);
      int num3 = (int) this.imagePrimitive.SetValue(ImageAndTextLayoutPanel.IsImagePrimitiveProperty, (object) true);
      this.imageAndTextLayout.Children.Add((RadElement) this.imagePrimitive);
      this.imageAndTextLayout.Children.Add((RadElement) this.textPrimitive);
      this.Children.Add((RadElement) this.imageAndTextLayout);
      this.Class = nameof (GroupBoxHeader);
    }

    public override string Text
    {
      get
      {
        return this.textPrimitive.Text;
      }
      set
      {
        if (!(this.textPrimitive.Text != value))
          return;
        this.textPrimitive.Text = value;
      }
    }

    public override string ToString()
    {
      return nameof (GroupBoxHeader);
    }

    public override bool RightToLeft
    {
      get
      {
        return base.RightToLeft;
      }
      set
      {
        base.RightToLeft = value;
        this.imageAndTextLayout.RightToLeft = value;
      }
    }

    private Padding GetBorderThickness(bool checkDrawBorder)
    {
      if (checkDrawBorder && this.borderPrimitive.Visibility == ElementVisibility.Collapsed)
        return Padding.Empty;
      Padding padding = Padding.Empty;
      if (this.borderPrimitive.BoxStyle == BorderBoxStyle.SingleBorder)
        padding = new Padding((int) this.borderPrimitive.Width);
      else if (this.borderPrimitive.BoxStyle == BorderBoxStyle.FourBorders)
        padding = new Padding((int) this.borderPrimitive.LeftWidth, (int) this.borderPrimitive.TopWidth, (int) this.borderPrimitive.RightWidth, (int) this.borderPrimitive.BottomWidth);
      else if (this.borderPrimitive.BoxStyle == BorderBoxStyle.OuterInnerBorders)
      {
        int all = (int) this.borderPrimitive.Width;
        if (all == 1)
          all = 2;
        padding = new Padding(all);
      }
      return padding;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      SizeF empty = SizeF.Empty;
      Padding borderThickness = this.GetBorderThickness(true);
      empty.Width = this.imageAndTextLayout.DesiredSize.Width + (float) borderThickness.Horizontal;
      empty.Height = this.imageAndTextLayout.DesiredSize.Height + (float) borderThickness.Vertical;
      if (this.GroupBoxStyle == RadGroupBoxStyle.Office)
        empty.Width = availableSize.Width;
      return empty;
    }
  }
}
