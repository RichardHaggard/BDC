// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VisualEffects.GridAnimationFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI.VisualEffects
{
  public class GridAnimationFactory
  {
    public static GridExpandAnimation GetExpandAnimation(
      GridTableElement tableElement)
    {
      switch (tableElement.GridViewElement.GroupExpandAnimationType)
      {
        case GridExpandAnimationType.Slide:
          return (GridExpandAnimation) new GridExpandAnimationSlide(tableElement);
        case GridExpandAnimationType.Accordion:
          return (GridExpandAnimation) new GridExpandAnimationAccoridion(tableElement);
        case GridExpandAnimationType.GradientWipe:
          return (GridExpandAnimation) new GridExpandAnimationGrdientWipe(tableElement);
        case GridExpandAnimationType.Fade:
          return (GridExpandAnimation) new GridExpandAnimationFade(tableElement);
        default:
          return (GridExpandAnimation) null;
      }
    }
  }
}
