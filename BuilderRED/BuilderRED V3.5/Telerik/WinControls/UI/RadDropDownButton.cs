// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadDropDownButton
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
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [DefaultBindingProperty("Text")]
  [DefaultEvent("Click")]
  [ToolboxItem(true)]
  [Description("Provides a menu-like interface within a button")]
  [DefaultProperty("Text")]
  [Designer("Telerik.WinControls.UI.Design.RadDropDownButtonDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadDropDownButton : RadControl
  {
    private static readonly object DropDownOpeningEventKey = new object();
    private static readonly object DropDownOpenedEventKey = new object();
    private static readonly object DropDownClosedEventKey = new object();
    private RadDropDownButtonElement dropDownButtonElement;

    public RadDropDownButton()
    {
      this.SetStyle(ControlStyles.Selectable, true);
      this.SetStyle(ControlStyles.StandardDoubleClick, false);
      this.AllowShowFocusCues = true;
      this.DropDownButtonElement.CanFocus = true;
      int num = (int) this.RootElement.BindProperty(RadItem.ShadowDepthProperty, (RadObject) this.DropDownButtonElement, RadItem.ShadowDepthProperty, PropertyBindingOptions.OneWay);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.dropDownButtonElement != null)
      {
        this.dropDownButtonElement.DropDownOpening -= new CancelEventHandler(this.dropDownButtonElement_DropDownOpening);
        this.dropDownButtonElement.DropDownOpened -= new EventHandler(this.dropDownButtonElement_DropDownOpened);
        this.dropDownButtonElement.DropDownClosed -= new EventHandler(this.dropDownButtonElement_DropDownClosed);
      }
      base.Dispose(disposing);
    }

    protected virtual RadDropDownButtonElement CreateButtonElement()
    {
      return new RadDropDownButtonElement();
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.dropDownButtonElement = this.CreateButtonElement();
      this.dropDownButtonElement.DropDownOpening += new CancelEventHandler(this.dropDownButtonElement_DropDownOpening);
      this.dropDownButtonElement.DropDownOpened += new EventHandler(this.dropDownButtonElement_DropDownOpened);
      this.dropDownButtonElement.DropDownClosed += new EventHandler(this.dropDownButtonElement_DropDownClosed);
      this.RootElement.Children.Add((RadElement) this.dropDownButtonElement);
      this.dropDownButtonElement.ArrowButton.Arrow.AutoSize = true;
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadDropDownButtonAccessibleObject(this);
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(140, 24));
      }
    }

    [Bindable(true)]
    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Localizable(true)]
    [Category("Behavior")]
    [SettingsBindable(true)]
    [Description("Gets or sets the text associated with this item.")]
    public override string Text
    {
      get
      {
        return this.dropDownButtonElement.Text;
      }
      set
      {
        if (!(this.dropDownButtonElement.Text != value))
          return;
        this.dropDownButtonElement.Text = value;
        base.Text = value;
        this.OnTextChanged(new EventArgs());
      }
    }

    [Description("Indicates focus cues display, when available, based on the corresponding control type and the current UI state.")]
    [Browsable(true)]
    [Category("Accessibility")]
    [DefaultValue(true)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadDropDownButtonElement DropDownButtonElement
    {
      get
      {
        return this.dropDownButtonElement;
      }
    }

    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(DisplayStyle.ImageAndText)]
    [Browsable(true)]
    public virtual DisplayStyle DisplayStyle
    {
      get
      {
        return this.dropDownButtonElement.DisplayStyle;
      }
      set
      {
        this.dropDownButtonElement.DisplayStyle = value;
      }
    }

    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(RadDirection.Down)]
    [Browsable(true)]
    public RadDirection DropDownDirection
    {
      get
      {
        return this.dropDownButtonElement.DropDownDirection;
      }
      set
      {
        this.dropDownButtonElement.DropDownDirection = value;
      }
    }

    [Browsable(true)]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [DefaultValue(Telerik.WinControls.ArrowDirection.Down)]
    public Telerik.WinControls.ArrowDirection ArrowDirection
    {
      get
      {
        return this.dropDownButtonElement.ArrowButton.Direction;
      }
      set
      {
        this.dropDownButtonElement.ArrowButton.Direction = value;
      }
    }

    [RadEditItemsAction]
    [Category("Data")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Editor("Telerik.WinControls.UI.Design.RadItemCollectionEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public virtual RadItemOwnerCollection Items
    {
      get
      {
        return this.dropDownButtonElement.Items;
      }
    }

    [RadDescription("Image", typeof (RadDropDownButtonElement))]
    [Localizable(true)]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    public Image Image
    {
      get
      {
        return this.dropDownButtonElement.Image;
      }
      set
      {
        this.dropDownButtonElement.Image = value;
        if (this.dropDownButtonElement.Image == null)
          return;
        this.ImageList = (ImageList) null;
      }
    }

    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Category("Appearance")]
    [Description("Gets or sets the alignment of image content on the drawing surface.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.dropDownButtonElement.ImageAlignment;
      }
      set
      {
        this.dropDownButtonElement.ImageAlignment = value;
      }
    }

    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RadDefaultValue("ImageIndex", typeof (RadDropDownButtonElement))]
    [Category("Appearance")]
    [RadDescription("ImageIndex", typeof (RadDropDownButtonElement))]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    public int ImageIndex
    {
      get
      {
        return this.dropDownButtonElement.ImageIndex;
      }
      set
      {
        this.dropDownButtonElement.ImageIndex = value;
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ImageList")]
    [RadDescription("ImageKey", typeof (RadDropDownButtonElement))]
    [Category("Appearance")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Localizable(true)]
    [RadDefaultValue("ImageKey", typeof (RadDropDownButtonElement))]
    public virtual string ImageKey
    {
      get
      {
        return this.dropDownButtonElement.ImageKey;
      }
      set
      {
        this.dropDownButtonElement.ImageKey = value;
      }
    }

    [Browsable(false)]
    public bool IsPressed
    {
      get
      {
        return this.dropDownButtonElement.IsPressed;
      }
    }

    [RadDefaultValue("ShowArrow", typeof (RadDropDownButtonElement))]
    [RadDescription("ShowArrow", typeof (RadDropDownButtonElement))]
    [Browsable(true)]
    [Category("Appearance")]
    public bool ShowArrow
    {
      get
      {
        return this.dropDownButtonElement.ShowArrow;
      }
      set
      {
        this.dropDownButtonElement.ShowArrow = value;
      }
    }

    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Description("Gets or sets the alignment of text content on the drawing surface.")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.dropDownButtonElement.TextAlignment;
      }
      set
      {
        this.dropDownButtonElement.TextAlignment = value;
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the position of text and image relative to each other.")]
    [Category("Appearance")]
    [DefaultValue(TextImageRelation.Overlay)]
    public TextImageRelation TextImageRelation
    {
      get
      {
        return this.dropDownButtonElement.TextImageRelation;
      }
      set
      {
        this.dropDownButtonElement.TextImageRelation = value;
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    public event EventHandler DropDownOpening
    {
      add
      {
        this.Events.AddHandler(RadDropDownButton.DropDownOpeningEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownButton.DropDownOpeningEventKey, (Delegate) value);
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    public event EventHandler DropDownOpened
    {
      add
      {
        this.Events.AddHandler(RadDropDownButton.DropDownOpenedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownButton.DropDownOpenedEventKey, (Delegate) value);
      }
    }

    [Browsable(true)]
    [Category("Behavior")]
    public event EventHandler DropDownClosed
    {
      add
      {
        this.Events.AddHandler(RadDropDownButton.DropDownClosedEventKey, (Delegate) value);
      }
      remove
      {
        this.Events.RemoveHandler(RadDropDownButton.DropDownClosedEventKey, (Delegate) value);
      }
    }

    public virtual void ShowDropDown()
    {
      this.dropDownButtonElement.ShowDropDown();
    }

    public virtual void HideDropDown()
    {
      this.dropDownButtonElement.HideDropDown();
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownClosed(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadDropDownButton.DropDownClosedEventKey];
      if (eventHandler != null)
        eventHandler((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Closed");
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownOpening(CancelEventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadDropDownButton.DropDownOpeningEventKey];
      if (eventHandler == null)
        return;
      eventHandler((object) this, (EventArgs) e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnDropDownOpened(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[RadDropDownButton.DropDownOpenedEventKey];
      if (eventHandler != null)
        eventHandler((object) this, e);
      ControlTraceMonitor.TrackAtomicFeature((RadControl) this, "Opened");
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.DropDownButtonElement.Focus();
    }

    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      if (this.dropDownButtonElement.DropDownMenu == null || this.dropDownButtonElement.DropDownMenu.Bounds.Contains(Control.MousePosition))
        return;
      this.dropDownButtonElement.DropDownMenu.ClosePopup(RadPopupCloseReason.AppFocusChange);
    }

    protected override bool ProcessMnemonic(char charCode)
    {
      if (!TelerikHelper.CanProcessMnemonic((Control) this) || (Control.ModifierKeys & Keys.Alt) != Keys.Alt || !Control.IsMnemonic(charCode, this.Text))
        return false;
      this.ShowDropDown();
      return true;
    }

    private void dropDownButtonElement_DropDownClosed(object sender, EventArgs e)
    {
      this.OnDropDownClosed(e);
    }

    private void dropDownButtonElement_DropDownOpened(object sender, EventArgs e)
    {
      this.OnDropDownOpened(e);
    }

    private void dropDownButtonElement_DropDownOpening(object sender, CancelEventArgs e)
    {
      this.OnDropDownOpening(e);
    }

    private bool ShouldSerializeImage()
    {
      if (this.Image != null && this.ImageList == null)
        return this.dropDownButtonElement.GetValueSource(RadDropDownButtonElement.ImageProperty) != ValueSource.Style;
      return false;
    }

    protected override bool IsInputKey(Keys keyData)
    {
      if (keyData == Keys.Down && this.Items.Count > 0)
        return true;
      return base.IsInputKey(keyData);
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      System.Type type = element.GetType();
      return (object) type == (object) typeof (RadDropDownButtonElement) || (object) type == (object) typeof (RadButtonElement);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.DropDownButtonElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.DropDownButtonElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.DropDownButtonElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.DropDownButtonElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "ButtonFill");
        this.DropDownButtonElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, state, "ButtonFill");
        this.DropDownButtonElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "RadArrowButtonFill");
        this.DropDownButtonElement.SetThemeValueOverride(FillPrimitive.GradientStyleProperty, (object) GradientStyles.Solid, state, "RadArrowButtonFill");
      }
      this.DropDownButtonElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.DropDownButtonElement.SuspendApplyOfThemeSettings();
      this.DropDownButtonElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.DropDownButtonElement.ResetThemeValueOverride(FillPrimitive.GradientStyleProperty);
      this.DropDownButtonElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.DropDownButtonElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates1 = this.DropDownButtonElement.GetAvailableVisualStates();
      availableVisualStates1.Add("");
      foreach (string state in availableVisualStates1)
        this.DropDownButtonElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
      this.DropDownButtonElement.ActionButton.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates2 = this.DropDownButtonElement.ActionButton.GetAvailableVisualStates();
      availableVisualStates2.Add("");
      foreach (string state in availableVisualStates2)
      {
        this.DropDownButtonElement.ActionButton.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.DropDownButtonElement.ActionButton.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state, typeof (TextPrimitive));
      }
      this.DropDownButtonElement.ResumeApplyOfThemeSettings();
      this.DropDownButtonElement.ActionButton.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.DropDownButtonElement.SuspendApplyOfThemeSettings();
      this.DropDownButtonElement.ActionButton.SuspendApplyOfThemeSettings();
      this.DropDownButtonElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      this.DropDownButtonElement.ActionButton.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num1 = (int) this.DropDownButtonElement.ActionButton.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      int num2 = (int) this.DropDownButtonElement.ActionButton.LayoutPanel.Children[1].ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.DropDownButtonElement.ElementTree.ApplyThemeToElementTree();
      this.DropDownButtonElement.ResumeApplyOfThemeSettings();
      this.DropDownButtonElement.ActionButton.ResumeApplyOfThemeSettings();
    }

    protected override void OnThemeChanged()
    {
      base.OnThemeChanged();
      string visualState = this.DropDownButtonElement.ActionButton.VisualState;
      this.DropDownButtonElement.ActionButton.VisualState = "";
      this.DropDownButtonElement.ActionButton.VisualState = visualState;
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
        if (request.Message == "HasChildNodes")
        {
          request.Data = (object) (this.Items.Count > 0);
          return;
        }
      }
      base.ProcessCodedUIMessage(ref request);
    }
  }
}
