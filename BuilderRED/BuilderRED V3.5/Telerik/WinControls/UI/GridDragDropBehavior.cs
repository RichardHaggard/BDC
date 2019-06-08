// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridDragDropBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public abstract class GridDragDropBehavior : IGridDragDropBehavior
  {
    private RadGridViewElement gridViewElement;

    public virtual void Initialize(RadGridViewElement gridViewElement)
    {
      this.gridViewElement = gridViewElement;
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
    }

    public abstract RadImageShape DragHint { get; }

    public abstract Point GetDragHintLocation(ISupportDrop dropTarget, Point mousePosition);

    public abstract Size GetDragHintSize(ISupportDrop dropTarget);

    public virtual void UpdateDropContext(
      ISupportDrag draggedContext,
      ISupportDrop dropTarget,
      Point? location)
    {
    }
  }
}
