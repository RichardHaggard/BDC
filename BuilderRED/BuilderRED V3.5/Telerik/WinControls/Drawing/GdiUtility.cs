// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.GdiUtility
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.Drawing
{
  internal class GdiUtility
  {
    [DllImport("Gdi32.dll", EntryPoint = "ExtTextOutW")]
    public static extern bool ExtTextOut(
      IntPtr hdc,
      int X,
      int Y,
      uint fuOptions,
      [In] ref Telerik.WinControls.NativeMethods.RECT lprc,
      [MarshalAs(UnmanagedType.LPWStr)] string lpString,
      uint cbCount,
      [In] int[] lpDx);

    public static ColorBlend GetColorBlend(GradientStop[] gradientStops)
    {
      int length = gradientStops.Length;
      ColorBlend colorBlend = new ColorBlend();
      float[] numArray = new float[length];
      Color[] colorArray = new Color[length];
      for (int index = 0; index < length; ++index)
      {
        GradientStop gradientStop = gradientStops[index];
        numArray[index] = gradientStop.Position;
        colorArray[index] = gradientStop.Color;
      }
      colorBlend.Colors = colorArray;
      colorBlend.Positions = numArray;
      return colorBlend;
    }
  }
}
