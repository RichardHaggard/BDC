// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Designer("Telerik.WinControls.UI.Design.RadTextBoxDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("Enables the user to enter text, and provides multiline editing and password character masking")]
  [DefaultEvent("TextChanged")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Editors")]
  [DefaultProperty("Text")]
  [ToolboxItem(true)]
  public class RadTextBox : RadTextBoxBase
  {
    private RadTextBoxElement textBoxElement;
    private RadTextBoxItem textBoxItem;

    public RadTextBox()
    {
      this.textBoxItem = this.TextBoxElement.TextBoxItem;
      this.WireHostEvents();
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle |= 33554432;
        return createParams;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.InitializeTextElement();
      this.RootElement.Children.Add((RadElement) this.textBoxElement);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadTextBoxAccessibleObject(this);
    }

    protected internal override void InitializeTextElement()
    {
      this.textBoxElement = this.CreateTextBoxElement();
      this.textBoxElement.StretchVertically = true;
      this.textBoxElement.ShowBorder = true;
      this.textBoxElement.ShowClearButton = false;
    }

    protected virtual RadTextBoxElement CreateTextBoxElement()
    {
      return new RadTextBoxElement();
    }

    protected internal override void WireHostEvents()
    {
      base.WireHostEvents();
      this.textBoxItem.Click += new EventHandler(this.textBoxItem_Click);
      this.textBoxItem.KeyDown += new KeyEventHandler(this.textBoxItem_KeyDown);
      this.textBoxItem.KeyUp += new KeyEventHandler(this.textBoxItem_KeyUp);
      this.textBoxItem.KeyPress += new KeyPressEventHandler(this.textBoxItem_KeyPress);
      this.textBoxItem.PreviewKeyDown += new PreviewKeyDownEventHandler(this.textBoxItem_PreviewKeyDown);
    }

    protected internal override void UnwireHostEvents()
    {
      base.UnwireHostEvents();
      this.textBoxItem.Click -= new EventHandler(this.textBoxItem_Click);
      this.textBoxItem.KeyDown -= new KeyEventHandler(this.textBoxItem_KeyDown);
      this.textBoxItem.KeyUp -= new KeyEventHandler(this.textBoxItem_KeyUp);
      this.textBoxItem.KeyPress -= new KeyPressEventHandler(this.textBoxItem_KeyPress);
      this.textBoxItem.PreviewKeyDown -= new PreviewKeyDownEventHandler(this.textBoxItem_PreviewKeyDown);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal override RadTextBoxItem TextBoxItem
    {
      get
      {
        return this.textBoxItem;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTextBoxElement TextBoxElement
    {
      get
      {
        return this.textBoxElement;
      }
    }

    [Category("Appearance")]
    [RadDescription("UseGenericBorderPaint", typeof (RadTextBoxElement))]
    [RadDefaultValue("UseGenericBorderPaint", typeof (RadTextBoxElement))]
    public bool UseGenericBorderPaint
    {
      get
      {
        return this.TextBoxElement.UseGenericBorderPaint;
      }
      set
      {
        this.TextBoxElement.UseGenericBorderPaint = value;
      }
    }

    [Description("Gets or sets a value indicating whether the text should appear as the default password character.")]
    [DefaultValue(false)]
    public bool UseSystemPasswordChar
    {
      get
      {
        return ((System.Windows.Forms.TextBox) this.TextBoxElement.TextBoxItem.HostedControl).UseSystemPasswordChar;
      }
      set
      {
        ((System.Windows.Forms.TextBox) this.TextBoxElement.TextBoxItem.HostedControl).UseSystemPasswordChar = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the clear button is shown.")]
    [Category("Behavior")]
    public bool ShowClearButton
    {
      get
      {
        return this.TextBoxElement.ShowClearButton;
      }
      set
      {
        this.TextBoxElement.ShowClearButton = value;
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
        this.TextBoxElement.TextBoxItem.HostedControl.CausesValidation = this.CausesValidation;
      }
    }

    private void textBoxItem_KeyDown(object sender, KeyEventArgs e)
    {
      this.Behavior.Enable = false;
      this.OnKeyDown(e);
      this.Behavior.Enable = true;
    }

    private void textBoxItem_KeyUp(object sender, KeyEventArgs e)
    {
      this.Behavior.Enable = false;
      this.OnKeyUp(e);
      this.Behavior.Enable = true;
    }

    private void textBoxItem_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.Behavior.Enable = false;
      this.OnKeyPress(e);
      this.Behavior.Enable = true;
    }

    private void textBoxItem_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      this.Behavior.Enable = false;
      this.OnPreviewKeyDown(e);
      this.Behavior.Enable = true;
    }

    private void textBoxItem_Click(object sender, EventArgs e)
    {
      this.Behavior.Enable = false;
      this.OnClick(e);
      this.Behavior.Enable = true;
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (element == this.textBoxElement)
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.TextBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TextBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.TextBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.TextBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TextBoxFill");
      }
      this.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.TextBoxElement.SuspendApplyOfThemeSettings();
      this.TextBoxElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.TextBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.TextBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.TextBoxElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.TextBoxElement.SuspendApplyOfThemeSettings();
      this.TextBoxElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.TextBoxElement.ElementTree.ApplyThemeToElementTree();
      this.TextBoxElement.ResumeApplyOfThemeSettings();
    }
  }
}
