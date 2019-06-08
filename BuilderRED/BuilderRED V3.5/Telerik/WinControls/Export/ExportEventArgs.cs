// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.ExportEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.Export
{
  public class ExportEventArgs : EventArgs
  {
    private IPdfEditor editor;
    private RectangleF rectangle;

    public ExportEventArgs(IPdfEditor editor, RectangleF rectangle)
    {
      this.editor = editor;
      this.rectangle = rectangle;
    }

    public IPdfEditor Editor
    {
      get
      {
        return this.editor;
      }
    }

    public RectangleF Rectangle
    {
      get
      {
        return this.rectangle;
      }
    }
  }
}
