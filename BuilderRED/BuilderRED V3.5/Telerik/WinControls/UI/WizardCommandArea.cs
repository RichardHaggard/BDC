// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WizardCommandArea
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class WizardCommandArea : BaseWizardElement
  {
    private ObservableCollection<RadElement> commandElements;
    private List<WizardCommandAreaButtonElement> navigationButtons;
    private WizardCommandAreaButtonElement nextButton;
    private WizardCommandAreaButtonElement cancelButton;
    private WizardCommandAreaButtonElement finishButton;
    private LightVisualElement helpButton;

    public WizardCommandArea()
    {
      this.commandElements = new ObservableCollection<RadElement>();
      this.commandElements.CollectionChanged += new NotifyCollectionChangedEventHandler(this.commandElements_CollectionChanged);
      this.commandElements.Add((RadElement) this.cancelButton);
      this.commandElements.Add((RadElement) this.finishButton);
      this.commandElements.Add((RadElement) this.nextButton);
      this.commandElements.Add((RadElement) this.helpButton);
      this.navigationButtons = new List<WizardCommandAreaButtonElement>();
      this.navigationButtons.Add(this.nextButton);
      this.navigationButtons.Add(this.finishButton);
      this.navigationButtons.Add(this.cancelButton);
    }

    public WizardCommandArea(RadWizardElement wizardElement)
    {
      this.Owner = wizardElement;
    }

    protected override void DisposeManagedResources()
    {
      this.commandElements.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.commandElements_CollectionChanged);
      this.nextButton.Click -= new EventHandler(this.NextButton_Click);
      base.DisposeManagedResources();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.nextButton = new WizardCommandAreaButtonElement();
      this.nextButton.Class = "NextButton";
      this.nextButton.UseDefaultDisabledPaint = false;
      this.nextButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("NextButtonText");
      this.nextButton.Alignment = ContentAlignment.MiddleRight;
      this.nextButton.MinSize = new Size(100, 24);
      this.nextButton.Click += new EventHandler(this.NextButton_Click);
      this.cancelButton = new WizardCommandAreaButtonElement();
      this.cancelButton.Class = "CancelButtont";
      this.cancelButton.UseDefaultDisabledPaint = false;
      this.cancelButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("CancelButtonText");
      this.cancelButton.Alignment = ContentAlignment.MiddleRight;
      this.cancelButton.MinSize = new Size(100, 24);
      this.finishButton = new WizardCommandAreaButtonElement();
      this.finishButton.Class = "FinishButton";
      this.finishButton.UseDefaultDisabledPaint = false;
      this.finishButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("FinishButtonText");
      this.finishButton.Alignment = ContentAlignment.MiddleRight;
      this.finishButton.MinSize = new Size(100, 24);
      this.helpButton = (LightVisualElement) new BaseWizardElement();
      this.helpButton.Class = "HelpButton";
      this.helpButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("HelpButtonText");
      this.helpButton.TextImageRelation = TextImageRelation.ImageBeforeText;
      this.helpButton.Alignment = ContentAlignment.MiddleLeft;
      this.helpButton.MinSize = new Size(60, 24);
    }

    public ObservableCollection<RadElement> CommandElements
    {
      get
      {
        return this.commandElements;
      }
      internal set
      {
        this.commandElements = value;
      }
    }

    public WizardCommandAreaButtonElement NextButton
    {
      get
      {
        return this.nextButton;
      }
    }

    public WizardCommandAreaButtonElement CancelButton
    {
      get
      {
        return this.cancelButton;
      }
    }

    public WizardCommandAreaButtonElement FinishButton
    {
      get
      {
        return this.finishButton;
      }
    }

    public LightVisualElement HelpButton
    {
      get
      {
        return this.helpButton;
      }
    }

    internal List<WizardCommandAreaButtonElement> NavigationButtons
    {
      get
      {
        return this.navigationButtons;
      }
      set
      {
        this.navigationButtons = value;
      }
    }

    private void commandElements_CollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Add:
          IEnumerator enumerator1 = e.NewItems.GetEnumerator();
          try
          {
            while (enumerator1.MoveNext())
            {
              RadElement current = (RadElement) enumerator1.Current;
              if (!this.Children.Contains(current))
                this.Children.Add(current);
            }
            break;
          }
          finally
          {
            (enumerator1 as IDisposable)?.Dispose();
          }
        case NotifyCollectionChangedAction.Remove:
          IEnumerator enumerator2 = e.NewItems.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
            {
              RadElement current = (RadElement) enumerator2.Current;
              if (this.Children.Contains(current))
                this.Children.Remove(current);
            }
            break;
          }
          finally
          {
            (enumerator2 as IDisposable)?.Dispose();
          }
      }
    }

    private void NextButton_Click(object sender, EventArgs e)
    {
      this.Owner.SelectNextPage();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if ((double) availableSize.Width < 1.0 || (double) availableSize.Height < 1.0)
        return availableSize;
      bool flag = (double) this.Owner.CommandAreaHeight > -1.0;
      float height = flag ? this.Owner.CommandAreaHeight : 0.0f;
      float num = availableSize.Width - (float) this.Padding.Left - (float) this.Padding.Right;
      foreach (RadElement commandElement in (Collection<RadElement>) this.commandElements)
      {
        commandElement.Measure(availableSize);
        num -= commandElement.DesiredSize.Width;
        if ((double) num < 0.0)
        {
          if (this.Children.Contains(commandElement))
            this.Children.Remove(commandElement);
        }
        else
        {
          if (!this.Children.Contains(commandElement))
          {
            this.Children.Add(commandElement);
            commandElement.Measure(availableSize);
          }
          if (!flag && (double) height < (double) commandElement.DesiredSize.Height)
            height = commandElement.DesiredSize.Height;
        }
      }
      if (!flag)
        height += (float) (this.Padding.Top + this.Padding.Bottom);
      return new SizeF(availableSize.Width, height);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      if ((double) finalSize.Width < 1.0 || (double) finalSize.Height < 1.0 || this.commandElements.Count < 1)
        return finalSize;
      float width = finalSize.Width;
      float num = 0.0f;
      bool flag1 = true;
      bool flag2 = true;
      foreach (RadElement commandElement in (Collection<RadElement>) this.commandElements)
      {
        bool flag3;
        switch (commandElement.Alignment)
        {
          case ContentAlignment.TopLeft:
          case ContentAlignment.MiddleLeft:
          case ContentAlignment.BottomLeft:
            flag3 = false;
            break;
          default:
            flag3 = true;
            break;
        }
        if (this.RightToLeft)
          flag3 = !flag3;
        float x;
        if (flag3)
        {
          width -= commandElement.DesiredSize.Width;
          if (flag2)
          {
            width -= (float) this.Padding.Right;
            flag2 = false;
          }
          x = width;
        }
        else
        {
          if (flag1)
          {
            num = (float) (this.Padding.Left + commandElement.Margin.Left);
            flag1 = false;
          }
          else
            num += commandElement.DesiredSize.Width;
          x = num;
        }
        float y = (float) (((double) finalSize.Height - (double) commandElement.DesiredSize.Height) / 2.0);
        RectangleF finalRect = new RectangleF(x, y, commandElement.DesiredSize.Width, commandElement.DesiredSize.Height);
        commandElement.Arrange(finalRect);
      }
      return finalSize;
    }

    internal virtual void UpdateButtonsText()
    {
      this.nextButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("NextButtonText");
      this.cancelButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("CancelButtonText");
      this.finishButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("FinishButtonText");
      this.helpButton.Text = LocalizationProvider<RadWizardLocalizationProvider>.CurrentProvider.GetLocalizedString("HelpButtonText");
    }

    internal void ResetButtonsFocus()
    {
      for (int index = 0; index < this.navigationButtons.Count; ++index)
        this.navigationButtons[index].IsFocusedWizardButton = false;
    }

    internal int SelectedButtonIndex()
    {
      for (int index = 0; index < this.NavigationButtons.Count; ++index)
      {
        if (this.NavigationButtons[index].IsFocusedWizardButton)
          return index;
      }
      return -1;
    }

    internal bool IsCommandButtonFocused()
    {
      for (int index = 0; index < this.NavigationButtons.Count; ++index)
      {
        if (this.NavigationButtons[index].IsFocused)
          return true;
      }
      return false;
    }
  }
}
