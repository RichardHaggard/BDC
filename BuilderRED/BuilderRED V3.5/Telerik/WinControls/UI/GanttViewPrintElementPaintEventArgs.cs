// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewPrintElementPaintEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GanttViewPrintElementPaintEventArgs : EventArgs
  {
    private Graphics graphics;
    private RectangleF rectangle;
    private GanttViewPrintElementContext printContext;
    private GanttViewPrintElement printElement;
    private object dataContext;
    private string columnName;

    public GanttViewPrintElementPaintEventArgs(
      Graphics g,
      RectangleF rect,
      GanttViewPrintElementContext printContext,
      GanttViewPrintElement printElement,
      object dataContext)
      : this(g, rect, printContext, printElement, dataContext, (string) null)
    {
    }

    public GanttViewPrintElementPaintEventArgs(
      Graphics g,
      RectangleF rect,
      GanttViewPrintElementContext printContext,
      GanttViewPrintElement printElement,
      object dataContext,
      string columnName)
    {
      this.graphics = g;
      this.rectangle = rect;
      this.printContext = printContext;
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

    public GanttViewPrintElementContext PrintContext
    {
      get
      {
        return this.printContext;
      }
    }

    public object DataContext
    {
      get
      {
        return this.dataContext;
      }
    }

    public RectangleF Rectangle
    {
      get
      {
        return this.rectangle;
      }
    }

    public Graphics Graphics
    {
      get
      {
        return this.graphics;
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
