// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupByField
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Text;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridGroupByField : NotifyPropertyBase
  {
    public static readonly string FieldDefaultFormatString = "{0}";
    private static readonly char[] InvalidChars = new char[3]
    {
      '!',
      '[',
      ']'
    };
    public static readonly string DefaultFormatString = "{0}: {1};";
    private string fieldName = string.Empty;
    private string fieldAlias = string.Empty;
    private string formatString = string.Empty;
    private RadSortOrder sortOrder = RadSortOrder.None;
    private string expression = string.Empty;
    private object dataSourceNullValue = (object) DBNull.Value;
    private Type dataType = typeof (string);
    private GridAggregateFunction aggregate;
    protected object aggregateResult;
    private string tmpFieldAlias;
    private object nullValue;
    private string _relationName;
    private string _headerText;
    private FilterExpression tempFilter;

    public GridGroupByField()
    {
      this.sortOrder = RadSortOrder.Ascending;
    }

    public GridGroupByField(string fieldName)
    {
      this.fieldName = fieldName;
      this.sortOrder = RadSortOrder.Ascending;
    }

    public GridGroupByField(string fieldName, Type dataType)
    {
      this.fieldName = fieldName;
      this.dataType = dataType;
      this.sortOrder = RadSortOrder.Ascending;
    }

    [DefaultValue(typeof (GridAggregateFunction), "None")]
    [Browsable(true)]
    [Description("Gets or sets aggregate function that will be applied on the grouped data.")]
    [NotifyParentProperty(true)]
    public virtual GridAggregateFunction Aggregate
    {
      get
      {
        return this.aggregate;
      }
      set
      {
        this.SetProperty<GridAggregateFunction>(nameof (Aggregate), ref this.aggregate, value);
      }
    }

    [NotifyParentProperty(true)]
    [DefaultValue(null)]
    [Localizable(true)]
    public string HeaderText
    {
      get
      {
        if (!string.IsNullOrEmpty(this._headerText))
          return this._headerText;
        return this.FieldAlias;
      }
      set
      {
        if (!(this._headerText != value))
          return;
        this._headerText = value;
        this.OnPropertyChanged(nameof (HeaderText));
      }
    }

    [DefaultValue("{0}: {1};")]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public string FormatString
    {
      get
      {
        if (!string.IsNullOrEmpty(this.formatString))
          return this.formatString;
        return GridGroupByField.DefaultFormatString;
      }
      set
      {
        this.formatString = value;
      }
    }

    [NotifyParentProperty(true)]
    [DefaultValue("")]
    [Description("Gets or sets the name data source property or database column from a data source.")]
    [Browsable(true)]
    [Localizable(true)]
    public virtual string FieldName
    {
      get
      {
        return this.fieldName;
      }
      set
      {
        this.SetProperty<string>(nameof (FieldName), ref this.fieldName, value);
        this.ResetFieldAliasCache();
      }
    }

    [NotifyParentProperty(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(typeof (string))]
    public virtual Type DataType
    {
      get
      {
        return this.dataType;
      }
      set
      {
        this.SetProperty<Type>(nameof (DataType), ref this.dataType, value);
      }
    }

    [Description("Gets or sets a value representing a friendly name for the field used for forming the group by expression.")]
    [Browsable(true)]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    [DefaultValue("")]
    public virtual string FieldAlias
    {
      get
      {
        if (!string.IsNullOrEmpty(this.fieldAlias))
          return this.fieldAlias;
        return this.GenerateFieldAlias();
      }
      set
      {
        this.SetProperty<string>(nameof (FieldAlias), ref this.fieldAlias, value);
        this.ResetFieldAliasCache();
      }
    }

    protected string GenerateFieldAlias()
    {
      if (string.IsNullOrEmpty(this.tmpFieldAlias))
        this.tmpFieldAlias = this.aggregate != GridAggregateFunction.None ? string.Format("{0} of {1}", (object) this.aggregate, (object) this.fieldName) : this.fieldName;
      return this.tmpFieldAlias;
    }

    protected internal void ResetFieldAliasCache()
    {
      this.tmpFieldAlias = (string) null;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual bool IsFormatStringSet
    {
      get
      {
        return !string.IsNullOrEmpty(this.formatString);
      }
    }

    [DefaultValue(typeof (RadSortOrder), "None")]
    [Browsable(true)]
    [NotifyParentProperty(true)]
    [Description("Gets or sets the value indicating how the data will be sorted.")]
    public virtual RadSortOrder SortOrder
    {
      get
      {
        return this.sortOrder;
      }
      set
      {
        this.SetProperty<RadSortOrder>(nameof (SortOrder), ref this.sortOrder, value);
      }
    }

    public virtual void SetAggregate(string value)
    {
      try
      {
        this.Aggregate = (GridAggregateFunction) Enum.Parse(typeof (GridAggregateFunction), value, true);
      }
      catch
      {
        throw new FormatException(string.Format("Aggregate function {0} is unknown. Please check the expression syntax.", (object) value));
      }
    }

    [NotifyParentProperty(true)]
    [DefaultValue(null)]
    public virtual FilterExpression Filter
    {
      get
      {
        return this.tempFilter;
      }
      set
      {
        this.tempFilter = value;
      }
    }

    [DefaultValue("")]
    [NotifyParentProperty(true)]
    public virtual string Expression
    {
      get
      {
        return this.expression;
      }
      set
      {
        this.SetProperty<string>(nameof (Expression), ref this.expression, value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    public virtual object DataSourceNullValue
    {
      get
      {
        return this.dataSourceNullValue;
      }
      set
      {
        this.SetProperty<object>(nameof (DataSourceNullValue), ref this.dataSourceNullValue, value);
      }
    }

    [NotifyParentProperty(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    [Browsable(false)]
    public virtual object NullValue
    {
      get
      {
        return this.nullValue;
      }
      set
      {
        this.SetProperty<object>(nameof (NullValue), ref this.nullValue, value);
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsFieldAliasSet
    {
      get
      {
        return !string.IsNullOrEmpty(this.fieldAlias);
      }
    }

    internal object AggregateResult
    {
      get
      {
        return this.aggregateResult;
      }
      set
      {
        this.aggregateResult = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool IsHeaderTextSet
    {
      get
      {
        return !string.IsNullOrEmpty(this._headerText);
      }
    }

    internal string RelationName
    {
      get
      {
        return this._relationName;
      }
      set
      {
        this._relationName = value;
      }
    }

    public void SetSortOrder(string SortOrder)
    {
      try
      {
        this.SortOrder = (RadSortOrder) Enum.Parse(typeof (RadSortOrder), SortOrder);
      }
      catch
      {
        throw new FormatException("Sort order " + SortOrder + " is unknown. Please check the expression syntax.");
      }
    }

    public void CopyFrom(GridGroupByField field)
    {
      this.fieldName = field.fieldName;
      this.fieldAlias = field.fieldAlias;
      this.aggregate = field.aggregate;
      this.formatString = field.formatString;
      this._headerText = field._headerText;
      this.RelationName = field.RelationName;
      this.SortOrder = field.SortOrder;
    }

    public string GetHeaderText()
    {
      this.Validate();
      return this.HeaderText;
    }

    public bool IsReferredAs(string name)
    {
      if (string.IsNullOrEmpty(name))
        return false;
      if (string.Compare(name, this.fieldName, true) != 0)
        return string.Compare(name, this.fieldAlias, true) == 0;
      return true;
    }

    public string ToGroupByString()
    {
      return this.ToGroupByString(true);
    }

    public string ToGroupByString(bool preferAlias)
    {
      this.Validate();
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsAggregate)
        stringBuilder.AppendFormat("{0}([{1}])", (object) this.Aggregate.ToString().ToLower(), !this.IsFieldAliasSet || !preferAlias ? (object) this.FieldName : (object) this.FieldAlias);
      else
        stringBuilder.AppendFormat("[{0}]", !this.IsFieldAliasSet || !preferAlias ? (object) this.FieldName : (object) this.FieldAlias);
      if (this.SortOrder == RadSortOrder.Descending)
        stringBuilder.Append(" DESC");
      return stringBuilder.ToString();
    }

    public string ToSelectString()
    {
      return this.ToSelectString(string.Empty);
    }

    public string ToSelectString(string defaultFormatString)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (this.IsAggregate)
        stringBuilder.AppendFormat("{0}([{1}])", (object) this.Aggregate.ToString().ToLower(), (object) this.FieldName);
      else
        stringBuilder.AppendFormat("[{0}]", (object) this.FieldName);
      if (this.IsFieldAliasSet)
        stringBuilder.AppendFormat(" as [{0}]", (object) this.FieldAlias);
      string str = this.IsFormatStringSet ? this.formatString : defaultFormatString;
      if (!string.IsNullOrEmpty(str) && string.Compare(str, GridGroupByField.DefaultFormatString) == 0)
        str = (string) null;
      if (!string.IsNullOrEmpty(str))
        stringBuilder.AppendFormat(" format {0}", (object) GridGroupByField.UpdateFormatString(str, true, false));
      return stringBuilder.ToString();
    }

    public virtual string UpdateFormatString(bool doubleQuote, bool forceChange)
    {
      if (this.IsFormatStringSet)
        this.formatString = GridGroupByField.UpdateFormatString(this.formatString, doubleQuote, forceChange);
      return this.formatString;
    }

    public static string UpdateFormatString(
      string formatString,
      bool doubleQuote,
      bool forceChange)
    {
      string str = (string) null;
      switch (GridGroupByField.ValidateFormatString(formatString))
      {
        case 0:
          str = string.Format("'{0}'", (object) formatString);
          break;
        case 1:
          str = doubleQuote || !forceChange ? string.Format("'{0}'", (object) formatString) : string.Format("\"{0}\"", (object) formatString.Replace('"', '\''));
          break;
        case 2:
          str = !doubleQuote || !forceChange ? string.Format("\"{0}\"", (object) formatString) : string.Format("'{0}'", (object) formatString.Replace('\'', '"'));
          break;
        case 3:
          str = !doubleQuote ? string.Format("\"{0}\"", (object) formatString.Replace('"', '\'')) : string.Format("'{0}'", (object) formatString.Replace('\'', '"'));
          break;
      }
      return str;
    }

    public override string ToString()
    {
      this.Validate();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat(this.Aggregate == GridAggregateFunction.None ? "{1}([{0}])" : "[{0}]", (object) this.FieldName, (object) this.Aggregate.ToString().ToLower());
      return stringBuilder.ToString();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool IsAggregate
    {
      get
      {
        return this.aggregate != GridAggregateFunction.None;
      }
    }

    public virtual void Validate()
    {
      if (string.IsNullOrEmpty(this.FieldName))
        throw new FormatException("Field definition is not valid. Field name cannot be null or empty.");
      if (this.FieldName.IndexOfAny(GridGroupByField.InvalidChars) >= 0)
        throw new FormatException(string.Format("Field definition is not valid. FieldAlias contains invalid characters: \"{0}\"", (object) this.FieldAlias));
      if (this.IsFieldAliasSet && this.FieldAlias.IndexOfAny(GridGroupByField.InvalidChars) >= 0)
        throw new FormatException(string.Format("Field definition is not valid. FieldAlias contains invalid characters: \"{0}\"", (object) this.FieldAlias));
      if (this.ValidateFormatString() == 3)
        throw new FormatException("format string cannot contain both \" and ' symbols.");
    }

    public virtual int ValidateFormatString()
    {
      return GridGroupByField.ValidateFormatString(this.formatString);
    }

    public static int ValidateFormatString(string formatString)
    {
      if (string.IsNullOrEmpty(formatString))
        return 0;
      int num = 0 + (formatString.IndexOf('"') > 0 ? 1 : 0) + (formatString.IndexOf('\'') > 0 ? 2 : 0);
      if (num > 3)
        throw new FormatException("Invalid format string! Cannot generate output.");
      return num;
    }
  }
}
