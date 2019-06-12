// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.ScreenTipEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls
{
  [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public class ScreenTipEditor : UITypeEditor
  {
    private ScreenTipUI screenTipUI;

    public override object EditValue(
      ITypeDescriptorContext context,
      System.IServiceProvider provider,
      object value)
    {
      if (provider != null)
      {
        IWindowsFormsEditorService service1 = (IWindowsFormsEditorService) provider.GetService(typeof (IWindowsFormsEditorService));
        ITypeDiscoveryService service2 = (ITypeDiscoveryService) provider.GetService(typeof (ITypeDiscoveryService));
        IDesignerHost service3 = (IDesignerHost) provider.GetService(typeof (IDesignerHost));
        IComponentChangeService service4 = (IComponentChangeService) provider.GetService(typeof (IComponentChangeService));
        if (service1 == null)
          return value;
        if (this.screenTipUI == null)
          this.screenTipUI = new ScreenTipUI(this);
        this.screenTipUI.Initialize(service1, service2, service3, service4, value);
        service1.DropDownControl((Control) this.screenTipUI);
        if (this.screenTipUI.Value != null)
          value = this.screenTipUI.Value;
        this.screenTipUI.End();
      }
      return value;
    }

    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.DropDown;
    }
  }
}
