// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBlockXComparer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TextBlockXComparer : IComparer
  {
    private float xCoordinate;

    public TextBlockXComparer(float xCoordinate)
    {
      this.xCoordinate = xCoordinate;
    }

    public int Compare(object xValue, object yValue)
    {
      ITextBlock textBlock = xValue as ITextBlock;
      Padding margin = (textBlock as RadElement).Margin;
      RectangleF boundingRectangle = (RectangleF) textBlock.ControlBoundingRectangle;
      boundingRectangle.X -= (float) margin.Left;
      boundingRectangle.Y -= (float) margin.Top;
      boundingRectangle.Width += (float) margin.Horizontal;
      boundingRectangle.Height += (float) margin.Vertical;
      if ((double) boundingRectangle.X <= (double) this.xCoordinate && (double) this.xCoordinate <= (double) boundingRectangle.Right)
        return 0;
      return (double) this.xCoordinate < (double) boundingRectangle.X ? 1 : -1;
    }
  }
}
