// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGridDateTimeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.WinControls.UI
{
  public class RadGridDateTimeConverter : DateTimeConverter
  {
    private GridViewDateTimeColumn ownerColumn;

    public RadGridDateTimeConverter(GridViewDateTimeColumn ownerColumn)
    {
      this.ownerColumn = ownerColumn;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if ((object) sourceType != (object) typeof (DateTime))
        return base.CanConvertFrom(context, sourceType);
      return true;
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      bool flag = value is DateTime;
      if (context is GridFilterCellElement && flag && this.ownerColumn.DateTimeKind != DateTimeKind.Unspecified)
        return (object) (DateTime) value;
      if (!flag)
        return base.ConvertFrom(context, culture, value);
      DateTime dateTime = (DateTime) value;
      switch (this.ownerColumn.DateTimeKind)
      {
        case DateTimeKind.Unspecified:
          dateTime = DateTime.SpecifyKind((DateTime) value, DateTimeKind.Unspecified);
          break;
        case DateTimeKind.Utc:
          dateTime = DateTime.SpecifyKind((DateTime) value, DateTimeKind.Utc).ToUniversalTime();
          break;
        case DateTimeKind.Local:
          dateTime = DateTime.SpecifyKind((DateTime) value, DateTimeKind.Local).ToUniversalTime();
          break;
      }
      return (object) dateTime;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      return (object) destinationType == (object) typeof (DateTime);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      bool flag = value is DateTime;
      if (context is GridFilterCellElement && flag && this.ownerColumn.DateTimeKind != DateTimeKind.Unspecified)
        return (object) (DateTime) value;
      if (value is DateTime)
      {
        DateTime dateTime = (DateTime) value;
        switch (this.ownerColumn.DateTimeKind)
        {
          case DateTimeKind.Unspecified:
            dateTime = DateTime.SpecifyKind((DateTime) value, DateTimeKind.Unspecified);
            break;
          case DateTimeKind.Utc:
            dateTime = DateTime.SpecifyKind((DateTime) value, DateTimeKind.Utc).ToUniversalTime();
            break;
          case DateTimeKind.Local:
            dateTime = DateTime.SpecifyKind((DateTime) value, DateTimeKind.Utc).ToLocalTime();
            break;
        }
        return (object) dateTime;
      }
      if ((object) destinationType == (object) typeof (DateTime) && (value == null || value == DBNull.Value))
        return (object) DateTime.MinValue;
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
