// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.BarcodeResources
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  internal static class BarcodeResources
  {
    private static string GetAllValues(string encodedValuesPath)
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string[] manifestResourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
      for (int index = 0; index < manifestResourceNames.Length; ++index)
      {
        if (manifestResourceNames[index].Contains(encodedValuesPath))
          empty1 = manifestResourceNames[index];
      }
      using (StreamReader streamReader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(empty1)))
        return streamReader.ReadToEnd();
    }

    public static List<string> GetEncodedValues(string encodedValuesPath, int validStep)
    {
      List<string> stringList = new List<string>();
      string allValues = BarcodeResources.GetAllValues(encodedValuesPath);
      int index1 = 0;
      while (index1 < allValues.Length)
      {
        switch (allValues[index1])
        {
          case ' ':
          case '"':
          case ',':
            ++index1;
            continue;
          default:
            string empty = string.Empty;
            for (int index2 = 0; index2 < validStep; ++index2)
            {
              char ch = allValues[index1 + index2];
              empty += ch.ToString();
            }
            stringList.Add(empty);
            index1 += validStep;
            continue;
        }
      }
      return stringList;
    }

    internal static List<int> GetCSValues(string encodedValuesPath)
    {
      List<int> intList = new List<int>();
      string allValues = BarcodeResources.GetAllValues(encodedValuesPath);
      string empty1 = string.Empty;
      for (int index = 0; index < allValues.Length; ++index)
      {
        char ch = allValues[index];
        switch (ch)
        {
          case ' ':
          case ',':
            if (empty1.Length > 0)
            {
              int num = int.Parse(empty1);
              intList.Add(num);
              empty1 = string.Empty;
              break;
            }
            break;
          default:
            empty1 += (string) (object) ch;
            break;
        }
      }
      if (empty1.Length > 0)
      {
        int num = int.Parse(empty1);
        intList.Add(num);
        string empty2 = string.Empty;
      }
      return intList;
    }

    internal static List<List<int>> GetBarSpaceSequence(
      string encodedValuesPath,
      int validStep)
    {
      List<int> intList = new List<int>();
      List<List<int>> intListList = new List<List<int>>();
      string allValues = BarcodeResources.GetAllValues(encodedValuesPath);
      int index1 = 0;
      while (index1 < allValues.Length)
      {
        switch (allValues[index1])
        {
          case '\n':
          case '\r':
          case ' ':
          case '"':
          case ',':
            ++index1;
            break;
          default:
            string empty = string.Empty;
            for (int index2 = 0; index2 < validStep; ++index2)
            {
              char ch = allValues[index1 + index2];
              empty += ch.ToString();
            }
            intList.Add(int.Parse(empty));
            index1 += validStep;
            break;
        }
        if (intList.Count == 3)
        {
          intListList.Add(new List<int>((IEnumerable<int>) intList));
          intList.Clear();
        }
      }
      return intListList;
    }
  }
}
