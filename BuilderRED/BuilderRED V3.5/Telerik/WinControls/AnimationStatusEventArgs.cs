// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AnimationStatusEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class AnimationStatusEventArgs : EventArgs
  {
    private RadObject element;
    private bool isAnimationInterrupt;
    private bool registerValueAsLocal;

    public AnimationStatusEventArgs(RadObject element)
    {
      this.element = element;
      this.isAnimationInterrupt = true;
    }

    public AnimationStatusEventArgs(RadObject element, bool isAnimationInterrupt)
      : this(element)
    {
      this.isAnimationInterrupt = isAnimationInterrupt;
    }

    public AnimationStatusEventArgs(
      RadObject element,
      bool isAnimationInterrupt,
      bool registerValueAsLocal)
      : this(element, isAnimationInterrupt)
    {
      this.registerValueAsLocal = registerValueAsLocal;
    }

    public bool IsInterrupt
    {
      get
      {
        return this.isAnimationInterrupt;
      }
    }

    public bool RegisterValueAsLocal
    {
      get
      {
        return this.registerValueAsLocal;
      }
    }

    public RadElement Element
    {
      get
      {
        return this.element as RadElement;
      }
    }

    public RadObject Object
    {
      get
      {
        return this.element;
      }
    }
  }
}
