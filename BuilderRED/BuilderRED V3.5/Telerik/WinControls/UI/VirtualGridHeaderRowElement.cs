﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridHeaderRowElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class VirtualGridHeaderRowElement : VirtualGridRowElement
  {
    protected override int MeasureRowHeight(SizeF availableSize)
    {
      return this.TableElement.HeaderRowHeight;
    }

    public override bool IsCompatible(int data, object context)
    {
      return data == -1;
    }

    public override bool CanApplyAlternatingColor
    {
      get
      {
        return false;
      }
    }
  }
}
