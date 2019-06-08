// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadColorBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Editors")]
  [Description("Displays a color value that the user can edit or opens a RadColorDialog to select a color from there.")]
  [DefaultProperty("Value")]
  [DefaultEvent("ValueChanged")]
  [DefaultBindingProperty("Value")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  public class RadColorBox : RadEditorControl
  {
    private RadColorBoxElement colorBoxElement;

    public RadColorBox()
    {
      this.AutoSize = true;
      this.WireEvents();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.colorBoxElement = this.CreateColorBoxElement();
      parent.Children.Add((RadElement) this.colorBoxElement);
    }

    protected virtual RadColorBoxElement CreateColorBoxElement()
    {
      return new RadColorBoxElement();
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.UnwireEvents();
    }

    protected virtual void WireEvents()
    {
      this.colorBoxElement.TextChanged += new EventHandler(this.ColorBoxElement_TextChanged);
      this.colorBoxElement.KeyDown += new KeyEventHandler(this.colorBoxElement_KeyDown);
      this.colorBoxElement.KeyPress += new KeyPressEventHandler(this.colorBoxElement_KeyPress);
      this.colorBoxElement.KeyUp += new KeyEventHandler(this.colorBoxElement_KeyUp);
      this.colorBoxElement.TextBoxItem.PreviewKeyDown += new PreviewKeyDownEventHandler(this.TextBoxItem_PreviewKeyDown);
    }

    protected virtual void UnwireEvents()
    {
      this.colorBoxElement.TextChanged -= new EventHandler(this.ColorBoxElement_TextChanged);
      this.colorBoxElement.KeyDown -= new KeyEventHandler(this.colorBoxElement_KeyDown);
      this.colorBoxElement.KeyPress -= new KeyPressEventHandler(this.colorBoxElement_KeyPress);
      this.colorBoxElement.KeyUp -= new KeyEventHandler(this.colorBoxElement_KeyUp);
      this.colorBoxElement.TextBoxItem.PreviewKeyDown -= new PreviewKeyDownEventHandler(this.TextBoxItem_PreviewKeyDown);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Layout")]
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
        return RadControl.GetDpiScaledSize(new Size(100, 20));
      }
    }

    [Category("Appearance")]
    [Description("Gets the RadColorDialog of this control.")]
    [Browsable(false)]
    public RadColorDialog ColorDialog
    {
      get
      {
        return this.colorBoxElement.ColorDialog;
      }
    }

    [Category("Appearance")]
    [Description("Gets the RadColorBoxElement of this control.")]
    [Browsable(false)]
    public RadColorBoxElement ColorBoxElement
    {
      get
      {
        return this.colorBoxElement;
      }
    }

    [Description("Gets or sets the value of the editor.")]
    [Category("Data")]
    [DefaultValue(typeof (Color), "")]
    [Browsable(true)]
    public Color Value
    {
      get
      {
        return this.colorBoxElement.Value;
      }
      set
      {
        this.colorBoxElement.Value = value;
      }
    }

    [Description("Determines if users can input text directly into the text field.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Browsable(true)]
    public bool ReadOnly
    {
      get
      {
        return this.colorBoxElement.ReadOnly;
      }
      set
      {
        this.colorBoxElement.ReadOnly = value;
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

    [Description("Fires after the color dialog is closed.")]
    [Category("Action")]
    public event DialogClosedEventHandler DialogClosed
    {
      add
      {
        this.colorBoxElement.DialogClosed += value;
      }
      remove
      {
        this.colorBoxElement.DialogClosed -= value;
      }
    }

    [Description("Fires right before the value is changed.")]
    [Category("Action")]
    public event ValueChangingEventHandler ValueChanging
    {
      add
      {
        this.colorBoxElement.ValueChanging += value;
      }
      remove
      {
        this.colorBoxElement.ValueChanging -= value;
      }
    }

    [Description("Fires after the editor value is changed.")]
    [Category("Action")]
    public event EventHandler ValueChanged
    {
      add
      {
        this.colorBoxElement.ValueChanged += value;
      }
      remove
      {
        this.colorBoxElement.ValueChanged -= value;
      }
    }

    private void TextBoxItem_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      this.CallOnPreviewKeyDown(e);
    }

    private void colorBoxElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.CallBaseOnKeyUp(e);
    }

    private void colorBoxElement_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.CallBaseOnKeyPress(e);
    }

    private void colorBoxElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.CallBaseOnKeyDown(e);
    }

    private void ColorBoxElement_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
      if (value)
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = false;
      }
      else
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = true;
      }
      this.RootElement.SaveCurrentStretchModeAsDefault();
    }

    protected override bool IsInputKey(Keys keyData)
    {
      if (keyData == Keys.Return || keyData == Keys.Escape)
        return true;
      return base.IsInputKey(keyData);
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (this.colorBoxElement.TextBoxItem.HostedControl.Focused && (keyData == Keys.Return || keyData == Keys.Escape))
        this.colorBoxElement.CallRaiseKeyDown(new KeyEventArgs(keyData));
      return base.ProcessDialogKey(keyData);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.ColorBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ColorBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.ColorBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.ColorBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TextBoxFill");
        this.ColorBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "ColorBoxLayout");
      }
      this.ColorBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.ColorBoxElement.SuspendApplyOfThemeSettings();
      this.ColorBoxElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.ColorBoxElement.ElementTree.ApplyThemeToElementTree();
      int num = (int) this.ColorBoxElement.Children[2].ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.ColorBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.ColorBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.ColorBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.ColorBoxElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.ColorBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.ColorBoxElement.SuspendApplyOfThemeSettings();
      this.ColorBoxElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.ColorBoxElement.ElementTree.ApplyThemeToElementTree();
      this.ColorBoxElement.ResumeApplyOfThemeSettings();
    }
  }
}
