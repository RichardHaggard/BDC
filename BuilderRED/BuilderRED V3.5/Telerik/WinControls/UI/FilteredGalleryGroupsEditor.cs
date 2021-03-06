﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.FilteredGalleryGroupsEditor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Telerik.WinControls.UI
{
  [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public class FilteredGalleryGroupsEditor : UITypeEditor
  {
    private FilteredItemsEditorUI filteredItemsUI;

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
        ISelectionService service5 = (ISelectionService) provider.GetService(typeof (ISelectionService));
        if (service1 == null)
          return value;
        RadItemOwnerCollection collection = value as RadItemOwnerCollection;
        RadGalleryGroupFilter instance = context.Instance as RadGalleryGroupFilter;
        if (collection == null || instance == null)
          return value;
        if (instance.Owner == null && service5 != null && service5.PrimarySelection != null)
          instance.Owner = (RadGalleryElement) service5.PrimarySelection;
        RadGalleryElement owner = instance.Owner;
        if (owner == null)
          return value;
        if (this.filteredItemsUI == null)
          this.filteredItemsUI = new FilteredItemsEditorUI();
        service4.OnComponentChanging(context.Instance, (MemberDescriptor) TypeDescriptor.GetProperties(context.Instance)["Items"]);
        this.filteredItemsUI.Start(service1, service2, service3, collection, instance, owner);
        if (service1.ShowDialog((Form) this.filteredItemsUI) == DialogResult.OK)
        {
          this.filteredItemsUI.End();
          value = (object) this.filteredItemsUI.Value;
          service4.OnComponentChanged(context.Instance, (MemberDescriptor) TypeDescriptor.GetProperties(context.Instance)["Items"], (object) null, (object) null);
          return value;
        }
      }
      return value;
    }

    public override UITypeEditorEditStyle GetEditStyle(
      ITypeDescriptorContext context)
    {
      return UITypeEditorEditStyle.Modal;
    }
  }
}
