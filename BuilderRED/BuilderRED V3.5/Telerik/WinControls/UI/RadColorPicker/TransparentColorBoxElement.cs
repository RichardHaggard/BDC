// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.TransparentColorBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI.RadColorPicker
{
  public class TransparentColorBoxElement : RadElement
  {
    private LightVisualElement colorElement = new LightVisualElement();

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      LightVisualElement lightVisualElement = new LightVisualElement();
      lightVisualElement.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.Checkerboard;
      lightVisualElement.Alignment = ContentAlignment.TopLeft;
      lightVisualElement.ImageLayout = ImageLayout.Tile;
      lightVisualElement.StretchVertically = true;
      lightVisualElement.StretchHorizontally = true;
      lightVisualElement.ZIndex = 1;
      this.colorElement.DrawFill = true;
      this.colorElement.NumberOfColors = 1;
      this.colorElement.BackColor = Color.Red;
      this.colorElement.StretchHorizontally = true;
      this.colorElement.StretchVertically = true;
      this.colorElement.ZIndex = 2;
      this.Children.Add((RadElement) lightVisualElement);
      this.Children.Add((RadElement) this.colorElement);
    }

    public Color BackColor
    {
      get
      {
        return this.colorElement.BackColor;
      }
      set
      {
        this.colorElement.BackColor = value;
      }
    }
  }
}
