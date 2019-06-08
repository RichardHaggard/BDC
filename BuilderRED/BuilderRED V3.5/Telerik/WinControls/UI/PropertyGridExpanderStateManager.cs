// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridExpanderStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class PropertyGridExpanderStateManager : ItemStateManager
  {
    public PropertyGridExpanderStateManager(StateNodeBase rootNode)
      : base(rootNode)
    {
    }

    public override void ItemStateChanged(
      RadObject senderItem,
      RadPropertyChangedEventArgs changeArgs)
    {
      PropertyGridExpanderElement gridExpanderElement = senderItem as PropertyGridExpanderElement;
      if (changeArgs != (RadPropertyChangedEventArgs) null && !gridExpanderElement.Enabled && gridExpanderElement.ExpanderItem.Expanded)
        this.SetItemState(senderItem, "Disabled" + (object) '.' + "IsExpanded");
      else
        base.ItemStateChanged(senderItem, changeArgs);
    }
  }
}
