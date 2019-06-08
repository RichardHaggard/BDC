// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlSelectorBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls
{
  public abstract class XmlSelectorBase : XmlElementSelector
  {
    private bool autoUnapply = true;
    private XmlCondition condition;
    private XmlCondition unapplyCondition;
    private XmlElementSelector childSelector;

    [DefaultValue(null)]
    public XmlCondition Condition
    {
      get
      {
        return this.condition;
      }
      set
      {
        this.condition = value;
      }
    }

    [DefaultValue(null)]
    public XmlCondition UnapplyCondition
    {
      get
      {
        return this.unapplyCondition;
      }
      set
      {
        this.unapplyCondition = value;
      }
    }

    public bool AutoUnapply
    {
      get
      {
        return this.autoUnapply;
      }
      set
      {
        this.autoUnapply = value;
      }
    }

    public bool ShouldSerializeAutoUnapply()
    {
      return false;
    }

    [DefaultValue(null)]
    public XmlElementSelector ChildSelector
    {
      get
      {
        return this.childSelector;
      }
      set
      {
        this.childSelector = value;
      }
    }

    protected override void DeserializeProperties(IElementSelector selector)
    {
      SelectorBase selectorBase = (SelectorBase) selector;
      if (this.Condition != null)
        selectorBase.Condition = this.Condition.Deserialize();
      if (this.UnapplyCondition != null)
        selectorBase.UnapplyCondition = this.UnapplyCondition.Deserialize();
      if (this.ChildSelector != null)
        selectorBase.ChildSelector = this.ChildSelector.Deserialize();
      selectorBase.AutoUnapply = this.AutoUnapply;
    }
  }
}
