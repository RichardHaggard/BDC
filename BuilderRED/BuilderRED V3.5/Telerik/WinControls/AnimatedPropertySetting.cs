// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimatedPropertySetting
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class AnimatedPropertySetting : IPropertySetting
  {
    private static bool animationsEnabled = true;
    private static Random rand = new Random();
    private int frames = 5;
    private int interval = 40;
    private RadEasingType applyEasingType = RadEasingType.OutQuad;
    private RadProperty property;
    private object startValue;
    private object endValue;
    private object step;
    private object maxValue;
    private int applyDelay;
    private int randomDelay;
    private bool removeAfterApply;
    public bool IsStyleSetting;

    public AnimatedPropertySetting()
    {
    }

    public AnimatedPropertySetting(RadProperty property, int frames, int interval, object step)
    {
      this.property = property;
      this.frames = frames;
      this.interval = interval;
      this.step = step;
    }

    public AnimatedPropertySetting(
      RadProperty property,
      object startValue,
      object endValue,
      int frames,
      int interval)
    {
      this.property = property;
      this.startValue = startValue;
      this.endValue = endValue;
      this.frames = frames;
      this.interval = interval;
    }

    public RadProperty Property
    {
      get
      {
        return this.property;
      }
      set
      {
        this.property = value;
      }
    }

    public object StartValue
    {
      get
      {
        return this.startValue;
      }
      set
      {
        this.startValue = value;
      }
    }

    public object EndValue
    {
      get
      {
        return this.endValue;
      }
      set
      {
        this.endValue = value;
      }
    }

    public object MaxValue
    {
      get
      {
        return this.maxValue;
      }
      set
      {
        this.maxValue = value;
      }
    }

    public object Step
    {
      get
      {
        return this.step;
      }
      set
      {
        this.step = value;
      }
    }

    public int NumFrames
    {
      get
      {
        return this.frames;
      }
      set
      {
        this.frames = value;
      }
    }

    public int Interval
    {
      get
      {
        return this.interval;
      }
      set
      {
        this.interval = value;
      }
    }

    public int ApplyDelay
    {
      get
      {
        return this.applyDelay;
      }
      set
      {
        this.applyDelay = value;
      }
    }

    public int RandomDelay
    {
      get
      {
        return this.randomDelay;
      }
      set
      {
        this.randomDelay = value;
      }
    }

    public RadEasingType ApplyEasingType
    {
      get
      {
        return this.applyEasingType;
      }
      set
      {
        this.applyEasingType = value;
      }
    }

    public bool RemoveAfterApply
    {
      get
      {
        return this.removeAfterApply;
      }
      set
      {
        this.removeAfterApply = value;
      }
    }

    public static bool AnimationsEnabled
    {
      get
      {
        return AnimatedPropertySetting.animationsEnabled;
      }
      set
      {
        AnimatedPropertySetting.animationsEnabled = value;
      }
    }

    public event AnimationFinishedEventHandler AnimationFinished;

    public event AnimationStartedEventHandler AnimationStarted;

    public void Start(RadObject element)
    {
      this.ApplyValue(element);
    }

    public void Stop(RadObject element)
    {
      this.StopAnimation(element, false);
    }

    public void Cancel(RadObject element)
    {
      this.StopAnimation(element, true);
    }

    public bool IsAnimating(RadObject element)
    {
      ElementValuesAnimator valuesAnimator = element.ValuesAnimators[(object) this.GetHashCode()] as ElementValuesAnimator;
      if (valuesAnimator != null)
        return valuesAnimator.IsRunning;
      return false;
    }

    public object GetCurrentValue(RadObject radObject)
    {
      if (!this.IsAnimationEnabled(radObject))
        return this.GetEndValue(radObject);
      ElementValuesAnimator valuesAnimator = radObject.ValuesAnimators[(object) this.GetHashCode()] as ElementValuesAnimator;
      if (valuesAnimator == null)
        return this.endValue;
      object obj = valuesAnimator.Value;
      if (this.ApplyEasingType == RadEasingType.OutElastic && obj != null && (this.maxValue != null && Convert.ToDouble(obj) > Convert.ToDouble(this.maxValue)))
        return this.maxValue;
      return obj;
    }

    public void ApplyValue(RadObject element)
    {
      this.RemovePreviousAnimation(element);
      if (!this.IsAnimationEnabled(element))
      {
        this.OnAnimationStarted(new AnimationStatusEventArgs(element, false));
        this.OnAnimationFinished(new AnimationStatusEventArgs(element, true, !this.RemoveAfterApply));
      }
      else
      {
        ElementValuesAnimator animator = this.GetAnimator(element);
        animator.Initialize(element, this.startValue != null ? this.startValue : element.GetValue(this.Property));
        if (this.RandomDelay != 0)
          this.ApplyDelay = AnimatedPropertySetting.rand.Next(this.RandomDelay);
        if (this.ApplyDelay > 0)
        {
          animator.Waiting = true;
          Timer timer = new Timer();
          timer.Interval = this.ApplyDelay;
          timer.Tick += new EventHandler(this.delayTimer_Tick);
          timer.Tag = (object) element;
          timer.Start();
        }
        else
        {
          this.OnAnimationStarted(new AnimationStatusEventArgs(element, false));
          animator.Start(element);
        }
      }
    }

    public void AppendValue(
      RadObject element,
      RadProperty property,
      object startValue,
      object endValue,
      int frames,
      int interval)
    {
      if (this.Property != property)
        return;
      this.StartValue = startValue;
      this.EndValue = endValue;
      this.NumFrames = frames;
      this.Interval = interval;
      ElementValuesAnimator animator = this.GetAnimator(element);
      animator.Stop();
      animator.Initialize(element, this.StartValue);
      animator.Start(element);
    }

    public void Resume(RadObject element)
    {
      this.GetAnimator(element)?.Resume(element);
    }

    public void Pause(RadObject element)
    {
      this.GetAnimator(element)?.Resume(element);
    }

    public void UnapplyValue(RadObject element)
    {
      this.OnAnimationFinished(new AnimationStatusEventArgs(element, false, false));
    }

    protected internal virtual void OnAnimationFinished(AnimationStatusEventArgs e)
    {
      if (e.Object != null)
      {
        int num = (int) e.Object.OnAnimationFinished(this);
      }
      AnimationFinishedEventHandler animationFinished = this.AnimationFinished;
      if (animationFinished != null)
        animationFinished((object) this, e);
      if (!(e.Object.ValuesAnimators[(object) this.GetHashCode()] is ElementValuesAnimator))
        return;
      e.Object.ValuesAnimators.Remove((object) this.GetHashCode());
    }

    protected virtual void OnAnimationStarted(AnimationStatusEventArgs e)
    {
      if (e.Object != null)
      {
        int num = (int) e.Object.OnAnimationStarted(this);
      }
      AnimationStartedEventHandler animationStarted = this.AnimationStarted;
      if (animationStarted == null)
        return;
      animationStarted((object) this, e);
    }

    private void delayTimer_Tick(object sender, EventArgs e)
    {
      Timer timer = (Timer) sender;
      RadObject tag = (RadObject) timer.Tag;
      timer.Tick -= new EventHandler(this.delayTimer_Tick);
      timer.Stop();
      timer.Dispose();
      ElementValuesAnimator valuesAnimator = tag.ValuesAnimators[(object) this.GetHashCode()] as ElementValuesAnimator;
      if (valuesAnimator == null)
        return;
      valuesAnimator.Waiting = false;
      this.OnAnimationStarted(new AnimationStatusEventArgs(tag, false));
      valuesAnimator.Start(tag);
    }

    private void StopAnimation(RadObject element, bool cancel)
    {
      ElementValuesAnimator valuesAnimator = element.ValuesAnimators[(object) this.GetHashCode()] as ElementValuesAnimator;
      if (valuesAnimator == null || !valuesAnimator.Waiting && !valuesAnimator.IsRunning)
        return;
      valuesAnimator.Stop();
      if (!cancel)
        valuesAnimator.Value = this.GetEndValue(element);
      this.OnAnimationFinished(new AnimationStatusEventArgs(element, true, !cancel && !this.RemoveAfterApply));
    }

    private bool IsAnimationEnabled(RadObject element)
    {
      bool flag = ThemeResolutionService.AllowAnimations && AnimatedPropertySetting.AnimationsEnabled;
      RadElement radElement = element as RadElement;
      if (radElement != null && flag)
      {
        ComponentThemableElementTree elementTree = radElement.ElementTree;
        if (elementTree == null || !elementTree.AnimationsEnabled || radElement.Visibility != ElementVisibility.Visible)
          return false;
        Control control = elementTree.Control;
        if (control == null)
          return false;
        flag = control.Visible;
        RadControl radControl = control as RadControl;
        if (radControl != null && !radControl.Visible && !radControl.IsLoaded)
          flag = true;
      }
      return flag;
    }

    private ElementValuesAnimator GetAnimator(RadObject element)
    {
      ElementValuesAnimator elementValuesAnimator = element.ValuesAnimators[(object) this.GetHashCode()] as ElementValuesAnimator;
      if (elementValuesAnimator == null)
      {
        elementValuesAnimator = new ElementValuesAnimator(element, this);
        element.ValuesAnimators.Add((object) this.GetHashCode(), (object) elementValuesAnimator);
      }
      return elementValuesAnimator;
    }

    private object GetEndValue(RadObject element)
    {
      if (this.endValue != null)
        return this.endValue;
      AnimationValueCalculator calculator = AnimationValueCalculatorFactory.GetCalculator(this.Property.PropertyType);
      EasingCalculator calc = (EasingCalculator) new StandardEasingCalculator(RadEasingType.InQuad);
      object currValue = this.StartValue;
      for (int currFrameNum = 1; currFrameNum <= this.NumFrames; ++currFrameNum)
        currValue = calculator.CalculateAnimatedValue(this.StartValue, this.EndValue, currValue, this.step, currFrameNum, this.NumFrames, calc);
      return currValue;
    }

    private void RemovePreviousAnimation(RadObject element)
    {
      AnimatedPropertySetting currentAnimation = element.GetCurrentAnimation(this.Property);
      if (currentAnimation == null)
        return;
      ElementValuesAnimator valuesAnimator = element.ValuesAnimators[(object) currentAnimation.GetHashCode()] as ElementValuesAnimator;
      if (valuesAnimator == null || !valuesAnimator.IsRunning)
        return;
      valuesAnimator.Stop();
      currentAnimation.OnAnimationFinished(new AnimationStatusEventArgs(element, true, false));
    }
  }
}
