// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.RBOrderedTreeNode`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Collections.Generic
{
  public class RBOrderedTreeNode<T> : RBTreeNodeBase<T, RBOrderedNodeParam>
  {
    public RBOrderedTreeNode()
    {
      this.mParam.mRank = 1;
      this.mParam.mCount = 0;
    }

    internal override void SetParent(RBTreeNodeBase<T, RBOrderedNodeParam> value)
    {
      this.mParent = value;
      if (this.mParent == null)
        return;
      this.mParent.OnUpdateCount();
    }

    internal override void SetLeft(RBTreeNodeBase<T, RBOrderedNodeParam> value)
    {
      this.mLeft = value;
      this.OnUpdateCount();
    }

    internal override void SetRight(RBTreeNodeBase<T, RBOrderedNodeParam> value)
    {
      this.mRight = value;
      this.OnUpdateCount();
    }

    internal override void OnUpdateCount()
    {
      int mCount = this.mParam.mCount;
      this.mParam.mCount = 0;
      if (this.mLeft != null)
      {
        this.mParam.mCount = this.mLeft.mParam.mCount + 1;
        this.mParam.mRank = this.mParam.mCount + 1;
      }
      else
        this.mParam.mRank = 1;
      if (this.mRight != null)
        this.mParam.mCount += this.mRight.mParam.mCount + 1;
      if (this.mParam.mCount == mCount || this.mParent == null)
        return;
      this.mParent.OnUpdateCount();
    }

    internal override void CopyFrom(RBTreeNodeBase<T, RBOrderedNodeParam> z)
    {
      this.mParam.mRank = z.mParam.mRank;
      this.mParam.mCount = z.mParam.mCount;
      base.CopyFrom(z);
    }
  }
}
