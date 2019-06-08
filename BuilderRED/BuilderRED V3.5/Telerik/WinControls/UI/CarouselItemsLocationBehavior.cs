// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CarouselItemsLocationBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class CarouselItemsLocationBehavior : PropertyChangeBehavior
  {
    private CarouselItemsContainer owner;

    private ICarouselPath Calculator
    {
      get
      {
        return this.owner.CarouselPath;
      }
    }

    public CarouselItemsLocationBehavior(CarouselItemsContainer owner)
      : base(CarouselItemsContainer.CarouselLocationProperty)
    {
      this.owner = owner;
    }

    public override void OnPropertyChange(RadElement element, RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChange(element, e);
      if (e.Property != CarouselItemsContainer.CarouselLocationProperty || double.IsNaN((double) e.NewValue))
        return;
      VisualElement element1 = (VisualElement) element;
      CarouselPathAnimationData data = (CarouselPathAnimationData) element.GetValue(CarouselItemsContainer.CarouselAnimationData);
      object evaluated = this.Calculator.Evaluate(element1, data, (object) (double) e.NewValue);
      this.Calculator.ApplyToElement(element1, data, (double) e.NewValue, evaluated, this.owner.AnimationsApplied, this.owner.Size);
    }
  }
}
