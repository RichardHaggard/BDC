// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.GridLayoutElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Layouts
{
  public class GridLayoutElement : RadObject
  {
    public static RadProperty SizingTypeProperty = RadProperty.Register(nameof (SizingType), typeof (GridLayoutSizingType), typeof (GridLayoutElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GridLayoutSizingType.Proportional));

    public GridLayoutSizingType SizingType
    {
      get
      {
        return (GridLayoutSizingType) this.GetValue(GridLayoutElement.SizingTypeProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridLayoutElement.SizingTypeProperty, (object) value);
      }
    }
  }
}
