// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadContextMenu
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  [ToolboxItem(true)]
  [DefaultProperty("Items")]
  [TelerikToolboxCategory("Menus & Toolbars")]
  [ToolboxBitmap(typeof (RadContextMenu), "RadDropDownMenu.bmp")]
  [Designer("Telerik.WinControls.UI.Design.RadContextMenuDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadContextMenu : Component, IAnalyticsProvider
  {
    private RadContextMenuDropDown menu;
    private Control control;
    private RadItem item;

    public RadContextMenu()
    {
      this.menu = new RadContextMenuDropDown();
      this.menu.DropDownOpening += new CancelEventHandler(this.menu_DropDownOpening);
      this.menu.DropDownOpened += new EventHandler(this.menu_DropDownOpened);
      this.menu.DropDownClosing += new RadPopupClosingEventHandler(this.menu_DropDownClosing);
      this.menu.DropDownClosed += new RadPopupClosedEventHandler(this.menu_DropDownClosed);
      this.menu.Items.ItemsChanged += new ItemChangedDelegate(this.Items_ItemsChanged);
    }

    public RadContextMenu(RadElement ownerElement)
    {
      this.menu = new RadContextMenuDropDown(ownerElement);
      this.menu.DropDownOpening += new CancelEventHandler(this.menu_DropDownOpening);
      this.menu.DropDownOpened += new EventHandler(this.menu_DropDownOpened);
      this.menu.DropDownClosing += new RadPopupClosingEventHandler(this.menu_DropDownClosing);
      this.menu.DropDownClosed += new RadPopupClosedEventHandler(this.menu_DropDownClosed);
      this.menu.Items.ItemsChanged += new ItemChangedDelegate(this.Items_ItemsChanged);
    }

    public RadContextMenu(IContainer owner)
      : this()
    {
      owner?.Add((IComponent) this);
    }

    public event CancelEventHandler DropDownOpening;

    public event CancelEventHandler DropDownClosing;

    public event EventHandler DropDownOpened;

    public event EventHandler DropDownClosed;

    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    [Description("Gets menu items collection")]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.DropDown.Items;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets theme name.")]
    [DefaultValue("")]
    [Editor("Telerik.WinControls.UI.Design.ThemeNameEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("StyleSheet")]
    public string ThemeName
    {
      get
      {
        return this.DropDown.ThemeName;
      }
      set
      {
        this.DropDown.ThemeName = value;
      }
    }

    [Description("Gets or sets the ImageList that contains the images displayed by this control.")]
    [DefaultValue(null)]
    [Browsable(true)]
    [Category("Appearance")]
    public ImageList ImageList
    {
      get
      {
        return this.DropDown.ImageList;
      }
      set
      {
        this.DropDown.ImageList = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Data")]
    [Description("Gets menu drop down panel")]
    public RadDropDownMenu DropDown
    {
      get
      {
        return (RadDropDownMenu) this.menu;
      }
    }

    public void Show()
    {
      this.DropDown.Show();
    }

    public void Show(int x, int y)
    {
      this.DropDown.Show(x, y);
    }

    public void Show(Point point)
    {
      this.DropDown.Show(point);
    }

    public void Show(Point point, RadDirection popupDirection)
    {
      this.DropDown.Show(point, popupDirection);
    }

    public void Show(Control control, int x, int y)
    {
      this.GetClickedControl(control, x, y);
      this.DropDown.Show(control, x, y);
    }

    public void Show(Control control, Point point)
    {
      this.GetClickedControl(control, point.X, point.Y);
      this.DropDown.Show(control, point);
    }

    public void Show(Control control, Point point, RadDirection popupDirection)
    {
      this.GetClickedControl(control, point.X, point.Y);
      this.DropDown.Show(control, point, popupDirection);
    }

    public void Show(RadItem item, int x, int y)
    {
      this.control = item == null || item.ElementTree == null ? (Control) null : item.ElementTree.Control;
      this.item = item;
      this.DropDown.Show(item, x, y);
    }

    public void Show(RadItem item, Point point)
    {
      this.control = item == null || item.ElementTree == null ? (Control) null : item.ElementTree.Control;
      this.item = item;
      this.DropDown.Show(item, point);
    }

    public void Show(RadItem item, Point point, RadDirection popupDirection)
    {
      this.control = item == null || item.ElementTree == null ? (Control) null : item.ElementTree.Control;
      this.item = item;
      this.DropDown.Show(item, point, popupDirection);
    }

    public void Show(RadItem item, int ownerOffset, RadDirection popupDirection)
    {
      this.control = item == null || item.ElementTree == null ? (Control) null : item.ElementTree.Control;
      this.item = item;
      this.DropDown.Show(item, ownerOffset, popupDirection);
    }

    protected virtual void OnDropDownOpening(CancelEventArgs args)
    {
      if (this.DropDownOpening == null)
        return;
      this.DropDownOpening(((object) this.item ?? (object) this.control) ?? (object) this, args);
    }

    protected virtual void OnDropDownClosing(CancelEventArgs args)
    {
      if (this.DropDownClosing == null)
        return;
      this.DropDownClosing((object) this, args);
    }

    protected virtual void OnDropDownOpened()
    {
      if (this.DropDownOpened == null)
        return;
      this.DropDownOpened(((object) this.item ?? (object) this.control) ?? (object) this, EventArgs.Empty);
    }

    protected virtual void OnDropDownClosed()
    {
      if (this.DropDownClosed == null)
        return;
      this.DropDownClosed((object) this, EventArgs.Empty);
    }

    private void menu_DropDownClosed(object sender, EventArgs e)
    {
      this.OnDropDownClosed();
      if (!this.EnableAnalytics)
        return;
      ControlTraceMonitor.TrackAtomicFeature(nameof (RadContextMenu), "Closed", (object) "");
    }

    private void menu_DropDownClosing(object sender, CancelEventArgs e)
    {
      this.OnDropDownClosing(e);
    }

    private void menu_DropDownOpened(object sender, EventArgs e)
    {
      this.OnDropDownOpened();
      if (!this.EnableAnalytics)
        return;
      ControlTraceMonitor.TrackAtomicFeature(nameof (RadContextMenu), "Opened", (object) "");
    }

    private void menu_DropDownOpening(object sender, CancelEventArgs e)
    {
      this.OnDropDownOpening(e);
    }

    private void Items_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (operation != ItemsChangeOperation.Inserted)
        return;
      RadMenuItemBase radMenuItemBase = target as RadMenuItemBase;
      if (radMenuItemBase == null)
        return;
      radMenuItemBase.OwnerControl = (Control) this.menu;
    }

    private void GetClickedControl(Control control, int x, int y)
    {
      this.control = control;
      RadControl radControl = control as RadControl;
      if (radControl != null && radControl.ElementTree != null)
        this.item = radControl.ElementTree.GetElementAtPoint<RadItem>(new Point(x, y));
      else
        this.item = (RadItem) null;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.item = (RadItem) null;
        this.control = (Control) null;
        this.DropDown.DropDownOpening -= new CancelEventHandler(this.menu_DropDownOpening);
        this.DropDown.DropDownOpened -= new EventHandler(this.menu_DropDownOpened);
        this.DropDown.DropDownClosing -= new RadPopupClosingEventHandler(this.menu_DropDownClosing);
        this.DropDown.DropDownClosed -= new RadPopupClosedEventHandler(this.menu_DropDownClosed);
        this.menu.Items.ItemsChanged -= new ItemChangedDelegate(this.Items_ItemsChanged);
        this.DropDown.Dispose();
      }
      base.Dispose(disposing);
    }

    [DefaultValue("")]
    [Browsable(false)]
    [Description("Gets or sets the Analytics Name associated with this control.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string AnalyticsName
    {
      get
      {
        return this.menu.AnalyticsName;
      }
      set
      {
        this.menu.AnalyticsName = value;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the Analytics functionality is enabled or disabled for this control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool EnableAnalytics
    {
      get
      {
        return this.menu.EnableAnalytics;
      }
      set
      {
        this.menu.EnableAnalytics = value;
      }
    }
  }
}
