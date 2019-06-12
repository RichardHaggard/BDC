// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualizedPanelElementProvider`2
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualizedPanelElementProvider<T, T1> : BaseVirtualizedElementProvider<T>
    where T1 : IVirtualizedElement<T>, new()
  {
    public override IVirtualizedElement<T> CreateElement(T data, object context)
    {
      return (IVirtualizedElement<T>) new T1();
    }

    public override bool ShouldUpdate(IVirtualizedElement<T> element, T data, object context)
    {
      return !element.Data.Equals((object) data);
    }
  }
}
