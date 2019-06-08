// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SettingValueEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class SettingValueEditor : UITypeEditor
  {
    private UITypeEditor actualEditor;
    private TypeConverter actualConverter;
    private RadProperty currProperty;
    private System.Type actualPropertyType;

    public System.Type GetActualPropertyType()
    {
      return this.actualPropertyType;
    }

    public RadProperty GetRadProperty()
    {
      return this.currProperty;
    }

    private UITypeEditor GetActualEditor(ITypeDescriptorContext context)
    {
      if (context == null)
        return this.actualEditor;
      XmlPropertySetting instance = (XmlPropertySetting) context.Instance;
      if (instance.Property == null)
      {
        this.actualEditor = (UITypeEditor) null;
        return (UITypeEditor) null;
      }
      string[] strArray = instance.Property.Split('.');
      if (strArray.Length > 1)
      {
        string propertyName = strArray[strArray.Length - 1];
        string className = string.Join(".", strArray, 0, strArray.Length - 1);
        RadProperty safe = RadProperty.FindSafe(className, propertyName);
        this.currProperty = safe;
        if (safe != null)
        {
          TypeConverter converter = TypeDescriptor.GetConverter(safe.PropertyType);
          this.actualPropertyType = safe.PropertyType;
          if (converter == null || !converter.CanConvertFrom(typeof (string)) || !converter.CanConvertTo(typeof (string)))
          {
            if (!converter.CanConvertFrom(typeof (string)))
            {
              int num1 = (int) MessageBox.Show("Converter can't convert from string");
            }
            else if (!converter.CanConvertTo(typeof (string)))
            {
              int num2 = (int) MessageBox.Show("Converter can't convert to string");
            }
            else
            {
              int num3 = (int) MessageBox.Show("Converter for type not found");
            }
            this.actualEditor = (UITypeEditor) null;
            return (UITypeEditor) null;
          }
          this.actualConverter = converter;
          PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(safe.OwnerType).Find(safe.Name, false);
          this.actualEditor = propertyDescriptor == null ? (UITypeEditor) TypeDescriptor.GetEditor(safe.PropertyType, typeof (UITypeEditor)) : (UITypeEditor) propertyDescriptor.GetEditor(typeof (UITypeEditor));
          return this.actualEditor;
        }
        int num = (int) MessageBox.Show("Can't find property " + instance.Property + ". Property " + propertyName + "not registered for RadObject" + className);
        this.actualEditor = (UITypeEditor) null;
        return (UITypeEditor) null;
      }
      int num4 = (int) MessageBox.Show("Invalid property name. Property consist of type FullName\".\"PropertyName.");
      this.actualEditor = (UITypeEditor) null;
      return (UITypeEditor) null;
    }

    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      UITypeEditor actualEditor = this.GetActualEditor(context);
      TypeConverter actualConverter = this.actualConverter;
      if (actualEditor == null || actualConverter == null || this.currProperty == null)
        return value;
      object obj1 = value == null || (object) value.GetType() != (object) this.currProperty.PropertyType ? (!this.currProperty.PropertyType.IsValueType ? (object) null : Activator.CreateInstance(this.currProperty.PropertyType)) : actualConverter.ConvertFrom(value);
      object obj2 = value;
      if (actualEditor != null)
      {
        object obj3 = actualEditor.EditValue(provider, obj1);
        obj2 = actualConverter.ConvertTo(obj3, typeof (string));
      }
      return obj2;
    }

    public bool CanEditValue(ITypeDescriptorContext context)
    {
      return this.GetActualEditor(context) != null;
    }
  }
}
