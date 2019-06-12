// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadStatusBarClickEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadStatusBarClickEventArgs : MouseEventArgs
  {
    private RadElement clickedElement;

    public RadElement ClickedElement
    {
      get
      {
        return this.clickedElement;
      }
      set
      {
        this.clickedElement = value;
      }
    }

    public RadStatusBarClickEventArgs(RadElement clickedElement, MouseEventArgs e)
      : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
    {
      this.ClickedElement = clickedElement;
    }
  }
}
