// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DataFilterElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class DataFilterElementProvider : TreeViewElementProvider
  {
    public DataFilterElementProvider(RadTreeViewElement treeViewElement)
      : base(treeViewElement)
    {
    }

    public override IVirtualizedElement<RadTreeNode> CreateElement(
      RadTreeNode data,
      object context)
    {
      CreateTreeNodeElementEventArgs e = new CreateTreeNodeElementEventArgs(data);
      this.TreeViewElement.OnCreateNodeElement(e);
      if (e.NodeElement != null)
        return (IVirtualizedElement<RadTreeNode>) e.NodeElement;
      if (data is DataFilterGroupNode)
        return (IVirtualizedElement<RadTreeNode>) new DataFilterGroupElement();
      if (data is DataFilterCriteriaNode)
      {
        DataFilterCriteriaNode filterCriteriaNode = data as DataFilterCriteriaNode;
        if ((object) filterCriteriaNode.ValueType == (object) typeof (bool))
          return (IVirtualizedElement<RadTreeNode>) new DataFilterCheckboxCriteriaElement();
        if ((object) filterCriteriaNode.ValueType == (object) typeof (Color))
          return (IVirtualizedElement<RadTreeNode>) new DataFilterColorboxCriteriaElement();
        return (IVirtualizedElement<RadTreeNode>) new DataFilterCriteriaElement();
      }
      if (data is DataFilterAddNode)
        return (IVirtualizedElement<RadTreeNode>) new DataFilterAddNodeElement();
      return base.CreateElement(data, context);
    }

    public override bool IsCompatible(
      IVirtualizedElement<RadTreeNode> element,
      RadTreeNode data,
      object context)
    {
      return base.IsCompatible(element, data, context);
    }
  }
}
