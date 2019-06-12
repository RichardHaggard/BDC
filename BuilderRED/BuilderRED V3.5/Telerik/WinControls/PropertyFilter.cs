// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PropertyFilter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls
{
  public class PropertyFilter : Filter
  {
    public static readonly PropertyFilter ExcludeFilter = new PropertyFilter();
    public static readonly PropertyFilter LayoutFilter;
    public static readonly PropertyFilter AppearanceFilter;
    public static readonly PropertyFilter BehaviorFilter;
    public static readonly PropertyFilter FillPrimitiveFilter;
    public static readonly PropertyFilter BorderPrimitiveFilter;
    public static readonly PropertyFilter ImagePrimitiveFilter;
    public static readonly PropertyFilter TextPrimitiveFilter;
    private Dictionary<int, RadProperty> availableProperties;
    private UnaryOperator unaryOperator;

    public PropertyFilter()
    {
      this.availableProperties = new Dictionary<int, RadProperty>();
      this.unaryOperator = UnaryOperator.None;
    }

    public PropertyFilter(IEnumerable<RadProperty> properties)
      : this()
    {
      foreach (RadProperty property in properties)
      {
        if (!this.availableProperties.ContainsKey(property.NameHash))
          this.availableProperties.Add(property.NameHash, property);
      }
    }

    static PropertyFilter()
    {
      PropertyFilter.ExcludeFilter.unaryOperator = UnaryOperator.NotOperator;
      PropertyFilter.AddExcludeProperties();
      PropertyFilter.LayoutFilter = new PropertyFilter((IEnumerable<RadProperty>) new RadProperty[10]
      {
        RadElement.AngleTransformProperty,
        RadElement.BorderThicknessProperty,
        RadElement.MarginProperty,
        RadElement.MaxSizeProperty,
        RadElement.MinSizeProperty,
        RadElement.PaddingProperty,
        RadElement.PositionOffsetProperty,
        RadElement.ScaleTransformProperty,
        RadElement.StretchHorizontallyProperty,
        RadElement.StretchVerticallyProperty
      });
      PropertyFilter.AppearanceFilter = new PropertyFilter((IEnumerable<RadProperty>) new RadProperty[9]
      {
        RadElement.ShapeProperty,
        VisualElement.BackColorProperty,
        VisualElement.ForeColorProperty,
        VisualElement.FontProperty,
        VisualElement.CustomFontProperty,
        VisualElement.CustomFontSizeProperty,
        VisualElement.CustomFontStyleProperty,
        VisualElement.SmoothingModeProperty,
        VisualElement.OpacityProperty
      });
      PropertyFilter.BehaviorFilter = new PropertyFilter((IEnumerable<RadProperty>) new RadProperty[3]
      {
        RadElement.ShouldPaintProperty,
        RadElement.ZIndexProperty,
        RadElement.VisibilityProperty
      });
      PropertyFilter.FillPrimitiveFilter = new PropertyFilter(PropertyFilter.GetPropertiesDeclaredByType(typeof (FillPrimitive)));
      PropertyFilter.FillPrimitiveFilter.availableProperties.Add(RadElement.MarginProperty.NameHash, RadElement.MarginProperty);
      PropertyFilter.BorderPrimitiveFilter = new PropertyFilter(PropertyFilter.GetPropertiesDeclaredByType(typeof (BorderPrimitive)));
      PropertyFilter.BorderPrimitiveFilter.availableProperties.Add(RadElement.BorderThicknessProperty.NameHash, RadElement.BorderThicknessProperty);
      PropertyFilter.BorderPrimitiveFilter.availableProperties.Add(RadElement.MarginProperty.NameHash, RadElement.MarginProperty);
      PropertyFilter.ImagePrimitiveFilter = new PropertyFilter(PropertyFilter.GetPropertiesDeclaredByType(typeof (ImagePrimitive)));
      PropertyFilter.ImagePrimitiveFilter.availableProperties.Add(RadElement.MarginProperty.NameHash, RadElement.MarginProperty);
      PropertyFilter.ImagePrimitiveFilter.availableProperties.Add(RadElement.ShouldPaintProperty.NameHash, RadElement.ShouldPaintProperty);
      PropertyFilter.TextPrimitiveFilter = new PropertyFilter(PropertyFilter.GetPropertiesDeclaredByType(typeof (TextPrimitive)));
      PropertyFilter.TextPrimitiveFilter.availableProperties.Add(RadElement.MarginProperty.NameHash, RadElement.MarginProperty);
      PropertyFilter.TextPrimitiveFilter.availableProperties.Add(RadElement.ShouldPaintProperty.NameHash, RadElement.ShouldPaintProperty);
      PropertyFilter.TextPrimitiveFilter.availableProperties.Add(VisualElement.ForeColorProperty.NameHash, VisualElement.ForeColorProperty);
      PropertyFilter.TextPrimitiveFilter.availableProperties.Remove(TextPrimitive.TextProperty.NameHash);
    }

    private static void AddExcludeProperties()
    {
      PropertyFilter.ExcludeFilter.availableProperties.Add("AccessibleDescription".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("AccessibleName".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("AccessibleRole".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("AutoToolTip".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add(RadElement.CanFocusProperty.NameHash, RadElement.CanFocusProperty);
      PropertyFilter.ExcludeFilter.availableProperties.Add(RadElement.ClickModeProperty.NameHash, RadElement.ClickModeProperty);
      PropertyFilter.ExcludeFilter.availableProperties.Add("CommandBinding".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add(RadElement.EnabledProperty.NameHash, RadElement.EnabledProperty);
      PropertyFilter.ExcludeFilter.availableProperties.Add("IsSharedImage".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("KeyTip".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("ScreenTip".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add(RadItem.TextProperty.NameHash, RadItem.TextProperty);
      PropertyFilter.ExcludeFilter.availableProperties.Add("ToolTipText".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("DataBindings".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("Children".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("Tag".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("SerializeChildren".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add(RadElement.BoundsProperty.NameHash, RadElement.BoundsProperty);
      PropertyFilter.ExcludeFilter.availableProperties.Add("Location".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("Size".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add("UseSystemSkin".GetHashCode(), (RadProperty) null);
      PropertyFilter.ExcludeFilter.availableProperties.Add(RadElement.ClassProperty.NameHash, RadElement.ClassProperty);
    }

    public Dictionary<int, RadProperty> AvailableProperties
    {
      get
      {
        return this.availableProperties;
      }
    }

    public static void AddPropertiesDeclaredByType(
      Dictionary<int, RadProperty> properties,
      Type type)
    {
      foreach (RadProperty radProperty in PropertyFilter.GetPropertiesDeclaredByType(type))
      {
        if (!properties.ContainsKey(radProperty.NameHash))
          properties.Add(radProperty.NameHash, radProperty);
      }
    }

    public static IEnumerable<RadProperty> GetPropertiesDeclaredByType(
      Type type)
    {
      FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
      foreach (FieldInfo fieldInfo in fields)
      {
        RadProperty property = fieldInfo.GetValue((object) null) as RadProperty;
        if (property != null)
          yield return property;
      }
    }

    public override bool Match(object obj)
    {
      PropertyDescriptor propertyDescriptor = obj as PropertyDescriptor;
      if (propertyDescriptor == null)
        return false;
      bool flag = this.availableProperties.ContainsKey(propertyDescriptor.Name.GetHashCode());
      if (this.unaryOperator == UnaryOperator.NotOperator)
        return !flag;
      return flag;
    }
  }
}
