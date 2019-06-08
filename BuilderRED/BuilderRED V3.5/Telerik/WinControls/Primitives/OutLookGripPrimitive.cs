// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.OutLookGripPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class OutLookGripPrimitive : FillPrimitive
  {
    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ZIndex = 10000;
    }

    public override void PaintPrimitive(IGraphics g, float angle, SizeF scale)
    {
      base.PaintPrimitive(g, angle, scale);
      Rectangle rectangle = new Rectangle(Point.Empty, this.Size);
      RectangleF BorderRectangle1 = new RectangleF((float) (this.Size.Width / 2 - 18), 4f, 2f, 2f);
      for (int index = 0; index < 9; ++index)
      {
        RectangleF BorderRectangle2 = new RectangleF(BorderRectangle1.X + 0.1f, BorderRectangle1.Y + 0.1f, 2f, 2f);
        g.FillRectangle(BorderRectangle2, Color.White);
        g.FillRectangle(BorderRectangle1, Color.Black);
        BorderRectangle1.X += 4f;
      }
    }
  }
}
