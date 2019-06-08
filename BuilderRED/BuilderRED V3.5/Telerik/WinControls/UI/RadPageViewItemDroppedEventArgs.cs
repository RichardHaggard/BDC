// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewItemDroppedEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class RadPageViewItemDroppedEventArgs : EventArgs
  {
    private RadPageViewItem draggedItem;
    private RadPageViewItem targetItem;

    public RadPageViewItemDroppedEventArgs(RadPageViewItem draggedItem, RadPageViewItem targetItem)
    {
      this.draggedItem = draggedItem;
      this.targetItem = targetItem;
    }

    public RadPageViewItem DraggedItem
    {
      get
      {
        return this.draggedItem;
      }
    }

    public RadPageViewItem TargetItem
    {
      get
      {
        return this.targetItem;
      }
    }
  }
}
