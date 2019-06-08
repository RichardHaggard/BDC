// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Wizard97View
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class Wizard97View : WizardView
  {
    public Wizard97View()
    {
    }

    public Wizard97View(RadWizardElement wizardElement)
    {
      this.Owner = wizardElement;
      this.CommandArea.Owner = wizardElement;
      this.PageHeaderElement.Owner = wizardElement;
      this.PageHeaderElement.IconElement.Alignment = ContentAlignment.MiddleRight;
      this.AddPages();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.CommandArea = (WizardCommandArea) new Wizard97CommandArea();
      this.Children.Add((RadElement) this.CommandArea);
      this.BackButton.Click += new EventHandler(this.UpdateButtonFocus);
      this.NextButton.Click += new EventHandler(this.UpdateButtonFocus);
      this.CancelButton.Click += new EventHandler(this.UpdateButtonFocus);
      this.FinishButton.Click += new EventHandler(this.UpdateButtonFocus);
    }

    protected override void DisposeManagedResources()
    {
      base.DisposeManagedResources();
      this.BackButton.Click -= new EventHandler(this.UpdateButtonFocus);
      this.NextButton.Click -= new EventHandler(this.UpdateButtonFocus);
      this.CancelButton.Click -= new EventHandler(this.UpdateButtonFocus);
      this.FinishButton.Click -= new EventHandler(this.UpdateButtonFocus);
    }

    public override RadButtonElement BackButton
    {
      get
      {
        return (RadButtonElement) (this.CommandArea as Wizard97CommandArea).BackButton;
      }
    }

    public override WizardCommandAreaButtonElement NextButton
    {
      get
      {
        return this.CommandArea.NextButton;
      }
    }

    public override WizardCommandAreaButtonElement CancelButton
    {
      get
      {
        return this.CommandArea.CancelButton;
      }
    }

    public override WizardCommandAreaButtonElement FinishButton
    {
      get
      {
        return this.CommandArea.FinishButton;
      }
    }

    public override LightVisualElement HelpButton
    {
      get
      {
        return this.CommandArea.HelpButton;
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return base.MeasureOverride(availableSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if ((double) finalSize.Width < 1.0 || (double) finalSize.Height < 1.0)
        return finalSize;
      float num1 = (double) this.PageHeaderHeight > -1.0 ? this.PageHeaderHeight : this.PageHeaderElement.DesiredSize.Height;
      RectangleF finalRect1 = new RectangleF(0.0f, 0.0f, finalSize.Width, num1);
      float height = (double) this.CommandAreaHeight > -1.0 ? this.CommandAreaHeight : this.CommandArea.DesiredSize.Height;
      RectangleF finalRect2 = new RectangleF(0.0f, finalSize.Height - height, finalSize.Width, height);
      float num2 = 0.0f;
      if (this.SelectedPage is WizardWelcomePage && !this.HideWelcomeImage)
        num2 = this.ArrangeImageElement(finalSize, this.WelcomeImageElement, num1);
      else
        this.WelcomeImageElement.Visibility = ElementVisibility.Collapsed;
      if (this.SelectedPage is WizardCompletionPage && !this.HideCompletionImage)
        num2 = this.ArrangeImageElement(finalSize, this.CompletionImageElement, num1);
      else
        this.CompletionImageElement.Visibility = ElementVisibility.Collapsed;
      RectangleF finalRect3 = new RectangleF(!this.RightToLeft ? num2 : 0.0f, num1, finalSize.Width - num2, finalSize.Height - num1 - height);
      this.PageHeaderElement.Arrange(finalRect1);
      if (this.SelectedPage != null)
      {
        this.SelectedPage.Arrange(finalRect3);
        if (this.SelectedPage.ContentArea != null)
          this.SelectedPage.LocateContentArea();
      }
      this.CommandArea.Arrange(finalRect2);
      return finalSize;
    }

    internal override bool SelectPreviousNavigationButton()
    {
      int index1 = this.CommandArea.SelectedButtonIndex();
      if (index1 == -1 || this.IsFirstNavigationButtonFocused())
      {
        if (index1 != -1)
          this.CommandArea.NavigationButtons[index1].IsFocusedWizardButton = false;
        index1 = this.CommandArea.NavigationButtons.Count;
      }
      for (int index2 = index1 - 1; index2 >= 0; --index2)
      {
        if (this.CommandArea.NavigationButtons[index2].Visibility == ElementVisibility.Visible && this.CommandArea.NavigationButtons[index2].Enabled)
        {
          if (index1 < this.CommandArea.NavigationButtons.Count)
            this.CommandArea.NavigationButtons[index1].IsFocusedWizardButton = false;
          this.CommandArea.NavigationButtons[index2].Focus();
          this.CommandArea.NavigationButtons[index2].IsFocusedWizardButton = true;
          return true;
        }
      }
      return false;
    }

    internal override bool SelectFollowingNavigationButton()
    {
      int index1 = this.CommandArea.SelectedButtonIndex();
      if (this.IsLastNavigationButtonFocused())
      {
        this.CommandArea.NavigationButtons[index1].IsFocusedWizardButton = false;
        index1 = -1;
      }
      for (int index2 = index1 + 1; index2 < this.CommandArea.NavigationButtons.Count; ++index2)
      {
        if (this.CommandArea.NavigationButtons[index2].Visibility == ElementVisibility.Visible && this.CommandArea.NavigationButtons[index2].Enabled)
        {
          if (index1 != -1)
            this.CommandArea.NavigationButtons[index1].IsFocusedWizardButton = false;
          this.CommandArea.NavigationButtons[index2].Focus();
          this.CommandArea.NavigationButtons[index2].IsFocusedWizardButton = true;
          return true;
        }
      }
      return false;
    }

    internal override bool IsFirstNavigationButtonFocused()
    {
      for (int index = 0; index < this.CommandArea.NavigationButtons.Count; ++index)
      {
        if (this.CommandArea.NavigationButtons[index].Visibility == ElementVisibility.Visible && this.CommandArea.NavigationButtons[index].Enabled)
          return this.CommandArea.NavigationButtons[index].IsFocusedWizardButton;
      }
      return false;
    }

    internal override bool IsLastNavigationButtonFocused()
    {
      for (int index = this.CommandArea.NavigationButtons.Count - 1; index >= 0; --index)
      {
        if (this.CommandArea.NavigationButtons[index].Visibility == ElementVisibility.Visible && this.CommandArea.NavigationButtons[index].Enabled)
          return this.CommandArea.NavigationButtons[index].IsFocusedWizardButton;
      }
      return false;
    }

    private void UpdateButtonFocus(object sender, EventArgs e)
    {
      if (sender == this.CommandArea.NextButton && this.CommandArea.FinishButton.Visibility == ElementVisibility.Visible)
      {
        this.CommandArea.CancelButton.IsFocusedWizardButton = false;
        ((WizardCommandAreaButtonElement) this.BackButton).IsFocusedWizardButton = false;
        this.CommandArea.NextButton.IsFocusedWizardButton = false;
        this.CommandArea.FinishButton.IsFocusedWizardButton = true;
        this.CommandArea.FinishButton.Focus();
      }
      else if (sender == this.BackButton && !this.BackButton.Enabled && this.CommandArea.NextButton.Visibility == ElementVisibility.Visible)
      {
        ((WizardCommandAreaButtonElement) this.BackButton).IsFocusedWizardButton = false;
        this.CommandArea.NextButton.IsFocusedWizardButton = true;
        this.CommandArea.NextButton.Focus();
      }
      else
      {
        for (int index = 0; index < this.CommandArea.NavigationButtons.Count; ++index)
        {
          if (this.CommandArea.NavigationButtons[index] == sender)
            this.CommandArea.NavigationButtons[index].IsFocusedWizardButton = true;
          else
            this.CommandArea.NavigationButtons[index].IsFocusedWizardButton = false;
        }
      }
    }
  }
}
