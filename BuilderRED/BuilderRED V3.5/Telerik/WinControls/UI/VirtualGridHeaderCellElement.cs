// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridHeaderCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.Styles;
using Telerik.WinControls.UI.StateManagers;

namespace Telerik.WinControls.UI
{
  public class VirtualGridHeaderCellElement : VirtualGridCellElement
  {
    private RadSortOrder? sortOrder = new RadSortOrder?();
    public static RadProperty IsSortedAscendingProperty = RadProperty.Register(nameof (IsSortedAscending), typeof (bool), typeof (VirtualGridHeaderCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty IsSortedDescendingProperty = RadProperty.Register(nameof (IsSortedDescending), typeof (bool), typeof (VirtualGridHeaderCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public new const int ResizePointerOffset = 4;
    private ArrowPrimitive arrow;

    static VirtualGridHeaderCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new VirtualGridHeaderCellStateManagerFactory(), typeof (VirtualGridHeaderCellElement));
    }

    public bool IsSortedAscending
    {
      get
      {
        return (bool) this.GetValue(VirtualGridHeaderCellElement.IsSortedAscendingProperty);
      }
    }

    public bool IsSortedDescending
    {
      get
      {
        return (bool) this.GetValue(VirtualGridHeaderCellElement.IsSortedDescendingProperty);
      }
    }

    public RadSortOrder SortOrder
    {
      get
      {
        return this.sortOrder ?? RadSortOrder.None;
      }
    }

    public ArrowPrimitive Arrow
    {
      get
      {
        return this.arrow;
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.arrow = new ArrowPrimitive();
      this.arrow.Alignment = ContentAlignment.MiddleRight;
      this.arrow.Visibility = ElementVisibility.Hidden;
      this.Children.Add((RadElement) this.arrow);
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawBorder = true;
    }

    public override void Synchronize()
    {
      base.Synchronize();
      this.UpdateArrowState();
      this.IsPinned = false;
    }

    public void UpdateArrowState()
    {
      if (!this.IsInValidState(true))
        return;
      RadSortOrder columnSortOrder = this.GetColumnSortOrder();
      if (columnSortOrder == RadSortOrder.None)
      {
        this.sortOrder = new RadSortOrder?(RadSortOrder.None);
        int num1 = (int) this.SetValue(VirtualGridHeaderCellElement.IsSortedAscendingProperty, (object) false);
        int num2 = (int) this.SetValue(VirtualGridHeaderCellElement.IsSortedDescendingProperty, (object) false);
        if (this.Arrow.Visibility == ElementVisibility.Hidden)
          return;
        this.Arrow.Visibility = ElementVisibility.Hidden;
      }
      else
      {
        RadSortOrder radSortOrder1 = columnSortOrder;
        RadSortOrder? sortOrder1 = this.sortOrder;
        RadSortOrder radSortOrder2 = radSortOrder1;
        if ((sortOrder1.GetValueOrDefault() != radSortOrder2 ? 1 : (!sortOrder1.HasValue ? 1 : 0)) != 0)
        {
          this.sortOrder = new RadSortOrder?(radSortOrder1);
          RadSortOrder? sortOrder2 = this.sortOrder;
          ref RadSortOrder? local = ref sortOrder2;
          RadSortOrder valueOrDefault = local.GetValueOrDefault();
          if (local.HasValue)
          {
            switch (valueOrDefault)
            {
              case RadSortOrder.Ascending:
                this.Arrow.Direction = ArrowDirection.Up;
                if (this.Arrow.Visibility != ElementVisibility.Visible)
                {
                  this.Arrow.Visibility = ElementVisibility.Visible;
                  break;
                }
                break;
              case RadSortOrder.Descending:
                this.Arrow.Direction = ArrowDirection.Down;
                if (this.Arrow.Visibility != ElementVisibility.Visible)
                {
                  this.Arrow.Visibility = ElementVisibility.Visible;
                  break;
                }
                break;
              case RadSortOrder.None:
                if (this.Arrow.Visibility != ElementVisibility.Hidden)
                {
                  this.Arrow.Visibility = ElementVisibility.Hidden;
                  break;
                }
                break;
            }
          }
        }
        int num1 = (int) this.SetValue(VirtualGridHeaderCellElement.IsSortedAscendingProperty, (object) (columnSortOrder == RadSortOrder.Ascending));
        int num2 = (int) this.SetValue(VirtualGridHeaderCellElement.IsSortedDescendingProperty, (object) (columnSortOrder == RadSortOrder.Descending));
      }
    }

    protected RadSortOrder GetColumnSortOrder()
    {
      int index = this.ViewInfo.SortDescriptors.IndexOf(this.FieldName);
      if (index < 0 || this.ViewInfo.GridElement != null && !this.ViewInfo.GridElement.AllowSorting)
        return RadSortOrder.None;
      return this.ViewInfo.SortDescriptors[index].Direction != ListSortDirection.Ascending ? RadSortOrder.Descending : RadSortOrder.Ascending;
    }

    public override bool IsCompatible(int data, object context)
    {
      if (data >= 0)
        return context is VirtualGridHeaderRowElement;
      return false;
    }

    public override bool IsInResizeLocation(Point point)
    {
      if (!this.ViewInfo.AllowColumnResize)
        return false;
      if (point.X >= this.ControlBoundingRectangle.X && point.X <= this.ControlBoundingRectangle.X + 4)
      {
        int num = this.Parent.Children.IndexOf((RadElement) this);
        if (!this.RightToLeft)
          return num > 0;
        if (this.ViewInfo.AutoSizeColumnsMode == VirtualGridAutoSizeColumnsMode.Fill)
          return num < this.Parent.Children.Count - 1;
        return true;
      }
      if (point.X < this.ControlBoundingRectangle.Right - 4 || point.X > this.ControlBoundingRectangle.Right)
        return false;
      int num1 = this.Parent.Children.IndexOf((RadElement) this);
      if (this.RightToLeft)
        return num1 > 0;
      if (this.ViewInfo.AutoSizeColumnsMode == VirtualGridAutoSizeColumnsMode.Fill)
        return num1 < this.Parent.Children.Count - 1;
      return true;
    }
  }
}
