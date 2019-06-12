// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadWizard
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.Licensing;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadWizardDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ToolboxItem(true)]
  public class RadWizard : RadControl
  {
    private RadWizardElement wizardElement;
    private Form parentForm;

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.UnWireEvents();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.wizardElement = new RadWizardElement();
      this.wizardElement.OwnerControl = this;
      this.WireEvents();
      parent.Children.Add((RadElement) this.wizardElement);
    }

    protected internal virtual void WireEvents()
    {
      if (this.wizardElement == null || this.wizardElement.View == null)
        return;
      this.wizardElement.ModeChanging += new ModeChangingEventHandler(this.wizardElement_ModeChanging);
      this.wizardElement.ModeChanged += new ModeChangedEventHandler(this.wizardElement_ModeChanged);
      this.wizardElement.Next += new WizardCancelEventHandler(this.wizardElement_Next);
      this.wizardElement.Previous += new WizardCancelEventHandler(this.wizardElement_Previous);
      this.wizardElement.FinishButton.Click += new EventHandler(this.FinishButton_Click);
      this.wizardElement.CancelButton.Click += new EventHandler(this.CancelButton_Click);
      this.wizardElement.HelpButton.Click += new EventHandler(this.HelpButton_Click);
      this.wizardElement.SelectedPageChanging += new SelectedPageChangingEventHandler(this.wizardElement_SelectedPageChanging);
      this.wizardElement.SelectedPageChanged += new SelectedPageChangedEventHandler(this.wizardElement_SelectedPageChanged);
      this.ParentChanged += new EventHandler(this.RadWizard_ParentChanged);
    }

    private void RadWizard_ParentChanged(object sender, EventArgs e)
    {
      if (this.Parent == null)
        return;
      this.parentForm = this.FindForm();
    }

    protected internal virtual void UnWireEvents()
    {
      if (this.wizardElement == null || this.wizardElement.View == null)
        return;
      this.wizardElement.ModeChanging -= new ModeChangingEventHandler(this.wizardElement_ModeChanging);
      this.wizardElement.ModeChanged -= new ModeChangedEventHandler(this.wizardElement_ModeChanged);
      this.wizardElement.Next -= new WizardCancelEventHandler(this.wizardElement_Next);
      this.wizardElement.Previous -= new WizardCancelEventHandler(this.wizardElement_Previous);
      this.wizardElement.FinishButton.Click -= new EventHandler(this.FinishButton_Click);
      this.wizardElement.CancelButton.Click -= new EventHandler(this.CancelButton_Click);
      this.wizardElement.HelpButton.Click -= new EventHandler(this.HelpButton_Click);
      this.wizardElement.SelectedPageChanging -= new SelectedPageChangingEventHandler(this.wizardElement_SelectedPageChanging);
      this.wizardElement.SelectedPageChanged -= new SelectedPageChangedEventHandler(this.wizardElement_SelectedPageChanged);
      this.ParentChanged -= new EventHandler(this.RadWizard_ParentChanged);
    }

    [Browsable(false)]
    public RadWizardElement WizardElement
    {
      get
      {
        return this.wizardElement;
      }
    }

    [Description("The mode of RadWizard.")]
    [Browsable(true)]
    [DefaultValue(WizardMode.Wizard97)]
    public WizardMode Mode
    {
      get
      {
        if (this.wizardElement == null)
          return WizardMode.Wizard97;
        return this.WizardElement.Mode;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.Mode = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AutoSize
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [DefaultValue(true)]
    [Description("Indication wether the Aero style should apply when the control is in Wizard Aero mode.")]
    public bool EnableAeroStyle
    {
      get
      {
        if (this.wizardElement == null)
          return false;
        return this.WizardElement.EnableAeroStyle;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.EnableAeroStyle = value;
      }
    }

    [Browsable(true)]
    [Description("The pages collection of RadWizad.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public WizardPageCollection Pages
    {
      get
      {
        if (this.wizardElement == null)
          return (WizardPageCollection) null;
        return this.wizardElement.Pages;
      }
    }

    [Description("The Welcome page of RadWizard.")]
    public WizardWelcomePage WelcomePage
    {
      get
      {
        if (this.wizardElement == null)
          return (WizardWelcomePage) null;
        return this.wizardElement.WelcomePage;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.WelcomePage = value;
      }
    }

    [Description("The Completion page of RadWizad.")]
    public WizardCompletionPage CompletionPage
    {
      get
      {
        if (this.wizardElement == null)
          return (WizardCompletionPage) null;
        return this.wizardElement.CompletionPage;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.CompletionPage = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WizardPage SelectedPage
    {
      get
      {
        if (this.wizardElement == null)
          return (WizardPage) null;
        return this.wizardElement.SelectedPage;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.SelectedPage = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WizardCommandArea CommandArea
    {
      get
      {
        if (this.wizardElement == null)
          return (WizardCommandArea) null;
        return this.wizardElement.CommandArea;
      }
    }

    [Description("The height of the command area. Negative value makes the command area autosize.")]
    [DefaultValue(-1f)]
    public float CommandAreaHeight
    {
      get
      {
        if (this.wizardElement == null)
          return -1f;
        return this.wizardElement.CommandAreaHeight;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.CommandAreaHeight = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public WizardPageHeaderElement PageHeaderElement
    {
      get
      {
        if (this.wizardElement == null)
          return (WizardPageHeaderElement) null;
        return this.wizardElement.PageHeaderElement;
      }
    }

    [DefaultValue(-1f)]
    [Description("The height of the page header. Negative value makes the page header autosize.")]
    public float PageHeaderHeight
    {
      get
      {
        if (this.wizardElement == null)
          return -1f;
        return this.wizardElement.PageHeaderHeight;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.PageHeaderHeight = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LightVisualElement WelcomeImageElement
    {
      get
      {
        if (this.wizardElement == null)
          return (LightVisualElement) null;
        return this.wizardElement.WelcomeImageElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public LightVisualElement CompletionImageElement
    {
      get
      {
        if (this.wizardElement == null)
          return (LightVisualElement) null;
        return this.wizardElement.CompletionImageElement;
      }
    }

    [Description("The image of welcome pages.")]
    [DefaultValue(null)]
    public Image WelcomeImage
    {
      get
      {
        if (this.wizardElement == null)
          return (Image) null;
        return this.wizardElement.WelcomeImage;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.WelcomeImage = value;
      }
    }

    [DefaultValue(false)]
    [Description("Indicates whether the image of the welcome pages should be visible.")]
    public bool HideWelcomeImage
    {
      get
      {
        if (this.wizardElement == null)
          return false;
        return this.wizardElement.HideWelcomeImage;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.HideWelcomeImage = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ImageLayout WelcomeImageLayout
    {
      get
      {
        if (this.wizardElement == null)
          return ImageLayout.None;
        return this.wizardElement.WelcomeImageLayout;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.WelcomeImageLayout = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadImageShape WelcomeImageBackgroundShape
    {
      get
      {
        if (this.wizardElement == null)
          return (RadImageShape) null;
        return this.wizardElement.WelcomeImageBackgroundShape;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.WelcomeImageBackgroundShape = value;
      }
    }

    [DefaultValue(null)]
    [Description("The image of completion pages.")]
    public Image CompletionImage
    {
      get
      {
        if (this.wizardElement == null)
          return (Image) null;
        return this.wizardElement.CompletionImage;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.CompletionImage = value;
      }
    }

    [DefaultValue(false)]
    [Description("Indicates whether the image of the completion pages should be visible.")]
    public bool HideCompletionImage
    {
      get
      {
        if (this.wizardElement == null)
          return false;
        return this.wizardElement.HideCompletionImage;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.HideCompletionImage = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ImageLayout CompletionImageLayout
    {
      get
      {
        if (this.wizardElement == null)
          return ImageLayout.None;
        return this.wizardElement.CompletionImageLayout;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.CompletionImageLayout = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadImageShape CompletionImageBackgroundShape
    {
      get
      {
        if (this.wizardElement == null)
          return (RadImageShape) null;
        return this.wizardElement.CompletionImageBackgroundShape;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.CompletionImageBackgroundShape = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ElementVisibility PageTitleTextVisibility
    {
      get
      {
        if (this.wizardElement == null)
          return ElementVisibility.Hidden;
        return this.wizardElement.PageTitleTextVisibility;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.PageTitleTextVisibility = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ElementVisibility PageHeaderTextVisibility
    {
      get
      {
        if (this.wizardElement == null)
          return ElementVisibility.Hidden;
        return this.wizardElement.PageHeaderTextVisibility;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.PageHeaderTextVisibility = value;
      }
    }

    [Description("The icon of the page header.")]
    public Image PageHeaderIcon
    {
      get
      {
        if (this.wizardElement == null)
          return (Image) null;
        return this.wizardElement.PageHeaderIcon;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.PageHeaderIcon = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ContentAlignment PageHeaderIconAlignment
    {
      get
      {
        if (this.wizardElement == null)
          return ContentAlignment.MiddleCenter;
        return this.wizardElement.PageHeaderIconAlignment;
      }
      set
      {
        if (this.wizardElement == null)
          return;
        this.wizardElement.PageHeaderIconAlignment = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadButtonElement BackButton
    {
      get
      {
        if (this.wizardElement == null)
          return (RadButtonElement) null;
        return this.wizardElement.BackButton;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement NextButton
    {
      get
      {
        if (this.wizardElement == null)
          return (RadButtonElement) null;
        return (RadButtonElement) this.wizardElement.NextButton;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement CancelButton
    {
      get
      {
        if (this.wizardElement == null)
          return (RadButtonElement) null;
        return (RadButtonElement) this.wizardElement.CancelButton;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement FinishButton
    {
      get
      {
        if (this.wizardElement == null)
          return (RadButtonElement) null;
        return (RadButtonElement) this.wizardElement.FinishButton;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public LightVisualElement HelpButton
    {
      get
      {
        if (this.wizardElement == null)
          return (LightVisualElement) null;
        return this.wizardElement.HelpButton;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(600, 400));
      }
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadWizardAccessibleObject(this);
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    internal void ApplyAeroStyle()
    {
      if (!DWMAPI.IsCompositionEnabled || this.IsDesignMode || this.parentForm == null)
        return;
      RadForm parentForm = this.parentForm as RadForm;
      if (parentForm != null)
      {
        parentForm.AllowTheming = false;
        parentForm.RootElement.BackColor = Color.Transparent;
      }
      else
        this.parentForm.BackColor = Color.Black;
      this.RootElement.BackColor = Color.Transparent;
      DWMAPI.DwmExtendFrameIntoClientArea(this.parentForm.Handle, ref new Telerik.WinControls.NativeMethods.MARGINS()
      {
        cxLeftWidth = 0,
        cxRightWidth = 0,
        cyTopHeight = this.Dock != DockStyle.Fill || this.Pages.Count == 0 ? this.parentForm.Size.Height : this.PageHeaderElement.Size.Height,
        cyBottomHeight = 0
      });
      this.parentForm.Refresh();
    }

    internal void UnapplyAeroStyle()
    {
      if (!DWMAPI.IsCompositionEnabled || this.IsDesignMode || this.parentForm == null)
        return;
      RadForm parentForm = this.parentForm as RadForm;
      if (parentForm != null)
      {
        parentForm.AllowTheming = true;
        int num = (int) parentForm.RootElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Local);
      }
      DWMAPI.DwmExtendFrameIntoClientArea(this.parentForm.Handle, ref new Telerik.WinControls.NativeMethods.MARGINS()
      {
        cxLeftWidth = 0,
        cxRightWidth = 0,
        cyTopHeight = 0,
        cyBottomHeight = 0
      });
      this.parentForm.Refresh();
    }

    protected virtual bool GiveFocusToNavigationButtonsShiftTab()
    {
      Form form = this.FindForm();
      Control ctl = form.ActiveControl;
      if (ctl is ContainerControl)
        ctl = ((ContainerControl) ctl).ActiveControl;
      if (ctl == this && !this.wizardElement.View.IsFirstNavigationButtonFocused())
        return true;
      do
      {
        ctl = ctl == this ? ctl.GetNextControl(ctl, false) : form.GetNextControl(ctl, false);
        if (ctl == this)
          return true;
        if (ctl != null && ctl.CanFocus && (ctl.CanSelect && ctl.TabStop))
          return false;
      }
      while (ctl != null);
      return true;
    }

    protected virtual bool GiveFocusToNavigationButtonsLeft()
    {
      Form form = this.FindForm();
      Control ctl = form.ActiveControl;
      Control parent = ctl.Parent;
      if (parent != this && ctl != this)
        return false;
      if (this.wizardElement.CommandArea.SelectedButtonIndex() != -1 && !this.wizardElement.View.IsFirstNavigationButtonFocused() || this.Mode == WizardMode.Aero && this.BackButton.IsFocused)
        return true;
      do
      {
        ctl = form.GetNextControl(ctl, false);
        if (ctl == null)
          return true;
      }
      while ((ctl == null || !ctl.CanFocus || (!ctl.CanSelect || ctl.Parent != parent)) && ctl != null);
      return false;
    }

    protected virtual bool GiveFocusToNavigationButtonsRight()
    {
      Form form = this.FindForm();
      Control ctl = form.ActiveControl;
      Control parent = ctl.Parent;
      if (parent != this && ctl != this)
        return false;
      if (this.wizardElement.CommandArea.SelectedButtonIndex() != -1 && !this.wizardElement.View.IsLastNavigationButtonFocused())
        return true;
      do
      {
        ctl = form.GetNextControl(ctl, true);
        if (ctl == null)
          return true;
      }
      while ((ctl == null || !ctl.CanFocus || (!ctl.CanSelect || ctl.Parent != parent)) && (ctl != null && ctl.Parent == parent));
      return false;
    }

    protected virtual bool GiveFocusToNavigationButtonsTab()
    {
      Form form = this.FindForm();
      Control ctl = form.ActiveControl;
      if (ctl is ContainerControl)
        ctl = ((ContainerControl) ctl).ActiveControl;
      if (ctl == this && !this.wizardElement.View.IsLastNavigationButtonFocused())
        return true;
      do
      {
        ctl = form.GetNextControl(ctl, true);
        if (ctl != null && ctl.CanFocus && (ctl.CanSelect && ctl.TabStop))
          return false;
      }
      while (ctl != null);
      return true;
    }

    protected virtual bool SelectLastWizardControl()
    {
      Form form = this.FindForm();
      Control ctl = form.ActiveControl;
      int index = this.CommandArea.SelectedButtonIndex();
      do
      {
        ctl = ctl == this ? form.GetNextControl((Control) null, false) : form.GetNextControl(ctl, false);
        if (ctl == this)
        {
          this.WizardElement.View.SelectPreviousNavigationButton();
          return true;
        }
        if (ctl != null && ctl.CanFocus && (ctl.CanSelect && ctl.Parent == this))
        {
          this.CommandArea.NavigationButtons[index].IsFocusedWizardButton = false;
          ctl.Focus();
          return true;
        }
      }
      while (ctl != null);
      return false;
    }

    protected virtual void SelectFirstWizardControl()
    {
      Form form = this.FindForm();
      Control ctl = form.ActiveControl;
      int index = this.CommandArea.SelectedButtonIndex();
      do
      {
        ctl = form.GetNextControl(ctl, true);
        if (ctl != null && ctl.CanFocus && (ctl.CanSelect && ctl.Parent == this))
        {
          if (index != -1)
            this.CommandArea.NavigationButtons[index].IsFocusedWizardButton = false;
          ctl.Focus();
          return;
        }
      }
      while (ctl != null);
      this.WizardElement.View.SelectFollowingNavigationButton();
    }

    public void SelectNextPage()
    {
      if (this.wizardElement == null)
        return;
      this.wizardElement.SelectNextPage();
    }

    public void SelectPreviousPage()
    {
      if (this.wizardElement == null)
        return;
      this.wizardElement.SelectPreviousPage();
    }

    public event ModeChangingEventHandler ModeChanging;

    protected virtual void OnModeChanging(ModeChangingEventArgs e)
    {
      if (this.ModeChanging == null)
        return;
      this.ModeChanging((object) this, e);
    }

    private void wizardElement_ModeChanging(object sender, ModeChangingEventArgs e)
    {
      this.OnModeChanging(e);
    }

    public event ModeChangedEventHandler ModeChanged;

    protected virtual void OnModeChanged(ModeChangedEventArgs e)
    {
      if (this.ModeChanged == null)
        return;
      this.ModeChanged((object) this, e);
    }

    private void wizardElement_ModeChanged(object sender, ModeChangedEventArgs e)
    {
      this.OnModeChanged(e);
    }

    public event WizardCancelEventHandler Next;

    protected virtual void OnNext(WizardCancelEventArgs e)
    {
      if (this.Next == null)
        return;
      this.Next((object) this, e);
    }

    private void wizardElement_Next(object sender, WizardCancelEventArgs e)
    {
      this.OnNext(e);
    }

    public event WizardCancelEventHandler Previous;

    protected virtual void OnPrevious(WizardCancelEventArgs e)
    {
      if (this.Previous == null)
        return;
      this.Previous((object) this, e);
    }

    private void wizardElement_Previous(object sender, WizardCancelEventArgs e)
    {
      this.OnPrevious(e);
    }

    public event EventHandler Finish;

    protected virtual void OnFinish(EventArgs e)
    {
      if (this.Finish == null)
        return;
      this.Finish((object) this, e);
    }

    private void FinishButton_Click(object sender, EventArgs e)
    {
      this.OnFinish(e);
    }

    public event EventHandler Cancel;

    protected virtual void OnCancel(EventArgs e)
    {
      if (this.Cancel == null)
        return;
      this.Cancel((object) this, e);
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
      this.OnCancel(e);
    }

    public event EventHandler Help;

    protected virtual void OnHelp(EventArgs e)
    {
      if (this.Help == null)
        return;
      this.Help((object) this, e);
    }

    private void HelpButton_Click(object sender, EventArgs e)
    {
      this.OnHelp(e);
    }

    public event SelectedPageChangingEventHandler SelectedPageChanging;

    protected virtual void OnSelectedPageChanging(object sender, SelectedPageChangingEventArgs e)
    {
      if (this.SelectedPageChanging == null)
        return;
      this.SelectedPageChanging((object) this, e);
    }

    private void wizardElement_SelectedPageChanging(object sender, SelectedPageChangingEventArgs e)
    {
      this.OnSelectedPageChanging((object) this, e);
    }

    public event SelectedPageChangedEventHandler SelectedPageChanged;

    protected virtual void OnSelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
    {
      if (this.SelectedPageChanged == null)
        return;
      this.SelectedPageChanged((object) this, e);
    }

    private void wizardElement_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
    {
      this.OnSelectedPageChanged((object) this, e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      Form form = this.FindForm();
      if (form == null || !form.ContainsFocus)
        return;
      this.CommandArea.ResetButtonsFocus();
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      base.ScaleControl(factor, specified);
      foreach (WizardPage page in (Collection<WizardPage>) this.Pages)
      {
        page.ContentArea.Scale(new SizeF(1f / factor.Width, 1f / factor.Height));
        foreach (Control control in (ArrangedElementCollection) page.ContentArea.Controls)
          control.Scale(factor);
      }
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Tab && this.GiveFocusToNavigationButtonsTab() && this.wizardElement.View.SelectFollowingNavigationButton() || keyData == (Keys.Tab | Keys.Shift) && this.GiveFocusToNavigationButtonsShiftTab() && this.wizardElement.View.SelectPreviousNavigationButton())
        return false;
      if (keyData == Keys.Right || keyData == Keys.Down)
      {
        if (this.wizardElement.View.IsLastNavigationButtonFocused())
        {
          this.SelectFirstWizardControl();
          return false;
        }
        if (this.GiveFocusToNavigationButtonsRight() && this.wizardElement.View.SelectFollowingNavigationButton())
          return false;
      }
      if ((keyData == Keys.Left || keyData == Keys.Up) && (this.wizardElement.View.IsFirstNavigationButtonFocused() && this.SelectLastWizardControl() || this.GiveFocusToNavigationButtonsLeft() && this.wizardElement.View.SelectPreviousNavigationButton()))
        return false;
      Keys keys = keyData & Keys.KeyCode;
      if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None && keys == Keys.Return)
      {
        Form form = this.FindForm();
        RadButtonElement acceptButton = form.AcceptButton as RadButtonElement;
        if (acceptButton != null && acceptButton.ElementTree.Control == this)
        {
          form.AcceptButton.PerformClick();
          return true;
        }
      }
      return base.ProcessDialogKey(keyData);
    }
  }
}
