// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CompositeFilter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls
{
  public abstract class CompositeFilter : Filter
  {
    protected List<Filter> filters;

    public CompositeFilter(params Filter[] filters)
    {
      this.filters = new List<Filter>();
      if (filters == null)
        return;
      foreach (Filter filter in filters)
        this.filters.Add(filter);
    }
  }
}
