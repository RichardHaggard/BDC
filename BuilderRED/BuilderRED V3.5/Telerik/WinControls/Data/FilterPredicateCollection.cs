// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterPredicateCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Data
{
  public class FilterPredicateCollection : ObservableCollection<FilterPredicate>
  {
    public void Add(
      FilterExpression.BinaryOperation binaryOperator,
      GridKnownFunction function,
      params object[] values)
    {
      this.Add(new FilterPredicate(binaryOperator, function, values));
    }

    public void AddRange(IEnumerable<FilterPredicate> predicates)
    {
      IEnumerator<FilterPredicate> enumerator = predicates.GetEnumerator();
      while (enumerator.MoveNext())
        this.Add(enumerator.Current);
    }

    public void AddRange(params FilterPredicate[] predicates)
    {
      this.AddRange((IEnumerable<FilterPredicate>) predicates);
    }

    public void RemoveRange(IEnumerable<FilterPredicate> predicates)
    {
      IEnumerator<FilterPredicate> enumerator = predicates.GetEnumerator();
      while (enumerator.MoveNext())
        this.Remove(enumerator.Current);
    }

    public void RemoveRange(params FilterPredicate[] predicates)
    {
      this.RemoveRange((IEnumerable<FilterPredicate>) predicates);
    }

    public FilterPredicate FindByValue(object value)
    {
      for (int index1 = 0; index1 < this.Count; ++index1)
      {
        FilterPredicate filterPredicate = this[index1];
        for (int index2 = 0; index2 < filterPredicate.Values.Count; ++index2)
        {
          if (filterPredicate.Values[index2].Equals(value))
            return filterPredicate;
        }
      }
      return (FilterPredicate) null;
    }
  }
}
