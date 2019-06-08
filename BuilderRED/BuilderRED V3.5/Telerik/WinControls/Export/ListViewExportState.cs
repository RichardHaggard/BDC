// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.ListViewExportState
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Export
{
  internal class ListViewExportState
  {
    public ListViewExportState()
    {
      this.SelectedItems = new List<ListViewDataItem>();
      this.CollapsedItems = new List<ListViewDataItemGroup>();
    }

    public ListViewDetailColumn CurrentColumn { get; set; }

    public ListViewDataItem CurrentItem { get; set; }

    public List<ListViewDataItem> SelectedItems { get; set; }

    public List<ListViewDataItemGroup> CollapsedItems { get; set; }

    public int HorizontalScrollbarValue { get; set; }

    public int VerticalScrolbarValue { get; set; }
  }
}
