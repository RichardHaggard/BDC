// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Carousel.Point3DConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.WinControls.UI.Carousel
{
  public sealed class Point3DConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if ((object) sourceType != (object) typeof (string))
        return base.CanConvertFrom(context, sourceType);
      return true;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if ((object) destinationType != (object) typeof (InstanceDescriptor))
        return base.CanConvertTo(context, destinationType);
      return true;
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      string str1 = value as string;
      if (str1 == null)
        return base.ConvertFrom(context, culture, value);
      string str2 = str1.Trim();
      if (str2.Length == 0)
        return (object) null;
      if (culture == null)
        culture = CultureInfo.CurrentCulture;
      char ch = culture.TextInfo.ListSeparator[0];
      string[] strArray = str2.Split(ch);
      double[] numArray = new double[strArray.Length];
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (double));
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = (double) converter.ConvertFromString(context, culture, strArray[index]);
      if (numArray.Length != 3)
        throw new ArgumentException(string.Format("Specified format {0} is not valid for values {1}", new object[2]{ (object) str2, (object) "x, y, z" }));
      return (object) new Point3D(numArray[0], numArray[1], numArray[2]);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == null)
        throw new ArgumentNullException(nameof (destinationType));
      if (value is Point3D)
      {
        if ((object) destinationType == (object) typeof (string))
        {
          Point3D point3D = (Point3D) value;
          if (culture == null)
            culture = CultureInfo.CurrentCulture;
          string separator = culture.TextInfo.ListSeparator + " ";
          TypeConverter converter = TypeDescriptor.GetConverter(typeof (double));
          string[] strArray1 = new string[3];
          int num1 = 0;
          string[] strArray2 = strArray1;
          int index1 = num1;
          int num2 = index1 + 1;
          string str1 = converter.ConvertToString(context, culture, (object) point3D.X);
          strArray2[index1] = str1;
          string[] strArray3 = strArray1;
          int index2 = num2;
          int num3 = index2 + 1;
          string str2 = converter.ConvertToString(context, culture, (object) point3D.Y);
          strArray3[index2] = str2;
          string[] strArray4 = strArray1;
          int index3 = num3;
          int num4 = index3 + 1;
          string str3 = converter.ConvertToString(context, culture, (object) point3D.Z);
          strArray4[index3] = str3;
          return (object) string.Join(separator, strArray1);
        }
        if ((object) destinationType == (object) typeof (InstanceDescriptor))
        {
          Point3D point3D = (Point3D) value;
          ConstructorInfo constructor = typeof (Point3D).GetConstructor(new Type[3]{ typeof (double), typeof (double), typeof (double) });
          if ((object) constructor != null)
            return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[3]{ (object) point3D.X, (object) point3D.Y, (object) point3D.Z });
        }
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object CreateInstance(
      ITypeDescriptorContext context,
      IDictionary propertyValues)
    {
      if (propertyValues == null)
        throw new ArgumentNullException(nameof (propertyValues));
      object propertyValue1 = propertyValues[(object) "X"];
      object propertyValue2 = propertyValues[(object) "Y"];
      object propertyValue3 = propertyValues[(object) "Z"];
      if (propertyValue1 == null || propertyValue2 == null || (!(propertyValue1 is double) || !(propertyValue2 is double)))
        throw new ArgumentException("Invalid property value entry");
      if (propertyValue3 == null || !(propertyValue3 is double))
        return (object) new Point3D((double) propertyValue1, (double) propertyValue2);
      return (object) new Point3D((double) propertyValue1, (double) propertyValue2, (double) propertyValue3);
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(typeof (Point3D), attributes).Sort(new string[3]{ "X", "Y", "Z" });
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
