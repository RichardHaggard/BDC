// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownListElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  [RadToolboxItem(true)]
  public class RadDropDownListElement : PopupEditorElement
  {
    private bool enableMouseWheel = true;
    private List<BaseAutoComplete> autoCompleteHelpers = new List<BaseAutoComplete>();
    private bool showImageInEditorArea = true;
    private int oldSelectedIndex = -1;
    private bool syncSelectionWithText = true;
    internal int selectedIndexOnPopupOpen = -1;
    private static RadProperty DropDownStyleProperty = RadProperty.Register(nameof (DropDownStyle), typeof (RadDropDownStyle), typeof (RadDropDownListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDropDownStyle.DropDown, ElementPropertyOptions.None));
    private static RadProperty CaseSensitiveProperty = RadProperty.Register(nameof (CaseSensitive), typeof (bool), typeof (RadDropDownListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    public static RadProperty IsDropDownShownProperty = RadProperty.Register(nameof (IsDropDownShown), typeof (bool), typeof (RadDropDownListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private RadDropDownListEditableAreaElement editableElement;
    private RadArrowButtonElement arrowButton;
    private BorderPrimitive borderPrimitive;
    private FillPrimitive fillPrimitive;
    private AutoCompleteMode autoCompleteMode;
    private SizingMode sizingMode;
    private int beginUpdateCount;
    private bool selectNextOnDoubleClick;
    private AutoCompleteAppendHelper autoCompleteAppend;
    private AutoCompleteSuggestHelper autoCompleteSuggest;
    private bool skipSelectionClear;
    private bool itemsChanged;
    private bool isTextChanged;
    internal bool isSuggestMode;

    static RadDropDownListElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadTextBoxElementStateManager(), typeof (RadDropDownListElement));
      RadElement.CanFocusProperty.OverrideMetadata(typeof (RadDropDownListElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    }

    protected override void DisposeManagedResources()
    {
      this.arrowButton.Click -= new EventHandler(this.arrowButton_Click);
      this.fillPrimitive.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.fillPrimitive_RadPropertyChanged);
      this.EditableElement.TextBox.TextBoxItem.TextChanged -= new EventHandler(this.CallTextChanged);
      base.DisposeManagedResources();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.editableElement = this.CreateTextEditorElement();
      this.EditableElement.TextBox.TextBoxItem.TextChanged += new EventHandler(this.CallTextChanged);
      int num1 = (int) this.BindProperty(RadTextBoxItem.IsNullTextProperty, (RadObject) this.editableElement.TextBox.TextBoxItem, RadTextBoxItem.IsNullTextProperty, PropertyBindingOptions.OneWay);
      this.arrowButton = (RadArrowButtonElement) this.CreateArrowButtonElement();
      int num2 = (int) this.arrowButton.Border.SetDefaultValueOverride(RadElement.VisibilityProperty, (object) ElementVisibility.Collapsed);
      this.arrowButton.ClickMode = ClickMode.Press;
      this.arrowButton.Click += new EventHandler(this.arrowButton_Click);
      this.arrowButton.ZIndex = 1;
      this.arrowButton.StretchHorizontally = false;
      this.arrowButton.StretchVertically = true;
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "DropDownListBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "DropDownFill";
      this.fillPrimitive.ZIndex = -1;
      this.fillPrimitive.RadPropertyChanged += new RadPropertyChangedEventHandler(this.fillPrimitive_RadPropertyChanged);
      this.Children.Add((RadElement) this.fillPrimitive);
      StackLayoutElement stackLayoutElement = new StackLayoutElement();
      stackLayoutElement.CanFocus = false;
      stackLayoutElement.StretchVertically = true;
      stackLayoutElement.StretchHorizontally = true;
      stackLayoutElement.Class = "DropDownListStack";
      stackLayoutElement.FitInAvailableSize = true;
      this.Children.Add((RadElement) stackLayoutElement);
      stackLayoutElement.Children.Add((RadElement) this.editableElement);
      stackLayoutElement.Children.Add((RadElement) this.arrowButton);
      int num3 = (int) this.BindProperty(RadItem.TextProperty, (RadObject) this.editableElement, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num4 = (int) this.BindProperty(LightVisualElement.ImageProperty, (RadObject) this.editableElement, LightVisualElement.ImageProperty, PropertyBindingOptions.TwoWay);
      int num5 = (int) this.editableElement.TextBox.BindProperty(VisualElement.BackColorProperty, (RadObject) this.fillPrimitive, VisualElement.BackColorProperty, PropertyBindingOptions.TwoWay);
    }

    protected virtual RadDropDownListEditableAreaElement CreateTextEditorElement()
    {
      return new RadDropDownListEditableAreaElement(this);
    }

    protected virtual RadDropDownListArrowButtonElement CreateArrowButtonElement()
    {
      return new RadDropDownListArrowButtonElement();
    }

    protected override RadPopupControlBase CreatePopupForm()
    {
      this.Popup = (RadEditorPopupControlBase) new DropDownPopupForm(this);
      this.Popup.VerticalAlignmentCorrectionMode = AlignmentCorrectionMode.SnapToOuterEdges;
      this.Popup.SizingMode = this.sizingMode;
      this.Popup.Height = this.DropDownHeight;
      this.Popup.HorizontalAlignmentCorrectionMode = AlignmentCorrectionMode.Smooth;
      this.Popup.RightToLeft = this.RightToLeft ? System.Windows.Forms.RightToLeft.Yes : System.Windows.Forms.RightToLeft.Inherit;
      this.WirePopupFormEvents((RadPopupControlBase) this.Popup);
      return (RadPopupControlBase) this.Popup;
    }

    [DefaultValue(true)]
    public virtual bool SyncSelectionWithText
    {
      get
      {
        if (this.syncSelectionWithText)
          return this.DropDownStyle == RadDropDownStyle.DropDown;
        return false;
      }
      set
      {
        this.syncSelectionWithText = value;
      }
    }

    public virtual string EditableElementText
    {
      get
      {
        return this.editableElement.TextBox.TextBoxItem.HostedControl.Text;
      }
      set
      {
        this.editableElement.TextBox.TextBoxItem.HostedControl.Text = value;
      }
    }

    public bool IsPopupVisible
    {
      get
      {
        return PopupManager.Default.ContainsPopup((IPopupControl) this.Popup);
      }
    }

    public override bool RightToLeft
    {
      get
      {
        return base.RightToLeft;
      }
      set
      {
        base.RightToLeft = value;
        this.ListElement.RightToLeft = value;
      }
    }

    public override BindingContext BindingContext
    {
      get
      {
        return base.BindingContext;
      }
      set
      {
        base.BindingContext = value;
        this.ListElement.BindingContext = value;
      }
    }

    public virtual List<BaseAutoComplete> AutoCompleteHelpers
    {
      get
      {
        return this.autoCompleteHelpers;
      }
    }

    public virtual bool ShowImageInEditorArea
    {
      get
      {
        return this.showImageInEditorArea;
      }
      set
      {
        this.showImageInEditorArea = value;
        if (!this.showImageInEditorArea)
        {
          int num = (int) this.EditableElement.SetDefaultValueOverride(LightVisualElement.ImageProperty, (object) null);
        }
        else
        {
          RadListDataItem selectedItem = this.ListElement.SelectedItem;
          if (selectedItem == null)
            return;
          this.SyncEditabaleElementWithSelectedItem(selectedItem);
        }
      }
    }

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
    public RadDropDownListEditableAreaElement EditableElement
    {
      get
      {
        return this.editableElement;
      }
      set
      {
        this.editableElement = value;
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
        return this.ListElement.AutoSizeItems;
      }
      set
      {
        this.ListElement.AutoSizeItems = value;
      }
    }

    private bool IsDropDownShown
    {
      get
      {
        return (bool) this.GetValue(RadDropDownListElement.IsDropDownShownProperty);
      }
    }

    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Indicates whether users can change the selected item by the mouse wheel.")]
    public bool EnableMouseWheel
    {
      get
      {
        return this.enableMouseWheel;
      }
      set
      {
        this.enableMouseWheel = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.UI.Design.RadListControlCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets a collection representing the items contained in this RadDropDownList.")]
    [Category("Data")]
    public RadListDataItemCollection Items
    {
      get
      {
        return (RadListDataItemCollection) this.ListElement.Items;
      }
    }

    [DefaultValue("")]
    [Category("Behavior")]
    [Localizable(true)]
    public string NullText
    {
      get
      {
        return this.editableElement.NullText;
      }
      set
      {
        this.editableElement.NullText = value;
      }
    }

    public void SelectText(int start, int length)
    {
      this.editableElement.Select(start, length);
    }

    public void SelectAllText()
    {
      this.editableElement.SelectAll();
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DefaultValue("")]
    [Description("Gets or sets the text that is selected in the editable portion of the DropDownList.")]
    public string SelectedText
    {
      get
      {
        return this.editableElement.SelectedText;
      }
      set
      {
        this.editableElement.SelectedText = value;
      }
    }

    [Description("Gets or sets the number of characters selected in the editable portion of the combo box.")]
    [DefaultValue(0)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectionLength
    {
      get
      {
        return this.editableElement.SelectionLength;
      }
      set
      {
        this.editableElement.SelectionLength = value;
      }
    }

    [Description("Gets or sets the starting index of text selected in the combo box.")]
    [DefaultValue(0)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectionStart
    {
      get
      {
        return this.editableElement.SelectionStart;
      }
      set
      {
        this.editableElement.SelectionStart = value;
      }
    }

    [Description("Gets or sets the maximum number of characters the user can type or paste into the text box control.")]
    [DefaultValue(0)]
    [Category("Behavior")]
    public int MaxLength
    {
      get
      {
        return this.editableElement.MaxLength;
      }
      set
      {
        this.editableElement.MaxLength = value;
      }
    }

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description("Specifies the mode for the automatic completion feature used in the DropDownList and TextBox controls.")]
    [Category("Behavior")]
    [DefaultValue(AutoCompleteMode.None)]
    public virtual AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.autoCompleteMode;
      }
      set
      {
        if (this.autoCompleteMode == value)
          return;
        this.autoCompleteMode = value;
        foreach (BaseAutoComplete autoCompleteHelper in this.autoCompleteHelpers)
          autoCompleteHelper.Dispose();
        this.autoCompleteHelpers.Clear();
        this.autoCompleteAppend = (AutoCompleteAppendHelper) null;
        this.autoCompleteSuggest = (AutoCompleteSuggestHelper) null;
        if ((this.autoCompleteMode & AutoCompleteMode.Append) != AutoCompleteMode.None)
        {
          this.autoCompleteAppend = this.CreateAutoCompleteAppendHandler();
          this.autoCompleteHelpers.Add((BaseAutoComplete) this.autoCompleteAppend);
        }
        if ((this.autoCompleteMode & AutoCompleteMode.Suggest) == AutoCompleteMode.None)
          return;
        this.autoCompleteSuggest = this.CreateAutoCompleteSuggestHelper();
        this.autoCompleteHelpers.Add((BaseAutoComplete) this.autoCompleteSuggest);
      }
    }

    [DefaultValue(SizingMode.None)]
    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, veritcal or a combination of them.")]
    [Category("Appearance")]
    [Browsable(true)]
    public SizingMode DropDownSizingMode
    {
      get
      {
        return this.sizingMode;
      }
      set
      {
        this.sizingMode = value;
        if (this.Popup == null)
          return;
        this.Popup.SizingMode = this.sizingMode;
      }
    }

    [Description("Gets or sets a value specifying the style of the combo box.")]
    [Category("Appearance")]
    [Browsable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public RadDropDownStyle DropDownStyle
    {
      get
      {
        return (RadDropDownStyle) this.GetValue(RadDropDownListElement.DropDownStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownListElement.DropDownStyleProperty, (object) value);
        this.editableElement.DropDownStyle = value;
      }
    }

    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RadDescription("SelectedValue", typeof (RadListElement))]
    public virtual object SelectedValue
    {
      get
      {
        if (!this.SyncSelectionWithText || this.FindStringExact(this.EditableElementText) > -1)
          return this.ListElement.SelectedValue;
        return (object) null;
      }
      set
      {
        this.ListElement.SelectedValue = value;
      }
    }

    [RadDescription("SelectedIndex", typeof (RadListElement))]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Behavior")]
    public virtual int SelectedIndex
    {
      get
      {
        if (!this.SyncSelectionWithText || this.FindStringExact(this.EditableElementText) > -1)
          return this.ListElement.SelectedIndex;
        return -1;
      }
      set
      {
        this.ListElement.SelectedIndex = value;
      }
    }

    [RadDescription("SelectedItem", typeof (RadListElement))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Bindable(true)]
    public virtual RadListDataItem SelectedItem
    {
      get
      {
        if (!this.SyncSelectionWithText || this.FindStringExact(this.EditableElementText) > -1)
          return this.ListElement.SelectedItem;
        return (RadListDataItem) null;
      }
      set
      {
        this.ListElement.SelectedItem = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

    [DefaultValue(SelectionMode.One)]
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

    public virtual object DataSource
    {
      get
      {
        return this.ListElement.DataSource;
      }
      set
      {
        if (this.autoCompleteSuggest != null)
          this.autoCompleteSuggest.IsItemsDirty = true;
        if (this.ListElement.DataSource == value)
          return;
        if (value != null && value is string && string.IsNullOrEmpty(value.ToString()))
          value = (object) null;
        this.ListElement.DataSource = value;
        if (value == null)
          this.EditableElementText = "";
        if (this.DropDownMinSize == Size.Empty)
          this.Popup.MinimumSize = Size.Empty;
        if (!(this.DropDownMaxSize == Size.Empty))
          return;
        this.Popup.MaximumSize = Size.Empty;
      }
    }

    public virtual string DataMember
    {
      get
      {
        return this.ListElement.DataMember;
      }
      set
      {
        if (this.ListElement.DataMember != value)
          this.ListElement.DataMember = value;
        if (this.DropDownMinSize == Size.Empty)
          this.Popup.MinimumSize = Size.Empty;
        if (!(this.DropDownMaxSize == Size.Empty))
          return;
        this.Popup.MaximumSize = Size.Empty;
      }
    }

    public virtual string DisplayMember
    {
      get
      {
        return this.ListElement.DisplayMember;
      }
      set
      {
        if (this.ListElement.DisplayMember == value)
          return;
        this.ListElement.DisplayMember = value;
        this.NotifyOwner(new PopupEditorNotificationData()
        {
          notificationContext = PopupEditorNotificationData.Context.DisplayMemberChanged
        });
      }
    }

    public virtual string ValueMember
    {
      get
      {
        return this.ListElement.ValueMember;
      }
      set
      {
        if (this.ListElement.ValueMember == value)
          return;
        this.ListElement.ValueMember = value;
        this.NotifyOwner(new PopupEditorNotificationData()
        {
          notificationContext = PopupEditorNotificationData.Context.ValueMemberChanged
        });
      }
    }

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

    public void SelectAll()
    {
      this.ListElement.SelectAll();
    }

    public void SelectRange(int startIndex, int endIndex)
    {
      this.ListElement.SelectRange(startIndex, endIndex);
    }

    public RadDropDownTextBoxElement TextBox
    {
      get
      {
        return this.editableElement.TextBox;
      }
      set
      {
        this.editableElement.TextBox = value;
        this.InvalidateMeasure();
      }
    }

    public RadArrowButtonElement ArrowButton
    {
      get
      {
        return this.arrowButton;
      }
      set
      {
        this.arrowButton = value;
        this.InvalidateMeasure();
      }
    }

    [Description("Gets or sets a value indicating whether string comparisons are case-sensitive.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Browsable(true)]
    public bool CaseSensitive
    {
      get
      {
        return (bool) this.GetValue(RadDropDownListElement.CaseSensitiveProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadDropDownListElement.CaseSensitiveProperty, (object) value);
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Rotate items on double click in the edit box part")]
    [DefaultValue(false)]
    public bool SelectNextOnDoubleClick
    {
      get
      {
        return this.selectNextOnDoubleClick;
      }
      set
      {
        this.selectNextOnDoubleClick = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RadDescription("FormatInfo", typeof (RadDropDownListElement))]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public IFormatProvider FormatInfo
    {
      get
      {
        return this.ListElement.FormatInfo;
      }
      set
      {
        this.ListElement.FormatInfo = value;
      }
    }

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

    [Description("Gets or sets the type of the DropDown animation.")]
    [Browsable(true)]
    [Category("Appearance")]
    public RadEasingType DropDownAnimationEasing
    {
      get
      {
        if (this.PopupForm != null)
          return this.PopupForm.AnimationProperties.EasingType;
        return RadEasingType.Default;
      }
      set
      {
        this.PopupForm.AnimationProperties.EasingType = value;
      }
    }

    [Description("Gets or sets a value indicating whether the RadDropDownList will be animated when displaying.")]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool DropDownAnimationEnabled
    {
      get
      {
        if (this.PopupForm != null)
          return this.PopupForm.AnimationEnabled;
        return false;
      }
      set
      {
        this.PopupForm.AnimationEnabled = value;
      }
    }

    public SortStyle SortStyle
    {
      get
      {
        return this.ListElement.SortStyle;
      }
      set
      {
        this.ListElement.SortStyle = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the number of frames that will be used when the DropDown is being animated.")]
    [Browsable(true)]
    [DefaultValue(1)]
    public int DropDownAnimationFrames
    {
      set
      {
        this.Popup.AnimationFrames = value;
      }
      get
      {
        return this.Popup.AnimationFrames;
      }
    }

    public virtual AutoCompleteSuggestHelper AutoCompleteSuggest
    {
      get
      {
        return this.autoCompleteSuggest;
      }
      set
      {
        if (this.autoCompleteSuggest != null && this.autoCompleteSuggest.DropDownList != null && this.Children.Contains((RadElement) this.autoCompleteSuggest.DropDownList))
          this.Children.Remove((RadElement) this.autoCompleteSuggest.DropDownList);
        this.autoCompleteSuggest = value;
      }
    }

    public virtual AutoCompleteAppendHelper AutoCompleteAppend
    {
      get
      {
        return this.autoCompleteAppend;
      }
      set
      {
        this.autoCompleteAppend = value;
      }
    }

    [Description("Represents the minimum width of the arrow button element")]
    [DefaultValue(0)]
    [Category("Layout")]
    public int ArrowButtonMinWidth
    {
      get
      {
        return this.ArrowButton.MinSize.Width;
      }
      set
      {
        this.ArrowButton.MinSize = new Size(value, this.ArrowButton.MinSize.Height);
      }
    }

    [RadPropertyDefaultValue("NullTextColor", typeof (RadTextBoxItem))]
    public Color NullTextColor
    {
      get
      {
        return this.TextBox.TextBoxItem.NullTextColor;
      }
      set
      {
        this.TextBox.TextBoxItem.NullTextColor = value;
      }
    }

    [Description("Gets or sets the drop down minimum width.")]
    [Category("Appearance")]
    [DefaultValue(0)]
    [Browsable(true)]
    public int DropDownWidth
    {
      get
      {
        return this.DropDownMinSize.Width;
      }
      set
      {
        this.DropDownMinSize = new Size(value, this.DropDownMinSize.Height);
      }
    }

    public virtual void BeginUpdate()
    {
      this.ListElement.BeginUpdate();
      ++this.beginUpdateCount;
    }

    public virtual void EndUpdate()
    {
      if (this.beginUpdateCount > 0)
        --this.beginUpdateCount;
      this.ListElement.EndUpdate();
    }

    public virtual bool IsUpdating()
    {
      return this.beginUpdateCount > 0;
    }

    public virtual IDisposable DeferRefresh()
    {
      this.BeginUpdate();
      return (IDisposable) new RadDropDownListElement.DeferHelper(this);
    }

    public event ValueChangedEventHandler SelectedValueChanged;

    public event Telerik.WinControls.UI.Data.PositionChangedEventHandler SelectedIndexChanged;

    public event Telerik.WinControls.UI.Data.PositionChangingEventHandler SelectedIndexChanging;

    public event ListItemDataBindingEventHandler ItemDataBinding;

    public event ListItemDataBoundEventHandler ItemDataBound;

    public event CreatingVisualListItemEventHandler CreatingVisualItem;

    public event SortStyleChangedEventHandler SortStyleChanged;

    public event VisualListItemFormattingEventHandler VisualItemFormatting;

    public new event KeyEventHandler KeyDown;

    public new event KeyPressEventHandler KeyPress;

    public new event KeyEventHandler KeyUp;

    public new event EventHandler TextChanged;

    public virtual int FindStringExact(string s)
    {
      return this.ListElement.FindStringExact(s, 0);
    }

    public virtual int FindStringExact(string s, int startIndex)
    {
      return this.ListElement.FindStringExact(s, startIndex);
    }

    public int FindString(string s)
    {
      return this.ListElement.FindString(s, 0);
    }

    public int FindString(string s, int startIndex)
    {
      return this.ListElement.FindString(s, startIndex);
    }

    public override bool Focus()
    {
      return this.editableElement.Focus();
    }

    public override void ShowPopup()
    {
      this.SynchronizePopupProperties();
      base.ShowPopup();
      RadListDataItem selectedItem = this.SelectedItem;
      if (!string.IsNullOrEmpty(this.Text) && (selectedItem == null || selectedItem.Text != this.Text))
        this.SelectItemFromText(this.Text);
      this.ListElement.SelectedIndex = this.SelectedIndex;
      this.selectedIndexOnPopupOpen = this.ListElement.SelectedIndex;
      this.ListElement.ScrollToActiveItem();
      this.RemoveSelectionInAutoSuggestPopup();
    }

    public override void ClosePopup(RadPopupCloseReason reason)
    {
      base.ClosePopup(reason);
      this.selectedIndexOnPopupOpen = -1;
    }

    public override void NotifyOwner(PopupEditorNotificationData notificationData)
    {
      base.NotifyOwner(notificationData);
      if (notificationData.notificationContext == PopupEditorNotificationData.Context.TextChanged && !this.SyncSelectionWithText)
        return;
      if (notificationData.notificationContext == PopupEditorNotificationData.Context.VisualItemFormatting)
      {
        this.OnVisualItemFormatting(notificationData.visualItemFormatting.VisualItem);
      }
      else
      {
        if (this.beginUpdateCount != 0)
          return;
        switch (notificationData.notificationContext)
        {
          case PopupEditorNotificationData.Context.SelectedIndexChanged:
            this.OnSelectedIndexChanged((object) this.ListElement, notificationData.positionChangedEventArgs);
            break;
          case PopupEditorNotificationData.Context.SelectedIndexChanging:
            this.OnSelectedIndexChanging((object) this.ListElement, notificationData.positionChangingCancelEventArgs);
            break;
          case PopupEditorNotificationData.Context.SelectedValueChanged:
            this.OnSelectedValueChanged((object) this.ListElement, notificationData.valueChangedEventArgs);
            break;
          case PopupEditorNotificationData.Context.ListItemDataBinding:
            this.OnListItemDataBinding((object) this.ListElement, notificationData.listItemDataBindingEventArgs);
            break;
          case PopupEditorNotificationData.Context.ListItemDataBound:
            this.OnListItemDataBound((object) this.ListElement, notificationData.listItemDataBoundEventArgs);
            break;
          case PopupEditorNotificationData.Context.CreatingVisualItem:
            this.OnVisualElementCreated((object) this.ListElement, notificationData.creatingVisualListItemEventArgs);
            break;
          case PopupEditorNotificationData.Context.KeyPress:
            this.OnKeyPress(this.editableElement, notificationData.keyPressEventArgs);
            break;
          case PopupEditorNotificationData.Context.TextChanged:
            this.OnTextChanged(this.editableElement, new EventArgs());
            break;
          case PopupEditorNotificationData.Context.SortStyleChanged:
            this.OnSortStyleChanged(notificationData.sortStyleChanged.SortStyle);
            break;
          case PopupEditorNotificationData.Context.MouseWheel:
            this.OnMouseWheel(notificationData.mouseEventArgs);
            break;
          case PopupEditorNotificationData.Context.TextBoxDoubleClick:
            this.OnDoubleClick(EventArgs.Empty);
            break;
          case PopupEditorNotificationData.Context.MouseUpOnEditorElement:
            this.TooglePopupState();
            break;
          case PopupEditorNotificationData.Context.DisplayMemberChanged:
          case PopupEditorNotificationData.Context.ValueMemberChanged:
            this.SyncEditorElementWithSelectedItem();
            break;
          case PopupEditorNotificationData.Context.F4Press:
            this.TooglePopupState();
            break;
          case PopupEditorNotificationData.Context.Esc:
            this.ClosePopup(RadPopupCloseReason.Keyboard);
            break;
          case PopupEditorNotificationData.Context.KeyUpKeyDownPress:
            this.HandleOnKeyUpKeyDownPress(notificationData.keyEventArgs);
            break;
          case PopupEditorNotificationData.Context.ItemsChanged:
            this.OnItemsChanged();
            break;
          case PopupEditorNotificationData.Context.ItemsClear:
            this.OnItemsClear();
            break;
          case PopupEditorNotificationData.Context.KeyDown:
            this.ProcessKeyDown((object) this.editableElement.TextBox, notificationData.keyEventArgs);
            break;
          case PopupEditorNotificationData.Context.KeyUp:
            this.ProcessKeyUp((object) this.editableElement.TextBox, notificationData.keyEventArgs);
            break;
          case PopupEditorNotificationData.Context.Clipboard:
            this.ProcessClipboard();
            break;
        }
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallClosePopupCore()
    {
      this.ClosePopupCore();
    }

    protected virtual void ProcessKeyDown(object sender, KeyEventArgs e)
    {
    }

    protected virtual void ProcessKeyUp(object sender, KeyEventArgs e)
    {
    }

    protected virtual void CallTextChanged(object sender, EventArgs e)
    {
      this.isTextChanged = true;
      if (this.TextChanged == null)
        return;
      sender = (object) this;
      if (this.ElementTree != null)
        sender = (object) this.ElementTree.Control;
      this.TextChanged(sender, EventArgs.Empty);
    }

    protected virtual void OnSelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
    {
      this.SyncEditorElementWithSelectedItem();
      if (this.SelectedIndexChanged == null)
        return;
      this.SelectedIndexChanged((object) this, e);
    }

    protected internal virtual bool CanClosePopUp(RadPopupCloseReason reason, MouseButtons buttons)
    {
      return true;
    }

    protected bool OnSelectedIndexChanging(object sender, PositionChangingCancelEventArgs e)
    {
      if (this.SelectedIndexChanging != null)
        this.SelectedIndexChanging((object) this, e);
      if (e.Cancel)
        return true;
      if (e.Position == -1)
      {
        this.isTextChanged = true;
        if (this.SelectItemFromText(this.EditableElementText, false, (object) null) == -1)
          return false;
        this.EditableElementText = "";
      }
      return false;
    }

    protected virtual void OnSelectedValueChanged(object sender, Telerik.WinControls.UI.Data.ValueChangedEventArgs e)
    {
      this.SyncVisualProperties(this.SelectedItem);
      if (this.SelectedValueChanged == null)
        return;
      this.SelectedValueChanged((object) this, e);
    }

    protected virtual void OnListItemDataBinding(object sender, ListItemDataBindingEventArgs args)
    {
      if (this.ItemDataBinding == null)
        return;
      this.ItemDataBinding(sender, args);
    }

    protected virtual void OnListItemDataBound(object sender, ListItemDataBoundEventArgs args)
    {
      if (this.ItemDataBound == null)
        return;
      this.ItemDataBound(sender, args);
    }

    protected virtual void OnVisualElementCreated(
      object sender,
      CreatingVisualListItemEventArgs args)
    {
      if (this.CreatingVisualItem == null)
        return;
      this.CreatingVisualItem(sender, args);
    }

    private void ProcessClipboard()
    {
      if (this.DropDownStyle == RadDropDownStyle.DropDownList || this.ListElement.ReadOnly)
        return;
      if (this.autoCompleteSuggest != null)
        this.autoCompleteSuggest.HandleAutoSuggest(this.Text);
      if (this.autoCompleteAppend == null)
        return;
      if (!this.autoCompleteAppend.LimitToList)
      {
        this.autoCompleteAppend.SearchForStringInList(this.Text);
      }
      else
      {
        if (this.autoCompleteAppend.FindShortestString(this.editableElement.TextBox.Text) != -1)
          return;
        int selectionStart = this.editableElement.TextBox.TextBoxItem.SelectionStart;
        int selectionLength = this.editableElement.TextBox.TextBoxItem.SelectionLength;
        this.Text = this.editableElement.OldText;
        this.editableElement.OldText = this.Text;
        this.editableElement.TextBox.TextBoxItem.SelectionStart = selectionStart;
        this.editableElement.TextBox.TextBoxItem.SelectionLength = selectionLength;
      }
    }

    protected void OnKeyPress(RadDropDownListEditableAreaElement sender, KeyPressEventArgs e)
    {
      this.HandleAutoComplete(e);
      this.HandleEnter(e);
      this.ProccesListFastNavigationInDropDownListMode(e.KeyChar);
    }

    public new virtual void OnKeyPress(KeyPressEventArgs e)
    {
      KeyPressEventHandler keyPress = this.KeyPress;
      if (keyPress == null)
        return;
      keyPress((object) this, e);
    }

    public new virtual void OnKeyDown(KeyEventArgs e)
    {
      KeyEventHandler keyDown = this.KeyDown;
      if (keyDown == null)
        return;
      keyDown((object) this, e);
    }

    public new virtual void OnKeyUp(KeyEventArgs e)
    {
      KeyEventHandler keyUp = this.KeyUp;
      if (keyUp == null)
        return;
      keyUp((object) this, e);
    }

    protected void OnTextChanged(RadDropDownListEditableAreaElement sender, EventArgs args)
    {
      if (this.EditableElementText == "" && this.FindStringExact("") < 0 && !this.skipSelectionClear)
        this.SelectedIndex = -1;
      if (!this.IsPopupOpen)
        return;
      this.ScrollToItemFromText(this.EditableElementText);
    }

    protected virtual void OnSortStyleChanged(SortStyle sortStyle)
    {
      if (this.SortStyleChanged == null)
        return;
      this.SortStyleChanged((object) this, new SortStyleChangedEventArgs(sortStyle));
    }

    protected internal virtual void OnVisualItemFormatting(RadListVisualItem item)
    {
      if (this.VisualItemFormatting == null)
        return;
      this.VisualItemFormatting((object) this, new VisualItemFormattingEventArgs(item));
    }

    protected virtual void OnItemsClear()
    {
      this.EditableElementText = "";
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.ListElement.SuspendSelectionEvents = true;
      string text = this.TextBox.Text;
      this.TextBox.SuspendPropertyNotifications();
      this.TextBox.TextBoxItem.SuspendPropertyNotifications();
      this.TextBox.TextBoxItem.HostedControl.Text = string.Empty;
      this.TextBox.TextBoxItem.HostedControl.Text = text;
      this.TextBox.ResumePropertyNotifications();
      this.TextBox.TextBoxItem.ResumePropertyNotifications();
      this.ListElement.SuspendSelectionEvents = false;
      this.NotifyParentOnMouseInput = true;
      if (!SystemInformation.TerminalServerSession)
        return;
      this.DropDownAnimationEnabled = SystemInformation.IsComboBoxAnimationEnabled;
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.OnMouseWheelCore(e);
    }

    protected internal virtual void OnMouseWheelCore(MouseEventArgs e)
    {
      if (!this.enableMouseWheel)
        return;
      if (!this.IsPopupVisible)
        this.HandleSelectNextOrPrev(e.Delta < 0, false);
      if (!(e is HandledMouseEventArgs))
        return;
      (e as HandledMouseEventArgs).Handled = true;
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (!this.selectNextOnDoubleClick)
        return;
      this.HandleSelectNextOrPrev(true, true);
    }

    protected virtual void OnItemsChanged()
    {
      if (this.SelectedItem != null)
        this.EditableElementText = this.SelectedItem.Text;
      else if (this.oldSelectedIndex != -1)
        this.EditableElementText = "";
      this.isTextChanged = false;
      this.itemsChanged = true;
    }

    protected override void OnPopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      int num1 = (int) this.arrowButton.SetValue(RadDropDownListElement.IsDropDownShownProperty, (object) false);
      int num2 = (int) this.SetValue(RadDropDownListElement.IsDropDownShownProperty, (object) false);
    }

    protected override void OnPopupOpened(object sender, EventArgs e)
    {
      int num1 = (int) this.arrowButton.SetValue(RadDropDownListElement.IsDropDownShownProperty, (object) true);
      int num2 = (int) this.SetValue(RadDropDownListElement.IsDropDownShownProperty, (object) true);
      if (!this.AutoSizeItems && !this.itemsChanged)
        return;
      this.itemsChanged = false;
      this.ListElement.Scroller.UpdateScrollRange();
      this.ListElement.ViewElement.UpdateItems();
      this.ListElement.InvalidateMeasure(true);
      this.ListElement.InvalidateArrange(true);
      this.ListElement.UpdateLayout();
    }

    protected override void OnAutoCompeleteDataSourceChanged()
    {
      this.SyncAutoCompleteSuggestHelperDataSource();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadObject.BindingContextProperty)
        this.SetDropDownBindingContext();
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      this.ListElement.RightToLeft = this.RightToLeft;
      bool newValue = (bool) e.NewValue;
      if (this.Shape != null)
        this.Shape.IsRightToLeft = newValue;
      if (this.ArrowButton == null || this.ArrowButton.Shape == null)
        return;
      this.ArrowButton.Shape.IsRightToLeft = newValue;
    }

    protected override void listElement_DataItemPropertyChanged(
      object sender,
      RadPropertyChangedEventArgs e)
    {
      if (sender != this.ListElement.ActiveItem)
        return;
      this.SyncVisualProperties(this.ListElement.ActiveItem);
    }

    private void fillPrimitive_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != VisualElement.BackColorProperty)
        return;
      int num = (int) this.editableElement.SetDefaultValueOverride(VisualElement.BackColorProperty, e.NewValue);
    }

    private void arrowButton_Click(object sender, EventArgs e)
    {
      this.EditableElement.Entering = false;
      this.EditableElement.Focus();
      this.TooglePopupState();
    }

    protected virtual AutoCompleteAppendHelper CreateAutoCompleteAppendHandler()
    {
      return new AutoCompleteAppendHelper(this);
    }

    protected virtual AutoCompleteSuggestHelper CreateAutoCompleteSuggestHelper()
    {
      return new AutoCompleteSuggestHelper(this);
    }

    protected virtual void ClosePopupCore()
    {
      if (this.ListElement.IsFilterActive)
        this.SyncEditorElementWithActiveItem();
      this.Focus();
    }

    protected void HandleOnKeyUpKeyDownPress(KeyEventArgs keyEventArgs)
    {
      if (this.ListElement.ReadOnly)
        return;
      if (this.autoCompleteSuggest != null && this.autoCompleteSuggest.DropDownList.IsPopupOpen)
      {
        this.autoCompleteSuggest.HandleSelectNextOrPrev(keyEventArgs.KeyCode == Keys.Down);
        keyEventArgs.Handled = true;
      }
      else
      {
        this.ListElement.ProcessKeyboardSelection(keyEventArgs.KeyCode);
        keyEventArgs.Handled = true;
      }
    }

    protected virtual void HandleSelectNextOrPrev(bool next, bool startFromBeginningIfEndReached)
    {
      if (this.ListElement.ReadOnly)
        return;
      if (this.autoCompleteSuggest != null && this.autoCompleteSuggest.DropDownList.IsPopupOpen)
      {
        this.autoCompleteSuggest.HandleSelectNextOrPrev(next);
      }
      else
      {
        int selectedIndex = this.ListElement.SelectedIndex;
        int count = this.ListElement.Items.Count;
        int index = startFromBeginningIfEndReached ? this.ClampSelectedIndexWithReverse(next, selectedIndex, count) : this.ClampSelectedIndex(next, selectedIndex, count);
        if (index <= -1 || index >= count)
          return;
        this.SelectedItem = this.Items[index];
        this.SyncEditorElementWithSelectedItem();
      }
    }

    protected virtual void SyncVisualProperties(RadListDataItem listItem)
    {
      if (listItem == null || !listItem.Selected)
      {
        int num1 = (int) this.EditableElement.ResetValue(LightVisualElement.ImageProperty, ValueResetFlags.DefaultValueOverride);
        int num2 = (int) this.EditableElement.ResetValue(LightVisualElement.ImageAlignmentProperty, ValueResetFlags.DefaultValueOverride);
        int num3 = (int) this.EditableElement.ResetValue(LightVisualElement.TextImageRelationProperty, ValueResetFlags.DefaultValueOverride);
      }
      else
      {
        if (this.ListElement.SuspendSelectionEvents)
          return;
        this.EditableElementText = listItem.Text;
        this.editableElement.Text = listItem.Text;
        if (this.DropDownStyle == RadDropDownStyle.DropDown)
          return;
        if (!this.showImageInEditorArea)
        {
          int num = (int) this.EditableElement.SetDefaultValueOverride(LightVisualElement.ImageProperty, (object) null);
        }
        else
          this.SyncEditabaleElementWithSelectedItem(listItem);
      }
    }

    private void SyncEditabaleElementWithSelectedItem(RadListDataItem listItem)
    {
      if (this.EditableElement.GetValueSource(LightVisualElement.ImageProperty) < ValueSource.Local)
      {
        int num1 = (int) this.EditableElement.SetDefaultValueOverride(LightVisualElement.ImageProperty, (object) listItem.Image);
      }
      if (this.EditableElement.GetValueSource(LightVisualElement.ImageAlignmentProperty) < ValueSource.Local)
      {
        int num2 = (int) this.EditableElement.SetDefaultValueOverride(LightVisualElement.ImageAlignmentProperty, (object) listItem.ImageAlignment);
      }
      if (this.EditableElement.GetValueSource(LightVisualElement.TextAlignmentProperty) < ValueSource.Local)
      {
        int num3 = (int) this.EditableElement.SetDefaultValueOverride(LightVisualElement.TextAlignmentProperty, (object) listItem.TextAlignment);
      }
      if (this.EditableElement.GetValueSource(LightVisualElement.TextAlignmentProperty) < ValueSource.Local)
      {
        int num4 = (int) this.EditableElement.SetDefaultValueOverride(LightVisualElement.TextAlignmentProperty, (object) listItem.TextAlignment);
      }
      if (this.EditableElement.GetValueSource(LightVisualElement.TextImageRelationProperty) >= ValueSource.Local)
        return;
      int num5 = (int) this.EditableElement.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) listItem.TextImageRelation);
    }

    private void RemoveSelectionInAutoSuggestPopup()
    {
      if (!this.isSuggestMode)
        return;
      this.BeginUpdate();
      this.SelectedIndex = -1;
      this.EndUpdate();
    }

    private void SyncAutoCompleteSuggestHelperDataSource()
    {
      if (this.autoCompleteSuggest == null)
        return;
      this.autoCompleteSuggest.AutoCompleteDataSource = this.AutoCompleteDataSource;
      this.autoCompleteSuggest.AutoCompleteDisplayMember = this.AutoCompleteDisplayMember;
      this.autoCompleteSuggest.AutoCompleteValueMember = this.AutoCompleteValueMember;
      this.autoCompleteSuggest.IsItemsDirty = true;
    }

    internal int SelectItemFromText(
      string text,
      bool syncEditorElementWithSelectedItem,
      object value = null)
    {
      StringComparison comparisonType = this.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
      if (this.ListElement.SelectedIndex > -1 && text.Equals(this.ListElement.SelectedItem.CachedText, comparisonType) && string.Concat(value) == string.Concat(this.ListElement.SelectedItem.Value))
      {
        if (syncEditorElementWithSelectedItem)
          this.SyncEditorElementWithSelectedItem();
        return this.ListElement.SelectedIndex;
      }
      if (!this.isTextChanged)
        return -1;
      int count = this.Items.Count;
      for (int index = 0; index < count; ++index)
      {
        if (text.Equals(this.Items[index].Text, comparisonType) && (value == null || string.Concat(value) == string.Concat(this.Items[index].Value)))
        {
          this.ListElement.SelectedIndex = index;
          if (syncEditorElementWithSelectedItem)
            this.SyncEditorElementWithSelectedItem();
          return index;
        }
      }
      if (syncEditorElementWithSelectedItem)
      {
        this.BeginUpdate();
        this.ListElement.SelectedIndex = -1;
        this.EndUpdate();
      }
      return -1;
    }

    protected virtual void ScrollToItemFromText(string text)
    {
      StringComparison stringComparison = this.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
      if (this.ScrollToActiveItem(text, stringComparison) || this.ScrollToItemText(text, stringComparison))
        return;
      this.ScrollToFirstItem();
    }

    private bool ScrollToItemText(string text, StringComparison stringComparison)
    {
      int count = this.Items.Count;
      for (int index = 0; index < count; ++index)
      {
        if (text.Equals(this.ListElement.Items[index].Text, stringComparison))
        {
          this.ListElement.ScrollToItem(this.ListElement.Items[index]);
          return true;
        }
      }
      return false;
    }

    private bool ScrollToActiveItem(string text, StringComparison stringComparison)
    {
      if (this.ListElement.SelectedIndex <= -1 || !text.Equals(this.ListElement.SelectedItem.CachedText, stringComparison))
        return false;
      this.ListElement.ScrollToActiveItem();
      return true;
    }

    private void ScrollToFirstItem()
    {
      if (this.ListElement.Items.Count <= 0)
        return;
      this.ListElement.ScrollToItem(this.ListElement.Items[0]);
    }

    private void HandleEnter(KeyPressEventArgs e)
    {
      if (e.KeyChar != '\r')
        return;
      this.HandleEnterCore();
    }

    protected virtual void HandleEnterCore()
    {
      if (this.DropDownStyle == RadDropDownStyle.DropDownList && !this.isTextChanged)
        return;
      this.SelectAllText();
      if (this.SelectedItem != null && this.SelectedItem.Text == this.EditableElementText)
        return;
      this.SelectItemFromText(this.EditableElementText, false, (object) null);
    }

    private void HandleAutoComplete(KeyPressEventArgs e)
    {
      if (this.DropDownStyle == RadDropDownStyle.DropDownList || this.ListElement.ReadOnly)
        return;
      if (this.autoCompleteSuggest != null)
        this.autoCompleteSuggest.AutoComplete(e);
      if (this.autoCompleteAppend == null)
        return;
      this.autoCompleteAppend.AutoComplete(e);
    }

    private void ProccesListFastNavigationInDropDownListMode(char pressedChar)
    {
      if (this.DropDownStyle != RadDropDownStyle.DropDownList || this.ListElement.ReadOnly)
        return;
      this.ListElement.ProcessKeyboardSearch(pressedChar);
    }

    internal virtual void SyncEditorElementWithSelectedItem()
    {
      RadListDataItem selectedItem = this.ListElement.SelectedItem;
      this.oldSelectedIndex = this.ListElement.SelectedIndex;
      if (selectedItem == null)
      {
        if (this.Items.Count != 0 || string.IsNullOrEmpty(this.EditableElementText))
          return;
        this.EditableElementText = string.Empty;
      }
      else
      {
        if (selectedItem.Text == "")
          this.skipSelectionClear = true;
        this.SyncVisualProperties(selectedItem);
        this.skipSelectionClear = false;
        if (this.isSuggestMode || this.EditableElement.SelectionLength == this.EditableElementText.Length)
          return;
        this.SelectAllText();
      }
    }

    internal virtual void SyncEditorElementWithActiveItem()
    {
      RadListDataItem activeItem = this.ListElement.ActiveItem;
      if (activeItem == null)
      {
        this.ListElement.SelectedIndex = -1;
      }
      else
      {
        this.ListElement.SelectedItem = this.ListElement.ActiveItem;
        this.EditableElementText = activeItem.Text;
        this.SelectAllText();
      }
    }

    internal int ClampSelectedIndex(bool down, int selectedIndex, int itemsCount)
    {
      bool flag = false;
      int num = selectedIndex;
      if (down)
      {
        while (selectedIndex < itemsCount - 1)
        {
          ++selectedIndex;
          if (this.ListElement.Items.Count > selectedIndex && this.ListElement.Items[selectedIndex].Enabled)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          selectedIndex = num;
      }
      else
      {
        while (selectedIndex > 0)
        {
          --selectedIndex;
          if (this.ListElement.Items.Count > selectedIndex && this.ListElement.Items[selectedIndex].Enabled)
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          selectedIndex = num;
      }
      return selectedIndex;
    }

    internal int ClampSelectedIndexWithReverse(bool down, int selectedIndex, int itemsCount)
    {
      if (down)
      {
        ++selectedIndex;
        if (selectedIndex >= itemsCount)
          selectedIndex = 0;
      }
      else
      {
        --selectedIndex;
        if (selectedIndex < 0)
          selectedIndex = itemsCount;
      }
      return selectedIndex;
    }

    protected internal virtual int SelectItemFromText(string text)
    {
      return this.SelectItemFromText(text, false, (object) null);
    }

    protected internal virtual void EnterPressedOrLeaveControl()
    {
      this.HandleEnterCore();
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnBubbleEvent(sender, args);
      if (!(args.RoutedEvent.EventName == "KeyPressEvent") || !(sender is StackLayoutElement))
        return;
      this.NotifyOwner(new PopupEditorNotificationData((KeyPressEventArgs) args.OriginalEventArgs)
      {
        notificationContext = PopupEditorNotificationData.Context.KeyPress
      });
    }

    private void SynchronizePopupProperties()
    {
      if (this.Popup.Font != this.Font)
      {
        int num1 = (int) this.Popup.ElementTree.RootElement.SetDefaultValueOverride(VisualElement.FontProperty, (object) this.Font);
      }
      if (!(this.Popup.ForeColor != this.ForeColor))
        return;
      int num2 = (int) this.Popup.ElementTree.RootElement.SetDefaultValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      if ((double) clientRectangle.Width < 2.0)
        return SizeF.Empty;
      SizeF sizeF = base.MeasureOverride(clientRectangle.Size);
      sizeF.Width = Math.Min(sizeF.Width, availableSize.Width);
      sizeF.Height = Math.Min(sizeF.Height, availableSize.Height);
      return sizeF;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      bool animationEnabled = this.DropDownAnimationEnabled;
      this.DropDownAnimationEnabled = false;
      this.TooglePopupState();
      this.TooglePopupState();
      this.DropDownAnimationEnabled = animationEnabled;
    }

    private class DeferHelper : IDisposable
    {
      private RadDropDownListElement listElement;

      public DeferHelper(RadDropDownListElement listElement)
      {
        this.listElement = listElement;
      }

      public void Dispose()
      {
        if (this.listElement == null)
          return;
        this.listElement.EndUpdate();
        this.listElement = (RadDropDownListElement) null;
      }
    }
  }
}
