// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadLabel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [DefaultProperty("Text")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("Provides run-time information or descriptive text for a control")]
  [DefaultBindingProperty("Text")]
  [DefaultEvent("Click")]
  [ToolboxItem(true)]
  public class RadLabel : RadControl
  {
    private RadLabelElement labelElement;
    private static UIPermission allWindows;

    public RadLabel()
    {
      this.AutoSize = true;
      this.TabStop = false;
      this.SetStyle(ControlStyles.Selectable, false);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.labelElement = this.CreateLabelElement();
      this.RootElement.Children.Add((RadElement) this.labelElement);
    }

    protected virtual RadLabelElement CreateLabelElement()
    {
      return new RadLabelElement();
    }

    protected override RootRadElement CreateRootElement()
    {
      return (RootRadElement) new RadLabelRootElement();
    }

    protected override AccessibleObject CreateAccessibilityInstance()
    {
      if (!this.EnableRadAccessibilityObjects)
        return base.CreateAccessibilityInstance();
      return (AccessibleObject) new RadLabelAccessibleObject(this);
    }

    [DefaultValue(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether the control is automatically resized to display its entire contents.")]
    [Browsable(true)]
    [Category("Layout")]
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

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(100, 18));
      }
    }

    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new bool TabStop
    {
      get
      {
        return base.TabStop;
      }
      set
      {
        base.TabStop = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(TextImageRelation.Overlay)]
    [Localizable(true)]
    [Description("Gets or sets the position of text and image relative to each other.")]
    public TextImageRelation TextImageRelation
    {
      get
      {
        return this.labelElement.TextImageRelation;
      }
      set
      {
        this.labelElement.TextImageRelation = value;
      }
    }

    [DefaultValue(true)]
    [Category("Appearance")]
    [Localizable(true)]
    [Description("true if the text should wrap to the available layout rectangle otherwise, false. The default is true")]
    public bool TextWrap
    {
      get
      {
        return this.labelElement.TextWrap;
      }
      set
      {
        this.labelElement.TextWrap = value;
      }
    }

    [Description("Gets or sets the alignment of image content on the drawing surface.")]
    [Localizable(true)]
    [Category("Appearance")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.labelElement.ImageAlignment;
      }
      set
      {
        this.labelElement.ImageAlignment = value;
      }
    }

    [Category("Appearance")]
    [Localizable(true)]
    [Description("Gets or sets the alignment of text content on the drawing surface.")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.labelElement.TextAlignment;
      }
      set
      {
        this.labelElement.TextAlignment = value;
      }
    }

    [Description("Gets or sets the key accessor for the image in the label.")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RelatedImageList("ImageList")]
    [Category("Appearance")]
    [Localizable(true)]
    [DefaultValue("")]
    public virtual string ImageKey
    {
      get
      {
        return this.labelElement.ImageKey;
      }
      set
      {
        this.labelElement.ImageKey = value;
      }
    }

    [Localizable(true)]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [RelatedImageList("ImageList")]
    [Category("Appearance")]
    [Description("Gets or sets the image list index value of the image displayed on the label control.")]
    [DefaultValue(-1)]
    public int ImageIndex
    {
      get
      {
        return this.labelElement.ImageIndex;
      }
      set
      {
        this.labelElement.ImageIndex = value;
      }
    }

    [Category("Appearance")]
    [Description("Gets or sets the image that is displayed on a button element.")]
    [Localizable(true)]
    public Image Image
    {
      get
      {
        return this.labelElement.Image;
      }
      set
      {
        if (this.labelElement.Image == value)
          return;
        this.labelElement.Image = value;
        if (this.labelElement.Image == null)
          return;
        this.ImageList = (ImageList) null;
      }
    }

    public bool ShouldSerializeImage()
    {
      if (this.labelElement.Image != null)
        return this.labelElement.GetValueSource(RadLabelElement.ImageProperty) != ValueSource.Style;
      return false;
    }

    [Localizable(true)]
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("If true, the first character preceded by an ampersand (&&) will be used as the label's mnemonic key")]
    public bool UseMnemonic
    {
      get
      {
        return this.labelElement.UseMnemonic;
      }
      set
      {
        this.labelElement.UseMnemonic = value;
      }
    }

    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether the border is visible.")]
    public bool BorderVisible
    {
      get
      {
        return this.labelElement.BorderVisible;
      }
      set
      {
        this.labelElement.BorderVisible = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public RadLabelElement LabelElement
    {
      get
      {
        return this.labelElement;
      }
    }

    private static CodeAccessPermission AllWindows
    {
      get
      {
        if (RadLabel.allWindows == null)
          RadLabel.allWindows = new UIPermission(UIPermissionWindow.AllWindows);
        return (CodeAccessPermission) RadLabel.allWindows;
      }
    }

    [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
    protected override bool ProcessMnemonic(char charCode)
    {
      if (!this.UseMnemonic || !Control.IsMnemonic(charCode, this.Text) || ((Control.ModifierKeys & Keys.Alt) != Keys.Alt || !this.Enabled) || !this.Visible)
        return false;
      Control parent = this.Parent;
      if (parent != null)
      {
        RadLabel.AllWindows.Assert();
        try
        {
          if (parent.SelectNextControl((Control) this, true, false, true, false))
          {
            if (!parent.ContainsFocus)
              parent.Focus();
          }
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
      }
      return true;
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      if (propertyName == "AutoSize")
      {
        if (!this.AutoSize)
        {
          this.RootElement.StretchHorizontally = true;
          this.RootElement.StretchVertically = true;
        }
        else
        {
          this.RootElement.StretchHorizontally = false;
          this.RootElement.StretchVertically = false;
        }
      }
      base.OnNotifyPropertyChanged(propertyName);
    }

    protected override void OnTextChanged(EventArgs e)
    {
      this.labelElement.Text = this.Text;
      base.OnTextChanged(e);
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.LabelElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.LabelElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.LabelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.LabelElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, typeof (FillPrimitive));
      }
      this.LabelElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.LabelElement.SuspendApplyOfThemeSettings();
      this.LabelElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num = (int) this.LabelElement.LabelFill.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.LabelElement.ElementTree.ApplyThemeToElementTree();
      this.LabelElement.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.LabelElement.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.LabelElement.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.LabelElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state);
        this.LabelElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, state, typeof (TextPrimitive));
      }
      this.LabelElement.ResumeApplyOfThemeSettings();
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.LabelElement.SuspendApplyOfThemeSettings();
      this.LabelElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num1 = (int) this.LabelElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      int num2 = (int) this.LabelElement.LabelText.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.LabelElement.ElementTree.ApplyThemeToElementTree();
      this.LabelElement.ResumeApplyOfThemeSettings();
    }
  }
}
