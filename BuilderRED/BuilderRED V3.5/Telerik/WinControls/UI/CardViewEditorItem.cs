// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardViewEditorItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class CardViewEditorItem : LightVisualElement
  {
    private IInputEditor editor;

    protected internal IInputEditor Editor
    {
      get
      {
        return this.editor;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawFill = false;
      this.DrawBorder = false;
      this.DrawText = true;
      this.TextAlignment = ContentAlignment.MiddleLeft;
      this.MinSize = new Size(46, 26);
      this.Bounds = new Rectangle(0, 0, 100, 100);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      RadCardView control = this.ElementTree.Control as RadCardView;
      CardListViewVisualItem ancestor = this.FindAncestor<CardListViewVisualItem>();
      CardViewItem parent = this.Parent as CardViewItem;
      if (control == null || ancestor == null || parent == null)
        return;
      if (parent.CardField != null)
        parent.CardField.Current = true;
      if (this.Editor != null)
        return;
      control.SelectedItem = ancestor.Data;
      ancestor.EditingItem = parent;
      control.ListViewElement.BeginEdit();
    }

    internal void AddEditor(IInputEditor editor)
    {
      if (editor == null)
        return;
      this.editor = editor;
      this.Children.Add((RadElement) this.GetEditorElement((IValueEditor) this.editor));
    }

    internal void RemoveEditor()
    {
      if (this.editor == null)
        return;
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
      CardViewItem parent = this.Parent as CardViewItem;
      if (parent == null)
        return base.MeasureOverride(availableSize);
      return parent.GetEditorSize(availableSize).Size;
    }
  }
}
