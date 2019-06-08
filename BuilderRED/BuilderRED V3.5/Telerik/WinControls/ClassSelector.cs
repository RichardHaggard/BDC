// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ClassSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class ClassSelector : HierarchicalSelector
  {
    private string elementClass;

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

    public ClassSelector()
    {
    }

    public ClassSelector(string className, Condition condition)
    {
      this.ElementClass = className;
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
      if (string.IsNullOrEmpty(this.elementClass))
        return 0;
      return ClassSelector.GetSelectorKey(this.elementClass).GetHashCode();
    }

    public static int GetSelectorKey(string elementClass)
    {
      return ("Class=" + elementClass).GetHashCode();
    }

    public override bool Equals(IElementSelector elementSelector)
    {
      ClassSelector classSelector = elementSelector as ClassSelector;
      if (classSelector != null)
        return classSelector.elementClass == this.elementClass;
      return false;
    }

    public ClassSelector(string elementClass)
    {
      this.elementClass = elementClass;
    }

    protected override bool CanSelectOverride(RadObject element)
    {
      IStylableNode stylableNode = element as IStylableNode;
      if (stylableNode != null && element != null)
        return string.Compare(stylableNode.Class, this.elementClass, true) == 0;
      return false;
    }

    public override LinkedList<RadObject> GetSelectedElements(RadObject element)
    {
      return base.GetSelectedElements(element);
    }

    protected override LinkedList<RadObject> FindElements(
      IDictionary ChildrenHierarchyByElement)
    {
      return (LinkedList<RadObject>) ChildrenHierarchyByElement[(object) this.elementClass];
    }

    public override string ToString()
    {
      if (this.elementClass == null)
        return "Class == NotSpecified";
      return "Class == " + this.elementClass;
    }
  }
}
