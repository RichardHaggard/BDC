// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ElementSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class ElementSelector
  {
    private ElementSelector childSelector;
    private ElementSelectorTypes type;
    private string value;
    internal bool IsRecursive;

    public ElementSelector()
    {
    }

    public ElementSelector(string elementState)
    {
      this.value = elementState;
      this.type = ElementSelectorTypes.VisualStateSelector;
    }

    public ElementSelector(ElementSelectorTypes type, string value)
    {
      this.type = type;
      this.value = value;
    }

    public ElementSelector(ElementSelector selector)
    {
      this.type = selector.type;
      this.value = selector.value;
      if (selector.ChildSelector == null)
        return;
      this.childSelector = new ElementSelector(selector.ChildSelector);
    }

    public string Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    public ElementSelectorTypes Type
    {
      get
      {
        return this.type;
      }
      set
      {
        this.type = value;
      }
    }

    public ElementSelector ChildSelector
    {
      get
      {
        return this.childSelector;
      }
      set
      {
        this.childSelector = value;
      }
    }

    public virtual bool IsCompatible(RadObject element)
    {
      IStylableNode stylableNode = element as IStylableNode;
      if (stylableNode == null)
        return false;
      if (this.type == ElementSelectorTypes.VisualStateSelector)
      {
        IStylableElement stylableElement = element as IStylableElement;
        string str = stylableElement != null ? stylableElement.ThemeRole : element.GetType().Name;
        if (this.value.StartsWith(str))
          return true;
        return str == this.value;
      }
      if (this.type == ElementSelectorTypes.ClassSelector)
      {
        if (this.childSelector == null || this.childSelector.Type != ElementSelectorTypes.VisualStateSelector)
          return stylableNode.Class == this.value;
        if (this.childSelector.IsCompatible(element))
        {
          for (IStylableNode parent = stylableNode.Parent; parent != null; parent = parent.Parent)
          {
            if (parent.Class == this.value)
              return true;
          }
        }
        return false;
      }
      if (this.type != ElementSelectorTypes.TypeSelector)
        return false;
      if (this.childSelector == null || this.childSelector.Type != ElementSelectorTypes.VisualStateSelector || !this.childSelector.IsCompatible(element))
        return stylableNode.GetThemeEffectiveType().FullName == this.value;
      for (IStylableNode parent = stylableNode.Parent; parent != null; parent = parent.Parent)
      {
        if (parent.GetThemeEffectiveType().FullName == this.value)
          return true;
      }
      return false;
    }

    public virtual bool IsValid(RadObject testElement, string state)
    {
      IStylableNode stylableNode = testElement as IStylableNode;
      if (this.type == ElementSelectorTypes.VisualStateSelector)
        return string.CompareOrdinal(state, this.Value) == 0;
      if (this.childSelector != null && this.childSelector.Type == ElementSelectorTypes.VisualStateSelector)
        return this.childSelector.IsValid(testElement, state);
      if (this.type == ElementSelectorTypes.ClassSelector)
        return string.CompareOrdinal(this.value, stylableNode.Class) == 0;
      if (this.type == ElementSelectorTypes.TypeSelector)
        return string.CompareOrdinal(this.value, stylableNode.GetThemeEffectiveType().FullName) == 0;
      return false;
    }

    public bool IsCompatible(ElementSelector selector)
    {
      if (this.value == selector.value)
        return this.type == selector.Type;
      return false;
    }

    public override bool Equals(object obj)
    {
      ElementSelector elementSelector = obj as ElementSelector;
      if (elementSelector == null)
        return base.Equals(obj);
      bool flag = this.type == elementSelector.type && this.value == elementSelector.value;
      if (flag && this.childSelector != null && elementSelector.ChildSelector != null)
        return this.childSelector.Equals((object) elementSelector.ChildSelector);
      return flag;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }
}
