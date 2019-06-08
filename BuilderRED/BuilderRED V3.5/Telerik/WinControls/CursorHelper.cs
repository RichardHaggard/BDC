// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CursorHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public static class CursorHelper
  {
    public static Cursor CursorFromBitmap(Bitmap bitmap, Point hotSpot)
    {
      IntPtr hicon = bitmap.GetHicon();
      NativeMethods.IconInfo iconInfo = new NativeMethods.IconInfo();
      NativeMethods.GetIconInfo(hicon, ref iconInfo);
      iconInfo.xHotspot = hotSpot.X;
      iconInfo.yHotspot = hotSpot.Y;
      iconInfo.fIcon = false;
      return new Cursor(NativeMethods.CreateIconIndirect(ref iconInfo));
    }
  }
}
