﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Zip.CentralDirectoryHeaderInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Zip
{
  internal class CentralDirectoryHeaderInfo : FileHeaderInfoBase
  {
    public CentralDirectoryHeaderInfo(CentralDirectoryHeader header)
      : base((FileHeaderBase) header)
    {
      this.LocalHeaderOffsetOverflow = header.LocalHeaderOffset == uint.MaxValue;
    }

    public bool LocalHeaderOffsetOverflow { get; private set; }
  }
}
