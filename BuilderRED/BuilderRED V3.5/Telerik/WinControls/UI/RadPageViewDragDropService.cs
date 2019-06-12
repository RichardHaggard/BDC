// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI.Properties;

namespace Telerik.WinControls.UI
{
  public class RadPageViewDragDropService : RadDragDropService
  {
    private RadPageViewElement owner;
    private RadLayeredWindow insertHint;
    private RadPageViewItem dragItem;
    private RadImageShape insertHintImage;
    private static Cursor DefaultValidCursor;

    public RadPageViewDragDropService(RadPageViewElement owner)
    {
      this.owner = owner;
      this.ValidCursor = RadPageViewDragDropService.DefaultValidCursor;
    }

    static RadPageViewDragDropService()
    {
      if (Telerik\u002EWinControls\u002EUI\u002EResources.cursor_move != null)
        RadPageViewDragDropService.DefaultValidCursor = CursorHelper.CursorFromBitmap(Telerik\u002EWinControls\u002EUI\u002EResources.cursor_move, new Point(1, 1));
      else
        RadPageViewDragDropService.DefaultValidCursor = Cursors.SizeAll;
    }

    protected override void PerformStart()
    {
      this.dragItem = this.Context as RadPageViewItem;
      this.insertHintImage = this.owner.ItemDragHint;
      if (this.owner.ItemDragMode == PageViewItemDragMode.Preview)
        this.PrepareInsertHint();
      base.PerformStart();
    }

    protected override void HandleMouseMove(Point mousePos)
    {
      if (this.owner.ItemDragMode == PageViewItemDragMode.Immediate)
      {
        this.DoImmediateDrag(mousePos);
      }
      else
      {
        base.HandleMouseMove(mousePos);
        if (this.insertHint == null)
          return;
        this.UpdateHintImage(mousePos);
      }
    }

    protected override void PerformStop()
    {
      base.PerformStop();
      this.owner.EndItemDrag(this.dragItem);
      this.dragItem = (RadPageViewItem) null;
      this.insertHintImage = (RadImageShape) null;
      if (this.insertHint == null)
        return;
      this.insertHint.Dispose();
      this.insertHint = (RadLayeredWindow) null;
    }

    private void DoImmediateDrag(Point mousePos)
    {
      PageViewLayoutInfo itemLayoutInfo = this.owner.ItemLayoutInfo;
      if (itemLayoutInfo == null)
        return;
      Point client = this.owner.ElementTree.Control.PointToClient(mousePos);
      RadPageViewItem hitItem = this.owner.ItemFromPoint(client);
      if (hitItem == null || hitItem == this.dragItem)
        return;
      RectangleF boundingRectangle1 = (RectangleF) this.dragItem.ControlBoundingRectangle;
      RectangleF boundingRectangle2 = (RectangleF) hitItem.ControlBoundingRectangle;
      if (!(!itemLayoutInfo.vertical ? ((double) boundingRectangle2.X <= (double) boundingRectangle1.X ? (double) client.X < (double) boundingRectangle2.X + (double) boundingRectangle1.Width : (double) client.X > (double) boundingRectangle2.Right - (double) boundingRectangle1.Width) : ((double) boundingRectangle2.Y <= (double) boundingRectangle1.Y ? (double) client.Y < (double) boundingRectangle2.Y + (double) boundingRectangle1.Height : (double) client.Y > (double) boundingRectangle2.Bottom - (double) boundingRectangle1.Height)))
        return;
      this.owner.PerformItemDrop(this.dragItem, hitItem);
      this.owner.UpdateLayout();
    }

    private void PrepareInsertHint()
    {
      if (this.insertHintImage == null || this.insertHintImage.Image == null)
        return;
      PageViewLayoutInfo itemLayoutInfo = this.owner.ItemLayoutInfo;
      RectangleF itemsRect = this.owner.GetItemsRect();
      int num = itemLayoutInfo.vertical ? (int) itemsRect.Size.Width : (int) itemsRect.Size.Height;
      if (num <= 0)
        return;
      Size size1 = this.insertHintImage.Image.Size;
      Size size2 = !itemLayoutInfo.vertical ? new Size(size1.Width, num + this.insertHintImage.Margins.Vertical) : new Size(num + this.insertHintImage.Margins.Horizontal, size1.Height);
      Bitmap bitmap = new Bitmap(size2.Width, size2.Height);
      Graphics g = Graphics.FromImage((Image) bitmap);
      this.insertHintImage.Paint(g, new RectangleF(PointF.Empty, (SizeF) size2));
      g.Dispose();
      this.insertHint = new RadLayeredWindow();
      this.insertHint.BackgroundImage = (Image) bitmap;
    }

    private void UpdateHintImage(Point mousePos)
    {
      if (!this.CanCommit)
      {
        this.insertHint.Visible = false;
      }
      else
      {
        Rectangle screen = this.owner.ElementTree.Control.RectangleToScreen(this.owner.ItemFromPoint(this.owner.ElementTree.Control.PointToClient(mousePos)).ControlBoundingRectangle);
        Size size = this.insertHintImage.Image.Size;
        Padding margins = this.insertHintImage.Margins;
        this.insertHint.ShowWindow(!this.owner.ItemLayoutInfo.vertical ? new Point(screen.X - size.Width / 2, screen.Y - margins.Top) : new Point(screen.X - margins.Left, screen.Y - size.Height / 2));
      }
    }
  }
}
