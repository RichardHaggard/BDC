// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TileDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class TileDragDropService : RadDragDropService
  {
    private Stack<RadTileElement> offsetStack = new Stack<RadTileElement>();
    private int autoScrollOffset = 20;
    private readonly Point[] checkDirections = new Point[9]{ new Point(0, 0), new Point(-1, 0), new Point(0, -1), new Point(0, 1), new Point(1, 0), new Point(1, -1), new Point(1, 1), new Point(-1, -1), new Point(-1, 1) };
    private DateTime lastInsertColumnTime = DateTime.MinValue;
    private RadPanoramaElement owner;
    private Timer scrollTimer;
    private int scrollStep;
    private RadTileElement previousHoveredTile;
    private bool zoomedOutTiles;

    public int AutoScrollOffset
    {
      get
      {
        return this.autoScrollOffset;
      }
      set
      {
        this.autoScrollOffset = value;
      }
    }

    public TileDragDropService(RadPanoramaElement owner)
    {
      this.owner = owner;
      this.scrollTimer = new Timer();
      this.scrollTimer.Interval = 20;
      this.scrollTimer.Tick += new EventHandler(this.scrollTimer_Tick);
    }

    protected override bool CanStart(object context)
    {
      if (this.owner.ElementTree.Control.Site != null || !this.owner.AllowDragDrop)
        return false;
      return base.CanStart(context);
    }

    protected override bool PrepareContext()
    {
      bool flag = base.PrepareContext();
      RadItem context = this.Context as RadItem;
      if (this.beginPoint.HasValue && context != null)
      {
        Point client = context.ElementTree.Control.PointToClient(this.beginPoint.Value);
        this.xOutlineFormOffset = client.X - context.ControlBoundingRectangle.X;
        this.yOutlineFormOffset = client.Y - context.ControlBoundingRectangle.Y;
        context.Visibility = ElementVisibility.Hidden;
      }
      this.ZoomOutTiles();
      return flag;
    }

    protected override void OnPreviewDragDrop(RadDropEventArgs e)
    {
      base.OnPreviewDragDrop(e);
      this.scrollTimer.Stop();
      if (!this.owner.ShowGroups)
        this.HandleUngroupedDragDrop(e);
      else
        this.HandleGroupedDragDrop(e);
      RadTileElement context = this.Context as RadTileElement;
      RadElement hitTarget = e.HitTarget as RadElement;
      Point point = e.DropLocation;
      if (context == null)
        return;
      if (hitTarget != null)
        point = hitTarget.PointToControl(point);
      Point location = context.ControlBoundingRectangle.Location;
      SizeF sizeF = new SizeF((float) (point.X - location.X), (float) (point.Y - location.Y));
      context.Visibility = ElementVisibility.Visible;
      new AnimatedPropertySetting(RadElement.PositionOffsetProperty, (object) sizeF, (object) SizeF.Empty, 5, 10)
      {
        RemoveAfterApply = true
      }.ApplyValue((RadObject) context);
      ControlTraceMonitor.TrackAtomicFeature(hitTarget, "Reordering");
    }

    protected override ISupportDrop GetDropTarget(
      Point mousePosition,
      out Point resultDropLocation)
    {
      mousePosition.Offset(-this.xOutlineFormOffset, -this.yOutlineFormOffset);
      return base.GetDropTarget(mousePosition, out resultDropLocation);
    }

    protected override bool IsDropTargetValid(ISupportDrop dropTarget)
    {
      if (!(dropTarget is RadPanoramaElement))
        return dropTarget is RadTileElement;
      return true;
    }

    protected override void HandleMouseMove(Point mousePos)
    {
      base.HandleMouseMove(mousePos);
      Point clientPoint = this.owner.PointFromScreen(mousePos);
      if (this.previousHoveredTile != null)
        this.previousHoveredTile.PositionOffset = SizeF.Empty;
      if (clientPoint.X > this.owner.Size.Width - this.autoScrollOffset)
      {
        this.scrollStep = clientPoint.X - this.owner.Size.Width + this.autoScrollOffset + 1;
        if (this.scrollTimer.Enabled)
          return;
        this.scrollTimer.Start();
      }
      else if (clientPoint.X < this.autoScrollOffset)
      {
        this.scrollStep = clientPoint.X + 1 - this.autoScrollOffset;
        if (this.scrollTimer.Enabled)
          return;
        this.scrollTimer.Start();
      }
      else
      {
        this.scrollTimer.Stop();
        this.OffsetHoveredTile(clientPoint);
      }
    }

    protected override void PerformStop()
    {
      RadItem context = this.Context as RadItem;
      if (context != null)
        context.Visibility = ElementVisibility.Visible;
      base.PerformStop();
      this.scrollTimer.Stop();
      if (this.previousHoveredTile != null)
        this.previousHoveredTile.PositionOffset = SizeF.Empty;
      this.ZoomInTiles();
    }

    private void ZoomInTiles()
    {
      if (!this.zoomedOutTiles)
        return;
      AnimatedPropertySetting animatedPropertySetting1 = new AnimatedPropertySetting(GridLayout.CellPaddingProperty, (object) new Padding(10), (object) new Padding(5), 3, 25);
      animatedPropertySetting1.RemoveAfterApply = true;
      foreach (RadTileElement tile in this.GetTiles())
      {
        AnimatedPropertySetting animatedPropertySetting2 = new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) tile.ScaleTransform, (object) new SizeF(1f, 1f), 3, 25);
        animatedPropertySetting2.RemoveAfterApply = true;
        if (tile != this.Context)
        {
          animatedPropertySetting2.ApplyValue((RadObject) tile);
          animatedPropertySetting1.ApplyValue((RadObject) tile);
        }
      }
      this.zoomedOutTiles = false;
    }

    private void ZoomOutTiles()
    {
      AnimatedPropertySetting animatedPropertySetting1 = new AnimatedPropertySetting(GridLayout.CellPaddingProperty, (object) new Padding(5), (object) new Padding(10), 3, 25);
      foreach (RadTileElement tile in this.GetTiles())
      {
        AnimatedPropertySetting animatedPropertySetting2 = new AnimatedPropertySetting(RadElement.ScaleTransformProperty, (object) new SizeF(1f, 1f), (object) new SizeF((float) (1.0 - 10.0 / (double) tile.Size.Width), (float) (1.0 - 10.0 / (double) tile.Size.Height)), 3, 25);
        if (tile != this.Context)
        {
          animatedPropertySetting2.ApplyValue((RadObject) tile);
          animatedPropertySetting1.ApplyValue((RadObject) tile);
        }
      }
      this.zoomedOutTiles = true;
    }

    private IEnumerable GetTiles()
    {
      if (!this.owner.ShowGroups)
        return (IEnumerable) this.owner.Items.ToArray();
      ArrayList arrayList = new ArrayList();
      foreach (TileGroupElement group in (RadItemCollection) this.owner.Groups)
      {
        foreach (RadTileElement radTileElement in (RadItemCollection) group.Items)
          arrayList.Add((object) radTileElement);
      }
      return (IEnumerable) arrayList;
    }

    protected bool OffsetHoveredTile(Point clientPoint)
    {
      if (this.owner.ShowGroups)
      {
        clientPoint.Offset(-this.xOutlineFormOffset, -this.yOutlineFormOffset);
        RadTileElement context = this.Context as RadTileElement;
        if (context == null)
          return true;
        TileGroupElement targetGroup = this.GetTargetGroup(new RectangleF((PointF) clientPoint, (SizeF) context.Size));
        if (targetGroup == null)
          return true;
        Point targetCell = this.GetTargetCell(targetGroup, clientPoint);
        if (targetCell.X == -1)
          return true;
        foreach (RadTileElement radTileElement in (RadItemCollection) targetGroup.Items)
        {
          if (radTileElement != context && radTileElement.Row == targetCell.X && radTileElement.Column == targetCell.Y)
          {
            radTileElement.PositionOffset = new SizeF(15f, 0.0f);
            this.previousHoveredTile = radTileElement;
            break;
          }
        }
      }
      else
      {
        clientPoint.Offset(-this.xOutlineFormOffset, -this.yOutlineFormOffset);
        clientPoint.Offset(-(int) this.owner.TileLayout.PositionOffset.Width, -(int) this.owner.TileLayout.PositionOffset.Height);
        RadTileElement context = this.Context as RadTileElement;
        if (context == null)
          return true;
        Point targetCell = this.GetTargetCell(context, clientPoint);
        if (targetCell.X == -1)
          return true;
        foreach (RadTileElement radTileElement in (RadItemCollection) this.owner.Items)
        {
          if (radTileElement != context && radTileElement.Row == targetCell.X && radTileElement.Column == targetCell.Y)
          {
            radTileElement.PositionOffset = new SizeF(15f, 0.0f);
            this.previousHoveredTile = radTileElement;
            break;
          }
        }
      }
      return false;
    }

    protected virtual void HandleGroupedDragDrop(RadDropEventArgs e)
    {
      RadTileElement hitTarget = e.HitTarget as RadTileElement;
      RadTileElement context = this.Context as RadTileElement;
      Point point = e.DropLocation;
      if (hitTarget != null)
        point = hitTarget.PointToControl(point);
      TileGroupElement targetGroup = this.GetTargetGroup(new RectangleF((PointF) point, (SizeF) context.Size));
      if (context == null || targetGroup == null)
        return;
      Point targetCell = this.GetTargetCell(targetGroup, point);
      if (targetCell.X == -1)
        return;
      targetCell.X = Math.Max(0, Math.Min(targetCell.X, targetGroup.RowsCount - context.RowSpan));
      context.Row = targetCell.X;
      context.Column = targetCell.Y;
      if (!targetGroup.Items.Contains((RadItem) context))
      {
        (context.Parent.Parent as TileGroupElement).Items.Remove((RadItem) context);
        targetGroup.Items.Add((RadItem) context);
      }
      int colSpan = context.ColSpan;
      context.ColSpan = 0;
      this.OffsetTiles(targetGroup, context, colSpan);
      context.ColSpan = colSpan;
      context.Column -= colSpan;
      this.owner.InvalidateMeasure(true);
      this.owner.UpdateLayout();
    }

    protected virtual void HandleUngroupedDragDrop(RadDropEventArgs e)
    {
      RadElement hitTarget = e.HitTarget as RadElement;
      RadTileElement context = this.Context as RadTileElement;
      Point point = e.DropLocation;
      point.Offset(-(int) this.owner.TileLayout.PositionOffset.Width, -(int) this.owner.TileLayout.PositionOffset.Height);
      if (hitTarget != null)
        point = hitTarget.PointToControl(point);
      if (context == null || hitTarget == null)
        return;
      Point targetCell = this.GetTargetCell(context, point);
      if (targetCell.X == -1)
        return;
      targetCell.X = Math.Min(targetCell.X, this.owner.RowsCount - context.RowSpan);
      context.Row = targetCell.X;
      context.Column = targetCell.Y;
      int colSpan = context.ColSpan;
      context.ColSpan = 0;
      this.OffsetTiles(context, colSpan);
      context.ColSpan = colSpan;
      context.Column -= colSpan;
      this.owner.InvalidateMeasure(true);
      this.owner.UpdateLayout();
    }

    protected virtual Point GetTargetCell(TileGroupElement group, Point dropLocation)
    {
      dropLocation = new Point(Math.Max(dropLocation.X - group.ControlBoundingRectangle.X, 0), Math.Max(dropLocation.Y - group.ContentElement.ControlBoundingRectangle.Y, 0));
      Point cellAtPoint = this.GetCellAtPoint(group, dropLocation);
      Point point1 = Point.Empty;
      PointF pointF1 = new PointF(float.PositiveInfinity, float.PositiveInfinity);
      for (int index = 0; index < this.checkDirections.Length; ++index)
      {
        Point point2 = new Point(cellAtPoint.X + this.checkDirections[index].X, cellAtPoint.Y + this.checkDirections[index].Y);
        PointF cellCoordinates = this.GetCellCoordinates(group, point2.X, point2.Y);
        PointF pointF2 = new PointF(cellCoordinates.X - (float) dropLocation.X, cellCoordinates.Y - (float) dropLocation.Y);
        if ((double) pointF2.X * (double) pointF2.X + (double) pointF2.Y * (double) pointF2.Y < (double) pointF1.X * (double) pointF1.X + (double) pointF1.Y * (double) pointF1.Y)
        {
          pointF1 = pointF2;
          point1 = new Point(point2.X, point2.Y);
        }
      }
      if ((double) pointF1.X == double.PositiveInfinity || (double) pointF1.Y == double.PositiveInfinity)
        return new Point(-1, -1);
      return point1;
    }

    protected virtual TileGroupElement GetTargetGroup(RectangleF tileRectangle)
    {
      TileGroupElement tileGroupElement = (TileGroupElement) null;
      float num1 = 0.0f;
      foreach (TileGroupElement group in (RadItemCollection) this.owner.Groups)
      {
        if (tileRectangle.IntersectsWith((RectangleF) group.ControlBoundingRectangle))
        {
          RectangleF rectangleF = tileRectangle;
          rectangleF.IntersectsWith((RectangleF) group.ControlBoundingRectangle);
          float num2 = rectangleF.Width * rectangleF.Height;
          if ((double) num2 > (double) num1)
          {
            num1 = num2;
            tileGroupElement = group;
          }
        }
      }
      return tileGroupElement;
    }

    protected virtual Point GetTargetCell(RadTileElement source, Point location)
    {
      float width = (float) this.owner.CellSize.Width;
      float height = (float) this.owner.CellSize.Height;
      int y = (int) ((double) location.X / (double) width) + (location.X - (int) ((double) location.X / (double) width) > 0 ? 1 : 0) - 1;
      Point point1 = new Point((int) ((double) location.Y / (double) height) + ((double) location.Y - (double) location.Y / (double) height > 0.0 ? 1 : 0) - 1, y);
      Point point2 = Point.Empty;
      PointF pointF1 = new PointF(float.PositiveInfinity, float.PositiveInfinity);
      if (point1.X < 0)
        point1.X = 0;
      if (point1.Y < 0)
        point1.Y = 0;
      for (int index = 0; index < this.checkDirections.Length; ++index)
      {
        Point point3 = new Point(point1.X + this.checkDirections[index].X, point1.Y + this.checkDirections[index].Y);
        if (this.CanPlaceElementAtPosition(point3.X, point3.Y, source.ColSpan, source.RowSpan))
        {
          PointF cellCoordinates = this.GetCellCoordinates(point3.X, point3.Y);
          PointF pointF2 = new PointF(cellCoordinates.X - (float) location.X, cellCoordinates.Y - (float) location.Y);
          if ((double) pointF2.X * (double) pointF2.X + (double) pointF2.Y * (double) pointF2.Y < (double) pointF1.X * (double) pointF1.X + (double) pointF1.Y * (double) pointF1.Y)
          {
            pointF1 = pointF2;
            point2 = new Point(point3.X, point3.Y);
          }
        }
      }
      if ((double) pointF1.X == double.PositiveInfinity || (double) pointF1.Y == double.PositiveInfinity)
        return new Point(-1, -1);
      return point2;
    }

    protected virtual Point GetCellAtPoint(TileGroupElement group, Point location)
    {
      float width = (float) group.CellSize.Width;
      float height = (float) group.CellSize.Height;
      int y = (int) ((double) location.X / (double) width) + (location.X - (int) ((double) location.X / (double) width) > 0 ? 1 : 0) - 1;
      return new Point((int) ((double) location.Y / (double) height) + ((double) location.Y - (double) location.Y / (double) height > 0.0 ? 1 : 0) - 1, y);
    }

    protected PointF GetCellCoordinates(int rowIndex, int columnIndex)
    {
      float width = (float) this.owner.CellSize.Width;
      float height = (float) this.owner.CellSize.Height;
      return new PointF(width * (float) columnIndex, height * (float) rowIndex);
    }

    protected PointF GetCellCoordinates(
      TileGroupElement group,
      int rowIndex,
      int columnIndex)
    {
      float width = (float) group.CellSize.Width;
      float height = (float) group.CellSize.Height;
      return new PointF(width * (float) columnIndex, height * (float) rowIndex);
    }

    protected virtual bool CanPlaceElementAtPosition(
      int rowIndex,
      int columnIndex,
      int colSpan,
      int rowSpan)
    {
      return rowIndex >= 0 && columnIndex >= 0 && rowIndex + rowSpan <= this.owner.RowsCount;
    }

    private void OffsetTiles(RadTileElement current, int offset)
    {
      this.offsetStack.Push(current);
      foreach (RadTileElement current1 in (RadItemCollection) this.owner.Items)
      {
        if (!this.offsetStack.Contains(current1) && current1.Column < current.Column + current.ColSpan + offset && (current1.Column + current1.ColSpan > current.Column && current1.Row < current.Row + current.RowSpan) && current1.Row + current1.RowSpan > current.Row)
          this.OffsetTiles(current1, offset - (current1.Column - current.Column - current.ColSpan));
      }
      current.Column += offset;
      this.owner.ColumnsCount = Math.Max(this.owner.ColumnsCount, current.Column + current.ColSpan);
      this.offsetStack.Pop();
    }

    private void OffsetTiles(TileGroupElement group, RadTileElement current, int offset)
    {
      this.offsetStack.Push(current);
      foreach (RadTileElement current1 in (RadItemCollection) group.Items)
      {
        if (!this.offsetStack.Contains(current1) && current1.Column < current.Column + current.ColSpan + offset && (current1.Column + current1.ColSpan > current.Column && current1.Row < current.Row + current.RowSpan) && current1.Row + current1.RowSpan > current.Row)
          this.OffsetTiles(group, current1, offset - (current1.Column - current.Column - current.ColSpan));
      }
      current.Column += offset;
      group.ColumnsCount = Math.Max(group.ColumnsCount, current.Column + current.ColSpan);
      this.offsetStack.Pop();
    }

    protected virtual void OnScrollTimerTick()
    {
      if (this.owner.ScrollBar.Maximum - this.owner.ScrollBar.LargeChange + 1 == this.owner.ScrollBar.Value && this.scrollStep > 0)
      {
        this.lastInsertColumnTime = DateTime.Now;
        ++this.owner.ColumnsCount;
        this.owner.InvalidateMeasure(true);
      }
      else
      {
        if (!(DateTime.Now - this.lastInsertColumnTime > TimeSpan.FromMilliseconds(500.0)))
          return;
        this.owner.ScrollView(-this.scrollStep);
      }
    }

    private void scrollTimer_Tick(object sender, EventArgs e)
    {
      this.OnScrollTimerTick();
    }
  }
}
