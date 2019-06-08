// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadButtonBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [DefaultBindingProperty("Text")]
  [DefaultProperty("Text")]
  [Description("Responds to user clicks.")]
  [ToolboxItem(false)]
  [DefaultEvent("Click")]
  public class RadButtonBase : RadControl
  {
    private static MethodInfo ValidateActiveControlMethod;
    private RadButtonElement buttonElement;

    public RadButtonBase()
    {
      this.SetStyle(ControlStyles.Selectable, true);
      this.SetStyle(ControlStyles.StandardDoubleClick, false);
      int num = (int) this.RootElement.BindProperty(RadItem.ShadowDepthProperty, (RadObject) this.ButtonElement, RadItem.ShadowDepthProperty, PropertyBindingOptions.OneWay);
    }

    protected override void InitializeRootElement(RootRadElement rootElement)
    {
      base.InitializeRootElement(rootElement);
      this.ElementTree.ComponentTreeHandler.Behavior.AllowShowFocusCues = true;
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadButtonAccessibleObject((Control) this);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.buttonElement = this.CreateButtonElement();
      this.buttonElement.Click += new EventHandler(this.buttonElement_Click);
      parent.Children.Add((RadElement) this.buttonElement);
    }

    protected virtual RadButtonElement CreateButtonElement()
    {
      return new RadButtonElement();
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public new event EventHandler DoubleClick
    {
      add
      {
        base.DoubleClick += value;
      }
      remove
      {
        base.DoubleClick -= value;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Browsable(false)]
    public new event MouseEventHandler MouseDoubleClick
    {
      add
      {
        base.MouseDoubleClick += value;
      }
      remove
      {
        base.MouseDoubleClick -= value;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(110, 24));
      }
    }

    protected virtual ContentAlignment DefaultTextAlignment
    {
      get
      {
        object defaultValueOverride = this.ButtonElement.GetPropertyValue(RadButtonItem.TextAlignmentProperty).DefaultValueOverride;
        if (defaultValueOverride == RadProperty.UnsetValue)
          return (ContentAlignment) RadButtonItem.TextAlignmentProperty.GetMetadata((RadObject) this.ButtonElement).DefaultValue;
        return (ContentAlignment) defaultValueOverride;
      }
    }

    [Localizable(true)]
    [Bindable(true)]
    [Description("Gets or sets the text associated with this item.")]
    [SettingsBindable(true)]
    [Editor("Telerik.WinControls.UI.Design.TextOrHtmlSelector, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Behavior")]
    public override string Text
    {
      get
      {
        return this.buttonElement.Text;
      }
      set
      {
        base.Text = value;
        this.buttonElement.Text = value;
      }
    }

    [Browsable(true)]
    [Category("Accessibility")]
    [Description("Indicates focus cues display, when available, based on the corresponding control type and the current UI state.")]
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

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public RadButtonElement ButtonElement
    {
      get
      {
        return this.buttonElement;
      }
    }

    [DefaultValue(true)]
    [Description("Includes the trailing space at the end of each line. By default the boundary rectangle returned by the Overload:System.Drawing.Graphics.MeasureString method excludes the space at the end of each line. Set this flag to include that space in measurement.")]
    [Localizable(true)]
    [Category("Appearance")]
    public bool MeasureTrailingSpaces
    {
      get
      {
        return this.ButtonElement.MeasureTrailingSpaces;
      }
      set
      {
        this.ButtonElement.MeasureTrailingSpaces = value;
      }
    }

    [RadDescription("Image", typeof (RadButtonElement))]
    [RefreshProperties(RefreshProperties.All)]
    [Localizable(true)]
    [Category("Appearance")]
    public Image Image
    {
      get
      {
        return this.buttonElement.Image;
      }
      set
      {
        Image image = this.buttonElement.Image;
        if (value == image)
          return;
        this.ImageList = (ImageList) null;
        this.ImageIndex = -1;
        this.buttonElement.Image = value;
      }
    }

    [Category("Appearance")]
    [RadDescription("ImageIndex", typeof (RadButtonElement))]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Localizable(true)]
    [RadDefaultValue("ImageIndex", typeof (RadButtonElement))]
    public int ImageIndex
    {
      get
      {
        return this.buttonElement.ImageIndex;
      }
      set
      {
        this.buttonElement.ImageIndex = value;
      }
    }

    [RadDefaultValue("ImageKey", typeof (RadButtonElement))]
    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.All)]
    [Localizable(true)]
    [RelatedImageList("ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RadDescription("ImageKey", typeof (RadButtonElement))]
    public string ImageKey
    {
      get
      {
        return this.buttonElement.ImageKey;
      }
      set
      {
        this.buttonElement.ImageKey = value;
      }
    }

    [Browsable(true)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Specifies the options for display of image and text primitives in the element.")]
    [RadDefaultValue("DisplayStyle", typeof (RadButtonElement))]
    [Localizable(true)]
    public DisplayStyle DisplayStyle
    {
      get
      {
        return this.buttonElement.DisplayStyle;
      }
      set
      {
        this.buttonElement.DisplayStyle = value;
      }
    }

    [Description("True if the text should wrap to the available layout rectangle otherwise, false.")]
    [RadPropertyDefaultValue("TextWrap", typeof (TextPrimitive))]
    [Category("Appearance")]
    public bool TextWrap
    {
      get
      {
        return this.buttonElement.TextWrap;
      }
      set
      {
        this.buttonElement.TextWrap = value;
      }
    }

    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the position of text and image relative to each other.")]
    [RadDefaultValue("TextImageRelation", typeof (RadButtonElement))]
    [Localizable(true)]
    public TextImageRelation TextImageRelation
    {
      get
      {
        return this.buttonElement.TextImageRelation;
      }
      set
      {
        this.buttonElement.TextImageRelation = value;
      }
    }

    [Category("Appearance")]
    [RefreshProperties(RefreshProperties.Repaint)]
    [Description("Gets or sets the alignment of image content on the drawing surface.")]
    [RadDefaultValue("ImageAlignment", typeof (RadButtonElement))]
    [Localizable(true)]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.buttonElement.ImageAlignment;
      }
      set
      {
        this.buttonElement.ImageAlignment = value;
      }
    }

    [RefreshProperties(RefreshProperties.Repaint)]
    [Category("Appearance")]
    [Description("Gets or sets the alignment of text content on the drawing surface.")]
    [Localizable(true)]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.buttonElement.TextAlignment;
      }
      set
      {
        this.buttonElement.TextAlignment = value;
      }
    }

    [Category("Appearance")]
    [Description("Determines whether the button can be clicked by using mnemonic characters.")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
      get
      {
        return this.buttonElement.TextElement.UseMnemonic;
      }
      set
      {
        this.buttonElement.TextElement.UseMnemonic = value;
      }
    }

    public virtual void PerformClick()
    {
      bool flag = false;
      if ((object) RadButtonBase.ValidateActiveControlMethod == null)
        RadButtonBase.ValidateActiveControlMethod = typeof (Control).GetMethod("ValidateActiveControl", BindingFlags.Instance | BindingFlags.NonPublic);
      if (!(bool) RadButtonBase.ValidateActiveControlMethod.Invoke((object) this, new object[1]{ (object) flag }) && !flag || !this.Enabled)
        return;
      ((IButtonControl) this.ButtonElement).PerformClick();
      this.OnClick(EventArgs.Empty);
    }

    public override bool ControlDefinesThemeForElement(RadElement element)
    {
      if (element is RadButtonElement)
        return true;
      return base.ControlDefinesThemeForElement(element);
    }

    [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
    protected override bool ProcessMnemonic(char charCode)
    {
      if (!this.UseMnemonic || !TelerikHelper.CanProcessMnemonic((Control) this) || ((Control.ModifierKeys & Keys.Alt) != Keys.Alt || !Control.IsMnemonic(charCode, this.Text)))
        return false;
      this.PerformClick();
      return true;
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.ButtonElement.Focus();
    }

    protected override void OnClick(EventArgs e)
    {
    }

    private void buttonElement_Click(object sender, EventArgs e)
    {
      base.OnClick(e);
    }

    private bool ShouldSerializeTextAlignment()
    {
      return this.TextAlignment != this.DefaultTextAlignment;
    }

    private bool ShouldSerializeImage()
    {
      if (this.Image != null && this.ImageList == null)
        return this.buttonElement.GetValueSource(RadButtonItem.ImageProperty) != ValueSource.Style;
      return false;
    }
  }
}
