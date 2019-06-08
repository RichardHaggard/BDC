// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadItemsContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [ComVisible(false)]
  public class RadItemsContainer : RadItem
  {
    private RadItemOwnerCollection items;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ShouldHandleMouseInput = true;
      this.items = new RadItemOwnerCollection();
      this.items.ItemTypes = new Type[10]
      {
        typeof (RadItemsContainer),
        typeof (RadRotatorElement),
        typeof (RadArrowButtonElement),
        typeof (RadDropDownListElement),
        typeof (RadButtonElement),
        typeof (RadWebBrowserElement),
        typeof (RadTextBoxElement),
        typeof (RadImageButtonElement),
        typeof (RadCheckBoxElement),
        typeof (RadMaskedEditBoxElement)
      };
      this.items.Owner = (RadElement) this;
      this.items.ItemsChanged += new ItemChangedDelegate(this.items_ItemsChanged);
    }

    protected override void DisposeManagedResources()
    {
      this.items.ItemsChanged -= new ItemChangedDelegate(this.items_ItemsChanged);
      base.DisposeManagedResources();
    }

    private void items_ItemsChanged(
      RadItemCollection changed,
      RadItem target,
      ItemsChangeOperation operation)
    {
      if (target == null)
        return;
      target.NotifyParentOnMouseInput = true;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RadItemOwnerCollection Items
    {
      get
      {
        return this.items;
      }
    }
  }
}
