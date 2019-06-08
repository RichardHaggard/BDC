// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewDataItemAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Reflection;

namespace Telerik.WinControls.UI
{
  internal class GanttViewDataItemAccessor
  {
    private GanttViewTextViewColumn column;
    private bool isSubPropertyMode;

    public GanttViewDataItemAccessor(GanttViewTextViewColumn column)
    {
      if (column == null)
        throw new ArgumentException("Column argument can not be null.");
      this.column = column;
      this.isSubPropertyMode = !string.IsNullOrEmpty(this.column.FieldName) && this.column.FieldName.Contains(".");
    }

    public bool IsSubPropertyMode
    {
      get
      {
        return this.isSubPropertyMode;
      }
    }

    public GanttViewTextViewColumn Column
    {
      get
      {
        return this.column;
      }
    }

    public RadGanttViewElement GanttViewElement
    {
      get
      {
        return this.Column.Owner;
      }
    }

    public virtual object this[GanttViewDataItem item]
    {
      get
      {
        if (item.Cache[this.Column] == null)
        {
          PropertyInfo property = item.GetType().GetProperty(this.Column.FieldName);
          if ((object) property != null)
          {
            object obj = property.GetValue((object) item, (object[]) null);
            this.SetUnboundValue(item, obj);
          }
        }
        return item.Cache[this.Column];
      }
      set
      {
        this.SetUnboundValue(item, value);
      }
    }

    private void SetUnboundValue(GanttViewDataItem item, object value)
    {
      if (item == null)
        return;
      if (this.GanttViewElement != null)
        this.GanttViewElement.Items.OnCollectionItemChanging(item);
      item.Cache[this.column] = value;
      PropertyInfo property = item.GetType().GetProperty(this.column.FieldName);
      if ((object) property != null)
        property.SetValue((object) item, value, (object[]) null);
      if (this.GanttViewElement == null)
        return;
      this.GanttViewElement.Items.OnCollectionItemChanged(item);
    }
  }
}
