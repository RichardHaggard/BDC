// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.ISortedTree`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Collections.Generic
{
  public interface ISortedTree<T> : ITree<T>
  {
    int Count { get; }

    ITreeNode<T> First();

    ITreeNode<T> Last();

    ITreeNode<T> Previous(ITreeNode<T> node);

    ITreeNode<T> Next(ITreeNode<T> node);
  }
}
