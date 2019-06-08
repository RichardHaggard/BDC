// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridUITypeEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI
{
  public class PropertyGridUITypeEditor : BaseInputEditor, System.IServiceProvider, IWindowsFormsEditorService
  {
    private PropertyGridDropDownForm dropDownForm;
    private bool closingDropDown;
    private PropertyGridUITypeEditorElement editor;

    public PropertyGridUITypeEditor()
    {
      this.editor = this.CreateEditorElement() as PropertyGridUITypeEditorElement;
    }

    public PropertyGridUITypeEditor(PropertyGridUITypeEditorElement editor)
    {
      this.editor = editor;
    }

    public override void Initialize(object owner, object value)
    {
      this.OwnerElement = (RadElement) (owner as PropertyGridItemElement);
      PropertyGridUITypeEditorElement editorElement = this.EditorElement as PropertyGridUITypeEditorElement;
      PropertyGridItem data = ((PropertyGridItemElementBase) this.OwnerElement).Data as PropertyGridItem;
      editorElement.EditedType = data.PropertyType;
      editorElement.Editor = (UITypeEditor) data.PropertyDescriptor.GetEditor(typeof (UITypeEditor));
      editorElement.Converter = data.TypeConverter;
      editorElement.Text = value != null ? value.ToString() : string.Empty;
      this.Value = value;
      this.originalValue = value;
    }

    public virtual PropertyGridDropDownForm DropDownForm
    {
      get
      {
        if (this.dropDownForm == null)
          this.dropDownForm = new PropertyGridDropDownForm((IWindowsFormsEditorService) this);
        return this.dropDownForm;
      }
    }

    public override object Value
    {
      get
      {
        return this.GetElementValue();
      }
      set
      {
        this.SetElementValue(value);
      }
    }

    public virtual PropertyGridUITypeEditorElement Editor
    {
      get
      {
        return this.editor;
      }
      set
      {
        this.editor = value;
      }
    }

    public object GetService(System.Type serviceType)
    {
      if ((object) serviceType == (object) typeof (IWindowsFormsEditorService))
        return (object) this;
      return (object) null;
    }

    public void CloseDropDown()
    {
      if (this.closingDropDown)
        return;
      try
      {
        this.closingDropDown = true;
        if (!this.DropDownForm.Visible)
          return;
        this.DropDownForm.Component = (Control) null;
        this.DropDownForm.Visible = false;
        this.editor.Focus();
      }
      finally
      {
        this.closingDropDown = false;
      }
    }

    public void DropDownControl(Control control)
    {
      this.DropDownForm.Visible = false;
      this.DropDownForm.Component = control;
      Rectangle bounds = this.editor.Bounds;
      Size size = this.DropDownForm.Size;
      Point screen = this.editor.Parent.PointToScreen(new Point(bounds.Right - size.Width, bounds.Bottom + 1));
      Rectangle workingArea = Screen.FromControl(this.editor.ElementTree.Control).WorkingArea;
      screen.X = Math.Min(workingArea.Right - size.Width, Math.Max(workingArea.X, screen.X));
      if (size.Height + screen.Y + this.editor.Size.Height > workingArea.Bottom)
        screen.Y = screen.Y - size.Height - bounds.Height - 1;
      this.DropDownForm.SetBounds(screen.X, screen.Y, size.Width, size.Height);
      this.DropDownForm.Visible = true;
      control.Focus();
      this.editor.SelectTextBox();
      while (this.DropDownForm.Visible)
      {
        Application.DoEvents();
        Telerik.WinControls.NativeMethods.MsgWaitForMultipleObjects(0, 0, true, 250, (int) byte.MaxValue);
      }
    }

    public void HideForm()
    {
      if (!this.DropDownForm.Visible)
        return;
      this.DropDownForm.Visible = false;
    }

    public DialogResult ShowDialog(Form dialog)
    {
      int num = (int) dialog.ShowDialog((IWin32Window) this.editor.ElementTree.Control);
      return dialog.DialogResult;
    }

    public override System.Type DataType
    {
      get
      {
        return typeof (object);
      }
    }

    protected override RadElement CreateEditorElement()
    {
      return (RadElement) new PropertyGridUITypeEditorElement(this.DataType);
    }

    private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    protected virtual void OnKeyPress(KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      PropertyGridItemElement ownerElement = this.OwnerElement as PropertyGridItemElement;
      if (ownerElement == null || !ownerElement.IsInValidState(true) || ownerElement.ElementTree.Control.Focused)
        return;
      ownerElement.PropertyTableElement.EndEdit();
    }

    protected virtual object GetElementValue()
    {
      return (this.EditorElement as PropertyGridUITypeEditorElement).Value;
    }

    protected virtual void SetElementValue(object value)
    {
      (this.EditorElement as PropertyGridUITypeEditorElement).Value = value;
    }

    public override void BeginEdit()
    {
      base.BeginEdit();
      (this.EditorElement as PropertyGridUITypeEditorElement).KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
    }

    public override bool EndEdit()
    {
      (this.EditorElement as PropertyGridUITypeEditorElement).KeyPress -= new KeyPressEventHandler(this.TextBox_KeyPress);
      this.HideForm();
      this.CloseDropDown();
      return base.EndEdit();
    }
  }
}
