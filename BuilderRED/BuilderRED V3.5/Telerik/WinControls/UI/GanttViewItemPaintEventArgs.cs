// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewItemPaintEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewItemPaintEventArgs : EventArgs
  {
    private GanttGraphicalViewBaseItemElement element;
    private Graphics graphics;

    public GanttViewItemPaintEventArgs(GanttGraphicalViewBaseItemElement element, Graphics graphics)
    {
      this.element = element;
      this.graphics = graphics;
    }

    public GanttGraphicalViewBaseItemElement Element
    {
      get
      {
        return this.element;
      }
    }

    public Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
    }
  }
}
