// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnGroupLayoutNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class ColumnGroupLayoutNode
  {
    public GridViewColumnGroup Group;
    public ColumnGroupRowCollection Rows;
    public List<ColumnGroupLayoutNode> Children;
    public ColumnGroupLayoutNode Parent;
    public Dictionary<GridViewColumn, ColumnGroupColumnData> ColumnData;
    public float MinWidth;
    public float MaxWidth;
    public RectangleF Bounds;
    public float OriginalWidth;
    public int Level;

    public ColumnGroupLayoutNode()
    {
      this.Children = new List<ColumnGroupLayoutNode>();
      this.ColumnData = new Dictionary<GridViewColumn, ColumnGroupColumnData>();
    }

    public ColumnGroupLayoutNode(GridViewColumnGroup group)
      : this()
    {
      this.Group = group;
    }

    public ColumnGroupLayoutNode(ColumnGroupRowCollection rows)
      : this()
    {
      this.Rows = rows;
    }

    public float GetConstrainedWidth(float width)
    {
      if (float.IsInfinity(width) || float.IsNaN(width))
        width = 0.0f;
      width = Math.Max(this.MinWidth, width);
      if ((double) this.MaxWidth != 0.0)
        width = Math.Min(width, this.MaxWidth);
      return width;
    }

    public void ConstrainWidth()
    {
      this.Bounds.Width = this.GetConstrainedWidth(this.Bounds.Width);
    }
  }
}
