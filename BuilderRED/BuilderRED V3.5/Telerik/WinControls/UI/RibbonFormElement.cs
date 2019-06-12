// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RibbonFormElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RibbonFormElement : LightVisualElement
  {
    public static RadProperty IsFormActiveProperty = RadProperty.Register("IsFormActive", typeof (bool), typeof (RibbonFormElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsDisplay));
    public static RadProperty FormWindowStateProperty = RadProperty.Register("FormWindowState", typeof (FormWindowState), typeof (RibbonFormElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) FormWindowState.Normal, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsDisplay));
    private RibbonFormBorderPrimitive borderPrimitive;
    private RadTitleBarElement titleBarElement;

    static RibbonFormElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RibbonFormElementStateManager(), typeof (RibbonFormElement));
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.borderPrimitive = new RibbonFormBorderPrimitive();
      this.borderPrimitive.Class = "RibbonFormBorder";
      this.borderPrimitive.StretchVertically = true;
      this.borderPrimitive.StretchHorizontally = true;
      this.Children.Add((RadElement) this.borderPrimitive);
      this.titleBarElement = (RadTitleBarElement) new RadFormTitleBarElement();
      this.titleBarElement.Class = "TitleBar";
      this.titleBarElement.Visibility = ElementVisibility.Collapsed;
      this.titleBarElement.StateManager = new RibbonFormTitleBarElementStateManager().StateManagerInstance;
      this.titleBarElement.HelpButton.StateManager = new RibbonFormTitleBarButtonStateManager().StateManagerInstance;
      this.titleBarElement.MinimizeButton.StateManager = new RibbonFormTitleBarButtonStateManager().StateManagerInstance;
      this.titleBarElement.MaximizeButton.StateManager = new RibbonFormTitleBarButtonStateManager().StateManagerInstance;
      this.titleBarElement.CloseButton.StateManager = new RibbonFormTitleBarButtonStateManager().StateManagerInstance;
      this.Children.Add((RadElement) this.titleBarElement);
    }

    public RibbonFormBorderPrimitive Border
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    protected internal RadTitleBarElement TitleBarElement
    {
      get
      {
        return this.titleBarElement;
      }
    }
  }
}
