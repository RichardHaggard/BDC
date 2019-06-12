// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.XmlSerializerExtensionServiceProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.XmlSerialization
{
  public class XmlSerializerExtensionServiceProvider : IServiceProvider, IProvideTargetValue
  {
    private IPropertiesProvider propertiesProvider;
    private object targetObject;
    private object targetProperty;
    private string sourceValue;

    public XmlSerializerExtensionServiceProvider(
      IPropertiesProvider propertiesProvider,
      object targetObject,
      object targetProperty,
      string sourceValue)
    {
      this.propertiesProvider = propertiesProvider;
      this.targetObject = targetObject;
      this.sourceValue = sourceValue;
      this.targetProperty = targetProperty;
    }

    public object GetService(Type serviceType)
    {
      if ((object) serviceType == (object) typeof (IProvideTargetValue))
        return (object) this;
      if ((object) serviceType == (object) typeof (IPropertiesProvider))
        return (object) this.propertiesProvider;
      return (object) null;
    }

    public object TargetObject
    {
      get
      {
        return this.targetObject;
      }
    }

    public object TargetProperty
    {
      get
      {
        return this.targetProperty;
      }
    }

    public string SourceValue
    {
      get
      {
        return this.sourceValue;
      }
    }
  }
}
