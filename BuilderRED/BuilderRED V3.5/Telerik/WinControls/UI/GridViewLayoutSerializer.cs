// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewLayoutSerializer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using Telerik.WinControls.Data;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI
{
  public class GridViewLayoutSerializer : ComponentXmlSerializer
  {
    private bool loadOldChildTemplates;
    private bool loadOldFilterExpressions;
    private bool loadOldSortExpressions;
    private bool loadOldGroupExpressions;

    public GridViewLayoutSerializer(
      ComponentXmlSerializationInfo componentSerializationInfo)
      : base(componentSerializationInfo)
    {
    }

    protected override bool ProcessProperty(PropertyDescriptor property)
    {
      if (property.Name.Equals("Filter", StringComparison.InvariantCultureIgnoreCase))
      {
        this.loadOldFilterExpressions = true;
        return base.ProcessProperty(property);
      }
      if (property.Name.Equals("Templates", StringComparison.InvariantCultureIgnoreCase))
        return this.loadOldChildTemplates;
      if (property.Name.Equals("SortDescriptors", StringComparison.InvariantCultureIgnoreCase))
        return this.loadOldSortExpressions;
      if (property.Name.Equals("GroupDescriptors", StringComparison.InvariantCultureIgnoreCase))
        return this.loadOldGroupExpressions;
      if (property.Name.Equals("FilterDescriptors", StringComparison.InvariantCultureIgnoreCase))
        return this.loadOldFilterExpressions;
      return base.ProcessProperty(property);
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
        this.loadOldChildTemplates = ownerProperty.Name.Equals("ChildGridViewTemplates", StringComparison.InvariantCultureIgnoreCase) && list.Count > 0;
        return true;
      }
      if (list is GridViewColumnCollection)
      {
        ((ObservableCollection<GridViewDataColumn>) list).BeginUpdate();
        Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
        for (int index = 0; index < list.Count; ++index)
        {
          GridViewDataColumn gridViewDataColumn = list[index] as GridViewDataColumn;
          if (gridViewDataColumn != null)
            dictionary.Add(gridViewDataColumn.Name, gridViewDataColumn.DataType);
        }
        this.ReadMergeCollection(reader, listOwner, ownerProperty, list, "UniqueName", true);
        for (int index = 0; index < list.Count; ++index)
        {
          GridViewDataColumn gridViewDataColumn = list[index] as GridViewDataColumn;
          if (gridViewDataColumn != null && (object) gridViewDataColumn.DataType == null && dictionary.ContainsKey(gridViewDataColumn.Name))
            gridViewDataColumn.DataType = dictionary[gridViewDataColumn.Name];
        }
        ((ObservableCollection<GridViewDataColumn>) list).EndUpdate();
        return true;
      }
      if (list is GridViewSortDescriptorCollection)
      {
        this.loadOldSortExpressions = ownerProperty.Name.Equals("SortExpressions", StringComparison.InvariantCultureIgnoreCase);
        return base.ProcessListOverride(reader, listOwner, ownerProperty, list);
      }
      if (list is GridViewGroupDescriptorCollection)
      {
        this.loadOldGroupExpressions = ownerProperty.Name.Equals("GroupByExpressions", StringComparison.InvariantCultureIgnoreCase);
        return base.ProcessListOverride(reader, listOwner, ownerProperty, list);
      }
      if (!(list is GridViewFilterDescriptorCollection))
        return base.ProcessListOverride(reader, listOwner, ownerProperty, list);
      this.loadOldFilterExpressions = ownerProperty.Name.Equals("FilterExpressions", StringComparison.InvariantCultureIgnoreCase);
      return base.ProcessListOverride(reader, listOwner, ownerProperty, list);
    }

    protected override object MatchObjectElement(
      XmlReader reader,
      object parent,
      PropertyDescriptor parentProperty,
      IList toRead,
      string propertyToMatch,
      IList existingInstancesToMatch,
      out int foundAtIndex)
    {
      return base.MatchObjectElement(reader, parent, parentProperty, toRead, propertyToMatch, existingInstancesToMatch, out foundAtIndex);
    }

    protected override bool ReadObjectElementOverride(XmlReader reader, object toRead)
    {
      if (reader.Name.Equals("Filter", StringComparison.InvariantCultureIgnoreCase))
        this.loadOldFilterExpressions = true;
      return base.ReadObjectElementOverride(reader, toRead);
    }

    protected override void ReadElementInObject(
      XmlReader reader,
      PropertyDescriptor property,
      object toRead)
    {
      base.ReadElementInObject(reader, property, toRead);
      GridViewTemplate template = toRead as GridViewTemplate;
      if (template == null || !(property.Name == "FilterDescriptors"))
        return;
      this.ProcessFilterDescriptors(template, template.FilterDescriptors);
    }

    private void ProcessFilterDescriptors(
      GridViewTemplate template,
      FilterDescriptorCollection filterDescriptorCollection)
    {
      foreach (FilterDescriptor filterDescriptor1 in (Collection<FilterDescriptor>) filterDescriptorCollection)
      {
        CompositeFilterDescriptor filterDescriptor2 = filterDescriptor1 as CompositeFilterDescriptor;
        if (filterDescriptor2 != null)
        {
          this.ProcessFilterDescriptors(template, filterDescriptor2.FilterDescriptors);
        }
        else
        {
          GridViewDataColumn[] columnByFieldName = template.Columns.GetColumnByFieldName(filterDescriptor1.PropertyName);
          if (columnByFieldName != null && columnByFieldName.Length > 0)
          {
            GridViewDataColumn gridViewDataColumn = columnByFieldName[0];
            filterDescriptor1.Value = RadDataConverter.Instance.Parse((IDataConversionInfoProvider) gridViewDataColumn, filterDescriptor1.Value);
          }
        }
      }
    }
  }
}
