// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.LayoutControlXmlSerializer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using Telerik.WinControls.XmlSerialization;

namespace Telerik.WinControls.UI
{
  public class LayoutControlXmlSerializer : ComponentXmlSerializer
  {
    private RadLayoutControl layoutControl;
    private Dictionary<string, LayoutControlItemBase> layoutControlItems;
    private Dictionary<string, Control> controls;

    public LayoutControlXmlSerializer(
      RadLayoutControl layoutControl,
      ComponentXmlSerializationInfo componentSerializationInfo)
      : base(componentSerializationInfo)
    {
      this.layoutControl = layoutControl;
      this.layoutControlItems = new Dictionary<string, LayoutControlItemBase>();
      foreach (LayoutControlItemBase allItem in this.layoutControl.GetAllItems())
      {
        if (!string.IsNullOrEmpty(allItem.Name) && !this.layoutControlItems.ContainsKey(allItem.Name))
          this.layoutControlItems.Add(allItem.Name, allItem);
      }
      this.controls = new Dictionary<string, Control>();
      foreach (Control control in (ArrangedElementCollection) layoutControl.Controls)
      {
        if (!string.IsNullOrEmpty(control.Name) && !this.controls.ContainsKey(control.Name))
          this.controls.Add(control.Name, control);
      }
    }

    protected override object MatchExistingItem(
      XmlReader reader,
      IList toRead,
      object parent,
      PropertyDescriptor parentProperty,
      string propertyToMatch,
      string uniquePropertyValue,
      IList existingInstancesToMatch,
      ref int foundAtIndex)
    {
      foundAtIndex = -1;
      object obj = (object) null;
      if (string.IsNullOrEmpty(uniquePropertyValue))
        return (object) null;
      LayoutControlItemBase layoutControlItemBase1;
      if (parentProperty.Name == "Items" && this.layoutControlItems.TryGetValue(uniquePropertyValue, out layoutControlItemBase1))
        obj = (object) layoutControlItemBase1;
      LayoutControlItemBase layoutControlItemBase2;
      if (parentProperty.Name == "ItemGroups" && this.layoutControlItems.TryGetValue(uniquePropertyValue, out layoutControlItemBase2) && layoutControlItemBase2 is LayoutControlGroupItem)
        obj = (object) layoutControlItemBase2;
      if (obj == null)
        obj = base.MatchExistingItem(reader, toRead, parent, parentProperty, propertyToMatch, uniquePropertyValue, existingInstancesToMatch, ref foundAtIndex);
      return obj;
    }

    protected internal override void SetPropertyValue(
      PropertyDescriptor property,
      object propertyOwner,
      object value)
    {
      if (property.Name == "AssociatedControlName")
      {
        string key = Convert.ToString(value);
        LayoutControlItem layoutControlItem = propertyOwner as LayoutControlItem;
        if (layoutControlItem != null && this.controls.ContainsKey(key))
        {
          layoutControlItem.AssociatedControl = this.controls[key];
          return;
        }
      }
      base.SetPropertyValue(property, propertyOwner, value);
    }

    protected override bool ProcessListOverride(
      XmlReader reader,
      object listOwner,
      PropertyDescriptor ownerProperty,
      IList list)
    {
      if (!(list is RadItemOwnerCollection))
        return base.ProcessListOverride(reader, listOwner, ownerProperty, list);
      this.ReadMergeCollection(reader, listOwner, ownerProperty, list, "Name", false, true);
      return true;
    }
  }
}
