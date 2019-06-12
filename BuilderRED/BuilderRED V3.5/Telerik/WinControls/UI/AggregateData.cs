// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.AggregateData
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.UI
{
  internal class AggregateData : IDataAggregate
  {
    private IEnumerable<GridViewRowInfo> rows;

    public AggregateData(IEnumerable<GridViewRowInfo> rows)
    {
      this.rows = rows;
    }

    public IEnumerable GetData()
    {
      return (IEnumerable) this.rows;
    }

    public static List<GridViewColumn> GetColumnsForExpression(
      GridViewTemplate template,
      ExpressionNode expression,
      out bool containsAggregate)
    {
      containsAggregate = false;
      List<GridViewColumn> gridViewColumnList = new List<GridViewColumn>();
      Stack<ExpressionNode> expressionNodeStack = new Stack<ExpressionNode>();
      expressionNodeStack.Push(expression);
      while (expressionNodeStack.Count > 0)
      {
        ExpressionNode expressionNode = expressionNodeStack.Pop();
        if (expressionNode != null)
        {
          NameNode nameNode = expressionNode as NameNode;
          if (nameNode != null && !nameNode.IsConst)
          {
            GridViewColumn column = (GridViewColumn) template.Columns[nameNode.Name];
            if (column != null)
              gridViewColumnList.Add(column);
          }
          else if (expressionNode is AggregateNode)
            containsAggregate = true;
          foreach (ExpressionNode childNode in expressionNode.GetChildNodes())
            expressionNodeStack.Push(childNode);
        }
      }
      return gridViewColumnList;
    }
  }
}
