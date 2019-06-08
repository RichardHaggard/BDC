// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ProgressIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ProgressIndicatorElement : LightVisualElement
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register("IsVertical", typeof (bool), typeof (ProgressIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));

    static ProgressIndicatorElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ProgressIndicatorStateManager(), typeof (ProgressIndicatorElement));
    }
  }
}
