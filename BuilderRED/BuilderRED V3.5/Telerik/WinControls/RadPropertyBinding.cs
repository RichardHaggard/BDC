// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadPropertyBinding
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class RadPropertyBinding : ITwoWayPropertyBinding, IPropertyBinding
  {
    private RadProperty fromProperty;
    private RadObject bindingSourceObject;
    private RadProperty toProperty;
    private PropertyBindingOptions options;

    public RadPropertyBinding(
      RadObject bindingSourceObject,
      RadProperty fromProperty,
      RadProperty bindingSourceProperty,
      PropertyBindingOptions options)
    {
      this.bindingSourceObject = bindingSourceObject;
      this.fromProperty = fromProperty;
      this.toProperty = bindingSourceProperty;
      this.options = options;
    }

    public RadObject BindingSourceObject
    {
      get
      {
        return this.bindingSourceObject;
      }
    }

    public bool CanGetValueForProperty(RadProperty property)
    {
      return property == this.fromProperty;
    }

    public object GetValue()
    {
      return this.BindingSourceObject.GetValue(this.toProperty);
    }

    public void ResetValue()
    {
      int num1 = (int) this.BindingSourceObject.ResetValue(this.fromProperty);
      int num2 = (int) this.BindingSourceObject.ResetValue(this.toProperty);
    }

    public void UpdateBindingSourceProperty(RadProperty boundProperty, object newValue)
    {
      if (this.options != PropertyBindingOptions.TwoWay)
        return;
      int num = (int) this.BindingSourceObject.SetValue(this.toProperty, newValue);
    }
  }
}
