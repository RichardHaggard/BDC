// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Design.GridViewDesignTimeSerializer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.ComponentModel;
using System.Xml;
using Telerik.WinControls.Data;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI.Design
{
  public class GridViewDesignTimeSerializer : ComponentXmlSerializer
  {
    public GridViewDesignTimeSerializer(
      ComponentXmlSerializationInfo componentSerializationInfo)
      : base(componentSerializationInfo)
    {
    }

    protected override bool ProcessListOverride(
      XmlReader reader,
      object listOwner,
      PropertyDescriptor ownerProperty,
      IList list)
    {
      if (list is GridViewTemplateCollection)
      {
        this.ReadMergeCollection(reader, listOwner, ownerProperty, list, (string) null);
        return true;
      }
      if (!(list is GridViewColumnCollection))
        return base.ProcessListOverride(reader, listOwner, ownerProperty, list);
      GridViewColumnCollection columnCollection = list as GridViewColumnCollection;
      columnCollection.BeginUpdate();
      FilterDescriptorCollection descriptorCollection = (FilterDescriptorCollection) null;
      if (columnCollection.Owner != null)
        descriptorCollection = columnCollection.Owner.FilterDescriptors;
      descriptorCollection?.BeginUpdate();
      for (int index = 0; index < list.Count; ++index)
      {
        GridViewDataColumn gridViewDataColumn = list[index] as GridViewDataColumn;
        if (gridViewDataColumn != null)
          gridViewDataColumn.FilterDescriptor = (FilterDescriptor) null;
      }
      this.ReadMergeCollection(reader, listOwner, ownerProperty, list, "UniqueName");
      descriptorCollection?.EndUpdate();
      columnCollection.EndUpdate();
      return true;
    }
  }
}
