// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarDropDownList
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.Design;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  public class CommandBarDropDownList : RadCommandBarBaseItem
  {
    private RadDropDownListElement dropDownListElement;

    static CommandBarDropDownList()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (CommandBarDropDownList));
    }

    [DefaultValue("")]
    [Description("Gets or sets the text associated with this item.")]
    [Bindable(true)]
    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Localizable(true)]
    [SettingsBindable(true)]
    [Category("Behavior")]
    public override string Text
    {
      get
      {
        if (this.dropDownListElement != null)
          return this.dropDownListElement.EditableElementText;
        return base.Text;
      }
      set
      {
        base.Text = value;
        if (this.dropDownListElement == null)
          return;
        this.dropDownListElement.EditableElementText = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadDropDownListElement CommandBarDropDownListElement
    {
      get
      {
        return this.dropDownListElement;
      }
      set
      {
        this.dropDownListElement = value;
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [ParenthesizePropertyName(true)]
    [Description("Gets the collection of data-binding objects for this IBindableComponent.")]
    public override ControlBindingsCollection DataBindings
    {
      get
      {
        return this.dropDownListElement.DataBindings;
      }
    }

    [Browsable(false)]
    [RadPropertyDefaultValue("BindingContext", typeof (RadObject))]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public override BindingContext BindingContext
    {
      get
      {
        return this.dropDownListElement.BindingContext;
      }
      set
      {
        this.dropDownListElement.BindingContext = value;
      }
    }

    [Description("Gets a collection representing the items contained in this RadDropDownList.")]
    [Category("Data")]
    [Editor("Telerik.WinControls.UI.Design.RadListControlCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadListDataItemCollection Items
    {
      get
      {
        return this.dropDownListElement.Items;
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

    [DefaultValue(true)]
    [Description("Determines whether control's height will be determines automatically, depending on the current Font.")]
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

    [DefaultValue(8)]
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
    [Description("Gets or sets a value that indicates whether items will be sized according to their content. If this property is true the user can set the Height property of each individual RadListDataItem in the Items collection in order to override the automatic sizing.")]
    [Browsable(true)]
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
    public int MaxLength
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

    [Category("Appearance")]
    [Browsable(true)]
    [Description("Gets or sets the drop down minimal size.")]
    [DefaultValue(typeof (Size), "0,0")]
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

    [Category("Appearance")]
    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, veritcal or a combination of them.")]
    [DefaultValue(SizingMode.None)]
    [Browsable(true)]
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
    [RadDescription("CaseSensitive", typeof (RadDropDownListElement))]
    [Browsable(true)]
    [Category("Behavior")]
    public bool CaseSensitive
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

    [RadDescription("AutoCompleteMode", typeof (RadDropDownListElement))]
    [Browsable(true)]
    [Category("Behavior")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(AutoCompleteMode.None)]
    public AutoCompleteMode AutoCompleteMode
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

    [DefaultValue(false)]
    [Browsable(true)]
    [Category("Behavior")]
    [RadDescription("SelectNextOnDoubleClick", typeof (RadDropDownListElement))]
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

    [Browsable(false)]
    [Category("Appearance")]
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

    [DefaultValue("")]
    [Category("Appearance")]
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

    [DefaultValue(RadEasingType.InQuad)]
    [Category("Appearance")]
    [Browsable(true)]
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
    [DefaultValue(false)]
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

    [DefaultValue(1)]
    [Category("Appearance")]
    [Description("Gets or sets the number of frames that will be used when the DropDown is being animated.")]
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

    [RadDescription("DropDownHeight", typeof (RadDropDownListElement))]
    [Category("Behavior")]
    [DefaultValue(106)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [RefreshProperties(RefreshProperties.Repaint)]
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

    [Category("Appearance")]
    [RadPropertyDefaultValue("DropDownStyle", typeof (RadDropDownListElement))]
    [Description("Gets or sets a value specifying the style of the combo box.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public RadDropDownStyle DropDownStyle
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

    [Category("Appearance")]
    [RadDefaultValue("DropDownMaxSize", typeof (RadDropDownListElement))]
    [Description("Gets or sets the drop down maximal size.")]
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
      set
      {
        this.dropDownListElement = value;
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
    }

    public IReadOnlyCollection<RadListDataItem> SelectedItems
    {
      get
      {
        return this.ListElement.SelectedItems;
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
    [Bindable(true)]
    public RadListDataItem SelectedItem
    {
      get
      {
        return this.dropDownListElement.ListElement.SelectedItem;
      }
      set
      {
        this.dropDownListElement.ListElement.SelectedItem = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectedIndex
    {
      get
      {
        return this.dropDownListElement.ListElement.SelectedIndex;
      }
      set
      {
        this.dropDownListElement.ListElement.SelectedIndex = value;
      }
    }

    [Browsable(false)]
    [DefaultValue(null)]
    [AttributeProvider(typeof (IListSource))]
    [Category("Data")]
    [Description("Gets or sets the object that is responsible for providing data objects for the AutoComplete Suggest.")]
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
    [Browsable(false)]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [RadDescription("DisplayMember", typeof (RadListElement))]
    [Category("Data")]
    public string AutoCompleteDisplayMember
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

    [DefaultValue("")]
    [RadDescription("ValueMember", typeof (RadListElement))]
    [Category("Data")]
    [Browsable(false)]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public string AutoCompleteValueMember
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

    [RadDescription("DataSource", typeof (RadListElement))]
    [AttributeProvider(typeof (IListSource))]
    [RadDefaultValue("DataSource", typeof (RadListElement))]
    [Category("Data")]
    public object DataSource
    {
      get
      {
        return this.dropDownListElement.DataSource;
      }
      set
      {
        this.dropDownListElement.DataSource = value;
      }
    }

    [DefaultValue("")]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [RadDescription("DisplayMember", typeof (RadListElement))]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
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

    [Category("Data")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [RadDescription("ValueMember", typeof (RadListElement))]
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

    [Description("Indicates whether users can change the selected item by the mouse wheel.")]
    [DefaultValue(true)]
    [Category("Behavior")]
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

    [Description("Indicating whether the Popup part of the control are displayed.")]
    [Browsable(false)]
    public bool IsPopupVisible
    {
      get
      {
        return this.dropDownListElement.IsPopupOpen;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
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

    [Category("Data")]
    [DefaultValue("")]
    [Description("Gets or sets a filter expression which determines which items will be visible.")]
    [Browsable(true)]
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

    [Localizable(true)]
    [RadDescription("NullText", typeof (RadTextBoxItem))]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    [Category("Behavior")]
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
      this.dropDownListElement.SelectAll();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets or sets the text that is selected in the editable portion of the DropDownList.")]
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

    [Description("Gets or sets the number of characters selected in the editable portion of the combo box.")]
    [Browsable(false)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets or sets the starting index of text selected in the combo box.")]
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

    public void Select()
    {
      this.dropDownListElement.Select();
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Indicates whether the item should be drawn in the strip.")]
    public override bool VisibleInStrip
    {
      get
      {
        return base.VisibleInStrip;
      }
      set
      {
        base.VisibleInStrip = value;
        if (this.dropDownListElement == null)
          return;
        int num = (int) this.dropDownListElement.SetValue(RadElement.VisibilityProperty, (object) (ElementVisibility) (value ? 0 : 2));
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.dropDownListElement = new RadDropDownListElement();
      this.dropDownListElement.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
      this.dropDownListElement.AutoSize = true;
      this.StretchHorizontally = this.StretchVertically = false;
      this.MinSize = new Size(106, 22);
      this.Children.Add((RadElement) this.dropDownListElement);
    }

    public event Telerik.WinControls.UI.Data.PositionChangedEventHandler SelectedIndexChanged
    {
      add
      {
        this.dropDownListElement.SelectedIndexChanged += value;
      }
      remove
      {
        this.dropDownListElement.SelectedIndexChanged -= value;
      }
    }

    public event Telerik.WinControls.UI.Data.PositionChangingEventHandler SelectedIndexChanging
    {
      add
      {
        this.dropDownListElement.SelectedIndexChanging += value;
      }
      remove
      {
        this.dropDownListElement.SelectedIndexChanging -= value;
      }
    }

    public event ValueChangedEventHandler SelectedValueChanged
    {
      add
      {
        this.dropDownListElement.SelectedValueChanged += value;
      }
      remove
      {
        this.dropDownListElement.SelectedValueChanged -= value;
      }
    }

    public event ListItemDataBindingEventHandler ItemDataBinding
    {
      add
      {
        this.dropDownListElement.ItemDataBinding += value;
      }
      remove
      {
        this.dropDownListElement.ItemDataBinding -= value;
      }
    }

    public event ListItemDataBoundEventHandler ItemDataBound
    {
      add
      {
        this.dropDownListElement.ItemDataBound += value;
      }
      remove
      {
        this.dropDownListElement.ItemDataBound -= value;
      }
    }

    public event CreatingVisualListItemEventHandler CreatingVisualItem
    {
      add
      {
        this.dropDownListElement.CreatingVisualItem += value;
      }
      remove
      {
        this.dropDownListElement.CreatingVisualItem -= value;
      }
    }

    public event SortStyleChangedEventHandler SortStyleChanged
    {
      add
      {
        this.dropDownListElement.SortStyleChanged += value;
      }
      remove
      {
        this.dropDownListElement.SortStyleChanged -= value;
      }
    }

    public event VisualListItemFormattingEventHandler VisualItemFormatting
    {
      add
      {
        this.dropDownListElement.VisualItemFormatting += value;
      }
      remove
      {
        this.dropDownListElement.VisualItemFormatting -= value;
      }
    }

    [Description("Occurs when the popup is about to be opened.")]
    [Category("Popup")]
    public event CancelEventHandler PopupOpening
    {
      add
      {
        this.dropDownListElement.PopupOpening += value;
      }
      remove
      {
        this.dropDownListElement.PopupOpening -= value;
      }
    }

    [Description("Occurs when the popup is opened.")]
    [Category("Popup")]
    public event EventHandler PopupOpened
    {
      add
      {
        this.dropDownListElement.PopupOpened += value;
      }
      remove
      {
        this.dropDownListElement.PopupOpened -= value;
      }
    }

    [Description("Occurs when the popup is about to be closed.")]
    [Category("Popup")]
    public event RadPopupClosingEventHandler PopupClosing
    {
      add
      {
        this.dropDownListElement.PopupClosing += value;
      }
      remove
      {
        this.dropDownListElement.PopupClosing -= value;
      }
    }

    [Description("Occurs when the popup is closed.")]
    [Category("Popup")]
    public event RadPopupClosedEventHandler PopupClosed
    {
      add
      {
        this.dropDownListElement.PopupClosed += value;
      }
      remove
      {
        this.dropDownListElement.PopupClosed -= value;
      }
    }

    [Description("Occurs when the RadItem has focus and the user presses a key")]
    [Category("Key")]
    public new event KeyPressEventHandler KeyPress
    {
      add
      {
        this.dropDownListElement.KeyPress += value;
      }
      remove
      {
        this.dropDownListElement.KeyPress -= value;
      }
    }

    [Category("Key")]
    [Description("Occurs when the RadItem has focus and the user releases the pressed key up")]
    public new event KeyEventHandler KeyUp
    {
      add
      {
        this.dropDownListElement.KeyUp += value;
      }
      remove
      {
        this.dropDownListElement.KeyUp -= value;
      }
    }

    [Description("Occurs when the RadItem has focus and the user presses a key down")]
    [Category("Key")]
    public new event KeyEventHandler KeyDown
    {
      add
      {
        this.dropDownListElement.KeyDown += value;
      }
      remove
      {
        this.dropDownListElement.KeyDown -= value;
      }
    }

    [Description("Occurs when the Text property value is about to be changed.")]
    [Category("Property Changed")]
    public new event TextChangingEventHandler TextChanging
    {
      add
      {
        this.dropDownListElement.TextChanging += value;
      }
      remove
      {
        this.dropDownListElement.TextChanging -= value;
      }
    }

    [Category("Property Changed")]
    [Description("Occurs when the Text property value changes.")]
    public new event EventHandler TextChanged
    {
      add
      {
        this.dropDownListElement.TextChanged += value;
      }
      remove
      {
        this.dropDownListElement.TextChanged -= value;
      }
    }
  }
}
