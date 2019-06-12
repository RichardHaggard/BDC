// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CSSGroup
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class CSSGroup
  {
    public List<string> BasedOn = new List<string>();
    public List<CSSItem> items = new List<CSSItem>();

    public bool isRoot { get; set; }

    public string name { get; set; }

    public string childName { get; set; }

    public CSSItem this[string name]
    {
      get
      {
        foreach (CSSItem cssItem in this.items)
        {
          if (cssItem.name == name)
            return cssItem;
        }
        return (CSSItem) null;
      }
    }

    public bool Contains(string name)
    {
      foreach (CSSItem cssItem in this.items)
      {
        if (cssItem.name == name)
          return true;
      }
      return false;
    }
  }
}
