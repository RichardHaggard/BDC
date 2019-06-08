// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.RBTreeNodeBase`2
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Collections.Generic
{
  public class RBTreeNodeBase<T, P> : ITreeNode<T>
  {
    internal RBTreeNodeBase<T, P> mParent;
    internal RBTreeNodeBase<T, P> mLeft;
    internal RBTreeNodeBase<T, P> mRight;
    internal T mKey;
    internal RBTreeColor mColor;
    internal P mParam;

    public RBTreeNodeBase<T, P> Parent
    {
      get
      {
        return this.mParent;
      }
    }

    internal virtual void SetParent(RBTreeNodeBase<T, P> value)
    {
      this.mParent = value;
    }

    public virtual RBTreeNodeBase<T, P> Left
    {
      get
      {
        return this.mLeft;
      }
    }

    internal virtual void SetLeft(RBTreeNodeBase<T, P> value)
    {
      this.mLeft = value;
    }

    public virtual RBTreeNodeBase<T, P> Right
    {
      get
      {
        return this.mRight;
      }
    }

    internal virtual void SetRight(RBTreeNodeBase<T, P> value)
    {
      this.mRight = value;
    }

    public virtual T Key
    {
      get
      {
        return this.mKey;
      }
    }

    public RBTreeColor Color
    {
      get
      {
        return this.mColor;
      }
    }

    internal virtual void OnUpdateCount()
    {
    }

    internal virtual void CopyFrom(RBTreeNodeBase<T, P> z)
    {
      if (z.mLeft != null)
        z.mLeft.mParent = this;
      this.mLeft = z.mLeft;
      if (z.mRight != null)
        z.mRight.mParent = this;
      this.mRight = z.mRight;
      if (z.mParent != null)
      {
        if (z.mParent.mLeft == z)
          z.mParent.SetLeft(this);
        else
          z.mParent.SetRight(this);
      }
      this.mColor = z.mColor;
      this.SetParent(z.mParent);
    }
  }
}
