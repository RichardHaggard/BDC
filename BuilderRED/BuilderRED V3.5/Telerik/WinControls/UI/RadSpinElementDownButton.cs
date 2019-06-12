// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSpinElementDownButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadSpinElementDownButton : RadRepeatArrowElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ThemeRole = "DownButton";
      this.Class = "DownButton";
      this.Padding = new Padding(1, 1, 3, 1);
    }

    public RadSpinElementDownButton()
    {
      this.Arrow.AutoSize = true;
      this.Direction = Telerik.WinControls.ArrowDirection.Down;
    }
  }
}
