// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.RadFormDesignerLite, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (IRootDesigner))]
  public class RadForm : RadFormControlBase
  {
    private bool? allowTheming = new bool?();
    private IContainer components;

    public RadForm()
    {
      this.InitializeComponent();
      this.Behavior.BitmapRepository.DisableBitmapCache = true;
    }

    [SettingsBindable(true)]
    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        if (this.IsInitialized && this.FormElement != null && (this.FormElement.IsInValidState(false) && this.FormElement.TitleBar != null) && this.FormElement.TitleBar.TitlePrimitive != null)
        {
          bool autoEllipsis = this.FormElement.TitleBar.TitlePrimitive.AutoEllipsis;
          this.FormElement.TitleBar.TitlePrimitive.AutoEllipsis = false;
          base.Text = value;
          this.FormElement.TitleBar.TitlePrimitive.AutoEllipsis = autoEllipsis;
        }
        else
          base.Text = value;
      }
    }

    public override string ThemeClassName
    {
      get
      {
        return this.FormBehavior is RadRibbonFormBehavior ? "Telerik.WinControls.UI.RadRibbonForm" : "Telerik.WinControls.UI.RadForm";
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    public RadFormElement FormElement
    {
      get
      {
        return this.FormBehavior.FormElement as RadFormElement;
      }
    }

    protected override Point ScrollToControl(Control activeControl)
    {
      Point control = base.ScrollToControl(activeControl);
      (this.FormBehavior as RadFormBehavior)?.SynchronizeScrollbarsValues();
      return control;
    }

    [Description("Gets or sets the scaling mode of the form's icon.")]
    [Browsable(true)]
    [DefaultValue(ImageScaling.SizeToFit)]
    public ImageScaling IconScaling
    {
      get
      {
        if (this.FormElement != null && this.FormElement.TitleBar != null && this.FormElement.TitleBar.IconPrimitive != null)
          return this.FormElement.TitleBar.IconPrimitive.ImageScaling;
        return ImageScaling.None;
      }
      set
      {
        ImageScaling iconScaling = this.IconScaling;
        if (this.FormElement != null && this.FormElement.TitleBar != null && this.FormElement.TitleBar.IconPrimitive != null)
          this.FormElement.TitleBar.IconPrimitive.ImageScaling = value;
        if (iconScaling == this.IconScaling)
          return;
        this.FormElement.TitleBar.UpdateLayout();
        this.CallSetClientSizeCore(this.ClientSize.Width, this.ClientSize.Height);
        this.Invalidate();
        this.PerformLayout();
        this.LayoutEngine.Layout((object) this, new LayoutEventArgs((Control) this, "ClientSize"));
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override bool AllowTheming
    {
      get
      {
        if (!this.IsInitialized)
          return true;
        if (this.FormBehavior is ThemedFormBehavior)
          return ((ThemedFormBehavior) this.FormBehavior).AllowTheming;
        return false;
      }
      set
      {
        if (!this.IsInitialized)
          this.allowTheming = new bool?(value);
        else
          this.AllowThemingCore(value);
      }
    }

    private void AllowThemingCore(bool value)
    {
      this.allowTheming = new bool?(value);
      if (!(this.FormBehavior is ThemedFormBehavior))
        return;
      ((ThemedFormBehavior) this.FormBehavior).AllowTheming = value;
    }

    public override void EndInit()
    {
      bool? allowTheming1 = this.allowTheming;
      bool allowTheming2 = this.AllowTheming;
      if ((allowTheming1.GetValueOrDefault() != allowTheming2 ? 1 : (!allowTheming1.HasValue ? 1 : 0)) != 0 && this.Site == null && this.allowTheming.HasValue)
        this.AllowThemingCore(this.allowTheming.Value);
      base.EndInit();
    }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      ThemedFormBehavior formBehavior = this.FormBehavior as ThemedFormBehavior;
      if (formBehavior == null || !this.allowTheming.HasValue)
        return;
      bool allowTheming1 = formBehavior.AllowTheming;
      bool? allowTheming2 = this.allowTheming;
      if ((allowTheming1 != allowTheming2.GetValueOrDefault() ? 1 : (!allowTheming2.HasValue ? 1 : 0)) == 0)
        return;
      formBehavior.AllowTheming = this.allowTheming.Value;
    }

    [DefaultValue(true)]
    public new bool ShowIcon
    {
      get
      {
        return base.ShowIcon;
      }
      set
      {
        base.ShowIcon = value;
        if (!(this.FormBehavior is RadFormBehavior))
          return;
        (this.FormBehavior as RadFormBehavior).OnIconVisibilityChanged();
      }
    }

    protected override bool ProcessCaptureChangeRequested(RadElement element, bool capture)
    {
      if (this.FormElement != null && element.IsChildOf((RadElement) this.FormElement) && !(element is ScrollBarThumb))
        return false;
      return base.ProcessCaptureChangeRequested(element, capture);
    }

    protected override FormControlBehavior InitializeFormBehavior()
    {
      return (FormControlBehavior) new RadFormBehavior((IComponentTreeHandler) this, true);
    }

    protected override void SetIconPrimitiveVisibility(bool visible)
    {
      if (this.FormElement == null || this.FormElement.TitleBar == null || this.FormElement.TitleBar.IconPrimitive == null)
        return;
      this.FormElement.TitleBar.IconPrimitive.Visibility = visible ? ElementVisibility.Visible : ElementVisibility.Collapsed;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.Text = nameof (RadForm);
    }
  }
}
