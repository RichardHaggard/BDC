// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.AvlTreeNode`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Collections.Generic
{
  public class AvlTreeNode<ValueT>
  {
    internal static readonly AvlTreeNode<ValueT> NIL = new AvlTreeNode<ValueT>();
    internal AvlTreeNode<ValueT> left;
    internal AvlTreeNode<ValueT> right;
    internal long rank;
    internal ValueT value;

    static AvlTreeNode()
    {
      AvlTreeNode<ValueT>.NIL.left = AvlTreeNode<ValueT>.NIL.right = AvlTreeNode<ValueT>.NIL;
    }

    public AvlTreeNode<ValueT> Left
    {
      get
      {
        return this.left;
      }
    }

    public AvlTreeNode<ValueT> Right
    {
      get
      {
        return this.right;
      }
    }

    public ValueT Value
    {
      get
      {
        return this.value;
      }
    }

    internal AvlTreeNode()
      : this(default (ValueT))
    {
    }

    internal AvlTreeNode(ValueT v)
    {
      this.rank = 28L;
      this.value = v;
    }

    internal void clear()
    {
      this.rank = 28L;
      this.value = default (ValueT);
      this.left = this.right = (AvlTreeNode<ValueT>) null;
    }

    internal bool nil
    {
      get
      {
        return this.right == this;
      }
    }

    internal void mkNil()
    {
      this.right = this;
      this.rank |= 8L;
    }

    internal void start(AvlTreeNode<ValueT> t)
    {
      this.right = t;
      this.rank &= 4294967287L;
    }
  }
}
