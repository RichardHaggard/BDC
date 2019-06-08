// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadDragDropEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class RadDragDropEventArgs : RadDragEventArgs
  {
    private ISupportDrop hitTarget;

    public RadDragDropEventArgs(ISupportDrag dragInstance, ISupportDrop hitTarget)
      : base(dragInstance)
    {
      this.hitTarget = hitTarget;
    }

    public ISupportDrop HitTarget
    {
      get
      {
        return this.hitTarget;
      }
    }
  }
}
