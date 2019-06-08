// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ViewChangedAction
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public enum ViewChangedAction
  {
    Add = 1,
    Remove = 2,
    Replace = 4,
    Move = 8,
    Reset = 16, // 0x00000010
    ItemChanged = 32, // 0x00000020
    FilteringChanged = 64, // 0x00000040
    SortingChanged = 128, // 0x00000080
    GroupingChanged = 256, // 0x00000100
    PagingChanged = 512, // 0x00000200
    MetaDataChanged = 1024, // 0x00000400
    CurrentRowChanged = 2048, // 0x00000800
    ColumnsChanged = 4096, // 0x00001000
    ColumnPropertyChanged = 8192, // 0x00002000
    RowPropertyChanged = 16384, // 0x00004000
    TemplatePropertyChanged = 32768, // 0x00008000
    CurrentViewChanged = 65536, // 0x00010000
    DataChanged = 131072, // 0x00020000
    EnsureRowVisible = 262144, // 0x00040000
    EnsureCellVisible = 524288, // 0x00080000
    BestFitColumn = 1048576, // 0x00100000
    InvalidateRow = 2097152, // 0x00200000
    ConditionalFormattingChanged = 4194304, // 0x00400000
    BeginEdit = 8388608, // 0x00800000
    CurrentColumnChanged = 16777216, // 0x01000000
    ExpandedChanged = 33554432, // 0x02000000
    ColumnGroupPropertyChanged = 67108864, // 0x04000000
    CurrentCellChanged = 134217728, // 0x08000000
    EndEdit = 268435456, // 0x10000000
    ItemChanging = 536870912, // 0x20000000
    FilterExpressionChanged = 1073741824, // 0x40000000
  }
}
