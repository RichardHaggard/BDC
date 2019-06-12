// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TreeNodeComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TreeNodeComparer : IComparer<RadTreeNode>
  {
    private List<TreeNodeComparer.TreeNodeDescriptor> context = new List<TreeNodeComparer.TreeNodeDescriptor>();
    private RadTreeViewElement treeView;

    public TreeNodeComparer(RadTreeViewElement treeView)
    {
      this.treeView = treeView;
      this.Update();
    }

    public void Update()
    {
      this.context.Clear();
    }

    public RadTreeViewElement TreeViewElement
    {
      get
      {
        return this.treeView;
      }
    }

    public virtual int Compare(RadTreeNode x, RadTreeNode y)
    {
      object text1 = (object) x.Text;
      object text2 = (object) y.Text;
      if (text1 == text2)
        return 0;
      if (text1 == null || text1 == DBNull.Value)
        return -1;
      if (text2 == null || text2 == DBNull.Value)
        return 1;
      IComparable comparable = text1 as IComparable;
      if (comparable == null)
        throw new ArgumentException("Argument_ImplementIComparable");
      if (this.treeView.SortOrder == SortOrder.Descending)
        return -comparable.CompareTo(text2);
      return comparable.CompareTo(text2);
    }

    private class TreeNodeDescriptor
    {
      public int Index = -1;
      public PropertyDescriptor Descriptor;
      public bool Descending;
    }
  }
}
