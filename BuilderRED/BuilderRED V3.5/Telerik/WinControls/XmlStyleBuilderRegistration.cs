// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlStyleBuilderRegistration
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class XmlStyleBuilderRegistration
  {
    private RadStyleSheetRelationList stylesheetRelations;
    private Type builderType;
    private XmlBuilderData builderData;

    public XmlStyleBuilderRegistration()
    {
    }

    public XmlStyleBuilderRegistration(XmlStyleSheet style, string controlType, string elementType)
    {
      this.builderData = (XmlBuilderData) style;
      this.stylesheetRelations = new RadStyleSheetRelationList(1);
      RadStylesheetRelation stylesheetRelation = new RadStylesheetRelation();
      this.StylesheetRelations.Add(stylesheetRelation);
      stylesheetRelation.RegistrationType = BuilderRegistrationType.ElementTypeControlType;
      stylesheetRelation.ElementType = elementType ?? string.Empty;
      stylesheetRelation.ControlType = controlType ?? string.Empty;
    }

    public XmlStyleBuilderRegistration(StyleGroup toCopyFrom)
    {
      foreach (StyleRegistration registration in toCopyFrom.Registrations)
        this.StylesheetRelations.Add(new RadStylesheetRelation()
        {
          RegistrationType = (BuilderRegistrationType) Enum.Parse(typeof (BuilderRegistrationType), registration.RegistrationType),
          ControlName = registration.ControlName,
          ControlType = registration.ControlType,
          ElementName = registration.ElementName,
          ElementType = registration.ElementType
        });
    }

    [XmlAttribute]
    public Type BuilderType
    {
      get
      {
        return this.builderType;
      }
      set
      {
        this.builderType = value;
      }
    }

    [TypeConverter(typeof (ExpandableObjectConverter))]
    public XmlBuilderData BuilderData
    {
      get
      {
        return this.builderData;
      }
      set
      {
        this.builderData = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadStyleSheetRelationList StylesheetRelations
    {
      get
      {
        if (this.stylesheetRelations == null)
          this.stylesheetRelations = new RadStyleSheetRelationList(1);
        return this.stylesheetRelations;
      }
    }

    public StyleGroup GetRegistration()
    {
      StyleGroup styleGroup = new StyleGroup();
      foreach (RadStylesheetRelation stylesheetRelation in (List<RadStylesheetRelation>) this.StylesheetRelations)
      {
        string elementType = stylesheetRelation.ElementType;
        if (stylesheetRelation.RegistrationType == BuilderRegistrationType.ElementTypeControlType && stylesheetRelation.ElementType == null)
          elementType = typeof (RootRadElement).FullName;
        styleGroup.Registrations.Add(new StyleRegistration(stylesheetRelation.RegistrationType.ToString(), elementType, stylesheetRelation.ControlType, stylesheetRelation.ElementName, stylesheetRelation.ControlName));
      }
      return styleGroup;
    }
  }
}
