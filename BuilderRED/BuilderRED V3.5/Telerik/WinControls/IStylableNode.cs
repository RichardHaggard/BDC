// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IStylableNode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls
{
  public interface IStylableNode
  {
    IStylableNode Parent { get; }

    IEnumerable<RadObject> ChildrenHierarchy { get; }

    IEnumerable<RadObject> Children { get; }

    string Class { get; }

    StyleSheet Style { get; set; }

    Type GetThemeEffectiveType();

    void ApplySettings(PropertySettingGroup group);
  }
}
