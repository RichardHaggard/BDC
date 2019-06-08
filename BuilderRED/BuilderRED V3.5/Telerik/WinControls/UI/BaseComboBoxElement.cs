// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseComboBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
  [ComVisible(false)]
  public abstract class BaseComboBoxElement : PopupEditorBaseElement
  {
    internal bool PopupDisplayedForTheFirstTime = true;
    private bool scrollOnMouseWheel = true;
    private int dropDownWidth = -1;
    private int dropDownHeight = 106;
    private int maxDropDownItems = 8;
    protected internal string LastTypedText = string.Empty;
    private Size dropDownMinSize = Size.Empty;
    private Size dropDownMaxSize = Size.Empty;
    private Timer autoFilterTimer = new Timer();
    private LightVisualElement textboxContentElement = new LightVisualElement();
    private string oldTextValue = string.Empty;
    internal static RadProperty CaseSensitiveProperty = RadProperty.Register(nameof (CaseSensitive), typeof (bool), typeof (BaseComboBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    internal static RadProperty SortedProperty = RadProperty.Register(nameof (Sorted), typeof (SortStyle), typeof (BaseComboBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SortStyle.None, ElementPropertyOptions.None));
    private static RadProperty DropDownStyleProperty = RadProperty.Register(nameof (DropDownStyle), typeof (RadDropDownStyle), typeof (BaseComboBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadDropDownStyle.DropDown, ElementPropertyOptions.None));
    private static RadProperty DropDownAnimationEnabledProperty = RadProperty.Register(nameof (DropDownAnimationEnabled), typeof (bool), typeof (BaseComboBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.None));
    private static RadProperty DropDownAnimationEasingProperty = RadProperty.Register(nameof (DropDownAnimationEasing), typeof (RadEasingType), typeof (BaseComboBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RadEasingType.Linear, ElementPropertyOptions.None));
    private static RadProperty DropDownAnimationFramesProperty = RadProperty.Register(nameof (DropDownAnimationFrames), typeof (int), typeof (BaseComboBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.None));
    public static RadProperty IsDropDownShownProperty = RadProperty.Register(nameof (IsDropDownShown), typeof (bool), typeof (BaseComboBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.None));
    private static readonly object CaseSensitiveChangedEventKey = new object();
    private static readonly object DropDownStyleChangedEventKey = new object();
    private static readonly object KeyDownEventKey = new object();
    private static readonly object KeyPressEventKey = new object();
    private static readonly object KeyUpEventKey = new object();
    private static readonly object SelectedIndexChangedEventKey = new object();
    private static readonly object SelectedValueChangedEventKey = new object();
    private static readonly object SortedChangedEventKey = new object();
    private const int DefaultDropDownWidth = -1;
    private const int DefaultDropDownHeight = 106;
    private const int DefaultDropDownItems = 8;
    protected RadTextBoxItem textBox;
    private RadTextBoxElement textBoxPanel;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private RadArrowButtonElement arrowButton;
    protected ComboBoxEditorLayoutPanel layoutPanel;
    private bool keyboardCommandIssued;
    internal int OnTextBoxCaptureChanged;
    private AutoCompleteMode autoCompleteMode;
    private bool dblClickRotate;
    private Keys lastPressedKey;
    protected char lastPressedChar;
    internal bool ListAutoCompleteIssued;
    internal string lastTextChangedValue;
    private bool limitToList;
    private int autoFilterDelay;

    public BaseComboBoxElement()
    {
      this.AutoSizeMode = RadAutoSizeMode.FitToAvailableSize;
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      if (this.autoFilterTimer != null)
      {
        this.autoFilterTimer.Stop();
        this.autoFilterTimer.Tick -= new EventHandler(this.Timer_Tick);
        this.autoFilterTimer = (Timer) null;
      }
      base.DisposeManagedResources();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public ComboBoxEditorLayoutPanel ComboboxLayoutPanel
    {
      get
      {
        return this.layoutPanel;
      }
    }

    [DefaultValue(false)]
    public bool LimitToList
    {
      get
      {
        return this.limitToList;
      }
      set
      {
        this.limitToList = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool KeyboardCommandIssued
    {
      get
      {
        return this.keyboardCommandIssued;
      }
      set
      {
        this.keyboardCommandIssued = value;
      }
    }

    [DefaultValue(0)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public int AutoFilterDelay
    {
      get
      {
        return this.autoFilterDelay;
      }
      set
      {
        this.autoFilterDelay = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public bool IsDropDownShown
    {
      get
      {
        return (bool) this.GetValue(BaseComboBoxElement.IsDropDownShownProperty);
      }
      internal set
      {
        int num = (int) this.SetValue(BaseComboBoxElement.IsDropDownShownProperty, (object) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public RadArrowButtonElement ArrowButton
    {
      get
      {
        return this.arrowButton;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public FillPrimitive ComboBoxFill
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public BorderPrimitive ComboBoxBorder
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    public int ArrowButtonMinWidth
    {
      get
      {
        return this.arrowButton.MinSize.Width;
      }
      set
      {
        this.arrowButton.MinSize = new Size(value, this.arrowButton.MinSize.Height);
      }
    }

    [Category("Behavior")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description("Specifies the mode for the automatic completion feature used in the ComboBox and TextBox controls.")]
    [Browsable(true)]
    [DefaultValue(AutoCompleteMode.None)]
    public virtual AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.autoCompleteMode;
      }
      set
      {
        this.autoCompleteMode = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether string comparisons are case-sensitive.")]
    [Category("Behavior")]
    [Browsable(true)]
    public bool CaseSensitive
    {
      get
      {
        return (bool) this.GetValue(BaseComboBoxElement.CaseSensitiveProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseComboBoxElement.CaseSensitiveProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Rotates selected items on double clicking inside the text edit control.")]
    public bool DblClickRotate
    {
      get
      {
        return this.dblClickRotate;
      }
      set
      {
        if (value == this.dblClickRotate)
          return;
        this.dblClickRotate = value;
        this.OnNotifyPropertyChanged(nameof (DblClickRotate));
      }
    }

    [Description("Gets or sets a boolean value determining whether the user can scroll throught the items when the popup is closed by using the mouse wheel.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ScrollOnMouseWheel
    {
      get
      {
        return this.scrollOnMouseWheel;
      }
      set
      {
        if (value == this.scrollOnMouseWheel)
          return;
        this.scrollOnMouseWheel = value;
        this.OnNotifyPropertyChanged(nameof (ScrollOnMouseWheel));
      }
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [DefaultValue(106)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the height in pixels of the drop-down portion of the ComboBox.")]
    public int DropDownHeight
    {
      get
      {
        return this.dropDownHeight;
      }
      set
      {
        if (this.dropDownHeight == value)
          return;
        this.dropDownHeight = value;
        this.OnNotifyPropertyChanged(nameof (DropDownHeight));
      }
    }

    [Description("Gets or sets a value specifying the style of the combo box.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    [Category("Appearance")]
    [RadPropertyDefaultValue("DropDownStyle", typeof (BaseComboBoxElement))]
    public RadDropDownStyle DropDownStyle
    {
      get
      {
        return (RadDropDownStyle) this.GetValue(BaseComboBoxElement.DropDownStyleProperty);
      }
      set
      {
        if (value == this.DropDownStyle)
          return;
        int num = (int) this.SetValue(BaseComboBoxElement.DropDownStyleProperty, (object) value);
        this.InsertTextbox();
      }
    }

    private void InsertTextbox()
    {
      if (this.DropDownStyle == RadDropDownStyle.DropDown)
      {
        if (this.layoutPanel.Children.Contains((RadElement) this.textBoxPanel))
          this.layoutPanel.Children.Remove((RadElement) this.textBoxPanel);
        this.layoutPanel.Content = (RadElement) this.textBoxPanel;
        this.textBoxPanel.Visibility = ElementVisibility.Visible;
      }
      else
      {
        if (this.layoutPanel.Children.Contains((RadElement) this.textboxContentElement))
          this.layoutPanel.Children.Remove((RadElement) this.textboxContentElement);
        LightVisualElement textboxContentElement1 = this.textboxContentElement;
        LightVisualElement textboxContentElement2 = this.textboxContentElement;
        int width = this.textBoxPanel.Size.Width;
        int height = this.textboxContentElement.MaxSize.Height;
        Size size1;
        Size size2 = size1 = new Size(width, height);
        textboxContentElement2.MaxSize = size1;
        Size size3 = size2;
        textboxContentElement1.MinSize = size3;
        this.layoutPanel.Content = (RadElement) this.textboxContentElement;
        this.textBoxPanel.Visibility = ElementVisibility.Collapsed;
        this.layoutPanel.Children.Add((RadElement) this.textBoxPanel);
      }
    }

    public bool IsWritable
    {
      get
      {
        return this.DropDownStyle == RadDropDownStyle.DropDown;
      }
    }

    [Description("Gets or sets the width of the of the drop-down portion of a combo box.")]
    [DefaultValue(-1)]
    [Category("Behavior")]
    [Browsable(true)]
    public int DropDownWidth
    {
      get
      {
        return this.dropDownWidth;
      }
      set
      {
        if (this.dropDownWidth == value)
          return;
        this.dropDownWidth = value;
        this.OnNotifyPropertyChanged(nameof (DropDownWidth));
        this.DropDownMinSize = new Size(value, this.DropDownMinSize.Height);
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the control should show or not partial items.")]
    public abstract bool IntegralHeight { get; set; }

    protected abstract bool IndexChanging { get; set; }

    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Description("Gets a collection representing the items contained in this ComboBox.")]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public abstract RadItemCollection Items { get; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets a value indicating whether the combo box is displaying its drop-down portion.")]
    public bool IsDroppedDown
    {
      get
      {
        return this.IsDropDownShown;
      }
    }

    [Category("Behavior")]
    [DefaultValue(8)]
    [Description("Gets or sets the maximum number of items to be shown in the drop-down portion of the ComboBox. ")]
    public int MaxDropDownItems
    {
      get
      {
        return this.maxDropDownItems;
      }
      set
      {
        this.maxDropDownItems = value;
      }
    }

    [Category("Behavior")]
    [RadDefaultValue("MaxLength", typeof (RadTextBoxItem))]
    [Description("Gets or sets the maximum number of characters the user can type or paste into the text box control.")]
    public int MaxLength
    {
      get
      {
        return this.textBox.MaxLength;
      }
      set
      {
        this.textBox.MaxLength = value;
      }
    }

    [Localizable(true)]
    [RadDescription("NullText", typeof (RadTextBoxItem))]
    [Category("Behavior")]
    [RadDefaultValue("NullText", typeof (RadTextBoxItem))]
    public string NullText
    {
      get
      {
        return this.textBox.NullText;
      }
      set
      {
        this.textBox.NullText = value;
      }
    }

    [Description("Gets or sets the currently selected item.")]
    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public abstract object SelectedItem { get; set; }

    [Description("Gets or sets the index specifying the currently selected item.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(true)]
    public abstract int SelectedIndex { get; set; }

    [Browsable(false)]
    [Description("Gets or sets the text that is selected in the editable portion of the ComboBox.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SelectedText
    {
      get
      {
        if (this.DropDownStyle != RadDropDownStyle.DropDownList)
          return this.textBox.SelectedText;
        return string.Empty;
      }
      set
      {
        if (this.DropDownStyle == RadDropDownStyle.DropDownList)
          return;
        this.textBox.SelectedText = value;
      }
    }

    [Description("Gets or sets the number of characters selected in the editable portion of the combo box.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectionLength
    {
      get
      {
        return this.textBox.SelectionLength;
      }
      set
      {
        this.textBox.SelectionLength = value;
      }
    }

    [Description("Gets or sets the starting index of text selected in the combo box.")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int SelectionStart
    {
      get
      {
        return this.textBox.SelectionStart;
      }
      set
      {
        this.textBox.SelectionStart = value;
      }
    }

    [Description("Gets or sets a value indicating the sort style the of items in the combo box.")]
    [Category("Behavior")]
    [DefaultValue(SortStyle.None)]
    public SortStyle Sorted
    {
      get
      {
        return (SortStyle) this.GetValue(BaseComboBoxElement.SortedProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseComboBoxElement.SortedProperty, (object) value);
      }
    }

    [DefaultValue("")]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
        RadTextBoxItem textBoxItem = this.TextBoxElement.TextBoxItem;
        if (!(textBoxItem.Text != value))
          return;
        textBoxItem.Text = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether the ComboBox DropDown will be enabled when it shows.")]
    [Browsable(true)]
    [RadPropertyDefaultValue("DropDownAnimationEnabled", typeof (BaseComboBoxElement))]
    public bool DropDownAnimationEnabled
    {
      get
      {
        return (bool) this.GetValue(BaseComboBoxElement.DropDownAnimationEnabledProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseComboBoxElement.DropDownAnimationEnabledProperty, (object) value);
      }
    }

    [Category("Appearance")]
    [RadPropertyDefaultValue("DropDownAnimationEasing", typeof (BaseComboBoxElement))]
    [Description("Gets or sets the type of the DropDown animation.")]
    [Browsable(true)]
    public RadEasingType DropDownAnimationEasing
    {
      get
      {
        return (RadEasingType) this.GetValue(BaseComboBoxElement.DropDownAnimationEasingProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseComboBoxElement.DropDownAnimationEasingProperty, (object) value);
      }
    }

    [RadPropertyDefaultValue("DropDownAnimationFrames", typeof (BaseComboBoxElement))]
    [Description("Gets or sets the number of frames that will be used when the DropDown is being animated.")]
    [Category("Appearance")]
    [Browsable(true)]
    public int DropDownAnimationFrames
    {
      get
      {
        return (int) this.GetValue(BaseComboBoxElement.DropDownAnimationFramesProperty);
      }
      set
      {
        int num = (int) this.SetValue(BaseComboBoxElement.DropDownAnimationFramesProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadTextBoxElement TextBoxElement
    {
      get
      {
        return this.textBoxPanel;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(SizingMode.None)]
    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, veritcal or a combination of them.")]
    public abstract SizingMode DropDownSizingMode { get; set; }

    [DefaultValue(typeof (Size), "0,0")]
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the drop down minimum size.")]
    public Size DropDownMinSize
    {
      get
      {
        return this.dropDownMinSize;
      }
      set
      {
        if (!(this.dropDownMinSize != value))
          return;
        this.dropDownMinSize = value;
        if (!(this.dropDownMaxSize != Size.Empty))
          return;
        if (this.dropDownMinSize.Width > this.dropDownMaxSize.Width)
          this.dropDownMaxSize.Width = this.dropDownMinSize.Width;
        if (this.dropDownMinSize.Height <= this.dropDownMaxSize.Height)
          return;
        this.dropDownMaxSize.Height = this.dropDownMinSize.Height;
      }
    }

    [Category("Appearance")]
    [DefaultValue(typeof (Size), "0,0")]
    [Description("Gets or sets the drop down maximum size.")]
    [Browsable(true)]
    public Size DropDownMaxSize
    {
      get
      {
        return this.dropDownMaxSize;
      }
      set
      {
        if (!(this.dropDownMaxSize != value))
          return;
        this.dropDownMaxSize = value;
        if (!(this.dropDownMinSize != Size.Empty))
          return;
        if (this.dropDownMaxSize.Width < this.dropDownMinSize.Width)
          this.dropDownMinSize.Width = this.dropDownMaxSize.Width;
        if (this.dropDownMaxSize.Height >= this.dropDownMinSize.Height)
          return;
        this.dropDownMinSize.Height = this.dropDownMaxSize.Height;
      }
    }

    [Description("Gets or sets a value indicating whether RadScrollViewer uses UI virtualization.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    [Browsable(true)]
    public abstract bool Virtualized { get; set; }

    protected override void CreateChildElements()
    {
      this.arrowButton = new RadArrowButtonElement();
      this.arrowButton.Arrow.AutoSize = true;
      this.arrowButton.MinSize = new Size(RadArrowButtonElement.RadArrowButtonDefaultSize.Width, this.arrowButton.ArrowFullSize.Height);
      this.arrowButton.Class = "ComboBoxdropDownButton";
      this.arrowButton.ClickMode = ClickMode.Press;
      this.textBoxPanel = new RadTextBoxElement();
      this.textBoxPanel.ThemeRole = "ComboTextBoxElement";
      this.textBoxPanel.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.textBoxPanel.ShowBorder = false;
      this.textBoxPanel.Class = "ComboBoxTextEditor";
      this.textBox = this.textBoxPanel.TextBoxItem;
      this.textBox.Multiline = false;
      this.textboxContentElement.DrawText = true;
      int num1 = (int) this.textboxContentElement.BindProperty(RadItem.TextProperty, (RadObject) this.textBox, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      int num2 = (int) this.textboxContentElement.BindProperty(VisualElement.BackColorProperty, (RadObject) this.textBox, VisualElement.BackColorProperty, PropertyBindingOptions.TwoWay);
      int num3 = (int) this.textboxContentElement.BindProperty(VisualElement.ForeColorProperty, (RadObject) this.textBox, VisualElement.ForeColorProperty, PropertyBindingOptions.TwoWay);
      this.textboxContentElement.TextAlignment = ContentAlignment.MiddleLeft;
      this.WireEvents();
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "ComboBoxBorder";
      this.borderPrimitive.ZIndex = 1;
      this.fillPrimitive = new FillPrimitive();
      int num4 = (int) this.fillPrimitive.BindProperty(RadElement.AutoSizeModeProperty, (RadObject) this, RadElement.AutoSizeModeProperty, PropertyBindingOptions.TwoWay);
      this.fillPrimitive.Class = "ComboBoxFill";
      this.layoutPanel = new ComboBoxEditorLayoutPanel();
      this.layoutPanel.Content = (RadElement) this.textBoxPanel;
      this.layoutPanel.ArrowButton = (RadElement) this.arrowButton;
      this.Children.Add((RadElement) this.fillPrimitive);
      this.Children.Add((RadElement) this.borderPrimitive);
      this.Children.Add((RadElement) this.layoutPanel);
      if (!this.DesignMode)
        return;
      this.textBox.TextBoxControl.Enabled = false;
      this.textBox.TextBoxControl.BackColor = Color.White;
    }

    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Description("Gets or sets the property to display.")]
    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    public abstract string DisplayMember { get; set; }

    [DefaultValue(null)]
    [Description("Gets or sets the data source.")]
    [Category("Data")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [AttributeProvider(typeof (IListSource))]
    public abstract object DataSource { get; set; }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Description("Gets or sets the IFormatProvider that provides custom formatting behavior.")]
    [DefaultValue(null)]
    [Browsable(false)]
    public abstract IFormatProvider FormatInfo { get; set; }

    [MergableProperty(false)]
    [DefaultValue("")]
    [Description("Gets or sets the format-specifier characters that indicate how a value is to be displayed. ")]
    public abstract string FormatString { get; set; }

    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether formatting is applied to the DisplayMember property.")]
    public abstract bool FormattingEnabled { get; set; }

    [Description("Gets or sets value specifying the currently selected item.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Bindable(true)]
    public abstract object SelectedValue { get; set; }

    [DefaultValue("")]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets or sets the property to use as the actual value for the items.")]
    [Category("Data")]
    public abstract string ValueMember { get; set; }

    public abstract string GetItemText(object item);

    protected virtual void WireEvents()
    {
      if (this.textBox == null)
        return;
      this.textBox.KeyDown += new KeyEventHandler(this.ProcessKeyDown);
      this.textBox.KeyPress += new KeyPressEventHandler(this.ProcessTextKeyPress);
      this.textBox.KeyUp += new KeyEventHandler(this.ProcessTextKeyUp);
      this.textBox.DoubleClick += new EventHandler(this.ProcessTextDoubleClick);
      this.textBox.TextChanged += new EventHandler(this.OnTextBoxControl_TextChanged);
      this.textboxContentElement.KeyDown += new KeyEventHandler(this.ProcessKeyDown);
      this.textboxContentElement.KeyPress += new KeyPressEventHandler(this.ProcessTextKeyPress);
      this.textboxContentElement.KeyUp += new KeyEventHandler(this.ProcessTextKeyUp);
      this.textboxContentElement.DoubleClick += new EventHandler(this.ProcessTextDoubleClick);
      this.textboxContentElement.TextChanged += new EventHandler(this.OnTextBoxControl_TextChanged);
      this.textboxContentElement.MouseWheel += new MouseEventHandler(this.ProcessTextMouseWheel);
      this.textboxContentElement.MouseEnter += new EventHandler(this.TextBoxControl_MouseEnter);
      HostedTextBoxBase textBoxControl = this.TextBoxElement.TextBoxItem.TextBoxControl;
      textBoxControl.LostFocus += new EventHandler(this.ProcessTextLostFocus);
      textBoxControl.GotFocus += new EventHandler(this.ProcessTextGotFocus);
      textBoxControl.MouseCaptureChanged += new EventHandler(this.ProcessTextMouseCaptureChanged);
      textBoxControl.MouseWheel += new MouseEventHandler(this.ProcessTextMouseWheel);
      textBoxControl.MouseEnter += new EventHandler(this.TextBoxControl_MouseEnter);
    }

    private void TextBoxControl_MouseEnter(object sender, EventArgs e)
    {
      if (this.DropDownStyle == RadDropDownStyle.DropDownList)
        this.textBox.TextBoxControl.Cursor = Cursors.Arrow;
      else
        this.textBox.TextBoxControl.Cursor = (Cursor) null;
    }

    private void ProcessTextGotFocus(object sender, EventArgs e)
    {
      if (this.DropDownStyle == RadDropDownStyle.DropDownList)
        Telerik.WinControls.NativeMethods.HideCaret(this.textBox.TextBoxControl.Handle);
      else
        this.UpdateFocusBorder(true);
    }

    protected virtual void UnwireEvents()
    {
      if (this.textBox == null)
        return;
      this.textBox.KeyDown -= new KeyEventHandler(this.ProcessKeyDown);
      this.textBox.KeyPress -= new KeyPressEventHandler(this.ProcessTextKeyPress);
      this.textBox.KeyUp -= new KeyEventHandler(this.ProcessTextKeyUp);
      this.textBox.DoubleClick -= new EventHandler(this.ProcessTextDoubleClick);
      this.textBox.TextChanged -= new EventHandler(this.OnTextBoxControl_TextChanged);
      this.textboxContentElement.KeyDown -= new KeyEventHandler(this.ProcessKeyDown);
      this.textboxContentElement.KeyPress -= new KeyPressEventHandler(this.ProcessTextKeyPress);
      this.textboxContentElement.KeyUp -= new KeyEventHandler(this.ProcessTextKeyUp);
      this.textboxContentElement.DoubleClick -= new EventHandler(this.ProcessTextDoubleClick);
      this.textboxContentElement.TextChanged -= new EventHandler(this.OnTextBoxControl_TextChanged);
      this.textboxContentElement.MouseWheel -= new MouseEventHandler(this.ProcessTextMouseWheel);
      this.textboxContentElement.MouseEnter -= new EventHandler(this.TextBoxControl_MouseEnter);
      HostedTextBoxBase textBoxControl = this.TextBoxElement.TextBoxItem.TextBoxControl;
      textBoxControl.LostFocus -= new EventHandler(this.ProcessTextLostFocus);
      textBoxControl.GotFocus -= new EventHandler(this.ProcessTextGotFocus);
      textBoxControl.MouseCaptureChanged -= new EventHandler(this.ProcessTextMouseCaptureChanged);
      textBoxControl.MouseWheel -= new MouseEventHandler(this.ProcessTextMouseWheel);
      textBoxControl.MouseEnter -= new EventHandler(this.TextBoxControl_MouseEnter);
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      if (args.RoutedEvent == RadElement.MouseWheelEvent && sender == this.textBox)
      {
        this.KeyboardCommandIssued = false;
        this.OnMouseWheel((MouseEventArgs) args.OriginalEventArgs);
      }
      if (args.RoutedEvent == RadElement.MouseUpEvent)
      {
        this.KeyboardCommandIssued = false;
        if (sender == this.textBox && this.DropDownStyle == RadDropDownStyle.DropDownList || sender == this.arrowButton)
        {
          if (!this.IsPopupOpen)
          {
            this.ShowPopup();
            this.textBox.Focus();
          }
          else
            this.ClosePopup(RadPopupCloseReason.Mouse);
        }
      }
      base.OnBubbleEvent(sender, args);
    }

    protected override void OnTunnelEvent(RadElement sender, RoutedEventArgs args)
    {
      base.OnTunnelEvent(sender, args);
      if (args.RoutedEvent != RootRadElement.OnRoutedImageListChanged)
        return;
      this.PopupForm.ImageList = this.ElementTree.ComponentTreeHandler.ImageList;
    }

    [Category("Property Changed")]
    [Browsable(true)]
    [Description("Occurs when the CaseSensitive property has changed.")]
    public event EventHandler CaseSensitiveChanged
    {
      add
      {
        this.Events.AddHandler(BaseComboBoxElement.CaseSensitiveChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(BaseComboBoxElement.CaseSensitiveChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCaseSensitiveChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[BaseComboBoxElement.CaseSensitiveChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Category("Property Changed")]
    [Browsable(true)]
    [Description("Occurs when the DropDownStyle property has changed.")]
    public event EventHandler DropDownStyleChanged
    {
      add
      {
        this.Events.AddHandler(BaseComboBoxElement.DropDownStyleChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(BaseComboBoxElement.DropDownStyleChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownStyleChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[BaseComboBoxElement.DropDownStyleChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Occurs when the SelectedIndex property has changed.")]
    public event EventHandler SelectedIndexChanged
    {
      add
      {
        this.Events.AddHandler(BaseComboBoxElement.SelectedIndexChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(BaseComboBoxElement.SelectedIndexChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnTextChanged(EventArgs e)
    {
      this.OnTextChanged(e);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnSelectedIndexChanged(EventArgs e)
    {
      this.OnSelectedIndexChanged(e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnSelectedIndexChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[BaseComboBoxElement.SelectedIndexChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Description("Occurs when the SelectedValue property has changed.")]
    [Category("Property Changed")]
    [Browsable(true)]
    public event EventHandler SelectedValueChanged
    {
      add
      {
        this.Events.AddHandler(BaseComboBoxElement.SelectedValueChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(BaseComboBoxElement.SelectedValueChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnSelectedValueChanged(EventArgs e)
    {
      this.OnSelectedValueChanged(e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnSelectedValueChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[BaseComboBoxElement.SelectedValueChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Browsable(true)]
    [Category("Property Changed")]
    [Description("Occurs when the Sorted property has changed.")]
    public event EventHandler SortedChanged
    {
      add
      {
        this.Events.AddHandler(BaseComboBoxElement.SortedChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(BaseComboBoxElement.SortedChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CallOnSortedChanged(EventArgs e)
    {
      this.OnSortedChanged(e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnSortedChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[BaseComboBoxElement.SortedChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      if (args.Property == BaseComboBoxElement.SortedProperty && this.DataSource != null && (SortStyle) args.NewValue != SortStyle.None)
        throw new InvalidOperationException("ComboBox that has a DataSource set cannot be sorted. Sort the data using the underlying data model.");
      base.OnPropertyChanging(args);
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      switch (propertyName)
      {
        case "DropDownHeight":
          this.IntegralHeight = false;
          break;
      }
      base.OnNotifyPropertyChanged(propertyName);
    }

    private void ProcessTextMouseWheel(object sender, MouseEventArgs e)
    {
      this.OnMouseWheel(e);
    }

    protected virtual void ProcessTextKeyUp(object sender, KeyEventArgs e)
    {
      if (this.ElementTree != null && this.ElementTree.Control != null)
      {
        Form form = this.ElementTree.Control.FindForm();
        if (form != null)
          form.Cursor = Cursors.Arrow;
        else if (Form.ActiveForm != null)
          Form.ActiveForm.Cursor = Form.ActiveForm.Cursor;
      }
      switch (e.KeyCode)
      {
        case Keys.Return:
        case Keys.Escape:
        case Keys.Left:
        case Keys.Up:
        case Keys.Right:
        case Keys.Down:
          this.OnKeyUp(e);
          break;
        default:
          if (this.DropDownStyle != RadDropDownStyle.DropDownList)
          {
            string str = this.Text;
            if (AutoCompleteMode.Append == (this.AutoCompleteMode & AutoCompleteMode.Append) && this.textBox.SelectionLength > 0 && this.textBox.SelectionStart > 0)
              str = this.textBox.Text.Substring(0, this.textBox.SelectionStart);
            if (this.oldTextValue != str)
            {
              this.oldTextValue = this.textBox.TextBoxControl.Text;
              if (this.autoFilterDelay > 0)
              {
                this.autoFilterTimer.Stop();
                this.autoFilterTimer.Interval = this.autoFilterDelay;
                this.autoFilterTimer.Tick -= new EventHandler(this.Timer_Tick);
                this.autoFilterTimer.Tick += new EventHandler(this.Timer_Tick);
                this.autoFilterTimer.Start();
                goto case Keys.Return;
              }
              else
              {
                this.ProcessTextChanged(sender, (EventArgs) e);
                this.KeyboardCommandIssued = false;
                goto case Keys.Return;
              }
            }
            else
              goto case Keys.Return;
          }
          else
            goto case Keys.Return;
      }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      this.autoFilterTimer.Tick -= new EventHandler(this.Timer_Tick);
      this.ProcessTextChanged(sender, e);
      this.KeyboardCommandIssued = false;
    }

    private void ProcessTextLostFocus(object sender, EventArgs e)
    {
      this.OnTextBoxCaptureChanged = 0;
      if (this.DropDownStyle != RadDropDownStyle.DropDown)
        return;
      this.UpdateFocusBorder(false);
    }

    private void ProcessTextMouseCaptureChanged(object sender, EventArgs e)
    {
      ++this.OnTextBoxCaptureChanged;
      HostedTextBoxBase textBoxControl = this.TextBoxElement.TextBoxItem.TextBoxControl;
      if (this.OnTextBoxCaptureChanged != 1 || !(this.Text == textBoxControl.Text) || this.SelectAllText(this.Text))
        return;
      this.textBox.DeselectAll();
    }

    protected virtual void ProcessKeyDown(object sender, KeyEventArgs e)
    {
      this.oldTextValue = this.textBox.Text;
      if (e.Handled)
      {
        this.OnKeyDown(e);
      }
      else
      {
        this.lastPressedKey = e.KeyCode;
        if (this.TooglePopupWithKeyboard(e))
        {
          this.OnKeyDown(e);
        }
        else
        {
          this.KeyboardCommandIssued = false;
          switch (e.KeyCode)
          {
            case Keys.Back:
            case Keys.Delete:
              this.KeyboardCommandIssued = true;
              this.ProcessDeleteKey(e);
              break;
            case Keys.Return:
              this.ProcessReturnKey(e);
              break;
            case Keys.Escape:
              e.Handled = this.ProcessEscKey(e);
              break;
            case Keys.Prior:
            case Keys.Next:
              this.ProcessPageUpDownKeys(e);
              break;
            case Keys.Left:
              if (this.DropDownStyle == RadDropDownStyle.DropDownList)
              {
                this.KeyboardCommandIssued = true;
                this.SelectPreviousItem();
                e.Handled = true;
                break;
              }
              break;
            case Keys.Up:
              this.KeyboardCommandIssued = true;
              this.SelectPreviousItem();
              e.Handled = true;
              break;
            case Keys.Right:
              if (this.DropDownStyle == RadDropDownStyle.DropDownList)
              {
                this.KeyboardCommandIssued = true;
                this.SelectNextItem();
                e.Handled = true;
                break;
              }
              break;
            case Keys.Down:
              this.KeyboardCommandIssued = true;
              this.SelectNextItem();
              e.Handled = true;
              break;
          }
          this.OnKeyDown(e);
        }
      }
    }

    protected virtual void ProcessPageUpDownKeys(KeyEventArgs e)
    {
    }

    protected virtual void ProcessDeleteKey(KeyEventArgs e)
    {
      if (this.DropDownStyle != RadDropDownStyle.DropDownList)
        return;
      if (this.Items.Count > 0)
        this.SelectedIndex = 0;
      e.Handled = true;
    }

    public virtual void ProcessReturnKey(KeyEventArgs e)
    {
    }

    public virtual bool ProcessEscKey(KeyEventArgs e)
    {
      return false;
    }

    protected virtual string GetText(object item)
    {
      return (item as RadItem)?.Text;
    }

    protected virtual object GetActiveItem()
    {
      return (object) null;
    }

    private void OnTextBoxControl_TextChanged(object sender, EventArgs e)
    {
      if (!(this.Text != this.textBox.Text))
        return;
      this.Text = this.textBox.Text;
      this.textBoxPanel.Text = this.Text;
    }

    protected virtual void ProcessTextChanged(object sender, EventArgs e)
    {
      if (!this.IsInValidState(true) || this.IsDesignMode || (this.DropDownStyle == RadDropDownStyle.DropDownList || this.IndexChanging))
        return;
      string text = this.TextBoxElement.TextBoxItem.TextBoxControl.Text;
      if (!(text != this.lastTextChangedValue))
        return;
      this.lastTextChangedValue = text;
      this.LastTypedText = text;
    }

    private void ProcessTextKeyPress(object sender, KeyPressEventArgs e)
    {
      this.lastPressedChar = e.KeyChar;
      switch (this.AutoCompleteMode)
      {
        case AutoCompleteMode.Suggest:
          if (this.lastPressedKey != Keys.Return)
          {
            this.SetSuggestAutoComplete();
            break;
          }
          break;
        case AutoCompleteMode.Append:
          this.SetAppendAutoComplete(e);
          break;
        case AutoCompleteMode.SuggestAppend:
          this.SetAppendAutoComplete(e);
          this.SetSuggestAutoComplete();
          break;
      }
      this.OnKeyPress(e);
    }

    public abstract ArrayList FindAllItems(string startsWith);

    protected virtual void SelectPreviousItem()
    {
    }

    protected virtual void SelectNextItem()
    {
    }

    private bool TooglePopupWithKeyboard(KeyEventArgs e)
    {
      if ((!e.Alt || e.KeyCode != Keys.Up && e.KeyCode != Keys.Down) && (e.Modifiers != Keys.None || e.KeyCode != Keys.F4))
        return false;
      this.TooglePopupState();
      return true;
    }

    private bool IsCommandKey(Keys keyCode)
    {
      return keyCode == Keys.Return || keyCode == Keys.Delete || (keyCode == Keys.Back || keyCode == Keys.Down) || (keyCode == Keys.Up || keyCode == Keys.Left || (keyCode == Keys.Right || keyCode == Keys.Escape));
    }

    protected abstract void SetAppendAutoComplete(KeyPressEventArgs e);

    protected virtual void SetSuggestAutoComplete()
    {
      if (this.IsPopupOpen)
      {
        string text = this.Text;
        if (AutoCompleteMode.Append == (this.AutoCompleteMode & AutoCompleteMode.Append) && this.textBox.SelectionLength > 0 && this.textBox.SelectionStart > 0)
          text = this.textBox.Text.Substring(0, this.textBox.SelectionStart);
        if (this.lastPressedKey == Keys.Up || this.lastPressedKey == Keys.Down)
          return;
        object itemExact = this.FindItemExact(text);
        if (itemExact == null)
          return;
        this.SelectedItem = itemExact;
      }
      else
      {
        if (char.IsControl((char) this.lastPressedKey) || this.lastPressedKey == Keys.Delete)
          return;
        this.ShowPopup();
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == BaseComboBoxElement.DropDownStyleProperty)
      {
        if ((RadDropDownStyle) e.NewValue == RadDropDownStyle.DropDownList)
        {
          if (!this.IsDesignMode && this.IsInValidState(true) && (!this.ElementTree.ComponentTreeHandler.Initializing && this.FindItemIndexExact(this.Text) == -1))
            this.TextBoxElement.TextBoxItem.Text = string.Empty;
          this.TextBoxElement.TextBoxItem.TextBoxControl.ReadOnly = true;
        }
        else
          this.TextBoxElement.TextBoxItem.TextBoxControl.ReadOnly = false;
        foreach (RadObject radObject in this.ChildrenHierarchy)
        {
          int num = (int) radObject.SetValue(BaseComboBoxElement.DropDownStyleProperty, e.NewValue);
        }
        this.OnDropDownStyleChanged(EventArgs.Empty);
      }
      if (e.Property == BaseComboBoxElement.CaseSensitiveProperty)
        this.OnCaseSensitiveChanged(EventArgs.Empty);
      if (e.Property == BaseComboBoxElement.DropDownAnimationEnabledProperty)
        this.PopupForm.AnimationEnabled = (bool) e.NewValue;
      if (e.Property == BaseComboBoxElement.DropDownAnimationEasingProperty)
        this.PopupForm.AnimationProperties.EasingType = (RadEasingType) e.NewValue;
      if (e.Property == BaseComboBoxElement.DropDownAnimationFramesProperty)
        this.PopupForm.AnimationProperties.AnimationFrames = (int) e.NewValue;
      if (e.Property == BaseComboBoxElement.IsDropDownShownProperty)
      {
        foreach (RadObject radObject in this.ChildrenHierarchy)
        {
          int num = (int) radObject.SetValue(BaseComboBoxElement.IsDropDownShownProperty, e.NewValue);
        }
      }
      if (e.Property == VisualElement.BackColorProperty)
        this.textBoxPanel.BackColor = (Color) e.NewValue;
      if (e.Property == RadItem.TextProperty)
        this.SetActiveItem((string) e.NewValue);
      if (e.Property != RadElement.RightToLeftProperty)
        return;
      if ((bool) e.NewValue)
      {
        this.PopupForm.RightToLeft = RightToLeft.Yes;
        this.textBox.RightToLeft = true;
      }
      else
      {
        this.PopupForm.RightToLeft = RightToLeft.No;
        this.textBox.RightToLeft = false;
      }
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (!this.DblClickRotate || this.Items == null || this.Items.Count <= 0)
        return;
      if (this.SelectedIndex < this.Items.Count - 1)
        ++this.SelectedIndex;
      else
        this.SelectedIndex = 0;
      this.ClosePopup();
    }

    private void ProcessTextDoubleClick(object sender, EventArgs e)
    {
      this.OnDoubleClick(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (e.Delta == 0)
        return;
      if (this.scrollOnMouseWheel && !this.IsPopupOpen)
      {
        if (e.Delta > 0)
          this.SelectPreviousItem();
        else
          this.SelectNextItem();
      }
      if (!this.IsPopupOpen)
        return;
      if (e.Delta > 0)
        this.DoScrollLineUp();
      else
        this.DoScrollLineDown();
    }

    protected abstract void DoScrollLineUp();

    protected abstract void DoScrollLineDown();

    public bool SelectAllText(string text)
    {
      if (this.TextBoxElement.TextBoxItem.TextBoxControl.ReadOnly)
        return false;
      this.SelectionStart = 0;
      this.SelectionLength = string.IsNullOrEmpty(text) ? 0 : text.Length;
      return true;
    }

    protected abstract object FindItem(string startsWith);

    protected abstract object FindItemExact(string text);

    protected abstract int FindItemIndexExact(string text);

    protected internal abstract void SetActiveItem(object item);

    protected internal abstract void SetActiveItem(string text);

    public abstract void BeginUpdate();

    public abstract void EndUpdate();

    public abstract int GetItemHeight(int index);

    public void Select(int start, int length)
    {
      this.textBox.Select(start, length);
    }

    public void SelectAll()
    {
      this.textBox.SelectAll();
    }

    protected abstract void ScrollToHome();

    protected abstract void ScrollItemIntoView(object item);

    protected virtual void ApplyThemeToPopupForm()
    {
      string str = "ControlDefault";
      if (this.ElementTree != null && this.ElementTree.ComponentTreeHandler != null && !string.IsNullOrEmpty(this.ElementTree.ComponentTreeHandler.ThemeName))
        str = this.ElementTree.ComponentTreeHandler.ThemeName;
      RadPopupControlBase popupForm = this.PopupForm;
      if (!(popupForm.ThemeName != str))
        return;
      popupForm.ThemeName = str;
      popupForm.RootElement.UpdateLayout();
    }

    private void BringSelectedItemIntoView()
    {
      if (this.PopupForm == null)
        return;
      if (this.SelectedItem != null)
        this.ScrollItemIntoView((object) (this.SelectedItem as RadItem));
      else
        this.ScrollToHome();
    }

    private void SetDropDownMinMaxSize()
    {
      RadSizablePopupControl popupForm = this.PopupForm as RadSizablePopupControl;
      if (popupForm == null)
        return;
      RadElement child = popupForm.SizingGrip.Children[3];
      Size b = Size.Add(child.BoundingRectangle.Size, child.Margin.Size);
      popupForm.MinimumSize = LayoutUtils.UnionSizes(this.dropDownMinSize, b);
      if (!(this.dropDownMaxSize != Size.Empty))
        return;
      popupForm.MaximumSize = this.dropDownMaxSize;
    }

    public override void ShowPopup()
    {
      this.ApplyThemeToPopupForm();
      this.SetDropDownMinMaxSize();
      this.BringSelectedItemIntoView();
      base.ShowPopup();
      if (!this.PopupDisplayedForTheFirstTime)
        this.PopupDisplayedForTheFirstTime = false;
      if (!this.IsPopupOpen)
        return;
      if (this.SelectedItem == null)
        this.ScrollItemIntoView((object) null);
      else
        this.SetActiveItem((object) (this.SelectedItem as RadItem));
    }

    protected override void OnPopupOpened(EventArgs args)
    {
      this.IsDropDownShown = true;
      base.OnPopupOpened(args);
    }

    protected override void OnPopupClosed(RadPopupClosedEventArgs e)
    {
      this.IsDropDownShown = false;
      base.OnPopupClosed(e);
    }

    public override object Value
    {
      get
      {
        if (this.ValueMember != string.Empty)
          return this.SelectedValue;
        return (object) string.Empty;
      }
      set
      {
        if (this.ValueMember != string.Empty)
          this.SelectedValue = value;
        else if (value != null)
        {
          object itemExact = this.FindItemExact(value.ToString());
          if (itemExact == null)
            return;
          this.SelectedItem = itemExact;
        }
        else
          this.SelectedItem = (object) null;
      }
    }

    public override EditorVisualMode VisualMode
    {
      get
      {
        return EditorVisualMode.Dropdown;
      }
    }

    public LightVisualElement TextboxContentElement
    {
      get
      {
        return this.textboxContentElement;
      }
      set
      {
        this.textboxContentElement = value;
      }
    }

    public override void BeginEdit()
    {
      this.SelectAllText(this.Text);
    }
  }
}
