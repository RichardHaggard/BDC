// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseTextBoxControlEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BaseTextBoxControlEditor : BaseInputEditor
  {
    protected int selectionStart;
    protected int selectionLength;
    protected bool isAtFirstLine;
    protected bool isAtLastLine;

    public BaseTextBoxControlEditor()
    {
      this.EditorElement.StretchVertically = false;
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

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadTextBoxControlElement();
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadTextBoxControlElement textBox = this.TextBox;
      textBox.SelectAll();
      textBox.Focus();
      textBox.TextChanging += new TextChangingEventHandler(this.OnTextBoxTextChanging);
      textBox.TextChanged += new EventHandler(this.OnTextBoxTextChanged);
      textBox.KeyDown += new KeyEventHandler(this.OnTextBoxKeyDown);
      textBox.RadPropertyChanged += new RadPropertyChangedEventHandler(this.OnTextBoxRadPropertyChanged);
    }

    public override bool EndEdit()
    {
      RadTextBoxControlElement textBox = this.TextBox;
      textBox.TextChanging -= new TextChangingEventHandler(this.OnTextBoxTextChanging);
      textBox.TextChanged -= new EventHandler(this.OnTextBoxTextChanged);
      textBox.KeyDown -= new KeyEventHandler(this.OnTextBoxKeyDown);
      textBox.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.OnTextBoxRadPropertyChanged);
      textBox.Text = string.Empty;
      textBox.Select(0, 0);
      return base.EndEdit();
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
    }

    protected virtual void OnLostFocus()
    {
    }

    private void OnTextBoxTextChanging(object sender, TextChangingEventArgs e)
    {
      if (this.IsInitalizing)
        return;
      ValueChangingEventArgs e1 = new ValueChangingEventArgs((object) e.NewValue, (object) e.OldValue);
      this.OnValueChanging(e1);
      e.Cancel = e1.Cancel;
    }

    private void OnTextBoxTextChanged(object sender, EventArgs e)
    {
      if (this.IsInitalizing)
        return;
      this.OnValueChanged();
    }

    private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
    {
      if (this.TextBox == null || !this.TextBox.IsInValidState(true))
        return;
      this.selectionStart = this.TextBox.SelectionStart;
      this.selectionLength = this.TextBox.SelectionLength;
      TextPosition caretPosition = this.TextBox.Navigator.CaretPosition;
      LineInfoCollection lines = this.TextBox.ViewElement.Lines;
      this.isAtFirstLine = caretPosition != (TextPosition) null && object.Equals((object) caretPosition.Line, (object) lines.FirstLine);
      this.isAtLastLine = caretPosition != (TextPosition) null && object.Equals((object) caretPosition.Line, (object) lines.LastLine);
      this.OnKeyDown(e);
    }

    private void OnTextBoxRadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.ContainsFocusProperty || (bool) e.NewValue)
        return;
      this.OnLostFocus();
    }
  }
}
