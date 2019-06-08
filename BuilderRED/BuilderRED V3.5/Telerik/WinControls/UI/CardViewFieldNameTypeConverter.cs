// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CardViewFieldNameTypeConverter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class CardViewFieldNameTypeConverter : TypeConverter
  {
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
    {
      return true;
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(
      ITypeDescriptorContext context)
    {
      List<string> stringList = new List<string>();
      LayoutControlItemBase layoutControlItemBase = context.Instance is ICardViewBoundItem ? context.Instance as LayoutControlItemBase : (LayoutControlItemBase) null;
      if (layoutControlItemBase != null && layoutControlItemBase.ElementTree != null && (layoutControlItemBase.ElementTree.Control != null && layoutControlItemBase.ElementTree.Control.Parent != null) && layoutControlItemBase.ElementTree.Control.Parent is RadCardView)
      {
        CardListViewElement viewElement = (layoutControlItemBase.ElementTree.Control.Parent as RadCardView).CardViewElement.ViewElement as CardListViewElement;
        if (viewElement != null)
        {
          stringList.Add(string.Empty);
          foreach (KeyValuePair<string, Type> fieldName in viewElement.GetFieldNames())
            stringList.Add(fieldName.Key);
        }
      }
      return new TypeConverter.StandardValuesCollection((ICollection) stringList);
    }
  }
}
