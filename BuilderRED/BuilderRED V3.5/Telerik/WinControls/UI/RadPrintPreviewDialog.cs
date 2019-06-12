// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPrintPreviewDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class RadPrintPreviewDialog : RadForm
  {
    private Point lastLocation = Point.Empty;
    private int totalPages = -1;
    private double[] zoomFactors = new double[16]{ 5.0, 8.33, 12.0, 25.0, 33.33, 50.0, 66.67, 75.0, 100.0, 200.0, 300.0, 400.0, 600.0, 800.0, 1200.0, -1.0 };
    protected ZoomMenuItem menuItemZoom;
    private TableSizePickerElement sizePickerMenu;
    private TableSizePickerElement sizePickerToolbar;
    private PrintDialog printDialog;
    private WatermarkPreviewDialog watermarkDialog;
    private IContainer components;
    protected RadPrintPreviewControl printPreviewControl;
    private RadCommandBar radCommandBar;
    private CommandBarRowElement commandBarRowElement1;
    private CommandBarStripElement stripElementTools;
    protected CommandBarButton buttonPrevPage;
    protected CommandBarTextBox textBoxCurrentPage;
    protected CommandBarButton buttonNextPage;
    protected CommandBarLabel labelTotalPages;
    private CommandBarStripElement stripElementNavigation;
    protected CommandBarButton buttonPrint;
    protected CommandBarButton buttonSettings;
    private RadMenuItem menuItemFile;
    private RadMenuItem menuItemView;
    private RadMenuItem menuItemTools;
    protected CommandBarDropDownList dropDownZoom;
    protected CommandBarButton buttonZoomOut;
    protected CommandBarButton buttonZoomIn;
    protected CommandBarDropDownButton dropDownButtonLayout;
    private RadMenuItem menuItemPrint;
    private RadMenuItem menuItemExit;
    private RadMenuSeparatorItem radMenuSeparatorItem1;
    private RadMenuItem menuItemPreviousPage;
    private RadMenuItem menuItemNextPage;
    private RadMenuItem menuItemPrintSettings;
    protected CommandBarButton commandBarButtonWatermark;
    private CommandBarSeparator commandBarSeparator1;
    private RadMenuSeparatorItem radMenuSeparatorItem2;
    private RadMenuItem menuItemLayout;
    private RadMenuItem menuItemWatermark;
    private RadMenu radMenu;

    [Browsable(false)]
    public RadPrintDocument Document
    {
      get
      {
        return this.printPreviewControl.Document as RadPrintDocument;
      }
      set
      {
        if (this.printPreviewControl.Document == value)
          return;
        if (this.printPreviewControl.Document != null)
          this.printPreviewControl.Document.EndPrint -= new PrintEventHandler(this.document_EndPrint);
        this.printPreviewControl.Document = (PrintDocument) value;
        if (value == null)
          return;
        value.EndPrint += new PrintEventHandler(this.document_EndPrint);
      }
    }

    [Browsable(false)]
    public RadMenu ToolMenu
    {
      get
      {
        return this.radMenu;
      }
    }

    [Browsable(false)]
    public RadCommandBar ToolCommandBar
    {
      get
      {
        return this.radCommandBar;
      }
    }

    [Browsable(false)]
    public PrintDialog PrintDialog
    {
      get
      {
        return this.PrintDialog;
      }
    }

    [Browsable(false)]
    public WatermarkPreviewDialog WatermarkDialog
    {
      get
      {
        return this.watermarkDialog;
      }
    }

    public RadPrintPreviewDialog()
    {
      this.InitializeComponent();
      for (int index = this.zoomFactors.Length - 1; index >= 0; --index)
      {
        if (this.zoomFactors[index] < 0.0)
          this.dropDownZoom.Items.Add(new RadListDataItem("Auto", (object) (this.zoomFactors[index] / 100.0)));
        else
          this.dropDownZoom.Items.Add(new RadListDataItem(this.zoomFactors[index].ToString() + " %", (object) (this.zoomFactors[index] / 100.0)));
      }
      this.textBoxCurrentPage.TextBoxElement.TextAlign = HorizontalAlignment.Right;
      this.dropDownZoom.DropDownListElement.TextBox.TextBoxItem.HostedControl.KeyDown += new KeyEventHandler(this.HostedControl_KeyDown);
      this.dropDownZoom.SelectedValueChanged += new ValueChangedEventHandler(this.dropDownZoom_SelectedValueChanged);
      this.buttonZoomIn.MouseDown += new MouseEventHandler(this.buttonZoomIn_MouseDown);
      this.buttonZoomOut.MouseDown += new MouseEventHandler(this.buttonZoomOut_MouseDown);
      this.sizePickerToolbar = new TableSizePickerElement();
      this.sizePickerToolbar.MaxColumns = 4;
      this.sizePickerToolbar.MaxRows = 4;
      this.sizePickerToolbar.SizeSelected += new EventHandler<TableSizeSelectedEventArgs>(this.sizePicker_SizeSelected);
      this.dropDownButtonLayout.Items.Add((RadItem) new RadMenuContentItem()
      {
        ContentElement = (RadElement) this.sizePickerToolbar
      });
      this.menuItemZoom = new ZoomMenuItem();
      this.menuItemZoom.Text = "Zoom";
      this.menuItemZoom.Editor.MinValue = new Decimal(10);
      this.menuItemZoom.Editor.MaxValue = new Decimal(200);
      this.menuItemZoom.Editor.Step = new Decimal(25, 0, 0, false, (byte) 1);
      this.menuItemZoom.Editor.FormatString = "{0:f2}%";
      this.menuItemZoom.Editor.TrimString = "%";
      this.menuItemZoom.Editor.Value = (Decimal) (this.printPreviewControl.Zoom * 100.0);
      this.menuItemZoom.Editor.ValueChanged += new EventHandler(this.Editor_ValueChanged);
      this.menuItemView.Items.Insert(0, (RadItem) this.menuItemZoom);
      this.sizePickerMenu = new TableSizePickerElement();
      this.sizePickerMenu.MaxColumns = 4;
      this.sizePickerMenu.MaxRows = 4;
      this.sizePickerMenu.SizeSelected += new EventHandler<TableSizeSelectedEventArgs>(this.sizePicker_SizeSelected);
      this.menuItemLayout.Items.Add((RadItem) new RadMenuContentItem()
      {
        ContentElement = (RadElement) this.sizePickerMenu
      });
      this.dropDownZoom.SelectedValue = (object) (this.zoomFactors[this.zoomFactors.Length - 1] / 100.0);
      this.LocalizeStrings();
      LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProviderChanged += new EventHandler(this.PrintingDialogsLocalizationProvider_CurrentProviderChanged);
      this.ThemeNameChanged += new ThemeNameChangedEventHandler(this.RadPrintPreviewDialog_ThemeNameChanged);
    }

    public RadPrintPreviewDialog(RadPrintDocument document)
      : this()
    {
      this.Document = document;
      if (this.Document == null || this.Document.AssociatedObject == null)
        return;
      if (this.Document.AssociatedObject.GetSettingsDialog(this.Document) == null)
      {
        this.buttonSettings.Visibility = ElementVisibility.Collapsed;
        this.buttonSettings.VisibleInStrip = false;
      }
      else
      {
        this.buttonSettings.Visibility = ElementVisibility.Visible;
        this.buttonSettings.VisibleInStrip = true;
      }
    }

    protected virtual PrintDialog CreatePrintDialog()
    {
      return new PrintDialog() { AllowPrintToFile = true, AllowCurrentPage = true, AllowSelection = true, AllowSomePages = true, UseEXDialog = true };
    }

    protected virtual WatermarkPreviewDialog CreateWatermarkDialog()
    {
      return new WatermarkPreviewDialog(this.Document);
    }

    public void ScrollWith(Point offset)
    {
      this.printPreviewControl.ScrollOffset = new Point(this.printPreviewControl.ScrollOffset.X - offset.X, this.printPreviewControl.ScrollOffset.Y - offset.Y);
    }

    public void SetZoom(double factor)
    {
      if (factor < 0.0)
      {
        this.printPreviewControl.AutoZoom = true;
      }
      else
      {
        this.printPreviewControl.AutoZoom = false;
        this.printPreviewControl.Zoom = factor;
        this.dropDownZoom.SelectedValueChanged -= new ValueChangedEventHandler(this.dropDownZoom_SelectedValueChanged);
        this.menuItemZoom.Editor.ValueChanged -= new EventHandler(this.Editor_ValueChanged);
        this.dropDownZoom.DropDownListElement.SelectedValue = (object) this.printPreviewControl.Zoom;
        this.dropDownZoom.DropDownListElement.TextBox.Text = string.Format("{0:p2}", (object) this.printPreviewControl.Zoom);
        this.menuItemZoom.Editor.Value = (Decimal) (this.printPreviewControl.Zoom * 100.0);
        this.dropDownZoom.SelectedValueChanged += new ValueChangedEventHandler(this.dropDownZoom_SelectedValueChanged);
        this.menuItemZoom.Editor.ValueChanged += new EventHandler(this.Editor_ValueChanged);
      }
    }

    protected virtual void LocalizeStrings()
    {
      PrintDialogsLocalizationProvider currentProvider = LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProvider;
      this.buttonPrint.Text = this.buttonPrint.ToolTipText = this.buttonPrint.DisplayName = currentProvider.GetLocalizedString("PreviewDialogPrint");
      this.buttonSettings.Text = this.buttonSettings.ToolTipText = this.buttonSettings.DisplayName = currentProvider.GetLocalizedString("PreviewDialogPrintSettings");
      this.commandBarButtonWatermark.Text = this.commandBarButtonWatermark.ToolTipText = this.commandBarButtonWatermark.DisplayName = currentProvider.GetLocalizedString("PreviewDialogWatermark");
      this.buttonPrevPage.Text = this.buttonPrevPage.ToolTipText = this.buttonPrevPage.DisplayName = currentProvider.GetLocalizedString("PreviewDialogPreviousPage");
      this.buttonNextPage.Text = this.buttonNextPage.ToolTipText = this.buttonNextPage.DisplayName = currentProvider.GetLocalizedString("PreviewDialogNextPage");
      this.buttonZoomOut.Text = this.buttonZoomOut.ToolTipText = this.buttonZoomOut.DisplayName = currentProvider.GetLocalizedString("PreviewDialogZoomOut");
      this.buttonZoomIn.Text = this.buttonZoomIn.ToolTipText = this.buttonZoomIn.DisplayName = currentProvider.GetLocalizedString("PreviewDialogZoomIn");
      this.dropDownButtonLayout.Text = this.dropDownButtonLayout.ToolTipText = this.dropDownButtonLayout.DisplayName = currentProvider.GetLocalizedString("PreviewDialogLayout");
      this.menuItemFile.Text = currentProvider.GetLocalizedString("PreviewDialogFile");
      this.menuItemView.Text = currentProvider.GetLocalizedString("PreviewDialogView");
      this.menuItemTools.Text = currentProvider.GetLocalizedString("PreviewDialogTools");
      this.menuItemPrint.Text = currentProvider.GetLocalizedString("PreviewDialogPrint");
      this.menuItemExit.Text = currentProvider.GetLocalizedString("PreviewDialogExit");
      this.menuItemZoom.Text = currentProvider.GetLocalizedString("PreviewDialogZoom");
      this.menuItemNextPage.Text = currentProvider.GetLocalizedString("PreviewDialogNextPage");
      this.menuItemPreviousPage.Text = currentProvider.GetLocalizedString("PreviewDialogPreviousPage");
      this.menuItemLayout.Text = currentProvider.GetLocalizedString("PreviewDialogLayout");
      this.menuItemPrintSettings.Text = currentProvider.GetLocalizedString("PreviewDialogPrintSettings");
      this.menuItemWatermark.Text = currentProvider.GetLocalizedString("PreviewDialogWatermark");
      this.stripElementTools.DisplayName = currentProvider.GetLocalizedString("PreviewDialogToolsStrip");
      this.stripElementNavigation.DisplayName = currentProvider.GetLocalizedString("PreviewDialogNavigationStrip");
      this.Text = currentProvider.GetLocalizedString("PreviewDialogTitle");
    }

    protected virtual void UnwireEvents()
    {
      LocalizationProvider<PrintDialogsLocalizationProvider>.CurrentProviderChanged -= new EventHandler(this.PrintingDialogsLocalizationProvider_CurrentProviderChanged);
    }

    private void UpdatePageCount()
    {
      this.totalPages = this.Document.PageCount;
      this.textBoxCurrentPage.Text = (this.printPreviewControl.StartPage + 1).ToString();
      this.labelTotalPages.Text = " / " + (object) this.totalPages;
    }

    protected virtual void OnShowPrintDialog()
    {
      if (this.Document == null)
        return;
      this.printDialog = this.CreatePrintDialog();
      this.printDialog.Document = (PrintDocument) this.Document;
      if (this.printDialog.ShowDialog() == DialogResult.OK)
      {
        try
        {
          this.Document.CurrentPage = this.printPreviewControl.StartPage + 1;
          this.Document.SelectionLength = this.printPreviewControl.Rows * this.printPreviewControl.Columns;
          this.Document.Print();
        }
        catch (Exception ex)
        {
          string themeName = RadMessageBox.ThemeName;
          RadMessageBox.SetThemeName(this.ThemeName);
          int num = (int) RadMessageBox.Show(ex.Message, "Error printing the document", MessageBoxButtons.OK, RadMessageIcon.Error);
          RadMessageBox.SetThemeName(themeName);
        }
      }
      if (this.Document == null)
        return;
      this.Document.PrinterSettings.PrintRange = PrintRange.AllPages;
    }

    protected virtual void OnShowPrintSettingsDialog()
    {
      if (this.Document == null)
        return;
      Form settingsDialog = this.Document.AssociatedObject.GetSettingsDialog(this.Document);
      RadForm radForm = settingsDialog as RadForm;
      if (radForm != null)
        radForm.ThemeName = this.ThemeName;
      if (settingsDialog.ShowDialog() != DialogResult.OK)
        return;
      this.Document.PrinterSettings.PrintRange = PrintRange.AllPages;
      this.printPreviewControl.InvalidatePreview();
      this.UpdatePageCount();
    }

    protected virtual void OnShowWatermarkDialog()
    {
      if (this.Document == null)
        return;
      this.watermarkDialog = this.CreateWatermarkDialog();
      this.watermarkDialog.ThemeName = this.ThemeName;
      if (this.watermarkDialog.ShowDialog() != DialogResult.OK)
        return;
      this.Document.PrinterSettings.PrintRange = PrintRange.AllPages;
      this.printPreviewControl.InvalidatePreview();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (!TelerikHelper.IsMaterialTheme(this.ThemeName))
        return;
      this.dropDownZoom.MinSize = new Size(110, 0);
      this.radCommandBar.MinimumSize = new Size(0, 65);
    }

    protected override void OnShown(EventArgs e)
    {
      if (this.Document != null)
        this.Document.PrinterSettings.PrintRange = PrintRange.AllPages;
      base.OnShown(e);
    }

    private void PrintingDialogsLocalizationProvider_CurrentProviderChanged(
      object sender,
      EventArgs e)
    {
      this.LocalizeStrings();
    }

    private void Editor_ValueChanged(object sender, EventArgs e)
    {
      this.SetZoom((double) this.menuItemZoom.Editor.Value / 100.0);
    }

    private void sizePicker_SizeSelected(object sender, TableSizeSelectedEventArgs e)
    {
      this.sizePickerMenu.SizeSelected -= new EventHandler<TableSizeSelectedEventArgs>(this.sizePicker_SizeSelected);
      this.sizePickerToolbar.SizeSelected -= new EventHandler<TableSizeSelectedEventArgs>(this.sizePicker_SizeSelected);
      this.dropDownButtonLayout.DropDownMenu.ClosePopup(RadPopupCloseReason.CloseCalled);
      this.printPreviewControl.Rows = e.Rows;
      this.printPreviewControl.Columns = e.Columns;
      this.dropDownZoom.SelectedValue = (object) (this.zoomFactors[this.zoomFactors.Length - 1] / 100.0);
      this.sizePickerMenu.SelectedColumns = this.sizePickerToolbar.SelectedColumns = e.Columns;
      this.sizePickerMenu.SelectedRows = this.sizePickerToolbar.SelectedRows = e.Rows;
      this.sizePickerMenu.SizeSelected += new EventHandler<TableSizeSelectedEventArgs>(this.sizePicker_SizeSelected);
      this.sizePickerToolbar.SizeSelected += new EventHandler<TableSizeSelectedEventArgs>(this.sizePicker_SizeSelected);
      this.textBoxCurrentPage.Text = (this.printPreviewControl.StartPage + 1).ToString();
    }

    private void radMenuItem6_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void document_EndPrint(object sender, PrintEventArgs e)
    {
      this.UpdatePageCount();
    }

    private void HostedControl_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      double result;
      if (!double.TryParse(this.dropDownZoom.DropDownListElement.TextBox.Text.Trim().Trim('%'), out result))
        return;
      this.SetZoom(result / 100.0);
    }

    private void buttonZoomOut_MouseDown(object sender, MouseEventArgs e)
    {
      int num1 = (int) Math.Floor(this.printPreviewControl.Zoom * 100.0);
      int num2 = num1 % 5 == 0 ? num1 - 5 : num1 - num1 % 5;
      if (num2 < 5)
        return;
      this.SetZoom((double) num2 / 100.0);
    }

    private void buttonZoomIn_MouseDown(object sender, MouseEventArgs e)
    {
      int num1 = (int) Math.Ceiling(this.printPreviewControl.Zoom * 100.0 + 5.0);
      int num2 = num1 - num1 % 5;
      if (num2 > 1600)
        return;
      this.SetZoom((double) num2 / 100.0);
    }

    private void dropDownZoom_SelectedValueChanged(object sender, Telerik.WinControls.UI.Data.ValueChangedEventArgs e)
    {
      if (!(this.dropDownZoom.SelectedValue is double))
        return;
      this.SetZoom((double) this.dropDownZoom.SelectedValue);
    }

    private void printPreviewControl1_MouseWheel(object sender, MouseEventArgs e)
    {
      if (Control.ModifierKeys == Keys.Control)
        this.SetZoom(Math.Min(16.0, Math.Max(0.05, this.printPreviewControl.Zoom + (double) e.Delta * 0.00025)));
      else
        this.ScrollWith(new Point(0, e.Delta));
    }

    private void printPreviewControl1_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.printPreviewControl.Focus();
      this.printPreviewControl.Capture = true;
      this.lastLocation = e.Location;
    }

    private void printPreviewControl1_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      this.printPreviewControl.Capture = false;
    }

    private void printPreviewControl1_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      Point offset = new Point(e.Location.X - this.lastLocation.X, e.Location.Y - this.lastLocation.Y);
      this.lastLocation = e.Location;
      this.ScrollWith(offset);
    }

    private void buttonNextPage_Click(object sender, EventArgs e)
    {
      ++this.printPreviewControl.StartPage;
    }

    private void buttonPrevPage_Click(object sender, EventArgs e)
    {
      this.printPreviewControl.StartPage = Math.Max(0, this.printPreviewControl.StartPage - 1);
    }

    private void commandBarTextBox1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      int result = 0;
      if (!int.TryParse(this.textBoxCurrentPage.Text, out result))
        return;
      this.printPreviewControl.StartPage = Math.Max(0, result - 1);
    }

    private void printPreviewControl1_StartPageChanged(object sender, EventArgs e)
    {
      this.UpdatePageCount();
    }

    private void buttonPrint_Click(object sender, EventArgs e)
    {
      this.OnShowPrintDialog();
    }

    private void buttonSettings_Click(object sender, EventArgs e)
    {
      this.OnShowPrintSettingsDialog();
    }

    private void commandBarButtonWatermark_Click(object sender, EventArgs e)
    {
      this.OnShowWatermarkDialog();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData != Keys.Escape)
        return base.ProcessCmdKey(ref msg, keyData);
      this.DialogResult = DialogResult.Cancel;
      this.Close();
      return true;
    }

    private void RadPrintPreviewDialog_ThemeNameChanged(
      object source,
      ThemeNameChangedEventArgs args)
    {
      ThemeResolutionService.ApplyThemeToControlTree((Control) this, args.newThemeName);
      ThemeResolutionService.ApplyThemeToControlTree(this.menuItemZoom.HostItem.HostedControl, args.newThemeName);
      Color backColor = this.radMenu.MenuElement.BackColor;
      this.printPreviewControl.BackColor = Color.FromArgb((int) byte.MaxValue, Math.Max((int) backColor.R - 35, 0), Math.Max((int) backColor.G - 35, 0), Math.Max((int) backColor.B - 35, 0));
      this.printPreviewControl.PageShadowColor = Color.FromArgb((int) byte.MaxValue, Math.Max((int) backColor.R - 70, 0), Math.Max((int) backColor.G - 70, 0), Math.Max((int) backColor.B - 70, 0));
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RadPrintPreviewDialog));
      this.printPreviewControl = new RadPrintPreviewControl();
      this.radCommandBar = new RadCommandBar();
      this.commandBarRowElement1 = new CommandBarRowElement();
      this.stripElementTools = new CommandBarStripElement();
      this.buttonPrint = new CommandBarButton();
      this.buttonSettings = new CommandBarButton();
      this.commandBarButtonWatermark = new CommandBarButton();
      this.stripElementNavigation = new CommandBarStripElement();
      this.buttonPrevPage = new CommandBarButton();
      this.buttonNextPage = new CommandBarButton();
      this.textBoxCurrentPage = new CommandBarTextBox();
      this.labelTotalPages = new CommandBarLabel();
      this.commandBarSeparator1 = new CommandBarSeparator();
      this.buttonZoomOut = new CommandBarButton();
      this.buttonZoomIn = new CommandBarButton();
      this.dropDownZoom = new CommandBarDropDownList();
      this.dropDownButtonLayout = new CommandBarDropDownButton();
      this.menuItemFile = new RadMenuItem();
      this.menuItemPrint = new RadMenuItem();
      this.menuItemExit = new RadMenuItem();
      this.menuItemView = new RadMenuItem();
      this.radMenuSeparatorItem1 = new RadMenuSeparatorItem();
      this.menuItemPreviousPage = new RadMenuItem();
      this.menuItemNextPage = new RadMenuItem();
      this.radMenuSeparatorItem2 = new RadMenuSeparatorItem();
      this.menuItemLayout = new RadMenuItem();
      this.menuItemTools = new RadMenuItem();
      this.menuItemPrintSettings = new RadMenuItem();
      this.menuItemWatermark = new RadMenuItem();
      this.radMenu = new RadMenu();
      this.radCommandBar.BeginInit();
      this.radMenu.BeginInit();
      this.BeginInit();
      this.SuspendLayout();
      this.printPreviewControl.AutoZoom = false;
      this.printPreviewControl.BackColor = Color.FromArgb(156, 179, 207);
      this.printPreviewControl.Dock = DockStyle.Fill;
      this.printPreviewControl.Location = new Point(0, 58);
      this.printPreviewControl.Name = "printPreviewControl";
      this.printPreviewControl.PageBorderColor = Color.White;
      this.printPreviewControl.PageInnerBorderColor = Color.White;
      this.printPreviewControl.PageShadowColor = Color.FromArgb(109, 125, 144);
      this.printPreviewControl.ScrollOffset = new Point(0, 0);
      this.printPreviewControl.ShadowThickness = 6;
      this.printPreviewControl.Size = new Size(713, 399);
      this.printPreviewControl.TabIndex = 1;
      this.printPreviewControl.StartPageChanged += new EventHandler(this.printPreviewControl1_StartPageChanged);
      this.printPreviewControl.MouseDown += new MouseEventHandler(this.printPreviewControl1_MouseDown);
      this.printPreviewControl.MouseMove += new MouseEventHandler(this.printPreviewControl1_MouseMove);
      this.printPreviewControl.MouseUp += new MouseEventHandler(this.printPreviewControl1_MouseUp);
      this.printPreviewControl.MouseWheel += new MouseEventHandler(this.printPreviewControl1_MouseWheel);
      this.radCommandBar.AutoSize = true;
      this.radCommandBar.Dock = DockStyle.Top;
      this.radCommandBar.Location = new Point(0, 20);
      this.radCommandBar.Name = "radCommandBar";
      this.radCommandBar.Rows.AddRange(this.commandBarRowElement1);
      this.radCommandBar.Size = new Size(713, 38);
      this.radCommandBar.TabIndex = 3;
      this.radCommandBar.Text = "radCommandBar1";
      this.commandBarRowElement1.DisplayName = (string) null;
      this.commandBarRowElement1.MinSize = new Size(25, 25);
      this.commandBarRowElement1.Strips.AddRange(this.stripElementTools, this.stripElementNavigation);
      this.stripElementTools.DisplayName = "Tools";
      this.stripElementTools.Items.AddRange((RadCommandBarBaseItem) this.buttonPrint, (RadCommandBarBaseItem) this.buttonSettings, (RadCommandBarBaseItem) this.commandBarButtonWatermark);
      this.stripElementTools.Name = "commandBarStripElement1";
      this.stripElementTools.Text = "";
      this.buttonPrint.AccessibleDescription = "Print";
      this.buttonPrint.AccessibleName = "Print";
      this.buttonPrint.DisplayName = "Print...";
      this.buttonPrint.DrawText = false;
      this.buttonPrint.Image = (Image) componentResourceManager.GetObject("print");
      this.buttonPrint.Name = "buttonPrint";
      this.buttonPrint.Text = "Print";
      this.buttonPrint.ToolTipText = "Print...";
      this.buttonPrint.Visibility = ElementVisibility.Visible;
      this.buttonPrint.MouseDown += new MouseEventHandler(this.buttonPrint_Click);
      this.buttonSettings.AccessibleDescription = "Settings";
      this.buttonSettings.AccessibleName = "Settings";
      this.buttonSettings.DisplayName = "Print Settings...";
      this.buttonSettings.DrawText = false;
      this.buttonSettings.Image = (Image) componentResourceManager.GetObject("page-setup");
      this.buttonSettings.Name = "buttonSettings";
      this.buttonSettings.Text = "Settings";
      this.buttonSettings.ToolTipText = "Print Settings...";
      this.buttonSettings.Visibility = ElementVisibility.Visible;
      this.buttonSettings.MouseDown += new MouseEventHandler(this.buttonSettings_Click);
      this.commandBarButtonWatermark.AccessibleDescription = "Watermark";
      this.commandBarButtonWatermark.AccessibleName = "Watermark";
      this.commandBarButtonWatermark.DisplayName = "Watermark";
      this.commandBarButtonWatermark.DrawText = false;
      this.commandBarButtonWatermark.Image = (Image) componentResourceManager.GetObject("watermark");
      this.commandBarButtonWatermark.Name = "commandBarButtonWatermark";
      this.commandBarButtonWatermark.Text = "Watermark";
      this.commandBarButtonWatermark.ToolTipText = "Watermark";
      this.commandBarButtonWatermark.Visibility = ElementVisibility.Visible;
      this.commandBarButtonWatermark.MouseUp += new MouseEventHandler(this.commandBarButtonWatermark_Click);
      this.stripElementNavigation.DisplayName = "Navigation";
      this.stripElementNavigation.Items.AddRange((RadCommandBarBaseItem) this.buttonPrevPage, (RadCommandBarBaseItem) this.buttonNextPage, (RadCommandBarBaseItem) this.textBoxCurrentPage, (RadCommandBarBaseItem) this.labelTotalPages, (RadCommandBarBaseItem) this.commandBarSeparator1, (RadCommandBarBaseItem) this.buttonZoomOut, (RadCommandBarBaseItem) this.buttonZoomIn, (RadCommandBarBaseItem) this.dropDownZoom, (RadCommandBarBaseItem) this.dropDownButtonLayout);
      this.stripElementNavigation.Name = "commandBarStripElement2";
      this.stripElementNavigation.Text = "";
      this.buttonPrevPage.AccessibleDescription = "<";
      this.buttonPrevPage.AccessibleName = "<";
      this.buttonPrevPage.DisplayName = "Previous Page";
      this.buttonPrevPage.DrawText = false;
      this.buttonPrevPage.Image = (Image) componentResourceManager.GetObject("arrow-up");
      this.buttonPrevPage.Name = "buttonPrevPage";
      this.buttonPrevPage.Text = "<";
      this.buttonPrevPage.ToolTipText = "Previous Page";
      this.buttonPrevPage.Visibility = ElementVisibility.Visible;
      this.buttonPrevPage.MouseDown += new MouseEventHandler(this.buttonPrevPage_Click);
      this.buttonNextPage.AccessibleDescription = ">";
      this.buttonNextPage.AccessibleName = ">";
      this.buttonNextPage.DisplayName = "Next Page";
      this.buttonNextPage.DrawText = false;
      this.buttonNextPage.Image = (Image) componentResourceManager.GetObject("arrow-down");
      this.buttonNextPage.Name = "buttonNextPage";
      this.buttonNextPage.Text = ">";
      this.buttonNextPage.ToolTipText = "Next Page";
      this.buttonNextPage.Visibility = ElementVisibility.Visible;
      this.buttonNextPage.MouseDown += new MouseEventHandler(this.buttonNextPage_Click);
      this.textBoxCurrentPage.DisplayName = "Current Page";
      this.textBoxCurrentPage.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.textBoxCurrentPage.MaxSize = new Size(0, 0);
      this.textBoxCurrentPage.Name = "textBoxCurrentPage";
      this.textBoxCurrentPage.StretchVertically = true;
      this.textBoxCurrentPage.Text = "";
      this.textBoxCurrentPage.Visibility = ElementVisibility.Visible;
      this.textBoxCurrentPage.KeyDown += new KeyEventHandler(this.commandBarTextBox1_KeyDown);
      ((RadItem) this.textBoxCurrentPage.GetChildAt(0)).Text = "";
      this.textBoxCurrentPage.GetChildAt(0).Alignment = ContentAlignment.MiddleCenter;
      this.textBoxCurrentPage.GetChildAt(0).MinSize = new Size(40, 20);
      this.labelTotalPages.AccessibleDescription = "of 0";
      this.labelTotalPages.AccessibleName = "of 0";
      this.labelTotalPages.DisplayName = "Total Pages";
      this.labelTotalPages.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
      this.labelTotalPages.Name = "labelTotalPages";
      this.labelTotalPages.Text = "/ 0";
      this.labelTotalPages.Visibility = ElementVisibility.Visible;
      this.commandBarSeparator1.AccessibleDescription = "commandBarSeparator1";
      this.commandBarSeparator1.AccessibleName = "commandBarSeparator1";
      this.commandBarSeparator1.DisplayName = "commandBarSeparator1";
      this.commandBarSeparator1.Name = "commandBarSeparator1";
      this.commandBarSeparator1.Visibility = ElementVisibility.Visible;
      this.commandBarSeparator1.VisibleInOverflowMenu = false;
      this.buttonZoomOut.AccessibleDescription = "-";
      this.buttonZoomOut.AccessibleName = "-";
      this.buttonZoomOut.DisplayName = "Zoom Out";
      this.buttonZoomOut.DrawText = false;
      this.buttonZoomOut.Image = (Image) componentResourceManager.GetObject("zoom-out");
      this.buttonZoomOut.Name = "buttonZoomOut";
      this.buttonZoomOut.Text = "-";
      this.buttonZoomOut.ToolTipText = "Zoom Out";
      this.buttonZoomOut.Visibility = ElementVisibility.Visible;
      this.buttonZoomIn.AccessibleDescription = "+";
      this.buttonZoomIn.AccessibleName = "+";
      this.buttonZoomIn.DisplayName = "Zoom In";
      this.buttonZoomIn.DrawText = false;
      this.buttonZoomIn.Image = (Image) componentResourceManager.GetObject("zoom-in");
      this.buttonZoomIn.Name = "buttonZoomIn";
      this.buttonZoomIn.Text = "+";
      this.buttonZoomIn.ToolTipText = "Zoom In";
      this.buttonZoomIn.Visibility = ElementVisibility.Visible;
      this.dropDownZoom.DisplayName = "Zoom";
      this.dropDownZoom.DropDownAnimationEnabled = true;
      this.dropDownZoom.MinSize = new Size(90, 22);
      this.dropDownZoom.Name = "dropDownZoom";
      this.dropDownZoom.Text = "";
      this.dropDownZoom.Visibility = ElementVisibility.Visible;
      this.dropDownButtonLayout.AccessibleDescription = "commandBarDropDownButton1";
      this.dropDownButtonLayout.AccessibleName = "commandBarDropDownButton1";
      this.dropDownButtonLayout.DisplayName = "Layout";
      this.dropDownButtonLayout.Image = (Image) componentResourceManager.GetObject("view-thumbnail");
      this.dropDownButtonLayout.Name = "dropDownButtonLayout";
      this.dropDownButtonLayout.Text = "commandBarDropDownButton1";
      this.dropDownButtonLayout.ToolTipText = "Layout";
      this.dropDownButtonLayout.Visibility = ElementVisibility.Visible;
      this.menuItemFile.AccessibleDescription = "File";
      this.menuItemFile.AccessibleName = "File";
      this.menuItemFile.Items.AddRange((RadItem) this.menuItemPrint, (RadItem) this.menuItemExit);
      this.menuItemFile.Name = "menuItemFile";
      this.menuItemFile.Text = "File";
      this.menuItemFile.Visibility = ElementVisibility.Visible;
      this.menuItemPrint.AccessibleDescription = "Print...";
      this.menuItemPrint.AccessibleName = "Print...";
      this.menuItemPrint.Name = "menuItemPrint";
      this.menuItemPrint.Text = "Print...";
      this.menuItemPrint.Visibility = ElementVisibility.Visible;
      this.menuItemPrint.Click += new EventHandler(this.buttonPrint_Click);
      this.menuItemExit.AccessibleDescription = "Exit";
      this.menuItemExit.AccessibleName = "Exit";
      this.menuItemExit.Name = "menuItemExit";
      this.menuItemExit.Text = "Exit";
      this.menuItemExit.Visibility = ElementVisibility.Visible;
      this.menuItemExit.Click += new EventHandler(this.radMenuItem6_Click);
      this.menuItemView.AccessibleDescription = "View";
      this.menuItemView.AccessibleName = "View";
      this.menuItemView.Items.AddRange((RadItem) this.radMenuSeparatorItem1, (RadItem) this.menuItemPreviousPage, (RadItem) this.menuItemNextPage, (RadItem) this.radMenuSeparatorItem2, (RadItem) this.menuItemLayout);
      this.menuItemView.Name = "menuItemView";
      this.menuItemView.Text = "View";
      this.menuItemView.Visibility = ElementVisibility.Visible;
      this.radMenuSeparatorItem1.Name = "radMenuSeparatorItem1";
      this.radMenuSeparatorItem1.Visibility = ElementVisibility.Visible;
      this.menuItemPreviousPage.AccessibleDescription = "Previous Page";
      this.menuItemPreviousPage.AccessibleName = "Previous Page";
      this.menuItemPreviousPage.Name = "menuItemPreviousPage";
      this.menuItemPreviousPage.Text = "Previous Page";
      this.menuItemPreviousPage.Visibility = ElementVisibility.Visible;
      this.menuItemPreviousPage.Click += new EventHandler(this.buttonPrevPage_Click);
      this.menuItemNextPage.AccessibleDescription = "Next Page";
      this.menuItemNextPage.AccessibleName = "Next Page";
      this.menuItemNextPage.Name = "menuItemNextPage";
      this.menuItemNextPage.Text = "Next Page";
      this.menuItemNextPage.Visibility = ElementVisibility.Visible;
      this.menuItemNextPage.Click += new EventHandler(this.buttonNextPage_Click);
      this.radMenuSeparatorItem2.Name = "radMenuSeparatorItem2";
      this.radMenuSeparatorItem2.Visibility = ElementVisibility.Visible;
      this.menuItemLayout.AccessibleDescription = "Layout";
      this.menuItemLayout.AccessibleName = "Layout";
      this.menuItemLayout.Name = "menuItemLayout";
      this.menuItemLayout.Text = "Layout";
      this.menuItemLayout.Visibility = ElementVisibility.Visible;
      this.menuItemTools.AccessibleDescription = "Tools";
      this.menuItemTools.AccessibleName = "Tools";
      this.menuItemTools.Items.AddRange((RadItem) this.menuItemPrintSettings, (RadItem) this.menuItemWatermark);
      this.menuItemTools.Name = "menuItemTools";
      this.menuItemTools.Text = "Tools";
      this.menuItemTools.Visibility = ElementVisibility.Visible;
      this.menuItemPrintSettings.AccessibleDescription = "Printer Settings";
      this.menuItemPrintSettings.AccessibleName = "Printer Settings";
      this.menuItemPrintSettings.Name = "menuItemPrintSettings";
      this.menuItemPrintSettings.Text = "Printer Settings...";
      this.menuItemPrintSettings.Visibility = ElementVisibility.Visible;
      this.menuItemPrintSettings.Click += new EventHandler(this.buttonSettings_Click);
      this.menuItemWatermark.AccessibleDescription = "Watermark Settings...";
      this.menuItemWatermark.AccessibleName = "Watermark Settings...";
      this.menuItemWatermark.Name = "menuItemWatermark";
      this.menuItemWatermark.Text = "Watermark Settings...";
      this.menuItemWatermark.Visibility = ElementVisibility.Visible;
      this.menuItemWatermark.Click += new EventHandler(this.commandBarButtonWatermark_Click);
      this.radMenu.Items.AddRange((RadItem) this.menuItemFile, (RadItem) this.menuItemView, (RadItem) this.menuItemTools);
      this.radMenu.Location = new Point(0, 0);
      this.radMenu.Name = "radMenu";
      this.radMenu.Size = new Size(713, 20);
      this.radMenu.TabIndex = 4;
      this.radMenu.Text = "radMenu1";
      this.radMenu.Visible = false;
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new Size(713, 457);
      this.Controls.Add((Control) this.printPreviewControl);
      this.Controls.Add((Control) this.radCommandBar);
      this.Controls.Add((Control) this.radMenu);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimizeBox = false;
      this.Name = nameof (RadPrintPreviewDialog);
      this.RootElement.ApplyShapeToControl = true;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Print Preview";
      this.radCommandBar.EndInit();
      this.radMenu.EndInit();
      this.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
