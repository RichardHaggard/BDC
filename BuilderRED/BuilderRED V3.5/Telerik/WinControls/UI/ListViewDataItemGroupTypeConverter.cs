// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ListViewDataItemGroupTypeConverter
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
  internal class ListViewDataItemGroupTypeConverter : TypeConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if ((object) sourceType != (object) typeof (string) || context == null || !(context.Instance is ListViewDataItem))
        return base.CanConvertFrom(context, sourceType);
      return true;
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if ((object) destinationType != (object) typeof (InstanceDescriptor) && ((object) destinationType != (object) typeof (string) || context == null || !(context.Instance is ListViewDataItem)))
        return base.CanConvertTo(context, destinationType);
      return true;
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (value is string)
      {
        string str = ((string) value).Trim();
        if (context != null && context.Instance != null)
        {
          ListViewDataItem instance = context.Instance as ListViewDataItem;
          if (instance != null && instance.Owner != null)
          {
            foreach (ListViewDataItemGroup group in instance.Owner.Groups)
            {
              if (group.Text == str)
                return (object) group;
            }
          }
        }
      }
      if (value != null && !value.Equals((object) string.Empty))
        return base.ConvertFrom(context, culture, value);
      return (object) null;
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == null)
        throw new ArgumentNullException(nameof (destinationType));
      if ((object) destinationType == (object) typeof (InstanceDescriptor) && value is ListViewDataItemGroup)
      {
        ListViewDataItemGroup viewDataItemGroup = (ListViewDataItemGroup) value;
        ConstructorInfo constructor = typeof (ListViewDataItemGroup).GetConstructor(new Type[1]{ typeof (string) });
        if ((object) constructor != null)
          return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[1]{ (object) viewDataItemGroup.Text }, false);
      }
      if ((object) destinationType == (object) typeof (string))
      {
        if (value == null)
          return (object) string.Empty;
        if (value is ListViewDataItemGroup)
          return (object) ((ListViewDataItem) value).Text;
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      if (context != null && context.Instance != null)
      {
        ListViewDataItem instance = context.Instance as ListViewDataItem;
        if (instance != null && instance.Owner != null)
        {
          ArrayList arrayList = new ArrayList();
          foreach (ListViewDataItemGroup group in instance.Owner.Groups)
            arrayList.Add((object) group.Text);
          arrayList.Add((object) null);
          return new TypeConverter.StandardValuesCollection((ICollection) arrayList);
        }
      }
      return (TypeConverter.StandardValuesCollection) null;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return true;
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
