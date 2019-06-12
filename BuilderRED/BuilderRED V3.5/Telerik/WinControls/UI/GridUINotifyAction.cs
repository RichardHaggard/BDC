// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridUINotifyAction
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  [Flags]
  public enum GridUINotifyAction
  {
    Reset = 1,
    AddRow = 2,
    RemoveRow = 4,
    SortingChanged = 8,
    GroupingChanged = 16, // 0x00000010
    FilteringChanged = 32, // 0x00000020
    ExpandedChanged = 64, // 0x00000040
    DataChanged = 128, // 0x00000080
    BatchDataChanged = 256, // 0x00000100
    MoveRow = 512, // 0x00000200
    RowCountChanged = 1024, // 0x00000400
    StateChanged = 2048, // 0x00000800
    RowsChanged = 4096, // 0x00001000
    LayoutChanged = 8192, // 0x00002000
    HierarchyChanged = 16384, // 0x00004000
    ThemeChanged = 32768, // 0x00008000
    RemoveCachedCurrentRow = 65536, // 0x00010000
    ChildViewColumnsChanged = 131072, // 0x00020000
    CurrentRowStateChanged = ChildViewColumnsChanged | Reset, // 0x00020001
    ResetView = ChildViewColumnsChanged | AddRow, // 0x00020002
    RowHeightChanged = ResetView | Reset, // 0x00020003
    ColumnWidthChanged = ChildViewColumnsChanged | RemoveRow, // 0x00020004
  }
}
