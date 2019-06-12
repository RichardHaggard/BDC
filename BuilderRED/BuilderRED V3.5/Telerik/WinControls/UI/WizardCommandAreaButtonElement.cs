// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardCommandAreaButtonElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class WizardCommandAreaButtonElement : RadButtonElement
  {
    public static RadProperty IsFocusedWizardButtonProperty = RadProperty.Register(nameof (IsFocusedWizardButton), typeof (bool), typeof (WizardCommandAreaButtonElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));

    static WizardCommandAreaButtonElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadWizardButtonElementStateManagerFactory(WizardCommandAreaButtonElement.IsFocusedWizardButtonProperty), typeof (WizardCommandAreaButtonElement));
    }

    public bool IsFocusedWizardButton
    {
      get
      {
        return (bool) this.GetValue(WizardCommandAreaButtonElement.IsFocusedWizardButtonProperty);
      }
      set
      {
        int num = (int) this.SetValue(WizardCommandAreaButtonElement.IsFocusedWizardButtonProperty, (object) value);
      }
    }
  }
}
