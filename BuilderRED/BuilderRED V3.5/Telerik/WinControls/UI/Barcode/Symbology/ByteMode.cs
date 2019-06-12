// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.ByteMode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class ByteMode
  {
    public static List<long> EncodeText(string text)
    {
      List<long> values = new List<long>();
      foreach (char ch in text)
      {
        if (SpecificationData.ByteModeValues.Contains((int) ch))
          values.Add((long) ch);
      }
      return ByteMode.EncodeData(values);
    }

    public static List<long> EncodeData(List<long> values)
    {
      List<long> longList = new List<long>();
      if (values.Count == 0)
        return longList;
      if (values.Count % 6 == 0)
        longList.Add(924L);
      else
        longList.Add(901L);
      for (int index1 = 0; index1 < values.Count; index1 += 6)
      {
        List<long> localValues = new List<long>();
        for (int index2 = 0; index1 + index2 < values.Count; ++index2)
        {
          localValues.Add(values[index1 + index2]);
          if (localValues.Count == 6)
            break;
        }
        if (localValues.Count == 6)
          longList.AddRange((IEnumerable<long>) ByteMode.GetValuesForLongRange(localValues));
        else
          longList.AddRange((IEnumerable<long>) localValues);
      }
      return longList;
    }

    internal static List<long> GetValuesForLongRange(List<long> localValues)
    {
      List<long> values = new List<long>(5);
      long num1 = (long) ((double) localValues[0] * Math.Pow(256.0, 5.0) + (double) localValues[1] * Math.Pow(256.0, 4.0) + (double) localValues[2] * Math.Pow(256.0, 3.0) + (double) localValues[3] * Math.Pow(256.0, 2.0) + (double) localValues[4] * Math.Pow(256.0, 1.0) + (double) localValues[5] * Math.Pow(256.0, 0.0));
      for (int index = 0; index < values.Capacity; ++index)
      {
        long num2 = num1 % 900L;
        num1 /= 900L;
        values.Add(num2);
      }
      return ByteMode.Reorder(values);
    }

    private static List<long> Reorder(List<long> values)
    {
      List<long> longList = new List<long>();
      longList.AddRange((IEnumerable<long>) values);
      int index1 = 0;
      for (int index2 = values.Count - 1; index2 >= 0; --index2)
      {
        longList[index2] = values[index1];
        ++index1;
      }
      return longList;
    }
  }
}
