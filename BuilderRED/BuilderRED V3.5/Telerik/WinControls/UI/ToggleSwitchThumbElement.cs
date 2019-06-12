// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToggleSwitchThumbElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ToggleSwitchThumbElement : LightVisualElement
  {
    public static RadProperty IsOnProperty = RadProperty.Register(nameof (IsOn), typeof (bool), typeof (ToggleSwitchThumbElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));

    static ToggleSwitchThumbElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ToggleSwitchThumbElementStateManager(), typeof (ToggleSwitchThumbElement));
    }

    public bool IsOn
    {
      get
      {
        return (bool) this.GetValue(ToggleSwitchThumbElement.IsOnProperty);
      }
      set
      {
        int num = (int) this.SetValue(ToggleSwitchThumbElement.IsOnProperty, (object) value);
      }
    }
  }
}
