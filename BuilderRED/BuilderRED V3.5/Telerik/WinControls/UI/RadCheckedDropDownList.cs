// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckedDropDownList
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [DefaultProperty("Items")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [Description("Displays an a CheckedDropDownList of permitted values")]
  [DefaultEvent("ItemCheckedChanged")]
  [TelerikToolboxCategory("Data Controls")]
  [DefaultBindingProperty("Text")]
  [ComplexBindingProperties("DataSource", "ValueMember")]
  [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
  public class RadCheckedDropDownList : RadDropDownList
  {
    private bool tabStop = true;
    private RadCheckedDropDownListElement dropDownListElement;

    public RadCheckedDropDownList()
    {
      base.DropDownStyle = RadDropDownStyle.DropDownList;
      base.TabStop = true;
    }

    [Browsable(false)]
    [Description("Please, use CheckedItems instead")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Bindable(true)]
    public override RadListDataItem SelectedItem
    {
      get
      {
        return base.SelectedItem;
      }
      set
      {
        base.SelectedItem = value;
      }
    }

    [Bindable(true)]
    [Description("Please, use CheckedItems instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override int SelectedIndex
    {
      get
      {
        return base.SelectedIndex;
      }
      set
      {
        base.SelectedIndex = value;
      }
    }

    [Browsable(false)]
    [Description("Please, use CheckedItems instead")]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override object SelectedValue
    {
      get
      {
        return this.CheckedDropDownListElement.SelectedValue;
      }
      set
      {
        this.CheckedDropDownListElement.SelectedValue = value;
        this.OnNotifyPropertyChanged(nameof (SelectedValue));
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Show or Hide the CheckAll item.")]
    public bool ShowCheckAllItems
    {
      get
      {
        return this.CheckedDropDownListElement.ShowCheckAllItems;
      }
      set
      {
        this.CheckedDropDownListElement.ShowCheckAllItems = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the hosted textbox is multiline.")]
    [Category("Behavior")]
    public bool Multiline
    {
      get
      {
        return this.CheckedDropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.Multiline;
      }
      set
      {
        this.CheckedDropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.Multiline = value;
      }
    }

    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Behavior")]
    [RadDescription("CaseSensitive", typeof (RadDropDownListElement))]
    public override bool CaseSensitive
    {
      get
      {
        return this.dropDownListElement.CaseSensitive;
      }
      set
      {
        this.dropDownListElement.CaseSensitive = value;
      }
    }

    [Description("Specifies the mode for the automatic completion feature used in the CheckDropDownList and TextBox controls.")]
    [DefaultValue(AutoCompleteMode.SuggestAppend)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [Category("Behavior")]
    public override AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.CheckedDropDownListElement.AutoCompleteMode;
      }
      set
      {
        this.CheckedDropDownListElement.AutoCompleteMode = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [RadPropertyDefaultValue("DropDownStyle", typeof (RadDropDownListElement))]
    [Description("Gets or sets a value specifying the style of the combo box. This property is not applicable for RadCheckedDropDownList!")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(false)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override RadDropDownStyle DropDownStyle
    {
      get
      {
        return base.DropDownStyle;
      }
      set
      {
        base.DropDownStyle = value;
      }
    }

    [Category("Behavior")]
    [Localizable(true)]
    [DefaultValue(2147483647)]
    [Description("Gets or sets the maximum number of characters the user can type or paste into the text box control.")]
    public override int MaxLength
    {
      get
      {
        return this.CheckedDropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.MaxLength;
      }
      set
      {
        this.CheckedDropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.MaxLength = value;
      }
    }

    [Browsable(false)]
    public RadCheckedDropDownListElement CheckedDropDownListElement
    {
      get
      {
        return this.dropDownListElement;
      }
      set
      {
        this.dropDownListElement = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the drop down list is read only.")]
    public override bool ReadOnly
    {
      get
      {
        return this.dropDownListElement.ReadOnly;
      }
      set
      {
        this.dropDownListElement.ReadOnly = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.Design.RadCheckedDropDownListCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Data")]
    [Description("Gets a collection representing the items contained in this RadCheckedDropDownList.")]
    public RadCheckedListDataItemCollection Items
    {
      get
      {
        return this.dropDownListElement.Items;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Data")]
    [Description("Gets a collection representing the checked items contained in this RadCheckedDropDownList.")]
    public DropDownCheckedItemsCollection CheckedItems
    {
      get
      {
        return this.dropDownListElement.CheckedItems;
      }
    }

    public override string ThemeClassName
    {
      get
      {
        return typeof (RadDropDownList).FullName;
      }
    }

    [Category("Data")]
    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string CheckedMember
    {
      get
      {
        return this.ListElement.CheckedMember;
      }
      set
      {
        this.ListElement.CheckedMember = value;
      }
    }

    [Browsable(true)]
    public new bool TabStop
    {
      get
      {
        return this.tabStop;
      }
      set
      {
        this.tabStop = value;
      }
    }

    [Description("Gets or sets a value indicating whether items checked state is synchronized with the text in the editable area.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool SyncSelectionWithText
    {
      get
      {
        return this.dropDownListElement.SyncSelectionWithText;
      }
      set
      {
        this.dropDownListElement.SyncSelectionWithText = value;
      }
    }

    protected override void OnEnter(EventArgs e)
    {
      if (this.entering)
        return;
      this.entering = true;
      base.OnEnter(e);
      this.DropDownListElement.EditableElement.Entering = true;
      if (this.DropDownStyle != RadDropDownStyle.DropDown)
        return;
      this.OnGotFocus(e);
    }

    protected RadAutoCompleteBoxElement AutoCompleteTextBoxElement
    {
      get
      {
        return (RadAutoCompleteBoxElement) this.CheckedDropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox;
      }
    }

    protected override RadDropDownListElement CreateDropDownListElement()
    {
      this.dropDownListElement = new RadCheckedDropDownListElement();
      return (RadDropDownListElement) this.dropDownListElement;
    }

    public override void BeginInit()
    {
      base.BeginInit();
      this.dropDownListElement.BeginUpdate();
    }

    public override void EndInit()
    {
      base.EndInit();
      this.dropDownListElement.EndUpdate();
      this.dropDownListElement.SyncEditorElementWithSelectedItem();
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.CheckedDropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.Focus();
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.dropDownListElement.SuspendApplyOfThemeSettings();
      this.dropDownListElement.AutoCompleteEditableAreaElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.DropDownListElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.dropDownListElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.dropDownListElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "DropDownFill");
        this.dropDownListElement.AutoCompleteEditableAreaElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.dropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
      }
      this.dropDownListElement.ResumeApplyOfThemeSettings();
      this.dropDownListElement.AutoCompleteEditableAreaElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.dropDownListElement.SuspendApplyOfThemeSettings();
      this.dropDownListElement.AutoCompleteEditableAreaElement.SuspendApplyOfThemeSettings();
      this.dropDownListElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.dropDownListElement.AutoCompleteEditableAreaElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num = (int) this.dropDownListElement.AutoCompleteEditableAreaElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.dropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.dropDownListElement.ElementTree.ApplyThemeToElementTree();
      this.dropDownListElement.ResumeApplyOfThemeSettings();
      this.dropDownListElement.AutoCompleteEditableAreaElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.dropDownListElement.SuspendApplyOfThemeSettings();
      this.dropDownListElement.AutoCompleteEditableAreaElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.DropDownListElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.dropDownListElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.dropDownListElement.AutoCompleteEditableAreaElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.dropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      }
      this.dropDownListElement.ResumeApplyOfThemeSettings();
      this.dropDownListElement.AutoCompleteEditableAreaElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.dropDownListElement.SuspendApplyOfThemeSettings();
      this.dropDownListElement.AutoCompleteEditableAreaElement.SuspendApplyOfThemeSettings();
      this.dropDownListElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.dropDownListElement.AutoCompleteEditableAreaElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num = (int) this.dropDownListElement.AutoCompleteEditableAreaElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.dropDownListElement.AutoCompleteEditableAreaElement.AutoCompleteTextBox.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.dropDownListElement.ElementTree.ApplyThemeToElementTree();
      this.dropDownListElement.ResumeApplyOfThemeSettings();
      this.dropDownListElement.AutoCompleteEditableAreaElement.ResumeApplyOfThemeSettings();
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

    public event TextBlockFormattingEventHandler TextBlockFormatting
    {
      add
      {
        this.AutoCompleteTextBoxElement.TextBlockFormatting += value;
      }
      remove
      {
        this.AutoCompleteTextBoxElement.TextBlockFormatting -= value;
      }
    }

    public event CreateTextBlockEventHandler CreateTextBlock
    {
      add
      {
        this.AutoCompleteTextBoxElement.CreateTextBlock += value;
      }
      remove
      {
        this.AutoCompleteTextBoxElement.CreateTextBlock -= value;
      }
    }

    public event RadCheckedListDataItemCancelEventHandler ItemCheckedChanging
    {
      add
      {
        this.dropDownListElement.ItemCheckedChanging += value;
      }
      remove
      {
        this.dropDownListElement.ItemCheckedChanging -= value;
      }
    }

    public event RadCheckedListDataItemCancelEventHandler CheckAllItemCheckedChanging
    {
      add
      {
        this.dropDownListElement.CheckAllItemCheckedChanging += value;
      }
      remove
      {
        this.dropDownListElement.CheckAllItemCheckedChanging -= value;
      }
    }

    public event RadCheckedListDataItemEventHandler CheckAllItemCheckedChanged
    {
      add
      {
        this.dropDownListElement.CheckAllItemCheckedChanged += value;
      }
      remove
      {
        this.dropDownListElement.CheckAllItemCheckedChanged -= value;
      }
    }

    public event RadCheckedListDataItemEventHandler ItemCheckedChanged
    {
      add
      {
        this.dropDownListElement.ItemCheckedChanged += value;
      }
      remove
      {
        this.dropDownListElement.ItemCheckedChanged -= value;
      }
    }
  }
}
