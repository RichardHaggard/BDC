// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxControlEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [TelerikToolboxCategory("Editors")]
  public class RadTextBoxControlEditor : BaseGridEditor
  {
    public RadTextBoxControlEditor()
    {
      this.TextBox.MinSize = new Size(0, 20);
    }

    protected RadTextBoxControlElement TextBox
    {
      get
      {
        return this.EditorElement as RadTextBoxControlElement;
      }
    }

    public override object Value
    {
      get
      {
        return (object) this.TextBox.Text;
      }
      set
      {
        this.TextBox.Text = Convert.ToString(value);
      }
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (string);
      }
    }

    public string NullText
    {
      get
      {
        return this.TextBox.NullText;
      }
      set
      {
        this.TextBox.NullText = value;
      }
    }

    public CharacterCasing CharacterCasing
    {
      get
      {
        return this.TextBox.CharacterCasing;
      }
      set
      {
        this.TextBox.CharacterCasing = value;
      }
    }

    public bool Multiline
    {
      get
      {
        return this.TextBox.Multiline;
      }
      set
      {
        this.TextBox.Multiline = value;
      }
    }

    public int MaxLength
    {
      get
      {
        return this.TextBox.MaxLength;
      }
      set
      {
        this.TextBox.MaxLength = value;
      }
    }

    public bool AcceptsTab
    {
      get
      {
        return this.TextBox.AcceptsTab;
      }
      set
      {
        this.TextBox.AcceptsTab = value;
      }
    }

    public bool AcceptsReturn
    {
      get
      {
        return this.TextBox.AcceptsReturn;
      }
      set
      {
        this.TextBox.AcceptsReturn = value;
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadTextBoxControlElement textBox = this.TextBox;
      RadControl radControl = this.EditorElement.ElementTree == null || this.EditorElement.ElementTree.Control == null ? (RadControl) null : this.EditorElement.ElementTree.Control as RadControl;
      if (radControl != null && TelerikHelper.IsMaterialTheme(radControl.ThemeName))
      {
        textBox.StretchVertically = true;
        if (this.EditorElement.Parent != null)
          this.EditorElement.Parent.UpdateLayout();
      }
      else
        textBox.StretchVertically = this.TextBox.Multiline;
      textBox.SelectAll();
      textBox.Focus();
      textBox.TextChanging += new TextChangingEventHandler(this.OnTextChanging);
      textBox.TextChanged += new EventHandler(this.OnTextChanged);
      textBox.MouseWheel += new MouseEventHandler(this.OnElementMouseWheel);
      textBox.KeyDown += new KeyEventHandler(this.OnElementKeyDown);
      textBox.KeyUp += new KeyEventHandler(this.OnElementKeyUp);
    }

    public override bool EndEdit()
    {
      RadTextBoxControlElement textBox = this.TextBox;
      textBox.TextChanging -= new TextChangingEventHandler(this.OnTextChanging);
      textBox.TextChanged -= new EventHandler(this.OnTextChanged);
      textBox.MouseWheel -= new MouseEventHandler(this.OnElementMouseWheel);
      textBox.KeyDown -= new KeyEventHandler(this.OnElementKeyDown);
      textBox.KeyUp -= new KeyEventHandler(this.OnElementKeyUp);
      textBox.Text = string.Empty;
      textBox.Select(0, 0);
      textBox.ListElement.StartPosition = (TextPosition) null;
      textBox.ListElement.EndPosition = (TextPosition) null;
      textBox.AutoCompleteDataSource = (object) null;
      textBox.AutoCompleteItems.Clear();
      return base.EndEdit();
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadTextBoxControlElement();
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
      RadTextBoxControlElement textBox = this.TextBox;
      if (textBox == null || !textBox.IsInValidState(true) || textBox.IsAutoCompleteDropDownOpen)
        return;
      int selectionStart = textBox.SelectionStart;
      int selectionLength = textBox.SelectionLength;
      bool flag1 = false;
      bool flag2 = false;
      TextPosition caretPosition = textBox.Navigator.CaretPosition;
      if (caretPosition != (TextPosition) null)
      {
        LineInfoCollection lines = textBox.ViewElement.Lines;
        flag1 = lines.FirstLine == caretPosition.Line;
        flag2 = lines.LastLine == caretPosition.Line;
      }
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (e.Modifiers == Keys.Control)
            break;
          base.OnKeyDown(e);
          break;
        case Keys.Up:
          if (this.Multiline && (selectionLength != 0 || !flag1))
            break;
          base.OnKeyDown(e);
          break;
        case Keys.Down:
          if (this.Multiline && (selectionLength != 0 || !flag2))
            break;
          base.OnKeyDown(e);
          break;
      }
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
      RadTextBoxControlElement textBox = this.TextBox;
      if (textBox == null || !textBox.IsInValidState(true) || textBox.IsAutoCompleteDropDownOpen)
        return;
      int textLength = textBox.TextLength;
      int selectionStart = textBox.SelectionStart;
      int selectionLength = textBox.SelectionLength;
      switch (e.KeyCode)
      {
        case Keys.Left:
          if (selectionLength != 0 || (this.RightToLeft || selectionStart != 0) && (!this.RightToLeft || selectionStart != textLength))
            break;
          base.OnKeyDown(e);
          break;
        case Keys.Right:
          if (selectionLength != 0 || (this.RightToLeft || selectionStart != textLength) && (!this.RightToLeft || selectionStart != 0))
            break;
          base.OnKeyDown(e);
          break;
      }
    }

    private void OnElementKeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void OnElementKeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void OnElementMouseWheel(object sender, MouseEventArgs e)
    {
      this.OnMouseWheel(e);
    }

    private void OnTextChanging(object sender, TextChangingEventArgs e)
    {
      if (this.IsInitalizing)
        return;
      ValueChangingEventArgs e1 = new ValueChangingEventArgs((object) e.NewValue);
      this.OnValueChanging(e1);
      e.Cancel = e1.Cancel;
    }

    private void OnTextChanged(object sender, EventArgs e)
    {
      if (this.IsInitalizing)
        return;
      this.OnValueChanged();
    }
  }
}
