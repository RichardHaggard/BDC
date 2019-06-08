// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.NameNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.Data.Expressions
{
  public class NameNode : ExpressionNode
  {
    private Dictionary<Type, PropertyDescriptor> properties = new Dictionary<Type, PropertyDescriptor>();
    private const string FildsObject = "Fields";
    private string name;
    private ExpressionNode parent;

    public ExpressionNode Parent
    {
      get
      {
        return this.parent;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
    }

    public NameNode(ExpressionNode node, string name)
    {
      this.name = NameNode.ParseName(name);
      this.parent = node;
    }

    public override object Eval(object row, object context)
    {
      object component = context;
      if (this.parent != null)
        component = this.parent.Eval(row, context);
      else if ("Fields" == this.name)
        return row;
      if (component == null)
        return (object) null;
      IDataItem dataItem = row as IDataItem;
      if (dataItem != null)
        return dataItem[this.name];
      PropertyDescriptor propertyDescriptor = (PropertyDescriptor) null;
      Type type = component.GetType();
      if (!this.properties.TryGetValue(type, out propertyDescriptor))
      {
        PropertyDescriptorCollection descriptorCollection = !(component is IDataDescriptor) ? TypeDescriptor.GetProperties(component) : ((IDataDescriptor) component).GetProperties();
        if (descriptorCollection != null && descriptorCollection.Count > 0)
        {
          bool ignoreCase = !(context is ExpressionContext) || !((ExpressionContext) context).CaseSensitive;
          this.properties[type] = propertyDescriptor = descriptorCollection.Find(this.name, ignoreCase);
        }
      }
      if (propertyDescriptor != null)
        return propertyDescriptor.GetValue(component);
      throw InvalidExpressionException.UndefinedObject(this.name);
    }

    public override string ToString()
    {
      return "Identifier(" + this.Name + ")";
    }

    private static string ParseName(string name)
    {
      char[] charArray = name.ToCharArray();
      char ch = char.MinValue;
      string str = string.Empty;
      if ('`' == charArray[0])
      {
        ch = '\\';
        str = "`";
      }
      else if ('[' == charArray[0])
      {
        ch = '\\';
        str = "]\\";
      }
      if (ch == char.MinValue)
        return name;
      string empty = string.Empty;
      int num = charArray.Length - 1;
      for (int index = 1; index < num; ++index)
      {
        if ((int) ch != (int) charArray[index] || index + 1 >= num || str.IndexOf(charArray[index + 1]) < 0)
          empty += (string) (object) charArray[index];
      }
      return empty;
    }

    public override IEnumerable<ExpressionNode> GetChildNodes()
    {
      yield return this.parent;
    }
  }
}
