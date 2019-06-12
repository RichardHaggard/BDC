// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollServiceBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class ScrollServiceBehavior
  {
    private List<ScrollService> list;

    public List<ScrollService> ScrollServices
    {
      get
      {
        return this.list;
      }
    }

    public bool IsRunning
    {
      get
      {
        foreach (ScrollService scrollService in this.list)
        {
          if (scrollService.IsScrolling)
            return true;
        }
        return false;
      }
    }

    public ScrollServiceBehavior()
    {
      this.list = new List<ScrollService>();
    }

    public void Add(ScrollService scrollService)
    {
      this.list.Add(scrollService);
    }

    public void Stop()
    {
      foreach (ScrollService scrollService in this.list)
        scrollService.Stop();
    }

    public void MouseDown(Point location)
    {
      foreach (ScrollService scrollService in this.list)
      {
        if (scrollService.Owner.ElementState == ElementState.Loaded)
          scrollService.MouseDown(location);
      }
    }

    public void MouseUp(Point location)
    {
      foreach (ScrollService scrollService in this.list)
      {
        if (scrollService.Owner.ElementState == ElementState.Loaded)
          scrollService.MouseUp(location);
      }
    }

    public void MouseMove(Point location)
    {
      foreach (ScrollService scrollService in this.list)
      {
        if (scrollService.Owner.ElementState == ElementState.Loaded)
          scrollService.MouseMove(location);
      }
    }
  }
}
