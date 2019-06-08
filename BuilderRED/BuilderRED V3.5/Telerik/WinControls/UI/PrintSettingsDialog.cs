// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PrintSettingsDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class PrintSettingsDialog : RadForm
  {
    private bool showPreviewButton = true;
    private const float InchToCentimeters = 2.54f;
    private bool isMetric;
    private RadPrintDocument document;
    private RadTextBox lastFocusedTextBox;
    private IContainer components;
    protected RadPageViewPage pageFormat;
    protected RadPageViewPage pagePaper;
    protected RadPageViewPage pageHeaderFooter;
    protected RadButton buttonOK;
    protected RadButton buttonCancel;
    protected RadCommandBar commandBarSnippets;
    protected CommandBarRowElement commandBarRowElement1;
    protected CommandBarStripElement commandBarStripElement1;
    protected CommandBarButton buttonPageNum;
    protected CommandBarButton buttonTotalPages;
    protected CommandBarButton buttonCurDate;
    protected CommandBarButton buttonCurTime;
    protected CommandBarButton buttonUser;
    protected CommandBarButton buttonLogo;
    protected RadCheckBox checkBoxReverseHeader;
    protected RadCheckBox checkBoxReverseFooter;
    protected RadTextBox textBoxHeaderLeftText;
    protected RadTextBox textBoxHeaderCenterText;
    protected RadTextBox textBoxHeaderRightText;
    protected RadTextBox textBoxFooterRightText;
    protected RadTextBox textBoxFooterCenterText;
    protected RadTextBox textBoxFooterLeftText;
    protected RadGroupBox groupBoxPage;
    protected RadGroupBox groupBoxMargins;
    protected RadGroupBox groupBoxOrientation;
    protected RadLabel labelType;
    protected RadLabel labelPageSource;
    protected RadListControl listBoxPaperType;
    protected RadDropDownList dropDownPageSource;
    protected RadLabel labelTopMargin;
    protected RadLabel labelLeftMargin;
    protected RadLabel labelBottomMargin;
    protected RadLabel labelRightMargin;
    protected RadRadioButton radioButtonPortrait;
    protected RadRadioButton radioButtonLandscape;
    protected RadPageView radPageView1;
    protected RadMaskedEditBox maskBoxTopMargin;
    protected RadMaskedEditBox maskBoxBottomMargin;
    protected RadMaskedEditBox maskBoxLeftMargin;
    protected RadMaskedEditBox maskBoxRightMargin;
    protected RadLabel labelHeader;
    protected RadLabel labelFooter;
    protected RadButton buttonPrint;
    protected PictureBox pictureBoxOrientation;
    private RadBrowseEditor radBrowseEditorHeaderFont;
    private RadBrowseEditor radBrowseEditorFooterFont;

    public PrintSettingsDialog()
    {
      this.InitializeComponent();
      this.isMetric = RegionInfo.CurrentRegion.IsMetric;
      this.listBoxPaperType.ItemHeight = RadControl.GetDpiScaledSize(new Size(0, this.listBoxPaperType.ItemHeight)).Height;
      this.buttonPageNum.MouseUp += new MouseEventHandler(this.buttonPageNum_MouseUp);
      this.buttonTotalPages.MouseUp += new MouseEventHandler(this.buttonTotalPages_MouseUp);
      this.buttonCurDate.MouseUp += new MouseEventHandler(this.buttonCurDate_MouseUp);
      this.buttonCurTime.MouseUp += new MouseEventHandler(this.buttonCurTime_MouseUp);
      this.buttonUser.MouseUp += new MouseEventHandler(this.buttonUser_MouseUp);
      this.buttonLogo.MouseUp += new MouseEventHandler(this.buttonLogo_MouseUp);
      this.lastFocusedTextBox = this.textBoxHeaderLeftText;
      this.commandBarSnippets.CommandBarElement.DrawBorder = false;
      Control formatControl = this.CreateFormatControl();
      if (formatControl != null)
        this.pageFormat.Controls.Add(formatControl);
      if (this.document != null)
        this.ShowPreviewButton = this.document.Site == null;
      this.LocalizeStrings();
      LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.PrintingDialogsLocalizationProvider_CurrentProviderChanged);
      this.ThemeNameChanged += new ThemeNameChangedEventHandler(this.PrintSettingsDialog_ThemeNameChanged);
    }

    public PrintSettingsDialog(RadPrintDocument document)
      : this()
    {
      this.PrintDocument = document;
    }

    public RadPageView PageView
    {
      get
      {
        return this.radPageView1;
      }
    }

    public RadPrintDocument PrintDocument
    {
      get
      {
        return this.document;
      }
      set
      {
        if (this.document == value)
          return;
        this.document = value;
        this.ShowPreviewButton = this.document.Site == null;
        this.LoadSettings();
      }
    }

    public bool ShowPreviewButton
    {
      get
      {
        return this.showPreviewButton;
      }
      set
      {
        this.showPreviewButton = value;
      }
    }

    protected virtual void LocalizeStrings()
    {
      PrintDialogsLocalizationProvider currentProvider = LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProvider;
      this.Text = currentProvider.GetLocalizedString("SettingDialogTitle");
      this.buttonPrint.Text = currentProvider.GetLocalizedString("SettingDialogButtonPrint");
      this.buttonOK.Text = currentProvider.GetLocalizedString("SettingDialogButtonPreview");
      this.buttonCancel.Text = currentProvider.GetLocalizedString("SettingDialogButtonCancel");
      this.pageFormat.Text = currentProvider.GetLocalizedString("SettingDialogPageFormat");
      this.pagePaper.Text = currentProvider.GetLocalizedString("SettingDialogPagePaper");
      this.pageHeaderFooter.Text = currentProvider.GetLocalizedString("SettingDialogPageHeaderFooter");
      this.labelHeader.Text = currentProvider.GetLocalizedString("SettingDialogLabelHeader");
      this.labelFooter.Text = currentProvider.GetLocalizedString("SettingDialogLabelFooter");
      this.checkBoxReverseFooter.Text = this.checkBoxReverseHeader.Text = currentProvider.GetLocalizedString("SettingDialogCheckboxReverse");
      this.buttonPageNum.ToolTipText = currentProvider.GetLocalizedString("SettingDialogButtonPageNumber");
      this.buttonTotalPages.ToolTipText = currentProvider.GetLocalizedString("SettingDialogButtonTotalPages");
      this.buttonCurDate.ToolTipText = currentProvider.GetLocalizedString("SettingDialogButtonCurrentDate");
      this.buttonCurTime.ToolTipText = currentProvider.GetLocalizedString("SettingDialogButtonCurrentTime");
      this.buttonUser.ToolTipText = currentProvider.GetLocalizedString("SettingDialogButtonUserName");
      this.buttonLogo.ToolTipText = currentProvider.GetLocalizedString("SettingDialogButtonLogo");
      this.groupBoxPage.HeaderText = currentProvider.GetLocalizedString("SettingDialogLabelPage");
      this.groupBoxOrientation.HeaderText = currentProvider.GetLocalizedString("SettingDialogLabelOrientation");
      this.groupBoxMargins.HeaderText = !this.isMetric ? currentProvider.GetLocalizedString("SettingDialogLabelMargins") : currentProvider.GetLocalizedString("SettingDialogLabelMarginsMetric");
      this.labelType.Text = currentProvider.GetLocalizedString("SettingDialogLabelType");
      this.labelPageSource.Text = currentProvider.GetLocalizedString("SettingDialogLabelPageSource");
      this.labelTopMargin.Text = currentProvider.GetLocalizedString("SettingDialogLabelTop");
      this.labelLeftMargin.Text = currentProvider.GetLocalizedString("SettingDialogLabelLeft");
      this.labelBottomMargin.Text = currentProvider.GetLocalizedString("SettingDialogLabelBottom");
      this.labelRightMargin.Text = currentProvider.GetLocalizedString("SettingDialogLabelRight");
      this.radioButtonLandscape.Text = currentProvider.GetLocalizedString("SettingDialogRadioLandscape");
      this.radioButtonPortrait.Text = currentProvider.GetLocalizedString("SettingDialogRadioPortrait");
    }

    protected virtual void UnwireEvents()
    {
      LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.PrintingDialogsLocalizationProvider_CurrentProviderChanged);
    }

    private void InsertText(string text)
    {
      if (this.lastFocusedTextBox == null)
        return;
      int selectionStart = this.lastFocusedTextBox.SelectionStart;
      this.lastFocusedTextBox.Text = this.lastFocusedTextBox.Text.Insert(this.lastFocusedTextBox.SelectionStart, text);
      this.lastFocusedTextBox.SelectionStart = selectionStart + text.Length;
      this.lastFocusedTextBox.SelectionLength = 0;
      this.lastFocusedTextBox.Focus();
    }

    protected virtual void LoadSettings()
    {
      if (this.document == null)
        return;
      if (!this.ShowPreviewButton)
      {
        this.buttonPrint.Visible = false;
        this.AcceptButton = (IButtonControl) this.buttonOK;
        this.buttonOK.Text = LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProvider.GetLocalizedString("SettingDialogButtonOK");
      }
      else
      {
        this.buttonPrint.Visible = true;
        this.AcceptButton = (IButtonControl) this.buttonPrint;
        this.buttonOK.Text = LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProvider.GetLocalizedString("SettingDialogButtonPreview");
      }
      FontConverter fontConverter = new FontConverter();
      this.radBrowseEditorHeaderFont.Value = fontConverter.ConvertToString((object) this.document.HeaderFont);
      this.radBrowseEditorFooterFont.Value = fontConverter.ConvertToString((object) this.document.FooterFont);
      this.checkBoxReverseHeader.Checked = this.document.ReverseHeaderOnEvenPages;
      this.checkBoxReverseFooter.Checked = this.document.ReverseFooterOnEvenPages;
      this.textBoxHeaderLeftText.Text = this.document.LeftHeader;
      this.textBoxHeaderCenterText.Text = this.document.MiddleHeader;
      this.textBoxHeaderRightText.Text = this.document.RightHeader;
      this.textBoxFooterLeftText.Text = this.document.LeftFooter;
      this.textBoxFooterCenterText.Text = this.document.MiddleFooter;
      this.textBoxFooterRightText.Text = this.document.RightFooter;
      this.radioButtonLandscape.IsChecked = this.document.Landscape;
      this.LoadDocumentMargins();
      List<PaperSize> paperSizeList = new List<PaperSize>();
      PrinterSettings.PaperSizeCollection paperSizes = this.document.PrinterSettings.PaperSizes;
      int index1 = 0;
      while (index1 < paperSizes.Count)
      {
        if (string.IsNullOrEmpty(paperSizes[index1].PaperName) || paperSizes[index1].PaperName == "-1")
        {
          ++index1;
        }
        else
        {
          paperSizeList.Add(paperSizes[index1]);
          ++index1;
        }
      }
      this.listBoxPaperType.VisualItemFormatting += new VisualListItemFormattingEventHandler(this.listBoxPaperType_VisualItemFormatting);
      this.listBoxPaperType.DisplayMember = "PaperName";
      this.listBoxPaperType.DescriptionTextMember = "Width";
      this.listBoxPaperType.DataSource = (object) paperSizeList;
      this.listBoxPaperType.SelectedItem = this.listBoxPaperType.FindItemExact(this.document.PaperSize != null ? this.document.PaperSize.PaperName.ToString() : this.document.DefaultPageSettings.PaperSize.PaperName.ToString(), false);
      List<PaperSource> paperSourceList = new List<PaperSource>();
      PrinterSettings.PaperSourceCollection paperSources = this.document.PrinterSettings.PaperSources;
      int index2 = 0;
      while (index2 < paperSources.Count)
      {
        if (string.IsNullOrEmpty(paperSources[index2].SourceName) || paperSources[index2].SourceName == "-1" || !Enum.IsDefined(typeof (PaperSourceKind), (object) paperSources[index2].Kind))
        {
          ++index2;
        }
        else
        {
          paperSourceList.Add(paperSources[index2]);
          ++index2;
        }
      }
      this.dropDownPageSource.DataSource = (object) paperSourceList;
      this.dropDownPageSource.DisplayMember = "SourceName";
      this.dropDownPageSource.SelectedItem = this.dropDownPageSource.FindItemExact(this.document.PaperSource != null ? this.document.PaperSource.SourceName.ToString() : this.document.DefaultPageSettings.PaperSource.SourceName.ToString(), false);
    }

    private void LoadDocumentMargins()
    {
      float num1 = this.document.Margins != (Margins) null ? (float) this.document.Margins.Top / 100f : (float) this.document.DefaultPageSettings.Margins.Top / 100f;
      float num2 = this.document.Margins != (Margins) null ? (float) this.document.Margins.Bottom / 100f : (float) this.document.DefaultPageSettings.Margins.Bottom / 100f;
      float num3 = this.document.Margins != (Margins) null ? (float) this.document.Margins.Left / 100f : (float) this.document.DefaultPageSettings.Margins.Left / 100f;
      float num4 = this.document.Margins != (Margins) null ? (float) this.document.Margins.Right / 100f : (float) this.document.DefaultPageSettings.Margins.Right / 100f;
      if (this.isMetric)
      {
        num1 *= 2.54f;
        num2 *= 2.54f;
        num3 *= 2.54f;
        num4 *= 2.54f;
      }
      this.maskBoxTopMargin.Value = (object) num1;
      this.maskBoxBottomMargin.Value = (object) num2;
      this.maskBoxLeftMargin.Value = (object) num3;
      this.maskBoxRightMargin.Value = (object) num4;
    }

    private void listBoxPaperType_VisualItemFormatting(
      object sender,
      VisualItemFormattingEventArgs args)
    {
      PaperSize dataBoundItem = args.VisualItem.Data.DataBoundItem as PaperSize;
      if (dataBoundItem == null)
        return;
      float num1 = (float) dataBoundItem.Width / 100f;
      float num2 = (float) dataBoundItem.Height / 100f;
      string empty = string.Empty;
      string format;
      if (this.isMetric)
      {
        num1 *= 2.54f;
        num2 *= 2.54f;
        format = "{0:F1}cm x {1:F1}cm";
      }
      else
        format = "{0:F1}\" x {1:F1}\"";
      ((DescriptionTextListVisualItem) args.VisualItem).DescriptionContent.Text = string.Format(format, (object) num1, (object) num2);
      ((DescriptionTextListVisualItem) args.VisualItem).Separator.Visibility = ElementVisibility.Collapsed;
    }

    protected virtual void ApplySettings()
    {
      if (this.document == null)
        return;
      FontConverter fontConverter = new FontConverter();
      this.document.HeaderFont = fontConverter.ConvertFromString(this.radBrowseEditorHeaderFont.Value) as Font;
      this.document.LeftHeader = this.textBoxHeaderLeftText.Text;
      this.document.MiddleHeader = this.textBoxHeaderCenterText.Text;
      this.document.RightHeader = this.textBoxHeaderRightText.Text;
      this.document.ReverseHeaderOnEvenPages = this.checkBoxReverseHeader.Checked;
      this.document.FooterFont = fontConverter.ConvertFromString(this.radBrowseEditorFooterFont.Value) as Font;
      this.document.LeftFooter = this.textBoxFooterLeftText.Text;
      this.document.MiddleFooter = this.textBoxFooterCenterText.Text;
      this.document.RightFooter = this.textBoxFooterRightText.Text;
      this.document.ReverseFooterOnEvenPages = this.checkBoxReverseFooter.Checked;
      this.document.Landscape = this.radioButtonLandscape.IsChecked;
      this.ApplyDocumentMargins();
      this.document.PaperSize = this.listBoxPaperType.SelectedValue as PaperSize;
      this.document.PaperSource = this.dropDownPageSource.SelectedValue as PaperSource;
    }

    private void ApplyDocumentMargins()
    {
      float num1 = 100f * float.Parse(this.maskBoxLeftMargin.Value.ToString());
      float num2 = 100f * float.Parse(this.maskBoxTopMargin.Value.ToString());
      float num3 = 100f * float.Parse(this.maskBoxRightMargin.Value.ToString());
      float num4 = 100f * float.Parse(this.maskBoxBottomMargin.Value.ToString());
      if (this.isMetric)
      {
        num1 /= 2.54f;
        num2 /= 2.54f;
        num3 /= 2.54f;
        num4 /= 2.54f;
      }
      this.document.Margins = new Margins((int) num1, (int) num3, (int) num2, (int) num4);
    }

    protected virtual Control CreateFormatControl()
    {
      return (Control) new UserControl();
    }

    private void PrintSettingsDialog_ThemeNameChanged(object source, ThemeNameChangedEventArgs args)
    {
      ThemeResolutionService.ApplyThemeToControlTree((Control) this, args.newThemeName);
    }

    private void PrintingDialogsLocalizationProvider_CurrentProviderChanged(
      object sender,
      EventArgs e)
    {
      this.LocalizeStrings();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.labelHeader.BackColor = this.labelFooter.BackColor = this.checkBoxReverseFooter.BackColor = this.checkBoxReverseHeader.BackColor = Color.Transparent;
      this.groupBoxPage.GroupBoxElement.Header.Fill.BackColor = this.groupBoxMargins.GroupBoxElement.Header.Fill.BackColor = this.groupBoxOrientation.GroupBoxElement.Header.Fill.BackColor = this.pagePaper.BackColor;
      if (this.document != null)
        this.LoadSettings();
      this.radPageView1.SelectedPage = this.pageHeaderFooter;
      if (this.radPageView1.Pages.Contains(this.pageFormat))
        this.radPageView1.SelectedPage = this.pageFormat;
      this.AdjustControlsForTouchThemes();
    }

    private void AdjustControlsForTouchThemes()
    {
      if (this.ThemeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
      {
        Control control = this.pagePaper.Controls["groupBoxMargins"];
        control.Height += 10;
        this.pagePaper.Controls["groupBoxOrientation"].Top += 10;
        this.pagePaper.Controls["groupBoxPage"].Height += 10;
        control.Controls["labelBottomMargin"].Top += 5;
        control.Controls["labelRightMargin"].Top += 5;
        control.Controls["maskBoxRightMargin"].Top += 5;
        control.Controls["maskBoxBottomMargin"].Top += 5;
        control.Controls["maskBoxTopMargin"].Left += 3;
        control.Controls["maskBoxBottomMargin"].Left += 3;
        int num = this.Controls["buttonCancel"].Width - this.Controls["buttonPrint"].Width;
        this.Controls["buttonPrint"].Left += num;
        this.Controls["buttonCancel"].Left += num;
        this.Controls["buttonOK"].Left += num;
        this.Controls["buttonCancel"].Width = this.Controls["buttonPrint"].Width;
      }
      if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
        return;
      this.radPageView1.Location = new Point(0, 0);
      this.radPageView1.Size = new Size(705, 517);
      this.pageFormat.ItemSize = new SizeF(75f, 49f);
      this.pageFormat.Location = new Point(6, 55);
      this.pageFormat.Size = new Size(633, 456);
      this.pagePaper.ItemSize = new SizeF(67f, 49f);
      this.pagePaper.Location = new Point(6, 55);
      this.pagePaper.Size = new Size(693, 456);
      this.groupBoxOrientation.Location = new Point(338, 119);
      this.groupBoxOrientation.Size = new Size(348, 160);
      this.pictureBoxOrientation.Location = new Point(6, 34);
      this.pictureBoxOrientation.Size = new Size(135, 104);
      this.radioButtonLandscape.Location = new Point(147, 88);
      this.radioButtonLandscape.Size = new Size(97, 22);
      this.radioButtonPortrait.Location = new Point(147, 60);
      this.radioButtonPortrait.Size = new Size(75, 22);
      this.groupBoxMargins.Location = new Point(338, 4);
      this.groupBoxMargins.Size = new Size(348, 109);
      this.maskBoxRightMargin.Location = new Point(232, 69);
      this.maskBoxRightMargin.Size = new Size(110, 36);
      this.maskBoxLeftMargin.Location = new Point(232, 27);
      this.maskBoxLeftMargin.Size = new Size(110, 36);
      this.maskBoxBottomMargin.Location = new Point(71, 69);
      this.maskBoxBottomMargin.Size = new Size(110, 36);
      this.maskBoxTopMargin.Location = new Point(71, 27);
      this.maskBoxTopMargin.Size = new Size(110, 36);
      this.labelRightMargin.Location = new Point(181, 64);
      this.labelRightMargin.Size = new Size(45, 21);
      this.labelBottomMargin.Location = new Point(6, 64);
      this.labelBottomMargin.Size = new Size(59, 21);
      this.labelLeftMargin.Location = new Point(181, 33);
      this.labelLeftMargin.Size = new Size(36, 21);
      this.labelTopMargin.Location = new Point(6, 33);
      this.labelTopMargin.Size = new Size(36, 21);
      this.groupBoxPage.Location = new Point(4, 4);
      this.groupBoxPage.Size = new Size(324, 275);
      this.dropDownPageSource.Location = new Point(8, 225);
      this.dropDownPageSource.Size = new Size(304, 37);
      this.listBoxPaperType.Location = new Point(9, 48);
      this.listBoxPaperType.Size = new Size(304, 146);
      this.labelPageSource.Location = new Point(5, 202);
      this.labelPageSource.Size = new Size(88, 21);
      this.labelType.Location = new Point(5, 25);
      this.labelType.Size = new Size(39, 21);
      this.pageHeaderFooter.ItemSize = new SizeF(123f, 49f);
      this.pageHeaderFooter.Location = new Point(6, 55);
      this.pageHeaderFooter.Size = new Size(693, 456);
      this.radBrowseEditorFooterFont.Location = new Point(6, 230);
      this.radBrowseEditorFooterFont.Size = new Size(215, 36);
      this.radBrowseEditorHeaderFont.Location = new Point(6, 31);
      this.radBrowseEditorHeaderFont.Size = new Size(215, 36);
      this.labelFooter.Location = new Point(3, 205);
      this.labelFooter.Size = new Size(79, 21);
      this.textBoxFooterRightText.Location = new Point(471, 272);
      this.textBoxFooterRightText.Size = new Size(215, 90);
      this.labelHeader.Location = new Point(3, 7);
      this.labelHeader.Size = new Size(84, 21);
      this.textBoxFooterCenterText.Location = new Point(240, 272);
      this.textBoxFooterCenterText.Size = new Size(195, 90);
      this.textBoxFooterLeftText.Location = new Point(6, 272);
      this.textBoxFooterLeftText.Size = new Size(215, 90);
      this.checkBoxReverseFooter.Location = new Point(6, 368);
      this.checkBoxReverseFooter.Size = new Size(173, 19);
      this.checkBoxReverseHeader.Location = new Point(6, 170);
      this.checkBoxReverseHeader.Size = new Size(173, 19);
      this.textBoxHeaderCenterText.Location = new Point(240, 73);
      this.textBoxHeaderCenterText.Size = new Size(215, 90);
      this.textBoxHeaderLeftText.Location = new Point(6, 73);
      this.textBoxHeaderLeftText.Size = new Size(215, 90);
      this.textBoxHeaderRightText.Location = new Point(471, 73);
      this.textBoxHeaderRightText.Size = new Size(215, 90);
      this.commandBarSnippets.Location = new Point(222, 388);
      this.commandBarSnippets.Size = new Size(225, 48);
      this.commandBarRowElement1.MinSize = new Size(25, 25);
      this.buttonPageNum.MinSize = new Size(0, 0);
      this.buttonTotalPages.MinSize = new Size(0, 0);
      this.buttonCurDate.MinSize = new Size(0, 0);
      this.buttonCurTime.MinSize = new Size(0, 0);
      this.buttonUser.MinSize = new Size(0, 0);
      this.buttonLogo.MinSize = new Size(0, 0);
      this.buttonOK.Location = new Point(506, 529);
      this.buttonOK.Size = new Size(90, 36);
      this.buttonCancel.Location = new Point(602, 529);
      this.buttonCancel.Size = new Size(90, 36);
      this.buttonPrint.Location = new Point(410, 529);
      this.buttonPrint.Size = new Size(90, 36);
      this.ClientSize = new Size(704, 577);
    }

    private void AdjustPageHeaderFooters()
    {
      if (!(this.ThemeName == "TelerikMetroTouch") && !(ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch"))
        return;
      this.textBoxHeaderLeftText.Top = this.radBrowseEditorHeaderFont.Bottom + 6;
      this.textBoxHeaderCenterText.Top = this.textBoxHeaderLeftText.Top;
      this.textBoxHeaderRightText.Top = this.textBoxHeaderLeftText.Top;
      this.textBoxFooterLeftText.Top = this.radBrowseEditorFooterFont.Bottom + 6;
      this.textBoxFooterCenterText.Top = this.textBoxFooterLeftText.Top;
      this.textBoxFooterRightText.Top = this.textBoxFooterLeftText.Top;
      this.commandBarSnippets.Width += 100;
    }

    private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
    {
      if (this.radPageView1.SelectedPage != this.pagePaper)
        return;
      this.radPageView1.SelectedPageChanged -= new EventHandler(this.radPageView1_SelectedPageChanged);
      this.listBoxPaperType.ScrollToItem(this.listBoxPaperType.SelectedItem);
    }

    private void buttonOK_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      if (this.document == null)
      {
        this.Close();
      }
      else
      {
        if (this.document.Site != null)
          (this.document.Site.GetService(typeof (IComponentChangeService)) as IComponentChangeService)?.OnComponentChanging((object) this.document, (MemberDescriptor) null);
        this.ApplySettings();
        if (this.document.Site != null)
          (this.document.Site.GetService(typeof (IComponentChangeService)) as IComponentChangeService)?.OnComponentChanged((object) this.document, (MemberDescriptor) null, (object) null, (object) null);
        this.Close();
      }
    }

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void buttonFont_Click(object sender, EventArgs e)
    {
      RadTextBox tag1 = (sender as RadButton).Tag as RadTextBox;
      Font tag2 = tag1.Tag as Font;
      FontDialog fontDialog = new FontDialog();
      fontDialog.Font = tag2;
      if (fontDialog.ShowDialog() != DialogResult.OK)
        return;
      FontConverter fontConverter = new FontConverter();
      tag1.Tag = (object) fontDialog.Font;
      tag1.Text = fontConverter.ConvertToString((object) fontDialog.Font);
    }

    private void buttonUser_MouseUp(object sender, MouseEventArgs e)
    {
      this.InsertText("[User Name]");
    }

    private void buttonCurTime_MouseUp(object sender, MouseEventArgs e)
    {
      this.InsertText("[Time Printed]");
    }

    private void buttonCurDate_MouseUp(object sender, MouseEventArgs e)
    {
      this.InsertText("[Date Printed]");
    }

    private void buttonPageNum_MouseUp(object sender, MouseEventArgs e)
    {
      this.InsertText("[Page #]");
    }

    private void buttonTotalPages_MouseUp(object sender, MouseEventArgs e)
    {
      this.InsertText("[Total Pages]");
    }

    private void buttonLogo_MouseUp(object sender, MouseEventArgs e)
    {
      this.InsertText("[Logo]");
    }

    private void textBoxHeaderFooter_Enter(object sender, EventArgs e)
    {
      this.lastFocusedTextBox = sender as RadTextBox;
    }

    private void buttonPrint_Click(object sender, EventArgs e)
    {
      if (this.document == null)
        return;
      if (new PrintDialog() { Document = ((System.Drawing.Printing.PrintDocument) this.document), AllowPrintToFile = true, AllowCurrentPage = true, AllowSelection = true, AllowSomePages = true, UseEXDialog = true }.ShowDialog() != DialogResult.OK)
        return;
      try
      {
        this.ApplySettings();
        this.document.Print();
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      catch (Exception ex)
      {
        RadMessageBox.SetThemeName(this.ThemeName);
        int num = (int) RadMessageBox.Show(ex.Message, "Error printing the document", MessageBoxButtons.OK, RadMessageIcon.Error);
        this.DialogResult = DialogResult.Abort;
        this.Close();
      }
    }

    private void toggleOrientation_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      if (args.ToggleState != Telerik.WinControls.Enumerations.ToggleState.On)
        return;
      if (sender == this.radioButtonLandscape)
      {
        this.pictureBoxOrientation.BackgroundImage = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.landscape;
      }
      else
      {
        if (sender != this.radioButtonPortrait)
          return;
        this.pictureBoxOrientation.BackgroundImage = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.portrait;
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData != Keys.Escape)
        return base.ProcessCmdKey(ref msg, keyData);
      this.DialogResult = DialogResult.Cancel;
      this.Close();
      return true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrintSettingsDialog));
      this.radPageView1 = new RadPageView();
      this.pageFormat = new RadPageViewPage();
      this.pagePaper = new RadPageViewPage();
      this.groupBoxOrientation = new RadGroupBox();
      this.pictureBoxOrientation = new PictureBox();
      this.radioButtonLandscape = new RadRadioButton();
      this.radioButtonPortrait = new RadRadioButton();
      this.groupBoxMargins = new RadGroupBox();
      this.maskBoxRightMargin = new RadMaskedEditBox();
      this.maskBoxLeftMargin = new RadMaskedEditBox();
      this.maskBoxBottomMargin = new RadMaskedEditBox();
      this.maskBoxTopMargin = new RadMaskedEditBox();
      this.labelRightMargin = new RadLabel();
      this.labelBottomMargin = new RadLabel();
      this.labelLeftMargin = new RadLabel();
      this.labelTopMargin = new RadLabel();
      this.groupBoxPage = new RadGroupBox();
      this.dropDownPageSource = new RadDropDownList();
      this.listBoxPaperType = new RadListControl();
      this.labelPageSource = new RadLabel();
      this.labelType = new RadLabel();
      this.pageHeaderFooter = new RadPageViewPage();
      this.radBrowseEditorFooterFont = new RadBrowseEditor();
      this.radBrowseEditorHeaderFont = new RadBrowseEditor();
      this.labelFooter = new RadLabel();
      this.textBoxFooterRightText = new RadTextBox();
      this.labelHeader = new RadLabel();
      this.textBoxFooterCenterText = new RadTextBox();
      this.textBoxFooterLeftText = new RadTextBox();
      this.checkBoxReverseFooter = new RadCheckBox();
      this.checkBoxReverseHeader = new RadCheckBox();
      this.textBoxHeaderCenterText = new RadTextBox();
      this.textBoxHeaderLeftText = new RadTextBox();
      this.textBoxHeaderRightText = new RadTextBox();
      this.commandBarSnippets = new RadCommandBar();
      this.commandBarRowElement1 = new CommandBarRowElement();
      this.commandBarStripElement1 = new CommandBarStripElement();
      this.buttonPageNum = new CommandBarButton();
      this.buttonTotalPages = new CommandBarButton();
      this.buttonCurDate = new CommandBarButton();
      this.buttonCurTime = new CommandBarButton();
      this.buttonUser = new CommandBarButton();
      this.buttonLogo = new CommandBarButton();
      this.buttonOK = new RadButton();
      this.buttonCancel = new RadButton();
      this.buttonPrint = new RadButton();
      this.radPageView1.BeginInit();
      this.radPageView1.SuspendLayout();
      this.pagePaper.SuspendLayout();
      this.groupBoxOrientation.BeginInit();
      this.groupBoxOrientation.SuspendLayout();
      ((ISupportInitialize) this.pictureBoxOrientation).BeginInit();
      this.radioButtonLandscape.BeginInit();
      this.radioButtonPortrait.BeginInit();
      this.groupBoxMargins.BeginInit();
      this.groupBoxMargins.SuspendLayout();
      this.maskBoxRightMargin.BeginInit();
      this.maskBoxLeftMargin.BeginInit();
      this.maskBoxBottomMargin.BeginInit();
      this.maskBoxTopMargin.BeginInit();
      this.labelRightMargin.BeginInit();
      this.labelBottomMargin.BeginInit();
      this.labelLeftMargin.BeginInit();
      this.labelTopMargin.BeginInit();
      this.groupBoxPage.BeginInit();
      this.groupBoxPage.SuspendLayout();
      this.dropDownPageSource.BeginInit();
      this.listBoxPaperType.BeginInit();
      this.labelPageSource.BeginInit();
      this.labelType.BeginInit();
      this.pageHeaderFooter.SuspendLayout();
      this.radBrowseEditorFooterFont.BeginInit();
      this.radBrowseEditorHeaderFont.BeginInit();
      this.labelFooter.BeginInit();
      this.textBoxFooterRightText.BeginInit();
      this.labelHeader.BeginInit();
      this.textBoxFooterCenterText.BeginInit();
      this.textBoxFooterLeftText.BeginInit();
      this.checkBoxReverseFooter.BeginInit();
      this.checkBoxReverseHeader.BeginInit();
      this.textBoxHeaderCenterText.BeginInit();
      this.textBoxHeaderLeftText.BeginInit();
      this.textBoxHeaderRightText.BeginInit();
      this.commandBarSnippets.BeginInit();
      this.buttonOK.BeginInit();
      this.buttonCancel.BeginInit();
      this.buttonPrint.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.radPageView1.Controls.Add((Control) this.pageFormat);
      this.radPageView1.Controls.Add((Control) this.pagePaper);
      this.radPageView1.Controls.Add((Control) this.pageHeaderFooter);
      this.radPageView1.Location = new Point(0, 0);
      this.radPageView1.Name = "radPageView1";
      this.radPageView1.SelectedPage = this.pageHeaderFooter;
      this.radPageView1.Size = new Size(645, 435);
      this.radPageView1.TabIndex = 0;
      this.radPageView1.SelectedPageChanged += new EventHandler(this.radPageView1_SelectedPageChanged);
      ((RadPageViewStripElement) this.radPageView1.GetChildAt(0)).StripButtons = StripViewButtons.None;
      this.pageFormat.ItemSize = new SizeF(52f, 28f);
      this.pageFormat.Location = new Point(10, 37);
      this.pageFormat.Name = "pageFormat";
      this.pageFormat.Size = new Size(625, 382);
      this.pageFormat.Text = "Format";
      this.pagePaper.Controls.Add((Control) this.groupBoxOrientation);
      this.pagePaper.Controls.Add((Control) this.groupBoxMargins);
      this.pagePaper.Controls.Add((Control) this.groupBoxPage);
      this.pagePaper.ItemSize = new SizeF(45f, 28f);
      this.pagePaper.Location = new Point(10, 37);
      this.pagePaper.Name = "pagePaper";
      this.pagePaper.Size = new Size(625, 382);
      this.pagePaper.Text = "Paper";
      this.groupBoxOrientation.AccessibleRole = AccessibleRole.Grouping;
      this.groupBoxOrientation.Controls.Add((Control) this.pictureBoxOrientation);
      this.groupBoxOrientation.Controls.Add((Control) this.radioButtonLandscape);
      this.groupBoxOrientation.Controls.Add((Control) this.radioButtonPortrait);
      this.groupBoxOrientation.HeaderText = "Orientation";
      this.groupBoxOrientation.Location = new Point(338, 100);
      this.groupBoxOrientation.Name = "groupBoxOrientation";
      this.groupBoxOrientation.Size = new Size(282, 136);
      this.groupBoxOrientation.TabIndex = 3;
      this.groupBoxOrientation.Text = "Orientation";
      this.pictureBoxOrientation.BackgroundImage = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.portrait;
      this.pictureBoxOrientation.BackgroundImageLayout = ImageLayout.Center;
      this.pictureBoxOrientation.Location = new Point(10, 23);
      this.pictureBoxOrientation.Name = "pictureBoxOrientation";
      this.pictureBoxOrientation.Size = new Size(135, 104);
      this.pictureBoxOrientation.TabIndex = 2;
      this.pictureBoxOrientation.TabStop = false;
      this.radioButtonLandscape.Location = new Point(151, 76);
      this.radioButtonLandscape.Name = "radioButtonLandscape";
      this.radioButtonLandscape.Size = new Size(73, 18);
      this.radioButtonLandscape.TabIndex = 1;
      this.radioButtonLandscape.Text = "Landscape";
      this.radioButtonLandscape.ToggleStateChanged += new StateChangedEventHandler(this.toggleOrientation_ToggleStateChanged);
      this.radioButtonPortrait.CheckState = CheckState.Checked;
      this.radioButtonPortrait.Location = new Point(151, 48);
      this.radioButtonPortrait.Name = "radioButtonPortrait";
      this.radioButtonPortrait.Size = new Size(57, 18);
      this.radioButtonPortrait.TabIndex = 0;
      this.radioButtonPortrait.Text = "Portrait";
      this.radioButtonPortrait.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.radioButtonPortrait.ToggleStateChanged += new StateChangedEventHandler(this.toggleOrientation_ToggleStateChanged);
      this.groupBoxMargins.AccessibleRole = AccessibleRole.Grouping;
      this.groupBoxMargins.Controls.Add((Control) this.maskBoxRightMargin);
      this.groupBoxMargins.Controls.Add((Control) this.maskBoxLeftMargin);
      this.groupBoxMargins.Controls.Add((Control) this.maskBoxBottomMargin);
      this.groupBoxMargins.Controls.Add((Control) this.maskBoxTopMargin);
      this.groupBoxMargins.Controls.Add((Control) this.labelRightMargin);
      this.groupBoxMargins.Controls.Add((Control) this.labelBottomMargin);
      this.groupBoxMargins.Controls.Add((Control) this.labelLeftMargin);
      this.groupBoxMargins.Controls.Add((Control) this.labelTopMargin);
      this.groupBoxMargins.HeaderText = "Margins (inches)";
      this.groupBoxMargins.Location = new Point(338, 4);
      this.groupBoxMargins.Name = "groupBoxMargins";
      this.groupBoxMargins.Size = new Size(282, 90);
      this.groupBoxMargins.TabIndex = 2;
      this.groupBoxMargins.Text = "Margins (inches)";
      this.maskBoxRightMargin.Location = new Point(192, 59);
      this.maskBoxRightMargin.Mask = "F2";
      this.maskBoxRightMargin.MaskType = MaskType.Numeric;
      this.maskBoxRightMargin.Name = "maskBoxRightMargin";
      this.maskBoxRightMargin.Size = new Size(80, 20);
      this.maskBoxRightMargin.TabIndex = 7;
      this.maskBoxRightMargin.TabStop = false;
      this.maskBoxRightMargin.Text = "0.00";
      this.maskBoxLeftMargin.Location = new Point(192, 27);
      this.maskBoxLeftMargin.Mask = "F2";
      this.maskBoxLeftMargin.MaskType = MaskType.Numeric;
      this.maskBoxLeftMargin.Name = "maskBoxLeftMargin";
      this.maskBoxLeftMargin.Size = new Size(80, 20);
      this.maskBoxLeftMargin.TabIndex = 6;
      this.maskBoxLeftMargin.TabStop = false;
      this.maskBoxLeftMargin.Text = "0.00";
      this.maskBoxBottomMargin.Location = new Point(58, 59);
      this.maskBoxBottomMargin.Mask = "F2";
      this.maskBoxBottomMargin.MaskType = MaskType.Numeric;
      this.maskBoxBottomMargin.Name = "maskBoxBottomMargin";
      this.maskBoxBottomMargin.Size = new Size(80, 20);
      this.maskBoxBottomMargin.TabIndex = 5;
      this.maskBoxBottomMargin.TabStop = false;
      this.maskBoxBottomMargin.Text = "0.00";
      this.maskBoxTopMargin.Location = new Point(58, 27);
      this.maskBoxTopMargin.Mask = "F2";
      this.maskBoxTopMargin.MaskType = MaskType.Numeric;
      this.maskBoxTopMargin.Name = "maskBoxTopMargin";
      this.maskBoxTopMargin.Size = new Size(80, 20);
      this.maskBoxTopMargin.TabIndex = 4;
      this.maskBoxTopMargin.TabStop = false;
      this.maskBoxTopMargin.Text = "0.00";
      this.labelRightMargin.Location = new Point(151, 59);
      this.labelRightMargin.Name = "labelRightMargin";
      this.labelRightMargin.Size = new Size(35, 18);
      this.labelRightMargin.TabIndex = 3;
      this.labelRightMargin.Text = "Right:";
      this.labelBottomMargin.Location = new Point(6, 59);
      this.labelBottomMargin.Name = "labelBottomMargin";
      this.labelBottomMargin.Size = new Size(46, 18);
      this.labelBottomMargin.TabIndex = 2;
      this.labelBottomMargin.Text = "Bottom:";
      this.labelLeftMargin.Location = new Point(151, 29);
      this.labelLeftMargin.Name = "labelLeftMargin";
      this.labelLeftMargin.Size = new Size(27, 18);
      this.labelLeftMargin.TabIndex = 1;
      this.labelLeftMargin.Text = "Left:";
      this.labelTopMargin.Location = new Point(6, 29);
      this.labelTopMargin.Name = "labelTopMargin";
      this.labelTopMargin.Size = new Size(28, 18);
      this.labelTopMargin.TabIndex = 0;
      this.labelTopMargin.Text = "Top:";
      this.groupBoxPage.AccessibleRole = AccessibleRole.Grouping;
      this.groupBoxPage.Controls.Add((Control) this.dropDownPageSource);
      this.groupBoxPage.Controls.Add((Control) this.listBoxPaperType);
      this.groupBoxPage.Controls.Add((Control) this.labelPageSource);
      this.groupBoxPage.Controls.Add((Control) this.labelType);
      this.groupBoxPage.HeaderText = "Page";
      this.groupBoxPage.Location = new Point(4, 4);
      this.groupBoxPage.Name = "groupBoxPage";
      this.groupBoxPage.Size = new Size(324, 232);
      this.groupBoxPage.TabIndex = 0;
      this.groupBoxPage.Text = "Page";
      this.dropDownPageSource.DropDownStyle = RadDropDownStyle.DropDownList;
      this.dropDownPageSource.Location = new Point(9, 198);
      this.dropDownPageSource.Name = "dropDownPageSource";
      this.dropDownPageSource.Size = new Size(304, 20);
      this.dropDownPageSource.TabIndex = 8;
      this.dropDownPageSource.Text = "Automatic Source";
      this.listBoxPaperType.ItemHeight = 36;
      this.listBoxPaperType.Location = new Point(9, 48);
      this.listBoxPaperType.Name = "listBoxPaperType";
      this.listBoxPaperType.Size = new Size(304, 118);
      this.listBoxPaperType.TabIndex = 3;
      this.listBoxPaperType.Text = "radListControl1";
      this.labelPageSource.Location = new Point(6, 175);
      this.labelPageSource.Name = "labelPageSource";
      this.labelPageSource.Size = new Size(67, 18);
      this.labelPageSource.TabIndex = 2;
      this.labelPageSource.Text = "Page source";
      this.labelType.Location = new Point(5, 25);
      this.labelType.Name = "labelType";
      this.labelType.Size = new Size(30, 18);
      this.labelType.TabIndex = 0;
      this.labelType.Text = "Type";
      this.pageHeaderFooter.Controls.Add((Control) this.radBrowseEditorFooterFont);
      this.pageHeaderFooter.Controls.Add((Control) this.radBrowseEditorHeaderFont);
      this.pageHeaderFooter.Controls.Add((Control) this.labelFooter);
      this.pageHeaderFooter.Controls.Add((Control) this.textBoxFooterRightText);
      this.pageHeaderFooter.Controls.Add((Control) this.labelHeader);
      this.pageHeaderFooter.Controls.Add((Control) this.textBoxFooterCenterText);
      this.pageHeaderFooter.Controls.Add((Control) this.textBoxFooterLeftText);
      this.pageHeaderFooter.Controls.Add((Control) this.checkBoxReverseFooter);
      this.pageHeaderFooter.Controls.Add((Control) this.checkBoxReverseHeader);
      this.pageHeaderFooter.Controls.Add((Control) this.textBoxHeaderCenterText);
      this.pageHeaderFooter.Controls.Add((Control) this.textBoxHeaderLeftText);
      this.pageHeaderFooter.Controls.Add((Control) this.textBoxHeaderRightText);
      this.pageHeaderFooter.Controls.Add((Control) this.commandBarSnippets);
      this.pageHeaderFooter.ItemSize = new SizeF(89f, 28f);
      this.pageHeaderFooter.Location = new Point(10, 37);
      this.pageHeaderFooter.Name = "pageHeaderFooter";
      this.pageHeaderFooter.Size = new Size(625, 382);
      this.pageHeaderFooter.Text = "Header/Footer";
      this.radBrowseEditorFooterFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorFooterFont.Location = new Point(6, 214);
      this.radBrowseEditorFooterFont.Name = "radBrowseEditorFooterFont";
      this.radBrowseEditorFooterFont.Size = new Size(195, 20);
      this.radBrowseEditorFooterFont.TabIndex = 11;
      this.radBrowseEditorFooterFont.Text = "radBrowseEditor1";
      this.radBrowseEditorHeaderFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorHeaderFont.Location = new Point(6, 31);
      this.radBrowseEditorHeaderFont.Name = "radBrowseEditorHeaderFont";
      this.radBrowseEditorHeaderFont.Size = new Size(195, 20);
      this.radBrowseEditorHeaderFont.TabIndex = 10;
      this.radBrowseEditorHeaderFont.Text = "radBrowseEditor1";
      this.labelFooter.Location = new Point(3, 189);
      this.labelFooter.Name = "labelFooter";
      this.labelFooter.Size = new Size(62, 18);
      this.labelFooter.TabIndex = 9;
      this.labelFooter.Text = "Footer font";
      this.textBoxFooterRightText.AutoSize = false;
      this.textBoxFooterRightText.Location = new Point(424, 239);
      this.textBoxFooterRightText.Multiline = true;
      this.textBoxFooterRightText.Name = "textBoxFooterRightText";
      this.textBoxFooterRightText.Size = new Size(195, 90);
      this.textBoxFooterRightText.TabIndex = 8;
      this.textBoxFooterRightText.TabStop = false;
      this.textBoxFooterRightText.TextAlign = HorizontalAlignment.Right;
      this.textBoxFooterRightText.Enter += new EventHandler(this.textBoxHeaderFooter_Enter);
      this.labelHeader.Location = new Point(3, 7);
      this.labelHeader.Name = "labelHeader";
      this.labelHeader.Size = new Size(66, 18);
      this.labelHeader.TabIndex = 6;
      this.labelHeader.Text = "Header font";
      this.textBoxFooterCenterText.AutoSize = false;
      this.textBoxFooterCenterText.Location = new Point(215, 239);
      this.textBoxFooterCenterText.Multiline = true;
      this.textBoxFooterCenterText.Name = "textBoxFooterCenterText";
      this.textBoxFooterCenterText.Size = new Size(195, 90);
      this.textBoxFooterCenterText.TabIndex = 7;
      this.textBoxFooterCenterText.TabStop = false;
      this.textBoxFooterCenterText.TextAlign = HorizontalAlignment.Center;
      this.textBoxFooterCenterText.Enter += new EventHandler(this.textBoxHeaderFooter_Enter);
      this.textBoxFooterLeftText.AutoSize = false;
      this.textBoxFooterLeftText.Location = new Point(6, 239);
      this.textBoxFooterLeftText.Multiline = true;
      this.textBoxFooterLeftText.Name = "textBoxFooterLeftText";
      this.textBoxFooterLeftText.Size = new Size(195, 90);
      this.textBoxFooterLeftText.TabIndex = 6;
      this.textBoxFooterLeftText.TabStop = false;
      this.textBoxFooterLeftText.Enter += new EventHandler(this.textBoxHeaderFooter_Enter);
      this.checkBoxReverseFooter.Location = new Point(6, 336);
      this.checkBoxReverseFooter.Name = "checkBoxReverseFooter";
      this.checkBoxReverseFooter.Size = new Size(135, 18);
      this.checkBoxReverseFooter.TabIndex = 3;
      this.checkBoxReverseFooter.Text = "Reverse on even pages";
      this.checkBoxReverseHeader.Location = new Point(6, 154);
      this.checkBoxReverseHeader.Name = "checkBoxReverseHeader";
      this.checkBoxReverseHeader.Size = new Size(135, 18);
      this.checkBoxReverseHeader.TabIndex = 2;
      this.checkBoxReverseHeader.Text = "Reverse on even pages";
      this.textBoxHeaderCenterText.AutoSize = false;
      this.textBoxHeaderCenterText.Location = new Point(215, 57);
      this.textBoxHeaderCenterText.Multiline = true;
      this.textBoxHeaderCenterText.Name = "textBoxHeaderCenterText";
      this.textBoxHeaderCenterText.Size = new Size(195, 90);
      this.textBoxHeaderCenterText.TabIndex = 4;
      this.textBoxHeaderCenterText.TabStop = false;
      this.textBoxHeaderCenterText.TextAlign = HorizontalAlignment.Center;
      this.textBoxHeaderCenterText.Enter += new EventHandler(this.textBoxHeaderFooter_Enter);
      this.textBoxHeaderLeftText.AutoSize = false;
      this.textBoxHeaderLeftText.Location = new Point(6, 57);
      this.textBoxHeaderLeftText.Multiline = true;
      this.textBoxHeaderLeftText.Name = "textBoxHeaderLeftText";
      this.textBoxHeaderLeftText.Size = new Size(195, 90);
      this.textBoxHeaderLeftText.TabIndex = 3;
      this.textBoxHeaderLeftText.TabStop = false;
      this.textBoxHeaderLeftText.Enter += new EventHandler(this.textBoxHeaderFooter_Enter);
      this.textBoxHeaderRightText.AutoSize = false;
      this.textBoxHeaderRightText.Location = new Point(424, 57);
      this.textBoxHeaderRightText.Multiline = true;
      this.textBoxHeaderRightText.Name = "textBoxHeaderRightText";
      this.textBoxHeaderRightText.Size = new Size(195, 90);
      this.textBoxHeaderRightText.TabIndex = 5;
      this.textBoxHeaderRightText.TabStop = false;
      this.textBoxHeaderRightText.TextAlign = HorizontalAlignment.Right;
      this.textBoxHeaderRightText.Enter += new EventHandler(this.textBoxHeaderFooter_Enter);
      this.commandBarSnippets.Location = new Point(234, 348);
      this.commandBarSnippets.Name = "commandBarSnippets";
      this.commandBarSnippets.Rows.AddRange(this.commandBarRowElement1);
      this.commandBarSnippets.Size = new Size(152, 30);
      this.commandBarSnippets.TabIndex = 2;
      this.commandBarSnippets.Text = "radCommandBar1";
      this.commandBarRowElement1.DisplayName = (string) null;
      this.commandBarRowElement1.MinSize = new Size(25, 25);
      this.commandBarRowElement1.Strips.AddRange(this.commandBarStripElement1);
      this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
      this.commandBarStripElement1.Grip.Visibility = ElementVisibility.Collapsed;
      this.commandBarStripElement1.Items.AddRange((RadCommandBarBaseItem) this.buttonPageNum, (RadCommandBarBaseItem) this.buttonTotalPages, (RadCommandBarBaseItem) this.buttonCurDate, (RadCommandBarBaseItem) this.buttonCurTime, (RadCommandBarBaseItem) this.buttonUser, (RadCommandBarBaseItem) this.buttonLogo);
      this.commandBarStripElement1.Name = "commandBarStripElement1";
      this.commandBarStripElement1.OverflowButton.Visibility = ElementVisibility.Collapsed;
      this.commandBarStripElement1.Text = "";
      this.commandBarStripElement1.GetChildAt(0).Visibility = ElementVisibility.Collapsed;
      this.commandBarStripElement1.GetChildAt(2).Visibility = ElementVisibility.Collapsed;
      this.buttonPageNum.AccessibleDescription = "commandBarButton1";
      this.buttonPageNum.AccessibleName = "commandBarButton1";
      this.buttonPageNum.DisplayName = "commandBarButton1";
      this.buttonPageNum.Image = (Image) componentResourceManager.GetObject("buttonPageNum.Image");
      this.buttonPageNum.Margin = new Padding(3, 0, 0, 0);
      this.buttonPageNum.MinSize = new Size(25, 0);
      this.buttonPageNum.Name = "buttonPageNum";
      this.buttonPageNum.Text = "";
      this.buttonPageNum.ToolTipText = "Page Number";
      this.buttonTotalPages.AccessibleDescription = "commandBarButton2";
      this.buttonTotalPages.AccessibleName = "commandBarButton2";
      this.buttonTotalPages.DisplayName = "commandBarButton2";
      this.buttonTotalPages.Image = (Image) componentResourceManager.GetObject("buttonTotalPages.Image");
      this.buttonTotalPages.MinSize = new Size(25, 0);
      this.buttonTotalPages.Name = "buttonTotalPages";
      this.buttonTotalPages.Text = "";
      this.buttonTotalPages.ToolTipText = "Total Pages";
      this.buttonCurDate.AccessibleDescription = "commandBarButton3";
      this.buttonCurDate.AccessibleName = "commandBarButton3";
      this.buttonCurDate.DisplayName = "commandBarButton3";
      this.buttonCurDate.Image = (Image) componentResourceManager.GetObject("buttonCurDate.Image");
      this.buttonCurDate.MinSize = new Size(25, 0);
      this.buttonCurDate.Name = "buttonCurDate";
      this.buttonCurDate.Text = "";
      this.buttonCurDate.ToolTipText = "Current Date";
      this.buttonCurTime.AccessibleDescription = "commandBarButton4";
      this.buttonCurTime.AccessibleName = "commandBarButton4";
      this.buttonCurTime.DisplayName = "commandBarButton4";
      this.buttonCurTime.Image = (Image) componentResourceManager.GetObject("buttonCurTime.Image");
      this.buttonCurTime.MinSize = new Size(25, 0);
      this.buttonCurTime.Name = "buttonCurTime";
      this.buttonCurTime.Text = "";
      this.buttonCurTime.ToolTipText = "Current Time";
      this.buttonUser.AccessibleDescription = "commandBarButton5";
      this.buttonUser.AccessibleName = "commandBarButton5";
      this.buttonUser.DisplayName = "commandBarButton5";
      this.buttonUser.Image = (Image) componentResourceManager.GetObject("buttonUser.Image");
      this.buttonUser.MinSize = new Size(25, 0);
      this.buttonUser.Name = "buttonUser";
      this.buttonUser.Text = "";
      this.buttonUser.ToolTipText = "User Name";
      this.buttonLogo.DisplayName = "commandBarButton1";
      this.buttonLogo.Image = (Image) componentResourceManager.GetObject("buttonLogo.Image");
      this.buttonLogo.Name = "buttonLogo";
      this.buttonLogo.Text = "";
      this.buttonLogo.ToolTipText = "Logo";
      this.buttonOK.Location = new Point(453, 440);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new Size(90, 24);
      this.buttonOK.TabIndex = 1;
      this.buttonOK.Text = "OK";
      this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
      this.buttonCancel.DialogResult = DialogResult.Cancel;
      this.buttonCancel.Location = new Point(549, 440);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(90, 24);
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.buttonPrint.Location = new Point(357, 440);
      this.buttonPrint.Name = "buttonPrint";
      this.buttonPrint.Size = new Size(90, 24);
      this.buttonPrint.TabIndex = 3;
      this.buttonPrint.Text = "Print";
      this.buttonPrint.Click += new EventHandler(this.buttonPrint_Click);
      this.AcceptButton = (IButtonControl) this.buttonPrint;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.CancelButton = (IButtonControl) this.buttonCancel;
      this.ClientSize = new Size(642, 476);
      this.Controls.Add((Control) this.buttonPrint);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.buttonOK);
      this.Controls.Add((Control) this.radPageView1);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PrintSettingsDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Print Settings";
      this.radPageView1.EndInit();
      this.radPageView1.ResumeLayout(false);
      this.pagePaper.ResumeLayout(false);
      this.groupBoxOrientation.EndInit();
      this.groupBoxOrientation.ResumeLayout(false);
      this.groupBoxOrientation.PerformLayout();
      ((ISupportInitialize) this.pictureBoxOrientation).EndInit();
      this.radioButtonLandscape.EndInit();
      this.radioButtonPortrait.EndInit();
      this.groupBoxMargins.EndInit();
      this.groupBoxMargins.ResumeLayout(false);
      this.groupBoxMargins.PerformLayout();
      this.maskBoxRightMargin.EndInit();
      this.maskBoxLeftMargin.EndInit();
      this.maskBoxBottomMargin.EndInit();
      this.maskBoxTopMargin.EndInit();
      this.labelRightMargin.EndInit();
      this.labelBottomMargin.EndInit();
      this.labelLeftMargin.EndInit();
      this.labelTopMargin.EndInit();
      this.groupBoxPage.EndInit();
      this.groupBoxPage.ResumeLayout(false);
      this.groupBoxPage.PerformLayout();
      this.dropDownPageSource.EndInit();
      this.listBoxPaperType.EndInit();
      this.labelPageSource.EndInit();
      this.labelType.EndInit();
      this.pageHeaderFooter.ResumeLayout(false);
      this.pageHeaderFooter.PerformLayout();
      this.radBrowseEditorFooterFont.EndInit();
      this.radBrowseEditorHeaderFont.EndInit();
      this.labelFooter.EndInit();
      this.textBoxFooterRightText.EndInit();
      this.labelHeader.EndInit();
      this.textBoxFooterCenterText.EndInit();
      this.textBoxFooterLeftText.EndInit();
      this.checkBoxReverseFooter.EndInit();
      this.checkBoxReverseHeader.EndInit();
      this.textBoxHeaderCenterText.EndInit();
      this.textBoxHeaderLeftText.EndInit();
      this.textBoxHeaderRightText.EndInit();
      this.commandBarSnippets.EndInit();
      this.buttonOK.EndInit();
      this.buttonCancel.EndInit();
      this.buttonPrint.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
    }
  }
}
