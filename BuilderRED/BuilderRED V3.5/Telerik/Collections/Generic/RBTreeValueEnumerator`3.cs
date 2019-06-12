// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.RBTreeValueEnumerator`3
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Collections.Generic
{
  internal class RBTreeValueEnumerator<N, K, P> : IEnumerator<K>, IDisposable, IEnumerator
    where N : RBTreeNodeBase<K, P>, new()
  {
    private RBTreeBase<K, N, P> mTree;
    private N mCurrent;

    public RBTreeValueEnumerator(RBTreeBase<K, N, P> aTree)
    {
      this.mTree = aTree;
      this.mCurrent = default (N);
    }

    K IEnumerator<K>.Current
    {
      get
      {
        return this.mCurrent.Key;
      }
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.mCurrent.Key;
      }
    }

    public bool MoveNext()
    {
      this.mCurrent = (object) this.mCurrent != null ? this.mTree.Next(this.mCurrent) : this.mTree.First();
      return (object) this.mCurrent != null;
    }

    public void Reset()
    {
      this.mCurrent = default (N);
    }

    public void Dispose()
    {
      this.mTree = (RBTreeBase<K, N, P>) null;
    }
  }
}
