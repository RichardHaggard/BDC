// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadListDataGroupItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  internal class RadListDataGroupItem : RadListDataItem
  {
    public static readonly RadProperty CollapsibleProperty = RadProperty.Register(nameof (Collapsible), typeof (bool), typeof (RadListDataGroupItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange));
    public static readonly RadProperty CollapsedProperty = RadProperty.Register(nameof (Collapsed), typeof (bool), typeof (RadListDataGroupItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsParentArrange));
    private bool cachedCollapsible;

    public RadListDataGroupItem(ListGroup group)
      : base(group.Header)
    {
      this.group = group;
      this.Collapsed = group.Collapsed;
      this.Collapsible = group.Collapsible;
    }

    public bool Collapsible
    {
      get
      {
        return this.cachedCollapsible;
      }
      set
      {
        if (this.cachedCollapsible == value)
          return;
        this.cachedCollapsible = value;
        this.group.Collapsible = value;
        int num = (int) this.SetValue(RadListDataGroupItem.CollapsibleProperty, (object) value);
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadListDataGroupItem.CollapsibleProperty || this.Collapsible)
        return;
      this.Collapsed = false;
    }

    public bool Collapsed
    {
      get
      {
        return this.group.Collapsed;
      }
      set
      {
        if (!this.Collapsible && (this.Collapsible || value) || value == this.group.Collapsed)
          return;
        this.group.Collapsed = value;
        int num = (int) this.SetValue(RadListDataGroupItem.CollapsedProperty, (object) value);
        this.Owner.Scroller.UpdateScrollRange();
        this.Owner.InvalidateMeasure(true);
      }
    }

    internal override ListGroup Group
    {
      get
      {
        return this.group;
      }
    }
  }
}
