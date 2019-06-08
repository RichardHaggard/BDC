// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadSplitContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI.Docking;

namespace Telerik.WinControls.UI
{
  [System.Windows.Forms.Docking(DockingBehavior.Ask)]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadSplitContainerDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [TelerikToolboxCategory("Containers")]
  [RadToolboxItem(false)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  public class RadSplitContainer : SplitPanel
  {
    private int lastSplitterDistance = -1;
    private int splitterDistance = -1;
    private bool nothingToRestore = true;
    private static readonly object SplitterMovingEventKey = new object();
    private static readonly object SplitterMovedEventKey = new object();
    private static readonly object ChildPanelCollapsedChangedEventKey = new object();
    private const int DEFAULT_SPLITTER_SLOTS = 4;
    private bool customThemeClassName;
    private SplitterElement currentSplitter;
    private int beginSplitterDistance;
    private bool resizing;
    private bool isCleanUpTarget;
    private bool suspendButtonLayout;
    private Orientation orientation;
    private SplitPanelCollection panels;
    private SplitContainerLayoutStrategy layoutStrategy;
    private SplitContainerElement splitContainerElement;
    private SplitterCollection splitters;
    private bool immediateSplitterMove;

    public RadSplitContainer()
    {
      this.panels = new SplitPanelCollection(this);
      this.layoutStrategy = new SplitContainerLayoutStrategy();
    }

    public RadSplitContainer(Orientation orientation)
      : this()
    {
      this.Orientation = orientation;
    }

    protected override void Construct()
    {
      this.Orientation = Orientation.Vertical;
      base.Construct();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.splitContainerElement = new SplitContainerElement();
      this.splitContainerElement.ShouldPaint = false;
      int num = (int) this.splitContainerElement.SetValue(SplitContainerElement.IsVerticalProperty, (object) true);
      parent.Children.Add((RadElement) this.splitContainerElement);
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
      return (Control.ControlCollection) new RadSplitContainer.ControlCollection(this);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Layout")]
    [DefaultValue(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public int SplitterDistance
    {
      get
      {
        return this.splitterDistance;
      }
      set
      {
        this.splitterDistance = value;
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool EnableImmediateResize
    {
      get
      {
        return this.immediateSplitterMove;
      }
      set
      {
        if (this.immediateSplitterMove == value)
          return;
        this.immediateSplitterMove = value;
        foreach (RadSplitContainer childControl in ControlHelper.GetChildControls<RadSplitContainer>((Control) this, true))
          childControl.immediateSplitterMove = value;
      }
    }

    [Browsable(false)]
    public SplitContainerElement SplitContainerElement
    {
      get
      {
        return this.splitContainerElement;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SplitPanelCollection SplitPanels
    {
      get
      {
        return this.panels;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DefaultValue(false)]
    public bool IsCleanUpTarget
    {
      get
      {
        return this.isCleanUpTarget;
      }
      set
      {
        this.isCleanUpTarget = value;
      }
    }

    [Browsable(false)]
    public bool HasNonCollapsedChild
    {
      get
      {
        int count = this.panels.Count;
        bool flag = false;
        for (int index = 0; index < count; ++index)
        {
          if (!this.panels[index].Collapsed)
          {
            flag = true;
            break;
          }
        }
        return flag;
      }
    }

    [Browsable(false)]
    public virtual Rectangle ContentRectangle
    {
      get
      {
        return this.ClientRectangle;
      }
    }

    [Browsable(false)]
    public RadSplitContainer RootContainer
    {
      get
      {
        Control parent = this.Parent;
        while (parent != null && parent.Parent is RadSplitContainer)
          parent = parent.Parent;
        if (parent is RadSplitContainer)
          return (RadSplitContainer) parent;
        return this;
      }
    }

    [Browsable(false)]
    public bool HasVisibleSplitPanels
    {
      get
      {
        for (int index = 0; index < this.SplitPanels.Count; ++index)
        {
          if (!this.SplitPanels[index].Collapsed)
            return true;
        }
        return false;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(Orientation.Vertical)]
    [Localizable(true)]
    public Orientation Orientation
    {
      get
      {
        return this.orientation;
      }
      set
      {
        if (!Telerik.WinControls.ClientUtils.IsEnumValid((Enum) value, (int) value, 0, 1))
          throw new InvalidEnumArgumentException(nameof (value), (int) value, typeof (Orientation));
        if (this.orientation == value)
          return;
        this.orientation = value;
        this.OnOrientationChanged(EventArgs.Empty);
      }
    }

    [Description("Gets or sets the length of a single splitter on the container. Specify zero to prevent displaying any splitters at all.")]
    [RadPropertyDefaultValue("SplitterWidth", typeof (SplitContainerElement))]
    public virtual int SplitterWidth
    {
      get
      {
        return this.splitContainerElement.SplitterWidth;
      }
      set
      {
        this.splitContainerElement.SplitterWidth = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void ApplySplitterWidth(int splitterWidth)
    {
      foreach (SplitterElement child in this.splitContainerElement.Children)
      {
        int num = (int) child.SetDefaultValueOverride(SplitterElement.SplitterWidthProperty, (object) splitterWidth);
        this.ElementTree.ApplyThemeToElement((RadObject) child);
      }
      for (int index = 0; index < this.panels.Count; ++index)
      {
        RadSplitContainer panel = this.panels[index] as RadSplitContainer;
        if (panel != null)
        {
          int num = (int) panel.splitContainerElement.SetDefaultValueOverride(SplitContainerElement.SplitterWidthProperty, (object) splitterWidth);
        }
      }
      this.PerformLayout();
    }

    protected virtual void ApplyThemeToSplitterElements()
    {
      foreach (RadObject child in this.splitContainerElement.Children)
        this.ElementTree.ApplyThemeToElement(child);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SplitContainerLayoutStrategy LayoutStrategy
    {
      get
      {
        return this.layoutStrategy;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (LayoutStrategy));
        this.layoutStrategy = value;
        this.PerformLayout();
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SplitterCollection Splitters
    {
      get
      {
        if (this.splitters == null)
          this.splitters = new SplitterCollection(this);
        return this.splitters;
      }
    }

    internal int VisiblePanelCount
    {
      get
      {
        int num = 0;
        int count = this.panels.Count;
        for (int index = 0; index < count; ++index)
        {
          if (!this.panels[index].Collapsed)
            ++num;
        }
        return num;
      }
    }

    private List<SplitPanel> VisiblePanels
    {
      get
      {
        List<SplitPanel> splitPanelList = new List<SplitPanel>(this.panels.Count);
        for (int index = 0; index < this.panels.Count; ++index)
        {
          if (!this.panels[index].Collapsed)
            splitPanelList.Add(this.panels[index]);
        }
        return splitPanelList;
      }
    }

    [Browsable(false)]
    public override string ThemeClassName
    {
      get
      {
        if (this.customThemeClassName)
          return base.ThemeClassName;
        return typeof (RadSplitContainer).FullName;
      }
      set
      {
        this.customThemeClassName = true;
        base.ThemeClassName = value;
      }
    }

    [DefaultValue(false)]
    public bool UseSplitterButtons
    {
      get
      {
        return this.splitContainerElement.UseSplitterButtons;
      }
      set
      {
        if (this.splitContainerElement.UseSplitterButtons == value)
          return;
        this.splitContainerElement.UseSplitterButtons = value;
        this.LayoutInternal();
      }
    }

    [DefaultValue(false)]
    public bool EnableCollapsing
    {
      get
      {
        return this.splitContainerElement.EnableCollapsing;
      }
      set
      {
        if (this.splitContainerElement.EnableCollapsing != value)
        {
          this.splitContainerElement.EnableCollapsing = value;
          if (!value)
            this.splitContainerElement.UseSplitterButtons = value;
        }
        this.LayoutInternal();
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

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    public event SplitPanelEventHandler PanelCollapsedChanged
    {
      add
      {
        this.Events.AddHandler(RadSplitContainer.ChildPanelCollapsedChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadSplitContainer.ChildPanelCollapsedChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when any of the splitters is moving.")]
    [Browsable(true)]
    [Category("Action")]
    public event SplitterCancelEventHandler SplitterMoving
    {
      add
      {
        this.Events.AddHandler(RadSplitContainer.SplitterMovingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadSplitContainer.SplitterMovingEventKey, (Delegate) value);
      }
    }

    [Category("Action")]
    [Browsable(true)]
    [Description("Occurs when any of the splitters is moved.")]
    public event SplitterEventHandler SplitterMoved
    {
      add
      {
        this.Events.AddHandler(RadSplitContainer.SplitterMovedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadSplitContainer.SplitterMovedEventKey, (Delegate) value);
      }
    }

    [Category("Behavior")]
    [Description("Occurs when some panel is collapsing.")]
    public event RadSplitContainer.PanelCollapsingEventHandler PanelCollapsing;

    [Category("Behavior")]
    [Description("Occurs when some panel collapsed.")]
    public event RadSplitContainer.PanelCollapsedEventHandler PanelCollapsed;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool MergeWithParentContainer()
    {
      this.UpdateCollapsed();
      if (!this.isCleanUpTarget)
        return false;
      if (this.panels.Count == 0)
      {
        this.Dispose();
        return true;
      }
      RadSplitContainer parent = this.Parent as RadSplitContainer;
      if (parent == null)
        return false;
      if (this.panels.Count == 1)
      {
        int index = parent.SplitPanels.IndexOf((SplitPanel) this);
        Size size = this.Size;
        this.Parent = (Control) null;
        SplitPanel panel = this.panels[0];
        panel.Parent = (Control) null;
        panel.Size = size;
        parent.panels.Insert(index, panel);
        return true;
      }
      if (parent.SplitPanels.Count == 1)
      {
        parent.Orientation = this.Orientation;
        List<SplitPanel> childControls = ControlHelper.GetChildControls<SplitPanel>((Control) this, false);
        this.Parent = (Control) null;
        this.SplitPanels.Clear();
        foreach (SplitPanel splitPanel in childControls)
          parent.SplitPanels.Add(splitPanel);
        return true;
      }
      if (parent.Orientation != this.Orientation)
        return false;
      int num = parent.SplitPanels.IndexOf((SplitPanel) this);
      List<SplitPanel> childControls1 = ControlHelper.GetChildControls<SplitPanel>((Control) this, false);
      this.Parent = (Control) null;
      this.SplitPanels.Clear();
      foreach (SplitPanel splitPanel in childControls1)
        parent.SplitPanels.Insert(num++, splitPanel);
      return true;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual void UpdateCollapsed()
    {
      this.Collapsed = !this.HasNonCollapsedChild;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual SplitterElement GetSplitterElementAtPoint(Point clientPoint)
    {
      if (this.splitContainerElement.Children.Count != this.VisiblePanelCount - 1)
        return (SplitterElement) null;
      for (int index = 0; index < this.VisiblePanels.Count - 1; ++index)
      {
        if (this.splitContainerElement[index].Bounds.Contains(clientPoint))
          return this.splitContainerElement[index];
      }
      return (SplitterElement) null;
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool CanSelectAtDesignTime()
    {
      return true;
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.ApplySplitterWidth(this.SplitterWidth);
      this.ApplyThemeToSplitterElements();
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      base.OnControlRemoved(e);
      this.ClearSplitterReferences(e.Control);
    }

    protected internal virtual void OnChildPanelCollapsedChanged(SplitPanel child)
    {
      SplitPanelEventHandler panelEventHandler = this.Events[RadSplitContainer.ChildPanelCollapsedChangedEventKey] as SplitPanelEventHandler;
      if (panelEventHandler == null)
        return;
      SplitPanelEventArgs e = new SplitPanelEventArgs(child);
      panelEventHandler((object) this, e);
    }

    protected virtual void OnOrientationChanged(EventArgs e)
    {
      if (this.splitContainerElement == null)
        return;
      int num = (int) this.splitContainerElement.SetValue(SplitContainerElement.IsVerticalProperty, (object) (this.Orientation == Orientation.Vertical));
      this.PerformLayout();
    }

    public void RestoreSplitterPosition(SplitterElement splitter)
    {
      if (this.nothingToRestore)
        return;
      this.currentSplitter = splitter;
      this.SplitterDistance = splitter.LastSplitterPosition;
      splitter.IsCollapsed = false;
      this.beginSplitterDistance = this.Orientation != Orientation.Vertical ? splitter.BoundingRectangle.Y : splitter.BoundingRectangle.X;
      this.UpdateBoundsFromSplitter();
    }

    public void MoveSplitter(SplitterElement splitter, RadDirection direction)
    {
      this.nothingToRestore = false;
      this.currentSplitter = splitter;
      Padding padding = this.SplitterBounds(splitter);
      if (this.Orientation == Orientation.Vertical)
      {
        this.beginSplitterDistance = splitter.BoundingRectangle.X;
        if (direction == RadDirection.Left)
          this.CalculateSplitterDistance(direction, 0, splitter.BoundingRectangle.X, padding.Left, padding.Right);
        else
          this.CalculateSplitterDistance(direction, this.Size.Width, splitter.BoundingRectangle.X, padding.Left, padding.Right);
      }
      else
      {
        this.beginSplitterDistance = splitter.BoundingRectangle.Y;
        if (direction == RadDirection.Up)
          this.CalculateSplitterDistance(direction, 0, splitter.BoundingRectangle.Y, padding.Top, padding.Bottom);
        else
          this.CalculateSplitterDistance(direction, this.Size.Height, splitter.BoundingRectangle.Y, padding.Top, padding.Bottom);
      }
      this.UpdateBoundsFromSplitter();
      this.DrawSplitBar();
      if (!this.UseSplitterButtons)
        return;
      if (this.Orientation == Orientation.Vertical)
      {
        if (direction == RadDirection.Left)
        {
          splitter.PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
          splitter.NextNavigationButton.Visibility = ElementVisibility.Visible;
        }
        else
        {
          splitter.PrevNavigationButton.Visibility = ElementVisibility.Visible;
          splitter.NextNavigationButton.Visibility = ElementVisibility.Collapsed;
        }
      }
      else if (direction == RadDirection.Up)
      {
        splitter.PrevNavigationButton.Visibility = ElementVisibility.Visible;
        splitter.NextNavigationButton.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        splitter.PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
        splitter.NextNavigationButton.Visibility = ElementVisibility.Visible;
      }
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
      base.OnLayout(e);
      this.LayoutInternal();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left || !this.ContentRectangle.Contains(e.Location))
        return;
      this.nothingToRestore = false;
      this.currentSplitter = this.GetSplitterElementAtPoint(e.Location);
      if (this.currentSplitter == null || this.currentSplitter.Fixed)
        return;
      bool flag = !this.UseSplitterButtons || this.currentSplitter.ElementTree.GetElementAtPoint<RadButtonElement>(e.Location) == null;
      this.resizing = true;
      if (this.Cursor == Cursors.VSplit)
      {
        this.beginSplitterDistance = e.X;
        this.splitterDistance = e.X;
        if (!flag)
          return;
        this.currentSplitter.LastSplitterPosition = e.X;
      }
      else
      {
        if (!(this.Cursor == Cursors.HSplit))
          return;
        this.beginSplitterDistance = e.Y;
        this.splitterDistance = e.Y;
        if (!flag)
          return;
        this.currentSplitter.LastSplitterPosition = e.Y;
      }
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (this.EnableCollapsing && !this.UseSplitterButtons)
      {
        this.currentSplitter = this.GetSplitterElementAtPoint(e.Location);
        this.nothingToRestore = false;
        if (this.currentSplitter != null && !this.currentSplitter.Fixed)
        {
          this.resizing = true;
          if (this.currentSplitter.IsCollapsed)
          {
            this.splitterDistance = this.currentSplitter.LastSplitterPosition;
            this.currentSplitter.IsCollapsed = false;
          }
          else
          {
            Padding padding = this.SplitterBounds(this.currentSplitter);
            if (this.Orientation == Orientation.Vertical)
            {
              RadSplitContainer.PanelCollapsingEventArgs e1 = new RadSplitContainer.PanelCollapsingEventArgs(RadDirection.Left);
              this.OnPanelCollapsing(this.currentSplitter, e1);
              if (e1.Cancel)
                return;
              this.splitterDistance = e1.Direction != RadDirection.Right ? 0 : this.Size.Width;
              this.currentSplitter.SplitterAlignment = e1.Direction;
              this.currentSplitter.LastSplitterPosition = e.X;
              if (this.splitterDistance < this.beginSplitterDistance - padding.Left)
                this.splitterDistance = this.beginSplitterDistance - padding.Left;
              if (this.splitterDistance > padding.Right + this.beginSplitterDistance)
                this.splitterDistance = padding.Right + this.beginSplitterDistance;
            }
            else
            {
              RadSplitContainer.PanelCollapsingEventArgs e1 = new RadSplitContainer.PanelCollapsingEventArgs(RadDirection.Up);
              this.OnPanelCollapsing(this.currentSplitter, e1);
              if (e1.Cancel)
                return;
              this.splitterDistance = e1.Direction != RadDirection.Down ? 0 : this.Size.Height;
              this.currentSplitter.SplitterAlignment = e1.Direction;
              this.currentSplitter.LastSplitterPosition = e.Y;
              if (this.splitterDistance < this.beginSplitterDistance - padding.Top)
                this.splitterDistance = this.beginSplitterDistance - padding.Top;
              if (this.splitterDistance > padding.Bottom + this.beginSplitterDistance)
                this.splitterDistance = padding.Bottom + this.beginSplitterDistance;
            }
            this.OnPanelCollapsed(this.currentSplitter, new EventArgs());
            this.currentSplitter.IsCollapsed = true;
          }
        }
      }
      base.OnMouseDoubleClick(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (!this.resizing)
      {
        this.UpdateCursor(e.Location);
      }
      else
      {
        if (e.Button != MouseButtons.Left)
          return;
        Padding padding = this.SplitterBounds(this.currentSplitter);
        if (this.Orientation == Orientation.Vertical)
        {
          this.splitterDistance = e.X;
          if (this.splitterDistance < this.beginSplitterDistance - padding.Left)
            this.splitterDistance = this.beginSplitterDistance - padding.Left;
          if (this.splitterDistance > padding.Right + this.beginSplitterDistance)
            this.splitterDistance = padding.Right + this.beginSplitterDistance;
        }
        else
        {
          this.splitterDistance = e.Y;
          if (this.splitterDistance < this.beginSplitterDistance - padding.Top)
            this.splitterDistance = this.beginSplitterDistance - padding.Top;
          if (this.splitterDistance > padding.Bottom + this.beginSplitterDistance)
            this.splitterDistance = padding.Bottom + this.beginSplitterDistance;
        }
        SplitterCancelEventArgs e1 = new SplitterCancelEventArgs(e.X, e.Y, this.currentSplitter.Bounds.Left, this.currentSplitter.Bounds.Top);
        this.OnSplitterMoving(e1);
        if (e1.Cancel)
        {
          this.resizing = false;
          this.Cursor = Cursors.Arrow;
        }
        this.currentSplitter.IsCollapsed = false;
        if (this.EnableImmediateResize)
        {
          this.UpdateBoundsFromSplitter();
          this.Refresh();
          this.beginSplitterDistance = this.splitterDistance;
        }
        else
          this.DrawSplitBar();
      }
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.UpdateCursor(e.Location);
      if (!this.resizing)
        return;
      this.resizing = false;
      this.DrawSplitBar();
      this.UpdateBoundsFromSplitter();
      Point client = this.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
      this.OnSplitterMoved(new SplitterEventArgs(client.X, client.Y, this.currentSplitter.Bounds.X, this.currentSplitter.Bounds.Y));
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.Cursor = (Cursor) null;
    }

    protected virtual void OnSplitterMoved(SplitterEventArgs e)
    {
      SplitterEventHandler splitterEventHandler = (SplitterEventHandler) this.Events[RadSplitContainer.SplitterMovedEventKey];
      if (splitterEventHandler != null)
        splitterEventHandler((object) this, e);
      if (!(this.Parent is RadSplitContainer))
        return;
      ((RadSplitContainer) this.Parent).OnSplitterMoved(e);
    }

    protected virtual void OnSplitterMoving(SplitterCancelEventArgs e)
    {
      SplitterCancelEventHandler cancelEventHandler = (SplitterCancelEventHandler) this.Events[RadSplitContainer.SplitterMovingEventKey];
      if (cancelEventHandler != null)
        cancelEventHandler((object) this, e);
      if (!(this.Parent is RadSplitContainer))
        return;
      ((RadSplitContainer) this.Parent).OnSplitterMoving(e);
    }

    protected override void OnThemeChanged()
    {
      foreach (RadControl panel in (IEnumerable<SplitPanel>) this.panels)
        panel.ThemeName = this.ThemeName;
      base.OnThemeChanged();
    }

    public virtual void OnPanelCollapsing(
      SplitterElement splitter,
      RadSplitContainer.PanelCollapsingEventArgs e)
    {
      RadSplitContainer.PanelCollapsingEventHandler panelCollapsing = this.PanelCollapsing;
      if (panelCollapsing == null)
        return;
      panelCollapsing((object) splitter, e);
    }

    public virtual void OnPanelCollapsed(SplitterElement splitter, EventArgs e)
    {
      RadSplitContainer.PanelCollapsedEventHandler panelCollapsed = this.PanelCollapsed;
      if (panelCollapsed == null)
        return;
      panelCollapsed((object) splitter, e);
    }

    protected virtual void OnNavigationButtonClick(object sender, EventArgs e)
    {
      RadButtonElement radButtonElement = sender as RadButtonElement;
      MouseEventArgs mouseEventArgs = e as MouseEventArgs;
      if (radButtonElement == null || mouseEventArgs == null)
        return;
      this.currentSplitter = this.GetSplitterElementAtPoint(mouseEventArgs.Location);
      if (this.currentSplitter == null || this.currentSplitter.Fixed)
        return;
      this.resizing = true;
      if (this.currentSplitter.IsCollapsed)
      {
        this.splitterDistance = this.currentSplitter.LastSplitterPosition;
        this.currentSplitter.IsCollapsed = false;
      }
      else
      {
        Padding padding = this.SplitterBounds(this.currentSplitter);
        if (radButtonElement.Equals((object) this.currentSplitter.PrevNavigationButton))
        {
          if (this.Orientation == Orientation.Vertical)
            this.CalculateSplitterDistance(RadDirection.Left, 0, mouseEventArgs.X, padding.Left, padding.Right);
          else
            this.CalculateSplitterDistance(RadDirection.Down, this.Size.Height, mouseEventArgs.Y, padding.Top, padding.Bottom);
        }
        else
        {
          if (this.Orientation == Orientation.Vertical)
            this.CalculateSplitterDistance(RadDirection.Right, this.Size.Width, mouseEventArgs.X, padding.Left, padding.Right);
          else
            this.CalculateSplitterDistance(RadDirection.Up, 0, mouseEventArgs.Y, padding.Top, padding.Bottom);
          this.OnPanelCollapsed(this.currentSplitter, new EventArgs());
        }
        this.currentSplitter.IsCollapsed = true;
      }
    }

    protected virtual void CalculateSplitterDistance(
      RadDirection direction,
      int initialSplitterDistance,
      int lastSplitterPosition,
      int boundsMinimum,
      int boundsMaximum)
    {
      RadSplitContainer.PanelCollapsingEventArgs e = new RadSplitContainer.PanelCollapsingEventArgs(direction);
      this.OnPanelCollapsing(this.currentSplitter, e);
      if (e.Cancel)
        return;
      this.currentSplitter.SplitterAlignment = e.Direction;
      this.currentSplitter.LastSplitterPosition = lastSplitterPosition;
      this.splitterDistance = initialSplitterDistance;
      if (this.splitterDistance < this.beginSplitterDistance - boundsMinimum)
        this.splitterDistance = this.beginSplitterDistance - boundsMinimum;
      if (this.splitterDistance <= boundsMaximum + this.beginSplitterDistance)
        return;
      this.splitterDistance = boundsMaximum + this.beginSplitterDistance;
    }

    protected override void OnParentChanged(EventArgs e)
    {
      base.OnParentChanged(e);
      for (Control parent = this.Parent; parent != null; parent = parent.Parent)
      {
        RadSplitContainer radSplitContainer = parent as RadSplitContainer;
        if (radSplitContainer != null)
          this.immediateSplitterMove = radSplitContainer.immediateSplitterMove;
      }
    }

    protected internal void LayoutInternal()
    {
      this.SuspendLayout();
      if (this.Controls.Count > 0 && this.layoutStrategy != null)
      {
        int num1 = this.VisiblePanelCount - 1;
        if (this.splitContainerElement.Count != num1)
        {
          for (int index = 0; index < this.splitContainerElement.Count; ++index)
          {
            if (this.splitContainerElement[index] != null)
            {
              SplitterElement splitterElement = this.splitContainerElement[index];
              if (splitterElement.PrevNavigationButton != null)
                splitterElement.PrevNavigationButton.Click -= new EventHandler(this.OnNavigationButtonClick);
              if (splitterElement.NextNavigationButton != null)
                splitterElement.NextNavigationButton.Click -= new EventHandler(this.OnNavigationButtonClick);
            }
          }
          this.splitContainerElement.DisposeChildren();
          while (this.splitContainerElement.Count < num1)
          {
            SplitterElement splitterElement = new SplitterElement();
            this.splitContainerElement.Children.Add((RadElement) splitterElement);
            splitterElement.AutoSize = false;
            splitterElement.Layout.UseSplitterButtons = this.UseSplitterButtons;
            splitterElement.Dock = this.Orientation == Orientation.Horizontal ? DockStyle.Top : DockStyle.Left;
            splitterElement.PrevNavigationButton.Click += new EventHandler(this.OnNavigationButtonClick);
            splitterElement.NextNavigationButton.Click += new EventHandler(this.OnNavigationButtonClick);
            if (this.UseSplitterButtons)
            {
              splitterElement.PrevNavigationButton.Visibility = ElementVisibility.Visible;
              splitterElement.NextNavigationButton.Visibility = ElementVisibility.Visible;
            }
            else
            {
              splitterElement.PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
              splitterElement.NextNavigationButton.Visibility = ElementVisibility.Collapsed;
            }
            int num2 = (int) splitterElement.SetDefaultValueOverride(SplitterElement.SplitterWidthProperty, (object) this.SplitterWidth);
            bool flag = (bool) this.splitContainerElement.GetValue(SplitContainerElement.IsVerticalProperty);
            int num3 = (int) splitterElement.SetValue(SplitterElement.IsVerticalProperty, (object) flag);
          }
        }
        this.LayoutSplitterButtons();
        this.layoutStrategy.PerformLayout(this);
      }
      foreach (SplitterElement splitter in this.Splitters)
      {
        Control leftNode = splitter.LeftNode as Control;
        if (leftNode != null)
        {
          Control rightNode = splitter.RightNode as Control;
          if (rightNode != null && (this.Orientation == Orientation.Vertical && leftNode.Width > leftNode.MinimumSize.Width && rightNode.Width > rightNode.MinimumSize.Width || this.Orientation == Orientation.Horizontal && leftNode.Height > leftNode.MinimumSize.Height && rightNode.Height > rightNode.MinimumSize.Height))
            splitter.IsCollapsed = false;
        }
      }
      this.ResumeLayout(true);
    }

    private void LayoutSplitterButtons()
    {
      if (this.suspendButtonLayout)
        this.suspendButtonLayout = false;
      else if (!this.UseSplitterButtons || !this.EnableCollapsing)
      {
        for (int index = 0; index < this.Splitters.Count; ++index)
        {
          this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
          this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Collapsed;
        }
      }
      else
      {
        for (int index = 0; index < this.Splitters.Count; ++index)
        {
          if (index == 0)
          {
            if (this.Orientation == Orientation.Vertical)
            {
              if (this.Splitters[index].IsCollapsed)
              {
                this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
                this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Visible;
              }
              else
              {
                this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Visible;
                this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Collapsed;
              }
            }
            else if (this.Splitters[index].IsCollapsed)
            {
              this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Visible;
              this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Collapsed;
            }
            else
            {
              this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
              this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Visible;
            }
          }
          else
          {
            if (index == this.Splitters.Count - 1)
            {
              if (this.Orientation == Orientation.Vertical)
              {
                if (this.Splitters[index].IsCollapsed)
                {
                  this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Visible;
                  this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Collapsed;
                  break;
                }
                this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
                this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Visible;
                break;
              }
              if (this.Splitters[index].IsCollapsed)
              {
                this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
                this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Visible;
                break;
              }
              this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Visible;
              this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Collapsed;
              break;
            }
            if (this.Orientation == Orientation.Vertical)
            {
              if (this.Splitters[index].IsCollapsed)
              {
                if (this.Splitters[index].Location.X < this.Splitters[index].LastSplitterPosition)
                {
                  this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
                  this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Visible;
                }
                else if (this.Splitters[index].Location.X > this.Splitters[index].LastSplitterPosition)
                {
                  this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Visible;
                  this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Collapsed;
                }
              }
              else
              {
                this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Visible;
                this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Visible;
              }
            }
            else if (this.Splitters[index].IsCollapsed)
            {
              if (this.Splitters[index].Location.Y < this.Splitters[index].LastSplitterPosition)
              {
                this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Visible;
                this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Collapsed;
              }
              else if (this.Splitters[index].Location.Y > this.Splitters[index].LastSplitterPosition)
              {
                this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Collapsed;
                this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Visible;
              }
            }
            else
            {
              this.Splitters[index].PrevNavigationButton.Visibility = ElementVisibility.Visible;
              this.Splitters[index].NextNavigationButton.Visibility = ElementVisibility.Visible;
            }
          }
        }
      }
    }

    protected internal virtual void UpdateSplitter(
      SplitContainerLayoutInfo info,
      int panelIndex,
      Rectangle bounds)
    {
      SplitterElement splitterElement = this.splitContainerElement[panelIndex - 1];
      SplitPanel layoutTarget1 = info.LayoutTargets[panelIndex - 1];
      SplitPanel layoutTarget2 = info.LayoutTargets[panelIndex];
      int num1 = layoutTarget1.Width;
      int num2 = layoutTarget2.Width;
      if (this.Orientation == Orientation.Horizontal)
      {
        num1 = layoutTarget1.Height;
        num2 = layoutTarget2.Height;
      }
      if (this.UseSplitterButtons && this.EnableCollapsing && (!splitterElement.IsCollapsed && splitterElement == this.currentSplitter) && (num1 == 0 || num2 == 0))
      {
        if (splitterElement.LastSplitterPosition == 0)
          splitterElement.LastSplitterPosition = this.Orientation == Orientation.Vertical ? splitterElement.Bounds.X : splitterElement.Bounds.Y;
        if (panelIndex == 1 && num2 == 0 || panelIndex == this.SplitPanels.Count - 1 && num1 == 0)
          this.suspendButtonLayout = true;
        splitterElement.IsCollapsed = true;
      }
      splitterElement.LeftNode = (object) layoutTarget1;
      splitterElement.RightNode = (object) layoutTarget2;
      splitterElement.Bounds = bounds;
    }

    private void ClearSplitterReferences(Control control)
    {
      for (int index = 0; index < this.splitContainerElement.Count; ++index)
      {
        SplitterElement splitterElement = this.splitContainerElement[index];
        if (splitterElement != null)
        {
          if (splitterElement.LeftNode == control)
            splitterElement.LeftNode = (object) null;
          if (splitterElement.RightNode == control)
            splitterElement.RightNode = (object) null;
        }
      }
    }

    private Padding SplitterBounds(SplitterElement splitter)
    {
      Padding padding = new Padding();
      SplitPanel leftNode = (SplitPanel) splitter.LeftNode;
      SplitPanel rightNode = (SplitPanel) splitter.RightNode;
      Size size1 = this.layoutStrategy == null ? leftNode.SizeInfo.MinimumSize : this.layoutStrategy.GetMinimumSize(leftNode);
      Size size2 = this.layoutStrategy == null ? rightNode.SizeInfo.MinimumSize : this.layoutStrategy.GetMinimumSize(rightNode);
      if (this.Orientation == Orientation.Vertical)
      {
        padding.Left = leftNode.Width - size1.Width;
        int val2_1 = rightNode.SizeInfo.MaximumSize.Width - rightNode.Width;
        if (rightNode.SizeInfo.MaximumSize.Width > 0 && val2_1 >= 0)
          padding.Left = Math.Min(padding.Left, val2_1);
        padding.Right = rightNode.Width - size2.Width;
        int val2_2 = leftNode.SizeInfo.MaximumSize.Width - leftNode.Width;
        if (leftNode.SizeInfo.MaximumSize.Width > 0 && val2_2 >= 0)
          padding.Right = Math.Min(padding.Right, val2_2);
      }
      else
      {
        padding.Top = leftNode.Height - size1.Height;
        int val2_1 = rightNode.SizeInfo.MaximumSize.Height - leftNode.Height;
        if (rightNode.SizeInfo.MaximumSize.Height > 0 && val2_1 >= 0)
          padding.Top = Math.Min(padding.Top, val2_1);
        padding.Bottom = rightNode.Height - size2.Height;
        int val2_2 = leftNode.SizeInfo.MaximumSize.Height - leftNode.Height;
        if (leftNode.SizeInfo.MaximumSize.Height > 0 && val2_2 >= 0)
          padding.Bottom = Math.Min(padding.Bottom, val2_2);
      }
      return padding;
    }

    private void UpdateCursor(Point mouse)
    {
      SplitterElement splitterElementAtPoint = this.GetSplitterElementAtPoint(mouse);
      if (splitterElementAtPoint != null && !splitterElementAtPoint.Fixed)
        this.Cursor = this.Orientation == Orientation.Vertical ? Cursors.VSplit : Cursors.HSplit;
      else
        this.Cursor = (Cursor) null;
    }

    private void DrawSplitBar()
    {
      if (this.lastSplitterDistance != -1)
        TelerikPaintHelper.DrawHalftoneLine((Control) this, this.CalcSplitRectangle(this.lastSplitterDistance));
      if (this.resizing)
      {
        TelerikPaintHelper.DrawHalftoneLine((Control) this, this.CalcSplitRectangle(this.splitterDistance));
        this.lastSplitterDistance = this.splitterDistance;
      }
      else
        this.lastSplitterDistance = -1;
    }

    private void UpdateBoundsFromSplitter()
    {
      if (this.splitterDistance == -1 || this.beginSplitterDistance == this.splitterDistance || this.layoutStrategy == null)
        return;
      this.SuspendLayout();
      this.layoutStrategy.ApplySplitterCorrection((SplitPanel) this.currentSplitter.LeftNode, (SplitPanel) this.currentSplitter.RightNode, this.beginSplitterDistance - this.splitterDistance);
      this.ResumeLayout(false);
      this.LayoutInternal();
      if (!this.DesignMode)
        return;
      (this.GetService(typeof (IComponentChangeService)) as IComponentChangeService)?.OnComponentChanged((object) this, (MemberDescriptor) null, (object) null, (object) null);
    }

    private Rectangle CalcSplitRectangle(int splitterDistance)
    {
      if (this.Orientation == Orientation.Vertical)
        return new Rectangle(splitterDistance - this.SplitterWidth / 2, this.currentSplitter.Bounds.Top, this.SplitterWidth, this.currentSplitter.Bounds.Height);
      return new Rectangle(this.currentSplitter.Bounds.Left, splitterDistance - this.SplitterWidth / 2, this.currentSplitter.Bounds.Width, this.SplitterWidth);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.SplitPanelElement.SuspendApplyOfThemeSettings();
      this.SplitPanelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
      this.SplitPanelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "", "SplitContainerFill");
      this.SplitPanelElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, "", "SplitContainerFill");
      this.SplitPanelElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.SplitPanelElement.SuspendApplyOfThemeSettings();
      this.SplitPanelElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.SplitPanelElement.ResetThemeValueOverride(FillPrimitive.GradientStyleProperty);
      int num = (int) this.SplitPanelElement.Fill.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.SplitPanelElement.ElementTree.ApplyThemeToElementTree();
      this.SplitPanelElement.ResumeApplyOfThemeSettings();
    }

    public class ControlCollection : Control.ControlCollection
    {
      private RadSplitContainer owner;

      public ControlCollection(RadSplitContainer owner)
        : base((Control) owner)
      {
        this.owner = owner;
      }

      public override void SetChildIndex(Control child, int newIndex)
      {
        if (!(child is SplitPanel))
          return;
        base.SetChildIndex(child, newIndex);
      }

      public override void Add(Control value)
      {
        if (!(value is SplitPanel))
          return;
        SplitPanel splitPanel = (SplitPanel) value;
        splitPanel.Dock = DockStyle.None;
        splitPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        base.Add((Control) splitPanel);
        ISite site = this.owner.Site;
        if (site != null && splitPanel.Site == null)
          site.Container?.Add((IComponent) splitPanel);
        splitPanel.ThemeName = this.owner.ThemeName;
      }
    }

    public delegate void PanelCollapsingEventHandler(
      object sender,
      RadSplitContainer.PanelCollapsingEventArgs e);

    public delegate void PanelCollapsedEventHandler(object sender, EventArgs e);

    public class PanelCollapsingEventArgs : CancelEventArgs
    {
      private RadDirection direction;

      public PanelCollapsingEventArgs(RadDirection direction)
      {
        this.Direction = direction;
      }

      public RadDirection Direction
      {
        get
        {
          return this.direction;
        }
        set
        {
          this.direction = value;
        }
      }
    }
  }
}
