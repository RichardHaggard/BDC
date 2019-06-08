// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridDetailViewCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class VirtualGridDetailViewCellElement : LightVisualElement
  {
    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.BackColor = Color.FromArgb(189, 218, 254);
      this.BackColor2 = Color.FromArgb(103, 146, 206);
      this.NumberOfColors = 2;
      this.GradientStyle = GradientStyles.Linear;
      this.DrawFill = true;
    }
  }
}
