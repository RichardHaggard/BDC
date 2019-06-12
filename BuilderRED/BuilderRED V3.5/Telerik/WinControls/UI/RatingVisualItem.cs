// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RatingVisualItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RatingVisualItem : RadItem
  {
    public static RadProperty IsInRadGridViewProperty = RadProperty.Register(nameof (IsInRadGridView), typeof (bool), typeof (RatingVisualItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private BorderPrimitive border;
    private FillPrimitive fill;

    static RatingVisualItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RatingVisualItemStateManager(), typeof (RatingVisualItem));
    }

    protected override void CreateChildElements()
    {
      this.fill = new FillPrimitive();
      this.border = new BorderPrimitive();
      this.Children.Add((RadElement) this.fill);
      this.Children.Add((RadElement) this.border);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldPaint = true;
    }

    public BorderPrimitive Border
    {
      get
      {
        return this.border;
      }
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.fill;
      }
    }

    public virtual bool IsInRadGridView
    {
      get
      {
        return (bool) this.GetValue(RatingVisualItem.IsInRadGridViewProperty);
      }
      set
      {
        int num = (int) this.SetValue(RatingVisualItem.IsInRadGridViewProperty, (object) value);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF size = this.GetClientRectangle(availableSize).Size;
      Padding borderThickness = this.GetBorderThickness(false);
      SizeF empty = SizeF.Empty;
      empty.Width += (float) (this.Padding.Horizontal + borderThickness.Horizontal);
      empty.Height += (float) (this.Padding.Vertical + borderThickness.Vertical);
      SizeF elementsDesiredSize = this.MeasureElements(availableSize, size, borderThickness);
      return this.CalculateDesiredSize(availableSize, empty, elementsDesiredSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF finalRect = new RectangleF(PointF.Empty, finalSize);
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
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

    protected override RectangleF GetClientRectangle(SizeF finalSize)
    {
      Padding padding = this.Padding;
      RectangleF rectangleF = new RectangleF((float) padding.Left, (float) padding.Top, finalSize.Width - (float) padding.Horizontal, finalSize.Height - (float) padding.Vertical);
      rectangleF.Width = Math.Max(0.0f, rectangleF.Width);
      rectangleF.Height = Math.Max(0.0f, rectangleF.Height);
      return rectangleF;
    }

    protected virtual Padding GetBorderThickness(bool checkDrawBorder)
    {
      if (checkDrawBorder)
        return Padding.Empty;
      Padding padding = Padding.Empty;
      if (this.Border.BoxStyle == BorderBoxStyle.SingleBorder)
        padding = new Padding((int) this.Border.Width);
      else if (this.Border.BoxStyle == BorderBoxStyle.FourBorders)
        padding = new Padding((int) this.Border.LeftWidth, (int) this.Border.TopWidth, (int) this.Border.RightWidth, (int) this.Border.BottomWidth);
      else if (this.Border.BoxStyle == BorderBoxStyle.OuterInnerBorders)
      {
        int all = (int) this.Border.Width;
        if (all == 1)
          all = 2;
        padding = new Padding(all);
      }
      return padding;
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
  }
}
