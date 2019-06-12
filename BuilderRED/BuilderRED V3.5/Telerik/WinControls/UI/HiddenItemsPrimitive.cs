// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.HiddenItemsPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  internal class HiddenItemsPrimitive : BasePrimitive
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.MinSize = new Size(10, 10);
    }

    public override void PaintPrimitive(IGraphics g, float angle, SizeF scale)
    {
      int num = 1;
      int y1 = 5;
      g.DrawLine(Color.Black, num, y1, num, y1 + 2);
      g.DrawLine(Color.Black, (float) num, (float) y1 + 1.5f, (float) (num + 1), (float) y1 + 1.5f);
      g.DrawLine(Color.White, num + 1, y1 + 2, num + 2, y1 + 2);
      g.DrawLine(Color.White, num + 1, y1 + 2, num + 1, y1 + 3);
      g.DrawLine(Color.Black, num + 4, y1, num + 4, y1 + 2);
      g.DrawLine(Color.Black, (float) (num + 4), (float) y1 + 1.5f, (float) (num + 5), (float) y1 + 1.5f);
      g.DrawLine(Color.White, num + 5, y1 + 2, num + 6, y1 + 2);
      g.DrawLine(Color.White, num + 5, y1 + 2, num + 5, y1 + 3);
    }
  }
}
