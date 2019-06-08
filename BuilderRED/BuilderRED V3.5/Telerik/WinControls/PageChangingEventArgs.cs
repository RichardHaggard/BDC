// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PageChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  public class PageChangingEventArgs : CancelEventArgs
  {
    private int newPageIndex;

    public PageChangingEventArgs(int newPageIndex)
    {
      this.newPageIndex = newPageIndex;
    }

    public int NewPageIndex
    {
      get
      {
        return this.newPageIndex;
      }
    }
  }
}
