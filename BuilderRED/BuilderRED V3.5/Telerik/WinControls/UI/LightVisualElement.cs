// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LightVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.TextPrimitiveUtils;

namespace Telerik.WinControls.UI
{
  public class LightVisualElement : UIItemBase, IImageElement, ITextPrimitive, ITextProvider
  {
    private FormattedTextBlock textBlock = new FormattedTextBlock();
    public static RadProperty BackgroundImageProperty = RadProperty.Register(nameof (BackgroundImage), typeof (Image), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BackgroundImageLayoutProperty = RadProperty.Register(nameof (BackgroundImageLayout), typeof (ImageLayout), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ImageLayout.Center, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BackColor2Property = RadProperty.Register(nameof (BackColor2), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.Control, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BackColor3Property = RadProperty.Register(nameof (BackColor3), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BackColor4Property = RadProperty.Register(nameof (BackColor4), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlLightLight, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderBottomColorProperty = RadProperty.Register(nameof (BorderBottomColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderBottomShadowColorProperty = RadProperty.Register(nameof (BorderBottomShadowColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Empty, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderBottomWidthProperty = RadProperty.Register(nameof (BorderBottomWidth), typeof (float), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderBoxStyleProperty = RadProperty.Register(nameof (BorderBoxStyle), typeof (BorderBoxStyle), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) BorderBoxStyle.SingleBorder, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderColorProperty = RadProperty.Register(nameof (BorderColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDarkDark, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderColor2Property = RadProperty.Register(nameof (BorderColor2), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderColor3Property = RadProperty.Register(nameof (BorderColor3), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderColor4Property = RadProperty.Register(nameof (BorderColor4), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderDashStyleProperty = RadProperty.Register(nameof (BorderDashStyle), typeof (DashStyle), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DashStyle.Solid, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderDashPatternProperty = RadProperty.Register(nameof (BorderDashPattern), typeof (float[]), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderDrawModeProperty = RadProperty.Register(nameof (BorderDrawMode), typeof (BorderDrawModes), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) BorderDrawModes.RightOverTop, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty BorderGradientAngleProperty = RadProperty.Register(nameof (BorderGradientAngle), typeof (float), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 270f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderGradientStyleProperty = RadProperty.Register(nameof (BorderGradientStyle), typeof (GradientStyles), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GradientStyles.Linear, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderInnerColorProperty = RadProperty.Register(nameof (BorderInnerColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlLightLight, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderInnerColor2Property = RadProperty.Register(nameof (BorderInnerColor2), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.Control, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderInnerColor3Property = RadProperty.Register(nameof (BorderInnerColor3), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderInnerColor4Property = RadProperty.Register(nameof (BorderInnerColor4), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDarkDark, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderLeftColorProperty = RadProperty.Register(nameof (BorderLeftColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderLeftShadowColorProperty = RadProperty.Register(nameof (BorderLeftShadowColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Empty, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderLeftWidthProperty = RadProperty.Register(nameof (BorderLeftWidth), typeof (float), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderRightColorProperty = RadProperty.Register(nameof (BorderRightColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderRightShadowColorProperty = RadProperty.Register(nameof (BorderRightShadowColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Empty, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderRightWidthProperty = RadProperty.Register(nameof (BorderRightWidth), typeof (float), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderTopColorProperty = RadProperty.Register(nameof (BorderTopColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderTopShadowColorProperty = RadProperty.Register(nameof (BorderTopShadowColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.Empty, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderTopWidthProperty = RadProperty.Register(nameof (BorderTopWidth), typeof (float), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BorderWidthProperty = RadProperty.Register(nameof (BorderWidth), typeof (float), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ClipTextProperty = RadProperty.Register(nameof (ClipText), typeof (bool), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DisabledTextRenderingHintProperty = RadProperty.Register(nameof (DisabledTextRenderingHint), typeof (TextRenderingHint), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextRenderingHint.AntiAliasGridFit, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DrawBorderProperty = RadProperty.Register(nameof (DrawBorder), typeof (bool), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DrawFillProperty = RadProperty.Register(nameof (DrawFill), typeof (bool), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DrawTextProperty = RadProperty.Register(nameof (DrawText), typeof (bool), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DrawBackgroundImageProperty = RadProperty.Register(nameof (DrawBackgroundImage), typeof (bool), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DrawImageProperty = RadProperty.Register(nameof (DrawImage), typeof (bool), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty EnableImageTransparencyProperty = RadProperty.Register(nameof (EnableImageTransparency), typeof (bool), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientAngleProperty = RadProperty.Register(nameof (GradientAngle), typeof (float), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientPercentageProperty = RadProperty.Register(nameof (GradientPercentage), typeof (float), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.5f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientPercentage2Property = RadProperty.Register(nameof (GradientPercentage2), typeof (float), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.666f, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty GradientStyleProperty = RadProperty.Register(nameof (GradientStyle), typeof (GradientStyles), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GradientStyles.Linear, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HorizontalLineColorProperty = RadProperty.Register(nameof (HorizontalLineColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.Control, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HorizontalLineWidthProperty = RadProperty.Register(nameof (HorizontalLineWidth), typeof (int), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageAlignmentProperty = RadProperty.Register(nameof (ImageAlignment), typeof (ContentAlignment), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleCenter, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageIndexProperty = RadProperty.Register(nameof (ImageIndex), typeof (int), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) -1, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageKeyProperty = RadProperty.Register(nameof (ImageKey), typeof (string), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageLayoutProperty = RadProperty.Register(nameof (ImageLayout), typeof (ImageLayout), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ImageLayout.Center, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageOpacityProperty = RadProperty.Register(nameof (ImageOpacity), typeof (double), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1.0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageProperty = RadProperty.Register(nameof (Image), typeof (Image), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ImageTransparentColorProperty = RadProperty.Register(nameof (ImageTransparentColor), typeof (Color), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue), ElementPropertyOptions.AffectsDisplay));
    public static RadProperty NumberOfColorsProperty = RadProperty.Register(nameof (NumberOfColors), typeof (int), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowHorizontalLineProperty = RadProperty.Register(nameof (ShowHorizontalLine), typeof (bool), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextAlignmentProperty = RadProperty.Register(nameof (TextAlignment), typeof (ContentAlignment), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleCenter, ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextImageRelationProperty = RadProperty.Register(nameof (TextImageRelation), typeof (TextImageRelation), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextImageRelation.Overlay, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextWrapProperty = RadProperty.Register(nameof (TextWrap), typeof (bool), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextRenderingHintProperty = RadProperty.Register(nameof (TextRenderingHint), typeof (TextRenderingHint), typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextRenderingHint.SystemDefault, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    internal const long CurrentlyAnimatingStateKey = 8796093022208;
    internal const long CurrentlyAnimatingBackGroundImageStateKey = 17592186044416;
    internal const long DisableHTMLRenderingStateKey = 35184372088832;
    internal const long textWrapStateKey = 70368744177664;
    internal const long showKeyboardCuesStateKey = 140737488355328;
    internal const long measureTrailingSpacesStateKey = 281474976710656;
    internal const long useMnemonicStateKey = 562949953421312;
    internal const long autoEllipsisStateKey = 1125899906842624;
    internal const long LightVisualElementLastStateKey = 1125899906842624;
    private static readonly Dictionary<RadProperty, RadProperty> mappedPrimitiveProperties;
    private Image cachedImage;
    private Image cachedBackgroundImage;
    private ITextPrimitive textPrimitiveImpl;
    private ImagePrimitiveImpl imagePrimitiveImpl;
    private LayoutManagerPart layoutManagerPart;
    private TextPart textElement;
    private ImagePart imageElement;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static Padding GetBorderThickness(
      LightVisualElement element,
      bool checkDrawBorder)
    {
      if (checkDrawBorder && !element.DrawBorder)
        return Padding.Empty;
      Padding padding = Padding.Empty;
      if (element.BorderBoxStyle == BorderBoxStyle.SingleBorder)
        padding = new Padding((int) element.BorderWidth);
      else if (element.BorderBoxStyle == BorderBoxStyle.FourBorders)
        padding = new Padding((int) element.BorderLeftWidth, (int) element.BorderTopWidth, (int) element.BorderRightWidth, (int) element.BorderBottomWidth);
      else if (element.BorderBoxStyle == BorderBoxStyle.OuterInnerBorders)
      {
        int all = (int) element.BorderWidth;
        if (all == 1)
          all = 2;
        padding = new Padding(all);
      }
      return padding;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.CanFocus = true;
      this.ShouldPaint = true;
      this.imageElement = new ImagePart(this);
      this.imagePrimitiveImpl = new ImagePrimitiveImpl((IImageElement) this);
      this.layoutManagerPart = new LayoutManagerPart(this);
      this.layoutManagerPart.LeftPart = (IVisualLayoutPart) this.imageElement;
      this.textElement = new TextPart(this);
      this.textPrimitiveImpl = (ITextPrimitive) new TextPrimitiveImpl();
      this.layoutManagerPart.RightPart = (IVisualLayoutPart) this.textElement;
    }

    static LightVisualElement()
    {
      RadTypeResolver.Instance.RegisterKnownType("Telerik.WinControls.UI.LightVisualElement", typeof (LightVisualElement));
      RadItem.TextProperty.OverrideMetadata(typeof (LightVisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
      LightVisualElement.mappedPrimitiveProperties = new Dictionary<RadProperty, RadProperty>(20);
      LightVisualElement.AddFillPrimitiveProperties();
      LightVisualElement.AddBorderPrimitiveProperties();
      LightVisualElement.AddImagePrimitiveProperties();
    }

    internal static Dictionary<RadProperty, RadProperty> PropertiesForMapping
    {
      get
      {
        return LightVisualElement.mappedPrimitiveProperties;
      }
    }

    private static void AddImagePrimitiveProperties()
    {
      LightVisualElement.mappedPrimitiveProperties.Add(ImagePrimitive.ImageProperty, LightVisualElement.ImageProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(ImagePrimitive.ImageLayoutProperty, LightVisualElement.ImageLayoutProperty);
    }

    private static void AddFillPrimitiveProperties()
    {
      LightVisualElement.mappedPrimitiveProperties.Add(FillPrimitive.BackColor2Property, LightVisualElement.BackColor2Property);
      LightVisualElement.mappedPrimitiveProperties.Add(FillPrimitive.BackColor3Property, LightVisualElement.BackColor3Property);
      LightVisualElement.mappedPrimitiveProperties.Add(FillPrimitive.BackColor4Property, LightVisualElement.BackColor4Property);
      LightVisualElement.mappedPrimitiveProperties.Add(FillPrimitive.NumberOfColorsProperty, LightVisualElement.NumberOfColorsProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(FillPrimitive.GradientStyleProperty, LightVisualElement.GradientStyleProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(FillPrimitive.GradientAngleProperty, LightVisualElement.GradientAngleProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(FillPrimitive.GradientPercentageProperty, LightVisualElement.GradientPercentageProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(FillPrimitive.GradientPercentage2Property, LightVisualElement.GradientPercentage2Property);
    }

    private static void AddBorderPrimitiveProperties()
    {
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.BorderBoxStyleProperty, LightVisualElement.BorderBoxStyleProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.BorderDrawModeProperty, LightVisualElement.BorderDrawModeProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.WidthProperty, LightVisualElement.BorderWidthProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.LeftWidthProperty, LightVisualElement.BorderLeftWidthProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.TopWidthProperty, LightVisualElement.BorderTopWidthProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.RightWidthProperty, LightVisualElement.BorderRightWidthProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.BottomWidthProperty, LightVisualElement.BorderBottomWidthProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.LeftColorProperty, LightVisualElement.BorderLeftColorProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.TopColorProperty, LightVisualElement.BorderTopColorProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.RightColorProperty, LightVisualElement.BorderRightColorProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.BottomColorProperty, LightVisualElement.BorderBottomColorProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.LeftShadowColorProperty, LightVisualElement.BorderLeftShadowColorProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.TopShadowColorProperty, LightVisualElement.BorderTopShadowColorProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.RightShadowColorProperty, LightVisualElement.BorderRightShadowColorProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.BottomShadowColorProperty, LightVisualElement.BorderBottomShadowColorProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.GradientAngleProperty, LightVisualElement.BorderGradientAngleProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.GradientStyleProperty, LightVisualElement.BorderGradientStyleProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.ForeColor2Property, LightVisualElement.BorderColor2Property);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.ForeColor3Property, LightVisualElement.BorderColor3Property);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.ForeColor4Property, LightVisualElement.BorderColor4Property);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.InnerColorProperty, LightVisualElement.BorderInnerColorProperty);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.InnerColor2Property, LightVisualElement.BorderInnerColor2Property);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.InnerColor3Property, LightVisualElement.BorderInnerColor3Property);
      LightVisualElement.mappedPrimitiveProperties.Add(BorderPrimitive.InnerColor4Property, LightVisualElement.BorderInnerColor4Property);
    }

    [Description("Graphics text-rendering mode to be used for painting text of the element")]
    [RadPropertyDefaultValue("TextRenderingHint", typeof (VisualElement))]
    [Category("Appearance")]
    public virtual TextRenderingHint TextRenderingHint
    {
      get
      {
        return (TextRenderingHint) this.GetValue(LightVisualElement.TextRenderingHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.TextRenderingHintProperty, (object) value);
      }
    }

    [Description("Graphics text-rendering mode to be used for painting text of the element in disabled mode.")]
    [RadPropertyDefaultValue("TextRenderingHint", typeof (VisualElement))]
    [Category("Appearance")]
    public virtual TextRenderingHint DisabledTextRenderingHint
    {
      get
      {
        return (TextRenderingHint) this.GetValue(LightVisualElement.DisabledTextRenderingHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.DisabledTextRenderingHintProperty, (object) value);
      }
    }

    [DefaultValue(true)]
    public override bool ShouldPaint
    {
      get
      {
        return base.ShouldPaint;
      }
      set
      {
        base.ShouldPaint = value;
      }
    }

    [DefaultValue(true)]
    public override bool CanFocus
    {
      get
      {
        return base.CanFocus;
      }
      set
      {
        base.CanFocus = value;
      }
    }

    [Description("Indicates whether the element should paint its text")]
    [Category("Appearance")]
    [RadPropertyDefaultValue("DrawText", typeof (LightVisualElement))]
    public virtual bool DrawText
    {
      get
      {
        return (bool) this.GetValue(LightVisualElement.DrawTextProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.DrawTextProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("DrawFill", typeof (LightVisualElement))]
    [Description("Indicates whether the element should paint its background")]
    public override bool DrawFill
    {
      get
      {
        return (bool) this.GetValue(LightVisualElement.DrawFillProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.DrawFillProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("DrawBorder", typeof (LightVisualElement))]
    [Description("Indicates whether the element should paint its border")]
    [Category("Appearance")]
    public override bool DrawBorder
    {
      get
      {
        return (bool) this.GetValue(LightVisualElement.DrawBorderProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.DrawBorderProperty, (object) value);
      }
    }

    [Description("Indicates whether the element should paint its background image.")]
    [Category("Appearance")]
    [RadPropertyDefaultValue("DrawBackgroundImage", typeof (LightVisualElement))]
    public bool DrawBackgroundImage
    {
      get
      {
        return (bool) this.GetValue(LightVisualElement.DrawBackgroundImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.DrawBackgroundImageProperty, (object) value);
      }
    }

    [Description("Indicates whether the element should paint its image.")]
    [Category("Appearance")]
    [RadPropertyDefaultValue("DrawImage", typeof (LightVisualElement))]
    public bool DrawImage
    {
      get
      {
        return (bool) this.GetValue(LightVisualElement.DrawImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.DrawImageProperty, (object) value);
      }
    }

    [Category("Box")]
    [RadPropertyDefaultValue("BorderBoxStyle", typeof (LightVisualElement))]
    public override BorderBoxStyle BorderBoxStyle
    {
      get
      {
        return (BorderBoxStyle) this.GetValue(LightVisualElement.BorderBoxStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderBoxStyleProperty, (object) value);
      }
    }

    [Description("Defines the order in which border lines are drawn.This property is considered when the BorderBoxStyle is FourBorders.")]
    [Category("Box")]
    [RadPropertyDefaultValue("BorderDrawMode", typeof (LightVisualElement))]
    public override BorderDrawModes BorderDrawMode
    {
      get
      {
        return (BorderDrawModes) this.GetValue(LightVisualElement.BorderDrawModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderDrawModeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderWidth", typeof (LightVisualElement))]
    [Category("Box")]
    public override float BorderWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleFloat((float) this.GetValue(LightVisualElement.BorderWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderWidthProperty, (object) value);
      }
    }

    [Category("Box")]
    [RadPropertyDefaultValue("BorderLeftWidth", typeof (LightVisualElement))]
    public override float BorderLeftWidth
    {
      get
      {
        return (float) this.GetValue(LightVisualElement.BorderLeftWidthProperty) * this.DpiScaleFactor.Width;
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderLeftWidthProperty, (object) value);
      }
    }

    [Category("Box")]
    [RadPropertyDefaultValue("BorderTopWidth", typeof (LightVisualElement))]
    public override float BorderTopWidth
    {
      get
      {
        return (float) this.GetValue(LightVisualElement.BorderTopWidthProperty) * this.DpiScaleFactor.Height;
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderTopWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderRightWidth", typeof (LightVisualElement))]
    [Category("Box")]
    public override float BorderRightWidth
    {
      get
      {
        return (float) this.GetValue(LightVisualElement.BorderRightWidthProperty) * this.DpiScaleFactor.Width;
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderRightWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderBottomWidth", typeof (LightVisualElement))]
    [Category("Box")]
    public override float BorderBottomWidth
    {
      get
      {
        return (float) this.GetValue(LightVisualElement.BorderBottomWidthProperty) * this.DpiScaleFactor.Height;
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderBottomWidthProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderGradientAngle", typeof (LightVisualElement))]
    [Category("Appearance")]
    public override float BorderGradientAngle
    {
      get
      {
        return (float) this.GetValue(LightVisualElement.BorderGradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderGradientAngleProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("BorderGradientStyle", typeof (LightVisualElement))]
    public override GradientStyles BorderGradientStyle
    {
      get
      {
        return (GradientStyles) this.GetValue(LightVisualElement.BorderGradientStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderGradientStyleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderColor", typeof (LightVisualElement))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [Category("Appearance")]
    public override Color BorderColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderColorProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("BorderColor2", typeof (LightVisualElement))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public override Color BorderColor2
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderColor2Property, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("BorderColor3", typeof (LightVisualElement))]
    public override Color BorderColor3
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderColor3Property, (object) value);
      }
    }

    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("BorderColor4", typeof (LightVisualElement))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public override Color BorderColor4
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderColor4Property, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("BorderInnerColor", typeof (LightVisualElement))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public override Color BorderInnerColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderInnerColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderInnerColorProperty, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("BorderInnerColor2", typeof (LightVisualElement))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public override Color BorderInnerColor2
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderInnerColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderInnerColor2Property, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("BorderInnerColor3", typeof (LightVisualElement))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public override Color BorderInnerColor3
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderInnerColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderInnerColor3Property, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderInnerColor4", typeof (LightVisualElement))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public override Color BorderInnerColor4
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderInnerColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderInnerColor4Property, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("BackColor2", typeof (LightVisualElement))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public override Color BackColor2
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BackColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BackColor2Property, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("BackColor3", typeof (LightVisualElement))]
    [Category("Appearance")]
    public override Color BackColor3
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BackColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BackColor3Property, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("BackColor4", typeof (LightVisualElement))]
    public override Color BackColor4
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BackColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BackColor4Property, (object) value);
      }
    }

    [RadPropertyDefaultValue("NumberOfColors", typeof (LightVisualElement))]
    [Category("Appearance")]
    public override int NumberOfColors
    {
      get
      {
        return (int) this.GetValue(LightVisualElement.NumberOfColorsProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.NumberOfColorsProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("GradientStyle", typeof (LightVisualElement))]
    [Category("Appearance")]
    public override GradientStyles GradientStyle
    {
      get
      {
        return (GradientStyles) this.GetValue(LightVisualElement.GradientStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.GradientStyleProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("GradientAngle", typeof (LightVisualElement))]
    public override float GradientAngle
    {
      get
      {
        return (float) this.GetValue(LightVisualElement.GradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.GradientAngleProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("GradientPercentage", typeof (LightVisualElement))]
    public override float GradientPercentage
    {
      get
      {
        return (float) this.GetValue(LightVisualElement.GradientPercentageProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.GradientPercentageProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("GradientPercentage2", typeof (LightVisualElement))]
    [Category("Appearance")]
    public override float GradientPercentage2
    {
      get
      {
        return (float) this.GetValue(LightVisualElement.GradientPercentage2Property);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.GradientPercentage2Property, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("BackgroundImage", typeof (LightVisualElement))]
    [TypeConverter(typeof (ImageTypeConverter))]
    public virtual Image BackgroundImage
    {
      get
      {
        return this.cachedBackgroundImage;
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BackgroundImageProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RadPropertyDefaultValue("Image", typeof (LightVisualElement))]
    public virtual Image Image
    {
      get
      {
        return this.cachedImage;
      }
      set
      {
        this.ApplyTransparentColor(value);
        int num = (int) this.SetValue(LightVisualElement.ImageProperty, (object) value);
      }
    }

    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [RadPropertyDefaultValue("ImageIndex", typeof (LightVisualElement))]
    [Category("Appearance")]
    [RelatedImageList("ElementTree.Control.ImageList")]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public virtual int ImageIndex
    {
      get
      {
        return (int) this.GetValue(LightVisualElement.ImageIndexProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.ImageIndexProperty, (object) value);
      }
    }

    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Appearance")]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RadPropertyDefaultValue("ImageKey", typeof (LightVisualElement))]
    public virtual string ImageKey
    {
      get
      {
        return (string) this.GetValue(LightVisualElement.ImageKeyProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.ImageKeyProperty, (object) value);
      }
    }

    [DefaultValue(ImageLayout.Center)]
    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Appearance")]
    public virtual ImageLayout ImageLayout
    {
      get
      {
        return (ImageLayout) this.GetValue(LightVisualElement.ImageLayoutProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.ImageLayoutProperty, (object) value);
      }
    }

    [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue(ImageLayout.Center)]
    [Category("Appearance")]
    public virtual ImageLayout BackgroundImageLayout
    {
      get
      {
        return (ImageLayout) this.GetValue(LightVisualElement.BackgroundImageLayoutProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BackgroundImageLayoutProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ImageOpacity", typeof (LightVisualElement))]
    [Category("Appearance")]
    public virtual double ImageOpacity
    {
      get
      {
        return (double) this.GetValue(LightVisualElement.ImageOpacityProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.ImageOpacityProperty, (object) value);
      }
    }

    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Category("Appearance")]
    public virtual ContentAlignment TextAlignment
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

    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Category("Appearance")]
    public virtual ContentAlignment ImageAlignment
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

    [Category("Behavior")]
    [RadPropertyDefaultValue("TextImageRelation", typeof (LightVisualElement))]
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

    [RadPropertyDefaultValue("ShowHorizontalLine", typeof (LightVisualElement))]
    [Category("Appearance")]
    public virtual bool ShowHorizontalLine
    {
      get
      {
        return (bool) this.GetValue(LightVisualElement.ShowHorizontalLineProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.ShowHorizontalLineProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("HorizontalLineColor", typeof (LightVisualElement))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public virtual Color HorizontalLineColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.HorizontalLineColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.HorizontalLineColorProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("HorizontalLineWidth", typeof (LightVisualElement))]
    public virtual int HorizontalLineWidth
    {
      get
      {
        return (int) Math.Round((double) (int) this.GetValue(LightVisualElement.HorizontalLineWidthProperty) * (double) this.DpiScaleFactor.Height);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.HorizontalLineWidthProperty, (object) value);
      }
    }

    private StringAlignment CreateStringAlignment(ContentAlignment textAlign)
    {
      switch (textAlign)
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.BottomLeft:
          return StringAlignment.Near;
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
        case ContentAlignment.BottomCenter:
          return StringAlignment.Center;
        case ContentAlignment.TopRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.BottomRight:
          return StringAlignment.Far;
        default:
          return StringAlignment.Center;
      }
    }

    private ImageList ImageList
    {
      get
      {
        if (this.ElementTree == null)
          return (ImageList) null;
        return this.ElementTree.ComponentTreeHandler.ImageList;
      }
    }

    private bool IsImageListSet
    {
      get
      {
        bool flag1 = this.ImageIndex >= 0 || this.ImageKey != string.Empty;
        bool flag2 = this.ImageList != null;
        if (flag1)
          return flag2;
        return false;
      }
    }

    [Category("Appearance")]
    [DefaultValue(false)]
    public bool DisableHTMLRendering
    {
      get
      {
        return this.GetBitState(35184372088832L);
      }
      set
      {
        this.SetBitState(35184372088832L, value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual FormattedTextBlock TextBlock
    {
      get
      {
        return (this.textPrimitiveImpl as TextPrimitiveHtmlImpl)?.TextBlock;
      }
      set
      {
        TextPrimitiveHtmlImpl textPrimitiveImpl = this.textPrimitiveImpl as TextPrimitiveHtmlImpl;
        if (textPrimitiveImpl == null)
          return;
        textPrimitiveImpl.TextBlock = value;
      }
    }

    [Category("Box")]
    [RadPropertyDefaultValue("BorderLeftColor", typeof (LightVisualElement))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public override Color BorderLeftColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderLeftColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderLeftColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderTopColor", typeof (LightVisualElement))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Box")]
    public override Color BorderTopColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderTopColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderTopColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderRightColor", typeof (LightVisualElement))]
    [Category("Box")]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public override Color BorderRightColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderRightColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderRightColorProperty, (object) value);
      }
    }

    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [RadPropertyDefaultValue("BorderBottomColor", typeof (LightVisualElement))]
    [Category("Box")]
    public override Color BorderBottomColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderBottomColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderBottomColorProperty, (object) value);
      }
    }

    [TypeConverter(typeof (RadColorEditorConverter))]
    [Category("Box")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [RadPropertyDefaultValue("BorderLeftShadowColor", typeof (LightVisualElement))]
    public override Color BorderLeftShadowColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderLeftShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderLeftShadowColorProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("BorderTopShadowColor", typeof (LightVisualElement))]
    [Category("Box")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public override Color BorderTopShadowColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderTopShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderTopShadowColorProperty, (object) value);
      }
    }

    [Category("Box")]
    [RadPropertyDefaultValue("BorderRightShadowColor", typeof (LightVisualElement))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public override Color BorderRightShadowColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderRightShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderRightShadowColorProperty, (object) value);
      }
    }

    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [Category("Box")]
    [RadPropertyDefaultValue("BorderBottomShadowColor", typeof (LightVisualElement))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public override Color BorderBottomShadowColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.BorderBottomShadowColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderBottomShadowColorProperty, (object) value);
      }
    }

    [Description("Determines whether text will be clipped within the calculated text paint rectangle.")]
    [RadPropertyDefaultValue("ClipText", typeof (LightVisualElement))]
    [Category("Appearance")]
    public bool ClipText
    {
      get
      {
        return (bool) this.GetValue(LightVisualElement.ClipTextProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.ClipTextProperty, (object) value);
      }
    }

    public virtual LayoutManagerPart Layout
    {
      get
      {
        return this.layoutManagerPart;
      }
    }

    [Description("Transparent color to be used for the image.")]
    [Category("Image")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RadPropertyDefaultValue("ImageTransparentColor", typeof (LightVisualElement))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    public Color ImageTransparentColor
    {
      get
      {
        return (Color) this.GetValue(LightVisualElement.ImageTransparentColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.ImageTransparentColorProperty, (object) value);
        this.ApplyTransparentColor(this.Image);
      }
    }

    [Description("Specifies the style of dashed lines drawn with a border")]
    [RadPropertyDefaultValue("BorderDashStyle", typeof (LightVisualElement))]
    public override DashStyle BorderDashStyle
    {
      get
      {
        return (DashStyle) this.GetValue(LightVisualElement.BorderDashStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderDashStyleProperty, (object) value);
      }
    }

    [Description("Specifies the style of dashed lines drawn with a border")]
    [RadPropertyDefaultValue("BorderDashPattern", typeof (LightVisualElement))]
    public override float[] BorderDashPattern
    {
      get
      {
        return (float[]) this.GetValue(LightVisualElement.BorderDashPatternProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.BorderDashPatternProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating whether image transparency is supported.")]
    [RadPropertyDefaultValue("EnableImageTransparency", typeof (LightVisualElement))]
    public bool EnableImageTransparency
    {
      get
      {
        return (bool) this.GetValue(LightVisualElement.EnableImageTransparencyProperty);
      }
      set
      {
        int num = (int) this.SetValue(LightVisualElement.EnableImageTransparencyProperty, (object) value);
      }
    }

    public override RadProperty MapStyleProperty(
      RadProperty propertyToMap,
      string settingType)
    {
      if (propertyToMap == VisualElement.ForeColorProperty && settingType == "Border")
        return LightVisualElement.BorderColorProperty;
      RadProperty radProperty;
      LightVisualElement.PropertiesForMapping.TryGetValue(propertyToMap, out radProperty);
      return radProperty;
    }

    public override Telerik.WinControls.Filter GetStylablePropertiesFilter()
    {
      return (Telerik.WinControls.Filter) new OrFilter(new Telerik.WinControls.Filter[4]{ (Telerik.WinControls.Filter) new PropertyFilter(PropertyFilter.GetPropertiesDeclaredByType(typeof (LightVisualElement))), (Telerik.WinControls.Filter) PropertyFilter.AppearanceFilter, (Telerik.WinControls.Filter) PropertyFilter.BehaviorFilter, (Telerik.WinControls.Filter) PropertyFilter.LayoutFilter });
    }

    public override float GetPaintingBorderWidth()
    {
      return this.BorderWidth;
    }

    protected override void OnBitStateChanged(long key, bool oldValue, bool newValue)
    {
      base.OnBitStateChanged(key, oldValue, newValue);
      if (key != 35184372088832L)
        return;
      bool htmlEnabled = TinyHTMLParsers.IsHTMLMode(this.Text);
      this.textPrimitiveImpl = TextPrimitiveFactory.CreateTextPrimitiveImp(htmlEnabled);
      if (!htmlEnabled)
        return;
      this.layoutManagerPart.Measure(new SizeF(float.MaxValue, float.MaxValue));
    }

    protected override void DisposeManagedResources()
    {
      if (this.cachedImage != null)
      {
        if (this.ImageIndex != -1 || this.ImageKey != string.Empty)
          this.cachedImage.Dispose();
        this.cachedImage = (Image) null;
      }
      base.DisposeManagedResources();
    }

    public override Bitmap GetAsBitmapEx(Color backColor, float totalAngle, SizeF totalScale)
    {
      TextRenderingHint textRenderingHint = this.TextRenderingHint;
      if (backColor == Color.Transparent)
        this.TextRenderingHint = TextRenderingHint.AntiAlias;
      Bitmap asBitmapEx = base.GetAsBitmapEx(backColor, totalAngle, totalScale);
      if (backColor == Color.Transparent)
        this.TextRenderingHint = textRenderingHint;
      return asBitmapEx;
    }

    private Padding GetClipPadding()
    {
      Padding padding = this.Padding;
      if (padding.Left > 0)
        --padding.Left;
      if (padding.Top > 0)
        --padding.Top;
      if (padding.Right > 0)
        --padding.Right;
      if (padding.Bottom > 0)
        --padding.Bottom;
      return padding;
    }

    protected override RectangleF GetClipRect()
    {
      Size size = this.Bounds.Size;
      Padding borderThickness = this.GetBorderThickness(true);
      Padding clipPadding = this.GetClipPadding();
      return new RectangleF((float) (borderThickness.Left + clipPadding.Left), (float) (borderThickness.Top + clipPadding.Top), (float) (size.Width - borderThickness.Horizontal - clipPadding.Horizontal), (float) (size.Height - borderThickness.Vertical - clipPadding.Vertical));
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      if (this.DrawFill)
        this.PaintFill(graphics, angle, scale);
      this.PaintContent(graphics);
      if (!this.DrawBorder)
        return;
      this.PaintBorder(graphics, angle, scale);
    }

    protected virtual void PaintText(IGraphics graphics)
    {
      if (string.IsNullOrEmpty(this.Text) || !this.DrawText)
      {
        if (!this.ShowHorizontalLine)
          return;
        this.DrawHorizontalLineWithoutText(graphics);
      }
      else
      {
        if (this.ShowHorizontalLine)
          this.DrawHorizontalLine(graphics);
        this.PaintTextCore(graphics);
      }
    }

    protected void PaintTextCore(IGraphics graphics)
    {
      TextParams textParams = this.CreateTextParams();
      this.textPrimitiveImpl.PaintPrimitive(graphics, this.AngleTransform, this.ScaleTransform, textParams);
    }

    protected virtual void DrawHorizontalLineWithoutText(IGraphics graphics)
    {
      using (Pen pen = new Pen(this.HorizontalLineColor, (float) this.HorizontalLineWidth))
        ((Graphics) graphics.UnderlayGraphics).DrawLine(pen, 0, this.Size.Height / 2, this.Size.Width, this.Size.Height / 2);
    }

    protected virtual void DrawHorizontalLine(IGraphics graphics)
    {
      SizeF desiredSize = this.textElement.DesiredSize;
      int x1_1 = 0;
      int y1 = this.Size.Height / 2;
      int x2_1 = 0;
      int y2 = y1;
      switch (this.GetTextAlignment())
      {
        case ContentAlignment.TopLeft:
        case ContentAlignment.MiddleLeft:
        case ContentAlignment.BottomLeft:
          x1_1 = (int) desiredSize.Width + 10;
          x2_1 = this.Size.Width - 2;
          break;
        case ContentAlignment.TopCenter:
        case ContentAlignment.MiddleCenter:
        case ContentAlignment.BottomCenter:
          x1_1 = 1;
          x2_1 = this.Size.Width / 2 - (int) desiredSize.Width / 2 - 10;
          break;
        case ContentAlignment.TopRight:
        case ContentAlignment.MiddleRight:
        case ContentAlignment.BottomRight:
          x1_1 = 1;
          x2_1 = this.Size.Width - 2 - (int) desiredSize.Width - 10;
          break;
      }
      if (x1_1 >= x2_1)
        return;
      Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
      using (Pen pen = new Pen(this.HorizontalLineColor, (float) this.HorizontalLineWidth))
      {
        underlayGraphics.DrawLine(pen, x1_1, y1, x2_1, y2);
        if (this.TextAlignment != ContentAlignment.MiddleCenter && this.TextAlignment != ContentAlignment.TopCenter && this.TextAlignment != ContentAlignment.BottomCenter)
          return;
        int x1_2 = this.Size.Width / 2 + (int) desiredSize.Width / 2 + 10;
        int x2_2 = this.Size.Width - 2;
        underlayGraphics.DrawLine(pen, x1_2, y1, x2_2, y2);
      }
    }

    protected virtual void PaintImage(IGraphics graphics)
    {
      if (this.cachedImage == null || !this.DrawImage)
        return;
      lock (this.cachedImage)
      {
        Image image = (double) this.DpiScaleFactor.Width != 1.0 || (double) this.DpiScaleFactor.Height != 1.0 ? (Image) new Bitmap(this.cachedImage, Size.Round(new SizeF((float) this.cachedImage.Width * this.DpiScaleFactor.Width, (float) this.cachedImage.Height * this.DpiScaleFactor.Height))) : this.cachedImage;
        this.AnimateImage(image, false);
        ImageAnimator.UpdateFrames();
        Region region1 = (Region) null;
        if (this.Shape != null)
        {
          using (GraphicsPath path = this.Shape.CreatePath(new Rectangle(Point.Empty, this.Size)))
          {
            using (Region region2 = new Region(path))
            {
              region1 = ((RadGdiGraphics) graphics).Graphics.Clip;
              ((RadGdiGraphics) graphics).Graphics.SetClip(region2, CombineMode.Intersect);
            }
          }
        }
        this.imagePrimitiveImpl.PaintImage(graphics, image, this.imageElement.Bounds, this.ImageLayout, this.ImageAlignment, (float) this.ImageOpacity, this.RightToLeft);
        if (region1 == null)
          return;
        ((RadGdiGraphics) graphics).Graphics.SetClip(region1, CombineMode.Replace);
      }
    }

    protected virtual void PaintBackgroundImage(IGraphics graphics)
    {
      if (this.cachedBackgroundImage == null || !this.DrawBackgroundImage)
        return;
      Image cachedBackgroundImage = this.cachedBackgroundImage;
      this.AnimateImage(this.cachedBackgroundImage, true);
      ImageAnimator.UpdateFrames();
      switch (this.BackgroundImageLayout)
      {
        case ImageLayout.Tile:
          for (int y = 0; y < this.Size.Height; y += cachedBackgroundImage.Height)
          {
            for (int x = 0; x < this.Size.Width; x += cachedBackgroundImage.Width)
            {
              if (this.ImageOpacity == 1.0)
                graphics.DrawBitmap(cachedBackgroundImage, x, y);
              else
                graphics.DrawBitmap(cachedBackgroundImage, x, y, this.ImageOpacity);
            }
          }
          break;
        case ImageLayout.Center:
          if (this.ImageOpacity == 1.0)
          {
            graphics.DrawBitmap(cachedBackgroundImage, Math.Max(0, (this.Size.Width - cachedBackgroundImage.Width) / 2), Math.Max(0, (this.Size.Height - cachedBackgroundImage.Height) / 2), Math.Min(this.Size.Width, cachedBackgroundImage.Size.Width), Math.Min(this.Size.Height, cachedBackgroundImage.Size.Height));
            break;
          }
          graphics.DrawBitmap(cachedBackgroundImage, Math.Max(0, (this.Size.Width - cachedBackgroundImage.Width) / 2), Math.Max(0, (this.Size.Height - cachedBackgroundImage.Height) / 2), Math.Min(this.Size.Width, cachedBackgroundImage.Size.Width), Math.Min(this.Size.Height, cachedBackgroundImage.Size.Height), this.ImageOpacity);
          break;
        case ImageLayout.Stretch:
          if (this.ImageOpacity == 1.0)
          {
            graphics.DrawBitmap(cachedBackgroundImage, 0, 0, this.Size.Width, this.Size.Height);
            break;
          }
          graphics.DrawBitmap(cachedBackgroundImage, 0, 0, this.Size.Width, this.Size.Height, this.ImageOpacity);
          break;
        case ImageLayout.Zoom:
          if (cachedBackgroundImage.Width == 0 || cachedBackgroundImage.Height == 0)
            break;
          float num = Math.Min((float) this.Size.Width / (float) cachedBackgroundImage.Width, (float) this.Size.Height / (float) cachedBackgroundImage.Height);
          if ((double) num <= 0.0)
            break;
          int width = (int) Math.Round((double) cachedBackgroundImage.Width * (double) num);
          int height = (int) Math.Round((double) cachedBackgroundImage.Height * (double) num);
          int x1 = this.Size.Width - width >> 1;
          int y1 = this.Size.Height - height >> 1;
          if (this.ImageOpacity == 1.0)
          {
            graphics.DrawBitmap(cachedBackgroundImage, x1, y1, width, height);
            break;
          }
          graphics.DrawBitmap(cachedBackgroundImage, x1, y1, width, height, this.ImageOpacity);
          break;
      }
    }

    protected virtual void PaintContent(IGraphics graphics)
    {
      this.PaintBackgroundImage(graphics);
      this.PaintImage(graphics);
      this.PaintText(graphics);
    }

    protected virtual void AnimateImage(Image image, bool isBackgroudnImage)
    {
      if (!ImageAnimator.CanAnimate(image))
        return;
      if (!isBackgroudnImage)
      {
        if (this.GetBitState(8796093022208L))
          return;
        ImageAnimator.Animate(image, new EventHandler(this.OnFrameChanged));
        this.BitState[8796093022208L] = true;
      }
      else
      {
        if (this.GetBitState(17592186044416L))
          return;
        ImageAnimator.Animate(image, new EventHandler(this.OnFrameChanged));
        this.BitState[17592186044416L] = true;
      }
    }

    protected virtual Image ClipImage(Image image, Rectangle imageRectange, Size size)
    {
      if (imageRectange.X < 0)
        imageRectange.X = 0;
      if (imageRectange.Y < 0)
        imageRectange.Y = 0;
      int width = imageRectange.X + imageRectange.Width;
      int height = imageRectange.Y + imageRectange.Height;
      bool flag = false;
      if (width > size.Width)
      {
        width = size.Width - imageRectange.X;
        flag = true;
      }
      if (height > size.Height)
      {
        height = size.Height - imageRectange.Y;
        flag = true;
      }
      if (flag)
      {
        Bitmap srcBitmap = image as Bitmap;
        if (srcBitmap != null)
          return (Image) ReflectionPrimitive.CopyBitmap(srcBitmap, new Rectangle(imageRectange.Location, new Size(width, height)));
      }
      return image;
    }

    private Image GetImage()
    {
      if (this.Image == null && !this.IsImageListSet)
        return (Image) null;
      if (this.IsImageListSet)
      {
        if (this.ImageIndex >= 0 && this.ImageIndex < this.ImageList.Images.Count && this.GetValueSource(LightVisualElement.ImageIndexProperty) > ValueSource.DefaultValue)
          return (Image) new Bitmap(this.ImageList.Images[this.ImageIndex]);
        if (!string.IsNullOrEmpty(this.ImageKey) && this.ImageList.Images.IndexOfKey(this.ImageKey) >= 0)
          return (Image) new Bitmap(this.ImageList.Images[this.ImageKey]);
      }
      return (Image) this.GetValue(LightVisualElement.ImageProperty);
    }

    private void ApplyTransparentColor(Image image)
    {
      if (!this.EnableImageTransparency)
        return;
      Bitmap bitmap = image as Bitmap;
      if (bitmap == null)
        return;
      try
      {
        Version version = Environment.OSVersion.Version;
        if ((version.Major != 6 || version.Minor != 1) && (version.Major <= 6 && bitmap.PixelFormat != PixelFormat.Format32bppArgb))
          return;
        bitmap.MakeTransparent(this.ImageTransparentColor);
      }
      catch
      {
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF size = this.GetClientRectangle(availableSize).Size;
      Padding borderThickness = this.GetBorderThickness(false);
      SizeF desiredSize = this.layoutManagerPart.Measure(size);
      desiredSize.Width += (float) (this.Padding.Horizontal + borderThickness.Horizontal);
      desiredSize.Height += (float) (this.Padding.Vertical + borderThickness.Vertical);
      if (this.ShowHorizontalLine)
        desiredSize.Height += (float) this.HorizontalLineWidth;
      SizeF elementsDesiredSize = this.MeasureElements(availableSize, size, borderThickness);
      return this.CalculateDesiredSize(availableSize, desiredSize, elementsDesiredSize);
    }

    protected virtual SizeF CalculateDesiredSize(
      SizeF availableSize,
      SizeF desiredSize,
      SizeF elementsDesiredSize)
    {
      if ((double) elementsDesiredSize.Width > (double) desiredSize.Width)
        desiredSize.Width = elementsDesiredSize.Width;
      if ((double) elementsDesiredSize.Height > (double) desiredSize.Height)
        desiredSize.Height = elementsDesiredSize.Height;
      desiredSize.Width = Math.Min(desiredSize.Width, availableSize.Width);
      desiredSize.Height = Math.Min(desiredSize.Height, availableSize.Height);
      return desiredSize;
    }

    protected virtual SizeF MeasureElements(
      SizeF availableSize,
      SizeF clientSize,
      Padding borderThickness)
    {
      SizeF sizeF1 = SizeF.Empty;
      if (this.AutoSize)
      {
        foreach (RadElement child in this.Children)
        {
          SizeF sizeF2 = SizeF.Empty;
          if (child.FitToSizeMode == RadFitToSizeMode.FitToParentBounds || this.BypassLayoutPolicies)
          {
            child.Measure(availableSize);
            sizeF2 = child.DesiredSize;
          }
          else if (child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
          {
            child.Measure(new SizeF(clientSize.Width - (float) borderThickness.Horizontal, clientSize.Height - (float) borderThickness.Vertical));
            sizeF2.Width = child.DesiredSize.Width + (float) borderThickness.Horizontal;
            sizeF2.Height += (float) borderThickness.Vertical;
          }
          else
          {
            child.Measure(clientSize);
            sizeF2.Width += child.DesiredSize.Width + (float) this.Padding.Horizontal + (float) borderThickness.Horizontal;
            sizeF2.Height += child.DesiredSize.Height + (float) this.Padding.Vertical + (float) borderThickness.Vertical;
          }
          sizeF1.Width = Math.Max(sizeF1.Width, sizeF2.Width);
          sizeF1.Height = Math.Max(sizeF1.Height, sizeF2.Height);
        }
      }
      else
      {
        foreach (RadElement child in this.Children)
          child.Measure(availableSize);
        sizeF1 = (SizeF) this.Size;
      }
      return sizeF1;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF finalRect = new RectangleF(PointF.Empty, finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.layoutManagerPart.Arrange(clientRectangle);
      foreach (RadElement child in this.Children)
      {
        if (!this.BypassLayoutPolicies)
        {
          if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent)
            child.Arrange(clientRectangle);
          else if (child.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
            child.Arrange(finalRect);
          else if (child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
          {
            Padding borderThickness = this.GetBorderThickness(false);
            child.Arrange(new RectangleF((float) borderThickness.Left, (float) borderThickness.Top, finalRect.Width - (float) borderThickness.Horizontal, finalRect.Height - (float) borderThickness.Vertical));
          }
        }
        else
          child.Arrange(finalRect);
      }
      return finalSize;
    }

    protected virtual void ArrangeElement(RadElement element, SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (element.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
        element.Arrange(new RectangleF((PointF) Point.Empty, finalSize));
      else if (element.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
        element.Arrange(new RectangleF((float) this.BorderThickness.Left, (float) this.BorderThickness.Top, finalSize.Width - (float) this.BorderThickness.Horizontal, finalSize.Height - (float) this.BorderThickness.Vertical));
      else
        element.Arrange(new RectangleF(new PointF(clientRectangle.Left, clientRectangle.Top), element.DesiredSize));
    }

    protected virtual Padding GetClientOffset(bool includeBorder)
    {
      Padding padding = this.Padding;
      if (includeBorder && this.DrawBorder)
      {
        if (this.BorderBoxStyle == BorderBoxStyle.FourBorders)
        {
          padding.Left += (int) this.BorderLeftWidth;
          padding.Top += (int) this.BorderTopWidth;
          padding.Right += (int) this.BorderRightWidth;
          padding.Bottom += (int) this.BorderBottomWidth;
        }
        else
        {
          int val1 = (int) this.BorderWidth;
          if (this.BorderBoxStyle == BorderBoxStyle.OuterInnerBorders)
            val1 = Math.Max(val1, 2);
          padding.Left += val1;
          padding.Top += val1;
          padding.Right += val1;
          padding.Bottom += val1;
        }
      }
      return padding;
    }

    protected virtual Padding GetBorderThickness(bool checkDrawBorder)
    {
      return LightVisualElement.GetBorderThickness(this, checkDrawBorder);
    }

    protected virtual RectangleF GetClientRectangle(bool includeBorder, SizeF finalSize)
    {
      Padding padding = this.Padding;
      float left = (float) padding.Left;
      float top = (float) padding.Top;
      float val2_1 = finalSize.Width - (float) padding.Horizontal;
      float val2_2 = finalSize.Height - (float) padding.Vertical;
      if (includeBorder)
      {
        Padding borderThickness = this.GetBorderThickness(false);
        left += (float) borderThickness.Left;
        top += (float) borderThickness.Top;
        val2_1 -= (float) borderThickness.Horizontal;
        val2_2 -= (float) borderThickness.Vertical;
      }
      float width = Math.Max(0.0f, val2_1);
      float height = Math.Max(0.0f, val2_2);
      return new RectangleF(left, top, width, height);
    }

    protected override RectangleF GetClientRectangle(SizeF finalSize)
    {
      Padding padding = this.Padding;
      RectangleF rectangleF = new RectangleF((float) padding.Left, (float) padding.Top, finalSize.Width - (float) padding.Horizontal, finalSize.Height - (float) padding.Vertical);
      if (this.DrawBorder)
      {
        Padding borderThickness = this.GetBorderThickness(false);
        rectangleF.X += (float) borderThickness.Left;
        rectangleF.Y += (float) borderThickness.Top;
        rectangleF.Width -= (float) borderThickness.Horizontal;
        rectangleF.Height -= (float) borderThickness.Vertical;
      }
      rectangleF.Width = Math.Max(0.0f, rectangleF.Width);
      rectangleF.Height = Math.Max(0.0f, rectangleF.Height);
      return rectangleF;
    }

    protected ContentAlignment GetTextAlignment(ContentAlignment textAlignment)
    {
      if (this.RightToLeft)
      {
        switch (textAlignment)
        {
          case ContentAlignment.TopLeft:
            return ContentAlignment.TopRight;
          case ContentAlignment.TopRight:
            return ContentAlignment.TopLeft;
          case ContentAlignment.MiddleLeft:
            return ContentAlignment.MiddleRight;
          case ContentAlignment.MiddleRight:
            return ContentAlignment.MiddleLeft;
          case ContentAlignment.BottomLeft:
            return ContentAlignment.BottomRight;
          case ContentAlignment.BottomRight:
            return ContentAlignment.BottomLeft;
        }
      }
      return textAlignment;
    }

    protected ContentAlignment GetTextAlignment()
    {
      return this.GetTextAlignment(this.TextAlignment);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      if (!this.IsImageListSet)
        return;
      this.cachedImage = this.GetImage();
      this.InvalidateMeasure();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == LightVisualElement.TextAlignmentProperty || e.Property == VisualElement.FontProperty || (e.Property == VisualElement.ForeColorProperty || e.Property == RadItem.TextProperty))
        this.ToggleTextPrimitive(e.Property);
      else if (e.Property == LightVisualElement.ImageProperty)
      {
        if (this.GetBitState(8796093022208L) && this.cachedImage != null)
          ImageAnimator.StopAnimate(this.cachedImage, new EventHandler(this.OnFrameChanged));
        this.BitState[8796093022208L] = false;
        this.cachedImage = (Image) e.NewValue;
        this.layoutManagerPart.IsDirty = true;
      }
      else if (e.Property == LightVisualElement.BackgroundImageProperty)
      {
        if (this.GetBitState(17592186044416L) && this.cachedBackgroundImage != null)
          ImageAnimator.StopAnimate(this.cachedBackgroundImage, new EventHandler(this.OnFrameChanged));
        this.BitState[17592186044416L] = false;
        this.cachedBackgroundImage = (Image) e.NewValue;
      }
      else if (e.Property == LightVisualElement.ImageIndexProperty || e.Property == LightVisualElement.ImageKeyProperty)
      {
        this.cachedImage = this.GetImage();
        this.InvalidateMeasure();
      }
      else if (e.Property == RadElement.RightToLeftProperty)
        this.layoutManagerPart.IsDirty = true;
      base.OnPropertyChanged(e);
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (args.RoutedEvent != RootRadElement.OnRoutedImageListChanged)
        return;
      this.cachedImage = this.GetImage();
      this.InvalidateMeasure();
    }

    protected virtual void ToggleTextPrimitive(RadProperty property)
    {
      string text = this.Text;
      this.textPrimitiveImpl = TextPrimitiveFactory.CreateTextPrimitiveImp(!this.DisableHTMLRendering && TinyHTMLParsers.IsHTMLMode(text));
      this.layoutManagerPart.IsDirty = true;
    }

    private void OnFrameChanged(object o, EventArgs e)
    {
      this.OnAnimatedImageFrameChanged();
    }

    protected virtual void OnAnimatedImageFrameChanged()
    {
      this.Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.DisableHTMLRendering)
        return;
      this.textPrimitiveImpl.OnMouseMove((object) this, e);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ShadowSettings Shadow
    {
      get
      {
        return new ShadowSettings(Point.Empty, this.ForeColor);
      }
      set
      {
      }
    }

    [Description("Determines whether character trimming will be automatically applied to the element if text cannot be fitted within the available space.")]
    [DefaultValue(false)]
    public bool AutoEllipsis
    {
      get
      {
        return this.GetBitState(1125899906842624L);
      }
      set
      {
        this.BitState[1125899906842624L] = value;
      }
    }

    [Description("Determines whether ampersand character will be treated as mnemonic or not.")]
    [DefaultValue(false)]
    public virtual bool UseMnemonic
    {
      get
      {
        return this.GetBitState(562949953421312L);
      }
      set
      {
        this.BitState[562949953421312L] = value;
      }
    }

    public RectangleF GetFaceRectangle()
    {
      return new RectangleF(PointF.Empty, this.DesiredSize);
    }

    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether text will be wrapped when exceeding the width of the element.")]
    [RadPropertyDefaultValue("TextWrap", typeof (LightVisualElement))]
    [Localizable(true)]
    public virtual bool TextWrap
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

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool ShowKeyboardCues
    {
      get
      {
        return this.GetBitState(140737488355328L);
      }
      set
      {
        this.BitState[140737488355328L] = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool MeasureTrailingSpaces
    {
      get
      {
        return this.GetBitState(281474976710656L);
      }
      set
      {
        this.BitState[281474976710656L] = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public TextParams TextParams
    {
      get
      {
        return this.CreateTextParams();
      }
    }

    protected virtual TextParams CreateTextParams()
    {
      TextParams textParams = new TextParams();
      textParams.text = this.Text;
      textParams.alignment = this.GetTextAlignment();
      textParams.autoEllipsis = this.AutoEllipsis;
      textParams.flipText = this.FlipText;
      textParams.font = this.GetScaledFont(this.DpiScaleFactor.Height);
      RadPropertyValue propertyValue = this.GetPropertyValue(VisualElement.BackColorProperty);
      textParams.backColor = propertyValue == null || propertyValue.CurrentValue == null ? Color.Empty : (Color) propertyValue.CurrentValue;
      textParams.foreColor = this.ForeColor;
      textParams.measureTrailingSpaces = this.MeasureTrailingSpaces;
      textParams.paintingRectangle = this.layoutManagerPart.RightPart.Bounds;
      textParams.rightToLeft = this.RightToLeft;
      textParams.shadow = this.Shadow;
      textParams.showKeyboardCues = this.ShowKeyboardCues;
      textParams.textOrientation = this.TextOrientation;
      textParams.textRenderingHint = this.Enabled || !this.UseDefaultDisabledPaint ? this.TextRenderingHint : this.DisabledTextRenderingHint;
      textParams.textWrap = this.TextWrap;
      textParams.useCompatibleTextRendering = this.UseCompatibleTextRendering;
      textParams.useMnemonic = this.UseMnemonic;
      textParams.stretchHorizontally = this.StretchHorizontally;
      textParams.ClipText = this.ClipText;
      textParams.highlightRanges = (CharacterRange[]) null;
      textParams.highlightColor = Color.Empty;
      textParams.enabled = this.Enabled || !this.UseDefaultDisabledPaint;
      return textParams;
    }

    public void PaintPrimitive(
      IGraphics graphics,
      float angle,
      SizeF scale,
      TextParams textParams)
    {
      this.textPrimitiveImpl.PaintPrimitive(graphics, angle, scale, textParams);
    }

    public void PaintPrimitive(IGraphics graphics, TextParams textParams)
    {
      this.textPrimitiveImpl.PaintPrimitive(graphics, textParams);
    }

    public SizeF MeasureOverride(SizeF availableSize, TextParams textParams)
    {
      return this.textPrimitiveImpl.MeasureOverride(availableSize, textParams);
    }

    public void OnMouseMove(object sender, MouseEventArgs e)
    {
      this.textPrimitiveImpl.OnMouseMove(sender, e);
    }

    public SizeF GetTextSize(SizeF proposedSize, TextParams textParams)
    {
      return this.textPrimitiveImpl.GetTextSize(proposedSize, textParams);
    }

    public SizeF GetTextSize(TextParams textParams)
    {
      return this.textPrimitiveImpl.GetTextSize(textParams);
    }
  }
}
