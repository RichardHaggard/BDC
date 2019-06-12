// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.SimpleListViewElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class SimpleListViewElement : BaseListViewElement
  {
    public SimpleListViewElement(RadListViewElement owner)
      : base(owner)
    {
    }

    protected override VirtualizedStackContainer<ListViewDataItem> CreateViewElement()
    {
      return (VirtualizedStackContainer<ListViewDataItem>) new SimpleListViewContainer((BaseListViewElement) this);
    }

    public override void UpdateHScrollbarMaximum()
    {
      int val2 = 0;
      ITraverser<ListViewDataItem> enumerator = (ITraverser<ListViewDataItem>) this.Scroller.Traverser.GetEnumerator();
      enumerator.Reset();
      bool flag = this.Owner.ShowGroups && this.Owner.Groups.Count > 0 && (this.Owner.EnableGrouping || this.Owner.EnableCustomGrouping) && !this.FullRowSelect;
      while (enumerator.MoveNext())
        val2 = Math.Max(enumerator.Current.ActualSize.Width + (enumerator.Current is ListViewDataItemGroup || !flag ? 0 : this.GroupIndent), val2);
      if (this.HScrollBar.Maximum != val2)
      {
        if (this.HScrollBar.Value + this.HScrollBar.LargeChange - 1 > val2)
        {
          this.HScrollBar.Maximum = val2;
          this.HScrollBar.Value = val2 - this.HScrollBar.LargeChange + 1;
        }
        else
          this.HScrollBar.Maximum = val2;
      }
      this.UpdateHScrollbarVisibility();
    }

    protected override void HScrollBar_ValueChanged(object sender, EventArgs e)
    {
      this.Owner.EndEdit();
      base.HScrollBar_ValueChanged(sender, e);
    }

    protected override void UpdateHScrollbarVisibility()
    {
      if (this.HorizontalScrollState == ScrollState.AutoHide)
        this.HScrollBar.Visibility = this.HScrollBar.LargeChange < this.HScrollBar.Maximum ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      else
        this.HScrollBar.Visibility = this.HorizontalScrollState == ScrollState.AlwaysShow ? ElementVisibility.Visible : ElementVisibility.Collapsed;
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.BoundsProperty || this.HScrollBar.Value <= this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1)
        return;
      this.HScrollBar.Value = this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1;
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName == "VerticalScrollState")
        this.Scroller.ScrollState = this.VerticalScrollState;
      if (!(propertyName == "HorizontalScrollState"))
        return;
      this.UpdateHScrollbarVisibility();
      this.Owner.Update(RadListViewElement.UpdateModes.UpdateLayout);
    }

    protected override void EnsureItemVisibleHorizontal(ListViewDataItem item)
    {
      RadElement element = (RadElement) this.GetElement(item);
      if (element == null)
        return;
      this.HScrollBar.Value = element.Bounds.Left;
    }

    protected override bool SupportsOrientation(Orientation orientation)
    {
      return orientation == Orientation.Vertical;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      if (this.VScrollBar.ControlBoundingRectangle.Contains(args.Location) || this.HScrollBar.ControlBoundingRectangle.Contains(args.Location))
        return;
      int num1 = this.VScrollBar.Value - args.Offset.Height;
      if (num1 > this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1)
        num1 = this.VScrollBar.Maximum - this.VScrollBar.LargeChange + 1;
      if (num1 < this.VScrollBar.Minimum)
        num1 = this.VScrollBar.Minimum;
      this.VScrollBar.Value = num1;
      int num2 = this.HScrollBar.Value - args.Offset.Width;
      if (num2 > this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1)
        num2 = this.HScrollBar.Maximum - this.HScrollBar.LargeChange + 1;
      if (num2 < this.HScrollBar.Minimum)
        num2 = this.HScrollBar.Minimum;
      this.HScrollBar.Value = num2;
      args.Handled = true;
    }
  }
}
