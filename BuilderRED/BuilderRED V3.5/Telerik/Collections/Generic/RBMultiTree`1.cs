// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.RBMultiTree`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.Collections.Generic
{
  public class RBMultiTree<T> : RBTreeBase<T, RBTreeNode<T>, bool>
  {
    public RBMultiTree()
      : base(false)
    {
    }

    public RBMultiTree(IComparer<T> aComparer)
      : base(aComparer, false)
    {
    }

    protected override RBTreeNodeBase<T, bool> NewNode()
    {
      return (RBTreeNodeBase<T, bool>) new RBTreeNode<T>();
    }
  }
}
