// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.OperatorContext
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Globalization;

namespace Telerik.Data.Expressions
{
  public sealed class OperatorContext
  {
    private object row;
    private IFormatProvider formatProvider;
    private object expressionContext;
    private CompareOptions compareFlags;

    public CompareOptions CompareOptions
    {
      get
      {
        return this.compareFlags;
      }
    }

    public IFormatProvider FormatProvider
    {
      get
      {
        return this.formatProvider;
      }
    }

    public object Row
    {
      get
      {
        return this.row;
      }
    }

    public object ExpressionContext
    {
      get
      {
        return this.expressionContext;
      }
    }

    public OperatorContext(object row, IFormatProvider formatProvider, object expressionContext)
    {
      this.row = row;
      this.formatProvider = formatProvider;
      this.expressionContext = expressionContext;
    }

    public OperatorContext(
      object row,
      IFormatProvider formatProvider,
      object expressionContext,
      CompareOptions compareFlags)
    {
      this.row = row;
      this.formatProvider = formatProvider;
      this.expressionContext = expressionContext;
      this.compareFlags = compareFlags;
    }
  }
}
