// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Docking.SplitPanelHelper
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI.Docking
{
  internal static class SplitPanelHelper
  {
    public static readonly Size MaxSize = new Size(int.MaxValue, int.MaxValue);
    public static readonly Size MinSize = new Size(int.MinValue, int.MinValue);

    public static Size EnsureSizeMaxBounds(Size size, Size max)
    {
      return SplitPanelHelper.EnsureSizeBounds(size, SplitPanelHelper.MinSize, max);
    }

    public static Size EnsureSizeMinBounds(Size size, Size min)
    {
      return SplitPanelHelper.EnsureSizeBounds(size, min, SplitPanelHelper.MaxSize);
    }

    public static Size EnsureSizeBounds(Size size, Size min, Size max)
    {
      size.Width = Math.Max(min.Width, size.Width);
      size.Width = Math.Min(max.Width, size.Width);
      size.Height = Math.Max(min.Height, size.Height);
      size.Height = Math.Min(max.Height, size.Height);
      return size;
    }

    public static SizeF EnsureSizeBounds(SizeF size, SizeF min, SizeF max)
    {
      size.Width = Math.Max(min.Width, size.Width);
      size.Width = Math.Min(max.Width, size.Width);
      size.Height = Math.Max(min.Height, size.Height);
      size.Height = Math.Min(max.Height, size.Height);
      return size;
    }

    public static bool ShouldBeginDrag(Point curr, Point capture)
    {
      Size dragSize = SystemInformation.DragSize;
      return !new Rectangle(capture.X - dragSize.Width / 2, capture.Y - dragSize.Height / 2, dragSize.Width, dragSize.Height).Contains(curr);
    }
  }
}
