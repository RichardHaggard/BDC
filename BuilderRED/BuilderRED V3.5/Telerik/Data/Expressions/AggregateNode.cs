// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.AggregateNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Reflection;

namespace Telerik.Data.Expressions
{
  public class AggregateNode : FunctionNode
  {
    private static MethodInfo[] methods;
    private MethodInfo methodInfo;

    public static bool IsAggregare(string name)
    {
      return null != (object) AggregateNode.LookupFunc(name);
    }

    public AggregateNode(string name)
      : base((ExpressionNode) null, name)
    {
      if ((object) (this.methodInfo = AggregateNode.LookupFunc(name)) == null)
        throw InvalidExpressionException.UndefinedFunction(name);
    }

    public override object Eval(object row, object context)
    {
      IDataAggregate dataAggregate = row as IDataAggregate;
      if (dataAggregate == null)
        return (object) null;
      ArrayList arrayList = new ArrayList();
      arrayList.Add((object) new FunctionContext((IFormatProvider) this.Culture, context));
      arrayList.Add((object) dataAggregate.GetData());
      arrayList.AddRange((ICollection) this.Arguments);
      return this.methodInfo.Invoke((object) null, arrayList.ToArray());
    }

    private static MethodInfo LookupFunc(string name)
    {
      try
      {
        if (AggregateNode.methods == null)
          AggregateNode.methods = typeof (Aggregates).GetMethods(BindingFlags.Static | BindingFlags.Public);
        if (AggregateNode.methods != null)
        {
          name = name.ToUpper();
          for (int index = 0; index < AggregateNode.methods.Length; ++index)
          {
            MethodInfo method = AggregateNode.methods[index];
            if (method.Name.ToUpper() == name)
              return method;
          }
        }
        return (MethodInfo) null;
      }
      catch (Exception ex)
      {
        return (MethodInfo) null;
      }
    }
  }
}
