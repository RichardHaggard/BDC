// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.GestureInfoHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Runtime.InteropServices;

namespace Telerik.WinControls
{
  public static class GestureInfoHelper
  {
    private static bool supportsGestures = Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor > 0;

    public static bool SupportsGestures
    {
      get
      {
        return GestureInfoHelper.supportsGestures;
      }
      set
      {
        GestureInfoHelper.supportsGestures = value;
      }
    }

    [DllImport("user32")]
    public static extern bool GetGestureInfo(IntPtr hGestureInfo, ref GESTUREINFO pGestureInfo);
  }
}
