// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ElementValuesAnimator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class ElementValuesAnimator : IDisposable
  {
    private StandardEasingCalculator easingCalculator = new StandardEasingCalculator();
    private RadObject element;
    private AnimatedPropertySetting setting;
    private int currentFrame;
    private object startValue;
    private object value;
    private object step;
    private bool running;
    private bool waiting;
    private AnimationValueCalculator calculator;
    private RadControlAnimationTimer animationTimer;

    public ElementValuesAnimator(RadObject element, AnimatedPropertySetting setting)
    {
      this.element = element;
      this.setting = setting;
    }

    public bool IsRunning
    {
      get
      {
        return this.running;
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    public bool Waiting
    {
      get
      {
        return this.waiting;
      }
      set
      {
        this.waiting = value;
      }
    }

    public void Initialize(RadObject element, object initialValue)
    {
      this.startValue = initialValue;
      this.value = initialValue;
      if (this.setting.EndValue == null && this.setting.Step == null)
        throw new InvalidOperationException("Error calculating animation properties: EndValue and Step are not specified for property '" + this.setting.Property.FullName + "' ");
      this.calculator = AnimationValueCalculatorFactory.GetCalculator(this.setting.Property.PropertyType);
      if (this.calculator == null)
        throw new Exception("Error calculating animation step because there is not any calculator for type '" + (object) this.setting.Property.PropertyType + "' registered.");
      if (this.setting.EndValue == null)
        this.setting.EndValue = this.calculator.CalculateAnimationEndValue(this.startValue, this.setting.Step, this.setting.NumFrames);
      this.step = this.setting.Step != null ? this.setting.Step : this.calculator.CalculateAnimationStep(this.startValue, this.setting.EndValue, this.setting.NumFrames);
      this.easingCalculator.EasingType = this.setting.ApplyEasingType;
    }

    public void Start(RadObject element)
    {
      if (this.running)
        return;
      this.running = true;
      this.currentFrame = 0;
      this.StartTimer();
    }

    public void Stop()
    {
      if (!this.running)
        return;
      this.StopTimer();
      if (this.currentFrame < this.setting.NumFrames)
        this.UpdateValue();
      this.running = false;
    }

    public void Resume(RadObject element)
    {
      if (this.IsRunning)
        return;
      this.animationTimer.Start();
    }

    public void Pause(RadObject element)
    {
      if (!this.IsRunning)
        return;
      this.animationTimer.Stop();
    }

    public void Dispose()
    {
      if (this.running)
        this.StopTimer();
      if (this.animationTimer != null)
      {
        this.animationTimer.Tick -= new EventHandler(this.AnimationTimer_Tick);
        this.animationTimer = (RadControlAnimationTimer) null;
      }
      this.setting = (AnimatedPropertySetting) null;
      this.element = (RadObject) null;
    }

    protected virtual void UpdateValue()
    {
      this.value = this.calculator.CalculateAnimatedValue(this.startValue, this.setting.EndValue, this.value, this.step, this.currentFrame, this.setting.NumFrames, (EasingCalculator) this.easingCalculator);
      RadElement element = this.element as RadElement;
      if (element != null && !element.IsInValidState(true))
        return;
      int num = (int) this.element.OnAnimatedPropertyValueChanged(this.setting);
    }

    private void StartTimer()
    {
      if (this.animationTimer == null)
      {
        this.animationTimer = new RadControlAnimationTimer();
        this.animationTimer.Interval = this.setting.Interval < 5 ? 40 : this.setting.Interval;
        this.animationTimer.Tick += new EventHandler(this.AnimationTimer_Tick);
      }
      this.animationTimer.Start();
    }

    private void StopTimer()
    {
      if (this.animationTimer == null)
        return;
      this.animationTimer.Stop();
      this.animationTimer = (RadControlAnimationTimer) null;
    }

    private void AnimationTimer_Tick(object sender, EventArgs e)
    {
      ++this.currentFrame;
      if (this.currentFrame > this.setting.NumFrames || this.calculator is AnimationValueBoolCalculator)
      {
        this.Stop();
        this.setting.OnAnimationFinished(new AnimationStatusEventArgs(this.element, false, !this.setting.RemoveAfterApply));
        if (!(this.calculator is AnimationValueBoolCalculator))
          return;
        this.UpdateValue();
      }
      else
        this.UpdateValue();
    }
  }
}
