// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class PropertyGridElement : StackLayoutElement
  {
    private PropertyGridSplitElement splitElement;
    private PropertyGridToolbarElement toolbarElement;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.splitElement = this.CreateSplitElement();
      this.splitElement.StretchHorizontally = true;
      this.splitElement.StretchVertically = true;
      this.toolbarElement = this.CreateToolbarElement();
      this.toolbarElement.StretchHorizontally = true;
      this.toolbarElement.Visibility = ElementVisibility.Collapsed;
      this.Children.Add((RadElement) this.toolbarElement);
      this.Children.Add((RadElement) this.splitElement);
    }

    protected virtual PropertyGridSplitElement CreateSplitElement()
    {
      return new PropertyGridSplitElement();
    }

    protected virtual PropertyGridToolbarElement CreateToolbarElement()
    {
      return new PropertyGridToolbarElement();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.StretchHorizontally = true;
      this.StretchVertically = true;
      this.Orientation = Orientation.Vertical;
      this.FitInAvailableSize = true;
    }

    public PropertyGridSplitElement SplitElement
    {
      get
      {
        return this.splitElement;
      }
    }

    public PropertyGridTableElement PropertyTableElement
    {
      get
      {
        return this.splitElement.PropertyTableElement;
      }
    }

    public PropertyGridToolbarElement ToolbarElement
    {
      get
      {
        return this.toolbarElement;
      }
    }

    public float ToolbarElementHeight
    {
      get
      {
        return this.ToolbarElement.ToolbarElementHeight;
      }
      set
      {
        this.ToolbarElement.ToolbarElementHeight = value;
      }
    }

    public bool ToolbarVisible
    {
      get
      {
        return this.toolbarElement.Visibility == ElementVisibility.Visible;
      }
      set
      {
        if (value)
          this.toolbarElement.Visibility = ElementVisibility.Visible;
        else
          this.toolbarElement.Visibility = ElementVisibility.Collapsed;
        this.OnNotifyPropertyChanged("SearchBarVisible");
      }
    }

    public bool EnableSorting
    {
      get
      {
        return this.PropertyTableElement.CollectionView.CanSort;
      }
      set
      {
        if (this.EnableSorting == value)
          return;
        this.PropertyTableElement.CollectionView.CanSort = value;
        if (value)
          this.ToolbarElement.AlphabeticalToggleButton.Visibility = ElementVisibility.Visible;
        else
          this.ToolbarElement.AlphabeticalToggleButton.Visibility = ElementVisibility.Collapsed;
      }
    }

    public bool EnableGrouping
    {
      get
      {
        return this.PropertyTableElement.CollectionView.CanGroup;
      }
      set
      {
        if (this.EnableGrouping == value)
          return;
        this.PropertyTableElement.CollectionView.CanGroup = value;
        if (value)
          this.ToolbarElement.CategorizedToggleButton.Visibility = ElementVisibility.Visible;
        else
          this.ToolbarElement.CategorizedToggleButton.Visibility = ElementVisibility.Collapsed;
      }
    }

    public bool EnableFiltering
    {
      get
      {
        return this.PropertyTableElement.CollectionView.CanFilter;
      }
      set
      {
        if (this.EnableFiltering == value)
          return;
        this.PropertyTableElement.CollectionView.CanFilter = value;
        ((TextBoxBase) this.ToolbarElement.SearchTextBoxElement.TextBoxItem.HostedControl).ReadOnly = !value;
      }
    }

    public bool EnableCustomGrouping
    {
      get
      {
        return this.PropertyTableElement.EnableCustomGrouping;
      }
      set
      {
        this.PropertyTableElement.EnableCustomGrouping = value;
      }
    }

    public void BestFit()
    {
      this.PropertyTableElement.BestFit();
    }

    public void BestFit(PropertyGridBestFitMode mode)
    {
      this.PropertyTableElement.BestFit(mode);
    }

    public void ExpandAllGridItems()
    {
      this.PropertyTableElement.BeginUpdate();
      foreach (PropertyGridGroupItem group in this.PropertyTableElement.Groups)
      {
        group.Expanded = true;
        this.SetExpandedState((IList<PropertyGridItem>) group.GridItems, true);
      }
      if (this.PropertyTableElement.Groups.Count == 0)
      {
        foreach (PropertyGridItem propertyGridItem in this.PropertyTableElement.CollectionView)
        {
          if (propertyGridItem.Expandable)
          {
            propertyGridItem.Expanded = true;
            this.SetExpandedState((IList<PropertyGridItem>) propertyGridItem.GridItems, true);
          }
        }
      }
      this.PropertyTableElement.EndUpdate(true, PropertyGridTableElement.UpdateActions.ExpandedChanged);
    }

    public void CollapseAllGridItems()
    {
      this.PropertyTableElement.BeginUpdate();
      foreach (PropertyGridGroupItem group in this.PropertyTableElement.Groups)
      {
        group.Expanded = false;
        this.SetExpandedState((IList<PropertyGridItem>) group.GridItems, false);
      }
      if (this.PropertyTableElement.Groups.Count == 0)
      {
        foreach (PropertyGridItem propertyGridItem in this.PropertyTableElement.CollectionView)
        {
          if (propertyGridItem.Expandable)
          {
            propertyGridItem.Expanded = false;
            this.SetExpandedState((IList<PropertyGridItem>) propertyGridItem.GridItems, false);
          }
        }
      }
      this.PropertyTableElement.EndUpdate(true, PropertyGridTableElement.UpdateActions.ExpandedChanged);
    }

    public void ResetSelectedProperty()
    {
      (this.PropertyTableElement.SelectedGridItem as PropertyGridItem)?.ResetValue();
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      this.PropertyTableElement.BeginUpdate();
      this.PropertyTableElement.EndUpdate(true, PropertyGridTableElement.UpdateActions.Reset);
    }

    private void SetExpandedState(IList<PropertyGridItem> items, bool newState)
    {
      foreach (PropertyGridItem propertyGridItem in (IEnumerable<PropertyGridItem>) items)
      {
        if (propertyGridItem.Expandable)
        {
          propertyGridItem.Expanded = newState;
          this.SetExpandedState((IList<PropertyGridItem>) propertyGridItem.GridItems, newState);
        }
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Height = Math.Min(availableSize.Height, sizeF.Height);
      return sizeF;
    }

    protected override void OnStyleChanged(RadPropertyChangedEventArgs e)
    {
      base.OnStyleChanged(e);
      if (this.PropertyTableElement.IsEditing)
        this.PropertyTableElement.CancelEdit();
      if (e.NewValue == null)
        return;
      this.PropertyTableElement.ViewElement.DisposeChildren();
      this.PropertyTableElement.ViewElement.ElementProvider.ClearCache();
      this.PropertyTableElement.ViewElement.InvalidateMeasure();
    }
  }
}
