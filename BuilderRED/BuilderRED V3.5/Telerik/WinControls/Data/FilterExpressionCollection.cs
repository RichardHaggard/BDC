// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterExpressionCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Data
{
  [ListBindable(BindableSupport.No)]
  public class FilterExpressionCollection : GridViewFilterDescriptorCollection
  {
    public FilterExpressionCollection(GridViewTemplate owner)
      : base(owner)
    {
    }

    public FilterExpression this[string fieldName]
    {
      get
      {
        int index = this.IndexOf(fieldName);
        if (index >= 0)
          return this[index] as FilterExpression;
        return (FilterExpression) null;
      }
    }
  }
}
