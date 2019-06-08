// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.RBTreeBase`3
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Collections.Generic
{
  public abstract class RBTreeBase<T, N, P> : ISortedTree<T>, ITree<T>, IEnumerable<N>, IEnumerable
    where N : RBTreeNodeBase<T, P>, new()
  {
    protected object mSyncRoot = new object();
    private bool mUnique;
    protected RBTreeNodeBase<T, P> mRoot;
    internal IComparer<T> mComparer;
    protected int mCount;
    private RBTreeBase<T, N, P>.CollectionAdapter<T, N, P> mAdapter;

    ITreeNode<T> ITree<T>.Add(T item)
    {
      return (ITreeNode<T>) this.Add(item);
    }

    ITreeNode<T> ITree<T>.AddOrGet(T item)
    {
      return (ITreeNode<T>) this.AddOrGet(item);
    }

    ITreeNode<T> ITree<T>.Find(T item)
    {
      return (ITreeNode<T>) this.Find(item);
    }

    bool ITree<T>.Remove(T item)
    {
      return this.Remove(item);
    }

    void ITree<T>.Clear()
    {
      this.Clear();
    }

    void ITree<T>.Remove(ITreeNode<T> node)
    {
      this.Remove((N) node);
    }

    ITreeNode<T> ISortedTree<T>.First()
    {
      return (ITreeNode<T>) this.First();
    }

    ITreeNode<T> ISortedTree<T>.Last()
    {
      return (ITreeNode<T>) this.Last();
    }

    ITreeNode<T> ISortedTree<T>.Previous(ITreeNode<T> node)
    {
      return (ITreeNode<T>) this.Previous((N) node);
    }

    ITreeNode<T> ISortedTree<T>.Next(ITreeNode<T> node)
    {
      return (ITreeNode<T>) this.Next((N) node);
    }

    public bool Unique
    {
      get
      {
        return this.mUnique;
      }
    }

    public object SyncRoot
    {
      get
      {
        return this.mSyncRoot;
      }
    }

    public N Root
    {
      get
      {
        return (N) this.mRoot;
      }
    }

    public int Count
    {
      get
      {
        return this.mCount;
      }
    }

    public RBTreeBase(bool unique)
    {
      this.mRoot = (RBTreeNodeBase<T, P>) null;
      this.mComparer = (IComparer<T>) Comparer<T>.Default;
      this.mCount = 0;
      this.mUnique = unique;
    }

    public RBTreeBase(IComparer<T> aComparer, bool unique)
    {
      this.mRoot = (RBTreeNodeBase<T, P>) null;
      this.mComparer = aComparer;
      this.mCount = 0;
      this.mUnique = unique;
    }

    public N Add(T aKey)
    {
      bool aInsert = true;
      RBTreeNodeBase<T, P> rbTreeNodeBase = this.Traverse(ref aInsert, aKey);
      if (!aInsert)
        throw new ArgumentException();
      ++this.mCount;
      return rbTreeNodeBase as N;
    }

    public N AddOrGet(T aKey)
    {
      bool aInsert1 = true;
      RBTreeNodeBase<T, P> rbTreeNodeBase;
      if (this.mUnique)
      {
        rbTreeNodeBase = this.Traverse(ref aInsert1, aKey);
        if (aInsert1)
          ++this.mCount;
      }
      else
      {
        bool aInsert2 = false;
        rbTreeNodeBase = this.Traverse(ref aInsert2, aKey);
        if (rbTreeNodeBase == null)
        {
          bool aInsert3 = true;
          rbTreeNodeBase = this.Traverse(ref aInsert3, aKey);
          ++this.mCount;
        }
      }
      return rbTreeNodeBase as N;
    }

    public bool Remove(T aKey)
    {
      RBTreeNodeBase<T, P> z = (RBTreeNodeBase<T, P>) this.Find(aKey);
      if (z == null)
        return false;
      --this.mCount;
      this.Delete(z);
      return true;
    }

    public void Clear()
    {
      this.mRoot = (RBTreeNodeBase<T, P>) null;
      this.mCount = 0;
    }

    public bool Remove(N aNode)
    {
      this.Delete((RBTreeNodeBase<T, P>) aNode);
      --this.mCount;
      return true;
    }

    public N Find(T aKey)
    {
      RBTreeNodeBase<T, P> x1 = this.mRoot;
      while (x1 != null)
      {
        int num = this.mComparer.Compare(aKey, x1.mKey);
        if (num < 0)
          x1 = x1.mLeft;
        else if (num > 0)
        {
          x1 = x1.mRight;
        }
        else
        {
          if (!this.mUnique)
          {
            if (object.Equals((object) aKey, (object) x1.mKey))
              return x1 as N;
            RBTreeNodeBase<T, P> x2 = x1;
            RBTreeNodeBase<T, P> x3 = this.Predecessor(x1);
            while (x3 != null && this.mComparer.Compare(aKey, x3.mKey) == 0)
            {
              x1 = x3;
              x3 = this.Predecessor(x3);
              if (object.Equals((object) aKey, (object) x1.mKey))
                return x1 as N;
            }
            RBTreeNodeBase<T, P> x4 = this.Successor(x2);
            while (x4 != null && this.mComparer.Compare(aKey, x4.mKey) == 0)
            {
              x1 = x4;
              x4 = this.Successor(x4);
              if (object.Equals((object) aKey, (object) x1.mKey))
                return x1 as N;
            }
          }
          return x1 as N;
        }
      }
      return default (N);
    }

    public N First()
    {
      RBTreeNodeBase<T, P> rbTreeNodeBase1 = (RBTreeNodeBase<T, P>) null;
      for (RBTreeNodeBase<T, P> rbTreeNodeBase2 = this.mRoot; rbTreeNodeBase2 != null; rbTreeNodeBase2 = rbTreeNodeBase2.mLeft)
        rbTreeNodeBase1 = rbTreeNodeBase2;
      return rbTreeNodeBase1 as N;
    }

    public N Last()
    {
      RBTreeNodeBase<T, P> rbTreeNodeBase1 = (RBTreeNodeBase<T, P>) null;
      for (RBTreeNodeBase<T, P> rbTreeNodeBase2 = this.mRoot; rbTreeNodeBase2 != null; rbTreeNodeBase2 = rbTreeNodeBase2.mRight)
        rbTreeNodeBase1 = rbTreeNodeBase2;
      return rbTreeNodeBase1 as N;
    }

    public N Next(N aNode)
    {
      return this.Successor((RBTreeNodeBase<T, P>) aNode) as N;
    }

    public N Previous(N aNode)
    {
      return this.Predecessor((RBTreeNodeBase<T, P>) aNode) as N;
    }

    IEnumerator<N> IEnumerable<N>.GetEnumerator()
    {
      return (IEnumerator<N>) new RBTreeEnumerator<N, T, P>(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new RBTreeEnumerator<N, T, P>(this);
    }

    protected void Balance(RBTreeNodeBase<T, P> z)
    {
      RBTreeNodeBase<T, P> rbTreeNodeBase = z;
      while (rbTreeNodeBase != this.mRoot && rbTreeNodeBase.mParent.mColor == RBTreeColor.Red)
      {
        if (rbTreeNodeBase.mParent == rbTreeNodeBase.mParent.mParent.mLeft)
        {
          RBTreeNodeBase<T, P> mRight = rbTreeNodeBase.mParent.mParent.mRight;
          if (mRight != null && mRight.mColor == RBTreeColor.Red)
          {
            rbTreeNodeBase.mParent.mColor = RBTreeColor.Black;
            mRight.mColor = RBTreeColor.Black;
            rbTreeNodeBase.mParent.mParent.mColor = RBTreeColor.Red;
            rbTreeNodeBase = rbTreeNodeBase.mParent.mParent;
          }
          else
          {
            if (rbTreeNodeBase == rbTreeNodeBase.mParent.mRight)
            {
              rbTreeNodeBase = rbTreeNodeBase.mParent;
              this.LeftRotate(rbTreeNodeBase);
            }
            rbTreeNodeBase.mParent.mColor = RBTreeColor.Black;
            rbTreeNodeBase.mParent.mParent.mColor = RBTreeColor.Red;
            this.RightRotate(rbTreeNodeBase.mParent.mParent);
          }
        }
        else
        {
          RBTreeNodeBase<T, P> mLeft = rbTreeNodeBase.mParent.mParent.mLeft;
          if (mLeft != null && mLeft.mColor == RBTreeColor.Red)
          {
            rbTreeNodeBase.mParent.mColor = RBTreeColor.Black;
            mLeft.mColor = RBTreeColor.Black;
            rbTreeNodeBase.mParent.mParent.mColor = RBTreeColor.Red;
            rbTreeNodeBase = rbTreeNodeBase.mParent.mParent;
          }
          else
          {
            if (rbTreeNodeBase == rbTreeNodeBase.mParent.mLeft)
            {
              rbTreeNodeBase = rbTreeNodeBase.mParent;
              this.RightRotate(rbTreeNodeBase);
            }
            rbTreeNodeBase.mParent.mColor = RBTreeColor.Black;
            rbTreeNodeBase.mParent.mParent.mColor = RBTreeColor.Red;
            this.LeftRotate(rbTreeNodeBase.mParent.mParent);
          }
        }
      }
      this.mRoot.mColor = RBTreeColor.Black;
    }

    protected abstract RBTreeNodeBase<T, P> NewNode();

    internal RBTreeNodeBase<T, P> Traverse(ref bool aInsert, T aKey)
    {
      RBTreeNodeBase<T, P> rbTreeNodeBase1 = (RBTreeNodeBase<T, P>) null;
      RBTreeNodeBase<T, P> rbTreeNodeBase2 = this.mRoot;
      while (rbTreeNodeBase2 != null)
      {
        rbTreeNodeBase1 = rbTreeNodeBase2;
        int num = this.mComparer.Compare(aKey, rbTreeNodeBase2.mKey);
        if (!this.mUnique && num == 0 && aInsert)
          num = 1;
        if (num < 0)
          rbTreeNodeBase2 = rbTreeNodeBase2.mLeft;
        else if (num > 0)
        {
          rbTreeNodeBase2 = rbTreeNodeBase2.mRight;
        }
        else
        {
          aInsert = false;
          return rbTreeNodeBase2;
        }
      }
      if (!aInsert)
        return (RBTreeNodeBase<T, P>) null;
      RBTreeNodeBase<T, P> z = this.NewNode();
      z.mKey = aKey;
      z.mParent = rbTreeNodeBase1;
      if (rbTreeNodeBase1 == null)
      {
        this.mRoot = z;
      }
      else
      {
        int num = this.mComparer.Compare(z.mKey, rbTreeNodeBase1.mKey);
        if (num == 0)
          num = 1;
        if (num < 0)
          rbTreeNodeBase1.SetLeft(z);
        else
          rbTreeNodeBase1.SetRight(z);
      }
      z.mColor = RBTreeColor.Red;
      this.Balance(z);
      this.mRoot.mColor = RBTreeColor.Black;
      return z;
    }

    protected void LeftRotate(RBTreeNodeBase<T, P> x)
    {
      RBTreeNodeBase<T, P> mRight = x.mRight;
      x.mRight = mRight.mLeft;
      if (mRight.mLeft != null)
        mRight.mLeft.mParent = x;
      mRight.mParent = x.mParent;
      if (x.mParent == null)
        this.mRoot = mRight;
      else if (x == x.mParent.mLeft)
        x.mParent.mLeft = mRight;
      else
        x.mParent.mRight = mRight;
      mRight.mLeft = x;
      x.mParent = mRight;
      x.OnUpdateCount();
    }

    protected void RightRotate(RBTreeNodeBase<T, P> y)
    {
      RBTreeNodeBase<T, P> mLeft = y.mLeft;
      y.mLeft = mLeft.mRight;
      if (mLeft.mRight != null)
        mLeft.mRight.mParent = y;
      mLeft.mParent = y.mParent;
      if (y.mParent == null)
        this.mRoot = mLeft;
      else if (y == y.mParent.mLeft)
        y.mParent.mLeft = mLeft;
      else
        y.mParent.mRight = mLeft;
      mLeft.mRight = y;
      y.mParent = mLeft;
      y.OnUpdateCount();
    }

    protected RBTreeNodeBase<T, P> Successor(RBTreeNodeBase<T, P> x)
    {
      RBTreeNodeBase<T, P> rbTreeNodeBase;
      if (x.mRight != null)
      {
        rbTreeNodeBase = x.mRight;
        while (rbTreeNodeBase.mLeft != null)
          rbTreeNodeBase = rbTreeNodeBase.mLeft;
      }
      else
      {
        for (rbTreeNodeBase = x.mParent; rbTreeNodeBase != null && x == rbTreeNodeBase.mRight; rbTreeNodeBase = rbTreeNodeBase.mParent)
          x = rbTreeNodeBase;
      }
      return rbTreeNodeBase;
    }

    protected RBTreeNodeBase<T, P> Predecessor(RBTreeNodeBase<T, P> x)
    {
      RBTreeNodeBase<T, P> rbTreeNodeBase;
      if (x.mLeft != null)
      {
        rbTreeNodeBase = x.mLeft;
        while (rbTreeNodeBase.mRight != null)
          rbTreeNodeBase = rbTreeNodeBase.mRight;
      }
      else
      {
        for (rbTreeNodeBase = x.mParent; rbTreeNodeBase != null && x == rbTreeNodeBase.mLeft; rbTreeNodeBase = rbTreeNodeBase.mParent)
          x = rbTreeNodeBase;
      }
      return rbTreeNodeBase;
    }

    protected virtual void Delete(RBTreeNodeBase<T, P> z)
    {
      RBTreeNodeBase<T, P> rbTreeNodeBase = z.mLeft == null || z.mRight == null ? z : this.Successor(z);
      RBTreeNodeBase<T, P> x = rbTreeNodeBase.mLeft == null ? rbTreeNodeBase.mRight : rbTreeNodeBase.mLeft;
      x?.SetParent(rbTreeNodeBase.mParent);
      if (rbTreeNodeBase.mParent == null)
        this.mRoot = x;
      else if (rbTreeNodeBase == rbTreeNodeBase.mParent.mLeft)
        rbTreeNodeBase.mParent.SetLeft(x);
      else
        rbTreeNodeBase.mParent.SetRight(x);
      if (rbTreeNodeBase != z)
      {
        rbTreeNodeBase.CopyFrom(z);
        if (z == this.mRoot)
          this.mRoot = rbTreeNodeBase;
      }
      if (rbTreeNodeBase.mColor != RBTreeColor.Black || x == null)
        return;
      this.DeleteFix(x);
    }

    protected void DeleteFix(RBTreeNodeBase<T, P> x)
    {
      while (x != this.mRoot && x.mColor == RBTreeColor.Black)
      {
        if (x == x.mParent.mLeft)
        {
          RBTreeNodeBase<T, P> mRight = x.mParent.mRight;
          if (mRight == null)
          {
            x = x.mParent;
          }
          else
          {
            if (mRight.mColor == RBTreeColor.Red)
            {
              mRight.mColor = RBTreeColor.Black;
              x.mParent.mColor = RBTreeColor.Red;
              this.LeftRotate(x.mParent);
              mRight = x.mParent.mRight;
            }
            if (mRight == null)
              x = x.mParent;
            else if ((mRight.mLeft == null || mRight.mLeft.mColor == RBTreeColor.Black) && (mRight.mRight == null || mRight.mRight.mColor == RBTreeColor.Black))
            {
              mRight.mColor = RBTreeColor.Red;
              x = x.mParent;
            }
            else
            {
              if (mRight.mRight == null || mRight.mRight.mColor == RBTreeColor.Black)
              {
                if (mRight.mLeft != null)
                  mRight.mLeft.mColor = RBTreeColor.Black;
                mRight.mColor = RBTreeColor.Red;
                this.RightRotate(mRight);
                mRight = x.mParent.mRight;
              }
              mRight.mColor = x.mParent.mColor;
              x.mParent.mColor = RBTreeColor.Black;
              if (mRight.mRight != null)
                mRight.mRight.mColor = RBTreeColor.Black;
              this.LeftRotate(x.mParent);
              x = this.mRoot;
            }
          }
        }
        else
        {
          RBTreeNodeBase<T, P> mLeft = x.mParent.mLeft;
          if (mLeft == null)
          {
            x = x.mParent;
          }
          else
          {
            if (mLeft.mColor == RBTreeColor.Red)
            {
              mLeft.mColor = RBTreeColor.Black;
              x.mParent.mColor = RBTreeColor.Red;
              this.RightRotate(x.mParent);
              mLeft = x.mParent.mLeft;
            }
            if (mLeft == null)
              x = x.mParent;
            else if ((mLeft.mRight == null || mLeft.mRight.mColor == RBTreeColor.Black) && (mLeft.mLeft == null || mLeft.mLeft.mColor == RBTreeColor.Black))
            {
              mLeft.mColor = RBTreeColor.Red;
              x = x.mParent;
            }
            else
            {
              if (mLeft.mLeft == null || mLeft.mLeft.mColor == RBTreeColor.Black)
              {
                if (mLeft.mRight != null)
                  mLeft.mRight.mColor = RBTreeColor.Black;
                mLeft.mColor = RBTreeColor.Red;
                this.LeftRotate(mLeft);
                mLeft = x.mParent.mLeft;
              }
              mLeft.mColor = x.mParent.mColor;
              x.mParent.mColor = RBTreeColor.Black;
              if (mLeft.mLeft != null)
                mLeft.mLeft.mColor = RBTreeColor.Black;
              this.RightRotate(x.mParent);
              x = this.mRoot;
            }
          }
        }
      }
      x.mColor = RBTreeColor.Black;
    }

    public IList<T> Collection
    {
      get
      {
        if (this.mAdapter == null)
          this.mAdapter = new RBTreeBase<T, N, P>.CollectionAdapter<T, N, P>(this);
        return (IList<T>) this.mAdapter;
      }
    }

    private class CollectionAdapter<T1, N1, P1> : IList<T1>, ICollection<T1>, IEnumerable<T1>, ICollection, IEnumerable
      where N1 : RBTreeNodeBase<T1, P1>, new()
    {
      private RBTreeBase<T1, N1, P1> mTree;

      public CollectionAdapter(RBTreeBase<T1, N1, P1> aTree)
      {
        this.mTree = aTree;
      }

      public int Count
      {
        get
        {
          return this.mTree.Count;
        }
      }

      public bool IsSynchronized
      {
        get
        {
          return false;
        }
      }

      public object SyncRoot
      {
        get
        {
          return this.mTree.SyncRoot;
        }
      }

      public bool IsReadOnly
      {
        get
        {
          return false;
        }
      }

      public void Add(T1 item)
      {
        this.mTree.Add(item);
      }

      public void Clear()
      {
        this.mTree.Clear();
      }

      public bool Contains(T1 item)
      {
        return (object) this.mTree.Find(item) != null;
      }

      public bool Remove(T1 item)
      {
        return this.mTree.Remove(item);
      }

      public void CopyTo(T1[] array, int index)
      {
        foreach (T1 obj in (IEnumerable<T1>) this)
          array[index++] = obj;
      }

      public void CopyTo(Array array, int index)
      {
        foreach (T1 obj in (IEnumerable<T1>) this)
          array.SetValue((object) obj, index++);
      }

      IEnumerator<T1> IEnumerable<T1>.GetEnumerator()
      {
        return (IEnumerator<T1>) new RBTreeValueEnumerator<N1, T1, P1>(this.mTree);
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) new RBTreeValueEnumerator<N1, T1, P1>(this.mTree);
      }

      public int IndexOf(T1 item)
      {
        RBOrderedMultiTree<T1> mTree = this.mTree as RBOrderedMultiTree<T1>;
        if (mTree == null)
          return -1;
        RBOrderedTreeNode<T1> aItem = mTree.Find(item);
        if (aItem == null)
          return -1;
        return mTree.GetOrder(aItem);
      }

      public void Insert(int index, T1 item)
      {
        this.Add(item);
      }

      public void RemoveAt(int index)
      {
        RBOrderedMultiTree<T1> mTree = this.mTree as RBOrderedMultiTree<T1>;
        if (mTree == null)
          return;
        RBOrderedTreeNode<T1> byOrder = mTree.GetByOrder(index);
        if (byOrder == null)
          return;
        mTree.Delete((RBTreeNodeBase<T1, RBOrderedNodeParam>) byOrder);
      }

      public T1 this[int index]
      {
        get
        {
          RBOrderedMultiTree<T1> mTree = this.mTree as RBOrderedMultiTree<T1>;
          if (mTree == null)
            return default (T1);
          RBOrderedTreeNode<T1> byOrder = mTree.GetByOrder(index);
          if (byOrder != null)
            return byOrder.Key;
          return default (T1);
        }
        set
        {
          RBOrderedMultiTree<T1> mTree = this.mTree as RBOrderedMultiTree<T1>;
          if (mTree == null)
            return;
          RBOrderedTreeNode<T1> byOrder = mTree.GetByOrder(index);
          if (byOrder == null)
            return;
          mTree.Remove(byOrder);
          mTree.Add(value);
        }
      }
    }
  }
}
