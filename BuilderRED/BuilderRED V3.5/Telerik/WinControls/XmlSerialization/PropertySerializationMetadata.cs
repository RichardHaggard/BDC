// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.PropertySerializationMetadata
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.XmlSerialization
{
  public class PropertySerializationMetadata
  {
    private bool? shouldSerializeProperty = new bool?();
    private string propertyName;
    private string typeFullName;
    private Type type;
    private AttributeCollection attributes;

    public PropertySerializationMetadata(
      string typeFullName,
      string propertyName,
      params Attribute[] attributes)
      : this(typeFullName, propertyName, new AttributeCollection(attributes))
    {
    }

    public PropertySerializationMetadata(
      Type targetType,
      string propertyName,
      params Attribute[] attributes)
      : this(targetType, propertyName, new AttributeCollection(attributes))
    {
    }

    public PropertySerializationMetadata(
      string targetTypeFullName,
      string propertyName,
      AttributeCollection attributes)
    {
      this.propertyName = propertyName;
      this.attributes = attributes;
      this.typeFullName = targetTypeFullName;
    }

    public PropertySerializationMetadata(
      string targetTypeFullName,
      string propertyName,
      bool shouldSerialize)
    {
      this.propertyName = propertyName;
      this.typeFullName = targetTypeFullName;
      this.shouldSerializeProperty = new bool?(shouldSerialize);
    }

    public PropertySerializationMetadata(
      Type targetType,
      string propertyName,
      AttributeCollection attributes)
    {
      this.propertyName = propertyName;
      this.attributes = attributes;
      this.type = targetType;
    }

    public PropertySerializationMetadata(
      Type targetType,
      string propertyName,
      bool shouldSerialize)
    {
      this.propertyName = propertyName;
      this.type = targetType;
      this.shouldSerializeProperty = new bool?(shouldSerialize);
    }

    public string PropertyName
    {
      get
      {
        return this.propertyName;
      }
    }

    public string TypeFullName
    {
      get
      {
        if ((object) this.type != null)
          return this.type.FullName;
        return this.typeFullName;
      }
    }

    public Type Type
    {
      get
      {
        return this.type;
      }
    }

    public AttributeCollection Attributes
    {
      get
      {
        if (this.attributes == null)
          this.attributes = new AttributeCollection(new Attribute[0]);
        return this.attributes;
      }
      set
      {
        this.attributes = value;
      }
    }

    public virtual bool? ShouldSerializeProperty
    {
      get
      {
        return this.shouldSerializeProperty;
      }
      set
      {
        this.shouldSerializeProperty = value;
      }
    }
  }
}
