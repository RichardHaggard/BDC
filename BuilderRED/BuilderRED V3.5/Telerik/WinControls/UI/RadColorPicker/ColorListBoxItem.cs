// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorPicker.ColorListBoxItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI.RadColorPicker
{
  public class ColorListBoxItem : RadListVisualItem
  {
    private const int colorBoxSizeWidth = 30;
    private LightVisualElement colorBox;
    private LightVisualElement textBox;

    protected override System.Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadListVisualItem);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Padding = new Padding(2);
      this.DrawText = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      StackLayoutElement stackLayoutElement = new StackLayoutElement();
      stackLayoutElement.StretchVertically = true;
      stackLayoutElement.FitInAvailableSize = true;
      stackLayoutElement.Orientation = Orientation.Horizontal;
      stackLayoutElement.Padding = new Padding(2);
      stackLayoutElement.ElementSpacing = 5;
      stackLayoutElement.StretchHorizontally = false;
      this.Children.Add((RadElement) stackLayoutElement);
      this.colorBox = new LightVisualElement();
      this.colorBox.GradientStyle = GradientStyles.Solid;
      this.colorBox.DrawFill = true;
      this.colorBox.NotifyParentOnMouseInput = true;
      this.colorBox.MinSize = new Size(30, 0);
      this.colorBox.MaxSize = new Size(30, 0);
      this.colorBox.DrawBorder = true;
      this.colorBox.BorderColor = Color.Black;
      this.colorBox.BorderGradientStyle = GradientStyles.Solid;
      this.colorBox.NotifyParentOnMouseInput = true;
      this.colorBox.StretchHorizontally = false;
      stackLayoutElement.Children.Add((RadElement) this.colorBox);
      this.textBox = new LightVisualElement();
      this.textBox.NotifyParentOnMouseInput = true;
      this.textBox.TextAlignment = ContentAlignment.MiddleLeft;
      this.textBox.StretchHorizontally = false;
      stackLayoutElement.Children.Add((RadElement) this.textBox);
    }

    public override void Synchronize()
    {
      base.Synchronize();
      this.colorBox.BackColor = (Color) this.Data.Value;
      string str = this.Data.Value.ToString();
      int num = str.IndexOf('[');
      this.textBox.Text = LocalizationProvider<ColorDialogLocalizationProvider>.CurrentProvider.GetLocalizedString(str.Substring(num + 1, str.Length - num - 2));
      this.Text = "";
    }
  }
}
