// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SettingValueConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class SettingValueConverter : TypeConverter
  {
    private ITypeDescriptorContext currContext;
    private TypeConverter underlayingConverter;
    private bool hasEditor;

    private TypeConverter GetUnderlayingConverter(ITypeDescriptorContext context)
    {
      if (this.underlayingConverter == null || context != this.currContext)
      {
        this.currContext = context;
        if (context == null)
          return this.underlayingConverter;
        XmlPropertySetting instance = (XmlPropertySetting) context.Instance;
        if (string.IsNullOrEmpty(instance.Property))
        {
          this.underlayingConverter = (TypeConverter) null;
          return (TypeConverter) null;
        }
        string[] strArray = instance.Property.Split('.');
        if (strArray.Length <= 1)
          throw new Exception("Invalid property name. Property consist of type FullName \".\" and property name.");
        string propertyName = strArray[strArray.Length - 1];
        string className = string.Join(".", strArray, 0, strArray.Length - 1);
        RadProperty safe = RadProperty.FindSafe(className, propertyName);
        if (safe != null)
        {
          TypeConverter converter = TypeDescriptor.GetConverter(safe.PropertyType);
          if (converter == null || !converter.CanConvertFrom(typeof (string)) || !converter.CanConvertTo(typeof (string)))
          {
            if (!converter.CanConvertFrom(typeof (string)))
            {
              int num1 = (int) MessageBox.Show("Converter can't convert from string");
            }
            else if (!converter.CanConvertTo(typeof (string)))
            {
              int num2 = (int) MessageBox.Show("Converter can't convert to string");
            }
            else
            {
              int num3 = (int) MessageBox.Show("Converter for type not found");
            }
          }
          this.hasEditor = (UITypeEditor) TypeDescriptor.GetEditor(safe.PropertyType, typeof (UITypeEditor)) != null;
          this.underlayingConverter = converter;
        }
        else
        {
          int num = (int) MessageBox.Show("Can't find property " + instance.Property + ". Property " + propertyName + " not registered for " + className);
          this.underlayingConverter = (TypeConverter) null;
          return (TypeConverter) null;
        }
      }
      return this.underlayingConverter;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
    {
      TypeConverter underlayingConverter = this.GetUnderlayingConverter(context);
      if (underlayingConverter == null)
        return false;
      return underlayingConverter.CanConvertFrom(sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, System.Type destinationType)
    {
      TypeConverter underlayingConverter = this.GetUnderlayingConverter(context);
      if (underlayingConverter == null)
        return false;
      return underlayingConverter.CanConvertTo(destinationType);
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      TypeConverter underlayingConverter = this.GetUnderlayingConverter(context);
      if (underlayingConverter == null || this.hasEditor)
        return false;
      return underlayingConverter.GetStandardValuesSupported();
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      TypeConverter underlayingConverter = this.GetUnderlayingConverter(context);
      if (underlayingConverter == null)
        return base.GetStandardValues(context);
      return (TypeConverter.StandardValuesCollection) underlayingConverter.GetStandardValues();
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      return this.GetUnderlayingConverter(context)?.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      System.Type destinationType)
    {
      return this.GetUnderlayingConverter(context)?.ConvertTo(context, culture, value, destinationType);
    }
  }
}
