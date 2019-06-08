// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TextBoxAutoCompleteSourceConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  internal class TextBoxAutoCompleteSourceConverter : EnumConverter
  {
    public TextBoxAutoCompleteSourceConverter(Type type)
      : base(type)
    {
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      TypeConverter.StandardValuesCollection standardValues = base.GetStandardValues(context);
      ArrayList arrayList = new ArrayList();
      int count = standardValues.Count;
      for (int index = 0; index < count; ++index)
      {
        if (!standardValues[index].ToString().Equals("ListItems"))
          arrayList.Add(standardValues[index]);
      }
      return new TypeConverter.StandardValuesCollection((ICollection) arrayList);
    }
  }
}
