// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewGroupToggleButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class ListViewGroupToggleButton : RadToggleButtonElement
  {
    protected override void OnKeyDown(KeyEventArgs e)
    {
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadItem.VisualStateProperty || !this.VisualState.Contains("Disabled"))
        return;
      ((IStylableElement) this).VisualState = this.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? "ListViewGroupToggleButton.ToggleState=On" : nameof (ListViewGroupToggleButton);
    }
  }
}
