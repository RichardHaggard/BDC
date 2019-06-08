// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ArrowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class ArrowElement : LightVisualElement
  {
    protected override bool PerformPaintTransformation(ref RadMatrix matrix)
    {
      bool flag = false;
      float angleTransform = this.AngleTransform;
      if ((double) angleTransform != 0.0)
      {
        flag = true;
        RectangleF bounds = new RectangleF(PointF.Empty, (SizeF) this.Bounds.Size);
        TelerikHelper.PerformCenteredRotation(ref matrix, bounds, angleTransform);
      }
      SizeF scaleTransform = this.ScaleTransform;
      if ((double) scaleTransform.Width > 0.0 && (double) scaleTransform.Height > 0.0 && ((double) scaleTransform.Width != 1.0 || (double) scaleTransform.Height != 1.0))
      {
        flag = true;
        matrix.Scale(scaleTransform.Width, scaleTransform.Height, MatrixOrder.Append);
      }
      SizeF positionOffset = this.PositionOffset;
      if (positionOffset != SizeF.Empty)
      {
        flag = true;
        matrix.Translate(this.RightToLeft ? -positionOffset.Width : positionOffset.Width, positionOffset.Height, MatrixOrder.Append);
      }
      return flag;
    }
  }
}
