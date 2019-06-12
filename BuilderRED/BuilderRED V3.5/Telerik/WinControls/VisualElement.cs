// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.VisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Themes;

namespace Telerik.WinControls
{
  [DefaultProperty("BackColor")]
  public class VisualElement : RadElement
  {
    private double? saveGraphicsOpacity = new double?();
    public static RadProperty DefaultSizeProperty = RadProperty.RegisterAttached(nameof (DefaultSize), typeof (Size), typeof (VisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Size.Empty, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ForeColorProperty = RadProperty.Register(nameof (ForeColor), typeof (Color), typeof (VisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlText), ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty BackColorProperty = RadProperty.RegisterAttached(nameof (BackColor), typeof (Color), typeof (VisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Color.FromKnownColor(KnownColor.ControlLightLight), ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FontProperty = RadProperty.Register(nameof (Font), typeof (Font), typeof (VisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Control.DefaultFont, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty SmoothingModeProperty = RadProperty.Register(nameof (SmoothingMode), typeof (SmoothingMode), typeof (VisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SmoothingMode.Default, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OpacityProperty = RadProperty.Register(nameof (Opacity), typeof (double), typeof (VisualElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1.0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty CustomFontProperty = RadProperty.Register(nameof (CustomFont), typeof (string), typeof (VisualElement), new RadPropertyMetadata((object) "None"));
    public static RadProperty CustomFontSizeProperty = RadProperty.Register(nameof (CustomFontSize), typeof (float), typeof (VisualElement), new RadPropertyMetadata((object) 12f));
    public static RadProperty CustomFontStyleProperty = RadProperty.Register(nameof (CustomFontStyle), typeof (FontStyle), typeof (VisualElement), new RadPropertyMetadata((object) FontStyle.Regular));
    private static object EventFontChanged = new object();
    internal const long VisualElementLastStateKey = 68719476736;
    private SmoothingMode graphicsCurrentSmoothingMode;
    private Dictionary<string, Font> scaledFontsCache;

    public event EventHandler FontChanged
    {
      add
      {
        this.Events.AddHandler(VisualElement.EventFontChanged, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(VisualElement.EventFontChanged, (Delegate) value);
      }
    }

    protected virtual void OnFontChanged(EventArgs e)
    {
      EventHandler eventHandler = this.Events[VisualElement.EventFontChanged] as EventHandler;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == VisualElement.FontProperty)
        this.OnFontChanged(EventArgs.Empty);
      else if (e.Property == VisualElement.CustomFontProperty || e.Property == VisualElement.CustomFontSizeProperty || e.Property == VisualElement.CustomFontStyleProperty)
      {
        FontFamily family = (FontFamily) null;
        string customFont = this.CustomFont;
        if (!string.IsNullOrEmpty(customFont) && customFont != "None")
          family = ThemeResolutionService.GetCustomFont(customFont);
        if (family == null)
        {
          int num = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.DefaultValueOverride);
          return;
        }
        if (!string.IsNullOrEmpty(customFont))
        {
          int num1 = (int) this.SetDefaultValueOverride(VisualElement.FontProperty, (object) new Font(family, this.CustomFontSize, this.CustomFontStyle));
        }
      }
      base.OnPropertyChanged(e);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      Rectangle BorderRectangle = new Rectangle(Point.Empty, this.Size);
      graphics.FillRectangle(BorderRectangle, this.BackColor);
      base.PaintElement(graphics, angle, scale);
    }

    protected override void PaintChildren(
      IGraphics graphics,
      Rectangle clipRectange,
      float angle,
      SizeF scale,
      bool useRelativeTransformation)
    {
      if (this.Opacity < 1.0)
      {
        this.saveGraphicsOpacity = new double?(graphics.Opacity);
        graphics.ChangeOpacity(this.Opacity * graphics.Opacity);
      }
      base.PaintChildren(graphics, clipRectange, angle, scale, useRelativeTransformation);
      if (!this.saveGraphicsOpacity.HasValue || !this.saveGraphicsOpacity.HasValue)
        return;
      graphics.ChangeOpacity(this.saveGraphicsOpacity.Value);
    }

    protected override void PrePaintElement(IGraphics graphics)
    {
      base.PrePaintElement(graphics);
      this.graphicsCurrentSmoothingMode = ((Graphics) graphics.UnderlayGraphics).SmoothingMode;
      graphics.ChangeSmoothingMode(this.SmoothingMode);
      if (this.Opacity >= 1.0)
        return;
      this.saveGraphicsOpacity = new double?(graphics.Opacity);
      graphics.ChangeOpacity(this.Opacity * graphics.Opacity);
    }

    protected override void PostPaintElement(IGraphics graphics)
    {
      base.PostPaintElement(graphics);
      Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
      graphics.ChangeSmoothingMode(this.graphicsCurrentSmoothingMode);
      if (!this.saveGraphicsOpacity.HasValue || !this.saveGraphicsOpacity.HasValue)
        return;
      graphics.ChangeOpacity(this.saveGraphicsOpacity.Value);
    }

    public virtual Font GetScaledFont(float scale)
    {
      Screen primaryScreen = Screen.PrimaryScreen;
      SizeF sizeF1 = new SizeF(1f, 1f);
      if (RadControl.EnableDpiScaling)
        sizeF1 = NativeMethods.GetMonitorDpi(primaryScreen, NativeMethods.DpiType.Effective);
      SizeF sizeF2 = new SizeF(scale / sizeF1.Width, scale / sizeF1.Height);
      Font font1 = this.Font ?? Control.DefaultFont;
      string key = sizeF2.ToString() + font1.FontFamily.Name + (object) font1.Size + font1.Style.ToString() + font1.Unit.ToString() + font1.GdiCharSet.ToString() + font1.GdiVerticalFont.ToString();
      if (this.ScaledFontsCache.ContainsKey(key))
        return this.ScaledFontsCache[key];
      Font font2 = new Font(font1.FontFamily, font1.Size * sizeF2.Height, font1.Style, font1.Unit, font1.GdiCharSet, font1.GdiVerticalFont);
      this.ScaledFontsCache.Add(key, font2);
      return font2;
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      if (this.scaledFontsCache == null)
        return;
      this.scaledFontsCache.Clear();
      this.scaledFontsCache = (Dictionary<string, Font>) null;
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("DefaultSize", typeof (VisualElement))]
    [Description("DefaultSize of an element. The property is inheritable through the element tree.")]
    public virtual Size DefaultSize
    {
      get
      {
        return (Size) this.GetValue(VisualElement.DefaultSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.DefaultSizeProperty, (object) value);
      }
    }

    [Description("Foreground color - ex. of the text and borders of an element. The property is inheritable through the element tree.")]
    [RadPropertyDefaultValue("ForeColor", typeof (VisualElement))]
    [Category("Appearance")]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    public virtual Color ForeColor
    {
      get
      {
        return (Color) this.GetValue(VisualElement.ForeColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.ForeColorProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [Description("Background color - ex. of the fill of an element. The property is inheritable through the element tree.")]
    [RadPropertyDefaultValue("BackColor", typeof (VisualElement))]
    [Editor(typeof (RadColorEditor), typeof (UITypeEditor))]
    [TypeConverter(typeof (RadColorEditorConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual Color BackColor
    {
      get
      {
        return (Color) this.GetValue(VisualElement.BackColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.BackColorProperty, (object) value);
      }
    }

    [Description("Font - ex. of the text of an element. The property is inheritable through the element tree.")]
    [Category("Appearance")]
    [RadPropertyDefaultValue("Font", typeof (VisualElement))]
    public virtual Font Font
    {
      get
      {
        return (Font) this.GetValue(VisualElement.FontProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.FontProperty, (object) value);
        this.ScaledFontsCache.Clear();
      }
    }

    [RadPropertyDefaultValue("CustomFont", typeof (VisualElement))]
    [VsbBrowsable(true)]
    [Category("Appearance")]
    [TypeConverter(typeof (CustomFontTypeConverter))]
    public virtual string CustomFont
    {
      get
      {
        return (string) this.GetValue(VisualElement.CustomFontProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.CustomFontProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("CustomFontSize", typeof (VisualElement))]
    [VsbBrowsable(true)]
    public virtual float CustomFontSize
    {
      get
      {
        return (float) this.GetValue(VisualElement.CustomFontSizeProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.CustomFontSizeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("CustomFontStyle", typeof (VisualElement))]
    [Category("Appearance")]
    [VsbBrowsable(true)]
    public virtual FontStyle CustomFontStyle
    {
      get
      {
        return (FontStyle) this.GetValue(VisualElement.CustomFontStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.CustomFontStyleProperty, (object) value);
      }
    }

    [Description("Graphics smoothing mode to be used for painting the element and its children.")]
    [RadPropertyDefaultValue("SmoothingMode", typeof (VisualElement))]
    [Category("Appearance")]
    public virtual SmoothingMode SmoothingMode
    {
      get
      {
        return (SmoothingMode) this.GetValue(VisualElement.SmoothingModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.SmoothingModeProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("Opacity", typeof (VisualElement))]
    [Description("Graphics opacity mode to be used for painting the element and its children.")]
    [Category("Appearance")]
    public virtual double Opacity
    {
      get
      {
        return (double) this.GetValue(VisualElement.OpacityProperty);
      }
      set
      {
        int num = (int) this.SetValue(VisualElement.OpacityProperty, (object) value);
      }
    }

    protected virtual Dictionary<string, Font> ScaledFontsCache
    {
      get
      {
        if (this.scaledFontsCache == null)
          this.scaledFontsCache = new Dictionary<string, Font>();
        return this.scaledFontsCache;
      }
    }
  }
}
