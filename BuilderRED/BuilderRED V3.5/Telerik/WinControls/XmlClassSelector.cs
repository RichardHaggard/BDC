// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlClassSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class XmlClassSelector : XmlSelectorBase
  {
    private string elementClass;

    public XmlClassSelector()
    {
    }

    public XmlClassSelector(string elementClass)
    {
      this.elementClass = elementClass;
    }

    public string ElementClass
    {
      get
      {
        return this.elementClass;
      }
      set
      {
        this.elementClass = value;
      }
    }

    public override string ToString()
    {
      return "ClassSelector";
    }

    public override bool Equals(object obj)
    {
      return obj is XmlClassSelector && (obj as XmlClassSelector).elementClass == this.elementClass;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    protected override IElementSelector CreateInstance()
    {
      return (IElementSelector) new ClassSelector(this.ElementClass);
    }
  }
}
