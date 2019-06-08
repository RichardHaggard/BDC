// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.DateTimeExtensions
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Licensing
{
  public static class DateTimeExtensions
  {
    public static DateTime FromIso8601(string date)
    {
      bool flag = date.EndsWith("Z");
      string[] strArray = date.Split('T', 'Z', ':', '-', '.', '+');
      DateTime dateTime = new DateTime(Convert.ToInt32(strArray[0]), Convert.ToInt32(strArray.Length > 1 ? strArray[1] : "1"), Convert.ToInt32(strArray.Length > 2 ? strArray[2] : "1"), Convert.ToInt32(strArray.Length > 3 ? strArray[3] : "0"), Convert.ToInt32(strArray.Length > 4 ? strArray[4] : "0"), Convert.ToInt32(strArray.Length > 5 ? strArray[5] : "0"), Convert.ToInt32(strArray.Length > 6 ? strArray[6] : "0"));
      if (!flag && strArray.Length >= 9)
      {
        string str1 = strArray.Length > 7 ? strArray[7] : "";
        string str2 = strArray.Length > 8 ? strArray[8] : "";
        if (date.Contains("+"))
        {
          dateTime = dateTime.AddHours(Convert.ToDouble(str1));
          dateTime = dateTime.AddMinutes(Convert.ToDouble(str2));
        }
        else
          dateTime = dateTime.AddHours(-Convert.ToDouble(str1)).AddMinutes(-Convert.ToDouble(str2));
      }
      if (flag)
        dateTime = new DateTime(0L, DateTimeKind.Utc).AddTicks(dateTime.Ticks);
      return dateTime;
    }

    public static string ToIso8601(DateTime dt)
    {
      return dt.Year.ToString() + "-" + DateTimeExtensions.TwoDigits(dt.Month) + "-" + DateTimeExtensions.TwoDigits(dt.Day) + "T" + DateTimeExtensions.TwoDigits(dt.Hour) + ":" + DateTimeExtensions.TwoDigits(dt.Minute) + ":" + DateTimeExtensions.TwoDigits(dt.Second) + "." + DateTimeExtensions.ThreeDigits(dt.Millisecond) + "Z";
    }

    private static string TwoDigits(int value)
    {
      if (value < 10)
        return "0" + value.ToString();
      return value.ToString();
    }

    private static string ThreeDigits(int value)
    {
      if (value < 10)
        return "00" + value.ToString();
      if (value < 100)
        return "0" + value.ToString();
      return value.ToString();
    }

    public static string ToASPNetAjax(DateTime dt)
    {
      return "\\/Date(" + dt.Ticks.ToString() + ")\\/";
    }

    public static DateTime FromASPNetAjax(string ajax)
    {
      return new DateTime(Convert.ToInt64(ajax.Split('(', ')')[1]), DateTimeKind.Utc);
    }
  }
}
