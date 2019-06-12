// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TimeCellFormattingEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class TimeCellFormattingEventArgs : EventArgs
  {
    private int column;
    private int row;
    private TimeTableVisualElement element;
    private bool isMinute;

    public TimeCellFormattingEventArgs(
      int column,
      int row,
      TimeTableVisualElement element,
      bool isMinute)
    {
      this.column = column;
      this.row = row;
      this.element = element;
      this.isMinute = isMinute;
    }

    public bool IsMinute
    {
      get
      {
        return this.isMinute;
      }
    }

    public TimeTableVisualElement Element
    {
      get
      {
        return this.element;
      }
      set
      {
        this.element = value;
      }
    }

    public int Row
    {
      get
      {
        return this.row;
      }
    }

    public int Column
    {
      get
      {
        return this.column;
      }
    }
  }
}
