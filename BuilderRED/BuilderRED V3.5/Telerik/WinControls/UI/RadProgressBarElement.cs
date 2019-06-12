// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadProgressBarElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class RadProgressBarElement : LightVisualElement
  {
    public static RadProperty ValueProperty1 = RadProperty.Register(nameof (Value1), typeof (int), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ValueProperty2 = RadProperty.Register(nameof (Value2), typeof (int), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MinimumProperty = RadProperty.Register(nameof (Minimum), typeof (int), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty MaximumProperty = RadProperty.Register(nameof (Maximum), typeof (int), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 100, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty StepProperty = RadProperty.Register(nameof (Step), typeof (int), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 10, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty DashProperty = RadProperty.Register(nameof (Dash), typeof (bool), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty HatchProperty = RadProperty.Register(nameof (Hatch), typeof (bool), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IntegralDashProperty = RadProperty.Register(nameof (IntegralDash), typeof (bool), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ProgressOrientationProperty = RadProperty.Register("Orientation", typeof (ProgressOrientation), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ProgressOrientation.Left, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowProgressIndicatorsProperty = RadProperty.Register(nameof (ShowProgressIndicators), typeof (bool), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsVerticalProperty = RadProperty.Register("IsVertical", typeof (bool), typeof (RadProgressBarElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private UpperProgressIndicatorElement indicatorElement1;
    private ProgressIndicatorElement indicatorElement2;
    private SeparatorsElement separatorsElement;
    private ProgressBarTextElement textElement;
    private string oldText;

    [DefaultValue(0)]
    public int Value1
    {
      get
      {
        return (int) this.GetValue(RadProgressBarElement.ValueProperty1);
      }
      set
      {
        if (value < this.Minimum || value > this.Maximum)
          throw new ArgumentException("'" + (object) value + "' is not a valid value for 'Value1'.\n'Value1' must be between 'Minimum' and 'Maximum'.");
        int num = (int) this.SetValue(RadProgressBarElement.ValueProperty1, (object) value);
        this.InvalidateMeasure();
      }
    }

    [DefaultValue(0)]
    public int Value2
    {
      get
      {
        return (int) this.GetValue(RadProgressBarElement.ValueProperty2);
      }
      set
      {
        if (value < this.Minimum || value > this.Maximum)
          throw new ArgumentException("'" + (object) value + "' is not a valid value for 'Value2'.\n'Value2' must be between 'Minimum' and 'Maximum'.");
        int num = (int) this.SetValue(RadProgressBarElement.ValueProperty2, (object) value);
        this.InvalidateMeasure();
      }
    }

    [DefaultValue(0)]
    public int Minimum
    {
      get
      {
        return (int) this.GetValue(RadProgressBarElement.MinimumProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadProgressBarElement.MinimumProperty, (object) value);
        if (this.Minimum > this.Maximum)
          this.Maximum = this.Minimum;
        if (this.Minimum > this.Value1)
          this.Value1 = this.Minimum;
        if (this.Minimum <= this.Value2)
          return;
        this.Value2 = this.Minimum;
      }
    }

    [DefaultValue(100)]
    public int Maximum
    {
      get
      {
        return (int) this.GetValue(RadProgressBarElement.MaximumProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadProgressBarElement.MaximumProperty, (object) value);
        if (this.Maximum < this.Value1)
          this.Value1 = this.Maximum;
        if (this.Maximum < this.Value2)
          this.Value2 = this.Maximum;
        if (this.Maximum >= this.Minimum)
          return;
        this.Minimum = this.Maximum;
      }
    }

    [DefaultValue(1)]
    public int Step
    {
      get
      {
        return (int) this.GetValue(RadProgressBarElement.StepProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadProgressBarElement.StepProperty, (object) value);
      }
    }

    [DefaultValue(3)]
    public int StepWidth
    {
      get
      {
        return this.separatorsElement.StepWidth;
      }
      set
      {
        this.separatorsElement.StepWidth = value;
      }
    }

    [DefaultValue(ProgressOrientation.Left)]
    public ProgressOrientation ProgressOrientation
    {
      get
      {
        return (ProgressOrientation) this.GetValue(RadProgressBarElement.ProgressOrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadProgressBarElement.ProgressOrientationProperty, (object) value);
      }
    }

    [DefaultValue(false)]
    public bool ShowProgressIndicators
    {
      get
      {
        return (bool) this.GetValue(RadProgressBarElement.ShowProgressIndicatorsProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadProgressBarElement.ShowProgressIndicatorsProperty, (object) value);
      }
    }

    [DefaultValue(false)]
    public bool Dash
    {
      get
      {
        return (bool) this.GetValue(RadProgressBarElement.DashProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadProgressBarElement.DashProperty, (object) value);
        this.separatorsElement.Dash = value;
      }
    }

    [DefaultValue(false)]
    public bool Hatch
    {
      get
      {
        return (bool) this.GetValue(RadProgressBarElement.HatchProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadProgressBarElement.HatchProperty, (object) value);
        this.separatorsElement.Hatch = value;
      }
    }

    [DefaultValue(false)]
    public bool IntegralDash
    {
      get
      {
        return (bool) this.GetValue(RadProgressBarElement.IntegralDashProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadProgressBarElement.IntegralDashProperty, (object) value);
      }
    }

    public override Image Image
    {
      get
      {
        return this.indicatorElement1.Image;
      }
      set
      {
        if (value == null)
          this.indicatorElement1.DrawFill = true;
        else
          this.indicatorElement1.DrawFill = false;
        this.indicatorElement1.Image = value;
      }
    }

    public override ImageLayout ImageLayout
    {
      get
      {
        return this.indicatorElement1.ImageLayout;
      }
      set
      {
        this.indicatorElement1.ImageLayout = value;
      }
    }

    public override int ImageIndex
    {
      get
      {
        return this.indicatorElement1.ImageIndex;
      }
      set
      {
        this.indicatorElement1.ImageIndex = value;
      }
    }

    public override string ImageKey
    {
      get
      {
        return this.indicatorElement1.ImageKey;
      }
      set
      {
        this.indicatorElement1.ImageKey = value;
      }
    }

    [DefaultValue(ContentAlignment.MiddleCenter)]
    public override ContentAlignment ImageAlignment
    {
      get
      {
        return this.indicatorElement1.ImageAlignment;
      }
      set
      {
        this.indicatorElement1.ImageAlignment = value;
      }
    }

    public UpperProgressIndicatorElement IndicatorElement1
    {
      get
      {
        return this.indicatorElement1;
      }
    }

    public ProgressIndicatorElement IndicatorElement2
    {
      get
      {
        return this.indicatorElement2;
      }
    }

    public SeparatorsElement SeparatorsElement
    {
      get
      {
        return this.separatorsElement;
      }
    }

    [DefaultValue(3)]
    public int SeparatorWidth
    {
      get
      {
        return this.SeparatorsElement.SeparatorWidth;
      }
      set
      {
        this.SeparatorsElement.SeparatorWidth = value;
      }
    }

    public Color SeparatorColor1
    {
      get
      {
        return this.SeparatorsElement.SeparatorColor1;
      }
      set
      {
        this.SeparatorsElement.SeparatorColor1 = value;
      }
    }

    public Color SeparatorColor2
    {
      get
      {
        return this.SeparatorsElement.SeparatorColor2;
      }
      set
      {
        this.SeparatorsElement.SeparatorColor2 = value;
      }
    }

    public Color SeparatorColor3
    {
      get
      {
        return this.SeparatorsElement.SeparatorColor3;
      }
      set
      {
        this.SeparatorsElement.SeparatorColor3 = value;
      }
    }

    public Color SeparatorColor4
    {
      get
      {
        return this.SeparatorsElement.SeparatorColor4;
      }
      set
      {
        this.SeparatorsElement.SeparatorColor4 = value;
      }
    }

    public int SeparatorGradientAngle
    {
      get
      {
        return this.separatorsElement.SeparatorGradientAngle;
      }
      set
      {
        this.separatorsElement.SeparatorGradientAngle = value;
      }
    }

    public float SeparatorGradientPercentage1
    {
      get
      {
        return this.separatorsElement.SeparatorGradientPercentage1;
      }
      set
      {
        this.separatorsElement.SeparatorGradientPercentage1 = value;
      }
    }

    public float SeparatorGradientPercentage2
    {
      get
      {
        return this.separatorsElement.SeparatorGradientPercentage2;
      }
      set
      {
        this.separatorsElement.SeparatorGradientPercentage2 = value;
      }
    }

    public int SeparatorNumberOfColors
    {
      get
      {
        return this.separatorsElement.NumberOfColors;
      }
      set
      {
        this.separatorsElement.NumberOfColors = value;
      }
    }

    public ProgressBarTextElement TextElement
    {
      get
      {
        return this.textElement;
      }
    }

    public override string Text
    {
      get
      {
        return this.textElement.Text;
      }
      set
      {
        this.textElement.Text = value;
        this.oldText = value;
        base.Text = value;
      }
    }

    public int SweepAngle
    {
      get
      {
        return this.separatorsElement.SweepAngle;
      }
      set
      {
        this.separatorsElement.SweepAngle = value;
      }
    }

    static RadProgressBarElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadProgressBarStateManager(), typeof (RadProgressBarElement));
    }

    protected override void CallCreateChildElements()
    {
      base.CallCreateChildElements();
      this.indicatorElement1 = new UpperProgressIndicatorElement();
      int num1 = (int) this.indicatorElement1.BindProperty(RadElement.ShapeProperty, (RadObject) this, RadElement.ShapeProperty, PropertyBindingOptions.OneWay);
      this.indicatorElement2 = new ProgressIndicatorElement();
      int num2 = (int) this.indicatorElement2.BindProperty(RadElement.ShapeProperty, (RadObject) this, RadElement.ShapeProperty, PropertyBindingOptions.OneWay);
      this.separatorsElement = new SeparatorsElement();
      int num3 = (int) this.separatorsElement.BindProperty(RadElement.ShapeProperty, (RadObject) this, RadElement.ShapeProperty, PropertyBindingOptions.OneWay);
      this.textElement = new ProgressBarTextElement();
      this.textElement.StretchHorizontally = true;
      this.textElement.StretchVertically = true;
      int num4 = (int) this.textElement.BindProperty(LightVisualElement.TextAlignmentProperty, (RadObject) this, LightVisualElement.TextAlignmentProperty, PropertyBindingOptions.OneWay);
      this.Children.Add((RadElement) this.indicatorElement2);
      this.Children.Add((RadElement) this.indicatorElement1);
      this.Children.Add((RadElement) this.separatorsElement);
      this.Children.Add((RadElement) this.textElement);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawText = false;
      this.DrawFill = true;
      this.DrawBorder = true;
      this.DefaultSize = new Size(50, 20);
      this.ClipDrawing = true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      RectangleF clientRect = new RectangleF(PointF.Empty, availableSize);
      Padding borderThickness = this.GetBorderThickness(false);
      clientRect.Width -= (float) (this.Padding.Horizontal + borderThickness.Horizontal);
      clientRect.Height -= (float) (this.Padding.Vertical + borderThickness.Vertical);
      RectangleF indicatorFinalSize1 = this.GetProgressIndicatorFinalSize((ProgressIndicatorElement) this.indicatorElement1, clientRect, this.Value1);
      RectangleF indicatorFinalSize2 = this.GetProgressIndicatorFinalSize(this.indicatorElement2, clientRect, this.Value2);
      RectangleF separatorsFinalSize = this.GetSeparatorsFinalSize(indicatorFinalSize1, indicatorFinalSize2);
      this.indicatorElement1.Measure(indicatorFinalSize1.Size);
      this.indicatorElement2.Measure(indicatorFinalSize2.Size);
      this.separatorsElement.Measure(separatorsFinalSize.Size);
      this.textElement.Measure(clientRect.Size);
      empty.Width = !float.IsInfinity(separatorsFinalSize.Size.Width) ? Math.Max(separatorsFinalSize.Size.Width, this.textElement.DesiredSize.Width) : this.textElement.DesiredSize.Width;
      empty.Height = !float.IsInfinity(separatorsFinalSize.Size.Height) ? Math.Max(separatorsFinalSize.Size.Height, this.textElement.DesiredSize.Height) : this.textElement.DesiredSize.Height;
      empty.Width += (float) (this.Padding.Horizontal + borderThickness.Horizontal);
      empty.Height += (float) (this.Padding.Vertical + borderThickness.Vertical);
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle1 = this.GetClientRectangle(finalSize);
      RectangleF indicatorFinalSize1 = this.GetProgressIndicatorFinalSize((ProgressIndicatorElement) this.indicatorElement1, clientRectangle1, this.Value1);
      RectangleF indicatorFinalSize2 = this.GetProgressIndicatorFinalSize(this.indicatorElement2, clientRectangle1, this.Value2);
      RectangleF separatorsFinalSize = this.GetSeparatorsFinalSize(indicatorFinalSize1, indicatorFinalSize2);
      RectangleF clientRectangle2 = this.GetClientRectangle(false, finalSize);
      this.indicatorElement1.Arrange(indicatorFinalSize1);
      this.indicatorElement2.Arrange(indicatorFinalSize2);
      this.separatorsElement.Arrange(separatorsFinalSize);
      this.textElement.Arrange(clientRectangle2);
      return finalSize;
    }

    protected RectangleF GetProgressIndicatorFinalSize(
      ProgressIndicatorElement element,
      RectangleF clientRect,
      int value)
    {
      if (value == this.Minimum)
      {
        element.Visibility = ElementVisibility.Collapsed;
        return RectangleF.Empty;
      }
      element.Visibility = ElementVisibility.Visible;
      if (value == this.Maximum)
        return clientRect;
      int step = this.separatorsElement.SeparatorWidth + this.separatorsElement.StepWidth;
      if (this.ProgressOrientation == ProgressOrientation.Left || this.ProgressOrientation == ProgressOrientation.Right)
        return this.GetHorizontalProgressIndicatorFinalSize(clientRect, value, step);
      return this.GetVerticalProgressIndicatorFinalSize(clientRect, value, step);
    }

    protected RectangleF GetVerticalProgressIndicatorFinalSize(
      RectangleF clientRect,
      int value,
      int step)
    {
      float num1 = (float) (this.Maximum - this.Minimum);
      float num2 = (float) (value - this.Minimum);
      float width = clientRect.Width;
      float height = (float) Math.Floor((double) (num2 * clientRect.Height / num1));
      if (this.Dash && !this.IntegralDash)
      {
        height -= height % (float) step;
        if ((double) height <= 0.0 && value > 0)
          height = (float) step;
      }
      PointF location = clientRect.Location;
      if (this.ProgressOrientation == ProgressOrientation.Bottom)
      {
        location.Y = (float) ((double) clientRect.Height - (double) height + (double) this.BorderTopWidth + (double) this.Padding.Top + 1.0);
        if (this.Dash && !this.IntegralDash)
        {
          location.Y += (float) this.separatorsElement.SeparatorWidth;
          height -= (float) this.separatorsElement.SeparatorWidth;
        }
      }
      if ((double) height > (double) clientRect.Height)
      {
        location.Y = clientRect.Y;
        height = clientRect.Height;
      }
      return new RectangleF(location, new SizeF(width, height));
    }

    protected RectangleF GetHorizontalProgressIndicatorFinalSize(
      RectangleF clientRect,
      int value,
      int step)
    {
      float num1 = (float) (this.Maximum - this.Minimum);
      float num2 = (float) (value - this.Minimum);
      float height = clientRect.Height;
      float width = (float) Math.Floor((double) (num2 * clientRect.Width / num1));
      if (this.Dash && !this.IntegralDash)
      {
        width -= width % (float) step;
        if ((double) width <= 0.0 && value > 0)
          width = (float) step;
      }
      PointF location = clientRect.Location;
      if (this.ProgressOrientation == ProgressOrientation.Right)
      {
        location.X = (float) ((double) clientRect.Width - (double) width + (double) this.BorderLeftWidth + (double) this.Padding.Left + 1.0);
        if (this.Dash && !this.IntegralDash)
        {
          location.X += (float) this.separatorsElement.SeparatorWidth;
          width -= (float) this.separatorsElement.SeparatorWidth;
        }
      }
      if ((double) width > (double) clientRect.Width)
      {
        location.X = clientRect.X;
        width = clientRect.Width;
      }
      return new RectangleF(location, new SizeF(width, height));
    }

    protected RectangleF GetSeparatorsFinalSize(
      RectangleF progressBar1Rectangle,
      RectangleF progressBar2Rectangle)
    {
      RectangleF empty = RectangleF.Empty;
      return this.ProgressOrientation == ProgressOrientation.Left || this.ProgressOrientation == ProgressOrientation.Right ? ((double) progressBar1Rectangle.Width <= (double) progressBar2Rectangle.Width ? progressBar2Rectangle : progressBar1Rectangle) : ((double) progressBar1Rectangle.Height <= (double) progressBar2Rectangle.Height ? progressBar2Rectangle : progressBar1Rectangle);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadProgressBarElement.ShowProgressIndicatorsProperty)
        this.UpdateProgressIndicator();
      if (e.Property == RadProgressBarElement.DashProperty || e.Property == RadProgressBarElement.HatchProperty)
      {
        if (this.Dash || this.Hatch)
          this.separatorsElement.Visibility = ElementVisibility.Visible;
        else
          this.separatorsElement.Visibility = ElementVisibility.Collapsed;
      }
      if (e.Property == RadProgressBarElement.ValueProperty1 || e.Property == RadProgressBarElement.ValueProperty2)
      {
        if (this.indicatorElement1.AutoOpacity)
          this.ControlProgressIndicatorsOpacity();
        this.UpdateProgressIndicator();
      }
      if (e.Property == RadProgressBarElement.ProgressOrientationProperty)
      {
        bool flag;
        switch ((ProgressOrientation) e.NewValue)
        {
          case ProgressOrientation.Top:
          case ProgressOrientation.Bottom:
            flag = true;
            break;
          default:
            flag = false;
            break;
        }
        int num1 = (int) this.SetValue(RadProgressBarElement.IsVerticalProperty, (object) flag);
        int num2 = (int) this.indicatorElement1.SetValue(ProgressIndicatorElement.IsVerticalProperty, (object) flag);
        int num3 = (int) this.indicatorElement2.SetValue(ProgressIndicatorElement.IsVerticalProperty, (object) flag);
      }
      base.OnPropertyChanged(e);
    }

    private void UpdateProgressIndicator()
    {
      if (this.ShowProgressIndicators)
        this.textElement.Text = (100 * this.Value1 / (this.Maximum - this.Minimum)).ToString() + " %";
      else if (string.IsNullOrEmpty(this.oldText))
        this.textElement.Text = string.Empty;
      else
        this.textElement.Text = this.oldText;
    }

    private void ControlProgressIndicatorsOpacity()
    {
      if (!(this.Value1 != 0 & this.Value2 != 0))
        return;
      double num = (double) this.Value2 / (double) this.Value1;
      if (num > 2.0 - this.indicatorElement1.AutoOpacityMinimum)
        this.indicatorElement1.Opacity = 1.0;
      else if (num < 1.0)
        this.indicatorElement1.Opacity = this.indicatorElement1.AutoOpacityMinimum;
      else
        this.indicatorElement1.Opacity = this.indicatorElement1.AutoOpacityMinimum + (num - 1.0);
    }

    public void PerformStepValue1()
    {
      if (this.Value1 < this.Maximum)
        this.Value1 += this.Step;
      else
        this.Value1 = this.Maximum;
    }

    public void PerformStepBackValue1()
    {
      if (this.Value1 > this.Minimum)
        this.Value1 -= this.Step;
      else
        this.Value1 = this.Minimum;
    }

    public void IncrementValue1(int value)
    {
      if (this.Value1 < this.Maximum)
        this.Value1 += value;
      else
        this.Value1 = this.Maximum;
    }

    public void DecrementValue1(int value)
    {
      if (this.Value1 > this.Minimum)
        this.Value1 -= value;
      else
        this.Value1 = this.Minimum;
    }

    public void PerformStepValue2()
    {
      if (this.Value2 < this.Maximum)
        this.Value2 += this.Step;
      else
        this.Value2 = this.Maximum;
    }

    public void PerformStepBackValue2()
    {
      if (this.Value2 > this.Minimum)
        this.Value2 -= this.Step;
      else
        this.Value2 = this.Minimum;
    }

    public void IncrementValue2(int value)
    {
      if (this.Value2 < this.Maximum)
        this.Value2 += value;
      else
        this.Value2 = this.Maximum;
    }

    public void DecrementValue2(int value)
    {
      if (this.Value2 > this.Minimum)
        this.Value2 -= value;
      else
        this.Value2 = this.Minimum;
    }
  }
}
