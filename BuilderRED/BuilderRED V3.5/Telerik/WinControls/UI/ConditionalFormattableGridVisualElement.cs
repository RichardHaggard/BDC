// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ConditionalFormattableGridVisualElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public abstract class ConditionalFormattableGridVisualElement : GridVisualElement
  {
    private BaseFormattingObject formattingObject;

    protected internal BaseFormattingObject FormattingObject
    {
      get
      {
        return this.formattingObject;
      }
    }

    [Browsable(false)]
    public virtual bool SupportsConditionalFormatting
    {
      get
      {
        return false;
      }
    }

    protected void SetFormattingObject(BaseFormattingObject format)
    {
      if (!this.SupportsConditionalFormatting || this.formattingObject == null && format == null)
        return;
      BaseFormattingObject formattingObject = this.formattingObject;
      this.formattingObject = format;
      this.SuspendPropertyNotifications();
      this.NotifyFormatChanged(formattingObject);
      this.ResumePropertyNotifications();
    }

    protected virtual void NotifyFormatChanged(BaseFormattingObject oldFormat)
    {
      this.Invalidate();
    }

    protected virtual void SetFormattingObjectProperties(
      BaseFormattingObject source,
      BaseFormattingObject target)
    {
      if (source == null || target == null)
        return;
      target.Enabled = source.Enabled;
      target.ApplyToRow = source.ApplyToRow;
      target.ApplyOnSelectedRows = source.ApplyOnSelectedRows;
      if (source.IsValueSet("CellBackColor"))
        target.cellBackColor = source.CellBackColor;
      if (source.IsValueSet("CellForeColor"))
        target.cellForeColor = source.CellForeColor;
      if (source.IsValueSet("TextAlignment"))
        target.textAlignment = source.TextAlignment;
      if (source.IsValueSet("RowBackColor"))
        target.rowBackColor = source.RowBackColor;
      if (source.IsValueSet("RowForeColor"))
        target.rowForeColor = source.RowForeColor;
      if (source.IsValueSet("RowTextAlignment"))
        target.rowTextAlignment = source.RowTextAlignment;
      if (source.IsValueSet("CellFont"))
        target.cellFont = source.CellFont;
      if (!source.IsValueSet("RowFont"))
        return;
      target.rowFont = source.RowFont;
    }

    protected virtual void UnsetFormattingObjectProperties(
      BaseFormattingObject source,
      BaseFormattingObject target)
    {
      if (source == null || target == null)
        return;
      if (source.IsValueSet("CellBackColor"))
        target.cellBackColor = Color.Empty;
      if (source.IsValueSet("CellForeColor"))
        target.cellForeColor = Color.Empty;
      if (source.IsValueSet("TextAlignment"))
        target.textAlignment = ContentAlignment.MiddleLeft;
      if (source.IsValueSet("RowBackColor"))
        target.rowBackColor = Color.Empty;
      if (source.IsValueSet("RowForeColor"))
        target.rowForeColor = Color.Empty;
      if (source.IsValueSet("RowTextAlignment"))
        target.rowTextAlignment = ContentAlignment.MiddleLeft;
      if (source.IsValueSet("CellFont"))
        target.CellFont = (Font) null;
      if (!source.IsValueSet("RowFont"))
        return;
      target.rowFont = (Font) null;
    }
  }
}
