// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.AvlTree`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Telerik.Collections.Generic
{
  public sealed class AvlTree<ValueT> : IList<ValueT>, ICollection<ValueT>, IEnumerable<ValueT>, IList, ICollection, IEnumerable, IDisposable
  {
    private const int STACK_SIZE = 32;
    private const long zFlags = 4294967280;
    private AvlTreeNode<ValueT> _root;
    private long _count;
    private int _height;
    private IComparer<ValueT> _comparer;
    private int _version;
    private AvlTreeNode<ValueT>[] _nodepath;
    private int[] _comparecache;
    private long[] _countcache;

    private AvlTreeNode<ValueT>[] NodePath
    {
      get
      {
        if (this._nodepath == null)
          this._nodepath = new AvlTreeNode<ValueT>[32];
        return this._nodepath;
      }
    }

    private int[] CompareCache
    {
      get
      {
        if (this._comparecache == null)
          this._comparecache = new int[32];
        return this._comparecache;
      }
    }

    public AvlTree()
      : this((IComparer<ValueT>) Comparer<ValueT>.Default)
    {
    }

    public AvlTree(IComparer<ValueT> comparer)
    {
      this._root = new AvlTreeNode<ValueT>();
      this._root.mkNil();
      this._count = 0L;
      this._height = 0;
      this._comparer = comparer;
      this._version = 0;
    }

    public AvlTreeNode<ValueT> Root
    {
      get
      {
        if (!this._root.nil)
          return this._root.Right;
        return (AvlTreeNode<ValueT>) null;
      }
    }

    private void FreeStacks()
    {
      this._nodepath = (AvlTreeNode<ValueT>[]) null;
      this._comparecache = (int[]) null;
      this._countcache = (long[]) null;
    }

    public override string ToString()
    {
      if (this._root.nil)
        return "[]";
      return this.Repr(new StringBuilder()).ToString();
    }

    public void Dispose()
    {
      if (this._root == null)
        return;
      this._root.mkNil();
      this._root = (AvlTreeNode<ValueT>) null;
      this._count = 0L;
      this._height = 0;
      this._comparer = (IComparer<ValueT>) null;
      this._version = 0;
      this.FreeStacks();
    }

    public int Size
    {
      get
      {
        return (int) this._count;
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this._root.nil;
      }
    }

    public void MkEmpty()
    {
      this._root.mkNil();
      this._count = 0L;
      this._height = 0;
      ++this._version;
    }

    public bool Contains(ValueT value)
    {
      return this.IndexOf(value) >= 0;
    }

    public ValueT Find(ValueT target)
    {
      if (!this._root.nil)
      {
        AvlTreeNode<ValueT> avlTreeNode = AvlTree<ValueT>.Lookup(this._root.right, target, this._comparer);
        if (avlTreeNode != null)
          return avlTreeNode.value;
      }
      return default (ValueT);
    }

    public AvlTreeNode<ValueT> FindNode(ValueT target)
    {
      if (!this._root.nil)
        return AvlTree<ValueT>.Lookup(this._root.right, target, this._comparer);
      return (AvlTreeNode<ValueT>) null;
    }

    public bool FindAtMost(ValueT target, out ValueT value)
    {
      value = default (ValueT);
      if (this._root.nil)
        return false;
      AvlTreeNode<ValueT> a = this._root.right;
      bool flag = false;
      while (true)
      {
        int num = this._comparer.Compare(target, a.value);
        if (num < 0)
        {
          if (!AvlTree<ValueT>.isLt(a))
            a = a.left;
          else
            break;
        }
        else
        {
          value = a.value;
          flag = true;
          if (num != 0 && !AvlTree<ValueT>.isRt(a))
            a = a.right;
          else
            break;
        }
      }
      return flag;
    }

    public bool FindAtLeast(ValueT target, out ValueT value)
    {
      value = default (ValueT);
      if (this._root.nil)
        return false;
      AvlTreeNode<ValueT> a = this._root.right;
      bool flag = false;
      while (true)
      {
        int num = this._comparer.Compare(target, a.value);
        if (num > 0)
        {
          if (!AvlTree<ValueT>.isRt(a))
            a = a.right;
          else
            break;
        }
        else
        {
          value = a.value;
          flag = true;
          if (num != 0 && !AvlTree<ValueT>.isLt(a))
            a = a.left;
          else
            break;
        }
      }
      return flag;
    }

    public ValueT FindByIndex(int index)
    {
      if (index < 0 || (long) index >= this._count)
        throw new IndexOutOfRangeException();
      return (index != 0 ? ((long) (index + 1) != this._count ? AvlTree<ValueT>.Select(this._root.right, index + 1) : AvlTree<ValueT>.Rightmost(this._root.right)) : AvlTree<ValueT>.Leftmost(this._root.right)).value;
    }

    public ValueT First
    {
      get
      {
        if (this._root.nil)
          throw new InvalidOperationException("Avl:empty");
        return AvlTree<ValueT>.Leftmost(this._root.right).value;
      }
    }

    public ValueT Last
    {
      get
      {
        if (this._root.nil)
          throw new InvalidOperationException("Avl:empty");
        return AvlTree<ValueT>.Rightmost(this._root.right).value;
      }
    }

    private static AvlTreeNode<ValueT> Leftmost(AvlTreeNode<ValueT> a)
    {
      while (!AvlTree<ValueT>.isLt(a))
        a = a.left;
      return a;
    }

    private static AvlTreeNode<ValueT> Rightmost(AvlTreeNode<ValueT> a)
    {
      while (!AvlTree<ValueT>.isRt(a))
        a = a.right;
      return a;
    }

    private static AvlTreeNode<ValueT> Select(AvlTreeNode<ValueT> a, int k)
    {
      long num1 = (long) k;
      long num2;
      while (num1 != (num2 = a.rank >> 4))
      {
        if (num1 < num2)
        {
          a = a.left;
        }
        else
        {
          num1 -= num2;
          a = a.right;
        }
      }
      return a;
    }

    public int Index(ValueT value)
    {
      if (this._root.nil)
        return -1;
      AvlTreeNode<ValueT> a = this._root.right;
      long num1 = 0;
      long num2 = 0;
      long num3 = this._count + 1L;
      while (true)
      {
        int num4 = this._comparer.Compare(value, a.value);
        if (num4 <= 0)
        {
          num3 = num1 + (a.rank >> 4);
          if (num4 == 0)
            num2 = num3;
          if (!AvlTree<ValueT>.isLt(a))
            a = a.left;
          else
            break;
        }
        else
        {
          num1 += a.rank >> 4;
          if (!AvlTree<ValueT>.isRt(a))
            a = a.right;
          else
            break;
        }
      }
      if (num2 != 0L)
        return (int) (num2 - 1L);
      return -(int) num3;
    }

    public int Index(ValueT value, int lo)
    {
      throw new NotImplementedException();
    }

    public int Index(ValueT value, int lo, int hi)
    {
      throw new NotImplementedException();
    }

    public int LastIndex(ValueT value)
    {
      if (this._root.nil)
        return -1;
      AvlTreeNode<ValueT> a = this._root.right;
      long num1 = 0;
      long num2 = 0;
      long num3 = this._count + 1L;
      while (true)
      {
        int num4 = this._comparer.Compare(value, a.value);
        if (num4 >= 0)
        {
          num1 += a.rank >> 4;
          if (num4 == 0)
            num2 = num1;
          if (!AvlTree<ValueT>.isRt(a))
            a = a.right;
          else
            break;
        }
        else
        {
          num3 = num1 + (a.rank >> 4);
          if (!AvlTree<ValueT>.isLt(a))
            a = a.left;
          else
            break;
        }
      }
      if (num2 != 0L)
        return (int) (num2 - 1L);
      return -(int) num3;
    }

    public void Span(ValueT value, out int loIndex, out int hiIndex)
    {
      long num1 = this._count + 1L;
      long num2 = 0;
      if (!this._root.nil)
      {
        AvlTreeNode<ValueT> a1 = this._root.right;
        int num3;
        while (true)
        {
          num3 = this._comparer.Compare(value, a1.value);
          if (num3 != 0)
          {
            if (num3 < 0)
            {
              num1 = num2 + (a1.rank >> 4);
              if (!AvlTree<ValueT>.isLt(a1))
                a1 = a1.left;
              else
                goto label_22;
            }
            else
            {
              num2 += a1.rank >> 4;
              if (!AvlTree<ValueT>.isRt(a1))
                a1 = a1.right;
              else
                goto label_22;
            }
          }
          else
            break;
        }
        AvlTreeNode<ValueT> avlTreeNode = a1;
        long num4 = num2;
        while (true)
        {
          if (num3 <= 0)
          {
            num1 = num4 + (a1.rank >> 4);
            if (!AvlTree<ValueT>.isLt(a1))
              a1 = a1.left;
            else
              break;
          }
          else
          {
            num4 += a1.rank >> 4;
            if (!AvlTree<ValueT>.isRt(a1))
              a1 = a1.right;
            else
              break;
          }
          num3 = this._comparer.Compare(value, a1.value);
        }
        AvlTreeNode<ValueT> a2 = avlTreeNode;
        while (true)
        {
          if (num3 < 0)
          {
            if (!AvlTree<ValueT>.isLt(a2))
              a2 = a2.left;
            else
              break;
          }
          else
          {
            num2 += a2.rank >> 4;
            if (!AvlTree<ValueT>.isRt(a2))
              a2 = a2.right;
            else
              break;
          }
          num3 = this._comparer.Compare(value, a2.value);
        }
      }
label_22:
      long num5 = num1 - 1L;
      loIndex = (int) num5;
      hiIndex = (int) num2;
    }

    public void Span(ValueT loValue, ValueT hiValue, out int loIndex, out int hiIndex)
    {
      long num1 = this._count + 1L;
      long num2 = 0;
      if (!this._root.nil)
      {
        long num3 = 0;
        if (this._comparer.Compare(loValue, hiValue) > 0)
        {
          ValueT valueT = loValue;
          loValue = hiValue;
          hiValue = valueT;
        }
        AvlTreeNode<ValueT> a1 = this._root.right;
        while (true)
        {
          for (; this._comparer.Compare(loValue, a1.value) > 0; a1 = a1.right)
          {
            num3 += a1.rank >> 4;
            if (AvlTree<ValueT>.isRt(a1))
              goto label_9;
          }
          num1 = num3 + (a1.rank >> 4);
          if (!AvlTree<ValueT>.isLt(a1))
            a1 = a1.left;
          else
            break;
        }
label_9:
        AvlTreeNode<ValueT> a2 = this._root.right;
        while (true)
        {
          for (; this._comparer.Compare(hiValue, a2.value) >= 0; a2 = a2.right)
          {
            num2 += a2.rank >> 4;
            if (AvlTree<ValueT>.isRt(a2))
              goto label_15;
          }
          if (!AvlTree<ValueT>.isLt(a2))
            a2 = a2.left;
          else
            break;
        }
      }
label_15:
      long num4 = num1 - 1L;
      loIndex = (int) num4;
      hiIndex = (int) num2;
    }

    public void ForEach(Action<ValueT> act)
    {
      if (this._root.nil)
        return;
      AvlTreeNode<ValueT> a = this._root.right;
      while (true)
      {
        while (!AvlTree<ValueT>.isLt(a))
          a = a.left;
        while (true)
        {
          act(a.value);
          if (AvlTree<ValueT>.isRt(a))
          {
            if (a.right != this._root)
              a = a.right;
            else
              goto label_9;
          }
          else
            break;
        }
        a = a.right;
      }
label_9:;
    }

    public void ForEachBackwards(Action<ValueT> act)
    {
      if (this._root.nil)
        return;
      AvlTreeNode<ValueT> a = this._root.right;
      while (true)
      {
        while (!AvlTree<ValueT>.isRt(a))
          a = a.right;
        while (true)
        {
          act(a.value);
          if (AvlTree<ValueT>.isLt(a))
          {
            if (a.left != this._root)
              a = a.left;
            else
              goto label_9;
          }
          else
            break;
        }
        a = a.left;
      }
label_9:;
    }

    public void ForEach(Action<ValueT> act, Predicate<ValueT> stopcondition)
    {
      if (this._root.nil)
        return;
      AvlTreeNode<ValueT> a = this._root.right;
      while (true)
      {
        while (!AvlTree<ValueT>.isLt(a))
          a = a.left;
        for (; !stopcondition(a.value); a = a.right)
        {
          act(a.value);
          if (AvlTree<ValueT>.isRt(a))
          {
            if (a.right == this._root)
              return;
          }
          else
            break;
        }
        a = a.right;
      }
    }

    public void ForEachBackwards(Action<ValueT> act, Predicate<ValueT> stopcondition)
    {
      if (this._root.nil)
        return;
      AvlTreeNode<ValueT> a = this._root.right;
      while (true)
      {
        while (!AvlTree<ValueT>.isRt(a))
          a = a.right;
        for (; !stopcondition(a.value); a = a.left)
        {
          act(a.value);
          if (AvlTree<ValueT>.isLt(a))
          {
            if (a.left == this._root)
              return;
          }
          else
            break;
        }
        a = a.left;
      }
    }

    private static AvlTreeNode<ValueT> Lookup(
      AvlTreeNode<ValueT> a,
      ValueT v,
      IComparer<ValueT> c)
    {
      while (true)
      {
        int num = c.Compare(v, a.value);
        if (num != 0)
        {
          if (num < 0)
          {
            if (!AvlTree<ValueT>.isLt(a))
              a = a.left;
            else
              goto label_7;
          }
          else if (!AvlTree<ValueT>.isRt(a))
            a = a.right;
          else
            goto label_7;
        }
        else
          break;
      }
      return a;
label_7:
      return (AvlTreeNode<ValueT>) null;
    }

    public string Repr()
    {
      if (this._root.nil)
        return "[]";
      return this.Repr(new StringBuilder()).ToString();
    }

    private StringBuilder Repr(StringBuilder buf)
    {
      IEnumerator<ValueT> forwardEnumerator = this.GetForwardEnumerator();
      long count = this._count;
      buf.Append("[");
      try
      {
        while (true)
        {
          forwardEnumerator.MoveNext();
          buf.Append(forwardEnumerator.Current.ToString());
          if (0L != --count)
            buf.Append(", ");
          else
            break;
        }
      }
      finally
      {
        forwardEnumerator.Dispose();
      }
      buf.Append("]");
      return buf;
    }

    public void Insert(ValueT value, bool duplicateallowed, bool overwrite)
    {
      if (this._root.nil)
      {
        this._root.right = new AvlTreeNode<ValueT>(value);
        this._root.right.left = this._root.right.right = this._root;
        AvlTree<ValueT>.unRt(this._root);
        this._height = 1;
      }
      else
      {
        int[] compareCache = this.CompareCache;
        int n = 1;
        AvlTreeNode<ValueT> t;
        AvlTreeNode<ValueT> a1 = t = this._root;
        AvlTreeNode<ValueT> a2 = a1.right;
        AvlTreeNode<ValueT> u = (AvlTreeNode<ValueT>) null;
        compareCache[0] = 1;
        if (duplicateallowed)
        {
          while (true)
          {
            int num = this._comparer.Compare(value, a2.value);
            if (AvlTree<ValueT>.skewed(a2))
            {
              t = a2;
              u = a1;
              n = 0;
            }
            a1 = a2;
            if (num <= 0)
            {
              compareCache[n] = 0;
              a1.rank += 16L;
              if (!AvlTree<ValueT>.isLt(a2))
                a2 = a1.left;
              else
                break;
            }
            else
            {
              compareCache[n] = 1;
              if (!AvlTree<ValueT>.isRt(a2))
                a2 = a1.right;
              else
                break;
            }
            ++n;
          }
        }
        else
        {
          AvlTreeNode<ValueT>[] nodePath = this.NodePath;
          int num1 = 0;
          while (true)
          {
            int num2 = this._comparer.Compare(value, a2.value);
            if (num2 != 0)
            {
              if (AvlTree<ValueT>.skewed(a2))
              {
                t = a2;
                u = a1;
                n = 0;
              }
              a1 = a2;
              if (num2 < 0)
              {
                compareCache[n] = 0;
                a1.rank += 16L;
                if (!AvlTree<ValueT>.isLt(a2))
                {
                  a2 = a1.left;
                  nodePath[num1++] = a1;
                }
                else
                  goto label_25;
              }
              else
              {
                compareCache[n] = 1;
                if (!AvlTree<ValueT>.isRt(a2))
                  a2 = a1.right;
                else
                  goto label_25;
              }
              ++n;
            }
            else
              break;
          }
          if (overwrite)
          {
            a2.value = value;
            return;
          }
          while (num1 != 0)
            nodePath[--num1].rank -= 16L;
          return;
        }
label_25:
        AvlTreeNode<ValueT> avlTreeNode = new AvlTreeNode<ValueT>(value);
        if (compareCache[n] == 0)
        {
          avlTreeNode.left = a1.left;
          AvlTree<ValueT>.unLt(a1);
          a1.left = avlTreeNode;
          avlTreeNode.right = a1;
        }
        else
        {
          avlTreeNode.right = a1.right;
          AvlTree<ValueT>.unRt(a1);
          a1.right = avlTreeNode;
          avlTreeNode.left = a1;
        }
        this._height += AvlTree<ValueT>.FixInsert(t, u, compareCache, n);
      }
      ++this._count;
      ++this._version;
    }

    public void InsertWithDuplicates(ValueT value)
    {
      int num = this.LastIndex(value);
      if (num >= 0)
        this.Insert(num + 1, value);
      else
        this.Insert(value, true, false);
    }

    public void InsertUnique(ValueT value, bool overwrite)
    {
      this.Insert(value, false, overwrite);
    }

    public void InsertUnique(ValueT value)
    {
      this.Insert(value, false, false);
    }

    public void Insert(int index, ValueT value)
    {
      if (index < 0 || (long) index > this._count)
        throw new IndexOutOfRangeException("Avl:Insert");
      AvlTreeNode<ValueT> mid = new AvlTreeNode<ValueT>(value);
      if (index == 0)
        this._height += AvlTree<ValueT>.RJoin(mid, AvlTreeNode<ValueT>.NIL, this._root, 0, 0L, true);
      else if ((long) index == this._count)
      {
        this._height += AvlTree<ValueT>.LJoin(mid, this._root, AvlTreeNode<ValueT>.NIL, 0, this._count, true);
      }
      else
      {
        int[] compareCache = this.CompareCache;
        int n = 1;
        AvlTreeNode<ValueT> t;
        AvlTreeNode<ValueT> avlTreeNode1 = t = this._root;
        AvlTreeNode<ValueT> a1 = avlTreeNode1.right;
        AvlTreeNode<ValueT> u = (AvlTreeNode<ValueT>) null;
        compareCache[0] = 1;
        long num1 = (long) index;
        while (true)
        {
          if (AvlTree<ValueT>.skewed(a1))
          {
            t = a1;
            u = avlTreeNode1;
            n = 0;
          }
          long num2;
          if (num1 != (num2 = a1.rank >> 4))
          {
            avlTreeNode1 = a1;
            if (num1 < num2)
            {
              avlTreeNode1.rank += 16L;
              a1 = avlTreeNode1.left;
              compareCache[n++] = 0;
            }
            else
            {
              num1 -= num2;
              a1 = avlTreeNode1.right;
              compareCache[n++] = 1;
            }
          }
          else
            break;
        }
        AvlTreeNode<ValueT> avlTreeNode2 = new AvlTreeNode<ValueT>(value);
        compareCache[n] = 1;
        if (AvlTree<ValueT>.isRt(a1))
        {
          avlTreeNode2.right = a1.right;
          AvlTree<ValueT>.unRt(a1);
          a1.right = avlTreeNode2;
          avlTreeNode2.left = a1;
        }
        else
        {
          AvlTreeNode<ValueT> avlTreeNode3 = a1;
          AvlTreeNode<ValueT> a2 = avlTreeNode3.right;
          ++n;
          while (true)
          {
            if (AvlTree<ValueT>.skewed(a2))
            {
              t = a2;
              u = avlTreeNode3;
              n = 0;
            }
            a2.rank += 16L;
            if (!AvlTree<ValueT>.isLt(a2))
            {
              avlTreeNode3 = a2;
              a2 = avlTreeNode3.left;
              compareCache[n++] = 0;
            }
            else
              break;
          }
          compareCache[n] = 0;
          avlTreeNode2.left = a2.left;
          AvlTree<ValueT>.unLt(a2);
          a2.left = avlTreeNode2;
          avlTreeNode2.right = a2;
        }
        this._height += AvlTree<ValueT>.FixInsert(t, u, compareCache, n);
      }
      ++this._count;
      ++this._version;
    }

    public void InsertFirst(ValueT value)
    {
      this._height += AvlTree<ValueT>.RJoin(new AvlTreeNode<ValueT>(value), AvlTreeNode<ValueT>.NIL, this._root, 0, 0L, true);
      ++this._count;
      ++this._version;
    }

    public void InsertLast(ValueT value)
    {
      this._height += AvlTree<ValueT>.LJoin(new AvlTreeNode<ValueT>(value), this._root, AvlTreeNode<ValueT>.NIL, 0, this._count, true);
      ++this._count;
      ++this._version;
    }

    private static int FixInsert(AvlTreeNode<ValueT> t, AvlTreeNode<ValueT> u, int[] ad, int n)
    {
      if (n != 0)
      {
        int num1 = 1;
        AvlTreeNode<ValueT> avlTreeNode = t;
        int num2 = ad[0];
        while (true)
        {
          avlTreeNode = num2 == 0 ? avlTreeNode.left : avlTreeNode.right;
          if (num1 != n)
          {
            num2 = ad[num1++];
            avlTreeNode.rank |= (long) (uint) (1 << num2);
          }
          else
            break;
        }
        avlTreeNode.rank |= (long) (uint) (1 << ad[n]);
      }
      if (u == null)
        return 1;
      if (0L == (t.rank >> ad[0] & 1L))
      {
        t.rank &= 4294967292L;
      }
      else
      {
        AvlTreeNode<ValueT> avlTreeNode;
        if (ad[0] == 0)
        {
          t.rank &= 4294967294L;
          if (AvlTree<ValueT>.lSkewed(t.left))
          {
            avlTreeNode = AvlTree<ValueT>.rotR(t);
          }
          else
          {
            avlTreeNode = AvlTree<ValueT>.rotLR(t);
            avlTreeNode.left.rank &= 4294967293L;
            switch (avlTreeNode.rank & 3L)
            {
              case 1:
                t.rank |= 2L;
                break;
              case 2:
                avlTreeNode.left.rank |= 1L;
                break;
            }
          }
        }
        else
        {
          t.rank &= 4294967293L;
          if (AvlTree<ValueT>.rSkewed(t.right))
          {
            avlTreeNode = AvlTree<ValueT>.rotL(t);
          }
          else
          {
            avlTreeNode = AvlTree<ValueT>.rotRL(t);
            avlTreeNode.right.rank &= 4294967294L;
            switch (avlTreeNode.rank & 3L)
            {
              case 1:
                avlTreeNode.right.rank |= 2L;
                break;
              case 2:
                t.rank |= 1L;
                break;
            }
          }
        }
        avlTreeNode.rank &= 4294967292L;
        if (t == u.left)
          u.left = avlTreeNode;
        else
          u.right = avlTreeNode;
      }
      return 0;
    }

    public ValueT Replace(int index, ValueT newValue)
    {
      if (index < 0 || (long) index >= this._count)
        throw new IndexOutOfRangeException();
      AvlTreeNode<ValueT> avlTreeNode = index != 0 ? ((long) (index + 1) != this._count ? AvlTree<ValueT>.Select(this._root.right, index + 1) : AvlTree<ValueT>.Rightmost(this._root.right)) : AvlTree<ValueT>.Leftmost(this._root.right);
      ValueT valueT = avlTreeNode.value;
      avlTreeNode.value = newValue;
      return valueT;
    }

    public ValueT ReplaceFirst(ValueT newValue)
    {
      if (this._root.nil)
        return default (ValueT);
      AvlTreeNode<ValueT> avlTreeNode = AvlTree<ValueT>.Leftmost(this._root.right);
      ValueT valueT = avlTreeNode.value;
      avlTreeNode.value = newValue;
      return valueT;
    }

    public ValueT ReplaceLast(ValueT newValue)
    {
      if (this._root.nil)
        return default (ValueT);
      AvlTreeNode<ValueT> avlTreeNode = AvlTree<ValueT>.Rightmost(this._root.right);
      ValueT valueT = avlTreeNode.value;
      avlTreeNode.value = newValue;
      return valueT;
    }

    public AvlTree<ValueT> Append(AvlTree<ValueT> that)
    {
      if (that.IsEmpty)
        return this;
      if (this.IsEmpty)
      {
        this._root = that._root;
        this._height = that._height;
        that._root = new AvlTreeNode<ValueT>();
      }
      else
      {
        int num = that._height - this._height;
        if (num <= 0)
        {
          AvlTreeNode<ValueT> leftmost;
          int delta = num - AvlTree<ValueT>.RemoveLeftmost(that._root, out leftmost, false, this.NodePath);
          this._height += AvlTree<ValueT>.LJoin(leftmost, this._root, that._root, delta, this._count, true);
        }
        else
        {
          AvlTreeNode<ValueT> rightmost;
          int delta = num + AvlTree<ValueT>.RemoveRightmost(this._root, out rightmost, false, this.NodePath);
          this._height = AvlTree<ValueT>.RJoin(rightmost, this._root, that._root, delta, this._count - 1L, true) + that._height;
          this._root.right = that._root.right;
        }
        AvlTree<ValueT>.Rightmost(this._root.right).right = this._root;
      }
      this._count += that._count;
      that.MkEmpty();
      ++this._version;
      return this;
    }

    private static int LJoin(
      AvlTreeNode<ValueT> mid,
      AvlTreeNode<ValueT> R,
      AvlTreeNode<ValueT> S,
      int delta,
      long nR,
      bool attach)
    {
      AvlTreeNode<ValueT> a1;
      AvlTreeNode<ValueT> a2 = a1 = R;
      AvlTreeNode<ValueT> right1 = a2.right;
      AvlTreeNode<ValueT> avlTreeNode1 = (AvlTreeNode<ValueT>) null;
      if (S.nil)
      {
        while (!AvlTree<ValueT>.isRt(a2))
        {
          if (AvlTree<ValueT>.skewed(right1))
          {
            a1 = right1;
            avlTreeNode1 = a2;
          }
          a2 = right1;
          right1 = a2.right;
          nR -= AvlTree<ValueT>.GetRank(a2);
        }
        if (attach)
          mid.right = R;
        AvlTree<ValueT>.Rt(mid);
      }
      else
      {
        for (; delta < -1; delta += 1 + (int) (a2.rank & 1L))
        {
          if (AvlTree<ValueT>.skewed(right1))
          {
            a1 = right1;
            avlTreeNode1 = a2;
          }
          a2 = right1;
          right1 = a2.right;
          nR -= AvlTree<ValueT>.GetRank(a2);
        }
        mid.right = S.right;
        AvlTree<ValueT>.unRt(mid);
      }
      if (AvlTree<ValueT>.isRt(a2))
      {
        mid.left = a2;
        AvlTree<ValueT>.Lt(mid);
        AvlTree<ValueT>.unRt(a2);
      }
      else
      {
        mid.left = right1;
        AvlTree<ValueT>.unLt(mid);
        if (attach)
          AvlTree<ValueT>.Rightmost(right1).right = mid;
      }
      AvlTree<ValueT>.Rank(mid, nR + 1L);
      mid.rank = mid.rank & 4294967292L | (long) (uint) -delta;
      a2.right = mid;
      for (AvlTreeNode<ValueT> right2 = a1.right; right2 != mid; right2 = right2.right)
        right2.rank |= 2L;
      if (avlTreeNode1 == null)
        return 1;
      if (0L == (a1.rank & 2L))
      {
        a1.rank &= 4294967292L;
      }
      else
      {
        a1.rank &= 4294967293L;
        AvlTreeNode<ValueT> avlTreeNode2;
        if (AvlTree<ValueT>.rSkewed(a1.right))
        {
          avlTreeNode2 = AvlTree<ValueT>.rotL(a1);
        }
        else
        {
          avlTreeNode2 = AvlTree<ValueT>.rotRL(a1);
          avlTreeNode2.right.rank &= 4294967294L;
          switch (avlTreeNode2.rank & 3L)
          {
            case 1:
              avlTreeNode2.right.rank |= 2L;
              break;
            case 2:
              a1.rank |= 1L;
              break;
          }
        }
        avlTreeNode2.rank &= 4294967292L;
        avlTreeNode1.right = avlTreeNode2;
      }
      return 0;
    }

    private static int RJoin(
      AvlTreeNode<ValueT> mid,
      AvlTreeNode<ValueT> R,
      AvlTreeNode<ValueT> S,
      int delta,
      long nR,
      bool attach)
    {
      S.left = S.right;
      if (!S.nil)
        AvlTree<ValueT>.unLt(S);
      AvlTreeNode<ValueT> a1;
      AvlTreeNode<ValueT> a2 = a1 = S;
      AvlTreeNode<ValueT> a3 = a2.right;
      AvlTreeNode<ValueT> avlTreeNode1 = (AvlTreeNode<ValueT>) null;
      ++nR;
      if (R.nil)
      {
        while (!AvlTree<ValueT>.isLt(a2))
        {
          if (AvlTree<ValueT>.skewed(a3))
          {
            a1 = a3;
            avlTreeNode1 = a2;
          }
          a2 = a3;
          a2.rank += nR << 4;
          a3 = a2.left;
        }
        if (attach)
          mid.left = S;
        AvlTree<ValueT>.Lt(mid);
      }
      else
      {
        for (; delta > 1; delta -= 1 + (int) (a2.rank >> 1 & 1L))
        {
          if (AvlTree<ValueT>.skewed(a3))
          {
            a1 = a3;
            avlTreeNode1 = a2;
          }
          a2 = a3;
          a2.rank += nR << 4;
          a3 = a2.left;
        }
        mid.left = R.right;
        AvlTree<ValueT>.unLt(mid);
      }
      if (AvlTree<ValueT>.isLt(a2))
      {
        mid.right = a2;
        AvlTree<ValueT>.Rt(mid);
        AvlTree<ValueT>.unLt(a2);
      }
      else
      {
        mid.right = a3;
        AvlTree<ValueT>.unRt(mid);
        if (attach)
          AvlTree<ValueT>.Leftmost(a3).left = mid;
      }
      AvlTree<ValueT>.Rank(mid, nR);
      mid.rank = mid.rank & 4294967292L | (long) delta << 1;
      a2.left = mid;
      for (AvlTreeNode<ValueT> left = a1.left; left != mid; left = left.left)
        left.rank |= 1L;
      int num = 1;
      if (avlTreeNode1 != null)
      {
        if (0L == (a1.rank & 1L))
        {
          a1.rank &= 4294967292L;
        }
        else
        {
          a1.rank &= 4294967294L;
          AvlTreeNode<ValueT> avlTreeNode2;
          if (AvlTree<ValueT>.lSkewed(a1.left))
          {
            avlTreeNode2 = AvlTree<ValueT>.rotR(a1);
          }
          else
          {
            avlTreeNode2 = AvlTree<ValueT>.rotLR(a1);
            avlTreeNode2.left.rank &= 4294967293L;
            switch (avlTreeNode2.rank & 3L)
            {
              case 1:
                a1.rank |= 2L;
                break;
              case 2:
                avlTreeNode2.left.rank |= 1L;
                break;
            }
          }
          avlTreeNode2.rank &= 4294967292L;
          avlTreeNode1.left = avlTreeNode2;
        }
        num = 0;
      }
      S.right = S.left;
      S.left = (AvlTreeNode<ValueT>) null;
      AvlTree<ValueT>.Lt(S);
      AvlTree<ValueT>.unRt(S);
      return num;
    }

    public ValueT SplitAt(int index, out AvlTree<ValueT> avl0, out AvlTree<ValueT> avl1)
    {
      if (index < 0 || (long) index >= this._count)
        throw new IndexOutOfRangeException("Avl:Split");
      AvlTreeNode<ValueT>[] nodePath = this.NodePath;
      int[] compareCache = this.CompareCache;
      long[] countCache = this.CountCache;
      long k = 0;
      long num1 = (long) index + 1L;
      AvlTreeNode<ValueT> s = this._root.right;
      long ns = this._count;
      int height = this._height;
      long num2;
      while (num1 != (num2 = s.rank >> 4))
      {
        nodePath[k] = s;
        countCache[k] = ns;
        if (num1 < num2)
        {
          compareCache[k] = 0;
          ns = num2 - 1L;
          s = s.left;
          height -= 1 + (int) (s.rank >> 1 & 1L);
        }
        else
        {
          num1 -= num2;
          ns -= num2;
          compareCache[k] = 1;
          s = s.right;
          height -= 1 + (int) (s.rank & 1L);
        }
        ++k;
      }
      ValueT valueT = s.value;
      avl0 = new AvlTree<ValueT>(this._comparer);
      avl1 = new AvlTree<ValueT>(this._comparer);
      AvlTree<ValueT>.DoSplit(avl0, avl1, s, ns, height, nodePath, compareCache, countCache, k);
      s.clear();
      this.MkEmpty();
      return valueT;
    }

    public ValueT Split(ValueT value, out AvlTree<ValueT> avl0, out AvlTree<ValueT> avl1)
    {
      throw new NotImplementedException();
    }

    private static void DoSplit(
      AvlTree<ValueT> avl0,
      AvlTree<ValueT> avl1,
      AvlTreeNode<ValueT> s,
      long ns,
      int hs,
      AvlTreeNode<ValueT>[] ap,
      int[] ad,
      long[] an,
      long k)
    {
      AvlTreeNode<ValueT> root1 = avl0._root;
      AvlTreeNode<ValueT> root2 = avl1._root;
      if (!AvlTree<ValueT>.isLt(s))
        root1.start(s.left);
      if (!AvlTree<ValueT>.isRt(s))
        root2.start(s.right);
      long num1 = AvlTree<ValueT>.GetRank(s) - 1L;
      long nR1 = ns - AvlTree<ValueT>.GetRank(s);
      int num2 = hs - 1 - (int) (s.rank >> 1 & 1L);
      int num3 = hs - 1 - (int) (s.rank & 1L);
      AvlTreeNode<ValueT> R = new AvlTreeNode<ValueT>();
      AvlTreeNode<ValueT> S = new AvlTreeNode<ValueT>();
      while (k != 0L)
      {
        AvlTreeNode<ValueT> avlTreeNode = ap[(IntPtr) --k];
        int delta1 = AvlTree<ValueT>.GetDelta(avlTreeNode);
        long nR2 = AvlTree<ValueT>.GetRank(avlTreeNode) - 1L;
        if (ad[k] == 0)
        {
          if (AvlTree<ValueT>.isRt(avlTreeNode))
            S.mkNil();
          else
            S.start(avlTreeNode.right);
          int num4 = num3;
          int num5 = hs + delta1;
          int delta2 = num5 - num4;
          if (delta2 <= 0)
          {
            num3 += AvlTree<ValueT>.LJoin(avlTreeNode, root2, S, delta2, nR1, false);
          }
          else
          {
            num3 = num5 + AvlTree<ValueT>.RJoin(avlTreeNode, root2, S, delta2, nR1, false);
            root2.start(S.right);
          }
          nR1 += an[k] - nR2;
          hs += delta1 <= 0 ? 1 : 2;
        }
        else
        {
          if (AvlTree<ValueT>.isLt(avlTreeNode))
            R.mkNil();
          else
            R.start(avlTreeNode.left);
          int num4 = hs - delta1;
          int delta2 = num2 - num4;
          if (delta2 <= 0)
          {
            num2 = num4 + AvlTree<ValueT>.LJoin(avlTreeNode, R, root1, delta2, nR2, false);
            root1.start(R.right);
          }
          else
            num2 += AvlTree<ValueT>.RJoin(avlTreeNode, R, root1, delta2, nR2, false);
          num1 += nR2 + 1L;
          hs += delta1 >= 0 ? 1 : 2;
        }
      }
      if (!root1.nil)
      {
        AvlTree<ValueT>.Leftmost(root1.right).left = root1;
        AvlTree<ValueT>.Rightmost(root1.right).right = root1;
      }
      if (!root2.nil)
      {
        AvlTree<ValueT>.Leftmost(root2.right).left = root2;
        AvlTree<ValueT>.Rightmost(root2.right).right = root2;
      }
      avl0._count = num1;
      avl0._height = num2;
      avl1._count = nR1;
      avl1._height = num3;
      R.right = S.right = (AvlTreeNode<ValueT>) null;
    }

    private long[] CountCache
    {
      get
      {
        if (this._countcache == null)
          this._countcache = new long[32];
        return this._countcache;
      }
    }

    public ValueT LTrim(int index)
    {
      if (index < 0 || (long) index >= this._count)
        throw new IndexOutOfRangeException("Avl:LTrim");
      if ((long) (index + 1) == this._count)
      {
        ValueT valueT = AvlTree<ValueT>.Rightmost(this._root.right).value;
        this.MkEmpty();
        return valueT;
      }
      AvlTreeNode<ValueT>[] nodePath = this.NodePath;
      int[] compareCache = this.CompareCache;
      long[] countCache = this.CountCache;
      long k = 0;
      long num1 = (long) index + 1L;
      AvlTreeNode<ValueT> s = this._root.right;
      long ns = this._count;
      int height = this._height;
      long num2;
      while (num1 != (num2 = s.rank >> 4))
      {
        nodePath[k] = s;
        countCache[k] = ns;
        if (num1 < num2)
        {
          compareCache[k] = 0;
          ns = num2 - 1L;
          s = s.left;
          height -= 1 + (int) (s.rank >> 1 & 1L);
        }
        else
        {
          num1 -= num2;
          ns -= num2;
          compareCache[k] = 1;
          s = s.right;
          height -= 1 + (int) (s.rank & 1L);
        }
        ++k;
      }
      ValueT valueT1 = s.value;
      this._height = AvlTree<ValueT>.DoLTrim(this._root, s, ns, height, nodePath, compareCache, countCache, k);
      s.clear();
      this._count -= (long) index + 1L;
      ++this._version;
      return valueT1;
    }

    public ValueT RTrim(int index)
    {
      if (index < 0 || (long) index >= this._count)
        throw new IndexOutOfRangeException("Avl:RTrim");
      if (index == 0)
      {
        ValueT valueT = AvlTree<ValueT>.Leftmost(this._root.right).value;
        this.MkEmpty();
        return valueT;
      }
      AvlTreeNode<ValueT>[] nodePath = this.NodePath;
      int[] compareCache = this.CompareCache;
      long[] countCache = this.CountCache;
      long k = 0;
      long num1 = (long) index + 1L;
      AvlTreeNode<ValueT> s = this._root.right;
      long ns = this._count;
      int height = this._height;
      long num2;
      while (num1 != (num2 = s.rank >> 4))
      {
        nodePath[k] = s;
        countCache[k] = ns;
        if (num1 < num2)
        {
          compareCache[k] = 0;
          ns = num2 - 1L;
          s = s.left;
          height -= 1 + (int) (s.rank >> 1 & 1L);
        }
        else
        {
          num1 -= num2;
          ns -= num2;
          compareCache[k] = 1;
          s = s.right;
          height -= 1 + (int) (s.rank & 1L);
        }
        ++k;
      }
      ValueT valueT1 = s.value;
      this._height = AvlTree<ValueT>.DoRTrim(this._root, s, ns, height, nodePath, compareCache, countCache, k);
      s.clear();
      this._count = (long) index;
      ++this._version;
      return valueT1;
    }

    private static int DoLTrim(
      AvlTreeNode<ValueT> R1,
      AvlTreeNode<ValueT> s,
      long ns,
      int hs,
      AvlTreeNode<ValueT>[] ap,
      int[] ad,
      long[] an,
      long k)
    {
      R1.mkNil();
      if (!AvlTree<ValueT>.isRt(s))
        R1.start(s.right);
      long nR = ns - AvlTree<ValueT>.GetRank(s);
      int num1 = hs - 1 - (int) (s.rank & 1L);
      AvlTreeNode<ValueT> S = new AvlTreeNode<ValueT>();
      while (k != 0L)
      {
        AvlTreeNode<ValueT> avlTreeNode = ap[(IntPtr) --k];
        int delta1 = AvlTree<ValueT>.GetDelta(avlTreeNode);
        long num2 = AvlTree<ValueT>.GetRank(avlTreeNode) - 1L;
        if (ad[k] == 0)
        {
          if (AvlTree<ValueT>.isRt(avlTreeNode))
            S.mkNil();
          else
            S.start(avlTreeNode.right);
          int num3 = num1;
          int num4 = hs + delta1;
          int delta2 = num4 - num3;
          if (delta2 <= 0)
          {
            num1 += AvlTree<ValueT>.LJoin(avlTreeNode, R1, S, delta2, nR, false);
          }
          else
          {
            num1 = num4 + AvlTree<ValueT>.RJoin(avlTreeNode, R1, S, delta2, nR, false);
            R1.start(S.right);
          }
          nR += an[k] - num2;
          hs += delta1 <= 0 ? 1 : 2;
        }
        else
        {
          avlTreeNode.clear();
          hs += delta1 >= 0 ? 1 : 2;
        }
      }
      if (!R1.nil)
      {
        AvlTree<ValueT>.Leftmost(R1.right).left = R1;
        AvlTree<ValueT>.Rightmost(R1.right).right = R1;
      }
      S.right = (AvlTreeNode<ValueT>) null;
      return num1;
    }

    private static int DoRTrim(
      AvlTreeNode<ValueT> R0,
      AvlTreeNode<ValueT> s,
      long ns,
      int hs,
      AvlTreeNode<ValueT>[] ap,
      int[] ad,
      long[] an,
      long k)
    {
      R0.mkNil();
      if (!AvlTree<ValueT>.isLt(s))
        R0.start(s.left);
      long num1 = AvlTree<ValueT>.GetRank(s) - 1L;
      int num2 = hs - 1 - (int) (s.rank >> 1 & 1L);
      AvlTreeNode<ValueT> R = new AvlTreeNode<ValueT>();
      while (k != 0L)
      {
        AvlTreeNode<ValueT> avlTreeNode = ap[(IntPtr) --k];
        int delta1 = AvlTree<ValueT>.GetDelta(avlTreeNode);
        long nR = AvlTree<ValueT>.GetRank(avlTreeNode) - 1L;
        if (ad[k] != 0)
        {
          if (AvlTree<ValueT>.isLt(avlTreeNode))
            R.mkNil();
          else
            R.start(avlTreeNode.left);
          int num3 = hs - delta1;
          int delta2 = num2 - num3;
          if (delta2 <= 0)
          {
            num2 = num3 + AvlTree<ValueT>.LJoin(avlTreeNode, R, R0, delta2, nR, false);
            R0.start(R.right);
          }
          else
            num2 += AvlTree<ValueT>.RJoin(avlTreeNode, R, R0, delta2, nR, false);
          num1 += nR + 1L;
          hs += delta1 >= 0 ? 1 : 2;
        }
        else
        {
          avlTreeNode.clear();
          hs += delta1 <= 0 ? 1 : 2;
        }
      }
      if (!R0.nil)
      {
        AvlTree<ValueT>.Leftmost(R0.right).left = R0;
        AvlTree<ValueT>.Rightmost(R0.right).right = R0;
      }
      R.right = (AvlTreeNode<ValueT>) null;
      return num2;
    }

    public bool Delete(ValueT value, out ValueT delValue)
    {
      delValue = default (ValueT);
      if (this._root.nil)
        return false;
      AvlTreeNode<ValueT>[] nodePath = this.NodePath;
      int[] compareCache = this.CompareCache;
      int n = 0;
      AvlTreeNode<ValueT> p = this._root;
      AvlTreeNode<ValueT> a = p.right;
      while (true)
      {
        int num = this._comparer.Compare(value, a.value);
        if (num != 0)
        {
          p = a;
          if (num < 0)
          {
            if (!AvlTree<ValueT>.isLt(a))
            {
              a = p.left;
              compareCache[n] = 0;
            }
            else
              break;
          }
          else if (!AvlTree<ValueT>.isRt(a))
          {
            a = p.right;
            compareCache[n] = 1;
          }
          else
            goto label_9;
          nodePath[n++] = p;
        }
        else
          goto label_12;
      }
      return false;
label_9:
      return false;
label_12:
      delValue = a.value;
      this._height -= AvlTree<ValueT>.FixDelete(this._root, a, p, nodePath, compareCache, n);
      --this._count;
      ++this._version;
      return true;
    }

    public void DeleteFirst(out ValueT delValue)
    {
      delValue = default (ValueT);
      if (this._root.nil)
        return;
      AvlTreeNode<ValueT> leftmost;
      this._height -= AvlTree<ValueT>.RemoveLeftmost(this._root, out leftmost, true, this.NodePath);
      delValue = leftmost.value;
      leftmost.clear();
      --this._count;
      ++this._version;
    }

    public void DeleteLast(out ValueT delValue)
    {
      delValue = default (ValueT);
      if (this._root.nil)
        return;
      AvlTreeNode<ValueT> rightmost;
      this._height -= AvlTree<ValueT>.RemoveRightmost(this._root, out rightmost, true, this.NodePath);
      delValue = rightmost.value;
      rightmost.clear();
      --this._count;
      ++this._version;
    }

    public void DeleteLast()
    {
      if (this._root.nil)
        return;
      AvlTreeNode<ValueT> rightmost;
      this._height -= AvlTree<ValueT>.RemoveRightmost(this._root, out rightmost, true, this.NodePath);
      ValueT valueT = rightmost.value;
      rightmost.clear();
      --this._count;
      ++this._version;
    }

    public void DeleteAt(int index, out ValueT delValue)
    {
      delValue = default (ValueT);
      if (index < 0 || (long) index >= this._count)
        throw new IndexOutOfRangeException();
      AvlTreeNode<ValueT> avlTreeNode;
      if (index == 0)
        this._height -= AvlTree<ValueT>.RemoveLeftmost(this._root, out avlTreeNode, true, this.NodePath);
      else if ((long) (index + 1) == this._count)
      {
        this._height -= AvlTree<ValueT>.RemoveRightmost(this._root, out avlTreeNode, true, this.NodePath);
      }
      else
      {
        AvlTreeNode<ValueT>[] nodePath = this.NodePath;
        int[] compareCache = this.CompareCache;
        int n = 0;
        AvlTreeNode<ValueT> p = this._root;
        AvlTreeNode<ValueT> a = p.right;
        long num1 = (long) index + 1L;
        long num2;
        while (num1 != (num2 = a.rank >> 4))
        {
          nodePath[n] = p = a;
          if (num1 < num2)
          {
            a = p.left;
            compareCache[n++] = 0;
          }
          else
          {
            num1 -= num2;
            a = p.right;
            compareCache[n++] = 1;
          }
        }
        avlTreeNode = a;
        this._height -= AvlTree<ValueT>.FixDelete(this._root, a, p, nodePath, compareCache, n);
      }
      delValue = avlTreeNode.value;
      avlTreeNode.clear();
      --this._count;
      ++this._version;
    }

    private static int RemoveLeftmost(
      AvlTreeNode<ValueT> R,
      out AvlTreeNode<ValueT> leftmost,
      bool detach,
      AvlTreeNode<ValueT>[] ap)
    {
      int num1 = 0;
      AvlTreeNode<ValueT> a1 = R;
      AvlTreeNode<ValueT> a2;
      for (a2 = a1.right; !AvlTree<ValueT>.isLt(a2); a2 = a1.left)
      {
        ap[num1++] = a1 = a2;
        a1.rank -= 16L;
      }
      if (AvlTree<ValueT>.isRt(a2))
      {
        if (num1 == 0)
        {
          R.mkNil();
        }
        else
        {
          AvlTree<ValueT>.Lt(a1);
          if (detach)
            a1.left = a2.left;
        }
      }
      else
      {
        if (detach)
          AvlTree<ValueT>.Leftmost(a2.right).left = a2.left;
        if (num1 == 0)
          R.right = a2.right;
        else
          a1.left = a2.right;
      }
      leftmost = a2;
      a2.left = a2.right = (AvlTreeNode<ValueT>) null;
      while (num1 != 0)
      {
        AvlTreeNode<ValueT> a3 = ap[--num1];
        long num2 = a3.rank & 3L;
        long num3;
        if ((num2 & 2L) == 0L)
        {
          num3 = (num2 ^ 1L) << 1;
          a3.rank = a3.rank & 4294967292L | num3;
        }
        else
        {
          AvlTreeNode<ValueT> avlTreeNode;
          if ((a3.right.rank & 1L) == 0L)
          {
            avlTreeNode = AvlTree<ValueT>.rotL(a3);
            long num4 = avlTreeNode.rank & 3L;
            a3.rank = a3.rank & 4294967292L | num4 ^ 2L;
            avlTreeNode.rank = avlTreeNode.rank & 4294967292L | (num4 ^ 2L) >> 1;
          }
          else
          {
            avlTreeNode = AvlTree<ValueT>.rotRL(a3);
            long num4 = avlTreeNode.rank & 3L;
            avlTreeNode.rank &= 4294967292L;
            a3.rank = a3.rank & 4294967292L | num4 >> 1 & 1L;
            avlTreeNode.right.rank = avlTreeNode.right.rank & 4294967292L | (num4 & 1L) << 1;
          }
          if (num1 == 0)
            R.right = avlTreeNode;
          else
            ap[num1 - 1].left = avlTreeNode;
          num3 = avlTreeNode.rank & 3L;
        }
        if (num3 != 0L)
          return 0;
      }
      return 1;
    }

    private static int RemoveRightmost(
      AvlTreeNode<ValueT> S,
      out AvlTreeNode<ValueT> rightmost,
      bool detach,
      AvlTreeNode<ValueT>[] ap)
    {
      int num1 = 0;
      AvlTreeNode<ValueT> a1 = S;
      AvlTreeNode<ValueT> right;
      for (right = a1.right; !AvlTree<ValueT>.isRt(right); right = a1.right)
        ap[num1++] = a1 = right;
      if (AvlTree<ValueT>.isLt(right))
      {
        if (num1 == 0)
        {
          S.mkNil();
        }
        else
        {
          AvlTree<ValueT>.Rt(a1);
          if (detach)
            a1.right = right.right;
        }
      }
      else
      {
        if (detach)
          AvlTree<ValueT>.Rightmost(right.left).right = right.right;
        if (num1 == 0)
          S.right = right.left;
        else
          a1.right = right.left;
      }
      rightmost = right;
      right.left = right.right = (AvlTreeNode<ValueT>) null;
      while (num1 != 0)
      {
        AvlTreeNode<ValueT> a2 = ap[--num1];
        long num2 = a2.rank & 3L;
        long num3;
        if ((num2 & 1L) == 0L)
        {
          num3 = (num2 ^ 2L) >> 1;
          a2.rank = a2.rank & 4294967292L | num3;
        }
        else
        {
          AvlTreeNode<ValueT> avlTreeNode;
          if ((a2.left.rank & 2L) == 0L)
          {
            avlTreeNode = AvlTree<ValueT>.rotR(a2);
            long num4 = avlTreeNode.rank & 3L;
            a2.rank = a2.rank & 4294967292L | num4 ^ 1L;
            avlTreeNode.rank = avlTreeNode.rank & 4294967292L | (num4 ^ 1L) << 1;
          }
          else
          {
            avlTreeNode = AvlTree<ValueT>.rotLR(a2);
            long num4 = avlTreeNode.rank & 3L;
            avlTreeNode.rank &= 4294967292L;
            a2.rank = a2.rank & 4294967292L | (num4 & 1L) << 1;
            avlTreeNode.left.rank = avlTreeNode.left.rank & 4294967292L | num4 >> 1 & 1L;
          }
          if (num1 == 0)
            S.right = avlTreeNode;
          else
            ap[num1 - 1].right = avlTreeNode;
          num3 = avlTreeNode.rank & 3L;
        }
        if (num3 != 0L)
          return 0;
      }
      return 1;
    }

    private static int FixDelete(
      AvlTreeNode<ValueT> R,
      AvlTreeNode<ValueT> a,
      AvlTreeNode<ValueT> p,
      AvlTreeNode<ValueT>[] ap,
      int[] ad,
      int n)
    {
      switch (a.rank >> 2 & 3L)
      {
        case 1:
          AvlTreeNode<ValueT> right = a.right;
          AvlTree<ValueT>.Leftmost(right).left = a.left;
          if (p.left == a)
          {
            p.left = right;
            break;
          }
          p.right = right;
          break;
        case 2:
          AvlTreeNode<ValueT> left = a.left;
          AvlTree<ValueT>.Rightmost(left).right = a.right;
          if (p.left == a)
          {
            p.left = left;
            break;
          }
          p.right = left;
          break;
        case 3:
          if (p.right == a)
          {
            p.right = a.right;
            AvlTree<ValueT>.Rt(p);
          }
          else
          {
            p.left = a.left;
            AvlTree<ValueT>.Lt(p);
          }
          if (n == 0)
          {
            p.mkNil();
            break;
          }
          break;
        default:
          AvlTreeNode<ValueT> a1 = a.right;
          if (AvlTree<ValueT>.isLt(a1))
          {
            ap[n] = a1;
            ad[n++] = 1;
          }
          else
          {
            int index = n++;
            AvlTreeNode<ValueT> a2;
            do
            {
              a2 = a1;
              a1 = a2.left;
              ap[n] = a2;
              ad[n++] = 0;
            }
            while (!AvlTree<ValueT>.isLt(a1));
            if (AvlTree<ValueT>.isRt(a1))
            {
              AvlTree<ValueT>.Lt(a2);
              AvlTree<ValueT>.unRt(a1);
            }
            else
              a2.left = a1.right;
            a1.right = a.right;
            ap[index] = a1;
            ad[index] = 1;
          }
          AvlTree<ValueT>.unLt(a1);
          a1.left = a.left;
          a1.rank = a.rank & 4294967283L | a1.rank & 12L;
          AvlTree<ValueT>.Rightmost(a1.left).right = a1;
          if (p.left == a)
          {
            p.left = a1;
            break;
          }
          p.right = a1;
          break;
      }
      while (n != 0)
      {
        a = ap[--n];
        long num1 = a.rank & 3L;
        if (ad[n] == 0)
        {
          a.rank -= 16L;
          if ((num1 & 2L) == 0L)
          {
            a.rank = a.rank & 4294967292L | (num1 ^ 1L) << 1;
          }
          else
          {
            AvlTreeNode<ValueT> avlTreeNode;
            if ((a.right.rank & 1L) == 0L)
            {
              avlTreeNode = AvlTree<ValueT>.rotL(a);
              long num2 = avlTreeNode.rank & 3L;
              a.rank = a.rank & 4294967292L | num2 ^ 2L;
              avlTreeNode.rank = avlTreeNode.rank & 4294967292L | (num2 ^ 2L) >> 1;
            }
            else
            {
              avlTreeNode = AvlTree<ValueT>.rotRL(a);
              long num2 = avlTreeNode.rank & 3L;
              avlTreeNode.rank &= 4294967292L;
              a.rank = a.rank & 4294967292L | num2 >> 1 & 1L;
              avlTreeNode.right.rank = avlTreeNode.right.rank & 4294967292L | (num2 & 1L) << 1;
            }
            if (n == 0)
              R.right = avlTreeNode;
            else if (ad[n - 1] == 0)
              ap[n - 1].left = avlTreeNode;
            else
              ap[n - 1].right = avlTreeNode;
            a = avlTreeNode;
          }
        }
        else if ((num1 & 1L) == 0L)
        {
          a.rank = a.rank & 4294967292L | (num1 ^ 2L) >> 1;
        }
        else
        {
          AvlTreeNode<ValueT> avlTreeNode;
          if ((a.left.rank & 2L) == 0L)
          {
            avlTreeNode = AvlTree<ValueT>.rotR(a);
            long num2 = avlTreeNode.rank & 3L;
            a.rank = a.rank & 4294967292L | num2 ^ 1L;
            avlTreeNode.rank = avlTreeNode.rank & 4294967292L | (num2 ^ 1L) << 1;
          }
          else
          {
            avlTreeNode = AvlTree<ValueT>.rotLR(a);
            long num2 = avlTreeNode.rank & 3L;
            avlTreeNode.rank &= 4294967292L;
            a.rank = a.rank & 4294967292L | (num2 & 1L) << 1;
            avlTreeNode.left.rank = avlTreeNode.left.rank & 4294967292L | num2 >> 1 & 1L;
          }
          if (n == 0)
            R.right = avlTreeNode;
          else if (ad[n - 1] == 0)
            ap[n - 1].left = avlTreeNode;
          else
            ap[n - 1].right = avlTreeNode;
          a = avlTreeNode;
        }
        if (AvlTree<ValueT>.skewed(a))
        {
          while (n != 0)
          {
            a = ap[--n];
            a.rank -= (long) (1 ^ ad[n]) << 4;
          }
          return 0;
        }
      }
      return 1;
    }

    public void DeleteRange(int lo, int hi)
    {
      throw new NotImplementedException("TrimTrim");
    }

    public AvlTree<ValueT> this[int lo, int hi]
    {
      get
      {
        int count = (int) this._count;
        if (lo < 0)
        {
          if ((lo += count) < 0)
            lo = 0;
        }
        else if (lo > count)
          lo = count;
        if (hi < 0)
        {
          if ((hi += count) < 0)
            hi = 0;
        }
        else if (hi > count)
          hi = count;
        if ((long) (hi - lo) != this._count)
          return this.SliceB(lo, hi);
        return this.Copy();
      }
    }

    public AvlTree<ValueT> Copy()
    {
      AvlTree<ValueT> avlTree = new AvlTree<ValueT>(this._comparer);
      if (!this._root.nil)
      {
        AvlTreeNode<ValueT> a = this._root.right;
        AvlTreeNode<ValueT> avlTreeNode1 = new AvlTreeNode<ValueT>(a.value)
        {
          rank = a.rank
        };
        avlTreeNode1.left = avlTreeNode1.right = avlTree._root;
        avlTree._root.right = avlTreeNode1;
        while (true)
        {
          while (!AvlTree<ValueT>.isLt(a))
          {
            a = a.left;
            AvlTreeNode<ValueT> avlTreeNode2 = avlTreeNode1;
            avlTreeNode1 = new AvlTreeNode<ValueT>(a.value);
            avlTreeNode1.left = avlTreeNode2.left;
            avlTreeNode2.left = avlTreeNode1;
            avlTreeNode1.rank = a.rank;
            avlTreeNode1.right = avlTreeNode2;
          }
          while (AvlTree<ValueT>.isRt(a))
          {
            if (a.right != this._root)
            {
              a = a.right;
              avlTreeNode1 = avlTreeNode1.right;
            }
            else
              goto label_8;
          }
          a = a.right;
          AvlTreeNode<ValueT> avlTreeNode3 = avlTreeNode1;
          avlTreeNode1 = new AvlTreeNode<ValueT>(a.value);
          avlTreeNode1.rank = a.rank;
          avlTreeNode1.left = avlTreeNode3;
          avlTreeNode1.right = avlTreeNode3.right;
          avlTreeNode3.right = avlTreeNode1;
        }
      }
label_8:
      avlTree._count = this._count;
      avlTree._height = this._height;
      return avlTree;
    }

    public AvlTree<ValueT> Slice(int lo, int hi)
    {
      if (lo < 0)
        lo = 0;
      if ((long) hi > this._count)
        hi = (int) this._count;
      return this.SliceB(lo, hi);
    }

    private AvlTree<ValueT> SliceB(int lo, int hi)
    {
      AvlTree<ValueT> avlTree = new AvlTree<ValueT>(this._comparer);
      if (lo < hi)
      {
        long count = (long) (hi - lo);
        AvlTreeNode<ValueT> cur = AvlTree<ValueT>.Select(this._root.right, lo + 1);
        avlTree._root.right = AvlTree<ValueT>.Slice(ref cur, count, avlTree._root, avlTree._root);
        AvlTree<ValueT>.unRt(avlTree._root);
        avlTree._count = count;
        avlTree._height = AvlTree<ValueT>.height(count);
      }
      return avlTree;
    }

    private static AvlTreeNode<ValueT> Slice(
      ref AvlTreeNode<ValueT> cur,
      long count,
      AvlTreeNode<ValueT> pred,
      AvlTreeNode<ValueT> succ)
    {
      AvlTreeNode<ValueT> avlTreeNode = new AvlTreeNode<ValueT>();
      if (count == 1L)
      {
        avlTreeNode.value = cur.value;
        avlTreeNode.left = pred;
        avlTreeNode.right = succ;
        cur = AvlTree<ValueT>.Next(cur);
      }
      else
      {
        long count1 = count / 2L;
        avlTreeNode.left = AvlTree<ValueT>.Slice(ref cur, count1, pred, avlTreeNode);
        avlTreeNode.value = cur.value;
        avlTreeNode.rank = count1 + 1L << 4;
        long count2 = count - (count1 + 1L);
        cur = AvlTree<ValueT>.Next(cur);
        if (count2 == 0L)
        {
          avlTreeNode.right = succ;
          AvlTree<ValueT>.Rt(avlTreeNode);
        }
        else
          avlTreeNode.right = AvlTree<ValueT>.Slice(ref cur, count2, avlTreeNode, succ);
        avlTreeNode.rank |= (count & -count) == count ? 1L : 0L;
      }
      return avlTreeNode;
    }

    private static AvlTreeNode<ValueT> Next(AvlTreeNode<ValueT> a)
    {
      if (!AvlTree<ValueT>.isRt(a))
        return AvlTree<ValueT>.Leftmost(a.right);
      return a.right;
    }

    private static AvlTreeNode<ValueT> Slice(
      IEnumerator<ValueT> e,
      long count,
      AvlTreeNode<ValueT> pred,
      AvlTreeNode<ValueT> succ)
    {
      AvlTreeNode<ValueT> avlTreeNode = new AvlTreeNode<ValueT>();
      if (count == 1L)
      {
        e.MoveNext();
        avlTreeNode.value = e.Current;
        avlTreeNode.left = pred;
        avlTreeNode.right = succ;
      }
      else
      {
        long count1 = count / 2L;
        avlTreeNode.left = AvlTree<ValueT>.Slice(e, count1, pred, avlTreeNode);
        e.MoveNext();
        avlTreeNode.value = e.Current;
        avlTreeNode.rank = count1 + 1L << 4;
        long count2 = count - (count1 + 1L);
        if (count2 == 0L)
        {
          avlTreeNode.right = succ;
          AvlTree<ValueT>.Rt(avlTreeNode);
        }
        else
          avlTreeNode.right = AvlTree<ValueT>.Slice(e, count2, avlTreeNode, succ);
        avlTreeNode.rank |= (count & -count) == count ? 1L : 0L;
      }
      return avlTreeNode;
    }

    private static IEnumerator<ValueT> ListEnumerator(
      IList<ValueT> list,
      int lo,
      long len)
    {
      int i = lo;
      do
      {
        yield return list[i++];
      }
      while (0L != --len);
    }

    public static AvlTree<ValueT> FromSequence(IEnumerable<ValueT> seq, long len)
    {
      if (seq == null)
        throw new ArgumentNullException();
      AvlTree<ValueT> avlTree = new AvlTree<ValueT>();
      if (len > 0L)
      {
        avlTree._root.right = AvlTree<ValueT>.Slice(AvlTree<ValueT>.SeqEnumerator(seq, len), len, avlTree._root, avlTree._root);
        AvlTree<ValueT>.unRt(avlTree._root);
        avlTree._count = len;
        avlTree._height = AvlTree<ValueT>.height(len);
      }
      return avlTree;
    }

    private static IEnumerator<ValueT> SeqEnumerator(IEnumerable<ValueT> seq, long len)
    {
      IEnumerator<ValueT> e = seq.GetEnumerator();
      while (len != 0L && e.MoveNext())
      {
        --len;
        yield return e.Current;
      }
    }

    public static AvlTree<ValueT> FromList(IList<ValueT> sortedList)
    {
      return AvlTree<ValueT>.FromList(sortedList, 0, sortedList.Count);
    }

    public static AvlTree<ValueT> FromList(IList<ValueT> sortedList, int lo, int hi)
    {
      return AvlTree<ValueT>.FromList(sortedList, lo, hi, (IComparer<ValueT>) Comparer<ValueT>.Default);
    }

    public static AvlTree<ValueT> FromList(
      IList<ValueT> sortedList,
      int lo,
      int hi,
      IComparer<ValueT> comparer)
    {
      if (sortedList == null)
        throw new ArgumentNullException();
      AvlTree<ValueT> avlTree = new AvlTree<ValueT>(comparer);
      if (lo < 0)
        lo = 0;
      if (hi > sortedList.Count)
        hi = sortedList.Count;
      if (lo < hi)
      {
        long num = (long) (hi - lo);
        avlTree._root.right = AvlTree<ValueT>.Slice(AvlTree<ValueT>.ListEnumerator(sortedList, lo, num), num, avlTree._root, avlTree._root);
        AvlTree<ValueT>.unRt(avlTree._root);
        avlTree._count = num;
        avlTree._height = AvlTree<ValueT>.height(num);
      }
      return avlTree;
    }

    public static AvlTree<ValueT> FromSequence(AvlTree<ValueT> avl)
    {
      if (avl == null)
        throw new ArgumentNullException(nameof (avl));
      return avl.Copy();
    }

    public static AvlTree<ValueT> FromSequence(AvlTree<ValueT> avl, int lo, int hi)
    {
      if (avl == null)
        throw new ArgumentNullException(nameof (avl));
      return avl.Slice(lo, hi);
    }

    public IEnumerator<ValueT> GetEnumerator()
    {
      return (IEnumerator<ValueT>) new AvlTree<ValueT>.Enumerator(this);
    }

    public IAvlEnumerator<ValueT> GetAvlEnumerator()
    {
      return (IAvlEnumerator<ValueT>) new AvlTree<ValueT>.Enumerator(this);
    }

    public IEnumerator<ValueT> GetForwardEnumerator()
    {
      if (!this._root.nil)
      {
        AvlTreeNode<ValueT> a = AvlTree<ValueT>.Leftmost(this._root.right);
        do
        {
          yield return a.value;
          a = AvlTree<ValueT>.isRt(a) ? a.right : AvlTree<ValueT>.Leftmost(a.right);
        }
        while (a != this._root);
      }
    }

    public IEnumerator<ValueT> GetForwardEnumerator(int lo, int hi)
    {
      if (lo < 0)
        lo = 0;
      if ((long) hi > this._count)
        hi = (int) this._count;
      if (lo < hi)
      {
        AvlTreeNode<ValueT> a = AvlTree<ValueT>.Select(this._root.right, lo + 1);
        long len = (long) (hi - lo);
        do
        {
          yield return a.value;
          a = AvlTree<ValueT>.isRt(a) ? a.right : AvlTree<ValueT>.Leftmost(a.right);
        }
        while (0L != --len);
      }
    }

    public IEnumerator<ValueT> GetBackwardEnumerator()
    {
      if (!this._root.nil)
      {
        AvlTreeNode<ValueT> a = AvlTree<ValueT>.Rightmost(this._root.right);
        do
        {
          yield return a.value;
          a = AvlTree<ValueT>.isLt(a) ? a.left : AvlTree<ValueT>.Rightmost(a.left);
        }
        while (a != this._root);
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new AvlTree<ValueT>.Enumerator(this);
    }

    public void Dump(TextWriter os)
    {
    }

    private static AvlTreeNode<ValueT> rotL(AvlTreeNode<ValueT> a)
    {
      AvlTreeNode<ValueT> right = a.right;
      if (AvlTree<ValueT>.isLt(right))
      {
        AvlTree<ValueT>.unLt(right);
        AvlTree<ValueT>.Rt(a);
      }
      else
      {
        a.right = right.left;
        right.left = a;
      }
      right.rank += a.rank & 4294967280L;
      return right;
    }

    private static AvlTreeNode<ValueT> rotR(AvlTreeNode<ValueT> a)
    {
      AvlTreeNode<ValueT> left = a.left;
      if (AvlTree<ValueT>.isRt(left))
      {
        AvlTree<ValueT>.unRt(left);
        AvlTree<ValueT>.Lt(a);
      }
      else
      {
        a.left = left.right;
        left.right = a;
      }
      a.rank -= left.rank & 4294967280L;
      return left;
    }

    private static AvlTreeNode<ValueT> rotRL(AvlTreeNode<ValueT> a)
    {
      AvlTreeNode<ValueT> left = a.right.left;
      if (AvlTree<ValueT>.isRt(left))
      {
        AvlTree<ValueT>.unRt(left);
        AvlTree<ValueT>.Lt(a.right);
      }
      else
      {
        a.right.left = left.right;
        left.right = a.right;
      }
      if (AvlTree<ValueT>.isLt(left))
      {
        AvlTree<ValueT>.unLt(left);
        AvlTree<ValueT>.Rt(a);
        a.right = left;
      }
      else
        a.right = left.left;
      left.left = a;
      left.right.rank -= left.rank & 4294967280L;
      left.rank += a.rank & 4294967280L;
      return left;
    }

    private static AvlTreeNode<ValueT> rotLR(AvlTreeNode<ValueT> a)
    {
      AvlTreeNode<ValueT> right = a.left.right;
      if (AvlTree<ValueT>.isLt(right))
      {
        AvlTree<ValueT>.unLt(right);
        AvlTree<ValueT>.Rt(a.left);
      }
      else
      {
        a.left.right = right.left;
        right.left = a.left;
      }
      if (AvlTree<ValueT>.isRt(right))
      {
        AvlTree<ValueT>.unRt(right);
        AvlTree<ValueT>.Lt(a);
        a.left = right;
      }
      else
        a.left = right.right;
      right.right = a;
      right.rank += right.left.rank & 4294967280L;
      a.rank -= right.rank & 4294967280L;
      return right;
    }

    private static bool isLt(AvlTreeNode<ValueT> a)
    {
      return 0L != (a.rank & 4L);
    }

    private static bool isRt(AvlTreeNode<ValueT> a)
    {
      return 0L != (a.rank & 8L);
    }

    private static void Lt(AvlTreeNode<ValueT> a)
    {
      a.rank |= 4L;
    }

    private static void Rt(AvlTreeNode<ValueT> a)
    {
      a.rank |= 8L;
    }

    private static void unLt(AvlTreeNode<ValueT> a)
    {
      a.rank &= 4294967291L;
    }

    private static void unRt(AvlTreeNode<ValueT> a)
    {
      a.rank &= 4294967287L;
    }

    private static long GetRank(AvlTreeNode<ValueT> a)
    {
      return a.rank >> 4;
    }

    private static void Rank(AvlTreeNode<ValueT> a, long r)
    {
      a.rank = r << 4 | a.rank & 15L;
    }

    private static bool skewed(AvlTreeNode<ValueT> a)
    {
      return 0L != (a.rank & 3L);
    }

    private static bool lSkewed(AvlTreeNode<ValueT> a)
    {
      return 0L != (a.rank & 1L);
    }

    private static bool rSkewed(AvlTreeNode<ValueT> a)
    {
      return 0L != (a.rank & 2L);
    }

    public static int GetDelta(AvlTreeNode<ValueT> a)
    {
      switch (a.rank & 3L)
      {
        case 1:
          return -1;
        case 2:
          return 1;
        default:
          return 0;
      }
    }

    private static int height(long count)
    {
      int num1 = 0;
      int num2 = 1;
      while ((long) num2 <= count)
      {
        num2 <<= 1;
        ++num1;
      }
      return num1;
    }

    int IList.Add(object value)
    {
      if (!(value is ValueT))
        throw new InvalidOperationException("InvalidValueType");
      this.InsertLast((ValueT) value);
      return this.Index((ValueT) value);
    }

    public void Clear()
    {
      this.MkEmpty();
    }

    bool IList.Contains(object value)
    {
      if (value is ValueT)
        return this.Contains((ValueT) value);
      return false;
    }

    int IList.IndexOf(object value)
    {
      if (value is ValueT)
        return this.IndexOf((ValueT) value);
      return -1;
    }

    void IList.Insert(int index, object value)
    {
      if (!(value is ValueT))
        return;
      this.InsertLast((ValueT) value);
    }

    bool IList.IsFixedSize
    {
      get
      {
        return false;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    void IList.Remove(object value)
    {
      if (!(value is ValueT))
        throw new InvalidOperationException("InvalidValueType");
      ValueT delValue;
      this.Delete((ValueT) value, out delValue);
    }

    public void RemoveAt(int index)
    {
      ValueT delValue;
      this.DeleteAt(index, out delValue);
    }

    object IList.this[int index]
    {
      get
      {
        return (object) this[index];
      }
      set
      {
        if (!(value is ValueT))
          throw new InvalidOperationException("InvalidValueType");
        ValueT delValue;
        this.DeleteAt(index, out delValue);
        this.InsertLast((ValueT) value);
      }
    }

    public void CopyTo(Array array, int index)
    {
      ArrayList arrayList = new ArrayList();
      for (int index1 = index; index1 < this.Count; ++index1)
        arrayList.Add((object) this[index1]);
      arrayList.CopyTo(array, index);
    }

    public int Count
    {
      get
      {
        return this.Size;
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
        return (object) this;
      }
    }

    public int IndexOf(ValueT item)
    {
      return this.Index(item);
    }

    public ValueT this[int index]
    {
      get
      {
        if (index < 0 && (index += (int) this._count) < 0 || (long) index >= this._count)
          throw new IndexOutOfRangeException();
        return (index != 0 ? ((long) (index + 1) != this._count ? AvlTree<ValueT>.Select(this._root.right, index + 1) : AvlTree<ValueT>.Rightmost(this._root.right)) : AvlTree<ValueT>.Leftmost(this._root.right)).value;
      }
      set
      {
      }
    }

    public void Add(ValueT item)
    {
      this.InsertWithDuplicates(item);
    }

    public void CopyTo(ValueT[] array, int arrayIndex)
    {
      this.CopyTo((Array) array, arrayIndex);
    }

    public bool Remove(ValueT item)
    {
      ValueT delValue;
      return this.Delete(item, out delValue);
    }

    IEnumerator<ValueT> IEnumerable<ValueT>.GetEnumerator()
    {
      return (IEnumerator<ValueT>) new AvlTree<ValueT>.Enumerator(this);
    }

    private struct Enumerator : IEnumerator<ValueT>, IDisposable, IAvlEnumerator<ValueT>, IEnumerator
    {
      private AvlTree<ValueT> _tree;
      private AvlTreeNode<ValueT> _current;
      private AvlTreeNode<ValueT> _next;
      private AvlTreeNode<ValueT> _begin;
      private AvlTreeNode<ValueT> _end;
      private int _version;

      internal Enumerator(AvlTree<ValueT> tree)
      {
        this._tree = tree;
        this._version = tree._version;
        this._next = (AvlTreeNode<ValueT>) null;
        this._current = (AvlTreeNode<ValueT>) null;
        this._begin = this._end = (AvlTreeNode<ValueT>) null;
        this.Init((AvlTreeNode<ValueT>) null, (AvlTreeNode<ValueT>) null);
      }

      public AvlTree<ValueT> Tree
      {
        get
        {
          return this._tree;
        }
      }

      public bool MoveNext()
      {
        this.CheckVersion();
        if (this._next == this._end)
        {
          this._current = this._end;
          return false;
        }
        this._current = this._next;
        this._next = !AvlTree<ValueT>.isRt(this._current) ? AvlTree<ValueT>.Leftmost(this._current.right) : this._current.right;
        return true;
      }

      public bool MovePrevious()
      {
        this.CheckVersion();
        this._next = this._current;
        if (this._current == this._begin)
        {
          this._current = this._tree._root;
          return false;
        }
        this._current = this._current != this._tree._root ? (!AvlTree<ValueT>.isLt(this._current) ? AvlTree<ValueT>.Rightmost(this._current.left) : this._current.left) : AvlTree<ValueT>.Rightmost(this._current.right);
        return true;
      }

      public void MoveBegin()
      {
        this.CheckVersion();
        this._current = this._tree._root;
        this._next = this._begin;
      }

      public void MoveEnd()
      {
        this.CheckVersion();
        this._current = this._end;
        this._next = this._end;
      }

      public ValueT Current
      {
        get
        {
          return this._current.value;
        }
        set
        {
          if (this._current == this._tree._root || this._current == this._end)
            return;
          this._current.value = value;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          if (this._current == this._tree._root || this._current == this._end)
            throw new InvalidOperationException();
          return (object) this._current.value;
        }
      }

      public void Reset()
      {
        this.MoveBegin();
      }

      public void Dispose()
      {
        this._tree = (AvlTree<ValueT>) null;
        this._current = this._next = (AvlTreeNode<ValueT>) null;
        this._begin = this._end = (AvlTreeNode<ValueT>) null;
        this._version = 0;
      }

      private void Init(AvlTreeNode<ValueT> begin, AvlTreeNode<ValueT> end)
      {
        this._current = this._tree._root;
        if (this._current.nil)
        {
          this._begin = this._end = this._current;
        }
        else
        {
          if (begin == null)
            begin = AvlTree<ValueT>.Leftmost(this._current.right);
          if (end == null)
            end = this._current;
          this._begin = begin;
          this._end = end;
        }
        this._next = this._begin;
      }

      private void CheckVersion()
      {
        if (this._version != this._tree._version)
          throw new InvalidOperationException("AvlEnumerator:version mismatch");
      }
    }
  }
}
