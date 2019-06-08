// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(true)]
  [DefaultEvent("SelectedPageChanged")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Containers")]
  [Designer("Telerik.WinControls.UI.Design.RadPageViewDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [DefaultProperty("Pages")]
  public class RadPageView : RadNCEnabledControl
  {
    private static object PageAddingEventKey = new object();
    private static object PageAddedEventKey = new object();
    private static object PageRemovingEventKey = new object();
    private static object PageRemovedEventKey = new object();
    private static object PageIndexChangingEventKey = new object();
    private static object PageIndexChangedEventKey = new object();
    private static object PagesClearingEventKey = new object();
    private static object PagesClearedEventKey = new object();
    private static object PageCollapsingEventKey = new object();
    private static object PageCollapsedEventKey = new object();
    private static object PageExpandingEventKey = new object();
    private static object PageExpandedEventKey = new object();
    private static object SelectedPageChangingEventKey = new object();
    private static object SelectedPageChangedEventKey = new object();
    private static object ItemListMenuDisplayingEventKey = new object();
    private static object ItemListMenuDisplayedEventKey = new object();
    private static object ViewModeChangingEventKey = new object();
    private static object ViewModeChangedEventKey = new object();
    private static object NewPageRequestedEventKey = new object();
    private RadPageViewPageCollection pages;
    private RadPageViewElement viewElement;
    private PageViewMode viewMode;
    private bool allowPageIndexChange;
    private byte suspendEvents;
    private Color pageBackColor;
    internal bool autoScroll;

    protected override void Construct()
    {
      base.Construct();
      this.pages = this.CreatePagesInstance();
      this.viewMode = PageViewMode.Strip;
      this.UpdateUI();
    }

    public event EventHandler<RadPageViewItemDroppingEventArgs> ItemDropping
    {
      add
      {
        if (this.viewElement == null)
          return;
        this.viewElement.ItemDropping += value;
      }
      remove
      {
        if (this.viewElement == null)
          return;
        this.viewElement.ItemDropping -= value;
      }
    }

    public event EventHandler<RadPageViewItemDroppedEventArgs> ItemDropped
    {
      add
      {
        if (this.viewElement == null)
          return;
        this.viewElement.ItemDropped += value;
      }
      remove
      {
        if (this.viewElement == null)
          return;
        this.viewElement.ItemDropped -= value;
      }
    }

    public event EventHandler<RadPageViewItemCreatingEventArgs> ItemCreating
    {
      add
      {
        if (this.viewElement == null)
          return;
        this.viewElement.ItemCreating += value;
      }
      remove
      {
        if (this.viewElement == null)
          return;
        this.viewElement.ItemCreating -= value;
      }
    }

    public event EventHandler NewPageRequested
    {
      add
      {
        this.Events.AddHandler(RadPageView.NewPageRequestedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.NewPageRequestedEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewModeChangingEventArgs> ViewModeChanging
    {
      add
      {
        this.Events.AddHandler(RadPageView.ViewModeChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.ViewModeChangingEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewModeEventArgs> ViewModeChanged
    {
      add
      {
        this.Events.AddHandler(RadPageView.ViewModeChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.ViewModeChangedEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewMenuDisplayingEventArgs> ItemListMenuDisplaying
    {
      add
      {
        this.Events.AddHandler(RadPageView.ItemListMenuDisplayingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.ItemListMenuDisplayingEventKey, (Delegate) value);
      }
    }

    public event EventHandler ItemListMenuDisplayed
    {
      add
      {
        this.Events.AddHandler(RadPageView.ItemListMenuDisplayedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.ItemListMenuDisplayedEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewCancelEventArgs> PageAdding
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageAddingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageAddingEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewEventArgs> PageAdded
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageAddedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageAddedEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewCancelEventArgs> PageRemoving
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageRemovingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageRemovingEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewEventArgs> PageRemoved
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageRemovedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageRemovedEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewIndexChangingEventArgs> PageIndexChanging
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageIndexChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageIndexChangingEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewIndexChangedEventArgs> PageIndexChanged
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageIndexChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageIndexChangedEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler PagesClearing
    {
      add
      {
        this.Events.AddHandler(RadPageView.PagesClearingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PagesClearingEventKey, (Delegate) value);
      }
    }

    public event EventHandler PagesCleared
    {
      add
      {
        this.Events.AddHandler(RadPageView.PagesClearedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PagesClearedEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewCancelEventArgs> PageExpanding
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageExpandingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageExpandingEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewEventArgs> PageExpanded
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageExpandedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageExpandedEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewCancelEventArgs> PageCollapsing
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageCollapsingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageCollapsingEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewEventArgs> PageCollapsed
    {
      add
      {
        this.Events.AddHandler(RadPageView.PageCollapsedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.PageCollapsedEventKey, (Delegate) value);
      }
    }

    public event EventHandler<RadPageViewCancelEventArgs> SelectedPageChanging
    {
      add
      {
        this.Events.AddHandler(RadPageView.SelectedPageChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.SelectedPageChangingEventKey, (Delegate) value);
      }
    }

    public event EventHandler SelectedPageChanged
    {
      add
      {
        this.Events.AddHandler(RadPageView.SelectedPageChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadPageView.SelectedPageChangedEventKey, (Delegate) value);
      }
    }

    public void SuspendEvents()
    {
      ++this.suspendEvents;
    }

    public void ResumeEvents()
    {
      if (this.suspendEvents <= (byte) 0)
        return;
      --this.suspendEvents;
    }

    protected override bool CanRaiseEvents
    {
      get
      {
        if (this.suspendEvents > (byte) 0)
          return false;
        return base.CanRaiseEvents;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(400, 300));
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Category("Layout")]
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

    [Description("Gets or sets the BackColor of all pages.")]
    public Color PageBackColor
    {
      get
      {
        return this.pageBackColor;
      }
      set
      {
        if (this.pageBackColor == value)
          return;
        this.pageBackColor = value;
        foreach (RadPageViewPage page in this.pages)
          page.OnPageBackColorChanged(EventArgs.Empty);
      }
    }

    private bool ShouldSerializePageBackColor()
    {
      return this.pageBackColor != Color.Empty;
    }

    [DefaultValue(PageViewMode.Strip)]
    [Description("Gets or sets the current mode of the view.")]
    public PageViewMode ViewMode
    {
      get
      {
        return this.viewMode;
      }
      set
      {
        if (this.viewMode == value)
          return;
        PageViewMode viewMode = this.viewMode;
        RadPageViewModeChangingEventArgs e = new RadPageViewModeChangingEventArgs(value);
        this.OnViewModeChanging(e);
        if (e.Cancel)
          return;
        this.viewMode = value;
        this.UpdateUI();
        if (viewMode == PageViewMode.ExplorerBar)
          Telerik.WinControls.NativeMethods.SetWindowPos(new HandleRef((object) null, this.Handle), new HandleRef((object) null, IntPtr.Zero), 0, 0, 0, 0, 547);
        this.OnViewModeChanged(new RadPageViewModeEventArgs(this.viewMode));
      }
    }

    public override Rectangle DisplayRectangle
    {
      get
      {
        if (this.viewElement == null || !this.IsLoaded)
          return base.DisplayRectangle;
        return this.viewElement.GetContentAreaRectangle();
      }
    }

    [DefaultValue(null)]
    [Browsable(false)]
    public RadPageViewPage SelectedPage
    {
      get
      {
        if (this.viewElement == null || this.viewElement.SelectedItem == null)
          return (RadPageViewPage) null;
        return this.viewElement.SelectedItem.Page;
      }
      set
      {
        if (this.SelectedPage == value)
          return;
        if (value != null && value.Owner != this)
          throw new ArgumentException("SelectedPage must be owned by the same RadPageView");
        RadPageViewCancelEventArgs e = new RadPageViewCancelEventArgs(value);
        this.OnSelectedPageChanging(e);
        if (e.Cancel)
          return;
        this.SetSelectedPage(new RadPageViewEventArgs(value));
        this.OnSelectedPageChanged(EventArgs.Empty);
      }
    }

    [Description("Gets the collection of pages for this view.")]
    public RadPageViewPageCollection Pages
    {
      get
      {
        return this.pages;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the current RadPageViewElement instance that represents the UI of the view.")]
    public RadPageViewElement ViewElement
    {
      get
      {
        return this.viewElement;
      }
    }

    internal bool AllowPageIndexChange
    {
      get
      {
        return this.allowPageIndexChange;
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets the default RadPageViewPage that will be loaded after EndInit of the control.If the DefaultPage is null the currently selected page will be loaded.")]
    [Browsable(true)]
    [AttributeProvider(typeof (RadPageViewPage))]
    public RadPageViewPage DefaultPage
    {
      get
      {
        if (this.viewElement == null || this.viewElement.DefaultPage == null)
          return (RadPageViewPage) null;
        return this.viewElement.DefaultPage;
      }
      set
      {
        this.viewElement.DefaultPage = value;
      }
    }

    [Description("Gets or sets the text orientation of the item within the owning RadPageViewElement instance.")]
    [DefaultValue(PageViewItemSizeMode.EqualHeight)]
    [Browsable(true)]
    public PageViewItemSizeMode ItemSizeMode
    {
      get
      {
        return this.ViewElement.ItemSizeMode;
      }
      set
      {
        this.ViewElement.ItemSizeMode = value;
      }
    }

    [Description("Gets or sets the size of the items when ItemSizeMode of RadPageView is PageViewItemSizeMode.EqualSize.")]
    [Browsable(true)]
    [DefaultValue(typeof (Size), "0,0")]
    public Size ItemSize
    {
      get
      {
        return this.ViewElement.ItemSize;
      }
      set
      {
        if (!(this.ViewElement.ItemSize != value))
          return;
        this.ViewElement.ItemSize = value;
        this.viewElement.InvalidateMeasure(true);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
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

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets whether the pages will be wrapped around when performing selection using the arrow keys. If the property is set to true, pressing the right arrow key when the last page is selected will result in selecting the first page.")]
    [Browsable(true)]
    public bool SelectionWrap
    {
      get
      {
        if (this.ViewElement == null)
          return true;
        return this.ViewElement.SelectionWrap;
      }
      set
      {
        this.ViewElement.SelectionWrap = value;
      }
    }

    [DefaultValue(false)]
    [Description("Determines whether ampersand character will be treated as mnemonic or not.")]
    [Category("Behavior")]
    [Browsable(true)]
    public bool UseMnemonic
    {
      get
      {
        if (this.ViewElement == null)
          return false;
        return this.ViewElement.UseMnemonic;
      }
      set
      {
        this.ViewElement.UseMnemonic = value;
        this.ViewElement.Invalidate();
      }
    }

    [Browsable(true)]
    [Description("Indicates focus cues display, when available, based on the corresponding control type and the current UI state.")]
    [Category("Accessibility")]
    [DefaultValue(false)]
    public override bool AllowShowFocusCues
    {
      get
      {
        return base.AllowShowFocusCues;
      }
      set
      {
        base.AllowShowFocusCues = value;
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

    protected override void OnNCPaint(Graphics g)
    {
      base.OnNCPaint(g);
      if (this.viewElement == null)
        return;
      this.viewElement.OnNCPaint(g);
    }

    protected override Padding GetNCMetrics()
    {
      if (this.viewElement == null)
        return Padding.Empty;
      return this.viewElement.GetNCMetrics();
    }

    protected override bool EnableNCModification
    {
      get
      {
        if (this.viewElement == null)
          return false;
        return this.viewElement.EnableNCModification;
      }
    }

    protected override bool EnableNCPainting
    {
      get
      {
        if (this.viewElement == null)
          return false;
        return this.viewElement.EnableNCPainting;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override bool AutoScroll
    {
      get
      {
        return false;
      }
      set
      {
        this.autoScroll = value;
      }
    }

    protected override bool IsInputKey(Keys keyData)
    {
      if (keyData == Keys.Left || keyData == Keys.Right || (keyData == Keys.Up || keyData == Keys.Down))
        return true;
      return base.IsInputKey(keyData);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      this.viewElement.ProcessKeyDown(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      if (!this.AllowShowFocusCues || this.SelectedPage == null)
        return;
      this.SelectedPage.Item.Invalidate();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      if (!this.AllowShowFocusCues || this.SelectedPage == null)
        return;
      this.SelectedPage.Item.Invalidate();
    }

    protected override bool CanEditElementAtDesignTime(RadElement element)
    {
      if (element is RadPageViewItem)
        return false;
      return base.CanEditElementAtDesignTime(element);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      this.viewElement.CallDoMouseWheel(e);
      base.OnMouseWheel(e);
    }

    public override void EndInit()
    {
      base.EndInit();
      if (this.DefaultPage == null)
        return;
      this.SelectedPage = this.DefaultPage;
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
      base.ScaleControl(factor, specified);
      this.viewElement.InvalidateMeasure(true);
    }

    protected override bool ProcessMnemonic(char charCode)
    {
      if (this.UseMnemonic && TelerikHelper.CanProcessMnemonic((Control) this) && (Control.ModifierKeys & Keys.Alt) == Keys.Alt)
      {
        foreach (RadPageViewPage page in this.Pages)
        {
          if (page.Item.ProcessMnemonic(charCode))
            return true;
        }
      }
      return false;
    }

    protected internal virtual void OnPageCollapsed(RadPageViewEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewEventArgs> eventHandler = this.Events[RadPageView.PageCollapsedEventKey] as EventHandler<RadPageViewEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPageCollapsing(RadPageViewCancelEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewCancelEventArgs> eventHandler = this.Events[RadPageView.PageCollapsingEventKey] as EventHandler<RadPageViewCancelEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPageExpanding(RadPageViewCancelEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewCancelEventArgs> eventHandler = this.Events[RadPageView.PageExpandingEventKey] as EventHandler<RadPageViewCancelEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPageExpanded(RadPageViewEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewEventArgs> eventHandler = this.Events[RadPageView.PageExpandedEventKey] as EventHandler<RadPageViewEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void OnSelectedPageChanged(EventArgs e)
    {
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "SelectionChanged", this.SelectedPage != null ? (object) this.SelectedPage.Text : (object) "");
      if (!this.CanRaiseEvents)
        return;
      EventHandler eventHandler = this.Events[RadPageView.SelectedPageChangedEventKey] as EventHandler;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void OnSelectedPageChanging(RadPageViewCancelEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewCancelEventArgs> eventHandler = this.Events[RadPageView.SelectedPageChangingEventKey] as EventHandler<RadPageViewCancelEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void SetSelectedPage(RadPageViewEventArgs e)
    {
      this.SuspendLayout();
      this.viewElement.OnSelectedPageChanged(e);
      this.ResumeLayout(false);
    }

    public void EnsurePageVisible(RadPageViewPage page)
    {
      if (page == null)
        throw new ArgumentNullException("Page");
      if (page.Owner != this)
        throw new InvalidOperationException("Page is not owned by this view.");
      this.viewElement.EnsureItemVisible(page.Item);
    }

    internal void EnablePageIndexChange()
    {
      this.allowPageIndexChange = true;
    }

    internal void DisablePageIndexChange()
    {
      this.allowPageIndexChange = false;
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
      return (Control.ControlCollection) new RadPageViewControlCollection(this);
    }

    protected virtual RadPageViewPageCollection CreatePagesInstance()
    {
      return new RadPageViewPageCollection(this);
    }

    protected internal virtual void OnNewPageRequested(EventArgs e)
    {
      EventHandler eventHandler = this.Events[RadPageView.NewPageRequestedEventKey] as EventHandler;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPageAdding(RadPageViewCancelEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewCancelEventArgs> eventHandler = this.Events[RadPageView.PageAddingEventKey] as EventHandler<RadPageViewCancelEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPageAdded(RadPageViewEventArgs e)
    {
      e.Page.Attach(this);
      this.viewElement.OnPageAdded(e);
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewEventArgs> eventHandler = this.Events[RadPageView.PageAddedEventKey] as EventHandler<RadPageViewEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPagesClearing(CancelEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      CancelEventHandler cancelEventHandler = this.Events[RadPageView.PagesClearingEventKey] as CancelEventHandler;
      if (cancelEventHandler == null)
        return;
      cancelEventHandler((object) this, e);
    }

    protected internal virtual void OnPagesCleared(EventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler eventHandler = this.Events[RadPageView.PagesClearedEventKey] as EventHandler;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPageRemoving(RadPageViewCancelEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewCancelEventArgs> eventHandler = this.Events[RadPageView.PageRemovingEventKey] as EventHandler<RadPageViewCancelEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPageRemoved(RadPageViewEventArgs e)
    {
      e.Page.Detach();
      this.viewElement.OnPageRemoved(e);
      this.Invalidate();
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewEventArgs> eventHandler = this.Events[RadPageView.PageRemovedEventKey] as EventHandler<RadPageViewEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPageIndexChanging(RadPageViewIndexChangingEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewIndexChangingEventArgs> eventHandler = this.Events[RadPageView.PageIndexChangingEventKey] as EventHandler<RadPageViewIndexChangingEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnPageIndexChanged(RadPageViewIndexChangedEventArgs e)
    {
      this.viewElement.OnPageIndexChanged(e);
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewIndexChangedEventArgs> eventHandler = this.Events[RadPageView.PageIndexChangedEventKey] as EventHandler<RadPageViewIndexChangedEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnItemListMenuDisplaying(RadPageViewMenuDisplayingEventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler<RadPageViewMenuDisplayingEventArgs> eventHandler = this.Events[RadPageView.ItemListMenuDisplayingEventKey] as EventHandler<RadPageViewMenuDisplayingEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void OnItemListMenuDisplayed(EventArgs e)
    {
      if (!this.CanRaiseEvents)
        return;
      EventHandler eventHandler = this.Events[RadPageView.ItemListMenuDisplayedEventKey] as EventHandler;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected void UpdateUI()
    {
      RadPageViewPage selectedPage = this.SelectedPage;
      RadPageViewPage defaultPage = this.DefaultPage;
      bool selectionWrap = this.SelectionWrap;
      bool useMnemonic = this.UseMnemonic;
      if (this.viewElement != null)
        this.viewElement.Dispose();
      this.viewElement = this.CreateUI();
      this.viewElement.Owner = this;
      this.SuspendEvents();
      foreach (RadPageViewPage page in this.pages)
        this.viewElement.OnPageAdded(new RadPageViewEventArgs(page));
      this.RootElement.Children.Add((RadElement) this.viewElement);
      if (selectedPage != null)
        this.SelectedPage = selectedPage;
      if (defaultPage != null && this.IsInitializing)
        this.DefaultPage = defaultPage;
      if (selectionWrap != this.SelectionWrap)
        this.SelectionWrap = selectionWrap;
      if (useMnemonic != this.UseMnemonic)
        this.UseMnemonic = useMnemonic;
      this.ResumeEvents();
    }

    protected virtual void OnViewModeChanging(RadPageViewModeChangingEventArgs e)
    {
      EventHandler<RadPageViewModeChangingEventArgs> eventHandler = this.Events[RadPageView.ViewModeChangingEventKey] as EventHandler<RadPageViewModeChangingEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void OnViewModeChanged(RadPageViewModeEventArgs e)
    {
      EventHandler<RadPageViewModeEventArgs> eventHandler = this.Events[RadPageView.ViewModeChangedEventKey] as EventHandler<RadPageViewModeEventArgs>;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual RadPageViewElement CreateUI()
    {
      switch (this.viewMode)
      {
        case PageViewMode.Stack:
          return (RadPageViewElement) new RadPageViewStackElement();
        case PageViewMode.Outlook:
          return (RadPageViewElement) new RadPageViewOutlookElement();
        case PageViewMode.ExplorerBar:
          return (RadPageViewElement) new RadPageViewExplorerBarElement();
        case PageViewMode.Backstage:
          return (RadPageViewElement) new RadPageViewBackstageElement();
        case PageViewMode.NavigationView:
          return (RadPageViewElement) new RadPageViewNavigationViewElement();
        default:
          return (RadPageViewElement) new RadPageViewStripElement();
      }
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadPageViewAccessibilityObject(this);
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (this.RootElement.IsInValidState(true) && request.Type == IPCMessage.MessageTypes.ExecuteMethod && request.Message == "ItemClick")
      {
        foreach (RadPageViewPage page in this.Pages)
        {
          if (page.Item.Text == request.ControlType)
            page.Item.CallDoClick(EventArgs.Empty);
        }
      }
      else if (request.Type == IPCMessage.MessageTypes.GetChildPropertyValue && request.Message == "Selected")
      {
        string str = request.Data.ToString();
        for (int index = 0; index < this.Pages.Count; ++index)
        {
          if (this.Pages[index].Item.IsSelected && str == this.Pages[index].Text)
          {
            request.Data = (object) true;
            return;
          }
        }
        request.Data = (object) false;
      }
      else if (request.Type == IPCMessage.MessageTypes.SetChildPropertyValue && request.Message == "Selected")
      {
        for (int index = 0; index < this.Pages.Count; ++index)
        {
          if (this.Pages[index].Item.Text == request.ControlType)
          {
            this.Pages[index].Item.IsSelected = true;
            break;
          }
        }
      }
      else if (request.Type == IPCMessage.MessageTypes.SetChildPropertyValue && request.Message == "Text")
      {
        for (int index = 0; index < this.Pages.Count; ++index)
        {
          if (this.Pages[index].Item.Text == request.ControlType)
          {
            this.Pages[index].Item.Text = request.Data.ToString();
            break;
          }
        }
      }
      else if (request.Type == IPCMessage.MessageTypes.GetChildPropertyValue && request.Message == "Text")
      {
        for (int index = 0; index < this.Pages.Count; ++index)
        {
          if (this.Pages[index].Item.Text == request.ControlType)
          {
            request.Data = (object) this.Pages[index].Item.Text;
            break;
          }
        }
      }
      else if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "SelectedIndex")
      {
        for (int index = 0; index < this.Pages.Count; ++index)
        {
          if (this.Pages[index].Item.IsSelected)
          {
            request.Data = (object) index;
            break;
          }
        }
      }
      else if (request.Type == IPCMessage.MessageTypes.SetPropertyValue && request.Message == "SelectedIndex")
      {
        int index = int.Parse(request.Data.ToString());
        if (index < 0 || index >= this.Pages.Count)
          return;
        this.Pages[index].Item.IsSelected = true;
      }
      else if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "TabCount")
        request.Data = (object) this.pages.Count;
      else if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "SelectedPage")
        request.Data = (object) this.SelectedPage.ToString();
      else if (request.Type == IPCMessage.MessageTypes.GetPropertyValue && request.Message == "ViewMode")
        request.Data = (object) this.ViewMode.ToString();
      else
        base.ProcessCodedUIMessage(ref request);
    }
  }
}
