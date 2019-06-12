// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GradientAngleBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  internal class GradientAngleBehavior : PropertyChangeBehavior
  {
    private RadElement toRotate;
    private RadScrollBarElement scrollBarElement;
    private float? initialAngle;
    private bool rotateCounterDirection;

    public GradientAngleBehavior(RadElement toRotate)
      : base(RadScrollBarElement.ScrollTypeProperty)
    {
      this.toRotate = toRotate;
    }

    public GradientAngleBehavior(RadElement toRotate, bool rotateCounterDirection)
      : base(RadScrollBarElement.ScrollTypeProperty)
    {
      this.toRotate = toRotate;
      this.rotateCounterDirection = rotateCounterDirection;
    }

    private RadScrollBarElement ScrollBarElement
    {
      get
      {
        if (this.toRotate.ElementState != ElementState.Loaded)
          return (RadScrollBarElement) null;
        if (this.scrollBarElement == null)
        {
          for (RadElement radElement = this.toRotate; radElement != null; radElement = radElement.Parent)
          {
            if (radElement is RadScrollBarElement)
            {
              this.scrollBarElement = (RadScrollBarElement) radElement;
              break;
            }
          }
        }
        return this.scrollBarElement;
      }
    }

    private bool ShouldRotateAccordingToScrollType(ScrollType scrollType)
    {
      return scrollType == ScrollType.Horizontal && (double) this.ScrollBarElement.GradientAngleCorrection == 90.0 || scrollType == ScrollType.Vertical && (double) this.ScrollBarElement.GradientAngleCorrection == -90.0;
    }

    public override void OnPropertyChange(RadElement element, RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChange(element, e);
      if (e.Property != RadScrollBarElement.ScrollTypeProperty)
        return;
      this.RecalculateOnGradientAngleCorrectionChanged();
    }

    public void RecalculateOnGradientAngleCorrectionChanged()
    {
      RadScrollBarElement scrollBarElement = this.ScrollBarElement;
      if (scrollBarElement == null)
        return;
      if (!this.initialAngle.HasValue)
        this.initialAngle = new float?(this.toRotate.AngleTransform);
      if (!this.ShouldRotateAccordingToScrollType(scrollBarElement.ScrollType))
        return;
      this.toRotate.AngleTransform = this.initialAngle.Value + (this.rotateCounterDirection ? -1f : 1f) * scrollBarElement.GradientAngleCorrection;
    }
  }
}
