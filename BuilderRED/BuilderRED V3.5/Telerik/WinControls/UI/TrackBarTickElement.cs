// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarTickElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class TrackBarTickElement : SeparatorElement
  {
    private bool isLargeTick;
    private bool isTopLeft;
    private int tickNumber;

    public TrackBarTickElement()
      : this(false, false)
    {
    }

    public TrackBarTickElement(bool isLargeTick, bool isTopLeft)
    {
      this.isLargeTick = isLargeTick;
      this.isTopLeft = isTopLeft;
    }

    public bool IsLargeTick
    {
      get
      {
        return this.isLargeTick;
      }
      internal set
      {
        this.isLargeTick = value;
      }
    }

    public bool IsTopLeft
    {
      get
      {
        return this.isTopLeft;
      }
      internal set
      {
        this.isTopLeft = value;
      }
    }

    public int TickNumber
    {
      get
      {
        return this.tickNumber;
      }
      internal set
      {
        this.tickNumber = value;
      }
    }
  }
}
