// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SeparatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class SeparatorElement : LightVisualElement
  {
    public static RadProperty ShadowOffsetProperty = RadProperty.Register(nameof (ShadowOffset), typeof (Point), typeof (SeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Point.Empty, ElementPropertyOptions.AffectsArrange | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty ShowShadowProperty = RadProperty.Register(nameof (ShowShadow), typeof (bool), typeof (SeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty OrientationProperty = RadProperty.Register(nameof (Orientation), typeof (Orientation), typeof (SeparatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));
    private LinePrimitive line1;
    private LinePrimitive line2;

    public LinePrimitive Line1
    {
      get
      {
        return this.line1;
      }
    }

    public LinePrimitive Line2
    {
      get
      {
        return this.line2;
      }
    }

    [DefaultValue(Orientation.Horizontal)]
    public Orientation Orientation
    {
      get
      {
        return (Orientation) this.GetValue(SeparatorElement.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorElement.OrientationProperty, (object) value);
      }
    }

    public Point ShadowOffset
    {
      get
      {
        return (Point) this.GetValue(SeparatorElement.ShadowOffsetProperty);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorElement.ShadowOffsetProperty, (object) value);
      }
    }

    public bool ShowShadow
    {
      get
      {
        return (bool) this.GetValue(SeparatorElement.ShowShadowProperty);
      }
      set
      {
        int num = (int) this.SetValue(SeparatorElement.ShowShadowProperty, (object) value);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Orientation = Orientation.Horizontal;
      this.DrawFill = false;
      this.DrawBorder = false;
    }

    protected override void CreateChildElements()
    {
      this.line1 = new LinePrimitive();
      this.line1.Class = "Line1";
      this.line1.BackColor = Color.Black;
      this.line1.GradientStyle = GradientStyles.Solid;
      this.line1.LineWidth = 1;
      this.line1.SmoothingMode = SmoothingMode.None;
      this.Children.Add((RadElement) this.line1);
      this.line2 = new LinePrimitive();
      this.line2.Class = "Line2";
      this.line2.BackColor = Color.LightGray;
      this.line2.GradientStyle = GradientStyles.Solid;
      this.line2.LineWidth = 1;
      this.line2.SmoothingMode = SmoothingMode.None;
      this.Children.Add((RadElement) this.line2);
    }

    static SeparatorElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new SeparatorElementStateManager(), typeof (SeparatorElement));
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.ShowShadow)
        this.line2.Visibility = ElementVisibility.Visible;
      else
        this.line2.Visibility = ElementVisibility.Collapsed;
      SizeF empty = SizeF.Empty;
      if (this.Orientation == Orientation.Horizontal)
      {
        foreach (RadElement child in this.Children)
        {
          child.Measure(availableSize);
          empty.Height += child.DesiredSize.Height;
          empty.Width = Math.Max(empty.Width, child.DesiredSize.Width);
        }
      }
      else
      {
        foreach (RadElement child in this.Children)
        {
          child.Measure(availableSize);
          empty.Width += child.DesiredSize.Width;
          empty.Height = Math.Max(empty.Height, child.DesiredSize.Height);
        }
      }
      return empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      if (this.Orientation == Orientation.Horizontal)
        this.ArrangeHorizontalSeparatorLines(clientRectangle);
      else
        this.ArrangeVerticalSeparatorLines(clientRectangle);
      return finalSize;
    }

    private void ArrangeVerticalSeparatorLines(RectangleF clientRect)
    {
      this.line1.SeparatorOrientation = this.line2.SeparatorOrientation = SepOrientation.Vertical;
      float height = this.line1.DesiredSize.Height;
      float num1 = (float) Math.Abs(this.ShadowOffset.Y);
      if (this.ShowShadow)
        height += this.line2.DesiredSize.Height + (float) this.ShadowOffset.X;
      float num2 = 0.0f;
      if (this.ShadowOffset.Y < 0)
        num2 = num1;
      float num3 = (float) (((double) clientRect.Width - (double) height) / 2.0);
      float num4 = clientRect.Height - num1;
      if (float.IsPositiveInfinity(num4))
        num4 = 100f;
      RectangleF finalRect = new RectangleF(clientRect.X + num3, clientRect.Y + num2, (float) this.line1.LineWidth, num4);
      this.line1.Arrange(finalRect);
      if (!this.ShowShadow)
        return;
      finalRect = new RectangleF((float) (this.line1.BoundingRectangle.X + this.line1.LineWidth + this.ShadowOffset.X), (float) (this.line1.BoundingRectangle.Y + this.ShadowOffset.Y), (float) this.line2.LineWidth, num4);
      this.line2.Arrange(finalRect);
    }

    private void ArrangeHorizontalSeparatorLines(RectangleF clientRect)
    {
      this.line1.SeparatorOrientation = this.line2.SeparatorOrientation = SepOrientation.Horizontal;
      float height = this.line1.DesiredSize.Height;
      float num1 = (float) Math.Abs(this.ShadowOffset.X);
      if (this.ShowShadow)
        height += this.line2.DesiredSize.Height + (float) this.ShadowOffset.Y;
      float num2 = 0.0f;
      if (this.ShadowOffset.X < 0)
        num2 = num1;
      float num3 = (float) (((double) clientRect.Height - (double) height) / 2.0);
      float num4 = clientRect.Width - num1;
      if (float.IsPositiveInfinity(num4))
        num4 = 100f;
      RectangleF finalRect = new RectangleF(clientRect.X + num2, clientRect.Y + num3, num4, (float) this.line1.LineWidth);
      this.line1.Arrange(finalRect);
      if (!this.ShowShadow)
        return;
      finalRect = new RectangleF((float) (this.line1.BoundingRectangle.X + this.ShadowOffset.X), (float) (this.line1.BoundingRectangle.Y + this.line1.LineWidth + this.ShadowOffset.Y), num4, (float) this.line2.LineWidth);
      this.line2.Arrange(finalRect);
    }
  }
}
