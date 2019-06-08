// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.ComponentXmlSerializationInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.XmlSerialization
{
  public class ComponentXmlSerializationInfo
  {
    private PropertySerializationMetadataCollection serializationMetadata;
    private bool disregardOriginalSerializationVisibility;
    private bool serializeDefaultValues;

    public ComponentXmlSerializationInfo(
      PropertySerializationMetadataCollection serializationMetadata)
    {
      this.serializationMetadata = serializationMetadata;
    }

    public PropertySerializationMetadataCollection SerializationMetadata
    {
      get
      {
        if (this.serializationMetadata == null)
          this.serializationMetadata = new PropertySerializationMetadataCollection();
        return this.serializationMetadata;
      }
    }

    public bool DisregardOriginalSerializationVisibility
    {
      get
      {
        return this.disregardOriginalSerializationVisibility;
      }
      set
      {
        this.disregardOriginalSerializationVisibility = value;
      }
    }

    public bool SerializeDefaultValues
    {
      get
      {
        return this.serializeDefaultValues;
      }
      set
      {
        this.serializeDefaultValues = value;
      }
    }
  }
}
