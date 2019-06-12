// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IDataConversionInfoProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.WinControls.UI
{
  public interface IDataConversionInfoProvider
  {
    Type DataType { get; set; }

    TypeConverter DataTypeConverter { get; set; }

    object DataSourceNullValue { get; set; }

    object NullValue { get; set; }

    CultureInfo FormatInfo { get; set; }

    string FormatString { get; set; }
  }
}
