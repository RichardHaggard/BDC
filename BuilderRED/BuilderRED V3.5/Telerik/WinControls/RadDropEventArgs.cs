// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadDropEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls
{
  public class RadDropEventArgs : RadDragDropEventArgs
  {
    private bool handled;
    private Point dropLocation;

    public RadDropEventArgs(ISupportDrag dragInstance, ISupportDrop dropTarget, Point dropLocation)
      : base(dragInstance, dropTarget)
    {
      this.dropLocation = dropLocation;
    }

    public Point DropLocation
    {
      get
      {
        return this.dropLocation;
      }
    }

    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }
  }
}
