// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.ObjectSerializationInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.WinControls.XmlSerialization
{
  internal class ObjectSerializationInfo
  {
    private Type objectType;
    private bool isSerializedAsString;
    private RadProperty radProperty;
    private SerializationConverter converter;
    private TypeConverter typeConverter;
    private object propertyOwner;
    private PropertyDescriptor property;
    private bool shouldWaitForAttribute;
    private object currentPropertyValue;
    private string tempStringValueFromDeserialization;
    private static Dictionary<string, Type> builtInExtensionMap;
    private ComponentXmlSerializer serializer;

    private ObjectSerializationInfo(ComponentXmlSerializer serializer)
    {
      this.serializer = serializer;
    }

    public ObjectSerializationInfo(
      ComponentXmlSerializer serializer,
      SerializationConverter converter,
      object propertyOwner,
      PropertyDescriptor property)
      : this(serializer)
    {
      this.converter = converter;
      this.propertyOwner = propertyOwner;
      this.property = property;
      if (converter != null)
        this.InitializeConverters();
      else
        this.isSerializedAsString = false;
    }

    private void InitializeConverters()
    {
      this.objectType = this.converter.GetActualPropertyType(this.propertyOwner, this.Property);
      if ((object) this.objectType == null)
      {
        if (this.shouldWaitForAttribute)
          throw new InvalidOperationException("Attribute PropertyName of a PropertySetting not found during deserialization.");
        this.shouldWaitForAttribute = true;
      }
      else
      {
        this.radProperty = this.converter.GetRadProperty(this.propertyOwner, this.Property);
        TypeConverter typeConverter = (TypeConverter) null;
        if (this.radProperty != null)
        {
          PropertyDescriptor clrProperty = this.radProperty.FindClrProperty();
          if (clrProperty != null)
            typeConverter = clrProperty.Converter;
        }
        if (typeConverter == null)
          typeConverter = TypeDescriptor.GetConverter(this.objectType);
        if (typeConverter.CanConvertFrom(typeof (string)) && typeConverter.CanConvertTo(typeof (string)))
        {
          this.isSerializedAsString = true;
          this.typeConverter = typeConverter;
          this.converter = (SerializationConverter) null;
        }
        else
          this.isSerializedAsString = false;
      }
    }

    public ObjectSerializationInfo(
      ComponentXmlSerializer serializer,
      TypeConverter typeConverter,
      object propertyOwner,
      PropertyDescriptor property)
      : this(serializer)
    {
      if (typeConverter != null)
      {
        this.currentPropertyValue = this.serializer.GetPropertyValue(property, propertyOwner);
        this.objectType = this.currentPropertyValue.GetType();
        this.isSerializedAsString = true;
        this.radProperty = (RadProperty) null;
        this.typeConverter = typeConverter;
        this.propertyOwner = propertyOwner;
        this.property = property;
      }
      else
        this.isSerializedAsString = false;
    }

    public ObjectSerializationInfo(
      ComponentXmlSerializer serializer,
      Type objectType,
      TypeConverter typeConverter,
      object propertyOwner,
      PropertyDescriptor property)
      : this(serializer)
    {
      if ((object) objectType != null)
      {
        this.objectType = objectType;
        this.isSerializedAsString = typeConverter != null || (object) objectType == (object) typeof (string);
        this.radProperty = (RadProperty) null;
        this.typeConverter = typeConverter;
        this.propertyOwner = propertyOwner;
        this.property = property;
      }
      else
        this.isSerializedAsString = false;
    }

    public ObjectSerializationInfo(
      ComponentXmlSerializer serializer,
      Type objectType,
      TypeConverter typeConverter,
      object currentValue)
      : this(serializer)
    {
      if ((object) objectType != null)
      {
        this.objectType = objectType;
        this.isSerializedAsString = typeConverter != null || (object) objectType == (object) typeof (string);
        this.radProperty = (RadProperty) null;
        this.typeConverter = typeConverter;
        this.currentPropertyValue = currentValue;
      }
      else
        this.isSerializedAsString = false;
    }

    public ObjectSerializationInfo(
      ComponentXmlSerializer serializer,
      Type objectType,
      object currentValue)
      : this(serializer)
    {
      if ((object) objectType != null)
      {
        this.objectType = objectType;
        this.isSerializedAsString = (object) objectType == (object) typeof (string);
        this.radProperty = (RadProperty) null;
        this.currentPropertyValue = currentValue;
      }
      else
        this.isSerializedAsString = false;
    }

    public Type ObjectType
    {
      get
      {
        return this.objectType;
      }
    }

    public bool IsSerializedAsString
    {
      get
      {
        return this.isSerializedAsString;
      }
    }

    public RadProperty RadProperty
    {
      get
      {
        return this.radProperty;
      }
    }

    public bool ShouldWaitForAttribute
    {
      get
      {
        return this.shouldWaitForAttribute;
      }
    }

    public PropertyDescriptor Property
    {
      get
      {
        return this.property;
      }
    }

    public string ConvertToString()
    {
      object currPropertyValue = this.GetCurrPropertyValue();
      IValueProvider valueProvider = currPropertyValue as IValueProvider;
      if (valueProvider != null)
        currPropertyValue = valueProvider.GetValue();
      if (this.converter != null)
        return this.converter.ConvertToString(this.propertyOwner, this.Property, currPropertyValue);
      if (this.typeConverter != null)
        return this.typeConverter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, currPropertyValue);
      if ((object) this.objectType == (object) typeof (string))
        return (string) currPropertyValue;
      return (string) null;
    }

    public object GetCurrPropertyValue()
    {
      if (this.currentPropertyValue == null)
        return this.serializer.GetPropertyValue(this.Property, this.propertyOwner);
      return this.currentPropertyValue;
    }

    public object ConvertFromString(string value)
    {
      if (!string.IsNullOrEmpty(value) && value.Length > 2)
      {
        string sourceValue = value.Trim();
        if (sourceValue.StartsWith("{") && sourceValue.EndsWith("}"))
        {
          object retValue = (object) null;
          if (this.DelegateParsingToExtender(sourceValue, this.propertyOwner, this.objectType, this.typeConverter, out retValue))
            return retValue;
        }
      }
      if (this.converter != null)
        return this.converter.ConvertFromString(this.propertyOwner, this.Property, value);
      if (this.typeConverter != null)
        return this.typeConverter.ConvertFromString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, value);
      if ((object) this.objectType == (object) typeof (string))
        return (object) value;
      return (object) null;
    }

    private static Dictionary<string, Type> GetBuiltInExtensionMap()
    {
      if (ObjectSerializationInfo.builtInExtensionMap == null)
      {
        ObjectSerializationInfo.builtInExtensionMap = new Dictionary<string, Type>();
        ObjectSerializationInfo.builtInExtensionMap.Add("RelativeColor", typeof (ColorBlendExtension));
        ObjectSerializationInfo.builtInExtensionMap.Add("ColorBlendExtension", typeof (ColorBlendExtension));
        ObjectSerializationInfo.builtInExtensionMap.Add("ParamRef", typeof (ParameterReferenceExtension));
        ObjectSerializationInfo.builtInExtensionMap.Add("ParameterReferenceExtension", typeof (ParameterReferenceExtension));
      }
      return ObjectSerializationInfo.builtInExtensionMap;
    }

    private bool DelegateParsingToExtender(
      string sourceValue,
      object targetObject,
      Type targetObjectType,
      TypeConverter originalTypeConverter,
      out object retValue)
    {
      retValue = (object) null;
      int num = sourceValue.IndexOf(":");
      if (num < 2)
        return false;
      string key = sourceValue.Substring(1, num - 1);
      string sourceValue1 = sourceValue.Substring(num + 1, sourceValue.Length - num - 2);
      Type type;
      ObjectSerializationInfo.GetBuiltInExtensionMap().TryGetValue(key, out type);
      if ((object) type == null)
        return false;
      if (!typeof (XmlSerializerExtention).IsAssignableFrom(type))
        throw new NotSupportedException(string.Format("Xml extension type not supported {0}. Extension type must inherit {1}.", (object) type, (object) typeof (XmlSerializerExtention)));
      XmlSerializerExtention instance = (XmlSerializerExtention) Activator.CreateInstance(type);
      XmlSerializerExtensionServiceProvider extensionServiceProvider = new XmlSerializerExtensionServiceProvider(this.serializer.PropertiesProvider, targetObject, (object) this.property, sourceValue1);
      retValue = instance.ProvideValue((IServiceProvider) extensionServiceProvider);
      return true;
    }

    public void SetTempStringValueFromDeserialization(string value)
    {
      this.tempStringValueFromDeserialization = value;
    }

    public object ConvertFromDelayedDeserialization()
    {
      this.InitializeConverters();
      return this.ConvertFromString(this.tempStringValueFromDeserialization);
    }
  }
}
