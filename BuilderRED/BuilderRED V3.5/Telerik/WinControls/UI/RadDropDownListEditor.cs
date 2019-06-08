// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownListEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Data Controls")]
  [RadToolboxItem(false)]
  public class RadDropDownListEditor : BaseGridEditor
  {
    private int selectionStart;
    private bool cancelValueChanging;
    private bool allowItemSelection;

    public virtual bool AllowItemSelection
    {
      get
      {
        return this.allowItemSelection;
      }
      set
      {
        this.allowItemSelection = value;
      }
    }

    public override object Value
    {
      get
      {
        RadDropDownListElement editorElement = this.EditorElement as RadDropDownListElement;
        if (editorElement.SelectedItem == null)
          return (object) string.Empty;
        if (!string.IsNullOrEmpty(editorElement.ValueMember))
          return editorElement.SelectedItem.Value;
        return (object) editorElement.SelectedItem.Text;
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
        if (!this.IsInitalizing)
          return;
        this.originalValue = this.Value;
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
        GridFilterCellElement ownerElement = this.OwnerElement as GridFilterCellElement;
        if (ownerElement != null)
        {
          GridViewComboBoxColumn columnInfo = ownerElement.ColumnInfo as GridViewComboBoxColumn;
          if (columnInfo != null)
            return columnInfo.FilteringMemberDataType;
        }
        return base.DataType;
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadDropDownListEditorElement editorElement = (RadDropDownListEditorElement) this.EditorElement;
      editorElement.TextBox.TextBoxItem.TextChanged += new EventHandler(this.TextBoxItem_TextChanged);
      editorElement.SelectedIndexChanging += new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.DropDownListElement_SelectedIndexChanging);
      editorElement.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.DropDownListElement_SelectedIndexChanged);
      editorElement.HandleKeyUp += new KeyEventHandler(this.DropDownListElement_HandleKeyUp);
      editorElement.HandleKeyDown += new KeyEventHandler(this.DropDownListElement_HandleKeyDown);
      editorElement.SelectionStart = 0;
      editorElement.SelectionLength = editorElement.Text == null ? 0 : editorElement.Text.Length;
      editorElement.Focus();
      if (editorElement.DropDownStyle == RadDropDownStyle.DropDown)
        editorElement.TextBox.Focus();
      if (this.originalValue != null)
        return;
      this.originalValue = (object) string.Empty;
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      RadDropDownListEditorElement editorElement = (RadDropDownListEditorElement) this.EditorElement;
      if (editorElement.IsPopupOpen)
        editorElement.ClosePopup();
      if (editorElement.AutoCompleteSuggest != null)
        editorElement.AutoCompleteSuggest.DropDownList.ClosePopup();
      editorElement.SelectedIndexChanging -= new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.DropDownListElement_SelectedIndexChanging);
      editorElement.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.DropDownListElement_SelectedIndexChanged);
      editorElement.HandleKeyUp -= new KeyEventHandler(this.DropDownListElement_HandleKeyUp);
      editorElement.HandleKeyDown -= new KeyEventHandler(this.DropDownListElement_HandleKeyDown);
      editorElement.TextBox.TextBoxItem.TextChanged -= new EventHandler(this.TextBoxItem_TextChanged);
      editorElement.Filter = (Predicate<RadListDataItem>) null;
      editorElement.DataSource = (object) null;
      editorElement.Items.Clear();
      return true;
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
      RadDropDownListEditorElement editorElement = this.EditorElement as RadDropDownListEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      this.selectionStart = editorElement.SelectionStart;
      if (e.KeyCode == Keys.Return && e.Modifiers != Keys.Control)
        base.OnKeyDown(e);
      else if (e.KeyCode == Keys.Escape)
      {
        base.OnKeyDown(e);
        e.Handled = true;
      }
      else
      {
        if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right || editorElement.DropDownStyle != RadDropDownStyle.DropDownList)
          return;
        e.Handled = true;
      }
    }

    public virtual void OnKeyUp(KeyEventArgs e)
    {
      RadDropDownListEditorElement editorElement = this.EditorElement as RadDropDownListEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      bool flag = editorElement.DropDownStyle == RadDropDownStyle.DropDown;
      int length = editorElement.Text.Length;
      switch (e.KeyCode)
      {
        case Keys.Left:
          if (flag && (!this.RightToLeft || this.selectionStart != length) && (this.RightToLeft || this.selectionStart != 0))
            break;
          base.OnKeyDown(e);
          break;
        case Keys.Right:
          if (flag && (!this.RightToLeft || this.selectionStart != 0) && (this.RightToLeft || this.selectionStart != length))
            break;
          base.OnKeyDown(e);
          break;
      }
    }

    private void DropDownListElement_HandleKeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void DropDownListElement_HandleKeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void DropDownListElement_SelectedIndexChanging(
      object sender,
      PositionChangingCancelEventArgs e)
    {
      if (e.Cancel || e.Position < 0)
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
      RadDropDownListEditorElement editorElement = (RadDropDownListEditorElement) this.EditorElement;
      if (editorElement.DropDownStyle != RadDropDownStyle.DropDown)
        return;
      string text = (sender as RadTextBoxItem).Text;
      StringComparison comparisonType = editorElement.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
      if (!this.AllowItemSelection)
        return;
      foreach (RadListDataItem radListDataItem in editorElement.Items)
      {
        if (radListDataItem.Text.Equals(text, comparisonType))
        {
          radListDataItem.Selected = true;
          break;
        }
      }
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadDropDownListEditorElement();
    }
  }
}
