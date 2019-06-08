// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.TextPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Drawing;
using Telerik.WinControls.Paint;
using Telerik.WinControls.TextPrimitiveUtils;

namespace Telerik.WinControls.Primitives
{
  public class TextPrimitive : BasePrimitive, ITextProvider, ITextPrimitive
  {
    public static RadProperty TextProperty = RadProperty.Register(nameof (Text), typeof (string), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) string.Empty, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShadowProperty = RadProperty.Register(nameof (Shadow), typeof (ShadowSettings), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextWrapProperty = RadProperty.Register(nameof (TextWrap), typeof (bool), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineLimitProperty = RadProperty.Register(nameof (LineLimit), typeof (bool), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty UseMnemonicProperty = RadProperty.Register(nameof (UseMnemonic), typeof (bool), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AutoEllipsisProperty = RadProperty.Register(nameof (AutoEllipsis), typeof (bool), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextAlignmentProperty = RadProperty.Register(nameof (TextAlignment), typeof (ContentAlignment), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowKeyboardCuesProperty = RadProperty.Register(nameof (ShowKeyboardCues), typeof (bool), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextRenderingHintProperty = RadProperty.Register(nameof (TextRenderingHint), typeof (TextRenderingHint), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextRenderingHint.SystemDefault, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DisabledTextRenderingHintProperty = RadProperty.Register(nameof (DisabledTextRenderingHint), typeof (TextRenderingHint), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TextRenderingHint.AntiAliasGridFit, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MeasureTrailingSpacesProperty = RadProperty.Register(nameof (MeasureTrailingSpaces), typeof (bool), typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty TextOrientationProperty = RadItem.TextOrientationProperty.AddOwner(typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange));
    public static RadProperty FlipTextProperty = RadItem.FlipTextProperty.AddOwner(typeof (TextPrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue));
    private string textWithoutMnemonics = string.Empty;
    private TextFormat textFormat = TextFormat.Default;
    private Color cachedForeColor = Color.Black;
    private ITextPrimitive textPrimitiveImpl;
    private bool useHTMLRendering;
    private bool disableHTMLRendering;
    private SizeF originalSize;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
      this.textPrimitiveImpl = (ITextPrimitive) new TextPrimitiveImpl();
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("TextRenderingHint", typeof (VisualElement))]
    [Description("Graphics text-rendering mode to be used for painting text of the element")]
    public virtual TextRenderingHint TextRenderingHint
    {
      get
      {
        return (TextRenderingHint) this.GetValue(TextPrimitive.TextRenderingHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.TextRenderingHintProperty, (object) value);
      }
    }

    [Description("Graphics text-rendering mode to be used for painting text of the element when in disabled mode")]
    [RadPropertyDefaultValue("TextRenderingHint", typeof (VisualElement))]
    [Category("Appearance")]
    public virtual TextRenderingHint DisabledTextRenderingHint
    {
      get
      {
        return (TextRenderingHint) this.GetValue(TextPrimitive.DisabledTextRenderingHintProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.DisabledTextRenderingHintProperty, (object) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public ITextPrimitive Impl
    {
      get
      {
        return this.textPrimitiveImpl;
      }
    }

    [Localizable(true)]
    [RadPropertyDefaultValue("Text", typeof (TextPrimitive))]
    [Category("Appearance")]
    public string Text
    {
      get
      {
        return (string) this.GetValue(TextPrimitive.TextProperty) ?? string.Empty;
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.TextProperty, (object) value);
        this.ToggleHTML(this.Text);
      }
    }

    [DefaultValue(false)]
    public override bool StretchHorizontally
    {
      get
      {
        return base.StretchHorizontally;
      }
      set
      {
        base.StretchHorizontally = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Color CachedForeColor
    {
      set
      {
        this.cachedForeColor = value;
      }
      get
      {
        return this.cachedForeColor;
      }
    }

    [DefaultValue(false)]
    public override bool StretchVertically
    {
      get
      {
        return base.StretchVertically;
      }
      set
      {
        base.StretchVertically = value;
      }
    }

    [Localizable(true)]
    [RadPropertyDefaultValue("AutoEllipsis", typeof (TextPrimitive))]
    [Category("Appearance")]
    public bool AutoEllipsis
    {
      get
      {
        return (bool) this.GetValue(TextPrimitive.AutoEllipsisProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.AutoEllipsisProperty, (object) value);
      }
    }

    [Localizable(true)]
    [DefaultValue(true)]
    [Description("Includes the trailing space at the end of each line. By default the boundary rectangle returned by the Overload:System.Drawing.Graphics.MeasureString method excludes the space at the end of each line. Set this flag to include that space in measurement.")]
    [Category("Appearance")]
    public bool MeasureTrailingSpaces
    {
      get
      {
        return (bool) this.GetValue(TextPrimitive.MeasureTrailingSpacesProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.MeasureTrailingSpacesProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TextWrap", typeof (TextPrimitive))]
    [Localizable(true)]
    [Category("Appearance")]
    public bool TextWrap
    {
      get
      {
        return (bool) this.GetValue(TextPrimitive.TextWrapProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.TextWrapProperty, (object) value);
        this.ToggleHTML(this.Text);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("UseMnemonic", typeof (TextPrimitive))]
    [Description("If true, each character immediately after an ampersand (&&) will be painted with underscore and the ampersand will be omitted")]
    [Localizable(true)]
    public bool UseMnemonic
    {
      get
      {
        return (bool) this.GetValue(TextPrimitive.UseMnemonicProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.UseMnemonicProperty, (object) value);
      }
    }

    [Localizable(true)]
    [RadPropertyDefaultValue("ShowKeyboardCues", typeof (TextPrimitive))]
    [Category("Appearance")]
    public bool ShowKeyboardCues
    {
      get
      {
        return (bool) this.GetValue(TextPrimitive.ShowKeyboardCuesProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.ShowKeyboardCuesProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("TextOrientation", typeof (TextPrimitive))]
    [Localizable(true)]
    public Orientation TextOrientation
    {
      get
      {
        return (Orientation) this.GetValue(TextPrimitive.TextOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.TextOrientationProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TextOrientation", typeof (TextPrimitive))]
    [Category("Appearance")]
    [Localizable(true)]
    public bool FlipText
    {
      get
      {
        return (bool) this.GetValue(TextPrimitive.FlipTextProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.FlipTextProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("TextAlignment", typeof (TextPrimitive))]
    [Localizable(true)]
    [Category("Appearance")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(TextPrimitive.TextAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.TextAlignmentProperty, (object) value);
      }
    }

    public override bool IsEmpty
    {
      get
      {
        return string.IsNullOrEmpty(this.Text);
      }
    }

    public bool LineLimit
    {
      get
      {
        return (bool) this.GetValue(TextPrimitive.LineLimitProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.LineLimitProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Shadow", typeof (TextPrimitive))]
    [Category("Appearance")]
    [TypeConverter("Telerik.WinControls.UI.Design.ShadowSettingsConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Editor("Telerik.WinControls.UI.Design.ShadowSettingsEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public ShadowSettings Shadow
    {
      get
      {
        return (ShadowSettings) this.GetValue(TextPrimitive.ShadowProperty);
      }
      set
      {
        int num = (int) this.SetValue(TextPrimitive.ShadowProperty, (object) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public TextParams TextParams
    {
      get
      {
        return this.CreateTextParams();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    public bool DisableHTMLRendering
    {
      get
      {
        return this.disableHTMLRendering;
      }
      set
      {
        this.disableHTMLRendering = value;
        this.useHTMLRendering = TinyHTMLParsers.IsHTMLMode(this.Text);
        this.textPrimitiveImpl = TextPrimitiveFactory.CreateTextPrimitiveImp(this.AllowHTMLRendering());
      }
    }

    private ContentAlignment GetTextAlignment()
    {
      if (this.RightToLeft)
      {
        switch (this.TextAlignment)
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
      return this.TextAlignment;
    }

    public SizeF GetTextSize()
    {
      return this.textPrimitiveImpl.GetTextSize(this.CreateTextParams());
    }

    public SizeF GetTextSize(SizeF proposedSize)
    {
      TextParams textParams = this.CreateTextParams();
      return this.textPrimitiveImpl.GetTextSize(proposedSize, textParams);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      TextParams textParams = this.CreateTextParams();
      return this.textPrimitiveImpl.MeasureOverride(availableSize, textParams);
    }

    public StringFormat CreateStringFormat()
    {
      return this.CreateTextParams().CreateStringFormat();
    }

    public RectangleF GetFaceRectangle()
    {
      Size size = this.Size;
      Padding padding = this.Padding;
      return (RectangleF) new Rectangle(padding.Left, padding.Top, size.Width - padding.Horizontal, size.Height - padding.Vertical);
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      TextParams textParams = this.CreateTextParams();
      if (!this.UseCompatibleTextRendering && !this.Enabled && textParams.backColor == Color.Empty)
        textParams.backColor = TelerikHelper.GetColorAtPoint(this.PointToScreen(Point.Round(textParams.paintingRectangle.Location)));
      this.textPrimitiveImpl.PaintPrimitive(graphics, angle, scale, textParams);
    }

    public override string ToString()
    {
      return "Text: " + this.Text;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == TextPrimitive.TextProperty)
      {
        this.textWithoutMnemonics = TelerikHelper.TextWithoutMnemonics((string) e.NewValue);
        this.ToggleHTML(this.textWithoutMnemonics);
      }
      else if (e.Property == RadElement.AlignmentProperty || e.Property == VisualElement.FontProperty || e.Property == VisualElement.ForeColorProperty)
        this.ToggleHTML(this.textWithoutMnemonics);
      else if (e.Property == RadElement.AutoSizeModeProperty)
      {
        switch ((RadAutoSizeMode) e.NewValue)
        {
          case RadAutoSizeMode.FitToAvailableSize:
            this.TextAlignment = ContentAlignment.TopLeft;
            break;
          case RadAutoSizeMode.WrapAroundChildren:
            this.TextAlignment = ContentAlignment.MiddleLeft;
            break;
        }
      }
      base.OnPropertyChanged(e);
    }

    public override Filter GetStylablePropertiesFilter()
    {
      return (Filter) new OrFilter(new Filter[3]{ (Filter) PropertyFilter.TextPrimitiveFilter, (Filter) PropertyFilter.AppearanceFilter, (Filter) PropertyFilter.BehaviorFilter });
    }

    public bool AllowHTMLRendering()
    {
      if (!this.DisableHTMLRendering)
        return this.useHTMLRendering;
      return false;
    }

    public void ToggleHTML(string text)
    {
      this.useHTMLRendering = TinyHTMLParsers.IsHTMLMode(text);
      if (this.DisableHTMLRendering)
        return;
      this.textPrimitiveImpl = TextPrimitiveFactory.CreateTextPrimitiveImp(TinyHTMLParsers.IsHTMLMode(text));
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.textPrimitiveImpl.OnMouseMove((object) this, e);
    }

    protected internal virtual TextParams CreateTextParams()
    {
      return new TextParams() { alignment = this.GetTextAlignment(), autoEllipsis = this.AutoEllipsis, lineLimit = this.LineLimit, flipText = this.FlipText, font = this.GetScaledFont(this.DpiScaleFactor.Height), foreColor = this.ForeColor, measureTrailingSpaces = this.MeasureTrailingSpaces, paintingRectangle = new RectangleF(PointF.Empty, (SizeF) this.Size), rightToLeft = this.RightToLeft, shadow = this.Shadow, showKeyboardCues = this.ShowKeyboardCues, stretchHorizontally = this.StretchHorizontally, text = this.Text, textOrientation = this.TextOrientation, textRenderingHint = this.Enabled ? this.TextRenderingHint : this.DisabledTextRenderingHint, textWrap = this.TextWrap, useCompatibleTextRendering = this.UseCompatibleTextRendering, useMnemonic = this.UseMnemonic, enabled = this.Enabled };
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

    protected override void ArrangeCore(RectangleF finalRect)
    {
      this.originalSize = finalRect.Size;
      base.ArrangeCore(finalRect);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if (this.AutoEllipsis && this.TextWrap)
      {
        if (this.originalSize != finalSize)
          this.LineLimit = true;
        return base.ArrangeOverride(this.originalSize);
      }
      this.LineLimit = false;
      return base.ArrangeOverride(finalSize);
    }
  }
}
