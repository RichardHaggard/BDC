// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewHyperlinkColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewHyperlinkColumn : GridViewDataColumn
  {
    public static RadProperty HyperlinkOpenActionProperty = RadProperty.Register(nameof (HyperlinkOpenAction), typeof (HyperlinkOpenAction), typeof (GridViewHyperlinkColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) HyperlinkOpenAction.SingleClick));
    public static RadProperty HyperlinkOpenAreaProperty = RadProperty.Register(nameof (HyperlinkOpenArea), typeof (HyperlinkOpenArea), typeof (GridViewHyperlinkColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) HyperlinkOpenArea.Text));

    public GridViewHyperlinkColumn()
    {
      int num = (int) this.SetDefaultValueOverride(GridViewColumn.ReadOnlyProperty, (object) true);
    }

    public GridViewHyperlinkColumn(string fieldName)
      : base(fieldName)
    {
      int num = (int) this.SetDefaultValueOverride(GridViewColumn.ReadOnlyProperty, (object) true);
    }

    public GridViewHyperlinkColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
      int num = (int) this.SetDefaultValueOverride(GridViewColumn.ReadOnlyProperty, (object) true);
    }

    [DefaultValue(true)]
    public override bool ReadOnly
    {
      get
      {
        return base.ReadOnly;
      }
      set
      {
        base.ReadOnly = value;
      }
    }

    [DefaultValue(HyperlinkOpenAction.SingleClick)]
    [Description("Defines the action for opening of a link.")]
    public HyperlinkOpenAction HyperlinkOpenAction
    {
      get
      {
        return (HyperlinkOpenAction) this.GetValue(GridViewHyperlinkColumn.HyperlinkOpenActionProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewHyperlinkColumn.HyperlinkOpenActionProperty, (object) value);
      }
    }

    [Description("Defines the active link area.")]
    [DefaultValue(HyperlinkOpenArea.Text)]
    public HyperlinkOpenArea HyperlinkOpenArea
    {
      get
      {
        return (HyperlinkOpenArea) this.GetValue(GridViewHyperlinkColumn.HyperlinkOpenAreaProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewHyperlinkColumn.HyperlinkOpenAreaProperty, (object) value);
      }
    }

    public override IInputEditor GetDefaultEditor()
    {
      return (IInputEditor) new RadTextBoxEditor();
    }

    public override Type GetDefaultEditorType()
    {
      return typeof (RadTextBoxEditor);
    }

    public override Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewDataRowInfo || row is GridViewNewRowInfo)
        return typeof (GridHyperlinkCellElement);
      return base.GetCellType(row);
    }
  }
}
