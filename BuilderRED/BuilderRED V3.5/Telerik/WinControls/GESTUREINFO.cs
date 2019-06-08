// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.GESTUREINFO
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public struct GESTUREINFO
  {
    public uint cbSize;
    public uint dwFlags;
    public uint dwID;
    public IntPtr hwndTarget;
    public POINTS ptsLocation;
    public uint dwInstanceID;
    public uint dwSequenceID;
    public ulong ullArguments;
    public uint cbExtraArgs;
  }
}
