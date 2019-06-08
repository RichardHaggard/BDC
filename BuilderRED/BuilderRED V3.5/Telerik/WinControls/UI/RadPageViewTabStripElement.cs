// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewTabStripElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadPageViewTabStripElement : RadPageViewStripElement
  {
    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ItemDragService = (RadDragDropService) new RadPageViewInDockDragDropService((RadPageViewElement) this);
      this.ItemDragMode = PageViewItemDragMode.Preview;
    }

    protected internal override void OnItemMouseDown(RadPageViewItem sender, MouseEventArgs e)
    {
      if (e.Button != this.ActionMouseButton && e.Button != MouseButtons.Left && e.Button != MouseButtons.Right || e.Clicks != 1)
        return;
      if (!sender.IsSelected)
      {
        this.SelectItem(sender);
      }
      else
      {
        if (!this.EnsureSelectedItemVisible)
          return;
        this.EnsureItemVisible(sender);
      }
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      RadPageViewItem dragItem = dragObject as RadPageViewItem;
      if (dragItem == null || dragItem.Owner != this)
        return false;
      Point mousePosition1 = this.GetMousePosition(mousePosition);
      RadPageViewItem hitItem = this.ItemFromPoint(mousePosition1);
      if (!this.CanDropOverItem(dragItem, hitItem))
        return false;
      this.EnsureItemVisible(hitItem);
      this.ItemsParent.UpdateLayout();
      return hitItem.ControlBoundingRectangle.Contains(mousePosition1);
    }

    protected override void ProcessDragDrop(Point dropLocation, ISupportDrag dragObject)
    {
      RadPageViewItem dragItem = dragObject as RadPageViewItem;
      if (dragItem == null)
        return;
      RadPageViewItem hitItem = this.ItemFromPoint(this.GetMousePosition(dropLocation));
      if (hitItem == null)
        return;
      this.PerformItemDrop(dragItem, hitItem);
    }

    protected internal override void OnItemDrag(RadPageViewItem sender, MouseEventArgs e)
    {
      if (this.ItemDragMode == PageViewItemDragMode.None)
        return;
      this.StartItemDrag(sender);
    }

    private Point GetMousePosition(Point mousePosition)
    {
      Point mousePosition1 = Control.MousePosition;
      return !this.IsInValidState(true) ? mousePosition : this.ElementTree.Control.PointToClient(mousePosition1);
    }
  }
}
