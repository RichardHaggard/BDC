// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SelectorCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.WinControls
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class SelectorCollection : List<IElementSelector>
  {
    public SelectorCollection()
      : base(1)
    {
    }

    public SelectorCollection(int capacity)
      : base(capacity)
    {
    }
  }
}
