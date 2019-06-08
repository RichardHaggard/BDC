// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.CommandInstanceConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Telerik.WinControls.Keyboard;

namespace Telerik.WinControls.Commands
{
  internal class CommandInstanceConverter : CommandBaseConverter
  {
    public CommandInstanceConverter(Type type)
      : base(type)
    {
    }

    public override TypeConverter.StandardValuesCollection GetStandardValuesCore(
      ITypeDescriptorContext context,
      TypeConverter.StandardValuesCollection collection)
    {
      List<CommandBase> commandBaseList = CommandBaseConverter.DiscoverCommands(collection);
      commandBaseList?.Insert(0, (CommandBase) null);
      return new TypeConverter.StandardValuesCollection((ICollection) commandBaseList);
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      if (!(context.Instance is InputBinding))
        return base.GetStandardValues(context);
      TypeConverter.StandardValuesCollection componentsReferences = this.GetComponentsReferences(context);
      return this.GetStandardValuesCore(context, componentsReferences);
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
      if (value is ICommand)
        return (object) (value as ICommand).ToString();
      return base.ConvertTo(context, culture, value, destinationType);
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
        List<CommandBase> commandBaseList = CommandBaseConverter.DiscoverCommands(this.GetComponentsReferences(context));
        if (commandBaseList != null && commandBaseList.Count > 0)
        {
          for (int index = 0; index < commandBaseList.Count; ++index)
          {
            string str = commandBaseList[index].ToString();
            if (a.Equals(str))
              return (object) commandBaseList[index];
          }
        }
      }
      return base.ConvertFrom(context, culture, value);
    }
  }
}
