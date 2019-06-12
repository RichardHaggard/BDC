// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ExplorerBarItemSizeInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class ExplorerBarItemSizeInfo : PageViewItemSizeInfo
  {
    public bool IsExpanded;
    public SizeF contentSize;

    public ExplorerBarItemSizeInfo(RadPageViewExplorerBarItem item, bool vertical, bool isExpanded)
      : base((RadPageViewItem) item, vertical)
    {
      this.IsExpanded = isExpanded;
    }
  }
}
