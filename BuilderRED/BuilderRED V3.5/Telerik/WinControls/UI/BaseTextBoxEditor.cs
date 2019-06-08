// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseTextBoxEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class BaseTextBoxEditor : BaseInputEditor
  {
    private string nullValue = string.Empty;
    protected int selectionStart;
    protected int selectionLength;
    protected bool isAtFirstLine;
    protected bool isAtLastLine;
    private bool isModified;

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

    protected BaseTextBoxEditorElement TextBoxEditorElement
    {
      get
      {
        return this.EditorElement as BaseTextBoxEditorElement;
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

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new BaseTextBoxEditorElement();
    }

    internal static bool IsDarkTheme(RadElement ownerElement)
    {
      string themeName = string.Empty;
      if (ownerElement != null && ownerElement.ElementTree != null)
        themeName = ownerElement.ElementTree.ThemeName;
      return TelerikHelper.IsDarkTheme(themeName);
    }

    public override void Initialize(object owner, object value)
    {
      this.isModified = false;
      base.Initialize(owner, value);
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadTextBoxItem textBoxItem = this.TextBoxEditorElement.TextBoxItem;
      if (!BaseTextBoxEditor.IsDarkTheme(this.OwnerElement))
      {
        int num = (int) this.TextBoxEditorElement.SetDefaultValueOverride(VisualElement.BackColorProperty, (object) Color.White);
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
      textBoxItem.HostedControl.LostFocus += new EventHandler(this.HostedControl_LostFocus);
    }

    public override bool EndEdit()
    {
      RadTextBoxItem textBoxItem = this.TextBoxEditorElement.TextBoxItem;
      textBoxItem.TextChanging -= new TextChangingEventHandler(this.TextBoxItem_TextChanging);
      textBoxItem.TextChanged -= new EventHandler(this.TextBoxItem_TextChanged);
      textBoxItem.KeyDown -= new KeyEventHandler(this.TextBoxItem_KeyDown);
      textBoxItem.HostedControl.LostFocus -= new EventHandler(this.HostedControl_LostFocus);
      textBoxItem.SelectionStart = 0;
      textBoxItem.SelectionLength = 0;
      textBoxItem.Text = "";
      return base.EndEdit();
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
    }

    protected virtual void OnLostFocus()
    {
    }

    private void TextBoxItem_KeyDown(object sender, KeyEventArgs e)
    {
      if (this.TextBoxEditorElement == null || !this.TextBoxEditorElement.IsInValidState(true))
        return;
      this.selectionStart = this.TextBoxEditorElement.TextBoxItem.SelectionStart;
      this.selectionLength = this.TextBoxEditorElement.TextBoxItem.SelectionLength;
      this.isAtFirstLine = this.TextBoxEditorElement.IsCaretAtFirstLine;
      this.isAtLastLine = this.TextBoxEditorElement.IsCaretAtLastLine;
      this.OnKeyDown(e);
    }

    private void TextBoxItem_TextChanging(object sender, TextChangingEventArgs e)
    {
      if (this.IsInitalizing)
        return;
      ValueChangingEventArgs e1 = new ValueChangingEventArgs((object) e.NewValue, (object) e.OldValue);
      this.OnValueChanging(e1);
      e.Cancel = e1.Cancel || e.Cancel;
    }

    private void TextBoxItem_TextChanged(object sender, EventArgs e)
    {
      if (this.IsInitalizing)
        return;
      this.OnValueChanged();
    }

    private void HostedControl_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus();
    }
  }
}
