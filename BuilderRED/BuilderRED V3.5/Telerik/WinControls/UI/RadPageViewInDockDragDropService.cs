// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewInDockDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadPageViewInDockDragDropService : RadPageViewDragDropService, IMessageListener
  {
    protected RadPageViewElement owner;
    protected bool processing;

    public RadPageViewInDockDragDropService(RadPageViewElement owner)
      : base(owner)
    {
      this.owner = owner;
    }

    InstalledHook IMessageListener.DesiredHook
    {
      get
      {
        return InstalledHook.GetMessage | InstalledHook.CallWndProc;
      }
    }

    MessagePreviewResult IMessageListener.PreviewMessage(
      ref Message msg)
    {
      switch (this.State)
      {
        case RadServiceState.Initial:
        case RadServiceState.Stopped:
          RadMessageFilter.Instance.RemoveListener((IMessageListener) this);
          this.messageFilterAdded = false;
          return MessagePreviewResult.NotProcessed;
        default:
          switch (msg.Msg)
          {
            case 162:
            case 514:
              this.Stop(this.CanCommit);
              return MessagePreviewResult.Processed;
            case 256:
            case 260:
              if (msg.WParam.ToInt32() == 27)
              {
                this.Stop(false);
                return MessagePreviewResult.Processed;
              }
              break;
            case 512:
            case 534:
              Point mousePosition = Control.MousePosition;
              if (mousePosition != this.beginPoint.Value)
                this.HandleMouseMove(mousePosition);
              return MessagePreviewResult.Processed;
          }
          return MessagePreviewResult.Processed;
      }
    }

    void IMessageListener.PreviewWndProc(Message msg)
    {
      if (msg.Msg != 28)
        return;
      this.Stop(false);
    }

    void IMessageListener.PreviewSystemMessage(SystemMessage message, Message msg)
    {
    }

    protected override void PerformStart()
    {
      this.processing = true;
      base.PerformStart();
    }

    protected override void HandleMouseMove(Point mousePos)
    {
      base.HandleMouseMove(mousePos);
      this.UpdateCursor(mousePos);
    }

    public override bool AvailableAtDesignTime
    {
      get
      {
        return true;
      }
    }

    protected virtual void UpdateCursor(Point mousePos)
    {
      RadPageViewStripElement owner = this.owner as RadPageViewStripElement;
      if (owner == null)
        return;
      if (!this.processing)
      {
        Cursor.Current = Cursors.Default;
      }
      else
      {
        Point mousePosition = Control.MousePosition;
        Point pt = !this.owner.IsInValidState(true) ? mousePos : this.owner.ElementTree.Control.PointToClient(mousePosition);
        if (owner.ItemContainer.ControlBoundingRectangle.Contains(pt))
          return;
        Cursor.Current = Cursors.Default;
        this.HintWindow.BackgroundImage = (Image) null;
        this.HintWindow.Hide();
        this.processing = false;
      }
    }
  }
}
