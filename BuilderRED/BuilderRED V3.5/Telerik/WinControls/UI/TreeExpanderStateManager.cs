// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeExpanderStateManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TreeExpanderStateManager : ItemStateManager
  {
    public TreeExpanderStateManager(StateNodeBase rootNode)
      : base(rootNode)
    {
    }

    public override void ItemStateChanged(
      RadObject senderItem,
      RadPropertyChangedEventArgs changeArgs)
    {
      TreeNodeExpanderItem nodeExpanderItem = senderItem as TreeNodeExpanderItem;
      if (changeArgs != (RadPropertyChangedEventArgs) null && !nodeExpanderItem.Enabled && nodeExpanderItem.Expanded)
        this.SetItemState(senderItem, "Disabled" + (object) '.' + "IsExpanded");
      else
        base.ItemStateChanged(senderItem, changeArgs);
    }
  }
}
