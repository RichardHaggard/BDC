﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCalculatorEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class RadCalculatorEditorElement : RadCalculatorDropDownElement
  {
    public RadCalculatorEditorElement()
    {
      int num = (int) this.EditorContentElement.SetDefaultValueOverride(RadElement.AlignmentProperty, (object) ContentAlignment.MiddleRight);
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadCalculatorDropDownElement);
      }
    }
  }
}
