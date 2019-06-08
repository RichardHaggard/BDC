// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewColumnChooser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class GridViewColumnChooser : RadForm
  {
    private IContainer components;
    private ImageList imageList2;
    private ColumnChooserControl columnChooserControl;

    public GridViewColumnChooser()
      : this((GridViewTemplate) null, (RadGridViewElement) null)
    {
    }

    public GridViewColumnChooser(GridViewTemplate template)
      : this(template, (RadGridViewElement) null)
    {
      this.Template = template;
    }

    public GridViewColumnChooser(GridViewTemplate template, RadGridViewElement rootElement)
    {
      this.InitializeComponent();
      this.Template = template;
      this.GridRootElement = rootElement;
      this.Text = LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("ColumnChooserFormCaption");
      this.ColumnChooserControl.ItemElementCreating += new ColumnChooserItemElementCreatingEventHandler(this.ColumnChooserControl_ItemElementCreating);
    }

    private void ColumnChooserControl_ItemElementCreating(
      object sender,
      ColumnChooserItemElementCreatingEventArgs e)
    {
      this.GridRootElement.Template.MasterTemplate.EventDispatcher.RaiseEvent<ColumnChooserItemElementCreatingEventArgs>(EventDispatcher.ColumnChooserItemElementCreating, sender, e);
    }

    public ColumnChooserControl ColumnChooserControl
    {
      get
      {
        return this.columnChooserControl;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public RadSortOrder SortOrder
    {
      get
      {
        return this.columnChooserControl.SortOrder;
      }
      set
      {
        this.columnChooserControl.SortOrder = value;
      }
    }

    public bool EnableFilter
    {
      get
      {
        return this.ColumnChooserControl.EnableFiltering;
      }
      set
      {
        this.ColumnChooserControl.EnableFiltering = value;
      }
    }

    public GridViewTemplate Template
    {
      get
      {
        return this.columnChooserControl.ViewTemplate;
      }
      set
      {
        this.columnChooserControl.ViewTemplate = value;
      }
    }

    public IList<GridViewColumn> Columns
    {
      get
      {
        return this.columnChooserControl.Columns;
      }
    }

    public RadGridViewElement GridRootElement
    {
      get
      {
        return this.columnChooserControl.GridViewElement;
      }
      private set
      {
        if (value == this.columnChooserControl.GridViewElement)
          return;
        if (this.GridRootElement != null)
          this.GridRootElement.ElementTree.ComponentTreeHandler.ThemeNameChanged -= new ThemeNameChangedEventHandler(this.OnGridView_ThemeNameChanged);
        this.columnChooserControl.GridViewElement = value;
        if (value == null)
          return;
        value.ElementTree.ComponentTreeHandler.ThemeNameChanged += new ThemeNameChangedEventHandler(this.OnGridView_ThemeNameChanged);
      }
    }

    private void OnGridView_ThemeNameChanged(object source, ThemeNameChangedEventArgs args)
    {
      ThemeResolutionService.ApplyThemeToControlTree((Control) this, args.newThemeName);
      this.ThemeName = args.newThemeName;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      if (disposing && this.GridRootElement != null)
        this.GridRootElement.ElementTree.ComponentTreeHandler.ThemeNameChanged -= new ThemeNameChangedEventHandler(this.OnGridView_ThemeNameChanged);
      base.Dispose(disposing);
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      string themeName = this.GridRootElement.ElementTree.ComponentTreeHandler.ThemeName;
      if (!(this.ThemeName != themeName))
        return;
      ThemeResolutionService.ApplyThemeToControlTree((Control) this, themeName);
      this.ThemeName = themeName;
    }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      if (!this.EnableFilter)
        return;
      this.ColumnChooserControl.FilterTextBox.TextBoxItem.HostedControl.Focus();
    }

    protected override void HandleDpiChanged()
    {
      if (TelerikHelper.IsWindows8OrLower || TelerikHelper.IsWindows10CreatorsUpdateOrHigher)
        this.Scale(TelerikDpiHelper.ScaleSizeF(Telerik.WinControls.NativeMethods.GetMonitorDpi(Screen.FromControl((Control) this), Telerik.WinControls.NativeMethods.DpiType.Effective), new SizeF(1f / this.RootElement.DpiScaleFactor.Width, 1f / this.RootElement.DpiScaleFactor.Height)));
      else
        base.HandleDpiChanged();
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GridViewColumnChooser));
      this.imageList2 = new ImageList(this.components);
      this.columnChooserControl = new ColumnChooserControl();
      this.columnChooserControl.BeginInit();
      this.SuspendLayout();
      this.imageList2.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList2.ImageStream");
      this.imageList2.TransparentColor = Color.Magenta;
      this.imageList2.Images.SetKeyName(0, "minimize.png");
      this.imageList2.Images.SetKeyName(1, "maximize.png");
      this.imageList2.Images.SetKeyName(2, "close.png");
      this.columnChooserControl.Dock = DockStyle.Fill;
      this.columnChooserControl.Location = new Point(0, 0);
      this.columnChooserControl.Name = "columnChooserControl";
      this.columnChooserControl.Size = new Size(248, 344);
      this.columnChooserControl.TabIndex = 0;
      this.columnChooserControl.Text = "columnChooserControl";
      this.AllowDrop = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new Size(248, 336);
      this.Controls.Add((Control) this.columnChooserControl);
      this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (GridViewColumnChooser);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Column Chooser";
      this.TopMost = true;
      this.columnChooserControl.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
