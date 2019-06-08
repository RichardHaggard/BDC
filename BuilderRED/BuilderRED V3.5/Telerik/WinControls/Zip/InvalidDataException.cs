// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.InvalidDataException
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Zip
{
  public sealed class InvalidDataException : Exception
  {
    public InvalidDataException()
      : base("Invalid data")
    {
    }

    public InvalidDataException(string message)
      : base(message)
    {
    }

    public InvalidDataException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
