// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuSeparatorItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadMenuSeparatorItem : RadMenuItemBase
  {
    public static RadProperty SweepAngleProperty = RadProperty.Register(nameof (SweepAngle), typeof (int), typeof (RadMenuSeparatorItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OrientationProperty = RadProperty.Register(nameof (SeparatorOrientation), typeof (SepOrientation), typeof (RadMenuSeparatorItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SepOrientation.Horizontal, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineWidthProperty = RadProperty.Register(nameof (LineWidth), typeof (int), typeof (RadMenuSeparatorItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineOffsetProperty = RadProperty.Register(nameof (LineOffset), typeof (float), typeof (RadMenuSeparatorItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 3f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowFillProperty = RadProperty.Register("ShowFill", typeof (bool), typeof (RadMenuSeparatorItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextVisibilityProperty = RadProperty.Register(nameof (TextVisibility), typeof (ElementVisibility), typeof (RadMenuSeparatorItem), new RadPropertyMetadata((object) ElementVisibility.Collapsed));
    private LinePrimitive linePrimitive;
    private LightVisualElement text;
    private float cachedLineOffset;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.TextVisibility = ElementVisibility.Collapsed;
      this.Class = nameof (RadMenuSeparatorItem);
    }

    [DefaultValue(0)]
    public virtual int SweepAngle
    {
      get
      {
        return (int) this.GetValue(RadMenuSeparatorItem.SweepAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuSeparatorItem.SweepAngleProperty, (object) value);
      }
    }

    public virtual SepOrientation SeparatorOrientation
    {
      get
      {
        return (SepOrientation) this.GetValue(RadMenuSeparatorItem.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuSeparatorItem.OrientationProperty, (object) value);
      }
    }

    public virtual int LineWidth
    {
      get
      {
        return (int) this.GetValue(RadMenuSeparatorItem.LineWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuSeparatorItem.LineWidthProperty, (object) value);
      }
    }

    public virtual float LineOffset
    {
      get
      {
        return this.cachedLineOffset;
      }
      set
      {
        this.cachedLineOffset = value;
        int num = (int) this.SetValue(RadMenuSeparatorItem.LineOffsetProperty, (object) value);
      }
    }

    public override bool Selectable
    {
      get
      {
        return false;
      }
    }

    [DefaultValue(ElementVisibility.Collapsed)]
    public ElementVisibility TextVisibility
    {
      get
      {
        return (ElementVisibility) this.GetValue(RadMenuSeparatorItem.TextVisibilityProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuSeparatorItem.TextVisibilityProperty, (object) value);
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadMenuSeparatorItem.ShowFillProperty)
        return;
      this.linePrimitive.Visibility = (bool) e.NewValue ? ElementVisibility.Visible : ElementVisibility.Hidden;
    }

    protected override void CreateChildElements()
    {
      this.linePrimitive = new LinePrimitive();
      this.linePrimitive.Class = "LineFill";
      int num1 = (int) this.linePrimitive.SetDefaultValueOverride(RadElement.MarginProperty, (object) new Padding(0, 2, 0, 2));
      int num2 = (int) this.linePrimitive.BindProperty(LinePrimitive.SweepAngleProperty, (RadObject) this, RadMenuSeparatorItem.SweepAngleProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.linePrimitive.BindProperty(LinePrimitive.LineWidthProperty, (RadObject) this, RadMenuSeparatorItem.LineWidthProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.linePrimitive.BindProperty(LinePrimitive.OrientationProperty, (RadObject) this, RadMenuSeparatorItem.OrientationProperty, PropertyBindingOptions.TwoWay);
      this.Children.Add((RadElement) this.linePrimitive);
      this.text = new LightVisualElement();
      this.text.Class = "TextElement";
      int num5 = (int) this.text.BindProperty(RadItem.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num6 = (int) this.text.BindProperty(RadElement.VisibilityProperty, (RadObject) this, RadMenuSeparatorItem.TextVisibilityProperty, PropertyBindingOptions.TwoWay);
      int num7 = (int) this.text.BindProperty(RadElement.AlignmentProperty, (RadObject) this, RadButtonItem.TextAlignmentProperty, PropertyBindingOptions.TwoWay);
      this.text.Alignment = ContentAlignment.MiddleLeft;
      this.text.DrawFill = true;
      this.text.StretchHorizontally = false;
      this.text.StretchVertically = false;
      this.Children.Add((RadElement) this.text);
      this.cachedLineOffset = (float) this.GetValue(RadMenuSeparatorItem.LineOffsetProperty);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Height = Math.Max(this.text.DesiredSize.Height, sizeF.Height);
      return sizeF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      RadDropDownMenuLayout ancestor = this.FindAncestor<RadDropDownMenuLayout>();
      if (ancestor != null && this.linePrimitive != null)
      {
        RectangleF finalRect = new RectangleF(ancestor.LeftColumnWidth + ancestor.LeftColumnMaxPadding + this.cachedLineOffset, (float) ((double) finalSize.Height / 2.0 - (double) this.linePrimitive.DesiredSize.Height / 2.0), finalSize.Width - (ancestor.LeftColumnWidth + ancestor.LeftColumnMaxPadding), finalSize.Height);
        if (this.RightToLeft)
          finalRect.X = finalSize.Width - finalRect.X - finalRect.Width;
        this.linePrimitive.Arrange(finalRect);
        finalRect = new RectangleF(finalRect.X, (float) ((double) finalSize.Height / 2.0 - (double) this.text.DesiredSize.Height / 2.0), finalRect.Width, finalRect.Height);
        this.text.Arrange(finalRect);
      }
      return finalSize;
    }

    public override bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      if (property.Name == "Class")
        return new bool?(this.Class != nameof (RadMenuSeparatorItem));
      return base.ShouldSerializeProperty(property);
    }
  }
}
