// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridLinkItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class GridLinkItem : RadItem
  {
    public static RadProperty TypeProperty = RadProperty.Register(nameof (Type), typeof (GridLinkItem.LinkType), typeof (GridLinkItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GridLinkItem.LinkType.VerticalLine, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineStyleProperty = RadProperty.Register(nameof (LineStyle), typeof (DashStyle), typeof (GridLinkItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DashStyle.Dot, ElementPropertyOptions.AffectsDisplay));
    private Size arrowSize;
    private GridTableElement tableElement;

    public GridLinkItem()
    {
    }

    public GridLinkItem(GridTableElement tableElement)
    {
      this.tableElement = tableElement;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BypassLayoutPolicies = true;
      this.ShouldHandleMouseInput = false;
      this.StretchHorizontally = false;
      this.StretchVertically = true;
      this.ShouldPaint = true;
    }

    internal Size ArrowSize
    {
      get
      {
        return this.arrowSize;
      }
      set
      {
        this.arrowSize = value;
      }
    }

    public GridLinkItem.LinkType Type
    {
      get
      {
        return (GridLinkItem.LinkType) this.GetValue(GridLinkItem.TypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridLinkItem.TypeProperty, (object) value);
      }
    }

    public DashStyle LineStyle
    {
      get
      {
        return (DashStyle) this.GetValue(GridLinkItem.LineStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridLinkItem.LineStyleProperty, (object) value);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.tableElement != null)
        sizeF.Width = (float) this.tableElement.TreeLevelIndent;
      return sizeF;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      switch (this.Type)
      {
        case GridLinkItem.LinkType.VerticalLine:
          this.PaintVerticalLine(graphics);
          break;
        case GridLinkItem.LinkType.HorizontalLine:
          this.PaintHorizontalLine(graphics);
          break;
        case GridLinkItem.LinkType.RightBottomAngleShape:
          this.PaintRightBottomAngleShape(graphics);
          break;
        case GridLinkItem.LinkType.LeftTopAngleShape:
          this.PaintLeftTopAngleShape(graphics);
          break;
        case GridLinkItem.LinkType.TShape:
          this.PaintTShape(graphics);
          break;
      }
    }

    protected virtual void PaintHorizontalLine(IGraphics graphics)
    {
      int x1 = 0;
      int num = this.Size.Height / 2;
      graphics.DrawLine(this.ForeColor, this.LineStyle, x1, num, this.Size.Width, num);
    }

    protected virtual void PaintVerticalLine(IGraphics graphics)
    {
      Size size = this.Size;
      int num = size.Width / 2;
      int y1 = 0;
      graphics.DrawLine(this.ForeColor, this.LineStyle, num, y1, num, size.Height);
    }

    protected virtual void PaintRightBottomAngleShape(IGraphics graphics)
    {
      Size size = this.Size;
      int num1 = size.Width / 2;
      int num2 = size.Height / 2;
      graphics.DrawLine(this.ForeColor, this.LineStyle, num1, 0, num1, num2);
      if (this.RightToLeft)
        graphics.DrawLine(this.ForeColor, this.LineStyle, 0, num2, num1, num2);
      else
        graphics.DrawLine(this.ForeColor, this.LineStyle, num1, num2, size.Width - 1, num2);
    }

    protected virtual void PaintLeftTopAngleShape(IGraphics graphics)
    {
      Size size = this.Size;
      int num1 = 0;
      if (size.Width % 2 > 0)
        num1 = (size.Width - this.arrowSize.Width) % 2;
      int num2 = size.Width / 2 + num1;
      int num3 = size.Height / 2;
      graphics.DrawLine(this.ForeColor, this.LineStyle, num2, num3, num2, size.Height - 1);
      if (this.RightToLeft)
        graphics.DrawLine(this.ForeColor, this.LineStyle, num2, num3, size.Width - 1, num3);
      else
        graphics.DrawLine(this.ForeColor, this.LineStyle, num2, num3, 0, num3);
    }

    protected virtual void PaintTShape(IGraphics graphics)
    {
      Size size = this.Size;
      int num1 = size.Width / 2;
      int num2 = size.Height / 2;
      if (this.RightToLeft)
      {
        graphics.DrawLine(this.ForeColor, this.LineStyle, size.Width / 2, 0, size.Width / 2, size.Height);
        graphics.DrawLine(this.ForeColor, this.LineStyle, 0, num2, size.Width / 2 - 1, num2);
      }
      else
      {
        graphics.DrawLine(this.ForeColor, this.LineStyle, num1, 0, num1, size.Height);
        graphics.DrawLine(this.ForeColor, this.LineStyle, num1 + 1, num2, size.Width, num2);
      }
    }

    public enum LinkType
    {
      VerticalLine,
      HorizontalLine,
      RightBottomAngleShape,
      LeftTopAngleShape,
      TShape,
    }
  }
}
