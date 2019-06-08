// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnGroupColumnData
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class ColumnGroupColumnData
  {
    public float MinWidth;
    public float MaxWidth;
    public RectangleF Bounds;
    public float OriginalWidth;
    public int Row;

    public float GetConstrainedWidth(float width)
    {
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
