// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class PropertyGridItemElementProvider : VirtualizedPanelElementProvider<PropertyGridItemBase, PropertyGridItemElementBase>
  {
    private PropertyGridTableElement propertyGridElement;

    public PropertyGridItemElementProvider(PropertyGridTableElement propertyGridElement)
    {
      this.propertyGridElement = propertyGridElement;
    }

    protected PropertyGridTableElement PropertyGridElement
    {
      get
      {
        return this.propertyGridElement;
      }
    }

    public override IVirtualizedElement<PropertyGridItemBase> CreateElement(
      PropertyGridItemBase data,
      object context)
    {
      if (this.propertyGridElement != null)
      {
        CreatePropertyGridItemElementEventArgs e = new CreatePropertyGridItemElementEventArgs(data);
        PropertyGridItem propertyGridItem = data as PropertyGridItem;
        if (propertyGridItem != null)
          e.ItemElementType = (object) propertyGridItem.PropertyType == (object) typeof (bool) || (object) propertyGridItem.PropertyType == (object) typeof (bool?) || (object) propertyGridItem.PropertyType == (object) typeof (ToggleState) ? typeof (PropertyGridCheckBoxItemElement) : ((object) propertyGridItem.PropertyType != (object) typeof (Color) ? typeof (PropertyGridItemElement) : typeof (PropertyGridColorItemElement));
        if (data is PropertyGridGroupItem)
          e.ItemElementType = typeof (PropertyGridGroupElement);
        this.propertyGridElement.OnCreateItemElement(e);
        if ((object) e.ItemElementType != null)
          return (IVirtualizedElement<PropertyGridItemBase>) Activator.CreateInstance(e.ItemElementType);
      }
      return base.CreateElement(data, context);
    }

    public override SizeF GetElementSize(PropertyGridItemBase item)
    {
      return new SizeF(0.0f, (float) this.propertyGridElement.ItemHeight);
    }
  }
}
