// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ModifiedValue
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  internal class ModifiedValue
  {
    private object _animatedValue;
    private object _baseValue;
    private object _coercedValue;
    private object _expressionValue;

    internal object AnimatedValue
    {
      get
      {
        return this._animatedValue;
      }
      set
      {
        this._animatedValue = value;
      }
    }

    internal object BaseValue
    {
      get
      {
        return this._baseValue;
      }
      set
      {
        this._baseValue = value;
      }
    }

    internal object CoercedValue
    {
      get
      {
        return this._coercedValue;
      }
      set
      {
        this._coercedValue = value;
      }
    }

    internal object ExpressionValue
    {
      get
      {
        return this._expressionValue;
      }
      set
      {
        this._expressionValue = value;
      }
    }
  }
}
