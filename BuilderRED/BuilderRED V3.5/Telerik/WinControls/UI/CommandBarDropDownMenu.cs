// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarDropDownMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  internal class CommandBarDropDownMenu : RadMenuItem
  {
    private CommandBarStripElement representedElement;

    public CommandBarDropDownMenu(CommandBarStripElement representedElement)
    {
      this.representedElement = representedElement;
      this.Class = "RadMenuItem";
    }

    public override bool IsChecked
    {
      get
      {
        return base.IsChecked;
      }
      set
      {
        base.IsChecked = value;
        this.representedElement.VisibleInCommandBar = value;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.IsChecked = !this.IsChecked;
      base.OnMouseDown(e);
    }

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadMenuItem);
      }
    }
  }
}
