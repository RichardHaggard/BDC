// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadLayoutControlControlCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadLayoutControlControlCollection : Control.ControlCollection
  {
    private RadLayoutControl layoutControl;

    public RadLayoutControlControlCollection(RadLayoutControl owner)
      : base((Control) owner)
    {
      this.layoutControl = owner;
    }

    public void AddInternal(Control value)
    {
      base.Add(value);
    }

    public override void Add(Control value)
    {
      base.Add(value);
      if (value is LayoutControlDraggableOverlay)
        return;
      LayoutControlItem layoutControlItem1 = this.FindLayoutItem(value);
      if (layoutControlItem1 != null || this.layoutControl.IsInitializing)
        return;
      if (this.Owner.Site != null)
      {
        IComponentChangeService service1 = this.Owner.Site.GetService(typeof (IComponentChangeService)) as IComponentChangeService;
        IDesignerHost service2 = this.Owner.Site.GetService(typeof (IDesignerHost)) as IDesignerHost;
        if (service1 != null && service2 != null)
          layoutControlItem1 = service2.CreateComponent(typeof (LayoutControlItem)) as LayoutControlItem;
      }
      LayoutControlItem layoutControlItem2 = layoutControlItem1 ?? new LayoutControlItem();
      layoutControlItem2.Text = value.Text;
      layoutControlItem2.Bounds = value.Bounds;
      layoutControlItem2.AssociatedControl = value;
      this.layoutControl.Items.Add((RadItem) layoutControlItem2);
    }

    public void RemoveInternal(Control value)
    {
      base.Remove(value);
    }

    public override void Remove(Control value)
    {
      base.Remove(value);
      LayoutControlItem layoutItem = this.FindLayoutItem(value);
      if (layoutItem == null)
        return;
      layoutItem.AssociatedControl = (Control) null;
      layoutItem.Dispose();
    }

    public LayoutControlItem FindLayoutItem(Control control)
    {
      using (List<RadElement>.Enumerator enumerator = this.layoutControl.ContainerElement.GetDescendants((Predicate<RadElement>) (x =>
      {
        if (x is LayoutControlItem)
          return ((LayoutControlItem) x).AssociatedControl == control;
        return false;
      }), TreeTraversalMode.BreadthFirst).GetEnumerator())
      {
        if (enumerator.MoveNext())
          return (LayoutControlItem) enumerator.Current;
      }
      return (LayoutControlItem) null;
    }
  }
}
