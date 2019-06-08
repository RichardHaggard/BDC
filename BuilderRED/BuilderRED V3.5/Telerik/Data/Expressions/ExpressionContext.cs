// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.ExpressionContext
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace Telerik.Data.Expressions
{
  public class ExpressionContext : DictionaryObject, IDisposable
  {
    private static Dictionary<int, ExpressionContext> threadedContexts = new Dictionary<int, ExpressionContext>();
    private static readonly object syncobjs = new object();
    private DateTime execTime = DateTime.Now;
    private IFormatProvider formatProvider = (IFormatProvider) CultureInfo.CurrentCulture;
    private const int MAX_THREADED_CONTEXTS = 50;
    private static ExpressionContext context;
    private uint version;
    public bool CaseSensitive;

    internal uint Version
    {
      get
      {
        return this.version;
      }
    }

    public static ExpressionContext Context
    {
      get
      {
        if (ExpressionContext.context == null)
          ExpressionContext.context = new ExpressionContext();
        return ExpressionContext.context;
      }
      set
      {
        ExpressionContext.context = value;
      }
    }

    public override void Add(string key, object value)
    {
      if (value is Color)
        value = (object) ((Color) value).ToArgb();
      base.Add(key, value);
      ++this.version;
    }

    public override void Clear()
    {
      base.Clear();
      ++this.version;
    }

    public virtual void Dispose()
    {
      this.formatProvider = (IFormatProvider) null;
    }

    public static ExpressionContext GetContext(int threadId)
    {
      if (!ExpressionContext.threadedContexts.ContainsKey(threadId))
      {
        lock (ExpressionContext.syncobjs)
          ExpressionContext.threadedContexts.Add(threadId, new ExpressionContext());
      }
      return ExpressionContext.threadedContexts[threadId];
    }

    public static void ClearThreadedContexts()
    {
      if (ExpressionContext.threadedContexts.Count <= 50)
        return;
      ExpressionContext.threadedContexts.Clear();
    }

    protected override void SetValue(string name, object value)
    {
      base.SetValue(name, value);
      ++this.version;
    }

    [Description("Returns the current date and time on this computer, expressed as the local time.")]
    public virtual DateTime Now()
    {
      return this.execTime;
    }

    [Description("Returns the current date. Regardless of the actual time, this function returns midnight of the current date.")]
    public virtual DateTime Today()
    {
      return this.execTime.Date;
    }

    [Description("Returns a date-time value that is the specified number of days away from the specified DateTime.")]
    public virtual DateTime AddDays(object value, object daysToAdd)
    {
      return Convert.ToDateTime(value).AddDays(Convert.ToDouble(daysToAdd));
    }

    [Description("Returns a date-time value that is the specified number of hours away from the specified DateTime.")]
    public virtual DateTime AddHours(object value, object hoursToAdd)
    {
      return Convert.ToDateTime(value).AddHours(Convert.ToDouble(hoursToAdd));
    }

    [Description("Returns a date-time value that is the specified number of milliseconds away from the specified DateTime.")]
    public virtual DateTime AddMilliseconds(object value, object millisecondsToAdd)
    {
      return Convert.ToDateTime(value).AddMilliseconds(Convert.ToDouble(millisecondsToAdd));
    }

    [Description("Returns a date-time value that is the specified number of minutes away from the specified DateTime.")]
    public virtual DateTime AddMinutes(object value, object minutesToAdd)
    {
      return Convert.ToDateTime(value).AddMinutes(Convert.ToDouble(minutesToAdd));
    }

    [Description("Returns a date-time value that is the specified number of months away from the specified DateTime.")]
    public virtual DateTime AddMonths(object value, object monthsToAdd)
    {
      return Convert.ToDateTime(value).AddMonths(Convert.ToInt32(monthsToAdd));
    }

    [Description("Returns a date-time value that is the specified number of seconds away from the specified DateTime.")]
    public virtual DateTime AddSeconds(object value, object secondsToAdd)
    {
      return Convert.ToDateTime(value).AddSeconds(Convert.ToDouble(secondsToAdd));
    }

    [Description("Returns a date-time value that is the specified number of ticks away from the specified DateTime.")]
    public virtual DateTime AddTicks(object value, object ticksToAdd)
    {
      return Convert.ToDateTime(value).AddTicks(Convert.ToInt64(ticksToAdd));
    }

    [Description("Returns a date-time value that is away from the specified DateTime for the given TimeSpan.")]
    public virtual DateTime AddTimeSpan(object value, object timeSpan)
    {
      return Convert.ToDateTime(value).Add((TimeSpan) timeSpan);
    }

    [Description("Returns a date-time value that is the specified number of years away from the specieid DateTime.")]
    public virtual DateTime AddYears(object value, object yearsToAdd)
    {
      return Convert.ToDateTime(value).AddYears(Convert.ToInt32(yearsToAdd));
    }

    [Description("Returns the number of day boundaries between two non-nullable dates.")]
    public virtual double DateDiffDay(object startDate, object endDate)
    {
      DateTime dateTime = Convert.ToDateTime(startDate);
      return (Convert.ToDateTime(endDate) - dateTime).TotalDays;
    }

    [Description("Returns the number of hour boundaries between two non-nullable dates.")]
    public virtual double DateDiffHour(object startDate, object endDate)
    {
      DateTime dateTime = Convert.ToDateTime(startDate);
      return (Convert.ToDateTime(endDate) - dateTime).TotalHours;
    }

    [Description("Returns the number of millisecond boundaries between two non-nullable dates.")]
    public virtual double DateDiffMilliSecond(object startDate, object endDate)
    {
      DateTime dateTime = Convert.ToDateTime(startDate);
      return (Convert.ToDateTime(endDate) - dateTime).TotalMilliseconds;
    }

    [Description("Returns the number of minute boundaries between two non-nullable dates.")]
    public virtual double DateDiffMinute(object startDate, object endDate)
    {
      DateTime dateTime = Convert.ToDateTime(startDate);
      return (Convert.ToDateTime(endDate) - dateTime).TotalMinutes;
    }

    [Description("Returns the number of second boundaries between two non-nullable dates.")]
    public virtual double DateDiffSecond(object startDate, object endDate)
    {
      DateTime dateTime = Convert.ToDateTime(startDate);
      return (Convert.ToDateTime(endDate) - dateTime).TotalSeconds;
    }

    [Description("Returns the number of tick boundaries between two non-nullable dates.")]
    public virtual long DateDiffTick(object startDate, object endDate)
    {
      DateTime dateTime = Convert.ToDateTime(startDate);
      return (Convert.ToDateTime(endDate) - dateTime).Ticks;
    }

    [Description("Extracts a date from the defined DateTime.")]
    public virtual DateTime GetDate(object value)
    {
      return Convert.ToDateTime(value).Date;
    }

    [Description("Extracts a day from the defined DateTime.")]
    public virtual int GetDay(object value)
    {
      return Convert.ToDateTime(value).Day;
    }

    [Description("Extracts a day of the week from the defined DateTime.")]
    public virtual DayOfWeek GetDayOfWeek(object value)
    {
      return Convert.ToDateTime(value).DayOfWeek;
    }

    [Description("Extracts a day of the year from the defined DateTime.")]
    public virtual int GetDayOfYear(object value)
    {
      return Convert.ToDateTime(value).DayOfYear;
    }

    [Description("Extracts an hour from the defined DateTime.")]
    public virtual int GetHour(object value)
    {
      return Convert.ToDateTime(value).Hour;
    }

    [Description("Extracts milliseconds from the defined DateTime.")]
    public virtual int GetMilliSecond(object value)
    {
      return Convert.ToDateTime(value).Millisecond;
    }

    [Description("Extracts minutes from the defined DateTime.")]
    public virtual int GetMinute(object value)
    {
      return Convert.ToDateTime(value).Minute;
    }

    [Description("Extracts a month from the defined DateTime.")]
    public virtual int GetMonth(object value)
    {
      return Convert.ToDateTime(value).Month;
    }

    [Description("Extracts seconds from the defined DateTime.")]
    public virtual int GetSecond(object value)
    {
      return Convert.ToDateTime(value).Second;
    }

    [Description("Extracts the time of the day from the defined DateTime, in ticks.")]
    public virtual long GetTimeOfDay(object value)
    {
      return Convert.ToDateTime(value).TimeOfDay.Ticks;
    }

    [Description("Extracts a year from the defined DateTime.")]
    public virtual int GetYear(object value)
    {
      return Convert.ToDateTime(value).Year;
    }

    [Description("Returns the current system date and time, expressed as Coordinated Universal Time (UTC).")]
    public virtual DateTime UtcNow()
    {
      return this.execTime.ToUniversalTime();
    }

    [Description("Returns one of two objects, depending on the evaluation of an expression.")]
    public virtual object IIf(object expr, object truePart, object falsePart)
    {
      if (DataStorageHelper.ToBoolean(expr))
        return truePart;
      return falsePart;
    }

    [Description("Replaces the NULL with the specified replacement value.")]
    public virtual object IsNull(object value, object defaultValue)
    {
      if (value == null || DBNull.Value == value)
        return defaultValue;
      return value;
    }

    [Description("Returns a string representation of an object.")]
    public virtual string ToStr(object value)
    {
      return value.ToString();
    }

    [Description("Retrieves a substring from a string. The substring starts at a specified character position and has a specified length. ")]
    public virtual string Substr(string text, int startIndex, int length)
    {
      if (text == null)
        text = string.Empty;
      if (length < 0)
        length = 0;
      if (startIndex < 0)
        startIndex = Math.Max(0, text.Length + startIndex);
      if (startIndex > text.Length - 1)
        return string.Empty;
      length = Math.Min(startIndex + length, text.Length) - startIndex;
      return text.Substring(startIndex, length);
    }

    [Description("Replaces the format item in a specified System.String with the text equivalent of the value of a specified System.Object instance. ")]
    public virtual string Format(string format, object value)
    {
      if (DBNull.Value == value)
        value = (object) null;
      return string.Format(format, value);
    }

    [Description("Removes all occurrences of white space characters from the beginning and end of this instance.")]
    public virtual string Trim(string text)
    {
      return text.Trim();
    }

    [Description("Gets the number of characters in a string.")]
    public virtual int Len(string text)
    {
      return text.Length;
    }

    [Description("Inserts String2 into String1 at the position specified by StartPositon.")]
    public virtual string Insert(string str1, int startPosition, string str2)
    {
      return str1.Insert(startPosition, str2);
    }

    [Description("Returns the String in lowercase.")]
    public virtual string Lower(string str)
    {
      return str.ToLower();
    }

    [Description("Returns String in uppercase.")]
    public virtual string Upper(string str)
    {
      return str.ToUpper();
    }

    [Description("Left-aligns characters in the defined string, padding its left side with white space characters up to a specified total length.")]
    public virtual string PadLeft(string str, int length)
    {
      return str.PadLeft(length);
    }

    [Description("Right-aligns characters in the defined string, padding its left side with white space characters up to a specified total length.")]
    public virtual string PadRight(string str, int length)
    {
      return str.PadRight(length);
    }

    [Description("Deletes a specified number of characters from this instance, beginning at a specified position.")]
    public virtual string Remove(string str1, int startPosition, int length)
    {
      return str1.Remove(startPosition, length);
    }

    [Description("Returns a copy of String1, in which SubString2 has been replaced with String3.")]
    public virtual string Replace(string str1, string subStr2, string str3)
    {
      return str1.Replace(subStr2, str3);
    }

    [Description("Returns the absolute value of a specified number.")]
    public virtual object Abs(object value)
    {
      if (value == null || DBNull.Value == value)
        return (object) null;
      StorageType storageType = DataStorageHelper.GetStorageType(value.GetType());
      if (!DataStorageHelper.IsInteger(storageType))
      {
        if (!DataStorageHelper.IsNumeric(storageType))
          throw InvalidExpressionException.ArgumentTypeInteger(nameof (Abs), 1);
        try
        {
          return (object) Math.Abs((Decimal) value);
        }
        catch (Exception ex)
        {
          return (object) Math.Abs((double) value);
        }
      }
      else
      {
        try
        {
          return (object) Math.Abs((int) value);
        }
        catch (Exception ex)
        {
          return (object) Math.Abs((long) value);
        }
      }
    }

    [Description("Returns the ceiling value of a specified number.")]
    public virtual object Ceiling(object value)
    {
      if (value == null || DBNull.Value == value)
        return (object) null;
      if (!DataStorageHelper.IsNumeric(DataStorageHelper.GetStorageType(value.GetType())))
        return (object) 0;
      try
      {
        Decimal result;
        if (Decimal.TryParse(value.ToString(), out result))
          return (object) Math.Ceiling(result);
        return (object) 0;
      }
      catch (Exception ex)
      {
        return (object) 0;
      }
    }

    [Description("Returns the floor value of a specified number.")]
    public virtual object Floor(object value)
    {
      if (value == null || DBNull.Value == value)
        return (object) null;
      if (!DataStorageHelper.IsNumeric(DataStorageHelper.GetStorageType(value.GetType())))
        return (object) 0;
      try
      {
        Decimal result;
        if (Decimal.TryParse(value.ToString(), out result))
          return (object) Math.Floor(result);
        return (object) 0;
      }
      catch (Exception ex)
      {
        return (object) 0;
      }
    }

    [Description("Returns the arccosine of a number (the angle, in radians, whose cosine is the given float expression).")]
    public virtual double Acos(object value)
    {
      return Math.Acos(Convert.ToDouble(value));
    }

    [Description("Returns the arcsine of a number (the angle, in radians, whose sine is the given float expression).")]
    public virtual double Asin(object value)
    {
      return Math.Asin(Convert.ToDouble(value));
    }

    [Description("Returns the arctangent of a number (the angle, in radians, whose tangent is the given float expression).")]
    public virtual double Atan(object value)
    {
      return Math.Atan(Convert.ToDouble(value));
    }

    [Description("Returns an Int64 containing the full product of two specified 32-bit numbers.")]
    public virtual long BigMul(object value1, object value2)
    {
      return Math.BigMul(Convert.ToInt32(value1), Convert.ToInt32(value2));
    }

    [Description("Returns the cosine of the angle defined in radians.")]
    public virtual double Cos(object value)
    {
      return Math.Cos(Convert.ToDouble(value));
    }

    [Description("Returns the hyperbolic cosine of the angle defined in radians.")]
    public virtual double Cosh(object value)
    {
      return Math.Cosh(Convert.ToDouble(value));
    }

    [Description("Returns the exponential value of the given float expression.")]
    public virtual double Exp(object value)
    {
      return Math.Exp(Convert.ToDouble(value));
    }

    [Description("Returns the natural logarithm of a specified number.")]
    public virtual double Log(object value)
    {
      return Math.Log(Convert.ToDouble(value));
    }

    [Description("Returns the base 10 logarithm of a specified number.")]
    public virtual double Log10(object value)
    {
      return Math.Log10(Convert.ToDouble(value));
    }

    [Description("Returns the maximum value from the specified values.")]
    public virtual object Max(object value1, object value2)
    {
      StorageType storageType1 = DataStorageHelper.GetStorageType(value1.GetType());
      StorageType storageType2 = DataStorageHelper.GetStorageType(value2.GetType());
      if (DataStorageHelper.IsInteger(storageType1))
      {
        if (DataStorageHelper.IsInteger(storageType2))
        {
          try
          {
            return (object) Math.Max((int) value1, (int) value2);
          }
          catch (Exception ex)
          {
            return (object) Math.Max((long) value1, (long) value2);
          }
        }
      }
      if (DataStorageHelper.IsNumeric(storageType1))
      {
        if (DataStorageHelper.IsNumeric(storageType2))
        {
          try
          {
            return (object) Math.Max((Decimal) value1, (Decimal) value2);
          }
          catch (Exception ex)
          {
            return (object) Math.Max((double) value1, (double) value2);
          }
        }
      }
      throw InvalidExpressionException.ArgumentTypeInteger(nameof (Max), 1);
    }

    [Description("Returns the minimum value from the specified values.")]
    public virtual object Min(object value1, object value2)
    {
      StorageType storageType1 = DataStorageHelper.GetStorageType(value1.GetType());
      StorageType storageType2 = DataStorageHelper.GetStorageType(value2.GetType());
      if (DataStorageHelper.IsInteger(storageType1))
      {
        if (DataStorageHelper.IsInteger(storageType2))
        {
          try
          {
            return (object) Math.Min((int) value1, (int) value2);
          }
          catch (Exception ex)
          {
            return (object) Math.Min((long) value1, (long) value2);
          }
        }
      }
      if (DataStorageHelper.IsNumeric(storageType1))
      {
        if (DataStorageHelper.IsNumeric(storageType2))
        {
          try
          {
            return (object) Math.Min((Decimal) value1, (Decimal) value2);
          }
          catch (Exception ex)
          {
            return (object) Math.Min((double) value1, (double) value2);
          }
        }
      }
      throw InvalidExpressionException.ArgumentTypeInteger(nameof (Min), 1);
    }

    [Description("Returns a specified number raised to a specified power.")]
    public virtual double Power(object value, object power)
    {
      return Math.Pow(Convert.ToDouble(value), Convert.ToDouble(power));
    }

    [Description("Returns a random number that is less than 1, but greater than or equal to zero.")]
    public virtual double Rnd()
    {
      return new Random().NextDouble();
    }

    [Description("Rounds the given value to the nearest integer.")]
    public virtual Decimal Round(object value)
    {
      return Math.Round(Convert.ToDecimal(value));
    }

    [Description("Returns the positive (+1), zero (0), or negative (-1) sign of the given expression.")]
    public virtual int Sign(object value)
    {
      StorageType storageType = DataStorageHelper.GetStorageType(value.GetType());
      if (!DataStorageHelper.IsInteger(storageType))
      {
        if (!DataStorageHelper.IsNumeric(storageType))
          throw InvalidExpressionException.ArgumentTypeInteger(nameof (Sign), 1);
        try
        {
          return Math.Sign((Decimal) value);
        }
        catch (Exception ex)
        {
          return Math.Sign((double) value);
        }
      }
      else
      {
        try
        {
          return Math.Sign((int) value);
        }
        catch (Exception ex)
        {
          return Math.Sign((long) value);
        }
      }
    }

    [Description("Returns the sine of the angle, defined in radians.")]
    public virtual double Sin(object value)
    {
      return Math.Sin(Convert.ToDouble(value));
    }

    [Description("Returns the hyperbolic sine of the angle defined in radians.")]
    public virtual double Sinh(object value)
    {
      return Math.Sinh(Convert.ToDouble(value));
    }

    [Description("Returns the square root of a given number.")]
    public virtual double Sqrt(object value)
    {
      return Math.Sqrt(Convert.ToDouble(value));
    }

    [Description("Returns the tangent of the angle defined in radians.")]
    public virtual double Tan(object value)
    {
      return Math.Tan(Convert.ToDouble(value));
    }

    [Description("Returns the hyperbolic tangent of the angle defined in radians.")]
    public virtual double Tanh(object value)
    {
      return Math.Tanh(Convert.ToDouble(value));
    }

    [Description("Converts an expression to Integer value.")]
    public virtual object CInt(object value)
    {
      if (value == null || DBNull.Value == value)
        return (object) null;
      return (object) Convert.ToInt32(value, this.formatProvider);
    }

    [Description("Converts an expression to Double value.")]
    public virtual object CDbl(object value)
    {
      if (value == null || DBNull.Value == value)
        return (object) null;
      return (object) Convert.ToDouble(value, this.formatProvider);
    }

    [Description("Converts an expression to Boolean value.")]
    public virtual object CBool(object value)
    {
      if (DBNull.Value == value)
        value = (object) null;
      try
      {
        return (object) Convert.ToBoolean(value);
      }
      catch (Exception ex)
      {
        throw InvalidExpressionException.DatavalueConvertion(value, typeof (bool), ex);
      }
    }

    [Description("Converts an expression to Date value.")]
    public virtual object CDate(object value)
    {
      if (value == null || DBNull.Value == value)
        return (object) null;
      return (object) Convert.ToDateTime(value, this.formatProvider);
    }

    [Description("Converts an expression to string value.")]
    public virtual string CStr(object value)
    {
      if (value == null || DBNull.Value == value)
        return (string) null;
      return Convert.ToString(value, this.formatProvider);
    }
  }
}
