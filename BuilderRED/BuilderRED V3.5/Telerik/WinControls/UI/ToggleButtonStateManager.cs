// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToggleButtonStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ToggleButtonStateManager : ItemStateManager
  {
    public ToggleButtonStateManager(StateNodeBase rootState)
      : base(rootState)
    {
    }

    public override void ItemStateChanged(
      RadObject senderItem,
      RadPropertyChangedEventArgs changeArgs)
    {
      if (senderItem is RadCheckBoxElement)
      {
        RadCheckBoxElement radCheckBoxElement = senderItem as RadCheckBoxElement;
        if (changeArgs != (RadPropertyChangedEventArgs) null && !radCheckBoxElement.Enabled && radCheckBoxElement.ToggleState != ToggleState.Off)
        {
          string str = "ToggleState=" + (radCheckBoxElement.ToggleState == ToggleState.On ? "On" : "Intermediate");
          this.SetItemState(senderItem, "Disabled" + (object) '.' + str);
        }
        else
          base.ItemStateChanged(senderItem, changeArgs);
      }
      else if (senderItem is RadRadioButtonElement)
      {
        base.ItemStateChanged(senderItem, changeArgs);
      }
      else
      {
        RadToggleButtonElement toggleButtonElement = senderItem as RadToggleButtonElement;
        if (changeArgs != (RadPropertyChangedEventArgs) null && !toggleButtonElement.Enabled && toggleButtonElement.ToggleState == ToggleState.On)
          this.SetItemState(senderItem, "Disabled" + (object) '.' + "ToggleState=On");
        else
          base.ItemStateChanged(senderItem, changeArgs);
      }
    }

    public override bool VerifyState(string themeRoleName, string key)
    {
      if (key.EndsWith("Disabled.ToggleState=On") || key.EndsWith("Disabled.ToggleState=Intermediate"))
        return true;
      return base.VerifyState(themeRoleName, key);
    }
  }
}
