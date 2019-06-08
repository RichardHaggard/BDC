// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.StackLayoutElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class StackLayoutElement : LightVisualElement
  {
    public static RadProperty OrientationProperty = RadProperty.Register(nameof (Orientation), typeof (Orientation), typeof (StackLayoutElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Orientation.Horizontal, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty ElementSpacingProperty = RadProperty.Register(nameof (ElementSpacing), typeof (int), typeof (StackLayoutElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    public static RadProperty RightToLeftModeProperty = RadProperty.Register(nameof (RightToLeftMode), typeof (StackLayoutElement.RightToLeftModes), typeof (StackLayoutElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) StackLayoutElement.RightToLeftModes.ReverseItems, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure));
    private bool fitInAvailableSize;
    private IComparer<RadElement> comparer;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
    }

    [RadPropertyDefaultValue("Orientation", typeof (StackLayoutElement))]
    [Category("Behavior")]
    public Orientation Orientation
    {
      get
      {
        return (Orientation) this.GetValue(StackLayoutElement.OrientationProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutElement.OrientationProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ElementSpacing", typeof (StackLayoutElement))]
    [Category("Behavior")]
    public int ElementSpacing
    {
      get
      {
        return (int) this.GetValue(StackLayoutElement.ElementSpacingProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutElement.ElementSpacingProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("ElementSpacing", typeof (StackLayoutElement))]
    [Category("Behavior")]
    public StackLayoutElement.RightToLeftModes RightToLeftMode
    {
      get
      {
        return (StackLayoutElement.RightToLeftModes) this.GetValue(StackLayoutElement.RightToLeftModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(StackLayoutElement.RightToLeftModeProperty, (object) value);
      }
    }

    public bool FitInAvailableSize
    {
      get
      {
        return this.fitInAvailableSize;
      }
      set
      {
        if (this.fitInAvailableSize == value)
          return;
        this.fitInAvailableSize = value;
        this.InvalidateMeasure();
      }
    }

    public IComparer<RadElement> Comparer
    {
      get
      {
        return this.comparer;
      }
      set
      {
        this.comparer = value;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      Padding clientOffset = this.GetClientOffset(true);
      SizeF sizeF = availableSize;
      SizeF desiredSize = new SizeF((float) clientOffset.Horizontal, (float) clientOffset.Vertical);
      availableSize.Width -= (float) clientOffset.Horizontal;
      availableSize.Height -= (float) clientOffset.Vertical;
      lock (this.Children)
      {
        this.Layout.Measure(availableSize);
        int elementSpacing = this.ElementSpacing;
        if (this.FitInAvailableSize)
        {
          foreach (RadElement child in this.Children)
          {
            if (!child.StretchHorizontally && this.Orientation == Orientation.Horizontal || !child.StretchVertically && this.Orientation == Orientation.Vertical)
              this.MeasureElement(child, ref availableSize, ref desiredSize, elementSpacing);
          }
          foreach (RadElement child in this.Children)
          {
            if (child.StretchHorizontally && this.Orientation == Orientation.Horizontal || child.StretchVertically && this.Orientation == Orientation.Vertical)
              this.MeasureElement(child, ref availableSize, ref desiredSize, elementSpacing);
          }
        }
        else
        {
          foreach (RadElement child in this.Children)
            this.MeasureElement(child, ref availableSize, ref desiredSize, elementSpacing);
        }
        if (this.Children.Count > 1)
        {
          if (this.Orientation == Orientation.Vertical)
            desiredSize.Height += (float) (elementSpacing * (this.Children.Count - 1));
          else
            desiredSize.Width += (float) (elementSpacing * (this.Children.Count - 1));
        }
        desiredSize.Width += (float) clientOffset.Horizontal;
        desiredSize.Height += (float) clientOffset.Vertical;
      }
      if (this.FitInAvailableSize)
      {
        desiredSize.Width = Math.Min(desiredSize.Width, sizeF.Width);
        desiredSize.Height = Math.Min(desiredSize.Height, sizeF.Height);
      }
      return desiredSize;
    }

    private void MeasureElement(
      RadElement element,
      ref SizeF availableSize,
      ref SizeF desiredSize,
      int spacing)
    {
      element.Measure(availableSize);
      if (this.Orientation == Orientation.Vertical)
      {
        desiredSize.Height += element.DesiredSize.Height;
        desiredSize.Width = Math.Max(desiredSize.Width, element.DesiredSize.Width);
        if (!this.FitInAvailableSize)
          return;
        availableSize.Height -= element.DesiredSize.Height + (float) spacing;
      }
      else
      {
        desiredSize.Width += element.DesiredSize.Width;
        desiredSize.Height = Math.Max(desiredSize.Height, element.DesiredSize.Height);
        if (!this.FitInAvailableSize)
          return;
        availableSize.Width -= element.DesiredSize.Width + (float) spacing;
      }
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      this.Layout.Arrange(this.GetClientRectangle(finalSize));
      if (this.Orientation == Orientation.Vertical)
        this.ArrangeVertically(finalSize);
      else
        this.ArrangeHorizontally(finalSize);
      return finalSize;
    }

    protected virtual void ArrangeHorizontally(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float stretchableWidth = 0.0f;
      int num1 = 0;
      int num2 = 0;
      foreach (RadElement child in this.Children)
      {
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          if (child.StretchHorizontally)
          {
            ++num1;
          }
          else
          {
            stretchableWidth += child.DesiredSize.Width;
            ++num2;
          }
        }
      }
      if (num2 > 0)
        stretchableWidth += (float) (this.ElementSpacing * (num2 - 1));
      int elementSpacing = this.ElementSpacing;
      if (num1 > 0)
        stretchableWidth = (clientRectangle.Width - stretchableWidth) / (float) num1 - (float) (elementSpacing * num1);
      this.ArrangeItemsHorizontaly(clientRectangle, finalSize, stretchableWidth, (float) elementSpacing);
    }

    protected virtual void ArrangeItemsHorizontaly(
      RectangleF clientRect,
      SizeF finalSize,
      float stretchableWidth,
      float spacing)
    {
      float x = clientRect.X;
      float y = clientRect.Y;
      List<RadElement> radElementList = new List<RadElement>((IEnumerable<RadElement>) this.Children);
      if (this.Comparer != null)
        radElementList.Sort(this.Comparer);
      for (int index = 0; index < radElementList.Count; ++index)
      {
        RadElement element = !this.RightToLeft || this.RightToLeftMode != StackLayoutElement.RightToLeftModes.ReverseItems ? radElementList[index] : radElementList[radElementList.Count - index - 1];
        if (element.Visibility != ElementVisibility.Collapsed)
        {
          RectangleF arrangeRect = new RectangleF(x, y, stretchableWidth, clientRect.Height);
          RectangleF finalRect = this.AlignRect(element, arrangeRect);
          if (this.RightToLeft && this.RightToLeftMode == StackLayoutElement.RightToLeftModes.ReverseOffset)
            finalRect.X = finalSize.Width - x - finalRect.Width;
          this.ArrangeElement(element, clientRect, finalRect, finalSize);
          x += finalRect.Width + spacing;
        }
      }
    }

    protected virtual void ArrangeVertically(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      float x = clientRectangle.X;
      float y = clientRectangle.Y;
      float height = 0.0f;
      int num1 = 0;
      int num2 = 0;
      foreach (RadElement child in this.Children)
      {
        if (child.Visibility != ElementVisibility.Collapsed)
        {
          if (child.StretchVertically)
          {
            ++num1;
          }
          else
          {
            height += child.DesiredSize.Height;
            ++num2;
          }
        }
      }
      if (num2 > 0)
        height += (float) (this.ElementSpacing * (num2 - 1));
      int elementSpacing = this.ElementSpacing;
      if (num1 > 0)
        height = (clientRectangle.Height - height) / (float) num1 - (float) (elementSpacing * num1);
      List<RadElement> radElementList = new List<RadElement>((IEnumerable<RadElement>) this.Children);
      if (this.Comparer != null)
        radElementList.Sort(this.Comparer);
      for (int index = 0; index < radElementList.Count; ++index)
      {
        if (radElementList[index].Visibility != ElementVisibility.Collapsed)
        {
          RectangleF arrangeRect = new RectangleF(x, y, clientRectangle.Width, height);
          if ((double) y + (double) arrangeRect.Height > (double) clientRectangle.Bottom)
            arrangeRect.Height = (float) ((double) clientRectangle.Bottom - (double) y - 1.0);
          RectangleF finalRect = this.AlignRect(radElementList[index], arrangeRect);
          this.ArrangeElement(radElementList[index], clientRectangle, finalRect, finalSize);
          y += finalRect.Height + (float) elementSpacing;
        }
      }
    }

    protected virtual void ArrangeElement(
      RadElement element,
      RectangleF clientRect,
      RectangleF finalRect,
      SizeF finalSize)
    {
      if (element.FitToSizeMode == RadFitToSizeMode.FitToParentBounds && this.Orientation == Orientation.Horizontal)
      {
        if (!element.RightToLeft)
          finalRect.X -= (float) (this.Padding.Left + this.GetBorderThickness(true).Left);
        else
          finalRect.X += (float) (this.Padding.Right + this.GetBorderThickness(true).Right);
        finalRect.Height = finalSize.Height;
        finalRect.Y = 0.0f;
      }
      element.Arrange(finalRect);
    }

    protected virtual RectangleF AlignRect(RadElement element, RectangleF arrangeRect)
    {
      if (!element.StretchHorizontally && (double) element.DesiredSize.Width != (double) arrangeRect.Width)
      {
        if (this.Orientation == Orientation.Horizontal)
          arrangeRect.Width = element.DesiredSize.Width;
        if (element.Alignment == ContentAlignment.MiddleCenter || element.Alignment == ContentAlignment.TopCenter || element.Alignment == ContentAlignment.BottomCenter)
          arrangeRect.X += (float) (((double) arrangeRect.Width - (double) element.DesiredSize.Width) / 2.0);
        else if (element.Alignment == ContentAlignment.MiddleRight || element.Alignment == ContentAlignment.TopRight || element.Alignment == ContentAlignment.BottomRight)
          arrangeRect.X = arrangeRect.Right - element.DesiredSize.Width;
        if (this.Orientation == Orientation.Vertical)
          arrangeRect.Width = element.DesiredSize.Width;
      }
      if (!element.StretchVertically && (double) element.DesiredSize.Height != (double) arrangeRect.Height)
      {
        if (this.Orientation == Orientation.Vertical)
          arrangeRect.Height = element.DesiredSize.Height;
        if (element.Alignment == ContentAlignment.MiddleLeft || element.Alignment == ContentAlignment.MiddleCenter || element.Alignment == ContentAlignment.MiddleRight)
          arrangeRect.Y += (float) (((double) arrangeRect.Height - (double) element.DesiredSize.Height) / 2.0);
        if (element.Alignment == ContentAlignment.BottomLeft || element.Alignment == ContentAlignment.BottomRight || element.Alignment == ContentAlignment.BottomCenter)
          arrangeRect.Y = arrangeRect.Bottom - element.DesiredSize.Height;
        if (this.Orientation == Orientation.Horizontal)
          arrangeRect.Height = element.DesiredSize.Height;
      }
      return arrangeRect;
    }

    public enum RightToLeftModes
    {
      None,
      ReverseItems,
      ReverseOffset,
    }
  }
}
