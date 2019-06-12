// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeViewVirtualizedContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadTreeViewVirtualizedContainer : VirtualizedStackContainer<RadTreeNode>
  {
    public RadTreeViewElement TreeViewElement
    {
      get
      {
        return this.FindAncestor<RadTreeViewElement>();
      }
    }

    protected override SizeF MeasureElementCore(RadElement element, SizeF availableSize)
    {
      TreeNodeElement treeNodeElement = (TreeNodeElement) element;
      float height = this.ElementProvider.GetElementSize(treeNodeElement.Data).Height;
      float width = float.PositiveInfinity;
      if (this.TreeViewElement.AutoSizeItems || treeNodeElement.Editor != null)
        height = float.PositiveInfinity;
      if (treeNodeElement.ContentElement.TextWrap)
        width = availableSize.Width - this.ScrollOffset.Width;
      element.Measure(new SizeF(width, height));
      treeNodeElement.ContentElement.FullDesiredSize = treeNodeElement.ContentElement.DesiredSize;
      element.Measure(new SizeF(availableSize.Width - this.ScrollOffset.Width, height));
      return element.DesiredSize;
    }

    protected override RectangleF ArrangeElementCore(
      RadElement element,
      SizeF finalSize,
      RectangleF arrangeRect)
    {
      arrangeRect.X = 0.0f;
      arrangeRect.Width = finalSize.Width;
      return base.ArrangeElementCore(element, finalSize, arrangeRect);
    }
  }
}
