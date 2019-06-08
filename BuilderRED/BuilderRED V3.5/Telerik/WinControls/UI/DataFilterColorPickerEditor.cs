// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterColorPickerEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterColorPickerEditor : BaseColorEditor
  {
    private TypeConverter converter = TypeDescriptor.GetConverter(typeof (Color));
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
      (this.EditorElement as RadColorPickerEditorElement).DialogClosed += new DialogClosedEventHandler(this.editorElement_DialogClosed);
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      (this.EditorElement as RadColorPickerEditorElement).SetColorValue(Color.Empty);
      return true;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      RadColorPickerEditorElement editorElement = this.EditorElement as RadColorPickerEditorElement;
      DataFilterCriteriaElement ownerElement = this.OwnerElement as DataFilterCriteriaElement;
      if (editorElement == null || !editorElement.IsInValidState(true) || ownerElement == null)
        return;
      this.selectionStart = editorElement.TextBoxItem.SelectionStart;
      this.selectionLength = editorElement.TextBoxItem.SelectionLength;
      if (e.KeyCode == Keys.Return && e.Modifiers != Keys.Control)
      {
        if (!this.Validate())
          return;
        ownerElement.TreeViewElement.EndEdit();
      }
      else if (e.KeyCode == Keys.Escape)
      {
        ownerElement.TreeViewElement.CancelEdit();
        e.Handled = true;
      }
      else
      {
        if (e.KeyCode != Keys.Delete || editorElement.TextBoxItem.SelectionLength != editorElement.TextBoxItem.TextLength)
          return;
        editorElement.Text = string.Empty;
      }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      RadColorPickerEditorElement editorElement = this.EditorElement as RadColorPickerEditorElement;
      DataFilterCriteriaElement ownerElement = this.OwnerElement as DataFilterCriteriaElement;
      if (editorElement == null || !editorElement.IsInValidState(true) || ownerElement == null)
        return;
      int length = editorElement.Text.Length;
      switch (e.KeyCode)
      {
        case Keys.Left:
          if ((!this.RightToLeft || this.selectionStart != editorElement.Text.Length) && (this.RightToLeft || this.selectionStart != 0 || this.selectionLength != 0))
            break;
          editorElement.Validate();
          break;
        case Keys.Right:
          if ((!this.RightToLeft || this.selectionStart != 0) && (this.RightToLeft || this.selectionStart != editorElement.Text.Length))
            break;
          editorElement.Validate();
          break;
      }
    }

    protected override void OnLostFocus()
    {
      base.OnLostFocus();
      TreeNodeElement ownerElement = this.OwnerElement as TreeNodeElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || (ownerElement.ElementTree.Control.Focused || ownerElement.ElementTree.Control.ContainsFocus) || ownerElement.TreeViewElement.IsPerformingEndEdit)
        return;
      ownerElement.TreeViewElement.EndEdit();
    }

    private void editorElement_DialogClosed(object sender, DialogClosedEventArgs e)
    {
      RadColorPickerEditorElement editorElement = this.EditorElement as RadColorPickerEditorElement;
      editorElement.DialogClosed -= new DialogClosedEventHandler(this.editorElement_DialogClosed);
      DataFilterCriteriaElement ownerElement = this.OwnerElement as DataFilterCriteriaElement;
      if (ownerElement == null)
        return;
      ownerElement.CriteriaNode.DescriptorValue = (object) editorElement.Value;
      ownerElement.Synchronize();
      ownerElement.InvalidateMeasure(true);
      ownerElement.UpdateLayout();
      ownerElement.TreeViewElement.UpdateScrollersOnNodesNeeded(ownerElement.Data);
    }
  }
}
