// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Commands.CommandBaseConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Telerik.WinControls.Elements;

namespace Telerik.WinControls.Commands
{
  public class CommandBaseConverter : TypeConverter
  {
    protected static string none = "(none)";
    protected Type type;
    protected List<CommandBase> commands;
    protected List<IComponent> commandSources;

    public CommandBaseConverter(Type type)
    {
      this.type = type;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if ((object) sourceType == (object) typeof (string) && context != null)
        return true;
      return base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      if ((object) destinationType == (object) typeof (string) && context != null)
        return true;
      return base.CanConvertTo(context, destinationType);
    }

    protected virtual bool IsValueAllowed(ITypeDescriptorContext context, object value)
    {
      return true;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return true;
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(value, attributes);
    }

    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      if (!(value is string))
        return base.ConvertFrom(context, culture, value);
      string index = ((string) value).Trim();
      if (!string.Equals(index, CommandBaseConverter.none) && context != null)
      {
        IReferenceService service = (IReferenceService) context.GetService(typeof (IReferenceService));
        if (service != null)
        {
          object reference = service.GetReference(index);
          if (reference != null)
            return reference;
        }
        IContainer container = context.Container;
        if (container != null)
        {
          object component = (object) container.Components[index];
          if (component != null)
            return component;
        }
      }
      return (object) null;
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
      if (context != null)
      {
        IReferenceService service = (IReferenceService) context.GetService(typeof (IReferenceService));
        if (service != null)
        {
          string name = service.GetName(value);
          if (name != null)
            return (object) name;
        }
      }
      if (!Marshal.IsComObject(value) && value is IComponent)
      {
        ISite site = ((IComponent) value).Site;
        if (site != null)
        {
          string name = site.Name;
          if (name != null)
            return (object) name;
        }
      }
      return (object) string.Empty;
    }

    public virtual TypeConverter.StandardValuesCollection GetStandardValuesCore(
      ITypeDescriptorContext context,
      TypeConverter.StandardValuesCollection collection)
    {
      return new TypeConverter.StandardValuesCollection((ICollection) null);
    }

    protected virtual TypeConverter.StandardValuesCollection GetComponentsReferences(
      ITypeDescriptorContext context)
    {
      return CommandBaseConverter.GetReferences(context, typeof (IComponent));
    }

    public static TypeConverter.StandardValuesCollection GetReferences(
      IServiceProvider context,
      Type type)
    {
      if (context != null)
      {
        ArrayList arrayList = new ArrayList();
        IReferenceService service = (IReferenceService) context.GetService(typeof (IReferenceService));
        if (service != null)
        {
          object[] references = service.GetReferences(type);
          int length = references.Length;
          for (int index = 0; index < length; ++index)
          {
            IComponent component = references[index] as IComponent;
            if (component != null && component.Site != null && component.Site.DesignMode)
              arrayList.Add(references[index]);
          }
          return new TypeConverter.StandardValuesCollection((ICollection) arrayList.ToArray());
        }
      }
      return (TypeConverter.StandardValuesCollection) null;
    }

    internal static TypeConverter.StandardValuesCollection GetReferences(
      ITypeDescriptorContext context,
      Type type)
    {
      object[] objArray = (object[]) null;
      if (context != null)
      {
        ArrayList arrayList = new ArrayList();
        arrayList.Add((object) null);
        IReferenceService service = (IReferenceService) context.GetService(typeof (IReferenceService));
        if (service != null)
        {
          object[] references = service.GetReferences(type);
          int length = references.Length;
          for (int index = 0; index < length; ++index)
            arrayList.Add(references[index]);
        }
        else
        {
          IContainer container = context.Container;
          if (container != null)
          {
            foreach (IComponent component in (ReadOnlyCollectionBase) container.Components)
            {
              if (component != null && type.IsInstanceOfType((object) component))
                arrayList.Add((object) component);
            }
          }
        }
        objArray = arrayList.ToArray();
      }
      return new TypeConverter.StandardValuesCollection((ICollection) objArray);
    }

    public static List<IComponent> DiscoverCommandsSources(
      ICommand command,
      TypeConverter.StandardValuesCollection collection)
    {
      List<IComponent> componentList1 = CommandBaseConverter.DiscoverCommandsSources(collection);
      List<IComponent> componentList2 = new List<IComponent>(1);
      for (int index = 0; index < componentList1.Count; ++index)
      {
        if (componentList1[index].GetType().IsAssignableFrom(command.ContextType))
          componentList2.Add(componentList1[index]);
      }
      if (componentList2.Count > 0)
        return componentList2;
      return (List<IComponent>) null;
    }

    public static List<IComponent> DiscoverCommandsSources(
      TypeConverter.StandardValuesCollection collection)
    {
      List<IComponent> list = new List<IComponent>(1);
      for (int index = 0; index < collection.Count; ++index)
      {
        IComponent component = collection[index] as IComponent;
        if (component != null)
          list.Add(component);
      }
      List<IComponent> componentList = CommandBaseConverter.DiscoverCommandsSources(list);
      if (componentList != null && componentList.Count > 0)
        return componentList;
      return (List<IComponent>) null;
    }

    public static List<IComponent> DiscoverCommandsContexts(
      TypeConverter.StandardValuesCollection collection)
    {
      List<IComponent> list = new List<IComponent>(1);
      List<IComponent> componentList1 = new List<IComponent>(1);
      for (int index = 0; index < collection.Count; ++index)
      {
        IComponent component = collection[index] as IComponent;
        if (component != null)
          list.Add(component);
      }
      List<IComponent> componentList2 = CommandBaseConverter.DiscoverCommandsContexts(list);
      if (componentList2 != null && componentList2.Count > 0)
        return componentList2;
      return (List<IComponent>) null;
    }

