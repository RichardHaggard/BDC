// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.XmlPropertySetting
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using Telerik.WinControls.Themes.XmlSerialization;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class XmlPropertySetting : ISerializationValidatable
  {
    private static bool duplicateTelerikAssembliesDetected = false;
    private static System.Type RadObjectType = typeof (RadObject);
    private string property;
    private object value;
    private string stringValue;
    private bool serializeValueAsString;
    private RadProperty tempPoroperty;

    public XmlPropertySetting()
    {
    }

    public XmlPropertySetting(RadProperty property, object value)
    {
      this.Property = property.FullName;
      this.Value = value;
    }

    [Description("RadProperty string representation, consisting of property owner type full name, and property name")]
    [XmlAttribute]
    public string Property
    {
      get
      {
        return this.property;
      }
      set
      {
        this.InvalidateTempValues();
        if (string.IsNullOrEmpty(value))
          throw new InvalidOperationException("Property cannot be null or empty");
        this.property = value;
      }
    }

    [SerializationConverter(typeof (XmlPropertySettingValueConverter))]
    [Description("RadProperty value to use when assigning or comparing")]
    [TypeConverter(typeof (SettingValueConverter))]
    [Editor(typeof (SettingValueEditor), typeof (UITypeEditor))]
    public object Value
    {
      get
      {
        if (this.value == null && this.stringValue != null)
        {
          RadProperty deserializedProperty = this.GetDeserializedProperty();
          if (deserializedProperty == null)
            throw new InvalidOperationException("Deserialized XmlProperty setting has no property assigned");
          this.value = XmlPropertySetting.DeserializeValue(deserializedProperty, this.stringValue);
        }
        return this.value;
      }
      set
      {
        this.InvalidateTempValues();
        this.value = value;
      }
    }

    [Browsable(false)]
    public string ValueString
    {
      get
      {
        RadProperty deserializedProperty = this.GetDeserializedProperty();
        if (deserializedProperty != null)
          return XmlPropertySetting.SerializeValue(deserializedProperty, this.value);
        return (string) null;
      }
      set
      {
        this.stringValue = value;
      }
    }

    private bool ShouldSerializeValue()
    {
      if (string.IsNullOrEmpty(this.property))
        return true;
      return !this.serializeValueAsString;
    }

    private bool ShouldSerializeValueString()
    {
      if (string.IsNullOrEmpty(this.property))
        return false;
      return this.serializeValueAsString;
    }

    public static RadProperty DeserializeProperty(string property)
    {
      return XmlPropertySetting.DeserializePropertyCore(property, true, true);
    }

    public static RadProperty DeserializePropertySafe(string property)
    {
      return XmlPropertySetting.DeserializePropertyCore(property, true, false);
    }

    private static RadProperty DeserializePropertyCore(
      string property,
      bool fallback,
      bool throwOnError)
    {
      if (string.IsNullOrEmpty(property))
        return (RadProperty) null;
      string[] strArray = property.Split('.');
      if (strArray.Length > 1)
      {
        string propertyName = strArray[strArray.Length - 1];
        string className = string.Join(".", strArray, 0, strArray.Length - 1);
        System.Type typeByName = RadTypeResolver.Instance.GetTypeByName(className);
        if ((object) typeByName == null)
        {
          if (throwOnError)
            throw new InvalidOperationException("Could not look-up type '" + className + "'");
          return (RadProperty) null;
        }
        RadProperty property1 = XmlPropertySetting.FindProperty(typeByName, propertyName, fallback);
        if (property1 != null)
          return property1;
        if (!throwOnError)
          return (RadProperty) null;
        return XmlPropertySetting.ProcessDuplicateAssemblies(propertyName, className);
      }
      if (throwOnError)
        throw new Exception("Invalid property parts");
      return (RadProperty) null;
    }

    private static RadProperty FindProperty(
      System.Type currentType,
      string propertyName,
      bool fallback)
    {
      RadProperty safe = RadProperty.FindSafe(currentType, propertyName);
      if (safe == null && fallback)
      {
        for (currentType = currentType.BaseType; (object) currentType != null && (object) currentType != (object) XmlPropertySetting.RadObjectType; currentType = currentType.BaseType)
        {
          safe = RadProperty.FindSafe(currentType, propertyName);
          if (safe != null)
            break;
        }
      }
      return safe;
    }

    private static RadProperty ProcessDuplicateAssemblies(
      string propertyName,
      string className)
    {
      if (XmlPropertySetting.duplicateTelerikAssembliesDetected)
        return (RadProperty) null;
      Hashtable hashtable = new Hashtable();
      foreach (RadTypeResolver.LoadedAssembly loadedAssembly in RadTypeResolver.Instance.LoadedAssemblies)
      {
        if (loadedAssembly.isTelerik)
        {
          Assembly assembly = hashtable[(object) loadedAssembly.assembly.FullName] as Assembly;
          if ((object) assembly != null)
          {
            int num = (int) MessageBox.Show(string.Format("Visual Studio is attempting to load Telerik assemblies that have different versions: \n {0} \n and \n {1} \n Please remove references to assemblies with incorrect version and restart Visual Studio.", (object) loadedAssembly.assembly.Location, (object) assembly.Location));
            XmlPropertySetting.duplicateTelerikAssembliesDetected = true;
            return (RadProperty) null;
          }
          hashtable[(object) loadedAssembly.assembly.FullName] = (object) loadedAssembly.assembly;
        }
      }
      throw new RadPropertyNotFoundException(propertyName, className);
    }

    public static string SerializeValue(RadProperty property, object value)
    {
      if (value == null)
        return (string) null;
      PropertyDescriptor prop = TypeDescriptor.GetProperties(property.OwnerType).Find(property.Name, true);
      return prop == null ? XmlPropertySetting.ConvertValueToString(TypeDescriptor.GetConverter(property.PropertyType), value, property.FullName) : XmlPropertySetting.SerializeValue(prop, value, property.FullName);
    }

    public static string SerializeValue(
      PropertyDescriptor prop,
      object value,
      string propertyDisplayName)
    {
      return XmlPropertySetting.ConvertValueToString(prop.Converter, value, propertyDisplayName);
    }

    private static string ConvertValueToString(
      TypeConverter converter,
      object value,
      string propertyName)
    {
      string str = (string) null;
      if (converter != null)
      {
        if (converter.CanConvertTo(typeof (string)))
        {
          try
          {
            str = (string) converter.ConvertTo((ITypeDescriptorContext) null, AnimationValueCalculatorFactory.SerializationCulture, value, typeof (string));
          }
          catch (Exception ex)
          {
            int num = (int) MessageBox.Show("Error setting value: " + ex.Message);
          }
        }
        else
        {
          if (value is string)
            return (string) value;
          int num = (int) MessageBox.Show("Can't find TypeConverter to string for property " + propertyName);
        }
      }
      else
      {
        int num1 = (int) MessageBox.Show("Can't find any TypeConverter for property " + propertyName);
      }
      return str;
    }

    private static object ConvertValueFromString(
      TypeConverter converter,
      string value,
      string propertyName,
      System.Type propertyType,
      bool throwOnError)
    {
      object obj = (object) null;
      if (converter != null)
      {
        if (converter.CanConvertFrom(typeof (string)))
        {
          try
          {
            obj = converter.ConvertFrom((ITypeDescriptorContext) null, AnimationValueCalculatorFactory.SerializationCulture, (object) value);
          }
          catch (Exception ex)
          {
            if (throwOnError)
            {
              throw;
            }
            else
            {
              int num = (int) MessageBox.Show("Error setting value: " + ex.Message);
            }
          }
        }
        else
        {
          if ((object) propertyType == (object) typeof (string))
            return (object) value;
          string str = "Can't find TypeConverter from string for property " + propertyName + " of type " + (object) propertyType;
          if (throwOnError)
            throw new InvalidOperationException(str);
          int num = (int) MessageBox.Show(str);
        }
      }
      else
      {
        string str = "Can't find any TypeConverter for property " + propertyName;
        if (throwOnError)
          throw new InvalidOperationException(str);
        int num = (int) MessageBox.Show(str);
      }
      return obj;
    }

    public static object DeserializeValue(RadProperty property, string value)
    {
      return XmlPropertySetting.DeserializeValue(property, value, false);
    }

    public static object DeserializeValue(RadProperty property, string value, bool throwOnError)
    {
      if (value == null)
        return (object) null;
      PropertyDescriptor prop = TypeDescriptor.GetProperties(property.OwnerType).Find(property.Name, true);
      return prop == null ? XmlPropertySetting.ConvertValueFromString(TypeDescriptor.GetConverter(property.PropertyType), value, property.FullName, property.PropertyType, throwOnError) : XmlPropertySetting.DeserializeValue(prop, value, property.FullName, throwOnError);
    }

    public static object DeserializeValue(
      PropertyDescriptor prop,
      string value,
      string propertyDisplayName,
      bool throwOnError)
    {
      return XmlPropertySetting.ConvertValueFromString(prop.Converter, value, propertyDisplayName, prop.PropertyType, throwOnError);
    }

    public static object DeserializeValue(
      System.Type propertyType,
      string value,
      string propertyDisplayName)
    {
      return XmlPropertySetting.ConvertValueFromString(TypeDescriptor.GetConverter(propertyType), value, propertyDisplayName, propertyType, false);
    }

    protected object GetConvertedValue(RadProperty property, object value)
    {
      if (value != null && !property.PropertyType.IsAssignableFrom(value.GetType()))
      {
        if (!(value is IValueProvider))
        {
          try
          {
            return Convert.ChangeType(value, property.PropertyType);
          }
          catch (Exception ex)
          {
            throw new InvalidCastException(string.Format("Error changing object type during deserialization. Property {0} of type: {1}, value type: {2}", (object) property.FullName, (object) property.PropertyType, (object) value.GetType().Name), ex);
          }
        }
      }
      return value;
    }

    public virtual IPropertySetting Deserialize()
    {
      if (string.IsNullOrEmpty(this.Property))
        throw new InvalidOperationException("Property to deserialize is null or empty");
      PropertySetting propertySetting = new PropertySetting()
      {
        Property = XmlPropertySetting.DeserializeProperty(this.Property)
      };
      propertySetting.Value = this.GetConvertedValue(propertySetting.Property, this.Value);
      return (IPropertySetting) propertySetting;
    }

    public override string ToString()
    {
      return "PropertySetting";
    }

    public string GetPropertyName()
    {
      if (string.IsNullOrEmpty(this.Property))
        return "unknown";
      string[] strArray = this.Property.Split('.');
      return strArray[strArray.Length - 1];
    }

    public RadProperty GetDeserializedProperty()
    {
      if (this.tempPoroperty == null)
        this.tempPoroperty = XmlPropertySetting.DeserializeProperty(this.Property);
      return this.tempPoroperty;
    }

    private void InvalidateTempValues()
    {
      this.tempPoroperty = (RadProperty) null;
    }

    void ISerializationValidatable.Validate()
    {
      if (string.IsNullOrEmpty(this.property))
        throw new InvalidOperationException("PropertySetting Property or Value cannot be null");
    }

    public void SetSerializeValueAsString(bool value)
    {
      this.serializeValueAsString = value;
    }
  }
}
