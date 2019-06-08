// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridFilterButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public class GridFilterButtonElement : RadButtonElement
  {
    public static RadProperty IsFilterMenuShownProperty = RadProperty.Register("IsFilterMenuShown", typeof (bool), typeof (GridFilterButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));

    static GridFilterButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GridFilterButtonStateManagerFactory(), typeof (GridFilterButtonElement));
    }
  }
}
