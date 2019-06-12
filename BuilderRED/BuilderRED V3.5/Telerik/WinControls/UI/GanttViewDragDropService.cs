// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewDragDropService : RadDragDropService
  {
    private RadGanttViewElement owner;
    private Point location;

    public GanttViewDragDropService(RadGanttViewElement owner)
    {
      this.owner = owner;
    }

    public Point Location
    {
      get
      {
        return this.location;
      }
      set
      {
        this.location = value;
      }
    }

    public RadGanttViewElement Owner
    {
      get
      {
        return this.owner;
      }
      set
      {
        this.owner = value;
      }
    }

    protected override bool PrepareContext()
    {
      bool flag = base.PrepareContext();
      GanttGraphicalViewBaseTaskElement context = this.Context as GanttGraphicalViewBaseTaskElement;
      if (context != null)
        this.xOutlineFormOffset = context.PointFromScreen(this.beginPoint.Value).X;
      return flag;
    }

    protected override void OnStarting(RadServiceStartingEventArgs e)
    {
      GanttGraphicalViewBaseTaskElement context = e.Context as GanttGraphicalViewBaseTaskElement;
      if (context != null)
      {
        this.location = context.PointToScreen(context.Location);
        this.location.Y += context.Size.Height / 2;
      }
      base.OnStarting(e);
    }

    protected override void HandleMouseMove(Point mousePos)
    {
      base.HandleMouseMove(new Point(mousePos.X, this.location.Y));
      this.Owner.GanttViewBehavior.ProcessScrolling(mousePos, false);
    }

    protected override void OnPreviewDragOver(RadDragOverEventArgs e)
    {
      GanttGraphicalViewBaseTaskElement context = this.Context as GanttGraphicalViewBaseTaskElement;
      if (context != null)
        e.CanDrop = context.Parent == e.HitTarget;
      base.OnPreviewDragOver(e);
    }

    protected override void OnPreviewDragDrop(RadDropEventArgs e)
    {
      base.OnPreviewDragDrop(e);
      if (e.Handled || !(e.DragInstance is GanttGraphicalViewBaseTaskElement))
        return;
      GanttGraphicalViewBaseItemElement hitTarget = e.HitTarget as GanttGraphicalViewBaseItemElement;
      if (hitTarget == null)
        return;
      DateTime dateTime = this.Owner.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart.AddSeconds((double) (this.Owner.GraphicalViewElement.HorizontalScrollBarElement.Value + e.DropLocation.X - this.xOutlineFormOffset) * this.Owner.GraphicalViewElement.OnePixelTime.TotalSeconds);
      if (dateTime == hitTarget.Data.Start)
        return;
      TimeSpan timeSpan = hitTarget.Data.End - hitTarget.Data.Start;
      hitTarget.Data.Start = dateTime;
      hitTarget.Data.End = dateTime + timeSpan;
      hitTarget.GraphicalViewElement.CalculateLinkLines(hitTarget.Data);
    }

    protected override void OnStopping(RadServiceStoppingEventArgs e)
    {
      RadElement context = this.Context as RadElement;
      if (context != null)
      {
        context.Parent.InvalidateArrange(true);
        this.Owner.GraphicalViewElement.Invalidate();
      }
      base.OnStopping(e);
    }

    protected override void OnStopped()
    {
      base.OnStopped();
      this.Owner.GanttViewBehavior.StopScrollTimers();
    }
  }
}
