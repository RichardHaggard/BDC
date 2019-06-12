// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.TreeViewSpreadExportCellFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Export
{
  public class TreeViewSpreadExportCellFormattingEventArgs : EventArgs
  {
    private int rowIndex;
    private TreeViewSpreadExportCell exportCell;
    private RadTreeNode treeNode;

    public TreeViewSpreadExportCellFormattingEventArgs(
      TreeViewSpreadExportCell exportCell,
      RadTreeNode treeNode,
      int rowIndex)
    {
      this.exportCell = exportCell;
      this.treeNode = treeNode;
      this.rowIndex = rowIndex;
    }

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
    }

    public TreeViewSpreadExportCell ExportCell
    {
      get
      {
        return this.exportCell;
      }
    }

    public RadTreeNode TreeNode
    {
      get
      {
        return this.treeNode;
      }
    }
  }
}
