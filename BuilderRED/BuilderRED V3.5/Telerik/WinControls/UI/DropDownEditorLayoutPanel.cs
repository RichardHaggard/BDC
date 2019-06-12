// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DropDownEditorLayoutPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class DropDownEditorLayoutPanel : DockLayoutPanel
  {
    public static RadProperty IsContentProperty = RadProperty.Register("IsContent", typeof (bool), typeof (DropDownEditorLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty IsArrowButtonProperty = RadProperty.Register("IsArrowButton", typeof (bool), typeof (DropDownEditorLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty IsButtonSeparatorProperty = RadProperty.Register("IsButtonSeparator", typeof (bool), typeof (DropDownEditorLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ExpandArrowProperty = RadProperty.Register(nameof (ExpandArrow), typeof (bool), typeof (DropDownEditorLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ArrowPositionProperty = RadProperty.Register(nameof (ArrowPosition), typeof (DropDownButtonArrowPosition), typeof (DropDownEditorLayoutPanel), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DropDownButtonArrowPosition.Right, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    private RadElement content;
    private RadElement arrowButton;
    private RadElement buttonSeparator;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.FitToSizeMode = RadFitToSizeMode.FitToParentPadding;
      this.NotifyParentOnMouseInput = true;
    }

    public bool ExpandArrow
    {
      get
      {
        return (bool) this.GetValue(DropDownEditorLayoutPanel.ExpandArrowProperty);
      }
      set
      {
        int num = (int) this.SetValue(DropDownEditorLayoutPanel.ExpandArrowProperty, (object) value);
      }
    }

    public DropDownButtonArrowPosition ArrowPosition
    {
      get
      {
        return (DropDownButtonArrowPosition) this.GetValue(DropDownEditorLayoutPanel.ArrowPositionProperty);
      }
      set
      {
        int num = (int) this.SetValue(DropDownEditorLayoutPanel.ArrowPositionProperty, (object) value);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF empty = SizeF.Empty;
      base.MeasureOverride(availableSize);
      this.EnsureContentAndArrowElements();
      availableSize -= (SizeF) this.GetParentBorderSize();
      if (this.ArrowPosition == DropDownButtonArrowPosition.Right || this.ArrowPosition == DropDownButtonArrowPosition.Left)
      {
        foreach (RadElement child in this.Children)
        {
          child.Measure(availableSize);
          availableSize.Width -= child.DesiredSize.Width;
          empty.Width += child.DesiredSize.Width;
          empty.Height = Math.Max(empty.Height, child.DesiredSize.Height);
        }
        return empty;
      }
      foreach (RadElement child in this.Children)
      {
        child.Measure(availableSize);
        availableSize.Height -= child.DesiredSize.Height;
        empty.Width = Math.Max(empty.Width, child.DesiredSize.Width);
        empty.Height += child.DesiredSize.Height;
      }
      if (this.ExpandArrow && !float.IsPositiveInfinity(availableSize.Height))
        return new SizeF(empty.Width, Math.Max(empty.Height, availableSize.Height));
      return new SizeF(empty.Width, empty.Height);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      this.EnsureContentAndArrowElements();
      if (this.ArrowPosition == DropDownButtonArrowPosition.Right || this.ArrowPosition == DropDownButtonArrowPosition.Left)
        this.ArrangeHorizontally(finalSize);
      else
        this.ArrangeVertically(finalSize);
      return finalSize;
    }

    private void ArrangeHorizontally(SizeF finalSize)
    {
      float width = this.arrowButton.DesiredSize.Width;
      if (this.ExpandArrow)
        width = finalSize.Width - this.buttonSeparator.DesiredSize.Width - this.content.DesiredSize.Width;
      if (this.GetHorizontalArrowPosition() == DropDownButtonArrowPosition.Left)
      {
        RectangleF rectangleF = new RectangleF(0.0f, 0.0f, finalSize.Width, finalSize.Height);
        this.arrowButton.Arrange(new RectangleF(0.0f, 0.0f, width, finalSize.Height));
        rectangleF.X += width;
        rectangleF.Width -= width;
        if (this.buttonSeparator != null)
        {
          this.buttonSeparator.Arrange(new RectangleF(rectangleF.X, 0.0f, this.buttonSeparator.DesiredSize.Width, finalSize.Height));
          rectangleF.X += this.buttonSeparator.DesiredSize.Width;
          rectangleF.Width -= this.buttonSeparator.DesiredSize.Width;
        }
        if (this.content == null)
          return;
        this.content.Arrange(new RectangleF(rectangleF.X, 0.0f, rectangleF.Width, finalSize.Height));
      }
      else
      {
        RectangleF rectangleF = new RectangleF(0.0f, 0.0f, finalSize.Width, finalSize.Height);
        rectangleF.Width -= width;
        this.arrowButton.Arrange(new RectangleF(rectangleF.Right, 0.0f, width, finalSize.Height));
        if (this.buttonSeparator != null)
        {
          rectangleF.Width -= this.buttonSeparator.DesiredSize.Width;
          this.buttonSeparator.Arrange(new RectangleF(rectangleF.Right, 0.0f, this.buttonSeparator.DesiredSize.Width, finalSize.Height));
        }
        if (this.content == null)
          return;
        this.content.Arrange(new RectangleF(0.0f, 0.0f, rectangleF.Width, finalSize.Height));
      }
    }

    private void ArrangeVertically(SizeF finalSize)
    {
      float height = this.arrowButton.DesiredSize.Height;
      if (this.ExpandArrow)
        height = finalSize.Height - this.buttonSeparator.DesiredSize.Height - this.content.DesiredSize.Height;
      if (this.ArrowPosition == DropDownButtonArrowPosition.Up)
      {
        RectangleF rectangleF = new RectangleF(0.0f, 0.0f, finalSize.Width, finalSize.Height);
        this.arrowButton.Arrange(new RectangleF(0.0f, 0.0f, finalSize.Width, height));
        rectangleF.Y += height;
        rectangleF.Height -= height;
        if (this.buttonSeparator != null)
        {
          this.buttonSeparator.Arrange(new RectangleF(0.0f, rectangleF.Y, finalSize.Width, this.buttonSeparator.DesiredSize.Height));
          rectangleF.Y += this.buttonSeparator.DesiredSize.Height;
          rectangleF.Height -= this.buttonSeparator.DesiredSize.Height;
        }
        if (this.content == null)
          return;
        this.content.Arrange(new RectangleF(0.0f, rectangleF.Y, finalSize.Width, rectangleF.Height));
      }
      else
      {
        RectangleF rectangleF = new RectangleF(0.0f, 0.0f, finalSize.Width, finalSize.Height);
        rectangleF.Height -= height;
        this.arrowButton.Arrange(new RectangleF(0.0f, rectangleF.Bottom, finalSize.Width, height));
        if (this.buttonSeparator != null)
        {
          rectangleF.Height -= this.buttonSeparator.DesiredSize.Height;
          this.buttonSeparator.Arrange(new RectangleF(0.0f, rectangleF.Bottom, finalSize.Width, this.buttonSeparator.DesiredSize.Height));
        }
        if (this.content == null)
          return;
        this.content.Arrange(new RectangleF(0.0f, 0.0f, finalSize.Width, rectangleF.Height));
      }
    }

    private DropDownButtonArrowPosition GetHorizontalArrowPosition()
    {
      if (this.ArrowPosition == DropDownButtonArrowPosition.Left)
        return this.RightToLeft ? DropDownButtonArrowPosition.Right : DropDownButtonArrowPosition.Left;
      return this.ArrowPosition == DropDownButtonArrowPosition.Right && this.RightToLeft ? DropDownButtonArrowPosition.Left : DropDownButtonArrowPosition.Right;
    }

    private Size GetParentBorderSize()
    {
      if (this.Parent != null)
      {
        foreach (RadElement child in this.Parent.Children)
        {
          BorderPrimitive borderPrimitive = child as BorderPrimitive;
          if (borderPrimitive != null)
          {
            Padding padding = Padding.Empty;
            if (borderPrimitive.BoxStyle == BorderBoxStyle.SingleBorder)
              padding = new Padding((int) Math.Round((double) borderPrimitive.Width, MidpointRounding.AwayFromZero));
            else if (borderPrimitive.BoxStyle == BorderBoxStyle.FourBorders)
              padding = new Padding((int) borderPrimitive.LeftWidth, (int) borderPrimitive.TopWidth, (int) borderPrimitive.RightWidth, (int) borderPrimitive.BottomWidth);
            else if (borderPrimitive.BoxStyle == BorderBoxStyle.OuterInnerBorders)
            {
              int all = (int) borderPrimitive.Width;
              if (all == 1)
                all = 2;
              padding = new Padding(all);
            }
            return new Size(padding.Horizontal, padding.Vertical);
          }
        }
      }
      return Size.Empty;
    }

    private bool EnsureContentAndArrowElements()
    {
      if (this.content != null && this.arrowButton != null)
        return true;
      foreach (RadElement child in this.GetChildren(ChildrenListOptions.IncludeCollapsed))
      {
        if ((bool) child.GetValue(DropDownEditorLayoutPanel.IsContentProperty))
          this.content = child;
        else if ((bool) child.GetValue(DropDownEditorLayoutPanel.IsArrowButtonProperty))
          this.arrowButton = child;
        else if ((bool) child.GetValue(DropDownEditorLayoutPanel.IsButtonSeparatorProperty))
          this.buttonSeparator = child;
      }
      if (this.content != null)
        return this.arrowButton != null;
      return false;
    }
  }
}
