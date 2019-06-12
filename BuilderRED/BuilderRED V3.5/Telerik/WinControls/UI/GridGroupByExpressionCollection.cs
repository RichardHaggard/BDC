// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupByExpressionCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  [ListBindable(BindableSupport.No)]
  public class GridGroupByExpressionCollection : GridViewGroupDescriptorCollection
  {
    public GridGroupByExpressionCollection(GridViewTemplate owner)
      : base(owner)
    {
    }

    public override string Expression
    {
      get
      {
        return base.Expression;
      }
      set
      {
        base.Expression = value;
      }
    }

    public void Add(string expression)
    {
      this.Add(expression, (string) null);
    }

    public void Add(string expression, string formatString)
    {
      this.Add((GroupDescriptor) new GridGroupByExpression(expression)
      {
        DefaultFormatString = formatString
      });
    }

    public int IndexOf(string expression)
    {
      GridGroupByExpression groupByExpression = new GridGroupByExpression(expression);
      return new SortDescriptorCollection()
      {
        Expression = groupByExpression.GroupByFields.ToString()
      }.Count < 1 ? -1 : -1;
    }

    private GroupDescriptor CreateGroupDescription(GridGroupByExpression expression)
    {
      return new GroupDescriptor(expression.GroupByFields.ToString());
    }

    private GridGroupByExpression CreateGridGroupByExpression(
      GroupDescriptor groupDescription)
    {
      GridGroupByExpression groupByExpression = new GridGroupByExpression();
      for (int index = 0; index < groupDescription.GroupNames.Count; ++index)
        groupByExpression.GroupByFields.Add(new GridGroupByField(groupDescription.GroupNames[index].PropertyName)
        {
          SortOrder = GridViewHelper.GetSortDirection(groupDescription.GroupNames[index].Direction)
        });
      return groupByExpression;
    }
  }
}
