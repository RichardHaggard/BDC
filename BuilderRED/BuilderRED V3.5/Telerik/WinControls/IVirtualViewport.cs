// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IVirtualViewport
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public interface IVirtualViewport
  {
    bool Virtualized { get; }

    void SetVirtualItemsCollection(IVirtualizationCollection virtualItemsCollection);

    void OnItemDataInserted(int index, object itemData);

    void OnItemDataRemoved(int index, object itemData);

    void OnItemDataSet(int index, object oldItemData, object newItemData);

    void OnItemsDataClear();

    void OnItemsDataClearComplete();

    void OnItemsDataSort();

    void OnItemsDataSortComplete();

    void BeginUpdate();

    void EndUpdate();
  }
}
