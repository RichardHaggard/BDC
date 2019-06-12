// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlAnimatedPropertySetting
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing.Design;
using System.Xml.Serialization;
using Telerik.WinControls.Themes.XmlSerialization;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls
{
  public class XmlAnimatedPropertySetting : XmlPropertySetting
  {
    private AnimatorStyles animatorStyle = AnimatorStyles.AnimateAlways;
    private int numFrames = 5;
    private int interval = 50;
    private RadAnimationType animationType = RadAnimationType.ByStartEndValues;
    private RadEasingType applyEasingType = RadEasingType.OutQuad;
    private RadEasingType unApplyEasingType = RadEasingType.OutQuad;
    private LoopType animationLoopType;
    private object endValue;
    private bool startValueIsCurrentValue;

    [XmlAttribute]
    [DefaultValue(LoopType.None)]
    public LoopType AnimationLoopType
    {
      get
      {
        return this.animationLoopType;
      }
      set
      {
        this.animationLoopType = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(AnimatorStyles.AnimateAlways)]
    public AnimatorStyles AnimatorStyle
    {
      get
      {
        return this.animatorStyle;
      }
      set
      {
        this.animatorStyle = value;
      }
    }

    [XmlAttribute]
    public bool StartValueIsCurrentValue
    {
      get
      {
        return this.startValueIsCurrentValue;
      }
      set
      {
        this.startValueIsCurrentValue = value;
      }
    }

    private bool ShouldSerializeStartValueIsCurrentValue()
    {
      if (this.startValueIsCurrentValue && this.Value != null)
        return true;
      if (this.Value == null)
        return !this.startValueIsCurrentValue;
      return false;
    }

    [DefaultValue(RadEasingType.OutQuad)]
    public RadEasingType ApplyEasingType
    {
      get
      {
        return this.applyEasingType;
      }
      set
      {
        this.applyEasingType = value;
      }
    }

    [DefaultValue(RadEasingType.OutQuad)]
    public RadEasingType UnapplyEasingType
    {
      get
      {
        return this.unApplyEasingType;
      }
      set
      {
        this.unApplyEasingType = value;
      }
    }

    [Editor(typeof (SettingValueEditor), typeof (UITypeEditor))]
    [SerializationConverter(typeof (XmlPropertySettingValueConverter))]
    [TypeConverter(typeof (SettingValueConverter))]
    public object EndValue
    {
      get
      {
        return this.endValue;
      }
      set
      {
        this.endValue = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(50)]
    public int Interval
    {
      get
      {
        return this.interval;
      }
      set
      {
        this.interval = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(5)]
    public int NumFrames
    {
      get
      {
        return this.numFrames;
      }
      set
      {
        this.numFrames = value;
      }
    }

    [XmlAttribute]
    [DefaultValue(RadAnimationType.ByStartEndValues)]
    public RadAnimationType AnimationType
    {
      get
      {
        return this.animationType;
      }
      set
      {
        this.animationType = value;
      }
    }

    public override IPropertySetting Deserialize()
    {
      AnimatedPropertySetting animatedPropertySetting = new AnimatedPropertySetting();
      animatedPropertySetting.Property = XmlPropertySetting.DeserializeProperty(this.Property);
      if (!this.StartValueIsCurrentValue)
        animatedPropertySetting.StartValue = this.GetConvertedValue(animatedPropertySetting.Property, this.Value);
      animatedPropertySetting.Interval = this.Interval;
      animatedPropertySetting.NumFrames = this.NumFrames;
      animatedPropertySetting.ApplyEasingType = this.ApplyEasingType;
      animatedPropertySetting.EndValue = this.GetConvertedValue(animatedPropertySetting.Property, this.EndValue);
      return (IPropertySetting) animatedPropertySetting;
    }

    public override string ToString()
    {
      return this.GetType().Name;
    }
  }
}
