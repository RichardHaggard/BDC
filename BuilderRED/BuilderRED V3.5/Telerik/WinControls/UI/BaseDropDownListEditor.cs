// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseDropDownListEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(false)]
  public class BaseDropDownListEditor : BaseInputEditor
  {
    protected int selectionStart;
    protected bool cancelValueChanging;

    public override object Value
    {
      get
      {
        RadDropDownListElement editorElement = this.EditorElement as RadDropDownListElement;
        if (!string.IsNullOrEmpty(editorElement.ValueMember))
          return editorElement.SelectedValue;
        if (editorElement.SelectedItem != null)
          return (object) editorElement.SelectedItem.Text;
        return (object) null;
      }
      set
      {
        RadDropDownListElement editorElement = this.EditorElement as RadDropDownListElement;
        if (value == null)
          editorElement.SelectedItem = (RadListDataItem) null;
        else if (editorElement.ValueMember == null)
          editorElement.SelectedItem = editorElement.ListElement.Items[editorElement.FindStringExact(value.ToString())];
        else
          editorElement.SelectedValue = value;
      }
    }

    public RadDropDownStyle DropDownStyle
    {
      get
      {
        return (this.EditorElement as RadDropDownListElement).DropDownStyle;
      }
      set
      {
        (this.EditorElement as RadDropDownListElement).DropDownStyle = value;
      }
    }

    public SizingMode DropDownSizingMode
    {
      get
      {
        return (this.EditorElement as RadDropDownListElement).DropDownSizingMode;
      }
      set
      {
        (this.EditorElement as RadDropDownListElement).DropDownSizingMode = value;
      }
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (string);
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      BaseDropDownListEditorElement editorElement = (BaseDropDownListEditorElement) this.EditorElement;
      editorElement.TextBox.TextBoxItem.TextChanged += new EventHandler(this.TextBoxItem_TextChanged);
      editorElement.TextBox.TextBoxItem.HostedControl.LostFocus += new EventHandler(this.HostedControl_LostFocus);
      editorElement.RadPropertyChanged += new RadPropertyChangedEventHandler(this.DropDownListElement_RadPropertyChanged);
      editorElement.SelectedIndexChanging += new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.DropDownListElement_SelectedIndexChanging);
      editorElement.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.DropDownListElement_SelectedIndexChanged);
      editorElement.HandleKeyDown += new KeyEventHandler(this.DropDownListElement_HandleKeyDown);
      editorElement.SelectionStart = 0;
      editorElement.SelectionLength = editorElement.Text.Length;
      editorElement.Focus();
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      BaseDropDownListEditorElement editorElement = (BaseDropDownListEditorElement) this.EditorElement;
      if (editorElement.IsPopupOpen)
        editorElement.ClosePopup();
      editorElement.SelectedIndexChanging -= new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.DropDownListElement_SelectedIndexChanging);
      editorElement.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.DropDownListElement_SelectedIndexChanged);
      editorElement.HandleKeyDown -= new KeyEventHandler(this.DropDownListElement_HandleKeyDown);
      editorElement.TextBox.TextBoxItem.TextChanged -= new EventHandler(this.TextBoxItem_TextChanged);
      editorElement.TextBox.TextBoxItem.HostedControl.LostFocus -= new EventHandler(this.HostedControl_LostFocus);
      editorElement.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.DropDownListElement_RadPropertyChanged);
      editorElement.DataSource = (object) null;
      editorElement.Items.Clear();
      editorElement.ListElement.BindingContext = (BindingContext) null;
      return true;
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
    }

    protected virtual void OnLostFocus()
    {
    }

    private void DropDownListElement_HandleKeyDown(object sender, KeyEventArgs e)
    {
      BaseDropDownListEditorElement editorElement = this.EditorElement as BaseDropDownListEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      this.OnKeyDown(e);
    }

    private void DropDownListElement_SelectedIndexChanging(
      object sender,
      PositionChangingCancelEventArgs e)
    {
      if (e.Position < 0)
        return;
      RadDropDownListElement editorElement = (RadDropDownListElement) this.EditorElement;
      if (this.IsInitalizing || !this.EditorElement.IsInValidState(true))
        return;
      object newValue = editorElement.Items[e.Position].Value;
      object oldValue = (object) null;
      if (editorElement.SelectedItem != null)
        oldValue = editorElement.SelectedItem.Value;
      ValueChangingEventArgs e1 = new ValueChangingEventArgs(newValue, oldValue);
      this.OnValueChanging(e1);
      e.Cancel = e1.Cancel;
      this.cancelValueChanging = e1.Cancel;
    }

    private void DropDownListElement_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      if (this.cancelValueChanging)
        return;
      this.OnValueChanged();
    }

    private void TextBoxItem_TextChanged(object sender, EventArgs e)
    {
      BaseDropDownListEditorElement editorElement = (BaseDropDownListEditorElement) this.EditorElement;
      if (editorElement.DropDownStyle != RadDropDownStyle.DropDown)
        return;
      string text = (sender as RadTextBoxItem).Text;
      StringComparison comparisonType = editorElement.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
      foreach (RadListDataItem radListDataItem in editorElement.Items)
      {
        if (radListDataItem.Text.Equals(text, comparisonType))
        {
          radListDataItem.Selected = true;
          break;
        }
      }
    }

    private void HostedControl_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus();
    }

    private void DropDownListElement_RadPropertyChanged(
      object sender,
      RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.ContainsFocusProperty || (bool) e.NewValue)
        return;
      this.OnLostFocus();
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new BaseDropDownListEditorElement();
    }
  }
}
