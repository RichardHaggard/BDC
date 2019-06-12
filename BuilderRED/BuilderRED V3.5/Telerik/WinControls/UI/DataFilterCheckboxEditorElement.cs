// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterCheckboxEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterCheckboxEditorElement : RadItem
  {
    private RadCheckmark checkmark;
    private DataFilterCheckboxEditor editor;
    private Keys downKeyCode;
    private bool checkBoxClicked;

    public DataFilterCheckboxEditorElement(DataFilterCheckboxEditor editor)
    {
      this.editor = editor;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.CanFocus = true;
      this.NotifyParentOnMouseInput = true;
      this.Alignment = ContentAlignment.MiddleCenter;
    }

    protected override void CreateChildElements()
    {
      this.checkmark = new RadCheckmark();
      this.checkmark.NotifyParentOnMouseInput = true;
      this.checkmark.ShouldHandleMouseInput = true;
      this.checkmark.EnableVisualStates = true;
      this.checkmark.Alignment = ContentAlignment.MiddleCenter;
      this.Children.Add((RadElement) this.checkmark);
    }

    public RadCheckmark Checkmark
    {
      get
      {
        return this.checkmark;
      }
    }

    public Telerik.WinControls.Enumerations.ToggleState CheckState
    {
      get
      {
        return this.checkmark.CheckState;
      }
      set
      {
        this.checkmark.CheckState = value;
      }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      TreeNodeElement ownerElement = this.editor.OwnerElement as TreeNodeElement;
      if (ownerElement != null)
      {
        if (e.KeyCode == Keys.Return)
          ownerElement.TreeViewElement.EndEdit();
        else if (e.KeyCode == Keys.Escape)
          ownerElement.TreeViewElement.CancelEdit();
      }
      if (e.KeyCode == Keys.Space)
        this.editor.ToggleState();
      else if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
        this.editor.RaiseKeyDown(e);
      this.downKeyCode = e.KeyCode;
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
      if (this.downKeyCode == e.KeyCode && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
        this.editor.RaiseKeyDown(e);
      this.downKeyCode = Keys.None;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.checkBoxClicked = this.CheckBoxClicked(e.Location);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (e.Button != MouseButtons.Left || !this.checkBoxClicked || e.Button != MouseButtons.Left)
        return;
      this.editor.ToggleState();
    }

    private bool CheckBoxClicked(Point point)
    {
      return this.Checkmark.ControlBoundingRectangle.Contains(point);
    }
  }
}
