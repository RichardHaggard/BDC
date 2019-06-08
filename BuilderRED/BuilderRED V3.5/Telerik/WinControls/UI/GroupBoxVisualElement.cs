// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GroupBoxVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class GroupBoxVisualElement : RadItem
  {
    private FillPrimitive fill;
    protected BorderPrimitive borderPrimitive;

    static GroupBoxVisualElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new GroupBoxVisualElementStateManagerFactory(), typeof (GroupBoxVisualElement));
    }

    [Browsable(false)]
    public FillPrimitive Fill
    {
      get
      {
        return this.fill;
      }
    }

    [Browsable(false)]
    public BorderPrimitive Border
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.fill = new FillPrimitive();
      this.borderPrimitive = new BorderPrimitive();
      this.Children.Add((RadElement) this.fill);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.fill.Class = "fill" + this.ToString();
      this.borderPrimitive.Class = "border" + this.ToString();
    }
  }
}
