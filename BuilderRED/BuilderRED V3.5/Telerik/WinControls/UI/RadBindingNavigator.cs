// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadBindingNavigator
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
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Data Controls")]
  [Designer("Telerik.WinControls.UI.Design.RadBindingNavigatorDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadBindingNavigator : RadControl
  {
    public static readonly object OrientationChangingEventKey = new object();
    public static readonly object OrientationChangedEventKey = new object();
    private RadBindingNavigatorElement navigatorElement;
    private Form parentForm;
    private RadDropDownMenu contextMenu;
    private RadMenuItem customizeMenuItem;

    public RadBindingNavigator()
    {
      this.AutoSize = true;
    }

    protected override RootRadElement CreateRootElement()
    {
      return (RootRadElement) new RadBindingNavigator.RadBindingNavigatorRootElement();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.navigatorElement = this.CreateNavigatorElement();
      parent.Children.Add((RadElement) this.navigatorElement);
      this.ApplyOrientation(this.navigatorElement.Orientation);
      this.contextMenu = new RadDropDownMenu();
      this.customizeMenuItem = new RadMenuItem(LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCustomizeText"));
      this.customizeMenuItem.Click += new EventHandler(this.customizeMenuItem_Click);
    }

    protected virtual RadBindingNavigatorElement CreateNavigatorElement()
    {
      return new RadBindingNavigatorElement();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadBindingNavigatorElement BindingNavigatorElement
    {
      get
      {
        return this.navigatorElement;
      }
    }

    [Category("Data")]
    [Description("Gets or sets the data source.")]
    [DefaultValue(null)]
    [Browsable(true)]
    public BindingSource BindingSource
    {
      get
      {
        return this.BindingNavigatorElement.BindingSource;
      }
      set
      {
        this.BindingNavigatorElement.BindingSource = value;
      }
    }

    [DefaultValue("of {0}")]
    [Browsable(true)]
    [Description("Gets or sets the CountItemFormat format string.")]
    [Category("Data")]
    public string CountItemFormat
    {
      get
      {
        return this.BindingNavigatorElement.CountItemFormat;
      }
      set
      {
        this.BindingNavigatorElement.CountItemFormat = value;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the control will handle internally the creation of new items.")]
    public bool AutoHandleAddNew
    {
      get
      {
        return this.BindingNavigatorElement.AutoHandleAddNew;
      }
      set
      {
        this.BindingNavigatorElement.AutoHandleAddNew = value;
      }
    }

    [RadEditItemsAction]
    [RadNewItem("", false, true, true)]
    [RefreshProperties(RefreshProperties.All)]
    [Browsable(true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadCommandBarLinesElementCollection Rows
    {
      get
      {
        return this.navigatorElement.Rows;
      }
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

    [RefreshProperties(RefreshProperties.All)]
    [Localizable(true)]
    [Category("Layout")]
    [DefaultValue(0)]
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

    [Description("Gets the width and height of a rectangle centered on the point the mouse button was pressed, within which a drag operation will not begin.")]
    [Category("Behavior")]
    [DefaultValue(typeof (Size), "4,4")]
    public Size DragSize
    {
      get
      {
        return this.BindingNavigatorElement.DragSize;
      }
      set
      {
        this.BindingNavigatorElement.DragSize = value;
      }
    }

    [Browsable(false)]
    [Category("Behavior")]
    [Description("Orientation of the BindingNavigator - could be horizontal or vertical")]
    [DefaultValue(Orientation.Horizontal)]
    public Orientation Orientation
    {
      get
      {
        return this.BindingNavigatorElement.Orientation;
      }
      set
      {
        this.SetOrientationCore(value, true);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
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
        this.Events.AddHandler(RadBindingNavigator.OrientationChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadBindingNavigator.OrientationChangingEventKey, (Delegate) value);
      }
    }

    public event EventHandler OrientationChanged
    {
      add
      {
        this.Events.AddHandler(RadBindingNavigator.OrientationChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadBindingNavigator.OrientationChangedEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler FloatingStripCreating
    {
      add
      {
        this.BindingNavigatorElement.FloatingStripCreating += value;
      }
      remove
      {
        this.BindingNavigatorElement.FloatingStripCreating -= value;
      }
    }

    public event CancelEventHandler FloatingStripDocking
    {
      add
      {
        this.BindingNavigatorElement.FloatingStripDocking += value;
      }
      remove
      {
        this.BindingNavigatorElement.FloatingStripDocking -= value;
      }
    }

    public event EventHandler FloatingStripCreated
    {
      add
      {
        this.BindingNavigatorElement.FloatingStripCreated += value;
      }
      remove
      {
        this.BindingNavigatorElement.FloatingStripCreated -= value;
      }
    }

    public event EventHandler FloatingStripDocked
    {
      add
      {
        this.BindingNavigatorElement.FloatingStripDocked += value;
      }
      remove
      {
        this.BindingNavigatorElement.FloatingStripDocked -= value;
      }
    }

    public event CancelEventHandler ContextMenuOpening;

    protected virtual void OnContextMenuOpenting(CancelEventArgs e)
    {
      if (this.ContextMenuOpening == null)
        return;
      this.ContextMenuOpening((object) this, e);
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
      foreach (CommandBarStripElement stripInfo in this.BindingNavigatorElement.StripInfoHolder.StripInfoList)
      {
        CommandBarDropDownMenu commandBarDropDownMenu = new CommandBarDropDownMenu(stripInfo);
        commandBarDropDownMenu.Text = stripInfo.DisplayName;
        commandBarDropDownMenu.IsChecked = stripInfo.VisibleInCommandBar;
        this.contextMenu.Items.Add((RadItem) commandBarDropDownMenu);
      }
      this.contextMenu.Items.Add((RadItem) new RadMenuSeparatorItem());
      this.customizeMenuItem.Text = LocalizationProvider<CommandBarLocalizationProvider>.CurrentProvider.GetLocalizedString("ContextMenuCustomizeText");
      this.contextMenu.Items.Add((RadItem) this.customizeMenuItem);
      CancelEventArgs e1 = new CancelEventArgs();
      this.OnContextMenuOpenting(e1);
      if (e1.Cancel)
        return;
      this.contextMenu.Show(this.PointToScreen(e.Location));
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      this.SetThemeCore();
    }

    protected virtual bool OnOrientationChanging(CancelEventArgs args)
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadBindingNavigator.OrientationChangingEventKey];
      if (cancelEventHandler != null)
        cancelEventHandler((object) this, args);
      return args.Cancel;
    }

    protected virtual void OnOrientationChanged(EventArgs args)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadBindingNavigator.OrientationChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, args);
    }

    private void parentForm_SizeChanged(object sender, EventArgs e)
    {
      if (FormWindowState.Minimized == this.parentForm.WindowState)
      {
        foreach (CommandBarStripElement stripInfo in this.BindingNavigatorElement.StripInfoHolder.StripInfoList)
        {
          if (stripInfo.FloatingForm != null)
            stripInfo.FloatingForm.Visible = false;
        }
      }
      else
      {
        foreach (CommandBarStripElement stripInfo in this.BindingNavigatorElement.StripInfoHolder.StripInfoList)
        {
          if (stripInfo.FloatingForm != null)
            stripInfo.FloatingForm.Visible = stripInfo.VisibleInCommandBar;
        }
      }
    }

    private void customizeMenuItem_Click(object sender, EventArgs e)
    {
      CommandBarCustomizeDialogProvider.CurrentProvider.ShowCustomizeDialog((object) this, this.BindingNavigatorElement.StripInfoHolder);
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
      if (this.BindingNavigatorElement == null || this.BindingNavigatorElement.Orientation == value)
        return;
      CancelEventArgs args = new CancelEventArgs();
      if (fireEvents && this.OnOrientationChanging(args))
        return;
      this.RootElement.SuspendLayout(true);
      this.ApplyOrientation(value);
      this.BindingNavigatorElement.SetOrientationCore(value);
      this.RootElement.ResumeLayout(true, true);
      if (!fireEvents)
        return;
      this.OnOrientationChanged(new EventArgs());
    }

    private void ApplyOrientation(Orientation orientation)
    {
      this.BindingNavigatorElement.SuspendLayout();
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
      this.BindingNavigatorElement.ResumeLayout(true);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.BindingNavigatorElement.SuspendApplyOfThemeSettings();
      this.BindingNavigatorElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
      this.BindingNavigatorElement.SetThemeValueOverride(LightVisualElement.GradientStyleProperty, (object) GradientStyles.Solid, "");
      this.BindingNavigatorElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "", typeof (CommandBarRowElement));
      this.BindingNavigatorElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.BindingNavigatorElement.SuspendApplyOfThemeSettings();
      this.BindingNavigatorElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.BindingNavigatorElement.ResetThemeValueOverride(LightVisualElement.GradientStyleProperty);
      this.BindingNavigatorElement.ElementTree.ApplyThemeToElementTree();
      this.BindingNavigatorElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.BindingNavigatorElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, "");
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.BindingNavigatorElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "Lines")
        request.Data = (object) this.Rows.Count;
      else
        base.ProcessCodedUIMessage(ref request);
    }

    public class RadBindingNavigatorRootElement : RootRadElement
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
