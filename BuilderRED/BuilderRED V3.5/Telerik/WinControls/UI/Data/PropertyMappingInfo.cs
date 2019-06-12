// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Data.PropertyMappingInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI.Data
{
  internal class PropertyMappingInfo : IPropertyMappingInfo, IEnumerable<PropertyMapping>, IEnumerable
  {
    protected List<PropertyMapping> mappings = new List<PropertyMapping>();

    public List<PropertyMapping> Mappings
    {
      get
      {
        return this.mappings;
      }
    }

    public PropertyMapping FindByLogicalItemProperty(string logicalProperty)
    {
      return this.mappings.Find((Predicate<PropertyMapping>) (p => p.LogicalItemProperty == logicalProperty));
    }

    public PropertyMapping FindByDataSourceProperty(string dataSourceProperty)
    {
      return this.mappings.Find((Predicate<PropertyMapping>) (p => p.DataSourceItemProperty == dataSourceProperty));
    }

    public IEnumerator<PropertyMapping> GetEnumerator()
    {
      return (IEnumerator<PropertyMapping>) this.mappings.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.mappings.GetEnumerator();
    }
  }
}
