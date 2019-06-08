// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorViewContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorViewContainer : RangeSelectorVisualElementWithOrientation
  {
    private RadElement backgroundElement;
    private RangeSelectorTrackingElement trackingElement;
    private RangeSelectorSelectionRectangle selectionRectangle;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchVertically = true;
      this.StretchHorizontally = true;
    }

    protected override void CreateChildElements()
    {
      this.AddBackGroundElement();
      this.trackingElement = new RangeSelectorTrackingElement();
      this.selectionRectangle = new RangeSelectorSelectionRectangle();
      this.Children.Add((RadElement) this.trackingElement);
      this.Children.Add((RadElement) this.selectionRectangle);
    }

    public RadElement AssociatedElement
    {
      get
      {
        return this.backgroundElement;
      }
      set
      {
        if (this.backgroundElement == value)
          return;
        if (value == null && this.backgroundElement != null && this.backgroundElement is IRangeSelectorElement)
          (this.backgroundElement as IRangeSelectorElement).RefreshNeeded -= new EventHandler(this.RangeSelectorViewContainer_RefreshNeeded);
        this.backgroundElement = value;
        this.AddBackGroundElement();
        this.InvalidateMeasure();
        if (!(this.backgroundElement is IRangeSelectorElement))
          return;
        (this.backgroundElement as IRangeSelectorElement).RefreshNeeded += new EventHandler(this.RangeSelectorViewContainer_RefreshNeeded);
      }
    }

    private void RangeSelectorViewContainer_RefreshNeeded(object sender, EventArgs e)
    {
      this.RangeSelectorElement.SuspendPropertyNotifications();
      IRangeSelectorElement associatedElement = this.RangeSelectorElement.AssociatedElement as IRangeSelectorElement;
      this.RangeSelectorElement.EndRange = associatedElement.RangeSelectorViewEnd;
      this.RangeSelectorElement.StartRange = associatedElement.RangeSelectorViewStart;
      this.RangeSelectorElement.ResumePropertyNotifications();
    }

    public RangeSelectorTrackingElement TrackingElement
    {
      get
      {
        return this.trackingElement;
      }
    }

    public RangeSelectorSelectionRectangle SelectionRectangle
    {
      get
      {
        return this.selectionRectangle;
      }
    }

    public float ZoomStart
    {
      get
      {
        if (this.AssociatedElement == null || !(this.AssociatedElement is IRangeSelectorElement))
          return 0.0f;
        return (this.AssociatedElement as IRangeSelectorElement).RangeSelectorViewZoomStart;
      }
      set
      {
        if (this.AssociatedElement == null)
          return;
        IRangeSelectorElement associatedElement = this.AssociatedElement as IRangeSelectorElement;
        if (associatedElement == null)
          throw new ArgumentException("AssociateElement does not implement IRangeSelectorElement interface.");
        associatedElement.RangeSelectorViewZoomStart = value;
        associatedElement.UpdateRangeSelectorView();
        this.InvalidateMeasure(true);
      }
    }

    public float ZoomEnd
    {
      get
      {
        if (this.AssociatedElement == null || !(this.AssociatedElement is IRangeSelectorElement))
          return 100f;
        return (this.AssociatedElement as IRangeSelectorElement).RangeSelectorViewZoomEnd;
      }
      set
      {
        if (this.AssociatedElement == null)
          return;
        IRangeSelectorElement associatedElement = this.AssociatedElement as IRangeSelectorElement;
        if (associatedElement == null)
          throw new ArgumentException("AssociateElement does not implement IRangeSelectorElement interface.");
        associatedElement.RangeSelectorViewZoomEnd = value;
        associatedElement.UpdateRangeSelectorView();
        this.InvalidateMeasure(true);
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      return availableSize;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      PointF location = new PointF(0.0f, 0.0f);
      SizeF size = new SizeF(finalSize.Width, finalSize.Height);
      if (this.RangeSelectorElement.Orientation == Orientation.Horizontal)
      {
        size = new SizeF(finalSize.Width * this.RangeSelectorElement.TotalZoomFactor, finalSize.Height);
        location = new PointF((float) (0.0 - (double) this.RangeSelectorElement.RangeSelectorViewZoomStart * (double) this.RangeSelectorElement.ZoomFactor / 100.0 * (double) finalSize.Width), 0.0f);
        if (this.backgroundElement != null)
          this.backgroundElement.Arrange(new RectangleF(location, size));
        this.trackingElement.Arrange(new RectangleF(location, size));
        if (this.selectionRectangle.Visibility == ElementVisibility.Visible)
          this.selectionRectangle.Arrange(new RectangleF(new PointF((float) Math.Min(this.selectionRectangle.ValueOne, this.selectionRectangle.ValueTwo) * (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.BoundingRectangle.Width / 100f + (float) this.RangeSelectorElement.BodyElement.ViewContainer.TrackingElement.BoundingRectangle.X, 0.0f), this.selectionRectangle.DesiredSize));
      }
      else
      {
        size = new SizeF(finalSize.Width, finalSize.Height * this.RangeSelectorElement.TotalZoomFactor);
        location = new PointF(0.0f, (float) (0.0 - (double) this.RangeSelectorElement.RangeSelectorViewZoomStart * (double) this.RangeSelectorElement.ZoomFactor / 100.0 * (double) finalSize.Height));
        if (this.backgroundElement != null)
          this.backgroundElement.Arrange(new RectangleF(location, size));
        this.trackingElement.Arrange(new RectangleF(location, size));
        if (this.selectionRectangle.Visibility == ElementVisibility.Visible)
          this.selectionRectangle.Arrange(new RectangleF(new PointF(0.0f, (float) ((100.0 - Math.Max(this.selectionRectangle.ValueOne, this.selectionRectangle.ValueTwo)) * (double) finalSize.Height / 100.0)), this.selectionRectangle.DesiredSize));
      }
      return finalSize;
    }

    private void AddBackGroundElement()
    {
      if (this.backgroundElement == null)
        return;
      this.backgroundElement.StretchHorizontally = true;
      this.backgroundElement.StretchVertically = true;
      this.backgroundElement.ZIndex = 0;
      this.Children.Add(this.backgroundElement);
    }
  }
}
