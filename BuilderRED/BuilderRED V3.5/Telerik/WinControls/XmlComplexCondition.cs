// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlComplexCondition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Xml.Serialization;

namespace Telerik.WinControls
{
  public class XmlComplexCondition : XmlCondition
  {
    private XmlCondition condition1;
    private XmlCondition condition2;
    private BinaryOperator binaryOperator;

    [Browsable(false)]
    public XmlCondition Condition1
    {
      get
      {
        return this.condition1;
      }
      set
      {
        this.condition1 = value;
      }
    }

    [DefaultValue(BinaryOperator.AndOperator)]
    [XmlAttribute]
    public BinaryOperator BinaryOperator
    {
      get
      {
        return this.binaryOperator;
      }
      set
      {
        this.binaryOperator = value;
      }
    }

    [Browsable(false)]
    public XmlCondition Condition2
    {
      get
      {
        return this.condition2;
      }
      set
      {
        this.condition2 = value;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj != null && obj is XmlComplexCondition)
      {
        XmlComplexCondition complexCondition = obj as XmlComplexCondition;
        if (this.binaryOperator == complexCondition.binaryOperator && this.condition1.Equals((object) complexCondition.Condition1) && this.condition2.Equals((object) complexCondition.Condition2))
          return true;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    protected override void DeserializeProperties(Condition selector)
    {
    }

    protected override Condition CreateInstance()
    {
      return (Condition) new ComplexCondition(this.Condition1.Deserialize(), this.BinaryOperator, this.Condition2.Deserialize());
    }
  }
}
