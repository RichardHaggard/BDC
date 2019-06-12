// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSplitter
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.RadSplitterDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadSplitter : RadControl
  {
    private int thumbLength = 50;
    private static readonly object EVENT_MOVING = new object();
    private static readonly object EVENT_MOVED = new object();
    private const int DefaultThumbLength = 50;
    private const int defaultWidth = 3;
    private const int DRAW_END = 3;
    private const int DRAW_MOVE = 2;
    private const int DRAW_START = 1;
    private Point anchor;
    private BorderStyle borderStyle;
    private int initTargetSize;
    private int lastDrawSplit;
    private int maxSize;
    private int minExtra;
    private int minSize;
    private int splitSize;
    private Control splitTarget;
    private RadSplitter.SplitterMessageFilter splitterMessageFilter;
    private int splitterThickness;
    private SplitterElement splitterElement;
    private SplitterManager splitterManager;
    private SplitterCollapsedState collapsedState;
    private int restoreSize;

    public RadSplitter()
    {
      this.anchor = Point.Empty;
      this.splitSize = -1;
      this.splitterThickness = 3;
      this.lastDrawSplit = -1;
      this.SetStyle(ControlStyles.Selectable, false);
      this.TabStop = false;
      this.minSize = 0;
      this.minExtra = 0;
      this.Dock = DockStyle.Left;
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      this.splitterManager = SplitterManager.CreateManager(this.Parent);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SplitterElement SplitterElement
    {
      get
      {
        return this.splitterElement;
      }
    }

    public bool Collapse(SplitterCollapsedState target)
    {
      if (this.IsCollapsed)
      {
        if (target == SplitterCollapsedState.None)
          return this.Expand();
        return false;
      }
      if (target == SplitterCollapsedState.Previous)
        this.CollapseToPrev();
      if (target == SplitterCollapsedState.Next)
        this.CollapseToNext();
      return true;
    }

    public bool Expand()
    {
      this.collapsedState = SplitterCollapsedState.None;
      this.SplitPosition = this.restoreSize;
      if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
      {
        this.splitterElement.PrevArrow.Direction = Telerik.WinControls.ArrowDirection.Left;
        this.splitterElement.NextArrow.Direction = Telerik.WinControls.ArrowDirection.Right;
      }
      else
      {
        this.splitterElement.PrevArrow.Direction = Telerik.WinControls.ArrowDirection.Up;
        this.splitterElement.NextArrow.Direction = Telerik.WinControls.ArrowDirection.Down;
      }
      this.splitterElement.PrevNavigationButton.Visibility = ElementVisibility.Visible;
      this.splitterElement.NextNavigationButton.Visibility = ElementVisibility.Visible;
      return false;
    }

    public bool IsCollapsed
    {
      get
      {
        return this.collapsedState != SplitterCollapsedState.None;
      }
    }

    public SplitterCollapsedState CollapsedState
    {
      get
      {
        return this.collapsedState;
      }
    }

    private void CollapseToPrev()
    {
      this.collapsedState = SplitterCollapsedState.Previous;
      this.restoreSize = this.SplitPosition;
      this.SplitPosition = 0;
      this.splitterElement.PrevArrow.Direction = this.Dock == DockStyle.Left || this.Dock == DockStyle.Right ? Telerik.WinControls.ArrowDirection.Right : Telerik.WinControls.ArrowDirection.Down;
      this.splitterElement.NextNavigationButton.Visibility = ElementVisibility.Collapsed;
    }

    private void CollapseToNext()
    {
      this.collapsedState = SplitterCollapsedState.Next;
      this.restoreSize = this.SplitPosition;
      this.SplitPosition = int.MaxValue;
      this.splitterElement.NextArrow.Direction = this.Dock == DockStyle.Left || this.Dock == DockStyle.Right ? Telerik.WinControls.ArrowDirection.Left : Telerik.WinControls.ArrowDirection.Up;
      this.splitterElement.PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
    }

    private void RestoreArrows()
    {
      if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
      {
        this.splitterElement.PrevArrow.Direction = Telerik.WinControls.ArrowDirection.Left;
        this.splitterElement.NextArrow.Direction = Telerik.WinControls.ArrowDirection.Right;
      }
      else
      {
        this.splitterElement.PrevArrow.Direction = Telerik.WinControls.ArrowDirection.Up;
        this.splitterElement.NextArrow.Direction = Telerik.WinControls.ArrowDirection.Down;
      }
    }

    [DefaultValue(50)]
    public int ThumbLength
    {
      get
      {
        return this.thumbLength;
      }
      set
      {
        this.thumbLength = value;
      }
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.splitterElement = new SplitterElement();
      this.splitterElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      parent.Children.Add((RadElement) this.splitterElement);
      this.RestoreArrows();
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(6, 200));
      }
    }

    private void ApplySplitPosition()
    {
      this.SplitPosition = this.splitSize;
    }

    private RadSplitter.SplitData CalcSplitBounds()
    {
      RadSplitter.SplitData splitData = new RadSplitter.SplitData();
      Control target = this.FindTarget();
      splitData.target = target;
      if (target != null)
      {
        switch (target.Dock)
        {
          case DockStyle.Top:
          case DockStyle.Bottom:
            this.initTargetSize = target.Bounds.Height;
            break;
          case DockStyle.Left:
          case DockStyle.Right:
            this.initTargetSize = target.Bounds.Width;
            break;
        }
        Control parentInternal = this.ParentInternal;
        Control.ControlCollection controls = parentInternal.Controls;
        int count = controls.Count;
        int num1 = 0;
        int num2 = 0;
        for (int index = 0; index < count; ++index)
        {
          Control control = controls[index];
          if (control != target)
          {
            switch (control.Dock)
            {
              case DockStyle.Top:
              case DockStyle.Bottom:
                num2 += control.Height;
                continue;
              case DockStyle.Left:
              case DockStyle.Right:
                num1 += control.Width;
                continue;
              default:
                continue;
            }
          }
        }
        Size clientSize = parentInternal.ClientSize;
        this.maxSize = !this.Horizontal ? clientSize.Height - num2 - this.minExtra : clientSize.Width - num1 - this.minExtra;
        splitData.dockWidth = num1;
        splitData.dockHeight = num2;
      }
      return splitData;
    }

    private Rectangle CalcSplitLine(int splitSize, int minWeight)
    {
      Rectangle bounds1 = this.Bounds;
      Rectangle bounds2 = this.splitTarget.Bounds;
      switch (this.Dock)
      {
        case DockStyle.Top:
          if (bounds1.Height < minWeight)
            bounds1.Height = minWeight;
          bounds1.Y = bounds2.Y + splitSize;
          return bounds1;
        case DockStyle.Bottom:
          if (bounds1.Height < minWeight)
            bounds1.Height = minWeight;
          bounds1.Y = bounds2.Y + bounds2.Height - splitSize - bounds1.Height;
          return bounds1;
        case DockStyle.Left:
          if (bounds1.Width < minWeight)
            bounds1.Width = minWeight;
          bounds1.X = bounds2.X + splitSize;
          return bounds1;
        case DockStyle.Right:
          if (bounds1.Width < minWeight)
            bounds1.Width = minWeight;
          bounds1.X = bounds2.X + bounds2.Width - splitSize - bounds1.Width;
          return bounds1;
        default:
          return bounds1;
      }
    }

    private int CalcSplitSize()
    {
      Control target = this.FindTarget();
      if (target != null)
      {
        Rectangle bounds = target.Bounds;
        switch (this.Dock)
        {
          case DockStyle.Top:
          case DockStyle.Bottom:
            return bounds.Height;
          case DockStyle.Left:
          case DockStyle.Right:
            return bounds.Width;
        }
      }
      return -1;
    }

    private void DrawSplitBar(int mode)
    {
      if (mode != 1 && this.lastDrawSplit != -1)
      {
        this.DrawSplitHelper(this.lastDrawSplit);
        this.lastDrawSplit = -1;
      }
      else if (mode != 1 && this.lastDrawSplit == -1)
        return;
      if (mode != 3)
      {
        this.DrawSplitHelper(this.splitSize);
        this.lastDrawSplit = this.splitSize;
      }
      else
      {
        if (this.lastDrawSplit != -1)
          this.DrawSplitHelper(this.lastDrawSplit);
        this.lastDrawSplit = -1;
      }
    }

    internal virtual Control ParentInternal
    {
      get
      {
        return this.Parent;
      }
      set
      {
        if (this.Parent == value)
          return;
        if (value != null)
          value.Controls.Add((Control) this);
        else
          this.Parent.Controls.Remove((Control) this);
      }
    }

    private void DrawSplitHelper(int splitSize)
    {
      if (this.splitTarget == null)
        return;
      Rectangle rectangle = this.CalcSplitLine(splitSize, 3);
      IntPtr handle1 = this.ParentInternal.Handle;
      IntPtr dcEx = Telerik.WinControls.NativeMethods.GetDCEx(new HandleRef((object) this.ParentInternal, handle1), Telerik.WinControls.NativeMethods.NullHandleRef, 1026);
      IntPtr halftoneBrush = TelerikPaintHelper.CreateHalftoneBrush();
      IntPtr handle2 = Telerik.WinControls.NativeMethods.SelectObject(new HandleRef((object) this.ParentInternal, dcEx), new HandleRef((object) null, halftoneBrush));
      Telerik.WinControls.NativeMethods.PatBlt(new HandleRef((object) this.ParentInternal, dcEx), rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 5898313);
      Telerik.WinControls.NativeMethods.SelectObject(new HandleRef((object) this.ParentInternal, dcEx), new HandleRef((object) null, handle2));
      Telerik.WinControls.NativeMethods.DeleteObject(new HandleRef((object) null, halftoneBrush));
      Telerik.WinControls.NativeMethods.ReleaseDC(new HandleRef((object) this.ParentInternal, handle1), new HandleRef((object) null, dcEx));
    }

    private Control FindTarget()
    {
      Control parentInternal = this.ParentInternal;
      if (parentInternal != null)
      {
        Control.ControlCollection controls = parentInternal.Controls;
        int count = controls.Count;
        DockStyle dock = this.Dock;
        for (int index = 0; index < count; ++index)
        {
          Control control = controls[index];
          if (control != this)
          {
            switch (dock)
            {
              case DockStyle.Top:
                if (control.Bottom == this.Top)
                  return control;
                continue;
              case DockStyle.Bottom:
                if (control.Top == this.Bottom)
                  return control;
                continue;
              case DockStyle.Left:
                if (control.Right == this.Left)
                  return control;
                continue;
              case DockStyle.Right:
                if (control.Left == this.Right)
                  return control;
                continue;
              default:
                continue;
            }
          }
        }
      }
      return (Control) null;
    }

    private int GetSplitSize(int x, int y)
    {
      int num = !this.Horizontal ? y - this.anchor.Y : x - this.anchor.X;
      int val1 = 0;
      switch (this.Dock)
      {
        case DockStyle.Top:
          val1 = this.splitTarget.Height + num;
          break;
        case DockStyle.Bottom:
          val1 = this.splitTarget.Height - num;
          break;
        case DockStyle.Left:
          val1 = this.splitTarget.Width + num;
          break;
        case DockStyle.Right:
          val1 = this.splitTarget.Width - num;
          break;
      }
      return Math.Max(Math.Min(val1, this.maxSize), this.minSize);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (this.splitTarget == null || e.KeyCode != Keys.Escape)
        return;
      this.SplitEnd(false);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.IsCollapsed || this.ThumbHitTest(e.Location) || (e.Button != MouseButtons.Left || e.Clicks != 1))
        return;
      this.SplitBegin(e.X, e.Y);
    }

    public bool ThumbHitTest(Point pt)
    {
      Rectangle rectangle1 = new Rectangle(this.splitterElement.PrevNavigationButton.PointToControl(Point.Empty), this.splitterElement.PrevNavigationButton.Size);
      Rectangle rectangle2 = new Rectangle(this.splitterElement.NextNavigationButton.PointToControl(Point.Empty), this.splitterElement.NextNavigationButton.Size);
      return rectangle1.Contains(pt) || rectangle2.Contains(pt);
    }

    private RadItem GetThumbFromPoint(Point pt)
    {
      if (new Rectangle(this.splitterElement.PrevNavigationButton.PointToControl(Point.Empty), this.splitterElement.PrevNavigationButton.Size).Contains(pt))
        return this.splitterElement.PrevNavigationButton;
      if (new Rectangle(this.splitterElement.NextNavigationButton.PointToControl(Point.Empty), this.splitterElement.NextNavigationButton.Size).Contains(pt))
        return this.splitterElement.NextNavigationButton;
      return (RadItem) null;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.IsCollapsed || this.ThumbHitTest(e.Location))
      {
        this.Cursor = Cursors.Arrow;
      }
      else
      {
        this.Cursor = this.DefaultCursor;
        if (this.splitTarget == null)
          return;
        int x1 = e.X + this.Left;
        int y1 = e.Y + this.Top;
        Rectangle rectangle = this.CalcSplitLine(this.GetSplitSize(e.X, e.Y), 0);
        int x2 = rectangle.X;
        int y2 = rectangle.Y;
        this.OnSplitterMoving(new SplitterEventArgs(x1, y1, x2, y2));
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.ThumbHitTest(e.Location))
      {
        if (this.IsCollapsed)
        {
          this.Expand();
          return;
        }
        RadItem thumbFromPoint = this.GetThumbFromPoint(e.Location);
        if (thumbFromPoint == this.splitterElement.PrevNavigationButton)
          this.CollapseToPrev();
        if (thumbFromPoint == this.splitterElement.NextNavigationButton)
          this.CollapseToNext();
      }
      if (this.splitTarget == null)
        return;
      int x1 = e.X;
      int left = this.Left;
      int y1 = e.Y;
      int top = this.Top;
      Rectangle rectangle = this.CalcSplitLine(this.GetSplitSize(e.X, e.Y), 0);
      int x2 = rectangle.X;
      int y2 = rectangle.Y;
      this.SplitEnd(true);
    }

    protected virtual void OnSplitterMoved(SplitterEventArgs sevent)
    {
      SplitterEventHandler splitterEventHandler = (SplitterEventHandler) this.Events[RadSplitter.EVENT_MOVED];
      if (splitterEventHandler != null)
        splitterEventHandler((object) this, sevent);
      if (this.splitTarget == null)
        return;
      this.SplitMove(sevent.SplitX, sevent.SplitY);
    }

    protected virtual void OnSplitterMoving(SplitterEventArgs sevent)
    {
      SplitterEventHandler splitterEventHandler = (SplitterEventHandler) this.Events[RadSplitter.EVENT_MOVING];
      if (splitterEventHandler != null)
        splitterEventHandler((object) this, sevent);
      if (this.splitTarget == null)
        return;
      this.SplitMove(sevent.SplitX, sevent.SplitY);
    }

    protected override void SetBoundsCore(
      int x,
      int y,
      int width,
      int height,
      BoundsSpecified specified)
    {
      if (this.Horizontal)
      {
        if (width < 1)
          width = 3;
        this.splitterThickness = width;
      }
      else
      {
        if (height < 1)
          height = 3;
        this.splitterThickness = height;
      }
      base.SetBoundsCore(x, y, width, height, specified);
    }

    private void SplitBegin(int x, int y)
    {
      RadSplitter.SplitData splitData = this.CalcSplitBounds();
      if (splitData.target == null || this.minSize >= this.maxSize)
        return;
      this.anchor = new Point(x, y);
      this.splitTarget = splitData.target;
      this.splitSize = this.GetSplitSize(x, y);
      try
      {
        if (this.splitterMessageFilter != null)
          this.splitterMessageFilter = new RadSplitter.SplitterMessageFilter(this);
        Application.AddMessageFilter((IMessageFilter) this.splitterMessageFilter);
      }
      finally
      {
        CodeAccessPermission.RevertAssert();
      }
      this.Capture = true;
      this.DrawSplitBar(1);
    }

    private void SplitEnd(bool accept)
    {
      this.DrawSplitBar(3);
      this.splitTarget = (Control) null;
      this.Capture = false;
      if (this.splitterMessageFilter != null)
      {
        Application.RemoveMessageFilter((IMessageFilter) this.splitterMessageFilter);
        this.splitterMessageFilter = (RadSplitter.SplitterMessageFilter) null;
      }
      if (accept)
        this.ApplySplitPosition();
      else if (this.splitSize != this.initTargetSize)
        this.SplitPosition = this.initTargetSize;
      this.anchor = Point.Empty;
    }

    private void SplitMove(int x, int y)
    {
      int splitSize = this.GetSplitSize(x - this.Left + this.anchor.X, y - this.Top + this.anchor.Y);
      if (this.splitSize == splitSize)
        return;
      this.splitSize = splitSize;
      this.DrawSplitBar(2);
    }

    public override string ToString()
    {
      return base.ToString() + ", MinExtra: " + this.MinExtra.ToString((IFormatProvider) CultureInfo.CurrentCulture) + ", MinSize: " + this.MinSize.ToString((IFormatProvider) CultureInfo.CurrentCulture);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool AllowDrop
    {
      get
      {
        return base.AllowDrop;
      }
      set
      {
        base.AllowDrop = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(0)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override AnchorStyles Anchor
    {
      get
      {
        return AnchorStyles.None;
      }
      set
      {
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Image BackgroundImage
    {
      get
      {
        return base.BackgroundImage;
      }
      set
      {
        base.BackgroundImage = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override ImageLayout BackgroundImageLayout
    {
      get
      {
        return base.BackgroundImageLayout;
      }
      set
      {
        base.BackgroundImageLayout = value;
      }
    }

    [DefaultValue(0)]
    [DispId(-504)]
    public BorderStyle BorderStyle
    {
      get
      {
        return this.borderStyle;
      }
      set
      {
        if (!Telerik.WinControls.ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 2))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (BorderStyle));
        if (this.borderStyle == value)
          return;
        this.borderStyle = value;
        this.UpdateStyles();
      }
    }

    protected override CreateParams CreateParams
    {
      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)] get
      {
        CreateParams createParams = base.CreateParams;
        createParams.ExStyle &= -513;
        createParams.Style &= -8388609;
        switch (this.borderStyle)
        {
          case BorderStyle.FixedSingle:
            createParams.Style |= 8388608;
            return createParams;
          case BorderStyle.Fixed3D:
            createParams.ExStyle |= 512;
            return createParams;
          default:
            return createParams;
        }
      }
    }

    protected override Cursor DefaultCursor
    {
      get
      {
        switch (this.Dock)
        {
          case DockStyle.Top:
          case DockStyle.Bottom:
            return Cursors.HSplit;
          case DockStyle.Left:
          case DockStyle.Right:
            return Cursors.VSplit;
          default:
            return base.DefaultCursor;
        }
      }
    }

    protected override ImeMode DefaultImeMode
    {
      get
      {
        return ImeMode.Disable;
      }
    }

    [DefaultValue(DockStyle.Left)]
    [Localizable(true)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        if (value != DockStyle.Top && value != DockStyle.Bottom && (value != DockStyle.Left && value != DockStyle.Right))
          throw new ArgumentException(Telerik.WinControls.SR.GetString("SplitterInvalidDockEnum"));
        int splitterThickness = this.splitterThickness;
        this.splitterElement.Dock = base.Dock = value;
        switch (this.Dock)
        {
          case DockStyle.Top:
          case DockStyle.Bottom:
            if (this.splitterThickness != -1)
            {
              this.Height = splitterThickness;
              return;
            }
            break;
          case DockStyle.Left:
          case DockStyle.Right:
            if (this.splitterThickness != -1)
            {
              this.Width = splitterThickness;
              break;
            }
            break;
          default:
            return;
        }
        this.RestoreArrows();
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    private bool Horizontal
    {
      get
      {
        DockStyle dock = this.Dock;
        if (dock != DockStyle.Left)
          return dock == DockStyle.Right;
        return true;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new ImeMode ImeMode
    {
      get
      {
        return base.ImeMode;
      }
      set
      {
        base.ImeMode = value;
      }
    }

    [DefaultValue(0)]
    [Localizable(true)]
    public int MinExtra
    {
      get
      {
        return this.minExtra;
      }
      set
      {
        if (value < 0)
          value = 0;
        this.minExtra = value;
      }
    }

    [Localizable(true)]
    [DefaultValue(0)]
    public int MinSize
    {
      get
      {
        return this.minSize;
      }
      set
      {
        if (value < 0)
          value = 0;
        this.minSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SplitPosition
    {
      get
      {
        if (this.splitSize == -1)
          this.splitSize = this.CalcSplitSize();
        return this.splitSize;
      }
      set
      {
        RadSplitter.SplitData splitData = this.CalcSplitBounds();
        if (value > this.maxSize)
          value = this.maxSize;
        if (value < this.minSize)
          value = this.minSize;
        this.splitSize = value;
        this.DrawSplitBar(3);
        if (splitData.target == null)
        {
          this.splitSize = -1;
        }
        else
        {
          Rectangle bounds = splitData.target.Bounds;
          switch (this.Dock)
          {
            case DockStyle.Top:
              bounds.Height = value;
              break;
            case DockStyle.Bottom:
              bounds.Y += bounds.Height - this.splitSize;
              bounds.Height = value;
              break;
            case DockStyle.Left:
              bounds.Width = value;
              break;
            case DockStyle.Right:
              bounds.X += bounds.Width - this.splitSize;
              bounds.Width = value;
              break;
          }
          splitData.target.Bounds = bounds;
          Application.DoEvents();
          this.OnSplitterMoved(new SplitterEventArgs(this.Left, this.Top, this.Left + bounds.Width / 2, this.Top + bounds.Height / 2));
        }
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new bool TabStop
    {
      get
      {
        return base.TabStop;
      }
      set
      {
        base.TabStop = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Bindable(false)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler BackgroundImageChanged
    {
      add
      {
        base.BackgroundImageChanged += value;
      }
      remove
      {
        base.BackgroundImageChanged -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event EventHandler BackgroundImageLayoutChanged
    {
      add
      {
        base.BackgroundImageLayoutChanged += value;
      }
      remove
      {
        base.BackgroundImageLayoutChanged -= value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler Enter
    {
      add
      {
        base.Enter += value;
      }
      remove
      {
        base.Enter -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event EventHandler FontChanged
    {
      add
      {
        base.FontChanged += value;
      }
      remove
      {
        base.FontChanged -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event EventHandler ForeColorChanged
    {
      add
      {
        base.ForeColorChanged += value;
      }
      remove
      {
        base.ForeColorChanged -= value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler ImeModeChanged
    {
      add
      {
        base.ImeModeChanged += value;
      }
      remove
      {
        base.ImeModeChanged -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event KeyEventHandler KeyDown
    {
      add
      {
        base.KeyDown += value;
      }
      remove
      {
        base.KeyDown -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event KeyPressEventHandler KeyPress
    {
      add
      {
        base.KeyPress += value;
      }
      remove
      {
        base.KeyPress -= value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event KeyEventHandler KeyUp
    {
      add
      {
        base.KeyUp += value;
      }
      remove
      {
        base.KeyUp -= value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler Leave
    {
      add
      {
        base.Leave += value;
      }
      remove
      {
        base.Leave -= value;
      }
    }

    public event SplitterEventHandler SplitterMoved
    {
      add
      {
        this.Events.AddHandler(RadSplitter.EVENT_MOVED, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadSplitter.EVENT_MOVED, (Delegate) value);
      }
    }

    public event SplitterEventHandler SplitterMoving
    {
      add
      {
        this.Events.AddHandler(RadSplitter.EVENT_MOVING, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadSplitter.EVENT_MOVING, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new event EventHandler TabStopChanged
    {
      add
      {
        base.TabStopChanged += value;
      }
      remove
      {
        base.TabStopChanged -= value;
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new event EventHandler TextChanged
    {
      add
      {
        base.TextChanged += value;
      }
      remove
      {
        base.TextChanged -= value;
      }
    }

    private class SplitData
    {
      public int dockHeight;
      public int dockWidth;
      internal Control target;

      public SplitData()
      {
        this.dockWidth = -1;
        this.dockHeight = -1;
      }
    }

    private class SplitterMessageFilter : IMessageFilter
    {
      private RadSplitter owner;

      public SplitterMessageFilter(RadSplitter splitter)
      {
        this.owner = splitter;
      }

      [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
      public bool PreFilterMessage(ref Message m)
      {
        if (m.Msg < 256 || m.Msg > 264)
          return false;
        if (m.Msg == 256 && (int) (long) m.WParam == 27)
          this.owner.SplitEnd(false);
        return true;
      }
    }
  }
}
