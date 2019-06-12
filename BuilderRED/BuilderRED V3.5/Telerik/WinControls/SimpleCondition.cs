// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SimpleCondition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class SimpleCondition : Condition
  {
    private IPropertySetting setting;
    private UnaryOperator unaryOperator;

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

    public SimpleCondition(IPropertySetting settingToCheck)
    {
      this.setting = settingToCheck;
    }

    public SimpleCondition(IPropertySetting settingToCheck, UnaryOperator unaryOperator)
    {
      this.setting = settingToCheck;
      this.unaryOperator = unaryOperator;
    }

    public SimpleCondition(RadProperty property, object value, UnaryOperator unaryOperator)
    {
      this.setting = (IPropertySetting) new PropertySetting(property, value);
      this.unaryOperator = unaryOperator;
    }

    public SimpleCondition(RadProperty property, object value)
    {
      this.setting = (IPropertySetting) new PropertySetting(property, value);
    }

    public IPropertySetting Setting
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

    public override bool Evaluate(RadObject target)
    {
      if (target == null)
        return false;
      switch (this.UnaryOperator)
      {
        case UnaryOperator.None:
          return target.GetValue(this.Setting.Property).Equals(this.Setting.GetCurrentValue(target));
        case UnaryOperator.NotOperator:
          return !target.GetValue(this.Setting.Property).Equals(this.Setting.GetCurrentValue(target));
        default:
          return false;
      }
    }

    protected override void FillAffectedProperties(List<RadProperty> inList)
    {
      inList.Add(this.setting.Property);
    }

    public override string ToString()
    {
      if (this.Setting != null)
        return (this.UnaryOperator == UnaryOperator.NotOperator ? "!" : "") + this.Setting.Property.FullName;
      return base.ToString();
    }
  }
}
