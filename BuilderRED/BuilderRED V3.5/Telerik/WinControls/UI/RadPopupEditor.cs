// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPopupEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Editors")]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadPopupEditorDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  public class RadPopupEditor : RadEditorControl
  {
    private RadPopupEditorElement popupEditorElement;
    private RadPopupContainer associatedControl;

    public RadPopupEditor()
    {
      this.AutoSize = true;
      base.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, true);
      this.WireHostEvents();
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireHostEvents();
      base.Dispose(disposing);
    }

    protected internal virtual void WireHostEvents()
    {
      this.TextBoxElement.TextBoxItem.TextChanged += new EventHandler(this.OnTextBoxItemTextChanged);
    }

    protected internal virtual void UnwireHostEvents()
    {
      this.TextBoxElement.TextBoxItem.TextChanged -= new EventHandler(this.OnTextBoxItemTextChanged);
    }

    protected virtual RadPopupEditorElement CreateElement()
    {
      return new RadPopupEditorElement();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.popupEditorElement = this.CreateElement();
      parent.Children.Add((RadElement) this.popupEditorElement);
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      if (this.Site != null)
        return;
      this.SetAssociatedControlRuntime(this.associatedControl);
    }

    public virtual void SetAssociatedControlRuntime(RadPopupContainer associatedControl)
    {
      if (associatedControl != null)
      {
        this.associatedControl = associatedControl;
        this.popupEditorElement.PopupForm.Controls.Add((Control) associatedControl);
        this.popupEditorElement.PopupContainerForm.Panel = associatedControl.PanelContainer;
      }
      else
      {
        if (this.associatedControl != null && this.popupEditorElement.PopupForm.Controls.Contains((Control) this.associatedControl))
          this.popupEditorElement.PopupForm.Controls.Remove((Control) this.associatedControl);
        this.popupEditorElement.PopupContainerForm.Panel = (Panel) null;
        this.associatedControl = (RadPopupContainer) null;
      }
    }

    [Category("Appearance")]
    [DefaultValue(SizingMode.UpDownAndRightBottom)]
    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, veritcal or a combination of them.")]
    [Browsable(true)]
    public SizingMode DropDownSizingMode
    {
      get
      {
        return this.popupEditorElement.DropDownSizingMode;
      }
      set
      {
        this.popupEditorElement.DropDownSizingMode = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool TabStop
    {
      get
      {
        if (this.DropDownStyle == RadDropDownStyle.DropDownList)
          return this.popupEditorElement.TextBoxElement.TextBoxItem.TextBoxControl.TabStop;
        return base.TabStop;
      }
      set
      {
        if (this.DropDownStyle == RadDropDownStyle.DropDown)
        {
          base.TabStop = false;
          this.popupEditorElement.TextBoxElement.TextBoxItem.TextBoxControl.TabStop = value;
        }
        else
          base.TabStop = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Layout")]
    [DefaultValue(true)]
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

    [Category("Behavior")]
    [DefaultValue("")]
    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Localizable(true)]
    [Browsable(true)]
    [Description("Gets or sets the text associated with this control.")]
    [Bindable(true)]
    [SettingsBindable(true)]
    public override string Text
    {
      get
      {
        return this.popupEditorElement.Text;
      }
      set
      {
        this.popupEditorElement.Text = value;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.RadPopupEditorAssociatedControl, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DefaultValue(null)]
    public RadPopupContainer AssociatedControl
    {
      get
      {
        return this.associatedControl;
      }
      set
      {
        this.associatedControl = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadPopupEditorElement PopupEditorElement
    {
      get
      {
        return this.popupEditorElement;
      }
      set
      {
        this.popupEditorElement = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadDropDownListEditableAreaElement EditableAreaElement
    {
      get
      {
        return this.popupEditorElement.ContainerElement;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadPopupControlBase PopupForm
    {
      get
      {
        return this.popupEditorElement.PopupForm;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadPopupControlBase Popup
    {
      get
      {
        return (RadPopupControlBase) this.popupEditorElement.PopupContainerForm;
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets a value specifying the style of the RadDropDownList.")]
    [Category("Appearance")]
    [DefaultValue(RadDropDownStyle.DropDownList)]
    public virtual RadDropDownStyle DropDownStyle
    {
      get
      {
        return this.popupEditorElement.DropDownStyle;
      }
      set
      {
        this.popupEditorElement.DropDownStyle = value;
        base.TabStop = value != RadDropDownStyle.DropDown;
        this.popupEditorElement.TextBoxElement.TextBoxItem.TabStop = base.TabStop;
      }
    }

    [Description("Represents the TextBox that is hosted inside.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual RadTextBoxElement TextBoxElement
    {
      get
      {
        return this.popupEditorElement.TextBoxElement;
      }
    }

    [Description("Set or get the TextBox visibility")]
    [DefaultValue(true)]
    public virtual bool ShowTextBox
    {
      get
      {
        return this.popupEditorElement.TextBoxVisibility == ElementVisibility.Visible;
      }
      set
      {
        this.popupEditorElement.TextBoxVisibility = value ? ElementVisibility.Visible : ElementVisibility.Hidden;
      }
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadDropDownList).FullName;
      }
    }

    [Description("Gets or sets the drop down maximal size.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Size), "0,0")]
    public Size DropDownMaxSize
    {
      get
      {
        return this.popupEditorElement.DropDownMaxSize;
      }
      set
      {
        this.popupEditorElement.DropDownMaxSize = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the drop down minimal size.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Size), "0,0")]
    public Size DropDownMinSize
    {
      get
      {
        return this.popupEditorElement.DropDownMinSize;
      }
      set
      {
        this.popupEditorElement.DropDownMinSize = value;
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if ((keyData | Keys.Back) != Keys.Back && (keyData | Keys.Tab) == Keys.Tab && this.popupEditorElement.IsPopupOpen)
        this.Popup.ClosePopup(RadPopupCloseReason.Keyboard);
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(125, 20));
      }
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
      this.RootElement.StretchHorizontally = true;
      this.RootElement.StretchVertically = !value;
      this.RootElement.SaveCurrentStretchModeAsDefault();
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      base.OnBindingContextChanged(e);
      this.popupEditorElement.PopupForm.BindingContext = this.BindingContext;
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.PopupEditorElement.SuspendApplyOfThemeSettings();
      this.PopupEditorElement.ContainerElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.PopupEditorElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.PopupEditorElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.PopupEditorElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "DropDownFill");
        this.PopupEditorElement.ContainerElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
      }
      this.PopupEditorElement.ResumeApplyOfThemeSettings();
      this.PopupEditorElement.ContainerElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.PopupEditorElement.SuspendApplyOfThemeSettings();
      this.PopupEditorElement.ContainerElement.SuspendApplyOfThemeSettings();
      this.PopupEditorElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.PopupEditorElement.ContainerElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num = (int) this.PopupEditorElement.ContainerElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.PopupEditorElement.ElementTree.ApplyThemeToElementTree();
      this.PopupEditorElement.ResumeApplyOfThemeSettings();
      this.PopupEditorElement.ContainerElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.PopupEditorElement.SuspendApplyOfThemeSettings();
      this.PopupEditorElement.ContainerElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.PopupEditorElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.PopupEditorElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.PopupEditorElement.ContainerElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      }
      this.PopupEditorElement.ResumeApplyOfThemeSettings();
      this.PopupEditorElement.ContainerElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.PopupEditorElement.SuspendApplyOfThemeSettings();
      this.PopupEditorElement.ContainerElement.SuspendApplyOfThemeSettings();
      this.PopupEditorElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.PopupEditorElement.ContainerElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num = (int) this.PopupEditorElement.ContainerElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.PopupEditorElement.ElementTree.ApplyThemeToElementTree();
      this.PopupEditorElement.ResumeApplyOfThemeSettings();
      this.PopupEditorElement.ContainerElement.ResumeApplyOfThemeSettings();
    }

    private void OnTextBoxItemTextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    public event EventHandler PopupOpened
    {
      add
      {
        this.popupEditorElement.PopupOpened += value;
      }
      remove
      {
        this.popupEditorElement.PopupOpened -= value;
      }
    }

    public event CancelEventHandler PopupOpening
    {
      add
      {
        this.popupEditorElement.PopupOpening += value;
      }
      remove
      {
        this.popupEditorElement.PopupOpening -= value;
      }
    }

    public event RadPopupClosingEventHandler PopupClosing
    {
      add
      {
        this.popupEditorElement.PopupClosing += value;
      }
      remove
      {
        this.popupEditorElement.PopupClosing -= value;
      }
    }

    public event RadPopupClosedEventHandler PopupClosed
    {
      add
      {
        this.popupEditorElement.PopupClosed += value;
      }
      remove
      {
        this.popupEditorElement.PopupClosed -= value;
      }
    }
  }
}
