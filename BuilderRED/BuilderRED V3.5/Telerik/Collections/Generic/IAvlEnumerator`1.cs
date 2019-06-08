// Decompiled with JetBrains decompiler
// Type: Telerik.Collections.Generic.IAvlEnumerator`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.Collections.Generic
{
  public interface IAvlEnumerator<ValueT>
  {
    ValueT Current { get; }

    bool MoveNext();

    bool MovePrevious();

    void MoveBegin();

    void MoveEnd();

    void Reset();

    void Dispose();
  }
}
