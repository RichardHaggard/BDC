﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IPickerContentElementOwner
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Globalization;

namespace Telerik.WinControls.UI
{
  public interface IPickerContentElementOwner
  {
    void CloseOwnerPopup();

    object Value { get; set; }

    CultureInfo Culture { get; set; }

    string Format { get; set; }

    string HourHeaderText { get; }

    string MinutesHeaderText { get; }
  }
}