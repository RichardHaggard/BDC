// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadPageViewExplorerBarItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Analytics;

namespace Telerik.WinControls.UI
{
  public class RadPageViewExplorerBarItem : RadPageViewStackItem
  {
    public static RadProperty IsExpandedProperty = RadProperty.Register(nameof (IsExpanded), typeof (bool), typeof (RadPageViewExplorerBarItem), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.CanInheritValue | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsParentMeasure | ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.Cancelable));
    private int itemPageLength = 300;
    internal const int DEFAULT_PAGE_LENGTH = 300;
    private RadPageViewContentAreaElement associatedContentArea;

    public RadPageViewExplorerBarItem()
    {
    }

    public RadPageViewExplorerBarItem(string text)
      : base(text)
    {
    }

    public RadPageViewExplorerBarItem(string text, Image image)
      : base(text, image)
    {
    }

    private void DoExpandCollapse(bool isExpanded)
    {
      if (this.Owner == null)
        return;
      if (isExpanded)
        (this.Owner as RadPageViewExplorerBarElement).ExpandItem(this);
      else
        (this.Owner as RadPageViewExplorerBarElement).CollapseItem(this);
    }

    private bool TryExpandCollapse(bool isExpanding)
    {
      if (this.Owner == null)
        return false;
      if (isExpanding)
        return (this.Owner as RadPageViewExplorerBarElement).OnItemExpanding(this);
      return (this.Owner as RadPageViewExplorerBarElement).OnItemCollapsing(this);
    }

    internal override int PageLength
    {
      get
      {
        return this.itemPageLength;
      }
      set
      {
        if (value == this.itemPageLength)
          return;
        this.itemPageLength = value;
        if (this.Owner == null)
          return;
        this.Owner.InvalidateMeasure();
      }
    }

    internal override bool IsContentVisible
    {
      get
      {
        return this.IsExpanded;
      }
      set
      {
        this.IsExpanded = value;
        if (this.Page == null)
          return;
        this.Page.IsContentVisible = value;
      }
    }

    [DefaultValue(false)]
    [Description("Gets or sets a boolean value that determines whether the content of the item is expanded.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(RadPageViewExplorerBarItem.IsExpandedProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadPageViewExplorerBarItem.IsExpandedProperty, (object) value);
      }
    }

    public RadPageViewContentAreaElement AssociatedContentAreaElement
    {
      get
      {
        return this.associatedContentArea;
      }
      set
      {
        if (this.associatedContentArea != value)
          this.associatedContentArea = value;
        if (this.associatedContentArea == null)
          return;
        this.associatedContentArea.Visibility = this.IsExpanded ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (RadPageViewStackItem);
      }
    }

    protected override void OnPropertyChanging(RadPropertyChangingEventArgs args)
    {
      if (args.Property == RadPageViewExplorerBarItem.IsExpandedProperty)
        args.Cancel = this.TryExpandCollapse((bool) args.NewValue);
      base.OnPropertyChanging(args);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadPageViewExplorerBarItem.IsExpandedProperty)
        return;
      if (this.Page != null)
        this.Page.IsContentVisible = (bool) e.NewValue;
      this.DoExpandCollapse((bool) e.NewValue);
      if (this.Page == null)
        return;
      this.AssociatedContentAreaElement.Visibility = this.Page.IsContentVisible ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      ControlTraceMonitor.TrackAtomicFeature((RadElement) this, this.Page.IsContentVisible ? "Expanded" : "Collapsed", (object) this.Text);
    }

    public override void Attach(RadPageViewPage page)
    {
      base.Attach(page);
      if (page.Owner == null || !page.Owner.IsHandleCreated)
        return;
      this.Page.Visible = this.IsExpanded;
    }

    public override void Detach()
    {
      if (this.Page.Owner != null && this.Page.Owner.IsHandleCreated)
        this.Page.Visible = false;
      base.Detach();
    }
  }
}
