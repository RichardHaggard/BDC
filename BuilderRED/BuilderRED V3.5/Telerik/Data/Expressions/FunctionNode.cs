// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.FunctionNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Telerik.Data.Expressions
{
  public class FunctionNode : ExpressionNode
  {
    private ExpressionNode node;
    private string name;
    private List<ExpressionNode> arguments;
    private MethodInfo method;

    public override bool IsConst
    {
      get
      {
        if (this.arguments != null)
        {
          for (int index = 0; index < this.arguments.Count; ++index)
          {
            if (!this.arguments[index].IsConst)
              return false;
          }
        }
        return true;
      }
    }

    public List<ExpressionNode> Arguments
    {
      get
      {
        return this.arguments;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public bool IsGlobal
    {
      get
      {
        return this.node == null;
      }
    }

    public FunctionNode(ExpressionNode node, string name)
    {
      this.node = node;
      this.name = name;
    }

    private static object NormalizeValue(object value)
    {
      if (DBNull.Value == value)
        return (object) null;
      return value;
    }

    public override object Eval(object row, object context)
    {
      ArrayList arrayList = new ArrayList();
      if (this.arguments != null && this.arguments.Count > 0)
      {
        for (int index = 0; index < this.arguments.Count; ++index)
          arrayList.Add(FunctionNode.NormalizeValue(this.arguments[index].Eval(row, context)));
      }
      object[] array = arrayList.ToArray();
      object target = context;
      if (this.node != null)
        target = this.node.Eval(row, context);
      if ((object) this.method == null)
        this.method = this.GetMethod(target, array);
      MethodInfo method = this.method;
      if ((object) method == null)
        throw InvalidExpressionException.UndefinedFunction(this.name);
      try
      {
        return method.Invoke(target, array);
      }
      catch (Exception ex)
      {
        throw InvalidExpressionException.ErrorInFunc(method.Name, ex);
      }
    }

    public void AddArgument(ExpressionNode argument)
    {
      if (this.arguments == null)
        this.arguments = new List<ExpressionNode>();
      this.arguments.Add(argument);
    }

    public void Check()
    {
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.name);
      stringBuilder.Append("(");
      if (this.arguments != null)
      {
        for (int index = 0; index < this.arguments.Count; ++index)
        {
          if (index > 0)
            stringBuilder.Append(", ");
          stringBuilder.Append(this.arguments[index].ToString());
        }
      }
      stringBuilder.Append(")");
      return stringBuilder.ToString();
    }

    private MethodInfo GetMethod(object target, object[] args)
    {
      try
      {
        foreach (MethodInfo method in FunctionNode.GetMethods(target))
        {
          if (string.Compare(method.Name, this.name, StringComparison.OrdinalIgnoreCase) == 0)
          {
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length == args.Length)
            {
              bool flag = true;
              for (int index = 0; index < args.Length; ++index)
              {
                Type parameterType = parameters[index].ParameterType;
                object obj = args[index];
                if (obj == null)
                {
                  if (!parameterType.IsValueType || (object) Nullable.GetUnderlyingType(parameterType) != null)
                    continue;
                }
                else if (parameterType.IsAssignableFrom(obj.GetType()))
                  continue;
                flag = false;
                break;
              }
              if (flag)
                return method;
            }
          }
        }
        return (MethodInfo) null;
      }
      catch (Exception ex)
      {
        return (MethodInfo) null;
      }
    }

    private static IEnumerable<MethodInfo> GetMethods(object target)
    {
      if (target is IEnumerable<MethodInfo>)
        return (IEnumerable<MethodInfo>) target;
      return (IEnumerable<MethodInfo>) target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
    }

    public override IEnumerable<ExpressionNode> GetChildNodes()
    {
      return (IEnumerable<ExpressionNode>) this.Arguments;
    }
  }
}
