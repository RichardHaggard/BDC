// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Design.TelerikToolboxCategoryAttribute
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Design
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public sealed class TelerikToolboxCategoryAttribute : Attribute
  {
    private string categoryTitle;

    public string CategoryTitle
    {
      get
      {
        return this.categoryTitle;
      }
      set
      {
        this.categoryTitle = value;
      }
    }

    public TelerikToolboxCategoryAttribute(string categoryTitle)
    {
      this.CategoryTitle = categoryTitle;
    }
  }
}
