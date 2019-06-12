// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlPropertySettingGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class XmlPropertySettingGroup
  {
    private XmlPropertySettingCollection propertySettings;
    private XmlSelectorCollection selectors;
    private string basedOn;

    public XmlPropertySettingGroup()
    {
    }

    public XmlPropertySettingGroup(PropertySettingGroup group)
    {
      foreach (PropertySetting propertySetting in group.PropertySettings)
      {
        string fullName;
        if (propertySetting.Property != null)
          fullName = propertySetting.Property.FullName;
        else if (!string.IsNullOrEmpty(propertySetting.FullName))
          fullName = propertySetting.FullName;
        else
          continue;
        if (propertySetting.EndValue != null)
        {
          XmlAnimatedPropertySetting animatedPropertySetting = new XmlAnimatedPropertySetting();
          animatedPropertySetting.Property = fullName;
          animatedPropertySetting.Value = propertySetting.Value;
          animatedPropertySetting.EndValue = propertySetting.EndValue;
          this.PropertySettings.Add((XmlPropertySetting) animatedPropertySetting);
        }
        else
          this.PropertySettings.Add(new XmlPropertySetting()
          {
            Property = fullName,
            Value = propertySetting.Value
          });
      }
      if (group.Selector != null)
        this.Selectors.Add(this.CreateSelector(group.Selector));
      if (!string.IsNullOrEmpty(group.BasedOn))
        this.basedOn = group.BasedOn;
      else
        this.basedOn = (string) null;
    }

    private XmlElementSelector CreateSelector(ElementSelector selector)
    {
      XmlSelectorBase xmlSelectorBase = (XmlSelectorBase) null;
      WrapSelector wrapSelector = selector as WrapSelector;
      if (wrapSelector != null)
      {
        TypeSelector internalSelector1 = wrapSelector.InternalSelector as TypeSelector;
        if (internalSelector1 != null)
          return (XmlElementSelector) new XmlTypeSelector(XmlTheme.SerializeType(internalSelector1.ElementType));
        ClassSelector internalSelector2 = wrapSelector.InternalSelector as ClassSelector;
        if (internalSelector2 != null)
          return (XmlElementSelector) new XmlClassSelector(internalSelector2.ElementClass);
      }
      if (selector.Type == ElementSelectorTypes.VisualStateSelector)
        xmlSelectorBase = (XmlSelectorBase) new XmlVisualStateSelector(selector.Value);
      else if (selector.Type == ElementSelectorTypes.TypeSelector)
        xmlSelectorBase = (XmlSelectorBase) new XmlTypeSelector(selector.Value);
      else if (selector.Type == ElementSelectorTypes.ClassSelector)
        xmlSelectorBase = (XmlSelectorBase) new XmlClassSelector(selector.Value);
      if (selector.ChildSelector != null)
        xmlSelectorBase.ChildSelector = this.CreateSelector(selector.ChildSelector);
      return (XmlElementSelector) xmlSelectorBase;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public XmlPropertySettingCollection PropertySettings
    {
      get
      {
        if (this.propertySettings == null)
          this.propertySettings = new XmlPropertySettingCollection();
        return this.propertySettings;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public XmlSelectorCollection Selectors
    {
      get
      {
        if (this.selectors == null)
          this.selectors = new XmlSelectorCollection(1);
        return this.selectors;
      }
    }

    public override string ToString()
    {
      return "PropertySettingGroup";
    }

    public string GroupName
    {
      get
      {
        if (this.Selectors == null || this.Selectors.Count == 0)
          return "Initial State";
        if (!(this.Selectors[0] is XmlSelectorBase))
          return "Unknown State";
        XmlCondition condition = ((XmlSelectorBase) this.Selectors[0]).Condition;
        if (condition != null)
          return condition.BuildExpressionString();
        return "Initial State";
      }
    }

    [DefaultValue("")]
    public string BasedOn
    {
      get
      {
        return this.basedOn;
      }
      set
      {
        this.basedOn = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldSerializeBasedOn()
    {
      return !string.IsNullOrEmpty(this.basedOn);
    }

    public PropertySettingGroup Deserialize()
    {
      PropertySettingGroup propertySettingGroup = new PropertySettingGroup();
      propertySettingGroup.BasedOn = this.basedOn;
      if (this.propertySettings != null)
      {
        for (int index = 0; index < this.PropertySettings.Count; ++index)
        {
          IPropertySetting propertySetting = this.PropertySettings[index].Deserialize();
          if (propertySetting is PropertySetting)
            propertySettingGroup.PropertySettings.Add(propertySetting as PropertySetting);
        }
      }
      if (this.selectors != null)
      {
        for (int index = 0; index < this.Selectors.Count; ++index)
        {
          IElementSelector internalSelector = this.Selectors[index].Deserialize();
          propertySettingGroup.Selector = (ElementSelector) new WrapSelector(internalSelector);
        }
      }
      return propertySettingGroup;
    }
  }
}
