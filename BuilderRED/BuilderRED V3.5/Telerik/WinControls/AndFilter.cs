// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.AndFilter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class AndFilter : CompositeFilter
  {
    public AndFilter(params Filter[] filters)
      : base(filters)
    {
    }

    public override bool Match(object obj)
    {
      foreach (Filter filter in this.filters)
      {
        if (!filter.Match(obj))
          return false;
      }
      return true;
    }
  }
}
