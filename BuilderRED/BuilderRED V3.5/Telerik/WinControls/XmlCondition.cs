// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlCondition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public abstract class XmlCondition
  {
    public string BuildExpressionString()
    {
      string str1 = "";
      if (this is XmlComplexCondition)
      {
        XmlComplexCondition complexCondition = this as XmlComplexCondition;
        string str2 = str1 + "(";
        string str3 = (complexCondition.Condition1 == null ? str2 + "unknown" : str2 + complexCondition.Condition1.BuildExpressionString()) + " " + complexCondition.BinaryOperator.ToString().Replace("Operator", "").ToLower() + " ";
        str1 = (complexCondition.Condition2 == null ? str3 + "unknown" : str3 + complexCondition.Condition2.BuildExpressionString()) + ")";
      }
      else if (this is XmlSimpleCondition)
      {
        XmlSimpleCondition xmlSimpleCondition = this as XmlSimpleCondition;
        if (xmlSimpleCondition.UnaryOperator == UnaryOperator.NotOperator)
          str1 += "!";
        str1 = xmlSimpleCondition.Setting == null ? str1 + "unknown" : str1 + xmlSimpleCondition.Setting.GetPropertyName();
      }
      return str1;
    }

    public Condition Deserialize()
    {
      Condition instance = this.CreateInstance();
      this.DeserializeProperties(instance);
      return instance;
    }

    protected abstract void DeserializeProperties(Condition selector);

    protected abstract Condition CreateInstance();
  }
}
