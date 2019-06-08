// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadWizardElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  public class RadWizardElement : LightVisualElement
  {
    public static RadProperty EnableAeroStyleProperty = RadProperty.Register(nameof (EnableAeroStyle), typeof (bool), typeof (RadWizardElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    private WizardMode mode;
    private WizardView view;
    private RadWizard ownerControl;
    private WizardPageCollection pages;
    private WizardWelcomePage welcomePage;
    private WizardCompletionPage completionPage;
    private WizardPage selectedPage;
    private bool pageHeaderCustomized;

    public RadWizardElement()
    {
      this.pages.CollectionChanging += new NotifyCollectionChangingEventHandler(this.Pages_CollectionChanging);
      this.pages.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Pages_CollectionChanged);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.pages = new WizardPageCollection(this);
      this.mode = WizardMode.Wizard97;
      this.UpdateView(this.mode);
      this.pageHeaderCustomized = false;
      LocalizationProvider<RadWizardLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.RadWizardLocalizationProvider_CurrentProviderChanged);
    }

    protected override void DisposeManagedResources()
    {
      this.pages.CollectionChanging -= new NotifyCollectionChangingEventHandler(this.Pages_CollectionChanging);
      this.pages.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.Pages_CollectionChanged);
      LocalizationProvider<RadWizardLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.RadWizardLocalizationProvider_CurrentProviderChanged);
      base.DisposeManagedResources();
    }

    protected internal override void OnParentPropertyChanged(RadPropertyChangedEventArgs args)
    {
      base.OnParentPropertyChanged(args);
      if (this.ElementTree == null || this.selectedPage == null || (this.selectedPage.ContentArea == null || this.selectedPage.ContentArea.Parent == this.ElementTree.Control))
        return;
      this.selectedPage.ContentArea.Parent = this.ElementTree.Control;
    }

    public WizardMode Mode
    {
      get
      {
        return this.mode;
      }
      set
      {
        if (this.mode == value)
          return;
        ModeChangingEventArgs e = new ModeChangingEventArgs(this.mode, value);
        this.OnModeChanging(e);
        if (e.Cancel)
          return;
        WizardMode mode = this.mode;
        this.mode = value;
        this.UpdateView(this.mode);
        this.OnModeChanged(new ModeChangedEventArgs(mode, this.mode));
      }
    }

    public WizardView View
    {
      get
      {
        return this.view;
      }
    }

    public RadWizard OwnerControl
    {
      get
      {
        return this.ownerControl;
      }
      internal set
      {
        this.ownerControl = value;
      }
    }

    public bool EnableAeroStyle
    {
      get
      {
        return (bool) this.GetValue(RadWizardElement.EnableAeroStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadWizardElement.EnableAeroStyleProperty, (object) value);
      }
    }

    public WizardPageCollection Pages
    {
      get
      {
        return this.pages;
      }
    }

    public WizardWelcomePage WelcomePage
    {
      get
      {
        return this.welcomePage;
      }
      set
      {
        this.welcomePage = value;
      }
    }

    public WizardCompletionPage CompletionPage
    {
      get
      {
        return this.completionPage;
      }
      set
      {
        this.completionPage = value;
      }
    }

    public WizardPage SelectedPage
    {
      get
      {
        return this.selectedPage;
      }
      set
      {
        SelectedPageChangingEventArgs e = new SelectedPageChangingEventArgs(this.selectedPage, value);
        this.OnSelectedPageChanging((object) this, e);
        if (e.Cancel)
          return;
        if (this.selectedPage != null)
        {
          this.selectedPage.Visibility = ElementVisibility.Collapsed;
          if (this.selectedPage.ContentArea != null)
            this.selectedPage.ContentArea.Visible = false;
        }
        if (value == null || value.Owner != this)
        {
          this.selectedPage = (WizardPage) null;
          this.UpdateView(this.selectedPage);
          this.InvalidateMeasure(true);
        }
        else
        {
          WizardPage selectedPage = this.selectedPage;
          this.selectedPage = value;
          if (this.selectedPage.ContentArea != null)
          {
            this.selectedPage.ContentArea.Visible = true;
            if (this.ElementTree != null && this.selectedPage.ContentArea.Parent != this.ElementTree.Control)
              this.selectedPage.ContentArea.Parent = this.ElementTree.Control;
          }
          this.UpdateView(this.selectedPage);
          this.selectedPage.Visibility = ElementVisibility.Visible;
          this.InvalidateMeasure(true);
          this.OnSelectedPageChanged((object) this, new SelectedPageChangedEventArgs(selectedPage, this.selectedPage));
        }
      }
    }

    public WizardCommandArea CommandArea
    {
      get
      {
        return this.view.CommandArea;
      }
    }

    public float CommandAreaHeight
    {
      get
      {
        return this.view.CommandAreaHeight;
      }
      set
      {
        this.view.CommandAreaHeight = value;
      }
    }

    public WizardPageHeaderElement PageHeaderElement
    {
      get
      {
        return this.view.PageHeaderElement;
      }
    }

    public float PageHeaderHeight
    {
      get
      {
        return this.view.PageHeaderHeight;
      }
      set
      {
        this.view.PageHeaderHeight = value;
      }
    }

    public LightVisualElement WelcomeImageElement
    {
      get
      {
        return this.view.WelcomeImageElement;
      }
    }

    public LightVisualElement CompletionImageElement
    {
      get
      {
        return this.view.CompletionImageElement;
      }
    }

    public Image WelcomeImage
    {
      get
      {
        return this.view.WelcomeImage;
      }
      set
      {
        this.view.WelcomeImage = value;
        this.UpdateImageElements(this.selectedPage);
      }
    }

    public bool HideWelcomeImage
    {
      get
      {
        return this.view.HideWelcomeImage;
      }
      set
      {
        this.view.HideWelcomeImage = value;
      }
    }

    public ImageLayout WelcomeImageLayout
    {
      get
      {
        return this.view.WelcomeImageLayout;
      }
      set
      {
        this.view.WelcomeImageLayout = value;
      }
    }

    public RadImageShape WelcomeImageBackgroundShape
    {
      get
      {
        return this.view.WelcomeImageBackgroundShape;
      }
      set
      {
        this.view.WelcomeImageBackgroundShape = value;
      }
    }

    public Image CompletionImage
    {
      get
      {
        return this.view.CompletionImage;
      }
      set
      {
        this.view.CompletionImage = value;
        this.UpdateImageElements(this.selectedPage);
      }
    }

    public bool HideCompletionImage
    {
      get
      {
        return this.view.HideCompletionImage;
      }
      set
      {
        this.view.HideCompletionImage = value;
      }
    }

    public ImageLayout CompletionImageLayout
    {
      get
      {
        return this.view.CompletionImageLayout;
      }
      set
      {
        this.view.CompletionImageLayout = value;
      }
    }

    public RadImageShape CompletionImageBackgroundShape
    {
      get
      {
        return this.view.CompletionImageBackgroundShape;
      }
      set
      {
        this.view.CompletionImageBackgroundShape = value;
      }
    }

    public ElementVisibility PageTitleTextVisibility
    {
      get
      {
        return this.view.PageTitleTextVisibility;
      }
      set
      {
        this.view.PageTitleTextVisibility = value;
      }
    }

    public ElementVisibility PageHeaderTextVisibility
    {
      get
      {
        return this.view.PageHeaderTextVisibility;
      }
      set
      {
        this.view.PageHeaderTextVisibility = value;
      }
    }

    public Image PageHeaderIcon
    {
      get
      {
        return this.view.PageHeaderIcon;
      }
      set
      {
        this.view.PageHeaderIcon = value;
        this.UpdatePageHeaderIconElement(this.selectedPage);
      }
    }

    public ContentAlignment PageHeaderIconAlignment
    {
      get
      {
        return this.view.PageHeaderIconAlignment;
      }
      set
      {
        this.view.PageHeaderIconAlignment = value;
      }
    }

    public RadButtonElement BackButton
    {
      get
      {
        return this.view.BackButton;
      }
    }

    public WizardCommandAreaButtonElement NextButton
    {
      get
      {
        return this.view.NextButton;
      }
    }

    public WizardCommandAreaButtonElement CancelButton
    {
      get
      {
        return this.view.CancelButton;
      }
    }

    public WizardCommandAreaButtonElement FinishButton
    {
      get
      {
        return this.view.FinishButton;
      }
    }

    public LightVisualElement HelpButton
    {
      get
      {
        return this.view.HelpButton;
      }
    }

    public void Refresh()
    {
      this.UpdateView(this.selectedPage);
      this.InvalidateMeasure(true);
    }

    internal void ApplyAeroStyle()
    {
      if (this.mode != WizardMode.Aero || this.ownerControl == null)
        return;
      this.ownerControl.ApplyAeroStyle();
    }

    public void SelectNextPage()
    {
      WizardCancelEventArgs e = new WizardCancelEventArgs();
      this.OnNext(e);
      if (e.Cancel)
        return;
      int num1 = this.Pages.IndexOf(this.SelectedPage);
      if (num1 >= this.Pages.Count - 1)
        return;
      int num2;
      this.SelectedPage = this.Pages[num2 = num1 + 1];
    }

    public void SelectPreviousPage()
    {
      WizardCancelEventArgs e = new WizardCancelEventArgs();
      this.OnPrevious(e);
      if (e.Cancel)
        return;
      int num1 = this.Pages.IndexOf(this.SelectedPage);
      if (num1 <= 0)
        return;
      int num2;
      this.SelectedPage = this.Pages[num2 = num1 - 1];
    }

    private void UpdateView(WizardMode mode)
    {
      RadWizard radWizard = (RadWizard) null;
      if (this.ElementTree != null)
        radWizard = this.ElementTree.Control as RadWizard;
      if (this.view != null)
      {
        radWizard?.UnWireEvents();
        this.Children.Remove((RadElement) this.view);
      }
      WizardView view = this.view;
      switch (this.mode)
      {
        case WizardMode.Wizard97:
          this.view = (WizardView) new Wizard97View(this);
          if (radWizard != null)
          {
            radWizard.UnapplyAeroStyle();
            break;
          }
          break;
        case WizardMode.Aero:
          this.view = (WizardView) new WizardAeroView(this);
          break;
      }
      if (this.view != null)
      {
        this.Children.Add((RadElement) this.view);
        this.UpdateView(this.selectedPage);
        radWizard?.WireEvents();
      }
      if (view == null)
        return;
      this.SetViewProperties(view);
    }

    private void UpdateView(WizardPage page)
    {
      this.UpdateImageElements(page);
      this.UpdatePageHeaderElement(page);
      this.UpdateCommandButtonsStatus(page);
      this.CommandArea.UpdateInfo(page);
      this.PageHeaderElement.UpdateInfo(page);
    }

    internal void UpdateImageElements(WizardPage page)
    {
      if (page == null)
      {
        if (this.selectedPage == null)
          return;
        page = this.selectedPage;
      }
      WizardWelcomePage wizardWelcomePage = page as WizardWelcomePage;
      if (wizardWelcomePage != null)
      {
        if (wizardWelcomePage.WelcomeImage != null)
          this.WelcomeImageElement.Image = wizardWelcomePage.WelcomeImage;
        else
          this.WelcomeImageElement.Image = this.WelcomeImage;
      }
      else
      {
        WizardCompletionPage selectedPage = this.SelectedPage as WizardCompletionPage;
        if (selectedPage == null)
          return;
        if (selectedPage.CompletionImage != null)
          this.CompletionImageElement.Image = selectedPage.CompletionImage;
        else
          this.CompletionImageElement.Image = this.CompletionImage;
      }
    }

    private void UpdatePageHeaderElement(WizardPage page)
    {
      if (page == null || this.view == null || this.PageHeaderElement == null)
      {
        this.PageHeaderElement.Title = string.Empty;
        this.PageHeaderElement.Header = string.Empty;
        this.PageHeaderElement.IconElement.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        this.PageHeaderElement.Title = page.Title;
        this.PageHeaderElement.Header = page.Header;
        this.UpdatePageHeaderTextsVisibility(page);
        this.UpdatePageHeaderIconElement(page);
        this.PageHeaderElement.IconElement.Visibility = ElementVisibility.Visible;
      }
    }

    private void UpdatePageHeaderTextsVisibility(WizardPage page)
    {
      if (page == null)
        return;
      if (page.CustomizePageHeader)
      {
        this.PageHeaderElement.TitleElement.Visibility = page.TitleVisibility;
        this.PageHeaderElement.HeaderElement.Visibility = page.HeaderVisibility;
        this.pageHeaderCustomized = true;
      }
      else
      {
        if (!this.pageHeaderCustomized)
          return;
        if (this.PageHeaderElement.SetDefaultTitleVisibility)
        {
          this.PageHeaderElement.TitleElement.Visibility = this.PageHeaderElement.DefaultTitleVisibility;
        }
        else
        {
          int num1 = (int) this.PageHeaderElement.TitleElement.ResetValue(RadElement.VisibilityProperty, ValueResetFlags.Local);
        }
        if (this.PageHeaderElement.SetDefaultHeaderVisibility)
        {
          this.PageHeaderElement.HeaderElement.Visibility = this.PageHeaderElement.DefaultHeaderVisibility;
        }
        else
        {
          int num2 = (int) this.PageHeaderElement.HeaderElement.ResetValue(RadElement.VisibilityProperty, ValueResetFlags.Local);
        }
        this.pageHeaderCustomized = false;
      }
    }

    private void UpdatePageHeaderIconElement(WizardPage page)
    {
      if (page == null)
        return;
      if (page.CustomizePageHeader)
        this.PageHeaderElement.IconElement.Image = page.Icon;
      else
        this.PageHeaderElement.IconElement.Image = this.PageHeaderIcon;
    }

    private void UpdateCommandButtonsStatus(WizardPage wizardPage)
    {
      if (wizardPage == null)
      {
        this.CollapseButtons();
      }
      else
      {
        int num1 = (int) this.BackButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num2 = (int) this.CancelButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num3 = (int) this.HelpButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        int num4 = this.Pages.IndexOf(wizardPage);
        if (wizardPage is WizardWelcomePage)
        {
          if (this.BackButton != null)
            this.BackButton.Enabled = false;
          if (this.NextButton != null)
          {
            int num5 = (int) this.NextButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
            this.NextButton.Enabled = num4 < this.Pages.Count - 1;
          }
          if (this.FinishButton == null)
            return;
          int num6 = (int) this.FinishButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
        }
        else if (wizardPage is WizardCompletionPage)
        {
          if (this.BackButton != null)
            this.BackButton.Enabled = num4 > 0;
          if (this.NextButton != null)
          {
            int num5 = (int) this.NextButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
          }
          if (this.FinishButton == null)
            return;
          int num6 = (int) this.FinishButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
        }
        else
        {
          if (this.BackButton != null)
            this.BackButton.Enabled = num4 > 0;
          if (this.NextButton != null)
          {
            int num5 = (int) this.NextButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
            this.NextButton.Enabled = num4 < this.Pages.Count - 1;
          }
          if (this.FinishButton == null)
            return;
          int num6 = (int) this.FinishButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
        }
      }
    }

    private void CollapseButtons()
    {
      int num1 = (int) this.BackButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      int num2 = (int) this.NextButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      int num3 = (int) this.FinishButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      int num4 = (int) this.CancelButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      int num5 = (int) this.HelpButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
    }

    private void SetViewProperties(WizardView previousView)
    {
      this.WelcomeImage = previousView.WelcomeImage;
      this.HideWelcomeImage = previousView.HideWelcomeImage;
      this.CompletionImage = previousView.CompletionImage;
      this.HideCompletionImage = previousView.HideCompletionImage;
    }

    private void FocusCommandButton()
    {
      WizardCommandAreaButtonElement backButton = this.BackButton as WizardCommandAreaButtonElement;
      if (backButton != null)
        backButton.IsFocusedWizardButton = false;
      this.NextButton.IsFocusedWizardButton = false;
      this.CancelButton.IsFocusedWizardButton = false;
      this.FinishButton.IsFocusedWizardButton = false;
      if (this.NextButton.Visibility == ElementVisibility.Visible && this.NextButton.Enabled)
      {
        this.NextButton.Focus();
        this.NextButton.IsFocusedWizardButton = true;
      }
      else if (this.FinishButton.Visibility == ElementVisibility.Visible && this.FinishButton.Enabled)
      {
        this.FinishButton.Focus();
        this.FinishButton.IsFocusedWizardButton = true;
      }
      else if (backButton != null && backButton.Visibility == ElementVisibility.Visible && backButton.Enabled)
      {
        this.BackButton.Focus();
        backButton.IsFocusedWizardButton = true;
      }
      else
      {
        if (this.CancelButton.Visibility != ElementVisibility.Visible || !this.CancelButton.Enabled)
          return;
        this.CancelButton.Focus();
        this.CancelButton.IsFocusedWizardButton = true;
      }
    }

    public bool HitTestButtons(Point controlClient)
    {
      return this.NextButton != null && this.NextButton.ControlBoundingRectangle.Contains(controlClient) || this.BackButton != null && this.BackButton.ControlBoundingRectangle.Contains(controlClient) || this.view != null && this.view.ControlBoundingRectangle.Contains(controlClient);
    }

    private void Pages_CollectionChanging(object sender, NotifyCollectionChangingEventArgs e)
    {
      if (e.Action != NotifyCollectionChangedAction.Remove || e.OldItems == null || (e.OldItems.Count <= 0 || e.OldItems[0] != this.selectedPage))
        return;
      WizardPage selectedPage = this.selectedPage;
      if (this.Pages.Count > 1)
      {
        if (selectedPage == this.Pages[this.Pages.Count - 1])
          this.SelectPreviousPage();
        else
          this.SelectNextPage();
      }
      else
        this.SelectedPage = (WizardPage) null;
    }

    private void Pages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null && e.NewItems.Count > 0)
      {
        WizardPage newItem = e.NewItems[0] as WizardPage;
        if (this.selectedPage == null)
          this.SelectedPage = newItem;
        if (newItem is WizardWelcomePage && this.welcomePage == null)
          this.welcomePage = newItem as WizardWelcomePage;
        if (newItem is WizardCompletionPage && this.completionPage == null)
          this.completionPage = newItem as WizardCompletionPage;
      }
      else if (e.Action == NotifyCollectionChangedAction.Remove && e.NewItems != null && e.NewItems.Count > 0)
      {
        WizardPage newItem = e.NewItems[0] as WizardPage;
        if (this.welcomePage == newItem)
          this.welcomePage = (WizardWelcomePage) null;
        else if (this.completionPage == newItem)
          this.completionPage = (WizardCompletionPage) null;
      }
      this.UpdateView(this.selectedPage);
      this.FocusCommandButton();
    }

    private void RadWizardLocalizationProvider_CurrentProviderChanged(object sender, EventArgs e)
    {
      this.CommandArea.UpdateButtonsText();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      if (e.Property == RadWizardElement.EnableAeroStyleProperty && this.Mode == WizardMode.Aero)
      {
        if ((bool) e.NewValue)
        {
          this.PageHeaderElement.UnapplyThemeStyles();
          this.PageHeaderElement.Owner.ApplyAeroStyle();
        }
        else
          this.PageHeaderElement.ApplyThemeStyles();
      }
      base.OnPropertyChanged(e);
    }

    public event ModeChangingEventHandler ModeChanging;

    protected virtual void OnModeChanging(ModeChangingEventArgs e)
    {
      if (this.ModeChanging == null)
        return;
      this.ModeChanging((object) this, e);
    }

    public event ModeChangedEventHandler ModeChanged;

    protected virtual void OnModeChanged(ModeChangedEventArgs e)
    {
      if (this.ModeChanged == null)
        return;
      this.ModeChanged((object) this, e);
    }

    public event WizardCancelEventHandler Next;

    protected virtual void OnNext(WizardCancelEventArgs e)
    {
      if (this.Next == null)
        return;
      this.Next((object) this, e);
    }

    public event WizardCancelEventHandler Previous;

    protected virtual void OnPrevious(WizardCancelEventArgs e)
    {
      if (this.Previous == null)
        return;
      this.Previous((object) this, e);
    }

    public event SelectedPageChangingEventHandler SelectedPageChanging;

    protected virtual void OnSelectedPageChanging(object sender, SelectedPageChangingEventArgs e)
    {
      if (this.SelectedPageChanging == null)
        return;
      this.SelectedPageChanging((object) this, e);
    }

    public event SelectedPageChangedEventHandler SelectedPageChanged;

    protected virtual void OnSelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
    {
      if (this.SelectedPageChanged == null)
        return;
      this.SelectedPageChanged((object) this, e);
    }
  }
}
