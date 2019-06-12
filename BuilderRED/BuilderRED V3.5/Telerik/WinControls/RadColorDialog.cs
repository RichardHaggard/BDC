// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadColorDialog
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls
{
  [DefaultProperty("SelectedColor")]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.ColorDialogDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [Description("Displays a dialog that can be used to select a color with rich UI and extended functionality.")]
  [TelerikToolboxCategory("Dialogs")]
  public class RadColorDialog : CommonDialog
  {
    private IRadColorDialog colorDialogForm = RadColorEditor.CreateColorDialogInstance();

    public RadColorDialog()
    {
      if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
        return;
      LicenseManager.Validate(this.GetType());
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.colorDialogForm != null)
        (this.colorDialogForm as IDisposable)?.Dispose();
      base.Dispose(disposing);
    }

    public override void Reset()
    {
      if (this.colorDialogForm != null)
        (this.colorDialogForm as IDisposable)?.Dispose();
      this.colorDialogForm = RadColorEditor.CreateColorDialogInstance();
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public IRadColorDialog ColorDialogForm
    {
      get
      {
        return this.colorDialogForm;
      }
    }

    [DefaultValue(null)]
    public Icon Icon
    {
      get
      {
        return ((Form) this.colorDialogForm).Icon;
      }
      set
      {
        ((Form) this.colorDialogForm).Icon = value;
      }
    }

    [AmbientValue(0)]
    [Localizable(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.")]
    public virtual RightToLeft RightToLeft
    {
      get
      {
        return ((Control) this.ColorDialogForm).RightToLeft;
      }
      set
      {
        ((Control) this.ColorDialogForm).RightToLeft = value;
      }
    }

    [DefaultValue("Red")]
    public Color SelectedColor
    {
      get
      {
        return this.colorDialogForm.SelectedColor;
      }
      set
      {
        this.colorDialogForm.SelectedColor = value;
      }
    }

    [DefaultValue("Red")]
    public HslColor SelectedHslColor
    {
      get
      {
        return this.colorDialogForm.SelectedHslColor;
      }
      set
      {
        this.colorDialogForm.SelectedHslColor = value;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public Color[] CustomColors
    {
      get
      {
        return this.colorDialogForm.CustomColors;
      }
    }

    protected override bool RunDialog(IntPtr hwndOwner)
    {
      return ((Form) this.colorDialogForm).ShowDialog((IWin32Window) NativeWindow.FromHandle(hwndOwner)) == DialogResult.OK;
    }
  }
}
