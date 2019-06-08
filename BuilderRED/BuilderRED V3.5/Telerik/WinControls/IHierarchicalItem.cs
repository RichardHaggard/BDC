// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IHierarchicalItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Elements;

namespace Telerik.WinControls
{
  public interface IHierarchicalItem : IItemsOwner
  {
    object Owner { get; set; }

    bool HasChildren { get; }

    bool IsRootItem { get; }

    IHierarchicalItem HierarchyParent { get; set; }

    IHierarchicalItem RootItem { get; }

    RadItem Next { get; }

    RadItem Previous { get; }
  }
}
