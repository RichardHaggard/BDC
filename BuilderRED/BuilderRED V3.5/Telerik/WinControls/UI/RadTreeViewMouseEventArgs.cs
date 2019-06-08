// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeViewMouseEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadTreeViewMouseEventArgs : RadTreeViewEventArgs
  {
    private MouseEventArgs originalEventArgs;

    public MouseEventArgs OriginalEventArgs
    {
      get
      {
        return this.originalEventArgs;
      }
      set
      {
        this.originalEventArgs = value;
      }
    }

    public RadTreeViewMouseEventArgs(RadTreeNode node)
      : base(node)
    {
    }

    public RadTreeViewMouseEventArgs(RadTreeNode node, TreeViewAction action)
      : base(node)
    {
    }

    public RadTreeViewMouseEventArgs(RadTreeNode node, MouseEventArgs originalArgs)
      : base(node)
    {
      this.OriginalEventArgs = originalArgs;
    }
  }
}
