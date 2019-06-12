// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSimpleCondition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  public class XmlSimpleCondition : XmlCondition
  {
    private XmlPropertySetting setting = new XmlPropertySetting();
    private UnaryOperator unaryOperator;

    [Description("Unary operator to apply when comparing property with value given")]
    [DefaultValue(UnaryOperator.None)]
    public UnaryOperator UnaryOperator
    {
      get
      {
        return this.unaryOperator;
      }
      set
      {
        this.unaryOperator = value;
      }
    }

    [Description("Property and value to compare")]
    public XmlPropertySetting Setting
    {
      get
      {
        return this.setting;
      }
      set
      {
        this.setting = value;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj != null && obj is XmlSimpleCondition && this.setting == (obj as XmlSimpleCondition).Setting)
        return this.unaryOperator == (obj as XmlSimpleCondition).UnaryOperator;
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
      return (Condition) new SimpleCondition(this.Setting.Deserialize(), this.UnaryOperator);
    }
  }
}
