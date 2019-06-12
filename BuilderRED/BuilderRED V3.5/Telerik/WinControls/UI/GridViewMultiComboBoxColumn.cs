// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewMultiComboBoxColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewMultiComboBoxColumn : GridViewComboBoxColumn
  {
    public GridViewMultiComboBoxColumn()
    {
    }

    public GridViewMultiComboBoxColumn(string fieldName)
      : base(fieldName)
    {
    }

    public GridViewMultiComboBoxColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
    }

    public override IInputEditor GetDefaultEditor()
    {
      return (IInputEditor) new RadMultiColumnComboBoxElement();
    }

    public override void InitializeEditor(IInputEditor editor)
    {
      RadMultiColumnComboBoxElement columnComboBoxElement = editor as RadMultiColumnComboBoxElement;
      if (columnComboBoxElement == null)
        return;
      if (columnComboBoxElement.EditorControl.BindingContext != columnComboBoxElement.ElementTree.Control.BindingContext)
        columnComboBoxElement.EditorControl.BindingContext = columnComboBoxElement.ElementTree.Control.BindingContext;
      if (columnComboBoxElement.EditorElement.Parent is GridFilterCellElement && this.FilteringMode == GridViewFilteringMode.DisplayMember)
      {
        columnComboBoxElement.DataSource = (object) null;
        columnComboBoxElement.ValueMember = this.DisplayMember;
        columnComboBoxElement.DisplayMember = this.DisplayMember;
        columnComboBoxElement.DataSource = this.DataSource;
      }
      else if (columnComboBoxElement.DataSource != this.DataSource || columnComboBoxElement.DisplayMember != this.DisplayMember || columnComboBoxElement.ValueMember != this.ValueMember)
      {
        columnComboBoxElement.BeginUpdate();
        columnComboBoxElement.DataSource = (object) null;
        columnComboBoxElement.DisplayMember = this.DisplayMember;
        columnComboBoxElement.ValueMember = this.ValueMember;
        columnComboBoxElement.DataSource = this.DataSource;
        columnComboBoxElement.EndUpdate();
      }
      columnComboBoxElement.BeginUpdate();
      columnComboBoxElement.AutoCompleteMode = this.AutoCompleteMode;
      columnComboBoxElement.DropDownStyle = this.DropDownStyle;
      columnComboBoxElement.EditorControl.CurrentRow = (GridViewRowInfo) null;
      columnComboBoxElement.EndUpdate();
    }

    public override Type GetDefaultEditorType()
    {
      return typeof (RadMultiColumnComboBoxElement);
    }
  }
}
