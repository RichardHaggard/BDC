// Decompiled with JetBrains decompiler
// Type: BuilderRED.TreeExtensions
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Infragistics.Win.UltraWinTree;
using Microsoft.VisualBasic.CompilerServices;

namespace BuilderRED
{
  [StandardModule]
  internal sealed class TreeExtensions
  {
    public static UltraTreeNode GetNodeByTag(this UltraTree tree, string tag)
    {
      UltraTreeNode ultraTreeNode1 = (UltraTreeNode) null;
      foreach (UltraTreeNode node in tree.Nodes)
      {
        UltraTreeNode ultraTreeNode2 = TreeExtensions.SearchChildNodes(node, tag);
        if (ultraTreeNode2 != null)
          ultraTreeNode1 = ultraTreeNode2;
      }
      return ultraTreeNode1;
    }

    private static UltraTreeNode SearchChildNodes(UltraTreeNode searchNode, string tag)
    {
      UltraTreeNode ultraTreeNode1;
      if (searchNode.Tag != null && Operators.CompareString(searchNode.Tag.ToString(), tag, false) == 0)
      {
        ultraTreeNode1 = searchNode;
      }
      else
      {
        foreach (UltraTreeNode node in searchNode.Nodes)
        {
          UltraTreeNode ultraTreeNode2 = TreeExtensions.SearchChildNodes(node, tag);
          if (ultraTreeNode2 != null)
          {
            ultraTreeNode1 = ultraTreeNode2;
            goto label_7;
          }
        }
        ultraTreeNode1 = (UltraTreeNode) null;
      }
label_7:
      return ultraTreeNode1;
    }
  }
}
