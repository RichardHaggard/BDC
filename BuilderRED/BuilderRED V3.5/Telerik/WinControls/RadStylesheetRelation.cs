// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadStylesheetRelation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Xml.Serialization;

namespace Telerik.WinControls
{
  public class RadStylesheetRelation
  {
    private BuilderRegistrationType registrationType = BuilderRegistrationType.ElementTypeControlType;
    private string controlType = string.Empty;
    private string elementType = string.Empty;
    private string controlName = string.Empty;
    private string elementName = string.Empty;

    public RadStylesheetRelation()
    {
    }

    public RadStylesheetRelation(
      BuilderRegistrationType type,
      string elementType,
      string controlType,
      string elementName,
      string controlName)
    {
      this.registrationType = type;
      this.elementType = elementType ?? string.Empty;
      this.controlType = controlType ?? string.Empty;
      this.elementName = elementName ?? string.Empty;
      this.controlName = controlName ?? string.Empty;
    }

    public bool Equals(RadStylesheetRelation relation)
    {
      if (this.registrationType == relation.registrationType && string.CompareOrdinal(this.elementType, relation.elementType) == 0 && (string.CompareOrdinal(this.controlType, relation.controlType) == 0 && string.CompareOrdinal(this.elementName, relation.elementName) == 0))
        return string.CompareOrdinal(this.controlName, relation.controlName) == 0;
      return false;
    }

    [XmlAttribute]
    [DefaultValue(BuilderRegistrationType.ElementTypeControlType)]
    public BuilderRegistrationType RegistrationType
    {
      get
      {
        return this.registrationType;
      }
      set
      {
        this.registrationType = value;
      }
    }

    [DefaultValue("")]
    [XmlAttribute]
    public string ControlType
    {
      get
      {
        return this.controlType;
      }
      set
      {
        this.controlType = value;
      }
    }

    [DefaultValue("")]
    [XmlAttribute]
    public string ElementType
    {
      get
      {
        return this.elementType;
      }
      set
      {
        this.elementType = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("")]
    public string ControlName
    {
      get
      {
        return this.controlName;
      }
      set
      {
        this.controlName = value;
      }
    }

    [DefaultValue("")]
    [XmlAttribute]
    public string ElementName
    {
      get
      {
        return this.elementName;
      }
      set
      {
        this.elementName = value;
      }
    }
  }
}
