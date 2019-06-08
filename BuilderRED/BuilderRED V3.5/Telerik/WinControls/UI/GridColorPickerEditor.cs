// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridColorPickerEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridColorPickerEditor : BaseColorEditor
  {
    private TypeConverter converter = TypeDescriptor.GetConverter(typeof (Color));
    private RadGridViewElement gridRootElement;
    private int selectionStart;
    private int selectionLength;

    public override void Initialize(object owner, object value)
    {
      base.Initialize(owner, value);
      this.originalValue = value ?? (object) Color.Empty;
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadColorPickerEditorElement();
    }

    public override object Value
    {
      get
      {
        return (object) (this.EditorElement as RadColorPickerEditorElement).GetColorValue();
      }
      set
      {
        RadColorPickerEditorElement editorElement = this.EditorElement as RadColorPickerEditorElement;
        if (value is Color)
        {
          editorElement.SetColorValue((Color) value);
        }
        else
        {
          if (!(value is string))
            return;
          editorElement.SetColorValue((Color) this.converter.ConvertFromString((string) value));
        }
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadColorPickerEditorElement editorElement = this.EditorElement as RadColorPickerEditorElement;
      editorElement.ValueChanging += new ValueChangingEventHandler(this.editorElement_ValueChanging);
      editorElement.ValueChanged += new EventHandler(this.editorElement_ValueChanged);
      editorElement.DialogClosed += new DialogClosedEventHandler(this.editorElement_DialogClosed);
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      RadColorPickerEditorElement editorElement = this.EditorElement as RadColorPickerEditorElement;
      editorElement.ValueChanging -= new ValueChangingEventHandler(this.editorElement_ValueChanging);
      editorElement.ValueChanged -= new EventHandler(this.editorElement_ValueChanged);
      editorElement.DialogClosed -= new DialogClosedEventHandler(this.editorElement_DialogClosed);
      editorElement.SetColorValue(Color.Empty);
      return true;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      RadColorPickerEditorElement editorElement = this.EditorElement as RadColorPickerEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      this.selectionStart = editorElement.TextBoxItem.SelectionStart;
      this.selectionLength = editorElement.TextBoxItem.SelectionLength;
      if (e.KeyCode == Keys.Return)
      {
        if (e.Modifiers != Keys.Control)
        {
          try
          {
            this.Value = (object) (Color) this.converter.ConvertFromString(editorElement.Text);
          }
          catch (Exception ex)
          {
            editorElement.Focus();
            editorElement.TextBoxItem.SelectAll();
          }
          this.ProcessKeyDown(e);
          return;
        }
      }
      if (e.KeyCode != Keys.Escape)
        return;
      this.ProcessKeyDown(e);
      e.Handled = true;
    }

    private void editorElement_ValueChanging(object sender, ValueChangingEventArgs e)
    {
      ValueChangingEventArgs args = new ValueChangingEventArgs(e.NewValue, e.OldValue);
      (this.OwnerElement as GridCellElement)?.ViewTemplate.EventDispatcher.RaiseEvent<ValueChangingEventArgs>(EventDispatcher.ValueChanging, (object) this, args);
      e.Cancel = args.Cancel;
    }

    private void editorElement_ValueChanged(object sender, EventArgs e)
    {
      GridCellElement ownerElement = this.OwnerElement as GridCellElement;
      if (ownerElement == null || ownerElement.ViewTemplate == null)
        return;
      ownerElement.ViewTemplate.EventDispatcher.RaiseEvent<EventArgs>(EventDispatcher.ValueChanged, (object) this, EventArgs.Empty);
    }

    private void editorElement_DialogClosed(object sender, DialogClosedEventArgs e)
    {
      (this.EditorElement as RadColorPickerEditorElement).Focus();
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      RadColorPickerEditorElement editorElement = this.EditorElement as RadColorPickerEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      int length = editorElement.Text.Length;
      switch (e.KeyCode)
      {
        case Keys.Left:
          if (this.selectionLength != 0 || (this.RightToLeft || this.selectionStart != 0) && (!this.RightToLeft || this.selectionStart != length))
            break;
          this.ProcessKeyDown(e);
          break;
        case Keys.Right:
          if (this.selectionLength != 0 || (this.RightToLeft || this.selectionStart != length) && (!this.RightToLeft || this.selectionStart != 0))
            break;
          this.ProcessKeyDown(e);
          break;
      }
    }

    private void ProcessKeyDown(KeyEventArgs e)
    {
      if (this.OwnerElement == null || !this.OwnerElement.IsInValidState(true))
        return;
      if (this.gridRootElement == null)
        this.gridRootElement = this.OwnerElement.ElementTree.RootElement.FindDescendant<RadGridViewElement>();
      if (this.gridRootElement == null)
        return;
      e.Handled = this.gridRootElement.GridBehavior.ProcessKeyDown(e);
    }
  }
}
