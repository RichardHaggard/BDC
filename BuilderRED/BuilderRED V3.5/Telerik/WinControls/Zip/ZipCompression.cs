// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.ZipCompression
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.Zip
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  [Obsolete("This class has been deprecated. Use ZipArchive instead of ZipPackage.")]
  public enum ZipCompression
  {
    Default = -1,
    Stored = 0,
    BestSpeed = 1,
    Method2 = 2,
    Method3 = 3,
    Method4 = 4,
    Method5 = 5,
    Method6 = 6,
    Method7 = 7,
    Deflated = 8,
    Deflate64 = 9,
  }
}
