// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeLineElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI
{
  public class TreeNodeLineElement : TreeViewVisual
  {
    public static RadProperty TypeProperty = RadProperty.Register(nameof (Type), typeof (TreeNodeLineElement.LinkType), typeof (TreeNodeLineElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) TreeNodeLineElement.LinkType.VerticalLine, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty LineStyleProperty = RadProperty.Register(nameof (LineStyle), typeof (DashStyle), typeof (TreeNodeLineElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) DashStyle.Solid, ElementPropertyOptions.AffectsDisplay));
    private Size arrowSize;
    private TreeNodeElement nodeElement;

    public TreeNodeLineElement()
    {
    }

    public TreeNodeLineElement(TreeNodeElement nodeElement)
    {
      this.nodeElement = nodeElement;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.BypassLayoutPolicies = true;
      this.ShouldHandleMouseInput = false;
      this.StretchHorizontally = false;
      this.StretchVertically = true;
      this.ShouldPaint = true;
      this.FitToSizeMode = RadFitToSizeMode.FitToParentBounds;
      this.LineStyle = DashStyle.Dot;
      this.ForeColor = Color.Gray;
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

    public TreeNodeLineElement.LinkType Type
    {
      get
      {
        return (TreeNodeLineElement.LinkType) this.GetValue(TreeNodeLineElement.TypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(TreeNodeLineElement.TypeProperty, (object) value);
      }
    }

    public DashStyle LineStyle
    {
      get
      {
        return (DashStyle) this.GetValue(TreeNodeLineElement.LineStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(TreeNodeLineElement.LineStyleProperty, (object) value);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      if (this.nodeElement != null)
        sizeF.Width = (float) this.nodeElement.TreeViewElement.TreeIndent;
      return sizeF;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      switch (this.Type)
      {
        case TreeNodeLineElement.LinkType.VerticalLine:
          this.PaintVerticalLine(graphics);
          break;
        case TreeNodeLineElement.LinkType.HorizontalLine:
          this.PaintHorizontalLine(graphics);
          break;
        case TreeNodeLineElement.LinkType.RightBottomAngleShape:
          this.PaintRightBottomAngleShape(graphics);
          break;
        case TreeNodeLineElement.LinkType.LeftTopAngleShape:
          this.PaintLeftTopAngleShape(graphics);
          break;
        case TreeNodeLineElement.LinkType.TShape:
          this.PaintTShape(graphics);
          break;
        case TreeNodeLineElement.LinkType.RightTopAngleShape:
          this.PaintRightTopAngleShape(graphics);
          break;
      }
    }

    protected virtual void PaintHorizontalLine(IGraphics graphics)
    {
      Size size = this.Size;
      int x1 = 0;
      int num = size.Height / 2;
      graphics.DrawLine(this.ForeColor, this.LineStyle, x1, num, size.Width, num);
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

    protected virtual void PaintRightTopAngleShape(IGraphics graphics)
    {
      Size size = this.Size;
      int num1 = size.Width / 2;
      int num2 = size.Height / 2;
      graphics.DrawLine(this.ForeColor, this.LineStyle, num1, num2, num1, size.Height);
      if (this.RightToLeft)
        graphics.DrawLine(this.ForeColor, this.LineStyle, 0, num2, num1 - 1, num2);
      else
        graphics.DrawLine(this.ForeColor, this.LineStyle, num1 + 1, num2, size.Width, num2);
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
      graphics.DrawLine(this.ForeColor, this.LineStyle, num1, 0, num1, size.Height);
      if (this.RightToLeft)
        graphics.DrawLine(this.ForeColor, this.LineStyle, 0, num2, num1 - 1, num2);
      else
        graphics.DrawLine(this.ForeColor, this.LineStyle, num1 + 1, num2, size.Width, num2);
    }

    public enum LinkType
    {
      VerticalLine,
      HorizontalLine,
      RightBottomAngleShape,
      LeftTopAngleShape,
      TShape,
      RightTopAngleShape,
    }
  }
}
