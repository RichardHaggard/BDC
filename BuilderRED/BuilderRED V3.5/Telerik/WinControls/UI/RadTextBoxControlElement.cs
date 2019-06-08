// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxControlElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Analytics;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RadTextBoxControlElement : ScrollViewElement<TextBoxViewElement>
  {
    private int maxDropDownItemCount = 6;
    private int maxLength = int.MaxValue;
    private ElementVisibility? readOnlyClearButtonVisibility = new ElementVisibility?();
    private ITextBoxInputHandler inputHandler;
    private TextBoxControlCaret caret;
    private bool isReadOnlyCaretVisible;
    private bool acceptsTab;
    private bool acceptsReturn;
    private CharacterCasing characterCasing;
    private ITextBoxNavigator navigator;
    private RadTextBoxListElement listElement;
    private Size dropDownMinSize;
    private Size dropDownMaxSize;
    private RadPopupControlBase autoCompletePopup;
    private RadContextMenu contextMenu;
    private TextBoxWrapPanel nullTextViewElement;
    private RadListDataItemCollection autoCompleteItems;
    private bool showClearButton;
    private StackLayoutPanel buttonsStack;
    private LightVisualButtonElement clearButton;
    private bool showNullText;
    private ImeSupport imeSupport;

    static RadTextBoxControlElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new EditorElementStateManager(), typeof (RadTextBoxControlElement));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = true;
      this.CaptureOnMouseDown = true;
      this.CanFocus = true;
      this.DrawText = false;
      this.dropDownMaxSize = Size.Empty;
      this.dropDownMinSize = Size.Empty;
      int num1 = (int) this.SetDefaultValueOverride(LightVisualElement.DrawBorderProperty, (object) true);
      int num2 = (int) this.SetDefaultValueOverride(LightVisualElement.BorderColorProperty, (object) Color.FromArgb((int) byte.MaxValue, 156, 189, 232));
      int num3 = (int) this.SetDefaultValueOverride(LightVisualElement.BorderGradientStyleProperty, (object) GradientStyles.Solid);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.ViewElement.PropertyChanged += new PropertyChangedEventHandler(this.OnViewElement_PropertyChanged);
      this.ViewElement.VScroller = new TextBoxScroller(this.VScrollBar);
      this.ViewElement.HScroller = new TextBoxScroller(this.HScrollBar);
      this.VScrollBar.NotifyParentOnMouseInput = true;
      this.HScrollBar.NotifyParentOnMouseInput = true;
      this.caret = this.CreateCaret();
      this.caret.RadPropertyChanging += new RadPropertyChangingEventHandler(this.OnCaretRadPropertyChanging);
      this.Children.Add((RadElement) this.caret);
      this.ViewElement.ZIndex = 0;
      this.caret.ZIndex = 1;
      this.VScrollBar.ZIndex = 2;
      this.HScrollBar.ZIndex = 3;
      this.listElement = this.CreateListElement();
      this.listElement.SuggestedTextChanged += new SuggestedTextChangedEventHandler(this.OnSuggestedTextChanged);
      this.autoCompleteItems = new RadListDataItemCollection(this.listElement.DataLayer, (RadListElement) this.listElement, false);
      this.autoCompletePopup = this.CreateAutoCompleteDropDown();
      this.autoCompletePopup.PopupClosed += new RadPopupClosedEventHandler(this.OnAutoCompleteDropDownClosed);
      this.autoCompletePopup.HorizontalPopupAlignment = HorizontalPopupAlignment.LeftToLeft;
      this.autoCompletePopup.VerticalPopupAlignment = VerticalPopupAlignment.TopToBottom;
      this.autoCompletePopup.BindingContext = this.BindingContext;
      TextBoxViewElement viewElement = this.ViewElement;
      viewElement.SelectionPrimitive = new TextBoxSelectionPrimitive(this);
      viewElement.ChildrenChanged += new ChildrenChangedEventHandler(this.OnViewElementChildrenChanged);
      this.buttonsStack = new StackLayoutPanel();
      this.clearButton = this.CreateClearButton();
      this.clearButton.CanFocus = false;
      this.clearButton.Class = "TextBoxClearButton";
      this.clearButton.Click += new EventHandler(this.ClearButton_Click);
      this.buttonsStack.Children.Add((RadElement) this.clearButton);
      this.Children.Add((RadElement) this.buttonsStack);
      this.navigator = (ITextBoxNavigator) new TextBoxNavigator(this);
      this.inputHandler = (ITextBoxInputHandler) new TextBoxInputHandler(this);
      viewElement.TextChanging += new TextChangingEventHandler(this.OnViewElementTextChanging);
      viewElement.TextChanged += new EventHandler(this.OnViewElementTextChanged);
    }

    protected virtual TextBoxWrapPanel CreateNullTextViewElement()
    {
      return new TextBoxWrapPanel();
    }

    protected virtual LightVisualButtonElement CreateClearButton()
    {
      LightVisualButtonElement visualButtonElement = new LightVisualButtonElement();
      visualButtonElement.Padding = new Padding(4, 2, 4, 0);
      return visualButtonElement;
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.imeSupport = new ImeSupport(this);
      this.SetClearButtonVisibility();
    }

    protected override void OnUnloaded(ComponentThemableElementTree oldTree)
    {
      base.OnUnloaded(oldTree);
      if (this.imeSupport == null)
        return;
      this.imeSupport.Dispose();
      this.imeSupport = (ImeSupport) null;
    }

    protected override void DisposeUnmanagedResources()
    {
      base.DisposeUnmanagedResources();
      if (this.imeSupport == null)
        return;
      this.imeSupport.Dispose();
      this.imeSupport = (ImeSupport) null;
    }

    public override string Text
    {
      get
      {
        return this.ViewElement.Text;
      }
      set
      {
        if (!(this.ViewElement.Text != value))
          return;
        this.navigator.CaretPosition = this.navigator.GetPositionFromPoint(PointF.Empty);
        this.ViewElement.Text = value;
      }
    }

    public string NullText
    {
      get
      {
        return this.NullTextViewElement.Text;
      }
      set
      {
        this.NullTextViewElement.Text = value;
      }
    }

    public Color NullTextColor
    {
      get
      {
        return this.NullTextViewElement.ForeColor;
      }
      set
      {
        this.NullTextViewElement.ForeColor = value;
      }
    }

    [Category("Appearance")]
    [Browsable(false)]
    [Description("Gets or sets a value indicating whether the null text will be shown when the control is focused and the text is empty")]
    public bool ShowNullText
    {
      get
      {
        return this.showNullText;
      }
      set
      {
        this.showNullText = value;
      }
    }

    public string[] Lines
    {
      get
      {
        return this.GetLines();
      }
      set
      {
        this.SetLines(value);
      }
    }

    public string SelectedText
    {
      get
      {
        TextPosition selectionStart = this.navigator.SelectionStart;
        TextPosition selectionEnd = this.navigator.SelectionEnd;
        if (selectionStart != (TextPosition) null && selectionEnd != (TextPosition) null)
          return this.ViewElement.GetTextRange(selectionStart, selectionEnd);
        return string.Empty;
      }
      set
      {
        TextBoxViewElement viewElement = this.ViewElement;
        TextPosition textPosition1 = this.navigator.SelectionStart;
        if ((object) textPosition1 == null)
          textPosition1 = TextPosition.GetFirstPosition((TextBoxWrapPanel) viewElement);
        TextPosition startPosition = textPosition1;
        TextPosition textPosition2 = this.navigator.SelectionEnd;
        if ((object) textPosition2 == null)
          textPosition2 = startPosition;
        TextPosition endPosition = textPosition2;
        if (!(this.SelectedText != value))
          return;
        viewElement.Replace(startPosition, endPosition, value);
      }
    }

    public int MaxLength
    {
      get
      {
        return this.maxLength;
      }
      set
      {
        this.maxLength = value;
      }
    }

    public bool AcceptsTab
    {
      get
      {
        return this.acceptsTab;
      }
      set
      {
        this.acceptsTab = value;
      }
    }

    public bool AcceptsReturn
    {
      get
      {
        return this.acceptsReturn;
      }
      set
      {
        this.acceptsReturn = value;
      }
    }

    public bool Multiline
    {
      get
      {
        return this.ViewElement.Multiline;
      }
      set
      {
        this.ViewElement.Multiline = value;
        this.NullTextViewElement.Multiline = value;
      }
    }

    public virtual bool UseSystemPasswordChar
    {
      get
      {
        return this.ViewElement.UseSystemPasswordChar;
      }
      set
      {
        this.ViewElement.UseSystemPasswordChar = value;
      }
    }

    public virtual char PasswordChar
    {
      get
      {
        return this.ViewElement.PasswordChar;
      }
      set
      {
        this.ViewElement.PasswordChar = value;
      }
    }

    public bool WordWrap
    {
      get
      {
        return this.ViewElement.WordWrap;
      }
      set
      {
        this.ViewElement.WordWrap = value;
        this.NullTextViewElement.WordWrap = value;
      }
    }

    public HorizontalAlignment TextAlign
    {
      get
      {
        return this.ViewElement.TextAlign;
      }
      set
      {
        this.ViewElement.TextAlign = value;
        this.NullTextViewElement.TextAlign = value;
      }
    }

    public int TextLength
    {
      get
      {
        return this.ViewElement.TextLength;
      }
    }

    public int CaretIndex
    {
      get
      {
        return this.SelectionStart;
      }
      set
      {
        this.Select(value, 0);
      }
    }

    public int SelectionStart
    {
      get
      {
        if (this.navigator.SelectionEnd != (TextPosition) null)
          return Math.Min((int) this.navigator.SelectionStart, (int) this.navigator.SelectionEnd);
        return (int) this.navigator.SelectionStart;
      }
      set
      {
        value = this.ClampOffset(value);
        int length = this.ClampOffset(value + this.SelectionLength) - value;
        this.Select(value, length);
      }
    }

    public int SelectionLength
    {
      get
      {
        return this.navigator.SelectionLength;
      }
      set
      {
        int selectionStart = this.SelectionStart;
        this.Select(this.SelectionStart, this.ClampOffset(selectionStart + value) - selectionStart);
      }
    }

    public Color SelectionColor
    {
      get
      {
        return this.ViewElement.SelectionPrimitive.SelectionColor;
      }
      set
      {
        this.ViewElement.SelectionPrimitive.SelectionColor = value;
      }
    }

    public int SelectionOpacity
    {
      get
      {
        return this.ViewElement.SelectionPrimitive.SelectionOpacity;
      }
      set
      {
        this.ViewElement.SelectionPrimitive.SelectionOpacity = value;
      }
    }

    public CharacterCasing CharacterCasing
    {
      get
      {
        return this.characterCasing;
      }
      set
      {
        this.characterCasing = value;
      }
    }

    public TextBoxControlCaret Caret
    {
      get
      {
        return this.caret;
      }
    }

    public ITextBoxInputHandler InputHandler
    {
      get
      {
        return this.inputHandler;
      }
      set
      {
        this.inputHandler = value;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return this.ViewElement.IsReadOnly;
      }
      set
      {
        this.ViewElement.IsReadOnly = value;
      }
    }

    public bool IsReadOnlyCaretVisible
    {
      get
      {
        return this.isReadOnlyCaretVisible;
      }
      set
      {
        if (this.isReadOnlyCaretVisible == value)
          return;
        this.isReadOnlyCaretVisible = value;
        this.OnNotifyPropertyChanged(nameof (IsReadOnlyCaretVisible));
      }
    }

    public bool HideSelection
    {
      get
      {
        return this.ViewElement.SelectionPrimitive.HideSelection;
      }
      set
      {
        this.ViewElement.SelectionPrimitive.HideSelection = value;
      }
    }

    public RadContextMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        if (this.contextMenu == value)
          return;
        if (this.contextMenu is TextBoxControlDefaultContextMenu)
          this.contextMenu.Dispose();
        this.contextMenu = value;
      }
    }

    public ITextBoxNavigator Navigator
    {
      get
      {
        return this.navigator;
      }
      set
      {
        if (value == this.navigator)
          return;
        (this.navigator as IDisposable)?.Dispose();
        this.navigator = value;
      }
    }

    public RadTextBoxListElement ListElement
    {
      get
      {
        return this.listElement;
      }
    }

    public TextBoxWrapPanel NullTextViewElement
    {
      get
      {
        if (this.nullTextViewElement == null)
        {
          this.nullTextViewElement = this.CreateNullTextViewElement();
          int num = (int) this.nullTextViewElement.SetDefaultValueOverride(VisualElement.ForeColorProperty, (object) SystemColors.GrayText);
          this.nullTextViewElement.TextChanged += new EventHandler(this.OnNullTextChanged);
          this.Children.Insert(1, (RadElement) this.nullTextViewElement);
        }
        return this.nullTextViewElement;
      }
    }

    public AutoCompleteMode AutoCompleteMode
    {
      get
      {
        return this.listElement.AutoCompleteMode;
      }
      set
      {
        this.listElement.AutoCompleteMode = value;
      }
    }

    public string AutoCompleteDisplayMember
    {
      get
      {
        return this.listElement.DisplayMember;
      }
      set
      {
        this.listElement.DisplayMember = value;
      }
    }

    public object AutoCompleteDataSource
    {
      get
      {
        return this.listElement.DataSource;
      }
      set
      {
        this.listElement.DataSource = value;
      }
    }

    public RadListDataItemCollection AutoCompleteItems
    {
      get
      {
        return this.autoCompleteItems;
      }
    }

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
        this.ClampDropDownMaxSize();
      }
    }

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
        this.ClampDropDownMinSize();
      }
    }

    public int MaxDropDownItemCount
    {
      get
      {
        return this.maxDropDownItemCount;
      }
      set
      {
        if (this.maxDropDownItemCount == value)
          return;
        this.maxDropDownItemCount = value;
      }
    }

    [Browsable(false)]
    public bool IsAutoCompleteDropDownOpen
    {
      get
      {
        if (this.autoCompletePopup != null)
          return PopupManager.Default.ContainsPopup((IPopupControl) this.autoCompletePopup);
        return false;
      }
    }

    public ScrollState VerticalScrollBarState
    {
      get
      {
        return this.ViewElement.VScroller.ScrollState;
      }
      set
      {
        this.ViewElement.VScroller.ScrollState = value;
      }
    }

    public ScrollState HorizontalScrollBarState
    {
      get
      {
        return this.ViewElement.HScroller.ScrollState;
      }
      set
      {
        this.ViewElement.HScroller.ScrollState = value;
      }
    }

    protected internal virtual bool CanPerformAutoComplete
    {
      get
      {
        if (!this.Multiline)
          return this.AutoCompleteMode != AutoCompleteMode.None;
        return false;
      }
    }

    public RadPopupControlBase AutoCompleteDropDown
    {
      get
      {
        return this.autoCompletePopup;
      }
    }

    public LightVisualButtonElement ClearButton
    {
      get
      {
        return this.clearButton;
      }
    }

    [DefaultValue(false)]
    public bool ShowClearButton
    {
      get
      {
        return this.showClearButton;
      }
      set
      {
        this.showClearButton = value;
        this.SetClearButtonVisibility();
      }
    }

    public event TextBlockFormattingEventHandler TextBlockFormatting
    {
      add
      {
        this.ViewElement.TextBlockFormatting += value;
      }
      remove
      {
        this.ViewElement.TextBlockFormatting -= value;
      }
    }

    public event CreateTextBlockEventHandler CreateTextBlock
    {
      add
      {
        this.ViewElement.CreateTextBlock += value;
      }
      remove
      {
        this.ViewElement.CreateTextBlock -= value;
      }
    }

    public event TreeBoxContextMenuOpeningEventHandler ContextMenuOpening;

    protected internal bool OnContextMenuOpenting(RadContextMenu menu)
    {
      TreeBoxContextMenuOpeningEventArgs e = new TreeBoxContextMenuOpeningEventArgs(menu);
      this.OnContextMenuOpenting(e);
      return !e.Cancel;
    }

    protected virtual void OnContextMenuOpenting(TreeBoxContextMenuOpeningEventArgs e)
    {
      TreeBoxContextMenuOpeningEventHandler contextMenuOpening = this.ContextMenuOpening;
      if (contextMenuOpening == null)
        return;
      contextMenuOpening((object) this, e);
    }

    public event SelectionChangingEventHandler SelectionChanging
    {
      add
      {
        this.navigator.SelectionChanging += value;
      }
      remove
      {
        this.navigator.SelectionChanging -= value;
      }
    }

    public event SelectionChangedEventHandler SelectionChanged
    {
      add
      {
        this.navigator.SelectionChanged += value;
      }
      remove
      {
        this.navigator.SelectionChanged -= value;
      }
    }

    public event EventHandler IMECompositionStarted;

    public event EventHandler IMECompositionEnded;

    public event EventHandler<IMECompositionResultEventArgs> IMECompositionResult;

    protected internal override bool IsInputKey(InputKeyEventArgs e)
    {
      Keys keys = e.Keys;
      if ((keys & Keys.Alt) != Keys.Alt)
      {
        switch (keys & Keys.KeyCode)
        {
          case Keys.Tab:
          case Keys.Prior:
          case Keys.Next:
          case Keys.End:
          case Keys.Home:
          case Keys.Left:
          case Keys.Up:
          case Keys.Right:
          case Keys.Down:
            e.Handled = true;
            return true;
          case Keys.Return:
            if (this.Multiline)
            {
              e.Handled = true;
              return this.acceptsReturn;
            }
            break;
        }
      }
      return base.IsInputKey(e);
    }

    protected override void OnBoundsChanged(RadPropertyChangedEventArgs e)
    {
      base.OnBoundsChanged(e);
      this.CloseDropDown();
    }

    protected internal virtual void OnIMECompositionStarted()
    {
      if (this.IMECompositionStarted == null)
        return;
      this.IMECompositionStarted((object) this, EventArgs.Empty);
    }

    protected internal virtual void OnIMECompositionEnded()
    {
      if (this.IMECompositionEnded == null)
        return;
      this.IMECompositionEnded((object) this, EventArgs.Empty);
    }

    protected internal virtual void OnIMECompositionResult(string result)
    {
      if (this.IMECompositionResult == null)
        return;
      this.IMECompositionResult((object) this, new IMECompositionResultEventArgs(result));
    }

    private void OnAutoCompleteDropDownClosed(object sender, RadPopupClosedEventArgs e)
    {
      this.OnAutoCompleteDropDownClosed(e as RadAutoCompleteDropDownClosedEventArgs);
    }

    protected virtual void OnAutoCompleteDropDownClosed(RadAutoCompleteDropDownClosedEventArgs e)
    {
      if (e.CloseReason != RadPopupCloseReason.Mouse || e.InputArguments == null)
        return;
      this.SelectAll();
    }

    private void OnNullTextChanged(object sender, EventArgs e)
    {
      this.SetNullTextVisibility();
    }

    private void OnViewElementTextChanging(object sender, TextChangingEventArgs e)
    {
      this.OnTextChanging(e);
    }

    private void OnViewElementTextChanged(object sender, EventArgs e)
    {
      this.SetNullTextVisibility();
      this.OnTextChanged(e);
      this.SetClearButtonVisibility();
    }

    private void OnSuggestedTextChanged(object sender, SuggestedTextChangedEventArgs e)
    {
      if (!this.CanPerformAutoComplete || e.Action == AutoCompleteAction.None || string.IsNullOrEmpty(e.SuggestedText))
        return;
      this.HandleSuggestedTextChanged(e);
    }

    protected virtual void HandleSuggestedTextChanged(SuggestedTextChangedEventArgs e)
    {
      if (e.Action == AutoCompleteAction.Append && !this.CanInsertText(e.SuggestedText))
        return;
      string text = e.SuggestedText;
      if (this.CharacterCasing == CharacterCasing.Upper)
        text = text.ToUpper();
      else if (this.CharacterCasing == CharacterCasing.Lower)
        text = text.ToLower();
      this.ViewElement.Replace(e.StartPosition, e.EndPosition, text);
      TextPosition completePosition1 = this.GetFirstAutoCompletePosition();
      TextPosition completePosition2 = this.GetLastAutoCompletePosition();
      if (e.Action == AutoCompleteAction.Append)
        this.navigator.Select(this.navigator.GetPositionFromOffset((int) completePosition1 + e.Text.Length), completePosition2);
      else
        this.navigator.CaretPosition = completePosition2;
      e.StartPosition = completePosition1;
      e.EndPosition = completePosition2;
    }

    protected internal virtual TextPosition GetFirstAutoCompletePosition()
    {
      return TextPosition.GetFirstPosition((TextBoxWrapPanel) this.ViewElement);
    }

    protected internal virtual TextPosition GetLastAutoCompletePosition()
    {
      return TextPosition.GetLastPosition((TextBoxWrapPanel) this.ViewElement);
    }

    protected override void ToggleTextPrimitive(RadProperty property)
    {
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property == RadElement.ContainsFocusProperty)
      {
        if (this.ContainsFocus)
        {
          this.Caret.Show();
        }
        else
        {
          this.Caret.Hide();
          if (!this.AutoCompleteDropDown.ContainsFocus)
            this.CloseDropDown();
        }
        this.SetNullTextVisibility();
        this.UpdateFocusBorder(this.ContainsFocus);
      }
      else
      {
        if (e.Property != RadObject.BindingContextProperty)
          return;
        this.AutoCompleteDropDown.BindingContext = (BindingContext) e.NewValue;
      }
    }

    private void OnViewElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "IsReadOnly") || !this.ShowClearButton)
        return;
      if (this.IsReadOnly)
      {
        this.readOnlyClearButtonVisibility = new ElementVisibility?(this.ClearButton.Visibility);
        this.ClearButton.Visibility = ElementVisibility.Collapsed;
      }
      else
      {
        if (!this.readOnlyClearButtonVisibility.HasValue)
          return;
        this.ClearButton.Visibility = this.readOnlyClearButtonVisibility.Value;
        this.readOnlyClearButtonVisibility = new ElementVisibility?();
      }
    }

    private void OnCaretRadPropertyChanging(object sender, RadPropertyChangingEventArgs e)
    {
      if (e.Property != RadElement.VisibilityProperty || !object.Equals(e.NewValue, (object) ElementVisibility.Visible))
        return;
      e.Cancel = !this.ContainsFocus || this.navigator.SelectionEnd != (TextPosition) null || this.navigator.SelectionStart == (TextPosition) null;
      if (e.Cancel || !this.IsReadOnly)
        return;
      e.Cancel = !this.IsReadOnlyCaretVisible;
    }

    private void OnViewElementChildrenChanged(object sender, ChildrenChangedEventArgs e)
    {
      if (e.ChangeOperation != ItemsChangeOperation.Cleared)
        return;
      this.navigator.Select((TextPosition) null, (TextPosition) null);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.inputHandler != null)
        this.inputHandler.ProcessKeyDown(e);
      base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (this.inputHandler != null)
        this.inputHandler.ProcessKeyUp(e);
      base.OnKeyUp(e);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
      if (this.inputHandler == null || e.Handled)
        return;
      this.inputHandler.ProcessKeyPress(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (this.inputHandler != null)
        this.inputHandler.ProcessMouseDown(e);
      base.OnMouseDown(e);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.inputHandler != null)
        this.inputHandler.ProcessMouseUp(e);
      base.OnMouseUp(e);
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      if (this.inputHandler != null)
        this.inputHandler.ProcessDoubleClick(e);
      base.OnDoubleClick(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      if (this.inputHandler != null)
        this.inputHandler.ProcessMouseMove(e);
      base.OnMouseMove(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      if (this.inputHandler != null)
        this.inputHandler.ProcessMouseLeave(e);
      base.OnMouseLeave(e);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.inputHandler != null)
        this.inputHandler.PrcessMouseEnter(e);
      base.OnMouseEnter(e);
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
      if (handledMouseEventArgs != null && handledMouseEventArgs.Handled || this.inputHandler == null)
        return;
      this.inputHandler.ProcessMouseWheel(e);
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
      this.Clear();
    }

    protected int ClampOffset(int offset)
    {
      if (offset > this.TextLength)
        offset = this.TextLength;
      return offset;
    }

    protected override void PaintText(IGraphics graphics)
    {
    }

    private void SetNullTextVisibility()
    {
      if (this.nullTextViewElement == null)
        return;
      if ((!this.ContainsFocus || this.ShowNullText) && (this.nullTextViewElement.HasText() && !this.ViewElement.HasText()))
        this.nullTextViewElement.Visibility = ElementVisibility.Visible;
      else
        this.nullTextViewElement.Visibility = ElementVisibility.Hidden;
    }

    private void SetClearButtonVisibility()
    {
      if (this.clearButton == null)
        return;
      ElementVisibility elementVisibility = this.ShowClearButton ? (!this.ViewElement.HasText() ? ElementVisibility.Hidden : ElementVisibility.Visible) : ElementVisibility.Collapsed;
      if (this.IsReadOnly && this.ShowClearButton)
        this.readOnlyClearButtonVisibility = new ElementVisibility?(elementVisibility);
      else
        this.clearButton.Visibility = elementVisibility;
    }

    protected virtual bool CanInsertText(string text)
    {
      return this.TextLength - this.SelectedText.Length + text.Length <= this.maxLength || this.maxLength <= 0;
    }

    protected virtual bool IsValidAutoCompletePosition()
    {
      if (this.SelectionStart != this.TextLength)
        return this.SelectionLength != 0;
      return true;
    }

    protected void PerformAutoComplete(EditOperation context)
    {
      if (!this.CanPerformAutoComplete)
        return;
      this.PerformAutoCompleteOverride(context);
      if (this.ElementTree == null || this.ElementTree.Control == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(this.ElementTree.Control as RadControl, "AutoCompleted", (object) context);
    }

    protected virtual void PerformAutoCompleteOverride(EditOperation context)
    {
      AutoCompleteMode autoCompleteMode = this.AutoCompleteMode;
      ITextBoxNavigator navigator = this.Navigator;
      bool notify = this.IsValidAutoCompletePosition() && (context == EditOperation.InsertText || context == EditOperation.ReplaceText);
      RadTextBoxListElement listElement = this.ListElement;
      TextPosition completePosition1 = this.GetFirstAutoCompletePosition();
      TextPosition completePosition2 = this.GetLastAutoCompletePosition();
      string pattern = string.Empty;
      if (completePosition1 != (TextPosition) null && completePosition2 != (TextPosition) null)
        pattern = this.GetAutoCompleteTextCondition(completePosition1, completePosition2);
      listElement.Suggest(pattern, completePosition1, completePosition2, notify);
      if (listElement.Items.Count == 0 || listElement.IsSuggestionMatched)
      {
        this.CloseDropDown();
      }
      else
      {
        if (autoCompleteMode != AutoCompleteMode.Suggest && autoCompleteMode != AutoCompleteMode.SuggestAppend)
          return;
        this.ShowDropDown(this.GetAutoCompleteDropDownLocation());
      }
    }

    protected virtual string GetAutoCompleteTextCondition(
      TextPosition startPosition,
      TextPosition endPosition)
    {
      return this.ViewElement.GetTextRange(startPosition, endPosition);
    }

    protected virtual Point GetAutoCompleteDropDownLocation()
    {
      Point screen = this.ElementTree.Control.PointToScreen(this.ControlBoundingRectangle.Location);
      screen.Y += this.ControlBoundingRectangle.Height;
      return screen;
    }

    protected virtual string[] GetLines()
    {
      string text = this.Text;
      List<string> stringList = new List<string>();
      int index;
      for (int startIndex = 0; startIndex < text.Length; startIndex = index)
      {
        for (index = startIndex; index < text.Length; ++index)
        {
          switch (text[index])
          {
            case '\n':
            case '\r':
              goto label_5;
            default:
              continue;
          }
        }
label_5:
        string str = text.Substring(startIndex, index - startIndex);
        stringList.Add(str);
        if (index < text.Length && text[index] == '\r')
          ++index;
        if (index < text.Length && text[index] == '\n')
          ++index;
      }
      if (text.Length > 0)
      {
        switch (text[text.Length - 1])
        {
          case '\n':
          case '\r':
            stringList.Add(string.Empty);
            break;
        }
      }
      return stringList.ToArray();
    }

    protected virtual void SetLines(string[] value)
    {
      if (value != null && value.Length > 0)
      {
        StringBuilder stringBuilder = new StringBuilder(value[0]);
        for (int index = 1; index < value.Length; ++index)
        {
          stringBuilder.Append("\r\n");
          stringBuilder.Append(value[index]);
        }
        this.Text = stringBuilder.ToString();
      }
      else
        this.Text = string.Empty;
    }

    private void ClampDropDownMinSize()
    {
      if (!(this.dropDownMaxSize != Size.Empty))
        return;
      if (this.dropDownMinSize.Width > this.dropDownMaxSize.Width)
        this.dropDownMaxSize.Width = this.dropDownMinSize.Width;
      if (this.dropDownMinSize.Height <= this.dropDownMaxSize.Height)
        return;
      this.dropDownMaxSize.Height = this.dropDownMinSize.Height;
    }

    private void ClampDropDownMaxSize()
    {
      if (!(this.dropDownMinSize != Size.Empty))
        return;
      if (this.dropDownMaxSize.Width < this.dropDownMinSize.Width)
        this.dropDownMinSize.Width = this.dropDownMaxSize.Width;
      if (this.dropDownMaxSize.Height >= this.dropDownMinSize.Height)
        return;
      this.dropDownMinSize.Height = this.dropDownMaxSize.Height;
    }

    protected virtual TextBoxControlCaret CreateCaret()
    {
      return new TextBoxControlCaret();
    }

    protected virtual RadTextBoxListElement CreateListElement()
    {
      return new RadTextBoxListElement();
    }

    protected virtual RadPopupControlBase CreateAutoCompleteDropDown()
    {
      return (RadPopupControlBase) new RadTextBoxAutoCompleteDropDown(this);
    }

    protected virtual Size GetDropDownSize()
    {
      int num1 = this.listElement.Items.Count;
      if (num1 > this.maxDropDownItemCount)
        num1 = this.maxDropDownItemCount;
      RadSizablePopupControl autoCompletePopup = this.autoCompletePopup as RadSizablePopupControl;
      int num2 = 0;
      if (autoCompletePopup.SizingGrip.Visibility == ElementVisibility.Visible)
      {
        RadElement sizingGrip = (RadElement) autoCompletePopup.SizingGrip;
        num2 = sizingGrip.BoundingRectangle.Height + sizingGrip.Margin.Size.Height;
      }
      int num3 = (this.listElement.Items as RadListDataItemCollection).First is DescriptionTextListDataItem ? 36 : 18;
      int itemSpacing = this.listElement.Scroller.ItemSpacing;
      return new Size(this.Size.Width, num1 * (num3 + itemSpacing) + num2 + (int) ((double) this.listElement.BorderWidth * 2.0));
    }

    public virtual void ShowDropDown(Point location)
    {
      if (!this.ContainsFocus)
        return;
      RadPopupControlBase completeDropDown = this.AutoCompleteDropDown;
      if (this.RightToLeft)
        completeDropDown.HorizontalPopupAlignment = HorizontalPopupAlignment.RightToRight;
      if (completeDropDown.ElementTree.RootElement.ElementState != ElementState.Loaded)
        completeDropDown.LoadElementTree();
      if (this.DropDownMinSize != Size.Empty)
        completeDropDown.MinimumSize = this.DropDownMinSize;
      if (this.DropDownMaxSize != Size.Empty)
        completeDropDown.MaximumSize = this.DropDownMaxSize;
      PopupEditorBaseElement.ApplyThemeToPopup((RadElementTree) this.ElementTree, completeDropDown);
      completeDropDown.Size = this.GetDropDownSize();
      this.AutoCompleteDropDown.ShowPopup(new Rectangle(location, Size.Empty));
    }

    public void CloseDropDown()
    {
      this.CloseDropDown(RadPopupCloseReason.CloseCalled);
    }

    public virtual void CloseDropDown(RadPopupCloseReason reason)
    {
      if (this.autoCompletePopup == null)
        return;
      this.autoCompletePopup.ClosePopup(reason);
    }

    public virtual bool Cut()
    {
      if (!this.Copy())
        return false;
      this.Delete();
      return true;
    }

    public virtual bool Copy()
    {
      string selectedText = this.SelectedText;
      if (string.IsNullOrEmpty(selectedText))
        return false;
      Clipboard.SetText(selectedText, TextDataFormat.UnicodeText);
      return true;
    }

    public virtual bool Paste()
    {
      if (!Clipboard.ContainsText())
        return false;
      this.Insert(Clipboard.GetText());
      return true;
    }

    public virtual bool Insert(string text)
    {
      ITextBoxNavigator navigator = this.Navigator;
      TextPosition textPosition1 = navigator.SelectionStart;
      if ((object) textPosition1 == null)
        textPosition1 = TextPosition.GetFirstPosition((TextBoxWrapPanel) this.ViewElement);
      TextPosition startPosition = textPosition1;
      TextPosition textPosition2 = navigator.SelectionEnd;
      if ((object) textPosition2 == null)
        textPosition2 = startPosition;
      TextPosition endPosition = textPosition2;
      EditOperation context = EditOperation.InsertText;
      bool flag = true;
      if (object.Equals((object) startPosition, (object) endPosition))
      {
        if (string.IsNullOrEmpty(text) || !this.CanInsertText(text))
          return false;
        if (startPosition.CharPosition == 1 && TextBoxWrapPanel.IsCarriageReturn(startPosition.TextBlock.Text))
          startPosition.CharPosition = 0;
        flag = this.ViewElement.Insert(startPosition, text);
      }
      else
      {
        TextPosition.Swap(ref startPosition, ref endPosition);
        context = EditOperation.ReplaceText;
        if (!this.CanInsertText(text))
          text = string.Empty;
        if (!this.ViewElement.Replace(startPosition, endPosition, text))
        {
          navigator.CaretPosition = endPosition;
          flag = false;
        }
      }
      this.PerformAutoComplete(context);
      return flag;
    }

    public bool Delete()
    {
      return this.Delete(false);
    }

    public virtual bool Delete(bool nextCharacter)
    {
      ITextBoxNavigator navigator = this.Navigator;
      TextBoxViewElement viewElement = this.ViewElement;
      TextPosition textPosition1 = navigator.SelectionStart;
      if ((object) textPosition1 == null)
        textPosition1 = TextPosition.GetFirstPosition((TextBoxWrapPanel) this.ViewElement);
      TextPosition textPosition2 = textPosition1;
      TextPosition endPosition = navigator.SelectionEnd;
      EditOperation context = EditOperation.DeleteText;
      if (endPosition == (TextPosition) null || object.Equals((object) textPosition2, (object) endPosition))
      {
        textPosition2 = nextCharacter ? navigator.GetNextPosition(textPosition2) : navigator.GetPreviousPosition(textPosition2);
        endPosition = navigator.SelectionStart;
      }
      if (!viewElement.Delete(textPosition2, endPosition))
        return false;
      this.PerformAutoComplete(context);
      return true;
    }

    public void AppendText(string text)
    {
      this.AppendText(text, false);
    }

    public void AppendText(string text, bool select)
    {
      int textLength = this.TextLength;
      TextBoxViewElement viewElement = this.ViewElement;
      TextPosition lastPosition = TextPosition.GetLastPosition((TextBoxWrapPanel) viewElement);
      if (lastPosition == (TextPosition) null)
      {
        this.Text = text;
        if (!select)
          return;
        this.SelectAll();
      }
      else
      {
        viewElement.Replace(lastPosition, lastPosition, text);
        if (!select)
          return;
        this.navigator.Select(this.navigator.GetPositionFromOffset(textLength), this.navigator.CaretPosition);
      }
    }

    public void Select(int start, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length));
      this.navigator.Select(this.navigator.GetPositionFromOffset(start), length > 0 ? this.navigator.GetPositionFromOffset(start + length) : (TextPosition) null);
    }

    public void SelectAll()
    {
      this.Select(0, this.TextLength);
    }

    public virtual bool DeselectAll()
    {
      this.navigator.SelectionEnd = (TextPosition) null;
      return true;
    }

    public void ScrollToCaret()
    {
      this.navigator.ScrollToCaret();
    }

    public void Clear()
    {
      this.ViewElement.Clear();
      this.CaretIndex = 0;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      this.buttonsStack.Measure(availableSize);
      availableSize.Width -= this.buttonsStack.DesiredSize.Width;
      SizeF sizeF = base.MeasureOverride(availableSize);
      return new SizeF(sizeF.Width + this.buttonsStack.DesiredSize.Width, Math.Max(sizeF.Height, this.buttonsStack.DesiredSize.Height));
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF finalRect = new RectangleF(clientRectangle.Location, new SizeF(this.buttonsStack.DesiredSize.Width, clientRectangle.Height));
      if (!this.RightToLeft)
        finalRect.Location = PointF.Add(clientRectangle.Location, new SizeF(clientRectangle.Width - this.buttonsStack.DesiredSize.Width, 0.0f));
      this.buttonsStack.Arrange(finalRect);
      return base.ArrangeOverride(finalSize);
    }

    protected override void MeasureViewElement(SizeF availableSize)
    {
      base.MeasureViewElement(availableSize);
      if (this.nullTextViewElement == null)
        return;
      this.nullTextViewElement.Measure(availableSize);
    }

    protected override void ArrangeViewElement(RectangleF viewElementRect)
    {
      if (this.RightToLeft)
        viewElementRect.Location = PointF.Add(viewElementRect.Location, new SizeF(this.buttonsStack.DesiredSize.Width, 0.0f));
      viewElementRect.Width -= this.buttonsStack.DesiredSize.Width;
      base.ArrangeViewElement(viewElementRect);
      if (this.nullTextViewElement == null)
        return;
      this.nullTextViewElement.Arrange(viewElementRect);
    }
  }
}
