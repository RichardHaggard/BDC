// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.InvalidSizeException
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class InvalidSizeException : ArgumentException
  {
    public InvalidSizeException(string text)
      : base(InvalidSizeException.Format(text))
    {
    }

    private static string Format(string text)
    {
      return string.Format("Barcode text '{0}' is too large to fit!", (object) text);
    }
  }
}
