// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.PropertySerializationMetadataCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.XmlSerialization
{
  public class PropertySerializationMetadataCollection : ObservableCollection<PropertySerializationMetadata>
  {
    public void Add(string objectTypeFullName, string propertyName, params Attribute[] attributes)
    {
      this.Add(new PropertySerializationMetadata(objectTypeFullName, propertyName, attributes));
    }

    public void Add(Type objectType, string propertyName, params Attribute[] attributes)
    {
      this.Add(new PropertySerializationMetadata(objectType, propertyName, attributes));
    }

    protected override bool OnCollectionChanging(NotifyCollectionChangingEventArgs e)
    {
      if ((e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.ItemChanged || e.Action == NotifyCollectionChangedAction.Replace) && e.NewItems.Count == 1)
      {
        int index = 0;
        PropertySerializationMetadata metadata = (PropertySerializationMetadata) e.NewItems[index];
        PropertySerializationMetadata serializationMetadata = this.Find((Predicate<PropertySerializationMetadata>) (toLookUp =>
        {
          if (toLookUp.TypeFullName == metadata.TypeFullName)
            return toLookUp.PropertyName == metadata.PropertyName;
          return false;
        }));
        if (serializationMetadata != null)
          serializationMetadata.Attributes = metadata.Attributes;
      }
      return base.OnCollectionChanging(e);
    }

    public PropertySerializationMetadata FindClassMetadata(
      object targetObject)
    {
      return this.FindPropertyMetadata(targetObject, (PropertyDescriptor) null);
    }

    public PropertySerializationMetadata FindPropertyMetadata(
      object targetObject,
      PropertyDescriptor property)
    {
      string name = property.Name;
      return this.Find((Predicate<PropertySerializationMetadata>) (metadata =>
      {
        if ((object) metadata.Type != null && metadata.Type.IsInstanceOfType(targetObject))
          return metadata.PropertyName == name;
        return false;
      })) ?? this.Find((Predicate<PropertySerializationMetadata>) (metadata =>
      {
        if (metadata.TypeFullName == targetObject.GetType().FullName || metadata.TypeFullName == property.ComponentType.FullName)
          return metadata.PropertyName == name;
        return false;
      }));
    }

    public PropertySerializationMetadata Find(
      Predicate<PropertySerializationMetadata> match)
    {
      if (match == null)
      {
        ArgumentException argumentException = new ArgumentException("Parameter 'match' cannot be null");
      }
      for (int index = 0; index < this.Count; ++index)
      {
        if (match(this[index]))
          return this[index];
      }
      return (PropertySerializationMetadata) null;
    }
  }
}
