// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlTypeSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Xml.Serialization;

namespace Telerik.WinControls
{
  public class XmlTypeSelector : XmlSelectorBase
  {
    private string elementType;

    public XmlTypeSelector()
    {
    }

    public XmlTypeSelector(string elementType)
    {
      this.elementType = elementType;
    }

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

    public override string ToString()
    {
      return "TypeSelector";
    }

    public override bool Equals(object obj)
    {
      return obj is XmlTypeSelector && (obj as XmlTypeSelector).elementType == this.elementType;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    protected override IElementSelector CreateInstance()
    {
      return (IElementSelector) new TypeSelector(XmlTheme.DeserializeType(this.ElementType));
    }
  }
}
