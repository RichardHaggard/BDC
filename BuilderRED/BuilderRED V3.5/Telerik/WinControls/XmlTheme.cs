// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlTheme
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Telerik.WinControls.Styles;
using Telerik.WinControls.Themes.Serialization;

namespace Telerik.WinControls
{
  [XmlInclude(typeof (XmlStyleBuilderRegistration))]
  public class XmlTheme : IXmlSerializable
  {
    private string themeVersion = "1.0";
    private string themeName;
    private XmlStyleBuilderRegistration[] builderRegistrations;
    private XmlStyleRepository repository;

    public XmlTheme()
    {
    }

    public XmlTheme(XmlStyleSheet style, string controlType, string elementType)
    {
      this.themeName = "";
      this.builderRegistrations = new XmlStyleBuilderRegistration[1]
      {
        new XmlStyleBuilderRegistration(style, controlType, elementType)
      };
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public XmlStyleRepository StyleRepository
    {
      get
      {
        if (this.repository == null)
          this.repository = new XmlStyleRepository();
        return this.repository;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool HasRepository
    {
      get
      {
        if (this.repository != null)
          return this.repository.RepositoryItems.Count > 0;
        return false;
      }
    }

    [XmlAttribute]
    [DefaultValue(null)]
    public string ThemeName
    {
      get
      {
        return this.themeName;
      }
      set
      {
        this.themeName = value;
      }
    }

    [XmlAttribute]
    [DefaultValue("1.0")]
    public string ThemeVersion
    {
      get
      {
        return this.themeVersion;
      }
      set
      {
        this.themeVersion = value;
      }
    }

    [DefaultValue(null)]
    public XmlStyleBuilderRegistration[] BuilderRegistrations
    {
      get
      {
        return this.builderRegistrations;
      }
      set
      {
        this.builderRegistrations = value;
      }
    }

    public static XmlTheme LoadFromStram(Stream stream)
    {
      return (XmlTheme) new XmlSerializer(typeof (XmlTheme)).Deserialize(stream);
    }

    public static XmlTheme LoadFromReader(TextReader reader)
    {
      return (XmlTheme) new XmlSerializer(typeof (XmlTheme)).Deserialize(reader);
    }

    public static XmlTheme LoadFromReader(XmlReader reader)
    {
      return (XmlTheme) new XmlSerializer(typeof (XmlTheme)).Deserialize(reader);
    }

    public void SaveToWriter(XmlWriter writer)
    {
      new XmlSerializer(typeof (XmlTheme)).Serialize(writer, (object) this);
    }

    public void SaveToStream(Stream stream)
    {
      this.SaveToWriter((XmlWriter) new XmlTextWriter(stream, Encoding.UTF8)
      {
        Formatting = Formatting.Indented
      });
    }

    public void DeserializePartially(XmlReader reader)
    {
      if (reader.ReadToFollowing(nameof (XmlTheme)))
      {
        new StyleXmlSerializer(true).ReadObjectElement(reader, (object) this);
        reader.ReadEndElement();
      }
      else
      {
        int num = (int) MessageBox.Show("Error reading theme: element XmlTheme not found!");
      }
      if (this.builderRegistrations == null)
        return;
      foreach (XmlStyleBuilderRegistration builderRegistration in this.BuilderRegistrations)
        (builderRegistration.BuilderData as XmlStyleSheet)?.SetThemeName(this.ThemeName);
    }

    XmlSchema IXmlSerializable.GetSchema()
    {
      return (XmlSchema) null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
      new StyleXmlSerializer().ReadObjectElement(reader, (object) this);
      reader.ReadEndElement();
    }

    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
      if (string.IsNullOrEmpty(writer.LookupPrefix("http://www.w3.org/2001/XMLSchema-instance")))
        writer.WriteAttributeString("xmlns", "xsi", (string) null, "http://www.w3.org/2001/XMLSchema-instance");
      new StyleXmlSerializer().WriteObjectElement(writer, (object) this);
    }

    public static string SerializeType(System.Type value)
    {
      return value.FullName;
    }

    public static System.Type DeserializeType(string className)
    {
      System.Type type = System.Type.GetType(className);
      if ((object) type == null)
      {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
          type = assembly.GetType(className);
          if ((object) type != null)
            break;
        }
        if ((object) type == null)
          throw new ArgumentException("Type not found during theme deserialization: " + className + ". Please make sure all required asseblies are referenced.");
      }
      return type;
    }

    public Theme Deserialize()
    {
      Theme theme = new Theme(this.ThemeName);
      if (this.HasRepository)
      {
        foreach (XmlRepositoryItem repositoryItem in this.repository.RepositoryItems)
        {
          Telerik.WinControls.StyleRepository styleRepository = new Telerik.WinControls.StyleRepository(repositoryItem.ItemType, repositoryItem.DisplayName, repositoryItem.Key);
          foreach (XmlPropertySetting xmlPropertySetting in (List<XmlPropertySetting>) repositoryItem)
            styleRepository.Settings.Add(new PropertySetting(xmlPropertySetting.Property, xmlPropertySetting.Value));
        }
      }
      return theme;
    }
  }
}
