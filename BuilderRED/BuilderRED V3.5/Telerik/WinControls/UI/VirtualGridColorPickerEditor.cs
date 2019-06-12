// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridColorPickerEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class VirtualGridColorPickerEditor : BaseColorEditor
  {
    private TypeConverter converter = TypeDescriptor.GetConverter(typeof (Color));
    private RadVirtualGridElement virtualGridElement;
    private int selectionStart;
    private int selectionLength;

    public override void Initialize(object owner, object value)
    {
      base.Initialize(owner, value);
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
      editorElement.KeyUp += new KeyEventHandler(this.editorElement_KeyUp);
      editorElement.KeyDown += new KeyEventHandler(this.editorElement_KeyDown);
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      RadColorPickerEditorElement editorElement = this.EditorElement as RadColorPickerEditorElement;
      editorElement.ValueChanging -= new ValueChangingEventHandler(this.editorElement_ValueChanging);
      editorElement.ValueChanged -= new EventHandler(this.editorElement_ValueChanged);
      editorElement.DialogClosed -= new DialogClosedEventHandler(this.editorElement_DialogClosed);
      editorElement.KeyUp -= new KeyEventHandler(this.editorElement_KeyUp);
      editorElement.KeyDown -= new KeyEventHandler(this.editorElement_KeyDown);
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
      ValueChangingEventArgs e1 = new ValueChangingEventArgs(e.NewValue, e.OldValue);
      (this.OwnerElement as VirtualGridCellElement)?.RowElement.TableElement.GridElement.OnValueChanging((object) this, e1);
      e.Cancel = e1.Cancel;
    }

    private void editorElement_ValueChanged(object sender, EventArgs e)
    {
      (this.OwnerElement as VirtualGridCellElement)?.RowElement.TableElement.GridElement.OnValueChanged((object) this, EventArgs.Empty);
    }

    private void editorElement_DialogClosed(object sender, DialogClosedEventArgs e)
    {
      (this.EditorElement as RadColorPickerEditorElement).Focus();
    }

    private void editorElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void editorElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
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
      if (this.virtualGridElement == null)
        this.virtualGridElement = this.OwnerElement.ElementTree.RootElement.FindDescendant<RadVirtualGridElement>();
      if (this.virtualGridElement == null)
        return;
      e.Handled = this.virtualGridElement.InputBehavior.HandleKeyDown(e);
    }
  }
}
