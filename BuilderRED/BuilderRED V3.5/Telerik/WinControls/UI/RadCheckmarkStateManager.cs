// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckmarkStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadCheckmarkStateManager : ItemStateManagerBase
  {
    public RadCheckmarkStateManager()
    {
      this.AddDefaultVisibleState("ToggleState=On");
      this.AddDefaultVisibleState("ToggleState=Indeterminate");
      this.AddDefaultVisibleState("Disabled");
      this.AddDefaultVisibleState("Disabled.ToggleState=On");
      this.AddDefaultVisibleState("Disabled.ToggleState=Indeterminate");
    }

    protected override void AttachToItemOverride(
      StateManagerAttachmentData attachData,
      RadObject item)
    {
      attachData.AddEventHandlers(new List<RadProperty>(2)
      {
        RadCheckmark.CheckStateProperty,
        RadElement.EnabledProperty
      });
    }

    public override void ItemStateChanged(RadObject item, RadPropertyChangedEventArgs changeArgs)
    {
      string str = string.Empty;
      RadItem radItem = item as RadItem;
      if (radItem != null && !radItem.Enabled)
        str = "Disabled";
      RadCheckmark radCheckmark = item as RadCheckmark;
      if (radCheckmark.CheckState == ToggleState.On)
      {
        if (!string.IsNullOrEmpty(str))
          str += (string) (object) '.';
        str += "ToggleState=On";
      }
      if (radCheckmark.CheckState == ToggleState.Indeterminate)
      {
        if (!string.IsNullOrEmpty(str))
          str += (string) (object) '.';
        str += "ToggleState=Indeterminate";
      }
      this.SetItemState(item, str);
    }

    public override StateDescriptionNode GetAvailableStates(string itemRoleName)
    {
      StateDescriptionNode stateDescriptionNode = new StateDescriptionNode(itemRoleName);
      stateDescriptionNode.AddNode("ToggleState=On");
      stateDescriptionNode.AddNode("ToggleState=Indeterminate");
      stateDescriptionNode.AddNode("Disabled");
      stateDescriptionNode.AddNode("Disabled.ToggleState=On");
      stateDescriptionNode.AddNode("Disabled.ToggleState=Indeterminate");
      return stateDescriptionNode;
    }

    public override bool VerifyState(string themeRoleName, string key)
    {
      if (key.EndsWith("Disabled.ToggleState=On") || key.EndsWith("Disabled.ToggleState=Indeterminate"))
        return true;
      return base.VerifyState(themeRoleName, key);
    }
  }
}
