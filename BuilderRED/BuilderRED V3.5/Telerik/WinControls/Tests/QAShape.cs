// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Tests.QAShape
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.Tests
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class QAShape : ElementShape
  {
    public override GraphicsPath CreatePath(Rectangle bounds)
    {
      bool flag = false;
      Rectangle rectangle = new Rectangle(bounds.X + 10, bounds.Y, bounds.Width, bounds.Height);
      int length = 12;
      int num1 = 19;
      int num2 = 25;
      int num3 = (int) Math.Pow((double) num2, 2.0);
      Point point1 = new Point(rectangle.Left - num2, rectangle.Top + num1);
      Point[] pointArray = new Point[length];
      byte[] numArray = new byte[length];
      int num4 = 1 + rectangle.Left - length;
      for (int index = 0; index < pointArray.Length; ++index)
      {
        int x = index == 0 ? num4 : 1 + pointArray[index - 1].X;
        int y = point1.Y - (int) Math.Sqrt((double) num3 - Math.Pow((double) (x - point1.X), 2.0));
        if (y < rectangle.Top)
          y = rectangle.Top;
        pointArray[index] = new Point(x, y);
        numArray[index] = (byte) 1;
      }
      if (!flag)
      {
        ArrayList arrayList = new ArrayList();
        for (int index1 = 0; index1 < pointArray.Length; ++index1)
        {
          arrayList.Add((object) pointArray[index1]);
          if (index1 != pointArray.Length - 1)
          {
            int num5 = pointArray[index1 + 1].Y - pointArray[index1].Y;
            if (num5 > 1)
            {
              int num6 = num5 > 4 ? (int) Math.Round((double) (num5 / 2)) : 2;
              for (int index2 = 1; index2 < num6; ++index2)
                arrayList.Add((object) new Point(pointArray[index1].X, pointArray[index1].Y + index2));
            }
          }
        }
        foreach (byte num5 in new byte[arrayList.Count])
          num5 = (byte) 1;
        pointArray = (Point[]) arrayList.ToArray(typeof (Point));
      }
      Point point2 = new Point(rectangle.Left, rectangle.Top);
      Point point3 = new Point(rectangle.Left - length, rectangle.Top);
      GraphicsPath graphicsPath = new GraphicsPath();
      Point[] points = new Point[pointArray.Length];
      pointArray[0] = pointArray[1];
      for (int index = 0; index < pointArray.Length; ++index)
        points[index] = pointArray[pointArray.Length - index - 1];
      GraphicsPath addingPath = new GraphicsPath();
      addingPath.AddLines(points);
      graphicsPath.AddPath(addingPath, true);
      graphicsPath.AddLine(bounds.Left + 10, bounds.Top, bounds.Right - 20, bounds.Top);
      if (rectangle.Height >= 15)
      {
        graphicsPath.AddArc(rectangle.Right - 20, rectangle.Top, 10, 20, -90f, 90f);
        graphicsPath.AddArc(rectangle.Right - 20, rectangle.Bottom - 20, 10, 20, 0.0f, 90f);
      }
      else
      {
        int height = rectangle.Height;
        if (height <= 0)
          height = 1;
        graphicsPath.AddArc(rectangle.Right - 10, rectangle.Top, 10, height, 270f, 180f);
      }
      graphicsPath.AddLine(rectangle.Right - 20, rectangle.Bottom, rectangle.Left, rectangle.Bottom);
      graphicsPath.CloseAllFigures();
      this.MirrorPath(graphicsPath, (RectangleF) bounds);
      return graphicsPath;
    }
  }
}
