// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewPrintSettingsControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class GanttViewPrintSettingsControl : UserControl
  {
    private GanttViewPrintSettings printSettings;
    private LightVisualElement imageBox;
    private Image downThenOverImage;
    private Image overThenDownImage;
    private IContainer components;
    private RadGroupBox radGroupBoxTextView;
    private RadGroupBox radGroupBoxPrintSettings;
    private RadButton radButtonWatermark;
    private RadPanel radPanel1;
    private RadGroupBox radGroupBoxGraphicalView;
    private RadCheckBox radCheckBoxPrintTextView;
    private RadLabel radLabelDataCellBackground;
    private RadLabel radLabelBordersColor;
    private RadLabel radLabelHeaderCellBackground;
    private RadColorBox radColorBoxDataCellBackColor;
    private RadColorBox radColorBoxTextViewBorders;
    private RadColorBox radColorBoxHeaderCellBackColor;
    private RadRadioButton radRadioButtonRowMajorOrder;
    private RadRadioButton radRadioButtonColumnMajorOrder;
    private RadSeparator radSeparator1;
    private RadCheckBox radCheckBoxTimelineVisibleOnEveryPage;
    private RadColorBox radColorBoxTimelineBottomRowBackColor;
    private RadColorBox radColorBoxTimelineTopRowBackColor;
    private RadColorBox radColorBoxTasksBorderColor;
    private RadColorBox radColorBoxMilestoneBackColor;
    private RadColorBox radColorBoxTasksBackColor;
    private RadLabel radLabelBottomRowBackground;
    private RadColorBox radColorBoxSummaryBackColor;
    private RadLabel radLabelTimeline;
    private RadLabel radLabelTopRowBackground;
    private RadColorBox radColorBoxLinksColor;
    private RadLabel radLabelTasksBorderColor;
    private RadLabel radLabelTasksBackground;
    private RadLabel radLabelMilestoneBackground;
    private RadLabel radLabelSummaryBackground;
    private RadLabel radLabelConnectorsColor;
    private RadBrowseEditor radBrowseEditorDataCellsFont;
    private RadLabel radLabelDataCellFont;
    private RadBrowseEditor radBrowseEditorHeaderCellsFont;
    private RadLabel radLabelHeaderCellFont;
    private RadBrowseEditor radBrowseEditorBottomRowFont;
    private RadBrowseEditor radBrowseEditorTopRowFont;
    private RadBrowseEditor radBrowseEditorTasksFont;
    private RadLabel radLabelBottomRowFont;
    private RadLabel radLabelTopRowFont;
    private RadLabel radLabelTasksFont;

    public GanttViewPrintSettingsControl()
    {
      this.InitializeComponent();
      this.imageBox = new LightVisualElement();
      this.imageBox.StretchHorizontally = true;
      this.imageBox.StretchVertically = true;
      this.imageBox.DrawText = false;
      this.radPanel1.PanelElement.Children.Add((RadElement) this.imageBox);
      this.printSettings = new GanttViewPrintSettings();
      this.overThenDownImage = this.LoadImage("Telerik.WinControls.UI.Printing.over-then-down.png");
      this.downThenOverImage = this.LoadImage("Telerik.WinControls.UI.Printing.down-then-over.png");
    }

    public GanttViewPrintSettings PrintSettings
    {
      get
      {
        this.ApplyPrintSettings();
        return this.printSettings;
      }
    }

    public virtual void LocalizeStrings()
    {
      PrintDialogsLocalizationProvider currentProvider = LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProvider;
      this.radGroupBoxPrintSettings.Text = currentProvider.GetLocalizedString("GanttSettingsPrintOrder");
      this.radRadioButtonRowMajorOrder.Text = currentProvider.GetLocalizedString("GanttSettingsDownThanOver");
      this.radRadioButtonColumnMajorOrder.Text = currentProvider.GetLocalizedString("GanttSettingsOverThanDown");
      this.radGroupBoxTextView.Text = currentProvider.GetLocalizedString("GanttSettingsTextView");
      this.radCheckBoxPrintTextView.Text = currentProvider.GetLocalizedString("GanttSettingsPrintTextViewPart");
      this.radLabelDataCellBackground.Text = currentProvider.GetLocalizedString("GanttSettingsDataCellsBackground");
      this.radLabelDataCellFont.Text = currentProvider.GetLocalizedString("GanttSettingsDataCellsFont");
      this.radLabelHeaderCellBackground.Text = currentProvider.GetLocalizedString("GanttSettingsHeaderCellsBackground");
      this.radLabelHeaderCellFont.Text = currentProvider.GetLocalizedString("GanttSettingsHeaderCellsFont");
      this.radGroupBoxGraphicalView.Text = currentProvider.GetLocalizedString("GanttSettingsGraphicalView");
      this.radLabelConnectorsColor.Text = currentProvider.GetLocalizedString("GanttSettingsConnectorsColor");
      this.radLabelSummaryBackground.Text = currentProvider.GetLocalizedString("GanttSettingsSummaryBackground");
      this.radLabelMilestoneBackground.Text = currentProvider.GetLocalizedString("GanttSettingsMilestoneBackground");
      this.radLabelTasksBackground.Text = currentProvider.GetLocalizedString("GanttSettingsTasksBackground");
      this.radLabelTasksBorderColor.Text = currentProvider.GetLocalizedString("GanttSettingsTasksBorderColor");
      this.radLabelTasksFont.Text = currentProvider.GetLocalizedString("GanttSettingsTasksFont");
      this.radLabelTimeline.Text = currentProvider.GetLocalizedString("GanttSettingsTimeline");
      this.radLabelTopRowBackground.Text = currentProvider.GetLocalizedString("GanttSettingsTopRowBackground");
      this.radLabelTopRowFont.Text = currentProvider.GetLocalizedString("GanttSettingsTopRowFont");
      this.radLabelBottomRowBackground.Text = currentProvider.GetLocalizedString("GanttSettingsBottomRowBackground");
      this.radLabelBottomRowFont.Text = currentProvider.GetLocalizedString("GanttSettingsBottomRowFont");
      this.radCheckBoxTimelineVisibleOnEveryPage.Text = currentProvider.GetLocalizedString("GanttSettingsTimelineVisibleOnEveryPage");
    }

    public void LoadPrintSettings(GanttViewPrintSettings printSettings)
    {
      this.UnwireEvents();
      this.LoadGeneralPrintSettings(printSettings);
      this.SetPreviewImage();
      this.WireEvents();
    }

    protected virtual void LoadGeneralPrintSettings(GanttViewPrintSettings printSettings)
    {
      if (printSettings.PrintDirection == GanttPrintDirection.RowMajorOrder)
        this.radRadioButtonRowMajorOrder.IsChecked = true;
      else
        this.radRadioButtonColumnMajorOrder.IsChecked = true;
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (Font));
      this.radCheckBoxPrintTextView.IsChecked = printSettings.PrintTextViewPart;
      this.radColorBoxDataCellBackColor.Value = printSettings.DataCellFill;
      this.radBrowseEditorDataCellsFont.Value = converter.ConvertToString((object) printSettings.DataCellFont);
      this.radColorBoxHeaderCellBackColor.Value = printSettings.HeaderCellFill;
      this.radBrowseEditorHeaderCellsFont.Text = converter.ConvertToString((object) printSettings.HeaderCellFont);
      this.radColorBoxTextViewBorders.Value = printSettings.DataCellBorder;
      this.radColorBoxLinksColor.Value = printSettings.LinksColor;
      this.radColorBoxSummaryBackColor.Value = printSettings.SummaryTaskFill;
      this.radColorBoxMilestoneBackColor.Value = printSettings.MilestoneTaskFill;
      this.radColorBoxTasksBackColor.Value = printSettings.TaskFill;
      this.radColorBoxTasksBorderColor.Value = printSettings.TaskBorder;
      this.radBrowseEditorTasksFont.Text = converter.ConvertToString((object) printSettings.TaskFont);
      this.radColorBoxTimelineTopRowBackColor.Value = printSettings.TimelineTopRowFill;
      this.radBrowseEditorTopRowFont.Text = converter.ConvertToString((object) printSettings.TimelineTopRowFont);
      this.radColorBoxTimelineBottomRowBackColor.Value = printSettings.TimelineBottomRowFill;
      this.radBrowseEditorBottomRowFont.Text = converter.ConvertToString((object) printSettings.TimelineBottomRowFont);
    }

    protected virtual void ApplyPrintSettings()
    {
      FontConverter fontConverter = new FontConverter();
      this.printSettings.PrintDirection = this.radRadioButtonRowMajorOrder.IsChecked ? GanttPrintDirection.RowMajorOrder : GanttPrintDirection.ColumnMajorOrder;
      this.printSettings.PrintTextViewPart = this.radCheckBoxPrintTextView.IsChecked;
      this.printSettings.DataCellFill = this.radColorBoxDataCellBackColor.Value;
      this.printSettings.DataCellBorder = this.radColorBoxTextViewBorders.Value;
      this.printSettings.HeaderCellFill = this.radColorBoxHeaderCellBackColor.Value;
      this.printSettings.HeaderCellBorder = this.radColorBoxTextViewBorders.Value;
      this.printSettings.LinksColor = this.radColorBoxLinksColor.Value;
      this.printSettings.SummaryTaskFill = this.radColorBoxSummaryBackColor.Value;
      this.printSettings.MilestoneTaskFill = this.radColorBoxMilestoneBackColor.Value;
      this.printSettings.TaskFill = this.radColorBoxTasksBackColor.Value;
      this.printSettings.TaskBorder = this.radColorBoxTasksBorderColor.Value;
      this.printSettings.TimelineTopRowFill = this.radColorBoxTimelineTopRowBackColor.Value;
      this.printSettings.TimelineBottomRowFill = this.radColorBoxTimelineBottomRowBackColor.Value;
      this.printSettings.TimelineVisibleOnEveryPage = this.radCheckBoxTimelineVisibleOnEveryPage.IsChecked;
      if (!string.IsNullOrEmpty(this.radBrowseEditorDataCellsFont.Value))
        this.printSettings.DataCellFont = fontConverter.ConvertFromString(this.radBrowseEditorDataCellsFont.Value) as Font;
      if (!string.IsNullOrEmpty(this.radBrowseEditorHeaderCellsFont.Value))
        this.printSettings.HeaderCellFont = fontConverter.ConvertFromString(this.radBrowseEditorHeaderCellsFont.Value) as Font;
      if (!string.IsNullOrEmpty(this.radBrowseEditorTasksFont.Value))
        this.printSettings.TaskFont = fontConverter.ConvertFromString(this.radBrowseEditorTasksFont.Value) as Font;
      if (!string.IsNullOrEmpty(this.radBrowseEditorTopRowFont.Value))
        this.printSettings.TimelineTopRowFont = fontConverter.ConvertFromString(this.radBrowseEditorTopRowFont.Value) as Font;
      if (string.IsNullOrEmpty(this.radBrowseEditorBottomRowFont.Value))
        return;
      this.printSettings.TimelineBottomRowFont = fontConverter.ConvertFromString(this.radBrowseEditorBottomRowFont.Value) as Font;
    }

    protected virtual void SetPreviewImage()
    {
      int num = this.radRadioButtonRowMajorOrder.IsChecked ? 1 : 0;
      Image image = this.radRadioButtonRowMajorOrder.IsChecked ? this.downThenOverImage : this.overThenDownImage;
      bool flag = false;
      GanttViewPrintSettingsDialog form = this.FindForm() as GanttViewPrintSettingsDialog;
      if (form != null && form.PrintDocument != null && form.PrintDocument.Watermark != null && (form.PrintDocument.Watermark.DrawText || form.PrintDocument.Watermark.DrawImage))
        flag = true;
      if (flag)
      {
        Image watermarkImage = this.LoadImage("Telerik.WinControls.UI.Printing.watermark.png");
        this.AddWatermarkSign(image, watermarkImage);
      }
      this.imageBox.Image = image;
      this.imageBox.Invalidate();
    }

    internal void AdjustControlsForTouchLayout(string themeName)
    {
      if (!TelerikHelper.IsMaterialTheme(themeName))
        return;
      this.radGroupBoxGraphicalView.Location = new Point(335, 4);
      this.radGroupBoxGraphicalView.Size = new Size(354, 449);
      this.radBrowseEditorBottomRowFont.Location = new Point(164, 388);
      this.radBrowseEditorBottomRowFont.Size = new Size(180, 36);
      this.radBrowseEditorTopRowFont.Location = new Point(164, 310);
      this.radBrowseEditorTopRowFont.Size = new Size(180, 36);
      this.radBrowseEditorTasksFont.Location = new Point(165, 217);
      this.radBrowseEditorTasksFont.Size = new Size(180, 36);
      this.radSeparator1.Location = new Point(79, 258);
      this.radSeparator1.Size = new Size(267, 10);
      this.radCheckBoxTimelineVisibleOnEveryPage.Location = new Point(5, 425);
      this.radCheckBoxTimelineVisibleOnEveryPage.Size = new Size(219, 19);
      this.radColorBoxTimelineBottomRowBackColor.Location = new Point(164, 349);
      this.radColorBoxTimelineBottomRowBackColor.Size = new Size(180, 36);
      this.radColorBoxTimelineTopRowBackColor.Location = new Point(164, 271);
      this.radColorBoxTimelineTopRowBackColor.Size = new Size(180, 36);
      this.radColorBoxTasksBorderColor.Location = new Point(165, 178);
      this.radColorBoxTasksBorderColor.Size = new Size(180, 36);
      this.radColorBoxMilestoneBackColor.Location = new Point(165, 100);
      this.radColorBoxMilestoneBackColor.Size = new Size(180, 36);
      this.radColorBoxTasksBackColor.Location = new Point(165, 139);
      this.radColorBoxTasksBackColor.Size = new Size(180, 36);
      this.radLabelBottomRowFont.Location = new Point(5, 396);
      this.radLabelBottomRowFont.Size = new Size(112, 21);
      this.radLabelBottomRowBackground.Location = new Point(5, 357);
      this.radLabelBottomRowBackground.Size = new Size(163, 21);
      this.radColorBoxSummaryBackColor.Location = new Point(165, 61);
      this.radColorBoxSummaryBackColor.Size = new Size(180, 36);
      this.radLabelTimeline.Location = new Point(6, 253);
      this.radLabelTimeline.Size = new Size(63, 21);
      this.radLabelTopRowFont.Location = new Point(5, 318);
      this.radLabelTopRowFont.Size = new Size(90, 21);
      this.radLabelTopRowBackground.Location = new Point(5, 279);
      this.radLabelTopRowBackground.Size = new Size(140, 21);
      this.radColorBoxLinksColor.Location = new Point(165, 22);
      this.radColorBoxLinksColor.Size = new Size(180, 36);
      this.radLabelTasksFont.Location = new Point(5, 225);
      this.radLabelTasksFont.Size = new Size(76, 21);
      this.radLabelTasksBorderColor.Location = new Point(5, 186);
      this.radLabelTasksBorderColor.Size = new Size(128, 21);
      this.radLabelTasksBackground.Location = new Point(5, 149);
      this.radLabelTasksBackground.Size = new Size(126, 21);
      this.radLabelMilestoneBackground.Location = new Point(5, 108);
      this.radLabelMilestoneBackground.Size = new Size(151, 21);
      this.radLabelSummaryBackground.Location = new Point(5, 71);
      this.radLabelSummaryBackground.Size = new Size(149, 21);
      this.radLabelConnectorsColor.Location = new Point(5, 34);
      this.radLabelConnectorsColor.Size = new Size(118, 21);
      this.radGroupBoxPrintSettings.Location = new Point(4, 4);
      this.radGroupBoxPrintSettings.Size = new Size(325, 143);
      this.radRadioButtonRowMajorOrder.Location = new Point(156, 60);
      this.radRadioButtonRowMajorOrder.Size = new Size(130, 22);
      this.radRadioButtonColumnMajorOrder.Location = new Point(156, 36);
      this.radRadioButtonColumnMajorOrder.Size = new Size(130, 22);
      this.radPanel1.Location = new Point(13, 34);
      this.radPanel1.Size = new Size(132, 97);
      this.radButtonWatermark.Location = new Point(156, 95);
      this.radButtonWatermark.Size = new Size(158, 36);
      this.radGroupBoxTextView.Location = new Point(4, 153);
      this.radGroupBoxTextView.Size = new Size(325, 300);
      this.radBrowseEditorHeaderCellsFont.Location = new Point(124, 195);
      this.radBrowseEditorHeaderCellsFont.Size = new Size(190, 36);
      this.radBrowseEditorDataCellsFont.Location = new Point(124, 107);
      this.radBrowseEditorDataCellsFont.Size = new Size(190, 36);
      this.radCheckBoxPrintTextView.Location = new Point(11, 35);
      this.radCheckBoxPrintTextView.Size = new Size(152, 19);
      this.radLabelDataCellFont.Location = new Point(8, 115);
      this.radLabelDataCellFont.Size = new Size(93, 21);
      this.radLabelDataCellBackground.AutoSize = false;
      this.radLabelDataCellBackground.Location = new Point(8, 65);
      this.radLabelDataCellBackground.Size = new Size(94, 36);
      this.radLabelBordersColor.Location = new Point(8, 249);
      this.radLabelBordersColor.Size = new Size(94, 21);
      this.radLabelHeaderCellFont.Location = new Point(8, 203);
      this.radLabelHeaderCellFont.Size = new Size(110, 21);
      this.radLabelHeaderCellBackground.AutoSize = false;
      this.radLabelHeaderCellBackground.Location = new Point(8, 151);
      this.radLabelHeaderCellBackground.Size = new Size(110, 38);
      this.radColorBoxDataCellBackColor.Location = new Point(124, 63);
      this.radColorBoxDataCellBackColor.Size = new Size(190, 36);
      this.radColorBoxTextViewBorders.Location = new Point(124, 239);
      this.radColorBoxTextViewBorders.Size = new Size(190, 36);
      this.radColorBoxHeaderCellBackColor.Location = new Point(124, 151);
      this.radColorBoxHeaderCellBackColor.Size = new Size(190, 36);
    }

    private Image LoadImage(string name)
    {
      Stream manifestResourceStream = Assembly.GetAssembly(typeof (LightVisualElement)).GetManifestResourceStream(name);
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

    protected virtual void SetTextViewControlsEnabled(bool enabled)
    {
      this.radColorBoxDataCellBackColor.Enabled = enabled;
      this.radBrowseEditorDataCellsFont.Enabled = enabled;
      this.radColorBoxHeaderCellBackColor.Enabled = enabled;
      this.radBrowseEditorHeaderCellsFont.Enabled = enabled;
      this.radColorBoxTextViewBorders.Enabled = enabled;
    }

    protected virtual void WireEvents()
    {
      this.radRadioButtonColumnMajorOrder.ToggleStateChanged += new StateChangedEventHandler(this.radRadioButtonRowMajorOrder_ToggleStateChanged);
      this.radCheckBoxPrintTextView.ToggleStateChanged += new StateChangedEventHandler(this.radCheckBoxPrintTextView_ToggleStateChanged);
    }

    protected virtual void UnwireEvents()
    {
      this.radRadioButtonColumnMajorOrder.ToggleStateChanged -= new StateChangedEventHandler(this.radRadioButtonRowMajorOrder_ToggleStateChanged);
      this.radCheckBoxPrintTextView.ToggleStateChanged -= new StateChangedEventHandler(this.radCheckBoxPrintTextView_ToggleStateChanged);
    }

    internal void CorrectPositions()
    {
      this.radGroupBoxPrintSettings.Height += this.radGroupBoxGraphicalView.Bottom - this.radGroupBoxPrintSettings.Bottom;
      this.radGroupBoxTextView.Width += this.radGroupBoxGraphicalView.Right - this.radGroupBoxTextView.Right;
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.Parent == null)
        return;
      this.radGroupBoxPrintSettings.GroupBoxElement.Header.Fill.BackColor = this.Parent.BackColor;
      this.radGroupBoxTextView.GroupBoxElement.Header.Fill.BackColor = this.Parent.BackColor;
      this.radGroupBoxGraphicalView.GroupBoxElement.Header.Fill.BackColor = this.Parent.BackColor;
    }

    private void radButtonWatermark_Click(object sender, EventArgs e)
    {
      GanttViewPrintSettingsDialog form = this.FindForm() as GanttViewPrintSettingsDialog;
      if (form == null)
        return;
      WatermarkPreviewDialog watermarkPreviewDialog = new WatermarkPreviewDialog(form.PrintDocument);
      watermarkPreviewDialog.ThemeName = form.ThemeName;
      int num = (int) watermarkPreviewDialog.ShowDialog();
      this.SetPreviewImage();
    }

    private void radRadioButtonRowMajorOrder_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.SetPreviewImage();
    }

    private void radCheckBoxPrintTextView_ToggleStateChanged(
      object sender,
      StateChangedEventArgs args)
    {
      this.SetTextViewControlsEnabled(this.radCheckBoxPrintTextView.IsChecked);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.radGroupBoxGraphicalView = new RadGroupBox();
      this.radSeparator1 = new RadSeparator();
      this.radCheckBoxTimelineVisibleOnEveryPage = new RadCheckBox();
      this.radColorBoxTimelineBottomRowBackColor = new RadColorBox();
      this.radColorBoxTimelineTopRowBackColor = new RadColorBox();
      this.radColorBoxTasksBorderColor = new RadColorBox();
      this.radColorBoxMilestoneBackColor = new RadColorBox();
      this.radColorBoxTasksBackColor = new RadColorBox();
      this.radLabelBottomRowBackground = new RadLabel();
      this.radColorBoxSummaryBackColor = new RadColorBox();
      this.radLabelTimeline = new RadLabel();
      this.radLabelTopRowBackground = new RadLabel();
      this.radColorBoxLinksColor = new RadColorBox();
      this.radLabelTasksBorderColor = new RadLabel();
      this.radLabelTasksBackground = new RadLabel();
      this.radLabelMilestoneBackground = new RadLabel();
      this.radLabelSummaryBackground = new RadLabel();
      this.radLabelConnectorsColor = new RadLabel();
      this.radGroupBoxPrintSettings = new RadGroupBox();
      this.radRadioButtonRowMajorOrder = new RadRadioButton();
      this.radRadioButtonColumnMajorOrder = new RadRadioButton();
      this.radPanel1 = new RadPanel();
      this.radButtonWatermark = new RadButton();
      this.radGroupBoxTextView = new RadGroupBox();
      this.radBrowseEditorDataCellsFont = new RadBrowseEditor();
      this.radCheckBoxPrintTextView = new RadCheckBox();
      this.radLabelDataCellFont = new RadLabel();
      this.radLabelDataCellBackground = new RadLabel();
      this.radLabelBordersColor = new RadLabel();
      this.radLabelHeaderCellBackground = new RadLabel();
      this.radColorBoxDataCellBackColor = new RadColorBox();
      this.radColorBoxTextViewBorders = new RadColorBox();
      this.radColorBoxHeaderCellBackColor = new RadColorBox();
      this.radBrowseEditorHeaderCellsFont = new RadBrowseEditor();
      this.radLabelHeaderCellFont = new RadLabel();
      this.radBrowseEditorTasksFont = new RadBrowseEditor();
      this.radLabelTasksFont = new RadLabel();
      this.radBrowseEditorTopRowFont = new RadBrowseEditor();
      this.radLabelTopRowFont = new RadLabel();
      this.radBrowseEditorBottomRowFont = new RadBrowseEditor();
      this.radLabelBottomRowFont = new RadLabel();
      this.radGroupBoxGraphicalView.BeginInit();
      this.radGroupBoxGraphicalView.SuspendLayout();
      this.radSeparator1.BeginInit();
      this.radCheckBoxTimelineVisibleOnEveryPage.BeginInit();
      this.radColorBoxTimelineBottomRowBackColor.BeginInit();
      this.radColorBoxTimelineTopRowBackColor.BeginInit();
      this.radColorBoxTasksBorderColor.BeginInit();
      this.radColorBoxMilestoneBackColor.BeginInit();
      this.radColorBoxTasksBackColor.BeginInit();
      this.radLabelBottomRowBackground.BeginInit();
      this.radColorBoxSummaryBackColor.BeginInit();
      this.radLabelTimeline.BeginInit();
      this.radLabelTopRowBackground.BeginInit();
      this.radColorBoxLinksColor.BeginInit();
      this.radLabelTasksBorderColor.BeginInit();
      this.radLabelTasksBackground.BeginInit();
      this.radLabelMilestoneBackground.BeginInit();
      this.radLabelSummaryBackground.BeginInit();
      this.radLabelConnectorsColor.BeginInit();
      this.radGroupBoxPrintSettings.BeginInit();
      this.radGroupBoxPrintSettings.SuspendLayout();
      this.radRadioButtonRowMajorOrder.BeginInit();
      this.radRadioButtonColumnMajorOrder.BeginInit();
      this.radPanel1.BeginInit();
      this.radButtonWatermark.BeginInit();
      this.radGroupBoxTextView.BeginInit();
      this.radGroupBoxTextView.SuspendLayout();
      this.radBrowseEditorDataCellsFont.BeginInit();
      this.radCheckBoxPrintTextView.BeginInit();
      this.radLabelDataCellFont.BeginInit();
      this.radLabelDataCellBackground.BeginInit();
      this.radLabelBordersColor.BeginInit();
      this.radLabelHeaderCellBackground.BeginInit();
      this.radColorBoxDataCellBackColor.BeginInit();
      this.radColorBoxTextViewBorders.BeginInit();
      this.radColorBoxHeaderCellBackColor.BeginInit();
      this.radBrowseEditorHeaderCellsFont.BeginInit();
      this.radLabelHeaderCellFont.BeginInit();
      this.radBrowseEditorTasksFont.BeginInit();
      this.radLabelTasksFont.BeginInit();
      this.radBrowseEditorTopRowFont.BeginInit();
      this.radLabelTopRowFont.BeginInit();
      this.radBrowseEditorBottomRowFont.BeginInit();
      this.radLabelBottomRowFont.BeginInit();
      this.SuspendLayout();
      this.radGroupBoxGraphicalView.AccessibleRole = AccessibleRole.Grouping;
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radBrowseEditorBottomRowFont);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radBrowseEditorTopRowFont);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radBrowseEditorTasksFont);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radSeparator1);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radCheckBoxTimelineVisibleOnEveryPage);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radColorBoxTimelineBottomRowBackColor);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radColorBoxTimelineTopRowBackColor);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radColorBoxTasksBorderColor);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radColorBoxMilestoneBackColor);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radColorBoxTasksBackColor);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelBottomRowFont);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelBottomRowBackground);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radColorBoxSummaryBackColor);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelTimeline);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelTopRowFont);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelTopRowBackground);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radColorBoxLinksColor);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelTasksFont);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelTasksBorderColor);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelTasksBackground);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelMilestoneBackground);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelSummaryBackground);
      this.radGroupBoxGraphicalView.Controls.Add((Control) this.radLabelConnectorsColor);
      this.radGroupBoxGraphicalView.HeaderText = "Graphical view";
      this.radGroupBoxGraphicalView.Location = new Point(321, 4);
      this.radGroupBoxGraphicalView.Name = "radGroupBoxGraphicalView";
      this.radGroupBoxGraphicalView.Size = new Size(301, 362);
      this.radGroupBoxGraphicalView.TabIndex = 3;
      this.radGroupBoxGraphicalView.Text = "Graphical view";
      this.radSeparator1.BackColor = Color.Transparent;
      this.radSeparator1.Location = new Point(60, 192);
      this.radSeparator1.Name = "radSeparator1";
      this.radSeparator1.Size = new Size(236, 18);
      this.radSeparator1.TabIndex = 5;
      this.radSeparator1.Text = "radSeparator1";
      this.radCheckBoxTimelineVisibleOnEveryPage.Location = new Point(12, 334);
      this.radCheckBoxTimelineVisibleOnEveryPage.Name = "radCheckBoxTimelineVisibleOnEveryPage";
      this.radCheckBoxTimelineVisibleOnEveryPage.Size = new Size(171, 18);
      this.radCheckBoxTimelineVisibleOnEveryPage.TabIndex = 18;
      this.radCheckBoxTimelineVisibleOnEveryPage.Text = "Timeline visible on every page";
      this.radColorBoxTimelineBottomRowBackColor.Location = new Point(146, 278);
      this.radColorBoxTimelineBottomRowBackColor.Name = "radColorBoxTimelineBottomRowBackColor";
      this.radColorBoxTimelineBottomRowBackColor.Size = new Size(150, 20);
      this.radColorBoxTimelineBottomRowBackColor.TabIndex = 16;
      this.radColorBoxTimelineBottomRowBackColor.Text = "radColorBox1";
      this.radColorBoxTimelineTopRowBackColor.Location = new Point(146, 215);
      this.radColorBoxTimelineTopRowBackColor.Name = "radColorBoxTimelineTopRowBackColor";
      this.radColorBoxTimelineTopRowBackColor.Size = new Size(150, 20);
      this.radColorBoxTimelineTopRowBackColor.TabIndex = 14;
      this.radColorBoxTimelineTopRowBackColor.Text = "radColorBox1";
      this.radColorBoxTasksBorderColor.Location = new Point(147, 134);
      this.radColorBoxTasksBorderColor.Name = "radColorBoxTasksBorderColor";
      this.radColorBoxTasksBorderColor.Size = new Size(150, 20);
      this.radColorBoxTasksBorderColor.TabIndex = 12;
      this.radColorBoxTasksBorderColor.Text = "radColorBox1";
      this.radColorBoxMilestoneBackColor.Location = new Point(147, 74);
      this.radColorBoxMilestoneBackColor.Name = "radColorBoxMilestoneBackColor";
      this.radColorBoxMilestoneBackColor.Size = new Size(150, 20);
      this.radColorBoxMilestoneBackColor.TabIndex = 10;
      this.radColorBoxMilestoneBackColor.Text = "radColorBox1";
      this.radColorBoxTasksBackColor.Location = new Point(147, 108);
      this.radColorBoxTasksBackColor.Name = "radColorBoxTasksBackColor";
      this.radColorBoxTasksBackColor.Size = new Size(150, 20);
      this.radColorBoxTasksBackColor.TabIndex = 11;
      this.radColorBoxTasksBackColor.Text = "radColorBox1";
      this.radLabelBottomRowBackground.Location = new Point(9, 280);
      this.radLabelBottomRowBackground.Name = "radLabelBottomRowBackground";
      this.radLabelBottomRowBackground.Size = new Size(128, 18);
      this.radLabelBottomRowBackground.TabIndex = 0;
      this.radLabelBottomRowBackground.Text = "Bottom row background";
      this.radColorBoxSummaryBackColor.Location = new Point(147, 48);
      this.radColorBoxSummaryBackColor.Name = "radColorBoxSummaryBackColor";
      this.radColorBoxSummaryBackColor.Size = new Size(150, 20);
      this.radColorBoxSummaryBackColor.TabIndex = 9;
      this.radColorBoxSummaryBackColor.Text = "radColorBox1";
      this.radLabelTimeline.Location = new Point(10, 191);
      this.radLabelTimeline.Name = "radLabelTimeline";
      this.radLabelTimeline.Size = new Size(48, 18);
      this.radLabelTimeline.TabIndex = 0;
      this.radLabelTimeline.Text = "Timeline";
      this.radLabelTopRowBackground.Location = new Point(9, 217);
      this.radLabelTopRowBackground.Name = "radLabelTopRowBackground";
      this.radLabelTopRowBackground.Size = new Size(110, 18);
      this.radLabelTopRowBackground.TabIndex = 0;
      this.radLabelTopRowBackground.Text = "Top row background";
      this.radColorBoxLinksColor.Location = new Point(147, 22);
      this.radColorBoxLinksColor.Name = "radColorBoxLinksColor";
      this.radColorBoxLinksColor.Size = new Size(150, 20);
      this.radColorBoxLinksColor.TabIndex = 8;
      this.radColorBoxLinksColor.Text = "radColorBox1";
      this.radLabelTasksBorderColor.Location = new Point(9, 134);
      this.radLabelTasksBorderColor.Name = "radLabelTasksBorderColor";
      this.radLabelTasksBorderColor.Size = new Size(98, 18);
      this.radLabelTasksBorderColor.TabIndex = 0;
      this.radLabelTasksBorderColor.Text = "Tasks border color";
      this.radLabelTasksBackground.Location = new Point(9, 110);
      this.radLabelTasksBackground.Name = "radLabelTasksBackground";
      this.radLabelTasksBackground.Size = new Size(96, 18);
      this.radLabelTasksBackground.TabIndex = 0;
      this.radLabelTasksBackground.Text = "Tasks background";
      this.radLabelMilestoneBackground.Location = new Point(9, 74);
      this.radLabelMilestoneBackground.Name = "radLabelMilestoneBackground";
      this.radLabelMilestoneBackground.Size = new Size(119, 18);
      this.radLabelMilestoneBackground.TabIndex = 0;
      this.radLabelMilestoneBackground.Text = "Milestone background";
      this.radLabelSummaryBackground.Location = new Point(9, 50);
      this.radLabelSummaryBackground.Name = "radLabelSummaryBackground";
      this.radLabelSummaryBackground.Size = new Size(116, 18);
      this.radLabelSummaryBackground.TabIndex = 0;
      this.radLabelSummaryBackground.Text = "Summary background";
      this.radLabelConnectorsColor.Location = new Point(9, 26);
      this.radLabelConnectorsColor.Name = "radLabelConnectorsColor";
      this.radLabelConnectorsColor.Size = new Size(91, 18);
      this.radLabelConnectorsColor.TabIndex = 0;
      this.radLabelConnectorsColor.Text = "Connectors color";
      this.radGroupBoxPrintSettings.AccessibleRole = AccessibleRole.Grouping;
      this.radGroupBoxPrintSettings.Controls.Add((Control) this.radRadioButtonRowMajorOrder);
      this.radGroupBoxPrintSettings.Controls.Add((Control) this.radRadioButtonColumnMajorOrder);
      this.radGroupBoxPrintSettings.Controls.Add((Control) this.radPanel1);
      this.radGroupBoxPrintSettings.Controls.Add((Control) this.radButtonWatermark);
      this.radGroupBoxPrintSettings.HeaderText = "Print settings";
      this.radGroupBoxPrintSettings.Location = new Point(4, 4);
      this.radGroupBoxPrintSettings.Name = "radGroupBoxPrintSettings";
      this.radGroupBoxPrintSettings.Size = new Size(311, (int) sbyte.MaxValue);
      this.radGroupBoxPrintSettings.TabIndex = 2;
      this.radGroupBoxPrintSettings.Text = "Print settings";
      this.radRadioButtonRowMajorOrder.Location = new Point(182, 59);
      this.radRadioButtonRowMajorOrder.Name = "radRadioButtonRowMajorOrder";
      this.radRadioButtonRowMajorOrder.Size = new Size(103, 18);
      this.radRadioButtonRowMajorOrder.TabIndex = 10;
      this.radRadioButtonRowMajorOrder.Text = "Down, then over";
      this.radRadioButtonColumnMajorOrder.Location = new Point(182, 35);
      this.radRadioButtonColumnMajorOrder.Name = "radRadioButtonColumnMajorOrder";
      this.radRadioButtonColumnMajorOrder.Size = new Size(103, 18);
      this.radRadioButtonColumnMajorOrder.TabIndex = 10;
      this.radRadioButtonColumnMajorOrder.Text = "Over, then down";
      this.radPanel1.Location = new Point(13, 22);
      this.radPanel1.Name = "radPanel1";
      this.radPanel1.Size = new Size(132, 97);
      this.radPanel1.TabIndex = 9;
      this.radButtonWatermark.Location = new Point(183, 94);
      this.radButtonWatermark.Name = "radButtonWatermark";
      this.radButtonWatermark.Size = new Size(102, 24);
      this.radButtonWatermark.TabIndex = 1;
      this.radButtonWatermark.Text = "Watermark...";
      this.radButtonWatermark.Click += new EventHandler(this.radButtonWatermark_Click);
      this.radGroupBoxTextView.AccessibleRole = AccessibleRole.Grouping;
      this.radGroupBoxTextView.Controls.Add((Control) this.radBrowseEditorHeaderCellsFont);
      this.radGroupBoxTextView.Controls.Add((Control) this.radBrowseEditorDataCellsFont);
      this.radGroupBoxTextView.Controls.Add((Control) this.radCheckBoxPrintTextView);
      this.radGroupBoxTextView.Controls.Add((Control) this.radLabelDataCellFont);
      this.radGroupBoxTextView.Controls.Add((Control) this.radLabelDataCellBackground);
      this.radGroupBoxTextView.Controls.Add((Control) this.radLabelBordersColor);
      this.radGroupBoxTextView.Controls.Add((Control) this.radLabelHeaderCellFont);
      this.radGroupBoxTextView.Controls.Add((Control) this.radLabelHeaderCellBackground);
      this.radGroupBoxTextView.Controls.Add((Control) this.radColorBoxDataCellBackColor);
      this.radGroupBoxTextView.Controls.Add((Control) this.radColorBoxTextViewBorders);
      this.radGroupBoxTextView.Controls.Add((Control) this.radColorBoxHeaderCellBackColor);
      this.radGroupBoxTextView.HeaderText = "Text view";
      this.radGroupBoxTextView.Location = new Point(4, 137);
      this.radGroupBoxTextView.Name = "radGroupBoxTextView";
      this.radGroupBoxTextView.Size = new Size(311, 229);
      this.radGroupBoxTextView.TabIndex = 1;
      this.radGroupBoxTextView.Text = "Text view";
      this.radBrowseEditorDataCellsFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorDataCellsFont.Location = new Point(156, 82);
      this.radBrowseEditorDataCellsFont.Name = "radBrowseEditorDataCellsFont";
      this.radBrowseEditorDataCellsFont.Size = new Size(150, 20);
      this.radBrowseEditorDataCellsFont.TabIndex = 8;
      this.radBrowseEditorDataCellsFont.Text = "radBrowseEditor1";
      this.radCheckBoxPrintTextView.Location = new Point(11, 28);
      this.radCheckBoxPrintTextView.Name = "radCheckBoxPrintTextView";
      this.radCheckBoxPrintTextView.Size = new Size(118, 18);
      this.radCheckBoxPrintTextView.TabIndex = 2;
      this.radCheckBoxPrintTextView.Text = "Print Text View part";
      this.radLabelDataCellFont.Location = new Point(8, 84);
      this.radLabelDataCellFont.Name = "radLabelDataCellFont";
      this.radLabelDataCellFont.Size = new Size(73, 18);
      this.radLabelDataCellFont.TabIndex = 0;
      this.radLabelDataCellFont.Text = "Data cell font";
      this.radLabelDataCellBackground.Location = new Point(8, 58);
      this.radLabelDataCellBackground.Name = "radLabelDataCellBackground";
      this.radLabelDataCellBackground.Size = new Size(112, 18);
      this.radLabelDataCellBackground.TabIndex = 0;
      this.radLabelDataCellBackground.Text = "Data cell background";
      this.radLabelBordersColor.Location = new Point(8, 182);
      this.radLabelBordersColor.Name = "radLabelBordersColor";
      this.radLabelBordersColor.Size = new Size(73, 18);
      this.radLabelBordersColor.TabIndex = 0;
      this.radLabelBordersColor.Text = "Borders color";
      this.radLabelHeaderCellBackground.Location = new Point(8, 119);
      this.radLabelHeaderCellBackground.Name = "radLabelHeaderCellBackground";
      this.radLabelHeaderCellBackground.Size = new Size(125, 18);
      this.radLabelHeaderCellBackground.TabIndex = 0;
      this.radLabelHeaderCellBackground.Text = "Header cell background";
      this.radColorBoxDataCellBackColor.Location = new Point(156, 56);
      this.radColorBoxDataCellBackColor.Name = "radColorBoxDataCellBackColor";
      this.radColorBoxDataCellBackColor.Size = new Size(150, 20);
      this.radColorBoxDataCellBackColor.TabIndex = 3;
      this.radColorBoxDataCellBackColor.Text = "radColorBox1";
      this.radColorBoxTextViewBorders.Location = new Point(156, 182);
      this.radColorBoxTextViewBorders.Name = "radColorBoxTextViewBorders";
      this.radColorBoxTextViewBorders.Size = new Size(150, 20);
      this.radColorBoxTextViewBorders.TabIndex = 7;
      this.radColorBoxTextViewBorders.Text = "radColorBox1";
      this.radColorBoxHeaderCellBackColor.Location = new Point(156, 119);
      this.radColorBoxHeaderCellBackColor.Name = "radColorBoxHeaderCellBackColor";
      this.radColorBoxHeaderCellBackColor.Size = new Size(150, 20);
      this.radColorBoxHeaderCellBackColor.TabIndex = 5;
      this.radColorBoxHeaderCellBackColor.Text = "radColorBox1";
      this.radBrowseEditorHeaderCellsFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorHeaderCellsFont.Location = new Point(156, 145);
      this.radBrowseEditorHeaderCellsFont.Name = "radBrowseEditorHeaderCellsFont";
      this.radBrowseEditorHeaderCellsFont.Size = new Size(150, 20);
      this.radBrowseEditorHeaderCellsFont.TabIndex = 8;
      this.radBrowseEditorHeaderCellsFont.Text = "radBrowseEditor1";
      this.radLabelHeaderCellFont.Location = new Point(8, 147);
      this.radLabelHeaderCellFont.Name = "radLabelHeaderCellFont";
      this.radLabelHeaderCellFont.Size = new Size(86, 18);
      this.radLabelHeaderCellFont.TabIndex = 0;
      this.radLabelHeaderCellFont.Text = "Header cell font";
      this.radBrowseEditorTasksFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorTasksFont.Location = new Point(147, 168);
      this.radBrowseEditorTasksFont.Name = "radBrowseEditorTasksFont";
      this.radBrowseEditorTasksFont.Size = new Size(150, 20);
      this.radBrowseEditorTasksFont.TabIndex = 8;
      this.radBrowseEditorTasksFont.Text = "radBrowseEditor1";
      this.radLabelTasksFont.Location = new Point(9, 170);
      this.radLabelTasksFont.Name = "radLabelTasksFont";
      this.radLabelTasksFont.Size = new Size(57, 18);
      this.radLabelTasksFont.TabIndex = 0;
      this.radLabelTasksFont.Text = "Tasks font";
      this.radBrowseEditorTopRowFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorTopRowFont.Location = new Point(146, 241);
      this.radBrowseEditorTopRowFont.Name = "radBrowseEditorTopRowFont";
      this.radBrowseEditorTopRowFont.Size = new Size(150, 20);
      this.radBrowseEditorTopRowFont.TabIndex = 8;
      this.radBrowseEditorTopRowFont.Text = "radBrowseEditor1";
      this.radLabelTopRowFont.Location = new Point(9, 243);
      this.radLabelTopRowFont.Name = "radLabelTopRowFont";
      this.radLabelTopRowFont.Size = new Size(71, 18);
      this.radLabelTopRowFont.TabIndex = 0;
      this.radLabelTopRowFont.Text = "Top row font";
      this.radBrowseEditorBottomRowFont.DialogType = BrowseEditorDialogType.FontDialog;
      this.radBrowseEditorBottomRowFont.Location = new Point(146, 304);
      this.radBrowseEditorBottomRowFont.Name = "radBrowseEditorBottomRowFont";
      this.radBrowseEditorBottomRowFont.Size = new Size(150, 20);
      this.radBrowseEditorBottomRowFont.TabIndex = 8;
      this.radBrowseEditorBottomRowFont.Text = "radBrowseEditor1";
      this.radLabelBottomRowFont.Location = new Point(9, 304);
      this.radLabelBottomRowFont.Name = "radLabelBottomRowFont";
      this.radLabelBottomRowFont.Size = new Size(89, 18);
      this.radLabelBottomRowFont.TabIndex = 0;
      this.radLabelBottomRowFont.Text = "Bottom row font";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.None;
      this.Controls.Add((Control) this.radGroupBoxGraphicalView);
      this.Controls.Add((Control) this.radGroupBoxPrintSettings);
      this.Controls.Add((Control) this.radGroupBoxTextView);
      this.Name = nameof (GanttViewPrintSettingsControl);
      this.Size = new Size(625, 379);
      this.radGroupBoxGraphicalView.EndInit();
      this.radGroupBoxGraphicalView.ResumeLayout(false);
      this.radGroupBoxGraphicalView.PerformLayout();
      this.radSeparator1.EndInit();
      this.radCheckBoxTimelineVisibleOnEveryPage.EndInit();
      this.radColorBoxTimelineBottomRowBackColor.EndInit();
      this.radColorBoxTimelineTopRowBackColor.EndInit();
      this.radColorBoxTasksBorderColor.EndInit();
      this.radColorBoxMilestoneBackColor.EndInit();
      this.radColorBoxTasksBackColor.EndInit();
      this.radLabelBottomRowBackground.EndInit();
      this.radColorBoxSummaryBackColor.EndInit();
      this.radLabelTimeline.EndInit();
      this.radLabelTopRowBackground.EndInit();
      this.radColorBoxLinksColor.EndInit();
      this.radLabelTasksBorderColor.EndInit();
      this.radLabelTasksBackground.EndInit();
      this.radLabelMilestoneBackground.EndInit();
      this.radLabelSummaryBackground.EndInit();
      this.radLabelConnectorsColor.EndInit();
      this.radGroupBoxPrintSettings.EndInit();
      this.radGroupBoxPrintSettings.ResumeLayout(false);
      this.radGroupBoxPrintSettings.PerformLayout();
      this.radRadioButtonRowMajorOrder.EndInit();
      this.radRadioButtonColumnMajorOrder.EndInit();
      this.radPanel1.EndInit();
      this.radButtonWatermark.EndInit();
      this.radGroupBoxTextView.EndInit();
      this.radGroupBoxTextView.ResumeLayout(false);
      this.radGroupBoxTextView.PerformLayout();
      this.radBrowseEditorDataCellsFont.EndInit();
      this.radCheckBoxPrintTextView.EndInit();
      this.radLabelDataCellFont.EndInit();
      this.radLabelDataCellBackground.EndInit();
      this.radLabelBordersColor.EndInit();
      this.radLabelHeaderCellBackground.EndInit();
      this.radColorBoxDataCellBackColor.EndInit();
      this.radColorBoxTextViewBorders.EndInit();
      this.radColorBoxHeaderCellBackColor.EndInit();
      this.radBrowseEditorHeaderCellsFont.EndInit();
      this.radLabelHeaderCellFont.EndInit();
      this.radBrowseEditorTasksFont.EndInit();
      this.radLabelTasksFont.EndInit();
      this.radBrowseEditorTopRowFont.EndInit();
      this.radLabelTopRowFont.EndInit();
      this.radBrowseEditorBottomRowFont.EndInit();
      this.radLabelBottomRowFont.EndInit();
      this.ResumeLayout(false);
    }
  }
}
