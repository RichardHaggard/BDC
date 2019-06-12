// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class DataFilterEditorElement : LightVisualElement
  {
    private IInputEditor editor;
    private BaseDataFilterNodeElement dataFilterNodeElement;

    public DataFilterEditorElement(BaseDataFilterNodeElement dataFilterNodeElement)
    {
      this.dataFilterNodeElement = dataFilterNodeElement;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = false;
      this.DrawBorder = false;
      this.DrawText = true;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.ChangeCursorOnMouseOver = true;
    }

    protected internal IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
    }

    protected internal virtual System.Type EditorType { get; set; }

    public bool ChangeCursorOnMouseOver { get; set; }

    public BaseDataFilterNodeElement DataFilterNodeElement
    {
      get
      {
        return this.dataFilterNodeElement;
      }
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      RadDataFilterElement dataFilterElement = this.DataFilterNodeElement.DataFilterElement;
      if (dataFilterElement == null)
        return;
      this.DataFilterNodeElement.EditingElement = this;
      dataFilterElement.currentNodeEditorType = this.EditorType;
      dataFilterElement.SelectedNode = this.DataFilterNodeElement.Data;
      dataFilterElement.SelectedNode.Value = this.DataFilterNodeElement.GetCurrentEditingElementValue();
      dataFilterElement.BeginEdit();
    }

    public virtual void Synchronize(DataFilterCriteriaNode criteriaNode)
    {
    }

    internal virtual void AddEditor(IInputEditor editor)
    {
      if (editor == null)
        return;
      this.editor = editor;
      this.Children.Add((RadElement) this.GetEditorElement((IValueEditor) editor));
    }

    internal virtual void RemoveEditor()
    {
      if (this.editor == null)
        return;
      RadDataFilterElement dataFilterElement = this.FindAncestor<BaseDataFilterNodeElement>().DataFilterElement;
      if (dataFilterElement != null)
        dataFilterElement.currentNodeEditorType = (System.Type) null;
      this.Children.Remove((RadElement) this.GetEditorElement((IValueEditor) this.editor));
      this.editor = (IInputEditor) null;
    }

    protected internal RadItem GetEditorElement(IValueEditor editor)
    {
      BaseInputEditor editor1 = this.Editor as BaseInputEditor;
      if (editor1 != null)
        return editor1.EditorElement as RadItem;
      return editor as RadItem;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF availableSize1 = base.MeasureOverride(availableSize);
      BaseDataFilterNodeElement ancestor = this.FindAncestor<BaseDataFilterNodeElement>();
      if (ancestor == null)
        return availableSize1;
      SizeF editorSize = ancestor.GetEditorSize(availableSize1, this);
      this.GetEditorElement((IValueEditor) this.Editor)?.Measure(editorSize);
      return editorSize;
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      if (!this.ChangeCursorOnMouseOver)
        return;
      this.DataFilterNodeElement.SetControlCursor(Cursors.Hand);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (!this.ChangeCursorOnMouseOver)
        return;
      this.DataFilterNodeElement.SetControlCursor(Cursors.Default);
    }
  }
}
