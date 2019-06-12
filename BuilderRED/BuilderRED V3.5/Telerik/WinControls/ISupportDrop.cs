// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ISupportDrop
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls
{
  public interface ISupportDrop
  {
    bool AllowDrop { get; }

    void DragDrop(Point dropLocation, ISupportDrag dragObject);

    bool DragOver(Point currentMouseLocation, ISupportDrag dragObject);

    void DragEnter(Point currentMouseLocation, ISupportDrag dragObject);

    void DragLeave(Point oldMouseLocation, ISupportDrag dragObject);
  }
}
