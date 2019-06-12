// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSerialization.ControlXmlSerializationInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.XmlSerialization
{
  public class ControlXmlSerializationInfo : ComponentXmlSerializationInfo
  {
    public ControlXmlSerializationInfo()
      : base(new PropertySerializationMetadataCollection())
    {
      this.SerializationMetadata.Add(typeof (Control), "Tag", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      this.SerializationMetadata.Add(typeof (RadControl), "RootElement", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
      this.SerializationMetadata.Add(typeof (Control), "DataBindings", (Attribute) new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden));
    }
  }
}
