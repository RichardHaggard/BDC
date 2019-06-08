// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewBrowseColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewBrowseColumn : GridViewDataColumn
  {
    public GridViewBrowseColumn()
      : this(string.Empty, string.Empty)
    {
    }

    public GridViewBrowseColumn(string fieldName)
      : this(string.Empty, fieldName)
    {
    }

    public GridViewBrowseColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
    }

    public override IInputEditor GetDefaultEditor()
    {
      return (IInputEditor) new GridBrowseEditor();
    }

    public override Type GetDefaultEditorType()
    {
      return typeof (GridBrowseEditor);
    }

    public override Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewDataRowInfo || row is GridViewNewRowInfo)
        return typeof (GridBrowseCellElement);
      return base.GetCellType(row);
    }
  }
}
