// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadBrowseEditor
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
  [DefaultProperty("Value")]
  [ToolboxItem(true)]
  [Description("Displays a file path value that the user can edit or can open a OpenFileDialog and select a file from there.")]
  [DefaultBindingProperty("Value")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Editors")]
  [DefaultEvent("ValueChanged")]
  public class RadBrowseEditor : RadEditorControl
  {
    private RadBrowseEditorElement browseElement;

    public RadBrowseEditor()
    {
      this.AutoSize = true;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      this.UnwireEvents();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.browseElement = this.CreateEditorElement();
      parent.Children.Add((RadElement) this.browseElement);
      this.WireEvents();
    }

    protected virtual RadBrowseEditorElement CreateEditorElement()
    {
      RadBrowseEditorElement browseEditorElement = new RadBrowseEditorElement();
      int num = (int) browseEditorElement.SetDefaultValueOverride(RadElement.StretchVerticallyProperty, (object) true);
      return browseEditorElement;
    }

    protected virtual void WireEvents()
    {
      this.browseElement.KeyPress += new KeyPressEventHandler(this.browseElement_KeyPress);
      this.browseElement.TextChanged += new EventHandler(this.browseElement_TextChanged);
      this.browseElement.KeyDown += new KeyEventHandler(this.browseElement_KeyDown);
      this.browseElement.KeyUp += new KeyEventHandler(this.browseElement_KeyUp);
      this.browseElement.TextBoxItem.PreviewKeyDown += new PreviewKeyDownEventHandler(this.TextBoxItem_PreviewKeyDown);
    }

    protected virtual void UnwireEvents()
    {
      this.browseElement.TextChanged -= new EventHandler(this.browseElement_TextChanged);
      this.browseElement.KeyDown -= new KeyEventHandler(this.browseElement_KeyDown);
      this.browseElement.KeyPress -= new KeyPressEventHandler(this.browseElement_KeyPress);
      this.browseElement.KeyUp -= new KeyEventHandler(this.browseElement_KeyUp);
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

    [Description("Gets the RadBrowseEditorElement of this control.")]
    [Category("Appearance")]
    [Browsable(false)]
    public RadBrowseEditorElement BrowseElement
    {
      get
      {
        return this.browseElement;
      }
    }

    [Category("Appearance")]
    [Browsable(false)]
    [Description("Gets the OpenFileDialog of this control.")]
    public CommonDialog Dialog
    {
      get
      {
        return this.browseElement.Dialog;
      }
    }

    [DefaultValue(BrowseEditorDialogType.OpenFileDialog)]
    [Category("Appearance")]
    [Description("Gets or sets the type of dialog to be opened when the browse button is pressed.")]
    [Browsable(true)]
    public virtual BrowseEditorDialogType DialogType
    {
      get
      {
        return this.browseElement.DialogType;
      }
      set
      {
        this.browseElement.DialogType = value;
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    [Category("Data")]
    [Description("Gets or sets the value of the editor.")]
    public string Value
    {
      get
      {
        return this.browseElement.Value;
      }
      set
      {
        this.browseElement.Value = value;
      }
    }

    [Browsable(true)]
    [Description("Determines if users can input text directly into the text field.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ReadOnly
    {
      get
      {
        return this.browseElement.ReadOnly;
      }
      set
      {
        this.browseElement.ReadOnly = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    [Category("Action")]
    [Description("Fires after the dialog window is closed.")]
    public event DialogClosedEventHandler DialogClosed
    {
      add
      {
        this.browseElement.DialogClosed += value;
      }
      remove
      {
        this.browseElement.DialogClosed -= value;
      }
    }

    [Category("Action")]
    [Description("Fires right before the value is changed.")]
    public event ValueChangingEventHandler ValueChanging
    {
      add
      {
        this.browseElement.ValueChanging += value;
      }
      remove
      {
        this.browseElement.ValueChanging -= value;
      }
    }

    [Description("Fires after the editor value is changed.")]
    [Category("Action")]
    public event EventHandler ValueChanged
    {
      add
      {
        this.browseElement.ValueChanged += value;
      }
      remove
      {
        this.browseElement.ValueChanged -= value;
      }
    }

    [Category("Action")]
    [Description("Fires when the ReadOnly property value is changed.")]
    public event EventHandler ReadOnlyChanged
    {
      add
      {
        this.browseElement.ReadOnlyChanged += value;
      }
      remove
      {
        this.browseElement.ReadOnlyChanged -= value;
      }
    }

    private void TextBoxItem_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
      this.CallOnPreviewKeyDown(e);
    }

    private void browseElement_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    private void browseElement_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.CallBaseOnKeyPress(e);
    }

    private void browseElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.CallBaseOnKeyDown(e);
    }

    private void browseElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.CallBaseOnKeyUp(e);
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

    protected override void SetBackColorThemeOverrides()
    {
      this.BrowseElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.BrowseElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.BrowseElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.BrowseElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TextBoxFill");
      }
      this.BrowseElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.BrowseElement.SuspendApplyOfThemeSettings();
      this.BrowseElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.BrowseElement.ElementTree.ApplyThemeToElementTree();
      this.BrowseElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.BrowseElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.BrowseElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.BrowseElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.BrowseElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.BrowseElement.SuspendApplyOfThemeSettings();
      this.BrowseElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.BrowseElement.ElementTree.ApplyThemeToElementTree();
      this.BrowseElement.ResumeApplyOfThemeSettings();
    }
  }
}
