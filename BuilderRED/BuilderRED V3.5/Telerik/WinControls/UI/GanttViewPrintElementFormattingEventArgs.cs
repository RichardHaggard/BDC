// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewPrintElementFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GanttViewPrintElementFormattingEventArgs : EventArgs
  {
    private GanttViewPrintElement printElement;
    private GanttViewPrintElementContext printContext;
    private object dataContext;
    private string columnName;

    public GanttViewPrintElementFormattingEventArgs(
      GanttViewPrintElementContext context,
      GanttViewPrintElement printElement)
      : this(context, printElement, (object) null)
    {
    }

    public GanttViewPrintElementFormattingEventArgs(
      GanttViewPrintElementContext context,
      GanttViewPrintElement printElement,
      object dataContext)
      : this(context, printElement, dataContext, (string) null)
    {
    }

    public GanttViewPrintElementFormattingEventArgs(
      GanttViewPrintElementContext context,
      GanttViewPrintElement printElement,
      object dataContext,
      string columnName)
    {
      this.printContext = context;
      this.printElement = printElement;
      this.dataContext = dataContext;
      this.columnName = columnName;
    }

    public GanttViewPrintElement PrintElement
    {
      get
      {
        return this.printElement;
      }
    }

    public object DataContext
    {
      get
      {
        return this.dataContext;
      }
    }

    public GanttViewPrintElementContext PrintContext
    {
      get
      {
        return this.printContext;
      }
    }

    public string ColumnName
    {
      get
      {
        return this.columnName;
      }
    }
  }
}
