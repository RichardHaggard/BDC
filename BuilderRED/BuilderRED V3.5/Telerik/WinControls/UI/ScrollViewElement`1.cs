// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollViewElement`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ScrollViewElement<T> : LightVisualElement where T : RadElement, new()
  {
    private RadScrollBarElement hscrollBar;
    private RadScrollBarElement vscrollBar;
    private T viewElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.viewElement = this.CreateViewElement();
      this.InitializeViewElement(this.viewElement);
      this.Children.Add((RadElement) this.viewElement);
      this.hscrollBar = this.CreateScrollBarElement();
      this.hscrollBar.ScrollType = ScrollType.Horizontal;
      this.hscrollBar.MinSize = new Size(0, RadScrollBarElement.HorizontalScrollBarHeight);
      this.hscrollBar.ScrollTimerDelay = 1;
      this.Children.Add((RadElement) this.hscrollBar);
      this.vscrollBar = this.CreateScrollBarElement();
      this.vscrollBar.ScrollType = ScrollType.Vertical;
      this.vscrollBar.MinSize = new Size(RadScrollBarElement.VerticalScrollBarWidth, 0);
      this.vscrollBar.ScrollTimerDelay = 1;
      this.Children.Add((RadElement) this.vscrollBar);
    }

    protected virtual RadScrollBarElement CreateScrollBarElement()
    {
      return new RadScrollBarElement();
    }

    protected virtual T CreateViewElement()
    {
      return new T();
    }

    protected virtual void InitializeViewElement(T viewElement)
    {
    }

    public RadScrollBarElement HScrollBar
    {
      get
      {
        return this.hscrollBar;
      }
    }

    public RadScrollBarElement VScrollBar
    {
      get
      {
        return this.vscrollBar;
      }
    }

    public T ViewElement
    {
      get
      {
        return this.viewElement;
      }
      set
      {
        if ((object) this.viewElement == (object) value)
          return;
        this.viewElement = value;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      this.Layout.Measure(clientRectangle.Size);
      return this.MeasureView(clientRectangle.Size);
    }

    protected virtual SizeF MeasureView(SizeF availableSize)
    {
      Padding clientOffset = this.GetClientOffset(true);
      SizeF sizeF = new SizeF((float) clientOffset.Horizontal, (float) clientOffset.Vertical);
      ElementVisibility visibility1 = this.hscrollBar.Visibility;
      if (this.hscrollBar.Visibility != ElementVisibility.Collapsed)
      {
        this.hscrollBar.Measure(availableSize);
        sizeF.Height += this.hscrollBar.DesiredSize.Height;
        availableSize.Height -= this.hscrollBar.DesiredSize.Height;
      }
      SizeF desiredSize1 = this.hscrollBar.DesiredSize;
      ElementVisibility visibility2 = this.vscrollBar.Visibility;
      if (this.vscrollBar.Visibility != ElementVisibility.Collapsed)
      {
        this.vscrollBar.Measure(availableSize);
        sizeF.Width += this.vscrollBar.DesiredSize.Width;
        availableSize.Width -= this.vscrollBar.DesiredSize.Width;
      }
      SizeF desiredSize2 = this.vscrollBar.DesiredSize;
      this.MeasureViewElement(availableSize);
      bool flag = false;
      if (visibility1 != this.hscrollBar.Visibility)
      {
        if (visibility1 == ElementVisibility.Visible)
        {
          sizeF.Height -= desiredSize1.Height;
          availableSize.Height += desiredSize1.Height;
        }
        else
        {
          this.hscrollBar.Measure(availableSize);
          sizeF.Height += this.hscrollBar.DesiredSize.Height;
          availableSize.Height -= this.hscrollBar.DesiredSize.Height;
        }
        flag = true;
      }
      if (flag)
      {
        this.MeasureViewElement(availableSize);
        flag = false;
      }
      if (visibility2 != this.vscrollBar.Visibility)
      {
        if (visibility2 == ElementVisibility.Visible)
        {
          sizeF.Width -= desiredSize2.Width;
          availableSize.Width += desiredSize2.Width;
        }
        else
        {
          this.vscrollBar.Measure(availableSize);
          sizeF.Width += this.vscrollBar.DesiredSize.Width;
          availableSize.Width -= this.vscrollBar.DesiredSize.Width;
        }
        flag = true;
      }
      if (flag)
        this.MeasureViewElement(availableSize);
      sizeF.Width += this.viewElement.DesiredSize.Width;
      sizeF.Height += this.viewElement.DesiredSize.Height;
      return sizeF;
    }

    protected virtual void MeasureViewElement(SizeF availableSize)
    {
      this.viewElement.Measure(availableSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF viewElementRect = new RectangleF(clientRectangle.X, clientRectangle.Y, clientRectangle.Width, clientRectangle.Height);
      this.Layout.Arrange(clientRectangle);
      RectangleF hscrollBarRect = this.ArrangeHScrollBar(ref viewElementRect, clientRectangle);
      this.ArrangeVScrollBar(ref viewElementRect, hscrollBarRect, clientRectangle);
      viewElementRect.Width = Math.Max(1f, viewElementRect.Width);
      viewElementRect.Height = Math.Max(1f, viewElementRect.Height);
      this.ArrangeViewElement(viewElementRect);
      return finalSize;
    }

    protected virtual void ArrangeViewElement(RectangleF viewElementRect)
    {
      this.viewElement.Arrange(viewElementRect);
    }

    protected virtual RectangleF ArrangeHScrollBar(
      ref RectangleF viewElementRect,
      RectangleF clientRect)
    {
      RectangleF finalRect = RectangleF.Empty;
      if (this.hscrollBar.Visibility != ElementVisibility.Collapsed)
      {
        int num = (int) this.hscrollBar.DesiredSize.Height;
        if (num == 0)
          num = RadScrollBarElement.HorizontalScrollBarHeight;
        float y = clientRect.Bottom - (float) num;
        float width = clientRect.Width - this.vscrollBar.DesiredSize.Width;
        finalRect = new RectangleF(clientRect.X, y, width, (float) num);
        if (this.RightToLeft && this.vscrollBar.Visibility != ElementVisibility.Collapsed)
          finalRect.X += this.vscrollBar.DesiredSize.Width;
        this.hscrollBar.Arrange(finalRect);
        viewElementRect.Height -= this.hscrollBar.DesiredSize.Height;
      }
      return finalRect;
    }

    protected virtual void ArrangeVScrollBar(
      ref RectangleF viewElementRect,
      RectangleF hscrollBarRect,
      RectangleF clientRect)
    {
      if (this.vscrollBar.Visibility == ElementVisibility.Collapsed)
        return;
      int num = (int) this.vscrollBar.DesiredSize.Width;
      if (num == 0)
        num = RadScrollBarElement.VerticalScrollBarWidth;
      float x = clientRect.Right - (float) num;
      float height = clientRect.Height - hscrollBarRect.Height;
      RectangleF finalRect = new RectangleF(x, clientRect.Y, (float) num, height);
      if (this.RightToLeft)
      {
        finalRect.X = clientRect.X;
        viewElementRect.X += (float) num;
      }
      this.vscrollBar.Arrange(finalRect);
      viewElementRect.Width -= (float) num;
    }
  }
}
