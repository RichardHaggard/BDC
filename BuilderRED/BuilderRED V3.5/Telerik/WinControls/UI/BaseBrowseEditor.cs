// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseBrowseEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class BaseBrowseEditor : BaseInputEditor
  {
    protected System.Type valueType = typeof (string);

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new RadBrowseEditorElement();
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (string);
      }
    }

    public override object Value
    {
      get
      {
        return (object) this.GetEditorValue();
      }
      set
      {
        this.SetEditorValue(value);
      }
    }

    public override bool IsModified
    {
      get
      {
        if (this.originalValue == null)
          return !string.IsNullOrEmpty(((RadItem) this.EditorElement).Text);
        return this.originalValue.ToString() != ((RadItem) this.EditorElement).Text;
      }
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      RadBrowseEditorElement editorElement = this.EditorElement as RadBrowseEditorElement;
      editorElement.ValueChanging += new ValueChangingEventHandler(this.editorElement_ValueChanging);
      editorElement.ValueChanged += new EventHandler(this.editorElement_ValueChanged);
      editorElement.KeyDown += new KeyEventHandler(this.editorElement_KeyDown);
      editorElement.KeyUp += new KeyEventHandler(this.editorElement_KeyUp);
      editorElement.TextBoxItem.SelectAll();
      editorElement.TextBoxItem.TextBoxControl.Focus();
      editorElement.RightToLeft = this.RightToLeft;
      editorElement.TextBoxItem.HostedControl.LostFocus += new EventHandler(this.HostedControl_LostFocus);
    }

    public override bool EndEdit()
    {
      base.EndEdit();
      RadBrowseEditorElement editorElement = this.EditorElement as RadBrowseEditorElement;
      editorElement.ValueChanging -= new ValueChangingEventHandler(this.editorElement_ValueChanging);
      editorElement.ValueChanged -= new EventHandler(this.editorElement_ValueChanged);
      editorElement.KeyDown -= new KeyEventHandler(this.editorElement_KeyDown);
      editorElement.KeyUp -= new KeyEventHandler(this.editorElement_KeyUp);
      editorElement.TextBoxItem.HostedControl.LostFocus -= new EventHandler(this.HostedControl_LostFocus);
      return true;
    }

    protected virtual void OnLostFocus()
    {
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
    }

    protected virtual void OnOpenFileDialogOpening(OpenFileDialogOpeningEventArgs e)
    {
    }

    private void HostedControl_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus();
    }

    private void editorElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void editorElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void editorElement_ValueChanged(object sender, EventArgs e)
    {
      this.OnValueChanged();
    }

    private void editorElement_ValueChanging(object sender, ValueChangingEventArgs e)
    {
      this.OnValueChanging(e);
    }

    private string GetEditorValue()
    {
      return (this.EditorElement as RadBrowseEditorElement).Value;
    }

    private void SetEditorValue(object value)
    {
      RadBrowseEditorElement editorElement = this.EditorElement as RadBrowseEditorElement;
      editorElement.Value = value == null || string.IsNullOrEmpty(value.ToString()) ? string.Empty : value.ToString();
      this.originalValue = (object) editorElement.Value;
    }
  }
}
