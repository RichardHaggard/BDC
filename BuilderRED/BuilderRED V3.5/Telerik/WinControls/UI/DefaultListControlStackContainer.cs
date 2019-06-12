// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DefaultListControlStackContainer
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class DefaultListControlStackContainer : VirtualizedStackContainer<RadListDataItem>
  {
    private SizeF _previousAvailableSize = SizeF.Empty;

    public void ForceVisualStateUpdate()
    {
      foreach (RadListVisualItem child in this.Children)
        child.Synchronize();
    }

    public void ForceVisualStateUpdate(RadListDataItem item)
    {
      foreach (RadListVisualItem child in this.Children)
      {
        if (child.Data == item)
        {
          child.Synchronize();
          break;
        }
      }
    }

    protected override bool BeginMeasure(SizeF availableSize)
    {
      RadListElement parent = this.Parent as RadListElement;
      if (parent != null && parent.AutoSizeItems)
      {
        this.SuspendThemeRefresh();
        bool flag = false;
        float num = 0.0f;
        if (this.ElementTree != null)
        {
          DropDownPopupForm control = this.ElementTree.Control as DropDownPopupForm;
          if (control != null)
            num = control.OwnerDropDownListElement.DesiredSize.Width;
        }
        foreach (RadListDataItem data in (IEnumerable<RadListDataItem>) parent.Items)
        {
          if ((double) this._previousAvailableSize.Width != (double) availableSize.Width || data.measureDirty)
          {
            float width = (double) availableSize.Width == 0.0 ? num : availableSize.Width;
            RadElement radElement = this.UpdateElement(data is DescriptionTextListDataItem ? 1 : 0, data) as RadElement;
            if (radElement != null)
            {
              radElement.InvalidateMeasure(true);
              radElement.Measure(parent.FitItemsToSize ? new SizeF(width, float.PositiveInfinity) : LayoutUtils.InfinitySize);
              flag |= data.MeasuredSize != radElement.DesiredSize;
              data.MeasuredSize = radElement.DesiredSize;
            }
          }
        }
        if (flag)
          parent.Scroller.UpdateScrollRange();
        this.ResumeThemeRefresh();
      }
      this._previousAvailableSize = availableSize;
      return base.BeginMeasure(availableSize);
    }

    protected override int FindCompatibleElement(int position, RadListDataItem data)
    {
      if (data is DescriptionTextListDataItem)
      {
        for (int index = position + 1; index < this.Children.Count; ++index)
        {
          DescriptionTextListVisualItem child = this.Children[index] as DescriptionTextListVisualItem;
          if (child != null && child.IsCompatible(data, (object) null))
            return index;
        }
        return -1;
      }
      if (data == null)
        return -1;
      for (int index = position + 1; index < this.Children.Count; ++index)
      {
        if (((RadListVisualItem) this.Children[index]).IsCompatible(data, (object) null))
          return index;
      }
      return -1;
    }

    protected override IVirtualizedElement<RadListDataItem> UpdateElement(
      int position,
      RadListDataItem data)
    {
      object elementContext = this.GetElementContext();
      IVirtualizedElement<RadListDataItem> element;
      if (position < this.Children.Count)
      {
        element = (IVirtualizedElement<RadListDataItem>) this.Children[position];
        if (this.ElementProvider.ShouldUpdate(element, data, elementContext))
        {
          if (position < this.Children.Count - 1)
          {
            IVirtualizedElement<RadListDataItem> child = (IVirtualizedElement<RadListDataItem>) this.Children[position + 1];
            if (child.Data.Equals((object) data))
            {
              this.Children.Move(position + 1, position);
              child.Synchronize();
              return child;
            }
          }
          if (this.ElementProvider.IsCompatible(element, data, elementContext))
          {
            element.Detach();
            element.Attach(data, elementContext);
          }
          else
          {
            int compatibleElement = this.FindCompatibleElement(position, data);
            if (compatibleElement > position)
            {
              this.Children.Move(compatibleElement, position);
              element = (IVirtualizedElement<RadListDataItem>) this.Children[position];
              element.Detach();
              element.Attach(data, elementContext);
            }
            else
            {
              position = Math.Min(position, this.Children.Count);
              element = this.ElementProvider.GetElement(data, elementContext);
              this.InsertElement(position, element, data);
            }
          }
        }
      }
      else
      {
        position = Math.Min(position, this.Children.Count);
        element = this.ElementProvider.GetElement(data, elementContext);
        this.InsertElement(position, element, data);
      }
      return element;
    }
  }
}
