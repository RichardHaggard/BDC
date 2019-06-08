// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridCalculatorEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class VirtualGridCalculatorEditor : BaseVirtualGridEditor
  {
    private int selectionStart;
    private int selectionLength;

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadCalculatorEditorElement();
    }

    public override object Value
    {
      get
      {
        return (this.EditorElement as RadCalculatorDropDownElement).Value;
      }
      set
      {
        (this.EditorElement as RadCalculatorDropDownElement).Value = value;
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadCalculatorDropDownElement editorElement = this.EditorElement as RadCalculatorDropDownElement;
      editorElement.CalculatorContentElement.Reset();
      editorElement.CalculatorContentElement.MemoryValue = new Decimal(0);
      editorElement.CalculatorValueChanging += new ValueChangingEventHandler(this.editorElement_CalculatorValueChanging);
      editorElement.CalculatorValueChanged += new EventHandler(this.editorElement_CalculatorValueChanged);
      editorElement.EditorContentElement.KeyUp += new KeyEventHandler(this.editorElement_KeyUp);
      editorElement.EditorContentElement.KeyDown += new KeyEventHandler(this.editorElement_KeyDown);
      editorElement.EditorContentElement.Focus();
      editorElement.EditorContentElement.TextBoxItem.SelectAll();
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      RadCalculatorDropDownElement editorElement = this.EditorElement as RadCalculatorDropDownElement;
      if (editorElement.IsPopupOpen)
        editorElement.ClosePopup();
      editorElement.CalculatorValueChanging -= new ValueChangingEventHandler(this.editorElement_CalculatorValueChanging);
      editorElement.CalculatorValueChanged -= new EventHandler(this.editorElement_CalculatorValueChanged);
      editorElement.EditorContentElement.KeyUp -= new KeyEventHandler(this.editorElement_KeyUp);
      editorElement.EditorContentElement.KeyDown -= new KeyEventHandler(this.editorElement_KeyDown);
      return true;
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
      RadCalculatorDropDownElement editorElement = this.EditorElement as RadCalculatorDropDownElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      this.selectionStart = editorElement.EditorContentElement.TextBoxItem.SelectionStart;
      this.selectionLength = editorElement.EditorContentElement.TextBoxItem.SelectionLength;
      if (e.KeyCode == Keys.Return && e.Modifiers != Keys.Control)
      {
        base.OnKeyDown(e);
      }
      else
      {
        if (e.KeyCode != Keys.Escape)
          return;
        base.OnKeyDown(e);
        e.Handled = true;
      }
    }

    private void editorElement_CalculatorValueChanging(object sender, ValueChangingEventArgs e)
    {
      ValueChangingEventArgs e1 = new ValueChangingEventArgs(e.NewValue, e.OldValue);
      this.OnValueChanging(e1);
      e.Cancel = e1.Cancel;
    }

    private void editorElement_CalculatorValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged();
    }

    private void editorElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void editorElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    public virtual void OnKeyUp(KeyEventArgs e)
    {
      RadCalculatorDropDownElement editorElement = this.EditorElement as RadCalculatorDropDownElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      int length = editorElement.EditorContentElement.Text.Length;
      switch (e.KeyCode)
      {
        case Keys.Left:
          if (this.selectionLength != 0 || (this.RightToLeft || this.selectionStart != 0) && (!this.RightToLeft || this.selectionStart != length))
            break;
          base.OnKeyDown(e);
          break;
        case Keys.Right:
          if (this.selectionLength != 0 || (this.RightToLeft || this.selectionStart != length) && (!this.RightToLeft || this.selectionStart != 0))
            break;
          base.OnKeyDown(e);
          break;
      }
    }
  }
}
