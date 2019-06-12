// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterComparer : TreeNodeComparer
  {
    public DataFilterComparer(RadDataFilterElement dataFilter)
      : base((RadTreeViewElement) dataFilter)
    {
    }

    public override int Compare(RadTreeNode x, RadTreeNode y)
    {
      bool flag1 = x is DataFilterGroupNode;
      bool flag2 = y is DataFilterGroupNode;
      bool flag3 = x is DataFilterCriteriaNode;
      bool flag4 = y is DataFilterCriteriaNode;
      if (flag1 && flag2 || flag3 && flag4)
        return base.Compare(x, y);
      int num;
      if (flag1)
        num = -1;
      else if (flag2)
        num = 1;
      else if (flag3)
      {
        num = -1;
      }
      else
      {
        if (!flag4)
          return base.Compare(x, y);
        num = 1;
      }
      if (this.TreeViewElement.SortOrder == SortOrder.Descending)
        num *= -1;
      return num;
    }
  }
}
