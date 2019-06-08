// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.CommandContextConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using Telerik.WinControls.Keyboard;

namespace Telerik.WinControls.Commands
{
  public class CommandContextConverter : CommandBaseConverter
  {
    public CommandContextConverter(Type type)
      : base(type)
    {
    }

    public override TypeConverter.StandardValuesCollection GetStandardValuesCore(
      ITypeDescriptorContext context,
      TypeConverter.StandardValuesCollection collection)
    {
      List<IComponent> componentList = CommandBaseConverter.DiscoverCommandsContexts(collection);
      if (context != null && componentList != null && context.GetService(typeof (IDesignerHost)) != null)
      {
        for (int index = componentList.Count - 1; index >= 0; --index)
        {
          if (componentList[index].Site == null)
            componentList.RemoveAt(index);
        }
        componentList.Insert(0, (IComponent) null);
      }
      return new TypeConverter.StandardValuesCollection((ICollection) componentList);
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      if (!(context.Instance is InputBinding))
        return base.GetStandardValues(context);
      TypeConverter.StandardValuesCollection componentsReferences = this.GetComponentsReferences(context);
      return this.GetStandardValuesCore(context, componentsReferences);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string))
        return base.ConvertFrom(context, culture, value);
      string a = ((string) value).Trim();
      if (!string.Equals(a, CommandBaseConverter.none) && context != null)
      {
        List<IComponent> componentList = CommandBaseConverter.DiscoverCommandsContexts(this.GetComponentsReferences(context));
        if (componentList != null && componentList.Count > 0)
        {
          for (int index = 0; index < componentList.Count; ++index)
          {
            string str = componentList[index].ToString();
            if (a.Equals(str))
              return (object) componentList[index];
          }
        }
      }
      return base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == null)
        throw new ArgumentNullException("Destination Type is not defined.");
      if ((object) destinationType != (object) typeof (string))
        return base.ConvertTo(context, culture, value, destinationType);
      if (value == null)
        return (object) CommandBaseConverter.none;
      if (context == null)
        return (object) string.Empty;
      if (!(value is IComponent))
        return base.ConvertTo(context, culture, value, destinationType);
      try
      {
        return (object) (value as IComponent).Site.Name;
      }
      catch (Exception ex)
      {
        return (object) CommandBaseConverter.none;
      }
    }
  }
}
