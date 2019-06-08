// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ExpressionAccessor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.UI
{
  internal class ExpressionAccessor : Accessor
  {
    internal static bool ExpressionErrorRaised;

    public ExpressionAccessor(GridViewColumn column)
      : base(column)
    {
    }

    public override object this[GridViewRowInfo row]
    {
      get
      {
        return this.EvaluateExpression(row, this.Column);
      }
      set
      {
        base[row] = value;
      }
    }

    private object EvaluateExpression(GridViewRowInfo row, GridViewColumn column)
    {
      if (row.ViewTemplate.MasterTemplate.Owner.InvokeRequired)
      {
        object result = (object) null;
        row.ViewTemplate.MasterTemplate.Owner.Invoke((Delegate) (() => result = this.EvaluateExpression(row, column)));
        return result;
      }
      ExpressionNode expressionNode;
      if (DataUtils.TryParse(column.Expression, row.ViewTemplate.CaseSensitive, out expressionNode))
      {
        ExpressionContext context = ExpressionContext.Context;
        bool containsAggregate = false;
        List<GridViewColumn> columnsForExpression = this.GetColumnsForExpression(row.ViewTemplate, expressionNode, out containsAggregate);
        for (int index1 = 0; index1 < columnsForExpression.Count; ++index1)
        {
          if (columnsForExpression[index1] != column)
          {
            GridViewColumn index2 = columnsForExpression[index1];
            Accessor accessor = index2.Accessor;
            object obj = !(accessor is ExpressionAccessor) || !row.Cache.ContainsKey((object) index2) ? accessor[row] : row.Cache[index2];
            if (context.ContainsKey(index2.Name))
              context[index2.Name] = obj;
            else
              context.Add(index2.Name, obj);
          }
        }
        AggregateData aggregateData = (AggregateData) null;
        if (containsAggregate)
        {
          GridViewGroupRowInfo parent = row.Parent as GridViewGroupRowInfo;
          aggregateData = parent == null ? new AggregateData((IEnumerable<GridViewRowInfo>) row.ViewTemplate.ChildRows) : new AggregateData((IEnumerable<GridViewRowInfo>) parent.ChildRows);
        }
        try
        {
          object obj1 = expressionNode.Eval((object) aggregateData, (object) context);
          object obj2 = RadDataConverter.Instance.Parse(column as IDataConversionInfoProvider, obj1);
          row.Cache[column] = obj2;
        }
        catch (InvalidExpressionException ex)
        {
          if (!ExpressionAccessor.ExpressionErrorRaised)
          {
            string message = string.Format("Invalid Expression: \n\"{0}\"", (object) ex.Message);
            row.ViewTemplate.SetError(new GridViewCellCancelEventArgs(row, column, (IInputEditor) null), DataUtils.CreateInvalidExpressionException(message));
            ExpressionAccessor.ExpressionErrorRaised = true;
          }
        }
      }
      else if (!ExpressionAccessor.ExpressionErrorRaised)
      {
        if (!string.IsNullOrEmpty(column.HeaderText))
        {
          string headerText = column.HeaderText;
        }
        else if (!string.IsNullOrEmpty(column.FieldName))
        {
          string fieldName = column.FieldName;
        }
        else if (!string.IsNullOrEmpty(column.Name))
        {
          string name = column.Name;
        }
        else
        {
          string empty = string.Empty;
        }
        string message = string.Format("Invalid expression value for [{0}] column!", (object) column.Name);
        row.ViewTemplate.SetError(new GridViewCellCancelEventArgs(row, column, (IInputEditor) null), DataUtils.CreateInvalidExpressionException(message));
        ExpressionAccessor.ExpressionErrorRaised = true;
      }
      return row.Cache[column];
    }

    private List<GridViewColumn> GetColumnsForExpression(
      GridViewTemplate gridTemplate,
      ExpressionNode expression,
      out bool containsAggregate)
    {
      containsAggregate = false;
      List<GridViewColumn> gridViewColumnList = new List<GridViewColumn>();
      Stack<ExpressionNode> expressionNodeStack = new Stack<ExpressionNode>();
      expressionNodeStack.Push(expression);
      while (expressionNodeStack.Count > 0)
      {
        ExpressionNode expressionNode1 = expressionNodeStack.Pop();
        if (expressionNode1 != null)
        {
          NameNode nameNode = expressionNode1 as NameNode;
          if (nameNode != null && !nameNode.IsConst)
          {
            GridViewColumn column = (GridViewColumn) gridTemplate.Columns[nameNode.Name];
            if (column != null && !gridViewColumnList.Contains(column))
              gridViewColumnList.Add(column);
          }
          else if (expressionNode1 is AggregateNode)
            containsAggregate = true;
          IEnumerable<ExpressionNode> childNodes = expressionNode1.GetChildNodes();
          if (childNodes != null)
          {
            foreach (ExpressionNode expressionNode2 in childNodes)
              expressionNodeStack.Push(expressionNode2);
          }
        }
      }
      return gridViewColumnList;
    }
  }
}
