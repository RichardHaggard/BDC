// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ExplorerBarLayoutInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class ExplorerBarLayoutInfo : StackViewLayoutInfo
  {
    public int expandedItemsCount;
    public float fullLayoutLength;
    public List<ExplorerBarItemSizeInfo> ExpandedItems;

    public ExplorerBarLayoutInfo(RadPageViewExplorerBarElement layout, SizeF availableSize)
      : base((RadPageViewStackElement) layout, availableSize)
    {
    }

    public override void Update()
    {
      this.expandedItemsCount = 0;
      if (this.ExpandedItems == null)
        this.ExpandedItems = new List<ExplorerBarItemSizeInfo>();
      else
        this.ExpandedItems.Clear();
      base.Update();
    }

    public override PageViewItemSizeInfo CreateItemSizeInfo(RadPageViewItem item)
    {
      RadPageViewExplorerBarItem viewExplorerBarItem = item as RadPageViewExplorerBarItem;
      bool isVertical = this.GetIsVertical();
      bool isExpanded = viewExplorerBarItem.IsExpanded;
      ExplorerBarItemSizeInfo explorerBarItemSizeInfo = new ExplorerBarItemSizeInfo(viewExplorerBarItem, isVertical, isExpanded);
      if (isExpanded)
      {
        this.ExpandedItems.Add(explorerBarItemSizeInfo);
        ++this.expandedItemsCount;
      }
      return (PageViewItemSizeInfo) explorerBarItemSizeInfo;
    }
  }
}
