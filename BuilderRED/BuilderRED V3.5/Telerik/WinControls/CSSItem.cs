// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.CSSItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls
{
  public class CSSItem
  {
    public List<CSSItem> childItems = new List<CSSItem>();

    public string name { get; set; }

    public string value { get; set; }

    public string this[string name]
    {
      get
      {
        foreach (CSSItem childItem in this.childItems)
        {
          if (string.Compare(childItem.name, name, true) == 0)
            return childItem.value;
        }
        return (string) null;
      }
    }
  }
}
