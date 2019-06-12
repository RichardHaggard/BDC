// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadStatusBarPanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadStatusBarPanelElement : RadItem
  {
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private TextPrimitive textPrimitive;
    private ImagePrimitive imagePrimitive;

    protected override void CreateChildElements()
    {
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "RadPanelBorder";
      this.borderPrimitive.BoxStyle = BorderBoxStyle.OuterInnerBorders;
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.BackColor = Color.Transparent;
      this.fillPrimitive.BackColor2 = Color.Transparent;
      this.fillPrimitive.BackColor3 = Color.Transparent;
      this.fillPrimitive.BackColor4 = Color.Transparent;
      this.fillPrimitive.Class = "RadPanelFill";
      this.textPrimitive = new TextPrimitive();
      this.textPrimitive.Alignment = ContentAlignment.MiddleLeft;
      int num = (int) this.textPrimitive.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      this.imagePrimitive = new ImagePrimitive();
      this.imagePrimitive.Alignment = ContentAlignment.MiddleLeft;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.textPrimitive);
      this.Children.Add((RadElement) this.imagePrimitive);
    }
  }
}
