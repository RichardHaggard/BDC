// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadRibbonForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.RadFormDesignerLite, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (IRootDesigner))]
  public class RadRibbonForm : RadFormControlBase
  {
    private Padding defaultBorderThickness = Padding.Empty;

    static RadRibbonForm()
    {
      RuntimeHelpers.RunClassConstructor(typeof (RibbonFormElement).TypeHandle);
    }

    [Browsable(false)]
    public new FormBorderStyle FormBorderStyle
    {
      get
      {
        return base.FormBorderStyle;
      }
      set
      {
        this.CheckBorderWidth(value);
        base.FormBorderStyle = value;
      }
    }

    public RibbonFormElement FormElement
    {
      get
      {
        RadRibbonFormBehavior formBehavior = this.FormBehavior as RadRibbonFormBehavior;
        if (formBehavior != null)
          return formBehavior.FormElement as RibbonFormElement;
        return (RibbonFormElement) null;
      }
    }

    public RadRibbonBar RibbonBar
    {
      get
      {
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          if (control is RadRibbonBar)
            return control as RadRibbonBar;
        }
        return (RadRibbonBar) null;
      }
    }

    public bool AllowAero
    {
      get
      {
        return (this.FormBehavior as RadRibbonFormBehavior).AllowTheming;
      }
      set
      {
        (this.FormBehavior as RadRibbonFormBehavior).AllowTheming = value;
      }
    }

    protected override bool HasOwnToolbar
    {
      get
      {
        return this.FormBehavior is RadRibbonFormBehavior;
      }
    }

    public override string ThemeClassName
    {
      get
      {
        return "Telerik.WinControls.UI.RadRibbonForm";
      }
      set
      {
        base.ThemeClassName = value;
      }
    }

    private void CheckBorderWidth(FormBorderStyle value)
    {
      if (value == FormBorderStyle.None)
      {
        this.defaultBorderThickness = this.FormBehavior.FormElement.BorderThickness;
        this.FormBehavior.FormElement.BorderThickness = Padding.Empty;
      }
      else
      {
        if (!(this.defaultBorderThickness != Padding.Empty) || !(this.FormBehavior.FormElement.BorderThickness == Padding.Empty))
          return;
        this.FormBehavior.FormElement.BorderThickness = this.defaultBorderThickness;
        this.defaultBorderThickness = Padding.Empty;
      }
    }

    protected override FormControlBehavior InitializeFormBehavior()
    {
      return (FormControlBehavior) new RadRibbonFormBehavior((IComponentTreeHandler) this, true);
    }

    protected override void SetIconPrimitiveVisibility(bool visible)
    {
      RadRibbonBar ribbonBar = this.RibbonBar;
      if (ribbonBar == null || ribbonBar.RibbonBarElement == null || ribbonBar.RibbonBarElement.IconPrimitive == null)
        return;
      ribbonBar.RibbonBarElement.IconPrimitive.Visibility = visible ? ElementVisibility.Visible : ElementVisibility.Collapsed;
    }

    private bool IsRibbonBarInForm()
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control is RadRibbonBar)
          return true;
      }
      return false;
    }

    protected virtual void AddRibbonBarInForm()
    {
      IDesignerHost service1 = this.GetService(typeof (IDesignerHost)) as IDesignerHost;
      if (service1 == null)
        return;
      RadRibbonBar component = service1.CreateComponent(typeof (RadRibbonBar)) as RadRibbonBar;
      if (component == null)
        return;
      IComponentChangeService service2 = this.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
      if (service2 == null)
        return;
      component.Dock = DockStyle.Top;
      service2.OnComponentChanging((object) this, (MemberDescriptor) TypeDescriptor.GetProperties((object) this)["Controls"]);
      this.Controls.Add((Control) component);
      service2.OnComponentChanged((object) this, (MemberDescriptor) TypeDescriptor.GetProperties((object) this)["Controls"], (object) null, (object) null);
    }

    protected override void OnShown(EventArgs e)
    {
      base.OnShown(e);
      if (!this.IsDesignMode || this.IsRibbonBarInForm())
        return;
      this.AddRibbonBarInForm();
    }
  }
}
