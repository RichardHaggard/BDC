// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadKeyTipShowingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadKeyTipShowingEventArgs : CancelEventArgs
  {
    private Point customLocation;
    private Font font;
    private Color backColor;
    private Color borderColor;
    private Color foreColor;

    public RadKeyTipShowingEventArgs(
      bool cancel,
      Point customLocation,
      Font font,
      Color backColor,
      Color borderColor,
      Color foreColor)
      : base(cancel)
    {
      this.customLocation = customLocation;
      this.font = font;
      this.backColor = backColor;
      this.borderColor = borderColor;
      this.foreColor = foreColor;
    }

    public Point CustomLocation
    {
      get
      {
        return this.customLocation;
      }
      set
      {
        this.customLocation = value;
      }
    }

    public Font Font
    {
      get
      {
        return this.font;
      }
      set
      {
        this.font = value;
      }
    }

    public Color BackColor
    {
      get
      {
        return this.backColor;
      }
      set
      {
        this.backColor = value;
      }
    }

    public Color BorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
      }
    }

    public Color ForeColor
    {
      get
      {
        return this.foreColor;
      }
      set
      {
        this.foreColor = value;
      }
    }
  }
}
