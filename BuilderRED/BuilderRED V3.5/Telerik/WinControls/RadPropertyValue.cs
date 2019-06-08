// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadPropertyValue
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls
{
  public class RadPropertyValue
  {
    private object localValue = RadProperty.UnsetValue;
    private object localValueFromBinding = RadProperty.UnsetValue;
    private object inheritedValue = RadProperty.UnsetValue;
    private object defaultValueOverride = RadProperty.UnsetValue;
    private object animatedValue = RadProperty.UnsetValue;
    private object currentValue = RadProperty.UnsetValue;
    private byte lockComposeCount;
    private byte lockValueUpdateCount;
    private RadProperty property;
    private RadObject owner;
    private RadPropertyMetadata metadata;
    private AnimatedPropertySetting animationSetting;
    private IPropertySetting styleSetting;
    private PropertyBinding propertyBinding;
    private ValueSource valueSource;
    private bool iscurrentValueCoerced;
    private bool isInheritedValueValid;
    private bool isSetAtDesignTime;
    private List<PropertyBoundObject> boundObjects;
    private int styleVersion;

    internal RadPropertyValue(RadObject owner, RadProperty property)
    {
      this.owner = owner;
      this.property = property;
      bool found;
      this.metadata = property.GetMetadata(this.owner.RadObjectType, out found);
      if (!found)
      {
        RadObjectType radObjectType = RadObjectType.FromSystemType(property.OwnerType);
        this.metadata = property.GetMetadata(radObjectType);
      }
      this.valueSource = ValueSource.Unknown;
    }

    internal RadPropertyValue(RadPropertyValue source)
    {
      this.Copy(source);
      ++this.lockComposeCount;
      ++this.lockValueUpdateCount;
    }

    internal void Dispose()
    {
      ++this.lockComposeCount;
      ++this.lockValueUpdateCount;
      if (this.owner != null)
        this.owner.RemoveBinding(this);
      if (this.boundObjects != null)
      {
        for (int index = this.boundObjects.Count - 1; index >= 0; --index)
        {
          PropertyBoundObject boundObject = this.boundObjects[index];
          RadObject radObject = boundObject.Object;
          if (radObject != null && !radObject.IsDisposed && !radObject.IsDisposing)
          {
            int num = (int) radObject.UnbindProperty(boundObject.Property);
          }
        }
        this.boundObjects.Clear();
      }
      this.property = (RadProperty) null;
      this.metadata = (RadPropertyMetadata) null;
      this.owner = (RadObject) null;
      this.propertyBinding = (PropertyBinding) null;
      this.animationSetting = (AnimatedPropertySetting) null;
      this.styleSetting = (IPropertySetting) null;
    }

    internal void Copy(RadPropertyValue source)
    {
      this.valueSource = source.valueSource;
      this.localValue = source.localValue;
      this.localValueFromBinding = source.localValueFromBinding;
      this.defaultValueOverride = source.defaultValueOverride;
      this.currentValue = source.currentValue;
      this.iscurrentValueCoerced = source.iscurrentValueCoerced;
      this.isSetAtDesignTime = source.isSetAtDesignTime;
      this.animationSetting = source.animationSetting;
      this.styleSetting = source.styleSetting;
      this.propertyBinding = source.propertyBinding;
      this.animatedValue = source.animatedValue;
    }

    internal void AddBoundObject(PropertyBoundObject relation)
    {
      if (this.boundObjects == null)
        this.boundObjects = new List<PropertyBoundObject>();
      if (this.FindBoundObjectIndex(relation.Object) >= 0)
        return;
      this.boundObjects.Add(relation);
    }

    public object GetCurrentValue(bool composeIfNeeded)
    {
      if (composeIfNeeded && this.valueSource == ValueSource.Unknown)
        this.ComposeCurrentValue();
      else if (this.currentValue == RadProperty.UnsetValue)
        this.ComposeCurrentValue();
      return this.currentValue;
    }

    internal void RemoveBoundObject(RadObject boundObject)
    {
      int boundObjectIndex = this.FindBoundObjectIndex(boundObject);
      if (boundObjectIndex < 0)
        return;
      this.boundObjects.RemoveAt(boundObjectIndex);
    }

    internal void NotifyBoundObjects()
    {
      if (this.boundObjects == null)
        return;
      for (int index = this.boundObjects.Count - 1; index >= 0; --index)
      {
        PropertyBoundObject boundObject = this.boundObjects[index];
        RadObject radObject = boundObject.Object;
        if (radObject != null)
          radObject.OnBoundSourcePropertyChanged(boundObject.Property);
        else
          this.boundObjects.RemoveAt(index);
      }
    }

    public void ComposeCurrentValue()
    {
      if (this.lockComposeCount > (byte) 0)
        return;
      ValueSource source = ValueSource.Unknown;
      object obj1 = RadProperty.UnsetValue;
      object obj2 = this.animationSetting == null ? this.animatedValue : this.animationSetting.GetCurrentValue(this.owner);
      if (obj2 != RadProperty.UnsetValue)
      {
        obj1 = obj2;
        source = ValueSource.Animation;
      }
      else if (this.localValueFromBinding != RadProperty.UnsetValue)
      {
        obj1 = this.localValueFromBinding;
        source = ValueSource.LocalFromBinding;
      }
      else if (this.propertyBinding != null)
      {
        obj1 = this.propertyBinding.GetValue();
        source = ValueSource.PropertyBinding;
      }
      else if (this.localValue != RadProperty.UnsetValue)
      {
        obj1 = this.localValue;
        source = ValueSource.Local;
      }
      else if (this.styleSetting != null)
      {
        obj1 = this.styleSetting.GetCurrentValue(this.owner);
        source = ValueSource.Style;
      }
      else if (this.defaultValueOverride != RadProperty.UnsetValue)
      {
        obj1 = this.defaultValueOverride;
        source = ValueSource.DefaultValueOverride;
      }
      if (!this.IsValueValid(obj1))
        this.SetInheritedOrDefaultValue(ref obj1, ref source);
      this.SetCurrentValue(obj1, source);
    }

    internal void SetInheritedOrDefaultValue(ref object value, ref ValueSource source)
    {
      if (this.metadata.IsInherited)
      {
        if (!this.isInheritedValueValid)
        {
          this.inheritedValue = this.owner.GetInheritedValue(this.property);
          this.isInheritedValueValid = true;
        }
        value = this.inheritedValue;
        source = ValueSource.Inherited;
      }
      if (this.IsValueValid(value))
        return;
      value = this.GetDefaultValue();
      source = ValueSource.DefaultValue;
    }

    internal bool IsValueValid(object value)
    {
      return value != RadProperty.UnsetValue && (value != null || !this.property.PropertyType.IsValueType);
    }

    public bool InvalidateInheritedValue()
    {
      this.isInheritedValueValid = false;
      this.inheritedValue = RadProperty.UnsetValue;
      bool flag = false;
      if (this.valueSource == ValueSource.DefaultValue || this.valueSource == ValueSource.Inherited)
      {
        this.valueSource = ValueSource.Unknown;
        flag = true;
      }
      return flag;
    }

    internal void SetLocalValue(object value)
    {
      if (this.propertyBinding != null && (this.propertyBinding.BindingOptions & PropertyBindingOptions.TwoWay) == PropertyBindingOptions.TwoWay)
        this.propertyBinding.UpdateSourceProperty(value);
      this.localValueFromBinding = RadProperty.UnsetValue;
      this.localValue = value;
      this.valueSource = ValueSource.Unknown;
    }

    internal void SetLocalValueFromBinding(object value)
    {
      if (this.propertyBinding != null && (this.propertyBinding.BindingOptions & PropertyBindingOptions.TwoWay) == PropertyBindingOptions.TwoWay)
        this.propertyBinding.UpdateSourceProperty(value);
      this.localValueFromBinding = value;
      this.valueSource = ValueSource.Unknown;
    }

    internal void SetAnimation(AnimatedPropertySetting animation)
    {
      if (animation == null)
        this.animatedValue = this.animationSetting == null || this.animationSetting.RemoveAfterApply ? RadProperty.UnsetValue : this.animationSetting.GetCurrentValue(this.owner);
      this.animationSetting = animation;
      this.valueSource = ValueSource.Unknown;
    }

    internal void SetStyle(IPropertySetting setting)
    {
      this.styleSetting = setting;
      this.valueSource = ValueSource.Unknown;
    }

    internal void SetBinding(PropertyBinding binding)
    {
      if (binding == null && this.propertyBinding != null && (this.propertyBinding.BindingOptions & PropertyBindingOptions.PreserveAsLocalValue) == PropertyBindingOptions.PreserveAsLocalValue)
        this.localValue = this.propertyBinding.GetValue();
      this.propertyBinding = binding;
      this.valueSource = ValueSource.Unknown;
    }

    internal bool IsObjectBound(RadObject target)
    {
      return this.FindBoundObjectIndex(target) != -1;
    }

    internal void BeginUpdate(bool lockCompose, bool lockValueUpdate)
    {
      if (lockCompose)
        ++this.lockComposeCount;
      if (!lockValueUpdate)
        return;
      ++this.lockValueUpdateCount;
    }

    internal void EndUpdate(bool unlockCompose, bool unlockValue)
    {
      if (unlockCompose && this.lockComposeCount > (byte) 0)
        --this.lockComposeCount;
      if (!unlockValue || this.lockValueUpdateCount <= (byte) 0)
        return;
      --this.lockValueUpdateCount;
    }

    internal void SetDefaultValueOverride(object value)
    {
      this.defaultValueOverride = value;
      this.valueSource = ValueSource.Unknown;
    }

    private int FindBoundObjectIndex(RadObject boundObject)
    {
      if (this.boundObjects == null)
        return -1;
      for (int index = this.boundObjects.Count - 1; index >= 0; --index)
      {
        RadObject radObject = this.boundObjects[index].Object;
        if (radObject == null)
          this.boundObjects.RemoveAt(index);
        else if (radObject == boundObject)
          return index;
      }
      return -1;
    }

    private void SetCurrentValue(object value, ValueSource source)
    {
      this.iscurrentValueCoerced = false;
      object obj = this.owner.CallCoerceValue(this, value);
      if (obj != RadProperty.UnsetValue)
      {
        value = obj;
        this.iscurrentValueCoerced = true;
      }
      this.currentValue = value;
      this.valueSource = source;
    }

    private object GetDefaultValue()
    {
      object baseDefaultValue = this.metadata.DefaultValue;
      object defaultValue = this.owner.GetDefaultValue(this, baseDefaultValue);
      if (defaultValue != RadProperty.UnsetValue)
        baseDefaultValue = defaultValue;
      return baseDefaultValue;
    }

    public bool HasBoundObjects
    {
      get
      {
        if (this.boundObjects != null)
          return this.boundObjects.Count > 0;
        return false;
      }
    }

    public bool IsCompositionLocked
    {
      get
      {
        return this.lockComposeCount > (byte) 0;
      }
    }

    public bool IsUpdatingValue
    {
      get
      {
        return this.lockValueUpdateCount > (byte) 0;
      }
    }

    public RadProperty Property
    {
      get
      {
        return this.property;
      }
    }

    public object CurrentValue
    {
      get
      {
        return this.currentValue;
      }
    }

    public object LocalValue
    {
      get
      {
        return this.localValue;
      }
    }

    public object BindingLocalValue
    {
      get
      {
        return this.localValueFromBinding;
      }
    }

    public PropertyBinding PropertyBinding
    {
      get
      {
        return this.propertyBinding;
      }
    }

    public AnimatedPropertySetting AnimationSetting
    {
      get
      {
        return this.animationSetting;
      }
    }

    public IPropertySetting StyleSetting
    {
      get
      {
        return this.styleSetting;
      }
    }

    public object AnimatedValue
    {
      get
      {
        return this.animatedValue;
      }
    }

    public ValueSource ValueSource
    {
      get
      {
        return this.valueSource;
      }
    }

    public RadPropertyMetadata Metadata
    {
      get
      {
        return this.metadata;
      }
    }

    public bool IsCurrentValueCoerced
    {
      get
      {
        return this.iscurrentValueCoerced;
      }
    }

    public object DefaultValueOverride
    {
      get
      {
        return this.defaultValueOverride;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsSetAtDesignTime
    {
      get
      {
        return this.isSetAtDesignTime;
      }
      internal set
      {
        this.isSetAtDesignTime = value;
      }
    }

    public int StyleVersion
    {
      get
      {
        return this.styleVersion;
      }
      internal set
      {
        this.styleVersion = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetStyleVersion(int newVersion)
    {
      this.styleVersion = newVersion;
    }
  }
}
