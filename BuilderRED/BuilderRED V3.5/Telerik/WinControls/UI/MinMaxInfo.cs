// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MinMaxInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class MinMaxInfo
  {
    private Size maxTrackSize;
    private Size minTrackSize;
    private Size maxSize;
    private Point maxPosition;
    private Size sizeReserved;

    public MinMaxInfo()
    {
    }

    public MinMaxInfo(
      Size maxTrack,
      Size minTrack,
      Size maxSize,
      Point maxPosition,
      Size sizeReserved)
    {
      this.maxTrackSize = maxTrack;
      this.minTrackSize = minTrack;
      this.maxSize = maxSize;
      this.maxPosition = maxPosition;
      this.sizeReserved = sizeReserved;
    }

    public Size MaxTrackSize
    {
      get
      {
        return this.maxTrackSize;
      }
      set
      {
        this.maxTrackSize = value;
      }
    }

    public Size MinTrackSize
    {
      get
      {
        return this.minTrackSize;
      }
      set
      {
        this.minTrackSize = value;
      }
    }

    public Size MaxSize
    {
      get
      {
        return this.maxSize;
      }
      set
      {
        this.maxSize = value;
      }
    }

    public Point MaxPosition
    {
      get
      {
        return this.maxPosition;
      }
      set
      {
        this.maxPosition = value;
      }
    }

    public Size SizeReserved
    {
      get
      {
        return this.sizeReserved;
      }
      set
      {
        this.sizeReserved = value;
      }
    }
  }
}
