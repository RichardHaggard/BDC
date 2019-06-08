// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CarouselPathAnimationData
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class CarouselPathAnimationData
  {
    private double? from = new double?();
    private double? to = new double?();
    private bool interrupt;
    protected bool specialHandling;
    private AnimatedPropertySetting currentAnimation;

    internal bool SpecialHandling
    {
      get
      {
        return this.specialHandling;
      }
    }

    public double? From
    {
      get
      {
        return this.from;
      }
      set
      {
        double? from = this.from;
        double? nullable = value;
        if ((from.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : (from.HasValue != nullable.HasValue ? 1 : 0)) == 0)
          return;
        this.from = value;
      }
    }

    public double? To
    {
      get
      {
        return this.to;
      }
      set
      {
        double? to = this.to;
        double? nullable = value;
        if ((to.GetValueOrDefault() != nullable.GetValueOrDefault() ? 1 : (to.HasValue != nullable.HasValue ? 1 : 0)) == 0)
          return;
        this.to = value;
      }
    }

    public bool Interrupt
    {
      get
      {
        return this.interrupt;
      }
      set
      {
        if (this.interrupt == value)
          return;
        this.interrupt = value;
      }
    }

    public AnimatedPropertySetting CurrentAnimation
    {
      get
      {
        return this.currentAnimation;
      }
      set
      {
        if (this.currentAnimation == value)
          return;
        this.currentAnimation = value;
      }
    }

    public virtual bool IsOutAnimation()
    {
      if (!this.to.HasValue)
        return false;
      if (!double.IsNaN(this.to.Value))
        return double.IsInfinity(this.to.Value);
      return true;
    }

    internal bool UpdateSpecialHandling()
    {
      this.specialHandling = this.From.HasValue && this.To.HasValue && (double.IsNegativeInfinity(this.From.Value) && double.IsPositiveInfinity(this.to.Value) || double.IsPositiveInfinity(this.From.Value) && double.IsNegativeInfinity(this.to.Value));
      return this.specialHandling;
    }

    internal void ResetSpecialHandling()
    {
      if (!this.specialHandling)
        return;
      this.from = new double?();
      this.to = new double?();
      this.specialHandling = false;
    }
  }
}
