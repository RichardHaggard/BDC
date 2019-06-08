// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ViewTypeChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class ViewTypeChangingEventArgs : CancelEventArgs
  {
    private ListViewType newViewType;
    private ListViewType oldViewType;

    public ViewTypeChangingEventArgs(ListViewType oldViewType, ListViewType newViewType)
    {
      this.newViewType = newViewType;
      this.oldViewType = oldViewType;
    }

    public ListViewType NewViewType
    {
      get
      {
        return this.newViewType;
      }
    }

    public ListViewType OldViewType
    {
      get
      {
        return this.oldViewType;
      }
    }
  }
}
