// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMultiColumnComboBox
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
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [DefaultEvent("SelectedIndexChanged")]
  [ToolboxItem(true)]
  [DefaultBindingProperty("Text")]
  [DefaultProperty("Text")]
  [Description("Displays an editable box with a drop down list of permitted values")]
  [Designer("Telerik.WinControls.UI.Design.RadMultiColumnComboDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Data Controls")]
  public class RadMultiColumnComboBox : RadEditorControl
  {
    private static readonly object CaseSensitiveChangedEventKey = new object();
    private static readonly object DropDownOpenedEventKey = new object();
    private static readonly object DropDownClosedEventKey = new object();
    private static readonly object DropDownOpeningEventKey = new object();
    private static readonly object DropDownClosingEventKey = new object();
    private static readonly object DropDownStyleChangedEventKey = new object();
    private static readonly object SelectedIndexChangedEventKey = new object();
    private static readonly object SelectedValueChangedEventKey = new object();
    private static readonly object SortedChangedEventKey = new object();
    private RadMultiColumnComboBoxElement comboBoxElement;
    private bool entering;

    public RadMultiColumnComboBox()
    {
      this.AutoSize = true;
      this.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, true);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.UnwireEvents();
      base.Dispose(disposing);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.comboBoxElement = this.CreateMultiColumnComboBoxElement();
      this.comboBoxElement.ArrowButton.Arrow.AutoSize = true;
      this.comboBoxElement.AutoSizeMode = RadAutoSizeMode.WrapAroundChildren;
      this.WireEvents();
      this.RootElement.Children.Add((RadElement) this.comboBoxElement);
      base.CreateChildItems(parent);
    }

    protected virtual RadMultiColumnComboBoxElement CreateMultiColumnComboBoxElement()
    {
      return new RadMultiColumnComboBoxElement();
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

    protected virtual void WireEvents()
    {
      if (this.comboBoxElement == null)
        return;
      this.comboBoxElement.CaseSensitiveChanged += new EventHandler(this.comboBoxElement_CaseSensitiveChanged);
      this.comboBoxElement.DropDownStyleChanged += new EventHandler(this.comboBoxElement_DropDownStyleChanged);
      this.comboBoxElement.KeyDown += new KeyEventHandler(this.comboBoxElement_KeyDown);
      this.comboBoxElement.KeyPress += new KeyPressEventHandler(this.comboBoxElement_KeyPress);
      this.comboBoxElement.KeyUp += new KeyEventHandler(this.comboBoxElement_KeyUp);
      this.comboBoxElement.PopupOpened += new EventHandler(this.comboBoxElement_PopupOpened);
      this.comboBoxElement.PopupClosed += new RadPopupClosedEventHandler(this.comboBoxElement_PopupClosed);
      this.comboBoxElement.PopupOpening += new CancelEventHandler(this.comboBoxElement_PopupOpening);
      this.comboBoxElement.PopupClosing += new RadPopupClosingEventHandler(this.comboBoxElement_PopupClosing);
      this.comboBoxElement.SelectedIndexChanged += new EventHandler(this.comboBoxElement_SelectedIndexChanged);
      this.comboBoxElement.SelectedValueChanged += new EventHandler(this.comboBoxElement_SelectedValueChanged);
      this.comboBoxElement.SortedChanged += new EventHandler(this.comboBoxElement_SortedChanged);
      this.comboBoxElement.TextChanged += new EventHandler(this.comboBoxElement_TextChanged);
      this.comboBoxElement.EditorControl.DataBindingComplete += new GridViewBindingCompleteEventHandler(this.EditControl_DataBindingComplete);
    }

    protected virtual void UnwireEvents()
    {
      if (this.comboBoxElement == null)
        return;
      this.comboBoxElement.CaseSensitiveChanged -= new EventHandler(this.comboBoxElement_CaseSensitiveChanged);
      this.comboBoxElement.DropDownStyleChanged -= new EventHandler(this.comboBoxElement_DropDownStyleChanged);
      this.comboBoxElement.KeyDown -= new KeyEventHandler(this.comboBoxElement_KeyDown);
      this.comboBoxElement.KeyPress -= new KeyPressEventHandler(this.comboBoxElement_KeyPress);
      this.comboBoxElement.KeyUp -= new KeyEventHandler(this.comboBoxElement_KeyUp);
      this.comboBoxElement.PopupOpened -= new EventHandler(this.comboBoxElement_PopupOpened);
      this.comboBoxElement.PopupOpening -= new CancelEventHandler(this.comboBoxElement_PopupOpening);
      this.comboBoxElement.PopupClosing -= new RadPopupClosingEventHandler(this.comboBoxElement_PopupClosing);
      this.comboBoxElement.PopupClosed -= new RadPopupClosedEventHandler(this.comboBoxElement_PopupClosed);
      this.comboBoxElement.SelectedIndexChanged -= new EventHandler(this.comboBoxElement_SelectedIndexChanged);
      this.comboBoxElement.SelectedValueChanged -= new EventHandler(this.comboBoxElement_SelectedValueChanged);
      this.comboBoxElement.SortedChanged -= new EventHandler(this.comboBoxElement_SortedChanged);
      this.comboBoxElement.TextChanged -= new EventHandler(this.comboBoxElement_TextChanged);
      if (this.comboBoxElement.EditorControl == null)
        return;
      this.comboBoxElement.EditorControl.DataBindingComplete -= new GridViewBindingCompleteEventHandler(this.EditControl_DataBindingComplete);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DefaultValue(0)]
    public int AutoFilterDelay
    {
      get
      {
        return this.MultiColumnComboBoxElement.AutoFilterDelay;
      }
      set
      {
        this.MultiColumnComboBoxElement.AutoFilterDelay = value;
      }
    }

    [DefaultValue(BestFitColumnMode.AllCells)]
    public virtual BestFitColumnMode AutoSizeDropDownColumnMode
    {
      get
      {
        return this.MultiColumnComboBoxElement.AutoSizeDropDownColumnMode;
      }
      set
      {
        this.MultiColumnComboBoxElement.AutoSizeDropDownColumnMode = value;
      }
    }

    [Browsable(true)]
    [DefaultValue(true)]
    [Category("Behavior")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Description("Specifies the mode for the Text clearing if the text does not match the text in the DisplayMembar column.")]
    public bool ClearTextOnValidation
    {
      get
      {
        return this.MultiColumnComboBoxElement.ClearTextOnValidation;
      }
      set
      {
        this.MultiColumnComboBoxElement.ClearTextOnValidation = value;
      }
    }

    [Browsable(false)]
    [Description("Specifies the mode for the automatic completion feature used in the MultiColumnComboBox.")]
    [Category("Behavior")]
    [DefaultValue(AutoCompleteMode.None)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.MultiColumnComboBoxElement.AutoCompleteMode;
      }
      set
      {
        this.MultiColumnComboBoxElement.AutoCompleteMode = value;
      }
    }

    protected internal MultiColumnComboPopupForm MultiColumnPopupForm
    {
      get
      {
        return this.comboBoxElement.MultiColumnPopupForm;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.GridViewColumnCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [NotifyParentProperty(true)]
    [Category("Data")]
    public GridViewColumnCollection Columns
    {
      get
      {
        return this.comboBoxElement.Columns;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(100, 20));
      }
    }

    public override bool ShowItemToolTips
    {
      get
      {
        return base.ShowItemToolTips;
      }
      set
      {
        base.ShowItemToolTips = value;
        this.MultiColumnComboBoxElement.MultiColumnPopupForm.ShowItemToolTips = value;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.SimpleTextUITypeEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public override string Text
    {
      get
      {
        return this.comboBoxElement.Text;
      }
      set
      {
        if (this.comboBoxElement.Text != value)
          this.comboBoxElement.Text = value;
        base.Text = this.comboBoxElement.Text;
        this.OnNotifyPropertyChanged(nameof (Text));
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [RadDefaultValue("DblClickRotate", typeof (RadMultiColumnComboBoxElement))]
    [Description("Rotates selected items on double clicking inside the text edit control.")]
    public bool DblClickRotate
    {
      get
      {
        return this.comboBoxElement.DblClickRotate;
      }
      set
      {
        this.comboBoxElement.DblClickRotate = value;
      }
    }

    [Description("Gets or sets a boolean value determining whether the user can scroll through the items when the popup is closed by using the mouse wheel.")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool ScrollOnMouseWheel
    {
      get
      {
        return this.comboBoxElement.ScrollOnMouseWheel;
      }
      set
      {
        this.comboBoxElement.ScrollOnMouseWheel = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadMultiColumnComboBoxElement MultiColumnComboBoxElement
    {
      get
      {
        return this.comboBoxElement;
      }
    }

    [Description("Gets or sets the drop down sizing mode. The mode can be: horizontal, vertical or a combination of them.")]
    [Browsable(true)]
    [Category("Appearance")]
    [RadDefaultValue("DropDownSizingMode", typeof (RadMultiColumnComboBoxElement))]
    public SizingMode DropDownSizingMode
    {
      get
      {
        return this.comboBoxElement.DropDownSizingMode;
      }
      set
      {
        this.comboBoxElement.DropDownSizingMode = value;
      }
    }

    [Bindable(true)]
    [Browsable(true)]
    [RadDefaultValue("NullText", typeof (RadMultiColumnComboBoxElement))]
    [Localizable(true)]
    [Category("Behavior")]
    [Description("Gets or sets the text that is displayed when the Text property contains empty string or is null.")]
    public string NullText
    {
      get
      {
        return this.comboBoxElement.NullText;
      }
      set
      {
        this.comboBoxElement.NullText = value;
      }
    }

    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [RadDescription("DisplayMember", typeof (RadMultiColumnComboBoxElement))]
    [RadDefaultValue("DisplayMember", typeof (RadMultiColumnComboBoxElement))]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [Category("Data")]
    public string DisplayMember
    {
      get
      {
        return this.comboBoxElement.DisplayMember;
      }
      set
      {
        this.comboBoxElement.DisplayMember = value;
        this.OnNotifyPropertyChanged(nameof (DisplayMember));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadGridView EditorControl
    {
      get
      {
        return this.comboBoxElement.EditorControl;
      }
    }

    [Category("Data")]
    [RadDefaultValue("AutoFilter", typeof (RadMultiColumnComboBoxElement))]
    [RadDescription("AutoFilter", typeof (RadMultiColumnComboBoxElement))]
    public bool AutoFilter
    {
      get
      {
        return this.comboBoxElement.AutoFilter;
      }
      set
      {
        this.comboBoxElement.AutoFilter = value;
      }
    }

    [RadDefaultValue("DataSource", typeof (RadMultiColumnComboBoxElement))]
    [Category("Data")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [AttributeProvider(typeof (IListSource))]
    [RadDescription("DataSource", typeof (RadMultiColumnComboBoxElement))]
    public object DataSource
    {
      get
      {
        return this.comboBoxElement.DataSource;
      }
      set
      {
        this.comboBoxElement.DataSource = value;
      }
    }

    [RadDescription("ValueMember", typeof (RadMultiColumnComboBoxElement))]
    [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Category("Data")]
    [RadDefaultValue("ValueMember", typeof (RadMultiColumnComboBoxElement))]
    [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public string ValueMember
    {
      get
      {
        return this.comboBoxElement.ValueMember;
      }
      set
      {
        this.comboBoxElement.ValueMember = value;
        this.OnNotifyPropertyChanged(nameof (ValueMember));
      }
    }

    [Category("Behavior")]
    [RadDescription("SelectedIndex", typeof (BaseComboBoxElement))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectedIndex
    {
      get
      {
        return this.comboBoxElement.SelectedIndex;
      }
      set
      {
        this.comboBoxElement.SelectedIndex = value;
        this.OnNotifyPropertyChanged(nameof (SelectedIndex));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Bindable(true)]
    [RadDescription("SelectedItem", typeof (BaseComboBoxElement))]
    public object SelectedItem
    {
      get
      {
        return this.comboBoxElement.SelectedItem;
      }
      set
      {
        this.comboBoxElement.SelectedItem = value;
        this.OnNotifyPropertyChanged(nameof (SelectedItem));
      }
    }

    [Bindable(true)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [RadDescription("SelectedValue", typeof (BaseComboBoxElement))]
    public object SelectedValue
    {
      get
      {
        return this.comboBoxElement.SelectedValue;
      }
      set
      {
        this.comboBoxElement.SelectedValue = value;
        this.OnNotifyPropertyChanged(nameof (SelectedValue));
      }
    }

    [Description("Gets or sets a value specifying the style of the combo box.")]
    [RadPropertyDefaultValue("DropDownStyle", typeof (RadMultiColumnComboBoxElement))]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Browsable(true)]
    public RadDropDownStyle DropDownStyle
    {
      get
      {
        return this.comboBoxElement.DropDownStyle;
      }
      set
      {
        this.comboBoxElement.DropDownStyle = value;
        this.OnNotifyPropertyChanged(nameof (DropDownStyle));
      }
    }

    [Description("Determines whether the drop-down portion of the control will be auto-sized to best fit all columns. The DropDownWidth property must be set to its default value (-1) to allow this setting to work properly.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AutoSizeDropDownToBestFit
    {
      get
      {
        return this.comboBoxElement.AutoSizeDropDownToBestFit;
      }
      set
      {
        this.comboBoxElement.AutoSizeDropDownToBestFit = value;
      }
    }

    [Description("Determines whether the drop-down height will be auto-sized when filtering is applied.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public bool AutoSizeDropDownHeight
    {
      get
      {
        return this.comboBoxElement.AutoSizeDropDownHeight;
      }
      set
      {
        this.comboBoxElement.AutoSizeDropDownHeight = value;
      }
    }

    [Description("Gets or sets the minimum size allowed for the drop-down of the RadMultiColumnComboBox control.")]
    [DefaultValue(typeof (Size), "0,0")]
    [Category("Behavior")]
    public Size DropDownMinSize
    {
      get
      {
        return this.comboBoxElement.DropDownMinSize;
      }
      set
      {
        this.comboBoxElement.DropDownMinSize = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the maximum size allowed for the drop-down of the RadMultiColumnComboBox control.")]
    [DefaultValue(typeof (Size), "0,0")]
    public Size DropDownMaxSize
    {
      get
      {
        return this.comboBoxElement.DropDownMaxSize;
      }
      set
      {
        this.comboBoxElement.DropDownMaxSize = value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [Category("Layout")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
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

    public event GridViewBindingCompleteEventHandler DataBindingComplete;

    [Description("Occurs when the CaseSensitive property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler CaseSensitiveChanged
    {
      add
      {
        this.Events.AddHandler(RadMultiColumnComboBox.CaseSensitiveChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMultiColumnComboBox.CaseSensitiveChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnCaseSensitiveChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadMultiColumnComboBox.CaseSensitiveChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Occurs before the drop-down window appears.")]
    public event EventHandler DropDownOpened
    {
      add
      {
        this.Events.AddHandler(RadMultiColumnComboBox.DropDownOpenedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMultiColumnComboBox.DropDownOpenedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownOpened(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadMultiColumnComboBox.DropDownOpenedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Occurs when the drop-down window has closed.")]
    public event RadPopupClosedEventHandler DropDownClosed
    {
      add
      {
        this.Events.AddHandler(RadMultiColumnComboBox.DropDownClosedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMultiColumnComboBox.DropDownClosedEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when the drop-down window is about to close.")]
    [Category("Behavior")]
    public event RadPopupClosingEventHandler DropDownClosing
    {
      add
      {
        this.Events.AddHandler(RadMultiColumnComboBox.DropDownClosingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMultiColumnComboBox.DropDownClosingEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    [Description("Occurs when the drop-down window is about to open.")]
    public event RadPopupOpeningEventHandler DropDownOpening
    {
      add
      {
        this.Events.AddHandler(RadMultiColumnComboBox.DropDownOpeningEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMultiColumnComboBox.DropDownOpeningEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownClosed(RadPopupClosedEventArgs e)
    {
      RadPopupClosedEventHandler closedEventHandler = (RadPopupClosedEventHandler) this.Events[RadMultiColumnComboBox.DropDownClosedEventKey];
      if (closedEventHandler == null)
        return;
      closedEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownClosing(RadPopupClosingEventArgs e)
    {
      RadPopupClosingEventHandler closingEventHandler = (RadPopupClosingEventHandler) this.Events[RadMultiColumnComboBox.DropDownClosingEventKey];
      if (closingEventHandler == null)
        return;
      closingEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownOpening(CancelEventArgs e)
    {
      RadPopupOpeningEventHandler openingEventHandler = (RadPopupOpeningEventHandler) this.Events[RadMultiColumnComboBox.DropDownOpeningEventKey];
      if (openingEventHandler == null)
        return;
      openingEventHandler((object) this, e);
    }

    [Browsable(true)]
    [Description("Occurs when the DropDownStyle property has changed.")]
    [Category("Property Changed")]
    public event EventHandler DropDownStyleChanged
    {
      add
      {
        this.Events.AddHandler(RadMultiColumnComboBox.DropDownStyleChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMultiColumnComboBox.DropDownStyleChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownStyleChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadMultiColumnComboBox.DropDownStyleChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Browsable(true)]
    [Description("Occurs when the SelectedIndex property has changed.")]
    [Category("Behavior")]
    public event EventHandler SelectedIndexChanged
    {
      add
      {
        this.Events.AddHandler(RadMultiColumnComboBox.SelectedIndexChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMultiColumnComboBox.SelectedIndexChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnSelectedIndexChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadMultiColumnComboBox.SelectedIndexChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Description("Occurs when the SelectedValue property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler SelectedValueChanged
    {
      add
      {
        this.Events.AddHandler(RadMultiColumnComboBox.SelectedValueChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMultiColumnComboBox.SelectedValueChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnSelectedValueChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadMultiColumnComboBox.SelectedValueChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [Description("Occurs when the Sorted property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler SortedChanged
    {
      add
      {
        this.Events.AddHandler(RadMultiColumnComboBox.SortedChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadMultiColumnComboBox.SortedChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnSortedChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadMultiColumnComboBox.SortedChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    public override void EndInit()
    {
      base.EndInit();
      this.comboBoxElement.EndInit();
    }

    public void BestFitColumns()
    {
      this.MultiColumnComboBoxElement.BestFitColumns();
    }

    public void BestFitColumns(bool adjustComboBoxSize, bool bestFitAllRows)
    {
      this.MultiColumnComboBoxElement.BestFitColumns(adjustComboBoxSize, bestFitAllRows);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Tab)
        this.comboBoxElement.SetCurrentRowOnReturnOrTabKey(new KeyEventArgs(keyData));
      return base.ProcessCmdKey(ref msg, keyData);
    }

    protected override void OnEnter(EventArgs e)
    {
      base.OnEnter(e);
      if (this.entering)
        return;
      this.entering = true;
      this.comboBoxElement.TextBoxElement.TextBoxItem.TextBoxControl.Focus();
      this.OnGotFocus(e);
      this.entering = false;
    }

    protected override void OnLeave(EventArgs e)
    {
      this.OnLostFocus(e);
      base.OnLeave(e);
      this.ClosePopup();
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      if (this.Text == null)
        return;
      this.comboBoxElement.SelectionStart = 0;
      this.comboBoxElement.SelectionLength = this.Text.Length;
    }

    protected override void OnLostFocus(EventArgs e)
    {
      if (this.entering)
        return;
      base.OnLostFocus(e);
      this.MultiColumnComboBoxElement.SetCurrentRowOnReturnOrTabKey(new KeyEventArgs(Keys.None));
      if (this.comboBoxElement.SelectionLength != this.Text.Length)
        return;
      this.comboBoxElement.SelectionStart = 0;
      this.comboBoxElement.SelectionLength = 0;
    }

    private void comboBoxElement_CaseSensitiveChanged(object sender, EventArgs e)
    {
      this.OnCaseSensitiveChanged(e);
    }

    private void comboBoxElement_DropDownStyleChanged(object sender, EventArgs e)
    {
      this.OnDropDownStyleChanged(e);
    }

    protected override bool IsInputKey(Keys keyData)
    {
      if (this.DropDownStyle == RadDropDownStyle.DropDown)
        return base.IsInputKey(keyData);
      if (keyData != Keys.Down && keyData != Keys.Up)
        return base.IsInputKey(keyData);
      return true;
    }

    private void comboBoxElement_KeyDown(object sender, KeyEventArgs e)
    {
      this.CallBaseOnKeyDown(e);
      this.Behavior.OnKeyDown(this.Behavior.FocusedElement, e);
    }

    private void comboBoxElement_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.CallBaseOnKeyPress(e);
      this.Behavior.OnKeyPress(this.Behavior.FocusedElement, e);
    }

    private void comboBoxElement_KeyUp(object sender, KeyEventArgs e)
    {
      this.CallBaseOnKeyUp(e);
      this.Behavior.OnKeyUp(this.Behavior.FocusedElement, e);
    }

    private void comboBoxElement_PopupOpened(object sender, EventArgs e)
    {
      this.OnDropDownOpened(e);
    }

    private void comboBoxElement_PopupClosed(object sender, RadPopupClosedEventArgs e)
    {
      this.OnDropDownClosed(e);
    }

    private void comboBoxElement_PopupClosing(object sender, RadPopupClosingEventArgs args)
    {
      this.OnDropDownClosing(args);
    }

    private void comboBoxElement_PopupOpening(object sender, CancelEventArgs e)
    {
      this.OnDropDownOpening(e);
    }

    private void comboBoxElement_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.OnSelectedIndexChanged(e);
    }

    private void comboBoxElement_SelectedValueChanged(object sender, EventArgs e)
    {
      this.OnSelectedValueChanged(e);
    }

    private void comboBoxElement_SortedChanged(object sender, EventArgs e)
    {
      this.OnSortedChanged(e);
    }

    private void comboBoxElement_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    private void EditControl_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
    {
      this.OnDataBindingComplete(e);
    }

    private void OnDataBindingComplete(GridViewBindingCompleteEventArgs e)
    {
      if (this.DataBindingComplete == null)
        return;
      this.DataBindingComplete((object) this, e);
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      this.ClosePopup();
    }

    protected override void OnParentVisibleChanged(EventArgs e)
    {
      this.ClosePopup();
      base.OnParentVisibleChanged(e);
    }

    protected override void OnParentChanged(EventArgs e)
    {
      this.ClosePopup();
      base.OnParentChanged(e);
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      base.OnBindingContextChanged(e);
      this.EditorControl.BindingContext = this.BindingContext;
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      return (object) element.GetType() == (object) typeof (RadTextBoxElement);
    }

    private void ClosePopup()
    {
      if (this.comboBoxElement == null || !this.comboBoxElement.IsPopupOpen)
        return;
      this.comboBoxElement.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.MultiColumnComboBoxElement.SuspendApplyOfThemeSettings();
      this.MultiColumnComboBoxElement.TextBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.MultiColumnComboBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.MultiColumnComboBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.MultiColumnComboBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "ComboBoxFill");
        this.MultiColumnComboBoxElement.TextBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.MultiColumnComboBoxElement.TextBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "TextBoxFill");
      }
      this.MultiColumnComboBoxElement.ResumeApplyOfThemeSettings();
      this.MultiColumnComboBoxElement.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.MultiColumnComboBoxElement.SuspendApplyOfThemeSettings();
      this.MultiColumnComboBoxElement.TextBoxElement.SuspendApplyOfThemeSettings();
      this.MultiColumnComboBoxElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.MultiColumnComboBoxElement.TextBoxElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num = (int) this.MultiColumnComboBoxElement.TextBoxElement.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.MultiColumnComboBoxElement.ElementTree.ApplyThemeToElementTree();
      this.MultiColumnComboBoxElement.ResumeApplyOfThemeSettings();
      this.MultiColumnComboBoxElement.TextBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.MultiColumnComboBoxElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.MultiColumnComboBoxElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
        this.MultiColumnComboBoxElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.MultiColumnComboBoxElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.MultiColumnComboBoxElement.SuspendApplyOfThemeSettings();
      this.MultiColumnComboBoxElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.MultiColumnComboBoxElement.ElementTree.ApplyThemeToElementTree();
      this.MultiColumnComboBoxElement.ResumeApplyOfThemeSettings();
    }
  }
}
