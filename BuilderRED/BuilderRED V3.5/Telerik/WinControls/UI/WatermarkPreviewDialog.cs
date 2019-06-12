// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.WatermarkPreviewDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class WatermarkPreviewDialog : RadForm
  {
    private bool isMetric;
    private RadPrintDocument document;
    private RadThemeComponentBase.ThemeContext context;
    private IContainer components;
    private WatermarkPreviewControl watermarkPreviewControl1;
    private RadGroupBox groupBoxPosition;
    private RadGroupBox groupBoxPageRange;
    private RadRadioButton radRadioButtonInFront;
    private RadRadioButton radRadioButtonBehind;
    private RadRadioButton radRadioButtonAllPages;
    private RadRadioButton radRadioButtonPages;
    private RadTextBox radTextBoxPages;
    private RadLabel radLabelDescription;
    private RadPageView radPageView1;
    private RadPageViewPage pageText;
    private RadPageViewPage pagePicture;
    private RadTextBox radTextBoxWMText;
    private RadLabel radLabelText;
    private RadLabel radLabelFont;
    private RadDropDownList radDropDownListFonts;
    private RadDropDownList radDropDownListFontSize;
    private RadLabel radLabelFontSize;
    private RadLabel radLabelColor;
    private RadColorBox radColorBoxFontColor;
    private RadLabel radLabelOpacity;
    private RadBrowseEditor radBrowseEditorImage;
    private RadLabel radLabelLoadImage;
    private RadLabel radLabelPreview;
    private RadLabel radLabelTextHOffset;
    private RadLabel radLabelTextVOffset;
    private RadLabel radLabelTextRotation;
    private RadButton radButtonClearImage;
    private RadLabel radLabelImageVOffset;
    private RadLabel radLabelImageHOffset;
    private RadLabel radLabelImageOpacity;
    private RadCheckBox radCheckBoxImageTiling;
    private RadButton radButtonCancel;
    private RadButton radButtonOK;
    private PlusMinusEditor plusMinusEditorTextHOffset;
    private PlusMinusEditor plusMinusEditorTextVOffset;
    private PlusMinusEditor plusMinusEditorTextAngle;
    private PlusMinusEditor plusMinusEditorTextOpacity;
    private PlusMinusEditor plusMinusEditorImageHOffset;
    private PlusMinusEditor plusMinusEditorImageVOffset;
    private PlusMinusEditor plusMinusEditorImageOpacity;
    private RadCommandBar radCommandBar1;
    private CommandBarRowElement commandBarRowElement1;
    private CommandBarStripElement commandBarStripElement1;
    private CommandBarToggleButton cbToggleButtonBold;
    private CommandBarToggleButton cbToggleButtonItalic;
    private CommandBarToggleButton cbToggleButtonUnderline;
    private CommandBarToggleButton cbToggleButtonStrikeout;
    private RadButton radButtonRemoveWatermark;

    public WatermarkPreviewDialog(RadPrintDocument document)
    {
      this.InitializeComponent();
      this.isMetric = RegionInfo.CurrentRegion.IsMetric;
      this.radCommandBar1.CommandBarElement.DrawFill = false;
      this.radCommandBar1.CommandBarElement.DrawBorder = false;
      this.document = document;
      this.watermarkPreviewControl1.Watermark = document.Watermark == null ? new RadPrintWatermark() : (RadPrintWatermark) document.Watermark.Clone();
      this.watermarkPreviewControl1.PaperSize = this.document.DefaultPageSettings.PaperSize;
      this.InitializeEditors();
      this.LoadWatermarkSettings(this.watermarkPreviewControl1.Watermark);
      this.LocalizeStrings();
      this.WireEvents();
      this.context = RadThemeComponentBase.CreateContext((Control) this);
    }

    private void WireEvents()
    {
      this.radTextBoxWMText.TextChanging += new TextChangingEventHandler(this.radTextBoxWMText_TextChanging);
      this.radColorBoxFontColor.ValueChanged += new EventHandler(this.radColorBoxFontColor_ValueChanged);
      this.radDropDownListFonts.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownListFonts_SelectedIndexChanged);
      this.radDropDownListFonts.VisualListItemFormatting += new VisualListItemFormattingEventHandler(this.radDropDownListFonts_VisualListItemFormatting);
      this.radDropDownListFontSize.DropDownListElement.EditableElement.TextBox.TextBoxItem.HostedControl.TextChanged += new EventHandler(this.radDropDownListFontSize_TextChanged);
      this.plusMinusEditorTextHOffset.ValueChanged += new EventHandler(this.plusMinusEditorTextHOffset_ValueChanged);
      this.plusMinusEditorTextVOffset.ValueChanged += new EventHandler(this.plusMinusEditorTextVOffset_ValueChanged);
      this.plusMinusEditorTextAngle.ValueChanged += new EventHandler(this.plusMinusEditorTextAngle_ValueChanged);
      this.plusMinusEditorTextOpacity.ValueChanged += new EventHandler(this.plusMinusEditorTextOpacity_ValueChanged);
      this.WireToggleButtons();
      this.radBrowseEditorImage.ValueChanged += new EventHandler(this.radBrowseEditorImage_ValueChanged);
      this.radCheckBoxImageTiling.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxImageTiling_ToggleStateChanged);
      this.plusMinusEditorImageHOffset.ValueChanged += new EventHandler(this.plusMinusEditorImageHOffset_ValueChanged);
      this.plusMinusEditorImageVOffset.ValueChanged += new EventHandler(this.plusMinusEditorImageVOffset_ValueChanged);
      this.plusMinusEditorImageOpacity.ValueChanged += new EventHandler(this.plusMinusEditorImageOpacity_ValueChanged);
      this.radButtonOK.Click += new EventHandler(this.radButtonOK_Click);
      this.radButtonCancel.Click += new EventHandler(this.radButtonCancel_Click);
      this.radRadioButtonAllPages.ToggleStateChanged += new StateChangedEventHandler(this.radRadioButtonAllPages_ToggleStateChanged);
      this.radRadioButtonPages.ToggleStateChanged += new StateChangedEventHandler(this.radRadioButtonPages_ToggleStateChanged);
      this.radRadioButtonInFront.ToggleStateChanged += new StateChangedEventHandler(this.radRadioButtonInFront_ToggleStateChanged);
      this.radRadioButtonBehind.ToggleStateChanged += new StateChangedEventHandler(this.radRadioButtonBehind_ToggleStateChanged);
      this.radTextBoxPages.TextChanged += new EventHandler(this.radTextBoxPages_TextChanged);
      this.radButtonClearImage.Click += new EventHandler(this.radButtonClearImage_Click);
      LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.PrintingDialogsLocalizationProvider_CurrentProviderChanged);
      this.ThemeNameChanged += new ThemeNameChangedEventHandler(this.WatermarkPreviewDialog_ThemeNameChanged);
    }

    private void UnwireEvents()
    {
      this.radTextBoxWMText.TextChanging -= new TextChangingEventHandler(this.radTextBoxWMText_TextChanging);
      this.radColorBoxFontColor.ValueChanged -= new EventHandler(this.radColorBoxFontColor_ValueChanged);
      this.radDropDownListFonts.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownListFonts_SelectedIndexChanged);
      this.radDropDownListFonts.VisualListItemFormatting -= new VisualListItemFormattingEventHandler(this.radDropDownListFonts_VisualListItemFormatting);
      if (this.radDropDownListFontSize.DropDownListElement != null)
        this.radDropDownListFontSize.DropDownListElement.EditableElement.TextBox.TextBoxItem.HostedControl.TextChanged -= new EventHandler(this.radDropDownListFontSize_TextChanged);
      this.plusMinusEditorTextHOffset.ValueChanged -= new EventHandler(this.plusMinusEditorTextHOffset_ValueChanged);
      this.plusMinusEditorTextVOffset.ValueChanged -= new EventHandler(this.plusMinusEditorTextVOffset_ValueChanged);
      this.plusMinusEditorTextOpacity.ValueChanged -= new EventHandler(this.plusMinusEditorTextOpacity_ValueChanged);
      this.plusMinusEditorTextAngle.ValueChanged -= new EventHandler(this.plusMinusEditorTextAngle_ValueChanged);
      this.UnwireToggleButtons();
      this.radBrowseEditorImage.ValueChanged -= new EventHandler(this.radBrowseEditorImage_ValueChanged);
      this.radCheckBoxImageTiling.ToggleStateChanged -= new StateChangedEventHandler(this.radCheckBoxImageTiling_ToggleStateChanged);
      this.plusMinusEditorImageHOffset.ValueChanged -= new EventHandler(this.plusMinusEditorImageHOffset_ValueChanged);
      this.plusMinusEditorImageVOffset.ValueChanged -= new EventHandler(this.plusMinusEditorImageVOffset_ValueChanged);
      this.plusMinusEditorImageOpacity.ValueChanged -= new EventHandler(this.plusMinusEditorImageOpacity_ValueChanged);
      this.radButtonOK.Click -= new EventHandler(this.radButtonOK_Click);
      this.radButtonCancel.Click -= new EventHandler(this.radButtonCancel_Click);
      this.radRadioButtonAllPages.ToggleStateChanged -= new StateChangedEventHandler(this.radRadioButtonAllPages_ToggleStateChanged);
      this.radRadioButtonPages.ToggleStateChanged -= new StateChangedEventHandler(this.radRadioButtonPages_ToggleStateChanged);
      this.radRadioButtonInFront.ToggleStateChanged -= new StateChangedEventHandler(this.radRadioButtonInFront_ToggleStateChanged);
      this.radRadioButtonBehind.ToggleStateChanged -= new StateChangedEventHandler(this.radRadioButtonBehind_ToggleStateChanged);
      this.radTextBoxPages.TextChanged -= new EventHandler(this.radTextBoxPages_TextChanged);
      this.radButtonClearImage.Click -= new EventHandler(this.radButtonClearImage_Click);
      LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.PrintingDialogsLocalizationProvider_CurrentProviderChanged);
      this.ThemeNameChanged -= new ThemeNameChangedEventHandler(this.WatermarkPreviewDialog_ThemeNameChanged);
    }

    private void WireToggleButtons()
    {
      this.cbToggleButtonBold.ToggleStateChanging += new StateChangingEventHandler(this.cbToggleButtonBold_ToggleStateChanging);
      this.cbToggleButtonItalic.ToggleStateChanging += new StateChangingEventHandler(this.cbToggleButtonItalic_ToggleStateChanging);
      this.cbToggleButtonUnderline.ToggleStateChanging += new StateChangingEventHandler(this.cbToggleButtonUnderline_ToggleStateChanging);
      this.cbToggleButtonStrikeout.ToggleStateChanging += new StateChangingEventHandler(this.cbToggleButtonStrikeout_ToggleStateChanging);
    }

    private void UnwireToggleButtons()
    {
      this.cbToggleButtonBold.ToggleStateChanging -= new StateChangingEventHandler(this.cbToggleButtonBold_ToggleStateChanging);
      this.cbToggleButtonItalic.ToggleStateChanging -= new StateChangingEventHandler(this.cbToggleButtonItalic_ToggleStateChanging);
      this.cbToggleButtonUnderline.ToggleStateChanging -= new StateChangingEventHandler(this.cbToggleButtonUnderline_ToggleStateChanging);
      this.cbToggleButtonStrikeout.ToggleStateChanging -= new StateChangingEventHandler(this.cbToggleButtonStrikeout_ToggleStateChanging);
    }

    private void LoadWatermarkSettings(RadPrintWatermark watermark)
    {
      this.radTextBoxWMText.Text = watermark.Text;
      this.plusMinusEditorTextHOffset.Value = (Decimal) watermark.TextHOffset / new Decimal(100);
      this.plusMinusEditorTextVOffset.Value = (Decimal) watermark.TextVOffset / new Decimal(100);
      if (this.isMetric)
      {
        this.plusMinusEditorTextHOffset.Value *= new Decimal(254, 0, 0, false, (byte) 2);
        this.plusMinusEditorTextVOffset.Value *= new Decimal(254, 0, 0, false, (byte) 2);
      }
      this.plusMinusEditorTextAngle.Value = (Decimal) watermark.TextAngle;
      this.plusMinusEditorTextOpacity.Value = Math.Round((Decimal) watermark.TextOpacity / new Decimal((int) byte.MaxValue, 0, 0, false, (byte) 2));
      this.radColorBoxFontColor.Value = watermark.ForeColor;
      if (watermark.Font != null)
      {
        this.radDropDownListFonts.SelectedValue = (object) watermark.Font.FontFamily.Name;
        this.radDropDownListFontSize.SelectedValue = (object) watermark.Font.Size;
        this.cbToggleButtonBold.ToggleState = watermark.Font.Bold ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        this.cbToggleButtonItalic.ToggleState = watermark.Font.Italic ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        this.cbToggleButtonUnderline.ToggleState = watermark.Font.Underline ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
        this.cbToggleButtonStrikeout.ToggleState = watermark.Font.Strikeout ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      }
      if (File.Exists(watermark.ImagePath))
        this.radBrowseEditorImage.Value = watermark.ImagePath;
      this.plusMinusEditorImageHOffset.Value = (Decimal) watermark.ImageHOffset / new Decimal(100);
      this.plusMinusEditorImageVOffset.Value = (Decimal) watermark.ImageVOffset / new Decimal(100);
      if (this.isMetric)
      {
        this.plusMinusEditorImageHOffset.Value *= new Decimal(254, 0, 0, false, (byte) 2);
        this.plusMinusEditorImageVOffset.Value *= new Decimal(254, 0, 0, false, (byte) 2);
      }
      this.plusMinusEditorImageOpacity.Value = Math.Round((Decimal) watermark.ImageOpacity / new Decimal((int) byte.MaxValue, 0, 0, false, (byte) 2));
      this.radCheckBoxImageTiling.IsChecked = watermark.ImageTiling;
      if (watermark.DrawInFront)
        this.radRadioButtonInFront.IsChecked = true;
      else
        this.radRadioButtonBehind.IsChecked = true;
      if (watermark.AllPages)
        this.radRadioButtonAllPages.IsChecked = true;
      else
        this.radRadioButtonPages.IsChecked = true;
      this.radTextBoxPages.Text = watermark.Pages;
    }

    private void InitializeEditors()
    {
      this.InitializeFontsDropDown();
      this.InitializeFontSizeDropDown();
      this.InitializeLoadImageDialog();
      this.InitializePlusMinusEditors();
    }

    private void InitializeFontsDropDown()
    {
      InstalledFontCollection installedFontCollection = new InstalledFontCollection();
      this.radDropDownListFonts.DisplayMember = "Name";
      this.radDropDownListFonts.ValueMember = "Name";
      this.radDropDownListFonts.DataSource = (object) installedFontCollection.Families;
    }

    private void InitializeLoadImageDialog()
    {
      OpenFileDialog dialog = this.radBrowseEditorImage.Dialog as OpenFileDialog;
      ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
      string str1 = string.Empty;
      string str2 = string.Empty;
      foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
      {
        str2 = str2 + imageCodecInfo.FilenameExtension + ";";
        string str3 = imageCodecInfo.CodecName.Substring(8).Replace("Codec", "Files").Trim();
        OpenFileDialog openFileDialog = dialog;
        openFileDialog.Filter = openFileDialog.Filter + str1 + str3 + string.Format("({0})|{0}", (object) imageCodecInfo.FilenameExtension);
        str1 = "|";
      }
      str2.Remove(str2.Length - 1);
      dialog.Filter = "All Image Files" + string.Format("({0})|{0}", (object) str2) + str1 + dialog.Filter;
    }

    private void InitializeFontSizeDropDown()
    {
      List<float> floatList = new List<float>() { 16f, 18f, 20f, 22f, 24f, 26f, 30f, 36f, 48f, 72f, 80f, 90f, 96f, 105f, 120f, 144f };
      if (!floatList.Contains(this.watermarkPreviewControl1.Watermark.Font.Size))
        floatList.Add(this.watermarkPreviewControl1.Watermark.Font.Size);
      floatList.Sort();
      this.radDropDownListFontSize.DataSource = (object) floatList;
    }

    private void InitializePlusMinusEditors()
    {
      this.plusMinusEditorTextHOffset.FormatString = this.isMetric ? "{0:F1}cm" : "{0:F1}\"";
      this.plusMinusEditorTextHOffset.TrimString = this.isMetric ? "cm" : "\"";
      this.plusMinusEditorTextVOffset.FormatString = this.isMetric ? "{0:F1}cm" : "{0:F1}\"";
      this.plusMinusEditorTextVOffset.TrimString = this.isMetric ? "cm" : "\"";
      this.plusMinusEditorImageHOffset.FormatString = this.isMetric ? "{0:F1}cm" : "{0:F1}\"";
      this.plusMinusEditorImageHOffset.TrimString = this.isMetric ? "cm" : "\"";
      this.plusMinusEditorImageVOffset.FormatString = this.isMetric ? "{0:F1}cm" : "{0:F1}\"";
      this.plusMinusEditorImageVOffset.TrimString = this.isMetric ? "cm" : "\"";
      this.plusMinusEditorTextHOffset.MaxValue = (Decimal) this.document.DefaultPageSettings.PaperSize.Width / new Decimal(100);
      this.plusMinusEditorTextVOffset.MaxValue = (Decimal) this.document.DefaultPageSettings.PaperSize.Height / new Decimal(100);
      this.plusMinusEditorImageHOffset.MaxValue = (Decimal) this.document.DefaultPageSettings.PaperSize.Width / new Decimal(100);
      this.plusMinusEditorImageVOffset.MaxValue = (Decimal) this.document.DefaultPageSettings.PaperSize.Height / new Decimal(100);
      if (!this.isMetric)
        return;
      this.plusMinusEditorTextHOffset.MaxValue *= new Decimal(254, 0, 0, false, (byte) 2);
      this.plusMinusEditorTextVOffset.MaxValue *= new Decimal(254, 0, 0, false, (byte) 2);
      this.plusMinusEditorImageHOffset.MaxValue *= new Decimal(254, 0, 0, false, (byte) 2);
      this.plusMinusEditorImageVOffset.MaxValue *= new Decimal(254, 0, 0, false, (byte) 2);
    }

    private FontStyle GetSupportedStyle(FontFamily family)
    {
      FontStyle fontStyle = FontStyle.Regular;
      if (!family.IsStyleAvailable(FontStyle.Regular))
      {
        if (family.IsStyleAvailable(FontStyle.Bold))
          fontStyle = FontStyle.Bold;
        else if (family.IsStyleAvailable(FontStyle.Italic))
          fontStyle = FontStyle.Italic;
        else if (family.IsStyleAvailable(FontStyle.Strikeout))
          fontStyle = FontStyle.Strikeout;
        else if (family.IsStyleAvailable(FontStyle.Underline))
          fontStyle = FontStyle.Underline;
      }
      return fontStyle;
    }

    private FontStyle GetCompositeFontStyle(
      bool bold,
      bool italic,
      bool underline,
      bool strikeout)
    {
      FontStyle fontStyle = FontStyle.Regular;
      if (bold)
        fontStyle |= FontStyle.Bold;
      if (italic)
        fontStyle |= FontStyle.Italic;
      if (underline)
        fontStyle |= FontStyle.Underline;
      if (strikeout)
        fontStyle |= FontStyle.Strikeout;
      return fontStyle;
    }

    private void SetEnabledStateOfFontStyleButtons(FontFamily family)
    {
      this.cbToggleButtonBold.Enabled = family.IsStyleAvailable(FontStyle.Bold);
      this.cbToggleButtonItalic.Enabled = family.IsStyleAvailable(FontStyle.Italic);
      this.cbToggleButtonUnderline.Enabled = family.IsStyleAvailable(FontStyle.Underline);
      this.cbToggleButtonStrikeout.Enabled = family.IsStyleAvailable(FontStyle.Strikeout);
    }

    private void SetToggleStateOfFontStyleButtons(Font font)
    {
      this.cbToggleButtonBold.ToggleState = font.Bold ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      this.cbToggleButtonItalic.ToggleState = font.Italic ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      this.cbToggleButtonUnderline.ToggleState = font.Underline ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
      this.cbToggleButtonStrikeout.ToggleState = font.Strikeout ? Telerik.WinControls.Enumerations.ToggleState.On : Telerik.WinControls.Enumerations.ToggleState.Off;
    }

    private FontStyle GetFontStyleFromButtons()
    {
      FontStyle fontStyle = FontStyle.Regular;
      if (this.cbToggleButtonBold.Enabled && this.cbToggleButtonBold.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        fontStyle |= FontStyle.Bold;
      if (this.cbToggleButtonItalic.Enabled && this.cbToggleButtonItalic.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        fontStyle |= FontStyle.Italic;
      if (this.cbToggleButtonUnderline.Enabled && this.cbToggleButtonUnderline.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        fontStyle |= FontStyle.Underline;
      if (this.cbToggleButtonStrikeout.Enabled && this.cbToggleButtonStrikeout.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        fontStyle |= FontStyle.Strikeout;
      return fontStyle;
    }

    private bool ValidatePages()
    {
      try
      {
        List<int> pageNumbers = this.watermarkPreviewControl1.Watermark.PageNumbers;
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    protected virtual void LocalizeStrings()
    {
      PrintDialogsLocalizationProvider currentProvider = LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProvider;
      this.Text = currentProvider.GetLocalizedString("WatermarkDialogTitle");
      this.radLabelPreview.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelPreview");
      this.radButtonRemoveWatermark.Text = currentProvider.GetLocalizedString("WatermarkDialogButtonRemove");
      this.radButtonOK.Text = currentProvider.GetLocalizedString("WatermarkDialogButtonOK");
      this.radButtonCancel.Text = currentProvider.GetLocalizedString("WatermarkDialogButtonCancel");
      this.groupBoxPosition.HeaderText = currentProvider.GetLocalizedString("WatermarkDialogLabelPosition");
      this.groupBoxPageRange.HeaderText = currentProvider.GetLocalizedString("WatermarkDialogLabelPageRange");
      this.radRadioButtonInFront.Text = currentProvider.GetLocalizedString("WatermarkDialogRadioInFront");
      this.radRadioButtonBehind.Text = currentProvider.GetLocalizedString("WatermarkDialogRadioBehind");
      this.radRadioButtonAllPages.Text = currentProvider.GetLocalizedString("WatermarkDialogRadioAll");
      this.radRadioButtonPages.Text = currentProvider.GetLocalizedString("WatermarkDialogRadioPages");
      this.radLabelDescription.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelPagesDescription");
      this.pageText.Text = currentProvider.GetLocalizedString("WatermarkDialogTabText");
      this.pagePicture.Text = currentProvider.GetLocalizedString("WatermarkDialogTabPicture");
      this.radLabelText.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelText");
      this.radTextBoxWMText.NullText = currentProvider.GetLocalizedString("WatermarkDialogWatermarkText");
      this.radLabelTextHOffset.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelHOffset");
      this.radLabelTextVOffset.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelVOffset");
      this.radLabelTextRotation.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelRotation");
      this.radLabelFont.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelFont");
      this.radLabelFontSize.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelSize");
      this.radLabelColor.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelColor");
      this.radLabelOpacity.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelOpacity");
      this.radLabelLoadImage.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelLoadImage");
      this.radLabelImageHOffset.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelHOffset");
      this.radLabelImageVOffset.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelVOffset");
      this.radLabelImageOpacity.Text = currentProvider.GetLocalizedString("WatermarkDialogLabelOpacity");
      this.radCheckBoxImageTiling.Text = currentProvider.GetLocalizedString("WatermarkDialogCheckboxTiling");
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.ThemeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch")
      {
        this.context.CorrectPositions();
        this.plusMinusEditorTextHOffset.Height = 30;
        this.plusMinusEditorTextVOffset.Height = 30;
        this.plusMinusEditorTextAngle.Height = 30;
        this.plusMinusEditorTextOpacity.Height = 30;
        this.plusMinusEditorImageHOffset.Height = 30;
        this.plusMinusEditorImageVOffset.Height = 30;
        this.plusMinusEditorImageOpacity.Height = 30;
        this.radPageView1.Width += this.groupBoxPageRange.Right - this.radPageView1.Right;
        this.radCommandBar1.Width += 100;
      }
      if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
        return;
      this.context.CorrectPositions();
      this.plusMinusEditorTextHOffset.MinusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorTextHOffset.PlusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorTextVOffset.MinusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorTextVOffset.PlusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorTextAngle.MinusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorTextAngle.PlusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorTextOpacity.MinusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorTextOpacity.PlusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorImageHOffset.MinusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorImageHOffset.PlusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorImageVOffset.MinusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorImageVOffset.PlusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorImageOpacity.MinusButton.ButtonElement.Padding = Padding.Empty;
      this.plusMinusEditorImageOpacity.PlusButton.ButtonElement.Padding = Padding.Empty;
      this.watermarkPreviewControl1.Location = new Point(12, 37);
      this.watermarkPreviewControl1.Size = new Size(191, 207);
      this.groupBoxPosition.Location = new Point(209, 421);
      this.groupBoxPosition.Size = new Size(183, 106);
      this.radRadioButtonBehind.Location = new Point(10, 45);
      this.radRadioButtonBehind.Size = new Size(71, 22);
      this.radRadioButtonInFront.Location = new Point(10, 21);
      this.radRadioButtonInFront.Size = new Size(73, 22);
      this.groupBoxPageRange.Location = new Point(398, 421);
      this.groupBoxPageRange.Size = new Size(325, 106);
      this.radLabelDescription.Location = new Point(223, 47);
      this.radLabelDescription.Size = new Size(97, 21);
      this.radTextBoxPages.Location = new Point(83, 44);
      this.radTextBoxPages.Size = new Size(123, 36);
      this.radRadioButtonPages.Location = new Point(10, 46);
      this.radRadioButtonPages.Size = new Size(67, 22);
      this.radRadioButtonAllPages.Location = new Point(10, 22);
      this.radRadioButtonAllPages.Size = new Size(43, 22);
      this.radPageView1.Location = new Point(209, 12);
      this.radPageView1.Size = new Size(514, 403);
      this.pageText.Controls.Add((Control) this.radDropDownListFontSize);
      this.pageText.Controls.Add((Control) this.radLabelFontSize);
      this.pageText.Location = new Point(6, 55);
      this.pageText.Size = new Size(502, 342);
      this.radCommandBar1.Location = new Point(301, 168);
      this.radCommandBar1.Size = new Size(207, 48);
      this.commandBarStripElement1.Grip.MaxSize = new Size(0, 0);
      this.commandBarStripElement1.Grip.MinSize = new Size(0, 0);
      this.commandBarStripElement1.GetChildAt(0).MinSize = new Size(0, 0);
      this.commandBarStripElement1.GetChildAt(0).MaxSize = new Size(0, 0);
      this.plusMinusEditorTextAngle.Location = new Point(301, 96);
      this.plusMinusEditorTextAngle.Size = new Size(130, 35);
      this.plusMinusEditorTextVOffset.Location = new Point(148, 96);
      this.plusMinusEditorTextVOffset.Size = new Size(130, 37);
      this.plusMinusEditorTextOpacity.Location = new Point(207, 258);
      this.plusMinusEditorTextOpacity.Size = new Size(130, 35);
      this.plusMinusEditorTextHOffset.Location = new Point(0, 96);
      this.plusMinusEditorTextHOffset.Size = new Size(130, 35);
      this.radLabelTextRotation.Location = new Point(295, 69);
      this.radLabelTextRotation.Size = new Size(62, 21);
      this.radLabelTextVOffset.Location = new Point(145, 69);
      this.radLabelTextVOffset.Size = new Size(105, 21);
      this.radLabelTextHOffset.Location = new Point(1, 69);
      this.radLabelTextHOffset.Size = new Size(116, 21);
      this.radColorBoxFontColor.Location = new Point(14, 257);
      this.radColorBoxFontColor.Size = new Size(139, 36);
      this.radLabelColor.Location = new Point(14, 230);
      this.radLabelColor.Size = new Size(46, 21);
      this.radDropDownListFontSize.Location = new Point(173, 182);
      this.radDropDownListFontSize.Size = new Size(94, 37);
      this.radDropDownListFonts.Location = new Point(14, 182);
      this.radDropDownListFonts.Size = new Size(139, 37);
      this.radLabelFontSize.Location = new Point(173, 148);
      this.radLabelFontSize.Size = new Size(38, 21);
      this.radLabelOpacity.Location = new Point(207, 230);
      this.radLabelOpacity.Size = new Size(60, 21);
      this.radLabelFont.Location = new Point(16, 148);
      this.radLabelFont.Size = new Size(40, 21);
      this.radLabelText.Location = new Point(1, 7);
      this.radLabelText.Size = new Size(40, 21);
      this.radTextBoxWMText.Location = new Point(3, 27);
      this.radTextBoxWMText.Size = new Size(319, 36);
      this.pagePicture.Location = new Point(6, 55);
      this.pagePicture.Size = new Size(502, 342);
      this.plusMinusEditorImageVOffset.Location = new Point(161, 101);
      this.plusMinusEditorImageVOffset.Size = new Size(135, 37);
      this.plusMinusEditorImageOpacity.Location = new Point(4, 183);
      this.plusMinusEditorImageOpacity.Size = new Size(130, 37);
      this.plusMinusEditorImageHOffset.Location = new Point(0, 101);
      this.plusMinusEditorImageHOffset.Size = new Size(135, 37);
      this.radCheckBoxImageTiling.Location = new Point(161, 183);
      this.radCheckBoxImageTiling.Size = new Size(60, 19);
      this.radLabelImageOpacity.Location = new Point(4, 156);
      this.radLabelImageOpacity.Size = new Size(56, 21);
      this.radLabelImageVOffset.Location = new Point(161, 74);
      this.radLabelImageVOffset.Size = new Size(99, 21);
      this.radLabelImageHOffset.Location = new Point(4, 74);
      this.radLabelImageHOffset.Size = new Size(116, 21);
      this.radButtonClearImage.Location = new Point(282, 32);
      this.radButtonClearImage.Size = new Size(36, 36);
      this.radLabelLoadImage.Location = new Point(0, 5);
      this.radLabelLoadImage.Size = new Size(86, 21);
      this.radBrowseEditorImage.Location = new Point(0, 32);
      this.radBrowseEditorImage.Size = new Size(276, 36);
      this.radLabelPreview.Location = new Point(9, 14);
      this.radLabelPreview.Size = new Size(58, 21);
      this.radButtonCancel.Location = new Point(629, 536);
      this.radButtonCancel.Size = new Size(94, 36);
      this.radButtonOK.Location = new Point(529, 536);
      this.radButtonOK.Size = new Size(94, 36);
      this.radButtonRemoveWatermark.Location = new Point(14, 250);
      this.radButtonRemoveWatermark.Size = new Size(189, 53);
      this.Size = new Size(737, 621);
    }

    private void WatermarkPreviewDialog_ThemeNameChanged(
      object source,
      ThemeNameChangedEventArgs args)
    {
      ThemeResolutionService.ApplyThemeToControlTree((Control) this, args.newThemeName);
    }

    private void PrintingDialogsLocalizationProvider_CurrentProviderChanged(
      object sender,
      EventArgs e)
    {
      this.LocalizeStrings();
    }

    private void radButtonClearImage_Click(object sender, EventArgs e)
    {
      this.radBrowseEditorImage.Value = (string) null;
      this.watermarkPreviewControl1.Watermark.ImagePath = string.Empty;
      this.watermarkPreviewControl1.Refresh();
    }

    private void radTextBoxPages_TextChanged(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.radTextBoxPages.Text))
      {
        this.radRadioButtonAllPages.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
        this.watermarkPreviewControl1.Watermark.Pages = (string) null;
      }
      else
      {
        this.radRadioButtonPages.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
        this.watermarkPreviewControl1.Watermark.Pages = this.radTextBoxPages.Text;
      }
    }

    private void radRadioButtonBehind_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      if (args.ToggleState != Telerik.WinControls.Enumerations.ToggleState.On)
        return;
      this.watermarkPreviewControl1.Watermark.DrawInFront = false;
    }

    private void radRadioButtonInFront_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      if (args.ToggleState != Telerik.WinControls.Enumerations.ToggleState.On)
        return;
      this.watermarkPreviewControl1.Watermark.DrawInFront = true;
    }

    private void radRadioButtonPages_ToggleStateChanged(object sender, StateChangedEventArgs args)
    {
      if (args.ToggleState != Telerik.WinControls.Enumerations.ToggleState.On)
        return;
      this.watermarkPreviewControl1.Watermark.Pages = this.radTextBoxPages.Text;
      this.radTextBoxPages.Select();
    }

    private void radRadioButtonAllPages_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.watermarkPreviewControl1.Watermark.AllPages = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
    }

    private void radButtonCancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.UnwireEvents();
      this.Close();
    }

    private void radButtonOK_Click(object sender, EventArgs e)
    {
      bool flag = this.ValidatePages();
      if (this.radRadioButtonAllPages.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On || flag)
      {
        if (!flag)
          this.watermarkPreviewControl1.Watermark.Pages = string.Empty;
        this.document.Watermark = this.watermarkPreviewControl1.Watermark;
        this.DialogResult = DialogResult.OK;
        this.UnwireEvents();
        this.Close();
      }
      else
      {
        string themeName = RadMessageBox.ThemeName;
        RadMessageBox.ThemeName = this.ThemeName;
        int num = (int) RadMessageBox.Show("The provided pages string is not valid");
        RadMessageBox.ThemeName = themeName;
      }
    }

    private void plusMinusEditorImageOpacity_ValueChanged(object sender, EventArgs e)
    {
      this.watermarkPreviewControl1.Watermark.ImageOpacity = (byte) (this.plusMinusEditorImageOpacity.Value * new Decimal((int) byte.MaxValue, 0, 0, false, (byte) 2));
      this.watermarkPreviewControl1.Refresh();
    }

    private void plusMinusEditorImageVOffset_ValueChanged(object sender, EventArgs e)
    {
      Decimal num = this.plusMinusEditorImageVOffset.Value * new Decimal(100);
      if (this.isMetric)
        num /= new Decimal(254, 0, 0, false, (byte) 2);
      this.watermarkPreviewControl1.Watermark.ImageVOffset = (int) num;
      this.watermarkPreviewControl1.Refresh();
    }

    private void plusMinusEditorImageHOffset_ValueChanged(object sender, EventArgs e)
    {
      Decimal num = this.plusMinusEditorImageHOffset.Value * new Decimal(100);
      if (this.isMetric)
        num /= new Decimal(254, 0, 0, false, (byte) 2);
      this.watermarkPreviewControl1.Watermark.ImageHOffset = (int) num;
      this.watermarkPreviewControl1.Refresh();
    }

    private void plusMinusEditorTextOpacity_ValueChanged(object sender, EventArgs e)
    {
      this.watermarkPreviewControl1.Watermark.TextOpacity = (byte) (this.plusMinusEditorTextOpacity.Value * new Decimal((int) byte.MaxValue, 0, 0, false, (byte) 2));
      this.watermarkPreviewControl1.Refresh();
    }

    private void plusMinusEditorTextAngle_ValueChanged(object sender, EventArgs e)
    {
      this.watermarkPreviewControl1.Watermark.TextAngle = (float) this.plusMinusEditorTextAngle.Value;
      this.watermarkPreviewControl1.Refresh();
    }

    private void plusMinusEditorTextVOffset_ValueChanged(object sender, EventArgs e)
    {
      Decimal num = this.plusMinusEditorTextVOffset.Value * new Decimal(100);
      if (this.isMetric)
        num /= new Decimal(254, 0, 0, false, (byte) 2);
      this.watermarkPreviewControl1.Watermark.TextVOffset = (int) num;
      this.watermarkPreviewControl1.Refresh();
    }

    private void plusMinusEditorTextHOffset_ValueChanged(object sender, EventArgs e)
    {
      Decimal num = this.plusMinusEditorTextHOffset.Value * new Decimal(100);
      if (this.isMetric)
        num /= new Decimal(254, 0, 0, false, (byte) 2);
      this.watermarkPreviewControl1.Watermark.TextHOffset = (int) num;
      this.watermarkPreviewControl1.Refresh();
    }

    private void radCheckBoxImageTiling_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.watermarkPreviewControl1.Watermark.ImageTiling = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
      this.watermarkPreviewControl1.Refresh();
    }

    private void radBrowseEditorImage_ValueChanged(object sender, EventArgs e)
    {
      if (File.Exists(this.radBrowseEditorImage.Value))
      {
        this.watermarkPreviewControl1.Watermark.ImagePath = this.radBrowseEditorImage.Value;
        this.watermarkPreviewControl1.Refresh();
      }
      else
      {
        if (this.radBrowseEditorImage.Value != null)
          return;
        this.watermarkPreviewControl1.Watermark.ImagePath = string.Empty;
      }
    }

    private void radColorBoxFontColor_ValueChanged(object sender, EventArgs e)
    {
      Color color = this.radColorBoxFontColor.Value;
      this.watermarkPreviewControl1.Watermark.ForeColor = this.radColorBoxFontColor.Value;
      this.watermarkPreviewControl1.Refresh();
    }

    private void radDropDownListFontSize_TextChanged(object sender, EventArgs e)
    {
      float result;
      if (this.radDropDownListFonts.SelectedValue == null || !float.TryParse(this.radDropDownListFontSize.Text, out result))
        return;
      FontFamily fontFamily = this.watermarkPreviewControl1.Watermark.Font.FontFamily;
      FontStyle style = this.watermarkPreviewControl1.Watermark.Font.Style;
      this.watermarkPreviewControl1.Watermark.Font = new Font(fontFamily, result, style);
      this.watermarkPreviewControl1.Refresh();
    }

    private void radDropDownListFonts_SelectedIndexChanged(
      object sender,
      Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      int result;
      if (e.Position < 0 || this.radDropDownListFontSize.SelectedValue == null || !int.TryParse(this.radDropDownListFontSize.SelectedValue.ToString(), out result))
        return;
      FontFamily dataBoundItem = (FontFamily) this.radDropDownListFonts.Items[e.Position].DataBoundItem;
      this.SetEnabledStateOfFontStyleButtons(dataBoundItem);
      FontStyle style = this.GetFontStyleFromButtons();
      if (!dataBoundItem.IsStyleAvailable(style))
        style = this.GetSupportedStyle(dataBoundItem);
      this.UnwireToggleButtons();
      Font font = new Font(dataBoundItem, (float) result, style);
      this.SetToggleStateOfFontStyleButtons(font);
      this.watermarkPreviewControl1.Watermark.Font = font;
      this.watermarkPreviewControl1.Refresh();
      this.WireToggleButtons();
    }

    private void radDropDownListFonts_VisualListItemFormatting(
      object sender,
      VisualItemFormattingEventArgs args)
    {
      FontFamily dataBoundItem = (FontFamily) args.VisualItem.Data.DataBoundItem;
      FontStyle supportedStyle = this.GetSupportedStyle(dataBoundItem);
      args.VisualItem.Font = new Font(dataBoundItem, 11f, supportedStyle);
    }

    private void radTextBoxWMText_TextChanging(object sender, TextChangingEventArgs e)
    {
      this.watermarkPreviewControl1.Watermark.Text = e.NewValue;
      this.watermarkPreviewControl1.Refresh();
    }

    private void cbToggleButtonStrikeout_ToggleStateChanging(
      object sender,
      StateChangingEventArgs args)
    {
      FontStyle style = this.watermarkPreviewControl1.Watermark.Font.Style;
      bool bold = (style | FontStyle.Bold) == style;
      bool italic = (style | FontStyle.Italic) == style;
      bool underline = (style | FontStyle.Underline) == style;
      FontStyle fontStyle = args.NewValue != Telerik.WinControls.Enumerations.ToggleState.On ? this.GetCompositeFontStyle(bold, italic, underline, false) : this.GetCompositeFontStyle(bold, italic, underline, true);
      if (!this.watermarkPreviewControl1.Watermark.Font.FontFamily.IsStyleAvailable(fontStyle))
      {
        args.Cancel = true;
      }
      else
      {
        this.watermarkPreviewControl1.Watermark.Font = new Font(this.watermarkPreviewControl1.Watermark.Font, fontStyle);
        this.watermarkPreviewControl1.Refresh();
      }
    }

    private void cbToggleButtonUnderline_ToggleStateChanging(
      object sender,
      StateChangingEventArgs args)
    {
      FontStyle style = this.watermarkPreviewControl1.Watermark.Font.Style;
      bool bold = (style | FontStyle.Bold) == style;
      bool italic = (style | FontStyle.Italic) == style;
      bool strikeout = (style | FontStyle.Strikeout) == style;
      FontStyle fontStyle = args.NewValue != Telerik.WinControls.Enumerations.ToggleState.On ? this.GetCompositeFontStyle(bold, italic, false, strikeout) : this.GetCompositeFontStyle(bold, italic, true, strikeout);
      if (!this.watermarkPreviewControl1.Watermark.Font.FontFamily.IsStyleAvailable(fontStyle))
      {
        args.Cancel = true;
      }
      else
      {
        this.watermarkPreviewControl1.Watermark.Font = new Font(this.watermarkPreviewControl1.Watermark.Font, fontStyle);
        this.watermarkPreviewControl1.Refresh();
      }
    }

    private void cbToggleButtonItalic_ToggleStateChanging(
      object sender,
      StateChangingEventArgs args)
    {
      FontStyle style = this.watermarkPreviewControl1.Watermark.Font.Style;
      bool bold = (style | FontStyle.Bold) == style;
      bool underline = (style | FontStyle.Underline) == style;
      bool strikeout = (style | FontStyle.Strikeout) == style;
      FontStyle fontStyle = args.NewValue != Telerik.WinControls.Enumerations.ToggleState.On ? this.GetCompositeFontStyle(bold, false, underline, strikeout) : this.GetCompositeFontStyle(bold, true, underline, strikeout);
      if (!this.watermarkPreviewControl1.Watermark.Font.FontFamily.IsStyleAvailable(fontStyle))
      {
        args.Cancel = true;
      }
      else
      {
        this.watermarkPreviewControl1.Watermark.Font = new Font(this.watermarkPreviewControl1.Watermark.Font, fontStyle);
        this.watermarkPreviewControl1.Refresh();
      }
    }

    private void cbToggleButtonBold_ToggleStateChanging(object sender, StateChangingEventArgs args)
    {
      FontStyle style = this.watermarkPreviewControl1.Watermark.Font.Style;
      bool italic = (style | FontStyle.Italic) == style;
      bool underline = (style | FontStyle.Underline) == style;
      bool strikeout = (style | FontStyle.Strikeout) == style;
      FontStyle fontStyle = args.NewValue != Telerik.WinControls.Enumerations.ToggleState.On ? this.GetCompositeFontStyle(false, italic, underline, strikeout) : this.GetCompositeFontStyle(true, italic, underline, strikeout);
      if (!this.watermarkPreviewControl1.Watermark.Font.FontFamily.IsStyleAvailable(fontStyle))
      {
        args.Cancel = true;
      }
      else
      {
        this.watermarkPreviewControl1.Watermark.Font = new Font(this.watermarkPreviewControl1.Watermark.Font, fontStyle);
        this.watermarkPreviewControl1.Refresh();
      }
    }

    private void radButtonRemoveWatermark_Click(object sender, EventArgs e)
    {
      this.radTextBoxWMText.Text = (string) null;
      this.plusMinusEditorTextHOffset.Value = new Decimal(0);
      this.plusMinusEditorTextVOffset.Value = new Decimal(0);
      this.plusMinusEditorTextOpacity.Value = new Decimal(50);
      this.plusMinusEditorTextAngle.Value = new Decimal(0);
      this.radColorBoxFontColor.Value = Color.Black;
      this.radDropDownListFonts.SelectedValue = (object) Control.DefaultFont.FontFamily.Name;
      this.radDropDownListFontSize.SelectedItem = this.radDropDownListFontSize.Items.Last;
      this.cbToggleButtonBold.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
      this.cbToggleButtonItalic.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
      this.cbToggleButtonUnderline.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
      this.cbToggleButtonStrikeout.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
      this.radRadioButtonBehind.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.radTextBoxPages.Text = string.Empty;
      this.radRadioButtonAllPages.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.radBrowseEditorImage.Value = (string) null;
      this.plusMinusEditorImageHOffset.Value = new Decimal(0);
      this.plusMinusEditorImageVOffset.Value = new Decimal(0);
      this.plusMinusEditorImageOpacity.Value = new Decimal(50);
      this.radCheckBoxImageTiling.IsChecked = false;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData != Keys.Escape)
        return base.ProcessCmdKey(ref msg, keyData);
      this.DialogResult = DialogResult.Cancel;
      this.UnwireEvents();
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WatermarkPreviewDialog));
      this.watermarkPreviewControl1 = new WatermarkPreviewControl();
      this.groupBoxPosition = new RadGroupBox();
      this.radRadioButtonBehind = new RadRadioButton();
      this.radRadioButtonInFront = new RadRadioButton();
      this.groupBoxPageRange = new RadGroupBox();
      this.radLabelDescription = new RadLabel();
      this.radTextBoxPages = new RadTextBox();
      this.radRadioButtonPages = new RadRadioButton();
      this.radRadioButtonAllPages = new RadRadioButton();
      this.radPageView1 = new RadPageView();
      this.pageText = new RadPageViewPage();
      this.radCommandBar1 = new RadCommandBar();
      this.commandBarRowElement1 = new CommandBarRowElement();
      this.commandBarStripElement1 = new CommandBarStripElement();
      this.cbToggleButtonBold = new CommandBarToggleButton();
      this.cbToggleButtonItalic = new CommandBarToggleButton();
      this.cbToggleButtonUnderline = new CommandBarToggleButton();
      this.cbToggleButtonStrikeout = new CommandBarToggleButton();
      this.plusMinusEditorTextAngle = new PlusMinusEditor();
      this.plusMinusEditorTextVOffset = new PlusMinusEditor();
      this.plusMinusEditorTextOpacity = new PlusMinusEditor();
      this.plusMinusEditorTextHOffset = new PlusMinusEditor();
      this.radLabelTextRotation = new RadLabel();
      this.radLabelTextVOffset = new RadLabel();
      this.radLabelTextHOffset = new RadLabel();
      this.radColorBoxFontColor = new RadColorBox();
      this.radLabelColor = new RadLabel();
      this.radDropDownListFontSize = new RadDropDownList();
      this.radDropDownListFonts = new RadDropDownList();
      this.radLabelFontSize = new RadLabel();
      this.radLabelOpacity = new RadLabel();
      this.radLabelFont = new RadLabel();
      this.radLabelText = new RadLabel();
      this.radTextBoxWMText = new RadTextBox();
      this.pagePicture = new RadPageViewPage();
      this.plusMinusEditorImageVOffset = new PlusMinusEditor();
      this.plusMinusEditorImageOpacity = new PlusMinusEditor();
      this.plusMinusEditorImageHOffset = new PlusMinusEditor();
      this.radCheckBoxImageTiling = new RadCheckBox();
      this.radLabelImageOpacity = new RadLabel();
      this.radLabelImageVOffset = new RadLabel();
      this.radLabelImageHOffset = new RadLabel();
      this.radButtonClearImage = new RadButton();
      this.radLabelLoadImage = new RadLabel();
      this.radBrowseEditorImage = new RadBrowseEditor();
      this.radLabelPreview = new RadLabel();
      this.radButtonCancel = new RadButton();
      this.radButtonOK = new RadButton();
      this.radButtonRemoveWatermark = new RadButton();
      this.watermarkPreviewControl1.BeginInit();
      this.groupBoxPosition.BeginInit();
      this.groupBoxPosition.SuspendLayout();
      this.radRadioButtonBehind.BeginInit();
      this.radRadioButtonInFront.BeginInit();
      this.groupBoxPageRange.BeginInit();
      this.groupBoxPageRange.SuspendLayout();
      this.radLabelDescription.BeginInit();
      this.radTextBoxPages.BeginInit();
      this.radRadioButtonPages.BeginInit();
      this.radRadioButtonAllPages.BeginInit();
      this.radPageView1.BeginInit();
      this.radPageView1.SuspendLayout();
      this.pageText.SuspendLayout();
      this.radCommandBar1.BeginInit();
      this.radLabelTextRotation.BeginInit();
      this.radLabelTextVOffset.BeginInit();
      this.radLabelTextHOffset.BeginInit();
      this.radColorBoxFontColor.BeginInit();
      this.radLabelColor.BeginInit();
      this.radDropDownListFontSize.BeginInit();
      this.radDropDownListFonts.BeginInit();
      this.radLabelFontSize.BeginInit();
      this.radLabelOpacity.BeginInit();
      this.radLabelFont.BeginInit();
      this.radLabelText.BeginInit();
      this.radTextBoxWMText.BeginInit();
      this.pagePicture.SuspendLayout();
      this.radCheckBoxImageTiling.BeginInit();
      this.radLabelImageOpacity.BeginInit();
      this.radLabelImageVOffset.BeginInit();
      this.radLabelImageHOffset.BeginInit();
      this.radButtonClearImage.BeginInit();
      this.radLabelLoadImage.BeginInit();
      this.radBrowseEditorImage.BeginInit();
      this.radLabelPreview.BeginInit();
      this.radButtonCancel.BeginInit();
      this.radButtonOK.BeginInit();
      this.radButtonRemoveWatermark.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.watermarkPreviewControl1.Location = new Point(12, 37);
      this.watermarkPreviewControl1.Name = "watermarkPreviewControl1";
      this.watermarkPreviewControl1.PaperSize = (PaperSize) null;
      this.watermarkPreviewControl1.Size = new Size(165, 207);
      this.watermarkPreviewControl1.TabIndex = 0;
      this.watermarkPreviewControl1.Watermark = (RadPrintWatermark) null;
      this.groupBoxPosition.AccessibleRole = AccessibleRole.Grouping;
      this.groupBoxPosition.Controls.Add((Control) this.radRadioButtonBehind);
      this.groupBoxPosition.Controls.Add((Control) this.radRadioButtonInFront);
      this.groupBoxPosition.HeaderText = "Position";
      this.groupBoxPosition.Location = new Point(188, 284);
      this.groupBoxPosition.Name = "groupBoxPosition";
      this.groupBoxPosition.Size = new Size(91, 80);
      this.groupBoxPosition.TabIndex = 1;
      this.groupBoxPosition.Text = "Position";
      this.radRadioButtonBehind.Location = new Point(10, 45);
      this.radRadioButtonBehind.Name = "radRadioButtonBehind";
      this.radRadioButtonBehind.Size = new Size(55, 18);
      this.radRadioButtonBehind.TabIndex = 0;
      this.radRadioButtonBehind.Text = "Behind";
      this.radRadioButtonInFront.CheckState = CheckState.Checked;
      this.radRadioButtonInFront.Location = new Point(10, 21);
      this.radRadioButtonInFront.Name = "radRadioButtonInFront";
      this.radRadioButtonInFront.Size = new Size(57, 18);
      this.radRadioButtonInFront.TabIndex = 0;
      this.radRadioButtonInFront.Text = "In front";
      this.radRadioButtonInFront.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.groupBoxPageRange.AccessibleRole = AccessibleRole.Grouping;
      this.groupBoxPageRange.Controls.Add((Control) this.radLabelDescription);
      this.groupBoxPageRange.Controls.Add((Control) this.radTextBoxPages);
      this.groupBoxPageRange.Controls.Add((Control) this.radRadioButtonPages);
      this.groupBoxPageRange.Controls.Add((Control) this.radRadioButtonAllPages);
      this.groupBoxPageRange.HeaderText = "Page range";
      this.groupBoxPageRange.Location = new Point(285, 284);
      this.groupBoxPageRange.Name = "groupBoxPageRange";
      this.groupBoxPageRange.Size = new Size(244, 80);
      this.groupBoxPageRange.TabIndex = 2;
      this.groupBoxPageRange.Text = "Page range";
      this.radLabelDescription.Location = new Point(165, 47);
      this.radLabelDescription.Name = "radLabelDescription";
      this.radLabelDescription.Size = new Size(74, 18);
      this.radLabelDescription.TabIndex = 2;
      this.radLabelDescription.Text = "(e.g. 1,3,5-12)";
      this.radTextBoxPages.Location = new Point(71, 45);
      this.radTextBoxPages.Name = "radTextBoxPages";
      this.radTextBoxPages.Size = new Size(91, 20);
      this.radTextBoxPages.TabIndex = 1;
      this.radTextBoxPages.TabStop = false;
      this.radRadioButtonPages.Location = new Point(10, 46);
      this.radRadioButtonPages.Name = "radRadioButtonPages";
      this.radRadioButtonPages.Size = new Size(50, 18);
      this.radRadioButtonPages.TabIndex = 0;
      this.radRadioButtonPages.Text = "Pages";
      this.radRadioButtonAllPages.CheckState = CheckState.Checked;
      this.radRadioButtonAllPages.Location = new Point(10, 22);
      this.radRadioButtonAllPages.Name = "radRadioButtonAllPages";
      this.radRadioButtonAllPages.Size = new Size(33, 18);
      this.radRadioButtonAllPages.TabIndex = 0;
      this.radRadioButtonAllPages.Text = "All";
      this.radRadioButtonAllPages.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
      this.radPageView1.Controls.Add((Control) this.pageText);
      this.radPageView1.Controls.Add((Control) this.pagePicture);
      this.radPageView1.Location = new Point(183, 12);
      this.radPageView1.Name = "radPageView1";
      this.radPageView1.SelectedPage = this.pageText;
      this.radPageView1.Size = new Size(351, 267);
      this.radPageView1.TabIndex = 3;
      this.radPageView1.Text = "radPageView1";
      ((RadPageViewStripElement) this.radPageView1.GetChildAt(0)).StripButtons = StripViewButtons.None;
      ((RadPageViewStripElement) this.radPageView1.GetChildAt(0)).ItemAlignment = StripViewItemAlignment.Near;
      ((RadPageViewStripElement) this.radPageView1.GetChildAt(0)).ItemFitMode = StripViewItemFitMode.None;
      this.pageText.Controls.Add((Control) this.radCommandBar1);
      this.pageText.Controls.Add((Control) this.plusMinusEditorTextAngle);
      this.pageText.Controls.Add((Control) this.plusMinusEditorTextVOffset);
      this.pageText.Controls.Add((Control) this.plusMinusEditorTextOpacity);
      this.pageText.Controls.Add((Control) this.plusMinusEditorTextHOffset);
      this.pageText.Controls.Add((Control) this.radLabelTextRotation);
      this.pageText.Controls.Add((Control) this.radLabelTextVOffset);
      this.pageText.Controls.Add((Control) this.radLabelTextHOffset);
      this.pageText.Controls.Add((Control) this.radColorBoxFontColor);
      this.pageText.Controls.Add((Control) this.radLabelColor);
      this.pageText.Controls.Add((Control) this.radDropDownListFontSize);
      this.pageText.Controls.Add((Control) this.radDropDownListFonts);
      this.pageText.Controls.Add((Control) this.radLabelFontSize);
      this.pageText.Controls.Add((Control) this.radLabelOpacity);
      this.pageText.Controls.Add((Control) this.radLabelFont);
      this.pageText.Controls.Add((Control) this.radLabelText);
      this.pageText.Controls.Add((Control) this.radTextBoxWMText);
      this.pageText.ItemSize = new SizeF(37f, 28f);
      this.pageText.Location = new Point(10, 37);
      this.pageText.Name = "pageText";
      this.pageText.Size = new Size(330, 219);
      this.pageText.Text = "Text";
      this.radCommandBar1.BackColor = Color.Transparent;
      this.radCommandBar1.Location = new Point(212, 122);
      this.radCommandBar1.Name = "radCommandBar1";
      this.radCommandBar1.Rows.AddRange(this.commandBarRowElement1);
      this.radCommandBar1.Size = new Size(112, 30);
      this.radCommandBar1.TabIndex = 6;
      this.radCommandBar1.Text = "radCommandBar1";
      this.commandBarRowElement1.BackColor = SystemColors.ControlLightLight;
      this.commandBarRowElement1.DisplayName = (string) null;
      this.commandBarRowElement1.MinSize = new Size(25, 25);
      this.commandBarRowElement1.Strips.AddRange(this.commandBarStripElement1);
      this.commandBarStripElement1.DisplayName = "commandBarStripElement1";
      this.commandBarStripElement1.Grip.MaxSize = new Size(2, 0);
      this.commandBarStripElement1.Grip.MinSize = new Size(2, 25);
      this.commandBarStripElement1.Grip.Visibility = ElementVisibility.Hidden;
      this.commandBarStripElement1.Items.AddRange((RadCommandBarBaseItem) this.cbToggleButtonBold, (RadCommandBarBaseItem) this.cbToggleButtonItalic, (RadCommandBarBaseItem) this.cbToggleButtonUnderline, (RadCommandBarBaseItem) this.cbToggleButtonStrikeout);
      this.commandBarStripElement1.Name = "commandBarStripElement1";
      this.commandBarStripElement1.OverflowButton.Visibility = ElementVisibility.Collapsed;
      this.commandBarStripElement1.ShowHorizontalLine = false;
      this.commandBarStripElement1.Text = "";
      this.commandBarStripElement1.GetChildAt(0).Visibility = ElementVisibility.Hidden;
      this.commandBarStripElement1.GetChildAt(0).MinSize = new Size(2, 25);
      this.commandBarStripElement1.GetChildAt(0).MaxSize = new Size(2, 0);
      this.commandBarStripElement1.GetChildAt(2).Visibility = ElementVisibility.Collapsed;
      this.cbToggleButtonBold.AccessibleDescription = "commandBarToggleButton1";
      this.cbToggleButtonBold.AccessibleName = "commandBarToggleButton1";
      this.cbToggleButtonBold.DisplayName = "commandBarToggleButton1";
      this.cbToggleButtonBold.Image = (Image) componentResourceManager.GetObject("cbToggleButtonBold.Image");
      this.cbToggleButtonBold.Name = "cbToggleButtonBold";
      this.cbToggleButtonBold.Text = "commandBarToggleButton1";
      this.cbToggleButtonItalic.AccessibleDescription = "commandBarToggleButton2";
      this.cbToggleButtonItalic.AccessibleName = "commandBarToggleButton2";
      this.cbToggleButtonItalic.DisplayName = "commandBarToggleButton2";
      this.cbToggleButtonItalic.Image = (Image) componentResourceManager.GetObject("cbToggleButtonItalic.Image");
      this.cbToggleButtonItalic.Name = "cbToggleButtonItalic";
      this.cbToggleButtonItalic.Text = "commandBarToggleButton2";
      this.cbToggleButtonUnderline.AccessibleDescription = "commandBarToggleButton3";
      this.cbToggleButtonUnderline.AccessibleName = "commandBarToggleButton3";
      this.cbToggleButtonUnderline.DisplayName = "commandBarToggleButton3";
      this.cbToggleButtonUnderline.Image = (Image) componentResourceManager.GetObject("cbToggleButtonUnderline.Image");
      this.cbToggleButtonUnderline.Name = "cbToggleButtonUnderline";
      this.cbToggleButtonUnderline.Text = "commandBarToggleButton3";
      this.cbToggleButtonStrikeout.AccessibleDescription = "commandBarToggleButton4";
      this.cbToggleButtonStrikeout.AccessibleName = "commandBarToggleButton4";
      this.cbToggleButtonStrikeout.DisplayName = "commandBarToggleButton4";
      this.cbToggleButtonStrikeout.Image = (Image) componentResourceManager.GetObject("cbToggleButtonStrikeout.Image");
      this.cbToggleButtonStrikeout.Name = "cbToggleButtonStrikeout";
      this.cbToggleButtonStrikeout.Text = "commandBarToggleButton4";
      this.plusMinusEditorTextAngle.FormatString = "{0}°";
      this.plusMinusEditorTextAngle.TrimString = "°";
      this.plusMinusEditorTextAngle.Location = new Point(216, 77);
      this.plusMinusEditorTextAngle.Loop = true;
      this.plusMinusEditorTextAngle.MaxValue = new Decimal(new int[4]
      {
        359,
        0,
        0,
        0
      });
      this.plusMinusEditorTextAngle.MinValue = new Decimal(new int[4]);
      this.plusMinusEditorTextAngle.Name = "plusMinusEditorTextAngle";
      this.plusMinusEditorTextAngle.Size = new Size(95, 21);
      this.plusMinusEditorTextAngle.Step = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.plusMinusEditorTextAngle.TabIndex = 10;
      this.plusMinusEditorTextAngle.Value = new Decimal(new int[4]);
      this.plusMinusEditorTextVOffset.FormatString = "{0:F1}''";
      this.plusMinusEditorTextVOffset.TrimString = "''";
      this.plusMinusEditorTextVOffset.Location = new Point(110, 77);
      this.plusMinusEditorTextVOffset.MaxValue = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.plusMinusEditorTextVOffset.MinValue = new Decimal(new int[4]);
      this.plusMinusEditorTextVOffset.Name = "plusMinusEditorTextVOffset";
      this.plusMinusEditorTextVOffset.Size = new Size(95, 21);
      this.plusMinusEditorTextVOffset.Step = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.plusMinusEditorTextVOffset.TabIndex = 10;
      this.plusMinusEditorTextVOffset.Value = new Decimal(new int[4]);
      this.plusMinusEditorTextOpacity.FormatString = "{0}%";
      this.plusMinusEditorTextOpacity.TrimString = "%";
      this.plusMinusEditorTextOpacity.Location = new Point(148, 178);
      this.plusMinusEditorTextOpacity.MaxValue = new Decimal(new int[4]
      {
        100,
        0,
        0,
        0
      });
      this.plusMinusEditorTextOpacity.MinValue = new Decimal(new int[4]);
      this.plusMinusEditorTextOpacity.Name = "plusMinusEditorTextOpacity";
      this.plusMinusEditorTextOpacity.Size = new Size(95, 21);
      this.plusMinusEditorTextOpacity.Step = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.plusMinusEditorTextOpacity.TabIndex = 10;
      this.plusMinusEditorTextOpacity.Value = new Decimal(new int[4]);
      this.plusMinusEditorTextHOffset.FormatString = "{0:F1}''";
      this.plusMinusEditorTextHOffset.TrimString = "''";
      this.plusMinusEditorTextHOffset.Location = new Point(4, 77);
      this.plusMinusEditorTextHOffset.MaxValue = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.plusMinusEditorTextHOffset.MinValue = new Decimal(new int[4]);
      this.plusMinusEditorTextHOffset.Name = "plusMinusEditorTextHOffset";
      this.plusMinusEditorTextHOffset.Size = new Size(95, 21);
      this.plusMinusEditorTextHOffset.Step = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.plusMinusEditorTextHOffset.TabIndex = 10;
      this.plusMinusEditorTextHOffset.Value = new Decimal(new int[4]);
      this.radLabelTextRotation.BackColor = Color.Transparent;
      this.radLabelTextRotation.Location = new Point(213, 59);
      this.radLabelTextRotation.Name = "radLabelTextRotation";
      this.radLabelTextRotation.Size = new Size(49, 18);
      this.radLabelTextRotation.TabIndex = 8;
      this.radLabelTextRotation.Text = "Rotation";
      this.radLabelTextVOffset.BackColor = Color.Transparent;
      this.radLabelTextVOffset.Location = new Point(108, 59);
      this.radLabelTextVOffset.Name = "radLabelTextVOffset";
      this.radLabelTextVOffset.Size = new Size(75, 18);
      this.radLabelTextVOffset.TabIndex = 8;
      this.radLabelTextVOffset.Text = "Vertical offset";
      this.radLabelTextHOffset.BackColor = Color.Transparent;
      this.radLabelTextHOffset.Location = new Point(1, 59);
      this.radLabelTextHOffset.Name = "radLabelTextHOffset";
      this.radLabelTextHOffset.Size = new Size(89, 18);
      this.radLabelTextHOffset.TabIndex = 8;
      this.radLabelTextHOffset.Text = "Horizontal offset";
      this.radColorBoxFontColor.Location = new Point(3, 178);
      this.radColorBoxFontColor.Name = "radColorBoxFontColor";
      this.radColorBoxFontColor.Size = new Size(139, 20);
      this.radColorBoxFontColor.TabIndex = 6;
      this.radColorBoxFontColor.Value = Color.Black;
      this.radLabelColor.BackColor = Color.Transparent;
      this.radLabelColor.Location = new Point(1, 160);
      this.radLabelColor.Name = "radLabelColor";
      this.radLabelColor.Size = new Size(36, 18);
      this.radLabelColor.TabIndex = 5;
      this.radLabelColor.Text = "Color:";
      this.radDropDownListFontSize.Location = new Point(148, 128);
      this.radDropDownListFontSize.Name = "radDropDownListFontSize";
      this.radDropDownListFontSize.NullText = "Font size";
      this.radDropDownListFontSize.Size = new Size(58, 20);
      this.radDropDownListFontSize.TabIndex = 4;
      this.radDropDownListFonts.Location = new Point(3, 128);
      this.radDropDownListFonts.Name = "radDropDownListFonts";
      this.radDropDownListFonts.NullText = "Select font";
      this.radDropDownListFonts.Size = new Size(139, 20);
      this.radDropDownListFonts.TabIndex = 3;
      this.radLabelFontSize.BackColor = Color.Transparent;
      this.radLabelFontSize.Location = new Point(145, 110);
      this.radLabelFontSize.Name = "radLabelFontSize";
      this.radLabelFontSize.Size = new Size(28, 18);
      this.radLabelFontSize.TabIndex = 2;
      this.radLabelFontSize.Text = "Size:";
      this.radLabelOpacity.BackColor = Color.Transparent;
      this.radLabelOpacity.Location = new Point(146, 160);
      this.radLabelOpacity.Name = "radLabelOpacity";
      this.radLabelOpacity.Size = new Size(47, 18);
      this.radLabelOpacity.TabIndex = 2;
      this.radLabelOpacity.Text = "Opacity:";
      this.radLabelFont.BackColor = Color.Transparent;
      this.radLabelFont.Location = new Point(0, 110);
      this.radLabelFont.Name = "radLabelFont";
      this.radLabelFont.Size = new Size(31, 18);
      this.radLabelFont.TabIndex = 2;
      this.radLabelFont.Text = "Font:";
      this.radLabelText.BackColor = Color.Transparent;
      this.radLabelText.Location = new Point(1, 7);
      this.radLabelText.Name = "radLabelText";
      this.radLabelText.Size = new Size(30, 18);
      this.radLabelText.TabIndex = 1;
      this.radLabelText.Text = "Text:";
      this.radTextBoxWMText.Location = new Point(3, 27);
      this.radTextBoxWMText.Name = "radTextBoxWMText";
      this.radTextBoxWMText.NullText = "Watermark text";
      this.radTextBoxWMText.Size = new Size(319, 20);
      this.radTextBoxWMText.TabIndex = 0;
      this.radTextBoxWMText.TabStop = false;
      this.radTextBoxWMText.TextChanging += new TextChangingEventHandler(this.radTextBoxWMText_TextChanging);
      this.pagePicture.Controls.Add((Control) this.plusMinusEditorImageVOffset);
      this.pagePicture.Controls.Add((Control) this.plusMinusEditorImageOpacity);
      this.pagePicture.Controls.Add((Control) this.plusMinusEditorImageHOffset);
      this.pagePicture.Controls.Add((Control) this.radCheckBoxImageTiling);
      this.pagePicture.Controls.Add((Control) this.radLabelImageOpacity);
      this.pagePicture.Controls.Add((Control) this.radLabelImageVOffset);
      this.pagePicture.Controls.Add((Control) this.radLabelImageHOffset);
      this.pagePicture.Controls.Add((Control) this.radButtonClearImage);
      this.pagePicture.Controls.Add((Control) this.radLabelLoadImage);
      this.pagePicture.Controls.Add((Control) this.radBrowseEditorImage);
      this.pagePicture.ItemSize = new SizeF(51f, 28f);
      this.pagePicture.Location = new Point(10, 37);
      this.pagePicture.Name = "pagePicture";
      this.pagePicture.Size = new Size(330, 219);
      this.pagePicture.Text = "Picture";
      this.plusMinusEditorImageVOffset.FormatString = "{0:F1}''";
      this.plusMinusEditorImageVOffset.TrimString = "''";
      this.plusMinusEditorImageVOffset.Location = new Point(110, 80);
      this.plusMinusEditorImageVOffset.MaxValue = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.plusMinusEditorImageVOffset.MinValue = new Decimal(new int[4]);
      this.plusMinusEditorImageVOffset.Name = "plusMinusEditorImageVOffset";
      this.plusMinusEditorImageVOffset.Size = new Size(95, 21);
      this.plusMinusEditorImageVOffset.Step = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.plusMinusEditorImageVOffset.TabIndex = 17;
      this.plusMinusEditorImageVOffset.Value = new Decimal(new int[4]);
      this.plusMinusEditorImageOpacity.FormatString = "{0}%";
      this.plusMinusEditorImageOpacity.TrimString = "%";
      this.plusMinusEditorImageOpacity.Location = new Point(3, 131);
      this.plusMinusEditorImageOpacity.MaxValue = new Decimal(new int[4]
      {
        100,
        0,
        0,
        0
      });
      this.plusMinusEditorImageOpacity.MinValue = new Decimal(new int[4]);
      this.plusMinusEditorImageOpacity.Name = "plusMinusEditorImageOpacity";
      this.plusMinusEditorImageOpacity.Size = new Size(90, 21);
      this.plusMinusEditorImageOpacity.Step = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.plusMinusEditorImageOpacity.TabIndex = 17;
      this.plusMinusEditorImageOpacity.Value = new Decimal(new int[4]);
      this.plusMinusEditorImageHOffset.FormatString = "{0:F1}''";
      this.plusMinusEditorImageHOffset.TrimString = "''";
      this.plusMinusEditorImageHOffset.Location = new Point(4, 80);
      this.plusMinusEditorImageHOffset.MaxValue = new Decimal(new int[4]
      {
        10,
        0,
        0,
        0
      });
      this.plusMinusEditorImageHOffset.MinValue = new Decimal(new int[4]);
      this.plusMinusEditorImageHOffset.Name = "plusMinusEditorImageHOffset";
      this.plusMinusEditorImageHOffset.Size = new Size(95, 21);
      this.plusMinusEditorImageHOffset.Step = new Decimal(new int[4]
      {
        1,
        0,
        0,
        0
      });
      this.plusMinusEditorImageHOffset.TabIndex = 17;
      this.plusMinusEditorImageHOffset.Value = new Decimal(new int[4]);
      this.radCheckBoxImageTiling.BackColor = Color.Transparent;
      this.radCheckBoxImageTiling.Location = new Point(110, 132);
      this.radCheckBoxImageTiling.Name = "radCheckBoxImageTiling";
      this.radCheckBoxImageTiling.Size = new Size(47, 18);
      this.radCheckBoxImageTiling.TabIndex = 14;
      this.radCheckBoxImageTiling.Text = "Tiling";
      this.radLabelImageOpacity.BackColor = Color.Transparent;
      this.radLabelImageOpacity.Location = new Point(1, 110);
      this.radLabelImageOpacity.Name = "radLabelImageOpacity";
      this.radLabelImageOpacity.Size = new Size(44, 18);
      this.radLabelImageOpacity.TabIndex = 12;
      this.radLabelImageOpacity.Text = "Opacity";
      this.radLabelImageVOffset.BackColor = Color.Transparent;
      this.radLabelImageVOffset.Location = new Point(108, 59);
      this.radLabelImageVOffset.Name = "radLabelImageVOffset";
      this.radLabelImageVOffset.Size = new Size(75, 18);
      this.radLabelImageVOffset.TabIndex = 10;
      this.radLabelImageVOffset.Text = "Vertical offset";
      this.radLabelImageHOffset.BackColor = Color.Transparent;
      this.radLabelImageHOffset.Location = new Point(1, 59);
      this.radLabelImageHOffset.Name = "radLabelImageHOffset";
      this.radLabelImageHOffset.Size = new Size(89, 18);
      this.radLabelImageHOffset.TabIndex = 9;
      this.radLabelImageHOffset.Text = "Horizontal offset";
      this.radButtonClearImage.DisplayStyle = DisplayStyle.Image;
      this.radButtonClearImage.Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.remove_image_file;
      this.radButtonClearImage.ImageAlignment = ContentAlignment.MiddleCenter;
      this.radButtonClearImage.Location = new Point(285, 26);
      this.radButtonClearImage.Name = "radButtonClearImage";
      this.radButtonClearImage.Size = new Size(26, 24);
      this.radButtonClearImage.TabIndex = 2;
      ((RadButtonItem) this.radButtonClearImage.GetChildAt(0)).Image = (Image) Telerik\u002EWinControls\u002EUI\u002EResources.remove_image_file;
      ((RadButtonItem) this.radButtonClearImage.GetChildAt(0)).ImageAlignment = ContentAlignment.MiddleCenter;
      ((RadButtonItem) this.radButtonClearImage.GetChildAt(0)).DisplayStyle = DisplayStyle.Image;
      this.radButtonClearImage.GetChildAt(0).Padding = new Padding(0);
      this.radLabelLoadImage.BackColor = Color.Transparent;
      this.radLabelLoadImage.Location = new Point(0, 5);
      this.radLabelLoadImage.Name = "radLabelLoadImage";
      this.radLabelLoadImage.Size = new Size(67, 18);
      this.radLabelLoadImage.TabIndex = 1;
      this.radLabelLoadImage.Text = "Load image:";
      this.radBrowseEditorImage.Location = new Point(3, 28);
      this.radBrowseEditorImage.Name = "radBrowseEditorImage";
      this.radBrowseEditorImage.Size = new Size(276, 20);
      this.radBrowseEditorImage.TabIndex = 0;
      this.radBrowseEditorImage.Text = "radBrowseEditor1";
      this.radLabelPreview.Location = new Point(9, 14);
      this.radLabelPreview.Name = "radLabelPreview";
      this.radLabelPreview.Size = new Size(45, 18);
      this.radLabelPreview.TabIndex = 4;
      this.radLabelPreview.Text = "Preview";
      this.radButtonCancel.DialogResult = DialogResult.Cancel;
      this.radButtonCancel.Location = new Point(435, 370);
      this.radButtonCancel.Name = "radButtonCancel";
      this.radButtonCancel.Size = new Size(94, 24);
      this.radButtonCancel.TabIndex = 5;
      this.radButtonCancel.Text = "Cancel";
      this.radButtonOK.Location = new Point(335, 370);
      this.radButtonOK.Name = "radButtonOK";
      this.radButtonOK.Size = new Size(94, 24);
      this.radButtonOK.TabIndex = 5;
      this.radButtonOK.Text = "OK";
      this.radButtonRemoveWatermark.Location = new Point(14, 250);
      this.radButtonRemoveWatermark.Name = "radButtonRemoveWatermark";
      this.radButtonRemoveWatermark.Size = new Size(161, 24);
      this.radButtonRemoveWatermark.TabIndex = 6;
      this.radButtonRemoveWatermark.Text = "Remove watermark";
      this.radButtonRemoveWatermark.Click += new EventHandler(this.radButtonRemoveWatermark_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.CancelButton = (IButtonControl) this.radButtonCancel;
      this.ClientSize = new Size(535, 402);
      this.Controls.Add((Control) this.radButtonRemoveWatermark);
      this.Controls.Add((Control) this.radButtonOK);
      this.Controls.Add((Control) this.radButtonCancel);
      this.Controls.Add((Control) this.radLabelPreview);
      this.Controls.Add((Control) this.radPageView1);
      this.Controls.Add((Control) this.groupBoxPageRange);
      this.Controls.Add((Control) this.groupBoxPosition);
      this.Controls.Add((Control) this.watermarkPreviewControl1);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (WatermarkPreviewDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Watermark Settings";
      this.watermarkPreviewControl1.EndInit();
      this.groupBoxPosition.EndInit();
      this.groupBoxPosition.ResumeLayout(false);
      this.groupBoxPosition.PerformLayout();
      this.radRadioButtonBehind.EndInit();
      this.radRadioButtonInFront.EndInit();
      this.groupBoxPageRange.EndInit();
      this.groupBoxPageRange.ResumeLayout(false);
      this.groupBoxPageRange.PerformLayout();
      this.radLabelDescription.EndInit();
      this.radTextBoxPages.EndInit();
      this.radRadioButtonPages.EndInit();
      this.radRadioButtonAllPages.EndInit();
      this.radPageView1.EndInit();
      this.radPageView1.ResumeLayout(false);
      this.pageText.ResumeLayout(false);
      this.pageText.PerformLayout();
      this.radCommandBar1.EndInit();
      this.radLabelTextRotation.EndInit();
      this.radLabelTextVOffset.EndInit();
      this.radLabelTextHOffset.EndInit();
      this.radColorBoxFontColor.EndInit();
      this.radLabelColor.EndInit();
      this.radDropDownListFontSize.EndInit();
      this.radDropDownListFonts.EndInit();
      this.radLabelFontSize.EndInit();
      this.radLabelOpacity.EndInit();
      this.radLabelFont.EndInit();
      this.radLabelText.EndInit();
      this.radTextBoxWMText.EndInit();
      this.pagePicture.ResumeLayout(false);
      this.pagePicture.PerformLayout();
      this.radCheckBoxImageTiling.EndInit();
      this.radLabelImageOpacity.EndInit();
      this.radLabelImageVOffset.EndInit();
      this.radLabelImageHOffset.EndInit();
      this.radButtonClearImage.EndInit();
      this.radLabelLoadImage.EndInit();
      this.radBrowseEditorImage.EndInit();
      this.radLabelPreview.EndInit();
      this.radButtonCancel.EndInit();
      this.radButtonOK.EndInit();
      this.radButtonRemoveWatermark.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
