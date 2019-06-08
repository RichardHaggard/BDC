// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewDataGroupEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridViewDataGroupEventArgs : EventArgs
  {
    private int groupCount;
    private string[] headerText;
    private int[] rowCount;
    private GridGroupByExpression expression;

    public GridViewDataGroupEventArgs(GridGroupByExpression expression, int groupCount)
    {
      this.expression = expression;
      this.groupCount = groupCount;
      if (this.groupCount > 0)
      {
        this.headerText = new string[groupCount];
        this.rowCount = new int[groupCount];
      }
      else
      {
        this.headerText = new string[0];
        this.rowCount = new int[0];
      }
    }

    public int GroupCount
    {
      get
      {
        return this.groupCount;
      }
      set
      {
        if (this.groupCount == value)
          return;
        this.groupCount = value;
        if (this.groupCount > 0)
        {
          this.headerText = new string[this.groupCount];
          this.rowCount = new int[this.groupCount];
        }
        else
        {
          this.headerText = new string[0];
          this.rowCount = new int[0];
        }
      }
    }

    internal int TotalRowCount
    {
      get
      {
        int num = 0;
        for (int index = 0; index < this.RowCount.Length; ++index)
          num += this.RowCount[index];
        return num;
      }
    }

    public GridGroupByExpression Expression
    {
      get
      {
        return this.expression;
      }
    }

    public string[] HeaderText
    {
      get
      {
        return this.headerText;
      }
    }

    public int[] RowCount
    {
      get
      {
        return this.rowCount;
      }
    }
  }
}
