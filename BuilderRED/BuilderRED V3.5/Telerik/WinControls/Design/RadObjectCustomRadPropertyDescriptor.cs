// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Design.RadObjectCustomRadPropertyDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.Design
{
  public class RadObjectCustomRadPropertyDescriptor : RadObjectCustomPropertyDescriptor
  {
    private RadProperty radProperty;

    public RadObjectCustomRadPropertyDescriptor(
      PropertyDescriptor wrapped,
      Attribute[] attributes,
      bool readOnly)
      : base(wrapped, attributes, readOnly)
    {
    }

    public RadObjectCustomRadPropertyDescriptor(PropertyDescriptor wrapped, Attribute[] attributes)
      : base(wrapped, attributes)
    {
    }

    public RadProperty GetRadProperty(RadObject component)
    {
      if (this.radProperty == null)
        this.radProperty = component.GetRegisteredRadProperty(this.Name);
      return this.radProperty;
    }

    public override void SetValue(object component, object value)
    {
      base.SetValue(component, value);
      RadObject component1 = (RadObject) component;
      if (component1 == null)
        return;
      RadProperty radProperty = this.GetRadProperty(component1);
      if (radProperty == null || !component1.IsDesignMode)
        return;
      component1.SetPropertyValueSetAtDesignTime(radProperty);
    }

    public override bool ShouldSerializeValue(object component)
    {
      bool? serializeFromRadobject = this.GetShouldSerializeFromRadobject(component);
      if (serializeFromRadobject.HasValue && serializeFromRadobject.HasValue)
        return serializeFromRadobject.Value;
      RadObject component1 = (RadObject) component;
      RadProperty radProperty = this.GetRadProperty(component1);
      RadPropertyValue propertyValue = component1.GetPropertyValue(radProperty);
      if (propertyValue == null || propertyValue.ValueSource != ValueSource.Local && propertyValue.ValueSource != ValueSource.LocalFromBinding)
        return false;
      if (radProperty == RadElement.BoundsProperty)
        return !(bool) component1.GetValue(RadElement.AutoSizeProperty);
      if (propertyValue.IsSetAtDesignTime)
        return true;
      return this.Wrapped.ShouldSerializeValue(component);
    }
  }
}
