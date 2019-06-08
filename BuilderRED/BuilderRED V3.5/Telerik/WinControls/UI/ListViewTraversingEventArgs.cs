// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewTraversingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class ListViewTraversingEventArgs : ListViewItemEventArgs
  {
    private bool process;

    public ListViewTraversingEventArgs(ListViewDataItem content)
      : base(content)
    {
      this.process = true;
    }

    public bool Process
    {
      get
      {
        return this.process;
      }
      set
      {
        this.process = value;
      }
    }
  }
}
