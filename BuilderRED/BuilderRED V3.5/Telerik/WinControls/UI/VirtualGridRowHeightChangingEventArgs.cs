﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridRowHeightChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class VirtualGridRowHeightChangingEventArgs : VirtualGridRowEventArgs
  {
    private bool cancel;
    private int oldHeight;
    private int newHeight;

    public VirtualGridRowHeightChangingEventArgs(
      int rowIndex,
      int oldHeight,
      int newHeight,
      VirtualGridViewInfo viewInfo)
      : base(rowIndex, viewInfo)
    {
      this.newHeight = newHeight;
      this.oldHeight = oldHeight;
    }

    public int OldHeight
    {
      get
      {
        return this.oldHeight;
      }
    }

    public int NewHeight
    {
      get
      {
        return this.newHeight;
      }
    }

    public bool Cancel
    {
      get
      {
        return this.cancel;
      }
      set
      {
        this.cancel = value;
      }
    }
  }
}
