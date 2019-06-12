// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterDescriptorCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Telerik.Collections.Generic;
using Telerik.Data.Expressions;

namespace Telerik.WinControls.Data
{
  public class FilterDescriptorCollection : NotifyCollection<FilterDescriptor>
  {
    private FilterLogicalOperator logicalOperator;
    private bool useCaseSensitiveFieldNames;

    public bool UseCaseSensitiveFieldNames
    {
      get
      {
        return this.useCaseSensitiveFieldNames;
      }
      set
      {
        this.useCaseSensitiveFieldNames = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(FilterLogicalOperator.And)]
    public virtual FilterLogicalOperator LogicalOperator
    {
      get
      {
        return this.logicalOperator;
      }
      set
      {
        if (this.logicalOperator == value)
          return;
        this.logicalOperator = value;
        this.OnPropertyChanged(nameof (LogicalOperator));
      }
    }

    public virtual string Expression
    {
      get
      {
        if (this.Count == 0)
          return string.Empty;
        List<string> stringList = new List<string>();
        for (int index = 0; index < this.Count; ++index)
        {
          string expression = this[index].Expression;
          if (!string.IsNullOrEmpty(expression))
            stringList.Add(expression);
        }
        return string.Join(this.LogicalOperator == FilterLogicalOperator.And ? " AND " : " OR ", stringList.ToArray());
      }
      set
      {
        this.Parse(value);
        this.OnPropertyChanged(nameof (Expression));
      }
    }

    public void Add(string propertyName, FilterOperator filterOperator, object value)
    {
      this.Add(new FilterDescriptor(propertyName, filterOperator, value));
    }

    public int IndexOf(string propertyName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (!string.IsNullOrEmpty(this[index].PropertyName) && this[index].PropertyName.Equals(propertyName, this.UseCaseSensitiveFieldNames ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
          return index;
      }
      return -1;
    }

    public bool Contains(string propertyName)
    {
      return this.IndexOf(propertyName) >= 0;
    }

    public bool Remove(string propertyName)
    {
      return this.Remove(propertyName, (Predicate<FilterDescriptor>) null);
    }

    public bool Remove(string propertyName, Predicate<FilterDescriptor> predicate)
    {
      bool flag1 = false;
      int index1 = 0;
      this.BeginUpdate();
      while (index1 < this.Count)
      {
        CompositeFilterDescriptor filterDescriptor = this[index1] as CompositeFilterDescriptor;
        if (filterDescriptor != null)
        {
          if (filterDescriptor.FilterDescriptors.Remove(propertyName, predicate))
            flag1 = true;
          if (filterDescriptor.FilterDescriptors.Count == 0)
          {
            this.RemoveAt(index1);
            continue;
          }
        }
        ++index1;
      }
      bool flag2 = flag1;
      int index2 = this.IndexOf(propertyName);
      bool flag3 = index2 >= 0;
      if (predicate != null && flag3)
        flag3 = predicate(this[index2]);
      if (flag3)
      {
        this.RemoveAt(index2);
        flag1 = true;
      }
      this.EndUpdate(flag2 || flag1);
      return flag1;
    }

    private void Parse(string expression)
    {
      ExpressionNode binaryNode1 = ExpressionParser.Parse(expression, false);
      this.BeginUpdate();
      this.Clear();
      Stack<ExpressionNode> expressionNodeStack = new Stack<ExpressionNode>();
      expressionNodeStack.Push(binaryNode1);
      Stack<FilterDescriptorCollection> descriptorCollectionStack = new Stack<FilterDescriptorCollection>();
      descriptorCollectionStack.Push(this);
      this.LogicalOperator = this.GetCompositeFilterDescriptorLogicalOperator(binaryNode1);
      bool flag = false;
      while (expressionNodeStack.Count > 0)
      {
        ExpressionNode expressionNode = expressionNodeStack.Pop();
        FilterDescriptorCollection descriptorCollection1 = descriptorCollectionStack.Pop();
        UnaryOpNode unaryOpNode = expressionNode as UnaryOpNode;
        if (unaryOpNode != null)
        {
          if (unaryOpNode.Op == Operator.Noop)
          {
            expressionNodeStack.Push(((UnaryOpNode) expressionNode).Right);
            descriptorCollectionStack.Push(descriptorCollection1);
            continue;
          }
          if (unaryOpNode.Op == Operator.Not)
          {
            expressionNodeStack.Push(((UnaryOpNode) expressionNode).Right);
            flag = true;
            descriptorCollectionStack.Push(descriptorCollection1);
            continue;
          }
        }
        BinaryOpNode binaryNode2 = expressionNode as BinaryOpNode;
        if (binaryNode2 != null)
        {
          if (this.IsPredicate((ExpressionNode) binaryNode2))
          {
            FilterDescriptor filterDescriptor = this.CreateFilterDescriptor(binaryNode2);
            if (flag)
            {
              filterDescriptor.Operator = this.GetNegativeOperator(filterDescriptor.Operator);
              flag = false;
            }
            descriptorCollection1.Add(filterDescriptor);
          }
          else
          {
            expressionNodeStack.Push(binaryNode2.Right);
            FilterDescriptorCollection descriptorCollection2 = descriptorCollection1;
            if (!this.IsPredicate(binaryNode2.Right))
            {
              CompositeFilterDescriptor filterDescriptor = new CompositeFilterDescriptor();
              filterDescriptor.LogicalOperator = this.GetCompositeFilterDescriptorLogicalOperator(binaryNode2.Right);
              descriptorCollection1.Add((FilterDescriptor) filterDescriptor);
              descriptorCollection2 = filterDescriptor.FilterDescriptors;
            }
            descriptorCollectionStack.Push(descriptorCollection2);
            expressionNodeStack.Push(binaryNode2.Left);
            FilterDescriptorCollection descriptorCollection3 = descriptorCollection1;
            if (!this.IsPredicate(binaryNode2.Left))
            {
              CompositeFilterDescriptor filterDescriptor = new CompositeFilterDescriptor();
              filterDescriptor.LogicalOperator = this.GetCompositeFilterDescriptorLogicalOperator(binaryNode2.Left);
              descriptorCollection1.Add((FilterDescriptor) filterDescriptor);
              descriptorCollection3 = filterDescriptor.FilterDescriptors;
            }
            descriptorCollectionStack.Push(descriptorCollection3);
          }
        }
      }
      this.EndUpdate();
    }

    private FilterOperator GetNegativeOperator(FilterOperator filterOperator)
    {
      FilterOperator filterOperator1 = FilterOperator.None;
      switch (filterOperator)
      {
        case FilterOperator.IsLike:
          filterOperator1 = FilterOperator.IsNotLike;
          break;
        case FilterOperator.IsNotLike:
          filterOperator1 = FilterOperator.IsLike;
          break;
        case FilterOperator.IsLessThan:
          filterOperator1 = FilterOperator.IsGreaterThan;
          break;
        case FilterOperator.IsLessThanOrEqualTo:
          filterOperator1 = FilterOperator.IsGreaterThanOrEqualTo;
          break;
        case FilterOperator.IsEqualTo:
          filterOperator1 = FilterOperator.IsNotEqualTo;
          break;
        case FilterOperator.IsNotEqualTo:
          filterOperator1 = FilterOperator.IsEqualTo;
          break;
        case FilterOperator.IsGreaterThanOrEqualTo:
          filterOperator1 = FilterOperator.IsLessThanOrEqualTo;
          break;
        case FilterOperator.IsGreaterThan:
          filterOperator1 = FilterOperator.IsLessThan;
          break;
        case FilterOperator.StartsWith:
          filterOperator1 = FilterOperator.EndsWith;
          break;
        case FilterOperator.EndsWith:
          filterOperator1 = FilterOperator.StartsWith;
          break;
        case FilterOperator.Contains:
          filterOperator1 = FilterOperator.NotContains;
          break;
        case FilterOperator.NotContains:
          filterOperator1 = FilterOperator.Contains;
          break;
        case FilterOperator.IsNull:
          filterOperator1 = FilterOperator.IsNotNull;
          break;
        case FilterOperator.IsNotNull:
          filterOperator1 = FilterOperator.IsNull;
          break;
        case FilterOperator.IsContainedIn:
          filterOperator1 = FilterOperator.IsNotContainedIn;
          break;
        case FilterOperator.IsNotContainedIn:
          filterOperator1 = FilterOperator.IsContainedIn;
          break;
      }
      return filterOperator1;
    }

    private FilterLogicalOperator GetCompositeFilterDescriptorLogicalOperator(
      ExpressionNode binaryNode)
    {
      ExpressionNode expressionNode = binaryNode;
      for (UnaryOpNode unaryOpNode = expressionNode as UnaryOpNode; unaryOpNode != null && unaryOpNode.Op == Operator.Noop; unaryOpNode = expressionNode as UnaryOpNode)
        expressionNode = unaryOpNode.Right;
      BinaryOpNode binaryOpNode = expressionNode as BinaryOpNode;
      return binaryOpNode != null && binaryOpNode.Op == Operator.Or ? FilterLogicalOperator.Or : FilterLogicalOperator.And;
    }

    private FilterDescriptor CreateFilterDescriptor(BinaryOpNode binaryNode)
    {
      bool flag = false;
      NameNode nameNode = binaryNode.Left as NameNode;
      ConstNode constNode1 = binaryNode.Right as ConstNode;
      ZeroOpNode zeroOpNode = (ZeroOpNode) null;
      FunctionNode functionNode = (FunctionNode) null;
      if (constNode1 == null)
        zeroOpNode = binaryNode.Right as ZeroOpNode;
      if (constNode1 == null && binaryNode.Right is UnaryOpNode)
      {
        UnaryOpNode right = binaryNode.Right as UnaryOpNode;
        flag = right.Op.Name == "Not";
        zeroOpNode = right.Right as ZeroOpNode;
      }
      if (constNode1 == null && binaryNode.Right is FunctionNode)
        functionNode = binaryNode.Right as FunctionNode;
      if (nameNode == null)
      {
        nameNode = binaryNode.Right as NameNode;
        constNode1 = binaryNode.Left as ConstNode;
        if (constNode1 == null)
          zeroOpNode = binaryNode.Left as ZeroOpNode;
        if (constNode1 == null && binaryNode.Left is UnaryOpNode)
          zeroOpNode = (binaryNode.Left as UnaryOpNode).Right as ZeroOpNode;
        if (constNode1 == null && binaryNode.Left is FunctionNode)
          functionNode = binaryNode.Left as FunctionNode;
      }
      if (nameNode == null)
        throw new ArgumentException("Invalid BinaryOpNode parameter");
      FilterDescriptor filterDescriptor = new FilterDescriptor();
      filterDescriptor.PropertyName = nameNode.Name;
      if (constNode1 != null)
        filterDescriptor.Value = constNode1.Value;
      else if (zeroOpNode != null)
        filterDescriptor.Value = this.GetValueFormText(zeroOpNode.Operator.Name);
      else if (functionNode != null)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (ConstNode constNode2 in functionNode.Arguments)
        {
          stringBuilder.Append(constNode2.Value);
          stringBuilder.Append(", ");
        }
        stringBuilder.Length -= 2;
        filterDescriptor.Value = (object) stringBuilder.ToString();
      }
      filterDescriptor.Operator = this.GetOperator(binaryNode.Op);
      if (binaryNode.Op == Operator.Is && filterDescriptor.Value == null)
        filterDescriptor.Operator = FilterOperator.IsNull;
      if (flag)
        filterDescriptor.Operator = this.GetNegativeOperator(filterDescriptor.Operator);
      return filterDescriptor;
    }

    private object GetValueFormText(string operatorText)
    {
      switch (operatorText.ToLower())
      {
        case "true":
          return (object) true;
        case "false":
          return (object) false;
        case "null":
          return (object) null;
        default:
          return (object) null;
      }
    }

    private FilterOperator GetOperator(Operator p)
    {
      if (p == Operator.Like)
        return FilterOperator.IsLike;
      if (p == Operator.LessOrEqual)
        return FilterOperator.IsLessThanOrEqualTo;
      if (p == Operator.LessThen)
        return FilterOperator.IsLessThan;
      if (p == Operator.GreaterOrEqual)
        return FilterOperator.IsGreaterThanOrEqualTo;
      if (p == Operator.GreaterThen)
        return FilterOperator.IsGreaterThan;
      if (p == Operator.NotEqual)
        return FilterOperator.IsNotEqualTo;
      if (p == Operator.EqualTo)
        return FilterOperator.IsEqualTo;
      if (p == Operator.In)
        return FilterOperator.IsContainedIn;
      return p == Operator.Null ? FilterOperator.IsNull : FilterOperator.Contains;
    }

    private bool IsPredicate(ExpressionNode node)
    {
      while (node is UnaryOpNode)
        node = ((UnaryOpNode) node).Right;
      BinaryOpNode binaryOpNode = node as BinaryOpNode;
      return binaryOpNode != null && (binaryOpNode.Left is NameNode || binaryOpNode.Right is NameNode);
    }
  }
}
