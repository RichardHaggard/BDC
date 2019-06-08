// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeViewTraversingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class TreeViewTraversingEventArgs : EventArgs
  {
    private RadTreeNode node;
    private bool process;

    public TreeViewTraversingEventArgs(RadTreeNode content)
    {
      this.node = content;
      this.process = content == null || (content.Visible || content.IsInDesignMode);
    }

    public bool Process
    {
      get
      {
        return this.process;
      }
      set
      {
        this.process = value;
      }
    }

    public RadTreeNode Node
    {
      get
      {
        return this.node;
      }
    }
  }
}
