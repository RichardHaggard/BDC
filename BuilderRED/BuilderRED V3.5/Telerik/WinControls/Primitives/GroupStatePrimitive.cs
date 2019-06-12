// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.GroupStatePrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Design;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class GroupStatePrimitive : BasePrimitive
  {
    private static readonly Size minSize = new Size(10, 10);
    public static readonly RadProperty GroupStyleProperty = RadProperty.Register(nameof (PanelBarStyle), typeof (PanelBarStyles), typeof (GroupStatePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) PanelBarStyles.ExplorerBarStyle, ElementPropertyOptions.AffectsDisplay));
    public static readonly RadProperty GroupStateProperty = RadProperty.Register(nameof (State), typeof (GroupStatePrimitive.GroupState), typeof (GroupStatePrimitive), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GroupStatePrimitive.GroupState.Expanded, ElementPropertyOptions.AffectsDisplay));

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = false;
      this.StretchVertically = false;
    }

    [DefaultValue(false)]
    public override bool StretchHorizontally
    {
      get
      {
        return base.StretchHorizontally;
      }
      set
      {
        base.StretchHorizontally = value;
      }
    }

    [DefaultValue(false)]
    public override bool StretchVertically
    {
      get
      {
        return base.StretchVertically;
      }
      set
      {
        base.StretchVertically = value;
      }
    }

    [Description("Appearance")]
    [RadPropertyDefaultValue("PanelBarStyle", typeof (GroupStatePrimitive))]
    public PanelBarStyles PanelBarStyle
    {
      get
      {
        return (PanelBarStyles) this.GetValue(GroupStatePrimitive.GroupStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GroupStatePrimitive.GroupStyleProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("State", typeof (GroupStatePrimitive))]
    [Description("Appearance")]
    public GroupStatePrimitive.GroupState State
    {
      get
      {
        return (GroupStatePrimitive.GroupState) this.GetValue(GroupStatePrimitive.GroupStateProperty);
      }
      set
      {
        int num = (int) this.SetValue(GroupStatePrimitive.GroupStateProperty, (object) value);
      }
    }

    public override void PaintPrimitive(IGraphics graphics, float angle, SizeF scale)
    {
      Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
      graphics.ChangeSmoothingMode(SmoothingMode.AntiAlias);
      switch (this.PanelBarStyle)
      {
        case PanelBarStyles.ExplorerBarStyle:
          int y = 4;
          if (this.State == GroupStatePrimitive.GroupState.Expanded)
          {
            graphics.FillPolygon(this.ForeColor, new Point[6]
            {
              new Point(0, rectangle.Height / 2),
              new Point(rectangle.Width / 2, 0),
              new Point(rectangle.Width, rectangle.Height / 2),
              new Point(rectangle.Width - 2, rectangle.Height / 2),
              new Point(rectangle.Width / 2, 2),
              new Point(2, rectangle.Height / 2)
            });
            graphics.FillPolygon(this.ForeColor, new Point[6]
            {
              new Point(0, rectangle.Height / 2 + y),
              new Point(rectangle.Width / 2, y),
              new Point(rectangle.Width, rectangle.Height / 2 + y),
              new Point(rectangle.Width - 2, rectangle.Height / 2 + y),
              new Point(rectangle.Width / 2, 2 + y),
              new Point(2, rectangle.Height / 2 + y)
            });
            break;
          }
          graphics.FillPolygon(this.ForeColor, new Point[6]
          {
            new Point(0, rectangle.Height / 2),
            new Point(rectangle.Width / 2, rectangle.Height),
            new Point(rectangle.Width, rectangle.Height / 2),
            new Point(rectangle.Width - 2, rectangle.Height / 2),
            new Point(rectangle.Width / 2, rectangle.Height - 2),
            new Point(2, rectangle.Height / 2)
          });
          graphics.FillPolygon(this.ForeColor, new Point[6]
          {
            new Point(0, rectangle.Height / 2 - y),
            new Point(rectangle.Width / 2, rectangle.Height - y),
            new Point(rectangle.Width, rectangle.Height / 2 - y),
            new Point(rectangle.Width - 2, rectangle.Height / 2 - y),
            new Point(rectangle.Width / 2, rectangle.Height - 2 - y),
            new Point(2, rectangle.Height / 2 - y)
          });
          break;
        case PanelBarStyles.VisualStudio2005ToolBox:
          if (this.State == GroupStatePrimitive.GroupState.Expanded)
          {
            graphics.DrawLine(Color.Black, 0, rectangle.Height / 2, rectangle.Width, rectangle.Height / 2);
            break;
          }
          graphics.DrawLine(Color.Black, 0, rectangle.Height / 2, rectangle.Width, rectangle.Height / 2);
          graphics.DrawLine(Color.Black, rectangle.Width / 2, 0, rectangle.Width / 2, rectangle.Height);
          break;
      }
      graphics.RestoreSmoothingMode();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      return (SizeF) GroupStatePrimitive.minSize;
    }

    public enum GroupState
    {
      Expanded,
      Collapsed,
    }
  }
}
