// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorSelectionChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorSelectionChangingEventArgs : CancelEventArgs
  {
    private static float newStartValue = 30f;
    private static float newEndValue = 70f;
    private static float oldStartValue;
    private static float oldEndValue;

    public RangeSelectorSelectionChangingEventArgs(float startValue, float endValue)
    {
      RangeSelectorSelectionChangingEventArgs.oldStartValue = this.NewStartValue;
      RangeSelectorSelectionChangingEventArgs.oldEndValue = this.NewEndValue;
      this.NewStartValue = startValue;
      this.NewEndValue = endValue;
    }

    public float NewStartValue
    {
      get
      {
        return RangeSelectorSelectionChangingEventArgs.newStartValue;
      }
      set
      {
        RangeSelectorSelectionChangingEventArgs.newStartValue = value;
      }
    }

    public float OldStartValue
    {
      get
      {
        return RangeSelectorSelectionChangingEventArgs.oldStartValue;
      }
      set
      {
        RangeSelectorSelectionChangingEventArgs.oldStartValue = value;
      }
    }

    public float NewEndValue
    {
      get
      {
        return RangeSelectorSelectionChangingEventArgs.newEndValue;
      }
      set
      {
        RangeSelectorSelectionChangingEventArgs.newEndValue = value;
      }
    }

    public float OldEndValue
    {
      get
      {
        return RangeSelectorSelectionChangingEventArgs.oldEndValue;
      }
      set
      {
        RangeSelectorSelectionChangingEventArgs.oldEndValue = value;
      }
    }
  }
}
