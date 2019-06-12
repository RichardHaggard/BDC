// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadPanelElement : RadItem
  {
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private TextPrimitive textPrimitive;

    static RadPanelElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new PanelStateManagerFactory(), typeof (RadPanelElement));
    }

    protected override void CreateChildElements()
    {
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "RadPanelBorder";
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.BackColor = Color.Transparent;
      this.fillPrimitive.BackColor2 = Color.Transparent;
      this.fillPrimitive.BackColor3 = Color.Transparent;
      this.fillPrimitive.BackColor4 = Color.Transparent;
      this.fillPrimitive.Class = "RadPanelFill";
      this.textPrimitive = new TextPrimitive();
      this.textPrimitive.StretchHorizontally = true;
      this.textPrimitive.StretchVertically = true;
      this.textPrimitive.Alignment = ContentAlignment.MiddleLeft;
      int num = (int) this.textPrimitive.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.textPrimitive);
    }

    [Browsable(false)]
    public FillPrimitive PanelFill
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    [Browsable(false)]
    public BorderPrimitive PanelBorder
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    [Browsable(false)]
    public TextPrimitive PanelText
    {
      get
      {
        return this.textPrimitive;
      }
    }
  }
}
