// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabStripPanelSelectedIndexChangingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class TabStripPanelSelectedIndexChangingEventArgs : CancelEventArgs
  {
    private int oldIndex;
    private int newIndex;
    private TabPanel oldTabPanel;
    private TabPanel newTabPanel;

    public TabStripPanelSelectedIndexChangingEventArgs(
      int oldIndex,
      int newIndex,
      TabPanel oldTabPanel,
      TabPanel newTabPanel)
    {
      this.oldIndex = oldIndex;
      this.newIndex = newIndex;
      this.oldTabPanel = oldTabPanel;
      this.newTabPanel = newTabPanel;
    }

    public int OldIndex
    {
      get
      {
        return this.oldIndex;
      }
    }

    public int NewIndex
    {
      get
      {
        return this.newIndex;
      }
    }

    public TabPanel OldTabPanel
    {
      get
      {
        return this.oldTabPanel;
      }
    }

    public TabPanel NewTabPanel
    {
      get
      {
        return this.newTabPanel;
      }
    }
  }
}
