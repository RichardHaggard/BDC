// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.TypeSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls
{
  [Serializable]
  public class TypeSelector : HierarchicalSelector
  {
    private Type elementType = typeof (TypeSelector.NoSuchElement);

    public TypeSelector()
    {
    }

    public TypeSelector(Type elementType, Condition condition)
    {
      this.ElementType = elementType;
      this.Condition = condition;
    }

    protected internal override bool CanUseCache
    {
      get
      {
        return true;
      }
    }

    protected override int GetKey()
    {
      if ((object) this.elementType == null)
        return 0;
      return this.elementType.GetHashCode();
    }

    public TypeSelector(Type elementType)
    {
      this.elementType = elementType;
    }

    public Type ElementType
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

    protected override bool CanSelectOverride(RadObject element)
    {
      IStylableNode stylableNode = element as IStylableNode;
      if (stylableNode != null && element != null)
        return (object) this.elementType == (object) stylableNode.GetThemeEffectiveType();
      return false;
    }

    public override bool Equals(IElementSelector elementSelector)
    {
      TypeSelector typeSelector = elementSelector as TypeSelector;
      if (typeSelector != null)
        return (object) typeSelector.elementType == (object) this.elementType;
      return false;
    }

    protected override LinkedList<RadObject> FindElements(
      IDictionary ChildrenHierarchyByElement)
    {
      return (LinkedList<RadObject>) ChildrenHierarchyByElement[(object) this.elementType];
    }

    public override string ToString()
    {
      if ((object) this.elementType == null)
        return "ThemeEffectiveType == NotSpecified";
      return "ThemeEffectiveType == " + (object) this.elementType;
    }

    private class NoSuchElement : VisualElement
    {
    }
  }
}
