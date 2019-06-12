// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonBar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Menus & Toolbars")]
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Designer("Telerik.WinControls.UI.Design.RadRibbonBarDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("Provides an implementation of the Microsoft Office 2007 ribbon-user interface")]
  public class RadRibbonBar : RadControl
  {
    private Font keyTipFont = new Font("Arial", 10f, FontStyle.Regular);
    public static readonly ExpandCollapseCommand ExpandCollapseCommand = new ExpandCollapseCommand();
    private RadRibbonBarElement ribbonBarElement;
    private bool enableKeyboardNavigation;
    private bool compositionEnabled;
    private bool enableTabScrollingOnMouseWheel;
    private RadRibbonBar.RibbonBarInputBehavior ribbonBehavior;
    private Form parentForm;

    [RadDescription("CommandTabSelecting", typeof (RadRibbonBarElement))]
    [Category("Behavior")]
    public event CommandTabSelectingEventHandler CommandTabSelecting;

    [Category("Behavior")]
    [RadDescription("CommandTabSelected", typeof (RadRibbonBarElement))]
    public event CommandTabEventHandler CommandTabSelected;

    [Category("Behavior")]
    [RadDescription("ExpandedStateChanged", typeof (RadRibbonBarElement))]
    public event EventHandler ExpandedStateChanged;

    [Category("Behavior")]
    [Description("Occurs when the RadRibbonBar is painting Key tips")]
    public event CancelEventHandler KeyTipShowing;

    [Category("Behavior")]
    [Description("Occurs when the user is press Key tip")]
    public event CancelEventHandler KeyTipActivating;

    static RadRibbonBar()
    {
      RadRibbonBar.ExpandCollapseCommand.Name = nameof (ExpandCollapseCommand);
      RadRibbonBar.ExpandCollapseCommand.Text = "This command expands/collapses the currently selected RibbonBar command tab.";
      RadRibbonBar.ExpandCollapseCommand.OwnerType = typeof (RadRibbonBar);
    }

    public RadRibbonBar()
    {
      this.AutoSize = true;
      this.CausesValidation = false;
      this.EnableKeyboardNavigation = true;
      this.ThemeNameChanged += new ThemeNameChangedEventHandler(this.RadRibbonBar_ThemeNameChanged);
      this.Dock = DockStyle.Top;
    }

    protected override void InitializeRootElement(RootRadElement rootElement)
    {
      base.InitializeRootElement(rootElement);
      rootElement.StretchVertically = false;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.ThemeNameChanged -= new ThemeNameChangedEventHandler(this.RadRibbonBar_ThemeNameChanged);
      if (this.parentForm == null)
        return;
      this.parentForm.TextChanged -= new EventHandler(this.ParentForm_TextChanged);
      this.parentForm = (Form) null;
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.ribbonBarElement = new RadRibbonBarElement();
      this.RootElement.Children.Add((RadElement) this.ribbonBarElement);
      this.ribbonBarElement.CommandTabSelecting += (CommandTabSelectingEventHandler) ((sender, args) => this.OnCommandTabSelecting(args));
      this.ribbonBarElement.CommandTabSelected += (CommandTabEventHandler) ((sender, args) => this.OnCommandTabSelected(args));
      this.ribbonBarElement.ExpandedStateChanged += (EventHandler) ((sender, args) => this.OnRibbonBarExpandedStateChanged(args));
      base.CreateChildItems((RadElement) this.ribbonBarElement);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadRibbonBarAccessibleObject(this);
    }

    protected override ComponentInputBehavior CreateBehavior()
    {
      this.ribbonBehavior = new RadRibbonBar.RibbonBarInputBehavior(this);
      return (ComponentInputBehavior) this.ribbonBehavior;
    }

    internal bool CompositionEnabled
    {
      get
      {
        return this.compositionEnabled;
      }
      set
      {
        this.compositionEnabled = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(400, 108));
      }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    [Category("Layout")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    [DefaultValue(DockStyle.Top)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        base.Dock = value;
      }
    }

    [Browsable(true)]
    public override ImageList SmallImageList
    {
      get
      {
        return base.SmallImageList;
      }
      set
      {
        base.SmallImageList = value;
      }
    }

    [Localizable(true)]
    public override string Text
    {
      get
      {
        return this.ribbonBarElement.Text;
      }
      set
      {
        RadForm parent = this.Parent as RadForm;
        if (parent != null && parent.FormBehavior is RadRibbonFormBehavior)
          parent.Text = value;
        this.ribbonBarElement.Text = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    public new bool CausesValidation
    {
      get
      {
        return base.CausesValidation;
      }
      set
      {
        base.CausesValidation = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(ApplicationMenuStyle.ApplicationMenu)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ApplicationMenuStyle ApplicationMenuStyle
    {
      get
      {
        return this.ribbonBarElement.ApplicationMenuStyle;
      }
      set
      {
        this.ribbonBarElement.ApplicationMenuStyle = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    public RadRibbonBarBackstageView BackstageControl
    {
      get
      {
        return this.ribbonBarElement.BackstageControl;
      }
      set
      {
        this.ribbonBarElement.BackstageControl = value;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.CommandTabsCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RadDescription("CommandTabs", typeof (RadRibbonBarElement))]
    [RadEditItemsAction]
    [RadNewItem("Add New Tab...", true, true, false)]
    public virtual RadRibbonBarCommandTabCollection CommandTabs
    {
      get
      {
        return this.ribbonBarElement.CommandTabs;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RadEditItemsAction]
    [Browsable(true)]
    [Category("Data")]
    [RadDescription("ContextualTabGroups", typeof (RadRibbonBarElement))]
    [RadNewItem("Add Context...", true, true, false)]
    public RadItemOwnerCollection ContextualTabGroups
    {
      get
      {
        return this.ribbonBarElement.ContextualTabGroups;
      }
    }

    [Browsable(true)]
    [DefaultValue(30)]
    [Category("Behavior")]
    [RadDescription("QuickAccessMenuHeight", typeof (RadRibbonBarElement))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int QuickAccessToolBarHeight
    {
      get
      {
        return this.ribbonBarElement.QuickAccessMenuHeight;
      }
      set
      {
        this.ribbonBarElement.QuickAccessMenuHeight = value;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    public bool EnableKeyboardNavigation
    {
      get
      {
        return this.enableKeyboardNavigation;
      }
      set
      {
        this.enableKeyboardNavigation = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    [Description("Represent the Ribbon Help button!")]
    public RadImageButtonElement HelpButton
    {
      get
      {
        return this.RibbonBarElement.HelpButton;
      }
      set
      {
        this.RibbonBarElement.HelpButton = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Represent the Ribbon Expand button!")]
    [Category("Behavior")]
    public RadToggleButtonElement ExpandButton
    {
      get
      {
        return this.RibbonBarElement.ExpandButton;
      }
      set
      {
        this.RibbonBarElement.ExpandButton = value;
      }
    }

    [DefaultValue(false)]
    [Description("Get or sets value indicating whether RibbonBar Help button is visible or hidden.")]
    [Category("Appearance")]
    public bool ShowHelpButton
    {
      get
      {
        return this.RibbonBarElement.ShowHelpButton;
      }
      set
      {
        this.RibbonBarElement.ShowHelpButton = value;
      }
    }

    [Description("Get or sets value indicating whether RibbonBar expand button is visible or hidden.")]
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool ShowExpandButton
    {
      get
      {
        return this.RibbonBarElement.ShowExpandButton;
      }
      set
      {
        this.RibbonBarElement.ShowExpandButton = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(true)]
    public new bool EnableKeyMap
    {
      get
      {
        return base.EnableKeyMap;
      }
      set
      {
        base.EnableKeyMap = value;
      }
    }

    [DefaultValue(FadeAnimationType.FadeOut)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating the type of the fade animation.")]
    public FadeAnimationType ApplicationMenuAnimantionType
    {
      get
      {
        return this.ribbonBarElement.ApplicationButtonElement.DropDownMenu.FadeAnimationType;
      }
      set
      {
        this.ribbonBarElement.ApplicationButtonElement.DropDownMenu.FadeAnimationType = value;
      }
    }

    [RadDescription("QuickAccessToolbarBelowRibbon", typeof (RadRibbonBarElement))]
    [Category("Behavior")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool QuickAccessToolbarBelowRibbon
    {
      get
      {
        return this.ribbonBarElement.QuickAccessToolbarBelowRibbon;
      }
      set
      {
        this.ribbonBarElement.QuickAccessToolbarBelowRibbon = value;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Enable or Disable Tab Changing on Mouse Wheel")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool EnableTabScrollingOnMouseWheel
    {
      get
      {
        return this.enableTabScrollingOnMouseWheel;
      }
      set
      {
        this.enableTabScrollingOnMouseWheel = value;
      }
    }

    [Browsable(true)]
    [RadNewItem("Type here", true, false)]
    [Category("Data")]
    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [RadDescription("QuickAccessMenuItems", typeof (RadRibbonBarElement))]
    public virtual RadItemOwnerCollection QuickAccessToolBarItems
    {
      get
      {
        return this.ribbonBarElement.QuickAccessMenuItems;
      }
    }

    [Description("Gets the QuickAccessToolBar element")]
    [Browsable(true)]
    [Category("Action")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadQuickAccessToolBar QuickAccessToolBar
    {
      get
      {
        return this.ribbonBarElement.QuickAccessToolBar;
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(null)]
    [Localizable(true)]
    [TypeConverter(typeof (ImageTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RadDescription("StartButtonImage", typeof (RadRibbonBarElement))]
    public Image StartButtonImage
    {
      get
      {
        return this.ribbonBarElement.StartButtonImage;
      }
      set
      {
        this.ribbonBarElement.StartButtonImage = value;
      }
    }

    [Browsable(true)]
    [RadDescription("StartMenuItems", typeof (RadRibbonBarElement))]
    [Category("Data")]
    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual RadItemOwnerCollection StartMenuItems
    {
      get
      {
        return this.ribbonBarElement.StartMenuItems;
      }
    }

    [RadDescription("StartMenuRightColumnItems", typeof (RadRibbonBarElement))]
    [Browsable(true)]
    [Category("Data")]
    [RadEditItemsAction]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public virtual RadItemOwnerCollection StartMenuRightColumnItems
    {
      get
      {
        return this.ribbonBarElement.StartMenuRightColumnItems;
      }
    }

    [RadDescription("StartMenuBottomStrip", typeof (RadRibbonBarElement))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Data")]
    public RadItemOwnerCollection StartMenuBottomStrip
    {
      get
      {
        return this.ribbonBarElement.StartMenuBottomStrip;
      }
    }

    [RadDefaultValue("StartMenuWidth", typeof (RadRibbonBarElement))]
    [Browsable(true)]
    [Category("Layout")]
    [RadDescription("StartMenuWidth", typeof (RadRibbonBarElement))]
    public int StartMenuWidth
    {
      get
      {
        return this.ribbonBarElement.StartMenuWidth;
      }
      set
      {
        this.ribbonBarElement.StartMenuWidth = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Action")]
    [Description("Gets the options menu button")]
    public RadMenuButtonItem OptionsButton
    {
      get
      {
        return this.ribbonBarElement.OptionsButton;
      }
    }

    [Browsable(true)]
    [Description("Gets the exit menu button")]
    [Category("Action")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadMenuButtonItem ExitButton
    {
      get
      {
        return this.ribbonBarElement.ExitButton;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RibbonTab SelectedCommandTab
    {
      get
      {
        return (RibbonTab) this.ribbonBarElement.TabStripElement.SelectedItem;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadRibbonBarElement RibbonBarElement
    {
      get
      {
        return this.ribbonBarElement;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool Expanded
    {
      get
      {
        return this.ribbonBarElement.Expanded;
      }
      set
      {
        this.LoadElementTree();
        this.ribbonBarElement.Expanded = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets a value indicating whether the ribbon bar will be collapsed or expanded on ribbon tab double click.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(true)]
    public bool CollapseRibbonOnTabDoubleClick
    {
      get
      {
        return this.RibbonBarElement.CollapseRibbonOnTabDoubleClick;
      }
      set
      {
        this.RibbonBarElement.CollapseRibbonOnTabDoubleClick = value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    public bool MinimizeButton
    {
      get
      {
        return this.ribbonBarElement.MinimizeButton;
      }
      set
      {
        this.ribbonBarElement.MinimizeButton = value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    public bool MaximizeButton
    {
      get
      {
        return this.ribbonBarElement.MaximizeButton;
      }
      set
      {
        this.ribbonBarElement.MaximizeButton = value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    public bool CloseButton
    {
      get
      {
        return this.ribbonBarElement.CloseButton;
      }
      set
      {
        this.ribbonBarElement.CloseButton = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RibbonBarLocalizationSettings LocalizationSettings
    {
      get
      {
        return this.ribbonBarElement.LocalizationSettings;
      }
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
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

    protected internal bool OnKeyTipItemActivating(RadItem item, CancelEventArgs eventArgs)
    {
      if (this.KeyTipActivating != null)
        this.KeyTipActivating((object) item, eventArgs);
      return eventArgs.Cancel;
    }

    protected virtual void OnCommandTabSelecting(CommandTabSelectingEventArgs args)
    {
      if (this.CommandTabSelecting == null)
        return;
      this.CommandTabSelecting((object) this, args);
    }

    protected virtual void OnCommandTabSelected(CommandTabEventArgs args)
    {
      if (this.CommandTabSelected != null)
        this.CommandTabSelected((object) this, args);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this.ElementTree.Control, "SelectionChanged", (object) args.CommandTab.Text);
    }

    protected virtual void OnRibbonBarExpandedStateChanged(EventArgs args)
    {
      if (this.ExpandedStateChanged != null)
        this.ExpandedStateChanged((object) this, args);
      this.ribbonBarElement.InvalidateMeasure(true);
      this.ribbonBarElement.InvalidateArrange(true);
      this.ribbonBarElement.UpdateLayout();
      ControlTraceMonitor.TrackAtomicFeature(this.ElementTree.Control as RadControl, "Expanded", this.Expanded ? (object) "Expanded" : (object) "Collapsed");
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (!this.enableTabScrollingOnMouseWheel || !this.Focused || this.ribbonBarElement.TabStripElement.Items.Count == 0 || !this.Expanded && !this.ribbonBarElement.Popup.IsPopupShown)
        return;
      int count = this.ribbonBarElement.TabStripElement.Items.Count;
      int num = this.ribbonBarElement.TabStripElement.Items.IndexOf(this.ribbonBarElement.TabStripElement.SelectedItem);
      RibbonTab ribbonTab = this.ribbonBarElement.TabStripElement.Items[(count + (Math.Sign(e.Delta) + num)) % count] as RibbonTab;
      if (ribbonTab.Visibility != ElementVisibility.Visible)
        return;
      this.ribbonBarElement.TabStripElement.SelectedItem = (RadPageViewItem) ribbonTab;
    }

    protected override void OnParentChanged(EventArgs e)
    {
      if (this.parentForm != null)
        this.parentForm.TextChanged -= new EventHandler(this.ParentForm_TextChanged);
      this.parentForm = this.Parent as Form;
      base.OnParentChanged(e);
      if (this.parentForm == null)
        return;
      this.parentForm.TextChanged += new EventHandler(this.ParentForm_TextChanged);
      this.ribbonBarElement.Text = this.Parent.Text;
      this.ribbonBarElement.SetParentForm();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      this.Behavior.ProccessKeyMap(e.KeyData);
    }

    protected virtual bool OnKeyTipShowing(RadItem currentKeyMapItem, CancelEventArgs eventArgs)
    {
      if (this.KeyTipShowing != null)
        this.KeyTipShowing((object) currentKeyMapItem, eventArgs);
      return eventArgs.Cancel;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (!this.enableKeyboardNavigation)
        return base.ProcessCmdKey(ref msg, keyData);
      if (!((RadRibbonBar.RibbonBarInputBehavior) this.Behavior).ProcessArrowKeys(keyData))
        return base.ProcessCmdKey(ref msg, keyData);
      return true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.Behavior.IsKeyMapActive)
        return;
      this.PaintKeyMap(e.Graphics);
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 132)
      {
        if (DWMAPI.IsCompositionEnabled && this.WmNCHitTest(ref m))
          return;
        if (this.FindForm() != null && Cursor.Current != (Cursor) null && Cursor.Current.Equals((object) Cursors.Help))
        {
          RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(this.PointToClient(Control.MousePosition));
          if (elementAtPoint != null && elementAtPoint == this.RibbonBarElement.RibbonCaption.HelpButton)
            return;
        }
      }
      base.WndProc(ref m);
    }

    protected virtual bool WmNCHitTest(ref Message msg)
    {
      RadFormControlBase form = this.FindForm() as RadFormControlBase;
      if (form != null && form.FormBehavior is RadRibbonFormBehavior)
      {
        Point client = this.PointToClient(new Point((int) msg.LParam));
        if (this.RibbonBarElement.IconPrimitive.ControlBoundingRectangle.Contains(client) && this.CompositionEnabled)
        {
          msg.Result = new IntPtr(-1);
          return true;
        }
        RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(client);
        if (elementAtPoint != null && !(elementAtPoint is RadRibbonBarCaption) && elementAtPoint.Visibility != ElementVisibility.Hidden || !this.CompositionEnabled)
          return false;
        int num = SystemInformation.FrameBorderSize.Height + SystemInformation.CaptionHeight;
        if (form.WindowState == FormWindowState.Maximized)
          num += SystemInformation.FrameBorderSize.Height;
        if (client.Y < num)
        {
          msg.Result = new IntPtr(-1);
          return true;
        }
      }
      return false;
    }

    private void ParentForm_TextChanged(object sender, EventArgs e)
    {
      if (this.Parent != null)
      {
        this.ribbonBarElement.Text = this.Parent.Text;
      }
      else
      {
        Form form = this.FindForm();
        if (form == null)
          return;
        this.ribbonBarElement.Text = form.Text;
      }
    }

    private void RadRibbonBar_ThemeNameChanged(object source, ThemeNameChangedEventArgs args)
    {
      if (this.ribbonBarElement.Popup == null)
        return;
      this.ribbonBarElement.Popup.ThemeName = args.newThemeName;
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      Application.DoEvents();
      this.RibbonBarElement.InvalidateMeasure(true);
      this.RibbonBarElement.UpdateLayout();
    }

    protected internal virtual void PaintKeyMap(Graphics graphics)
    {
      List<RadItem> currentKeyMap = this.Behavior.GetCurrentKeyMap(this.Behavior.ActiveKeyMapItem);
      int num = 1;
      for (int index1 = 0; index1 < currentKeyMap.Count; ++index1)
      {
        RadItem currentKeyMapItem = currentKeyMap[index1];
        Rectangle boundingRectangle = currentKeyMapItem.ControlBoundingRectangle;
        if (currentKeyMapItem.ElementTree != null)
        {
          Point customLocation = Point.Empty;
          int y = boundingRectangle.Y + (int) ((double) boundingRectangle.Height * 0.66);
          customLocation = new Point(boundingRectangle.X + (int) ((double) boundingRectangle.Width / 2.0), y);
          RadKeyTipShowingEventArgs args = new RadKeyTipShowingEventArgs(false, customLocation, this.keyTipFont, Color.White, Color.Black, Color.Gray);
          if (!this.OnKeyTipShowing(currentKeyMapItem, (CancelEventArgs) args) && currentKeyMapItem.Visibility == ElementVisibility.Visible)
          {
            Control control = currentKeyMapItem.ElementTree.Control;
            Graphics graphics1 = (Graphics) null;
            if (control != this)
              graphics1 = control.CreateGraphics();
            string empty = string.Empty;
            string keyTip;
            if (!string.IsNullOrEmpty(currentKeyMapItem.KeyTip))
            {
              keyTip = currentKeyMapItem.KeyTip;
            }
            else
            {
              bool flag;
              do
              {
                RadItem radItem1 = currentKeyMap[index1];
                keyTip = num >= 10 ? ((char) (65 + num - 10)).ToString() : num.ToString();
                flag = false;
                for (int index2 = 0; index2 < currentKeyMap.Count; ++index2)
                {
                  RadItem radItem2 = currentKeyMap[index2];
                  if (keyTip == radItem2.KeyTip)
                  {
                    ++num;
                    flag = true;
                    break;
                  }
                }
              }
              while (flag);
              currentKeyMapItem.KeyTip = keyTip;
              ++num;
            }
            if (graphics1 != null)
            {
              this.PaintKeyTip(graphics1, args, keyTip);
              graphics1.Dispose();
            }
            else
              this.PaintKeyTip(graphics, args, keyTip);
          }
        }
      }
    }

    protected internal virtual void PaintKeyTip(
      Graphics graphics,
      RadKeyTipShowingEventArgs args,
      string keyTip)
    {
      int num = 0;
      Size size = TextRenderer.MeasureText(keyTip, args.Font);
      Point point = new Point(args.CustomLocation.X - (size.Width / 2 + num), args.CustomLocation.Y);
      Rectangle rect = new Rectangle(point.X, point.Y, size.Width + 2 * num, size.Height + 2 * num);
      graphics.FillRectangle((Brush) new SolidBrush(args.BackColor), rect);
      graphics.DrawRectangle(new Pen(args.BorderColor), rect);
      if (this.compositionEnabled)
      {
        using (GraphicsPath path = new GraphicsPath())
        {
          SmoothingMode smoothingMode = graphics.SmoothingMode;
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
          float emSize = args.Font.SizeInPoints / 72f * graphics.DpiX;
          path.AddString(keyTip, args.Font.FontFamily, (int) args.Font.Style, emSize, new Point(point.X + num, point.Y + num), StringFormat.GenericDefault);
          graphics.FillPath((Brush) new SolidBrush(args.ForeColor), path);
          graphics.SmoothingMode = smoothingMode;
        }
      }
      else
        TextRenderer.DrawText((IDeviceContext) graphics, keyTip, args.Font, new Point(point.X + num, point.Y + num), args.ForeColor);
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      System.Type type = element.GetType();
      if ((object) type == (object) typeof (RadButtonElement) || (object) type == (object) typeof (RadRibbonBarButtonGroup) || ((object) type == (object) typeof (RibbonTabStripElement) || (object) type == (object) typeof (RadRibbonBarElement)) || (element.Class == "ApplicationButton" || (object) type == (object) typeof (RadScrollViewer) || ((object) type == (object) typeof (RadCheckBoxElement) || (object) type == (object) typeof (RadToggleButtonElement))) || ((object) type == (object) typeof (RadRepeatButtonElement) || (object) type == (object) typeof (RadRibbonBarGroupDropDownButtonElement)))
        return true;
      if (type.Equals(typeof (RadDropDownTextBoxElement)))
      {
        if (element.FindAncestorByThemeEffectiveType(typeof (RadDropDownListElement)) != null)
          return true;
      }
      else if (type.Equals(typeof (RadMaskedEditBoxElement)) && element.FindAncestor<RadDateTimePickerElement>() != null)
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "ItemsCount")
      {
        request.Data = (object) this.RibbonBarElement.TabStripElement.TabItems.Count.ToString();
      }
      else
      {
        if (request.ControlType == "TabPage" && request.Type == IPCMessage.MessageTypes.GetChildPropertyValue && request.Data != null)
        {
          string s = request.Data.ToString();
          int result = 0;
          int.TryParse(s, out result);
          if (request.Message == "Selected")
            request.Data = (object) (this.RibbonBarElement.TabStripElement.TabItems[result] == this.RibbonBarElement.TabStripElement.SelectedItem);
          if (request.Message == "Text")
            request.Data = (object) this.RibbonBarElement.TabStripElement.TabItems[result].Text;
        }
        if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "HasChildNodes")
          request.Data = (object) (this.RibbonBarElement.TabStripElement.TabItems.Count > 0).ToString();
        else if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "SelectedTab")
          request.Data = (object) this.RibbonBarElement.TabStripElement.SelectedItem.ToString();
        else if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "Text")
          request.Data = (object) this.Text;
        else
          base.ProcessCodedUIMessage(ref request);
      }
    }

    public class RibbonBarInputBehavior : ComponentInputBehavior
    {
      private RadRibbonBar owner;
      private bool navigationActive;
      private RadElement lastFocused;

      public RibbonBarInputBehavior(RadRibbonBar owner)
        : base((IComponentTreeHandler) owner)
      {
        this.owner = owner;
        this.EnableKeyTips = true;
        this.ShowScreenTipsBellowControl = true;
      }

      protected override bool SetInternalKeyMapFocus()
      {
        if (this.owner.ribbonBarElement.TabStripElement.SelectedItem != null)
          this.owner.ribbonBarElement.TabStripElement.SelectedItem.Focus();
        else if (this.owner.ribbonBarElement.TabStripElement.Items.Count > 0)
          this.owner.ribbonBarElement.TabStripElement.Items[0].Focus();
        else
          this.owner.ribbonBarElement.TabStripElement.Focus();
        if (!this.owner.IsDisposed)
          this.owner.Capture = true;
        return true;
      }

      public override bool SetKeyMap()
      {
        if (this.owner.BackstageControl == null || !this.owner.BackstageControl.Visible)
          return base.SetKeyMap();
        if (this.owner.BackstageControl.EnableKeyMap)
          this.owner.BackstageControl.Behavior.SetKeyMap();
        return false;
      }

      protected override void InitializeKeyMap()
      {
        base.InitializeKeyMap();
        this.lastFocused = (RadElement) null;
      }

      protected override List<RadItem> GetRootItems()
      {
        List<RadItem> radItemList = new List<RadItem>();
        radItemList.Add((RadItem) this.owner.ribbonBarElement.ApplicationButtonElement);
        foreach (RadItem accessToolBarItem in (RadItemCollection) this.owner.QuickAccessToolBarItems)
        {
          if (accessToolBarItem.Enabled)
            radItemList.Add(accessToolBarItem);
        }
        foreach (RibbonTab commandTab in (RadItemCollection) this.owner.CommandTabs)
        {
          if (commandTab.Enabled)
            radItemList.Add((RadItem) commandTab.Tab);
        }
        return radItemList;
      }

      protected override bool ActivateSelectedItem(RadItem currentKeyMapItem)
      {
        if (currentKeyMapItem == null || currentKeyMapItem.Visibility != ElementVisibility.Visible || !this.EnableKeyMap)
          return false;
        this.owner.AccessibilityNotifyClients(AccessibleEvents.Focus, -1);
        CancelEventArgs eventArgs = new CancelEventArgs(false);
        if (this.owner.OnKeyTipItemActivating(currentKeyMapItem, eventArgs))
          return false;
        if (currentKeyMapItem is RadPageViewItem)
        {
          foreach (RibbonTab commandTab in (RadItemCollection) this.owner.CommandTabs)
          {
            if (commandTab.Tab == currentKeyMapItem)
            {
              RadPageViewItem radPageViewItem = currentKeyMapItem as RadPageViewItem;
              radPageViewItem.IsSelected = true;
              if (!this.owner.Expanded)
                this.owner.RibbonBarElement.ShowPopup(radPageViewItem);
              currentKeyMapItem.Focus();
              break;
            }
          }
          return false;
        }
        if (currentKeyMapItem == this.owner.RibbonBarElement.ApplicationButtonElement)
        {
          if (this.owner.ApplicationMenuStyle == ApplicationMenuStyle.BackstageView)
          {
            if (this.owner.BackstageControl.EnableKeyMap && !this.owner.BackstageControl.Behavior.IsKeyMapActive)
              this.owner.BackstageControl.Behavior.SetKeyMap();
            this.owner.BackstageControl.ShowPopup(this.owner.RibbonBarElement);
          }
          else
            (this.ActiveKeyMapItem as RadDropDownButtonElement).ShowDropDown();
          return false;
        }
        if (currentKeyMapItem is InnerItem)
        {
          this.ActiveKeyMapItem.PerformClick();
          return false;
        }
        if (currentKeyMapItem is RadRibbonBarGroup)
        {
          (currentKeyMapItem as RadRibbonBarGroup).DropDownElement.PerformClick();
          return false;
        }
        if (currentKeyMapItem is RadDropDownButtonElement)
        {
          (this.ActiveKeyMapItem as RadDropDownButtonElement).ShowDropDown();
          return false;
        }
        if (currentKeyMapItem is RadButtonElement)
        {
          (currentKeyMapItem as RadButtonElement).PerformClick();
          return true;
        }
        if (currentKeyMapItem is RadTextBoxElement)
        {
          (currentKeyMapItem as RadTextBoxElement).Focus();
          return false;
        }
        if (currentKeyMapItem is RadDropDownListElement)
        {
          if ((currentKeyMapItem as RadDropDownListElement).DropDownStyle == RadDropDownStyle.DropDownList)
            (currentKeyMapItem as RadDropDownListElement).ArrowButton.PerformClick();
          else
            (currentKeyMapItem as RadDropDownListElement).TextBox.Focus();
          return false;
        }
        if (currentKeyMapItem is RadGalleryElement)
        {
          (currentKeyMapItem as RadGalleryElement).ShowDropDown();
          return false;
        }
        this.ActiveKeyMapItem.PerformClick();
        return false;
      }

      protected override List<RadItem> GetKeyFocusChildren(RadItem currentKeyMapItem)
      {
        if (currentKeyMapItem == null)
          return this.GetRootItems();
        List<RadItem> radItemList = new List<RadItem>();
        if (currentKeyMapItem is RadPageViewItem && this.owner.SelectedCommandTab != null && this.owner.SelectedCommandTab.Tab == currentKeyMapItem)
        {
          for (int index = 0; index < this.owner.SelectedCommandTab.Items.Count; ++index)
          {
            RadRibbonBarGroup radRibbonBarGroup = this.owner.SelectedCommandTab.Items[index] as RadRibbonBarGroup;
            if (radRibbonBarGroup != null && radRibbonBarGroup.Visibility != ElementVisibility.Collapsed)
            {
              if (radRibbonBarGroup.VisibilityState == ChunkVisibilityState.Collapsed)
              {
                radItemList.Add((RadItem) radRibbonBarGroup);
              }
              else
              {
                List<RadItem> keyFocusChildren = this.GetKeyFocusChildren((RadItem) radRibbonBarGroup);
                radItemList.AddRange((IEnumerable<RadItem>) keyFocusChildren);
              }
            }
          }
        }
        else if (currentKeyMapItem is RadRibbonBarGroup)
        {
          RadRibbonBarGroup radRibbonBarGroup = currentKeyMapItem as RadRibbonBarGroup;
          if (radRibbonBarGroup != null)
          {
            if (radRibbonBarGroup.VisibilityState == ChunkVisibilityState.Collapsed)
            {
              radItemList.AddRange((IEnumerable<RadItem>) radRibbonBarGroup.DropDownElement.Items);
            }
            else
            {
              foreach (RadItem currentKeyMapItem1 in (RadItemCollection) radRibbonBarGroup.Items)
              {
                if (!(currentKeyMapItem1 is RadRibbonBarButtonGroup))
                {
                  if (currentKeyMapItem1.Enabled && currentKeyMapItem1.Visibility == ElementVisibility.Visible)
                    radItemList.Add(currentKeyMapItem1);
                }
                else
                {
                  List<RadItem> keyFocusChildren = this.GetKeyFocusChildren(currentKeyMapItem1);
                  radItemList.AddRange((IEnumerable<RadItem>) keyFocusChildren);
                }
              }
            }
          }
        }
        else
        {
          if (currentKeyMapItem is RadDropDownButtonElement || currentKeyMapItem is RadDropDownListElement)
            return radItemList;
          if (currentKeyMapItem is RadRibbonBarButtonGroup)
          {
            foreach (RadItem currentKeyMapItem1 in (RadItemCollection) (currentKeyMapItem as RadRibbonBarButtonGroup).Items)
            {
              if (!(currentKeyMapItem1 is RadRibbonBarButtonGroup) && !(currentKeyMapItem1 is RibbonBarGroupSeparator) && currentKeyMapItem1.Enabled)
              {
                radItemList.Add(currentKeyMapItem1);
              }
              else
              {
                List<RadItem> keyFocusChildren = this.GetKeyFocusChildren(currentKeyMapItem1);
                radItemList.AddRange((IEnumerable<RadItem>) keyFocusChildren);
              }
            }
          }
          else
          {
            foreach (RadElement child in currentKeyMapItem.Children)
            {
              if (child is RadItem && child.Enabled)
                radItemList.Add(child as RadItem);
            }
          }
        }
        return radItemList;
      }

      private bool ArrowKeySelectionCases(Keys key)
      {
        switch (key)
        {
          case Keys.Left:
            this.SelectNextElement(RadDirection.Left);
            break;
          case Keys.Up:
            this.SelectNextElement(RadDirection.Up);
            break;
          case Keys.Right:
            this.SelectNextElement(RadDirection.Right);
            break;
          case Keys.Down:
            this.SelectNextElement(RadDirection.Down);
            break;
          default:
            this.lastFocused.IsMouseOver = this.lastFocused.ContainsMouse;
            return false;
        }
        return true;
      }

      protected internal bool ProcessArrowKeys(Keys input)
      {
        if (this.IsKeyMapActive && this.owner.enableKeyboardNavigation && (input == Keys.Right || input == Keys.Left || (input == Keys.Up || input == Keys.Down)))
        {
          this.ResetKeyMapInternal();
          this.navigationActive = true;
        }
        if (!this.navigationActive)
          return false;
        if (this.lastFocused == null)
          this.lastFocused = this.CurrentFocusedElement;
        if (this.lastFocused == null)
          this.lastFocused = this.LastFocusedElement;
        if (this.lastFocused == null || this.lastFocused.ElementTree == null || this.lastFocused.ElementTree.Control != this.owner)
          this.lastFocused = (RadElement) this.owner.ribbonBarElement.ApplicationButtonElement;
        bool flag = this.ArrowKeySelectionCases(input);
        if (!flag && (!(this.lastFocused is RadGalleryElement) || input != Keys.Escape && input != Keys.Space))
          this.navigationActive = false;
        return flag;
      }

      private bool SelectNextElement(RadDirection direction)
      {
        RadElement radElement = direction == RadDirection.Left || direction == RadDirection.Right ? this.FindNextHorizontalElement(direction) : this.FindNextVerticalElement(direction);
        if (radElement == this.lastFocused)
          return false;
        if (radElement is RibbonTab)
          ((RadPageViewItem) radElement).IsSelected = true;
        if (radElement is RadDropDownButtonElement)
        {
          int num = (int) radElement.SetValue(RadDropDownButtonElement.MouseOverStateProperty, (object) DropDownButtonMouseOverState.OverActionButton);
        }
        if (radElement == null)
          return false;
        radElement.IsMouseOver = true;
        this.lastFocused.IsMouseOver = this.lastFocused.ContainsMouse;
        this.lastFocused = radElement;
        radElement.Focus();
        this.owner.AccessibilityNotifyClients(AccessibleEvents.Focus, -1);
        return true;
      }

      private RadElement FindNextHorizontalElement(RadDirection direction)
      {
        List<RadElement> tabStripLevelItems = this.GetTabStripLevelItems();
        List<RadElement> topAreaLevelItems = this.GetTopAreaLevelItems();
        List<RadElement> contentAreaLevelItems = this.GetContentAreaLevelItems();
        List<RadElement> captionLevelItems = this.GetGroupCaptionLevelItems();
        List<RadElement> bottomAreaLevelItems = this.GetBottomAreaLevelItems();
        if (tabStripLevelItems.Contains(this.lastFocused))
        {
          int num = tabStripLevelItems.IndexOf(this.lastFocused) + (direction == RadDirection.Right ? 1 : -1);
          while (num < 0)
            num += tabStripLevelItems.Count;
          int index = num % tabStripLevelItems.Count;
          return tabStripLevelItems[index];
        }
        if (topAreaLevelItems.Contains(this.lastFocused))
        {
          int num = topAreaLevelItems.IndexOf(this.lastFocused) + (direction == RadDirection.Right ? 1 : -1);
          while (num < 0)
            num += topAreaLevelItems.Count;
          int index = num % topAreaLevelItems.Count;
          return topAreaLevelItems[index];
        }
        List<RadElement> list = !contentAreaLevelItems.Contains(this.lastFocused) ? (!captionLevelItems.Contains(this.lastFocused) ? bottomAreaLevelItems : captionLevelItems) : contentAreaLevelItems;
        if (direction != RadDirection.Right)
          return RadRibbonBar.RibbonBarInputBehavior.FindLeftItem(this.lastFocused, list);
        return RadRibbonBar.RibbonBarInputBehavior.FindRightItem(this.lastFocused, list);
      }

      private RadElement FindNextVerticalElement(RadDirection direction)
      {
        List<RadElement> tabStripLevelItems = this.GetTabStripLevelItems();
        List<RadElement> topAreaLevelItems = this.GetTopAreaLevelItems();
        List<RadElement> contentAreaLevelItems = this.GetContentAreaLevelItems();
        List<RadElement> captionLevelItems = this.GetGroupCaptionLevelItems();
        List<RadElement> bottomAreaLevelItems = this.GetBottomAreaLevelItems();
        if (tabStripLevelItems.Contains(this.lastFocused))
        {
          if (direction == RadDirection.Up)
            return RadRibbonBar.RibbonBarInputBehavior.FindUpperItem(this.lastFocused, topAreaLevelItems);
          if (contentAreaLevelItems.Count <= 0)
            return RadRibbonBar.RibbonBarInputBehavior.FindLowerItem(this.lastFocused, captionLevelItems) ?? RadRibbonBar.RibbonBarInputBehavior.FindLowerItem(this.lastFocused, bottomAreaLevelItems);
          return contentAreaLevelItems[0];
        }
        if (topAreaLevelItems.Contains(this.lastFocused) && direction == RadDirection.Down)
          return RadRibbonBar.RibbonBarInputBehavior.FindLowerItem(this.lastFocused, tabStripLevelItems);
        if (contentAreaLevelItems.Contains(this.lastFocused))
        {
          if (direction == RadDirection.Down)
            return RadRibbonBar.RibbonBarInputBehavior.FindLowerItem(this.lastFocused, contentAreaLevelItems) ?? RadRibbonBar.RibbonBarInputBehavior.FindLowerItem(this.lastFocused, captionLevelItems) ?? RadRibbonBar.RibbonBarInputBehavior.FindLowerItem(this.lastFocused, bottomAreaLevelItems);
          return RadRibbonBar.RibbonBarInputBehavior.FindUpperItem(this.lastFocused, contentAreaLevelItems) ?? (RadElement) this.owner.SelectedCommandTab;
        }
        if (captionLevelItems.Contains(this.lastFocused))
        {
          if (direction == RadDirection.Down)
            return RadRibbonBar.RibbonBarInputBehavior.FindLowerItem(this.lastFocused, bottomAreaLevelItems);
          return RadRibbonBar.RibbonBarInputBehavior.FindUpperItem(this.lastFocused, contentAreaLevelItems) ?? (RadElement) this.owner.SelectedCommandTab;
        }
        if (direction == RadDirection.Up && bottomAreaLevelItems.Contains(this.lastFocused))
          return RadRibbonBar.RibbonBarInputBehavior.FindUpperItem(this.lastFocused, captionLevelItems) ?? RadRibbonBar.RibbonBarInputBehavior.FindUpperItem(this.lastFocused, contentAreaLevelItems) ?? (RadElement) this.owner.SelectedCommandTab;
        return (RadElement) null;
      }

      private static void TraverseItems(IItemsElement owner, List<RadElement> list)
      {
        if (owner == null)
          return;
        foreach (RadItem radItem in (RadItemCollection) owner.Items)
        {
          RadRibbonBarGroup radRibbonBarGroup = owner as RadRibbonBarGroup;
          if (radRibbonBarGroup != null && radRibbonBarGroup.DropDownElement.Visibility == ElementVisibility.Visible)
            list.Add((RadElement) radRibbonBarGroup.DropDownElement);
          else if (radItem.CanFocus && radItem.Visibility == ElementVisibility.Visible)
            list.Add((RadElement) radItem);
          if (radItem is IItemsElement)
            RadRibbonBar.RibbonBarInputBehavior.TraverseItems(radItem as IItemsElement, list);
        }
      }

      private static int ElementLocationComparison(RadElement e1, RadElement e2)
      {
        if (e1 == e2)
          return 0;
        if (e1 == null)
          return -1;
        if (e2 == null)
          return 1;
        if (e1.ControlBoundingRectangle.X.CompareTo(e2.ControlBoundingRectangle.X) == 0)
          return e1.ControlBoundingRectangle.Y.CompareTo(e2.ControlBoundingRectangle.Y);
        return e1.ControlBoundingRectangle.X.CompareTo(e2.ControlBoundingRectangle.X);
      }

      private static RadElement FindUpperItem(
        RadElement lastFocused,
        List<RadElement> list)
      {
        RadElement radElement1 = (RadElement) null;
        Point from = new Point(lastFocused.ControlBoundingRectangle.X + lastFocused.ControlBoundingRectangle.Width / 2, lastFocused.ControlBoundingRectangle.Bottom);
        foreach (RadElement radElement2 in list)
        {
          if (radElement2 != lastFocused && radElement2.ControlBoundingRectangle.Bottom <= lastFocused.ControlBoundingRectangle.Y + lastFocused.ControlBoundingRectangle.Height / 2 && (!list.Contains(lastFocused) || radElement2.ControlBoundingRectangle.Right > lastFocused.ControlBoundingRectangle.Left && radElement2.ControlBoundingRectangle.Left < lastFocused.ControlBoundingRectangle.Right))
          {
            if (radElement1 == null)
            {
              radElement1 = radElement2;
            }
            else
            {
              Point to1 = new Point(radElement2.ControlBoundingRectangle.X + radElement2.ControlBoundingRectangle.Width / 2, radElement2.ControlBoundingRectangle.Bottom);
              Point to2 = new Point(radElement1.ControlBoundingRectangle.X + radElement1.ControlBoundingRectangle.Width / 2, radElement1.ControlBoundingRectangle.Bottom);
              if (LayoutUtils.GetDistance(from, to2) > LayoutUtils.GetDistance(from, to1))
                radElement1 = radElement2;
            }
          }
        }
        return radElement1;
      }

      private static RadElement FindLowerItem(
        RadElement lastFocused,
        List<RadElement> list)
      {
        RadElement radElement1 = (RadElement) null;
        Point from = new Point(lastFocused.ControlBoundingRectangle.X + lastFocused.ControlBoundingRectangle.Width / 2, lastFocused.ControlBoundingRectangle.Y);
        foreach (RadElement radElement2 in list)
        {
          if (radElement2 != lastFocused && radElement2.ControlBoundingRectangle.Top >= lastFocused.ControlBoundingRectangle.Y + lastFocused.ControlBoundingRectangle.Height / 2 && (!list.Contains(lastFocused) || radElement2.ControlBoundingRectangle.Right > lastFocused.ControlBoundingRectangle.Left && radElement2.ControlBoundingRectangle.Left < lastFocused.ControlBoundingRectangle.Right))
          {
            if (radElement1 == null)
            {
              radElement1 = radElement2;
            }
            else
            {
              Point to1 = new Point(radElement2.ControlBoundingRectangle.X + radElement2.ControlBoundingRectangle.Width / 2, radElement2.ControlBoundingRectangle.Bottom);
              Point to2 = new Point(radElement1.ControlBoundingRectangle.X + radElement1.ControlBoundingRectangle.Width / 2, radElement1.ControlBoundingRectangle.Bottom);
              if (LayoutUtils.GetDistance(from, to2) > LayoutUtils.GetDistance(from, to1))
                radElement1 = radElement2;
            }
          }
        }
        return radElement1;
      }

      private static RadElement FindLeftItem(
        RadElement lastFocused,
        List<RadElement> list)
      {
        RadElement radElement1 = (RadElement) null;
        Point from = new Point(lastFocused.ControlBoundingRectangle.Right, lastFocused.ControlBoundingRectangle.Y + lastFocused.ControlBoundingRectangle.Height / 2);
        foreach (RadElement radElement2 in list)
        {
          if (radElement2 != lastFocused && radElement2.ControlBoundingRectangle.Right <= lastFocused.ControlBoundingRectangle.X + lastFocused.ControlBoundingRectangle.Width / 2)
          {
            if (radElement1 == null)
            {
              radElement1 = radElement2;
            }
            else
            {
              Point to1 = new Point(radElement2.ControlBoundingRectangle.X, radElement2.ControlBoundingRectangle.Y + radElement2.ControlBoundingRectangle.Height / 2);
              Point to2 = new Point(radElement1.ControlBoundingRectangle.X, radElement1.ControlBoundingRectangle.Y + radElement1.ControlBoundingRectangle.Height / 2);
              if (LayoutUtils.GetDistance(from, to2) > LayoutUtils.GetDistance(from, to1))
                radElement1 = radElement2;
            }
          }
        }
        if (radElement1 != null)
          return radElement1;
        if (list.Count > 0)
          return list[list.Count - 1];
        return (RadElement) null;
      }

      private static RadElement FindRightItem(
        RadElement lastFocused,
        List<RadElement> list)
      {
        RadElement radElement1 = (RadElement) null;
        Point from = new Point(lastFocused.ControlBoundingRectangle.X, lastFocused.ControlBoundingRectangle.Y + lastFocused.ControlBoundingRectangle.Height / 2);
        foreach (RadElement radElement2 in list)
        {
          if (radElement2 != lastFocused && radElement2.ControlBoundingRectangle.X >= lastFocused.ControlBoundingRectangle.X + lastFocused.ControlBoundingRectangle.Width / 2)
          {
            if (radElement1 == null)
            {
              radElement1 = radElement2;
            }
            else
            {
              Point to1 = new Point(radElement2.ControlBoundingRectangle.X, radElement2.ControlBoundingRectangle.Y + radElement2.ControlBoundingRectangle.Height / 2);
              Point to2 = new Point(radElement1.ControlBoundingRectangle.X, radElement1.ControlBoundingRectangle.Y + radElement1.ControlBoundingRectangle.Height / 2);
              if (LayoutUtils.GetDistance(from, to2) > LayoutUtils.GetDistance(from, to1))
                radElement1 = radElement2;
            }
          }
        }
        if (radElement1 != null)
          return radElement1;
        if (list.Count > 0)
          return list[0];
        return (RadElement) null;
      }

      private List<RadElement> GetContentAreaLevelItems()
      {
        List<RadElement> list = new List<RadElement>();
        if (this.owner.SelectedCommandTab != null)
        {
          foreach (RadItem radItem in (RadItemCollection) this.owner.SelectedCommandTab.Items)
            RadRibbonBar.RibbonBarInputBehavior.TraverseItems(radItem as IItemsElement, list);
          list.Sort(new Comparison<RadElement>(RadRibbonBar.RibbonBarInputBehavior.ElementLocationComparison));
        }
        return list;
      }

      private List<RadElement> GetTopAreaLevelItems()
      {
        List<RadElement> radElementList = new List<RadElement>();
        if (this.owner.CloseButton)
          radElementList.Add((RadElement) this.owner.RibbonBarElement.RibbonCaption.CloseButton);
        if (this.owner.MinimizeButton)
          radElementList.Add((RadElement) this.owner.RibbonBarElement.RibbonCaption.MinimizeButton);
        if (this.owner.MaximizeButton)
          radElementList.Add((RadElement) this.owner.RibbonBarElement.RibbonCaption.MaximizeButton);
        if (!this.owner.RibbonBarElement.QuickAccessToolbarBelowRibbon && this.owner.RibbonBarElement.QuickAccessToolBar.Visibility == ElementVisibility.Visible)
        {
          if (this.owner.RibbonBarElement.QuickAccessToolBar.OverflowButtonElement.Visibility == ElementVisibility.Visible)
            radElementList.Add((RadElement) this.owner.RibbonBarElement.QuickAccessToolBar.OverflowButtonElement);
          foreach (RadItem accessToolBarItem in (RadItemCollection) this.owner.QuickAccessToolBarItems)
          {
            if (accessToolBarItem.Visibility == ElementVisibility.Visible)
              radElementList.Add((RadElement) accessToolBarItem);
          }
        }
        radElementList.Sort(new Comparison<RadElement>(RadRibbonBar.RibbonBarInputBehavior.ElementLocationComparison));
        return radElementList;
      }

      private List<RadElement> GetTabStripLevelItems()
      {
        List<RadElement> radElementList = new List<RadElement>();
        if (this.owner.RibbonBarElement.ApplicationButtonElement.Visibility == ElementVisibility.Visible)
          radElementList.Add((RadElement) this.owner.RibbonBarElement.ApplicationButtonElement);
        if (this.owner.RibbonBarElement.ExpandButton.Visibility == ElementVisibility.Visible)
          radElementList.Add((RadElement) this.owner.RibbonBarElement.ExpandButton);
        if (this.owner.RibbonBarElement.MDIbutton.Visibility == ElementVisibility.Visible)
        {
          RadMDIControlsItem mdIbutton = this.owner.RibbonBarElement.MDIbutton;
          if (mdIbutton.CloseButton.Visibility == ElementVisibility.Visible)
            radElementList.Add((RadElement) mdIbutton.CloseButton);
          if (mdIbutton.MaximizeButton.Visibility == ElementVisibility.Visible)
            radElementList.Add((RadElement) mdIbutton.MaximizeButton);
          if (mdIbutton.MinimizeButton.Visibility == ElementVisibility.Visible)
            radElementList.Add((RadElement) mdIbutton.MinimizeButton);
        }
        foreach (RibbonTab commandTab in (RadItemCollection) this.owner.CommandTabs)
          radElementList.Add((RadElement) commandTab);
        radElementList.Sort(new Comparison<RadElement>(RadRibbonBar.RibbonBarInputBehavior.ElementLocationComparison));
        return radElementList;
      }

      private List<RadElement> GetGroupCaptionLevelItems()
      {
        List<RadElement> radElementList = new List<RadElement>();
        if (this.owner.SelectedCommandTab != null)
        {
          foreach (RadRibbonBarGroup radRibbonBarGroup in (RadItemCollection) this.owner.SelectedCommandTab.Items)
          {
            if (radRibbonBarGroup.ShowDialogButton)
              radElementList.Add((RadElement) radRibbonBarGroup.DialogButton);
          }
          radElementList.Sort(new Comparison<RadElement>(RadRibbonBar.RibbonBarInputBehavior.ElementLocationComparison));
        }
        return radElementList;
      }

      private List<RadElement> GetBottomAreaLevelItems()
      {
        List<RadElement> radElementList = new List<RadElement>();
        if (this.owner.RibbonBarElement.QuickAccessToolbarBelowRibbon && this.owner.RibbonBarElement.QuickAccessToolBar.Visibility == ElementVisibility.Visible)
        {
          if (this.owner.RibbonBarElement.QuickAccessToolBar.OverflowButtonElement.Visibility == ElementVisibility.Visible)
            radElementList.Add((RadElement) this.owner.RibbonBarElement.QuickAccessToolBar.OverflowButtonElement);
          foreach (RadItem accessToolBarItem in (RadItemCollection) this.owner.QuickAccessToolBarItems)
          {
            if (accessToolBarItem.Visibility == ElementVisibility.Visible)
              radElementList.Add((RadElement) accessToolBarItem);
          }
        }
        radElementList.Sort(new Comparison<RadElement>(RadRibbonBar.RibbonBarInputBehavior.ElementLocationComparison));
        return radElementList;
      }
    }
  }
}
