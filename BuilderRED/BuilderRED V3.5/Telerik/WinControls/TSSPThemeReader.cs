// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TSSPThemeReader
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Xml;

namespace Telerik.WinControls
{
  public class TSSPThemeReader : IThemeReader
  {
    private bool storeFullPropertyName;

    public TSSPThemeReader()
    {
    }

    public TSSPThemeReader(bool storeFullPropertyName)
    {
      this.storeFullPropertyName = storeFullPropertyName;
    }

    public bool StoreFullPropertyName
    {
      get
      {
        return this.storeFullPropertyName;
      }
      set
      {
        this.storeFullPropertyName = value;
      }
    }

    public Theme Read(string filePath)
    {
      using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        return this.Read((Stream) fileStream);
    }

    public Theme Read(Stream stream)
    {
      Theme theme = new Theme();
      XMLThemeReader xmlThemeReader = new XMLThemeReader(this.storeFullPropertyName);
      using (XmlTextReader xmlTextReader = new XmlTextReader(stream))
      {
        xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
        if (xmlTextReader.Read() && xmlTextReader.NodeType != XmlNodeType.XmlDeclaration || xmlTextReader.Read() && (xmlTextReader.NodeType != XmlNodeType.Element || xmlTextReader.Depth != 0 || xmlTextReader.Name != "RadThemePackage"))
          return theme;
        do
          ;
        while (xmlTextReader.Read() && (xmlTextReader.NodeType != XmlNodeType.Element || xmlTextReader.Depth != 1 || !(xmlTextReader.Name == "Streams")));
        while (xmlTextReader.Read())
        {
          if (xmlTextReader.Depth > 1)
          {
            if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == "RadArchiveStream")
            {
              xmlTextReader.Read();
              if (xmlTextReader.Name == "ZippedBytes")
              {
                int content = (int) xmlTextReader.MoveToContent();
                xmlTextReader.Read();
                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(xmlTextReader.Value)))
                {
                  using (DeflateStream deflateStream = new DeflateStream((Stream) memoryStream, CompressionMode.Decompress, true))
                  {
                    xmlThemeReader.Read(theme, new XmlTextReader((Stream) deflateStream));
                    memoryStream.Close();
                  }
                }
              }
            }
          }
          else
            break;
        }
      }
      return theme;
    }

    public Theme ReadResource(string resourcePath)
    {
      using (Stream manifestResourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourcePath))
        return this.Read(manifestResourceStream);
    }
  }
}
