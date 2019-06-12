// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ScreenTipNeededEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls
{
  public class ScreenTipNeededEventArgs : EventArgs
  {
    private int delay = 900;
    private Size offset = Size.Empty;
    private RadElement item;

    public ScreenTipNeededEventArgs(RadItem item)
      : this((RadElement) item, Size.Empty)
    {
    }

    public ScreenTipNeededEventArgs(RadElement item, Size offset)
    {
      this.item = item;
      this.offset = offset;
    }

    public RadElement Item
    {
      get
      {
        return this.item;
      }
    }

    public int Delay
    {
      get
      {
        return this.delay;
      }
      set
      {
        this.delay = value;
      }
    }

    public Size Offset
    {
      get
      {
        return this.offset;
      }
      set
      {
        this.offset = value;
      }
    }
  }
}
