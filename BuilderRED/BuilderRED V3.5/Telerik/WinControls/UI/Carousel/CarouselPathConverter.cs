// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Carousel.CarouselPathConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.WinControls.UI.Carousel
{
  public class CarouselPathConverter : ReferenceConverter
  {
    public CarouselPathConverter()
      : base(typeof (ICarouselPath))
    {
    }

    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      ArrayList arrayList = new ArrayList();
      TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
      if (standardValues != null)
      {
        for (int index = 0; index < standardValues.Count; ++index)
        {
          if (index > 0)
          {
            object obj = standardValues[index];
            arrayList.Add(obj);
          }
        }
      }
      return new TypeConverter.StandardValuesCollection((ICollection) arrayList);
    }

    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(value, attributes);
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
