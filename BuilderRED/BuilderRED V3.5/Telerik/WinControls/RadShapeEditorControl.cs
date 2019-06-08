// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadShapeEditorControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  [ToolboxItem(false)]
  public class RadShapeEditorControl : UserControl
  {
    private bool drawsGuideLines = true;
    private const float snapDistConst = 10f;
    private Rectangle dimension;
    private ShapeLinesCollection shape;
    private SnapToGrid snapToGrid;
    private ShapeEditorZoom zoom;
    private PointF snappedPoint;
    private ShapePoint snappedCtrlPoint;
    private IShapeCurve snappedCurve;
    private uint snapStates;
    private bool snappingOccured;
    private bool isDragging;
    private bool isScrolling;
    private PointF scrollStartPos;
    private PointF areaStartPos;
    private Pen gridDrawPen;
    private Pen PenSnappedPoint;
    private Pen PenSnappedCtrlPoint;
    private Pen PenSnappedCtrlPointLocked;
    private Pen PenSnappedPointLine;
    private Pen PenSnappedLine;
    private Pen PenShape;
    private Brush BrushControlPoints;
    private Pen PenControlLines;
    private Pen PenLinesExtensions;
    private Brush BrushControlLinesPts;
    private Brush DimensionRectFill;
    private Brush DimensionHelperRects;
    private Pen DimensionRectLines;
    private Timer scrollTimer;
    public PropertyGrid propertyGrid;
    internal bool debugMode;
    internal string scrolls;
    internal string mousePos;
    private IContainer components;
    private ContextMenuStrip contextMenuPoint;
    private ToolStripMenuItem pointMenuItemDelete;
    private ToolStripMenuItem lockToolStripMenuItem;
    private ToolStripMenuItem topLeftCornerToolStripMenuItem;
    private ToolStripMenuItem pointMenuItemLocked;
    private ToolStripMenuItem topRightCorToolStripMenuItem;
    private ToolStripMenuItem bottomLeftCrnerToolStripMenuItem;
    private ToolStripMenuItem bottomRightCornerToolStripMenuItem;
    private ContextMenuStrip contextMenuCurve;
    private ToolStripMenuItem lineMenuItemConvert;
    private ToolStripMenuItem insertPointToolStripMenuItem;
    private ContextMenuStrip contextMenuGeneral;
    private ToolStripMenuItem zoomInToolStripMenuItem;
    private ToolStripMenuItem zoomOutToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem actualPixelSizeToolStripMenuItem;
    private ToolStripMenuItem fitToScreenToolStripMenuItem;
    private ToolStripMenuItem fitBoundsToScreenMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem extendBoundsToFitShapeToolStripMenuItem;

    public event SnapChangedEventHandler SnapChanged;

    public event ZoomChangedEventHandler ZoomChanged;

    private object SelectedProperty
    {
      get
      {
        if (this.propertyGrid == null)
          return (object) null;
        return this.propertyGrid.SelectedObject;
      }
      set
      {
        if (this.propertyGrid == null)
          return;
        this.propertyGrid.SelectedObject = value;
      }
    }

    public ShapeLinesCollection Shape
    {
      get
      {
        return this.shape;
      }
      set
      {
        if (value == null)
          return;
        this.shape = value;
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
      }
    }

    public float GridSize
    {
      get
      {
        return this.snapToGrid.FieldWidth;
      }
      set
      {
        if ((double) value < 10.0 || (double) value > 500.0)
          return;
        this.snapToGrid.FieldWidth = value;
        this.Refresh();
      }
    }

    private void StopOnDebug()
    {
      if (!this.debugMode)
        return;
      this.debugMode = false;
    }

    private void PrintDebugInformation(PaintEventArgs e)
    {
      if (this.scrolls != null)
        e.Graphics.DrawString(this.scrolls, new Font("Verdana", 12f, FontStyle.Bold, GraphicsUnit.Pixel, (byte) 0), (Brush) new SolidBrush(Color.Green), new PointF(0.0f, 0.0f));
      if (this.mousePos == null)
        return;
      e.Graphics.DrawString(this.mousePos, new Font("Verdana", 12f, FontStyle.Bold, GraphicsUnit.Pixel, (byte) 0), (Brush) new SolidBrush(Color.Green), new PointF(0.0f, 16f));
    }

    public RadShapeEditorControl()
    {
      this.InitializeComponent();
      this.InitPensAndBrushes();
      this.InitZoomAndAutoScroll();
      this.InitSnapToGrid();
      this.InitShape();
      this.Cursor = Cursors.Cross;
      this.SetSnapping(RadShapeEditorControl.SnapTypes.SnapToGrid | RadShapeEditorControl.SnapTypes.SnapToCtrl | RadShapeEditorControl.SnapTypes.SnapToCurves | RadShapeEditorControl.SnapTypes.SnapToExtensions);
      this.SetStyle(ControlStyles.ContainerControl | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.InitScrollTimer();
    }

    public void Reset()
    {
      this.snappingOccured = false;
      this.drawsGuideLines = true;
      this.isDragging = false;
      this.isScrolling = false;
      this.snappedPoint = new PointF(0.0f, 0.0f);
      this.snappedCtrlPoint = (ShapePoint) null;
      this.snappedCurve = (IShapeCurve) null;
      this.zoom.ZoomFactor = 1f;
      this.snapToGrid.SnapRelative = 0.2f;
      this.SetSnapping(RadShapeEditorControl.SnapTypes.SnapToGrid | RadShapeEditorControl.SnapTypes.SnapToCtrl | RadShapeEditorControl.SnapTypes.SnapToCurves | RadShapeEditorControl.SnapTypes.SnapToExtensions);
      this.scrollStartPos = new PointF(0.0f, 0.0f);
      this.areaStartPos = new PointF(0.0f, 0.0f);
    }

    public bool GridSnap
    {
      get
      {
        return this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToGrid);
      }
      set
      {
        if (value == this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToGrid))
          return;
        if (value)
          this.SetSnapping(RadShapeEditorControl.SnapTypes.SnapToGrid);
        else
          this.ClrSnapping(RadShapeEditorControl.SnapTypes.SnapToGrid);
        this.OnSnapChanged(new SnapChangedEventArgs(RadShapeEditorControl.SnapTypes.SnapToGrid));
        this.Refresh();
      }
    }

    public bool CtrlPointsSnap
    {
      get
      {
        return this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToCtrl);
      }
      set
      {
        if (value == this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToCtrl))
          return;
        if (value)
          this.SetSnapping(RadShapeEditorControl.SnapTypes.SnapToCtrl);
        else
          this.ClrSnapping(RadShapeEditorControl.SnapTypes.SnapToCtrl);
        this.OnSnapChanged(new SnapChangedEventArgs(RadShapeEditorControl.SnapTypes.SnapToCtrl));
        this.Refresh();
      }
    }

    public bool CurvesSnap
    {
      get
      {
        return this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToCurves);
      }
      set
      {
        if (value == this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToCurves))
          return;
        if (value)
          this.SetSnapping(RadShapeEditorControl.SnapTypes.SnapToCurves);
        else
          this.ClrSnapping(RadShapeEditorControl.SnapTypes.SnapToCurves);
        this.OnSnapChanged(new SnapChangedEventArgs(RadShapeEditorControl.SnapTypes.SnapToCurves));
        this.Refresh();
      }
    }

    public bool ExtensionsSnap
    {
      get
      {
        return this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToExtensions);
      }
      set
      {
        if (value == this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToExtensions))
          return;
        if (value)
          this.SetSnapping(RadShapeEditorControl.SnapTypes.SnapToExtensions);
        else
          this.ClrSnapping(RadShapeEditorControl.SnapTypes.SnapToExtensions);
        this.OnSnapChanged(new SnapChangedEventArgs(RadShapeEditorControl.SnapTypes.SnapToExtensions));
        this.Refresh();
      }
    }

    public float Zoom(PointF center, float factor)
    {
      if ((double) factor == (double) this.zoom.ZoomFactor)
        return factor;
      float zoomFactor = this.zoom.ZoomFactor;
      PointF pt = this.zoom.VisibleToPt(center);
      if (!this.zoom.Zoom(factor))
        return this.zoom.ZoomFactor;
      this.snapToGrid.SnapRelative = 0.2f / this.zoom.ZoomFactor;
      this.zoom.ZoomAtPoint(pt, center);
      if ((double) zoomFactor != (double) this.zoom.ZoomFactor)
        this.OnZoomChanged(new ZoomChangedEventArgs(this.zoom.ZoomFactor));
      this.Refresh();
      return this.zoom.ZoomFactor;
    }

    public float ZoomCenter(float factor)
    {
      return this.Zoom(new PointF(this.zoom.VisibleArea.Width / 2f, this.zoom.VisibleArea.Height / 2f), factor);
    }

    private void OnPaint(object sender, PaintEventArgs e)
    {
      this.DrawDimensionRectAndGrid(e);
      this.DrawExtensions(e);
      this.DrawShape(e);
      this.DrawSnappedPoint(e);
    }

    private void DrawGrid(PaintEventArgs e)
    {
      float num1 = this.snapToGrid.FieldWidth * this.zoom.ZoomFactor;
      Point pt1_1 = new Point();
      Point pt2_1 = new Point();
      Point pt1_2 = new Point();
      Point pt2_2 = new Point();
      pt1_2.X = 0;
      pt2_2.X = this.ClientRectangle.Width;
      pt1_1.Y = 0;
      pt2_1.Y = this.ClientRectangle.Height;
      Size size = new Size(-(this.zoom.Location.X % (int) Math.Round((double) num1)), -(this.zoom.Location.Y % (int) Math.Round((double) num1)));
      int num2 = Math.Max(this.ClientRectangle.Width, this.ClientRectangle.Height);
      for (float num3 = 0.0f; (double) num3 < (double) num2 + (double) num1; num3 += num1)
      {
        pt2_2.Y = pt1_2.Y = (int) num3 + size.Height;
        pt2_1.X = pt1_1.X = (int) num3 + size.Width;
        e.Graphics.DrawLine(this.gridDrawPen, pt1_2, pt2_2);
        e.Graphics.DrawLine(this.gridDrawPen, pt1_1, pt2_1);
      }
    }

    private void DrawSnappedPoint(PaintEventArgs e)
    {
      if (!this.snappingOccured || this.isScrolling)
        return;
      PointF visible = this.zoom.PtToVisible(this.snappedPoint);
      Rectangle rect = new Rectangle();
      rect.Location = new Point((int) Math.Round((double) visible.X), (int) Math.Round((double) visible.Y));
      e.Graphics.DrawLine(this.PenSnappedPointLine, rect.Location, this.PointToClient(Control.MousePosition));
      rect.Inflate(2, 2);
      e.Graphics.DrawEllipse(this.snappedCtrlPoint == null ? this.PenSnappedPoint : (this.snappedCtrlPoint.IsLocked ? this.PenSnappedCtrlPointLocked : this.PenSnappedCtrlPoint), rect);
    }

    private void DrawDimensionRectAndGrid(PaintEventArgs e)
    {
      RectangleF visible = this.zoom.RectToVisible((RectangleF) this.dimension);
      PointF pointF = new PointF(visible.X + visible.Width / 2f, visible.Y + visible.Height / 2f);
      e.Graphics.FillRectangle(this.DimensionRectFill, visible.X, visible.Y, visible.Width, visible.Height);
      this.DrawGrid(e);
      e.Graphics.DrawRectangle(this.DimensionRectLines, visible.X, visible.Y, visible.Width, visible.Height);
      e.Graphics.FillRectangle(this.DimensionHelperRects, pointF.X - 4f, visible.Y - 4f, 8f, 4f);
      e.Graphics.FillRectangle(this.DimensionHelperRects, pointF.X - 4f, visible.Bottom, 8f, 4f);
      e.Graphics.FillRectangle(this.DimensionHelperRects, visible.X - 4f, pointF.Y - 4f, 4f, 8f);
      e.Graphics.FillRectangle(this.DimensionHelperRects, visible.Right, pointF.Y - 4f, 4f, 8f);
    }

    private void DrawShape(PaintEventArgs e)
    {
      for (int index1 = 0; index1 < this.shape.Lines.Count; ++index1)
      {
        ShapePoint[] points = this.shape.Lines[index1].Points;
        Pen pen = this.shape.Lines[index1] == this.snappedCurve ? this.PenSnappedLine : this.PenShape;
        for (int index2 = 0; index2 < points.Length - 1; ++index2)
          e.Graphics.DrawLine(pen, this.zoom.PtToVisible(points[index2].Location), this.zoom.PtToVisible(points[index2 + 1].Location));
        if (this.drawsGuideLines)
        {
          this.DrawEndPoint(e, this.shape.Lines[index1].LastPoint);
          this.DrawEndPoint(e, this.shape.Lines[index1].LastPoint);
        }
        if (this.shape.Lines[index1] is ShapeBezier)
          this.DrawControlPoints(e, this.shape.Lines[index1] as ShapeBezier);
      }
    }

    private void DrawEndPoint(PaintEventArgs e, ShapePoint pt)
    {
      RectangleF rect = new RectangleF();
      rect.Location = this.zoom.PtToVisible(pt.Location);
      rect.Inflate(2f, 2f);
      e.Graphics.FillEllipse(this.BrushControlPoints, rect);
    }

    private void DrawControlPoints(PaintEventArgs e, ShapeBezier b)
    {
      RectangleF rect = new RectangleF();
      rect.Location = this.zoom.PtToVisible(b.ControlPoints[1].Location);
      rect.Inflate(2f, 2f);
      e.Graphics.DrawLine(this.PenControlLines, this.zoom.PtToVisible(b.ControlPoints[0].Location), this.zoom.PtToVisible(b.ControlPoints[1].Location));
      e.Graphics.FillEllipse(this.BrushControlLinesPts, rect);
      rect.Width = rect.Height = 0.0f;
      rect.Location = this.zoom.PtToVisible(b.ControlPoints[2].Location);
      rect.Inflate(2f, 2f);
      e.Graphics.DrawLine(this.PenControlLines, this.zoom.PtToVisible(b.ControlPoints[3].Location), this.zoom.PtToVisible(b.ControlPoints[2].Location));
      e.Graphics.FillEllipse(this.BrushControlLinesPts, rect);
    }

    private void DrawExtensions(PaintEventArgs e)
    {
      if (!this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToExtensions) || !this.drawsGuideLines)
        return;
      PointF pointF1 = new PointF();
      PointF pointF2 = new PointF();
      for (int index1 = 0; index1 < this.shape.Lines.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.shape.Lines[index1].Extensions.Length; index2 += 2)
        {
          PointF visible1 = this.zoom.PtToVisible(this.shape.Lines[index1].Extensions[index2].Location);
          PointF visible2 = this.zoom.PtToVisible(this.shape.Lines[index1].Extensions[index2 + 1].Location);
          this.ExtendLineToBorders(ref visible1, ref visible2);
          e.Graphics.DrawLine(this.PenLinesExtensions, visible1, visible2);
        }
      }
    }

    private void ExtendLineToBorders(ref PointF from, ref PointF to)
    {
      PointF pointF = new PointF(to.X - from.X, to.Y - from.Y);
      if ((double) pointF.X == 0.0)
      {
        from.Y = 0.0f;
        to.Y = (float) this.ClientRectangle.Height;
      }
      else if ((double) pointF.Y == 0.0)
      {
        from.X = 0.0f;
        to.X = (float) this.ClientRectangle.Width;
      }
      else
      {
        float num = pointF.X / pointF.Y;
        if ((double) Math.Abs(num) <= 1.0)
        {
          from.X -= num * from.Y;
          from.Y = 0.0f;
          to.X += num * ((float) this.ClientRectangle.Height - to.Y);
          to.Y = (float) this.ClientRectangle.Height;
        }
        else
        {
          from.Y -= from.X / num;
          from.X = 0.0f;
          to.Y += ((float) this.ClientRectangle.Width - to.X) / num;
          to.X = (float) this.ClientRectangle.Width;
        }
      }
    }

    protected void OnMouseMove(object sender, MouseEventArgs e)
    {
      this.ActivateShapeEditor();
      this.snappingOccured = false;
      this.StopOnDebug();
      if (this.isScrolling)
      {
        this.zoom.Location = new Point((int) Math.Round((double) this.scrollStartPos.X - (double) e.Location.X + (double) this.areaStartPos.X), (int) Math.Round((double) this.scrollStartPos.Y - (double) e.Location.Y + (double) this.areaStartPos.Y));
        this.Refresh();
      }
      if (this.isDragging)
      {
        if (!this.ClientRectangle.Contains(e.Location) && !this.scrollTimer.Enabled)
          this.scrollTimer.Enabled = true;
        this.snappingOccured = this.DoSnap(this.zoom.VisibleToPt((PointF) e.Location), 10f);
        this.snappedCtrlPoint.Location = this.snappedPoint;
      }
      else
      {
        this.snappingOccured = false;
        this.snappedCtrlPoint = (ShapePoint) null;
        this.snappedCurve = (IShapeCurve) null;
        this.StopOnDebug();
        if (this.snappingOccured = this.DoSnap(this.zoom.VisibleToPt((PointF) e.Location), 10f))
          this.snappedCtrlPoint = this.shape.SnappedCtrlPoint;
      }
      this.Refresh();
    }

    private void ActivateShapeEditor()
    {
      if (!this.CanFocus || this.Focused)
        return;
      this.Focus();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      float factor = this.zoom.ZoomFactor * (Math.Sign(e.Delta) > 0 ? 2f : 0.5f);
      double num = (double) this.Zoom((PointF) e.Location, factor);
      this.Refresh();
    }

    private void OnMouseLeave(object sender, EventArgs e)
    {
      this.snappingOccured = false;
      this.Refresh();
    }

    private void OnMouseDown(object sender, MouseEventArgs e)
    {
      if (this.snappedCtrlPoint != null)
      {
        if ((double) ShapePoint.DistSquared(this.zoom.VisibleToPt((PointF) e.Location), this.snappedCtrlPoint.Location) > 100.0)
          this.snappedCtrlPoint = (ShapePoint) null;
        this.SelectedProperty = (object) this.snappedCtrlPoint;
      }
      if (this.snappedCurve != null)
        this.SelectedProperty = (object) this.snappedCurve;
      if (e.Button == MouseButtons.Left && this.snappedCtrlPoint != null)
      {
        this.snappedCtrlPoint.IsModified = true;
        this.isDragging = true;
      }
      if (e.Button == MouseButtons.Middle || e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.Shift)
      {
        Cursor.Current = Cursors.Hand;
        this.isScrolling = true;
        this.scrollStartPos = (PointF) e.Location;
        this.areaStartPos = (PointF) this.zoom.Location;
      }
      else
      {
        if (e.Button != MouseButtons.Right || this.ShowSnappedPointMenu(e) || this.ShowSnappedLineMenu(e))
          return;
        this.contextMenuGeneral.Show(this.PointToScreen(e.Location));
      }
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
      this.isScrolling = false;
      this.scrollTimer.Enabled = false;
      this.propertyGrid.Refresh();
      if (e.Button == MouseButtons.Left)
      {
        this.isDragging = false;
        if (this.snappedCtrlPoint != null)
          this.snappedCtrlPoint.IsModified = false;
      }
      int button = (int) e.Button;
    }

    private bool ShowSnappedPointMenu(MouseEventArgs e)
    {
      if (this.snappedCtrlPoint == null)
        return false;
      this.pointMenuItemDelete.Enabled = !this.snappedCtrlPoint.IsLocked;
      this.pointMenuItemLocked.Checked = this.snappedCtrlPoint.IsLocked;
      this.contextMenuPoint.Show(this.PointToScreen(e.Location));
      return true;
    }

    private bool ShowSnappedLineMenu(MouseEventArgs e)
    {
      if (this.snappedCurve == null)
        return false;
      this.contextMenuCurve.Show(this.PointToScreen(e.Location));
      return true;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
      this.debugMode = e.Control;
      switch (e.KeyCode)
      {
        case Keys.C:
          this.CtrlPointsSnap = !this.CtrlPointsSnap;
          break;
        case Keys.E:
          this.ExtensionsSnap = !this.ExtensionsSnap;
          break;
        case Keys.G:
          this.GridSnap = !this.GridSnap;
          break;
        case Keys.L:
          this.CurvesSnap = !this.CurvesSnap;
          break;
      }
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
      this.debugMode = e.Control;
    }

    private void OnScroll(object sender, ScrollEventArgs e)
    {
      this.scrolls = string.Format("{0}, {1}", (object) this.HorizontalScroll.Value, (object) this.VerticalScroll.Value);
      this.StopOnDebug();
      this.Refresh();
    }

    public virtual void OnSnapChanged(SnapChangedEventArgs e)
    {
      if (this.SnapChanged == null)
        return;
      this.SnapChanged((object) this, e);
    }

    public virtual void OnZoomChanged(ZoomChangedEventArgs e)
    {
      if (this.ZoomChanged == null)
        return;
      this.ZoomChanged((object) this, e);
    }

    private void InitShape()
    {
      float[] numArray = new float[8]
      {
        64f,
        64f,
        192f,
        64f,
        576f,
        320f,
        64f,
        320f
      };
      ShapePoint[] shapePointArray = new ShapePoint[4];
      this.shape = new ShapeLinesCollection();
      for (int index = 0; index < 4; ++index)
        shapePointArray[index] = new ShapePoint(numArray[index * 2], numArray[index * 2 + 1]);
      this.shape.Add((IShapeCurve) new ShapeLine(shapePointArray[0], shapePointArray[1]));
      this.shape.Add((IShapeCurve) new ShapeLine(shapePointArray[1], shapePointArray[2]));
      this.shape.Add((IShapeCurve) new ShapeLine(shapePointArray[2], shapePointArray[3]));
      this.shape.Add((IShapeCurve) new ShapeLine(shapePointArray[3], shapePointArray[0]));
      this.dimension = new Rectangle(64, 64, 512, 256);
    }

    private void InitZoomAndAutoScroll()
    {
      this.zoom = new ShapeEditorZoom(16f, 16f);
      this.zoom.WorkingArea = new RectangleF(0.0f, 0.0f, 600f, 340f);
      this.zoom.VisibleArea = (RectangleF) this.ClientRectangle;
    }

    private void InitSnapToGrid()
    {
      this.snapToGrid = new SnapToGrid();
      this.snapToGrid.FieldWidth = 32f;
      this.snapToGrid.SnapType = SnapToGrid.SnapTypes.Relative;
      this.snapToGrid.SnapRelative = 0.2f;
    }

    private void InitPensAndBrushes()
    {
      this.gridDrawPen = new Pen(Color.FromArgb(232, 232, 232));
      this.gridDrawPen.DashStyle = DashStyle.Dash;
      this.PenSnappedPoint = new Pen(Color.Red);
      this.PenSnappedCtrlPoint = new Pen(Color.Green);
      this.PenSnappedCtrlPointLocked = new Pen(Color.Gray);
      this.PenSnappedPointLine = new Pen(Color.Gray);
      this.PenSnappedLine = new Pen(Color.BlueViolet);
      this.PenShape = new Pen(Color.Black);
      this.BrushControlPoints = (Brush) new SolidBrush(Color.Black);
      this.PenControlLines = new Pen(Color.DarkRed);
      this.BrushControlLinesPts = (Brush) new SolidBrush(Color.Red);
      this.PenLinesExtensions = new Pen(Color.Cyan, 1f);
      this.PenLinesExtensions.DashStyle = DashStyle.Dash;
      this.DimensionRectFill = (Brush) new SolidBrush(Color.FromArgb(192, 192, 192));
      this.DimensionHelperRects = (Brush) new SolidBrush(Color.Black);
      this.DimensionRectLines = new Pen(Color.Black, 1f);
      this.DimensionRectLines.DashStyle = DashStyle.Solid;
    }

    private void InitScrollTimer()
    {
      this.scrollTimer = new Timer();
      this.scrollTimer.Interval = 20;
      this.scrollTimer.Tick += new EventHandler(this.OnScrollTimer);
    }

    private void SetSnapping(RadShapeEditorControl.SnapTypes flags)
    {
      RadShapeEditorControl shapeEditorControl = this;
      shapeEditorControl.snapStates = (uint) ((RadShapeEditorControl.SnapTypes) shapeEditorControl.snapStates | flags);
    }

    private void ClrSnapping(RadShapeEditorControl.SnapTypes flags)
    {
      RadShapeEditorControl shapeEditorControl = this;
      shapeEditorControl.snapStates = (uint) ((RadShapeEditorControl.SnapTypes) shapeEditorControl.snapStates & ~flags);
    }

    private bool IsSnappingActive(RadShapeEditorControl.SnapTypes flags)
    {
      return ((RadShapeEditorControl.SnapTypes) this.snapStates & flags) != (RadShapeEditorControl.SnapTypes) 0;
    }

    private void ToggleSnapping(RadShapeEditorControl.SnapTypes flags)
    {
      RadShapeEditorControl shapeEditorControl = this;
      shapeEditorControl.snapStates = (uint) ((RadShapeEditorControl.SnapTypes) shapeEditorControl.snapStates ^ flags);
    }

    private void Point_Lock(object sender, EventArgs e)
    {
      if (this.snappedCtrlPoint == null)
        return;
      this.snappedCtrlPoint.IsLocked = !this.snappedCtrlPoint.IsLocked;
    }

    private void Point_Insert(object sender, EventArgs e)
    {
      this.shape.InsertPoint(this.snappedCurve, this.snappedPoint);
    }

    private void Point_Delete(object sender, EventArgs e)
    {
      this.shape.DeletePoint(this.snappedCtrlPoint);
      this.snappedCtrlPoint = (ShapePoint) null;
    }

    private void Curve_Convert(object sender, EventArgs e)
    {
      if (this.snappedCurve == null)
        return;
      this.shape.ConvertCurve(this.snappedCurve);
    }

    private void fitToScreenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.FitShapeToScreen();
    }

    private void fitBoundsToScreenToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.FitBoundsToScreen();
    }

    private void actualPixelSizeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      double num = (double) this.ZoomCenter(1f);
    }

    private void extendBoundsToFitShapeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.ExtendBoundsToFitShape();
    }

    private void OnResize(object sender, EventArgs e)
    {
      this.zoom.VisibleAreaSize = this.ClientRectangle.Size;
    }

    public void UpdateShape()
    {
      this.shape.UpdateShape();
      this.Refresh();
    }

    public void ExtendBoundsToFitShape()
    {
      RectangleF boundingRect = this.shape.GetBoundingRect();
      this.dimension = new Rectangle((int) Math.Round((double) boundingRect.X), (int) Math.Round((double) boundingRect.Y), (int) Math.Round((double) boundingRect.Width), (int) Math.Round((double) boundingRect.Height));
      this.Refresh();
    }

    public void FitBoundsToScreen()
    {
      this.FitToScreen((RectangleF) this.dimension);
    }

    public void FitShapeToScreen()
    {
      this.FitToScreen(this.shape.GetBoundingRect());
    }

    private void OnScrollTimer(object source, EventArgs e)
    {
      Point client = this.PointToClient(Control.MousePosition);
      Point location = this.zoom.Location;
      int num = this.scrollTimer.Interval / 4;
      if (client.X < this.ClientRectangle.Left)
        location.X -= num * (1 + (this.ClientRectangle.Left - client.X) / 10);
      if (client.Y < this.ClientRectangle.Top)
        location.Y -= num * (1 + (this.ClientRectangle.Top - client.Y) / 10);
      if (client.X > this.ClientRectangle.Right)
        location.X += num * (1 + (client.X - this.ClientRectangle.Right) / 10);
      if (client.Y > this.ClientRectangle.Bottom)
        location.Y += num * (1 + (client.Y - this.ClientRectangle.Bottom) / 10);
      this.zoom.Location = location;
      this.DoSnap(this.zoom.VisibleToPt((PointF) client), 10f);
      if (this.snappedCtrlPoint != null)
        this.snappedCtrlPoint.Location = this.snappedPoint;
      this.Refresh();
    }

    private bool DoSnap(PointF location, float snapDist)
    {
      PointF pointF1 = new PointF();
      int type = 0;
      snapDist /= this.zoom.ZoomFactor;
      this.snappedCurve = (IShapeCurve) null;
      if (this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToCtrl) && this.shape.SnapToCtrlPoints(location, snapDist))
      {
        this.snappedPoint = this.shape.SnappedCtrlPoint.Location;
        return true;
      }
      if (this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToGrid))
        type = this.snapToGrid.SnapPtToGrid(location);
      PointF pointF2;
      bool flag;
      if (type > 0)
      {
        pointF2 = this.snapToGrid.SnappedPoint;
        flag = true;
      }
      else
      {
        pointF2 = location;
        flag = false;
      }
      if (type != 0 && this.shape.SnapToGrid(location, pointF2, type, snapDist))
      {
        this.snappedCurve = this.shape.SnappedCurve;
        this.snappedPoint = this.shape.SnappedPoint;
        return true;
      }
      if (this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToCurves) && this.shape.SnapToSegments(pointF2, snapDist))
      {
        this.snappedCurve = this.shape.SnappedCurve;
        this.snappedPoint = this.shape.SnappedPoint;
        return true;
      }
      if (this.IsSnappingActive(RadShapeEditorControl.SnapTypes.SnapToExtensions) && this.shape.SnapToExtensions(pointF2, snapDist))
      {
        this.snappedPoint = this.shape.SnappedPoint;
        return true;
      }
      this.snappedPoint = pointF2;
      return flag;
    }

    private void FitToScreen(RectangleF rect)
    {
      this.zoom.FitToScreen(new RectangleF?(rect));
      this.snapToGrid.SnapRelative = 0.2f / this.zoom.ZoomFactor;
      this.OnZoomChanged(new ZoomChangedEventArgs(this.zoom.ZoomFactor));
      this.Refresh();
    }

    private void ShapeEditorControl_Load(object sender, EventArgs e)
    {
      this.Reset();
      this.FitToScreen((RectangleF) this.dimension);
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
      this.pointMenuItemDelete = new ToolStripMenuItem();
      this.lockToolStripMenuItem = new ToolStripMenuItem();
      this.topLeftCornerToolStripMenuItem = new ToolStripMenuItem();
      this.topRightCorToolStripMenuItem = new ToolStripMenuItem();
      this.bottomLeftCrnerToolStripMenuItem = new ToolStripMenuItem();
      this.bottomRightCornerToolStripMenuItem = new ToolStripMenuItem();
      this.pointMenuItemLocked = new ToolStripMenuItem();
      this.contextMenuCurve = new ContextMenuStrip(this.components);
      this.lineMenuItemConvert = new ToolStripMenuItem();
      this.insertPointToolStripMenuItem = new ToolStripMenuItem();
      this.contextMenuGeneral = new ContextMenuStrip(this.components);
      this.zoomInToolStripMenuItem = new ToolStripMenuItem();
      this.zoomOutToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.actualPixelSizeToolStripMenuItem = new ToolStripMenuItem();
      this.fitToScreenToolStripMenuItem = new ToolStripMenuItem();
      this.fitBoundsToScreenMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.extendBoundsToFitShapeToolStripMenuItem = new ToolStripMenuItem();
      this.contextMenuPoint.SuspendLayout();
      this.contextMenuCurve.SuspendLayout();
      this.contextMenuGeneral.SuspendLayout();
      this.SuspendLayout();
      this.contextMenuPoint.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.pointMenuItemDelete,
        (ToolStripItem) this.lockToolStripMenuItem,
        (ToolStripItem) this.pointMenuItemLocked
      });
      this.contextMenuPoint.Name = "contextMenuPoint";
      this.contextMenuPoint.RenderMode = ToolStripRenderMode.Professional;
      this.contextMenuPoint.Size = new Size((int) sbyte.MaxValue, 70);
      this.pointMenuItemDelete.Name = "pointMenuItemDelete";
      this.pointMenuItemDelete.Size = new Size(126, 22);
      this.pointMenuItemDelete.Text = "Delete";
      this.pointMenuItemDelete.Click += new EventHandler(this.Point_Delete);
      this.lockToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.topLeftCornerToolStripMenuItem,
        (ToolStripItem) this.topRightCorToolStripMenuItem,
        (ToolStripItem) this.bottomLeftCrnerToolStripMenuItem,
        (ToolStripItem) this.bottomRightCornerToolStripMenuItem
      });
      this.lockToolStripMenuItem.Name = "lockToolStripMenuItem";
      this.lockToolStripMenuItem.Size = new Size(126, 22);
      this.lockToolStripMenuItem.Text = "Move To";
      this.topLeftCornerToolStripMenuItem.Name = "topLeftCornerToolStripMenuItem";
      this.topLeftCornerToolStripMenuItem.Size = new Size(185, 22);
      this.topLeftCornerToolStripMenuItem.Text = "Top, Left corner";
      this.topRightCorToolStripMenuItem.Name = "topRightCorToolStripMenuItem";
      this.topRightCorToolStripMenuItem.Size = new Size(185, 22);
      this.topRightCorToolStripMenuItem.Text = "Top, Right corner";
      this.bottomLeftCrnerToolStripMenuItem.Name = "bottomLeftCrnerToolStripMenuItem";
      this.bottomLeftCrnerToolStripMenuItem.Size = new Size(185, 22);
      this.bottomLeftCrnerToolStripMenuItem.Text = "Bottom, Left corner";
      this.bottomRightCornerToolStripMenuItem.Name = "bottomRightCornerToolStripMenuItem";
      this.bottomRightCornerToolStripMenuItem.Size = new Size(185, 22);
      this.bottomRightCornerToolStripMenuItem.Text = "Bottom, Right corner";
      this.pointMenuItemLocked.Name = "pointMenuItemLocked";
      this.pointMenuItemLocked.Size = new Size(126, 22);
      this.pointMenuItemLocked.Text = "Locked";
      this.pointMenuItemLocked.Click += new EventHandler(this.Point_Lock);
      this.contextMenuCurve.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.lineMenuItemConvert,
        (ToolStripItem) this.insertPointToolStripMenuItem
      });
      this.contextMenuCurve.Name = "contextMenuLine";
      this.contextMenuCurve.RenderMode = ToolStripRenderMode.Professional;
      this.contextMenuCurve.Size = new Size(142, 48);
      this.lineMenuItemConvert.Name = "lineMenuItemConvert";
      this.lineMenuItemConvert.Size = new Size(141, 22);
      this.lineMenuItemConvert.Text = "Convert";
      this.lineMenuItemConvert.Click += new EventHandler(this.Curve_Convert);
      this.insertPointToolStripMenuItem.Name = "insertPointToolStripMenuItem";
      this.insertPointToolStripMenuItem.Size = new Size(141, 22);
      this.insertPointToolStripMenuItem.Text = "Insert Point";
      this.insertPointToolStripMenuItem.Click += new EventHandler(this.Point_Insert);
      this.contextMenuGeneral.Items.AddRange(new ToolStripItem[8]
      {
        (ToolStripItem) this.zoomInToolStripMenuItem,
        (ToolStripItem) this.zoomOutToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.actualPixelSizeToolStripMenuItem,
        (ToolStripItem) this.fitToScreenToolStripMenuItem,
        (ToolStripItem) this.fitBoundsToScreenMenuItem,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.extendBoundsToFitShapeToolStripMenuItem
      });
      this.contextMenuGeneral.Name = "contextMenuGeneral";
      this.contextMenuGeneral.Size = new Size(219, 170);
      this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
      this.zoomInToolStripMenuItem.Size = new Size(218, 22);
      this.zoomInToolStripMenuItem.Text = "Zoom In";
      this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
      this.zoomOutToolStripMenuItem.Size = new Size(218, 22);
      this.zoomOutToolStripMenuItem.Text = "Zoom Out";
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(215, 6);
      this.actualPixelSizeToolStripMenuItem.Name = "actualPixelSizeToolStripMenuItem";
      this.actualPixelSizeToolStripMenuItem.Size = new Size(218, 22);
      this.actualPixelSizeToolStripMenuItem.Text = "Actual Pixel Size";
      this.actualPixelSizeToolStripMenuItem.Click += new EventHandler(this.actualPixelSizeToolStripMenuItem_Click);
      this.fitToScreenToolStripMenuItem.Name = "fitToScreenToolStripMenuItem";
      this.fitToScreenToolStripMenuItem.Size = new Size(218, 22);
      this.fitToScreenToolStripMenuItem.Text = "Fit Shape to Window";
      this.fitToScreenToolStripMenuItem.Click += new EventHandler(this.fitToScreenToolStripMenuItem_Click);
      this.fitBoundsToScreenMenuItem.Name = "fitBoundsToScreenMenuItem";
      this.fitBoundsToScreenMenuItem.Size = new Size(218, 22);
      this.fitBoundsToScreenMenuItem.Text = "Fit Bounds to Window";
      this.fitBoundsToScreenMenuItem.Click += new EventHandler(this.fitBoundsToScreenToolStripMenuItem_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(215, 6);
      this.extendBoundsToFitShapeToolStripMenuItem.Name = "extendBoundsToFitShapeToolStripMenuItem";
      this.extendBoundsToFitShapeToolStripMenuItem.Size = new Size(218, 22);
      this.extendBoundsToFitShapeToolStripMenuItem.Text = "Extend Bounds to Fit Shape";
      this.extendBoundsToFitShapeToolStripMenuItem.Click += new EventHandler(this.extendBoundsToFitShapeToolStripMenuItem_Click);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.White;
      this.BorderStyle = BorderStyle.FixedSingle;
      this.MinimumSize = new Size(60, 60);
      this.Name = "ShapeEditorControl";
      this.Size = new Size(458, 326);
      this.Load += new EventHandler(this.ShapeEditorControl_Load);
      this.MouseDown += new MouseEventHandler(this.OnMouseDown);
      this.MouseMove += new MouseEventHandler(this.OnMouseMove);
      this.Scroll += new ScrollEventHandler(this.OnScroll);
      this.Resize += new EventHandler(this.OnResize);
      this.KeyUp += new KeyEventHandler(this.OnKeyUp);
      this.Paint += new PaintEventHandler(this.OnPaint);
      this.MouseLeave += new EventHandler(this.OnMouseLeave);
      this.MouseUp += new MouseEventHandler(this.OnMouseUp);
      this.KeyDown += new KeyEventHandler(this.OnKeyDown);
      this.contextMenuPoint.ResumeLayout(false);
      this.contextMenuCurve.ResumeLayout(false);
      this.contextMenuGeneral.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    [Flags]
    public enum SnapTypes
    {
      SnapToGrid = 1,
      SnapToCtrl = 2,
      SnapToCurves = 4,
      SnapToExtensions = 8,
    }
  }
}
