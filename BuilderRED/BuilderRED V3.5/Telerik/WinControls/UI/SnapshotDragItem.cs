// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SnapshotDragItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class SnapshotDragItem : RadItem
  {
    private ISupportDrag supportDrag;
    private object context;
    private Image hint;
    private Rectangle controlBoundingRectangle;
    private ComponentThemableElementTree elementTree;

    public SnapshotDragItem(RadItem item)
    {
      this.supportDrag = (ISupportDrag) item;
      this.context = this.supportDrag.GetDataContext();
      this.hint = this.InitializeDragHint();
      this.AllowDrag = this.supportDrag.AllowDrag;
      this.controlBoundingRectangle = item.ControlBoundingRectangle;
      this.elementTree = item.ElementTree;
    }

    protected virtual Image InitializeDragHint()
    {
      return this.supportDrag.GetDragHint();
    }

    public RadItem Item
    {
      get
      {
        return (RadItem) this.supportDrag;
      }
    }

    public override Rectangle ControlBoundingRectangle
    {
      get
      {
        return this.controlBoundingRectangle;
      }
    }

    public override ComponentThemableElementTree ElementTree
    {
      get
      {
        return this.elementTree;
      }
    }

    protected void SetElementTree(ComponentThemableElementTree value)
    {
      this.elementTree = value;
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      return this.supportDrag.CanDrag(dragStartPoint);
    }

    protected override object GetDragContextCore()
    {
      return this.context;
    }

    protected override Image GetDragHintCore()
    {
      return this.hint;
    }
  }
}
