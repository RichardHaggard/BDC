// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewSummaryLeftElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GanttViewSummaryLeftElement : GanttViewVisualElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.ShouldHandleMouseInput = false;
      this.Shape = (ElementShape) new GanttViewSummaryLeftElement.LeftShape();
      this.Margin = new Padding(0, 6, 0, 0);
      this.MaxSize = new Size(4, 16);
      this.MinSize = new Size(4, 16);
      this.DrawFill = true;
      this.BackColor = Color.LightGreen;
      this.GradientStyle = GradientStyles.Solid;
    }

    [ToolboxItem(false)]
    public class LeftShape : ElementShape
    {
      public override GraphicsPath CreatePath(Rectangle bounds)
      {
        GraphicsPath graphicsPath = new GraphicsPath();
        graphicsPath.AddLine(bounds.Location, new Point(bounds.X, bounds.Bottom));
        graphicsPath.AddLine(new Point(bounds.X, bounds.Bottom), new Point(bounds.Right, bounds.Height / 2));
        graphicsPath.AddLine(new Point(bounds.Right, bounds.Height / 2), new Point(bounds.Right, bounds.Y));
        this.MirrorPath(graphicsPath, (RectangleF) bounds);
        return graphicsPath;
      }
    }
  }
}
