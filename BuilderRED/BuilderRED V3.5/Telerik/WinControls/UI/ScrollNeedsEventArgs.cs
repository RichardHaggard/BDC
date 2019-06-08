// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ScrollNeedsEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class ScrollNeedsEventArgs : EventArgs
  {
    private bool oldHorizontalScrollNeed;
    private bool newHorizontalScrollNeed;
    private bool oldVerticalScrollNeed;
    private bool newVerticalScrollNeed;

    public bool OldHorizontalScrollNeed
    {
      get
      {
        return this.oldHorizontalScrollNeed;
      }
    }

    public bool NewHorizontalScrollNeed
    {
      get
      {
        return this.newHorizontalScrollNeed;
      }
    }

    public bool OldVerticalScrollNeed
    {
      get
      {
        return this.oldVerticalScrollNeed;
      }
    }

    public bool NewVerticalScrollNeed
    {
      get
      {
        return this.newVerticalScrollNeed;
      }
    }

    public ScrollNeedsEventArgs(
      bool oldHorizontalScrollNeed,
      bool newHorizontalScrollNeed,
      bool oldVerticalScrollNeed,
      bool newVerticalScrollNeed)
    {
      this.oldHorizontalScrollNeed = oldHorizontalScrollNeed;
      this.newHorizontalScrollNeed = newHorizontalScrollNeed;
      this.oldVerticalScrollNeed = oldVerticalScrollNeed;
      this.newVerticalScrollNeed = newVerticalScrollNeed;
    }
  }
}
