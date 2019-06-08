// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlLabelItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class LayoutControlLabelItem : LayoutControlItemBase
  {
    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawText = true;
      this.Padding = new Padding(3);
      this.MinSize = new Size(46, 26);
      this.Bounds = new Rectangle(0, 0, 100, 100);
      this.TextAlignment = ContentAlignment.MiddleLeft;
    }
  }
}
