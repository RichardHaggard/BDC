// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ICarouselPath
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public interface ICarouselPath : INotifyPropertyChanged
  {
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    object InitialPathPoint { get; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    object FinalPathPoint { get; }

    double ZScale { get; set; }

    void ApplyToElement(
      VisualElement element,
      CarouselPathAnimationData data,
      double value,
      object evaluated,
      Animations flags,
      Size controlSize);

    void CreateAnimation(
      VisualElement element,
      CarouselPathAnimationData data,
      int frames,
      int delay);

    object Evaluate(VisualElement element, CarouselPathAnimationData data, object value);

    void InitializeItem(VisualElement element, object flags);

    double PositionsCount(int itemsCount);

    double Step(int itemsCount);

    bool ZindexFromPath();

    bool EnableRelativePath { get; set; }

    Point3D ConvertFromRelativePath(Point3D point, Size ownerSize);

    Point3D ConvertToRelativePath(Point3D point, Size ownerSize);
  }
}
