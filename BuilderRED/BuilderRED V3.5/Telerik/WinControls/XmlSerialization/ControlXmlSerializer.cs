// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.ControlXmlSerializer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace Telerik.WinControls.XmlSerialization
{
  public class ControlXmlSerializer : ComponentXmlSerializer
  {
    public ControlXmlSerializer(
      ComponentXmlSerializationInfo componentSerializationInfo)
      : base(componentSerializationInfo)
    {
    }

    public ControlXmlSerializer()
      : base((ComponentXmlSerializationInfo) new ControlXmlSerializationInfo())
    {
    }

    protected override bool ProcessListOverride(
      XmlReader reader,
      object listOwner,
      PropertyDescriptor parentProperty,
      IList list)
    {
      if (!(list is Control.ControlCollection))
        return base.ProcessListOverride(reader, listOwner, parentProperty, list);
      this.ReadMergeCollection(reader, listOwner, parentProperty, list, "Name");
      return true;
    }
  }
}
