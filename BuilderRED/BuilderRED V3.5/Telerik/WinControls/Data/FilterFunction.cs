// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterFunction
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Telerik.WinControls.UI;

namespace Telerik.WinControls.Data
{
  internal abstract class FilterFunction
  {
    protected string functionFormat = string.Empty;
    private static List<Type> emptyTypeList = new List<Type>();
    private static Dictionary<Type, string> quotedValueFormatStrings = (Dictionary<Type, string>) null;
    private static Dictionary<GridKnownFunction, FilterFunction> functionDictionary = (Dictionary<GridKnownFunction, FilterFunction>) null;
    private static string[] _illegalStrings = (string[]) null;
    protected GridKnownFunction gkfunction;
    protected bool mustQuoteValues;
    protected int parameterCount;

    public abstract string DisplayName { get; }

    public int ParameterCount
    {
      get
      {
        return this.parameterCount;
      }
    }

    public abstract List<Type> DefaultForTypes { get; }

    public abstract List<Type> ApplicableForTypes { get; }

    public virtual List<Type> NotApplicableForTypes
    {
      get
      {
        return FilterFunction.emptyTypeList;
      }
    }

    public bool IsApplicableForType(Type type)
    {
      List<Type> applicableForTypes1 = this.NotApplicableForTypes;
      if (applicableForTypes1 != null)
      {
        for (int index = 0; index < applicableForTypes1.Count; ++index)
        {
          if (applicableForTypes1[index].IsAssignableFrom(type))
            return false;
        }
      }
      List<Type> applicableForTypes2 = this.ApplicableForTypes;
      if (applicableForTypes2 != null)
      {
        for (int index = 0; index < applicableForTypes2.Count; ++index)
        {
          if (applicableForTypes2[index].IsAssignableFrom(type))
            return true;
        }
      }
      return false;
    }

    public bool IsDefaultForType(Type type)
    {
      List<Type> defaultForTypes = this.DefaultForTypes;
      if (defaultForTypes != null)
      {
        for (int index = 0; index < defaultForTypes.Count; ++index)
        {
          if (defaultForTypes[index].IsAssignableFrom(type))
            return true;
        }
      }
      return false;
    }

    static FilterFunction()
    {
      FilterFunction.quotedValueFormatStrings = new Dictionary<Type, string>();
      FilterFunction.quotedValueFormatStrings.Add(typeof (string), "'{0}'");
      FilterFunction.quotedValueFormatStrings.Add(typeof (TimeSpan), "'{0}'");
      FilterFunction.quotedValueFormatStrings.Add(typeof (byte[]), "'{0}'");
      FilterFunction.quotedValueFormatStrings.Add(typeof (bool), "'{0}'");
      FilterFunction.quotedValueFormatStrings.Add(typeof (DateTime), "#{0}#");
      FilterFunction.functionDictionary = new Dictionary<GridKnownFunction, FilterFunction>();
      Array values = Enum.GetValues(typeof (GridKnownFunction));
      for (int index = 0; index < values.Length; ++index)
        FilterFunction.GetFilterFunction((GridKnownFunction) values.GetValue(index));
    }

    public static FilterFunction GetFilterFunction(GridKnownFunction function)
    {
      FilterFunction filterFunction;
      if (!FilterFunction.functionDictionary.ContainsKey(function))
      {
        switch (function)
        {
          case GridKnownFunction.NoFilter:
            filterFunction = (FilterFunction) new NoFilterFunction();
            break;
          case GridKnownFunction.Contains:
            filterFunction = (FilterFunction) new ContainsFunction();
            break;
          case GridKnownFunction.DoesNotContain:
            filterFunction = (FilterFunction) new DoesNotContainFunction();
            break;
          case GridKnownFunction.StartsWith:
            filterFunction = (FilterFunction) new StartsWithFunction();
            break;
          case GridKnownFunction.EndsWith:
            filterFunction = (FilterFunction) new EndsWithFunction();
            break;
          case GridKnownFunction.EqualTo:
            filterFunction = (FilterFunction) new EqualToFunction();
            break;
          case GridKnownFunction.NotEqualTo:
            filterFunction = (FilterFunction) new NotEqualToFunction();
            break;
          case GridKnownFunction.GreaterThan:
            filterFunction = (FilterFunction) new GreaterThanFunction();
            break;
          case GridKnownFunction.LessThan:
            filterFunction = (FilterFunction) new LessThanFunction();
            break;
          case GridKnownFunction.GreaterThanOrEqualTo:
            filterFunction = (FilterFunction) new GreaterThanOrEqualToFunction();
            break;
          case GridKnownFunction.LessThanOrEqualTo:
            filterFunction = (FilterFunction) new LessThanOrEqualToFunction();
            break;
          case GridKnownFunction.Between:
            filterFunction = (FilterFunction) new BetweenFunction();
            break;
          case GridKnownFunction.NotBetween:
            filterFunction = (FilterFunction) new NotBetweenFunction();
            break;
          case GridKnownFunction.IsEmpty:
            filterFunction = (FilterFunction) new IsEmptyFunction();
            break;
          case GridKnownFunction.NotIsEmpty:
            filterFunction = (FilterFunction) new NotIsEmptyFunction();
            break;
          case GridKnownFunction.IsNull:
            filterFunction = (FilterFunction) new IsNullFunction();
            break;
          case GridKnownFunction.NotIsNull:
            filterFunction = (FilterFunction) new NotIsNullFunction();
            break;
          default:
            return (FilterFunction) null;
        }
        FilterFunction.functionDictionary.Add(function, filterFunction);
      }
      else
        filterFunction = FilterFunction.functionDictionary[function];
      return filterFunction;
    }