    public static List<IComponent> DiscoverCommandsContexts(List<IComponent> list)
    {
      List<IComponent> destinationList = new List<IComponent>(1);
      for (int index = 0; index < list.Count; ++index)
      {
        List<IComponent> sourceList = CommandBaseConverter.DiscoverCommandsContexts(list[index]);
        if (sourceList != null && sourceList.Count > 0)
          CommandBaseConverter.TransferListUniquePart<IComponent>(sourceList, destinationList);
      }
      if (destinationList.Count > 0)
        return destinationList;
      return (List<IComponent>) null;
    }

    public static List<IComponent> DiscoverCommandsContexts(IComponent source)
    {
      if (source == null)
        return (List<IComponent>) null;
      List<IComponent> destinationList = new List<IComponent>(1);
      if (typeof (RadControl).IsAssignableFrom(source.GetType()) || typeof (RadItem).IsAssignableFrom(source.GetType()))
        CommandBaseConverter.TransferListUniquePart<IComponent>(source, destinationList);
      if (source is RadControl)
      {
        RootRadElement rootElement = (source as RadControl).RootElement;
        if (rootElement != null && rootElement.Children.Count > 0)
        {
          foreach (RadElement child in rootElement.Children)
          {
            IComponent source1 = child as IComponent;
            if (source1 != null && child is IItemsOwner)
            {
              List<IComponent> sourceList = CommandBaseConverter.DiscoverCommandsContexts(source1);
              if (destinationList != null)
                CommandBaseConverter.TransferListUniquePart<IComponent>(sourceList, destinationList);
            }
          }
        }
      }
      if (destinationList.Count > 0)
        return destinationList;
      return (List<IComponent>) null;
    }

    public static List<IComponent> DiscoverCommandsSources(List<IComponent> list)
    {
      List<IComponent> destinationList = new List<IComponent>(1);
      for (int index = 0; index < list.Count; ++index)
      {
        List<IComponent> sourceList = CommandBaseConverter.DiscoverCommandsSources(list[index]);
        if (sourceList != null && sourceList.Count > 0)
          CommandBaseConverter.TransferListUniquePart<IComponent>(sourceList, destinationList);
      }
      if (destinationList.Count > 0)
        return destinationList;
      return (List<IComponent>) null;
    }

    public static List<IComponent> DiscoverCommandsSources(IComponent source)
    {
      if (source == null)
        return (List<IComponent>) null;
      List<IComponent> destinationList = new List<IComponent>(1);
      if (CommandBaseConverter.DiscoverCommands(source) != null)
        CommandBaseConverter.TransferListUniquePart<IComponent>(source, destinationList);
      if (source is RadControl)
      {
        RootRadElement rootElement = (source as RadControl).RootElement;
        if (rootElement != null && rootElement.Children.Count > 0)
        {
          for (int index = 0; index < rootElement.Children.Count; ++index)
          {
            IComponent child = rootElement.Children[index] as IComponent;
            if (child != null && child is IItemsOwner)
            {
              List<IComponent> sourceList = CommandBaseConverter.DiscoverCommandsSources(child);
              if (destinationList != null)
                CommandBaseConverter.TransferListUniquePart<IComponent>(sourceList, destinationList);
            }
          }
        }
      }
      if (destinationList.Count > 0)
        return destinationList;
      return (List<IComponent>) null;
    }

    protected static void TransferListUniquePart<T>(List<T> sourceList, List<T> destinationList)
    {
      for (int index = 0; index < sourceList.Count; ++index)
        CommandBaseConverter.TransferListUniquePart<T>(sourceList[index], destinationList);
    }

    protected static void TransferListUniquePart<T>(T sourceItem, List<T> destinationList)
    {
      if (destinationList.Contains(sourceItem))
        return;
      destinationList.Add(sourceItem);
    }

    public static List<CommandBase> DiscoverCommands(
      TypeConverter.StandardValuesCollection collection)
    {
      List<CommandBase> commandBaseList = CommandBaseConverter.DiscoverCommands(CommandBaseConverter.DiscoverCommandsSources(collection));
      if (commandBaseList != null && commandBaseList.Count > 0)
        return commandBaseList;
      return (List<CommandBase>) null;
    }

    public static List<CommandBase> DiscoverCommands(List<IComponent> list)
    {
      List<CommandBase> destinationList = new List<CommandBase>(1);
      if (list != null)
      {
        for (int index = 0; index < list.Count; ++index)
        {
          List<CommandBase> sourceList = CommandBaseConverter.DiscoverCommands(list[index]);
          if (sourceList != null && sourceList.Count > 0)
            CommandBaseConverter.TransferListUniquePart<CommandBase>(sourceList, destinationList);
        }
      }
      if (destinationList.Count > 0)
        return destinationList;
      return (List<CommandBase>) null;
    }

    public static List<CommandBase> DiscoverCommands(IComponent source)
    {
      if (source == null)
        return (List<CommandBase>) null;
      List<CommandBase> commandBaseList = new List<CommandBase>();
      foreach (FieldInfo field in source.GetType().GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.GetField))
      {
        CommandBase commandBase = field.GetValue((object) source) as CommandBase;
        if (commandBase != null)
          commandBaseList.Add(commandBase);
      }
      if (commandBaseList.Count > 0)
        return commandBaseList;
      return (List<CommandBase>) null;
    }
  }
}
