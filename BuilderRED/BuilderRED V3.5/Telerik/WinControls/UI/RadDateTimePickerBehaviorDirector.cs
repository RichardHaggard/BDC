﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDateTimePickerBehaviorDirector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public abstract class RadDateTimePickerBehaviorDirector
  {
    public abstract RadMaskedEditBoxElement TextBoxElement { get; }

    public abstract void CreateChildren();

    public abstract void SetDateByValue(DateTime? date, DateTimePickerFormat formatType);

    public abstract RadDateTimePickerElement DateTimePickerElement { get; }
  }
}
