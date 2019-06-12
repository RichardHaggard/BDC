// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabStripPanelEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class TabStripPanelEventArgs : EventArgs
  {
    private TabControlAction action;
    private TabPanel tabPanel;
    private int tabPanelIndex;

    public TabStripPanelEventArgs(TabPanel tabPanel, int tabPanelIndex, TabControlAction action)
    {
      this.tabPanel = tabPanel;
      this.tabPanelIndex = tabPanelIndex;
      this.action = action;
    }

    public TabControlAction Action
    {
      get
      {
        return this.action;
      }
    }

    public TabPanel TabPanel
    {
      get
      {
        return this.tabPanel;
      }
    }

    public int TabPanelIndex
    {
      get
      {
        return this.tabPanelIndex;
      }
    }
  }
}
