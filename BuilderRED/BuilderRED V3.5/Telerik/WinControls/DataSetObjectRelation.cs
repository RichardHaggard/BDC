// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.DataSetObjectRelation
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class DataSetObjectRelation : ObjectRelation
  {
    private List<string> childRelationNames;
    private List<string> parentRelationNames;

    internal DataSetObjectRelation(object list)
      : base(list)
    {
    }

    internal DataSetObjectRelation(object dataSource, string dataMember)
      : this(ListBindingHelper.GetList(dataSource, dataMember))
    {
    }

    public override string[] ParentRelationNames
    {
      get
      {
        return this.parentRelationNames.ToArray();
      }
    }

    public override string[] ChildRelationNames
    {
      get
      {
        return this.childRelationNames.ToArray();
      }
    }

    protected override void Initialize()
    {
      this.parentRelationNames = new List<string>();
      this.childRelationNames = new List<string>();
      DataTable dataTable = this.List as DataTable;
      if (dataTable == null && this.List is DataView)
        dataTable = ((DataView) this.List).Table;
      if (dataTable == null)
        return;
      this.Properties = ListBindingHelper.GetListItemProperties((object) dataTable);
      this.Name = dataTable.TableName;
      for (int index1 = 0; index1 < dataTable.ChildRelations.Count; ++index1)
      {
        DataTable childTable = dataTable.ChildRelations[index1].ChildTable;
        if (childTable == dataTable)
        {
          this.ChildRelations.Add((ObjectRelation) new SelfReferenceRelation((object) childTable, dataTable.ChildRelations[index1].ParentColumns[0].ColumnName, dataTable.ChildRelations[index1].ChildColumns[0].ColumnName));
        }
        else
        {
          DataSetObjectRelation setObjectRelation = new DataSetObjectRelation((object) childTable);
          setObjectRelation.Parent = (ObjectRelation) this;
          for (int index2 = 0; index2 < dataTable.ChildRelations[index1].ParentColumns.Length; ++index2)
            setObjectRelation.parentRelationNames.Add(dataTable.ChildRelations[index1].ParentColumns[index2].ColumnName);
          for (int index2 = 0; index2 < dataTable.ChildRelations[index1].ChildColumns.Length; ++index2)
            setObjectRelation.childRelationNames.Add(dataTable.ChildRelations[index1].ChildColumns[index2].ColumnName);
          this.ChildRelations.Add((ObjectRelation) setObjectRelation);
        }
      }
    }
  }
}
