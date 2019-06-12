// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.NodesNeededEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class NodesNeededEventArgs : EventArgs
  {
    private RadTreeNode parent;
    private IList<RadTreeNode> nodes;

    public NodesNeededEventArgs(RadTreeNode parent, IList<RadTreeNode> nodes)
    {
      this.parent = parent;
      this.nodes = nodes;
    }

    public RadTreeNode Parent
    {
      get
      {
        return this.parent;
      }
    }

    public IList<RadTreeNode> Nodes
    {
      get
      {
        return this.nodes;
      }
    }
  }
}
