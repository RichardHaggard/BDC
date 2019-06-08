﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ToggleSwitchThumbButtonStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ToggleSwitchThumbButtonStateManager : ItemStateManager
  {
    public ToggleSwitchThumbButtonStateManager(StateNodeBase rootState)
      : base(rootState)
    {
    }

    public override string GetInitialState(RadObject item)
    {
      return base.GetInitialState(item) + (object) '.' + "IsOn";
    }

    public override void ItemStateChanged(
      RadObject senderItem,
      RadPropertyChangedEventArgs changeArgs)
    {
      ToggleSwitchThumbElement switchThumbElement = senderItem as ToggleSwitchThumbElement;
      if (changeArgs != (RadPropertyChangedEventArgs) null && !switchThumbElement.Enabled && switchThumbElement.IsOn)
        this.SetItemState(senderItem, "Disabled" + (object) '.' + "IsOn");
      else
        base.ItemStateChanged(senderItem, changeArgs);
    }

    public override bool VerifyState(string themeRoleName, string key)
    {
      if (key.EndsWith("Disabled.IsOn"))
        return true;
      return base.VerifyState(themeRoleName, key);
    }
  }
}
