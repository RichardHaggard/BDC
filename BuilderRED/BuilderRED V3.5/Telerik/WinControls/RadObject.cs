// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls
{
  public class RadObject : DisposableObject, INotifyPropertyChanged, ICustomTypeDescriptor
  {
    public static RadProperty BindingContextProperty = RadProperty.Register(nameof (BindingContext), typeof (BindingContext), typeof (RadObject), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null, ElementPropertyOptions.CanInheritValue));
    public static readonly RadObjectType RadType = RadObjectType.FromSystemType(typeof (RadObject));
    private static readonly object PropertyChangedEventKey = new object();
    private static readonly object RadPropertyChangedEventKey = new object();
    private static readonly object RadPropertyChangingEventKey = new object();
    internal const long IsDesignModeStateKey = 4;
    internal const long RadObjectLastStateKey = 4;
    private byte suspendPropertyNotifications;
    internal RadPropertyValueCollection propertyValues;
    private RadObjectType radType;
    private Filter propertyFilter;
    private HybridDictionary valuesAnimators;

    public RadObject()
    {
      this.propertyValues = new RadPropertyValueCollection(this);
      this.radType = RadObjectType.FromSystemType(this.GetType());
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public event PropertyChangedEventHandler PropertyChanged
    {
      add
      {
        this.Events.AddHandler(RadObject.PropertyChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadObject.PropertyChangedEventKey, (Delegate) value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event RadPropertyChangedEventHandler RadPropertyChanged
    {
      add
      {
        this.Events.AddHandler(RadObject.RadPropertyChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadObject.RadPropertyChangedEventKey, (Delegate) value);
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event RadPropertyChangingEventHandler RadPropertyChanging
    {
      add
      {
        this.Events.AddHandler(RadObject.RadPropertyChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadObject.RadPropertyChangingEventKey, (Delegate) value);
      }
    }

    AttributeCollection ICustomTypeDescriptor.GetAttributes()
    {
      return TypeDescriptor.GetAttributes((object) this, true);
    }

    string ICustomTypeDescriptor.GetClassName()
    {
      return (string) null;
    }

    string ICustomTypeDescriptor.GetComponentName()
    {
      return (string) null;
    }

    TypeConverter ICustomTypeDescriptor.GetConverter()
    {
      return TypeDescriptor.GetConverter((object) this, true);
    }

    EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
    {
      return TypeDescriptor.GetDefaultEvent((object) this, true);
    }

    PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
    {
      return TypeDescriptor.GetDefaultProperty((object) this, true);
    }

    object ICustomTypeDescriptor.GetEditor(System.Type editorBaseType)
    {
      return TypeDescriptor.GetEditor((object) this, editorBaseType, true);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
    {
      return TypeDescriptor.GetEvents((object) this, true);
    }

    EventDescriptorCollection ICustomTypeDescriptor.GetEvents(
      Attribute[] attributes)
    {
      return TypeDescriptor.GetEvents((object) this, attributes, true);
    }

    internal virtual PropertyDescriptorCollection ReplaceDefaultDescriptors(
      PropertyDescriptorCollection props)
    {
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>(props.Count);
      for (int index = 0; index < props.Count; ++index)
      {
        PropertyDescriptor prop = props[index];
        Attribute[] attributes = new Attribute[prop.Attributes.Count];
        prop.Attributes.CopyTo((Array) attributes, 0);
        PropertyDescriptor propertyDescriptor = RadTypeResolver.Instance.GetRegisteredRadProperty(this.GetType(), prop.Name) == null ? (PropertyDescriptor) new RadObjectCustomPropertyDescriptor(prop, attributes) : (PropertyDescriptor) new RadObjectCustomRadPropertyDescriptor(prop, attributes);
        if (this.propertyFilter == null || this.propertyFilter.Match((object) prop))
          propertyDescriptorList.Add(propertyDescriptor);
      }
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
    {
      return this.ReplaceDefaultDescriptors(TypeDescriptor.GetProperties((object) this, true));
    }

    PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
      Attribute[] attributes)
    {
      return this.ReplaceDefaultDescriptors(TypeDescriptor.GetProperties((object) this, attributes, true));
    }

    object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
    {
      return (object) this;
    }

    protected override void DisposeManagedResources()
    {
      this.ClearPropertyStore();
      base.DisposeManagedResources();
      if (this.valuesAnimators == null)
        return;
      foreach (ElementValuesAnimator elementValuesAnimator in (IEnumerable) this.valuesAnimators.Values)
        elementValuesAnimator.Dispose();
      this.valuesAnimators.Clear();
      this.valuesAnimators = (HybridDictionary) null;
    }

    protected virtual void ClearPropertyStore()
    {
      this.propertyValues.Reset();
    }

    public void SuspendPropertyNotifications()
    {
      ++this.suspendPropertyNotifications;
    }

    public void ResumePropertyNotifications()
    {
      if (this.suspendPropertyNotifications <= (byte) 0)
        return;
      --this.suspendPropertyNotifications;
    }

    public RadPropertyValue GetPropertyValue(RadProperty property)
    {
      return this.propertyValues.GetEntry(property, false);
    }

    public ValueUpdateResult SetDefaultValueOverride(
      RadProperty property,
      object value)
    {
      return this.SetValueCore(this.propertyValues.GetEntry(property, true), (object) null, value, ValueSource.DefaultValueOverride);
    }

    internal void SetPropertyValueSetAtDesignTime(RadProperty property)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(property, false);
      if (entry == null)
        return;
      entry.IsSetAtDesignTime = true;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SetValueAtDesignTime(RadProperty property, object value)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(property, true);
      entry.IsSetAtDesignTime = true;
      int num = (int) this.SetValueCore(entry, (object) null, value, ValueSource.Local);
    }

    public virtual object GetValue(RadProperty property)
    {
      return this.propertyValues.GetEntry(property, true).GetCurrentValue(true);
    }

    public ValueUpdateResult SetValue(RadProperty property, object value)
    {
      return this.SetValueCore(this.propertyValues.GetEntry(property, true), (object) null, value, ValueSource.Local);
    }

    public ValueUpdateResult ResetValue(RadProperty property)
    {
      return this.ResetValue(property, ValueResetFlags.All);
    }

    public ValueUpdateResult ResetValue(
      RadProperty property,
      ValueResetFlags flags)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(property, false);
      if (entry != null)
        return this.ResetValueCore(entry, flags);
      return ValueUpdateResult.NotUpdated;
    }

    public ValueUpdateResult UpdateValue(RadProperty property)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(property, false);
      if (entry == null)
        return ValueUpdateResult.NotUpdated;
      return this.UpdateValueCore(entry);
    }

    public ValueSource GetValueSource(RadProperty property)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(property, true);
      if (entry.ValueSource == ValueSource.Unknown)
        entry.ComposeCurrentValue();
      return entry.ValueSource;
    }

    public RadProperty GetRegisteredRadProperty(string propertyName)
    {
      return RadTypeResolver.Instance.GetRegisteredRadProperty(this.GetType(), propertyName);
    }

    protected internal virtual ValueUpdateResult UpdateValueCore(
      RadPropertyValue propVal)
    {
      object currentValue = propVal.GetCurrentValue(false);
      ValueSource valueSource = propVal.ValueSource;
      propVal.ComposeCurrentValue();
      return this.RaisePropertyNotifications(propVal, currentValue, propVal.GetCurrentValue(false), valueSource);
    }

    protected virtual ValueUpdateResult SetValueCore(
      RadPropertyValue propVal,
      object propModifier,
      object newValue,
      ValueSource source)
    {
      if (this.GetBitState(1L) || this.GetBitState(2L))
        return ValueUpdateResult.Canceled;
      object currentValue = propVal.GetCurrentValue(false);
      ValueSource valueSource = propVal.ValueSource;
      RadPropertyValue source1 = (RadPropertyValue) null;
      bool isUpdatingValue = propVal.IsUpdatingValue;
      if (!propVal.IsCompositionLocked && this.IsPropertyCancelable(propVal.Metadata))
        source1 = new RadPropertyValue(propVal);
      propVal.BeginUpdate(true, true);
      switch (source)
      {
        case ValueSource.DefaultValue:
        case ValueSource.DefaultValueOverride:
          propVal.SetDefaultValueOverride(newValue);
          break;
        case ValueSource.Inherited:
          propVal.InvalidateInheritedValue();
          break;
        case ValueSource.Style:
          if (!isUpdatingValue)
            this.RemoveAnimation(propVal);
          propVal.SetStyle((IPropertySetting) propModifier);
          break;
        case ValueSource.Local:
          propVal.SetLocalValue(newValue);
          break;
        case ValueSource.PropertyBinding:
          if (!isUpdatingValue)
            this.RemoveBinding(propVal);
          propVal.SetBinding((PropertyBinding) propModifier);
          break;
        case ValueSource.LocalFromBinding:
          propVal.SetLocalValueFromBinding(newValue);
          break;
        case ValueSource.Animation:
          if (!isUpdatingValue)
            this.RemoveAnimation(propVal);
          propVal.SetAnimation((AnimatedPropertySetting) propModifier);
          break;
      }
      propVal.EndUpdate(true, true);
      if (propVal.IsCompositionLocked)
        return ValueUpdateResult.Updating;
      ValueUpdateResult valueUpdateResult = this.RaisePropertyNotifications(propVal, currentValue, propVal.GetCurrentValue(true), valueSource);
      if (valueUpdateResult == ValueUpdateResult.Canceled && source1 != null)
        propVal.Copy(source1);
      source1?.Dispose();
      return valueUpdateResult;
    }

    protected internal virtual ValueUpdateResult ResetValueCore(
      RadPropertyValue propVal,
      ValueResetFlags flags)
    {
      if (flags == ValueResetFlags.None)
        return ValueUpdateResult.NotUpdated;
      object currentValue = propVal.GetCurrentValue(false);
      ValueSource valueSource = propVal.ValueSource;
      RadPropertyValue source = (RadPropertyValue) null;
      if (!propVal.IsCompositionLocked)
        source = new RadPropertyValue(propVal);
      propVal.BeginUpdate(true, false);
      if ((flags & ValueResetFlags.Animation) == ValueResetFlags.Animation)
      {
        int num1 = (int) this.SetValueCore(propVal, (object) null, (object) null, ValueSource.Animation);
      }
      if ((flags & ValueResetFlags.Local) == ValueResetFlags.Local)
      {
        int num2 = (int) this.SetValueCore(propVal, (object) null, RadProperty.UnsetValue, ValueSource.Local);
      }
      if ((flags & ValueResetFlags.DefaultValueOverride) == ValueResetFlags.DefaultValueOverride)
      {
        int num3 = (int) this.SetValueCore(propVal, (object) null, RadProperty.UnsetValue, ValueSource.DefaultValue);
      }
      if ((flags & ValueResetFlags.Binding) == ValueResetFlags.Binding)
      {
        int num4 = (int) this.SetValueCore(propVal, (object) null, (object) null, ValueSource.PropertyBinding);
      }
      if ((flags & ValueResetFlags.TwoWayBindingLocal) == ValueResetFlags.TwoWayBindingLocal)
      {
        int num5 = (int) this.SetValueCore(propVal, (object) null, RadProperty.UnsetValue, ValueSource.LocalFromBinding);
      }
      if ((flags & ValueResetFlags.Style) == ValueResetFlags.Style)
      {
        int num6 = (int) this.SetValueCore(propVal, (object) null, (object) null, ValueSource.Style);
      }
      if ((flags & ValueResetFlags.Inherited) == ValueResetFlags.Inherited)
      {
        int num7 = (int) this.SetValueCore(propVal, (object) null, RadProperty.UnsetValue, ValueSource.Inherited);
      }
      propVal.EndUpdate(true, false);
      if (propVal.IsCompositionLocked)
        return ValueUpdateResult.Updating;
      ValueUpdateResult valueUpdateResult = this.RaisePropertyNotifications(propVal, currentValue, propVal.GetCurrentValue(true), valueSource);
      if (valueUpdateResult == ValueUpdateResult.Canceled)
        propVal.Copy(source);
      source?.Dispose();
      return valueUpdateResult;
    }

    protected internal virtual object GetDefaultValue(
      RadPropertyValue propVal,
      object baseDefaultValue)
    {
      return RadProperty.UnsetValue;
    }

    internal object CallCoerceValue(RadPropertyValue propVal, object baseValue)
    {
      return this.CoerceValue(propVal, baseValue);
    }

    protected virtual object CoerceValue(RadPropertyValue propVal, object baseValue)
    {
      if (propVal.Metadata != null && propVal.Metadata.CoerceValueCallback != null)
        return propVal.Metadata.CoerceValueCallback(this, baseValue);
      return RadProperty.UnsetValue;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool? ShouldSerializeProperty(PropertyDescriptor property)
    {
      return new bool?();
    }

    internal void EnsurePropertySet(RadPropertyValue propVal, object value)
    {
      if (propVal.Metadata.ReadOnly)
        throw new ArgumentException("Attemt to modify the value of a read-only property");
      if (value != null && value != RadProperty.UnsetValue && !RadProperty.IsValidType(value, propVal.Property.PropertyType))
        throw new ArgumentException("New value does not match declared property type.");
      if (propVal.Property.ValidateValueCallback != null && !propVal.Property.ValidateValueCallback(value, this))
        throw new ArgumentException("Specified value " + value.ToString() + " is not valid for property " + propVal.Property.Name);
    }

    internal ValueUpdateResult RaisePropertyNotifications(
      RadPropertyValue propVal,
      object oldValue,
      object newValue,
      ValueSource oldSource)
    {
      if (!this.CanRaisePropertyChangeNotifications(propVal))
        return ValueUpdateResult.NotUpdated;
      if (propVal.IsCompositionLocked)
        return ValueUpdateResult.Updating;
      if (object.Equals(oldValue, newValue))
        return ValueUpdateResult.UpdatedNotChanged;
      RadPropertyChangingEventArgs args = new RadPropertyChangingEventArgs(propVal.Property, oldValue, newValue, propVal.Metadata);
      this.OnPropertyChanging(args);
      ValueUpdateResult valueUpdateResult;
      if (args.Cancel)
      {
        valueUpdateResult = ValueUpdateResult.Canceled;
      }
      else
      {
        RadPropertyChangedEventArgs e = new RadPropertyChangedEventArgs(propVal.Property, propVal.Metadata, oldValue, newValue, false, false, oldSource, propVal.ValueSource);
        this.OnPropertyChanged(e);
        propVal.NotifyBoundObjects();
        if (propVal.Metadata != null && propVal.Metadata.PropertyChangedCallback != null)
          propVal.Metadata.PropertyChangedCallback(this, e);
        valueUpdateResult = ValueUpdateResult.UpdatedChanged;
      }
      return valueUpdateResult;
    }

    public RadPropertyValueCollection PropertyValues
    {
      get
      {
        return this.propertyValues;
      }
    }

    protected virtual bool CanRaisePropertyChangeNotifications(RadPropertyValue propVal)
    {
      return this.suspendPropertyNotifications <= (byte) 0 && !this.GetBitState(1L) && !this.GetBitState(2L);
    }

    internal void RemoveAnimation(RadPropertyValue propVal)
    {
      this.GetCurrentAnimation(propVal)?.Stop(this);
    }

    internal AnimatedPropertySetting GetCurrentAnimation(RadProperty property)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(property, false);
      if (entry == null)
        return (AnimatedPropertySetting) null;
      return this.GetCurrentAnimation(entry);
    }

    internal AnimatedPropertySetting GetCurrentAnimation(
      RadPropertyValue propVal)
    {
      if (propVal.AnimationSetting != null)
        return propVal.AnimationSetting;
      return propVal.StyleSetting as AnimatedPropertySetting;
    }

    internal ValueUpdateResult OnAnimatedPropertyValueChanged(
      AnimatedPropertySetting setting)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(setting.Property, false);
      if (entry == null)
        return ValueUpdateResult.NotUpdated;
      return this.UpdateValueCore(entry);
    }

    internal ValueUpdateResult OnAnimationStarted(AnimatedPropertySetting setting)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(setting.Property, true);
      if (entry == null)
        return ValueUpdateResult.NotUpdated;
      ValueSource source = setting.IsStyleSetting ? ValueSource.Style : ValueSource.Animation;
      return this.SetValueCore(entry, (object) setting, (object) null, source);
    }

    internal ValueUpdateResult OnAnimationFinished(AnimatedPropertySetting setting)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(setting.Property, false);
      if (entry == null)
        return ValueUpdateResult.NotUpdated;
      object propModifier;
      ValueSource source;
      if (setting.IsStyleSetting)
      {
        propModifier = (object) setting;
        source = ValueSource.Style;
      }
      else
      {
        propModifier = (object) null;
        source = ValueSource.Animation;
      }
      if (setting.RemoveAfterApply && setting.IsStyleSetting)
      {
        IStylableNode stylableNode = this as IStylableNode;
        if (stylableNode != null && stylableNode.Style != null)
        {
          foreach (PropertySettingGroup propertySettingGroup in stylableNode.Style.PropertySettingGroups)
          {
            foreach (PropertySetting propertySetting in propertySettingGroup.PropertySettings)
            {
              if (propertySetting.AnimatedSetting == setting)
              {
                propertySettingGroup.PropertySettings.Remove(propertySetting);
                break;
              }
            }
          }
        }
        PropertySetting propertySetting1 = new PropertySetting(setting.Property, setting.EndValue);
        return this.SetValueCore(entry, (object) propertySetting1, propertySetting1.Value, source);
      }
      if (!setting.RemoveAfterApply)
        return ValueUpdateResult.UpdatedNotChanged;
      return this.SetValueCore(entry, propModifier, (object) null, source);
    }

    public ValueUpdateResult BindProperty(
      RadProperty propertyToBind,
      RadObject sourceObject,
      RadProperty sourceProperty,
      PropertyBindingOptions options)
    {
      if (sourceObject == null)
        throw new ArgumentNullException("Binding source object");
      if (sourceObject.IsDisposing || sourceObject.IsDisposed)
        return ValueUpdateResult.NotUpdated;
      RadPropertyValue entry = this.propertyValues.GetEntry(propertyToBind, true);
      if (entry.PropertyBinding != null)
      {
        entry.BeginUpdate(true, false);
        int num = (int) this.ResetValueCore(entry, ValueResetFlags.Binding);
        entry.EndUpdate(true, false);
      }
      PropertyBinding binding = new PropertyBinding(sourceObject, propertyToBind, sourceProperty, options);
      ValueUpdateResult valueUpdateResult = this.SetValueCore(entry, (object) binding, (object) null, ValueSource.PropertyBinding);
      if ((options & PropertyBindingOptions.NoChangeNotify) == (PropertyBindingOptions) 0)
        sourceObject.OnPropertyBoundExternally(binding, this);
      return valueUpdateResult;
    }

    public ValueUpdateResult UnbindProperty(RadProperty boundProperty)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(boundProperty, false);
      if (entry == null || entry.PropertyBinding == null)
        return ValueUpdateResult.NotUpdated;
      return this.ResetValueCore(entry, ValueResetFlags.Binding);
    }

    internal void OnPropertyBoundExternally(PropertyBinding binding, RadObject boundObject)
    {
      this.propertyValues.GetEntry(binding.SourceProperty, true).AddBoundObject(new PropertyBoundObject(boundObject, binding.BoundProperty));
    }

    internal void OnPropertyUnboundExternally(PropertyBinding binding, RadObject boundObject)
    {
      if (this.IsDisposing)
        return;
      RadPropertyValue entry = this.propertyValues.GetEntry(binding.SourceProperty, false);
      if (entry == null)
        return;
      entry.BeginUpdate(true, false);
      entry.RemoveBoundObject(boundObject);
      if ((binding.BindingOptions & PropertyBindingOptions.TwoWay) == PropertyBindingOptions.TwoWay)
      {
        int num1 = (int) this.ResetValueCore(entry, ValueResetFlags.TwoWayBindingLocal);
      }
      if ((binding.BindingOptions & PropertyBindingOptions.PreserveAsLocalValue) == PropertyBindingOptions.PreserveAsLocalValue)
        entry.SetLocalValue(binding.GetValue());
      entry.EndUpdate(true, false);
      int num2 = (int) this.UpdateValueCore(entry);
    }

    internal void OnTwoWayBoundPropertyChanged(PropertyBinding binding, object newValue)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(binding.SourceProperty, false);
      if (entry == null)
        return;
      int num = (int) this.SetValueCore(entry, (object) null, newValue, ValueSource.LocalFromBinding);
    }

    internal void OnBoundSourcePropertyChanged(RadProperty boundProperty)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(boundProperty, false);
      if (entry == null || entry.IsCompositionLocked)
        return;
      int num = (int) this.UpdateValueCore(entry);
    }

    internal void RemoveBinding(RadPropertyValue propVal)
    {
      PropertyBinding propertyBinding = propVal.PropertyBinding;
      if (propertyBinding == null)
        return;
      RadObject sourceObject = propertyBinding.SourceObject;
      if (sourceObject == null || (propertyBinding.BindingOptions & PropertyBindingOptions.NoChangeNotify) != (PropertyBindingOptions) 0)
        return;
      sourceObject.OnPropertyUnboundExternally(propertyBinding, this);
    }

    protected internal virtual ValueUpdateResult AddStylePropertySetting(
      IPropertySetting setting)
    {
      return this.SetValueCore(this.propertyValues.GetEntry(setting.Property, true), (object) setting, (object) null, ValueSource.Style);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void RemoveStylePropertySetting(IPropertySetting setting)
    {
      this.RemoveStylePropertySetting(setting.Property);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void RemoveStylePropertySetting(RadProperty property)
    {
      RadPropertyValue entry = this.propertyValues.GetEntry(property, false);
      if (entry == null)
        return;
      int num = (int) this.ResetValueCore(entry, ValueResetFlags.Style);
    }

    protected internal virtual object GetInheritedValue(RadProperty property)
    {
      if (this.GetBitState(1L) || this.GetBitState(2L))
        return RadProperty.UnsetValue;
      int globalIndex = property.GlobalIndex;
      System.Type ownerType = property.OwnerType;
      object obj = RadProperty.UnsetValue;
      for (RadObject inheritanceParent = this.InheritanceParent; inheritanceParent != null; inheritanceParent = inheritanceParent.InheritanceParent)
      {
        if (ownerType.IsInstanceOfType((object) inheritanceParent))
        {
          RadPropertyValue entry = inheritanceParent.propertyValues.GetEntry(property, false);
          if (entry != null)
          {
            obj = entry.GetCurrentValue(true);
            break;
          }
        }
      }
      return obj;
    }

    internal virtual RadObject InheritanceParent
    {
      get
      {
        return (RadObject) null;
      }
    }

    protected virtual void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      RadPropertyChangingEventHandler changingEventHandler = this.Events[RadObject.RadPropertyChangingEventKey] as RadPropertyChangingEventHandler;
      if (changingEventHandler == null)
        return;
      changingEventHandler((object) this, args);
    }

    protected virtual void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      RadPropertyChangedEventHandler changedEventHandler = this.Events[RadObject.RadPropertyChangedEventKey] as RadPropertyChangedEventHandler;
      if (changedEventHandler != null)
        changedEventHandler((object) this, e);
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(e.Property.Name));
    }

    protected virtual void OnNotifyPropertyChanged(string propertyName)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      PropertyChangedEventHandler changedEventHandler = this.Events[RadObject.PropertyChangedEventKey] as PropertyChangedEventHandler;
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, e);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public virtual bool IsDesignMode
    {
      get
      {
        return this.GetBitState(4L);
      }
      set
      {
        this.SetBitState(4L, value);
      }
    }

    protected virtual bool IsPropertyCancelable(RadPropertyMetadata metadata)
    {
      return false;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Filter PropertyFilter
    {
      get
      {
        return this.propertyFilter;
      }
      set
      {
        this.propertyFilter = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadObjectType RadObjectType
    {
      get
      {
        return this.radType;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [RadPropertyDefaultValue("BindingContext", typeof (RadObject))]
    public virtual BindingContext BindingContext
    {
      get
      {
        return (BindingContext) this.GetValue(RadObject.BindingContextProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadObject.BindingContextProperty, (object) value);
      }
    }

    internal HybridDictionary ValuesAnimators
    {
      get
      {
        if (this.valuesAnimators == null)
          this.valuesAnimators = new HybridDictionary();
        return this.valuesAnimators;
      }
    }
  }
}
