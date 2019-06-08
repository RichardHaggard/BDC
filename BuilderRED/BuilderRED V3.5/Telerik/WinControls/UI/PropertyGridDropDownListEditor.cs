// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridDropDownListEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridDropDownListEditor : BaseDropDownListEditor
  {
    private bool loopValuesOnDoubleClick = true;

    public bool LoopValuesOnDoubleClick
    {
      get
      {
        return this.loopValuesOnDoubleClick;
      }
      set
      {
        this.loopValuesOnDoubleClick = value;
      }
    }

    public override object Value
    {
      get
      {
        PropertyGridItem data = (this.OwnerElement as PropertyGridItemElement).Data as PropertyGridItem;
        if (data == null || !data.TypeConverter.GetStandardValuesSupported((ITypeDescriptorContext) data) || data.TypeConverter.GetStandardValuesExclusive((ITypeDescriptorContext) data))
          return base.Value;
        BaseDropDownListEditorElement editorElement = this.EditorElement as BaseDropDownListEditorElement;
        if (data != null && data.TypeConverter.CanConvertFrom((ITypeDescriptorContext) data, typeof (string)))
          return data.TypeConverter.ConvertFrom((ITypeDescriptorContext) data, Thread.CurrentThread.CurrentCulture, (object) editorElement.Text);
        return (object) editorElement.Text;
      }
      set
      {
        PropertyGridItem data = (this.OwnerElement as PropertyGridItemElement).Data as PropertyGridItem;
        if (data.TypeConverter.GetStandardValuesSupported((ITypeDescriptorContext) data) && !data.TypeConverter.GetStandardValuesExclusive((ITypeDescriptorContext) data) && data.TypeConverter.CanConvertTo((ITypeDescriptorContext) data, typeof (string)))
          base.Value = (object) data.TypeConverter.ConvertToString((ITypeDescriptorContext) data, value);
        else
          base.Value = value;
      }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      BaseDropDownListEditorElement editorElement = this.EditorElement as BaseDropDownListEditorElement;
      this.selectionStart = editorElement.SelectionStart;
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null)
        return;
      if (e.KeyCode == Keys.Return && e.Modifiers != Keys.Control)
        ownerElement.PropertyTableElement.EndEdit();
      else if (e.KeyCode == Keys.Escape)
      {
        ownerElement.PropertyTableElement.CancelEdit();
        e.Handled = true;
      }
      else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
      {
        if (editorElement.DropDownStyle != RadDropDownStyle.DropDownList)
          return;
        e.Handled = true;
      }
      else
      {
        if (e.KeyCode != Keys.Down || e.Modifiers != Keys.Alt)
          return;
        ((PopupEditorBaseElement) this.EditorElement).ShowPopup();
        e.Handled = true;
      }
    }

    protected override void OnLostFocus()
    {
      base.OnLostFocus();
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus))
        return;
      ownerElement.PropertyTableElement.EndEdit();
    }

    public override void Initialize(object owner, object value)
    {
      base.Initialize(owner, value);
      PropertyGridItem data = (owner as PropertyGridItemElement).Data as PropertyGridItem;
      if (data == null)
        return;
      BaseDropDownListEditorElement editorElement = this.EditorElement as BaseDropDownListEditorElement;
      editorElement.ListElement.BindingContext = data.PropertyGridTableElement.BindingContext;
      editorElement.DropDownStyle = RadDropDownStyle.DropDownList;
      if (data.TypeConverter.GetStandardValuesSupported((ITypeDescriptorContext) data))
      {
        ICollection standardValues = (ICollection) data.TypeConverter.GetStandardValues((ITypeDescriptorContext) data);
        List<string> stringList = new List<string>();
        if (standardValues != null)
        {
          foreach (object obj in (IEnumerable) standardValues)
            stringList.Add(data.TypeConverter.ConvertToString(obj));
        }
        string str = data.TypeConverter.ConvertToString(data.Value);
        if (!data.TypeConverter.GetStandardValuesExclusive((ITypeDescriptorContext) data))
        {
          editorElement.DropDownStyle = RadDropDownStyle.DropDown;
          if (!string.IsNullOrEmpty(str) && !stringList.Contains(str))
            stringList.Insert(0, str);
        }
        editorElement.DataSource = (object) stringList;
        editorElement.SelectedValue = (object) str;
      }
      editorElement.DoubleClick += new EventHandler(this.editorElement_DoubleClick);
      editorElement.MouseMove += new MouseEventHandler(this.editorElement_MouseMove);
    }

    public override bool EndEdit()
    {
      BaseDropDownListEditorElement editorElement = this.EditorElement as BaseDropDownListEditorElement;
      editorElement.DoubleClick -= new EventHandler(this.editorElement_DoubleClick);
      editorElement.MouseEnter -= new EventHandler(this.editorElement_MouseMove);
      return base.EndEdit();
    }

    private void editorElement_DoubleClick(object sender, EventArgs e)
    {
      this.OnEditorElementDoubleClick();
    }

    private void editorElement_MouseMove(object sender, EventArgs e)
    {
      if (this.OwnerElement == null || this.OwnerElement.ElementTree == null)
        return;
      this.OwnerElement.ElementTree.Control.Cursor = Cursors.Default;
    }

    protected virtual void OnEditorElementDoubleClick()
    {
      if (!this.LoopValuesOnDoubleClick)
        return;
      BaseDropDownListEditorElement editorElement = this.EditorElement as BaseDropDownListEditorElement;
      if (editorElement == null || editorElement.SelectedValue == null || editorElement.Items.Count == 0)
        return;
      if (editorElement.SelectedIndex < editorElement.Items.Count - 1)
        ++editorElement.SelectedIndex;
      else
        editorElement.SelectedIndex = 0;
    }

    public override void OnValueChanging(ValueChangingEventArgs e)
    {
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement.PropertyTableElement.isChanging)
        return;
      ownerElement.PropertyTableElement.isChanging = true;
      base.OnValueChanging(e);
      ownerElement?.PropertyTableElement.OnValueChanging((object) this, e);
      ownerElement.PropertyTableElement.isChanging = false;
    }

    public override void OnValueChanged()
    {
      base.OnValueChanged();
      (this.OwnerElement as PropertyGridItemElement)?.PropertyTableElement.OnValueChanged((object) this, EventArgs.Empty);
    }
  }
}
