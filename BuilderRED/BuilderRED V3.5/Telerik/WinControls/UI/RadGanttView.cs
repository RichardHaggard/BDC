// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGanttView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [DefaultEvent("SelectedItemChanged")]
  [DefaultProperty("Items")]
  [Description("Displays a hierarchical collection of task items along with the relations between them. Each item is represented by a GanttViewDataItem and each link is represented by a GanttViewLinkDataItem.")]
  [ComplexBindingProperties("DataSource")]
  [Docking(DockingBehavior.Ask)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [TelerikToolboxCategory("Data Controls")]
  [Designer("Telerik.WinControls.UI.Design.RadGanttViewDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadGanttView : RadControl, IPrintable
  {
    private RadGanttViewElement ganttViewElement;
    private GanttViewPrintSettings printSettings;
    private bool isPrintRectangleTrimmed;
    private bool shouldPrintTimelineElement;
    protected Bitmap printBmp;
    protected Bitmap timelineBmp;
    protected bool bitmapCreated;
    protected int rowCount;
    protected int colCount;
    protected RectangleF drawArea;

    public RadGanttView()
    {
      this.printSettings = new GanttViewPrintSettings();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.ganttViewElement = this.CreateGanttViewElement();
      parent.Children.Add((RadElement) this.ganttViewElement);
    }

    protected virtual RadGanttViewElement CreateGanttViewElement()
    {
      return new RadGanttViewElement();
    }

    protected override void Dispose(bool disposing)
    {
      this.PrintSettings.Dispose();
      base.Dispose(disposing);
    }

    [Browsable(false)]
    public RadGanttViewElement GanttViewElement
    {
      get
      {
        return this.ganttViewElement;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public GanttViewLinkDataItemCollection Links
    {
      get
      {
        return this.GanttViewElement.Links;
      }
    }

    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(true)]
    public GanttViewDataItemCollection Items
    {
      get
      {
        return this.GanttViewElement.Items;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    public GanttViewTextViewColumnCollection Columns
    {
      get
      {
        return this.GanttViewElement.Columns;
      }
    }

    [DefaultValue(0.5f)]
    public float Ratio
    {
      get
      {
        return this.GanttViewElement.Ratio;
      }
      set
      {
        this.GanttViewElement.Ratio = value;
      }
    }

    [DefaultValue(25)]
    public int ItemHeight
    {
      get
      {
        return this.GanttViewElement.ItemHeight;
      }
      set
      {
        this.GanttViewElement.ItemHeight = value;
      }
    }

    [DefaultValue(50)]
    public int HeaderHeight
    {
      get
      {
        return this.GanttViewElement.HeaderHeight;
      }
      set
      {
        this.GanttViewElement.HeaderHeight = value;
      }
    }

    [DefaultValue(5)]
    public int SplitterWidth
    {
      get
      {
        return this.GanttViewElement.SplitterWidth;
      }
      set
      {
        this.GanttViewElement.SplitterWidth = value;
      }
    }

    [DefaultValue(false)]
    public bool IsEditing
    {
      get
      {
        return this.GanttViewElement.IsEditing;
      }
    }

    [DefaultValue(true)]
    public bool AllowSummaryEditing
    {
      get
      {
        return this.GanttViewElement.AllowSummaryEditing;
      }
      set
      {
        this.GanttViewElement.AllowSummaryEditing = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public BaseGanttViewBehavior GanttViewBehavior
    {
      get
      {
        return this.GanttViewElement.GanttViewBehavior;
      }
      set
      {
        this.GanttViewElement.GanttViewBehavior = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GanttViewDragDropService DragDropService
    {
      get
      {
        return this.GanttViewElement.DragDropService;
      }
      set
      {
        this.GanttViewElement.DragDropService = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public LinkTypeConverter LinkTypeConverter
    {
      get
      {
        return this.GanttViewElement.LinkTypeConverter;
      }
      set
      {
        this.GanttViewElement.LinkTypeConverter = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GanttViewDataItem SelectedItem
    {
      get
      {
        return this.GanttViewElement.SelectedItem;
      }
      set
      {
        this.GanttViewElement.SelectedItem = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public GanttViewLinkDataItem SelectedLink
    {
      get
      {
        return this.GanttViewElement.SelectedLink;
      }
      set
      {
        this.GanttViewElement.SelectedLink = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the current column.")]
    public GanttViewTextViewColumn CurrentColumn
    {
      get
      {
        return this.GanttViewElement.CurrentColumn;
      }
      set
      {
        this.GanttViewElement.CurrentColumn = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool IsDataBound
    {
      get
      {
        return this.GanttViewElement.IsDataBound;
      }
    }

    [Category("Data")]
    [DefaultValue(null)]
    [AttributeProvider(typeof (IListSource))]
    [Description("Gets or sets the data source that the RadGanttView is displaying data for.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public object DataSource
    {
      get
      {
        return this.GanttViewElement.DataSource;
      }
      set
      {
        this.GanttViewElement.DataSource = value;
      }
    }

    [Browsable(true)]
    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    [Description("Gets or sets the name of the list or table in the data source from which the RadGanttView will get data for the tasks it will be displaying.")]
    [Category("Data")]
    public string TaskDataMember
    {
      get
      {
        return this.GanttViewElement.TaskDataMember;
      }
      set
      {
        this.GanttViewElement.TaskDataMember = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Description("Gets or sets the Parent member.")]
    [Category("Data")]
    public string ParentMember
    {
      get
      {
        return this.GanttViewElement.ParentMember;
      }
      set
      {
        this.GanttViewElement.ParentMember = value;
      }
    }

    [DefaultValue("")]
    [Category("Data")]
    [Description("Gets or sets the Child member.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string ChildMember
    {
      get
      {
        return this.GanttViewElement.ChildMember;
      }
      set
      {
        this.GanttViewElement.ChildMember = value;
      }
    }

    [Description("Gets or sets the Title member.")]
    [DefaultValue("")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string TitleMember
    {
      get
      {
        return this.GanttViewElement.TitleMember;
      }
      set
      {
        this.GanttViewElement.TitleMember = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Description("Gets or sets the Start member.")]
    [Category("Data")]
    public string StartMember
    {
      get
      {
        return this.GanttViewElement.StartMember;
      }
      set
      {
        this.GanttViewElement.StartMember = value;
      }
    }

    [Description("Gets or sets the End member.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    [DefaultValue("")]
    public string EndMember
    {
      get
      {
        return this.GanttViewElement.EndMember;
      }
      set
      {
        this.GanttViewElement.EndMember = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets the Progress member.")]
    public string ProgressMember
    {
      get
      {
        return this.GanttViewElement.ProgressMember;
      }
      set
      {
        this.GanttViewElement.ProgressMember = value;
      }
    }

    [DefaultValue("")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    [Description("Gets or sets the name of the list or table in the data source from which the RadGanttView will get data for the links it will be displaying.")]
    [Browsable(true)]
    public string LinkDataMember
    {
      get
      {
        return this.GanttViewElement.LinkDataMember;
      }
      set
      {
        this.GanttViewElement.LinkDataMember = value;
      }
    }

    [Description("Gets or sets the LinkStart member.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Category("Data")]
    public string LinkStartMember
    {
      get
      {
        return this.GanttViewElement.LinkStartMember;
      }
      set
      {
        this.GanttViewElement.LinkStartMember = value;
      }
    }

    [DefaultValue("")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the LinkEnd member.")]
    public string LinkEndMember
    {
      get
      {
        return this.GanttViewElement.LinkEndMember;
      }
      set
      {
        this.GanttViewElement.LinkEndMember = value;
      }
    }

    [Description("Gets or sets the LinkType member.")]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    public string LinkTypeMember
    {
      get
      {
        return this.GanttViewElement.LinkTypeMember;
      }
      set
      {
        this.GanttViewElement.LinkTypeMember = value;
      }
    }

    [Description("Gets or  a value indicating whether the control is in design mode.")]
    [Browsable(false)]
    public bool IsInDesignMode
    {
      get
      {
        if (this.ElementTree != null && this.ElementTree.Control != null)
          return this.ElementTree.Control.Site != null;
        return false;
      }
    }

    [Description("Gets or sets a value indicating whether custom painting is enabled.")]
    [DefaultValue(false)]
    public bool EnableCustomPainting
    {
      get
      {
        return this.GanttViewElement.EnableCustomPainting;
      }
      set
      {
        this.GanttViewElement.EnableCustomPainting = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(500, 300));
      }
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Return:
        case Keys.Escape:
        case Keys.End:
        case Keys.Home:
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the shortcut menu associated to the RadTreeView.")]
    public virtual RadContextMenu RadContextMenu
    {
      get
      {
        return this.GanttViewElement.ContextMenu;
      }
      set
      {
        if (this.GanttViewElement.ContextMenu == value)
          return;
        if (this.GanttViewElement.ContextMenu is GanttViewDefaultContextMenu)
          this.GanttViewElement.ContextMenu.Dispose();
        this.GanttViewElement.ContextMenu = value;
        if (this.GanttViewElement.ContextMenu == null)
          return;
        this.GanttViewElement.ContextMenu.ThemeName = this.ThemeName;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to show the timeline today indicator.")]
    [Category("Behavior")]
    public bool ShowTimelineTodayIndicator
    {
      get
      {
        return this.GanttViewElement.ShowTimelineTodayIndicator;
      }
      set
      {
        this.GanttViewElement.ShowTimelineTodayIndicator = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to show the today indicator.")]
    public bool ShowTodayIndicator
    {
      get
      {
        return this.GanttViewElement.ShowTodayIndicator;
      }
      set
      {
        this.GanttViewElement.ShowTodayIndicator = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a IGanttViewDataProvider instance, which enables integration with other controls.")]
    [DefaultValue(null)]
    public IGanttViewDataProvider DataProvider
    {
      get
      {
        return this.GanttViewElement.DataProvider;
      }
      set
      {
        this.GanttViewElement.DataProvider = value;
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Behavior")]
    [Description("Gets or sets a GanttViewPrintSettings instance, which hold the default print settings.")]
    public GanttViewPrintSettings PrintSettings
    {
      get
      {
        return this.printSettings;
      }
      set
      {
        this.printSettings = value;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the gantt view is read only.")]
    public bool ReadOnly
    {
      get
      {
        return this.GanttViewElement.ReadOnly;
      }
      set
      {
        this.GanttViewElement.ReadOnly = value;
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

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    public void BeginUpdate()
    {
      this.GanttViewElement.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.GanttViewElement.EndUpdate();
    }

    [Category("Action")]
    [Description("Occurs when an item needs an id for storing in data sources.")]
    public event GanttViewItemChildIdNeededEventHandler ItemChildIdNeeded
    {
      add
      {
        this.GanttViewElement.ItemChildIdNeeded += value;
      }
      remove
      {
        this.GanttViewElement.ItemChildIdNeeded -= value;
      }
    }

    [Description("Occurs when an item is painted. Allows custom painting over the item. EnableCustomPainting must be set to true for this event to be fired.")]
    [Category("Action")]
    public event GanttViewItemPaintEventHandler ItemPaint
    {
      add
      {
        this.GanttViewElement.ItemPaint += value;
      }
      remove
      {
        this.GanttViewElement.ItemPaint -= value;
      }
    }

    [Description("Occurs when a context menu is about to be opened.")]
    [Category("Action")]
    public event GanttViewContextMenuOpeningEventHandler ContextMenuOpening
    {
      add
      {
        this.GanttViewElement.ContextMenuOpening += value;
      }
      remove
      {
        this.GanttViewElement.ContextMenuOpening -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when a new data item is created.")]
    public event CreateGanttDataItemEventHandler CreateDataItem
    {
      add
      {
        this.GanttViewElement.CreateDataItem += value;
      }
      remove
      {
        this.GanttViewElement.CreateDataItem -= value;
      }
    }

    [Description("Occurs when a new link data item is created.")]
    [Category("Action")]
    public event CreateGanttLinkDataItemEventHandler CreateLinkDataItem
    {
      add
      {
        this.GanttViewElement.CreateLinkDataItem += value;
      }
      remove
      {
        this.GanttViewElement.CreateLinkDataItem -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs before an GanttViewDataItem is added to the Items collection.")]
    public event GanttItemAddingEventHandler ItemAdding
    {
      add
      {
        this.GanttViewElement.ItemAdding += value;
      }
      remove
      {
        this.GanttViewElement.ItemAdding -= value;
      }
    }

    [Description("Occurs before an GanttViewLinkDataItem is added to the Links collection.")]
    [Category("Action")]
    public event GanttLinkAddingEventHandler LinkAdding
    {
      add
      {
        this.GanttViewElement.LinkAdding += value;
      }
      remove
      {
        this.GanttViewElement.LinkAdding -= value;
      }
    }

    [Description("Occurs when there is an error in the data layer of RadGanttView related to data operations with Item objects.")]
    [Category("Action")]
    public event GanttItemDataErrorEventHandler ItemDataError
    {
      add
      {
        this.GanttViewElement.ItemDataError += value;
      }
      remove
      {
        this.GanttViewElement.ItemDataError -= value;
      }
    }

    [Description("Occurs when there is an error in the data layer of RadGanttView related to data operations with Link objects.")]
    [Category("Action")]
    public event GanttLinkDataErrorEventHandler LinkDataError
    {
      add
      {
        this.GanttViewElement.LinkDataError += value;
      }
      remove
      {
        this.GanttViewElement.LinkDataError -= value;
      }
    }

    [Description("Occurs when the selected item is about to be changed.")]
    [Category("Action")]
    public event GanttViewSelectedItemChangingEventHandler SelectedItemChanging
    {
      add
      {
        this.GanttViewElement.SelectedItemChanging += value;
      }
      remove
      {
        this.GanttViewElement.SelectedItemChanging -= value;
      }
    }

    [Description("Occurs when selected item has been changed.")]
    [Category("Action")]
    public event GanttViewSelectedItemChangedEventHandler SelectedItemChanged
    {
      add
      {
        this.GanttViewElement.SelectedItemChanged += value;
      }
      remove
      {
        this.GanttViewElement.SelectedItemChanged -= value;
      }
    }

    [Description("Occurs when the selected link is about to be changed.")]
    [Category("Action")]
    public event GanttViewSelectedLinkChangingEventHandler SelectedLinkChanging
    {
      add
      {
        this.GanttViewElement.SelectedLinkChanging += value;
      }
      remove
      {
        this.GanttViewElement.SelectedLinkChanging -= value;
      }
    }

    [Description("Occurs when selected link has been changed.")]
    [Category("Action")]
    public event GanttViewSelectedLinkChangedEventHandler SelectedLinkChanged
    {
      add
      {
        this.GanttViewElement.SelectedLinkChanged += value;
      }
      remove
      {
        this.GanttViewElement.SelectedLinkChanged -= value;
      }
    }

    [Description("Occurs when an item is about to be expanded or collapsed.")]
    [Category("Action")]
    public event GanttViewExpandedChangingEventHandler ItemExpandedChanging
    {
      add
      {
        this.GanttViewElement.ItemExpandedChanging += value;
      }
      remove
      {
        this.GanttViewElement.ItemExpandedChanging -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs after an item is expanded or collapsed.")]
    public event GanttViewExpandedChangedEventHandler ItemExpandedChanged
    {
      add
      {
        this.GanttViewElement.ItemExpandedChanged += value;
      }
      remove
      {
        this.GanttViewElement.ItemExpandedChanged -= value;
      }
    }

    [Description("Occurs when an item is data bound.")]
    [Category("Action")]
    public event GanttViewItemDataBoundEventHandler ItemDataBound
    {
      add
      {
        this.GanttViewElement.ItemDataBound += value;
      }
      remove
      {
        this.GanttViewElement.ItemDataBound -= value;
      }
    }

    [Description("Occurs when a new item is added to the Items collection.")]
    [Category("Action")]
    public event GanttViewItemAddedEventHandler ItemAdded
    {
      add
      {
        this.GanttViewElement.ItemAdded += value;
      }
      remove
      {
        this.GanttViewElement.ItemAdded -= value;
      }
    }

    [Description("Occurs when an item removed from the Items collection.")]
    [Category("Action")]
    public event GanttViewItemRemovedEventHandler ItemRemoved
    {
      add
      {
        this.GanttViewElement.ItemRemoved += value;
      }
      remove
      {
        this.GanttViewElement.ItemRemoved -= value;
      }
    }

    [Description("Occurs when an item's property is changed.")]
    [Category("Action")]
    public event GanttViewItemChangedEventhandler ItemChanged
    {
      add
      {
        this.GanttViewElement.ItemChanged += value;
      }
      remove
      {
        this.GanttViewElement.ItemChanged -= value;
      }
    }

    [Description("Occurs when a link is data bound.")]
    [Category("Action")]
    public event GanttViewLinkDataBoundEventHandler LinkDataBound
    {
      add
      {
        this.GanttViewElement.LinkDataBound += value;
      }
      remove
      {
        this.GanttViewElement.LinkDataBound -= value;
      }
    }

    [Description("Occurs when a new link added to the Links collection.")]
    [Category("Action")]
    public event GanttViewLinkAddedEventHandler LinkAdded
    {
      add
      {
        this.GanttViewElement.LinkAdded += value;
      }
      remove
      {
        this.GanttViewElement.LinkAdded -= value;
      }
    }

    [Description("Occurs when a link is removed from the Links collection.")]
    [Category("Action")]
    public event GanttViewLinkRemovedEventHandler LinkRemoved
    {
      add
      {
        this.GanttViewElement.LinkRemoved += value;
      }
      remove
      {
        this.GanttViewElement.LinkRemoved -= value;
      }
    }

    [Description("Occurs when a new header cell element needs to be created.")]
    [Category("Action")]
    public event GanttViewHeaderCellElementCreatingEventHandler HeaderCellElementCreating
    {
      add
      {
        this.GanttViewElement.HeaderCellElementCreating += value;
      }
      remove
      {
        this.GanttViewElement.HeaderCellElementCreating -= value;
      }
    }

    [Description("Occurs when a new data cell element needs to be created.")]
    [Category("Action")]
    public event GanttViewDataCellElementCreatingEventHandler DataCellElementCreating
    {
      add
      {
        this.GanttViewElement.DataCellElementCreating += value;
      }
      remove
      {
        this.GanttViewElement.DataCellElementCreating -= value;
      }
    }

    [Description("Occurs when the content of a cell needs to be formatted for display.")]
    [Category("Action")]
    public event GanttViewTextViewCellFormattingEventHandler TextViewCellFormatting
    {
      add
      {
        this.GanttViewElement.TextViewCellFormatting += value;
      }
      remove
      {
        this.GanttViewElement.TextViewCellFormatting -= value;
      }
    }

    [Description("Occurs when an item in the GanttViewTextViewElement state changes and it needs to be formatted.")]
    [Category("Action")]
    public event GanttViewTextViewItemFormattingEventHandler TextViewItemFormatting
    {
      add
      {
        this.GanttViewElement.TextViewItemFormatting += value;
      }
      remove
      {
        this.GanttViewElement.TextViewItemFormatting -= value;
      }
    }

    [Description("Occurs when the state of a timeline item changes and it needs to be formatted.")]
    [Category("Action")]
    public event GanttViewTimelineItemFormattingEventHandler TimelineItemFormatting
    {
      add
      {
        this.GanttViewElement.TimelineItemFormatting += value;
      }
      remove
      {
        this.GanttViewElement.TimelineItemFormatting -= value;
      }
    }

    [Description("Occurs when the state of an item in the GanttViewGraphicalViewElement changes and it needs to be formatted.")]
    [Category("Action")]
    public event GanttViewGraphicalViewItemFormattingEventHandler GraphicalViewItemFormatting
    {
      add
      {
        this.GanttViewElement.GraphicalViewItemFormatting += value;
      }
      remove
      {
        this.GanttViewElement.GraphicalViewItemFormatting -= value;
      }
    }

    [Description("Occurs when the state of a link item in the GanttViewGraphicalViewElement changes and it needs to be formatted.")]
    [Category("Action")]
    public event GanttViewLinkItemFormattingEventHandler GraphicalViewLinkItemFormatting
    {
      add
      {
        this.GanttViewElement.GraphicalViewLinkItemFormatting += value;
      }
      remove
      {
        this.GanttViewElement.GraphicalViewLinkItemFormatting -= value;
      }
    }

    [Description("Occurs when an item element needs to be created.")]
    [Category("Action")]
    public event GanttViewItemElementCreatingEventHandler ItemElementCreating
    {
      add
      {
        this.GanttViewElement.ItemElementCreating += value;
      }
      remove
      {
        this.GanttViewElement.ItemElementCreating -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when a timeline item element needs to be created.")]
    public event GanttViewTimelineItemElementCreatingEventHandler TimelineItemElementCreating
    {
      add
      {
        this.GanttViewElement.TimelineItemElementCreating += value;
      }
      remove
      {
        this.GanttViewElement.TimelineItemElementCreating -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when an element will be printed. Allows formatting of the element.")]
    public event GanttViewPrintElementFormattingEventHandler PrintElementFormatting;

    protected virtual void OnPrintElementFormatting(GanttViewPrintElementFormattingEventArgs e)
    {
      if (this.PrintElementFormatting == null)
        return;
      this.PrintElementFormatting((object) this, e);
    }

    [Category("Action")]
    [Description("Occurs after an element is printed. Allows for custom painting over the element.")]
    public event GanttViewPrintElementPaintEventHandler PrintElementPaint;

    protected virtual void OnPrintElementPaint(GanttViewPrintElementPaintEventArgs e)
    {
      if (this.PrintElementPaint == null)
        return;
      this.PrintElementPaint((object) this, e);
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      if (this.BindingContext != null)
        this.GanttViewElement.BindingContext = this.BindingContext;
      base.OnBindingContextChanged(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.GanttViewElement.ProcessMouseDown(e))
        return;
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.GanttViewElement.ProcessMouseMove(e))
        return;
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.GanttViewElement.ProcessMouseUp(e))
        return;
      base.OnMouseUp(e);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      if (this.GanttViewElement.ProcessMouseClick(e))
        return;
      base.OnMouseClick(e);
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (this.GanttViewElement.ProcessDoubleClick(e))
        return;
      base.OnMouseDoubleClick(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.GanttViewElement.ProcessMouseEnter(e))
        return;
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.GanttViewElement.ProcessMouseLeave(e))
        return;
      base.OnMouseLeave(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (this.GanttViewElement.ProcessMouseWheel(e))
        return;
      base.OnMouseWheel(e);
    }

    protected override void OnMouseHover(EventArgs e)
    {
      if (this.GanttViewElement.ProcessMouseHover(e))
        return;
      base.OnMouseHover(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.GanttViewElement.ProcessKeyDown(e))
        return;
      base.OnKeyDown(e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (this.GanttViewElement.ProcessKeyPress(e))
        return;
      base.OnKeyPress(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (this.GanttViewElement.ProcessKeyUp(e))
        return;
      base.OnKeyUp(e);
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (this.GanttViewElement.GanttViewBehavior.ProcessDialogKey(keyData))
        return true;
      return base.ProcessDialogKey(keyData);
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == 123)
        this.WmContextMenu(ref m);
      else
        base.WndProc(ref m);
    }

    private void WmContextMenu(ref Message m)
    {
      int x = Telerik.WinControls.NativeMethods.Util.SignedLOWORD(m.LParam);
      int y = Telerik.WinControls.NativeMethods.Util.SignedHIWORD(m.LParam);
      Point point = (int) (long) m.LParam != -1 ? this.PointToClient(new Point(x, y)) : new Point(this.Width / 2, this.Height / 2);
      if (!this.GanttViewElement.GanttViewBehavior.ProcessContextMenu(point))
      {
        ContextMenu contextMenu = this.ContextMenu;
        ContextMenuStrip contextMenuStrip = contextMenu != null ? (ContextMenuStrip) null : this.ContextMenuStrip;
        if ((contextMenu != null || contextMenuStrip != null) && this.ClientRectangle.Contains(point))
        {
          if (contextMenu != null)
          {
            contextMenu.Show((Control) this, point);
            return;
          }
          contextMenuStrip?.Show((Control) this, point);
          return;
        }
      }
      this.DefWndProc(ref m);
    }

    public virtual void Print()
    {
      this.Print(false);
    }

    public virtual void Print(bool showPrinterSettings)
    {
      RadPrintDocument document = new RadPrintDocument();
      this.Print(showPrinterSettings, document);
    }

    public virtual void Print(bool showPrinterSettings, RadPrintDocument document)
    {
      if (document == null)
        return;
      document.AssociatedObject = (IPrintable) this;
      if (showPrinterSettings)
      {
        if (new PrintDialog() { Document = ((PrintDocument) document), AllowPrintToFile = true, AllowCurrentPage = true, AllowSelection = true, AllowSomePages = true, UseEXDialog = true }.ShowDialog() != DialogResult.OK)
          return;
        document.Print();
      }
      else
        document.Print();
    }

    public virtual void PrintPreview()
    {
      this.PrintPreview(new RadPrintDocument());
    }

    public virtual void PrintPreview(RadPrintDocument document)
    {
      if (document == null)
        return;
      document.AssociatedObject = (IPrintable) this;
      RadPrintPreviewDialog printPreviewDialog = new RadPrintPreviewDialog(document);
      printPreviewDialog.ThemeName = this.ThemeName;
      int num = (int) printPreviewDialog.ShowDialog();
    }

    public int BeginPrint(RadPrintDocument sender, PrintEventArgs args)
    {
      Margins margins = sender.DefaultPageSettings.Margins;
      float left = (float) margins.Left;
      float y = (float) (sender.HeaderHeight + margins.Top);
      float height = (float) (sender.DefaultPageSettings.Bounds.Height - (sender.HeaderHeight + sender.FooterHeight + margins.Top + margins.Bottom));
      float width = (float) (sender.DefaultPageSettings.Bounds.Width - (margins.Left + margins.Right));
      if (this.PrintSettings.TimelineVisibleOnEveryPage)
        height -= (float) this.HeaderHeight;
      this.drawArea = new RectangleF(left, y, width, height);
      float num1 = (float) ((this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineEnd.AddDays(1.0) - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this.GanttViewElement);
      ganttViewTraverser.TraverseAllItems = true;
      List<GanttViewDataItem> ganttViewDataItemList = new List<GanttViewDataItem>();
      while (ganttViewTraverser.MoveNext())
        ganttViewDataItemList.Add(ganttViewTraverser.Current);
      float num2 = (float) (ganttViewDataItemList.Count * this.ItemHeight + this.HeaderHeight);
      if (this.PrintSettings.PrintTextViewPart)
        num1 += this.drawArea.Width;
      this.colCount = (int) Math.Ceiling((double) num1 / (double) this.drawArea.Width);
      this.rowCount = (int) Math.Ceiling((double) num2 / (double) this.drawArea.Height);
      this.printBmp = new Bitmap((int) num1, (int) num2);
      this.timelineBmp = new Bitmap((int) num1, this.HeaderHeight);
      if (!this.bitmapCreated)
      {
        this.bitmapCreated = true;
        this.DrawTextViewAndGraphicalViewToBitmap(this.printBmp);
        this.DrawHeaderAndTimelineViewToBitmap(this.timelineBmp);
      }
      return this.colCount * this.rowCount;
    }

    public bool EndPrint(RadPrintDocument sender, PrintEventArgs args)
    {
      this.bitmapCreated = false;
      this.rowCount = 0;
      this.colCount = 0;
      this.printBmp.Dispose();
      this.timelineBmp.Dispose();
      return true;
    }

    public bool PrintPage(int pageNumber, RadPrintDocument sender, PrintPageEventArgs args)
    {
      this.DrawCurrentPage(args.Graphics, sender.PrintedPage);
      return sender.PrintedPage < sender.PageCount;
    }

    public Form GetSettingsDialog(RadPrintDocument document)
    {
      return (Form) new GanttViewPrintSettingsDialog(document);
    }

    protected virtual void DrawCurrentPage(Graphics g, int printedPage)
    {
      int num1 = printedPage - 1;
      int num2;
      int num3;
      if (this.PrintSettings.PrintDirection == GanttPrintDirection.RowMajorOrder)
      {
        num2 = num1 % this.rowCount;
        num3 = num1 / this.rowCount;
      }
      else
      {
        num3 = num1 % this.colCount;
        num2 = num1 / this.colCount;
      }
      RectangleF rect = new RectangleF((float) num3 * this.drawArea.Width, (float) num2 * this.drawArea.Height, this.drawArea.Width, this.drawArea.Height);
      rect.Intersect((RectangleF) new Rectangle(Point.Empty, this.printBmp.Size));
      using (Bitmap bitmap1 = this.printBmp.Clone(rect, this.printBmp.PixelFormat))
      {
        PointF point = this.drawArea.Location;
        if (num2 == 0 || this.PrintSettings.TimelineVisibleOnEveryPage)
        {
          using (Bitmap bitmap2 = this.timelineBmp.Clone(new RectangleF(rect.X, 0.0f, rect.Width, (float) this.timelineBmp.Height), this.timelineBmp.PixelFormat))
            g.DrawImage((Image) bitmap2, this.drawArea.Location);
          if (num2 > 0 && this.PrintSettings.TimelineVisibleOnEveryPage)
            point = new PointF(this.drawArea.Location.X, (float) ((double) this.drawArea.Location.Y + (double) this.HeaderHeight + 2.0));
        }
        g.DrawImage((Image) bitmap1, point);
      }
    }

    protected virtual void DrawTextViewAndGraphicalViewToBitmap(Bitmap bmp)
    {
      using (Graphics g = Graphics.FromImage((Image) bmp))
      {
        this.DrawLinksToBitmap(g);
        this.DrawTextViewCellsAndTasksToBitmap(g);
      }
    }

    protected virtual void DrawTextViewCellsAndTasksToBitmap(Graphics g)
    {
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this.GanttViewElement);
      ganttViewTraverser.TraverseAllItems = true;
      List<GanttViewDataItem> ganttViewDataItemList = new List<GanttViewDataItem>();
      while (ganttViewTraverser.MoveNext())
        ganttViewDataItemList.Add(ganttViewTraverser.Current);
      int num1 = 0;
      foreach (GanttViewTextViewColumn column in (Collection<GanttViewTextViewColumn>) this.Columns)
      {
        if (column.Visible)
          num1 += column.Width;
      }
      float num2 = (this.drawArea.Width - 1f) / (float) num1;
      for (int index = 0; index < ganttViewDataItemList.Count; ++index)
      {
        GanttViewDataItem ganttViewDataItem = ganttViewDataItemList[index];
        RectangleF printRectangle = this.GetPrintRectangle(ganttViewDataItem, index);
        RectangleF rectangleF = new RectangleF(0.0f, printRectangle.Y, this.drawArea.Width, printRectangle.Height);
        if (this.PrintSettings.PrintTextViewPart)
        {
          float x = rectangleF.X;
          foreach (GanttViewTextViewColumn column in (Collection<GanttViewTextViewColumn>) this.Columns)
          {
            if (column.Visible)
            {
              float width = (float) column.Width * num2;
              RectangleF rect = new RectangleF(x, rectangleF.Y, width, rectangleF.Height);
              object obj = ganttViewDataItem[column];
              string empty = string.Empty;
              string text;
              if ((object) column.DataType == (object) typeof (DateTime))
              {
                DateTime dateTime = !(obj is DateTime) ? Convert.ToDateTime(obj) : (DateTime) obj;
                text = string.Format(column.FormatString ?? "{0}", (object) dateTime);
              }
              else if (TelerikHelper.IsNumericType(column.DataType))
              {
                Decimal num3 = Convert.ToDecimal(obj);
                text = string.Format(column.FormatString ?? "{0}", (object) num3);
              }
              else
                text = string.Format(column.FormatString ?? "{0}", obj);
              this.PrintGanttViewElement(g, GanttViewPrintElementContext.DataCell, rect, text, (object) ganttViewDataItem, column.Name);
              x += width;
            }
          }
          printRectangle.Offset(this.drawArea.Width, 0.0f);
        }
        if (this.shouldPrintTimelineElement)
        {
          if (ganttViewDataItem.Items.Count > 0)
            this.PrintGanttViewElement(g, GanttViewPrintElementContext.SummaryTaskElement, printRectangle, string.Empty, (object) ganttViewDataItem);
          else if (ganttViewDataItem.Start == ganttViewDataItem.End)
            this.PrintGanttViewElement(g, GanttViewPrintElementContext.MilestoneElement, printRectangle, string.Empty, (object) ganttViewDataItem);
          else
            this.PrintGanttViewElement(g, GanttViewPrintElementContext.TaskElement, printRectangle, Convert.ToString(ganttViewDataItem.Title), (object) ganttViewDataItem);
        }
      }
    }

    protected virtual void DrawLinksToBitmap(Graphics g)
    {
      GanttViewTraverser ganttViewTraverser = new GanttViewTraverser(this.GanttViewElement);
      ganttViewTraverser.TraverseAllItems = true;
      List<GanttViewDataItem> ganttViewDataItemList = new List<GanttViewDataItem>();
      while (ganttViewTraverser.MoveNext())
        ganttViewDataItemList.Add(ganttViewTraverser.Current);
      using (Pen pen = new Pen(this.PrintSettings.LinksColor))
      {
        pen.EndCap = LineCap.ArrowAnchor;
        if (this.PrintSettings.PrintTextViewPart)
          g.TranslateTransform(this.drawArea.Width, 0.0f);
        foreach (GanttViewLinkDataItem link in (Collection<GanttViewLinkDataItem>) this.Links)
        {
          List<PointF> linkLines = this.GetLinkLines(link, ganttViewDataItemList.IndexOf(link.StartItem), ganttViewDataItemList.IndexOf(link.EndItem));
          if (linkLines.Count > 3)
            g.DrawLines(pen, linkLines.ToArray());
        }
        if (!this.PrintSettings.PrintTextViewPart)
          return;
        g.ResetTransform();
      }
    }

    protected virtual void DrawHeaderAndTimelineViewToBitmap(Bitmap bmp)
    {
      using (Graphics g = Graphics.FromImage((Image) bmp))
      {
        this.DrawHeaderCellsToBitmap(g);
        this.DrawTimelineItemsToBitmap(g);
      }
    }

    protected virtual void DrawTimelineItemsToBitmap(Graphics g)
    {
      float x1 = 0.0f;
      float y1 = 0.0f;
      float num1 = (float) (this.HeaderHeight - 1);
      if (this.PrintSettings.PrintTextViewPart)
        x1 += this.drawArea.Width;
      foreach (GanttViewTimelineDataItem timelineItem in (Collection<GanttViewTimelineDataItem>) this.GanttViewElement.GraphicalViewElement.TimelineItems)
      {
        RectangleF rect = new RectangleF(x1, y1, timelineItem.Width, num1 / 2f);
        string timelineTopElementText = this.GanttViewElement.GraphicalViewElement.TimelineBehavior.GetTimelineTopElementText(timelineItem);
        this.PrintGanttViewElement(g, GanttViewPrintElementContext.TimelineUpperElement, rect, timelineTopElementText, (object) timelineItem);
        int num2 = (int) Math.Round((timelineItem.End - timelineItem.Start).TotalDays, MidpointRounding.AwayFromZero);
        float width = timelineItem.Width / (float) num2;
        float x2 = x1;
        float y2 = y1 + num1 / 2f;
        for (int index = 0; index < num2; ++index)
        {
          rect = new RectangleF(x2, y2, width, num1 / 2f);
          string bottomElementText = this.GanttViewElement.GraphicalViewElement.TimelineBehavior.GetTimelineBottomElementText(timelineItem, index);
          this.PrintGanttViewElement(g, GanttViewPrintElementContext.TimelineBottomElement, rect, bottomElementText, (object) timelineItem);
          x2 += width;
        }
        x1 += timelineItem.Width;
      }
    }

    protected virtual void DrawHeaderCellsToBitmap(Graphics g)
    {
      int num1 = 0;
      foreach (GanttViewTextViewColumn column in (Collection<GanttViewTextViewColumn>) this.Columns)
      {
        if (column.Visible)
          num1 += column.Width;
      }
      float num2 = (this.drawArea.Width - 1f) / (float) num1;
      RectangleF rectangleF = new RectangleF(0.0f, 0.0f, this.drawArea.Width, (float) this.HeaderHeight);
      float x = rectangleF.X;
      foreach (GanttViewTextViewColumn column in (Collection<GanttViewTextViewColumn>) this.Columns)
      {
        if (column.Visible)
        {
          float width = (float) column.Width * num2;
          RectangleF rect = new RectangleF(x, rectangleF.Y, width, rectangleF.Height);
          this.PrintGanttViewElement(g, GanttViewPrintElementContext.HeaderCell, rect, column.HeaderText, (object) column, column.Name);
          x += width;
        }
      }
    }

    protected virtual void PrintGanttViewElement(
      Graphics g,
      GanttViewPrintElementContext context,
      RectangleF rect,
      string text,
      object dataItem)
    {
      this.PrintGanttViewElement(g, context, rect, text, dataItem, (string) null);
    }

    protected virtual void PrintGanttViewElement(
      Graphics g,
      GanttViewPrintElementContext context,
      RectangleF rect,
      string text,
      object dataItem,
      string columnName)
    {
      GanttViewPrintElement printElement = this.GetPrintElement(context);
      printElement.Polygon = this.GetElementShape(context, rect);
      printElement.Text = text;
      GanttViewPrintElementFormattingEventArgs e = new GanttViewPrintElementFormattingEventArgs(context, printElement, dataItem, columnName);
      this.OnPrintElementFormatting(e);
      e.PrintElement.Paint(g, rect);
      this.OnPrintElementPaint(new GanttViewPrintElementPaintEventArgs(g, rect, context, printElement, dataItem, columnName));
    }

    protected virtual PointF[] GetElementShape(
      GanttViewPrintElementContext context,
      RectangleF rect)
    {
      PointF[] pointFArray = (PointF[]) null;
      switch (context)
      {
        case GanttViewPrintElementContext.SummaryTaskElement:
          if (!this.isPrintRectangleTrimmed)
          {
            pointFArray = new PointF[6]
            {
              rect.Location,
              new PointF(rect.X, rect.Y + rect.Height / 2f),
              new PointF(rect.X + 3f, rect.Y + rect.Height / 4f),
              new PointF(rect.Right - 3f, rect.Y + rect.Height / 4f),
              new PointF(rect.Right, rect.Y + rect.Height / 2f),
              new PointF(rect.Right, rect.Y)
            };
            break;
          }
          pointFArray = new PointF[5]
          {
            rect.Location,
            new PointF(rect.X, rect.Y + rect.Height / 4f),
            new PointF(rect.Right - 3f, rect.Y + rect.Height / 4f),
            new PointF(rect.Right, rect.Y + rect.Height / 2f),
            new PointF(rect.Right, rect.Y)
          };
          break;
        case GanttViewPrintElementContext.MilestoneElement:
          if (!this.isPrintRectangleTrimmed)
          {
            pointFArray = new PointF[4]
            {
              new PointF(rect.X, rect.Y),
              new PointF(rect.X + rect.Height / 2f, rect.Y + rect.Height / 2f),
              new PointF(rect.X, rect.Y + rect.Height),
              new PointF(rect.X - rect.Height / 2f, rect.Y + rect.Height / 2f)
            };
            break;
          }
          pointFArray = new PointF[3]
          {
            new PointF(rect.X, rect.Y),
            new PointF(rect.X + rect.Height / 2f, rect.Y + rect.Height / 2f),
            new PointF(rect.X, rect.Y + rect.Height)
          };
          break;
      }
      return pointFArray;
    }

    protected virtual GanttViewPrintElement GetPrintElement(
      GanttViewPrintElementContext context)
    {
      GanttViewPrintElement viewPrintElement = new GanttViewPrintElement();
      viewPrintElement.DrawFill = true;
      viewPrintElement.DrawBorder = true;
      viewPrintElement.DrawText = true;
      switch (context)
      {
        case GanttViewPrintElementContext.HeaderCell:
          viewPrintElement.BackColor = this.PrintSettings.HeaderCellFill;
          viewPrintElement.BorderColor = this.PrintSettings.HeaderCellBorder;
          viewPrintElement.ForeColor = this.PrintSettings.HeaderCellForeColor;
          if (this.PrintSettings.HeaderCellFont == null)
            this.PrintSettings.HeaderCellFont = SystemFonts.DialogFont;
          viewPrintElement.Font = this.PrintSettings.HeaderCellFont;
          break;
        case GanttViewPrintElementContext.DataCell:
          viewPrintElement.BackColor = this.PrintSettings.DataCellFill;
          viewPrintElement.BorderColor = this.PrintSettings.DataCellBorder;
          viewPrintElement.ForeColor = this.PrintSettings.DataCellForeColor;
          if (this.PrintSettings.DataCellFont == null)
            this.PrintSettings.DataCellFont = SystemFonts.DialogFont;
          viewPrintElement.Font = this.PrintSettings.DataCellFont;
          break;
        case GanttViewPrintElementContext.TaskElement:
          viewPrintElement.BackColor = this.PrintSettings.TaskFill;
          viewPrintElement.BorderColor = this.PrintSettings.TaskBorder;
          viewPrintElement.ForeColor = this.PrintSettings.TaskForeColor;
          if (this.PrintSettings.TaskFont == null)
            this.PrintSettings.TaskFont = SystemFonts.DialogFont;
          viewPrintElement.Font = this.PrintSettings.TaskFont;
          break;
        case GanttViewPrintElementContext.SummaryTaskElement:
          viewPrintElement.BackColor = this.PrintSettings.SummaryTaskFill;
          viewPrintElement.BorderColor = this.PrintSettings.SummaryTaskBorder;
          viewPrintElement.DrawText = false;
          break;
        case GanttViewPrintElementContext.MilestoneElement:
          viewPrintElement.BackColor = this.PrintSettings.MilestoneTaskFill;
          viewPrintElement.BorderColor = this.PrintSettings.MilestoneTaskBorder;
          viewPrintElement.DrawText = false;
          break;
        case GanttViewPrintElementContext.TimelineUpperElement:
          viewPrintElement.BackColor = this.PrintSettings.TimelineTopRowFill;
          viewPrintElement.BorderColor = this.PrintSettings.TimelineTopRowBorder;
          viewPrintElement.ForeColor = this.PrintSettings.TimelineTopRowForeColor;
          if (this.PrintSettings.TimelineTopRowFont == null)
            this.PrintSettings.TimelineTopRowFont = SystemFonts.DialogFont;
          viewPrintElement.Font = this.PrintSettings.TimelineTopRowFont;
          break;
        case GanttViewPrintElementContext.TimelineBottomElement:
          viewPrintElement.BackColor = this.PrintSettings.TimelineBottomRowFill;
          viewPrintElement.BorderColor = this.PrintSettings.TimelineBottomRowBorder;
          viewPrintElement.ForeColor = this.PrintSettings.TimelineBottomRowForeColor;
          if (this.PrintSettings.TimelineBottomRowFont == null)
            this.PrintSettings.TimelineBottomRowFont = SystemFonts.DialogFont;
          viewPrintElement.Font = this.PrintSettings.TimelineBottomRowFont;
          break;
      }
      return viewPrintElement;
    }

    protected virtual RectangleF GetPrintRectangle(GanttViewDataItem item, int index)
    {
      if (index < 0)
        return RectangleF.Empty;
      float val1 = (float) ((item.Start - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      float y = (float) (index * (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) + this.GanttViewElement.HeaderHeight);
      float width = (float) ((item.End - item.Start).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      int num = this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing;
      if ((double) width == 0.0)
        width = (float) num;
      this.shouldPrintTimelineElement = (double) val1 + (double) width > 0.0;
      this.isPrintRectangleTrimmed = (double) val1 <= 0.0;
      if (this.isPrintRectangleTrimmed && this.shouldPrintTimelineElement)
        width += val1;
      return new RectangleF(Math.Max(val1, 0.0f), y, width, (float) num);
    }

    protected virtual List<PointF> GetLinkLines(
      GanttViewLinkDataItem link,
      int startItemIndex,
      int endItemIndex)
    {
      List<PointF> pointFList = new List<PointF>();
      if (link.StartItem == null || link.StartItem.GanttViewElement == null || (link.EndItem == null || link.EndItem.GanttViewElement == null))
        return pointFList;
      switch (link.LinkType)
      {
        case TasksLinkType.FinishToFinish:
          return this.GetFinishToFinishLines(link, startItemIndex, endItemIndex);
        case TasksLinkType.FinishToStart:
          return this.GetFinishToStartLines(link, startItemIndex, endItemIndex);
        case TasksLinkType.StartToFinish:
          return this.GetStartToFinishLines(link, startItemIndex, endItemIndex);
        case TasksLinkType.StartToStart:
          return this.GetStartToStartLines(link, startItemIndex, endItemIndex);
        default:
          return pointFList;
      }
    }

    protected virtual List<PointF> GetStartToStartLines(
      GanttViewLinkDataItem link,
      int startItemIndex,
      int endItemIndex)
    {
      int num = (int) Math.Round((double) (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) / 2.0, MidpointRounding.AwayFromZero);
      int x1 = (int) ((link.StartItem.Start - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      int y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * startItemIndex + num;
      int x2 = (int) ((link.EndItem.Start - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      int y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * endItemIndex + num;
      List<PointF> pointFList = new List<PointF>();
      pointFList.Add((PointF) new Point(x1, y1));
      if (x1 < x2)
      {
        pointFList.Add((PointF) new Point(x1 - this.GanttViewElement.MinimumLinkLength, y1));
        pointFList.Add((PointF) new Point(x1 - this.GanttViewElement.MinimumLinkLength, y2));
      }
      else
      {
        pointFList.Add((PointF) new Point(x2 - this.GanttViewElement.MinimumLinkLength, y1));
        pointFList.Add((PointF) new Point(x2 - this.GanttViewElement.MinimumLinkLength, y2));
      }
      pointFList.Add((PointF) new Point(x2, y2));
      return pointFList;
    }

    protected virtual List<PointF> GetStartToFinishLines(
      GanttViewLinkDataItem link,
      int startItemIndex,
      int endItemIndex)
    {
      int num = (int) Math.Round((double) (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) / 2.0, MidpointRounding.AwayFromZero);
      int x1 = (int) ((link.StartItem.Start - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      int y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * startItemIndex + num;
      int x2 = (int) ((link.EndItem.End - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      int y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * endItemIndex + num;
      List<PointF> pointFList = new List<PointF>();
      pointFList.Add((PointF) new Point(x1, y1));
      if (x1 - this.GanttViewElement.MinimumLinkLength > x2 + this.GanttViewElement.MinimumLinkLength)
      {
        int x3 = x1 - (x1 - x2) / 2;
        pointFList.Add((PointF) new Point(x3, y1));
        pointFList.Add((PointF) new Point(x3, y2));
      }
      else
      {
        int y3 = y1 + (y2 - y1) / 2;
        pointFList.Add((PointF) new Point(x1 - this.GanttViewElement.MinimumLinkLength, y1));
        pointFList.Add((PointF) new Point(x1 - this.GanttViewElement.MinimumLinkLength, y3));
        pointFList.Add((PointF) new Point(x2 + this.GanttViewElement.MinimumLinkLength, y3));
        pointFList.Add((PointF) new Point(x2 + this.GanttViewElement.MinimumLinkLength, y2));
      }
      pointFList.Add((PointF) new Point(x2, y2));
      return pointFList;
    }

    protected virtual List<PointF> GetFinishToStartLines(
      GanttViewLinkDataItem link,
      int startItemIndex,
      int endItemIndex)
    {
      int num = (int) Math.Round((double) (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) / 2.0, MidpointRounding.AwayFromZero);
      int x1 = (int) ((link.StartItem.End - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      int y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * startItemIndex + num;
      int x2 = (int) ((link.EndItem.Start - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      int y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * endItemIndex + num;
      List<PointF> pointFList = new List<PointF>();
      pointFList.Add((PointF) new Point(x1, y1));
      if (x1 + this.GanttViewElement.MinimumLinkLength < x2 - this.GanttViewElement.MinimumLinkLength)
      {
        int x3 = x1 + (x2 - x1) / 2;
        pointFList.Add((PointF) new Point(x3, y1));
        pointFList.Add((PointF) new Point(x3, y2));
      }
      else
      {
        int y3 = y1 + (y2 - y1) / 2;
        pointFList.Add((PointF) new Point(x1 + this.GanttViewElement.MinimumLinkLength, y1));
        pointFList.Add((PointF) new Point(x1 + this.GanttViewElement.MinimumLinkLength, y3));
        pointFList.Add((PointF) new Point(x2 - this.GanttViewElement.MinimumLinkLength, y3));
        pointFList.Add((PointF) new Point(x2 - this.GanttViewElement.MinimumLinkLength, y2));
      }
      pointFList.Add((PointF) new Point(x2, y2));
      return pointFList;
    }

    protected virtual List<PointF> GetFinishToFinishLines(
      GanttViewLinkDataItem link,
      int startItemIndex,
      int endItemIndex)
    {
      int num = (int) Math.Round((double) (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) / 2.0, MidpointRounding.AwayFromZero);
      int x1 = (int) ((link.StartItem.End - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      int y1 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * startItemIndex + num;
      int x2 = (int) ((link.EndItem.End - this.GanttViewElement.GraphicalViewElement.TimelineBehavior.AdjustedTimelineStart).TotalSeconds / this.GanttViewElement.GraphicalViewElement.OnePixelTime.TotalSeconds);
      int y2 = this.GanttViewElement.HeaderHeight + (this.GanttViewElement.ItemHeight + this.GanttViewElement.ItemSpacing) * endItemIndex + num;
      List<PointF> pointFList = new List<PointF>();
      pointFList.Add((PointF) new Point(x1, y1));
      if (x1 < x2)
      {
        pointFList.Add((PointF) new Point(x2 + this.GanttViewElement.MinimumLinkLength, y1));
        pointFList.Add((PointF) new Point(x2 + this.GanttViewElement.MinimumLinkLength, y2));
      }
      else
      {
        pointFList.Add((PointF) new Point(x1 + this.GanttViewElement.MinimumLinkLength, y1));
        pointFList.Add((PointF) new Point(x1 + this.GanttViewElement.MinimumLinkLength, y2));
      }
      pointFList.Add((PointF) new Point(x2, y2));
      return pointFList;
    }
  }
}
