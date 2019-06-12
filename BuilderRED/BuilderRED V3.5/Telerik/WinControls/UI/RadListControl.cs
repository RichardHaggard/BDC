// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListControl
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.CodedUI;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
  [ClassInterface(ClassInterfaceType.AutoDispatch)]
  [ComVisible(true)]
  [TelerikToolboxCategory("Data Controls")]
  [DefaultEvent("SelectedIndexChanged")]
  [Designer("Telerik.WinControls.UI.Design.RadListControlDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [ComplexBindingProperties("DataSource", "ValueMember")]
  [ToolboxItem(true)]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  public class RadListControl : RadControl
  {
    public static readonly object SelectedIndexChangedEventKey = new object();
    public static readonly object SelectedIndexChangingEventKey = new object();
    public static readonly object SelectedValueChangedEventKey = new object();
    public static readonly object ListItemDataBindingEventKey = new object();
    public static readonly object ListItemDataBoundEventKey = new object();
    public static readonly object CreatingVisualListItemEventKey = new object();
    public static readonly object SortStyleChangedEventKey = new object();
    public static readonly object VisualItemFormattingEventKey = new object();
    private RadListElement element;

    public RadListControl()
    {
      this.SetStyle(ControlStyles.UseTextForAccessibility, false);
    }

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.element = this.CreateListElement();
      parent.Children.Add((RadElement) this.element);
      this.WireEvents();
    }

    protected virtual RadListElement CreateListElement()
    {
      return new RadListElement();
    }

    protected virtual void WireEvents()
    {
      this.element.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.element_SelectedIndexChanged);
      this.element.SelectedIndexChanging += new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.element_SelectedIndexChanging);
      this.element.SelectedValueChanged += new EventHandler(this.element_SelectedValueChanged);
      this.element.ItemDataBinding += new ListItemDataBindingEventHandler(this.element_ItemDataBinding);
      this.element.ItemDataBound += new ListItemDataBoundEventHandler(this.element_ItemDataBound);
      this.element.CreatingVisualItem += new CreatingVisualListItemEventHandler(this.element_CreatingVisualItem);
      this.element.SortStyleChanged += new SortStyleChangedEventHandler(this.element_SortStyleChanged);
      this.element.VisualItemFormatting += new VisualListItemFormattingEventHandler(this.element_VisualItemFormatting);
      this.element.PropertyChanged += new PropertyChangedEventHandler(this.element_PropertyChanged);
    }

    protected virtual void UnwireEvents()
    {
      if (this.element == null)
        return;
      this.element.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.element_SelectedIndexChanged);
      this.element.SelectedIndexChanging -= new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.element_SelectedIndexChanging);
      this.element.SelectedValueChanged -= new EventHandler(this.element_SelectedValueChanged);
      this.element.ItemDataBinding -= new ListItemDataBindingEventHandler(this.element_ItemDataBinding);
      this.element.ItemDataBound -= new ListItemDataBoundEventHandler(this.element_ItemDataBound);
      this.element.CreatingVisualItem -= new CreatingVisualListItemEventHandler(this.element_CreatingVisualItem);
      this.element.SortStyleChanged -= new SortStyleChangedEventHandler(this.element_SortStyleChanged);
      this.element.VisualItemFormatting -= new VisualListItemFormattingEventHandler(this.element_VisualItemFormatting);
      this.element.PropertyChanged -= new PropertyChangedEventHandler(this.element_PropertyChanged);
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets value indicating if the user can reorder items via drag and drop.")]
    public bool AllowDragDrop
    {
      get
      {
        return this.ListElement.AllowDragDrop;
      }
      set
      {
        this.ListElement.AllowDragDrop = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether alternating item color is enabled.")]
    public virtual bool EnableAlternatingItemColor
    {
      get
      {
        return this.ListElement.EnableAlternatingItemColor;
      }
      set
      {
        this.ListElement.EnableAlternatingItemColor = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled.")]
    [DefaultValue(false)]
    [Browsable(true)]
    public bool EnableKineticScrolling
    {
      get
      {
        return this.ListElement.EnableKineticScrolling;
      }
      set
      {
        this.ListElement.EnableKineticScrolling = value;
      }
    }

    [Description("Indicates whether the items should be displayed in groups.")]
    [Browsable(false)]
    [DefaultValue(false)]
    private bool ShowGroups
    {
      get
      {
        return this.ListElement.ShowGroups;
      }
      set
      {
        this.ListElement.ShowGroups = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets the collection of groups that items are grouped into.")]
    private ListGroupCollection Groups
    {
      get
      {
        return this.ListElement.Groups;
      }
    }

    [DefaultValue(true)]
    public bool FitItemsToSize
    {
      get
      {
        return this.ListElement.FitItemsToSize;
      }
      set
      {
        this.ClearMeasuredSize();
        this.ListElement.FitItemsToSize = value;
        this.RootElement.InvalidateMeasure(true);
        this.RootElement.InvalidateArrange(true);
        this.RootElement.UpdateLayout();
        this.ListElement.ViewElement.UpdateItems();
        this.ListElement.ViewElement.InvalidateMeasure();
        this.ListElement.ViewElement.InvalidateArrange();
        this.ListElement.Scroller.UpdateScrollRange();
      }
    }

    [DefaultValue(true)]
    [Browsable(true)]
    [Description("Gets or sets a value that indicates whether text case will be taken into account when sorting.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    public bool CaseSensitiveSort
    {
      get
      {
        return this.ListElement.CaseSensitiveSort;
      }
      set
      {
        this.ListElement.CaseSensitiveSort = value;
      }
    }

    [DefaultValue(18)]
    [Browsable(true)]
    [Category("Layout")]
    [Description("Gets or sets the item height for the items. This property is disregarded when AutoSizeItems is set to true.")]
    public int ItemHeight
    {
      get
      {
        return this.ListElement.ItemHeight;
      }
      set
      {
        this.ListElement.ItemHeight = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(300)]
    [Category("Behavior")]
    [Description("Gets or sets a value that specifies how long the user must wait before searching with the keyboard is reset.")]
    public int KeyboardSearchResetInterval
    {
      get
      {
        return this.ListElement.KeyboardSearchResetInterval;
      }
      set
      {
        this.ListElement.KeyboardSearchResetInterval = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value that determines whether the user can search for an item by typing characters when RadListControl is focused.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool KeyboardSearchEnabled
    {
      get
      {
        return this.ListElement.KeyboardSearchEnabled;
      }
      set
      {
        this.ListElement.KeyboardSearchEnabled = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadListElement ListElement
    {
      get
      {
        return this.element;
      }
      set
      {
        this.UnwireEvents();
        this.element = value;
        this.WireEvents();
        this.OnNotifyPropertyChanged(nameof (ListElement));
      }
    }

    [Editor("Telerik.WinControls.UI.Design.RadListControlCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [Description("Gets a collection representing the items contained in this RadListControl.")]
    [Browsable(true)]
    public RadListDataItemCollection Items
    {
      get
      {
        return (RadListDataItemCollection) this.element.Items;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IReadOnlyCollection<RadListDataItem> SelectedItems
    {
      get
      {
        return this.ListElement.SelectedItems;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the SelectionMode of RadListControl. This property has a similar effect to the SelectionMode of the standard Microsoft ListBox control.")]
    [DefaultValue(SelectionMode.One)]
    [Category("Behavior")]
    public SelectionMode SelectionMode
    {
      get
      {
        return this.ListElement.SelectionMode;
      }
      set
      {
        this.ListElement.SelectionMode = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    public object SelectedValue
    {
      get
      {
        return this.ListElement.SelectedValue;
      }
      set
      {
        this.ListElement.SelectedValue = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadListDataItem ActiveItem
    {
      get
      {
        return this.ListElement.ActiveItem;
      }
      set
      {
        this.ListElement.ActiveItem = value;
      }
    }

    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadListDataItem SelectedItem
    {
      get
      {
        return this.element.SelectedItem;
      }
      set
      {
        this.element.SelectedItem = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
      get
      {
        return this.element.SelectedIndex;
      }
      set
      {
        this.element.SelectedIndex = value;
      }
    }

    [DefaultValue(null)]
    [Browsable(true)]
    [AttributeProvider(typeof (IListSource))]
    [Description("Gets or sets the object that is responsible for providing data objects for the RadListElement. Setting this property throws an InvalidOperationException if Items is not empty and the data source is null.")]
    [Category("Data")]
    public object DataSource
    {
      get
      {
        return this.element.DataSource;
      }
      set
      {
        this.element.DataSource = value;
        this.OnDataBindingComplete((object) this, new ListBindingCompleteEventArgs(ListChangedType.Reset));
      }
    }

    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the name of the list or table in the data source for which the RadListControl is displaying data. ")]
    [Browsable(true)]
    [DefaultValue("")]
    public string DataMember
    {
      get
      {
        return this.element.DataMember;
      }
      set
      {
        this.element.DataMember = value;
      }
    }

    [Category("Data")]
    [DefaultValue("")]
    [Browsable(true)]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets a string which will be used to get a text string for each visual item. This property can not be set to null. Setting it to null will cause it to contain an empty string.")]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public string DisplayMember
    {
      get
      {
        return this.element.DisplayMember;
      }
      set
      {
        this.element.DisplayMember = value;
      }
    }

    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets the string through which the SelectedValue property will be determined. This property can not be set to null. Setting it to null will cause it to contain an empty string.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Browsable(true)]
    public string ValueMember
    {
      get
      {
        return this.element.ValueMember;
      }
      set
      {
        this.element.ValueMember = value;
      }
    }

    [Category("Data")]
    [Browsable(true)]
    [DefaultValue("")]
    [Description("Gets or sets the string through which the SelectedValue property will be determined. This property can not be set to null. Setting it to null will cause it to contain an empty string.")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string DescriptionTextMember
    {
      get
      {
        return this.element.DescriptionTextMember;
      }
      set
      {
        this.element.DescriptionTextMember = value;
      }
    }

    [DefaultValue(SortStyle.None)]
    [Category("Behavior")]
    [Description("Gets or sets the sort style.")]
    [Browsable(true)]
    public SortStyle SortStyle
    {
      get
      {
        return this.element.SortStyle;
      }
      set
      {
        this.element.SortStyle = value;
      }
    }

    [Description("Gets or set the scroll mode.")]
    [DefaultValue(ItemScrollerScrollModes.Smooth)]
    [Browsable(true)]
    [Category("Behavior")]
    public ItemScrollerScrollModes ScrollMode
    {
      get
      {
        return this.ListElement.ScrollMode;
      }
      set
      {
        this.ListElement.ScrollMode = value;
      }
    }

    [Browsable(true)]
    [DefaultValue("")]
    [Description("Gets or sets a format string which will be used for visual formatting of the items text.")]
    [Category("Behavior")]
    public string FormatString
    {
      get
      {
        return this.ListElement.FormatString;
      }
      set
      {
        this.ListElement.FormatString = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value that indicates whether the FormatString and FormatInfo properties will be used to format the items text. Setting this property to false may improve performance.")]
    [Browsable(true)]
    public bool FormattingEnabled
    {
      get
      {
        return this.ListElement.FormattingEnabled;
      }
      set
      {
        this.ListElement.FormattingEnabled = value;
      }
    }

    [Description("Gets or sets a value that indicates whether items will be sized according to their content. If this property is true the user can set the Height property of each individual RadListDataItem in the Items collection in order to override the automatic sizing.")]
    [Category("Layout")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool AutoSizeItems
    {
      get
      {
        return this.ListElement.AutoSizeItems;
      }
      set
      {
        this.ListElement.AutoSizeItems = value;
        this.RootElement.InvalidateMeasure(true);
        this.RootElement.InvalidateArrange(true);
        this.RootElement.UpdateLayout();
        this.ListElement.ViewElement.UpdateItems();
        this.ListElement.ViewElement.InvalidateMeasure();
        this.ListElement.ViewElement.InvalidateArrange();
        this.ListElement.Scroller.UpdateScrollRange();
        this.ListElement.HScrollBar.Maximum = this.ListElement.Scroller.MaxItemWidth;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Predicate<RadListDataItem> Filter
    {
      get
      {
        return this.ListElement.Filter;
      }
      set
      {
        this.ListElement.Filter = value;
      }
    }

    [Description("Gets or sets a filter expression which determines which items will be visible.")]
    [DefaultValue("")]
    [Category("Data")]
    [Browsable(true)]
    public string FilterExpression
    {
      get
      {
        return this.ListElement.FilterExpression;
      }
      set
      {
        this.ListElement.FilterExpression = value;
      }
    }

    [Browsable(false)]
    public bool IsFilterActive
    {
      get
      {
        return this.ListElement.IsFilterActive;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IFindStringComparer FindStringComparer
    {
      get
      {
        return this.ListElement.FindStringComparer;
      }
      set
      {
        this.ListElement.FindStringComparer = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets a value that determines whether the FindString() method searches via the text property set by the user or by the text provided by the data binding logic, that is, by DisplayMember.")]
    [Category("Behavior")]
    [DefaultValue(ItemTextComparisonMode.UserText)]
    public ItemTextComparisonMode ItemTextComparisonMode
    {
      get
      {
        return this.ListElement.ItemTextComparisonMode;
      }
      set
      {
        this.ListElement.ItemTextComparisonMode = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool SuspendItemsChangeEvents
    {
      get
      {
        return this.ListElement.SuspendItemsChangeEvents;
      }
      set
      {
        this.ListElement.SuspendItemsChangeEvents = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool SuspendSelectionEvents
    {
      get
      {
        return this.ListElement.SuspendSelectionEvents;
      }
      set
      {
        this.ListElement.SuspendSelectionEvents = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
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

    public RadListDataItem FindItemExact(string text, bool caseSensitive)
    {
      return this.ListElement.FindItemExact(text, caseSensitive);
    }

    public void Rebind()
    {
      this.element.Rebind();
    }

    public void BeginUpdate()
    {
      this.ListElement.BeginUpdate();
    }

    public void EndUpdate()
    {
      this.ListElement.EndUpdate();
    }

    public virtual IDisposable DeferRefresh()
    {
      return this.ListElement.DeferRefresh();
    }

    public void SelectAll()
    {
      this.ListElement.SelectAll();
    }

    public void SelectRange(int startIndex, int endIndex)
    {
      this.ListElement.SelectRange(startIndex, endIndex);
    }

    public void ScrollToItem(RadListDataItem item)
    {
      this.ListElement.ScrollToItem(item);
    }

    public int FindString(string s)
    {
      return this.ListElement.FindString(s);
    }

    public int FindString(string s, int startIndex)
    {
      return this.ListElement.FindString(s, startIndex);
    }

    public int FindStringExact(string s)
    {
      return this.ListElement.FindStringExact(s);
    }

    public int FindStringExact(string s, int startIndex)
    {
      return this.ListElement.FindStringExact(s, startIndex);
    }

    public int FindStringNonWrapping(string s)
    {
      return this.ListElement.FindStringNonWrapping(s);
    }

    public int FindStringNonWrapping(string s, int startIndex)
    {
      return this.ListElement.FindStringNonWrapping(s, startIndex);
    }

    [Category("Data")]
    [Browsable(true)]
    [Description("Fires after data binding operation has finished.")]
    public event ListBindingCompleteEventHandler DataBindingComplete;

    protected virtual void OnDataBindingComplete(object sender, ListBindingCompleteEventArgs e)
    {
      if (this.DataBindingComplete == null)
        return;
      this.DataBindingComplete((object) this, e);
    }

    public event Telerik.WinControls.UI.Data.PositionChangedEventHandler SelectedIndexChanged
    {
      add
      {
        this.Events.AddHandler(RadListControl.SelectedIndexChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadListControl.SelectedIndexChangedEventKey, (Delegate) value);
      }
    }

    public event Telerik.WinControls.UI.Data.PositionChangingEventHandler SelectedIndexChanging
    {
      add
      {
        this.Events.AddHandler(RadListControl.SelectedIndexChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadListControl.SelectedIndexChangingEventKey, (Delegate) value);
      }
    }

    public event EventHandler SelectedValueChanged
    {
      add
      {
        this.Events.AddHandler(RadListControl.SelectedValueChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadListControl.SelectedValueChangedEventKey, (Delegate) value);
      }
    }

    public event ListItemDataBindingEventHandler ItemDataBinding
    {
      add
      {
        this.Events.AddHandler(RadListControl.ListItemDataBindingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadListControl.ListItemDataBindingEventKey, (Delegate) value);
      }
    }

    public event ListItemDataBoundEventHandler ItemDataBound
    {
      add
      {
        this.Events.AddHandler(RadListControl.ListItemDataBoundEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadListControl.ListItemDataBoundEventKey, (Delegate) value);
      }
    }

    public event CreatingVisualListItemEventHandler CreatingVisualListItem
    {
      add
      {
        this.Events.AddHandler(RadListControl.CreatingVisualListItemEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadListControl.CreatingVisualListItemEventKey, (Delegate) value);
      }
    }

    public event SortStyleChangedEventHandler SortStyleChanged
    {
      add
      {
        this.Events.AddHandler(RadListControl.SortStyleChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadListControl.SortStyleChangedEventKey, (Delegate) value);
      }
    }

    public event VisualListItemFormattingEventHandler VisualItemFormatting
    {
      add
      {
        this.Events.AddHandler(RadListControl.VisualItemFormattingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadListControl.VisualItemFormattingEventKey, (Delegate) value);
      }
    }

    public event NotifyCollectionChangedEventHandler SelectedItemsChanged
    {
      add
      {
        this.ListElement.SelectedItemsChanged += value;
      }
      remove
      {
        this.ListElement.SelectedItemsChanged -= value;
      }
    }

    public event NotifyCollectionChangingEventHandler SelectedItemsChanging
    {
      add
      {
        this.ListElement.SelectedItemsChanging += value;
      }
      remove
      {
        this.ListElement.SelectedItemsChanging -= value;
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

    private void element_SelectedIndexChanging(object sender, PositionChangingCancelEventArgs e)
    {
      e.Cancel = this.OnSelectedIndexChanging(sender, e.Position);
    }

    private void element_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.OnSelectedIndexChanged(sender, e.Position);
    }

    private void element_SelectedValueChanged(object sender, EventArgs e)
    {
      Telerik.WinControls.UI.Data.ValueChangedEventArgs changedEventArgs = (Telerik.WinControls.UI.Data.ValueChangedEventArgs) e;
      this.OnSelectedValueChanged(sender, changedEventArgs.Position, changedEventArgs.NewValue, changedEventArgs.OldValue);
    }

    private void element_ItemDataBound(object sender, ListItemDataBoundEventArgs args)
    {
      this.OnItemDataBound(sender, args);
    }

    private void element_ItemDataBinding(object sender, ListItemDataBindingEventArgs args)
    {
      this.OnItemDataBinding(sender, args);
    }

    private void element_CreatingVisualItem(object sender, CreatingVisualListItemEventArgs args)
    {
      this.OnCreatingVisualItem(sender, args);
    }

    private void element_SortStyleChanged(object sender, SortStyleChangedEventArgs args)
    {
      this.OnSortStyleChanged(sender, args);
    }

    private void element_VisualItemFormatting(object sender, VisualItemFormattingEventArgs args)
    {
      this.OnVisualItemFormatting(sender, args);
    }

    private void element_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnNotifyPropertyChanged(e.PropertyName);
    }

    protected virtual void OnSelectedIndexChanged(object sender, int newIndex)
    {
      Telerik.WinControls.UI.Data.PositionChangedEventHandler changedEventHandler = (Telerik.WinControls.UI.Data.PositionChangedEventHandler) this.Events[RadListControl.SelectedIndexChangedEventKey];
      if (changedEventHandler != null)
        changedEventHandler(sender, new Telerik.WinControls.UI.Data.PositionChangedEventArgs(newIndex));
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "SelectionChanged", (object) this.SelectedIndex);
    }

    protected virtual bool OnSelectedIndexChanging(object sender, int newIndex)
    {
      Telerik.WinControls.UI.Data.PositionChangingEventHandler changingEventHandler = (Telerik.WinControls.UI.Data.PositionChangingEventHandler) this.Events[RadListControl.SelectedIndexChangingEventKey];
      if (changingEventHandler == null)
        return false;
      PositionChangingCancelEventArgs e = new PositionChangingCancelEventArgs(newIndex);
      changingEventHandler((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnSelectedValueChanged(
      object sender,
      int newIndex,
      object newValue,
      object oldValue)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadListControl.SelectedValueChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, (EventArgs) new Telerik.WinControls.UI.Data.ValueChangedEventArgs(newIndex, newValue, oldValue));
    }

    protected virtual void OnItemDataBinding(object sender, ListItemDataBindingEventArgs args)
    {
      ListItemDataBindingEventHandler bindingEventHandler = (ListItemDataBindingEventHandler) this.Events[RadListControl.ListItemDataBindingEventKey];
      if (bindingEventHandler == null)
        return;
      bindingEventHandler((object) this, args);
    }

    protected virtual void OnItemDataBound(object sender, ListItemDataBoundEventArgs args)
    {
      ListItemDataBoundEventHandler boundEventHandler = (ListItemDataBoundEventHandler) this.Events[RadListControl.ListItemDataBoundEventKey];
      if (boundEventHandler == null)
        return;
      boundEventHandler((object) this, args);
    }

    protected virtual void OnCreatingVisualItem(object sender, CreatingVisualListItemEventArgs args)
    {
      CreatingVisualListItemEventHandler itemEventHandler = (CreatingVisualListItemEventHandler) this.Events[RadListControl.CreatingVisualListItemEventKey];
      if (itemEventHandler == null)
        return;
      itemEventHandler((object) this, args);
    }

    protected virtual void OnSortStyleChanged(object sender, SortStyleChangedEventArgs args)
    {
      SortStyleChangedEventHandler changedEventHandler = (SortStyleChangedEventHandler) this.Events[RadListControl.SortStyleChangedEventKey];
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, args);
    }

    protected virtual void OnVisualItemFormatting(object sender, VisualItemFormattingEventArgs args)
    {
      VisualListItemFormattingEventHandler formattingEventHandler = (VisualListItemFormattingEventHandler) this.Events[RadListControl.VisualItemFormattingEventKey];
      if (formattingEventHandler == null)
        return;
      formattingEventHandler((object) this, args);
    }

    public override void EndInit()
    {
      base.EndInit();
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (this.Items[index].Selected)
          this.ListElement.OnSelectedItemAdded(this.Items[index]);
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(120, 95));
      }
    }

    protected override bool IsInputChar(char charCode)
    {
      if (!base.IsInputChar(charCode))
        return char.IsLetterOrDigit(charCode);
      return true;
    }

    protected override bool IsInputKey(Keys keyData)
    {
      switch (keyData & Keys.KeyCode)
      {
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
          return true;
        default:
          return base.IsInputKey(keyData);
      }
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.ListElement.Focus();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.UnwireEvents();
        this.element = (RadListElement) null;
      }
      base.Dispose(disposing);
    }

    protected override bool CanEditElementAtDesignTime(RadElement element)
    {
      if (element is RadListVisualItem)
        return false;
      return base.CanEditElementAtDesignTime(element);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.ListElement.OnMouseWheel(e.Delta);
      if (!(e is HandledMouseEventArgs))
        return;
      (e as HandledMouseEventArgs).Handled = true;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      RadElement elementAtPoint = this.RootElement.ElementTree.GetElementAtPoint((RadElement) this.RootElement, e.Location, (List<RadElement>) null);
      if (elementAtPoint != null)
      {
        RadItem radItem = elementAtPoint as RadItem;
      }
      if (this.ListElement.OnControlMouseDown(e))
        return;
      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.ListElement == null || this.ListElement.OnControlMouseMove(e))
        return;
      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.ListElement == null || this.ListElement.OnControlMouseUp(e))
        return;
      base.OnMouseUp(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      switch (e.KeyCode)
      {
        case Keys.Prior:
          this.HandlePageUpKey();
          break;
        case Keys.Next:
          this.HandlePageDownKey();
          break;
        case Keys.End:
          this.HandleEndKey();
          break;
        case Keys.Home:
          this.HandleHomeKey();
          break;
      }
    }

    protected virtual void HandlePageDownKey()
    {
      this.ScrollByPage(1);
    }

    protected virtual void HandlePageUpKey()
    {
      this.ScrollByPage(-1);
    }

    protected virtual void HandleHomeKey()
    {
      this.ListElement.HomeEndSelect(this.Items.First);
    }

    protected virtual void HandleEndKey()
    {
      this.ListElement.HomeEndSelect(this.Items.Last);
    }

    public void ScrollByPage(int pageCount)
    {
      this.ListElement.ScrollByPage(pageCount);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadListControlAccessibleObject(this);
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request.Type == IPCMessage.MessageTypes.GetPropertyValue)
      {
        if (request.Message == "ItemsCount")
        {
          request.Data = (object) this.Items.Count;
          return;
        }
        if (request.Message == "SelectedIndex")
        {
          request.Data = (object) this.SelectedIndex;
          return;
        }
        if (request.Message == "IsMultipleSelection")
        {
          request.Data = (object) (bool) (this.SelectionMode == SelectionMode.MultiSimple ? 1 : (this.SelectionMode == SelectionMode.MultiExtended ? 1 : 0));
          return;
        }
        if (request.Message == "DataSource")
        {
          if (this.DataSource != null)
          {
            request.Data = (object) this.DataSource.ToString();
            return;
          }
          request.Data = (object) "(none)";
          return;
        }
      }
      base.ProcessCodedUIMessage(ref request);
    }

    protected virtual void ClearMeasuredSize()
    {
      foreach (RadListDataItem radListDataItem in (IEnumerable<RadListDataItem>) this.ListElement.Items)
        radListDataItem.MeasuredSize = (SizeF) Size.Empty;
    }
  }
}
