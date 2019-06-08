// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.TransparentColorBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI.RadColorPicker
{
  [ComVisible(false)]
  [ToolboxItem(false)]
  public class TransparentColorBox : RadControl
  {
    private TransparentColorBoxElement element;

    public override Color BackColor
    {
      get
      {
        return this.element.BackColor;
      }
      set
      {
        this.element.BackColor = value;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.element = new TransparentColorBoxElement();
      this.element.StretchHorizontally = true;
      this.element.StretchVertically = true;
      this.RootElement.Children.Add((RadElement) this.element);
    }
  }
}
