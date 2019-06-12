// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadScrollablePanelElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class RadScrollablePanelElement : RadItem
  {
    private BorderPrimitive panelBorderPrimitive;
    private FillPrimitive panelFillPrimitive;
    private TextPrimitive panelTextPrimitive;

    [Browsable(false)]
    public TextPrimitive TextPrimitive
    {
      get
      {
        return this.panelTextPrimitive;
      }
    }

    [Browsable(false)]
    public BorderPrimitive Border
    {
      get
      {
        return this.panelBorderPrimitive;
      }
    }

    [Browsable(false)]
    public FillPrimitive Fill
    {
      get
      {
        return this.panelFillPrimitive;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.panelFillPrimitive = new FillPrimitive();
      this.panelFillPrimitive.Class = "RadScrollablePanelFill";
      this.Children.Add((RadElement) this.panelFillPrimitive);
      this.panelBorderPrimitive = new BorderPrimitive();
      this.panelBorderPrimitive.Class = "RadScrollablePanelBorder";
      this.Children.Add((RadElement) this.panelBorderPrimitive);
      this.panelTextPrimitive = new TextPrimitive();
      this.panelTextPrimitive.Text = "RadScrollablePanelText";
      int num = (int) this.panelTextPrimitive.BindProperty(TextPrimitive.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      this.Children.Add((RadElement) this.panelTextPrimitive);
    }
  }
}
