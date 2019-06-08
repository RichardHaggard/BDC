// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewExpandedChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RadPageViewExpandedChangedEventArgs : EventArgs
  {
    private RadPageViewExplorerBarItem item;

    public RadPageViewExpandedChangedEventArgs(RadPageViewExplorerBarItem item)
    {
      this.item = item;
    }

    public RadPageViewExplorerBarItem Item
    {
      get
      {
        return this.item;
      }
    }

    public bool Expanded
    {
      get
      {
        return this.item.IsExpanded;
      }
    }
  }
}
