// Decompiled with JetBrains decompiler
// Type: Telerik.Licensing.Device
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace Telerik.Licensing
{
  internal abstract class Device : IDisposable
  {
    private static readonly IDictionary<Type, string> types = (IDictionary<Type, string>) new Dictionary<Type, string>();
    private static readonly object typesLock = new object();
    private readonly ManagementClass _managmentClass;

    protected Device(string scope)
    {
      this._managmentClass = new ManagementClass(scope);
    }

    protected ManagementClass ManagementClass
    {
      get
      {
        return this._managmentClass;
      }
    }

    private static IDictionary<Type, string> Types
    {
      get
      {
        return Device.types;
      }
    }

    public static string GetId(Type type)
    {
      if (!typeof (Device).IsAssignableFrom(type))
        throw new NotSupportedException("Type must inherit from Telerik.Device");
      if (!Device.Types.ContainsKey(type))
      {
        lock (Device.typesLock)
        {
          if (!Device.Types.ContainsKey(type))
            Device.Types[type] = ((Device) Activator.CreateInstance(type)).ReadId();
        }
      }
      return Device.Types[type];
    }

    public abstract string[] GetWmiProperties();

    public void Dispose()
    {
      this.Dispose(true);
    }

    protected virtual string ReadId()
    {
      StringBuilder stringBuilder = new StringBuilder();
      ManagementObjectCollection instances = this.ManagementClass.GetInstances();
      try
      {
        foreach (ManagementBaseObject managementBaseObject in instances)
        {
          IDictionary<string, string> dictionary = this.PopulateProperties(managementBaseObject);
          if (this.ValidateProperties(dictionary))
            stringBuilder.Append(this.StringifyProperties(dictionary.Values));
        }
      }
      finally
      {
        instances.Dispose();
      }
      return stringBuilder.ToString();
    }

    protected virtual bool ValidateProperties(IDictionary<string, string> obj)
    {
      return true;
    }

    protected virtual string StringifyProperties(ICollection<string> valuesCollection)
    {
      string[] array = new string[valuesCollection.Count];
      valuesCollection.CopyTo(array, 0);
      return string.Join(";", array) + ";";
    }

    protected virtual IDictionary<string, string> PopulateProperties(
      ManagementBaseObject obj)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (string wmiProperty in this.GetWmiProperties())
      {
        string propertyValue = obj.GetPropertyValue(wmiProperty) as string;
        if (!string.IsNullOrEmpty(propertyValue))
          dictionary[wmiProperty] = propertyValue;
      }
      return (IDictionary<string, string>) dictionary;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.ManagementClass == null)
        return;
      this._managmentClass.Dispose();
    }
  }
}
