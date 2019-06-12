// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadContextMenuManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Licensing;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  [Description("Adds the RadContextMenu dynamic property and enables using RadContextMenu in all controls.")]
  [ToolboxBitmap(typeof (RadContextMenu), "RadContextMenuManager.bmp")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [ToolboxItem(true)]
  [ProvideProperty("RadContextMenu", typeof (Control))]
  [Designer("Telerik.WinControls.UI.Design.RadContextMenuManagerDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [TelerikToolboxCategory("Menus & Toolbars")]
  public class RadContextMenuManager : Component, IExtenderProvider
  {
    private Hashtable contextMenus = new Hashtable();

    public bool CanExtend(object extendee)
    {
      if (extendee is Control && !(extendee is RadTreeView))
        return !(extendee is RadTextBoxControl);
      return false;
    }

    [DefaultValue(null)]
    [DisplayName("RadContextMenu")]
    [Category("Behavior")]
    public RadContextMenu GetRadContextMenu(Control control)
    {
      if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
        LicenseManager.Validate(this.GetType());
      return (RadContextMenu) this.contextMenus[(object) control];
    }

    public void SetRadContextMenu(Control control, RadContextMenu value)
    {
      this.contextMenus[(object) control] = (object) value;
      if (value != null)
        control.MouseDown += new MouseEventHandler(this.control_MouseDown);
      else
        control.MouseDown -= new MouseEventHandler(this.control_MouseDown);
    }

    private void control_MouseDown(object sender, MouseEventArgs e)
    {
      RadContextMenu contextMenu = (RadContextMenu) this.contextMenus[sender];
      if (contextMenu == null || e.Button != MouseButtons.Right)
        return;
      contextMenu.Show((Control) sender, e.X, e.Y);
    }
  }
}
