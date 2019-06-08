// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.SerializationConverterAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.XmlSerialization
{
  public class SerializationConverterAttribute : Attribute
  {
    private Type converterType;

    public SerializationConverterAttribute(Type converterType)
    {
      if (!typeof (SerializationConverter).IsAssignableFrom(converterType))
        return;
      this.converterType = converterType;
    }

    public Type ConverterType
    {
      get
      {
        return this.converterType;
      }
    }

    public SerializationConverter GetConverterInstance()
    {
      if ((object) this.converterType == null)
        throw new InvalidOperationException(string.Format("converterType not specified. Please make sure the SerializationConverterAttribute attribute specifies a type that inherits from {0}", (object) typeof (SerializationConverter)));
      return (SerializationConverter) Activator.CreateInstance(this.converterType);
    }
  }
}
