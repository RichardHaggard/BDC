// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGroupBox
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadGroupBoxDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Containers")]
  public class RadGroupBox : RadControl
  {
    private RadGroupBoxElement groupBoxElement;

    public RadGroupBox()
    {
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.ContainerControl, true);
      this.AccessibleRole = AccessibleRole.Grouping;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.AutoSize)
      {
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          control.SizeChanged -= new EventHandler(this.Control_SizeChanged);
      }
      base.Dispose(disposing);
    }

    protected override void CreateChildItems(RadElement parent)
    {
      this.groupBoxElement = this.CreateGroupBoxElement();
      this.RootElement.Children.Add((RadElement) this.groupBoxElement);
      base.CreateChildItems(parent);
      this.FooterText = "Footer Text";
      int num = (int) this.RootElement.SetDefaultValueOverride(RadElement.PaddingProperty, (object) new Padding(2, 18, 2, 2));
    }

    protected virtual RadGroupBoxElement CreateGroupBoxElement()
    {
      return new RadGroupBoxElement();
    }

    protected override Size DefaultSize
    {
      get
      {
        return RadControl.GetDpiScaledSize(new Size(200, 100));
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public override bool AutoScroll
    {
      get
      {
        return base.AutoScroll;
      }
      set
      {
      }
    }

    [Category("Appearance")]
    [Localizable(true)]
    public override string Text
    {
      get
      {
        return this.HeaderText;
      }
      set
      {
        if (!(this.HeaderText != value))
          return;
        this.HeaderText = value;
        this.OnTextChanged(new EventArgs());
      }
    }

    [DefaultValue(typeof (Padding), "2, 18, 2, 2")]
    public new Padding Padding
    {
      get
      {
        return base.Padding;
      }
      set
      {
        base.Padding = value;
      }
    }

    [Browsable(false)]
    public RadGroupBoxElement GroupBoxElement
    {
      get
      {
        return this.groupBoxElement;
      }
    }

    [Category("Appearance")]
    [DefaultValue(RadGroupBoxStyle.Standard)]
    public RadGroupBoxStyle GroupBoxStyle
    {
      get
      {
        return this.groupBoxElement.GroupBoxStyle;
      }
      set
      {
        this.groupBoxElement.GroupBoxStyle = value;
      }
    }

    [DefaultValue(HeaderPosition.Top)]
    [Category("Appearance")]
    public HeaderPosition HeaderPosition
    {
      get
      {
        return this.groupBoxElement.HeaderPosition;
      }
      set
      {
        this.groupBoxElement.HeaderPosition = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(HeaderAlignment.Near)]
    public HeaderAlignment HeaderAlignment
    {
      get
      {
        return this.groupBoxElement.HeaderAlignment;
      }
      set
      {
        this.groupBoxElement.HeaderAlignment = value;
      }
    }

    [RadPropertyDefaultValue("Margin", typeof (RadElement))]
    [Category("Appearance")]
    public Padding HeaderMargin
    {
      get
      {
        return this.groupBoxElement.HeaderMargin;
      }
      set
      {
        this.groupBoxElement.HeaderMargin = value;
      }
    }

    [DefaultValue(ElementVisibility.Collapsed)]
    [Category("Appearance")]
    public ElementVisibility FooterVisibility
    {
      get
      {
        return this.groupBoxElement.FooterVisibile;
      }
      set
      {
        this.groupBoxElement.FooterVisibile = value;
      }
    }

    [Localizable(true)]
    [Category("Appearance")]
    public string HeaderText
    {
      get
      {
        return this.groupBoxElement.HeaderText;
      }
      set
      {
        this.groupBoxElement.HeaderText = value;
      }
    }

    [DefaultValue("Footer Text")]
    [Category("Appearance")]
    public string FooterText
    {
      get
      {
        return this.groupBoxElement.FooterText;
      }
      set
      {
        this.groupBoxElement.FooterText = value;
      }
    }

    [DefaultValue(null)]
    [Category("Appearance")]
    public Image HeaderImage
    {
      get
      {
        return this.groupBoxElement.HeaderImage;
      }
      set
      {
        this.groupBoxElement.HeaderImage = value;
      }
    }

    [DefaultValue(null)]
    [Category("Appearance")]
    public Image FooterImage
    {
      get
      {
        return this.groupBoxElement.FooterImage;
      }
      set
      {
        this.groupBoxElement.FooterImage = value;
      }
    }

    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [Category("Appearance")]
    [RadDescription("HeaderImageKey", typeof (RadGroupBoxElement))]
    [RefreshProperties(RefreshProperties.All)]
    [Localizable(true)]
    [RelatedImageList("ImageList")]
    [RadDefaultValue("ImageKey", typeof (ImagePrimitive))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public string HeaderImageKey
    {
      get
      {
        return this.groupBoxElement.HeaderImageKey;
      }
      set
      {
        this.groupBoxElement.HeaderImageKey = value;
      }
    }

    [RadDefaultValue("ImageIndex", typeof (ImagePrimitive))]
    [Category("Appearance")]
    [RadDescription("HeaderImageIndex", typeof (RadGroupBoxElement))]
    [RefreshProperties(RefreshProperties.All)]
    [RelatedImageList("ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Localizable(true)]
    public int HeaderImageIndex
    {
      get
      {
        return this.groupBoxElement.HeaderImageIndex;
      }
      set
      {
        this.groupBoxElement.HeaderImageIndex = value;
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Category("Appearance")]
    [RadDescription("FooterImageKey", typeof (RadGroupBoxElement))]
    [RadDefaultValue("ImageKey", typeof (ImagePrimitive))]
    [Localizable(true)]
    [RelatedImageList("ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageKeyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.RadImageKeyConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    public string FooterImageKey
    {
      get
      {
        return this.groupBoxElement.FooterImageKey;
      }
      set
      {
        this.groupBoxElement.FooterImageKey = value;
      }
    }

    [RefreshProperties(RefreshProperties.All)]
    [Category("Appearance")]
    [RadDescription("FooterImageIndex", typeof (RadGroupBoxElement))]
    [RadDefaultValue("ImageIndex", typeof (ImagePrimitive))]
    [RelatedImageList("ImageList")]
    [Editor("Telerik.WinControls.UI.Design.ImageIndexEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter("Telerik.WinControls.UI.Design.NoneExcludedImageIndexConverter, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
    [Localizable(true)]
    public int FooterImageIndex
    {
      get
      {
        return this.groupBoxElement.FooterImageIndex;
      }
      set
      {
        this.groupBoxElement.FooterImageIndex = value;
      }
    }

    [DefaultValue(TextImageRelation.Overlay)]
    [Category("Appearance")]
    public TextImageRelation HeaderTextImageRelation
    {
      get
      {
        return this.groupBoxElement.HeaderTextImageRelation;
      }
      set
      {
        this.groupBoxElement.HeaderTextImageRelation = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(TextImageRelation.Overlay)]
    public TextImageRelation FooterTextImageRelation
    {
      get
      {
        return this.groupBoxElement.FooterTextImageRelation;
      }
      set
      {
        this.groupBoxElement.FooterTextImageRelation = value;
      }
    }

    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Category("Appearance")]
    public ContentAlignment HeaderTextAlignment
    {
      get
      {
        return this.groupBoxElement.HeaderTextAlignment;
      }
      set
      {
        this.groupBoxElement.HeaderTextAlignment = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment FooterTextAlignment
    {
      get
      {
        return this.groupBoxElement.FooterTextAlignment;
      }
      set
      {
        this.groupBoxElement.FooterTextAlignment = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment HeaderImageAlignment
    {
      get
      {
        return this.groupBoxElement.HeaderImageAlignment;
      }
      set
      {
        this.groupBoxElement.HeaderImageAlignment = value;
      }
    }

    [Category("Appearance")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    public ContentAlignment FooterImageAlignment
    {
      get
      {
        return this.groupBoxElement.FooterImageAlignment;
      }
      set
      {
        this.groupBoxElement.FooterImageAlignment = value;
      }
    }

    protected override void OnScroll(ScrollEventArgs se)
    {
      base.OnScroll(se);
      this.Invalidate();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.Invalidate();
    }

    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      if (!this.AutoSize)
        return;
      this.CalcSize();
      e.Control.SizeChanged += new EventHandler(this.Control_SizeChanged);
    }

    protected override void OnControlRemoved(ControlEventArgs e)
    {
      base.OnControlRemoved(e);
      if (!this.AutoSize)
        return;
      this.CalcSize();
      e.Control.SizeChanged -= new EventHandler(this.Control_SizeChanged);
    }

    protected override void ProcessAutoSizeChanged(bool value)
    {
      if (value)
      {
        this.CalcSize();
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          control.SizeChanged += new EventHandler(this.Control_SizeChanged);
      }
      else
      {
        foreach (Control control in (ArrangedElementCollection) this.Controls)
          control.SizeChanged -= new EventHandler(this.Control_SizeChanged);
      }
    }

    private void Control_SizeChanged(object sender, EventArgs e)
    {
      this.CalcSize();
    }

    private void CalcSize()
    {
      Size empty = Size.Empty;
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        empty.Width = Math.Max(empty.Width, control.Right);
        empty.Height = Math.Max(empty.Height, control.Bottom);
      }
      empty.Width += this.Padding.Right;
      empty.Height += this.Padding.Bottom;
      this.ClientSize = empty;
    }

    protected override bool ProcessMnemonic(char charCode)
    {
      if (!this.UseMnemonic || !Control.IsMnemonic(charCode, this.Text) || (!this.Enabled || !this.Visible))
        return false;
      Control parent = this.Parent;
      if (parent != null && parent.SelectNextControl((Control) this, true, false, true, false) && !parent.ContainsFocus)
        parent.Focus();
      return true;
    }

    [Localizable(true)]
    [Category("Appearance")]
    [Description("If true, the first character preceded by an ampersand (&&) will be used as mnemonic key")]
    [DefaultValue(true)]
    public bool UseMnemonic
    {
      get
      {
        return this.GroupBoxElement.Header.TextPrimitive.UseMnemonic;
      }
      set
      {
        this.GroupBoxElement.Header.TextPrimitive.UseMnemonic = value;
      }
    }

    protected override void SetBackColorThemeOverrides()
    {
      this.GroupBoxElement.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, "");
      this.GroupBoxElement.Content.SuspendApplyOfThemeSettings();
      List<string> availableVisualStates = this.GroupBoxElement.Content.GetAvailableVisualStates();
      availableVisualStates.Add("");
      foreach (string state in availableVisualStates)
      {
        this.GroupBoxElement.Content.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state);
        this.GroupBoxElement.Content.SetThemeValueOverride(VisualElement.BackColorProperty, (object) this.BackColor, state, "fillGroupBoxContent");
      }
      this.GroupBoxElement.Content.ResumeApplyOfThemeSettings();
    }

    protected override void ResetBackColorThemeOverrides()
    {
      this.GroupBoxElement.Content.SuspendApplyOfThemeSettings();
      this.GroupBoxElement.ResetThemeValueOverride(VisualElement.BackColorProperty);
      this.GroupBoxElement.Content.ResetThemeValueOverride(VisualElement.BackColorProperty);
      int num = (int) this.GroupBoxElement.Content.Fill.ResetValue(VisualElement.BackColorProperty, ValueResetFlags.Style);
      this.GroupBoxElement.ElementTree.ApplyThemeToElementTree();
      this.GroupBoxElement.Content.ResumeApplyOfThemeSettings();
    }

    protected override void SetForeColorThemeOverrides()
    {
      this.GroupBoxElement.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, "");
      this.GroupBoxElement.Header.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, "");
      this.GroupBoxElement.Footer.SetThemeValueOverride(VisualElement.ForeColorProperty, (object) this.ForeColor, "");
    }

    protected override void ResetForeColorThemeOverrides()
    {
      this.GroupBoxElement.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num1 = (int) this.GroupBoxElement.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.GroupBoxElement.Header.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num2 = (int) this.GroupBoxElement.Header.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.GroupBoxElement.Footer.ResetThemeValueOverride(VisualElement.ForeColorProperty);
      int num3 = (int) this.GroupBoxElement.Footer.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Style);
      this.GroupBoxElement.ElementTree.ApplyThemeToElementTree();
    }
  }
}
