// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.InvalidLengthException
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class InvalidLengthException : ValidateException
  {
    public InvalidLengthException(int length)
      : base(InvalidLengthException.Format(length))
    {
    }

    public InvalidLengthException(string message)
      : base(message)
    {
    }

    private static string Format(int length)
    {
      return string.Format("The length cannot exceed {0} characters!", (object) length);
    }
  }
}
