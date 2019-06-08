// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeViewEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RadTreeViewEventArgs : EventArgs
  {
    private RadTreeNode node;
    private RadTreeViewAction action;

    public RadTreeViewEventArgs(RadTreeNode node)
    {
      this.node = node;
    }

    public RadTreeViewEventArgs(RadTreeNode node, RadTreeViewAction action)
    {
      this.node = node;
      this.action = action;
    }

    public virtual RadTreeNode Node
    {
      get
      {
        return this.node;
      }
      set
      {
        this.node = value;
      }
    }

    public RadTreeViewElement TreeElement
    {
      get
      {
        return this.node.TreeViewElement;
      }
    }

    public RadTreeView TreeView
    {
      get
      {
        return this.node.TreeView;
      }
    }

    public RadTreeViewAction Action
    {
      get
      {
        return this.action;
      }
    }
  }
}