    public static List<FilterFunction> GetApplicableFunctionsForType(Type type)
    {
      List<FilterFunction> filterFunctionList = new List<FilterFunction>();
      Dictionary<GridKnownFunction, FilterFunction>.Enumerator enumerator = FilterFunction.functionDictionary.GetEnumerator();
      while (enumerator.MoveNext())
      {
        FilterFunction filterFunction = enumerator.Current.Value;
        if (filterFunction.IsApplicableForType(type))
          filterFunctionList.Add(filterFunction);
      }
      return filterFunctionList;
    }

    public static FilterFunction GetDefaultFunctionForType(Type type)
    {
      Dictionary<GridKnownFunction, FilterFunction>.Enumerator enumerator = FilterFunction.functionDictionary.GetEnumerator();
      while (enumerator.MoveNext())
      {
        FilterFunction filterFunction = enumerator.Current.Value;
        if (filterFunction != null && filterFunction.IsDefaultForType(type))
          return filterFunction;
      }
      return FilterFunction.functionDictionary[GridKnownFunction.NoFilter];
    }

    private static string[] IllegalStrings
    {
      get
      {
        if (FilterFunction._illegalStrings == null)
          FilterFunction._illegalStrings = new string[10]
          {
            " LIKE ",
            " AND ",
            " OR ",
            "\"",
            ">",
            "<",
            "<>",
            "%",
            " NULL ",
            " IS "
          };
        return FilterFunction._illegalStrings;
      }
      set
      {
        FilterFunction._illegalStrings = value;
      }
    }

    private string FormatValue(string value, string quotedValueFormatString)
    {
      string str = value.Trim();
      if (this.mustQuoteValues)
        str = string.Format(quotedValueFormatString, (object) str);
      return str;
    }

    private bool CheckParameters(object[] values)
    {
      if (values == null)
        throw new ArgumentNullException("Value should not be null.");
      if (this.parameterCount > values.Length)
        return false;
      for (int index = 0; index < this.parameterCount; ++index)
      {
        if (values.GetValue(index) == null)
          return false;
      }
      return true;
    }

    public virtual string GetFunctionString(string fieldName, object[] values)
    {
      if (!this.CheckParameters(values))
        return string.Empty;
      List<string> stringList = new List<string>(3);
      stringList.Add(fieldName);
      for (int index = 0; index < this.parameterCount; ++index)
      {
        object obj = values[index];
        string filterString = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}", obj);
        if (obj is string)
        {
          foreach (string illegalString in FilterFunction.IllegalStrings)
          {
            if (filterString.IndexOf(illegalString) >= 0)
              return "";
          }
          filterString = this.EscapeFilterString(filterString);
        }
        Type type = obj.GetType();
        string quotedValueFormatString = FilterFunction.quotedValueFormatStrings.ContainsKey(type) ? FilterFunction.quotedValueFormatStrings[type] : "{0}";
        stringList.Add(this.FormatValue(filterString, quotedValueFormatString));
      }
      return string.Format(this.functionFormat, (object[]) stringList.ToArray());
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      return (object) this.GetType() == (object) obj.GetType();
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public static explicit operator GridKnownFunction(FilterFunction ff)
    {
      if (ff == null)
        return GridKnownFunction.NoFilter;
      return ff.gkfunction;
    }

    private string EscapeFilterString(string filterString)
    {
      filterString = filterString.Replace("'", "''");
      filterString = Regex.Replace(filterString, "[\\]\\\\]", "\\$&");
      filterString = Regex.Replace(filterString, "[\\~\\(\\)\\#\\/\\=\\>\\<\\+\\-\\*\\%\\&\\|\\^\\\"\\[]", "[$&]");
      return filterString;
    }
  }
}
