// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.MeasurementGraphics
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  public class MeasurementGraphics : IDisposable
  {
    private static object instance = new object();
    [ThreadStatic]
    private static int controlCount;
    [ThreadStatic]
    private static IntPtr memoryDC;
    private Graphics graphics;

    public static object SyncObject
    {
      get
      {
        return MeasurementGraphics.instance;
      }
    }

    private MeasurementGraphics()
    {
      if (MeasurementGraphics.memoryDC == IntPtr.Zero)
        MeasurementGraphics.memoryDC = NativeMethods.CreateCompatibleDC(new HandleRef((object) null, IntPtr.Zero));
      this.graphics = Graphics.FromHdcInternal(MeasurementGraphics.memoryDC);
    }

    public Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
    }

    public void Dispose()
    {
    }

    public static MeasurementGraphics CreateMeasurementGraphics()
    {
      return new MeasurementGraphics();
    }

    internal static void IncreaseControlCount()
    {
      ++MeasurementGraphics.controlCount;
    }

    internal static void DecreaseControlCount()
    {
      if (MeasurementGraphics.controlCount <= 0)
        return;
      --MeasurementGraphics.controlCount;
      if (MeasurementGraphics.controlCount != 0 || !(MeasurementGraphics.memoryDC != IntPtr.Zero))
        return;
      NativeMethods.DeleteDC(new HandleRef((object) null, MeasurementGraphics.memoryDC));
      MeasurementGraphics.memoryDC = IntPtr.Zero;
    }
  }
}
