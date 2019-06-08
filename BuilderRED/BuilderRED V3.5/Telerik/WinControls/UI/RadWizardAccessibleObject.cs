// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadWizardAccessibleObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadWizardAccessibleObject : Control.ControlAccessibleObject
  {
    public RadWizardAccessibleObject(RadWizard owner)
      : base((Control) owner)
    {
    }

    public override int GetChildCount()
    {
      return ((RadWizard) this.Owner).WizardElement.View.CommandArea.NavigationButtons.Count;
    }

    public override AccessibleObject GetChild(int index)
    {
      WizardCommandAreaButtonElement navigationButton = ((RadWizard) this.Owner).WizardElement.View.CommandArea.NavigationButtons[index];
      return (AccessibleObject) new RadButtonElementAccessibleObject((RadButtonItem) navigationButton, navigationButton.Text);
    }
  }
}
