﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.CompositeFilterExpression
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.UI;

namespace Telerik.WinControls.Data
{
  public class CompositeFilterExpression : FilterExpression
  {
    private FilterExpressionCollection filterExpressions = new FilterExpressionCollection((GridViewTemplate) null);

    public FilterExpressionCollection FilterExpressions
    {
      get
      {
        return this.filterExpressions;
      }
    }
  }
}
