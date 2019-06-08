// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPropertyGrid
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(true)]
  [DefaultProperty("SelectedObject")]
  [Description("Displays the properties of an object in a grid with two columns with a property name in the first column and value in the second.")]
  [DefaultEvent("SelectedItemChanged")]
  [TelerikToolboxCategory("Editors")]
  [Designer("Telerik.WinControls.UI.Design.RadPropertyGridDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  public class RadPropertyGrid : RadControl
  {
    private PropertyGridElement propertyGridElement;

    protected override void OnLoad(Size desiredSize)
    {
      base.OnLoad(desiredSize);
      this.EnableGesture(GestureType.Pan);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.propertyGridElement = this.CreatePropertyGridElement();
      parent.Children.Add((RadElement) this.propertyGridElement);
    }

    protected virtual PropertyGridElement CreatePropertyGridElement()
    {
      return new PropertyGridElement();
    }

    protected override void Dispose(bool disposing)
    {
      if (this.RadContextMenu != null && (object) this.RadContextMenu.GetType() == (object) typeof (PropertyGridDefaultContextMenu))
      {
        this.RadContextMenu.Dispose();
        this.RadContextMenu = (RadContextMenu) null;
      }
      base.Dispose(disposing);
    }

    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled.")]
    public bool EnableKineticScrolling
    {
      get
      {
        return this.PropertyGridElement.SplitElement.PropertyTableElement.EnableKineticScrolling;
      }
      set
      {
        this.PropertyGridElement.SplitElement.PropertyTableElement.EnableKineticScrolling = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the data can be grouped programmatically.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool EnableCustomGrouping
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.EnableCustomGrouping;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.EnableCustomGrouping = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets a value indicating whether there are currently open editors.")]
    [Browsable(false)]
    public bool IsEditing
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.IsEditing;
      }
    }

    [Description("Gets or sets a value indicating whether the user is allowed to edit the values of the properties.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Browsable(true)]
    public bool ReadOnly
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.ReadOnly;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.ReadOnly = value;
      }
    }

    [Browsable(false)]
    [Description("Gets the active editor.")]
    [Category("Behavior")]
    public IValueEditor ActiveEditor
    {
      get
      {
        return this.propertyGridElement.PropertyTableElement.ActiveEditor;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets a value indicating how user begins editing a cell.")]
    [DefaultValue(RadPropertyGridBeginEditModes.BeginEditOnClick)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public RadPropertyGridBeginEditModes BeginEditMode
    {
      get
      {
        return this.propertyGridElement.PropertyTableElement.BeginEditMode;
      }
      set
      {
        if (this.propertyGridElement.PropertyTableElement.BeginEditMode == value)
          return;
        this.propertyGridElement.PropertyTableElement.BeginEditMode = value;
        this.OnNotifyPropertyChanged(nameof (BeginEditMode));
      }
    }

    [Description("Gets or sets a value indicating whether the groups will be expanded or collapsed upon creation.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool AutoExpandGroups
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.AutoExpandGroups;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.AutoExpandGroups = value;
      }
    }

    [Description("Gets or sets the shortcut menu associated to the RadPropertyGrid.")]
    [Category("Behavior")]
    [DefaultValue(null)]
    public virtual RadContextMenu RadContextMenu
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.ContextMenu;
      }
      set
      {
        PropertyGridTableElement propertyTableElement = this.PropertyGridElement.PropertyTableElement;
        if (propertyTableElement.ContextMenu == value)
          return;
        if (propertyTableElement.ContextMenu is PropertyGridDefaultContextMenu)
          propertyTableElement.ContextMenu.Dispose();
        propertyTableElement.ContextMenu = value;
        if (propertyTableElement.ContextMenu == null)
          return;
        propertyTableElement.ContextMenu.ThemeName = this.ThemeName;
      }
    }

    [Description("Gets or sets a value indicating whether the default context menu is enabled.")]
    [DefaultValue(true)]
    public bool AllowDefaultContextMenu
    {
      get
      {
        return this.RadContextMenu is PropertyGridDefaultContextMenu;
      }
      set
      {
        if (value && this.RadContextMenu is PropertyGridDefaultContextMenu)
          return;
        if (!value && this.RadContextMenu is PropertyGridDefaultContextMenu)
        {
          this.RadContextMenu = (RadContextMenu) null;
        }
        else
        {
          this.RadContextMenu = (RadContextMenu) new PropertyGridDefaultContextMenu(this.PropertyGridElement.PropertyTableElement);
          this.OnNotifyPropertyChanged(nameof (AllowDefaultContextMenu));
        }
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets a value that determines whether the user can navigate to an item by typing when RadPropertyGrid is focused.")]
    public bool KeyboardSearchEnabled
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.KeyboardSearchEnabled;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.KeyboardSearchEnabled = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a value that specifies how long the user must wait before searching with the keyboard is reset.")]
    [DefaultValue(300)]
    [Category("Behavior")]
    public int KeyboardSearchResetInterval
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.KeyboardSearchResetInterval;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.KeyboardSearchResetInterval = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the string comparer used by the keyboard navigation functionality.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IFindStringComparer FindStringComparer
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.FindStringComparer;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.FindStringComparer = value;
      }
    }

    [DefaultValue(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Category("Layout")]
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

    [Description("Gets or sets the PropertyGridViewElement selected item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Data")]
    [DefaultValue(null)]
    [Browsable(false)]
    public PropertyGridItemBase SelectedGridItem
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.SelectedGridItem;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.SelectedGridItem = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the object which properties the RadPropertyGrid is displaying.")]
    [Category("Data")]
    [DefaultValue(null)]
    [TypeConverter(typeof (SelectedObjectConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public object SelectedObject
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.SelectedObject;
      }
      set
      {
        if (value is string && (string) value == "")
          value = (object) null;
        this.PropertyGridElement.PropertyTableElement.SelectedObject = value;
        this.PropertyGridElement.SplitElement.ClearHelpBarText();
      }
    }

    [Description("Gets or sets the objects which properties the RadPropertyGrid is displaying.")]
    [TypeConverter(typeof (SelectedObjectConverter))]
    [Category("Data")]
    [DefaultValue(null)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(false)]
    public object[] SelectedObjects
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.SelectedObjects;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.SelectedObjects = value;
        this.PropertyGridElement.SplitElement.ClearHelpBarText();
      }
    }

    [Browsable(false)]
    [Description("Gets the Items collection.")]
    [Category("Data")]
    public PropertyGridItemCollection Items
    {
      get
      {
        return new PropertyGridItemCollection((IList<PropertyGridItem>) this.PropertyGridElement.PropertyTableElement.PropertyItems);
      }
    }

    [Category("Data")]
    [Browsable(false)]
    [Description("Gets the Groups collection.")]
    public PropertyGridGroupItemCollection Groups
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.Groups;
      }
    }

    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether grouping is enabled.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    public bool EnableGrouping
    {
      get
      {
        return this.PropertyGridElement.EnableGrouping;
      }
      set
      {
        this.PropertyGridElement.EnableGrouping = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether sorting is enabled.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool EnableSorting
    {
      get
      {
        return this.PropertyGridElement.EnableSorting;
      }
      set
      {
        this.PropertyGridElement.EnableSorting = value;
      }
    }

    [Description("Gets or sets a value indicating whether filtering is enabled.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool EnableFiltering
    {
      get
      {
        return this.PropertyGridElement.EnableFiltering;
      }
      set
      {
        this.PropertyGridElement.EnableFiltering = value;
      }
    }

    [Category("Data")]
    [Description("Gets the group descriptors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue(null)]
    [Browsable(false)]
    public GroupDescriptorCollection GroupDescriptors
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.GroupDescriptors;
      }
    }

    [Description("Gets the filter descriptors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Data")]
    [DefaultValue(null)]
    [Browsable(false)]
    public FilterDescriptorCollection FilterDescriptors
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.FilterDescriptors;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets the sort descriptors.")]
    [Category("Data")]
    [DefaultValue(null)]
    [Browsable(false)]
    public SortDescriptorCollection SortDescriptors
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.SortDescriptors;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [DefaultValue(SortOrder.None)]
    [Browsable(true)]
    [Description("Gets or sets the sort order of items.")]
    public SortOrder SortOrder
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.SortOrder;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.SortOrder = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the mode in which the properties will be displayed in the RadPropertyGrid.")]
    [Browsable(true)]
    [DefaultValue(PropertySort.NoSort)]
    public PropertySort PropertySort
    {
      get
      {
        return this.propertyGridElement.PropertyTableElement.PropertySort;
      }
      set
      {
        this.propertyGridElement.PropertyTableElement.PropertySort = value;
      }
    }

    [Description("Gets or sets a value indicating whether the PropertyGridHelpElement is visible.")]
    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(true)]
    public bool HelpVisible
    {
      get
      {
        return this.propertyGridElement.SplitElement.HelpVisible;
      }
      set
      {
        this.propertyGridElement.SplitElement.HelpVisible = value;
      }
    }

    [Description("Gets or sets the height of the PropertyGridHelpElement.")]
    [DefaultValue(80f)]
    [Category("Appearance")]
    [Browsable(true)]
    public float HelpBarHeight
    {
      get
      {
        return this.PropertyGridElement.SplitElement.HelpElementHeight;
      }
      set
      {
        this.PropertyGridElement.SplitElement.HelpElementHeight = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the search box of the RadPropertyGrid should be visible")]
    [Category("Appearance")]
    [Browsable(true)]
    public bool ToolbarVisible
    {
      get
      {
        return this.propertyGridElement.ToolbarVisible;
      }
      set
      {
        this.propertyGridElement.ToolbarVisible = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets the RadPropertyGridElement of this control.")]
    [Browsable(false)]
    public PropertyGridElement PropertyGridElement
    {
      get
      {
        return this.propertyGridElement;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(24)]
    [Description("Gets or sets a value indicating the height of the RadPropertyGrid items.")]
    public int ItemHeight
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.ItemHeight;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.ItemHeight = value;
      }
    }

    [Description("Gets or sets the distance between items of the RadPropertyGridElement.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Appearance")]
    [DefaultValue(-1)]
    [Browsable(true)]
    public int ItemSpacing
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.ItemSpacing;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.ItemSpacing = value;
      }
    }

    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the width of the indentation of subitems.")]
    [DefaultValue(20)]
    [Browsable(true)]
    public int ItemIndent
    {
      get
      {
        return this.PropertyGridElement.PropertyTableElement.ItemIndent;
      }
      set
      {
        this.PropertyGridElement.PropertyTableElement.ItemIndent = value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(200, 300));
      }
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    [Description("Occurs before the selected object is changed.")]
    [Category("Behavior")]
    public event PropertyGridSelectedObjectChangingEventHandler SelectedObjectChanging
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.SelectedObjectChanging += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.SelectedObjectChanging -= value;
      }
    }

    [Description("Occurs after the selected object is changed.")]
    [Category("Behavior")]
    public event PropertyGridSelectedObjectChangedEventHandler SelectedObjectChanged
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.SelectedObjectChanged += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.SelectedObjectChanged -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs before a property grid item is selected.")]
    public event RadPropertyGridCancelEventHandler SelectedGridItemChanging
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.SelectedGridItemChanging += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.SelectedGridItemChanging -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs after a property grid item is selected.")]
    public event RadPropertyGridEventHandler SelectedGridItemChanged
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.SelectedGridItemChanged += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.SelectedGridItemChanged -= value;
      }
    }

    [Description("Occurs when opening the context menu.")]
    [Category("Action")]
    public event PropertyGridContextMenuOpeningEventHandler ContextMenuOpening
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ContextMenuOpening += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ContextMenuOpening -= value;
      }
    }

    [Description("Fires for custom grouping operation.")]
    [Browsable(true)]
    [Category("Data")]
    public event PropertyGridCustomGroupingEventHandler CustomGrouping
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.CustomGrouping += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.CustomGrouping -= value;
      }
    }

    [Description("Occurs when the user presses a mouse button over a property grid item.")]
    [Category("Behavior")]
    public event PropertyGridMouseEventHandler ItemMouseDown
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ItemMouseDown += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ItemMouseDown -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when the user moves the mouse in the area of a property grid item.")]
    public event PropertyGridMouseEventHandler ItemMouseMove
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ItemMouseMove += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ItemMouseMove -= value;
      }
    }

    [Description("Occurs when a mouse button is clicked inside a PropertyGridItemElementBase")]
    [Category("Behavior")]
    public event RadPropertyGridEventHandler ItemMouseClick
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ItemMouseClick += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ItemMouseClick -= value;
      }
    }

    [Category("Behavior")]
    [Description("Occurs when a mouse button is double clicked inside a PropertyGridItemElementBase")]
    public event RadPropertyGridEventHandler ItemMouseDoubleClick
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ItemMouseDoubleClick += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ItemMouseDoubleClick -= value;
      }
    }

    [Description("Occurs before the value of the Expanded property of a property grid item is changed.")]
    [Category("Action")]
    public event RadPropertyGridCancelEventHandler ItemExpandedChanging
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ItemExpandedChanging += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ItemExpandedChanging -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs after the value of the Expanded property of a property grid item is changed.")]
    public event RadPropertyGridEventHandler ItemExpandedChanged
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ItemExpandedChanged += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ItemExpandedChanged -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when the item changes its state and needs to be formatted.")]
    public event PropertyGridItemFormattingEventHandler ItemFormatting
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ItemFormatting += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ItemFormatting -= value;
      }
    }

    [Description("Occurs when a new item is going to be created.")]
    [Category("Action")]
    public event CreatePropertyGridItemEventHandler CreateItem
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.CreateItem += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.CreateItem -= value;
      }
    }

    [Description("Occurs when a new item element is going to be created.")]
    [Category("Action")]
    public event CreatePropertyGridItemElementEventHandler CreateItemElement
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.CreateItemElement += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.CreateItemElement -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when editor is required.")]
    public event PropertyGridEditorRequiredEventHandler EditorRequired
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.EditorRequired += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.EditorRequired -= value;
      }
    }

    [Description("Occurs when editing is started.")]
    [Category("Action")]
    public event PropertyGridItemEditingEventHandler Editing
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.Editing += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.Editing -= value;
      }
    }

    [Description("Occurs when editor is initialized.")]
    [Category("Action")]
    public event PropertyGridItemEditorInitializedEventHandler EditorInitialized
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.EditorInitialized += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.EditorInitialized -= value;
      }
    }

    [Category("Action")]
    [Description("Occurs when editing has been finished.")]
    public event PropertyGridItemEditedEventHandler Edited
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.Edited += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.Edited -= value;
      }
    }

    [Description("Occurs when item's value is changing.")]
    [Category("Action")]
    public event PropertyGridItemValueChangingEventHandler PropertyValueChanging
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.PropertyValueChanging += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.PropertyValueChanging -= value;
      }
    }

    [Description("Occurs when a property value changes.")]
    [Category("Action")]
    public event PropertyGridItemValueChangedEventHandler PropertyValueChanged
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.PropertyValueChanged += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.PropertyValueChanged -= value;
      }
    }

    [Description("Fires when a property value is validating.")]
    [Category("Action")]
    public event PropertyValidatingEventHandler PropertyValidating
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.PropertyValidating += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.PropertyValidating -= value;
      }
    }

    [Category("Action")]
    [Description("Fires when a property has finished validating.")]
    public event PropertyValidatedEventHandler PropertyValidated
    {
      add
      {
        this.propertyGridElement.PropertyTableElement.PropertyValidated += value;
      }
      remove
      {
        this.propertyGridElement.PropertyTableElement.PropertyValidated -= value;
      }
    }

    [Description("Fires before the value in an editor is changing.")]
    [Browsable(true)]
    [Category("Action")]
    public event ValueChangingEventHandler ValueChanging
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ValueChanging += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ValueChanging -= value;
      }
    }

    [Browsable(true)]
    [Description("Fires when the value of a editor changes.")]
    [Category("Action")]
    public event EventHandler ValueChanged
    {
      add
      {
        this.PropertyGridElement.PropertyTableElement.ValueChanged += value;
      }
      remove
      {
        this.PropertyGridElement.PropertyTableElement.ValueChanged -= value;
      }
    }

    public void BestFit()
    {
      this.PropertyGridElement.BestFit();
    }

    public void BestFit(PropertyGridBestFitMode mode)
    {
      this.PropertyGridElement.BestFit(mode);
    }

    public void ExpandAllGridItems()
    {
      this.propertyGridElement.ExpandAllGridItems();
    }

    public void CollapseAllGridItems()
    {
      this.propertyGridElement.CollapseAllGridItems();
    }

    public void ResetSelectedProperty()
    {
      this.propertyGridElement.ResetSelectedProperty();
    }

    public void BeginEdit()
    {
      this.propertyGridElement.PropertyTableElement.BeginEdit();
    }

    public bool EndEdit()
    {
      return this.propertyGridElement.PropertyTableElement.EndEdit();
    }

    public void CancelEdit()
    {
      this.propertyGridElement.PropertyTableElement.CancelEdit();
    }

    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (!this.IsEditing || keyData != Keys.Return && keyData != Keys.Escape)
        return base.ProcessDialogKey(keyData);
      switch (keyData)
      {
        case Keys.Return:
          this.EndEdit();
          break;
        case Keys.Escape:
          this.CancelEdit();
          break;
      }
      return true;
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
      if (this.PropertyGridElement.PropertyTableElement.ProcessContextMenu(point))
        return;
      ContextMenu contextMenu = this.ContextMenu;
      ContextMenuStrip contextMenuStrip = contextMenu != null ? (ContextMenuStrip) null : this.ContextMenuStrip;
      if ((contextMenu != null || contextMenuStrip != null) && this.ClientRectangle.Contains(point))
      {
        if (contextMenu != null)
          contextMenu.Show((Control) this, point);
        else
          contextMenuStrip?.Show((Control) this, point);
      }
      else
        this.DefWndProc(ref m);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProcessMouseWheel(e))
        return;
      base.OnMouseWheel(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProcessMouseDown(e))
        return;
      base.OnMouseDown(e);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProcessMouseClick(e))
        return;
      base.OnMouseClick(e);
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProcessMouseDoubleClick(e))
        return;
      base.OnMouseDoubleClick(e);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProcessKeyDown(e))
        return;
      base.OnKeyDown(e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProcessKeyPress(e))
        return;
      base.OnKeyPress(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProcessMouseMove(e))
        return;
      base.OnMouseMove(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProecessMouseEnter(e))
        return;
      base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProecessMouseLeave(e))
        return;
      base.OnMouseLeave(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.PropertyGridElement.PropertyTableElement.ProcessMouseUp(e))
        return;
      base.OnMouseUp(e);
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

    protected override void OnValidating(CancelEventArgs e)
    {
      base.OnValidating(e);
      if (!this.IsEditing)
        return;
      e.Cancel |= !this.EndEdit();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadPropertyGridAccessibilityInstance(this);
    }
  }
}
