// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupExpandingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GroupExpandingEventArgs : CancelEventArgs
  {
    private DataGroup dataGroup;
    private bool isExpanded;

    public GroupExpandingEventArgs(DataGroup dataGroup, bool isExpanded)
    {
      this.dataGroup = dataGroup;
      this.isExpanded = isExpanded;
    }

    public DataGroup DataGroup
    {
      get
      {
        return this.dataGroup;
      }
    }

    public bool IsExpanded
    {
      get
      {
        return this.isExpanded;
      }
    }
  }
}
