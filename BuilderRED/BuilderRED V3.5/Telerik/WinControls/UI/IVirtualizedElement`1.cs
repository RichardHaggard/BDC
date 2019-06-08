// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IVirtualizedElement`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public interface IVirtualizedElement<T>
  {
    T Data { get; }

    void Attach(T data, object context);

    void Detach();

    void Synchronize();

    bool IsCompatible(T data, object context);
  }
}
