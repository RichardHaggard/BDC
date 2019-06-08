// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.DataTypeConvertor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Globalization;

namespace Telerik.WinControls.Export
{
  public class DataTypeConvertor
  {
    public virtual bool CanConvert(object obj)
    {
      return (object) obj.GetType() == (object) typeof (DateTime) || (object) obj.GetType() == (object) typeof (TimeSpan) || ((object) obj.GetType() == (object) typeof (DateTimeOffset) || (object) obj.GetType() == (object) typeof (string)) || ((object) obj.GetType() == (object) typeof (short) || (object) obj.GetType() == (object) typeof (int) || ((object) obj.GetType() == (object) typeof (long) || (object) obj.GetType() == (object) typeof (ushort))) || ((object) obj.GetType() == (object) typeof (uint) || (object) obj.GetType() == (object) typeof (ulong) || ((object) obj.GetType() == (object) typeof (byte) || (object) obj.GetType() == (object) typeof (float)) || ((object) obj.GetType() == (object) typeof (Decimal) || (object) obj.GetType() == (object) typeof (double) || ((object) obj.GetType() == (object) typeof (bool) || (object) obj.GetType() == (object) typeof (DBNull)))) || (object) obj.GetType() == (object) typeof (Guid);
    }

    public virtual string ConvertDataEnumToString(DataType dataType)
    {
      string empty = string.Empty;
      switch (dataType)
      {
        case DataType.String:
          return "String";
        case DataType.Number:
          return "Number";
        case DataType.DateTime:
          return "DateTime";
        case DataType.Boolean:
          return "Boolean";
        case DataType.Null:
          return "String";
        case DataType.Other:
          return "String";
        default:
          throw new Exception("Type cannot be converted");
      }
    }

    public virtual string Convert(object obj)
    {
      return this.Convert(obj, true);
    }

    public virtual string Convert(object obj, bool escapeStrings)
    {
      string empty = string.Empty;
      string str;
      switch (obj.GetType().FullName)
      {
        case "System.DateTime":
          str = ((DateTime) obj).ToString("yyyy-MM-ddTHH:mm:sss.fff");
          break;
        case "System.String":
          str = obj.ToString();
          if (escapeStrings)
          {
            str = str.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\n", "&#xA;");
            break;
          }
          break;
        case "System.Int16":
          str = ((short) obj).ToString("0");
          break;
        case "System.UInt16":
          str = ((ushort) obj).ToString("0");
          break;
        case "System.Int32":
          str = ((int) obj).ToString("0");
          break;
        case "System.UInt32":
          str = ((uint) obj).ToString("0");
          break;
        case "System.Int64":
          str = ((long) obj).ToString("0");
          break;
        case "System.UInt64":
          str = ((ulong) obj).ToString("0");
          break;
        case "System.Decimal":
          str = ((Decimal) obj).ToString((IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case "System.Single":
          str = ((float) obj).ToString((IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case "System.Double":
          str = ((double) obj).ToString((IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case "System.DBNull":
          str = string.Empty;
          break;
        case "System.Boolean":
          str = (bool) obj ? "1" : "0";
          break;
        default:
          str = obj.ToString();
          break;
      }
      return str;
    }

    public virtual DataType ConvertToDataType(object obj)
    {
      if (obj == null)
        return DataType.Empty;
      DataType dataType;
      switch (obj.GetType().FullName)
      {
        case "System.DateTime":
          dataType = DataType.DateTime;
          break;
        case "System.String":
          dataType = DataType.String;
          break;
        case "System.Int16":
        case "System.UInt16":
        case "System.Int32":
        case "System.UInt32":
        case "System.Int64":
        case "System.UInt64":
        case "System.SByte":
        case "System.Byte":
        case "System.Decimal":
        case "System.Single":
        case "System.Double":
          dataType = DataType.Number;
          break;
        case "System.DBNull":
          dataType = DataType.Null;
          break;
        case "System.Boolean":
          dataType = DataType.Boolean;
          break;
        default:
          dataType = DataType.Other;
          break;
      }
      return dataType;
    }

    public virtual DataType ConvertToDataType(string dateType)
    {
      DataType dataType;
      switch (dateType.ToLower())
      {
        case "datetime":
          dataType = DataType.DateTime;
          break;
        case "string":
          dataType = DataType.String;
          break;
        case "integer":
        case "int":
        case "int16":
        case "uint16":
        case "int32":
        case "uint32":
        case "int64":
        case "uint64":
        case "sbyte":
        case "byte":
        case "decimal":
        case "single":
        case "double":
        case "number":
          dataType = DataType.Number;
          break;
        case "dbnull":
          dataType = DataType.Null;
          break;
        case "boolean":
          dataType = DataType.Boolean;
          break;
        default:
          dataType = DataType.Other;
          break;
      }
      return dataType;
    }
  }
}
