// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.NumericMode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public class NumericMode
  {
    public static List<long> EncodeData(string values)
    {
      List<long> longList = new List<long>();
      while (values.Length > 0)
      {
        if (values.Length >= 44)
        {
          string values1 = "1" + values.Substring(0, 44);
          values = values.Substring(44);
          longList.AddRange((IEnumerable<long>) NumericMode.CalculateRange(values1));
        }
        else
        {
          string values1 = "1" + values;
          values = string.Empty;
          longList.AddRange((IEnumerable<long>) NumericMode.CalculateRange(values1));
        }
      }
      return longList;
    }

    private static List<long> CalculateRange(string values)
    {
      List<long> longList1 = new List<long>();
      List<long> longList2 = new List<long>();
      List<ulong> ulongList1 = new List<ulong>();
      for (string values1 = values; values1 != "0"; values1 = NumericMode.Division900(values1))
      {
        List<ulong> ulongList2 = NumericMode.Remainder(values1);
        long num = (long) ulongList2[ulongList2.Count - 1];
        longList2.Add(num);
      }
      for (int index = longList2.Count - 1; index >= 0; --index)
        longList1.Add(longList2[index]);
      return longList1;
    }

    private static string Division900(string values)
    {
      string empty = string.Empty;
      string str;
      if (values.Length <= 10)
        str = empty + NumericMode.CalculateDivision5(values);
      else if (values.Length <= 20)
      {
        ulong num = ulong.Parse(values.Substring(0, values.Length - 10));
        str = empty + NumericMode.CalculateDivision5(num.ToString()) + NumericMode.CalculateDivision4(values);
      }
      else if (values.Length <= 30)
      {
        ulong num = ulong.Parse(values.Substring(0, values.Length - 20));
        str = empty + NumericMode.CalculateDivision5(num.ToString()) + NumericMode.CalculateDivision4(values) + NumericMode.CalculateDivision3(values);
      }
      else if (values.Length <= 40)
      {
        ulong num = ulong.Parse(values.Substring(0, values.Length - 30));
        str = empty + NumericMode.CalculateDivision5(num.ToString()) + NumericMode.CalculateDivision4(values) + NumericMode.CalculateDivision3(values) + NumericMode.CalculateDivision2(values);
      }
      else
      {
        ulong num = ulong.Parse(values.Substring(0, values.Length - 40));
        str = empty + NumericMode.CalculateDivision5(num.ToString()) + NumericMode.CalculateDivision4(values) + NumericMode.CalculateDivision3(values) + NumericMode.CalculateDivision2(values) + NumericMode.CalculateDivision1(values);
      }
      return str;
    }

    private static string CalculateDivision5(string data)
    {
      List<ulong> ulongList = NumericMode.Remainder(data);
      long num = (long) ulongList[ulongList.Count - 1];
      return (ulongList[ulongList.Count - 2] / 900UL).ToString();
    }

    private static string CalculateDivision4(string data)
    {
      List<ulong> ulongList1 = NumericMode.Remainder(data);
      long num1 = (long) ulongList1[ulongList1.Count - 1];
      ulong num2 = ulongList1[ulongList1.Count - 2];
      ulong num3 = ulongList1[ulongList1.Count - 3];
      List<ulong> ulongList2 = NumericMode.Remainder(num2.ToString());
      return ((ulong) ((double) ulongList2[ulongList2.Count - 1] * Math.Pow(10.0, 10.0) + (double) num3) / 900UL).ToString();
    }

    private static string CalculateDivision3(string data)
    {
      List<ulong> ulongList1 = NumericMode.Remainder(data);
      ulong num1 = ulongList1[ulongList1.Count - 1];
      ulong num2 = ulongList1[ulongList1.Count - 2];
      ulong num3 = ulongList1[ulongList1.Count - 3];
      ulong num4 = ulongList1[ulongList1.Count - 4];
      List<ulong> ulongList2 = NumericMode.Remainder(num2.ToString() + num3.ToString());
      ulong num5 = (ulong) ((double) ulongList2[ulongList2.Count - 1] * Math.Pow(10.0, 10.0) + (double) num4) / 900UL;
      string str = string.Empty;
      if (num1 <= 9UL && num5.ToString().Length < 10)
        str = "00";
      else if (num1 >= 10UL && num1 <= 99UL && num5.ToString().Length < 10)
        str = "0";
      return str + num5.ToString();
    }

    private static string CalculateDivision2(string data)
    {
      List<ulong> ulongList1 = NumericMode.Remainder(data);
      ulong num1 = ulongList1[ulongList1.Count - 1];
      ulong num2 = ulongList1[ulongList1.Count - 2];
      ulong num3 = ulongList1[ulongList1.Count - 3];
      ulong num4 = ulongList1[ulongList1.Count - 4];
      ulong num5 = ulongList1[ulongList1.Count - 5];
      List<ulong> ulongList2 = NumericMode.Remainder(num2.ToString() + num3.ToString() + num4.ToString());
      ulong num6 = (ulong) ((double) ulongList2[ulongList2.Count - 1] * Math.Pow(10.0, 10.0) + (double) num5) / 900UL;
      string str = string.Empty;
      if (num1 <= 9UL && num6.ToString().Length < 10)
        str = "00";
      else if (num1 >= 10UL && num1 <= 99UL && num6.ToString().Length < 10)
        str = "0";
      return str + num6.ToString();
    }

    private static string CalculateDivision1(string data)
    {
      List<ulong> ulongList1 = NumericMode.Remainder(data);
      ulong num1 = ulongList1[ulongList1.Count - 1];
      ulong num2 = ulongList1[ulongList1.Count - 2];
      ulong num3 = ulongList1[ulongList1.Count - 3];
      ulong num4 = ulongList1[ulongList1.Count - 4];
      ulong num5 = ulongList1[ulongList1.Count - 5];
      ulong num6 = ulongList1[ulongList1.Count - 6];
      List<ulong> ulongList2 = NumericMode.Remainder(num2.ToString() + num3.ToString() + num4.ToString() + num5.ToString());
      ulong num7 = (ulong) ((double) ulongList2[ulongList2.Count - 1] * Math.Pow(10.0, 10.0) + (double) num6) / 900UL;
      string str = string.Empty;
      if (num1 <= 9UL && num7.ToString().Length < 10)
        str = "00";
      else if (num1 >= 10UL && num1 <= 99UL && num7.ToString().Length < 10)
        str = "0";
      return str + num7.ToString();
    }

    private static List<ulong> Remainder(string values)
    {
      List<ulong> ulongList = new List<ulong>();
      if (values.Length <= 10)
      {
        ulong group5 = ulong.Parse(values);
        ulong remainder5 = NumericMode.CalculateRemainder5(group5);
        ulongList.Add(group5);
        ulongList.Add(remainder5);
      }
      else if (values.Length <= 20)
      {
        ulong group4 = ulong.Parse(values.Substring(0, values.Length - 10));
        ulong group5 = ulong.Parse(values.Substring(values.Length - 10));
        ulong remainder4 = NumericMode.CalculateRemainder4(group4, group5);
        ulongList.Add(group5);
        ulongList.Add(group4);
        ulongList.Add(remainder4);
      }
      else if (values.Length <= 30)
      {
        ulong group3 = ulong.Parse(values.Substring(0, values.Length - 20));
        ulong group4 = ulong.Parse(values.Substring(values.Length - 20, 10));
        ulong group5 = ulong.Parse(values.Substring(values.Length - 10));
        ulong remainder3 = NumericMode.CalculateRemainder3(group3, group4, group5);
        ulongList.Add(group5);
        ulongList.Add(group4);
        ulongList.Add(group3);
        ulongList.Add(remainder3);
      }
      else if (values.Length <= 40)
      {
        ulong group2 = ulong.Parse(values.Substring(0, values.Length - 30));
        ulong group3 = ulong.Parse(values.Substring(values.Length - 30, 10));
        ulong group4 = ulong.Parse(values.Substring(values.Length - 20, 10));
        ulong group5 = ulong.Parse(values.Substring(values.Length - 10));
        ulong remainder2 = NumericMode.CalculateRemainder2(group2, group3, group4, group5);
        ulongList.Add(group5);
        ulongList.Add(group4);
        ulongList.Add(group3);
        ulongList.Add(group2);
        ulongList.Add(remainder2);
      }
      else
      {
        ulong group1 = ulong.Parse(values.Substring(0, values.Length - 40));
        ulong group2 = ulong.Parse(values.Substring(values.Length - 40, 10));
        ulong group3 = ulong.Parse(values.Substring(values.Length - 30, 10));
        ulong group4 = ulong.Parse(values.Substring(values.Length - 20, 10));
        ulong group5 = ulong.Parse(values.Substring(values.Length - 10));
        ulong remainder1 = NumericMode.CalculateRemainder1(group1, group2, group3, group4, group5);
        ulongList.Add(group5);
        ulongList.Add(group4);
        ulongList.Add(group3);
        ulongList.Add(group2);
        ulongList.Add(group1);
        ulongList.Add(remainder1);
      }
      return ulongList;
    }

    private static ulong CalculateRemainder5(ulong group5)
    {
      return group5 % 900UL;
    }

    private static ulong CalculateRemainder4(ulong group4, ulong group5)
    {
      return (ulong) ((double) NumericMode.CalculateRemainder5(group4) * Math.Pow(10.0, 10.0) + (double) group5) % 900UL;
    }

    private static ulong CalculateRemainder3(ulong group3, ulong group4, ulong group5)
    {
      return (ulong) (((double) (ulong) (((double) NumericMode.CalculateRemainder5(group3) * Math.Pow(10.0, 10.0) + (double) group4) % 900.0) * Math.Pow(10.0, 10.0) + (double) group5) % 900.0);
    }

    private static ulong CalculateRemainder2(
      ulong group2,
      ulong group3,
      ulong group4,
      ulong group5)
    {
      return (ulong) ((double) ((ulong) ((double) ((ulong) ((double) NumericMode.CalculateRemainder5(group2) * Math.Pow(10.0, 10.0) + (double) group3) % 900UL) * Math.Pow(10.0, 10.0) + (double) group4) % 900UL) * Math.Pow(10.0, 10.0) + (double) group5) % 900UL;
    }

    private static ulong CalculateRemainder1(
      ulong group1,
      ulong group2,
      ulong group3,
      ulong group4,
      ulong group5)
    {
      return (ulong) ((double) ((ulong) ((double) ((ulong) ((double) ((ulong) ((double) NumericMode.CalculateRemainder5(group1) * Math.Pow(10.0, 10.0) + (double) group2) % 900UL) * Math.Pow(10.0, 10.0) + (double) group3) % 900UL) * Math.Pow(10.0, 10.0) + (double) group4) % 900UL) * Math.Pow(10.0, 10.0) + (double) group5) % 900UL;
    }
  }
}
