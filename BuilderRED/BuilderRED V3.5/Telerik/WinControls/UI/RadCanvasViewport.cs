// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCanvasViewport
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadCanvasViewport : RadElement, IRadScrollViewport
  {
    private bool canvasSizeInvalidated;
    private Size canvasSize;

    public Size CanvasSize
    {
      get
      {
        if (this.canvasSizeInvalidated)
        {
          this.canvasSizeInvalidated = false;
          this.canvasSize = this.CalcCanvasSize();
        }
        return this.canvasSize;
      }
      set
      {
      }
    }

    protected virtual Size CalcCanvasSize()
    {
      Rectangle a = Rectangle.Empty;
      foreach (RadElement child in this.GetChildren(ChildrenListOptions.Normal))
      {
        if (!child.AutoSize || child.AutoSizeMode != RadAutoSizeMode.FitToAvailableSize)
        {
          Rectangle b = new Rectangle(child.BoundingRectangle.Location, Size.Add(child.BoundingRectangle.Size, child.Margin.Size));
          a = !a.IsEmpty ? Rectangle.Union(a, b) : b;
        }
      }
      return new Size(a.Left + a.Width, a.Top + a.Height);
    }

    public Size GetExtentSize()
    {
      return this.CanvasSize;
    }

    public void InvalidateViewport()
    {
      this.canvasSizeInvalidated = true;
    }

    public Point ResetValue(Point currentValue, Size viewportSize, Size canvasSize)
    {
      Point empty = Point.Empty;
      empty.X = RadCanvasViewport.ValidatePosition(currentValue.X, canvasSize.Width - viewportSize.Width);
      empty.Y = RadCanvasViewport.ValidatePosition(currentValue.Y, canvasSize.Height - viewportSize.Height);
      return empty;
    }

    internal static int ValidatePosition(int currentPosition, int maxPosition)
    {
      int num = currentPosition;
      if (num > maxPosition)
        num = maxPosition;
      if (num < 0)
        num = 0;
      return num;
    }

    internal static Size CalcMinOffset(Rectangle src, Rectangle dest)
    {
      Size empty = Size.Empty;
      empty.Width = dest.Left - src.Left;
      int num1 = dest.Right - src.Right;
      if (Math.Sign(empty.Width) * Math.Sign(num1) == 1)
      {
        if (Math.Abs(empty.Width) > Math.Abs(num1))
          empty.Width = num1;
      }
      else
        empty.Width = 0;
      empty.Height = dest.Top - src.Top;
      int num2 = dest.Bottom - src.Bottom;
      if (Math.Sign(empty.Height) * Math.Sign(num2) == 1)
      {
        if (Math.Abs(empty.Height) > Math.Abs(num2))
          empty.Height = num2;
      }
      else
        empty.Height = 0;
      return empty;
    }

    public virtual void DoScroll(Point oldValue, Point newValue)
    {
      this.PositionOffset = new SizeF((float) -newValue.X, (float) -newValue.Y);
    }

    public virtual Size ScrollOffsetForChildVisible(
      RadElement childElement,
      Point currentScrollValue)
    {
      Rectangle dest = new Rectangle(Point.Empty, this.Size);
      Rectangle src = new Rectangle(childElement.BoundingRectangle.Location, Size.Add(childElement.BoundingRectangle.Size, childElement.Margin.Size));
      src.Offset((int) Math.Round((double) this.PositionOffset.Width), (int) Math.Round((double) this.PositionOffset.Height));
      Size size = RadCanvasViewport.CalcMinOffset(src, dest);
      return new Size(-size.Width, -size.Height);
    }

    public ScrollPanelParameters GetScrollParams(
      Size viewportSize,
      Size canvasSize)
    {
      return new ScrollPanelParameters(0, Math.Max(1, canvasSize.Width), 1, Math.Max(1, viewportSize.Width), 0, Math.Max(1, canvasSize.Height), 1, Math.Max(1, viewportSize.Height));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.canvasSizeInvalidated = true;
    }

    protected override void OnChildrenChanged(
      RadElement child,
      ItemsChangeOperation changeOperation)
    {
      base.OnChildrenChanged(child, changeOperation);
      this.canvasSizeInvalidated = true;
    }
  }
}
