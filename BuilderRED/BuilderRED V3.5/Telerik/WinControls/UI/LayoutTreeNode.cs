// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutTreeNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class LayoutTreeNode
  {
    internal LayoutTreeNode Left;
    internal LayoutTreeNode Right;
    internal LayoutTreeNode Parent;
    internal LayoutControlItemBase Item;
    internal Orientation SplitType;
    internal float SplitPosition;
    internal float OriginalSplitPosition;
    internal RectangleF Bounds;
    internal RectangleF OriginalBounds;
    internal SizeF MinSize;
    internal SizeF MaxSize;
  }
}
