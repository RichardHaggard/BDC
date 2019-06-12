// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.ComponentXmlSerializer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Telerik.WinControls.XmlSerialization
{
  public class ComponentXmlSerializer
  {
    private XmlSerializerDisposalBin disposalBin = new XmlSerializerDisposalBin();
    private IDictionary typeMap;
    private IDictionary reversedTypeMap;
    private ComponentXmlSerializationInfo componentSerializationInfo;
    private object rootSerializationObject;
    private IPropertiesProvider propertiesProvider;
    private bool resolveTypesOnlyInTelerikAssemblies;
    private InstanceFactory instanceFactory;

    public ComponentXmlSerializer()
    {
      this.componentSerializationInfo = new ComponentXmlSerializationInfo(new PropertySerializationMetadataCollection());
    }

    public ComponentXmlSerializer(
      ComponentXmlSerializationInfo componentSerializationInfo)
    {
      this.componentSerializationInfo = componentSerializationInfo;
      if (this.componentSerializationInfo != null)
        return;
      this.componentSerializationInfo = new ComponentXmlSerializationInfo(new PropertySerializationMetadataCollection());
    }

    public ComponentXmlSerializer(
      IDictionary typeMap,
      ComponentXmlSerializationInfo componentSerializationInfo)
      : this(componentSerializationInfo)
    {
      this.typeMap = typeMap;
      if (this.typeMap == null)
        return;
      this.reversedTypeMap = (IDictionary) new Hashtable(this.typeMap.Count);
      foreach (DictionaryEntry type in typeMap)
        this.reversedTypeMap[type.Value] = type.Key;
    }

    public object RootSerializationObject
    {
      get
      {
        return this.rootSerializationObject;
      }
    }

    public IPropertiesProvider PropertiesProvider
    {
      get
      {
        return this.propertiesProvider;
      }
      set
      {
        this.propertiesProvider = value;
      }
    }

    public bool ResolveTypesOnlyInTelerikAssemblies
    {
      get
      {
        return this.resolveTypesOnlyInTelerikAssemblies;
      }
      set
      {
        this.resolveTypesOnlyInTelerikAssemblies = value;
      }
    }

    protected void ReadDictionaryElement(
      XmlReader reader,
      object dictionaryOwner,
      IDictionary toRead)
    {
      this.ReadCollectionElement(reader, dictionaryOwner, (IList) new DictionarySerializationListWrapper(toRead));
    }

    public void ReadCollectionElement(XmlReader reader, IList toRead)
    {
      this.ReadCollectionElement(reader, (object) null, toRead);
    }

    public void ReadCollectionElement(XmlReader reader, object collectionOwner, IList toRead)
    {
      this.ReadCollectionElement(reader, collectionOwner, toRead, false);
    }

    public void ReadCollectionElement(
      XmlReader reader,
      object collectionOwner,
      IList toRead,
      bool disposeObjects)
    {
      if (this.RootSerializationObject == null)
      {
        this.rootSerializationObject = (object) toRead;
        this.InitializeRead();
      }
      if (reader.IsEmptyElement)
        return;
      int depth = reader.Depth;
      if (disposeObjects)
        this.disposalBin.AddObjectsToDispose((IEnumerable) toRead);
      toRead.Clear();
      while (reader.ReadState != ReadState.Error && !reader.EOF && (reader.NodeType != XmlNodeType.EndElement || reader.Depth != depth))
      {
        reader.Read();
        switch (reader.NodeType)
        {
          case XmlNodeType.Element:
            if (reader.Depth == depth + 1)
            {
              ObjectSerializationInfo info;
              try
              {
                info = this.ResolveSerializationInfo(reader, (PropertyDescriptor) null, (object) toRead);
              }
              catch (Exception ex)
              {
                this.SkipUnknownXml(reader);
                continue;
              }
              Type objectType = info.ObjectType;
              if ((object) objectType != null)
              {
                object toRead1;
                if (info.IsSerializedAsString)
                {
                  toRead1 = this.ReadElementValueAsObject(reader, info);
                }
                else
                {
                  try
                  {
                    toRead1 = this.InstanceFactory.CreateInstance(objectType);
                  }
                  catch (TargetInvocationException ex)
                  {
                    continue;
                  }
                  catch (MissingMethodException ex)
                  {
                    continue;
                  }
                  this.ReadObjectElement(reader, collectionOwner, toRead1);
                }
                toRead.Add(toRead1);
                continue;
              }
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }

    public virtual InstanceFactory InstanceFactory
    {
      get
      {
        if (this.instanceFactory == null)
          this.instanceFactory = (InstanceFactory) new RuntimeInstanceFactory();
        return this.instanceFactory;
      }
      set
      {
        this.instanceFactory = value;
      }
    }

    private object CreateInstance(Type type)
    {
      return Activator.CreateInstance(type);
    }

    protected void ReadMergeCollection(
      XmlReader reader,
      object parent,
      PropertyDescriptor parentProperty,
      IList toRead,
      string uniquePropertyName)
    {
      this.ReadMergeCollection(reader, parent, parentProperty, toRead, uniquePropertyName, false, false);
    }

    protected void ReadMergeCollection(
      XmlReader reader,
      object parent,
      PropertyDescriptor parentProperty,
      IList toRead,
      string uniquePropertyName,
      bool disposeObjects)
    {
      this.ReadMergeCollection(reader, parent, parentProperty, toRead, uniquePropertyName, false, disposeObjects);
    }

    protected void ReadMergeCollection(
      XmlReader reader,
      object parent,
      PropertyDescriptor parentProperty,
      IList toRead,
      string uniquePropertyName,
      bool preserveNotSerializedExistingElements,
      bool disposeObjects)
    {
      if (reader.IsEmptyElement)
        return;
      int depth = reader.Depth;
      int index1 = 0;
      if (toRead == null)
        return;
      ArrayList arrayList = new ArrayList(toRead.Count);
      for (int index2 = 0; index2 < toRead.Count; ++index2)
        arrayList.Add(toRead[index2]);
      toRead.Clear();
      while (reader.NodeType != XmlNodeType.EndElement || reader.Depth != depth)
      {
        reader.Read();
        switch (reader.NodeType)
        {
          case XmlNodeType.Element:
            if (reader.Depth == depth + 1)
            {
              ObjectSerializationInfo info;
              try
              {
                info = this.ResolveSerializationInfo(reader, (PropertyDescriptor) null, (object) toRead);
              }
              catch (Exception ex)
              {
                this.SkipUnknownXml(reader);
                continue;
              }
              Type objectType = info.ObjectType;
              if ((object) objectType != null)
              {
                if (info.IsSerializedAsString)
                {
                  this.ReadElementValueAsObject(reader, info);
                  continue;
                }
                if (uniquePropertyName != null)
                {
                  int foundAtIndex;
                  object obj = this.MatchObjectElement(reader, parent, parentProperty, toRead, uniquePropertyName, (IList) arrayList, out foundAtIndex);
                  if (obj == null)
                  {
                    obj = this.InstanceFactory.CreateInstance(objectType);
                  }
                  else
                  {
                    this.disposalBin.SetObjectShouldNotBeDisposed(obj);
                    if (foundAtIndex != -1)
                      arrayList.RemoveAt(foundAtIndex);
                  }
                  this.ReadObjectElement(reader, parent, obj);
                  toRead.Add(obj);
                }
                else if (arrayList.Count > index1)
                {
                  object toRead1 = arrayList[index1];
                  this.ReadObjectElement(reader, toRead1);
                  if (!preserveNotSerializedExistingElements)
                    toRead.Add(toRead1);
                }
                else
                {
                  object instance = this.InstanceFactory.CreateInstance(objectType);
                  toRead.Add(instance);
                  this.ReadObjectElement(reader, instance);
                }
                ++index1;
                continue;
              }
              continue;
            }
            continue;
          default:
            continue;
        }
      }
      if (preserveNotSerializedExistingElements)
      {
        for (int index2 = 0; index2 < arrayList.Count; ++index2)
          toRead.Add(arrayList[index2]);
      }
      else
      {
        if (!disposeObjects)
          return;
        for (int index2 = 0; index2 < arrayList.Count; ++index2)
          this.disposalBin.AddObjectToDispose(arrayList[index2]);
      }
    }

    private ObjectSerializationInfo ResolveSerializationInfo(
      PropertyDescriptor property,
      object propertyOwner)
    {
      if (property != null)
      {
        SerializationConverterAttribute attribute = (SerializationConverterAttribute) property.Attributes[typeof (SerializationConverterAttribute)];
        if (attribute != null)
          return new ObjectSerializationInfo(this, attribute.GetConverterInstance(), propertyOwner, property);
        if ((object) property.PropertyType == (object) typeof (string))
          return new ObjectSerializationInfo(this, property.PropertyType, (TypeConverter) null, propertyOwner, property);
        TypeConverter converter = property.Converter;
        if (converter.CanConvertTo(typeof (string)) && converter.CanConvertFrom(typeof (string)))
          return new ObjectSerializationInfo(this, property.PropertyType, converter, propertyOwner, property);
      }
      return new ObjectSerializationInfo(this, property.PropertyType, (TypeConverter) null, propertyOwner, property);
    }

    private ObjectSerializationInfo ResolveSerializationInfo(
      object currentObject)
    {
      Type type = currentObject.GetType();
      if ((object) type == (object) typeof (string))
        return new ObjectSerializationInfo(this, type, currentObject);
      TypeConverter converter = TypeDescriptor.GetConverter(type);
      if (converter.CanConvertTo(typeof (string)) && converter.CanConvertFrom(typeof (string)))
        return new ObjectSerializationInfo(this, type, converter, currentObject);
      return new ObjectSerializationInfo(this, type, currentObject);
    }

    private ObjectSerializationInfo ResolveSerializationInfo(
      XmlReader reader,
      PropertyDescriptor property,
      object propertyOwner)
    {
      string className = (string) null;
      string str = reader.LookupNamespace("http://www.w3.org/2001/XMLSchema-instance");
      if (string.IsNullOrEmpty(str))
        str = "xsi";
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name == str + ":type")
        {
          className = reader.Value;
          break;
        }
      }
      reader.MoveToElement();
      Type type = (Type) null;
      if (className == null && property == null)
        className = reader.Name;
      if (className != null)
      {
        type = this.FindTypeSafe(className);
        if ((object) type == null)
        {
          if (className != null && this.typeMap != null)
            type = (Type) this.typeMap[(object) className];
          if ((object) type == null && !className.Contains("."))
            type = RadTypeResolver.Instance.GetTypeByName(typeof (RadObject).Namespace + "." + className, false, true);
        }
      }
      if ((object) type == null && property != null)
      {
        SerializationConverterAttribute attribute = (SerializationConverterAttribute) property.Attributes[typeof (SerializationConverterAttribute)];
        if (attribute != null)
          return new ObjectSerializationInfo(this, attribute.GetConverterInstance(), propertyOwner, property);
        type = property.PropertyType;
      }
      else if (property != null)
      {
        if ((object) property.PropertyType == (object) typeof (string))
          return new ObjectSerializationInfo(this, typeof (string), (TypeConverter) null, propertyOwner, property);
        TypeConverter converter = property.Converter;
        if (converter.CanConvertTo(typeof (string)) && converter.CanConvertFrom(typeof (string)))
          return new ObjectSerializationInfo(this, type, converter, propertyOwner, property);
      }
      else if ((object) type != null)
      {
        TypeConverter converter = TypeDescriptor.GetConverter(type);
        if (converter.CanConvertTo(typeof (string)) && converter.CanConvertFrom(typeof (string)))
          return new ObjectSerializationInfo(this, type, converter, propertyOwner, property);
      }
      return new ObjectSerializationInfo(this, type, (TypeConverter) null, propertyOwner, property);
    }

    internal Type FindTypeSafe(string className)
    {
      return RadTypeResolver.Instance.GetTypeByName(className, false, this.ResolveTypesOnlyInTelerikAssemblies);
    }

    protected virtual object MatchObjectElement(
      XmlReader reader,
      object parent,
      PropertyDescriptor parentProperty,
      IList toRead,
      string propertyToMatch,
      IList existingInstancesToMatch,
      out int foundAtIndex)
    {
      string uniquePropertyValue = (string) null;
      while (reader.MoveToNextAttribute())
      {
        if (reader.Name == propertyToMatch)
        {
          uniquePropertyValue = reader.Value;
          break;
        }
      }
      reader.MoveToElement();
      object obj = (object) null;
      foundAtIndex = -1;
      if (uniquePropertyValue != null)
        obj = this.MatchExistingItem(reader, toRead, parent, parentProperty, propertyToMatch, uniquePropertyValue, existingInstancesToMatch, ref foundAtIndex);
      return obj;
    }

    protected virtual object MatchExistingItem(
      XmlReader reader,
      IList toRead,
      object parent,
      PropertyDescriptor parentProperty,
      string propertyToMatch,
      string uniquePropertyValue,
      IList existingInstancesToMatch,
      ref int foundAtIndex)
    {
      object obj1 = (object) null;
      PropertyDescriptor property = (PropertyDescriptor) null;
      for (int index = 0; index < existingInstancesToMatch.Count; ++index)
      {
        object obj2 = existingInstancesToMatch[index];
        if (obj2 != null)
        {
          if (property == null)
            property = TypeDescriptor.GetProperties(obj2).Find(propertyToMatch, false);
          if (property != null)
          {
            object propertyValue = this.GetPropertyValue(property, obj2);
            if (propertyValue != null && propertyValue.Equals((object) uniquePropertyValue))
            {
              obj1 = obj2;
              foundAtIndex = index;
              break;
            }
          }
        }
      }
      return obj1;
    }

    public void ReadObjectElement(XmlReader reader, object toRead)
    {
      this.ReadObjectElement(reader, (object) null, toRead);
    }

    public void ReadObjectElement(XmlReader reader, object parentObject, object toRead)
    {
      if (reader.EOF)
        return;
      if (this.RootSerializationObject == null)
      {
        this.rootSerializationObject = toRead;
        this.InitializeRead();
      }
      if (this.ReadObjectElementOverride(reader, toRead))
        return;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(toRead);
      int depth = reader.Depth;
      bool flag1 = false;
      if (reader.IsEmptyElement)
        flag1 = true;
      Hashtable hashtable = new Hashtable();
      List<ObjectSerializationInfo> serializationInfoList = new List<ObjectSerializationInfo>(1);
      while (reader.MoveToNextAttribute())
      {
        PropertyDescriptor property = properties.Find(reader.Name, true);
        if (property != null)
        {
          hashtable[(object) reader.Name] = (object) true;
          if (property.ComponentType.IsAssignableFrom(toRead.GetType()))
          {
            ObjectSerializationInfo serializationInfo;
            try
            {
              serializationInfo = this.ResolveSerializationInfo(property, toRead);
            }
            catch (Exception ex)
            {
              this.SkipUnknownXml(reader);
              return;
            }
            if (!string.IsNullOrEmpty(reader.Value))
            {
              if (serializationInfo.ShouldWaitForAttribute)
              {
                serializationInfo.SetTempStringValueFromDeserialization(reader.Value);
                serializationInfoList.Add(serializationInfo);
              }
              else if (serializationInfo.IsSerializedAsString)
              {
                object obj = serializationInfo.ConvertFromString(reader.Value);
                this.SetPropertyValue(property, toRead, obj);
              }
            }
          }
        }
      }
      foreach (ObjectSerializationInfo serializationInfo in serializationInfoList)
      {
        object obj = serializationInfo.ConvertFromDelayedDeserialization();
        if (serializationInfo.IsSerializedAsString)
          this.SetPropertyValue(serializationInfo.Property, toRead, obj);
      }
      if (flag1)
        return;
      if (toRead is IList)
      {
        reader.MoveToElement();
        this.ReadCollectionElement(reader, parentObject, (IList) toRead);
      }
      else
      {
        while (reader.ReadState != ReadState.Error && !reader.EOF && (reader.NodeType != XmlNodeType.EndElement || reader.Depth != depth))
        {
          reader.Read();
          switch (reader.NodeType)
          {
            case XmlNodeType.Element:
              if (reader.Depth == depth || reader.Depth == depth + 1)
              {
                hashtable[(object) reader.Name] = (object) true;
                PropertyDescriptor property = properties.Find(reader.Name, true);
                if (property != null)
                {
                  this.ReadElementInObject(reader, property, toRead);
                  continue;
                }
                continue;
              }
              continue;
            default:
              continue;
          }
        }
        foreach (PropertyDescriptor property in properties)
        {
          if (hashtable[(object) property.Name] == null && !this.ProcessProperty(property) && ((object) property.PropertyType != (object) typeof (Type) && property.ComponentType.IsAssignableFrom(toRead.GetType())))
          {
            Attribute attribute = (Attribute) null;
            bool flag2;
            bool flag3;
            if (attribute == null)
            {
              attribute = property.Attributes[typeof (XmlAttributeAttribute)];
              flag2 = attribute != null;
              flag3 = attribute != null;
            }
            if (attribute == null)
            {
              PropertySerializationMetadata propertyMetadata = this.componentSerializationInfo.SerializationMetadata.FindPropertyMetadata(toRead, property);
              if (propertyMetadata != null)
              {
                attribute = propertyMetadata.Attributes[typeof (DesignerSerializationVisibilityAttribute)];
                attribute?.IsDefaultAttribute();
                if (attribute != null)
                {
                  DesignerSerializationVisibility visibility = ((DesignerSerializationVisibilityAttribute) attribute).Visibility;
                  flag3 = visibility != DesignerSerializationVisibility.Hidden;
                  flag2 = visibility == DesignerSerializationVisibility.Visible;
                }
              }
            }
            if (attribute == null && !this.componentSerializationInfo.DisregardOriginalSerializationVisibility)
            {
              property.Attributes[typeof (DesignerSerializationVisibilityAttribute)]?.IsDefaultAttribute();
              flag3 = property.SerializationVisibility != DesignerSerializationVisibility.Hidden;
              flag2 = property.SerializationVisibility == DesignerSerializationVisibility.Visible;
            }
            bool flag4 = false;
            bool flag5 = false;
            if (flag4)
            {
              try
              {
                if (!flag5)
                {
                  ObjectSerializationInfo serializationInfo = this.ResolveSerializationInfo(property, toRead);
                  if (!serializationInfo.IsSerializedAsString)
                  {
                    if (typeof (IList).IsAssignableFrom(serializationInfo.ObjectType))
                      ((IList) serializationInfo.GetCurrPropertyValue())?.Clear();
                    else if (typeof (IDictionary).IsAssignableFrom(serializationInfo.ObjectType))
                      ((IDictionary) serializationInfo.GetCurrPropertyValue())?.Clear();
                  }
                }
              }
              catch (Exception ex)
              {
              }
            }
          }
        }
        if (!reader.EOF && (reader.NodeType != XmlNodeType.EndElement || reader.Depth != 0))
          return;
        this.disposalBin.DisposeDisposalBin(new DisposeObjectDelegate(this.DisposeObject));
      }
    }

    protected virtual bool ProcessProperty(PropertyDescriptor property)
    {
      return false;
    }

    protected virtual void DisposeObject(IDisposable toBeDisposed)
    {
      toBeDisposed.Dispose();
    }

    protected virtual bool ReadObjectElementOverride(XmlReader reader, object toRead)
    {
      return false;
    }

    private void SkipUnknownXml(XmlReader reader)
    {
      int depth = reader.Depth;
      while (reader.ReadState != ReadState.Error && !reader.EOF && (reader.Depth >= depth || reader.NodeType != XmlNodeType.EndElement))
        reader.Read();
    }

    private void ReadElementProperty(
      XmlReader reader,
      object toRead,
      object propertyOwner,
      PropertyDescriptor currentProperty,
      ObjectSerializationInfo info)
    {
      if (reader.IsEmptyElement)
        return;
      int depth = reader.Depth;
      while (reader.ReadState != ReadState.Error && !reader.EOF && (reader.NodeType != XmlNodeType.EndElement || reader.Depth != depth))
      {
        reader.Read();
        switch (reader.NodeType)
        {
          case XmlNodeType.Element:
            this.ReadElementInObject(reader, currentProperty, toRead);
            continue;
          case XmlNodeType.Text:
            object obj = info.ConvertFromString(reader.Value);
            this.SetPropertyValue(currentProperty, toRead, obj);
            continue;
          default:
            continue;
        }
      }
    }

    private object GetNullValue(Type type)
    {
      if (type.IsValueType)
        return Activator.CreateInstance(type);
      return (object) null;
    }

    private object ReadElementValueAsObject(XmlReader reader, ObjectSerializationInfo info)
    {
      if (reader.IsEmptyElement)
        return this.GetNullValue(info.ObjectType);
      int depth = reader.Depth;
      while (reader.ReadState != ReadState.Error && !reader.EOF && (reader.NodeType != XmlNodeType.EndElement || reader.Depth != depth))
      {
        reader.Read();
        switch (reader.NodeType)
        {
          case XmlNodeType.Text:
            return info.ConvertFromString(reader.Value);
          default:
            continue;
        }
      }
      return (object) null;
    }

    protected virtual void ReadElementInObject(
      XmlReader reader,
      PropertyDescriptor property,
      object toRead)
    {
      ObjectSerializationInfo info;
      try
      {
        info = this.ResolveSerializationInfo(reader, property, toRead);
      }
      catch (Exception ex)
      {
        this.SkipUnknownXml(reader);
        return;
      }
      Type objectType = info.ObjectType;
      if (objectType.IsArray)
      {
        ArrayList arrayList = new ArrayList();
        this.ReadCollectionElement(reader, toRead, (IList) arrayList);
        Array instance = Array.CreateInstance(objectType.GetElementType(), arrayList.Count);
        arrayList.CopyTo(instance);
        this.SetPropertyValue(property, toRead, (object) instance);
      }
      else if (typeof (IList).IsAssignableFrom(objectType))
      {
        if (property.IsReadOnly || property.SerializationVisibility == DesignerSerializationVisibility.Content)
        {
          IList propertyValue = (IList) this.GetPropertyValue(property, toRead);
          if (propertyValue == null || this.ProcessListOverride(reader, toRead, property, propertyValue))
            return;
          this.ReadCollectionElement(reader, toRead, propertyValue);
        }
        else
        {
          IList instance = (IList) this.InstanceFactory.CreateInstance(objectType);
          this.ReadCollectionElement(reader, toRead, instance);
          this.SetPropertyValue(property, toRead, (object) instance);
        }
      }
      else if (typeof (IDictionary).IsAssignableFrom(objectType))
      {
        if (property.IsReadOnly || property.SerializationVisibility == DesignerSerializationVisibility.Content)
        {
          IDictionary propertyValue = (IDictionary) this.GetPropertyValue(property, toRead);
          if (propertyValue == null)
            return;
          this.ReadDictionaryElement(reader, toRead, propertyValue);
        }
        else
        {
          IDictionary instance = (IDictionary) this.InstanceFactory.CreateInstance(objectType);
          this.ReadDictionaryElement(reader, toRead, instance);
          this.SetPropertyValue(property, toRead, (object) instance);
        }
      }
      else if (info.IsSerializedAsString)
      {
        this.ReadElementProperty(reader, toRead, toRead, property, info);
      }
      else
      {
        if ((object) property.PropertyType == (object) typeof (object) && (object) objectType != (object) typeof (object))
        {
          TypeConverter converter = TypeDescriptor.GetConverter(objectType);
          if (converter != null && converter.CanConvertTo(typeof (string)) && converter.CanConvertFrom(typeof (string)))
          {
            string str = reader.ReadElementContentAsString();
            object obj = converter.ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) str);
            this.SetPropertyValue(property, toRead, obj);
            return;
          }
        }
        if (property.IsReadOnly)
        {
          object propertyValue = this.GetPropertyValue(property, toRead);
          if (propertyValue == null)
            return;
          this.ReadObjectElement(reader, toRead, propertyValue);
        }
        else
        {
          ConstructorInfo constructor = objectType.GetConstructor(Type.EmptyTypes);
          if ((object) constructor == null || !constructor.IsPublic)
            return;
          object instance;
          try
          {
            instance = this.InstanceFactory.CreateInstance(objectType);
          }
          catch (TargetInvocationException ex)
          {
            return;
          }
          catch (MissingMethodException ex)
          {
            return;
          }
          this.ReadObjectElement(reader, toRead, instance);
          this.SetPropertyValue(property, toRead, instance);
        }
      }
    }

    protected internal virtual void SetPropertyValue(
      PropertyDescriptor property,
      object propertyOwner,
      object value)
    {
      object association = TypeDescriptor.GetAssociation(property.ComponentType, propertyOwner);
      property.SetValue(association, value);
    }

    internal object GetPropertyValue(PropertyDescriptor property, object propertyOwner)
    {
      object obj = TypeDescriptor.GetAssociation(property.ComponentType, propertyOwner);
      if (!property.ComponentType.IsInstanceOfType(obj))
      {
        ICustomTypeDescriptor customTypeDescriptor = propertyOwner as ICustomTypeDescriptor;
        if (customTypeDescriptor != null)
        {
          obj = customTypeDescriptor.GetPropertyOwner(property);
          if (!property.ComponentType.IsInstanceOfType(obj))
            return (object) null;
        }
        else
        {
          try
          {
            property.GetValue(obj);
          }
          catch (Exception ex)
          {
            return (object) null;
          }
        }
      }
      return property.GetValue(obj);
    }

    protected virtual bool ProcessListOverride(
      XmlReader reader,
      object listOwner,
      PropertyDescriptor ownerProperty,
      IList list)
    {
      return false;
    }

    private void WriteTypeAttribute(
      XmlWriter writer,
      Type expectedType,
      object toWrite,
      DesignerSerializationVisibility serializationVisibility)
    {
      if (toWrite == null || serializationVisibility != DesignerSerializationVisibility.Visible)
        return;
      Type type = toWrite.GetType();
      if ((object) type == (object) expectedType)
        return;
      writer.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", this.GetElementNameByType(type));
    }

    public virtual void WriteObjectElement(XmlWriter writer, object toWrite)
    {
      if (toWrite == null)
        return;
      if (this.RootSerializationObject == null)
      {
        this.rootSerializationObject = toWrite;
        this.InitializeWrite();
      }
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(toWrite);
      ArrayList arrayList = new ArrayList();
      foreach (PropertyDescriptor propertyDescriptor in properties)
      {
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        Attribute attribute1 = (Attribute) null;
        if (attribute1 == null)
        {
          attribute1 = propertyDescriptor.Attributes[typeof (XmlAttributeAttribute)];
          flag2 = attribute1 != null;
          flag1 = attribute1 != null;
        }
        PropertySerializationMetadata overwriteMetadata = (PropertySerializationMetadata) null;
        if (attribute1 == null)
        {
          overwriteMetadata = this.componentSerializationInfo.SerializationMetadata.FindPropertyMetadata(toWrite, propertyDescriptor);
          if (overwriteMetadata != null)
          {
            attribute1 = overwriteMetadata.Attributes[typeof (DesignerSerializationVisibilityAttribute)];
            flag3 = attribute1 == null || attribute1.IsDefaultAttribute();
            if (attribute1 != null)
            {
              DesignerSerializationVisibility visibility = ((DesignerSerializationVisibilityAttribute) attribute1).Visibility;
              flag1 = visibility != DesignerSerializationVisibility.Hidden;
              flag2 = visibility == DesignerSerializationVisibility.Visible;
            }
          }
        }
        if (attribute1 == null && !this.componentSerializationInfo.DisregardOriginalSerializationVisibility)
        {
          Attribute attribute2 = propertyDescriptor.Attributes[typeof (DesignerSerializationVisibilityAttribute)];
          flag3 = attribute2 == null || attribute2.IsDefaultAttribute();
          flag1 = propertyDescriptor.SerializationVisibility != DesignerSerializationVisibility.Hidden;
          flag2 = propertyDescriptor.SerializationVisibility == DesignerSerializationVisibility.Visible;
        }
        if (flag1)
        {
          if (this.ShouldSerializeValue(toWrite, propertyDescriptor, overwriteMetadata))
          {
            try
            {
              if (flag2 && !flag3)
              {
                writer.WriteAttributeString(propertyDescriptor.Name, XmlPropertySetting.SerializeValue(propertyDescriptor, this.GetPropertyValue(propertyDescriptor, toWrite), propertyDescriptor.DisplayName));
              }
              else
              {
                ObjectSerializationInfo serializationInfo = this.ResolveSerializationInfo(propertyDescriptor, toWrite);
                if (serializationInfo.IsSerializedAsString)
                  writer.WriteAttributeString(propertyDescriptor.Name, serializationInfo.ConvertToString());
                else
                  arrayList.Add((object) propertyDescriptor);
              }
            }
            catch (Exception ex)
            {
            }
          }
        }
      }
      foreach (PropertyDescriptor property in arrayList)
      {
        DesignerSerializationVisibility serializationVisibility = property.SerializationVisibility;
        PropertySerializationMetadata propertyMetadata = this.componentSerializationInfo.SerializationMetadata.FindPropertyMetadata(toWrite, property);
        if (propertyMetadata != null)
        {
          Attribute attribute = propertyMetadata.Attributes[typeof (DesignerSerializationVisibilityAttribute)];
          if (attribute != null)
            serializationVisibility = ((DesignerSerializationVisibilityAttribute) attribute).Visibility;
        }
        if ((serializationVisibility != DesignerSerializationVisibility.Content || property.IsReadOnly) && (serializationVisibility != DesignerSerializationVisibility.Visible || !property.IsReadOnly))
        {
          ObjectSerializationInfo serializationInfo = this.ResolveSerializationInfo(property, toWrite);
          Type objectType = serializationInfo.ObjectType;
          object currPropertyValue = serializationInfo.GetCurrPropertyValue();
          if ((object) property.PropertyType == (object) typeof (object) && currPropertyValue != null)
          {
            TypeConverter converter = TypeDescriptor.GetConverter(currPropertyValue.GetType());
            if (converter != null && converter.CanConvertTo(typeof (string)) && converter.CanConvertFrom(typeof (string)))
            {
              writer.WriteStartElement(property.Name);
              this.WriteTypeAttribute(writer, property.PropertyType, currPropertyValue, serializationVisibility);
              writer.WriteValue(converter.ConvertToString((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, currPropertyValue));
              writer.WriteEndElement();
              continue;
            }
          }
          if (currPropertyValue != null)
          {
            using (StringWriter stringWriter = new StringWriter())
            {
              using (XmlWriter writer1 = (XmlWriter) new XmlTextWriter((TextWriter) stringWriter))
              {
                writer1.WriteStartElement(property.Name);
                try
                {
                  this.WriteTypeAttribute(writer1, property.PropertyType, currPropertyValue, serializationVisibility);
                  if (objectType.IsArray)
                    this.WriteCollectionElement(writer1, (IEnumerable) currPropertyValue, toWrite, property);
                  else if (typeof (IList).IsAssignableFrom(objectType))
                    this.WriteCollectionElement(writer1, (IEnumerable) currPropertyValue, toWrite, property);
                  else if (typeof (IDictionary).IsAssignableFrom(objectType))
                    this.WriteDictionaryElement(writer1, (IDictionary) currPropertyValue, toWrite, property);
                  else
                    this.WriteObjectElement(writer1, currPropertyValue);
                }
                catch (Exception ex)
                {
                  continue;
                }
                if (writer1.WriteState != WriteState.Error)
                {
                  writer1.WriteEndElement();
                  string s = stringWriter.ToString();
                  if (s.Length > property.Name.Length + 4)
                  {
                    using (StringReader stringReader = new StringReader(s))
                    {
                      using (XmlTextReader xmlTextReader = new XmlTextReader((TextReader) stringReader))
                        writer.WriteNode((XmlReader) xmlTextReader, true);
                    }
                  }
                }
              }
            }
          }
        }
      }
      if (toWrite is IList)
      {
        this.WriteCollectionElement(writer, (IEnumerable) toWrite, (object) null, (PropertyDescriptor) null);
      }
      else
      {
        if (!(toWrite is IDictionary))
          return;
        this.WriteDictionaryElement(writer, (IDictionary) toWrite, (object) null, (PropertyDescriptor) null);
      }
    }

    protected virtual bool ShouldSerializeValue(
      object component,
      PropertyDescriptor property,
      PropertySerializationMetadata overwriteMetadata)
    {
      object association = TypeDescriptor.GetAssociation(property.ComponentType, component);
      if (!property.ComponentType.IsInstanceOfType(association))
      {
        ICustomTypeDescriptor customTypeDescriptor = component as ICustomTypeDescriptor;
        if (customTypeDescriptor != null)
        {
          object propertyOwner = customTypeDescriptor.GetPropertyOwner(property);
          if (!property.ComponentType.IsInstanceOfType(propertyOwner))
            return false;
        }
        return false;
      }
      if (property.Attributes.Contains((Attribute) DesignOnlyAttribute.Yes))
        return false;
      if (overwriteMetadata != null)
      {
        if (property.IsReadOnly)
          return overwriteMetadata.Attributes.Contains((Attribute) DesignerSerializationVisibilityAttribute.Content);
        if (overwriteMetadata.ShouldSerializeProperty.HasValue)
          return overwriteMetadata.ShouldSerializeProperty.Value;
        if (overwriteMetadata.Attributes.Contains((Attribute) DesignerSerializationVisibilityAttribute.Visible))
          return true;
        if (overwriteMetadata.Attributes.Contains((Attribute) DesignerSerializationVisibilityAttribute.Hidden))
          return false;
      }
      if (!this.componentSerializationInfo.SerializeDefaultValues)
        return property.ShouldSerializeValue(TypeDescriptor.GetAssociation(property.ComponentType, component));
      if (property.IsReadOnly)
        return property.Attributes.Contains((Attribute) DesignerSerializationVisibilityAttribute.Content);
      return true;
    }

    protected void WriteDictionaryElement(
      XmlWriter writer,
      IDictionary toWrite,
      object listOwner,
      PropertyDescriptor property)
    {
      this.WriteCollectionElement(writer, (IEnumerable) new DictionarySerializationListWrapper(toWrite), listOwner, property);
    }

    protected virtual void InitializeWrite()
    {
    }

    protected virtual void InitializeRead()
    {
    }

    protected virtual IEnumerable GetCollectionElementOverride(
      IEnumerable list,
      object owner,
      PropertyDescriptor property)
    {
      return list;
    }

    public void WriteCollectionElement(XmlWriter writer, IEnumerable list, string collectionName)
    {
      writer.WriteStartElement(collectionName);
      this.WriteCollectionElement(writer, list, (object) null, (PropertyDescriptor) null);
      writer.WriteEndElement();
    }

    protected virtual void WriteCollectionElement(
      XmlWriter writer,
      IEnumerable list,
      object listOwner,
      PropertyDescriptor property)
    {
      if (this.RootSerializationObject == null)
      {
        this.rootSerializationObject = (object) list;
        this.InitializeWrite();
      }
      foreach (object obj in this.GetCollectionElementOverride(list, listOwner, property))
      {
        string elementNameByType = this.GetElementNameByType(obj.GetType());
        ObjectSerializationInfo serializationInfo = this.ResolveSerializationInfo(obj);
        if (serializationInfo.IsSerializedAsString)
        {
          writer.WriteElementString(elementNameByType, serializationInfo.ConvertToString());
        }
        else
        {
          writer.WriteStartElement(elementNameByType);
          this.WriteObjectElement(writer, obj);
          writer.WriteEndElement();
        }
      }
    }

    protected virtual string GetElementNameByType(Type elementType)
    {
      if (this.reversedTypeMap != null)
      {
        string reversedType = (string) this.reversedTypeMap[(object) elementType];
        if (!string.IsNullOrEmpty(reversedType))
          return reversedType;
      }
      string str = elementType.FullName;
      if (elementType.Namespace == "Telerik.WinControls")
        str = elementType.Name;
      return str;
    }
  }
}
