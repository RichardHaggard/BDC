﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.RBTree`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.Collections.Generic
{
  public class RBTree<T> : RBTreeBase<T, RBTreeNode<T>, bool>
  {
    public RBTree()
      : base(true)
    {
    }

    public RBTree(IComparer<T> aComparer)
      : base(aComparer, true)
    {
    }

    protected override RBTreeNodeBase<T, bool> NewNode()
    {
      return (RBTreeNodeBase<T, bool>) new RBTreeNode<T>();
    }
  }
}
