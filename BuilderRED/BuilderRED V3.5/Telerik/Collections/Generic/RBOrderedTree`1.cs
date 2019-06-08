// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.RBOrderedTree`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.Collections.Generic
{
  public class RBOrderedTree<T> : RBOrderedTreeBase<T>
  {
    public RBOrderedTree()
      : base(true)
    {
    }

    public RBOrderedTree(IComparer<T> aComparer)
      : base(aComparer, true)
    {
    }
  }
}
