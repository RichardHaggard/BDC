// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewIndentColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewIndentColumn : GridViewColumn
  {
    private int indentLevel;

    public GridViewIndentColumn(GridViewTemplate template, int indentLevel)
    {
      this.OwnerTemplate = template;
      this.SuspendPropertyNotifications();
      int num1 = (int) this.SetDefaultValueOverride(GridViewColumn.PinPositionProperty, (object) PinnedColumnPosition.Left);
      int num2 = (int) this.SetDefaultValueOverride(GridViewColumn.WidthProperty, (object) -1);
      int num3 = (int) this.SetDefaultValueOverride(GridViewColumn.MinWidthProperty, (object) 0);
      this.ResumePropertyNotifications();
      this.indentLevel = indentLevel;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the indent level for this column.")]
    public int IndentLevel
    {
      get
      {
        return this.indentLevel;
      }
      internal set
      {
        this.indentLevel = value;
      }
    }

    internal override bool CanStretch
    {
      get
      {
        return false;
      }
    }

    public override Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewTableHeaderRowInfo)
        return typeof (GridHeaderIndentCellElement);
      return typeof (GridIndentCellElement);
    }
  }
}
