// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SelectorBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls
{
  public abstract class SelectorBase : IElementSelector
  {
    private Condition condition;
    private Condition unapplyCondition;
    private bool isActiveSelectorInStyleBuilder;
    private bool autoUnapply;
    public SelectorBase ExcludeSelector;
    private bool disableStyle;
    private PropertyChangeBehaviorCollection createdPropertyChangeBehaviors;
    private IElementSelector childSelector;

    public Condition Condition
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

    public Condition UnapplyCondition
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

    int IElementSelector.Key
    {
      get
      {
        return this.GetKey();
      }
    }

    protected abstract int GetKey();

    public abstract bool Equals(IElementSelector elementSelector);

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

    public bool IsActiveSelectorInStyleBuilder
    {
      get
      {
        return this.isActiveSelectorInStyleBuilder;
      }
      set
      {
        this.isActiveSelectorInStyleBuilder = value;
      }
    }

    public bool DisableStyle
    {
      get
      {
        return this.disableStyle;
      }
      set
      {
        this.disableStyle = value;
      }
    }

    public bool CanSelectIgnoringConditions(RadObject targetElement)
    {
      return this.CanSelectOverride(targetElement);
    }

    public virtual bool CanSelect(RadObject targetElement)
    {
      if (this.CanSelectOverride(targetElement))
        return this.CanSelectCore(targetElement);
      return false;
    }

    protected virtual bool CanSelectCore(RadObject onElement)
    {
      if (this.DisableStyle)
        return false;
      if (!this.IsActiveSelectorInStyleBuilder && this.Condition != null)
        return this.Condition.Evaluate(onElement);
      return true;
    }

    protected virtual bool CanSelectOverride(RadObject element)
    {
      return false;
    }

    public virtual bool ShouldUnapply(RadObject onElement)
    {
      if (this.AutoUnapply)
      {
        if (this.Condition != null)
          return !this.Condition.Evaluate(onElement);
        return false;
      }
      if (this.UnapplyCondition != null)
        return this.UnapplyCondition.Evaluate(onElement);
      return false;
    }

    public virtual bool HasApplyCondition
    {
      get
      {
        return this.Condition != null;
      }
    }

    public bool IsValueApplied(RadObject element)
    {
      RadElement radElement = element as RadElement;
      if (radElement == null)
        return false;
      object styleSelectorValue = radElement.IsStyleSelectorValueSet[(object) this.GetHashCode()];
      if (styleSelectorValue == null)
        return false;
      return (bool) styleSelectorValue;
    }

    public bool IsValueUnapplied(RadObject element)
    {
      RadElement radElement = element as RadElement;
      if (radElement == null)
        return false;
      object styleSelectorValue = radElement.IsStyleSelectorValueSet[(object) this.GetHashCode()];
      if (styleSelectorValue == null)
        return false;
      return !(bool) styleSelectorValue;
    }

    public void Apply(RadObject element, List<IPropertySetting> propertySettings)
    {
      bool flag1 = true;
      bool flag2 = false;
      bool flag3 = false;
      if (this.condition != null || this.unapplyCondition != null)
      {
        flag2 = this.CanSelect(element);
        flag1 = flag2 && (!this.IsValueApplied(element) || this.IsValueUnapplied(element));
        if (!flag1)
        {
          flag3 = this.ShouldUnapply(element);
          if (this.ShouldUnapply(element))
            this.IsValueApplied(element);
        }
      }
      if (flag1)
      {
        foreach (IPropertySetting propertySetting in propertySettings)
          propertySetting.ApplyValue(element);
      }
      RadElement radElement = element as RadElement;
      if (radElement == null || this.condition == null && this.unapplyCondition == null)
        return;
      if (flag2)
        radElement.IsStyleSelectorValueSet[(object) this.GetHashCode()] = (object) true;
      else if (flag3)
        radElement.IsStyleSelectorValueSet[(object) this.GetHashCode()] = (object) false;
      else
        radElement.IsStyleSelectorValueSet.Remove((object) this.GetHashCode());
    }

    public abstract LinkedList<RadObject> GetSelectedElements(RadObject element);

    public IElementSelector ChildSelector
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

    public virtual PropertyChangeBehaviorCollection GetBehaviors(
      PropertySettingGroup group)
    {
      if (this.createdPropertyChangeBehaviors == null)
      {
        if (this.condition != null)
          throw new NotImplementedException();
        this.createdPropertyChangeBehaviors = new PropertyChangeBehaviorCollection();
      }
      return this.createdPropertyChangeBehaviors;
    }

    void IElementSelector.AddConditionPropertiesToList(List<RadProperty> list)
    {
      if (this.condition == null)
        return;
      list.AddRange((IEnumerable<RadProperty>) this.condition.AffectedProperties);
    }
  }
}
