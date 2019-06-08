// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTextBoxElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class RadTextBoxElement : RadEditorElement
  {
    private Padding defaultPadding = new Padding(2, 2, 2, 3);
    private ElementVisibility? readOnlyClearButtonVisibility = new ElementVisibility?();
    public static RadProperty NullTextColorProperty = RadProperty.Register(nameof (NullTextColor), typeof (Color), typeof (RadTextBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.GrayText, ElementPropertyOptions.AffectsDisplay));
    private static readonly object AcceptsTabChangedEventKey = new object();
    private static readonly object HideSelectionChangedEventKey = new object();
    private static readonly object ModifiedChangedEventKey = new object();
    private static readonly object MultilineChangedEventKey = new object();
    private static readonly object ReadOnlyChangedEventKey = new object();
    private static readonly object TextAlignChangedEventKey = new object();
    private static readonly object TextChangingEventKey = new object();
    private static readonly object TextChangedEventKey = new object();
    private RadTextBoxItem textBoxItem;
    private FillPrimitive fillPrimitive;
    private BorderPrimitive borderPrimitive;
    private StackLayoutPanel buttonsStack;
    private LightVisualButtonElement clearButton;
    private bool showBorder;
    private bool showClearButton;

    static RadTextBoxElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RadTextBoxElementStateManager(), typeof (RadTextBoxElement));
      RadElement.CanFocusProperty.OverrideMetadata(typeof (RadTextBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.AffectsDisplay));
    }

    public RadTextBoxElement()
      : this(new RadTextBoxItem())
    {
    }

    public RadTextBoxElement(RadTextBoxItem textBoxItem)
    {
      this.InitializeTextBoxItem(textBoxItem);
      int num1 = (int) this.BindProperty(RadTextBoxItem.IsNullTextProperty, (RadObject) this.textBoxItem, RadTextBoxItem.IsNullTextProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.textBoxItem.BindProperty(RadTextBoxItem.NullTextColorProperty, (RadObject) this, RadTextBoxElement.NullTextColorProperty, PropertyBindingOptions.TwoWay);
    }

    protected override void CreateChildElements()
    {
      this.fillPrimitive = new FillPrimitive();
      this.fillPrimitive.Class = "TextBoxFill";
      this.Children.Add((RadElement) this.fillPrimitive);
      this.borderPrimitive = new BorderPrimitive();
      this.borderPrimitive.Class = "TextBoxBorder";
      this.Children.Add((RadElement) this.borderPrimitive);
      this.buttonsStack = new StackLayoutPanel();
      this.Children.Add((RadElement) this.buttonsStack);
      this.clearButton = this.CreateClearButton();
      this.clearButton.Class = "TextBoxClearButton";
      this.clearButton.Click += new EventHandler(this.ClearButton_Click);
      this.buttonsStack.Children.Add((RadElement) this.clearButton);
    }

    protected virtual LightVisualButtonElement CreateClearButton()
    {
      LightVisualButtonElement visualButtonElement = new LightVisualButtonElement();
      visualButtonElement.Padding = new Padding(4, 2, 4, 0);
      return visualButtonElement;
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.NotifyParentOnMouseInput = true;
      this.StretchHorizontally = true;
      this.StretchVertically = false;
    }

    private void InitializeTextBoxItem(RadTextBoxItem textBoxItem)
    {
      this.textBoxItem = textBoxItem;
      int num = (int) this.textBoxItem.BindProperty(RadItem.TextProperty, (RadObject) this, RadItem.TextProperty, PropertyBindingOptions.TwoWay);
      this.textBoxItem.AcceptsTabChanged += new EventHandler(this.textBoxItem_AcceptsTabChanged);
      this.textBoxItem.HideSelectionChanged += new EventHandler(this.textBoxItem_HideSelectionChanged);
      this.textBoxItem.ModifiedChanged += new EventHandler(this.textBoxItem_ModifiedChanged);
      this.textBoxItem.MultilineChanged += new EventHandler(this.textBoxItem_MultilineChanged);
      this.textBoxItem.ReadOnlyChanged += new EventHandler(this.textBoxItem_ReadOnlyChanged);
      this.textBoxItem.TextAlignChanged += new EventHandler(this.textBoxItem_TextAlignChanged);
      this.textBoxItem.TextChanged += new EventHandler(this.textBoxItem_TextChanged);
      this.textBoxItem.TextChanging += new TextChangingEventHandler(this.textBoxItem_TextChanging);
      this.textBoxItem.KeyDown += new KeyEventHandler(this.textBoxItem_KeyDown);
      this.textBoxItem.KeyUp += new KeyEventHandler(this.textBoxItem_KeyUp);
      this.textBoxItem.KeyPress += new KeyPressEventHandler(this.textBoxItem_KeyPress);
      this.textBoxItem.GotFocus += new EventHandler(this.TextBoxItem_GotFocus);
      this.textBoxItem.LostFocus += new EventHandler(this.TextBoxItem_LostFocus);
      this.Children.Insert(0, (RadElement) this.textBoxItem);
    }

    public HorizontalAlignment TextAlign
    {
      get
      {
        return this.textBoxItem.TextAlign;
      }
      set
      {
        this.textBoxItem.TextAlign = value;
      }
    }

    public FillPrimitive Fill
    {
      get
      {
        return this.fillPrimitive;
      }
    }

    public BorderPrimitive Border
    {
      get
      {
        return this.borderPrimitive;
      }
    }

    public StackLayoutPanel ButtonsStack
    {
      get
      {
        return this.buttonsStack;
      }
    }

    public LightVisualButtonElement ClearButton
    {
      get
      {
        return this.clearButton;
      }
    }

    public RadTextBoxItem TextBoxItem
    {
      get
      {
        return this.textBoxItem;
      }
    }

    [RadDescription("UseGenericBorderPaint", typeof (RadTextBoxItem))]
    [RadDefaultValue("UseGenericBorderPaint", typeof (RadTextBoxItem))]
    public bool UseGenericBorderPaint
    {
      get
      {
        return this.textBoxItem.UseGenericBorderPaint;
      }
      set
      {
        this.defaultPadding = !value ? new Padding(2, 2, 2, 3) : new Padding(0);
        this.Padding = this.defaultPadding;
        this.textBoxItem.UseGenericBorderPaint = value;
      }
    }

    [DefaultValue(false)]
    public bool ShowBorder
    {
      get
      {
        return this.showBorder;
      }
      set
      {
        this.showBorder = value;
        if (this.borderPrimitive == null)
          return;
        this.borderPrimitive.Visibility = this.showBorder ? ElementVisibility.Visible : ElementVisibility.Collapsed;
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

    [RadDefaultValue("PasswordChar", typeof (RadTextBoxItem))]
    [Category("Behavior")]
    public char PasswordChar
    {
      get
      {
        return this.TextBoxItem.PasswordChar;
      }
      set
      {
        this.TextBoxItem.PasswordChar = value;
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the color of prompt text that is displayed when the TextBox contains no text.")]
    public Color NullTextColor
    {
      get
      {
        return (Color) this.GetValue(RadTextBoxElement.NullTextColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadTextBoxElement.NullTextColorProperty, (object) value);
      }
    }

    [Browsable(true)]
    [Description("Occurs when the AcceptsTab property has changed.")]
    [Category("Property Changed")]
    public event EventHandler AcceptsTabChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxElement.AcceptsTabChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxElement.AcceptsTabChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the HideSelection property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler HideSelectionChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxElement.HideSelectionChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxElement.HideSelectionChangedEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Category("Property Changed")]
    [Description("Occurs when the Modified property has changed.")]
    public event EventHandler ModifiedChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxElement.ModifiedChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxElement.ModifiedChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the Multiline property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler MultilineChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxElement.MultilineChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxElement.MultilineChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the ReadOnly property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler ReadOnlyChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxElement.ReadOnlyChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxElement.ReadOnlyChangedEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the TextAlign property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public event EventHandler TextAlignChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxElement.TextAlignChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxElement.TextAlignChangedEventKey, (Delegate) value);
      }
    }

    [Category("Property Changed")]
    [Browsable(true)]
    [Description("Occurs before the Text property changes.")]
    public new event TextChangingEventHandler TextChanging
    {
      add
      {
        this.Events.AddHandler(RadTextBoxElement.TextChangingEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxElement.TextChangingEventKey, (Delegate) value);
      }
    }

    [Description("Occurs when the Text property has changed.")]
    [Browsable(true)]
    [Category("Property Changed")]
    public new event EventHandler TextChanged
    {
      add
      {
        this.Events.AddHandler(RadTextBoxElement.TextChangedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadTextBoxElement.TextChangedEventKey, (Delegate) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMultilineChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxElement.MultilineChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnReadOnlyChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxElement.ReadOnlyChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void CallTextChanging(TextChangingEventArgs e)
    {
      TextChangingEventHandler changingEventHandler = (TextChangingEventHandler) this.Events[RadTextBoxElement.TextChangingEventKey];
      if (changingEventHandler == null)
        return;
      changingEventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void CallTextChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxElement.TextChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnTextAlignChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxElement.TextAlignChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnModifiedChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxElement.ModifiedChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnHideSelectionChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxElement.HideSelectionChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnAcceptsTabChanged(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadTextBoxElement.AcceptsTabChangedEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override void OnLoaded()
    {
      base.OnLoaded();
      this.SetClearButtonVisibility();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.textBoxItem == null || this.textBoxItem.HostedControl == null || this.textBoxItem.HostedControl.Focused)
        return;
      this.textBoxItem.HostedControl.Focus();
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      if (args.RoutedEvent == RadElement.MouseDownEvent && sender == this.textBoxItem)
      {
        if (RadModalFilter.Instance.ActiveDropDown != null && !RadModalFilter.Instance.Suspended)
        {
          RadModalFilter.Instance.Suspended = true;
          Point client = this.textBoxItem.HostedControl.PointToClient(Control.MousePosition);
          if (this.textBoxItem.HostedControl.ClientRectangle.Contains(client))
            Telerik.WinControls.NativeMethods.SendMessage(new HandleRef((object) null, this.textBoxItem.HostedControl.Handle), 513, 1, Telerik.WinControls.NativeMethods.Util.MAKELPARAM(client.X, client.Y));
        }
      }
      else if (args.RoutedEvent == RadTextBoxItem.MultilineEvent && sender == this.textBoxItem)
      {
        args.Canceled = true;
        this.StretchVertically = this.textBoxItem.Multiline;
        RootRadElement parent = this.Parent as RootRadElement;
        if (parent != null)
          parent.StretchVertically = this.textBoxItem.Multiline;
      }
      base.OnBubbleEvent(sender, args);
    }

    protected override void OnSelect()
    {
      base.OnSelect();
      this.Focus();
    }

    private void textBoxItem_AcceptsTabChanged(object sender, EventArgs e)
    {
      this.OnAcceptsTabChanged(e);
    }

    private void textBoxItem_HideSelectionChanged(object sender, EventArgs e)
    {
      this.OnHideSelectionChanged(e);
    }

    private void textBoxItem_ModifiedChanged(object sender, EventArgs e)
    {
      this.OnModifiedChanged(e);
    }

    private void textBoxItem_MultilineChanged(object sender, EventArgs e)
    {
      this.OnMultilineChanged(e);
    }

    private void textBoxItem_ReadOnlyChanged(object sender, EventArgs e)
    {
      this.OnReadOnlyChanged(e);
      if (!this.ShowClearButton)
        return;
      if (this.TextBoxItem.ReadOnly)
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

    private void textBoxItem_TextAlignChanged(object sender, EventArgs e)
    {
      this.OnTextAlignChanged(e);
    }

    private void textBoxItem_TextChanging(object sender, TextChangingEventArgs e)
    {
      this.CallTextChanging(e);
    }

    private void textBoxItem_TextChanged(object sender, EventArgs e)
    {
      this.CallTextChanged(e);
      this.SetClearButtonVisibility();
    }

    private void textBoxItem_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void textBoxItem_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void textBoxItem_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    private void TextBoxItem_GotFocus(object sender, EventArgs e)
    {
      this.UpdateFocusBorder(true);
    }

    private void TextBoxItem_LostFocus(object sender, EventArgs e)
    {
      this.UpdateFocusBorder(false);
    }

    private void ClearButton_Click(object sender, EventArgs e)
    {
      this.Text = string.Empty;
      this.Focus();
    }

    public override bool Focus()
    {
      return this.textBoxItem.TextBoxControl.Focus();
    }

    private void SetClearButtonVisibility()
    {
      if (this.clearButton == null)
        return;
      ElementVisibility elementVisibility = this.ShowClearButton ? (this.Text.Length <= 0 ? ElementVisibility.Hidden : ElementVisibility.Visible) : ElementVisibility.Collapsed;
      if (this.TextBoxItem.ReadOnly && this.ShowClearButton)
        this.readOnlyClearButtonVisibility = new ElementVisibility?(elementVisibility);
      else
        this.clearButton.Visibility = elementVisibility;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(availableSize);
      SizeF sizeF1 = SizeF.Empty;
      if (this.AutoSize)
      {
        for (int index = 0; index < this.Children.Count; ++index)
        {
          RadElement child = this.Children[index];
          if (this.ShouldMeasureChild(child))
          {
            SizeF sizeF2 = SizeF.Empty;
            if (child.FitToSizeMode == RadFitToSizeMode.FitToParentBounds)
            {
              child.Measure(availableSize);
              sizeF2 = child.DesiredSize;
            }
            if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent || child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
            {
              child.Measure(clientRectangle.Size);
              sizeF2 = this.AddClientRectangle(child.DesiredSize).Size;
            }
            if ((double) sizeF1.Width < (double) sizeF2.Width)
              sizeF1.Width = sizeF2.Width;
            if ((double) sizeF1.Height < (double) sizeF2.Height)
              sizeF1.Height = sizeF2.Height;
          }
        }
      }
      else
      {
        for (int index = 0; index < this.Children.Count; ++index)
          this.Children[index].Measure(clientRectangle.Size);
        sizeF1 = (SizeF) this.Size;
      }
      sizeF1.Width = Math.Min(sizeF1.Width, availableSize.Width);
      sizeF1.Height = Math.Min(sizeF1.Height, availableSize.Height);
      return sizeF1;
    }

    private RectangleF AddClientRectangle(SizeF finalSize)
    {
      Padding padding = this.Padding;
      RectangleF rectangleF = new RectangleF((float) padding.Left, (float) padding.Top, finalSize.Width + (float) padding.Horizontal, finalSize.Height + (float) padding.Vertical);
      foreach (RadElement child in this.Children)
      {
        if (child is BorderPrimitive)
        {
          if (child != null)
          {
            if (child.Visibility != ElementVisibility.Collapsed)
            {
              Padding borderThickness = this.GetBorderThickness(child as BorderPrimitive);
              rectangleF.X -= (float) borderThickness.Left;
              rectangleF.Y -= (float) borderThickness.Top;
              rectangleF.Width += (float) borderThickness.Horizontal;
              rectangleF.Height += (float) borderThickness.Vertical;
              break;
            }
            break;
          }
          break;
        }
      }
      return rectangleF;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      for (int index = 0; index < this.Children.Count; ++index)
      {
        RadElement child = this.Children[index];
        if (this.ShouldArrangeChild(child))
        {
          RectangleF finalRect = new RectangleF(PointF.Empty, finalSize);
          if (!this.BypassLayoutPolicies)
          {
            if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent || child.FitToSizeMode == RadFitToSizeMode.FitToParentPadding)
            {
              Padding borderThickness = this.BorderThickness;
              finalRect.Location = PointF.Add(finalRect.Location, new SizeF((float) borderThickness.Left, (float) borderThickness.Top));
              finalRect.Size = SizeF.Subtract(finalRect.Size, (SizeF) borderThickness.Size);
            }
            if (child.FitToSizeMode == RadFitToSizeMode.FitToParentContent)
            {
              finalRect.Location = PointF.Add(finalRect.Location, new SizeF((float) this.Padding.Left, (float) this.Padding.Top));
              finalRect.Size = SizeF.Subtract(finalRect.Size, (SizeF) this.Padding.Size);
            }
          }
          if (child == this.textBoxItem && this.ButtonsStack.DesiredSize != SizeF.Empty)
          {
            finalRect.Size = SizeF.Subtract(finalRect.Size, new SizeF(this.ButtonsStack.DesiredSize.Width, 0.0f));
            if (this.RightToLeft)
              finalRect.Location = PointF.Add(finalRect.Location, new SizeF(this.ButtonsStack.DesiredSize.Width, 0.0f));
          }
          else if (child == this.ButtonsStack)
          {
            if (!this.RightToLeft)
              finalRect.Location = PointF.Add(finalRect.Location, new SizeF(finalRect.Width - this.ButtonsStack.DesiredSize.Width, 0.0f));
            finalRect.Size = new SizeF(this.ButtonsStack.DesiredSize.Width, finalRect.Height);
          }
          child.Arrange(finalRect);
        }
      }
      return finalSize;
    }
  }
}
