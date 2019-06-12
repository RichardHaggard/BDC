// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Wizard97CommandArea
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class Wizard97CommandArea : WizardCommandArea
  {
    private WizardCommandAreaButtonElement backButton;

    public Wizard97CommandArea()
    {
      if (this.CommandElements.Count > 1)
        this.CommandElements.Insert(this.CommandElements.Count - 1, (RadElement) this.backButton);
      else
        this.CommandElements.Add((RadElement) this.backButton);
      this.NavigationButtons.Insert(0, this.backButton);
    }

    public Wizard97CommandArea(RadWizardElement wizardElement)
    {
      this.Owner = wizardElement;
    }

    protected override void DisposeManagedResources()
    {
      this.backButton.Click -= new EventHandler(this.BackButton_Click);
      base.DisposeManagedResources();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.backButton = new WizardCommandAreaButtonElement();
      this.backButton.Class = "BackButton";
      this.backButton.UseDefaultDisabledPaint = false;
      this.backButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("BackButtonText");
      this.backButton.Alignment = ContentAlignment.MiddleRight;
      this.backButton.MinSize = new Size(100, 24);
      this.backButton.Click += new EventHandler(this.BackButton_Click);
    }

    public WizardCommandAreaButtonElement BackButton
    {
      get
      {
        return this.backButton;
      }
    }

    private void BackButton_Click(object sender, EventArgs e)
    {
      this.Owner.SelectPreviousPage();
    }

    internal override void UpdateButtonsText()
    {
      base.UpdateButtonsText();
      this.backButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("BackButtonText");
    }
  }
}
