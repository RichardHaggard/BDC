// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewDataErrorEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewDataErrorEventArgs : CancelEventArgs
  {
    private GridViewDataErrorContexts context;
    private Exception exception;
    private bool throwException;
    private int columnIndex;
    private int rowIndex;

    public GridViewDataErrorEventArgs(
      Exception exception,
      int columnIndex,
      int rowIndex,
      GridViewDataErrorContexts context)
    {
      this.exception = exception;
      this.context = context;
      this.columnIndex = columnIndex;
      this.rowIndex = rowIndex;
    }

    public int ColumnIndex
    {
      get
      {
        return this.columnIndex;
      }
    }

    public int RowIndex
    {
      get
      {
        return this.rowIndex;
      }
    }

    public GridViewDataErrorContexts Context
    {
      get
      {
        return this.context;
      }
    }

    public Exception Exception
    {
      get
      {
        return this.exception;
      }
    }

    public bool ThrowException
    {
      get
      {
        return this.throwException;
      }
      set
      {
        if (value && this.exception == null)
          throw new ArgumentException("RadGridView_CannotThrowNullException");
        this.throwException = value;
      }
    }
  }
}
