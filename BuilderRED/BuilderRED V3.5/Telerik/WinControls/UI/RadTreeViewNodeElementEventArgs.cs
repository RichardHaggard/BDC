// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTreeViewNodeElementEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class RadTreeViewNodeElementEventArgs : RadTreeViewEventArgs
  {
    private TreeNodeElement nodeElement;

    public RadTreeViewNodeElementEventArgs(TreeNodeElement nodeElement)
      : base(nodeElement.Data)
    {
      this.nodeElement = nodeElement;
    }

    public TreeNodeElement NodeElement
    {
      get
      {
        return this.nodeElement;
      }
    }
  }
}
