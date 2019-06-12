// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDataConversionInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.WinControls.UI
{
  public class GridDataConversionInfo : IDataConversionInfoProvider
  {
    private Type dataType;
    private TypeConverter dataTypeConverter;
    private object dataSourceNullValue;
    private object nullValue;
    private CultureInfo formatInfo;
    private string formatString;

    public GridDataConversionInfo(IDataConversionInfoProvider conversionInfo)
    {
      this.dataType = conversionInfo.DataType;
      this.dataTypeConverter = conversionInfo.DataTypeConverter;
      this.dataSourceNullValue = conversionInfo.DataSourceNullValue;
      this.nullValue = conversionInfo.NullValue;
      this.formatInfo = conversionInfo.FormatInfo;
      this.formatString = conversionInfo.FormatString;
    }

    public Type DataType
    {
      get
      {
        return this.dataType;
      }
      set
      {
        this.dataType = value;
      }
    }

    public TypeConverter DataTypeConverter
    {
      get
      {
        return this.dataTypeConverter;
      }
      set
      {
        this.dataTypeConverter = value;
      }
    }

    public object DataSourceNullValue
    {
      get
      {
        return this.dataSourceNullValue;
      }
      set
      {
        this.dataSourceNullValue = value;
      }
    }

    public object NullValue
    {
      get
      {
        return this.nullValue;
      }
      set
      {
        this.nullValue = value;
      }
    }

    public CultureInfo FormatInfo
    {
      get
      {
        return this.formatInfo;
      }
      set
      {
        this.formatInfo = value;
      }
    }

    public string FormatString
    {
      get
      {
        return this.formatString;
      }
      set
      {
        this.formatString = value;
      }
    }
  }
}
