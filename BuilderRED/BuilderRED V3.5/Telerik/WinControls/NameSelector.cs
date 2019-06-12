// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.NameSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  [Serializable]
  public class NameSelector : HierarchicalSelector
  {
    private string elementName;

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

    protected internal override bool CanUseCache
    {
      get
      {
        return false;
      }
    }

    protected override int GetKey()
    {
      if (string.IsNullOrEmpty(this.elementName))
        return 0;
      return this.elementName.GetHashCode();
    }

    public static int GetSelectorKey(string name)
    {
      return name.GetHashCode();
    }

    public NameSelector()
    {
    }

    public NameSelector(string elementName)
    {
      this.elementName = elementName;
    }

    protected override bool CanSelectOverride(RadObject element)
    {
      RadElement radElement = element as RadElement;
      if (radElement != null && element != null)
        return string.CompareOrdinal(radElement.Name, this.elementName) == 0;
      return false;
    }

    public override bool Equals(IElementSelector elementSelector)
    {
      NameSelector nameSelector = elementSelector as NameSelector;
      if (nameSelector != null)
        return nameSelector.elementName == this.elementName;
      return false;
    }

    public override string ToString()
    {
      if (this.elementName == null)
        return "Name == NotSpecified";
      return "Name == " + this.elementName;
    }
  }
}
