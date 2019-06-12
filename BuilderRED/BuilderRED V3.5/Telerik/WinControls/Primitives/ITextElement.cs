// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.ITextElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Drawing;

namespace Telerik.WinControls.Primitives
{
  public interface ITextElement
  {
    string Text { get; }

    Color ForeColor { get; }

    Font Font { get; }

    TextFormat TextFormat { get; }

    object RawResource { get; set; }
  }
}
