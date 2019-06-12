// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadStatusStrip
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ProvideProperty("Spring", typeof (RadItem))]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Menus & Toolbars")]
  [ToolboxItem(true)]
  [Description("A themable component which displays status information in an application")]
  [DefaultProperty("Items")]
  [DefaultEvent("StatusBarClick")]
  [Designer("Telerik.WinControls.UI.Design.RadStatusStripDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadStatusStrip : RadControl, IExtenderProvider
  {
    private RadStatusBarElement statusBarElement;
    private bool isInRibbonForm;

    public RadStatusStrip()
    {
      this.AutoSize = true;
      this.Dock = DockStyle.Bottom;
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.statusBarElement = new RadStatusBarElement();
      this.RootElement.Children.Add((RadElement) this.statusBarElement);
      this.statusBarElement.Padding = new Padding(2);
      this.statusBarElement.MinSize = new Size(0, 19);
      this.statusBarElement.LayoutStyleChanged += new EventHandler(this.statusBarElement_LayoutStyleChanged);
      this.statusBarElement.LayoutStyleChanging += new ValueChangingEventHandler(this.statusBarElement_LayoutStyleChanging);
      this.statusBarElement.GripStyle = ToolStripGripStyle.Visible;
      base.CreateChildItems(parent);
    }

    protected override void InitializeRootElement(RootRadElement rootElement)
    {
      base.InitializeRootElement(rootElement);
      rootElement.StretchHorizontally = true;
      rootElement.StretchVertically = false;
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadStatusStripAccessibleObject(this, this.Name);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    internal bool IsInRibbonForm
    {
      get
      {
        return this.isInRibbonForm;
      }
      set
      {
        this.isInRibbonForm = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(300, 24));
      }
    }

    [DefaultValue(true)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether the control is automatically resized to display its entire contents.")]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
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

    [DefaultValue(DockStyle.Bottom)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        if (base.Dock == value)
          return;
        switch (value)
        {
          case DockStyle.Top:
          case DockStyle.Bottom:
            this.StatusBarElement.Orientation = Orientation.Horizontal;
            break;
          case DockStyle.Left:
          case DockStyle.Right:
            this.StatusBarElement.Orientation = Orientation.Vertical;
            break;
        }
        base.Dock = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override string Text
    {
      get
      {
        return this.statusBarElement.Text;
      }
      set
      {
        this.statusBarElement.Text = value;
      }
    }

    [DefaultValue(true)]
    public bool SizingGrip
    {
      get
      {
        return this.statusBarElement.GripStyle == ToolStripGripStyle.Visible;
      }
      set
      {
        this.statusBarElement.GripStyle = value ? ToolStripGripStyle.Visible : ToolStripGripStyle.Hidden;
      }
    }

    [Browsable(true)]
    [RadEditItemsAction]
    [RadNewItem("Type here", true)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.StatusBarElement.Items;
      }
    }

    [AmbientValue(0)]
    [Description("ToolStripLayoutStyle")]
    [DefaultValue(RadStatusBarLayoutStyle.Stack)]
    [Category("Layout")]
    [RefreshProperties(RefreshProperties.All)]
    public RadStatusBarLayoutStyle LayoutStyle
    {
      get
      {
        return this.StatusBarElement.LayoutStyle;
      }
      set
      {
        this.StatusBarElement.LayoutStyle = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadStatusBarElement StatusBarElement
    {
      get
      {
        return this.statusBarElement;
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

    [Description("StatusBarClick")]
    public event RadStatusStrip.RadStatusBarClickEvenHandler StatusBarClick;

    public event ValueChangingEventHandler LayoutStyleChanging;

    public event EventHandler LayoutStyleChanged;

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 132 && this.WmNCHitTest(ref m))
        return;
      base.WndProc(ref m);
    }

    protected virtual bool WmNCHitTest(ref Message msg)
    {
      bool flag = false;
      Form form = this.FindForm();
      if (form != null)
        flag = form.WindowState == FormWindowState.Maximized;
      if (!this.IsInRibbonForm || flag || this.Height - this.PointToClient(new Point((int) msg.LParam)).Y > 4)
        return false;
      msg.Result = new IntPtr(-1);
      return true;
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      this.OnStatusBarClick((object) this, new RadStatusBarClickEventArgs(this.ElementTree.GetElementAtPoint(e.Location), e));
      base.OnMouseClick(e);
    }

    private void OnLayoutStyleChanging(object sender, ValueChangingEventArgs e)
    {
      if (this.LayoutStyleChanging == null)
        return;
      this.LayoutStyleChanging(sender, e);
    }

    private void OnLayoutStyleChanged(object sender, EventArgs e)
    {
      if (this.LayoutStyleChanged == null)
        return;
      this.LayoutStyleChanged(sender, e);
    }

    private void OnStatusBarClick(object sender, RadStatusBarClickEventArgs args)
    {
      if (this.StatusBarClick == null)
        return;
      this.StatusBarClick(sender, args);
    }

    private void statusBarElement_LayoutStyleChanging(object sender, ValueChangingEventArgs e)
    {
      this.OnLayoutStyleChanging(sender, e);
    }

    private void statusBarElement_LayoutStyleChanged(object sender, EventArgs e)
    {
      this.OnLayoutStyleChanged(sender, e);
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
    }

    public bool CanExtend(object extendee)
    {
      RadItem radItem = extendee as RadItem;
      return radItem != null && extendee != this && this.Items.Contains(radItem);
    }

    [RefreshProperties(RefreshProperties.All)]
    public void SetSpring(RadItem control, bool value)
    {
      int num = (int) control.SetValue(StatusBarBoxLayout.SpringProperty, (object) value);
    }

    public bool GetSpring(RadItem control)
    {
      return (bool) control.GetValue(StatusBarBoxLayout.SpringProperty);
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request.Type == IPCMessage.MessageTypes.GetPropertyValue)
      {
        bool flag = request.ControlType == "DropDownButton";
        if (request.Data != null)
          flag = flag | request.Data.ToString().ToLower().Contains("splitbutton") | request.Data.ToString().ToLower().Contains("dropdownbutton");
        if (request.Message == "ItemsCount" && flag)
        {
          if (request.Data != null && !string.IsNullOrEmpty(request.Data.ToString()))
          {
            string str = request.Data.ToString();
            foreach (RadItem radItem in (RadItemCollection) this.Items)
            {
              if (radItem.Name == str && radItem is RadDropDownButtonElement)
              {
                request.Data = (object) (radItem as RadDropDownButtonElement).Items.Count;
                return;
              }
            }
          }
          request.Data = (object) 0;
          return;
        }
        if (request.Message == "HasChildNodes" && flag)
        {
          if (request.Data != null && !string.IsNullOrEmpty(request.Data.ToString()))
          {
            string str = request.Data.ToString();
            foreach (RadItem radItem in (RadItemCollection) this.Items)
            {
              if (radItem.Name == str && radItem is RadDropDownButtonElement)
              {
                request.Data = (object) ((radItem as RadDropDownButtonElement).Items.Count > 0);
                return;
              }
            }
          }
          request.Data = (object) 0;
          return;
        }
        if (request.Message == "ItemsCount")
        {
          request.Data = (object) this.Items.Count;
          return;
        }
      }
      base.ProcessCodedUIMessage(ref request);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.StatusBarElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.StatusBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.StatusBarElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.StatusBarElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "StatusBarFill");
        this.StatusBarElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, state, "StatusBarFill");
      }
      this.StatusBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.StatusBarElement.SuspendApplyOfThemeSettings();
      this.StatusBarElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.StatusBarElement.ResetThemeValueOverride(FillPrimitive.GradientStyleProperty);
      this.StatusBarElement.ElementTree.ApplyThemeToElementTree();
      this.StatusBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.StatusBarElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.StatusBarElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.StatusBarElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.StatusBarElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.StatusBarElement.SuspendApplyOfThemeSettings();
      this.StatusBarElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.StatusBarElement.ElementTree.ApplyThemeToElementTree();
      this.StatusBarElement.ResumeApplyOfThemeSettings();
    }

    public delegate void RadStatusBarClickEvenHandler(
      object sender,
      RadStatusBarClickEventArgs args);
  }
}
