// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IVirtualizedElementProvider`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public interface IVirtualizedElementProvider<T>
  {
    IVirtualizedElement<T> GetElement(T data, object context);

    bool CacheElement(IVirtualizedElement<T> element);

    bool ShouldUpdate(IVirtualizedElement<T> element, T data, object context);

    bool IsCompatible(IVirtualizedElement<T> element, T data, object context);

    SizeF GetElementSize(T data);

    SizeF GetElementSize(IVirtualizedElement<T> element);

    SizeF DefaultElementSize { get; set; }

    void ClearCache();
  }
}
