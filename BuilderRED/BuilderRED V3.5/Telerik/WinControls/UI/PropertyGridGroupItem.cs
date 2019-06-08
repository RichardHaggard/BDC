// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridGroupItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class PropertyGridGroupItem : PropertyGridItemBase
  {
    private PropertyGridGroup dataGroup;
    private PropertyGridItemCollection items;

    public PropertyGridGroupItem(
      PropertyGridTableElement propertyGridElement,
      PropertyGridGroup dataGroup)
      : base(propertyGridElement)
    {
      this.dataGroup = dataGroup;
    }

    public override string Label
    {
      get
      {
        if (base.Label != null)
          return base.Label;
        return this.Group.Header;
      }
      set
      {
        base.Label = value;
      }
    }

    public override PropertyGridItemCollection GridItems
    {
      get
      {
        if (this.items == null)
          this.items = new PropertyGridItemCollection(this.Group.GetItems());
        return this.items;
      }
    }

    public override bool Expandable
    {
      get
      {
        return true;
      }
    }

    public PropertyGridGroup Group
    {
      get
      {
        return this.dataGroup;
      }
    }

    public override string Name
    {
      get
      {
        return this.Group.Header;
      }
    }
  }
}
