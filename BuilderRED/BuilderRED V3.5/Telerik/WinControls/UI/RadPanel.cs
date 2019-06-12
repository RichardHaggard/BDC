// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [DefaultEvent("Paint")]
  [Description("Enables you to group collections of controls")]
  [DefaultProperty("BorderStyle")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Containers")]
  [ToolboxItem(true)]
  [Docking(DockingBehavior.Ask)]
  public class RadPanel : RadControl
  {
    private bool autoScrollToCurrentControl = true;
    private RadPanelElement panelElement;
    private Point location;

    public RadPanel()
    {
      this.CausesValidation = true;
      this.Focusable = false;
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.ContainerControl, true);
      this.Scroll += new ScrollEventHandler(this.RadPanel_Scroll);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      if (this.panelElement == null)
        this.panelElement = new RadPanelElement();
      parent.Children.Add((RadElement) this.panelElement);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadPanelElement PanelElement
    {
      get
      {
        return this.panelElement;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or set a value indicating whether panel will scroll automatically to show the currently focused control inside it.")]
    public bool AutoScrollToCurrentControl
    {
      get
      {
        return this.autoScrollToCurrentControl;
      }
      set
      {
        this.autoScrollToCurrentControl = value;
      }
    }

    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Description("Gets or sets the alignment of the text within Panel's bounds.")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.panelElement.PanelText.TextAlignment;
      }
      set
      {
        this.panelElement.PanelText.TextAlignment = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(200, 100));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    [DefaultValue(true)]
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

    protected override bool ProcessMnemonic(char charCode)
    {
      if (!Control.IsMnemonic(charCode, this.Text) || !this.Enabled || !this.Visible)
        return false;
      Control parent = this.Parent;
      if (parent != null && parent.SelectNextControl((Control) this, true, false, true, false) && !parent.ContainsFocus)
        parent.Focus();
      return true;
    }

    protected override Point ScrollToControl(Control activeControl)
    {
      if (this.autoScrollToCurrentControl)
        return base.ScrollToControl(activeControl);
      return this.location;
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.PanelElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.PanelElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.PanelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.PanelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "RadPanelFill");
      }
      this.PanelElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.PanelElement.SuspendApplyOfThemeSettings();
      this.PanelElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num = (int) this.PanelElement.PanelFill.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.PanelElement.ElementTree.ApplyThemeToElementTree();
      this.PanelElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.PanelElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.PanelElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.PanelElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.PanelElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state, typeof (TextPrimitive));
      }
      this.PanelElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.PanelElement.SuspendApplyOfThemeSettings();
      this.PanelElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num1 = (int) this.PanelElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      int num2 = (int) this.PanelElement.PanelText.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.PanelElement.ResumeApplyOfThemeSettings();
    }

    private void RadPanel_Scroll(object sender, ScrollEventArgs e)
    {
      if (!this.DesignMode)
      {
        this.location.X = this.DisplayRectangle.X;
        this.location.Y = this.DisplayRectangle.Y;
      }
      this.Invalidate();
    }

    protected override void OnTextChanged(EventArgs e)
    {
      this.panelElement.Text = this.Text;
      base.OnTextChanged(e);
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      if (!this.AutoSize || this.DesignMode)
        return;
      this.CalcSize();
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      if (!this.AutoSize)
        return;
      this.CalcSize();
      e.Control.SizeChanged += new EventHandler(this.Control_SizeChanged);
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      base.OnControlRemoved(e);
      if (!this.AutoSize)
        return;
      this.CalcSize();
      e.Control.SizeChanged -= new EventHandler(this.Control_SizeChanged);
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
      if (value)
      {
        this.CalcSize();
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          control.SizeChanged += new EventHandler(this.Control_SizeChanged);
      }
      else
      {
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          control.SizeChanged -= new EventHandler(this.Control_SizeChanged);
      }
    }

    private void Control_SizeChanged(object sender, EventArgs e)
    {
      this.CalcSize();
    }

    private void CalcSize()
    {
      Size size = this.RootElement.DesiredSize.ToSize();
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        size.Width = Math.Max(size.Width, control.Right);
        size.Height = Math.Max(size.Height, control.Bottom);
      }
      size.Width += this.Padding.Right;
      size.Height += this.Padding.Bottom;
      this.ClientSize = size;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.AutoSize)
      {
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          control.SizeChanged -= new EventHandler(this.Control_SizeChanged);
      }
      base.Dispose(disposing);
    }
  }
}
