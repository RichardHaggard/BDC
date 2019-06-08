// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlNameSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class XmlNameSelector : XmlSelectorBase
  {
    private string elementName;

    public XmlNameSelector()
    {
    }

    public XmlNameSelector(string elementName)
    {
      this.elementName = elementName;
    }

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

    public override string ToString()
    {
      return "NameSelector";
    }

    public override bool Equals(object obj)
    {
      return obj is XmlNameSelector && (obj as XmlNameSelector).elementName == this.elementName;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    protected override IElementSelector CreateInstance()
    {
      return (IElementSelector) new NameSelector(this.ElementName);
    }
  }
}
