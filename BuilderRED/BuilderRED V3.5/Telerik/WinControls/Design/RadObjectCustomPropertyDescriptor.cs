// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Design.RadObjectCustomPropertyDescriptor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Telerik.WinControls.Design
{
  public class RadObjectCustomPropertyDescriptor : PropertyDescriptor
  {
    private PropertyDescriptor wrapped;
    private bool readOnly;

    public RadObjectCustomPropertyDescriptor(
      PropertyDescriptor wrapped,
      Attribute[] attributes,
      bool readOnly)
      : base(wrapped.Name, attributes)
    {
      this.wrapped = wrapped;
      this.readOnly = readOnly;
    }

    public RadObjectCustomPropertyDescriptor(PropertyDescriptor wrapped, Attribute[] attributes)
      : base(wrapped.Name, attributes)
    {
      this.wrapped = wrapped;
      this.readOnly = wrapped.IsReadOnly;
    }

    public override bool CanResetValue(object component)
    {
      return this.Wrapped.CanResetValue(component);
    }

    public override System.Type ComponentType
    {
      get
      {
        return this.Wrapped.ComponentType;
      }
    }

    protected override void OnValueChanged(object component, EventArgs e)
    {
      base.OnValueChanged(component, e);
    }

    public override object GetValue(object component)
    {
      return this.Wrapped.GetValue(component);
    }

    public override bool IsReadOnly
    {
      get
      {
        return this.readOnly;
      }
    }

    public override System.Type PropertyType
    {
      get
      {
        return this.Wrapped.PropertyType;
      }
    }

    public PropertyDescriptor Wrapped
    {
      get
      {
        return this.wrapped;
      }
    }

    public override void ResetValue(object component)
    {
      this.Wrapped.ResetValue(component);
    }

    public override void SetValue(object component, object value)
    {
      try
      {
        if (component is RadElement && this.SerializationVisibility != DesignerSerializationVisibility.Hidden)
          (component as RadElement).SerializeProperties = true;
        this.Wrapped.SetValue(component, value);
      }
      catch (Exception ex)
      {
        if (ex.InnerException != null && ex.InnerException.Message != string.Empty)
        {
          int num1 = (int) MessageBox.Show("Property value is not valid." + Environment.NewLine + ex.InnerException.Message, "Properties Window", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
        }
        else
        {
          int num2 = (int) MessageBox.Show("Error setting property \"" + this.Name + "\" in class \"" + this.Wrapped.ComponentType.FullName + "\" to value \"" + value.ToString() + "\".\n\nThe error was:\n" + ex.ToString());
        }
      }
    }

    protected bool? GetShouldSerializeFromRadobject(object component)
    {
      if (component is RadObject)
        return (component as RadObject).ShouldSerializeProperty((PropertyDescriptor) this);
      return new bool?();
    }

    public override void AddValueChanged(object component, EventHandler handler)
    {
      base.AddValueChanged(component, handler);
      this.wrapped.AddValueChanged(component, handler);
    }

    public override void RemoveValueChanged(object component, EventHandler handler)
    {
      base.RemoveValueChanged(component, handler);
      this.wrapped.RemoveValueChanged(component, handler);
    }

    public override bool ShouldSerializeValue(object component)
    {
      bool? serializeFromRadobject = this.GetShouldSerializeFromRadobject(component);
      if (serializeFromRadobject.HasValue && serializeFromRadobject.HasValue)
        return serializeFromRadobject.Value;
      return this.Wrapped.ShouldSerializeValue(component);
    }
  }
}
