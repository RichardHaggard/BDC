// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxEditor
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
  public class RadTextBoxEditor : BaseGridEditor
  {
    private string nullValue = string.Empty;
    private int selectionStart;
    private int selectionLength;
    private bool isAtFirstLine;
    private bool isAtLastLine;
    private bool isModified;

    public RadTextBoxEditor()
    {
      this.originalValue = (object) string.Empty;
    }

    public override object Value
    {
      get
      {
        return (object) ((RadTextBoxElement) this.EditorElement).TextBoxItem.Text;
      }
      set
      {
        string empty = string.Empty;
        if (value != null)
          empty = value.ToString();
        RadTextBoxElement editorElement = (RadTextBoxElement) this.EditorElement;
        editorElement.TextBoxItem.Text = empty;
        if (!this.IsInitalizing || !(editorElement.TextBoxItem.Text != empty))
          return;
        this.isModified = true;
      }
    }

    public string NullValue
    {
      get
      {
        return this.nullValue;
      }
      set
      {
        this.nullValue = value;
      }
    }

    public CharacterCasing CharacterCasing
    {
      get
      {
        return ((RadTextBoxElement) this.EditorElement).TextBoxItem.CharacterCasing;
      }
      set
      {
        ((RadTextBoxElement) this.EditorElement).TextBoxItem.CharacterCasing = value;
      }
    }

    public bool Multiline
    {
      get
      {
        return ((RadTextBoxElement) this.EditorElement).TextBoxItem.Multiline;
      }
      set
      {
        ((RadTextBoxElement) this.EditorElement).TextBoxItem.Multiline = value;
      }
    }

    public int MaxLength
    {
      get
      {
        return ((RadTextBoxElement) this.EditorElement).TextBoxItem.MaxLength;
      }
      set
      {
        ((RadTextBoxElement) this.EditorElement).TextBoxItem.MaxLength = value;
      }
    }

    public bool AcceptsTab
    {
      get
      {
        return ((RadTextBoxElement) this.EditorElement).TextBoxItem.AcceptsTab;
      }
      set
      {
        ((RadTextBoxElement) this.EditorElement).TextBoxItem.AcceptsTab = value;
      }
    }

    public bool AcceptsReturn
    {
      get
      {
        return ((RadTextBoxElement) this.EditorElement).TextBoxItem.AcceptsReturn;
      }
      set
      {
        ((RadTextBoxElement) this.EditorElement).TextBoxItem.AcceptsReturn = value;
      }
    }

    private RadTextBoxEditorElement TextBoxEditorElement
    {
      get
      {
        return this.EditorElement as RadTextBoxEditorElement;
      }
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (string);
      }
    }

    public override bool IsModified
    {
      get
      {
        if (!base.IsModified)
          return this.isModified;
        return true;
      }
    }

    public override bool ClearCellText
    {
      get
      {
        return !this.Multiline;
      }
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadTextBoxEditorElement();
    }

    public override void Initialize(object owner, object value)
    {
      this.isModified = false;
      base.Initialize(owner, value);
      this.originalValue = value ?? (object) string.Empty;
    }

    internal static bool IsDarkTheme(RadElement ownerElement)
    {
      return TelerikHelper.IsDarkTheme(ownerElement == null ? string.Empty : ownerElement.ElementTree.ThemeName);
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadTextBoxEditorElement editorElement = (RadTextBoxEditorElement) this.EditorElement;
      RadTextBoxItem textBoxItem = editorElement.TextBoxItem;
      if (!RadTextBoxEditor.IsDarkTheme(this.OwnerElement))
      {
        int num = (int) editorElement.SetDefaultValueOverride(VisualElement.BackColorProperty, (object) Color.White);
      }
      RadControl radControl = this.EditorElement.ElementTree == null || this.EditorElement.ElementTree.Control == null ? (RadControl) null : this.EditorElement.ElementTree.Control as RadControl;
      if (radControl != null && TelerikHelper.IsMaterialTheme(radControl.ThemeName))
      {
        textBoxItem.StretchVertically = true;
        if (this.EditorElement.Parent != null)
          this.EditorElement.Parent.UpdateLayout();
      }
      else
        textBoxItem.StretchVertically = textBoxItem.Multiline;
      textBoxItem.SelectAll();
      textBoxItem.TextBoxControl.Focus();
      textBoxItem.TextChanging += new TextChangingEventHandler(this.TextBoxItem_TextChanging);
      textBoxItem.TextChanged += new EventHandler(this.TextBoxItem_TextChanged);
      textBoxItem.KeyDown += new KeyEventHandler(this.TextBoxItem_KeyDown);
      textBoxItem.KeyUp += new KeyEventHandler(this.TextBoxItem_KeyUp);
      textBoxItem.MouseWheel += new MouseEventHandler(this.TextBoxItem_MouseWheel);
    }

    public override bool EndEdit()
    {
      RadTextBoxItem textBoxItem = ((RadTextBoxElement) this.EditorElement).TextBoxItem;
      textBoxItem.TextChanging -= new TextChangingEventHandler(this.TextBoxItem_TextChanging);
      textBoxItem.TextChanged -= new EventHandler(this.TextBoxItem_TextChanged);
      textBoxItem.KeyDown -= new KeyEventHandler(this.TextBoxItem_KeyDown);
      textBoxItem.KeyUp -= new KeyEventHandler(this.TextBoxItem_KeyUp);
      textBoxItem.MouseWheel -= new MouseEventHandler(this.TextBoxItem_MouseWheel);
      textBoxItem.SelectionStart = 0;
      textBoxItem.SelectionLength = 0;
      textBoxItem.Text = "";
      bool flag = base.EndEdit();
      this.originalValue = (object) string.Empty;
      return flag;
    }

    public override void OnKeyDown(KeyEventArgs e)
    {
      if (this.TextBoxEditorElement == null || !this.TextBoxEditorElement.IsInValidState(true))
        return;
      this.selectionStart = this.TextBoxEditorElement.TextBoxItem.SelectionStart;
      this.selectionLength = this.TextBoxEditorElement.TextBoxItem.SelectionLength;
      this.isAtFirstLine = this.TextBoxEditorElement.IsCaretAtFirstLine;
      this.isAtLastLine = this.TextBoxEditorElement.IsCaretAtLastLine;
      switch (e.KeyCode)
      {
        case Keys.Return:
          if (e.Modifiers == Keys.Control)
            break;
          base.OnKeyDown(e);
          break;
        case Keys.Up:
          if (this.Multiline && (this.selectionLength != 0 || !this.isAtFirstLine))
            break;
          base.OnKeyDown(e);
          break;
        case Keys.Down:
          if (this.Multiline && (this.selectionLength != 0 || !this.isAtLastLine))
            break;
          base.OnKeyDown(e);
          break;
      }
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
      RadTextBoxEditorElement editorElement = this.EditorElement as RadTextBoxEditorElement;
      if (editorElement == null || !editorElement.IsInValidState(true))
        return;
      int length = editorElement.Text.Length;
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

    private void TextBoxItem_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void TextBoxItem_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void TextBoxItem_TextChanging(object sender, TextChangingEventArgs e)
    {
      if (this.IsInitalizing)
        return;
      ValueChangingEventArgs e1 = new ValueChangingEventArgs((object) e.NewValue, (object) e.OldValue);
      this.OnValueChanging(e1);
      e.Cancel = e1.Cancel;
    }

    private void TextBoxItem_TextChanged(object sender, EventArgs e)
    {
      if (this.IsInitalizing)
        return;
      this.OnValueChanged();
    }

    private void TextBoxItem_MouseWheel(object sender, MouseEventArgs e)
    {
      this.OnMouseWheel(e);
    }
  }
}
