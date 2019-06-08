// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadAutoCompleteBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Editors")]
  [DefaultProperty("Text")]
  [DefaultEvent("TextChanged")]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  [ComVisible(true)]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadAutoCompleteBoxControlDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("Enables the user to enter text, and provides tokenized text representation")]
  public class RadAutoCompleteBox : RadTextBoxControl
  {
    protected override RadTextBoxControlElement CreateTextBoxElement()
    {
      return (RadTextBoxControlElement) new RadAutoCompleteBoxElement();
    }

    protected RadAutoCompleteBoxElement AutoCompleteTextBoxElement
    {
      get
      {
        return this.TextBoxElement as RadAutoCompleteBoxElement;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool UseSystemPasswordChar
    {
      get
      {
        return base.UseSystemPasswordChar;
      }
      set
      {
        base.UseSystemPasswordChar = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override char PasswordChar
    {
      get
      {
        return base.PasswordChar;
      }
      set
      {
        base.PasswordChar = value;
      }
    }

    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(';')]
    [Description("Gets or sets the delimiter used to tokenize the text.")]
    public char Delimiter
    {
      get
      {
        return this.AutoCompleteTextBoxElement.Delimiter;
      }
      set
      {
        this.AutoCompleteTextBoxElement.Delimiter = value;
      }
    }

    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the remove button of tokenized text blocks should appear.")]
    public bool ShowRemoveButton
    {
      get
      {
        return this.AutoCompleteTextBoxElement.ShowRemoveButton;
      }
      set
      {
        this.AutoCompleteTextBoxElement.ShowRemoveButton = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTokenizedTextItemCollection Items
    {
      get
      {
        return this.AutoCompleteTextBoxElement.Items;
      }
    }

    [DefaultValue(AutoCompleteMode.Suggest)]
    public override AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return base.AutoCompleteMode;
      }
      set
      {
        base.AutoCompleteMode = value;
      }
    }

    [Category("Data")]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [DefaultValue("")]
    [Browsable(true)]
    [Editor("Telerik.WinControls.UI.Design.AutoCompleteDataMemberFieldEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets or sets a property name which will be used to extract a value from the data items.")]
    public string AutoCompleteValueMember
    {
      get
      {
        return this.AutoCompleteTextBoxElement.AutoCompleteValueMember;
      }
      set
      {
        this.AutoCompleteTextBoxElement.AutoCompleteValueMember = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(164, 26));
      }
    }

    public event TokenValidatingEventHandler TokenValidating
    {
      add
      {
        this.AutoCompleteTextBoxElement.TokenValidating += value;
      }
      remove
      {
        this.AutoCompleteTextBoxElement.TokenValidating -= value;
      }
    }

    protected override bool CanEditElementAtDesignTime(RadElement element)
    {
      return !(element is TextBlockElement);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.AutoCompleteTextBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.AutoCompleteTextBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.AutoCompleteTextBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
      this.AutoCompleteTextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.AutoCompleteTextBoxElement.SuspendApplyOfThemeSettings();
      this.AutoCompleteTextBoxElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.AutoCompleteTextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.AutoCompleteTextBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.AutoCompleteTextBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.AutoCompleteTextBoxElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.AutoCompleteTextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.AutoCompleteTextBoxElement.SuspendApplyOfThemeSettings();
      this.AutoCompleteTextBoxElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.AutoCompleteTextBoxElement.ElementTree.ApplyThemeToElementTree();
      this.AutoCompleteTextBoxElement.ResumeApplyOfThemeSettings();
    }
  }
}
