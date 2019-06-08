// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.HierarchicalSelector
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls
{
  public abstract class HierarchicalSelector : SelectorBase
  {
    private IDictionary childrenHierarchyByElement;

    protected internal abstract bool CanUseCache { get; }

    public override LinkedList<RadObject> GetSelectedElements(RadObject element)
    {
      IStylableNode stylableNode = element as IStylableNode;
      if (stylableNode == null)
        return new LinkedList<RadObject>();
      if (this.childrenHierarchyByElement != null && this.CanUseCache)
        return this.FindElements(this.childrenHierarchyByElement) ?? new LinkedList<RadObject>();
      LinkedList<RadObject> linkedList = new LinkedList<RadObject>();
      if (this.CanSelectOverride(element))
        linkedList.AddLast(element);
      if (this.CanSelectOverride(element))
        linkedList.AddLast(element);
      foreach (RadObject targetElement in stylableNode.ChildrenHierarchy)
      {
        if (this.CanSelectIgnoringConditions(targetElement))
          linkedList.AddLast(targetElement);
      }
      return linkedList;
    }

    protected virtual LinkedList<RadObject> FindElements(
      IDictionary childrenHierarchyByElement)
    {
      return (LinkedList<RadObject>) null;
    }

    internal void SetCache(IDictionary cache)
    {
      this.childrenHierarchyByElement = cache;
    }
  }
}
