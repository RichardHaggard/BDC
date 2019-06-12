// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.SerializationConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.XmlSerialization
{
  public abstract class SerializationConverter
  {
    public abstract string ConvertToString(
      object propertyOwner,
      PropertyDescriptor property,
      object value);

    public abstract object ConvertFromString(
      object propertyOwner,
      PropertyDescriptor property,
      string value);

    public abstract Type GetActualPropertyType(
      object propertyOwner,
      PropertyDescriptor property);

    public virtual RadProperty GetRadProperty(
      object propertyOwner,
      PropertyDescriptor property)
    {
      return (RadProperty) null;
    }
  }
}
