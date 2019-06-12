// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.TextPrimitiveFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Primitives
{
  public class TextPrimitiveFactory
  {
    public static ITextPrimitive CreateTextPrimitiveImp(bool htmlEnabled)
    {
      if (htmlEnabled)
        return (ITextPrimitive) new TextPrimitiveHtmlImpl();
      return (ITextPrimitive) new TextPrimitiveImpl();
    }
  }
}
