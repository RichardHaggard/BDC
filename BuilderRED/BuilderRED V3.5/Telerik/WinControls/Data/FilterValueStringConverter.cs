// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterValueStringConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace Telerik.WinControls.Data
{
  public class FilterValueStringConverter : StringConverter
  {
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
      try
      {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        return base.ConvertTo(context, culture, value, destinationType);
      }
      finally
      {
        Thread.CurrentThread.CurrentCulture = currentCulture;
      }
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
      try
      {
        Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        return base.ConvertFrom(context, culture, value);
      }
      finally
      {
        Thread.CurrentThread.CurrentCulture = currentCulture;
      }
    }
  }
}
