// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SplitterManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class SplitterManager
  {
    private static Dictionary<Control, SplitterManager> managers = new Dictionary<Control, SplitterManager>();
    private Control container;

    protected SplitterManager(Control container)
    {
      this.container = container;
      this.container.ControlAdded += new ControlEventHandler(this.container_ControlAdded);
      this.container.ControlRemoved += new ControlEventHandler(this.container_ControlRemoved);
      this.Initialize();
    }

    private void Initialize()
    {
    }

    private void container_ControlRemoved(object sender, ControlEventArgs e)
    {
    }

    private void container_ControlAdded(object sender, ControlEventArgs e)
    {
    }

    public static SplitterManager CreateManager(Control container)
    {
      if (SplitterManager.managers.ContainsKey(container))
        return SplitterManager.managers[container];
      SplitterManager splitterManager = new SplitterManager(container);
      SplitterManager.managers.Add(container, splitterManager);
      return splitterManager;
    }

    public bool Collapse(RadSplitter splitter, SplitterCollapsedState collapsedState)
    {
      return false;
    }

    public bool Expand(RadSplitter splitter)
    {
      return false;
    }

    public bool IsCollapsed(RadSplitter splitter)
    {
      return false;
    }
  }
}
