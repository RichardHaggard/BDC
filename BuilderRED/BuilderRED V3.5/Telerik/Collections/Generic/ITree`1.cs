// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.ITree`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Collections.Generic
{
  public interface ITree<T>
  {
    object SyncRoot { get; }

    ITreeNode<T> Add(T item);

    ITreeNode<T> AddOrGet(T item);

    ITreeNode<T> Find(T item);

    bool Remove(T item);

    void Remove(ITreeNode<T> node);

    void Clear();
  }
}
