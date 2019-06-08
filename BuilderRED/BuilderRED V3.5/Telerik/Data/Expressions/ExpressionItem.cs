// Decompiled with JetBrains decompiler
// Type: Telerik.Data.Expressions.ExpressionItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.Data.Expressions
{
  [Serializable]
  public class ExpressionItem
  {
    private string name;
    private string value;
    private string description;
    private string syntax;
    private ExpressionItemType type;

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public string Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }

    public string Description
    {
      get
      {
        return this.description;
      }
      set
      {
        this.description = value;
      }
    }

    public string Syntax
    {
      get
      {
        return this.syntax;
      }
      set
      {
        this.syntax = value;
      }
    }

    public ExpressionItemType Type
    {
      get
      {
        return this.type;
      }
      set
      {
        this.type = value;
      }
    }

    public override string ToString()
    {
      return string.Format("ExpressionItem: {0}", (object) this.syntax);
    }
  }
}
