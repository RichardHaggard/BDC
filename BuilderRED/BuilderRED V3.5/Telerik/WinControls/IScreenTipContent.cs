// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.IScreenTipContent
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Elements;

namespace Telerik.WinControls
{
  public interface IScreenTipContent
  {
    RadItemReadOnlyCollection TipItems { get; }

    string Description { get; set; }

    Type TemplateType { get; set; }

    Size TipSize { get; }
  }
}
