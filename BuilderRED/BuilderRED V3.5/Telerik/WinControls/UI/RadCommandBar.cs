// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Design;
using Telerik.WinControls.Localization;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Menus & Toolbars")]
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("A flexible component for implementation of tool and button bars featuring docking behavior, toggling buttons, shrinkable toolbars")]
  [DefaultProperty("Rows")]
  [Docking(DockingBehavior.Ask)]
  [Designer("Telerik.WinControls.UI.Design.RadCommandBarDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadCommandBar : RadControl
  {
    public static readonly object OrientationChangingEventKey = new object();
    public static readonly object OrientationChangedEventKey = new object();
    private RadCommandBarElement commandBarElement;
    private Form parentForm;
    private RadDropDownMenu contextMenu;
    private RadMenuItem customizeMenuItem;

    public RadCommandBar()
    {
      this.AutoSize = true;
    }

    protected override RootRadElement CreateRootElement()
    {
      return (RootRadElement) new RadCommandBar.RadCommandBarRootElement();
    }

    protected virtual RadCommandBarElement CreateCommandBarElement()
    {
      return new RadCommandBarElement();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.commandBarElement = this.CreateCommandBarElement();
      this.RootElement.Children.Add((RadElement) this.commandBarElement);
      this.ApplyOrientation(this.commandBarElement.Orientation);
      this.contextMenu = new RadDropDownMenu();
      this.customizeMenuItem = new RadMenuItem(LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCustomizeText"));
      this.customizeMenuItem.Click += new EventHandler(this.customizeMenuItem_Click);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadCommandBarAccessibleObject(this);
    }

    [DefaultValue(true)]
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

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(25, 25));
      }
    }

    [Localizable(true)]
    [DefaultValue(0)]
    [Category("Layout")]
    [RefreshProperties(RefreshProperties.All)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        switch (value)
        {
          case DockStyle.None:
          case DockStyle.Top:
          case DockStyle.Bottom:
          case DockStyle.Fill:
            this.Orientation = Orientation.Horizontal;
            break;
          case DockStyle.Left:
          case DockStyle.Right:
            this.Orientation = Orientation.Vertical;
            break;
        }
        base.Dock = value;
      }
    }

    public RadDropDownMenu CustomizeContextMenu
    {
      get
      {
        return this.contextMenu;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the size in pixels when current strip is being Drag and Drop in next or previous row")]
    [DefaultValue(typeof (Size), "4,4")]
    public Size DragSize
    {
      get
      {
        return this.CommandBarElement.DragSize;
      }
      set
      {
        this.CommandBarElement.DragSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadCommandBarElement CommandBarElement
    {
      get
      {
        return this.commandBarElement;
      }
      set
      {
        this.commandBarElement = value;
      }
    }

    [DefaultValue(Orientation.Horizontal)]
    [Category("Behavior")]
    [Description("Orientation of the CommandBar - could be horizontal or vertical")]
    [Browsable(false)]
    public Orientation Orientation
    {
      get
      {
        return this.commandBarElement.Orientation;
      }
      set
      {
        if (this.commandBarElement == null || this.commandBarElement.Orientation == value)
          return;
        this.SetOrientationCore(value, true);
      }
    }

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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    public event CancelEventHandler OrientationChanging
    {
      add
      {
        this.Events.AddHandler(RadCommandBar.OrientationChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCommandBar.OrientationChangingEventKey, (Delegate) value);
      }
    }

    public event EventHandler OrientationChanged
    {
      add
      {
        this.Events.AddHandler(RadCommandBar.OrientationChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadCommandBar.OrientationChangedEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler FloatingStripCreating
    {
      add
      {
        this.CommandBarElement.FloatingStripCreating += value;
      }
      remove
      {
        this.CommandBarElement.FloatingStripCreating -= value;
      }
    }

    public event CancelEventHandler FloatingStripDocking
    {
      add
      {
        this.CommandBarElement.FloatingStripDocking += value;
      }
      remove
      {
        this.CommandBarElement.FloatingStripDocking -= value;
      }
    }

    public event EventHandler FloatingStripCreated
    {
      add
      {
        this.CommandBarElement.FloatingStripCreated += value;
      }
      remove
      {
        this.CommandBarElement.FloatingStripCreated -= value;
      }
    }

    public event EventHandler FloatingStripDocked
    {
      add
      {
        this.CommandBarElement.FloatingStripDocked += value;
      }
      remove
      {
        this.CommandBarElement.FloatingStripDocked -= value;
      }
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
    }

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      if (this.parentForm != null)
        this.parentForm.SizeChanged -= new EventHandler(this.parentForm_SizeChanged);
      this.parentForm = this.FindForm();
      if (this.parentForm == null)
        return;
      this.parentForm.SizeChanged += new EventHandler(this.parentForm_SizeChanged);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Right)
        return;
      RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(e.Location);
      if (!(elementAtPoint is CommandBarRowElement) && !(elementAtPoint is RadCommandBarElement) && elementAtPoint != null)
        return;
      this.contextMenu.Items.Clear();
      this.contextMenu.RightToLeft = this.RightToLeft;
      this.contextMenu.ThemeName = this.ThemeName;
      foreach (CommandBarStripElement stripInfo in this.CommandBarElement.StripInfoHolder.StripInfoList)
      {
        CommandBarDropDownMenu commandBarDropDownMenu = new CommandBarDropDownMenu(stripInfo);
        commandBarDropDownMenu.Text = stripInfo.DisplayName;
        commandBarDropDownMenu.IsChecked = stripInfo.VisibleInCommandBar;
        this.contextMenu.Items.Add((RadItem) commandBarDropDownMenu);
      }
      this.contextMenu.Items.Add((RadItem) new RadMenuSeparatorItem());
      this.customizeMenuItem.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCustomizeText");
      this.contextMenu.Items.Add((RadItem) this.customizeMenuItem);
      this.contextMenu.Show(this.PointToScreen(e.Location));
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      this.SetThemeCore();
    }

    protected virtual bool OnOrientationChanging(CancelEventArgs args)
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadCommandBar.OrientationChangingEventKey];
      if (cancelEventHandler != null)
        cancelEventHandler((object) this, args);
      return args.Cancel;
    }

    protected virtual void OnOrientationChanged(EventArgs args)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadCommandBar.OrientationChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, args);
    }

    private void parentForm_SizeChanged(object sender, EventArgs e)
    {
      if (FormWindowState.Minimized == this.parentForm.WindowState)
      {
        foreach (CommandBarStripElement stripInfo in this.CommandBarElement.StripInfoHolder.StripInfoList)
        {
          if (stripInfo.FloatingForm != null)
            stripInfo.FloatingForm.Visible = false;
        }
      }
      else
      {
        foreach (CommandBarStripElement stripInfo in this.CommandBarElement.StripInfoHolder.StripInfoList)
        {
          if (stripInfo.FloatingForm != null)
            stripInfo.FloatingForm.Visible = stripInfo.VisibleInCommandBar;
        }
      }
    }

    private void customizeMenuItem_Click(object sender, EventArgs e)
    {
      CommandBarCustomizeDialogProvider.CurrentProvider.ShowCustomizeDialog((object) this, this.CommandBarElement.StripInfoHolder);
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (!(element is RadTextBoxElement) && !(element is RadDropDownListArrowButtonElement) && (!(element is RadDropDownListEditableAreaElement) && !(element is RadDropDownListElement)) && !(element is RadArrowButtonElement))
        return element is RadCommandBarArrowButton;
      return true;
    }

    protected virtual void SetThemeCore()
    {
      foreach (CommandBarRowElement row in this.Rows)
      {
        foreach (CommandBarStripElement strip in row.Strips)
          strip.OverflowButton.HostControlThemeName = this.ThemeName;
      }
    }

    protected virtual void SetOrientationCore(Orientation value, bool fireEvents)
    {
      CancelEventArgs args = new CancelEventArgs();
      if (fireEvents && this.OnOrientationChanging(args))
        return;
      this.RootElement.SuspendLayout(true);
      this.ApplyOrientation(value);
      this.commandBarElement.SetOrientationCore(value);
      this.RootElement.ResumeLayout(true, true);
      if (!fireEvents)
        return;
      this.OnOrientationChanged(new EventArgs());
    }

    private void ApplyOrientation(Orientation orientation)
    {
      this.commandBarElement.SuspendLayout();
      if (orientation == Orientation.Horizontal)
      {
        this.RootElement.StretchVertically = false;
        this.RootElement.StretchHorizontally = true;
      }
      else
      {
        this.RootElement.StretchVertically = true;
        this.RootElement.StretchHorizontally = false;
      }
      this.commandBarElement.ResumeLayout(true);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.CommandBarElement.SuspendApplyOfThemeSettings();
      this.CommandBarElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
      this.CommandBarElement.SetThemeValueOverride(LightVisualElement.GradientStyleProperty, (object) GradientStyles.Solid, "");
      foreach (RadItem row in this.CommandBarElement.Rows)
        row.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
      this.CommandBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.CommandBarElement.SuspendApplyOfThemeSettings();
      this.CommandBarElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.CommandBarElement.ResetThemeValueOverride(LightVisualElement.GradientStyleProperty);
      foreach (RadItem row in this.CommandBarElement.Rows)
        row.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.CommandBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.CommandBarElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, "");
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.CommandBarElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.CommandBarElement.ElementTree.ApplyThemeToElementTree();
    }

    [RefreshProperties(RefreshProperties.All)]
    [RadNewItem("", false, true, true)]
    [RadEditItemsAction]
    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadCommandBarLinesElementCollection Rows
    {
      get
      {
        return this.commandBarElement.Rows;
      }
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "Lines")
        request.Data = (object) this.Rows.Count;
      else
        base.ProcessCodedUIMessage(ref request);
    }

    public class RadCommandBarRootElement : RootRadElement
    {
      public override bool? ShouldSerializeProperty(PropertyDescriptor property)
      {
        if (property.Name == "StretchHorizontally" || property.Name == "StretchVertically")
          return new bool?(false);
        return base.ShouldSerializeProperty(property);
      }
    }
  }
}
