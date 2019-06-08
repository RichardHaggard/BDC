// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Data.PropertyMapping
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI.Data
{
  public class PropertyMapping
  {
    private string logicalItemProperty = string.Empty;
    private string dataSourceProperty = string.Empty;
    public ConvertCallback ConvertToLogicalValue;
    public ConvertCallback ConvertToDataSourceValue;

    public PropertyMapping(string logicalItemProperty, string dataSourceProperty)
    {
      this.logicalItemProperty = logicalItemProperty;
      this.dataSourceProperty = dataSourceProperty;
    }

    public string LogicalItemProperty
    {
      get
      {
        return this.logicalItemProperty;
      }
      set
      {
        this.logicalItemProperty = value;
      }
    }

    public string DataSourceItemProperty
    {
      get
      {
        return this.dataSourceProperty;
      }
      set
      {
        this.dataSourceProperty = value;
      }
    }
  }
}
