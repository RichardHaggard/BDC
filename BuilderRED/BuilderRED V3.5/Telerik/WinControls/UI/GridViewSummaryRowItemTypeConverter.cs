// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewSummaryRowItemTypeConverter
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
  internal class GridViewSummaryRowItemTypeConverter : TypeConverter
  {
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if ((object) destinationType == (object) typeof (InstanceDescriptor))
        return true;
      return base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if ((object) destinationType != (object) typeof (InstanceDescriptor))
        return base.ConvertTo(context, culture, value, destinationType);
      GridViewSummaryRowItem viewSummaryRowItem = value as GridViewSummaryRowItem;
      if (viewSummaryRowItem == null)
        throw new ArgumentException("GridViewSummaryRowItemTypeConverter can convert items of type GridViewSummaryRowItem");
      return (object) new InstanceDescriptor((MemberInfo) typeof (GridViewSummaryRowItem).GetConstructor(new Type[1]
      {
        typeof (GridViewSummaryItem[])
      }), (ICollection) new object[1]
      {
        (object) viewSummaryRowItem.Fields
      });
    }
  }
}
