﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnsState
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class ColumnsState
  {
    private Dictionary<GridViewColumn, bool> allowResizeState;

    public ColumnsState()
    {
      this.allowResizeState = new Dictionary<GridViewColumn, bool>();
    }

    public Dictionary<GridViewColumn, bool> AllowResizeState
    {
      get
      {
        return this.allowResizeState;
      }
      set
      {
        this.allowResizeState = value;
      }
    }
  }
}