// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilterMenuTreeItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class FilterMenuTreeItem : RadMenuItemBase
  {
    private FilterMenuTreeElement treeElement;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(5, 5, 5, 0);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.treeElement = new FilterMenuTreeElement();
      this.Children.Add((RadElement) this.treeElement);
    }

    public FilterMenuTreeElement TreeElement
    {
      get
      {
        return this.treeElement;
      }
    }

    protected override void OnLoaded()
    {
      this.treeElement.Initialize();
      base.OnLoaded();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      base.MeasureOverride(availableSize);
      return new SizeF(0.0f, 40f);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      foreach (RadElement child in this.Children)
      {
        RectangleF finalRect = new RectangleF(PointF.Empty, finalSize);
        if (child == this.treeElement)
        {
          finalRect = this.GetClientRectangle(finalSize);
          RadDropDownMenuLayout ancestor = this.FindAncestor<RadDropDownMenuLayout>();
          if (ancestor != null)
          {
            finalRect.X += this.RightToLeft ? 0.0f : ancestor.LeftColumnWidth;
            finalRect.Width -= ancestor.LeftColumnWidth;
          }
          this.treeElement.TreeView.Size = finalRect.Size.ToSize();
        }
        child.Arrange(finalRect);
      }
      return finalSize;
    }
  }
}
