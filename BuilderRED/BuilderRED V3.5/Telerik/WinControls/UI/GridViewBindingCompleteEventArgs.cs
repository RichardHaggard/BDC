﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewBindingCompleteEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewBindingCompleteEventArgs : EventArgs
  {
    private ListChangedType listChangedType;

    public GridViewBindingCompleteEventArgs(ListChangedType listChangedType)
    {
      this.listChangedType = listChangedType;
    }

    public ListChangedType ListChangedType
    {
      get
      {
        return this.listChangedType;
      }
    }
  }
}
