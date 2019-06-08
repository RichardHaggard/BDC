// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Serialization.RadXmlThemeArchiveStream
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.IO;
using System.Xml;

namespace Telerik.WinControls.Serialization
{
  [Serializable]
  public class RadXmlThemeArchiveStream : RadArchiveStream
  {
    public RadXmlThemeArchiveStream()
    {
    }

    public RadXmlThemeArchiveStream(XmlTheme theme)
      : base((object) theme)
    {
    }

    public override StreamFormat Format
    {
      get
      {
        return StreamFormat.XML;
      }
    }

    protected override byte[] GetRawBytes()
    {
      XmlTheme context = this.Context as XmlTheme;
      MemoryStream memoryStream = new MemoryStream();
      context.SaveToStream((Stream) memoryStream);
      byte[] array = memoryStream.ToArray();
      memoryStream.Close();
      return array;
    }

    protected override object Deserialize(byte[] rawBytes)
    {
      XmlTheme xmlTheme = new XmlTheme();
      MemoryStream memoryStream = new MemoryStream(rawBytes);
      XmlReader reader = XmlReader.Create((Stream) memoryStream);
      xmlTheme.DeserializePartially(reader);
      reader.Close();
      memoryStream.Close();
      return (object) xmlTheme;
    }
  }
}
