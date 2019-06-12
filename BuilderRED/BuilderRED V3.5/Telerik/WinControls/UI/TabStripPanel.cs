// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TabStripPanel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.UI.Docking;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  public class TabStripPanel : SplitPanel
  {
    private int selectedIndex = -1;
    private static readonly object EVENT_DESELECTING = new object();
    private static readonly object EVENT_DESELECTED = new object();
    private static readonly object EVENT_SELECTING = new object();
    private static readonly object EVENT_SELECTED = new object();
    private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();
    public static bool DisableSelection;
    private RadPageViewTabStripElement tabStripElement;
    private TabPanelCollection tabPanels;
    private ImageList tabImageList;
    private Point dragStart;
    private bool dragging;
    private bool lockChildIndexSet;
    private int suspendStripItemsChanged;
    private int suspendStripSelecting;
    private bool showTabStrip;
    private bool showItemCloseButton;
    private bool showItemPinButton;
    private bool pageViewUpdate;
    private TabStripAlignment tabStripAlign;
    private TabStripTextOrientation tabStripTextOrientation;
    public static bool SuspendFocusChange;

    public TabStripPanel()
    {
      this.tabPanels = new TabPanelCollection(this);
      this.showTabStrip = true;
      this.tabStripAlign = this.DefaultTabStripAlignment;
      this.tabStripTextOrientation = TabStripTextOrientation.Default;
      this.Behavior.BitmapRepository.DisableBitmapCache = true;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.tabImageList != null)
          this.tabImageList.Disposed -= new EventHandler(this.DetachImageList);
        if (this.tabStripElement != null)
        {
          this.tabStripElement.ItemSelecting -= new EventHandler<RadPageViewItemSelectingEventArgs>(this.tabStripElement_ItemSelecting);
          this.tabStripElement.ItemsChanged -= new EventHandler<RadPageViewItemsChangedEventArgs>(this.tabStripElement_ItemsChanged);
        }
      }
      base.Dispose(disposing);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.tabStripElement = this.CreateTabStripElementInstance();
      this.tabStripElement.StretchHorizontally = true;
      this.tabStripElement.StretchVertically = true;
      this.tabStripElement.StripButtons = StripViewButtons.None;
      this.tabStripElement.ContentArea.Visibility = ElementVisibility.Collapsed;
      this.tabStripElement.StripAlignment = StripViewAlignment.Bottom;
      this.tabStripElement.ItemSelecting += new EventHandler<RadPageViewItemSelectingEventArgs>(this.tabStripElement_ItemSelecting);
      this.tabStripElement.ItemsChanged += new EventHandler<RadPageViewItemsChangedEventArgs>(this.tabStripElement_ItemsChanged);
      this.TabStripElement.ItemContainer.RadPropertyChanged += new RadPropertyChangedEventHandler(this.ItemContainer_RadPropertyChanged);
      this.SplitPanelElement.Children.Add((RadElement) this.tabStripElement);
    }

    protected virtual RadPageViewTabStripElement CreateTabStripElementInstance()
    {
      return new RadPageViewTabStripElement();
    }

    protected override Control.ControlCollection CreateControlsInstance()
    {
      return (Control.ControlCollection) new TabStripPanel.ControlCollection(this);
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TabPanel SelectedTab
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        if (this.tabPanels != null && this.tabPanels.Count > 0)
        {
          if (selectedIndex < 0)
            this.selectedIndex = 0;
          if (selectedIndex >= 0 && selectedIndex < this.tabPanels.Count)
            return this.tabPanels[selectedIndex];
        }
        return (TabPanel) null;
      }
      set
      {
        this.SelectedIndex = this.GetTabPanelIndex(value);
      }
    }

    [DefaultValue(-1)]
    [Category("Behavior")]
    [Browsable(false)]
    public int SelectedIndex
    {
      get
      {
        return this.selectedIndex;
      }
      set
      {
        if (this.selectedIndex != value)
        {
          TabPanel tabPanel1 = this.GetTabPanel(this.selectedIndex, false);
          TabPanel tabPanel2 = this.GetTabPanel(value, false);
          TabStripPanelSelectedIndexChangingEventArgs e = new TabStripPanelSelectedIndexChangingEventArgs(this.selectedIndex, value, tabPanel1, tabPanel2);
          this.OnSelectedIndexChanging(e);
          if (e.Cancel)
            return;
        }
        if (this.IsHandleCreated)
        {
          if (value < -1)
            throw new ArgumentOutOfRangeException(nameof (SelectedIndex));
          if (this.selectedIndex == value)
            return;
          this.selectedIndex = value;
          this.UpdateTabSelection(true);
          this.OnSelectedIndexChanged(EventArgs.Empty);
        }
        else
          this.selectedIndex = value;
      }
    }

    protected virtual TabStripAlignment DefaultTabStripAlignment
    {
      get
      {
        return TabStripAlignment.Bottom;
      }
    }

    protected virtual TabStripTextOrientation DefaultTabStripTextOrientation
    {
      get
      {
        return this.tabStripAlign == TabStripAlignment.Top || this.tabStripAlign == TabStripAlignment.Bottom ? TabStripTextOrientation.Horizontal : TabStripTextOrientation.Vertical;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the text orientation of the TabStripElement used to switch among child panels.")]
    public TabStripTextOrientation TabStripTextOrientation
    {
      get
      {
        return this.tabStripTextOrientation;
      }
      set
      {
        this.tabStripTextOrientation = value;
        this.OnTabStripTextOrientationChanged(EventArgs.Empty);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TabPanelCollection TabPanels
    {
      get
      {
        return this.tabPanels;
      }
    }

    [Browsable(false)]
    public bool HasVisibleTabPanels
    {
      get
      {
        return this.tabPanels.Count > 0;
      }
    }

    [DefaultValue(false)]
    [Description("Determines whether each TabStripItem will display a CloseButton, which allows for explicit close of its corresponding panel.")]
    public bool ShowItemCloseButton
    {
      get
      {
        return this.showItemCloseButton;
      }
      set
      {
        this.showItemCloseButton = value;
        foreach (TabStripItem tabStripItem in (IEnumerable<RadPageViewItem>) this.tabStripElement.Items)
          tabStripItem.ShowCloseButton = this.showItemCloseButton;
        this.UpdateLayout();
      }
    }

    [Description("Determines whether each TabStripItem will display a CloseButton, which allows for explicit close of its corresponding panel.")]
    [DefaultValue(false)]
    public bool ShowItemPinButton
    {
      get
      {
        return this.showItemPinButton;
      }
      set
      {
        this.showItemPinButton = value;
        foreach (TabStripItem tabStripItem in (IEnumerable<RadPageViewItem>) this.tabStripElement.Items)
          tabStripItem.ShowPinButton = this.showItemPinButton;
        this.UpdateLayout();
      }
    }

    [Browsable(false)]
    public Point DragStart
    {
      get
      {
        return this.dragStart;
      }
    }

    [Category("Appearance")]
    [Description("Determines whether the TabStripElement used to navigate among child panels is displayed.")]
    [DefaultValue(true)]
    public bool TabStripVisible
    {
      get
      {
        return this.showTabStrip;
      }
      set
      {
        if (this.showTabStrip == value)
          return;
        this.showTabStrip = value;
        this.UpdateTabStripVisibility(this.GetTabStripVisible());
        this.UpdateLayout();
      }
    }

    [Description("Gets or sets the alignment of the TabStripElement used to switch among child panels.")]
    [Category("Appearance")]
    public TabStripAlignment TabStripAlignment
    {
      get
      {
        return this.tabStripAlign;
      }
      set
      {
        if (value == TabStripAlignment.Default)
          value = this.DefaultTabStripAlignment;
        if (this.tabStripAlign == value)
          return;
        this.tabStripAlign = value;
        this.OnTabStripAlignmentChanged(EventArgs.Empty);
      }
    }

    [Browsable(false)]
    public RadPageViewStripElement TabStripElement
    {
      get
      {
        return (RadPageViewStripElement) this.tabStripElement;
      }
    }

    [Browsable(false)]
    public virtual Rectangle TabPanelBounds
    {
      get
      {
        bool tabStripVisible = this.GetTabStripVisible();
        this.UpdateTabStripVisibility(tabStripVisible);
        Rectangle displayRectangle = this.DisplayRectangle;
        Padding padding = this.tabStripElement.ContentArea.Padding + LightVisualElement.GetBorderThickness((LightVisualElement) this.tabStripElement.ContentArea, true);
        Size size = tabStripVisible ? this.tabStripElement.ItemContainer.Size : Size.Empty;
        Rectangle rectangle = Rectangle.Empty;
        switch (this.tabStripAlign)
        {
          case TabStripAlignment.Left:
            rectangle = new Rectangle(displayRectangle.Left + padding.Left + size.Width, displayRectangle.Top + padding.Top, displayRectangle.Width - (padding.Horizontal + size.Width), displayRectangle.Height - padding.Vertical);
            break;
          case TabStripAlignment.Top:
            rectangle = new Rectangle(displayRectangle.Left + padding.Left, displayRectangle.Top + padding.Top + size.Height, displayRectangle.Width - padding.Horizontal, displayRectangle.Height - (padding.Vertical + size.Height));
            break;
          case TabStripAlignment.Right:
            rectangle = new Rectangle(displayRectangle.Left + padding.Left, displayRectangle.Top + padding.Top, displayRectangle.Width - (padding.Horizontal + size.Width), displayRectangle.Height - padding.Vertical);
            break;
          case TabStripAlignment.Bottom:
            rectangle = new Rectangle(displayRectangle.Left + padding.Left, displayRectangle.Top + padding.Top, displayRectangle.Width - padding.Horizontal, displayRectangle.Height - (padding.Vertical + size.Height));
            break;
        }
        return rectangle;
      }
    }

    protected Padding TabPanelPaddings
    {
      get
      {
        Rectangle displayRectangle = this.DisplayRectangle;
        Rectangle tabPanelBounds = this.TabPanelBounds;
        return new Padding() { Left = tabPanelBounds.Left - displayRectangle.Left, Right = displayRectangle.Right - tabPanelBounds.Right, Top = tabPanelBounds.Top - displayRectangle.Top, Bottom = displayRectangle.Bottom - tabPanelBounds.Bottom };
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool CanUpdateChildIndex
    {
      get
      {
        return !this.lockChildIndexSet;
      }
      set
      {
        this.lockChildIndexSet = !value;
      }
    }

    [DefaultValue(null)]
    [Category("CatAppearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("TabBaseImageListDescr")]
    public override ImageList ImageList
    {
      get
      {
        return this.tabImageList;
      }
      set
      {
        if (this.tabImageList == value)
          return;
        if (this.tabImageList != null)
          this.tabImageList.Disposed -= new EventHandler(this.DetachImageList);
        this.tabImageList = value;
        if (value == null)
          return;
        value.Disposed += new EventHandler(this.DetachImageList);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override AnchorStyles Anchor
    {
      get
      {
        return base.Anchor;
      }
      set
      {
        base.Anchor = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new Size Size
    {
      get
      {
        return base.Size;
      }
      set
      {
        base.Size = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override DockStyle Dock
    {
      get
      {
        return base.Dock;
      }
      set
      {
        base.Dock = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new bool Enabled
    {
      get
      {
        return base.Enabled;
      }
      set
      {
        base.Enabled = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new Point Location
    {
      get
      {
        return base.Location;
      }
      set
      {
        base.Location = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new bool Visible
    {
      get
      {
        return base.Visible;
      }
      set
      {
        base.Visible = value;
      }
    }

    public event TabStripPanelSelectedIndexChangingEventHandler SelectedIndexChanging;

    public event EventHandler SelectedIndexChanged;

    protected internal virtual void OnTabCloseButtonClicked(TabStripItem item)
    {
      this.tabPanels.Remove(item.TabPanel);
    }

    protected virtual void OnDragInitialized(Point mouse)
    {
    }

    protected virtual void OnTabStripAlignmentChanged(EventArgs e)
    {
      switch (this.tabStripAlign)
      {
        case TabStripAlignment.Left:
          this.tabStripElement.StripAlignment = StripViewAlignment.Left;
          break;
        case TabStripAlignment.Top:
          this.tabStripElement.StripAlignment = StripViewAlignment.Top;
          break;
        case TabStripAlignment.Right:
          this.tabStripElement.StripAlignment = StripViewAlignment.Right;
          break;
        case TabStripAlignment.Bottom:
          this.tabStripElement.StripAlignment = StripViewAlignment.Bottom;
          break;
      }
      this.UpdateLayout();
    }

    protected virtual void OnTabStripTextOrientationChanged(EventArgs e)
    {
      this.UpdateTextOrientation();
      this.UpdateLayout();
    }

    protected virtual void OnSelectedIndexChanging(TabStripPanelSelectedIndexChangingEventArgs e)
    {
      TabStripPanelSelectedIndexChangingEventHandler selectedIndexChanging = this.SelectedIndexChanging;
      if (selectedIndexChanging == null)
        return;
      selectedIndexChanging((object) this, e);
    }

    protected virtual void OnSelectedIndexChanged(EventArgs e)
    {
      EventHandler selectedIndexChanged = this.SelectedIndexChanged;
      if (selectedIndexChanged == null)
        return;
      selectedIndexChanged((object) this, e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.dragStart = Point.Empty;
      if (e.Button != MouseButtons.Left)
        return;
      this.dragStart = new Point(e.X, e.Y);
    }

    protected override void OnMouseCaptureChanged(EventArgs e)
    {
      base.OnMouseCaptureChanged(e);
      this.dragging = false;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (e.Button != MouseButtons.Left || this.dragging)
        return;
      Point point = new Point(e.X, e.Y);
      if (!SplitPanelHelper.ShouldBeginDrag(point, this.dragStart))
        return;
      this.dragging = true;
      this.OnDragInitialized(point);
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
      this.UpdateActivePanelBounds();
      base.OnLayout(e);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
      base.OnHandleCreated(e);
      if (TabStripPanel.DisableSelection)
        return;
      if (this.tabPanels.Count > 0 && (this.selectedIndex < 0 || this.selectedIndex > this.tabPanels.Count - 1))
        this.selectedIndex = 0;
      this.UpdateTabSelection(true);
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      this.UpdateLayout();
    }

    private void tabStripElement_ItemSelecting(object sender, RadPageViewItemSelectingEventArgs e)
    {
      if (this.suspendStripSelecting > 0)
        return;
      TabStripItem nextItem = e.NextItem as TabStripItem;
      if (nextItem != null)
      {
        this.pageViewUpdate = true;
        if (!this.SelectTab(nextItem.TabPanel))
          e.Cancel = true;
        this.pageViewUpdate = false;
      }
      else
        e.Cancel = true;
    }

    private void tabStripElement_ItemsChanged(object sender, RadPageViewItemsChangedEventArgs e)
    {
      if (this.Disposing || this.IsDisposed || (!this.IsHandleCreated || this.suspendStripItemsChanged > 0))
        return;
      this.SuspendStripNotifications(true, true);
      switch (e.Operation)
      {
        case ItemsChangeOperation.Inserted:
        case ItemsChangeOperation.Set:
          TabStripItem changedItem = e.ChangedItem as TabStripItem;
          if (changedItem != null)
          {
            int newIndex = this.tabStripElement.Items.IndexOf((RadPageViewItem) changedItem);
            this.Controls.SetChildIndex((Control) changedItem.TabPanel, newIndex);
            this.selectedIndex = newIndex;
            this.UpdateTabSelection(true);
            break;
          }
          break;
      }
      this.ResumeStripNotifications(true, true);
    }

    private void ItemContainer_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != RadElement.BoundsProperty || this.DesignMode)
        return;
      this.UpdateActivePanelBounds();
    }

    public bool SelectTab(int index)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException(nameof (index));
      this.SelectedIndex = index;
      return this.SelectedIndex == index;
    }

    public bool SelectTab(string tabPanelName)
    {
      if (tabPanelName == null)
        throw new ArgumentNullException(nameof (tabPanelName));
      return this.SelectTab(this.TabPanels[tabPanelName]);
    }

    public bool SelectTab(TabPanel tabPanel)
    {
      if (tabPanel == null)
        throw new ArgumentNullException(nameof (tabPanel));
      return this.SelectTab(this.GetTabPanelIndex(tabPanel));
    }

    public void DeselectTab(int index)
    {
      if (this.SelectedTab != this.GetTabPanel(index, true))
        return;
      if (0 <= index && index < this.TabPanels.Count - 1)
        this.SelectedTab = this.GetTabPanel(++index, true);
      else
        this.SelectedTab = this.GetTabPanel(0, true);
    }

    public void DeselectTab(string tabPanelName)
    {
      if (tabPanelName == null)
        throw new ArgumentNullException(nameof (tabPanelName));
      this.DeselectTab(this.TabPanels[tabPanelName]);
    }

    public void DeselectTab(TabPanel tabPanel)
    {
      if (tabPanel == null)
        throw new ArgumentNullException(nameof (tabPanel));
      this.DeselectTab(this.GetTabPanelIndex(tabPanel));
    }

    public override string ToString()
    {
      string str = base.ToString();
      if (this.TabPanels != null)
      {
        str = str + ", TabPanels.Count: " + this.TabPanels.Count.ToString((IFormatProvider) CultureInfo.CurrentCulture);
        if (this.TabPanels.Count > 0)
          str = str + ", TabPanels[0]: " + this.TabPanels[0].ToString();
      }
      return str;
    }

    public virtual void UpdateTabSelection(bool updateFocus)
    {
      if (!this.IsHandleCreated || this.tabPanels == null)
        return;
      this.SuspendLayout();
      this.lockChildIndexSet = true;
      this.SuspendStripNotifications(false, true);
      if (this.selectedIndex >= 0 && this.selectedIndex < this.tabPanels.Count)
      {
        TabPanel tabPanel = this.tabPanels[this.selectedIndex];
        this.SetSelected(tabPanel);
        if (updateFocus && !tabPanel.ContainsFocus && !TabStripPanel.SuspendFocusChange)
          tabPanel.SelectNextControl((Control) null, true, true, false, false);
      }
      else
        this.SelectTabItem((TabStripItem) null);
      for (int index = 0; index < this.tabPanels.Count; ++index)
      {
        if (index != this.selectedIndex)
          this.tabPanels[index].Visible = false;
      }
      this.ResumeStripNotifications(false, true);
      this.lockChildIndexSet = false;
      this.ResumeLayout(true);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SuspendStripNotifications(bool suspendItemsChanged, bool suspendSelectionChanged)
    {
      if (suspendItemsChanged)
        ++this.suspendStripItemsChanged;
      if (!suspendSelectionChanged)
        return;
      ++this.suspendStripSelecting;
    }

    protected virtual void UpdateAfterControlRemoved(Control value)
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ResumeStripNotifications(bool itemsChanged, bool selection)
    {
      if (itemsChanged && this.suspendStripItemsChanged > 0)
        --this.suspendStripItemsChanged;
      if (!selection || this.suspendStripSelecting <= 0)
        return;
      --this.suspendStripSelecting;
    }

    protected override bool IsInputKey(Keys keyData)
    {
      if ((keyData & Keys.Alt) == Keys.Alt)
        return false;
      switch (keyData & Keys.KeyCode)
      {
        case Keys.Prior:
        case Keys.Next:
        case Keys.End:
        case Keys.Home:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    protected virtual bool GetTabStripVisible()
    {
      if (!this.showTabStrip || this.tabPanels == null)
        return false;
      return this.tabPanels.Count > 0;
    }

    protected virtual void UpdateLayout()
    {
      this.RootElement.InvalidateMeasure();
      this.RootElement.UpdateLayout();
      this.PerformLayout();
    }

    protected virtual void UpdateTabStripVisibility(bool visible)
    {
      if (this.tabStripElement == null)
        return;
      if (visible)
        this.tabStripElement.ItemContainer.Visibility = ElementVisibility.Visible;
      else
        this.tabStripElement.ItemContainer.Visibility = ElementVisibility.Collapsed;
    }

    private void UpdateTextOrientation()
    {
      if (this.tabStripTextOrientation == TabStripTextOrientation.Vertical)
        this.tabStripElement.TextOrientation = Orientation.Vertical;
      else
        this.tabStripElement.TextOrientation = Orientation.Horizontal;
    }

    protected virtual void UpdateActivePanelBounds()
    {
      TabPanel selectedTab = this.SelectedTab;
      if (selectedTab != null)
        selectedTab.Bounds = this.TabPanelBounds;
      else
        this.UpdateTabStripVisibility(this.GetTabStripVisible());
    }

    private void RemoveTabElement(TabPanel tabPanel)
    {
      for (int index = 0; index < this.tabStripElement.Items.Count; ++index)
      {
        if (((TabStripItem) this.tabStripElement.Items[index]).TabPanel.Equals((object) tabPanel))
        {
          this.SuspendStripNotifications(true, true);
          this.tabStripElement.RemoveItem(this.tabStripElement.Items[index]);
          this.ResumeStripNotifications(true, true);
          break;
        }
      }
    }

    private void SetSelected(TabPanel tabPanel)
    {
      tabPanel.SuspendLayout();
      bool flag = !tabPanel.Visible;
      bool containsFocus = this.ContainsFocus;
      tabPanel.Visible = true;
      if (flag && containsFocus)
        tabPanel.Focus();
      tabPanel.Bounds = this.TabPanelBounds;
      this.SelectTabItem(tabPanel.TabStripItem);
      tabPanel.ResumeLayout();
      if (!this.DesignMode)
        return;
      if (!flag)
        return;
      try
      {
        (this.GetService(typeof (ISelectionService)) as ISelectionService)?.SetSelectedComponents((ICollection) new Component[1]
        {
          (Component) tabPanel
        }, SelectionTypes.Replace);
        (this.GetService(typeof (IComponentChangeService)) as IComponentChangeService)?.OnComponentChanged((object) this, (MemberDescriptor) null, (object) null, (object) null);
      }
      catch
      {
      }
    }

    private void DetachImageList(object sender, EventArgs e)
    {
      this.ImageList = (ImageList) null;
    }

    private TabPanel GetTabPanel(int index, bool throwException)
    {
      if (index >= 0 && index < this.tabPanels.Count)
        return this.tabPanels[index];
      if (throwException)
        throw new ArgumentOutOfRangeException(nameof (index), "InvalidArgument");
      return (TabPanel) null;
    }

    private int GetTabPanelIndex(TabPanel tabPanel)
    {
      if (this.tabPanels != null)
      {
        for (int index = 0; index < this.tabPanels.Count; ++index)
        {
          if (this.tabPanels[index].Equals((object) tabPanel))
            return index;
        }
      }
      return -1;
    }

    private bool ShouldSerializeTabStripAlignment()
    {
      return this.tabStripAlign != this.DefaultTabStripAlignment;
    }

    private bool ShouldSerializeTabStripTextOrientation()
    {
      return this.tabStripTextOrientation != TabStripTextOrientation.Default;
    }

    private void SelectTabItem(TabStripItem tabStripItem)
    {
      if (this.pageViewUpdate)
        return;
      this.TabStripElement.SelectedItem = (RadPageViewItem) tabStripItem;
      if (this.TabStripElement.SelectedItem == null || this.TabStripElement.SelectedItem.Owner != this.TabStripElement)
        return;
      this.TabStripElement.EnsureItemVisible(this.TabStripElement.SelectedItem);
    }

    protected virtual TabStripItem CreateTabItem(TabPanel tabPanel)
    {
      return new TabStripItem(tabPanel);
    }

    public class ControlCollection : Control.ControlCollection
    {
      private TabStripPanel owner;

      public ControlCollection(TabStripPanel owner)
        : base((Control) owner)
      {
        this.owner = owner;
      }

      public override void Add(Control value)
      {
        TabPanel tabPanel = value as TabPanel;
        if (tabPanel == null)
          throw new ArgumentException("Collection may contain only TabPanel instances.");
        this.owner.SuspendStripNotifications(true, true);
        if (!this.owner.tabPanels.Contains(tabPanel) && this.owner.tabStripElement != null)
        {
          tabPanel.TabStripItem = this.owner.CreateTabItem(tabPanel);
          tabPanel.TabStripItem.ShowCloseButton = this.owner.showItemCloseButton;
          tabPanel.TabStripItem.ShowPinButton = this.owner.showItemPinButton;
          this.owner.tabStripElement.AddItem((RadPageViewItem) tabPanel.TabStripItem);
          tabPanel.TabStripItem.UpdateTabButtoms(this.owner);
        }
        base.Add((Control) tabPanel);
        tabPanel.Visible = false;
        ISite site = this.owner.Site;
        if (site != null && tabPanel.Site == null)
          site.Container?.Add((IComponent) tabPanel);
        if (this.owner.IsHandleCreated && !TabStripPanel.DisableSelection)
        {
          this.owner.selectedIndex = this.IndexOf((Control) tabPanel);
          this.owner.UpdateTabSelection(true);
        }
        this.owner.ResumeStripNotifications(true, true);
      }

      public override void SetChildIndex(Control child, int newIndex)
      {
        if (this.owner.lockChildIndexSet)
          return;
        base.SetChildIndex(child, newIndex);
        if (this.owner.suspendStripItemsChanged > 0)
          return;
        this.owner.SuspendStripNotifications(true, true);
        for (int index = 0; index < this.owner.TabStripElement.Items.Count; ++index)
        {
          TabStripItem tabStripItem = this.owner.TabStripElement.Items[index] as TabStripItem;
          if (tabStripItem != null && tabStripItem.TabPanel == child)
          {
            this.owner.TabStripElement.RemoveItem((RadPageViewItem) tabStripItem);
            if (newIndex >= 0)
              this.owner.TabStripElement.InsertItem(newIndex, (RadPageViewItem) tabStripItem);
            else
              this.owner.TabStripElement.AddItem((RadPageViewItem) tabStripItem);
            if (!TabStripPanel.DisableSelection)
            {
              this.owner.selectedIndex = newIndex;
              this.owner.UpdateTabSelection(true);
              break;
            }
            break;
          }
        }
        this.owner.ResumeStripNotifications(true, true);
      }

      public override void Remove(Control value)
      {
        int num = this.IndexOf(value);
        if (num == -1)
          return;
        this.owner.SuspendStripNotifications(true, true);
        int selectedIndex = this.owner.SelectedIndex;
        base.Remove(value);
        this.owner.RemoveTabElement((TabPanel) value);
        if (this.owner.IsHandleCreated && !TabStripPanel.DisableSelection)
        {
          if (this.Count > 0)
          {
            if (num > this.Count - 1)
              num = this.Count - 1;
            this.owner.selectedIndex = num;
          }
          else
            this.owner.selectedIndex = -1;
          this.owner.UpdateTabSelection(true);
        }
        this.owner.ResumeStripNotifications(true, true);
        this.owner.UpdateAfterControlRemoved(value);
      }
    }
  }
}
