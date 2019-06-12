// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewShowExpanderEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class TreeViewShowExpanderEventArgs : RadTreeViewEventArgs
  {
    private bool showExpander;

    public TreeViewShowExpanderEventArgs(RadTreeNode node)
      : base(node)
    {
    }

    public TreeViewShowExpanderEventArgs(RadTreeNode node, bool showExpander)
      : base(node)
    {
      this.showExpander = showExpander;
    }

    public bool ShowExpander
    {
      get
      {
        return this.showExpander;
      }
      set
      {
        this.showExpander = value;
      }
    }
  }
}
