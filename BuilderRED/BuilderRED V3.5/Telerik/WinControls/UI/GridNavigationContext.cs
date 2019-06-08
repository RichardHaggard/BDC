// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridNavigationContext
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridNavigationContext
  {
    private GridNavigationInputType inputType;
    private MouseButtons mouseButtons;
    private Keys modifierKeys;

    public GridNavigationContext(
      GridNavigationInputType inputType,
      MouseButtons mouseButtons,
      Keys modifierKeys)
    {
      this.inputType = inputType;
      this.mouseButtons = mouseButtons;
      this.modifierKeys = modifierKeys;
    }

    public GridNavigationInputType InputType
    {
      get
      {
        return this.inputType;
      }
    }

    public MouseButtons MouseButtons
    {
      get
      {
        return this.mouseButtons;
      }
    }

    public Keys ModifierKeys
    {
      get
      {
        return this.modifierKeys;
      }
    }
  }
}
