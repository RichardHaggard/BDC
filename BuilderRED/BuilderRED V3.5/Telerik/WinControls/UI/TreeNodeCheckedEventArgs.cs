// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeCheckedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class TreeNodeCheckedEventArgs : RadTreeViewEventArgs
  {
    private CheckedMode checkedMode;

    public TreeNodeCheckedEventArgs(RadTreeNode node)
      : base(node)
    {
    }

    public TreeNodeCheckedEventArgs(RadTreeNode node, CheckedMode checkedMode)
      : base(node)
    {
      this.checkedMode = checkedMode;
    }

    public CheckedMode CheckedMode
    {
      get
      {
        return this.checkedMode;
      }
    }
  }
}
