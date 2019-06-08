// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.FilterExpressionEnumerator
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.Data
{
  public class FilterExpressionEnumerator : IEnumerator<FilterExpression>, IDisposable, IEnumerator
  {
    private int currentIndex = -1;
    private List<FilterExpression> expressions;

    public FilterExpressionEnumerator(List<FilterExpression> expressions)
    {
      this.expressions = expressions;
    }

    public FilterExpression Current
    {
      get
      {
        return this.expressions[this.currentIndex];
      }
    }

    public void Dispose()
    {
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.expressions[this.currentIndex];
      }
    }

    public bool MoveNext()
    {
      ++this.currentIndex;
      return this.currentIndex < this.expressions.Count;
    }

    public void Reset()
    {
      this.currentIndex = -1;
    }
  }
}
