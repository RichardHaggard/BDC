// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorScrollThumb
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorScrollThumb : RangeSelectorVisualElementWithOrientation
  {
    private PointF oldLocation = PointF.Empty;
    private long prev = DateTime.Now.Ticks;
    private bool isLeftTopThumb;
    private float startDelta;
    private float endDelta;
    private long ticks;

    public RangeSelectorScrollThumb(bool isleftTop)
    {
      this.isLeftTopThumb = isleftTop;
      if (isleftTop)
        this.Class = "ScrolLeftThumb";
      else
        this.Class = "ScrollRightThumb";
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MinSize = new Size(10, 10);
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.BackColor = Color.Red;
      this.GradientStyle = GradientStyles.Solid;
      this.DrawFill = true;
      this.ZIndex = 1;
    }

    public bool IsLeftTopThumb
    {
      get
      {
        return this.isLeftTopThumb;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.Capture = true;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.RangeSelectorElement.AssociatedElement is IRangeSelectorElement)
        (this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement).UpdateAssociatedView();
      this.Capture = false;
      if (this.IsLeftTopThumb)
        this.RangeSelectorElement.RangeSelectorViewZoomStart = this.RangeSelectorElement.ScrollSelectorElement.Start;
      else
        this.RangeSelectorElement.RangeSelectorViewZoomEnd = this.RangeSelectorElement.ScrollSelectorElement.End;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      RangeSelectorScrollElement scrollSelectorElement = this.RangeSelectorElement.ScrollSelectorElement;
      if (scrollSelectorElement.ToolTip == null)
      {
        scrollSelectorElement.ToolTip = this.ElementTree.ComponentTreeHandler.Behavior.ToolTip;
        scrollSelectorElement.ToolTip.InitialDelay = 0;
      }
      if ((PointF) e.Location == this.oldLocation)
      {
        base.OnMouseMove(e);
      }
      else
      {
        this.oldLocation = (PointF) e.Location;
        if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
        {
          this.ticks = DateTime.Now.Ticks;
          if (this.ticks - this.prev > this.RangeSelectorElement.LayoutsRefreshRateInTicks)
            this.MoveThumb(e);
          this.prev = this.ticks;
        }
        if (!this.ElementTree.ComponentTreeHandler.Behavior.ShowItemToolTips)
          return;
        string text = string.Format(scrollSelectorElement.ToolTipThumbFormatString, (object) scrollSelectorElement.Start, (object) scrollSelectorElement.End);
        scrollSelectorElement.ToolTip.Show(text, (IWin32Window) this.ElementTree.Control, e.X + scrollSelectorElement.ToolTipOffset.X, e.Y + scrollSelectorElement.ToolTipOffset.Y, scrollSelectorElement.ToolTipDuration);
      }
    }

    private void MoveThumb(MouseEventArgs e)
    {
      if (!this.Capture)
        this.CaptureDeltas(e);
      if (this.RangeSelectorElement.Orientation != Orientation.Horizontal)
        return;
      this.PerformHorizontalThumbMove(e);
    }

    private void CaptureDeltas(MouseEventArgs e)
    {
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
      {
        this.startDelta = (float) (e.X - this.BoundingRectangle.X);
        this.endDelta = this.DesiredSize.Width - (float) e.X + (float) this.BoundingRectangle.X;
      }
      else
      {
        this.startDelta = (float) (e.Y - this.BoundingRectangle.Y);
        this.endDelta = this.DesiredSize.Height - (float) e.Y + (float) this.BoundingRectangle.Y;
      }
    }

    private void PerformHorizontalThumbMove(MouseEventArgs e)
    {
      float num1 = (float) this.Parent.BoundingRectangle.Width - this.RangeSelectorElement.ScrollSelectorElement.LeftTopButton.DesiredSize.Width - this.RangeSelectorElement.ScrollSelectorElement.BottomRightButton.DesiredSize.Width;
      if (this.isLeftTopThumb)
      {
        float num2 = (float) (((double) e.Location.X - (double) this.startDelta - (double) this.RangeSelectorElement.ScrollSelectorElement.LeftTopButton.DesiredSize.Width - (double) this.Parent.LocationToControl().X) / (double) num1 * 100.0);
        if ((double) num2 > 100.0 - (double) this.RangeSelectorElement.ScrollSelectorElement.MinScrollLength)
          num2 = 100f - this.RangeSelectorElement.ScrollSelectorElement.MinScrollLength;
        if ((double) num2 < 0.0)
          num2 = 0.0f;
        this.RangeSelectorElement.ScrollSelectorElement.Start = num2;
      }
      else
      {
        float num2 = (float) (((double) e.Location.X + (double) this.endDelta - (double) this.Parent.LocationToControl().X - (double) this.RangeSelectorElement.ScrollSelectorElement.LeftTopButton.DesiredSize.Width) / (double) num1 * 100.0);
        if ((double) num2 < (double) this.RangeSelectorElement.ScrollSelectorElement.MinScrollLength)
          num2 = this.RangeSelectorElement.ScrollSelectorElement.MinScrollLength;
        if ((double) num2 > 100.0)
          num2 = 100f;
        this.RangeSelectorElement.ScrollSelectorElement.End = num2;
      }
    }
  }
}
