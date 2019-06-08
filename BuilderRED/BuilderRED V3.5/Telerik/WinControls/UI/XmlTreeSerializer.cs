// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.XmlTreeSerializer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Xml.Serialization;

namespace Telerik.WinControls.UI
{
  internal class XmlTreeSerializer : XmlSerializer
  {
    public XmlTreeSerializer(Type type, Type[] extraType)
      : base(type, extraType)
    {
      this.UnknownAttribute += new XmlAttributeEventHandler(this.XmlTreeSerializer_UnknownAttribute);
    }

    public XmlTreeSerializer(Type type)
      : base(type)
    {
      this.UnknownAttribute += new XmlAttributeEventHandler(this.XmlTreeSerializer_UnknownAttribute);
    }

    private void XmlTreeSerializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
    {
      (e.ObjectBeingDeserialized as IXmlTreeSerializable)?.ReadUnknownAttribute(e.Attr);
    }
  }
}
