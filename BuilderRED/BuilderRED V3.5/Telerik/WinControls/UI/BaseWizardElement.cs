// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseWizardElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class BaseWizardElement : LightVisualElement
  {
    public static RadProperty IsWelcomePageProperty = RadProperty.Register(nameof (IsWelcomePage), typeof (bool), typeof (WizardCommandArea), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsCompletionPageProperty = RadProperty.Register(nameof (IsCompletionPage), typeof (bool), typeof (WizardCommandArea), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    private RadWizardElement owner;

    static BaseWizardElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new BaseWizardElementStateManagerFactory(), typeof (BaseWizardElement));
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating that the element currently refers to a WizardWelcomePage.")]
    public virtual bool IsWelcomePage
    {
      get
      {
        return (bool) this.GetValue(BaseWizardElement.IsWelcomePageProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseWizardElement.IsWelcomePageProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating that the element currently refers to a WizardCompletionPage.")]
    [Category("Behavior")]
    public virtual bool IsCompletionPage
    {
      get
      {
        return (bool) this.GetValue(BaseWizardElement.IsCompletionPageProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseWizardElement.IsCompletionPageProperty, (object) value);
      }
    }

    public RadWizardElement Owner
    {
      get
      {
        return this.owner;
      }
      internal set
      {
        this.owner = value;
      }
    }

    public virtual void UpdateInfo(WizardPage page)
    {
      if (page == null)
      {
        this.IsWelcomePage = false;
        this.IsCompletionPage = false;
      }
      this.IsWelcomePage = page is WizardWelcomePage;
      this.IsCompletionPage = page is WizardCompletionPage;
    }
  }
}
