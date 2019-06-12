// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.RBOrderedTreeBase`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.Collections.Generic
{
  public class RBOrderedTreeBase<T> : RBTreeBase<T, RBOrderedTreeNode<T>, RBOrderedNodeParam>, IOrderedTree<T>, ISortedTree<T>, ITree<T>
  {
    public RBOrderedTreeBase(bool aUnique)
      : base(aUnique)
    {
    }

    public RBOrderedTreeBase(IComparer<T> aComparer, bool aUnique)
      : base(aComparer, aUnique)
    {
    }

    protected override RBTreeNodeBase<T, RBOrderedNodeParam> NewNode()
    {
      return (RBTreeNodeBase<T, RBOrderedNodeParam>) new RBOrderedTreeNode<T>();
    }

    public RBOrderedTreeNode<T> GetByOrder(int idx)
    {
      int num = idx + 1;
      RBTreeNodeBase<T, RBOrderedNodeParam> rbTreeNodeBase = this.mRoot;
      while (rbTreeNodeBase != null && num > 0)
      {
        if (num < rbTreeNodeBase.mParam.mRank)
          rbTreeNodeBase = rbTreeNodeBase.mLeft;
        else if (num > rbTreeNodeBase.mParam.mRank)
        {
          num -= rbTreeNodeBase.mParam.mRank;
          rbTreeNodeBase = rbTreeNodeBase.mRight;
        }
        else if (num == rbTreeNodeBase.mParam.mRank)
          return rbTreeNodeBase as RBOrderedTreeNode<T>;
      }
      return (RBOrderedTreeNode<T>) null;
    }

    public int GetOrder(RBOrderedTreeNode<T> aItem)
    {
      RBTreeNodeBase<T, RBOrderedNodeParam> rbTreeNodeBase = (RBTreeNodeBase<T, RBOrderedNodeParam>) aItem;
      int mRank = rbTreeNodeBase.mParam.mRank;
      for (; rbTreeNodeBase.mParent != null; rbTreeNodeBase = rbTreeNodeBase.mParent)
      {
        if (rbTreeNodeBase.mParent.mRight == rbTreeNodeBase)
          mRank += rbTreeNodeBase.mParent.mParam.mRank;
      }
      return mRank - 1;
    }

    ITreeNode<T> IOrderedTree<T>.GetByOrder(int idx)
    {
      return (ITreeNode<T>) this.GetByOrder(idx);
    }

    int IOrderedTree<T>.GetOrder(ITreeNode<T> node)
    {
      return this.GetOrder((RBOrderedTreeNode<T>) node);
    }
  }
}
