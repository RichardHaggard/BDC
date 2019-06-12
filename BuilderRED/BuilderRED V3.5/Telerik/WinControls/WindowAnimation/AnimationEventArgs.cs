// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.WindowAnimation.AnimationEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.WindowAnimation
{
  public class AnimationEventArgs : EventArgs
  {
    private object animationValue;
    private bool finished;

    public AnimationEventArgs(object animationValue)
      : this(animationValue, false)
    {
    }

    public AnimationEventArgs(object animationValue, bool finished)
    {
      this.animationValue = animationValue;
      this.finished = finished;
    }

    public object AnimationValue
    {
      get
      {
        return this.animationValue;
      }
    }

    public object AnimationFinished
    {
      get
      {
        return (object) this.finished;
      }
    }
  }
}
