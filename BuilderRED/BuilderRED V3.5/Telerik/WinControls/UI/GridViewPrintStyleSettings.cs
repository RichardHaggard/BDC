// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewPrintStyleSettings
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class GridViewPrintStyleSettings : UserControl
  {
    private GridPrintStyle printStyle;
    private LightVisualElement imageBox;
    private IContainer components;
    private RadGroupBox radGroupBoxStyleSettings;
    private RadCheckBox radCheckBoxPrintGrouping;
    private RadCheckBox radCheckBoxPrintSummaries;
    private RadCheckBox radCheckBoxPrintHiddenRows;
    private RadCheckBox radCheckBoxPrintHiddenColumns;
    private RadCheckBox radCheckBoxHeaderOnEachPage;
    private RadDropDownList radDropDownListFitWidthMode;
    private RadLabel radLabelHeaderCellsFont;
    private RadLabel radLabelDataCellsFont;
    private RadLabel radLabelSummaryCellsFont;
    private RadLabel radLabelGroupCellsFont;
    private RadGroupBox radGroupBoxPreview;
    private RadLabel radLabelPageFitMode;
    private RadButton radButtonWatermark;
    private RadPanel radPanel1;
    private RadCheckBox radCheckBoxPrintAlternatingRowColor;
    private RadColorBox radColorBoxHeaderBackColor;
    private RadColorBox radColorBoxGroupBackColor;
    private RadColorBox radColorBoxDataBackColor;
    private RadColorBox radColorBoxSummaryBackColor;
    private RadColorBox radColorBoxAlternatingRowColor;
    private RadColorBox radColorBoxCellBorder;
    private RadTextBox radTextBoxCellPadding;
    private RadGroupBox radGroupBoxPrint;
    private RadLabel radLabelBackground;
    private RadLabel radLabelBorderColor;
    private RadLabel radLabelAlternatingRowColor;
    private RadLabel radLabelPadding;
    private RadBrowseEditor radBrowseEditorHeaderCellsFont;
    private RadBrowseEditor radBrowseEditorSummaryCellsFont;
    private RadBrowseEditor radBrowseEditorDataCellsFont;
    private RadBrowseEditor radBrowseEditorGroupCellsFont;
    private RadCheckBox radCheckBoxPrintHierarchy;

    public GridViewPrintStyleSettings()
    {
      this.InitializeComponent();
      this.radDropDownListFitWidthMode.Items[0].Value = (object) PrintFitWidthMode.FitPageWidth;
      this.radDropDownListFitWidthMode.Items[1].Value = (object) PrintFitWidthMode.NoFit;
      this.radDropDownListFitWidthMode.Items[2].Value = (object) PrintFitWidthMode.NoFitCentered;
      this.imageBox = new LightVisualElement();
      this.imageBox.StretchHorizontally = true;
      this.imageBox.StretchVertically = true;
      this.imageBox.DrawText = false;
      this.radPanel1.PanelElement.Children.Add((RadElement) this.imageBox);
      this.printStyle = new GridPrintStyle();
      this.radLabelHeaderCellsFont.LocationChanged += new EventHandler(this.radLabelHeaderCellsFont_LocationChanged);
    }

    private void radLabelHeaderCellsFont_LocationChanged(object sender, EventArgs e)
    {
    }

    public GridPrintStyle PrintStyle
    {
      get
      {
        this.ApplyPrintStyleSettings();
        return this.printStyle;
      }
    }

    public virtual void LocalizeStrings()
    {
      PrintDialogsLocalizationProvider currentProvider = LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProvider;
      this.radGroupBoxPreview.HeaderText = currentProvider.GetLocalizedString("GridSettingsLabelPreview");
      this.radGroupBoxStyleSettings.HeaderText = currentProvider.GetLocalizedString("GridSettingsLabelStyleSettings");
      this.radGroupBoxPrint.HeaderText = currentProvider.GetLocalizedString("GridSettingsLabelPrint");
      this.radLabelHeaderCellsFont.Text = currentProvider.GetLocalizedString("GridSettingsLabelHeaderCells");
      this.radLabelGroupCellsFont.Text = currentProvider.GetLocalizedString("GridSettingsLabelGroupCells");
      this.radLabelDataCellsFont.Text = currentProvider.GetLocalizedString("GridSettingsLabelDateCells");
      this.radLabelBackground.Text = currentProvider.GetLocalizedString("GridSettingsLabelBackground");
      this.radLabelBorderColor.Text = currentProvider.GetLocalizedString("GridSettingsLabelBorderColor");
      this.radLabelAlternatingRowColor.Text = currentProvider.GetLocalizedString("GridSettingsLabelAlternatingRowColor");
      this.radLabelPadding.Text = currentProvider.GetLocalizedString("GridSettingsLabelPadding");
      this.radLabelSummaryCellsFont.Text = currentProvider.GetLocalizedString("GridSettingsLabelSummaryCells");
      this.radLabelPageFitMode.Text = currentProvider.GetLocalizedString("GridSettingsLabelFitMode");
      this.radCheckBoxPrintGrouping.Text = currentProvider.GetLocalizedString("GridSettingsPrintGrouping");
      this.radCheckBoxPrintSummaries.Text = currentProvider.GetLocalizedString("GridSettingsPrintSummaries");
      this.radCheckBoxPrintHiddenRows.Text = currentProvider.GetLocalizedString("GridSettingsPrintHiddenRows");
      this.radCheckBoxPrintHiddenColumns.Text = currentProvider.GetLocalizedString("GridSettingsPrintHiddenColumns");
      this.radCheckBoxPrintHierarchy.Text = currentProvider.GetLocalizedString("GridSettingsPrintHierarchy");
      this.radCheckBoxHeaderOnEachPage.Text = currentProvider.GetLocalizedString("GridSettingsPrintHeader");
      this.radCheckBoxPrintAlternatingRowColor.Text = currentProvider.GetLocalizedString("GridSettingsLabelAlternatingRowColor");
      this.radButtonWatermark.Text = currentProvider.GetLocalizedString("GridSettingsButtonWatermark");
      this.radDropDownListFitWidthMode.Items[0].Text = currentProvider.GetLocalizedString("GridSettingsFitPageWidth");
      this.radDropDownListFitWidthMode.Items[1].Text = currentProvider.GetLocalizedString("GridSettingsNoFit");
      this.radDropDownListFitWidthMode.Items[2].Text = currentProvider.GetLocalizedString("GridSettingsNoFitCentered");
    }

    public void LoadPrintStyle(GridPrintStyle printStyle)
    {
      this.UnwireEvents();
      this.LoadGeneralPrintStyleSettings(printStyle);
      this.SetPreviewImage();
      this.WireEvents();
    }

    protected virtual void LoadGeneralPrintStyleSettings(GridPrintStyle printStyle)
    {
      this.radCheckBoxPrintGrouping.Checked = printStyle.PrintGrouping;
      this.radCheckBoxPrintSummaries.Checked = printStyle.PrintSummaries;
      this.radCheckBoxPrintHiddenRows.Checked = printStyle.PrintHiddenRows;
      this.radCheckBoxHeaderOnEachPage.Checked = printStyle.PrintHeaderOnEachPage;
      this.radCheckBoxPrintHiddenColumns.Checked = printStyle.PrintHiddenColumns;
      this.radCheckBoxPrintHierarchy.Checked = printStyle.PrintHierarchy;
      this.radCheckBoxPrintAlternatingRowColor.Checked = printStyle.PrintAlternatingRowColor;
      this.radColorBoxCellBorder.Value = printStyle.BorderColor;
      this.radColorBoxDataBackColor.Value = printStyle.CellBackColor;
      this.radColorBoxGroupBackColor.Value = printStyle.GroupRowBackColor;
      this.radColorBoxHeaderBackColor.Value = printStyle.HeaderCellBackColor;
      this.radColorBoxSummaryBackColor.Value = printStyle.SummaryCellBackColor;
      this.radColorBoxAlternatingRowColor.Value = printStyle.AlternatingRowColor;
      this.radTextBoxCellPadding.Text = TypeDescriptor.GetConverter(typeof (Padding)).ConvertToString((object) printStyle.CellPadding);
      this.radBrowseEditorDataCellsFont.Value = TypeDescriptor.GetConverter(typeof (Font)).ConvertToString((object) printStyle.CellFont);
      this.radBrowseEditorGroupCellsFont.Value = TypeDescriptor.GetConverter(typeof (Font)).ConvertToString((object) printStyle.GroupRowFont);
      this.radBrowseEditorHeaderCellsFont.Value = TypeDescriptor.GetConverter(typeof (Font)).ConvertToString((object) printStyle.HeaderCellFont);
      this.radBrowseEditorSummaryCellsFont.Value = TypeDescriptor.GetConverter(typeof (Font)).ConvertToString((object) printStyle.SummaryCellFont);
      this.radDropDownListFitWidthMode.SelectedValue = (object) printStyle.FitWidthMode;
    }

    protected virtual void ApplyPrintStyleSettings()
    {
      this.printStyle.PrintGrouping = this.radCheckBoxPrintGrouping.Checked;
      this.printStyle.PrintSummaries = this.radCheckBoxPrintSummaries.Checked;
      this.printStyle.PrintHiddenRows = this.radCheckBoxPrintHiddenRows.Checked;
      this.printStyle.PrintHiddenColumns = this.radCheckBoxPrintHiddenColumns.Checked;
      this.printStyle.PrintHierarchy = this.radCheckBoxPrintHierarchy.Checked;
      this.printStyle.PrintHeaderOnEachPage = this.radCheckBoxHeaderOnEachPage.Checked;
      this.printStyle.PrintAlternatingRowColor = this.radCheckBoxPrintAlternatingRowColor.Checked;
      this.printStyle.BorderColor = this.radColorBoxCellBorder.Value;
      this.printStyle.CellBackColor = this.radColorBoxDataBackColor.Value;
      this.printStyle.GroupRowBackColor = this.radColorBoxGroupBackColor.Value;
      this.printStyle.HeaderCellBackColor = this.radColorBoxHeaderBackColor.Value;
      this.printStyle.SummaryCellBackColor = this.radColorBoxSummaryBackColor.Value;
      this.printStyle.AlternatingRowColor = this.radColorBoxAlternatingRowColor.Value;
      if (!string.IsNullOrEmpty(this.radBrowseEditorDataCellsFont.Value))
        this.printStyle.CellFont = TypeDescriptor.GetConverter(typeof (Font)).ConvertFromString(this.radBrowseEditorDataCellsFont.Value) as Font;
      if (!string.IsNullOrEmpty(this.radBrowseEditorGroupCellsFont.Value))
        this.printStyle.GroupRowFont = TypeDescriptor.GetConverter(typeof (Font)).ConvertFromString(this.radBrowseEditorGroupCellsFont.Value) as Font;
      if (!string.IsNullOrEmpty(this.radBrowseEditorHeaderCellsFont.Value))
        this.printStyle.HeaderCellFont = TypeDescriptor.GetConverter(typeof (Font)).ConvertFromString(this.radBrowseEditorHeaderCellsFont.Value) as Font;
      if (!string.IsNullOrEmpty(this.radBrowseEditorSummaryCellsFont.Value))
        this.printStyle.SummaryCellFont = TypeDescriptor.GetConverter(typeof (Font)).ConvertFromString(this.radBrowseEditorSummaryCellsFont.Value) as Font;
      if (this.radTextBoxCellPadding.Tag != null)
        this.printStyle.CellPadding = (Padding) this.radTextBoxCellPadding.Tag;
      this.printStyle.FitWidthMode = this.radDropDownListFitWidthMode.SelectedValue == null ? PrintFitWidthMode.FitPageWidth : (PrintFitWidthMode) this.radDropDownListFitWidthMode.SelectedValue;
    }

    protected virtual void SetCustomFontControlsEnableProperty(bool enabled)
    {
      this.radLabelDataCellsFont.Enabled = enabled;
      this.radLabelGroupCellsFont.Enabled = enabled;
      this.radLabelHeaderCellsFont.Enabled = enabled;
      this.radLabelSummaryCellsFont.Enabled = enabled;
      this.radBrowseEditorDataCellsFont.Enabled = enabled;
      this.radBrowseEditorGroupCellsFont.Enabled = enabled;
      this.radBrowseEditorHeaderCellsFont.Enabled = enabled;
      this.radBrowseEditorSummaryCellsFont.Enabled = enabled;
    }

    protected virtual void SetPreviewImage()
    {
      bool isChecked = this.radCheckBoxPrintGrouping.IsChecked;
      Image image;
      switch ((PrintFitWidthMode) this.radDropDownListFitWidthMode.SelectedValue)
      {
        case PrintFitWidthMode.FitPageWidth:
          image = !isChecked ? this.LoadImage(typeof (GridViewPrintStyleSettings), "Telerik.WinControls.UI.Resources.FitPageWidthNG.png") : this.LoadImage(typeof (GridViewPrintStyleSettings), "Telerik.WinControls.UI.Resources.FitPageWidthG.png");
          break;
        case PrintFitWidthMode.NoFitCentered:
          image = !isChecked ? this.LoadImage(typeof (GridViewPrintStyleSettings), "Telerik.WinControls.UI.Resources.NoFitCenteredNG.png") : this.LoadImage(typeof (GridViewPrintStyleSettings), "Telerik.WinControls.UI.Resources.NoFitCenteredG.png");
          break;
        default:
          image = !isChecked ? this.LoadImage(typeof (GridViewPrintStyleSettings), "Telerik.WinControls.UI.Resources.NoFitNG.png") : this.LoadImage(typeof (GridViewPrintStyleSettings), "Telerik.WinControls.UI.Resources.NoFitG.png");
          break;
      }
      bool flag = false;
      GridViewPrintSettingsDialog form = this.FindForm() as GridViewPrintSettingsDialog;
      if (form != null && form.PrintDocument != null && form.PrintDocument.Watermark != null && (form.PrintDocument.Watermark.DrawText || form.PrintDocument.Watermark.DrawImage))
        flag = true;
      if (flag)
      {
        Image watermarkImage = this.LoadImage(typeof (LightVisualElement), "Telerik.WinControls.UI.Printing.watermark.png");
        this.AddWatermarkSign(image, watermarkImage);
      }
      this.imageBox.Image = image;
    }

    private Image LoadImage(System.Type assemblyType, string name)
    {
      Stream manifestResourceStream = Assembly.GetAssembly(assemblyType).GetManifestResourceStream(name);
      if (manifestResourceStream == null)
        throw new NullReferenceException("Cannot find " + name);
      return (Image) new Bitmap(manifestResourceStream);
    }

    private void AddWatermarkSign(Image image, Image watermarkImage)
    {
      if (image == null || watermarkImage == null)
        return;
      Graphics graphics = Graphics.FromImage(image);
      graphics.DrawImage(watermarkImage, new Point((image.Width - watermarkImage.Width) / 2, (image.Height - watermarkImage.Height) / 2));
      graphics.Dispose();
    }

    protected virtual void WireEvents()
    {
      this.radCheckBoxPrintGrouping.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintGrouping_ToggleStateChanged);
      this.radCheckBoxPrintSummaries.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintSummaries_ToggleStateChanged);
      this.radCheckBoxPrintHiddenRows.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintHiddenRows_ToggleStateChanged);
      this.radCheckBoxHeaderOnEachPage.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxHeaderOnEachPage_ToggleStateChanged);
      this.radDropDownListFitWidthMode.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownListFitWidthMode_SelectedIndexChanged);
      this.radCheckBoxPrintHiddenColumns.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintHiddenColumns_ToggleStateChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.radCheckBoxPrintGrouping.ToggleStateChanged -= new StateChangedEventHandler(this.radCheckBoxPrintGrouping_ToggleStateChanged);
      this.radCheckBoxPrintSummaries.ToggleStateChanged -= new StateChangedEventHandler(this.radCheckBoxPrintSummaries_ToggleStateChanged);
      this.radCheckBoxPrintHiddenRows.ToggleStateChanged -= new StateChangedEventHandler(this.radCheckBoxPrintHiddenRows_ToggleStateChanged);
      this.radCheckBoxHeaderOnEachPage.ToggleStateChanged -= new StateChangedEventHandler(this.radCheckBoxHeaderOnEachPage_ToggleStateChanged);
      this.radDropDownListFitWidthMode.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownListFitWidthMode_SelectedIndexChanged);
      this.radCheckBoxPrintHiddenColumns.ToggleStateChanged -= new StateChangedEventHandler(this.radCheckBoxPrintHiddenColumns_ToggleStateChanged);
    }

    internal void CorrectPositions()
    {
      this.radGroupBoxPreview.Height += this.radGroupBoxPrint.Bottom - this.radGroupBoxPreview.Bottom;
      this.radGroupBoxStyleSettings.Width += this.radGroupBoxPrint.Right - this.radGroupBoxStyleSettings.Right;
    }

    internal void AdjustControlsForTouchLayout(string themeName)
    {
      if (!TelerikHelper.IsMaterialTheme(themeName))
        return;
      this.radGroupBoxPrint.Location = new Point(316, 4);
      this.radGroupBoxPrint.Size = new Size(374, 142);
      this.radCheckBoxPrintGrouping.Location = new Point(13, 28);
      this.radCheckBoxPrintGrouping.Size = new Size(116, 19);
      this.radCheckBoxPrintHierarchy.Location = new Point(13, 82);
      this.radCheckBoxPrintHierarchy.Size = new Size(118, 19);
      this.radCheckBoxPrintSummaries.Location = new Point(13, 55);
      this.radCheckBoxPrintSummaries.Size = new Size(131, 19);
      this.radCheckBoxHeaderOnEachPage.Location = new Point(13, 109);
      this.radCheckBoxHeaderOnEachPage.Size = new Size(193, 19);
      this.radCheckBoxPrintAlternatingRowColor.Location = new Point(161, 82);
      this.radCheckBoxPrintAlternatingRowColor.Size = new Size(159, 19);
      this.radCheckBoxPrintHiddenRows.Location = new Point(161, 28);
      this.radCheckBoxPrintHiddenRows.Size = new Size(137, 19);
      this.radCheckBoxPrintHiddenColumns.Location = new Point(161, 56);
      this.radCheckBoxPrintHiddenColumns.Size = new Size(161, 19);
      this.radGroupBoxPreview.Location = new Point(4, 4);
      this.radGroupBoxPreview.Size = new Size(306, 142);
      this.radPanel1.Location = new Point(12, 34);
      this.radPanel1.Size = new Size(77, 91);
      this.radButtonWatermark.Location = new Point(107, 89);
      this.radButtonWatermark.Size = new Size(171, 36);
      this.radLabelPageFitMode.Location = new Point(104, 23);
      this.radLabelPageFitMode.Size = new Size(97, 21);
      this.radDropDownListFitWidthMode.Location = new Point(107, 47);
      this.radDropDownListFitWidthMode.Size = new Size(171, 36);
      this.radGroupBoxStyleSettings.Location = new Point(4, 152);
      this.radGroupBoxStyleSettings.Size = new Size(686, 301);
      this.radBrowseEditorSummaryCellsFont.Location = new Point(15, 252);
      this.radBrowseEditorSummaryCellsFont.Size = new Size(200, 36);
      this.radBrowseEditorDataCellsFont.Location = new Point(15, 186);
      this.radBrowseEditorDataCellsFont.Size = new Size(200, 36);
      this.radBrowseEditorGroupCellsFont.Location = new Point(15, 121);
      this.radBrowseEditorGroupCellsFont.Size = new Size(200, 36);
      this.radBrowseEditorHeaderCellsFont.Location = new Point(15, 54);
      this.radBrowseEditorHeaderCellsFont.Size = new Size(200, 36);
      this.radLabelPadding.Location = new Point(457, 229);
      this.radLabelPadding.Size = new Size(60, 21);
      this.radLabelAlternatingRowColor.Location = new Point(458, 98);
      this.radLabelAlternatingRowColor.Size = new Size(142, 21);
      this.radLabelBorderColor.Location = new Point(458, 31);
      this.radLabelBorderColor.Size = new Size(87, 21);
      this.radLabelBackground.Location = new Point(238, 31);
      this.radLabelBackground.Size = new Size(85, 21);
      this.radTextBoxCellPadding.Location = new Point(461, 252);
      this.radTextBoxCellPadding.Size = new Size(200, 36);
      this.radColorBoxAlternatingRowColor.Location = new Point(461, 121);
      this.radColorBoxAlternatingRowColor.Size = new Size(200, 36);
      this.radColorBoxSummaryBackColor.Location = new Point(238, 252);
      this.radColorBoxSummaryBackColor.Size = new Size(200, 36);
      this.radColorBoxDataBackColor.Location = new Point(238, 186);
      this.radColorBoxDataBackColor.Size = new Size(200, 36);
      this.radColorBoxGroupBackColor.Location = new Point(238, 121);
      this.radColorBoxGroupBackColor.Size = new Size(200, 36);
      this.radColorBoxCellBorder.Location = new Point(461, 54);
      this.radColorBoxCellBorder.Size = new Size(200, 36);
      this.radColorBoxHeaderBackColor.Location = new Point(238, 54);
      this.radColorBoxHeaderBackColor.Size = new Size(200, 36);
      this.radLabelSummaryCellsFont.Location = new Point(12, 229);
      this.radLabelSummaryCellsFont.Size = new Size(132, 21);
      this.radLabelDataCellsFont.Location = new Point(11, 163);
      this.radLabelDataCellsFont.Size = new Size(100, 21);
      this.radLabelGroupCellsFont.Location = new Point(12, 98);
      this.radLabelGroupCellsFont.Size = new Size(110, 21);
      this.radLabelHeaderCellsFont.Location = new Point(11, 31);
      this.radLabelHeaderCellsFont.Size = new Size(117, 21);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.Parent == null)
        return;
      this.radGroupBoxPreview.GroupBoxElement.Header.Fill.BackColor = this.Parent.BackColor;
      this.radGroupBoxStyleSettings.GroupBoxElement.Header.Fill.BackColor = this.Parent.BackColor;
      this.radGroupBoxPrint.GroupBoxElement.Header.Fill.BackColor = this.Parent.BackColor;
    }

    private void radCheckBoxPrintGrouping_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.printStyle.PrintGrouping = this.radCheckBoxPrintGrouping.Checked;
      this.SetPreviewImage();
    }

    private void radCheckBoxPrintSummaries_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.printStyle.PrintSummaries = this.radCheckBoxPrintSummaries.Checked;
    }

    private void radCheckBoxPrintHiddenRows_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.printStyle.PrintHiddenRows = this.radCheckBoxPrintHiddenRows.Checked;
    }

    private void radCheckBoxPrintHiddenColumns_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.printStyle.PrintHiddenColumns = this.radCheckBoxPrintHiddenColumns.Checked;
    }

    private void radCheckBoxHeaderOnEachPage_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.printStyle.PrintHeaderOnEachPage = this.radCheckBoxHeaderOnEachPage.Checked;
    }

    private void radCheckBoxPrintHierarchy_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.printStyle.PrintHierarchy = this.radCheckBoxPrintHierarchy.Checked;
    }

    private void radDropDownListFitWidthMode_SelectedIndexChanged(
      object sender,
      Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      if (e.Position < 0)
        return;
      this.printStyle.FitWidthMode = (PrintFitWidthMode) this.radDropDownListFitWidthMode.Items[e.Position].Value;
      this.SetPreviewImage();
    }

    private void radButtonWatermark_Click(object sender, EventArgs e)
    {
      GridViewPrintSettingsDialog form1 = (GridViewPrintSettingsDialog) this.FindForm();
      if (form1 == null)
        return;
      WatermarkPreviewDialog watermarkPreviewDialog = new WatermarkPreviewDialog(form1.PrintDocument);
      RadForm form2 = this.FindForm() as RadForm;
      if (form2 != null)
        watermarkPreviewDialog.ThemeName = form2.ThemeName;
      int num = (int) watermarkPreviewDialog.ShowDialog();
      this.SetPreviewImage();
    }

    private void radTextBoxCellPadding_Validating(object sender, CancelEventArgs e)
    {
      try
      {
        Padding padding = (Padding) TypeDescriptor.GetConverter(typeof (Padding)).ConvertFromString((ITypeDescriptorContext) null, CultureInfo.CurrentUICulture, this.radTextBoxCellPadding.Text);
        e.Cancel = false;
        this.radTextBoxCellPadding.Tag = (object) padding;
      }
      catch (Exception ex)
      {
        e.Cancel = true;
      }
    }

    private void radGroupBoxStyleSettings_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      RadListDataItem radListDataItem1 = new RadListDataItem();
      RadListDataItem radListDataItem2 = new RadListDataItem();
      RadListDataItem radListDataItem3 = new RadListDataItem();
      this.radGroupBoxPrint = new RadGroupBox();
      this.radCheckBoxPrintGrouping = new RadCheckBox();
      this.radCheckBoxPrintSummaries = new RadCheckBox();
      this.radCheckBoxHeaderOnEachPage = new RadCheckBox();
      this.radCheckBoxPrintAlternatingRowColor = new RadCheckBox();
      this.radCheckBoxPrintHiddenRows = new RadCheckBox();
      this.radCheckBoxPrintHiddenColumns = new RadCheckBox();
      this.radGroupBoxPreview = new RadGroupBox();
      this.radPanel1 = new RadPanel();
      this.radButtonWatermark = new RadButton();
      this.radLabelPageFitMode = new RadLabel();
      this.radDropDownListFitWidthMode = new RadDropDownList();
      this.radGroupBoxStyleSettings = new RadGroupBox();
      this.radBrowseEditorSummaryCellsFont = new RadBrowseEditor();
      this.radBrowseEditorDataCellsFont = new RadBrowseEditor();
      this.radBrowseEditorGroupCellsFont = new RadBrowseEditor();
      this.radBrowseEditorHeaderCellsFont = new RadBrowseEditor();
      this.radLabelPadding = new RadLabel();
      this.radLabelAlternatingRowColor = new RadLabel();
      this.radLabelBorderColor = new RadLabel();
      this.radLabelBackground = new RadLabel();
      this.radTextBoxCellPadding = new RadTextBox();
      this.radColorBoxAlternatingRowColor = new RadColorBox();
      this.radColorBoxSummaryBackColor = new RadColorBox();
      this.radColorBoxDataBackColor = new RadColorBox();
      this.radColorBoxGroupBackColor = new RadColorBox();
      this.radColorBoxCellBorder = new RadColorBox();
      this.radColorBoxHeaderBackColor = new RadColorBox();
      this.radLabelSummaryCellsFont = new RadLabel();
      this.radLabelDataCellsFont = new RadLabel();
      this.radLabelGroupCellsFont = new RadLabel();
      this.radLabelHeaderCellsFont = new RadLabel();
      this.radCheckBoxPrintHierarchy = new RadCheckBox();
      this.radGroupBoxPrint.BeginInit();
      this.radGroupBoxPrint.SuspendLayout();
      this.radCheckBoxPrintGrouping.BeginInit();
      this.radCheckBoxPrintSummaries.BeginInit();
      this.radCheckBoxHeaderOnEachPage.BeginInit();
      this.radCheckBoxPrintAlternatingRowColor.BeginInit();
      this.radCheckBoxPrintHiddenRows.BeginInit();
      this.radCheckBoxPrintHiddenColumns.BeginInit();
      this.radGroupBoxPreview.BeginInit();
      this.radGroupBoxPreview.SuspendLayout();
      this.radPanel1.BeginInit();
      this.radButtonWatermark.BeginInit();
      this.radLabelPageFitMode.BeginInit();
      this.radDropDownListFitWidthMode.BeginInit();
      this.radGroupBoxStyleSettings.BeginInit();
      this.radGroupBoxStyleSettings.SuspendLayout();
      this.radBrowseEditorSummaryCellsFont.BeginInit();
      this.radBrowseEditorDataCellsFont.BeginInit();
      this.radBrowseEditorGroupCellsFont.BeginInit();
      this.radBrowseEditorHeaderCellsFont.BeginInit();
      this.radLabelPadding.BeginInit();
      this.radLabelAlternatingRowColor.BeginInit();
      this.radLabelBorderColor.BeginInit();
      this.radLabelBackground.BeginInit();
      this.radTextBoxCellPadding.BeginInit();
      this.radColorBoxAlternatingRowColor.BeginInit();
      this.radColorBoxSummaryBackColor.BeginInit();
      this.radColorBoxDataBackColor.BeginInit();
      this.radColorBoxGroupBackColor.BeginInit();
      this.radColorBoxCellBorder.BeginInit();
      this.radColorBoxHeaderBackColor.BeginInit();
      this.radLabelSummaryCellsFont.BeginInit();
      this.radLabelDataCellsFont.BeginInit();
      this.radLabelGroupCellsFont.BeginInit();
      this.radLabelHeaderCellsFont.BeginInit();
      this.radCheckBoxPrintHierarchy.BeginInit();
      this.SuspendLayout();
      this.radGroupBoxPrint.AccessibleRole = AccessibleRole.Grouping;
      this.radGroupBoxPrint.Controls.Add((Control) this.radCheckBoxPrintGrouping);
      this.radGroupBoxPrint.Controls.Add((Control) this.radCheckBoxPrintHierarchy);
      this.radGroupBoxPrint.Controls.Add((Control) this.radCheckBoxPrintSummaries);
      this.radGroupBoxPrint.Controls.Add((Control) this.radCheckBoxHeaderOnEachPage);
      this.radGroupBoxPrint.Controls.Add((Control) this.radCheckBoxPrintAlternatingRowColor);
      this.radGroupBoxPrint.Controls.Add((Control) this.radCheckBoxPrintHiddenRows);
      this.radGroupBoxPrint.Controls.Add((Control) this.radCheckBoxPrintHiddenColumns);
      this.radGroupBoxPrint.HeaderText = "Print:";
      this.radGroupBoxPrint.Location = new Point(316, 4);
      this.radGroupBoxPrint.Name = "radGroupBoxPrint";
      this.radGroupBoxPrint.RootElement.ControlBounds = new Rectangle(316, 4, 200, 100);
      this.radGroupBoxPrint.Size = new Size(306, (int) sbyte.MaxValue);
      this.radGroupBoxPrint.TabIndex = 3;
      this.radGroupBoxPrint.Text = "Print:";
      this.radCheckBoxPrintGrouping.Location = new Point(12, 26);
      this.radCheckBoxPrintGrouping.Name = "radCheckBoxPrintGrouping";
      this.radCheckBoxPrintGrouping.Size = new Size(93, 18);
      this.radCheckBoxPrintGrouping.TabIndex = 0;
      this.radCheckBoxPrintGrouping.Text = "Print grouping";
      this.radCheckBoxPrintGrouping.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintGrouping_ToggleStateChanged);
      this.radCheckBoxPrintSummaries.Location = new Point(12, 50);
      this.radCheckBoxPrintSummaries.Name = "radCheckBoxPrintSummaries";
      this.radCheckBoxPrintSummaries.Size = new Size(100, 18);
      this.radCheckBoxPrintSummaries.TabIndex = 1;
      this.radCheckBoxPrintSummaries.Text = "Print summaries";
      this.radCheckBoxPrintSummaries.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintSummaries_ToggleStateChanged);
      this.radCheckBoxHeaderOnEachPage.Location = new Point(12, 98);
      this.radCheckBoxHeaderOnEachPage.Name = "radCheckBoxHeaderOnEachPage";
      this.radCheckBoxHeaderOnEachPage.Size = new Size(152, 18);
      this.radCheckBoxHeaderOnEachPage.TabIndex = 4;
      this.radCheckBoxHeaderOnEachPage.Text = "Print header on each page";
      this.radCheckBoxHeaderOnEachPage.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxHeaderOnEachPage_ToggleStateChanged);
      this.radCheckBoxPrintAlternatingRowColor.Location = new Point(155, 74);
      this.radCheckBoxPrintAlternatingRowColor.Name = "radCheckBoxPrintAlternatingRowColor";
      this.radCheckBoxPrintAlternatingRowColor.Size = new Size(126, 18);
      this.radCheckBoxPrintAlternatingRowColor.TabIndex = 4;
      this.radCheckBoxPrintAlternatingRowColor.Text = "Alternating row color";
      this.radCheckBoxPrintAlternatingRowColor.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxHeaderOnEachPage_ToggleStateChanged);
      this.radCheckBoxPrintHiddenRows.Location = new Point(155, 26);
      this.radCheckBoxPrintHiddenRows.Name = "radCheckBoxPrintHiddenRows";
      this.radCheckBoxPrintHiddenRows.Size = new Size(108, 18);
      this.radCheckBoxPrintHiddenRows.TabIndex = 2;
      this.radCheckBoxPrintHiddenRows.Text = "Print hidden rows";
      this.radCheckBoxPrintHiddenRows.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintHiddenRows_ToggleStateChanged);
      this.radCheckBoxPrintHiddenColumns.Location = new Point(155, 51);
      this.radCheckBoxPrintHiddenColumns.Name = "radCheckBoxPrintHiddenColumns";
      this.radCheckBoxPrintHiddenColumns.Size = new Size(126, 18);
      this.radCheckBoxPrintHiddenColumns.TabIndex = 3;
      this.radCheckBoxPrintHiddenColumns.Text = "Print hidden columns";
      this.radCheckBoxPrintHiddenColumns.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintHiddenColumns_ToggleStateChanged);
      this.radGroupBoxPreview.AccessibleRole = AccessibleRole.Grouping;
      this.radGroupBoxPreview.Controls.Add((Control) this.radPanel1);
      this.radGroupBoxPreview.Controls.Add((Control) this.radButtonWatermark);
      this.radGroupBoxPreview.Controls.Add((Control) this.radLabelPageFitMode);
      this.radGroupBoxPreview.Controls.Add((Control) this.radDropDownListFitWidthMode);
      this.radGroupBoxPreview.HeaderText = "Preview";
      this.radGroupBoxPreview.Location = new Point(4, 4);
      this.radGroupBoxPreview.Name = "radGroupBoxPreview";
      this.radGroupBoxPreview.RootElement.ControlBounds = new Rectangle(4, 4, 200, 100);
      this.radGroupBoxPreview.Size = new Size(306, (int) sbyte.MaxValue);
      this.radGroupBoxPreview.TabIndex = 2;
      this.radGroupBoxPreview.Text = "Preview";
      this.radPanel1.BackColor = SystemColors.ControlLightLight;
      this.radPanel1.Location = new Point(12, 21);
      this.radPanel1.Name = "radPanel1";
      this.radPanel1.RootElement.ControlBounds = new Rectangle(12, 21, 200, 100);
      this.radPanel1.RootElement.EnableElementShadow = false;
      this.radPanel1.Size = new Size(77, 91);
      this.radPanel1.TabIndex = 9;
      this.radButtonWatermark.Location = new Point(107, 77);
      this.radButtonWatermark.Name = "radButtonWatermark";
      this.radButtonWatermark.RootElement.ControlBounds = new Rectangle(107, 77, 110, 24);
      this.radButtonWatermark.Size = new Size(103, 24);
      this.radButtonWatermark.TabIndex = 8;
      this.radButtonWatermark.Text = "Watermark...";
      this.radButtonWatermark.Click += new EventHandler(this.radButtonWatermark_Click);
      this.radLabelPageFitMode.Location = new Point(104, 25);
      this.radLabelPageFitMode.Name = "radLabelPageFitMode";
      this.radLabelPageFitMode.RootElement.ControlBounds = new Rectangle(104, 25, 100, 18);
      this.radLabelPageFitMode.Size = new Size(76, 18);
      this.radLabelPageFitMode.TabIndex = 7;
      this.radLabelPageFitMode.Text = "Page fit mode";
      radListDataItem1.Text = "FitPageWidth";
      radListDataItem2.Text = "NoFit";
      radListDataItem3.Text = "NoFitCentered";
      this.radDropDownListFitWidthMode.Items.Add(radListDataItem1);
      this.radDropDownListFitWidthMode.Items.Add(radListDataItem2);
      this.radDropDownListFitWidthMode.Items.Add(radListDataItem3);
      this.radDropDownListFitWidthMode.Location = new Point(107, 48);
      this.radDropDownListFitWidthMode.Name = "radDropDownListFitWidthMode";
      this.radDropDownListFitWidthMode.RootElement.ControlBounds = new Rectangle(107, 48, 125, 20);
      this.radDropDownListFitWidthMode.RootElement.StretchVertically = true;
      this.radDropDownListFitWidthMode.Size = new Size(171, 20);
      this.radDropDownListFitWidthMode.TabIndex = 6;
      this.radDropDownListFitWidthMode.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownListFitWidthMode_SelectedIndexChanged);
      this.radGroupBoxStyleSettings.AccessibleRole = AccessibleRole.Grouping;
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radBrowseEditorSummaryCellsFont);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radBrowseEditorDataCellsFont);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radBrowseEditorGroupCellsFont);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radBrowseEditorHeaderCellsFont);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radLabelPadding);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radLabelAlternatingRowColor);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radLabelBorderColor);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radLabelBackground);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radTextBoxCellPadding);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radColorBoxAlternatingRowColor);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radColorBoxSummaryBackColor);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radColorBoxDataBackColor);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radColorBoxGroupBackColor);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radColorBoxCellBorder);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radColorBoxHeaderBackColor);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radLabelSummaryCellsFont);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radLabelDataCellsFont);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radLabelGroupCellsFont);
      this.radGroupBoxStyleSettings.Controls.Add((Control) this.radLabelHeaderCellsFont);
      this.radGroupBoxStyleSettings.HeaderText = "Style Settings";
      this.radGroupBoxStyleSettings.Location = new Point(4, 137);
      this.radGroupBoxStyleSettings.Name = "radGroupBoxStyleSettings";
      this.radGroupBoxStyleSettings.RootElement.ControlBounds = new Rectangle(4, 137, 200, 100);
      this.radGroupBoxStyleSettings.Size = new Size(618, 229);
      this.radGroupBoxStyleSettings.TabIndex = 1;
      this.radGroupBoxStyleSettings.Text = "Style Settings";
      this.radBrowseEditorSummaryCellsFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorSummaryCellsFont.Location = new Point(15, 187);
      this.radBrowseEditorSummaryCellsFont.Name = "radBrowseEditorSummaryCellsFont";
      this.radBrowseEditorSummaryCellsFont.Size = new Size(175, 20);
      this.radBrowseEditorSummaryCellsFont.TabIndex = 16;
      this.radBrowseEditorSummaryCellsFont.Text = "radBrowseEditor1";
      this.radBrowseEditorDataCellsFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorDataCellsFont.Location = new Point(15, 140);
      this.radBrowseEditorDataCellsFont.Name = "radBrowseEditorDataCellsFont";
      this.radBrowseEditorDataCellsFont.Size = new Size(175, 20);
      this.radBrowseEditorDataCellsFont.TabIndex = 16;
      this.radBrowseEditorDataCellsFont.Text = "radBrowseEditor1";
      this.radBrowseEditorGroupCellsFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorGroupCellsFont.Location = new Point(15, 93);
      this.radBrowseEditorGroupCellsFont.Name = "radBrowseEditorGroupCellsFont";
      this.radBrowseEditorGroupCellsFont.Size = new Size(175, 20);
      this.radBrowseEditorGroupCellsFont.TabIndex = 16;
      this.radBrowseEditorGroupCellsFont.Text = "radBrowseEditor1";
      this.radBrowseEditorHeaderCellsFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorHeaderCellsFont.Location = new Point(15, 46);
      this.radBrowseEditorHeaderCellsFont.Name = "radBrowseEditorHeaderCellsFont";
      this.radBrowseEditorHeaderCellsFont.Size = new Size(175, 20);
      this.radBrowseEditorHeaderCellsFont.TabIndex = 16;
      this.radBrowseEditorHeaderCellsFont.Text = "radBrowseEditor1";
      this.radLabelPadding.Location = new Point(421, 164);
      this.radLabelPadding.Name = "radLabelPadding";
      this.radLabelPadding.RootElement.ControlBounds = new Rectangle(421, 164, 100, 18);
      this.radLabelPadding.Size = new Size(47, 18);
      this.radLabelPadding.TabIndex = 15;
      this.radLabelPadding.Text = "Padding";
      this.radLabelAlternatingRowColor.Location = new Point(422, 70);
      this.radLabelAlternatingRowColor.Name = "radLabelAlternatingRowColor";
      this.radLabelAlternatingRowColor.RootElement.ControlBounds = new Rectangle(422, 70, 100, 18);
      this.radLabelAlternatingRowColor.Size = new Size(112, 18);
      this.radLabelAlternatingRowColor.TabIndex = 14;
      this.radLabelAlternatingRowColor.Text = "Alternating row color";
      this.radLabelBorderColor.Location = new Point(422, 23);
      this.radLabelBorderColor.Name = "radLabelBorderColor";
      this.radLabelBorderColor.RootElement.ControlBounds = new Rectangle(422, 23, 100, 18);
      this.radLabelBorderColor.Size = new Size(68, 18);
      this.radLabelBorderColor.TabIndex = 13;
      this.radLabelBorderColor.Text = "Border color";
      this.radLabelBackground.Location = new Point(218, 23);
      this.radLabelBackground.Name = "radLabelBackground";
      this.radLabelBackground.RootElement.ControlBounds = new Rectangle(218, 23, 100, 18);
      this.radLabelBackground.Size = new Size(66, 18);
      this.radLabelBackground.TabIndex = 12;
      this.radLabelBackground.Text = "Background";
      this.radTextBoxCellPadding.Location = new Point(425, 187);
      this.radTextBoxCellPadding.Name = "radTextBoxCellPadding";
      this.radTextBoxCellPadding.RootElement.ControlBounds = new Rectangle(425, 187, 100, 20);
      this.radTextBoxCellPadding.RootElement.StretchVertically = true;
      this.radTextBoxCellPadding.Size = new Size(175, 20);
      this.radTextBoxCellPadding.TabIndex = 11;
      this.radTextBoxCellPadding.TabStop = false;
      this.radTextBoxCellPadding.Validating += new CancelEventHandler(this.radTextBoxCellPadding_Validating);
      this.radColorBoxAlternatingRowColor.Location = new Point(425, 93);
      this.radColorBoxAlternatingRowColor.Name = "radColorBoxAlternatingRowColor";
      this.radColorBoxAlternatingRowColor.RootElement.ControlBounds = new Rectangle(425, 93, 100, 20);
      this.radColorBoxAlternatingRowColor.RootElement.StretchVertically = true;
      this.radColorBoxAlternatingRowColor.Size = new Size(175, 20);
      this.radColorBoxAlternatingRowColor.TabIndex = 10;
      this.radColorBoxAlternatingRowColor.Text = "radColorBox1";
      ((RadItem) this.radColorBoxAlternatingRowColor.GetChildAt(0)).Text = "";
      this.radColorBoxAlternatingRowColor.GetChildAt(0).StretchVertically = true;
      this.radColorBoxSummaryBackColor.Location = new Point(221, 187);
      this.radColorBoxSummaryBackColor.Name = "radColorBoxSummaryBackColor";
      this.radColorBoxSummaryBackColor.RootElement.ControlBounds = new Rectangle(221, 187, 100, 20);
      this.radColorBoxSummaryBackColor.RootElement.StretchVertically = true;
      this.radColorBoxSummaryBackColor.Size = new Size(175, 20);
      this.radColorBoxSummaryBackColor.TabIndex = 10;
      this.radColorBoxSummaryBackColor.Text = "radColorBox1";
      ((RadItem) this.radColorBoxSummaryBackColor.GetChildAt(0)).Text = "";
      this.radColorBoxSummaryBackColor.GetChildAt(0).StretchVertically = true;
      this.radColorBoxDataBackColor.Location = new Point(221, 140);
      this.radColorBoxDataBackColor.Name = "radColorBoxDataBackColor";
      this.radColorBoxDataBackColor.RootElement.ControlBounds = new Rectangle(221, 140, 100, 20);
      this.radColorBoxDataBackColor.RootElement.StretchVertically = true;
      this.radColorBoxDataBackColor.Size = new Size(175, 20);
      this.radColorBoxDataBackColor.TabIndex = 10;
      this.radColorBoxDataBackColor.Text = "radColorBox1";
      ((RadItem) this.radColorBoxDataBackColor.GetChildAt(0)).Text = "";
      this.radColorBoxDataBackColor.GetChildAt(0).StretchVertically = true;
      this.radColorBoxGroupBackColor.Location = new Point(221, 93);
      this.radColorBoxGroupBackColor.Name = "radColorBoxGroupBackColor";
      this.radColorBoxGroupBackColor.RootElement.ControlBounds = new Rectangle(221, 93, 100, 20);
      this.radColorBoxGroupBackColor.RootElement.StretchVertically = true;
      this.radColorBoxGroupBackColor.Size = new Size(175, 20);
      this.radColorBoxGroupBackColor.TabIndex = 10;
      this.radColorBoxGroupBackColor.Text = "radColorBox1";
      ((RadItem) this.radColorBoxGroupBackColor.GetChildAt(0)).Text = "";
      this.radColorBoxGroupBackColor.GetChildAt(0).StretchVertically = true;
      this.radColorBoxCellBorder.Location = new Point(425, 46);
      this.radColorBoxCellBorder.Name = "radColorBoxCellBorder";
      this.radColorBoxCellBorder.RootElement.ControlBounds = new Rectangle(425, 46, 100, 20);
      this.radColorBoxCellBorder.RootElement.StretchVertically = true;
      this.radColorBoxCellBorder.Size = new Size(175, 20);
      this.radColorBoxCellBorder.TabIndex = 10;
      this.radColorBoxCellBorder.Text = "radColorBox1";
      ((RadItem) this.radColorBoxCellBorder.GetChildAt(0)).Text = "";
      this.radColorBoxCellBorder.GetChildAt(0).StretchVertically = true;
      this.radColorBoxHeaderBackColor.Location = new Point(221, 46);
      this.radColorBoxHeaderBackColor.Name = "radColorBoxHeaderBackColor";
      this.radColorBoxHeaderBackColor.RootElement.ControlBounds = new Rectangle(221, 46, 100, 20);
      this.radColorBoxHeaderBackColor.RootElement.StretchVertically = true;
      this.radColorBoxHeaderBackColor.Size = new Size(175, 20);
      this.radColorBoxHeaderBackColor.TabIndex = 10;
      this.radColorBoxHeaderBackColor.Text = "radColorBox1";
      ((RadItem) this.radColorBoxHeaderBackColor.GetChildAt(0)).Text = "";
      this.radColorBoxHeaderBackColor.GetChildAt(0).StretchVertically = true;
      this.radLabelSummaryCellsFont.Location = new Point(12, 164);
      this.radLabelSummaryCellsFont.Name = "radLabelSummaryCellsFont";
      this.radLabelSummaryCellsFont.RootElement.ControlBounds = new Rectangle(12, 164, 100, 18);
      this.radLabelSummaryCellsFont.Size = new Size(101, 18);
      this.radLabelSummaryCellsFont.TabIndex = 7;
      this.radLabelSummaryCellsFont.Text = "Summary cells font";
      this.radLabelDataCellsFont.Location = new Point(11, 117);
      this.radLabelDataCellsFont.Name = "radLabelDataCellsFont";
      this.radLabelDataCellsFont.RootElement.ControlBounds = new Rectangle(11, 117, 100, 18);
      this.radLabelDataCellsFont.Size = new Size(78, 18);
      this.radLabelDataCellsFont.TabIndex = 7;
      this.radLabelDataCellsFont.Text = "Data cells font";
      this.radLabelGroupCellsFont.Location = new Point(12, 70);
      this.radLabelGroupCellsFont.Name = "radLabelGroupCellsFont";
      this.radLabelGroupCellsFont.RootElement.ControlBounds = new Rectangle(12, 70, 100, 18);
      this.radLabelGroupCellsFont.Size = new Size(86, 18);
      this.radLabelGroupCellsFont.TabIndex = 7;
      this.radLabelGroupCellsFont.Text = "Group cells font";
      this.radLabelHeaderCellsFont.Location = new Point(11, 23);
      this.radLabelHeaderCellsFont.Name = "radLabelHeaderCellsFont";
      this.radLabelHeaderCellsFont.RootElement.ControlBounds = new Rectangle(11, 23, 100, 18);
      this.radLabelHeaderCellsFont.Size = new Size(91, 18);
      this.radLabelHeaderCellsFont.TabIndex = 7;
      this.radLabelHeaderCellsFont.Text = "Header cells font";
      this.radCheckBoxPrintHierarchy.Location = new Point(12, 74);
      this.radCheckBoxPrintHierarchy.Name = "radCheckBoxPrintHierarchy";
      this.radCheckBoxPrintHierarchy.Size = new Size(92, 18);
      this.radCheckBoxPrintHierarchy.TabIndex = 1;
      this.radCheckBoxPrintHierarchy.Text = "Print hierarchy";
      this.radCheckBoxPrintHierarchy.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintHierarchy_ToggleStateChanged);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.radGroupBoxPrint);
      this.Controls.Add((Control) this.radGroupBoxPreview);
      this.Controls.Add((Control) this.radGroupBoxStyleSettings);
      this.Name = nameof (GridViewPrintStyleSettings);
      this.Size = new Size(625, 379);
      this.radGroupBoxPrint.EndInit();
      this.radGroupBoxPrint.ResumeLayout(false);
      this.radGroupBoxPrint.PerformLayout();
      this.radCheckBoxPrintGrouping.EndInit();
      this.radCheckBoxPrintSummaries.EndInit();
      this.radCheckBoxHeaderOnEachPage.EndInit();
      this.radCheckBoxPrintAlternatingRowColor.EndInit();
      this.radCheckBoxPrintHiddenRows.EndInit();
      this.radCheckBoxPrintHiddenColumns.EndInit();
      this.radGroupBoxPreview.EndInit();
      this.radGroupBoxPreview.ResumeLayout(false);
      this.radGroupBoxPreview.PerformLayout();
      this.radPanel1.EndInit();
      this.radButtonWatermark.EndInit();
      this.radLabelPageFitMode.EndInit();
      this.radDropDownListFitWidthMode.EndInit();
      this.radGroupBoxStyleSettings.EndInit();
      this.radGroupBoxStyleSettings.ResumeLayout(false);
      this.radGroupBoxStyleSettings.PerformLayout();
      this.radBrowseEditorSummaryCellsFont.EndInit();
      this.radBrowseEditorDataCellsFont.EndInit();
      this.radBrowseEditorGroupCellsFont.EndInit();
      this.radBrowseEditorHeaderCellsFont.EndInit();
      this.radLabelPadding.EndInit();
      this.radLabelAlternatingRowColor.EndInit();
      this.radLabelBorderColor.EndInit();
      this.radLabelBackground.EndInit();
      this.radTextBoxCellPadding.EndInit();
      this.radColorBoxAlternatingRowColor.EndInit();
      this.radColorBoxSummaryBackColor.EndInit();
      this.radColorBoxDataBackColor.EndInit();
      this.radColorBoxGroupBackColor.EndInit();
      this.radColorBoxCellBorder.EndInit();
      this.radColorBoxHeaderBackColor.EndInit();
      this.radLabelSummaryCellsFont.EndInit();
      this.radLabelDataCellsFont.EndInit();
      this.radLabelGroupCellsFont.EndInit();
      this.radLabelHeaderCellsFont.EndInit();
      this.radCheckBoxPrintHierarchy.EndInit();
      this.ResumeLayout(false);
    }
  }
}
