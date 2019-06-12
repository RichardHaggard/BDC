// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadDragDropService
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls
{
  public class RadDragDropService : RadService, IMessageListener
  {
    private static readonly object PreviewDragOverEventKey = new object();
    private static readonly object PreviewDragDropEventKey = new object();
    private static readonly object PreviewDropTargetEventKey = new object();
    private static readonly object PreviewDragStartEventKey = new object();
    private static readonly object PreviewDragHintEventKey = new object();
    private static Cursor DefaultValidCursor = ResourceHelper.CursorFromResource(typeof (RadDragDropService), "Telerik.WinControls.Resources.Cursors." + "DragMove.cur");
    private Cursor validCursor;
    private Cursor invalidCursor;
    private RadLayeredWindow hintWindow;
    protected bool messageFilterAdded;
    protected int xOutlineFormOffset;
    protected int yOutlineFormOffset;
    protected Point? beginPoint;
    private Point dropLocation;
    private ISupportDrop target;
    private bool doCommit;
    private bool initialized;
    private bool useDefaultPreview;
    private Cursor originalMouseCursor;
    private bool startedProgramatically;

    public RadDragDropService()
    {
      this.validCursor = this.ValidCursor;
      this.hintWindow = new RadLayeredWindow();
      this.hintWindow.TopMost = true;
      this.hintWindow.Alpha = 0.7f;
      this.hintWindow.HitTestable = false;
      this.useDefaultPreview = true;
    }

    protected override void DisposeManagedResources()
    {
      if (this.hintWindow != null)
        this.hintWindow.Dispose();
      base.DisposeManagedResources();
    }

    public event EventHandler<RadDropEventArgs> PreviewDragDrop
    {
      add
      {
        this.Events.AddHandler(RadDragDropService.PreviewDragDropEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDragDropService.PreviewDragDropEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadDragOverEventArgs> PreviewDragOver
    {
      add
      {
        this.Events.AddHandler(RadDragDropService.PreviewDragOverEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDragDropService.PreviewDragOverEventKey, (Delegate) value);
      }
    }

    public event EventHandler<PreviewDropTargetEventArgs> PreviewDropTarget
    {
      add
      {
        this.Events.AddHandler(RadDragDropService.PreviewDropTargetEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDragDropService.PreviewDropTargetEventKey, (Delegate) value);
      }
    }

    public event EventHandler<PreviewDragStartEventArgs> PreviewDragStart
    {
      add
      {
        this.Events.AddHandler(RadDragDropService.PreviewDragStartEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDragDropService.PreviewDragStartEventKey, (Delegate) value);
      }
    }

    public event EventHandler<PreviewDragHintEventArgs> PreviewDragHint
    {
      add
      {
        this.Events.AddHandler(RadDragDropService.PreviewDragHintEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDragDropService.PreviewDragHintEventKey, (Delegate) value);
      }
    }

    protected bool CanCommit
    {
      get
      {
        return this.doCommit;
      }
    }

    public Cursor ValidCursor
    {
      get
      {
        if (this.validCursor != (Cursor) null)
          return this.validCursor;
        return RadDragDropService.DefaultValidCursor;
      }
      set
      {
        this.validCursor = value;
      }
    }

    protected bool Initialized
    {
      get
      {
        return this.initialized;
      }
    }

    public Cursor InvalidCursor
    {
      get
      {
        if (this.invalidCursor != (Cursor) null)
          return this.invalidCursor;
        return Cursors.No;
      }
      set
      {
        this.invalidCursor = value;
      }
    }

    [DefaultValue(true)]
    public bool UseDefaultPreview
    {
      get
      {
        return this.useDefaultPreview;
      }
      set
      {
        this.useDefaultPreview = value;
      }
    }

    public ISupportDrop DropTarget
    {
      get
      {
        return this.target;
      }
    }

    public Point DropLocation
    {
      get
      {
        return this.dropLocation;
      }
    }

    protected RadLayeredWindow HintWindow
    {
      get
      {
        return this.hintWindow;
      }
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
      if (this.startedProgramatically)
        return MessagePreviewResult.NotProcessed;
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
              MessagePreviewResult messagePreviewResult = this.initialized ? MessagePreviewResult.ProcessedNoDispatch : MessagePreviewResult.Processed;
              this.Stop(this.doCommit);
              return messagePreviewResult;
            case 256:
            case 260:
              if (msg.WParam.ToInt32() == 27)
              {
                this.Stop(false);
                return MessagePreviewResult.ProcessedNoDispatch;
              }
              break;
            case 512:
            case 534:
              Point mousePosition = Control.MousePosition;
              if (mousePosition != this.beginPoint.Value)
                this.HandleMouseMove(mousePosition);
              return MessagePreviewResult.ProcessedNoDispatch;
          }
          return MessagePreviewResult.Processed;
      }
    }

    protected virtual void HandleMouseMove(Point mousePos)
    {
      if (!this.initialized && RadDragDropService.ShouldBeginDrag(this.beginPoint.Value, mousePos))
        this.initialized = this.PrepareContext();
      if (!this.initialized)
        return;
      try
      {
        this.DoDrag(mousePos);
        this.beginPoint = new Point?(mousePos);
      }
      catch (Exception ex)
      {
      }
    }

    protected virtual bool PrepareContext()
    {
      this.originalMouseCursor = Cursor.Current;
      RadItem context1 = this.Context as RadItem;
      if (!context1.Capture)
        context1.Capture = true;
      ISupportDrag context2 = this.Context as ISupportDrag;
      PreviewDragHintEventArgs e = new PreviewDragHintEventArgs(context2);
      this.OnPreviewDragHint(e);
      if (e.DragHint == null && e.UseDefaultHint)
        e.DragHint = context2.GetDragHint();
      this.hintWindow.BackgroundImage = e.DragHint;
      Size size = e.DragHint != null ? e.DragHint.Size : (context1 == null ? Size.Empty : context1.Size);
      this.xOutlineFormOffset = size.Width / 2;
      this.yOutlineFormOffset = size.Height / 2;
      return true;
    }

    protected virtual void OnPreviewDragHint(PreviewDragHintEventArgs e)
    {
      EventHandler<PreviewDragHintEventArgs> eventHandler = this.Events[RadDragDropService.PreviewDragHintEventKey] as EventHandler<PreviewDragHintEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    void IMessageListener.PreviewWndProc(Message msg)
    {
      if (msg.Msg != 28)
        return;
      this.Stop(false);
    }

    void IMessageListener.PreviewSystemMessage(SystemMessage message, Message msg)
    {
      throw new NotImplementedException();
    }

    private void DoDrag(Point mousePt)
    {
      object context1 = this.Context;
      this.SetHintWindowPosition(mousePt);
      Point dropLocation = this.dropLocation;
      ISupportDrop dropTarget = this.GetDropTarget(mousePt, out this.dropLocation);
      if (dropTarget == null || !this.IsDropTargetValid(dropTarget))
      {
        Cursor.Current = this.InvalidCursor;
        this.doCommit = false;
      }
      else if (dropTarget != null)
      {
        ISupportDrag context2 = this.Context as ISupportDrag;
        if (this.target != dropTarget)
        {
          if (this.target != null)
            this.target.DragLeave(dropLocation, context2);
          this.target = dropTarget;
          this.target.DragEnter(this.dropLocation, context2);
        }
        this.doCommit = this.target.DragOver(this.dropLocation, context2);
        RadDragOverEventArgs e = new RadDragOverEventArgs(context2, this.target);
        e.CanDrop = this.doCommit;
        this.OnPreviewDragOver(e);
        this.doCommit = e.CanDrop;
        if (this.doCommit)
          Cursor.Current = this.ValidCursor;
        else
          Cursor.Current = this.InvalidCursor;
      }
      else
      {
        this.target = (ISupportDrop) null;
        this.doCommit = false;
      }
    }

    protected virtual bool IsDropTargetValid(ISupportDrop dropTarget)
    {
      return dropTarget != this.Context;
    }

    protected virtual void OnPreviewDragOver(RadDragOverEventArgs e)
    {
      EventHandler<RadDragOverEventArgs> eventHandler = this.Events[RadDragDropService.PreviewDragOverEventKey] as EventHandler<RadDragOverEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    private void SetInvalidTargetMouseCursor()
    {
      if (this.originalMouseCursor == (Cursor) null)
        this.originalMouseCursor = Cursor.Current;
      Cursor.Current = this.InvalidCursor;
    }

    private void RestoreOriginalMouseCursort()
    {
      if (!(this.originalMouseCursor != (Cursor) null))
        return;
      Cursor.Current = this.originalMouseCursor;
      this.originalMouseCursor = (Cursor) null;
    }

    protected virtual void SetHintWindowPosition(Point mousePt)
    {
      if (this.hintWindow.BackgroundImage == null)
        return;
      this.hintWindow.ShowWindow(new Point(mousePt.X - this.xOutlineFormOffset, mousePt.Y - this.yOutlineFormOffset));
    }

    public static bool ShouldBeginDrag(Point current, Point capture)
    {
      Size dragSize = SystemInformation.DragSize;
      return !new Rectangle(capture.X - dragSize.Width / 2, capture.Y - dragSize.Height / 2, dragSize.Width, dragSize.Height).Contains(current);
    }

    protected override bool CanStart(object context)
    {
      RadItem radItem = context as RadItem;
      if (radItem == null)
        return false;
      if (!this.beginPoint.HasValue)
        this.beginPoint = new Point?(Control.MousePosition);
      Point client = radItem.ElementTree.Control.PointToClient(this.beginPoint.Value);
      bool flag = ((ISupportDrag) radItem).CanDrag(client);
      PreviewDragStartEventArgs e = new PreviewDragStartEventArgs((ISupportDrag) radItem);
      e.CanStart = flag;
      this.OnPreviewDragStart(e);
      if (!e.CanStart)
        this.beginPoint = new Point?();
      return e.CanStart;
    }

    protected virtual void OnPreviewDragStart(PreviewDragStartEventArgs e)
    {
      EventHandler<PreviewDragStartEventArgs> eventHandler = this.Events[RadDragDropService.PreviewDragStartEventKey] as EventHandler<PreviewDragStartEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override void PerformStart()
    {
      if (this.messageFilterAdded)
        return;
      RadMessageFilter.Instance.AddListener((IMessageListener) this);
      this.messageFilterAdded = true;
    }

    protected override void Commit()
    {
      RadItem context = this.Context as RadItem;
      context.Capture = false;
      RadDropEventArgs e = new RadDropEventArgs((ISupportDrag) context, this.target, this.dropLocation);
      this.OnPreviewDragDrop(e);
      if (e.Handled)
        return;
      this.target.DragDrop(this.dropLocation, (ISupportDrag) context);
    }

    protected virtual void OnPreviewDragDrop(RadDropEventArgs e)
    {
      EventHandler<RadDropEventArgs> eventHandler = this.Events[RadDragDropService.PreviewDragDropEventKey] as EventHandler<RadDropEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual ISupportDrop GetDropTarget(
      Point mousePosition,
      out Point resultDropLocation)
    {
      return this.HitTestElementTree(this.ElementTreeFromPoint(mousePosition), mousePosition, out resultDropLocation);
    }

    private ComponentThemableElementTree ElementTreeFromPoint(
      Point mousePosition)
    {
      IntPtr handle = NativeMethods.WindowFromPoint(mousePosition.X, mousePosition.Y);
      if (handle == IntPtr.Zero)
        return (ComponentThemableElementTree) null;
      for (Control control = Control.FromHandle(handle); control != null; control = control.Parent)
      {
        IComponentTreeHandler componentTreeHandler = control as IComponentTreeHandler;
        if (componentTreeHandler != null)
          return componentTreeHandler.ElementTree;
      }
      return (ComponentThemableElementTree) null;
    }

    private ISupportDrop HitTestElementTree(
      ComponentThemableElementTree elementTree,
      Point screenMouse,
      out Point resultDropLocation)
    {
      if (elementTree == null)
      {
        resultDropLocation = Point.Empty;
        return (ISupportDrop) null;
      }
      resultDropLocation = Point.Empty;
      Point client = elementTree.Control.PointToClient(screenMouse);
      RadElement radElement1 = elementTree.GetElementAtPoint(client);
      ISupportDrag context = this.Context as ISupportDrag;
      for (; radElement1 != null; radElement1 = radElement1.Parent)
      {
        ISupportDrop hitTarget = radElement1 as ISupportDrop;
        ISupportDrop supportDrop = (ISupportDrop) null;
        if (hitTarget != null && hitTarget.AllowDrop)
        {
          RadElement radElement2 = hitTarget as RadElement;
          resultDropLocation = radElement2 == null ? client : radElement2.PointFromControl(client);
          supportDrop = hitTarget;
        }
        PreviewDropTargetEventArgs e = new PreviewDropTargetEventArgs(context, hitTarget);
        e.DropTarget = supportDrop;
        this.OnPreviewDropTarget(e);
        ISupportDrop dropTarget = e.DropTarget;
        if (dropTarget != null)
          return dropTarget;
      }
      return (ISupportDrop) null;
    }

    protected virtual void OnPreviewDropTarget(PreviewDropTargetEventArgs e)
    {
      EventHandler<PreviewDropTargetEventArgs> eventHandler = this.Events[RadDragDropService.PreviewDropTargetEventKey] as EventHandler<PreviewDropTargetEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override void PerformStop()
    {
      base.PerformStop();
      if (this.messageFilterAdded)
      {
        RadMessageFilter.Instance.RemoveListener((IMessageListener) this);
        this.messageFilterAdded = false;
      }
      if (this.initialized)
      {
        RadItem context = this.Context as RadItem;
        if (context != null && context.ElementState == ElementState.Loaded)
        {
          context.IsMouseDown = false;
          context.Capture = false;
          context.ElementTree.ComponentTreeHandler.Behavior.ItemCapture = (RadElement) null;
          context.ElementTree.ComponentTreeHandler.Behavior.selectedElement = (RadElement) null;
        }
      }
      this.beginPoint = new Point?();
      this.doCommit = false;
      this.dropLocation = Point.Empty;
      this.xOutlineFormOffset = 0;
      this.yOutlineFormOffset = 0;
      this.hintWindow.Visible = false;
      this.hintWindow.BackgroundImage = (Image) null;
      this.target = (ISupportDrop) null;
      this.initialized = false;
      this.startedProgramatically = false;
      this.RestoreOriginalMouseCursort();
    }

    public void BeginDrag(Point mouseBeginPoint, ISupportDrag draggedObject)
    {
      if (this.State == RadServiceState.Working)
        return;
      this.beginPoint = new Point?(mouseBeginPoint);
      this.startedProgramatically = true;
      this.Start((object) draggedObject);
      if (this.State != RadServiceState.Working)
        return;
      this.PrepareContext();
      this.initialized = true;
    }

    public void EndDrag(Point mouseEndPoint, RadControl targetControl)
    {
      if (this.State == RadServiceState.Stopped)
        return;
      this.target = this.HitTestElementTree(targetControl.ElementTree, mouseEndPoint, out this.dropLocation);
      if (this.target != null)
        this.Stop(true);
      else
        this.Stop(false);
    }

    public void EndDrag()
    {
      if (this.State == RadServiceState.Stopped)
        return;
      this.Stop(this.CanCommit);
    }

    public void DoMouseMove(Point mousePos)
    {
      this.HandleMouseMove(mousePos);
    }
  }
}
