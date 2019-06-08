// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.FunctionContext
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Data.Expressions
{
  internal class FunctionContext
  {
    private IFormatProvider formatProvider;
    private object globalContext;

    public IFormatProvider FormatProvider
    {
      get
      {
        return this.formatProvider;
      }
    }

    public object GlobalContext
    {
      get
      {
        return this.globalContext;
      }
    }

    public FunctionContext(IFormatProvider formatProvider, object globalContext)
    {
      this.formatProvider = formatProvider;
      this.globalContext = globalContext;
    }
  }
}
