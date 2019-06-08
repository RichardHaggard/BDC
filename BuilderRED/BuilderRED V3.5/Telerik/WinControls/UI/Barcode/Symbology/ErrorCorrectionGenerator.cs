// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.ErrorCorrectionGenerator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public static class ErrorCorrectionGenerator
  {
    public static List<long> GenerateErrorCorrectionSequence(
      List<long> dataCodeWords,
      int errorCorrectionLevel)
    {
      int count = SpecificationData.ErrorCorrectionLevels[errorCorrectionLevel].Count;
      List<long> longList = new List<long>(count);
      for (int index = 0; index < count; ++index)
        longList.Add(0L);
      foreach (int dataCodeWord in dataCodeWords)
      {
        long num1 = ((long) dataCodeWord + longList[count - 1]) % 929L;
        for (int index = longList.Count - 1; index > 0; --index)
        {
          long num2 = 929L - num1 * (long) SpecificationData.ErrorCorrectionLevels[errorCorrectionLevel][index] % 929L;
          longList[index] = (longList[index - 1] + num2) % 929L;
        }
        long num3 = 929L - num1 * (long) SpecificationData.ErrorCorrectionLevels[errorCorrectionLevel][0] % 929L;
        longList[0] = num3 % 929L;
      }
      for (int index = 0; index < longList.Count; ++index)
      {
        if (longList[index] != 0L)
          longList[index] = 929L - longList[index];
      }
      longList.Reverse();
      return longList;
    }
  }
}
