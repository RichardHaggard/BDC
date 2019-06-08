// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTitleBar
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [Docking(DockingBehavior.Ask)]
  [Description("Used to add a titlebar to a shaped form")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  public class RadTitleBar : RadControl
  {
    private RadTitleBarElement titleBarElement;
    private ContextMenu menu;
    private Form associatedForm;

    public RadTitleBar()
    {
      this.menu = new ContextMenu();
      this.menu.Popup += new EventHandler(this.menu_Popup);
      MenuItem menuItem1 = new MenuItem("Restore");
      menuItem1.Click += new EventHandler(this.item_RestoreClick);
      this.menu.MenuItems.Add(menuItem1);
      this.menu.MenuItems.Add(new MenuItem("Move"));
      this.menu.MenuItems.Add(new MenuItem("Size"));
      MenuItem menuItem2 = new MenuItem(nameof (Minimize));
      menuItem2.Click += new EventHandler(this.item_MinimizeClick);
      this.menu.MenuItems.Add(menuItem2);
      MenuItem menuItem3 = new MenuItem("Maximize");
      menuItem3.Click += new EventHandler(this.item_MaximizeClick);
      this.menu.MenuItems.Add(menuItem3);
      this.menu.MenuItems.Add(new MenuItem("-"));
      MenuItem menuItem4 = new MenuItem("Close    Alt+F4");
      menuItem4.Click += new EventHandler(this.item_CloseClick);
      this.menu.MenuItems.Add(menuItem4);
      this.ContextMenu = this.menu;
      this.TabStop = false;
      Size defaultSize = this.DefaultSize;
      this.ElementTree.PerformInnerLayout(true, 0, 0, defaultSize.Width, defaultSize.Height);
    }

    [SettingsBindable(true)]
    [Bindable(true)]
    [Description("Gets or sets the text associated with this item.")]
    [Category("Appearance")]
    public override string Text
    {
      get
      {
        return this.titleBarElement.Text;
      }
      set
      {
        this.titleBarElement.Text = value;
      }
    }

    [DefaultValue(true)]
    [Description("Determines whether the parent form can be managed by the title bar.")]
    [Category("Behavior")]
    public bool CanManageOwnerForm
    {
      get
      {
        return this.titleBarElement.CanManageOwnerForm;
      }
      set
      {
        this.titleBarElement.CanManageOwnerForm = value;
      }
    }

    [Description("Get or sets a value indicating whether the form can be resized")]
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(true)]
    public bool AllowResize
    {
      get
      {
        return this.titleBarElement.AllowResize;
      }
      set
      {
        this.titleBarElement.AllowResize = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(220, 23));
      }
    }

    [Category("Window Style")]
    [DefaultValue(null)]
    public Icon ImageIcon
    {
      get
      {
        return this.titleBarElement.ImageIcon;
      }
      set
      {
        this.titleBarElement.ImageIcon = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadTitleBarElement TitleBarElement
    {
      get
      {
        return this.titleBarElement;
      }
    }

    [Localizable(true)]
    [Category("Appearance")]
    [DefaultValue(null)]
    [Description("Background right image")]
    public Image RightImage
    {
      get
      {
        return this.titleBarElement.RightImage;
      }
      set
      {
        this.titleBarElement.RightImage = value;
      }
    }

    [DefaultValue(null)]
    [Description("Background left image")]
    [Localizable(true)]
    [Category("Appearance")]
    public Image LeftImage
    {
      get
      {
        return this.titleBarElement.LeftImage;
      }
      set
      {
        this.titleBarElement.LeftImage = value;
      }
    }

    public event TitleBarSystemEventHandler Close;

    public event TitleBarSystemEventHandler Minimize;

    public event TitleBarSystemEventHandler MaximizeRestore;

    public event TitleBarSystemEventHandler MinimizeInTheTray;

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      this.titleBarElement.Text = this.Text;
    }

    protected virtual void OnHelpButtonClicked(object sender, EventArgs args)
    {
      Form parentForm = RadTitleBar.FindParentForm((Control) this);
      if (parentForm == null || !parentForm.IsHandleCreated)
        return;
      int num = 61824;
      Telerik.WinControls.NativeMethods.PostMessage(new HandleRef((object) this, parentForm.Handle), 274, new IntPtr(num), IntPtr.Zero);
    }

    protected virtual void OnClose(object sender, EventArgs args)
    {
      if (this.Close != null)
        this.Close(sender, args);
      RadTitleBar.FindParentForm((Control) this)?.Close();
    }

    protected virtual void OnMinimize(object sender, EventArgs args)
    {
      Form parentForm = RadTitleBar.FindParentForm((Control) this);
      if (parentForm != null && !parentForm.MinimizeBox)
        return;
      if (this.Minimize != null)
        this.Minimize(sender, args);
      if (parentForm == null)
        return;
      parentForm.WindowState = FormWindowState.Minimized;
    }

    protected virtual void OnMaximizeRestore(object sender, EventArgs args)
    {
      Form parentForm = RadTitleBar.FindParentForm((Control) this);
      if (parentForm != null && !parentForm.MaximizeBox)
        return;
      if (this.MaximizeRestore != null)
        this.MaximizeRestore(sender, args);
      if (parentForm == null)
        return;
      if (parentForm.WindowState == FormWindowState.Maximized)
        parentForm.WindowState = FormWindowState.Normal;
      else
        parentForm.WindowState = FormWindowState.Maximized;
    }

    protected virtual void OnMinimizeInTheTray(object sender, EventArgs args)
    {
      if (this.MinimizeInTheTray == null)
        return;
      this.MinimizeInTheTray(sender, args);
    }

    protected override void OnBackgroundImageChanged(EventArgs e)
    {
      base.OnBackgroundImageChanged(e);
      this.TitleBarElement.MiddleImage = this.BackgroundImage;
    }

    protected override void OnParentChanged(EventArgs e)
    {
      if (this.associatedForm != null)
      {
        this.associatedForm.TextChanged -= new EventHandler(this.OnParentForm_TextChanged);
        this.associatedForm.StyleChanged -= new EventHandler(this.OnAssociatedForm_StyleChanged);
      }
      base.OnParentChanged(e);
      this.associatedForm = RadTitleBar.FindParentForm((Control) this);
      if (this.associatedForm == null)
        return;
      this.associatedForm.TextChanged += new EventHandler(this.OnParentForm_TextChanged);
      this.associatedForm.StyleChanged += new EventHandler(this.OnAssociatedForm_StyleChanged);
      this.titleBarElement.Text = this.associatedForm.Text;
    }

    private void OnAssociatedForm_StyleChanged(object sender, EventArgs e)
    {
      Form parentForm = RadTitleBar.FindParentForm((Control) this);
      if (parentForm == null)
        return;
      if (parentForm.MaximizeBox)
      {
        int num1 = (int) this.titleBarElement.MaximizeButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      else
      {
        int num2 = (int) this.titleBarElement.MaximizeButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      }
      if (parentForm.MinimizeBox)
      {
        int num3 = (int) this.titleBarElement.MinimizeButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      else
      {
        int num4 = (int) this.titleBarElement.MinimizeButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      }
      if (!parentForm.MinimizeBox && !parentForm.MaximizeBox && parentForm.HelpButton)
      {
        int num5 = (int) this.titleBarElement.HelpButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Visible);
      }
      else
      {
        int num6 = (int) this.titleBarElement.HelpButton.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      }
    }

    private void OnParentForm_TextChanged(object sender, EventArgs e)
    {
      if (this.associatedForm == null)
        return;
      this.titleBarElement.Text = this.associatedForm.Text;
    }

    private void item_RestoreClick(object sender, EventArgs e)
    {
      this.OnMaximizeRestore(sender, e);
    }

    private void item_MinimizeClick(object sender, EventArgs e)
    {
      this.OnMinimize(sender, e);
    }

    private void item_MaximizeClick(object sender, EventArgs e)
    {
      this.OnMaximizeRestore(sender, e);
    }

    private void item_CloseClick(object sender, EventArgs e)
    {
      this.OnClose(sender, e);
    }

    private void menu_Popup(object sender, EventArgs e)
    {
      Form parentForm = RadTitleBar.FindParentForm((Control) this);
      if (parentForm == null)
        return;
      bool flag = parentForm.WindowState == FormWindowState.Maximized;
      this.menu.MenuItems[0].Enabled = flag;
      this.menu.MenuItems[1].Enabled = !flag;
      this.menu.MenuItems[2].Enabled = !flag;
      this.menu.MenuItems[4].Enabled = !flag;
    }

    internal void CallOnClose(object sender, EventArgs args)
    {
      this.OnClose(sender, args);
    }

    internal void CallOnMinimize(object sender, EventArgs args)
    {
      this.OnMinimize(sender, args);
    }

    internal void CallOnMaximizeRestore(object sender, EventArgs args)
    {
      this.OnMaximizeRestore(sender, args);
    }

    internal void CallOnMinimizeInTheTray(object sender, EventArgs args)
    {
      this.OnMinimizeInTheTray(sender, args);
    }

    internal void CallOnHelpButtonClicked(object sender, EventArgs args)
    {
      this.OnHelpButtonClicked(sender, args);
    }

    internal static Form FindParentForm(Control ctl)
    {
      if (ctl == null)
        return (Form) null;
      if (ctl.Parent != null && ctl.Parent is Form && !((Form) ctl.Parent).TopLevel)
        return (Form) ctl.Parent;
      Form form = ctl.FindForm();
      if (form != null && form.IsMdiChild)
        return form;
      if (ctl is IComponentTreeHandler)
        return ((IComponentTreeHandler) ctl).Behavior.FindFormInternal(ctl);
      return (Form) null;
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.titleBarElement = this.CreateTitleBarElement();
      this.titleBarElement.Class = "TitleBar";
      this.RootElement.Children.Add((RadElement) this.titleBarElement);
      base.CreateChildItems(parent);
    }

    protected virtual RadTitleBarElement CreateTitleBarElement()
    {
      return new RadTitleBarElement();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadTitleBarAccessibleObject(this);
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 132 && this.FindForm() != null && (Cursor.Current != (Cursor) null && Cursor.Current.Equals((object) Cursors.Help)))
      {
        RadElement elementAtPoint = this.ElementTree.GetElementAtPoint(this.PointToClient(Control.MousePosition));
        if (elementAtPoint != null && elementAtPoint == this.TitleBarElement.HelpButton)
          return;
      }
      base.WndProc(ref m);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.TitleBarElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TitleBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.TitleBarElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.TitleBarElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TitleFill");
        this.TitleBarElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, state, "TitleFill");
      }
      this.TitleBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.TitleBarElement.SuspendApplyOfThemeSettings();
      this.TitleBarElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.TitleBarElement.ResetThemeValueOverride(FillPrimitive.GradientStyleProperty);
      this.TitleBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.TitleBarElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TitleBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.TitleBarElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.TitleBarElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state, "TitleCaption");
      }
      this.TitleBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.TitleBarElement.SuspendApplyOfThemeSettings();
      this.TitleBarElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num = (int) this.TitleBarElement.TitlePrimitive.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.TitleBarElement.ElementTree.ApplyThemeToElementTree();
      this.TitleBarElement.ResumeApplyOfThemeSettings();
    }
  }
}
