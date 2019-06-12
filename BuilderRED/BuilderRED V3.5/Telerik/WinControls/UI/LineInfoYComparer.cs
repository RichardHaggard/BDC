// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LineInfoYComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class LineInfoYComparer : IComparer<LineInfo>
  {
    private float yCoordinate;

    public LineInfoYComparer(float yCoorditante)
    {
      this.yCoordinate = yCoorditante;
    }

    public int Compare(LineInfo lineX, LineInfo lineY)
    {
      RectangleF boundingRectangle = lineX.ControlBoundingRectangle;
      if ((double) boundingRectangle.Top <= (double) this.yCoordinate && (double) this.yCoordinate <= (double) boundingRectangle.Bottom)
        return 0;
      return (double) this.yCoordinate < (double) boundingRectangle.Top ? 1 : -1;
    }
  }
}
