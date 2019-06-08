// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.OldShapeEditor.RadShapeEditorControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls.OldShapeEditor
{
  [ToolboxItem(false)]
  public class RadShapeEditorControl : UserControl
  {
    private float zoomFactor = 1f;
    private List<ShapePoint> points = new List<ShapePoint>();
    private Rectangle dimension = new Rectangle(10, 10, 100, 100);
    private const float ZOOM_INCREMENT = 0.2f;
    private const float MAX_ZOOM_FACTOR = 2f;
    private const float MIN_ZOOM_FACTOR = 1f;
    private const int FIELD_WIDTH = 20;
    private int xOverflowOffset;
    private int yOverflowOffset;
    private int DRAWABLE_GRID_LINE_WIDTH;
    private int maxWidth;
    private int maxHeight;
    private int minWidth;
    private int minHeight;
    private int xoff;
    private int yoff;
    private SnapToGrid snapToGrid;
    public PropertyGrid propertyGrid;
    private ShapePoint[] dimensionPoints;
    private ShapePointBase point;
    private RadShapeEditorControl.PointTypes pointType;
    private Point downPoint;
    private Point curPoint;
    private Point oldCurPoint;
    private bool mouseDown;
    private bool isSymmetricPointMode;
    private bool isVerticalSymmetry;
    private ShapePointBase newSymmetricPoint;
    private ShapePointBase referencePoint;
    private IContainer components;
    private ContextMenuStrip contextMenuPoint;
    private ContextMenuStrip contextMenuLine;
    private ToolStripMenuItem menuItemRemovePoint;
    private ToolStripMenuItem menuItemConvert;
    private ToolStripMenuItem anchorStylesToolStripMenuItem;
    private ToolStripMenuItem menuItemAnchorLeft;
    private ToolStripMenuItem menuItemAnchorRight;
    private ToolStripMenuItem menuItemAnchorTop;
    private ToolStripMenuItem menuItemAnchorBottom;
    private ToolStripMenuItem menuItemRemoveLine;
    private ToolStripMenuItem menuItemAddPoint;
    private ToolStripMenuItem menuItemConvertLine;
    private ToolStripMenuItem snapToToolStripMenuItem;
    private ToolStripMenuItem menuItemLeftTopCorner;
    private ToolStripMenuItem menuItemRightTopCorner;
    private ToolStripMenuItem menuItemLeftBottomCorner;
    private ToolStripMenuItem menuItemRightBottomCorner;
    private ToolStripMenuItem menuItemLocked;
    private ToolStripMenuItem creToolStripMenuItem;
    private ToolStripMenuItem makeSymmetricToolStripMenuItem;
    private ToolStripMenuItem horizontallyToolStripMenuItem;
    private ToolStripMenuItem verticallyToolStripMenuItem;
    private ToolStripMenuItem horizontallyToolStripMenuItem1;
    private ToolStripMenuItem verticallyToolStripMenuItem1;
    private HScrollBar hScrollBar1;
    private VScrollBar vScrollBar1;

    public List<ShapePoint> Points
    {
      get
      {
        return this.points;
      }
    }

    public Rectangle Dimension
    {
      get
      {
        return this.dimension;
      }
      set
      {
        this.dimension = value;
        int left = this.dimension.Left;
        int num1 = this.dimension.Width / 2;
        int top = this.dimension.Top;
        int num2 = this.dimension.Height / 2;
      }
    }

    public RadShapeEditorControl()
    {
      this.snapToGrid = new SnapToGrid();
      this.xoff = this.yoff = 0;
      this.xOverflowOffset = this.yOverflowOffset = 0;
      this.maxWidth = this.ClientRectangle.Width;
      this.maxHeight = this.ClientRectangle.Height;
      this.minWidth = 0;
      this.minHeight = 0;
      this.snapToGrid.FieldWidth = 20f;
      this.DRAWABLE_GRID_LINE_WIDTH = (int) this.snapToGrid.FieldWidth;
      this.snapToGrid.SnapType = SnapToGrid.SnapTypes.Fixed;
      this.snapToGrid.SnapFixed = 6f;
      this.newSymmetricPoint = this.referencePoint = (ShapePointBase) null;
      this.dimensionPoints = new ShapePoint[4];
      for (int index = 0; index < 4; ++index)
        this.dimensionPoints[index] = new ShapePoint();
      this.InitializeComponent();
      this.SetStyle(ControlStyles.ContainerControl | ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.contextMenuPoint.RenderMode = ToolStripRenderMode.Professional;
      ((ToolStripProfessionalRenderer) this.contextMenuPoint.Renderer).ColorTable.UseSystemColors = true;
      this.contextMenuLine.RenderMode = ToolStripRenderMode.Professional;
      ((ToolStripProfessionalRenderer) this.contextMenuLine.Renderer).ColorTable.UseSystemColors = true;
      this.menuItemAddPoint.Click += new EventHandler(this.menuItemAddPoint_Click);
      this.menuItemConvert.Click += new EventHandler(this.menuItemConvert_Click);
      this.menuItemRemovePoint.Click += new EventHandler(this.menuItemRemovePoint_Click);
      this.menuItemRemoveLine.Click += new EventHandler(this.menuItemRemoveLine_Click);
      this.menuItemAnchorLeft.Click += new EventHandler(this.menuItemAnchorLeft_Click);
      this.menuItemAnchorRight.Click += new EventHandler(this.menuItemAnchorRight_Click);
      this.menuItemAnchorTop.Click += new EventHandler(this.menuItemAnchorTop_Click);
      this.menuItemAnchorBottom.Click += new EventHandler(this.menuItemAnchorBottom_Click);
      this.menuItemConvertLine.Click += new EventHandler(this.menuItemConvert_Click);
      this.menuItemLeftTopCorner.Click += new EventHandler(this.menuItemLeftTopCorner_Click);
      this.menuItemLeftBottomCorner.Click += new EventHandler(this.menuItemLeftBottomCorner_Click);
      this.menuItemRightTopCorner.Click += new EventHandler(this.menuItemRightTopCorner_Click);
      this.menuItemRightBottomCorner.Click += new EventHandler(this.menuItemRightBottomCorner_Click);
      this.menuItemLocked.Click += new EventHandler(this.menuItemLocked_Click);
    }

    private void DrawGridLines(Graphics graphics, Rectangle rect, Color color)
    {
      Pen pen = new Pen((Brush) new SolidBrush(color));
      pen.DashStyle = DashStyle.Dot;
      this.xoff = (int) Math.Round((double) -this.xOverflowOffset * (double) this.zoomFactor) % this.DRAWABLE_GRID_LINE_WIDTH;
      this.yoff = (int) Math.Round((double) -this.yOverflowOffset * (double) this.zoomFactor) % this.DRAWABLE_GRID_LINE_WIDTH;
      for (int index = 0; index < rect.Width / this.DRAWABLE_GRID_LINE_WIDTH; ++index)
        graphics.DrawLine(pen, new Point(this.xoff + index * this.DRAWABLE_GRID_LINE_WIDTH, 0), new Point(this.xoff + index * this.DRAWABLE_GRID_LINE_WIDTH, rect.Bottom));
      for (int index = 0; index < rect.Height / this.DRAWABLE_GRID_LINE_WIDTH; ++index)
        graphics.DrawLine(pen, new Point(0, this.yoff + index * this.DRAWABLE_GRID_LINE_WIDTH), new Point(rect.Right, this.yoff + index * this.DRAWABLE_GRID_LINE_WIDTH));
    }

    private Rectangle GetRealFromVirtualRect(Rectangle r)
    {
      return new Rectangle((int) Math.Round((double) (r.X - this.xOverflowOffset) * (double) this.zoomFactor), (int) Math.Round((double) (r.Y - this.yOverflowOffset) * (double) this.zoomFactor), (int) Math.Round((double) r.Width * (double) this.zoomFactor), (int) Math.Round((double) r.Height * (double) this.zoomFactor));
    }

    private Point GetRealFromVirtualPoint(ShapePointBase pt)
    {
      return new Point((int) Math.Round((double) (pt.GetPoint().X - this.xOverflowOffset) * (double) this.zoomFactor), (int) Math.Round((double) (pt.GetPoint().Y - this.yOverflowOffset) * (double) this.zoomFactor));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.FillRectangle(Brushes.White, this.ClientRectangle);
      Rectangle clientRectangle = this.ClientRectangle;
      --clientRectangle.Width;
      --clientRectangle.Height;
      e.Graphics.DrawRectangle(SystemPens.ControlDark, clientRectangle);
      Matrix matrix = new Matrix(1f, 0.0f, 0.0f, 1f, (float) (this.xOverflowOffset - this.hScrollBar1.Value), (float) (this.yOverflowOffset - this.vScrollBar1.Value));
      matrix.Scale(this.zoomFactor, this.zoomFactor);
      e.Graphics.Transform = matrix;
      e.Graphics.FillRectangle(Brushes.LightGray, this.GetRealFromVirtualRect(this.Dimension));
      this.DrawGridLines(e.Graphics, new Rectangle(0, 0, (int) Math.Round((double) this.ClientRectangle.Width * 1.5), (int) Math.Round((double) this.ClientRectangle.Height * 1.5)), Color.Black);
      Rectangle dimension = this.dimension;
      int num1 = dimension.Left + dimension.Width / 2;
      int num2 = dimension.Top + dimension.Height / 2;
      e.Graphics.DrawRectangle(Pens.Black, this.GetRealFromVirtualRect(dimension));
      e.Graphics.FillRectangle(Brushes.Black, this.GetRealFromVirtualRect(new Rectangle(num1 - 4, dimension.Y - 4, 8, 4)));
      e.Graphics.FillRectangle(Brushes.Black, this.GetRealFromVirtualRect(new Rectangle(num1 - 4, dimension.Bottom, 8, 4)));
      e.Graphics.FillRectangle(Brushes.Black, this.GetRealFromVirtualRect(new Rectangle(dimension.X - 4, num2 - 4, 4, 8)));
      e.Graphics.FillRectangle(Brushes.Black, this.GetRealFromVirtualRect(new Rectangle(dimension.Right, num2 - 4, 4, 8)));
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      this.minWidth = this.minHeight = 0;
      this.maxWidth = this.ClientRectangle.Width;
      this.maxHeight = this.ClientRectangle.Height;
      using (Brush brush1 = (Brush) new SolidBrush(Color.Red))
      {
        using (Pen pen1 = new Pen(Color.Black, 2f))
        {
          using (Pen pen2 = new Pen(Color.Blue, 2f))
          {
            using (Brush brush2 = (Brush) new SolidBrush(Color.Blue))
            {
              using (Pen pen3 = new Pen(Color.Green))
              {
                using (Brush brush3 = (Brush) new SolidBrush(Color.Green))
                {
                  for (int index = 0; index < this.points.Count; ++index)
                  {
                    ShapePoint point = this.points[index];
                    ShapePoint shapePoint = index < this.points.Count - 1 ? this.points[index + 1] : this.points[0];
                    this.UpdateMaxSize((ShapePointBase) point);
                    this.UpdateMaxSize((ShapePointBase) shapePoint);
                    Pen pen4 = this.point == point ? pen2 : pen1;
                    if (point.Bezier)
                    {
                      e.Graphics.DrawBezier(pen4, this.GetRealFromVirtualPoint((ShapePointBase) point), this.GetRealFromVirtualPoint(point.ControlPoint1), this.GetRealFromVirtualPoint(point.ControlPoint2), this.GetRealFromVirtualPoint((ShapePointBase) shapePoint));
                      e.Graphics.DrawLine(pen3, this.GetRealFromVirtualPoint((ShapePointBase) point), this.GetRealFromVirtualPoint(point.ControlPoint1));
                      e.Graphics.DrawLine(pen3, this.GetRealFromVirtualPoint((ShapePointBase) shapePoint), this.GetRealFromVirtualPoint(point.ControlPoint2));
                      e.Graphics.FillEllipse(point.ControlPoint1 == this.point || point.ControlPoint1.Selected ? brush2 : brush3, this.GetRealFromVirtualRect(point.ControlPoint1.GetBounds(8)));
                      e.Graphics.FillEllipse(point.ControlPoint2 == this.point || point.ControlPoint2.Selected ? brush2 : brush3, this.GetRealFromVirtualRect(point.ControlPoint2.GetBounds(8)));
                      this.UpdateMaxSize(point.ControlPoint1);
                      this.UpdateMaxSize(point.ControlPoint2);
                    }
                    else
                      e.Graphics.DrawLine(point == this.point ? pen2 : pen1, this.GetRealFromVirtualPoint((ShapePointBase) point), this.GetRealFromVirtualPoint((ShapePointBase) shapePoint));
                    e.Graphics.FillEllipse(point == this.point || point.Selected ? brush2 : brush1, this.GetRealFromVirtualRect(point.GetBounds(8)));
                    e.Graphics.FillEllipse(shapePoint == this.point || shapePoint.Selected ? brush2 : brush1, this.GetRealFromVirtualRect(shapePoint.GetBounds(8)));
                  }
                  if (this.mouseDown)
                  {
                    Rectangle rect = new Rectangle(this.downPoint.X < this.curPoint.X ? this.downPoint.X : this.curPoint.X, this.downPoint.Y < this.curPoint.Y ? this.downPoint.Y : this.curPoint.Y, Math.Abs(this.downPoint.X - this.curPoint.X), Math.Abs(this.downPoint.Y - this.curPoint.Y));
                    rect.X = (int) Math.Round((double) rect.X / Math.Pow((double) this.zoomFactor, 1.0));
                    rect.Y = (int) Math.Round((double) rect.Y / Math.Pow((double) this.zoomFactor, 1.0));
                    rect.Width = (int) Math.Round((double) rect.Width / Math.Pow((double) this.zoomFactor, 1.0));
                    rect.Height = (int) Math.Round((double) rect.Height / Math.Pow((double) this.zoomFactor, 1.0));
                    pen2.Width = 1f;
                    e.Graphics.DrawRectangle(pen2, rect);
                  }
                }
              }
            }
          }
        }
      }
      this.vScrollBar1.Minimum = this.minHeight;
      this.hScrollBar1.Minimum = this.minWidth;
      this.vScrollBar1.Maximum = this.maxHeight;
      this.hScrollBar1.Maximum = this.maxWidth;
      this.SizeScrollBars();
      if (!this.snapToGrid.IsLastSnapped)
        return;
      e.Graphics.FillEllipse((Brush) new SolidBrush(Color.Red), this.GetRealFromVirtualRect(new Rectangle((int) this.snapToGrid.SnappedPoint.X - 2, (int) this.snapToGrid.SnappedPoint.Y - 2, 4, 4)));
    }

    private void UpdateMaxSize(ShapePointBase pt)
    {
      if ((double) pt.X > (double) this.maxWidth)
        this.maxWidth = (int) pt.X;
      if ((double) pt.Y > (double) this.maxHeight)
        this.maxHeight = (int) pt.Y;
      if ((double) pt.X < (double) this.minWidth)
        this.minWidth = (int) pt.X;
      if ((double) pt.Y >= (double) this.minHeight)
        return;
      this.minHeight = (int) pt.Y;
    }

    private void RadShapeEditorControl_Load(object sender, EventArgs e)
    {
      if (this.points.Count == 0)
      {
        this.Dimension = new Rectangle(this.ClientRectangle.X + 20, this.ClientRectangle.Y + 20, this.ClientRectangle.Width - 40, this.ClientRectangle.Height - 40);
        this.points.Add(new ShapePoint(this.Dimension.X, this.Dimension.Y));
        this.points.Add(new ShapePoint(this.Dimension.Right, this.Dimension.Y));
        this.points.Add(new ShapePoint(this.Dimension.Right, this.Dimension.Bottom));
        this.points.Add(new ShapePoint(this.Dimension.X, this.Dimension.Bottom));
      }
      if (this.propertyGrid == null)
        return;
      this.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
    }

    private void RadShapeEditorControl_MouseDown(object sender, MouseEventArgs e)
    {
      this.Cursor = Cursors.Cross;
      this.mouseDown = true;
      this.downPoint = new Point(e.X, e.Y);
      Point point1 = new Point((int) Math.Round((double) this.downPoint.X / Math.Pow((double) this.zoomFactor, 2.0)) + this.xOverflowOffset, (int) Math.Round((double) this.downPoint.Y / Math.Pow((double) this.zoomFactor, 2.0)) + this.yOverflowOffset);
      this.curPoint = this.downPoint;
      this.point = (ShapePointBase) null;
      foreach (ShapePoint point2 in this.points)
      {
        if (point2.IsVisible(point1.X, point1.Y, (int) Math.Round(8.0 * (double) this.zoomFactor)))
        {
          this.pointType = RadShapeEditorControl.PointTypes.Point;
          this.point = (ShapePointBase) point2;
          break;
        }
        if (point2.Bezier)
        {
          if (point2.ControlPoint1.IsVisible(point1.X, point1.Y, (int) Math.Round(8.0 * (double) this.zoomFactor)))
          {
            this.pointType = RadShapeEditorControl.PointTypes.ControlPoint;
            this.point = point2.ControlPoint1;
            break;
          }
          if (point2.ControlPoint2.IsVisible(point1.X, point1.Y, (int) Math.Round(8.0 * (double) this.zoomFactor)))
          {
            this.pointType = RadShapeEditorControl.PointTypes.ControlPoint;
            this.point = point2.ControlPoint2;
            break;
          }
        }
      }
      if (this.isSymmetricPointMode)
      {
        if (this.point == null)
        {
          this.isSymmetricPointMode = false;
        }
        else
        {
          this.referencePoint = this.point;
          Rectangle dimension = this.dimension;
          int num1 = dimension.Top + dimension.Height / 2;
          int num2 = dimension.Left + dimension.Width / 2;
          if (this.isVerticalSymmetry)
            this.newSymmetricPoint.Y = this.referencePoint.Y + (float) (2.0 * ((double) num1 - (double) this.point.Y));
          else
            this.newSymmetricPoint.X = this.referencePoint.X + (float) (2.0 * ((double) num2 - (double) this.point.X));
          this.isSymmetricPointMode = false;
          this.Refresh();
        }
      }
      else
      {
        if (this.point == null)
        {
          for (int index = 0; index < this.points.Count; ++index)
          {
            ShapePoint point2 = this.points[index];
            ShapePoint nextPoint = index < this.points.Count - 1 ? this.points[index + 1] : this.points[0];
            if (point2.IsVisible(nextPoint, new Point(point1.X, point1.Y), 3))
            {
              this.pointType = RadShapeEditorControl.PointTypes.Line;
              this.point = (ShapePointBase) point2;
              break;
            }
          }
        }
        if (this.propertyGrid != null)
          this.propertyGrid.SelectedObject = this.point == null ? (object) null : (object) this.point;
        this.Refresh();
        if (e.Button == MouseButtons.Left && (this.point == null || !this.point.Selected))
        {
          foreach (ShapePoint point2 in this.points)
          {
            point2.Selected = false;
            point2.ControlPoint1.Selected = false;
            point2.ControlPoint2.Selected = false;
          }
        }
        if (this.point != null)
          this.mouseDown = false;
        if (e.Button != MouseButtons.Right || this.point == null)
          return;
        if (this.pointType == RadShapeEditorControl.PointTypes.Point && this.point is ShapePoint)
        {
          this.menuItemAnchorLeft.Checked = (this.point.Anchor & AnchorStyles.Left) != AnchorStyles.None;
          this.menuItemAnchorRight.Checked = (this.point.Anchor & AnchorStyles.Right) != AnchorStyles.None;
          this.menuItemAnchorTop.Checked = (this.point.Anchor & AnchorStyles.Top) != AnchorStyles.None;
          this.menuItemAnchorBottom.Checked = (this.point.Anchor & AnchorStyles.Bottom) != AnchorStyles.None;
          if (this.points.Count <= 2)
          {
            this.menuItemRemoveLine.Enabled = false;
            this.menuItemRemovePoint.Enabled = false;
          }
          this.menuItemLocked.Checked = this.point.Locked;
          if ((this.point as ShapePoint).Bezier)
            this.contextMenuPoint.Items[1].Text = "Convert to Line";
          else
            this.contextMenuPoint.Items[1].Text = "Convert to Bezier Curve";
          this.contextMenuPoint.Show(this.PointToScreen(new Point(e.X, e.Y)));
        }
        else
        {
          if (this.pointType != RadShapeEditorControl.PointTypes.Line)
            return;
          if ((this.point as ShapePoint).Bezier)
            this.contextMenuLine.Items[1].Text = "Convert to Line";
          else
            this.contextMenuLine.Items[1].Text = "Convert to Bezier Curve";
          this.contextMenuLine.Show(this.PointToScreen(new Point(e.X, e.Y)));
        }
      }
    }

    private void RadShapeEditorControl_MouseMove(object sender, MouseEventArgs e)
    {
      this.oldCurPoint = this.curPoint;
      this.curPoint = new Point(e.X, e.Y);
      if (e.Button != MouseButtons.Left || !(this.oldCurPoint != this.curPoint))
        return;
      Point point1 = new Point((int) Math.Round((double) this.curPoint.X / Math.Pow((double) this.zoomFactor, 2.0)) + this.xOverflowOffset, (int) Math.Round((double) this.curPoint.Y / Math.Pow((double) this.zoomFactor, 2.0)) + this.yOverflowOffset);
      this.snapToGrid.SnapPtToGrid(new Point(point1.X, point1.Y));
      if (this.point != null && this.pointType != RadShapeEditorControl.PointTypes.Line && !this.point.Locked)
      {
        this.point.X = (float) (int) this.snapToGrid.SnappedPoint.X;
        this.point.Y = (float) (int) this.snapToGrid.SnappedPoint.Y;
        this.propertyGrid.Refresh();
      }
      if (!this.mouseDown)
      {
        int num1 = this.curPoint.X - this.oldCurPoint.X;
        int num2 = this.curPoint.Y - this.oldCurPoint.Y;
        foreach (ShapePoint point2 in this.points)
        {
          if (point2 != this.point && point2.Selected && !point2.Locked)
          {
            point2.X += (float) num1;
            point2.Y += (float) num2;
          }
          if (point2.Bezier)
          {
            if (point2.ControlPoint1.Selected && !point2.ControlPoint1.Locked)
            {
              point2.ControlPoint1.X += (float) num1;
              point2.ControlPoint1.Y += (float) num2;
            }
            if (point2.ControlPoint2.Selected && !point2.ControlPoint2.Locked)
            {
              point2.ControlPoint2.X += (float) num1;
              point2.ControlPoint2.Y += (float) num2;
            }
          }
        }
      }
      this.Refresh();
    }

    private void RadShapeEditorControl_MouseUp(object sender, MouseEventArgs e)
    {
      this.Cursor = Cursors.Arrow;
      if (!this.mouseDown)
        return;
      this.mouseDown = false;
      Rectangle rectangle = new Rectangle(this.downPoint.X < this.curPoint.X ? this.downPoint.X : this.curPoint.X, this.downPoint.Y < this.curPoint.Y ? this.downPoint.Y : this.curPoint.Y, Math.Abs(this.downPoint.X - this.curPoint.X), Math.Abs(this.downPoint.Y - this.curPoint.Y));
      rectangle.X = (int) Math.Round((double) rectangle.X / Math.Pow((double) this.zoomFactor, 2.0)) + this.xOverflowOffset;
      rectangle.Y = (int) Math.Round((double) rectangle.Y / Math.Pow((double) this.zoomFactor, 2.0)) + this.yOverflowOffset;
      rectangle.Width = (int) Math.Round((double) rectangle.Width / (double) this.zoomFactor);
      rectangle.Height = (int) Math.Round((double) rectangle.Height / (double) this.zoomFactor);
      foreach (ShapePoint point in this.points)
      {
        point.Selected = this.point == null && rectangle.Contains(point.GetPoint());
        if (point.Bezier)
        {
          point.ControlPoint1.Selected = this.point == null && rectangle.Contains(point.ControlPoint1.GetPoint());
          point.ControlPoint2.Selected = this.point == null && rectangle.Contains(point.ControlPoint2.GetPoint());
        }
        else
        {
          point.ControlPoint1.Selected = false;
          point.ControlPoint2.Selected = false;
        }
      }
      this.Refresh();
    }

    private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
      if (this.propertyGrid.SelectedObject is ShapePoint)
      {
        int index = this.points.IndexOf(this.point as ShapePoint);
        this.points[index] = this.propertyGrid.SelectedObject as ShapePoint;
        this.point = (ShapePointBase) this.points[index];
      }
      this.Refresh();
    }

    private void menuItemAnchorLeft_Click(object sender, EventArgs e)
    {
      this.point.Anchor ^= AnchorStyles.Left;
      this.propertyGrid.Refresh();
    }

    private void menuItemAnchorRight_Click(object sender, EventArgs e)
    {
      this.point.Anchor ^= AnchorStyles.Right;
      this.propertyGrid.Refresh();
    }

    private void menuItemAnchorTop_Click(object sender, EventArgs e)
    {
      this.point.Anchor ^= AnchorStyles.Top;
      this.propertyGrid.Refresh();
    }

    private void menuItemAnchorBottom_Click(object sender, EventArgs e)
    {
      this.point.Anchor ^= AnchorStyles.Bottom;
      this.propertyGrid.Refresh();
    }

    private void menuItemRemoveLine_Click(object sender, EventArgs e)
    {
      if (this.points.Count <= 2)
        return;
      this.points.Remove(this.point as ShapePoint);
      this.point = (ShapePointBase) null;
      this.propertyGrid.SelectedObject = (object) null;
      this.Refresh();
    }

    private void menuItemRemovePoint_Click(object sender, EventArgs e)
    {
      if (this.points.Count <= 2)
        return;
      this.points.Remove(this.point as ShapePoint);
      this.point = (ShapePointBase) null;
      this.propertyGrid.SelectedObject = (object) null;
      this.Refresh();
    }

    private void menuItemConvert_Click(object sender, EventArgs e)
    {
      (this.point as ShapePoint).Bezier = !(this.point as ShapePoint).Bezier;
      if ((this.point as ShapePoint).Bezier)
      {
        int index = this.points.IndexOf(this.point as ShapePoint) + 1;
        if (index >= this.points.Count)
          index = 0;
        (this.point as ShapePoint).CreateBezier((ShapePointBase) this.points[index]);
      }
      this.propertyGrid.Refresh();
      this.Refresh();
    }

    private void menuItemAddPoint_Click(object sender, EventArgs e)
    {
      int index = this.points.IndexOf(this.point as ShapePoint) + 1;
      if (index >= this.points.Count)
        index = 0;
      this.points.Insert(index, new ShapePoint(this.downPoint.X, this.downPoint.Y));
      this.point = (ShapePointBase) this.points[index];
      this.propertyGrid.SelectedObject = (object) this.point;
      this.Refresh();
    }

    private void menuItemLeftTopCorner_Click(object sender, EventArgs e)
    {
      this.point.X = (float) this.dimension.X;
      this.point.Y = (float) this.dimension.Y;
      this.point.Anchor = AnchorStyles.Top | AnchorStyles.Left;
      this.point.Locked = true;
      this.Refresh();
    }

    private void menuItemRightTopCorner_Click(object sender, EventArgs e)
    {
      this.point.X = (float) this.dimension.Right;
      this.point.Y = (float) this.dimension.Y;
      this.point.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.point.Locked = true;
      this.Refresh();
    }

    private void menuItemLeftBottomCorner_Click(object sender, EventArgs e)
    {
      this.point.X = (float) this.dimension.X;
      this.point.Y = (float) this.dimension.Bottom;
      this.point.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.point.Locked = true;
      this.Refresh();
    }

    private void menuItemRightBottomCorner_Click(object sender, EventArgs e)
    {
      this.point.X = (float) this.dimension.Right;
      this.point.Y = (float) this.dimension.Bottom;
      this.point.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.point.Locked = true;
      this.Refresh();
    }

    private void menuItemLocked_Click(object sender, EventArgs e)
    {
      this.point.Locked = !this.point.Locked;
    }

    public CustomShape GetShape()
    {
      CustomShape customShape = new CustomShape();
      customShape.Dimension = this.Dimension;
      foreach (ShapePoint point in this.Points)
        customShape.Points.Add(point);
      return customShape;
    }

    private void horizontallyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int index = this.points.IndexOf(this.point as ShapePoint) + 1;
      if (index >= this.points.Count)
        index = 0;
      Rectangle dimension = this.dimension;
      int num = dimension.Left + dimension.Width / 2;
      ShapePoint shapePoint = new ShapePoint();
      shapePoint.X = this.point.X + (float) (2.0 * ((double) num - (double) this.point.X));
      shapePoint.Y = this.point.Y;
      this.points.Insert(index, shapePoint);
      this.point = (ShapePointBase) this.points[index];
      this.propertyGrid.SelectedObject = (object) this.point;
      this.Refresh();
    }

    private void verticallyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int index = this.points.IndexOf(this.point as ShapePoint) + 1;
      if (index >= this.points.Count)
        index = 0;
      Rectangle dimension = this.dimension;
      int num = dimension.Top + dimension.Height / 2;
      ShapePoint shapePoint = new ShapePoint();
      shapePoint.X = this.point.X;
      shapePoint.Y = this.point.Y + (float) (2.0 * ((double) num - (double) this.point.Y));
      this.points.Insert(index, shapePoint);
      this.point = (ShapePointBase) this.points[index];
      this.propertyGrid.SelectedObject = (object) this.point;
      this.Refresh();
    }

    private void horizontallyToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      int num = (int) MessageBox.Show("Please select the point to be used as a reference for the symmmetry", "Shape Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      this.isSymmetricPointMode = true;
      this.newSymmetricPoint = this.point;
      this.isVerticalSymmetry = false;
    }

    private void verticallyToolStripMenuItem1_Click(object sender, EventArgs e)
    {
      int num = (int) MessageBox.Show("Please select the point to be used as a reference for the symmmetry", "Shape Editor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      this.isSymmetricPointMode = true;
      this.isVerticalSymmetry = true;
      this.newSymmetricPoint = this.point;
    }

    private void RadShapeEditorControl_KeyPress(object sender, KeyPressEventArgs e)
    {
      if ((int) e.KeyChar == (int) Convert.ToChar("+"))
      {
        if ((double) this.snapToGrid.FieldWidth < 100.0)
          this.snapToGrid.FieldWidth *= 2f;
        this.Refresh();
      }
      else
      {
        if ((int) e.KeyChar != (int) Convert.ToChar("-"))
          return;
        if ((double) this.snapToGrid.FieldWidth > 8.0)
          this.snapToGrid.FieldWidth /= 2f;
        this.Refresh();
      }
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (e.Delta > 0)
      {
        if ((double) this.zoomFactor < 2.0)
        {
          this.zoomFactor += 0.2f;
          this.DRAWABLE_GRID_LINE_WIDTH = (int) Math.Round(20.0 * (double) this.zoomFactor);
          this.Refresh();
        }
      }
      else if (e.Delta < 0 && (double) this.zoomFactor > 1.0)
      {
        this.zoomFactor -= 0.2f;
        this.DRAWABLE_GRID_LINE_WIDTH = (int) Math.Round(20.0 * (double) this.zoomFactor);
        this.Refresh();
      }
      base.OnMouseWheel(e);
    }

    private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
    {
      this.yOverflowOffset = this.vScrollBar1.Value = e.NewValue;
      this.Refresh();
    }

    private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
    {
      this.xOverflowOffset = this.hScrollBar1.Value = e.NewValue;
      this.Refresh();
    }

    private void SizeScrollBars()
    {
      this.hScrollBar1.Minimum = this.minWidth;
      this.vScrollBar1.Minimum = this.minHeight;
      if (this.xOverflowOffset < this.hScrollBar1.Minimum)
        this.xOverflowOffset = this.hScrollBar1.Minimum;
      if (this.yOverflowOffset < this.vScrollBar1.Minimum)
        this.yOverflowOffset = this.vScrollBar1.Minimum;
      this.hScrollBar1.SetBounds(0, this.ClientRectangle.Height - this.hScrollBar1.Height, this.ClientRectangle.Width, this.hScrollBar1.Height);
      this.vScrollBar1.SetBounds(this.ClientRectangle.Right - this.vScrollBar1.Width, 0, this.vScrollBar1.Width, this.ClientRectangle.Height - this.hScrollBar1.Height);
      this.hScrollBar1.Maximum = (int) Math.Round((double) this.maxWidth * (double) this.zoomFactor * (double) this.zoomFactor - (double) this.ClientRectangle.Width) + this.vScrollBar1.Width * 2;
      this.vScrollBar1.Maximum = (int) Math.Round((double) this.maxHeight * (double) this.zoomFactor * (double) this.zoomFactor - (double) this.ClientRectangle.Height) + this.hScrollBar1.Height * 2;
      if (this.xOverflowOffset > this.hScrollBar1.Maximum)
        this.xOverflowOffset = this.hScrollBar1.Maximum;
      if (this.yOverflowOffset > this.vScrollBar1.Maximum)
        this.yOverflowOffset = this.vScrollBar1.Maximum;
      this.vScrollBar1.Refresh();
      this.hScrollBar1.Refresh();
    }

    private void vScrollBar1_ValueChanged(object sender, EventArgs e)
    {
      this.yOverflowOffset = this.vScrollBar1.Value;
      this.Refresh();
    }

    private void hScrollBar1_ValueChanged(object sender, EventArgs e)
    {
      this.xOverflowOffset = this.hScrollBar1.Value;
      this.Refresh();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.contextMenuPoint = new ContextMenuStrip(this.components);
      this.menuItemRemovePoint = new ToolStripMenuItem();
      this.menuItemConvert = new ToolStripMenuItem();
      this.anchorStylesToolStripMenuItem = new ToolStripMenuItem();
      this.menuItemAnchorLeft = new ToolStripMenuItem();
      this.menuItemAnchorRight = new ToolStripMenuItem();
      this.menuItemAnchorTop = new ToolStripMenuItem();
      this.menuItemAnchorBottom = new ToolStripMenuItem();
      this.snapToToolStripMenuItem = new ToolStripMenuItem();
      this.menuItemLeftTopCorner = new ToolStripMenuItem();
      this.menuItemRightTopCorner = new ToolStripMenuItem();
      this.menuItemLeftBottomCorner = new ToolStripMenuItem();
      this.menuItemRightBottomCorner = new ToolStripMenuItem();
      this.menuItemLocked = new ToolStripMenuItem();
      this.creToolStripMenuItem = new ToolStripMenuItem();
      this.horizontallyToolStripMenuItem = new ToolStripMenuItem();
      this.verticallyToolStripMenuItem = new ToolStripMenuItem();
      this.makeSymmetricToolStripMenuItem = new ToolStripMenuItem();
      this.horizontallyToolStripMenuItem1 = new ToolStripMenuItem();
      this.verticallyToolStripMenuItem1 = new ToolStripMenuItem();
      this.contextMenuLine = new ContextMenuStrip(this.components);
      this.menuItemRemoveLine = new ToolStripMenuItem();
      this.menuItemConvertLine = new ToolStripMenuItem();
      this.menuItemAddPoint = new ToolStripMenuItem();
      this.hScrollBar1 = new HScrollBar();
      this.vScrollBar1 = new VScrollBar();
      this.contextMenuPoint.SuspendLayout();
      this.contextMenuLine.SuspendLayout();
      this.SuspendLayout();
      this.contextMenuPoint.Items.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.menuItemRemovePoint,
        (ToolStripItem) this.menuItemConvert,
        (ToolStripItem) this.anchorStylesToolStripMenuItem,
        (ToolStripItem) this.snapToToolStripMenuItem,
        (ToolStripItem) this.menuItemLocked,
        (ToolStripItem) this.creToolStripMenuItem,
        (ToolStripItem) this.makeSymmetricToolStripMenuItem
      });
      this.contextMenuPoint.Name = "contextMenuPoint";
      this.contextMenuPoint.Size = new Size(217, 158);
      this.menuItemRemovePoint.Name = "menuItemRemovePoint";
      this.menuItemRemovePoint.Size = new Size(216, 22);
      this.menuItemRemovePoint.Text = "Remove Point";
      this.menuItemConvert.Name = "menuItemConvert";
      this.menuItemConvert.Size = new Size(216, 22);
      this.menuItemConvert.Text = "Convert to Bezier Curve";
      this.anchorStylesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.menuItemAnchorLeft,
        (ToolStripItem) this.menuItemAnchorRight,
        (ToolStripItem) this.menuItemAnchorTop,
        (ToolStripItem) this.menuItemAnchorBottom
      });
      this.anchorStylesToolStripMenuItem.Name = "anchorStylesToolStripMenuItem";
      this.anchorStylesToolStripMenuItem.Size = new Size(216, 22);
      this.anchorStylesToolStripMenuItem.Text = "Anchor styles";
      this.menuItemAnchorLeft.Name = "menuItemAnchorLeft";
      this.menuItemAnchorLeft.Size = new Size(119, 22);
      this.menuItemAnchorLeft.Text = "Left";
      this.menuItemAnchorRight.Name = "menuItemAnchorRight";
      this.menuItemAnchorRight.Size = new Size(119, 22);
      this.menuItemAnchorRight.Text = "Right";
      this.menuItemAnchorTop.Name = "menuItemAnchorTop";
      this.menuItemAnchorTop.Size = new Size(119, 22);
      this.menuItemAnchorTop.Text = "Top";
      this.menuItemAnchorBottom.Name = "menuItemAnchorBottom";
      this.menuItemAnchorBottom.Size = new Size(119, 22);
      this.menuItemAnchorBottom.Text = "Bottom";
      this.snapToToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.menuItemLeftTopCorner,
        (ToolStripItem) this.menuItemRightTopCorner,
        (ToolStripItem) this.menuItemLeftBottomCorner,
        (ToolStripItem) this.menuItemRightBottomCorner
      });
      this.snapToToolStripMenuItem.Name = "snapToToolStripMenuItem";
      this.snapToToolStripMenuItem.Size = new Size(216, 22);
      this.snapToToolStripMenuItem.Text = "Snap to";
      this.menuItemLeftTopCorner.Name = "menuItemLeftTopCorner";
      this.menuItemLeftTopCorner.Size = new Size(177, 22);
      this.menuItemLeftTopCorner.Text = "LeftTop Corner";
      this.menuItemRightTopCorner.Name = "menuItemRightTopCorner";
      this.menuItemRightTopCorner.Size = new Size(177, 22);
      this.menuItemRightTopCorner.Text = "RightTop Corner";
      this.menuItemLeftBottomCorner.Name = "menuItemLeftBottomCorner";
      this.menuItemLeftBottomCorner.Size = new Size(177, 22);
      this.menuItemLeftBottomCorner.Text = "LeftBottom Corner";
      this.menuItemRightBottomCorner.Name = "menuItemRightBottomCorner";
      this.menuItemRightBottomCorner.Size = new Size(177, 22);
      this.menuItemRightBottomCorner.Text = "RightBottomCorner";
      this.menuItemLocked.Name = "menuItemLocked";
      this.menuItemLocked.Size = new Size(216, 22);
      this.menuItemLocked.Text = "Locked";
      this.creToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.horizontallyToolStripMenuItem,
        (ToolStripItem) this.verticallyToolStripMenuItem
      });
      this.creToolStripMenuItem.Name = "creToolStripMenuItem";
      this.creToolStripMenuItem.Size = new Size(216, 22);
      this.creToolStripMenuItem.Text = "Create Symmetric Point";
      this.horizontallyToolStripMenuItem.Name = "horizontallyToolStripMenuItem";
      this.horizontallyToolStripMenuItem.Size = new Size(141, 22);
      this.horizontallyToolStripMenuItem.Text = "Horizontally";
      this.horizontallyToolStripMenuItem.Click += new EventHandler(this.horizontallyToolStripMenuItem_Click);
      this.verticallyToolStripMenuItem.Name = "verticallyToolStripMenuItem";
      this.verticallyToolStripMenuItem.Size = new Size(141, 22);
      this.verticallyToolStripMenuItem.Text = "Vertically";
      this.verticallyToolStripMenuItem.Click += new EventHandler(this.verticallyToolStripMenuItem_Click);
      this.makeSymmetricToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.horizontallyToolStripMenuItem1,
        (ToolStripItem) this.verticallyToolStripMenuItem1
      });
      this.makeSymmetricToolStripMenuItem.Name = "makeSymmetricToolStripMenuItem";
      this.makeSymmetricToolStripMenuItem.Size = new Size(216, 22);
      this.makeSymmetricToolStripMenuItem.Text = "Make Point Symmetric To...";
      this.horizontallyToolStripMenuItem1.Name = "horizontallyToolStripMenuItem1";
      this.horizontallyToolStripMenuItem1.Size = new Size(141, 22);
      this.horizontallyToolStripMenuItem1.Text = "Horizontally";
      this.horizontallyToolStripMenuItem1.Click += new EventHandler(this.horizontallyToolStripMenuItem1_Click);
      this.verticallyToolStripMenuItem1.Name = "verticallyToolStripMenuItem1";
      this.verticallyToolStripMenuItem1.Size = new Size(141, 22);
      this.verticallyToolStripMenuItem1.Text = "Vertically";
      this.verticallyToolStripMenuItem1.Click += new EventHandler(this.verticallyToolStripMenuItem1_Click);
      this.contextMenuLine.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.menuItemRemoveLine,
        (ToolStripItem) this.menuItemConvertLine,
        (ToolStripItem) this.menuItemAddPoint
      });
      this.contextMenuLine.Name = "contextMenuLine";
      this.contextMenuLine.Size = new Size(202, 70);
      this.menuItemRemoveLine.Name = "menuItemRemoveLine";
      this.menuItemRemoveLine.Size = new Size(201, 22);
      this.menuItemRemoveLine.Text = "Remove Line";
      this.menuItemConvertLine.Name = "menuItemConvertLine";
      this.menuItemConvertLine.Size = new Size(201, 22);
      this.menuItemConvertLine.Text = "Convert to Bezier Curve";
      this.menuItemAddPoint.Name = "menuItemAddPoint";
      this.menuItemAddPoint.Size = new Size(201, 22);
      this.menuItemAddPoint.Text = "Add Point";
      this.hScrollBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.hScrollBar1.Location = new Point(0, 133);
      this.hScrollBar1.Name = "hScrollBar1";
      this.hScrollBar1.Size = new Size(133, 16);
      this.hScrollBar1.TabIndex = 2;
      this.hScrollBar1.ValueChanged += new EventHandler(this.hScrollBar1_ValueChanged);
      this.hScrollBar1.Scroll += new ScrollEventHandler(this.hScrollBar1_Scroll);
      this.vScrollBar1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      this.vScrollBar1.Location = new Point(133, 0);
      this.vScrollBar1.Name = "vScrollBar1";
      this.vScrollBar1.Size = new Size(16, 134);
      this.vScrollBar1.TabIndex = 3;
      this.vScrollBar1.ValueChanged += new EventHandler(this.vScrollBar1_ValueChanged);
      this.vScrollBar1.Scroll += new ScrollEventHandler(this.vScrollBar1_Scroll);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.AutoScroll = true;
      this.Controls.Add((Control) this.vScrollBar1);
      this.Controls.Add((Control) this.hScrollBar1);
      this.Name = nameof (RadShapeEditorControl);
      this.Load += new EventHandler(this.RadShapeEditorControl_Load);
      this.MouseDown += new MouseEventHandler(this.RadShapeEditorControl_MouseDown);
      this.MouseMove += new MouseEventHandler(this.RadShapeEditorControl_MouseMove);
      this.KeyPress += new KeyPressEventHandler(this.RadShapeEditorControl_KeyPress);
      this.MouseUp += new MouseEventHandler(this.RadShapeEditorControl_MouseUp);
      this.contextMenuPoint.ResumeLayout(false);
      this.contextMenuLine.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private enum PointTypes
    {
      Point,
      ControlPoint,
      Line,
    }
  }
}
