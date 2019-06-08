// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.WindowAnimation.WindowAnimationEngine
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Threading;

namespace Telerik.WinControls.WindowAnimation
{
  public class WindowAnimationEngine : AnimationEngine
  {
    private Rectangle minimum = Rectangle.Empty;
    private Rectangle maximum = Rectangle.Empty;
    protected Rectangle currentFrameValue = Rectangle.Empty;
    protected bool minmax = true;
    private bool cancel;

    public Rectangle Minimum
    {
      get
      {
        return this.minimum;
      }
      set
      {
        this.minimum = value;
      }
    }

    public Rectangle Maximum
    {
      get
      {
        return this.maximum;
      }
      set
      {
        this.maximum = value;
      }
    }

    public bool AnimateMinimumToMaximum
    {
      get
      {
        return this.minmax;
      }
      set
      {
        this.minmax = value;
      }
    }

    public void Cancel()
    {
      this.cancel = true;
    }

    protected override void Animate()
    {
      base.Animate();
      if (this.IsDisposing || this.IsDisposed)
        return;
      Rectangle rectangle1 = this.minimum;
      Rectangle rectangle2 = this.maximum;
      if (!this.AnimateMinimumToMaximum)
      {
        rectangle2 = this.minimum;
        rectangle1 = this.maximum;
      }
      AnimationValueRectangleCalculator rectangleCalculator = new AnimationValueRectangleCalculator();
      for (int currFrameNum = 0; currFrameNum < this.AnimationFrames; ++currFrameNum)
      {
        if (this.IsDisposing || this.IsDisposed)
          return;
        if (this.cancel)
        {
          this.cancel = false;
          break;
        }
        this.currentFrameValue = (Rectangle) rectangleCalculator.CalculateAnimatedValue((object) rectangle1, (object) rectangle2, (object) null, (object) null, currFrameNum, this.frames, (EasingCalculator) this.calculator);
        if (this.IsDisposing || this.IsDisposed)
          return;
        this.OnAnimating(new AnimationEventArgs((object) this.currentFrameValue, false));
        Thread.Sleep(20);
      }
      if (this.IsDisposing || this.IsDisposed)
        return;
      this.OnAnimationFinished(new AnimationEventArgs((object) this.Maximum, true));
    }
  }
}
