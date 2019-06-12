// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCheckBoxEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadCheckBoxEditorElement : RadItem
  {
    private RadCheckmark checkmark;
    private RadCheckBoxEditor editor;
    private Keys downKeyCode;
    private bool checkBoxClicked;

    public RadCheckBoxEditorElement(RadCheckBoxEditor editor)
    {
      this.editor = editor;
    }

    static RadCheckBoxEditorElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (RadCheckBoxEditorElement));
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

    private GridViewEditManager GridViewEditorManager
    {
      get
      {
        GridCellElement parent = this.Parent as GridCellElement;
        if (parent != null && parent.IsInValidState(true))
          return parent.GridViewElement.EditorManager;
        return this.editor.EditorManager as GridViewEditManager;
      }
    }

    private BaseGridBehavior GridBehavior
    {
      get
      {
        GridCellElement parent = this.Parent as GridCellElement;
        if (parent != null)
          return parent.GridViewElement.GridBehavior as BaseGridBehavior;
        return (BaseGridBehavior) null;
      }
    }

    private bool IsInEditMode
    {
      get
      {
        if (this.GridViewEditorManager != null)
          return this.GridViewEditorManager.ActiveEditor == this.editor;
        return false;
      }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (this.IsInEditMode)
      {
        switch (e.KeyCode)
        {
          case Keys.Return:
            this.GridBehavior.ProcessKeyDown(e);
            return;
          case Keys.Escape:
            this.GridViewEditorManager.CancelEdit();
            return;
        }
      }
      else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Return)
        this.GridViewEditorManager.BeginEdit();
      if (this.IsInEditMode && e.KeyCode == Keys.Space)
        this.editor.ToggleState();
      else if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
        this.editor.RaiseKeyDown(e);
      this.downKeyCode = e.KeyCode;
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
      RadElement parent = this.Parent;
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
      if (e.Button != MouseButtons.Left || !this.checkBoxClicked)
        return;
      RadElement ownerElement = this.editor.OwnerElement;
      if (this.GridViewEditorManager != null && !this.IsInEditMode)
        this.GridViewEditorManager.BeginEdit();
      if (!this.IsInEditMode || e.Button != MouseButtons.Left)
        return;
      this.editor.ToggleState();
    }

    private bool CheckBoxClicked(Point point)
    {
      return this.Checkmark.ControlBoundingRectangle.Contains(point);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.editor.OnMouseWheel(e);
    }
  }
}
