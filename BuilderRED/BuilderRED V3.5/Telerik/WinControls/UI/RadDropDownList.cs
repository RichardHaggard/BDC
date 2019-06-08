// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownList
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
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
  [Designer("Telerik.WinControls.UI.Design.RadListControlDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Data Controls")]
  [ToolboxItem(true)]
  [ComplexBindingProperties("DataSource", "ValueMember")]
  [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
  [Description("Displays an a DropDownList of permitted values")]
  [DefaultBindingProperty("Text")]
  [DefaultEvent("SelectedIndexChanged")]
  [DefaultProperty("Items")]
  public class RadDropDownList : RadEditorControl
  {
    public static readonly object SelectedIndexChangedEventKey = new object();
    public static readonly object SelectedIndexChangingEventKey = new object();
    public static readonly object SelectedValueChangedEventKey = new object();
    public static readonly object ListItemDataBindingEventKey = new object();
    public static readonly object ListItemDataBoundEventKey = new object();
    public static readonly object CreatingVisualListItemEventKey = new object();
    public static readonly object PopupOpenedEventKey = new object();
    public static readonly object PopupOpeningEventKey = new object();
    public static readonly object PopupClosingEventKey = new object();
    public static readonly object PopupClosedEventKey = new object();
    public static readonly object SelectionRangeChangedKey = new object();
    public static readonly object SortStyleChangedKey = new object();
    public static readonly object VisualItemFormattingKey = new object();
    public static readonly object KeyDownEventKey = new object();
    public static readonly object KeyUpEventKey = new object();
    public static readonly object KeyPressEventKey = new object();
    private RadDropDownListElement dropDownListElement;
    internal bool entering;

    public RadDropDownList()
    {
      this.AutoSize = true;
      base.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, true);
      int num = (int) this.RootElement.BindProperty(RadItem.ShadowDepthProperty, (RadObject) this.DropDownListElement, RadItem.ShadowDepthProperty, PropertyBindingOptions.OneWay);
    }

    protected override void Dispose(bool disposing)
    {
      if (this.IsDisposed)
        return;
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.dropDownListElement = this.CreateDropDownListElement();
      this.RootElement.Children.Add((RadElement) this.dropDownListElement);
      this.WireEvents();
    }

    protected virtual RadDropDownListElement CreateDropDownListElement()
    {
      return new RadDropDownListElement();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether alternating item color is enabled.")]
    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Appearance")]
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

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(125, 20));
      }
    }

    [Browsable(true)]
    [Category("Accessibility")]
    [DefaultValue(false)]
    [Description("Indicates focus cues display, when available, based on the corresponding control type and the current UI state.")]
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

    [Category("Layout")]
    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new bool TabStop
    {
      get
      {
        if (this.DropDownStyle == RadDropDownStyle.DropDownList)
          return this.dropDownListElement.TextBox.TextBoxItem.TextBoxControl.TabStop;
        return base.TabStop;
      }
      set
      {
        if (this.DropDownStyle == RadDropDownStyle.DropDown)
        {
          base.TabStop = false;
          this.dropDownListElement.TextBox.TextBoxItem.TextBoxControl.TabStop = value;
        }
        else
          base.TabStop = value;
      }
    }

    [Description("Gets or sets a value indicating whether the kinetic scrolling function is enabled.")]
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
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

    [DefaultValue(true)]
    [Description("Gets or sets that RadListDataItem Image will be displayd in Editor Element when DropDownStyle is set to DropDownStyleList")]
    public virtual bool ShowImageInEditorArea
    {
      get
      {
        return this.DropDownListElement.ShowImageInEditorArea;
      }
      set
      {
        this.DropDownListElement.ShowImageInEditorArea = value;
        this.OnNotifyPropertyChanged(nameof (ShowImageInEditorArea));
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
        this.ListElement.FitItemsToSize = value;
      }
    }

    [Browsable(false)]
    public RadEditorPopupControlBase Popup
    {
      get
      {
        return this.DropDownListElement.Popup;
      }
    }

    [DefaultValue(0)]
    [Description("Gets or sets the maximum number of items to be shown in the drop-down portion of the RadDropDownList.")]
    [Category("Behavior")]
    public int MaxDropDownItems
    {
      get
      {
        return this.dropDownListElement.MaxDropDownItems;
      }
      set
      {
        this.dropDownListElement.MaxDropDownItems = value;
      }
    }

    [Category("Layout")]
    [Browsable(true)]
    [Description("Gets or sets a value that indicates whether items will be sized according to their content. If this property is true the user can set the Height property of each individual RadListDataItem in the Items collection in order to override the automatic sizing.")]
    [DefaultValue(false)]
    public bool AutoSizeItems
    {
      get
      {
        return this.dropDownListElement.AutoSizeItems;
      }
      set
      {
        this.dropDownListElement.AutoSizeItems = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(32767)]
    [Description("Gets or sets the maximum number of characters the user can type or paste into the text box control.")]
    [Localizable(true)]
    public virtual int MaxLength
    {
      get
      {
        return this.DropDownListElement.MaxLength;
      }
      set
      {
        this.DropDownListElement.MaxLength = value;
      }
    }

    [Description("Gets or sets the drop down minimal size.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Size), "0,0")]
    [Browsable(true)]
    public Size DropDownMinSize
    {
      get
      {
        return this.DropDownListElement.DropDownMinSize;
      }
      set
      {
        this.DropDownListElement.DropDownMinSize = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(SizingMode.None)]
    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, veritcal or a combination of them.")]
    public SizingMode DropDownSizingMode
    {
      get
      {
        return this.DropDownListElement.DropDownSizingMode;
      }
      set
      {
        this.DropDownListElement.DropDownSizingMode = value;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [RadDescription("CaseSensitive", typeof (RadDropDownListElement))]
    [Browsable(true)]
    public virtual bool CaseSensitive
    {
      get
      {
        return this.dropDownListElement.CaseSensitive;
      }
      set
      {
        this.dropDownListElement.CaseSensitive = value;
      }
    }

    [DefaultValue(AutoCompleteMode.None)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [RadDescription("AutoCompleteMode", typeof (RadDropDownListElement))]
    [Browsable(true)]
    [Category("Behavior")]
    public virtual AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.DropDownListElement.AutoCompleteMode;
      }
      set
      {
        this.DropDownListElement.AutoCompleteMode = value;
      }
    }

    [RadDescription("SelectNextOnDoubleClick", typeof (RadDropDownListElement))]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Browsable(true)]
    public bool SelectNextOnDoubleClick
    {
      get
      {
        return this.dropDownListElement.SelectNextOnDoubleClick;
      }
      set
      {
        this.dropDownListElement.SelectNextOnDoubleClick = value;
      }
    }

    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IFormatProvider FormatInfo
    {
      get
      {
        return this.dropDownListElement.FormatInfo;
      }
      set
      {
        this.dropDownListElement.FormatInfo = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue("")]
    public string FormatString
    {
      get
      {
        return this.dropDownListElement.FormatString;
      }
      set
      {
        this.dropDownListElement.FormatString = value;
      }
    }

    [DefaultValue(SortStyle.None)]
    [Category("Appearance")]
    public SortStyle SortStyle
    {
      get
      {
        return this.dropDownListElement.SortStyle;
      }
      set
      {
        this.dropDownListElement.SortStyle = value;
      }
    }

    [DefaultValue(true)]
    [RadDescription("FormattingEnabled", typeof (RadDropDownListElement))]
    public bool FormattingEnabled
    {
      get
      {
        return this.dropDownListElement.FormattingEnabled;
      }
      set
      {
        this.dropDownListElement.FormattingEnabled = value;
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(RadEasingType.InQuad)]
    public RadEasingType DropDownAnimationEasing
    {
      get
      {
        return this.dropDownListElement.DropDownAnimationEasing;
      }
      set
      {
        this.dropDownListElement.DropDownAnimationEasing = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(true)]
    public bool DropDownAnimationEnabled
    {
      get
      {
        return this.dropDownListElement.DropDownAnimationEnabled;
      }
      set
      {
        this.dropDownListElement.DropDownAnimationEnabled = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the number of frames that will be used when the DropDown is being animated.")]
    [DefaultValue(1)]
    public int DropDownAnimationFrames
    {
      get
      {
        return this.dropDownListElement.DropDownAnimationFrames;
      }
      set
      {
        this.dropDownListElement.DropDownAnimationFrames = value;
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Behavior")]
    [DefaultValue(106)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [RadDescription("DropDownHeight", typeof (RadDropDownListElement))]
    public int DropDownHeight
    {
      get
      {
        return this.dropDownListElement.DropDownHeight;
      }
      set
      {
        this.dropDownListElement.DropDownHeight = value;
      }
    }

    [Description("Gets or sets a value specifying the style of the RadDropDownList.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [RadPropertyDefaultValue("DropDownStyle", typeof (RadDropDownListElement))]
    [Category("Appearance")]
    public virtual RadDropDownStyle DropDownStyle
    {
      get
      {
        return this.dropDownListElement.DropDownStyle;
      }
      set
      {
        this.dropDownListElement.DropDownStyle = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(6)]
    public int DefaultItemsCountInDropDown
    {
      get
      {
        return this.DropDownListElement.DefaultItemsCountInDropDown;
      }
      set
      {
        this.DropDownListElement.DefaultItemsCountInDropDown = value;
      }
    }

    [Description("Gets or sets the drop down maximal size.")]
    [RadDefaultValue("DropDownMaxSize", typeof (RadDropDownListElement))]
    [Category("Appearance")]
    public Size DropDownMaxSize
    {
      get
      {
        return this.dropDownListElement.DropDownMaxSize;
      }
      set
      {
        this.dropDownListElement.DropDownMaxSize = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    public RadDropDownListElement DropDownListElement
    {
      get
      {
        return this.dropDownListElement;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadListElement ListElement
    {
      get
      {
        return this.DropDownListElement.ListElement;
      }
      set
      {
        this.UnwireEvents();
        this.DropDownListElement.ListElement = value;
        this.WireEvents();
      }
    }

    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets a collection representing the items contained in this RadDropDownList.")]
    [Editor("Telerik.WinControls.UI.Design.RadListControlCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public RadListDataItemCollection Items
    {
      get
      {
        return this.dropDownListElement.Items;
      }
    }

    public IReadOnlyCollection<RadListDataItem> SelectedItems
    {
      get
      {
        return this.ListElement.SelectedItems;
      }
    }

    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual object SelectedValue
    {
      get
      {
        return this.dropDownListElement.SelectedValue;
      }
      set
      {
        this.dropDownListElement.SelectedValue = value;
        this.OnNotifyPropertyChanged(nameof (SelectedValue));
      }
    }

    [Browsable(false)]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual RadListDataItem SelectedItem
    {
      get
      {
        return this.dropDownListElement.SelectedItem;
      }
      set
      {
        this.dropDownListElement.SelectedItem = value;
      }
    }

    [Browsable(false)]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual int SelectedIndex
    {
      get
      {
        return this.dropDownListElement.SelectedIndex;
      }
      set
      {
        this.dropDownListElement.SelectedIndex = value;
        this.OnNotifyPropertyChanged(nameof (SelectedIndex));
      }
    }

    [DefaultValue(null)]
    [Description("Gets or sets the object that is responsible for providing data objects for the AutoComplete Suggest.")]
    [AttributeProvider(typeof (IListSource))]
    [Category("Data")]
    [Browsable(false)]
    public object AutoCompleteDataSource
    {
      get
      {
        return this.dropDownListElement.AutoCompleteDataSource;
      }
      set
      {
        this.dropDownListElement.AutoCompleteDataSource = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Category("Data")]
    [RadDescription("DisplayMember", typeof (RadListElement))]
    [Browsable(false)]
    public string AutoCompleteDisplayMember
    {
      get
      {
        return this.dropDownListElement.AutoCompleteDisplayMember;
      }
      set
      {
        this.dropDownListElement.AutoCompleteDisplayMember = value;
      }
    }

    [RadDescription("ValueMember", typeof (RadListElement))]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Browsable(false)]
    [Category("Data")]
    [DefaultValue("")]
    public string AutoCompleteValueMember
    {
      get
      {
        return this.dropDownListElement.AutoCompleteValueMember;
      }
      set
      {
        this.dropDownListElement.AutoCompleteValueMember = value;
      }
    }

    [RadDescription("DataSource", typeof (RadListElement))]
    [Category("Data")]
    [RadDefaultValue("DataSource", typeof (RadListElement))]
    [AttributeProvider(typeof (IListSource))]
    public object DataSource
    {
      get
      {
        return this.dropDownListElement.DataSource;
      }
      set
      {
        this.dropDownListElement.DataSource = value;
        this.OnNotifyPropertyChanged(nameof (DataSource));
        this.OnDataBindingComplete((object) this, new ListBindingCompleteEventArgs(ListChangedType.Reset));
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Browsable(true)]
    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets the name of the list or table in the data source for which the RadListControl is displaying data. ")]
    public string DataMember
    {
      get
      {
        return this.dropDownListElement.DataMember;
      }
      set
      {
        this.dropDownListElement.DataMember = value;
        this.OnNotifyPropertyChanged(nameof (DataMember));
      }
    }

    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [DefaultValue("")]
    [Category("Data")]
    [RadDescription("DisplayMember", typeof (RadListElement))]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string DisplayMember
    {
      get
      {
        return this.dropDownListElement.DisplayMember;
      }
      set
      {
        this.dropDownListElement.DisplayMember = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [RadDescription("ValueMember", typeof (RadListElement))]
    [Category("Data")]
    [DefaultValue("")]
    public string ValueMember
    {
      get
      {
        return this.dropDownListElement.ValueMember;
      }
      set
      {
        this.dropDownListElement.ValueMember = value;
      }
    }

    [Browsable(true)]
    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [DefaultValue("")]
    [Description("Gets or sets the string through which the SelectedValue property will be determined. This property can not be set to null. Setting it to null will cause it to contain an empty string.")]
    public string DescriptionTextMember
    {
      get
      {
        return this.ListElement.DescriptionTextMember;
      }
      set
      {
        this.ListElement.DescriptionTextMember = value;
      }
    }

    [Category("Behavior")]
    [Description("Indicates whether users can change the selected item by the mouse wheel.")]
    [DefaultValue(true)]
    public bool EnableMouseWheel
    {
      get
      {
        return this.dropDownListElement.EnableMouseWheel;
      }
      set
      {
        this.dropDownListElement.EnableMouseWheel = value;
      }
    }

    [Browsable(false)]
    [Description("Indicating whether the Popup part of the control are displayed.")]
    public bool IsPopupVisible
    {
      get
      {
        return this.dropDownListElement.IsPopupOpen;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Predicate<RadListDataItem> Filter
    {
      get
      {
        return this.DropDownListElement.Filter;
      }
      set
      {
        this.DropDownListElement.Filter = value;
      }
    }

    [Browsable(true)]
    [Description("Gets or sets a filter expression which determines which items will be visible.")]
    [Category("Data")]
    [DefaultValue("")]
    public string FilterExpression
    {
      get
      {
        return this.DropDownListElement.FilterExpression;
      }
      set
      {
        this.DropDownListElement.FilterExpression = value;
      }
    }

    [Browsable(false)]
    public bool IsFilterActive
    {
      get
      {
        return this.DropDownListElement.IsFilterActive;
      }
    }

    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether the drop down list is read only.")]
    [Browsable(true)]
    public virtual bool ReadOnly
    {
      get
      {
        return this.dropDownListElement.ListElement.ReadOnly;
      }
      set
      {
        this.dropDownListElement.ListElement.ReadOnly = value;
        this.dropDownListElement.EditableElement.TextBox.TextBoxItem.ReadOnly = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue("")]
    [Localizable(true)]
    [Description("Gets or sets the text associated with this control.")]
    [Bindable(true)]
    [SettingsBindable(true)]
    [Editor("Telerik.WinControls.UI.Design.SimpleTextUITypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Browsable(true)]
    public override string Text
    {
      get
      {
        return this.dropDownListElement.EditableElementText;
      }
      set
      {
        if (!(this.dropDownListElement.EditableElementText != value))
          return;
        this.dropDownListElement.EditableElementText = value;
        this.OnTextChanged(EventArgs.Empty);
      }
    }

    [Category("Property Changed")]
    [Description("Occurs when the Text property value changes.")]
    public new event EventHandler TextChanged
    {
      add
      {
        this.DropDownListElement.TextChanged += value;
      }
      remove
      {
        this.DropDownListElement.TextChanged -= value;
      }
    }

    [RadDescription("NullText", typeof (RadTextBoxItem))]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    [Category("Behavior")]
    [Localizable(true)]
    public string NullText
    {
      get
      {
        return this.dropDownListElement.NullText;
      }
      set
      {
        this.dropDownListElement.NullText = value;
      }
    }

    public void SelectText(int start, int length)
    {
      this.dropDownListElement.SelectText(start, length);
    }

    public void SelectAllText()
    {
      this.dropDownListElement.SelectAllText();
    }

    public void SelectAll()
    {
      this.dropDownListElement.SelectAll();
    }

    [Description("Gets or sets the text that is selected in the editable portion of the DropDownList.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedText
    {
      get
      {
        return this.dropDownListElement.SelectedText;
      }
      set
      {
        this.dropDownListElement.SelectedText = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the number of characters selected in the editable portion of the combo box.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionLength
    {
      get
      {
        return this.dropDownListElement.SelectionLength;
      }
      set
      {
        this.dropDownListElement.SelectionLength = value;
      }
    }

    [Browsable(false)]
    [Description("Gets or sets the starting index of text selected in the combo box.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
      get
      {
        return this.dropDownListElement.SelectionStart;
      }
      set
      {
        this.dropDownListElement.SelectionStart = value;
      }
    }

    [DefaultValue(true)]
    public new bool CausesValidation
    {
      get
      {
        return base.CausesValidation;
      }
      set
      {
        base.CausesValidation = value;
        this.dropDownListElement.TextBox.TextBoxItem.HostedControl.CausesValidation = base.CausesValidation;
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
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IComparer<RadListDataItem> ItemsSortComparer
    {
      get
      {
        return this.ListElement.ItemsSortComparer;
      }
      set
      {
        this.ListElement.ItemsSortComparer = value;
      }
    }

    [Description("Fires after data binding operation has finished.")]
    [Category("Data")]
    [Browsable(true)]
    public event ListBindingCompleteEventHandler DataBindingComplete;

    protected virtual void OnDataBindingComplete(object sender, ListBindingCompleteEventArgs e)
    {
      if (this.DataBindingComplete == null)
        return;
      this.DataBindingComplete((object) this, e);
    }

    public new event KeyPressEventHandler KeyPress
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.KeyPressEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.KeyPressEventKey, (Delegate) value);
      }
    }

    public new event KeyEventHandler KeyUp
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.KeyUpEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.KeyUpEventKey, (Delegate) value);
      }
    }

    public new event KeyEventHandler KeyDown
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.KeyDownEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.KeyDownEventKey, (Delegate) value);
      }
    }

    public event EventHandler PopupOpened
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.PopupOpenedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.PopupOpenedEventKey, (Delegate) value);
      }
    }

    public event CancelEventHandler PopupOpening
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.PopupOpeningEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.PopupOpeningEventKey, (Delegate) value);
      }
    }

    public event RadPopupClosingEventHandler PopupClosing
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.PopupClosingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.PopupClosingEventKey, (Delegate) value);
      }
    }

    public event RadPopupClosedEventHandler PopupClosed
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.PopupClosedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.PopupClosedEventKey, (Delegate) value);
      }
    }

    public event Telerik.WinControls.UI.Data.PositionChangedEventHandler SelectedIndexChanged
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.SelectedIndexChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.SelectedIndexChangedEventKey, (Delegate) value);
      }
    }

    public event Telerik.WinControls.UI.Data.PositionChangingEventHandler SelectedIndexChanging
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.SelectedIndexChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.SelectedIndexChangingEventKey, (Delegate) value);
      }
    }

    public event EventHandler SelectedValueChanged
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.SelectedValueChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.SelectedValueChangedEventKey, (Delegate) value);
      }
    }

    public event ListItemDataBindingEventHandler ItemDataBinding
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.ListItemDataBindingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.ListItemDataBindingEventKey, (Delegate) value);
      }
    }

    public event ListItemDataBoundEventHandler ItemDataBound
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.ListItemDataBoundEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.ListItemDataBoundEventKey, (Delegate) value);
      }
    }

    public event CreatingVisualListItemEventHandler CreatingVisualListItem
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.CreatingVisualListItemEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.CreatingVisualListItemEventKey, (Delegate) value);
      }
    }

    public event SortStyleChangedEventHandler SortStyleChanged
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.SortStyleChangedKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.SortStyleChangedKey, (Delegate) value);
      }
    }

    public event VisualListItemFormattingEventHandler VisualListItemFormatting
    {
      add
      {
        this.Events.AddHandler(RadDropDownList.VisualItemFormattingKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownList.VisualItemFormattingKey, (Delegate) value);
      }
    }

    public RadListDataItem FindItemExact(string text, bool caseSensitive)
    {
      return this.ListElement.FindItemExact(text, caseSensitive);
    }

    public int FindString(string s)
    {
      return this.ListElement.FindString(s, 0);
    }

    public int FindString(string s, int startIndex)
    {
      return this.ListElement.FindString(s, startIndex);
    }

    public int FindStringExact(string s)
    {
      return this.FindStringExact(s, 0);
    }

    public int FindStringExact(string s, int startIndex)
    {
      return this.ListElement.FindStringExact(s, startIndex);
    }

    public int FindStringNonWrapping(string s)
    {
      return this.FindStringNonWrapping(s, 0);
    }

    public int FindStringNonWrapping(string s, int startIndex)
    {
      return this.ListElement.FindStringNonWrapping(s, startIndex);
    }

    public void Rebind()
    {
      this.dropDownListElement.ListElement.Rebind();
    }

    public virtual void ShowDropDown()
    {
      this.DropDownListElement.ShowPopup();
    }

    public virtual void CloseDropDown()
    {
      this.DropDownListElement.ClosePopup();
    }

    public virtual void BeginUpdate()
    {
      this.DropDownListElement.BeginUpdate();
    }

    public virtual void EndUpdate()
    {
      this.DropDownListElement.EndUpdate();
    }

    public virtual IDisposable DeferRefresh()
    {
      return this.DropDownListElement.DeferRefresh();
    }

    private void WireEvents()
    {
      this.dropDownListElement.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.element_SelectedIndexChanged);
      this.dropDownListElement.SelectedIndexChanging += new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.element_SelectedIndexChanging);
      this.dropDownListElement.SelectedValueChanged += new ValueChangedEventHandler(this.element_SelectedValueChanged);
      this.dropDownListElement.ItemDataBinding += new ListItemDataBindingEventHandler(this.element_ItemDataBinding);
      this.dropDownListElement.ItemDataBound += new ListItemDataBoundEventHandler(this.element_ItemDataBound);
      this.dropDownListElement.CreatingVisualItem += new CreatingVisualListItemEventHandler(this.element_CreatingVisualItem);
      this.dropDownListElement.PopupOpened += new EventHandler(this.element_PopupOpened);
      this.dropDownListElement.PopupOpening += new CancelEventHandler(this.element_PopupOpening);
      this.dropDownListElement.PopupClosed += new RadPopupClosedEventHandler(this.element_PopupClosed);
      this.dropDownListElement.PopupClosing += new RadPopupClosingEventHandler(this.element_PopupClosing);
      this.dropDownListElement.SortStyleChanged += new SortStyleChangedEventHandler(this.dropDownListElement_SortStyleChanged);
      this.dropDownListElement.VisualItemFormatting += new VisualListItemFormattingEventHandler(this.dropDownListElement_VisualItemFormatting);
      this.dropDownListElement.KeyDown += new KeyEventHandler(this.dropDownListElement_KeyDown);
      this.dropDownListElement.KeyPress += new KeyPressEventHandler(this.dropDownListElement_KeyPress);
      this.dropDownListElement.KeyUp += new KeyEventHandler(this.dropDownListElement_KeyUp);
    }

    private void UnwireEvents()
    {
      this.dropDownListElement.SelectedIndexChanged -= new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.element_SelectedIndexChanged);
      this.dropDownListElement.SelectedIndexChanging -= new Telerik.WinControls.UI.Data.PositionChangingEventHandler(this.element_SelectedIndexChanging);
      this.dropDownListElement.SelectedValueChanged -= new ValueChangedEventHandler(this.element_SelectedValueChanged);
      this.dropDownListElement.ItemDataBinding -= new ListItemDataBindingEventHandler(this.element_ItemDataBinding);
      this.dropDownListElement.ItemDataBound -= new ListItemDataBoundEventHandler(this.element_ItemDataBound);
      this.dropDownListElement.CreatingVisualItem -= new CreatingVisualListItemEventHandler(this.element_CreatingVisualItem);
      this.dropDownListElement.PopupOpened -= new EventHandler(this.element_PopupOpened);
      this.dropDownListElement.PopupOpening -= new CancelEventHandler(this.element_PopupOpening);
      this.dropDownListElement.PopupClosed -= new RadPopupClosedEventHandler(this.element_PopupClosed);
      this.dropDownListElement.PopupClosing -= new RadPopupClosingEventHandler(this.element_PopupClosing);
      this.dropDownListElement.SortStyleChanged -= new SortStyleChangedEventHandler(this.dropDownListElement_SortStyleChanged);
      this.dropDownListElement.VisualItemFormatting -= new VisualListItemFormattingEventHandler(this.dropDownListElement_VisualItemFormatting);
      this.dropDownListElement.KeyDown -= new KeyEventHandler(this.dropDownListElement_KeyDown);
      this.dropDownListElement.KeyPress -= new KeyPressEventHandler(this.dropDownListElement_KeyPress);
      this.dropDownListElement.KeyUp -= new KeyEventHandler(this.dropDownListElement_KeyUp);
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      this.AccessibilityRequested = true;
      return (AccessibleObject) new RadDropDownListAccessibleObject(this);
    }

    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      this.dropDownListElement.RightToLeft = this.RightToLeft == RightToLeft.Yes;
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      try
      {
        base.OnBindingContextChanged(e);
        this.dropDownListElement.BindingContext = this.BindingContext;
      }
      catch
      {
      }
    }

    protected override void OnEnter(EventArgs e)
    {
      if (this.entering)
        return;
      base.OnEnter(e);
      this.entering = true;
      this.DropDownListElement.EditableElement.Entering = true;
      if (!string.IsNullOrEmpty(this.dropDownListElement.Text))
        this.dropDownListElement.SelectAllText();
      if (this.DropDownStyle != RadDropDownStyle.DropDown)
        return;
      this.OnGotFocus(e);
    }

    protected override void OnLeave(EventArgs e)
    {
      base.OnLeave(e);
      this.OnLostFocus(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      if (this.DropDownStyle != RadDropDownStyle.DropDownList)
        return;
      this.DropDownListElement.Focus();
      this.DropDownListElement.SelectAllText();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      this.DropDownListElement.EnterPressedOrLeaveControl();
      if (this.SelectionLength == this.Text.Length)
      {
        this.dropDownListElement.SelectionStart = 0;
        this.dropDownListElement.SelectionLength = 0;
      }
      if (!this.entering)
        return;
      base.OnLostFocus(e);
      this.entering = false;
    }

    private void element_SelectedIndexChanging(object sender, PositionChangingCancelEventArgs e)
    {
      e.Cancel = this.OnSelectedIndexChanging(sender, e.Position);
    }

    private void element_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.OnSelectedIndexChanged(sender, e.Position);
    }

    private void element_SelectedValueChanged(object sender, Telerik.WinControls.UI.Data.ValueChangedEventArgs e)
    {
      this.OnSelectedValueChanged(sender, e.Position, e.OldValue, e.NewValue);
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

    private void dropDownListElement_VisualItemFormatting(
      object sender,
      VisualItemFormattingEventArgs args)
    {
      this.OnVisualItemFormatting(args.VisualItem);
    }

    private void dropDownListElement_SortStyleChanged(object sender, SortStyleChangedEventArgs args)
    {
      this.OnSortStyleChanged(args.SortStyle);
    }

    protected virtual void OnSortStyleChanged(SortStyle sortStyle)
    {
      SortStyleChangedEventHandler changedEventHandler = (SortStyleChangedEventHandler) this.Events[RadDropDownList.SortStyleChangedKey];
      if (changedEventHandler == null)
        return;
      changedEventHandler((object) this, new SortStyleChangedEventArgs(sortStyle));
    }

    protected internal virtual void OnVisualItemFormatting(RadListVisualItem item)
    {
      VisualListItemFormattingEventHandler formattingEventHandler = (VisualListItemFormattingEventHandler) this.Events[RadDropDownList.VisualItemFormattingKey];
      if (formattingEventHandler == null)
        return;
      formattingEventHandler((object) this, new VisualItemFormattingEventArgs(item));
    }

    protected virtual void OnSelectedIndexChanged(object sender, int newIndex)
    {
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs("SelectedIndex"));
      Telerik.WinControls.UI.Data.PositionChangedEventHandler changedEventHandler = (Telerik.WinControls.UI.Data.PositionChangedEventHandler) this.Events[RadDropDownList.SelectedIndexChangedEventKey];
      if (changedEventHandler != null)
        changedEventHandler((object) this, new Telerik.WinControls.UI.Data.PositionChangedEventArgs(newIndex));
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "SelectionChanged", (object) this.SelectedIndex);
    }

    protected virtual bool OnSelectedIndexChanging(object sender, int newIndex)
    {
      Telerik.WinControls.UI.Data.PositionChangingEventHandler changingEventHandler = (Telerik.WinControls.UI.Data.PositionChangingEventHandler) this.Events[RadDropDownList.SelectedIndexChangingEventKey];
      if (changingEventHandler == null)
        return false;
      PositionChangingCancelEventArgs e = new PositionChangingCancelEventArgs(newIndex);
      changingEventHandler((object) this, e);
      return e.Cancel;
    }

    protected virtual void OnSelectedValueChanged(
      object sender,
      int newIndex,
      object oldValue,
      object newValue)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadDropDownList.SelectedValueChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, (EventArgs) new Telerik.WinControls.UI.Data.ValueChangedEventArgs(newIndex, newValue, oldValue));
    }

    protected virtual void OnItemDataBinding(object sender, ListItemDataBindingEventArgs args)
    {
      ListItemDataBindingEventHandler bindingEventHandler = (ListItemDataBindingEventHandler) this.Events[RadDropDownList.ListItemDataBindingEventKey];
      if (bindingEventHandler == null)
        return;
      bindingEventHandler((object) this, args);
    }

    protected virtual void OnItemDataBound(object sender, ListItemDataBoundEventArgs args)
    {
      ListItemDataBoundEventHandler boundEventHandler = (ListItemDataBoundEventHandler) this.Events[RadDropDownList.ListItemDataBoundEventKey];
      if (boundEventHandler == null)
        return;
      boundEventHandler((object) this, args);
    }

    protected virtual void OnCreatingVisualItem(object sender, CreatingVisualListItemEventArgs args)
    {
      CreatingVisualListItemEventHandler itemEventHandler = (CreatingVisualListItemEventHandler) this.Events[RadDropDownList.CreatingVisualListItemEventKey];
      if (itemEventHandler == null)
        return;
      itemEventHandler((object) this, args);
    }

    protected virtual void element_PopupClosing(object sender, RadPopupClosingEventArgs args)
    {
      RadPopupClosingEventHandler closingEventHandler = (RadPopupClosingEventHandler) this.Events[RadDropDownList.PopupClosingEventKey];
      if (closingEventHandler == null)
        return;
      closingEventHandler(sender, args);
    }

    protected virtual void element_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      RadPopupClosedEventHandler closedEventHandler = (RadPopupClosedEventHandler) this.Events[RadDropDownList.PopupClosedEventKey];
      if (closedEventHandler != null)
        closedEventHandler(sender, args);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Closed");
    }

    protected virtual void element_PopupOpening(object sender, CancelEventArgs e)
    {
      CancelEventHandler cancelEventHandler = (CancelEventHandler) this.Events[RadDropDownList.PopupOpeningEventKey];
      if (cancelEventHandler == null)
        return;
      cancelEventHandler(sender, e);
    }

    protected virtual void element_PopupOpened(object sender, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadDropDownList.PopupOpenedEventKey];
      if (eventHandler != null)
        eventHandler(sender, e);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Opened");
    }

    protected virtual void dropDownListElement_KeyUp(object sender, KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadDropDownList.KeyUpEventKey];
      if (keyEventHandler == null)
        return;
      keyEventHandler((object) this, e);
    }

    protected virtual void dropDownListElement_KeyPress(object sender, KeyPressEventArgs e)
    {
      KeyPressEventHandler pressEventHandler = (KeyPressEventHandler) this.Events[RadDropDownList.KeyPressEventKey];
      if (pressEventHandler == null)
        return;
      pressEventHandler((object) this, e);
    }

    protected virtual void dropDownListElement_KeyDown(object sender, KeyEventArgs e)
    {
      KeyEventHandler keyEventHandler = (KeyEventHandler) this.Events[RadDropDownList.KeyDownEventKey];
      if (keyEventHandler == null)
        return;
      object sender1 = (object) this.dropDownListElement;
      DropDownPopupForm popup = this.Popup as DropDownPopupForm;
      if (this.DropDownStyle == RadDropDownStyle.DropDownList && popup != null && popup.LastPressedKey.HasValue)
      {
        sender1 = (object) popup;
        popup.LastPressedKey = new Keys?();
      }
      keyEventHandler(sender1, e);
    }

    protected override RootRadElement CreateRootElement()
    {
      return (RootRadElement) new RadDropDownListRootElement();
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
      if (value)
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = false;
      }
      else
      {
        this.RootElement.StretchHorizontally = true;
        this.RootElement.StretchVertically = true;
      }
      this.RootElement.SaveCurrentStretchModeAsDefault();
    }

    protected override void ProcessCodedUIMessage(ref IPCMessage request)
    {
      if (request == null)
        return;
      if (request.Type == IPCMessage.MessageTypes.GetPropertyValue)
      {
        if (request.Message == "IsEditable")
        {
          request.Data = (object) (this.DropDownStyle == RadDropDownStyle.DropDown);
          return;
        }
        if (request.Message == "Expanded")
        {
          request.Data = (object) this.IsPopupVisible;
          return;
        }
        if (request.Message == "SelectedItem")
        {
          if (this.SelectedItem != null)
          {
            request.Data = (object) this.SelectedItem.Text;
            return;
          }
          request.Data = (object) "";
          return;
        }
      }
      if (request.Type == IPCMessage.MessageTypes.SetChildPropertyValue && request.Data != null && (request.ControlType != "" && request.Message == "Selected"))
        this.SelectedItem = this.FindItemExact(request.ControlType, false);
      else
        base.ProcessCodedUIMessage(ref request);
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      if (m.Msg != 7)
        return;
      this.entering = true;
      if (this.DropDownStyle != RadDropDownStyle.DropDown)
        return;
      this.DropDownListElement.TextBox.TextBoxItem.HostedControl.Focus();
      this.dropDownListElement.TextBox.TextBoxItem.SelectAll();
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      System.Type type = element.GetType();
      if (type.Equals(typeof (RadTextBoxElement)) || type.Equals(typeof (RadArrowButtonElement)))
        return true;
      return base.ControlDefinesThemeForElement(element);
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

    protected override void SetBackColorThemeOverrides()
    {
      this.DropDownListElement.SuspendApplyOfThemeSettings();
      this.DropDownListElement.EditableElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.DropDownListElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.DropDownListElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.DropDownListElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "DropDownFill");
        this.DropDownListElement.EditableElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.DropDownListElement.EditableElement.TextBox.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TextBoxFill");
      }
      this.DropDownListElement.ResumeApplyOfThemeSettings();
      this.DropDownListElement.EditableElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.DropDownListElement.SuspendApplyOfThemeSettings();
      this.DropDownListElement.EditableElement.SuspendApplyOfThemeSettings();
      this.DropDownListElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num1 = (int) this.DropDownListElement.Children[1].ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.DropDownListElement.EditableElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num2 = (int) this.DropDownListElement.EditableElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.DropDownListElement.EditableElement.TextBox.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num3 = (int) this.DropDownListElement.EditableElement.TextBox.Fill.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.DropDownListElement.ElementTree.ApplyThemeToElementTree();
      this.DropDownListElement.ResumeApplyOfThemeSettings();
      this.DropDownListElement.EditableElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.DropDownListElement.SuspendApplyOfThemeSettings();
      this.DropDownListElement.EditableElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.DropDownListElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.DropDownListElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.DropDownListElement.EditableElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.DropDownListElement.EditableElement.TextBox.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      }
      this.DropDownListElement.ResumeApplyOfThemeSettings();
      this.DropDownListElement.EditableElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.DropDownListElement.SuspendApplyOfThemeSettings();
      this.DropDownListElement.EditableElement.SuspendApplyOfThemeSettings();
      this.DropDownListElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.DropDownListElement.EditableElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num = (int) this.DropDownListElement.EditableElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.DropDownListElement.EditableElement.TextBox.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.DropDownListElement.ElementTree.ApplyThemeToElementTree();
      this.DropDownListElement.ResumeApplyOfThemeSettings();
      this.DropDownListElement.EditableElement.ResumeApplyOfThemeSettings();
    }
  }
}
