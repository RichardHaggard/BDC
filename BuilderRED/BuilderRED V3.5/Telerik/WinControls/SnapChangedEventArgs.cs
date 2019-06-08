// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.SnapChangedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class SnapChangedEventArgs : EventArgs
  {
    public readonly RadShapeEditorControl.SnapTypes param;

    public SnapChangedEventArgs(RadShapeEditorControl.SnapTypes data)
    {
      this.param = data;
    }
  }
}
