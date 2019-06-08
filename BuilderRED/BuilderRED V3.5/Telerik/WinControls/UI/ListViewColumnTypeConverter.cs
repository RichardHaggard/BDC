// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewColumnTypeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.WinControls.UI
{
  public class ListViewColumnTypeConverter : TypeConverter
  {
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if ((object) destinationType != (object) typeof (string) && (object) destinationType != (object) typeof (InstanceDescriptor))
        return base.CanConvertTo(context, destinationType);
      return true;
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == null)
        throw new ArgumentNullException(nameof (destinationType));
      if ((object) destinationType == (object) typeof (InstanceDescriptor) && value is ListViewDetailColumn)
      {
        ListViewDetailColumn viewDetailColumn = (ListViewDetailColumn) value;
        ConstructorInfo constructor = typeof (ListViewDetailColumn).GetConstructor(new Type[2]{ typeof (string), typeof (string) });
        if ((object) constructor != null)
          return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[2]{ (object) viewDetailColumn.Name, (object) viewDetailColumn.HeaderText }, false);
      }
      if ((object) destinationType == (object) typeof (string) && value is ListViewDataItem)
        return (object) ((ListViewDetailColumn) value).Name;
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
