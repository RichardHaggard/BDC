// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardAeroView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class WizardAeroView : WizardView
  {
    private WizardAeroTopElement topElement;

    public WizardAeroView()
    {
    }

    public WizardAeroView(RadWizardElement wizardElement)
    {
      this.Owner = wizardElement;
      this.CommandArea.Owner = wizardElement;
      this.topElement.Owner = wizardElement;
      this.PageHeaderElement.Owner = wizardElement;
      this.PageHeaderElement.IconElement.Alignment = ContentAlignment.MiddleLeft;
      this.AddPages();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.CommandArea = new WizardCommandArea();
      this.Children.Add((RadElement) this.CommandArea);
      this.topElement = new WizardAeroTopElement();
      this.Children.Add((RadElement) this.topElement);
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

    public WizardAeroTopElement TopElement
    {
      get
      {
        return this.topElement;
      }
    }

    public override RadButtonElement BackButton
    {
      get
      {
        return (RadButtonElement) this.topElement.BackButton;
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
      float x1;
      float x2;
      if (this.RightToLeft)
      {
        x1 = finalSize.Width - this.topElement.DesiredSize.Width;
        x2 = 0.0f;
      }
      else
      {
        x1 = 0.0f;
        x2 = this.topElement.DesiredSize.Width;
      }
      float num1 = (double) this.PageHeaderHeight > -1.0 ? this.PageHeaderHeight : this.PageHeaderElement.DesiredSize.Height;
      if ((double) num1 < (double) this.topElement.DesiredSize.Height)
        num1 = this.topElement.DesiredSize.Height;
      RectangleF finalRect1 = new RectangleF(x1, 0.0f, this.topElement.DesiredSize.Width, num1);
      RectangleF finalRect2 = new RectangleF(x2, 0.0f, finalSize.Width - this.topElement.DesiredSize.Width, num1);
      float height = (double) this.CommandAreaHeight > -1.0 ? this.CommandAreaHeight : this.CommandArea.DesiredSize.Height;
      RectangleF finalRect3 = new RectangleF(0.0f, finalSize.Height - height, finalSize.Width, height);
      float num2 = 0.0f;
      if (this.SelectedPage is WizardWelcomePage && !this.HideWelcomeImage)
        num2 = this.ArrangeImageElement(finalSize, this.WelcomeImageElement, num1);
      else
        this.WelcomeImageElement.Visibility = ElementVisibility.Collapsed;
      if (this.SelectedPage is WizardCompletionPage && !this.HideCompletionImage)
        num2 = this.ArrangeImageElement(finalSize, this.CompletionImageElement, num1);
      else
        this.CompletionImageElement.Visibility = ElementVisibility.Collapsed;
      RectangleF finalRect4 = new RectangleF(!this.RightToLeft ? num2 : 0.0f, num1, finalSize.Width - num2, finalSize.Height - num1 - height);
      this.topElement.Arrange(finalRect1);
      this.PageHeaderElement.Arrange(finalRect2);
      if (this.SelectedPage != null)
      {
        this.SelectedPage.Arrange(finalRect4);
        if (this.SelectedPage.ContentArea != null)
          this.SelectedPage.LocateContentArea();
      }
      this.CommandArea.Arrange(finalRect3);
      return finalSize;
    }

    internal override bool IsLastNavigationButtonFocused()
    {
      if (this.BackButton.Visibility == ElementVisibility.Visible && this.BackButton.Enabled)
        return this.BackButton.IsFocused;
      for (int index = this.CommandArea.NavigationButtons.Count - 1; index >= 0; --index)
      {
        if (this.CommandArea.NavigationButtons[index].Visibility == ElementVisibility.Visible && this.CommandArea.NavigationButtons[index].Enabled)
          return this.CommandArea.NavigationButtons[index].IsFocusedWizardButton;
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
      if (this.BackButton.Visibility == ElementVisibility.Visible && this.BackButton.Enabled)
        return this.BackButton.IsFocused;
      return false;
    }

    internal override bool SelectPreviousNavigationButton()
    {
      int index1 = this.CommandArea.SelectedButtonIndex();
      if (index1 == -1 || this.IsFirstNavigationButtonFocused())
      {
        if (index1 != -1)
          this.CommandArea.NavigationButtons[index1].IsFocusedWizardButton = false;
        if (this.BackButton.Visibility == ElementVisibility.Visible && this.BackButton.Enabled && !this.BackButton.IsFocused)
        {
          this.BackButton.Focus();
          return true;
        }
        index1 = this.CommandArea.NavigationButtons.Count;
      }
      int index2 = index1 == -1 ? this.CommandArea.NavigationButtons.Count : index1;
      for (int index3 = index2 - 1; index3 >= 0; --index3)
      {
        if (this.CommandArea.NavigationButtons[index3].Visibility == ElementVisibility.Visible && this.CommandArea.NavigationButtons[index3].Enabled)
        {
          if (index2 < this.CommandArea.NavigationButtons.Count)
            this.CommandArea.NavigationButtons[index2].IsFocusedWizardButton = false;
          this.CommandArea.NavigationButtons[index3].Focus();
          this.CommandArea.NavigationButtons[index3].IsFocusedWizardButton = true;
          return true;
        }
      }
      return false;
    }

    internal override bool SelectFollowingNavigationButton()
    {
      int index1 = this.CommandArea.SelectedButtonIndex();
      if (this.IsLastNavigationButtonFocused() && (this.BackButton.Visibility != ElementVisibility.Visible || !this.BackButton.Enabled))
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
      if (index1 == -1 || this.BackButton.Visibility != ElementVisibility.Visible || (!this.BackButton.Enabled || this.BackButton.IsFocused))
        return false;
      this.CommandArea.NavigationButtons[index1].IsFocusedWizardButton = false;
      this.BackButton.Focus();
      return true;
    }

    private void UpdateButtonFocus(object sender, EventArgs e)
    {
      if (sender == this.CommandArea.NextButton && this.CommandArea.FinishButton.Visibility == ElementVisibility.Visible)
      {
        this.CommandArea.CancelButton.IsFocusedWizardButton = false;
        this.CommandArea.NextButton.IsFocusedWizardButton = false;
        this.CommandArea.FinishButton.IsFocusedWizardButton = true;
        this.CommandArea.FinishButton.Focus();
      }
      else if (sender == this.BackButton && !this.BackButton.Enabled && this.CommandArea.NextButton.Visibility == ElementVisibility.Visible)
      {
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
