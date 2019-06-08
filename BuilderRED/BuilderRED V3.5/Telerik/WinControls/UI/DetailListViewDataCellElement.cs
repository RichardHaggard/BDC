// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.DetailListViewDataCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class DetailListViewDataCellElement : DetailListViewCellElement
  {
    public static RadProperty SelectedProperty = RadProperty.Register("Selected", typeof (bool), typeof (DetailListViewDataCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty CurrentRowProperty = RadProperty.Register("OwnerCurrent", typeof (bool), typeof (DetailListViewDataCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty ShowGridLinesProperty = RadProperty.Register(nameof (ShowGridLines), typeof (bool), typeof (DetailListViewDataCellElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    private DetailListViewVisualItem row;
    private bool cachedShowGridLines;

    public ListViewDataItem Row
    {
      get
      {
        return this.row.Data;
      }
    }

    public DetailListViewVisualItem RowElement
    {
      get
      {
        return this.row;
      }
    }

    protected bool ShowGridLines
    {
      get
      {
        return this.cachedShowGridLines;
      }
      set
      {
        if (value == this.cachedShowGridLines)
          return;
        this.cachedShowGridLines = value;
        int num = (int) this.SetValue(DetailListViewDataCellElement.ShowGridLinesProperty, (object) value);
      }
    }

    static DetailListViewDataCellElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ListViewDataCellStateManagerFactory(), typeof (DetailListViewDataCellElement));
    }

    public DetailListViewDataCellElement(
      DetailListViewVisualItem owner,
      ListViewDetailColumn column)
      : base(column)
    {
      this.row = owner;
      int num1 = (int) this.BindProperty(DetailListViewDataCellElement.SelectedProperty, (RadObject) owner, BaseListViewVisualItem.SelectedProperty, PropertyBindingOptions.OneWay);
      int num2 = (int) this.BindProperty(DetailListViewDataCellElement.CurrentRowProperty, (RadObject) owner, BaseListViewVisualItem.CurrentProperty, PropertyBindingOptions.OneWay);
    }

    public override void Synchronize()
    {
      if (this.Data == null || this.row == null || this.row.Data == null)
        return;
      int num1 = (int) this.SetValue(DetailListViewCellElement.CurrentProperty, (object) this.column.Current);
      this.Text = Convert.ToString(this.row.Data[this.column]);
      this.ShowGridLines = this.Data.Owner.ShowGridLines;
      if (this.row != null && this.GetFirstVisibleColumn() == this.column)
      {
        this.Image = this.row.Data.Image;
      }
      else
      {
        int num2 = (int) this.ResetValue(LightVisualElement.ImageProperty);
      }
      this.SynchronizeStyleProperties();
      this.column.Owner.OnCellFormatting(new ListViewCellFormattingEventArgs((DetailListViewCellElement) this));
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      return this.Data.Owner.AllowDragDrop;
    }

    protected override void DisposeManagedResources()
    {
      int num1 = (int) this.UnbindProperty(DetailListViewDataCellElement.SelectedProperty);
      int num2 = (int) this.UnbindProperty(DetailListViewDataCellElement.CurrentRowProperty);
      base.DisposeManagedResources();
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      this.row.CallDoDoubleClick(e);
    }

    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
      this.row.CallDoClick(e);
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.IsMouseOverElementProperty || !this.IsInValidState(true) || this.Data == null)
        return;
      if (this.Data.Owner.HotTracking)
      {
        int num1 = (int) this.row.SetValue(BaseListViewVisualItem.HotTrackingProperty, e.NewValue);
      }
      else
      {
        int num2 = (int) this.row.SetValue(BaseListViewVisualItem.HotTrackingProperty, (object) false);
      }
    }

    private ListViewDetailColumn GetFirstVisibleColumn()
    {
      if (this.row == null || this.row.Data == null || this.row.Data.Owner == null)
        return (ListViewDetailColumn) null;
      DetailListViewElement viewElement = this.row.Data.Owner.ViewElement as DetailListViewElement;
      if (viewElement == null)
      {
        if (this.Data.Owner.Columns.Count <= 0)
          return (ListViewDetailColumn) null;
        return this.Data.Owner.Columns[0];
      }
      foreach (ListViewDetailColumn column in (Collection<ListViewDetailColumn>) viewElement.Owner.Columns)
      {
        if (column.Visible)
          return column;
      }
      if (this.Data.Owner.Columns.Count <= 0)
        return (ListViewDetailColumn) null;
      return this.Data.Owner.Columns[0];
    }

    private void SynchronizeStyleProperties()
    {
      if (this.row != null && this.row.Data != null && this.row.Data.HasStyle)
      {
        if (this.row.Data.Font != ListViewDataItemStyle.DefaultFont && this.row.Data.Font != this.Font)
          this.Font = this.row.Data.Font;
        else if (this.row.Data.Font == ListViewDataItemStyle.DefaultFont)
        {
          int num1 = (int) this.ResetValue(VisualElement.FontProperty, ValueResetFlags.Local);
        }
        if (this.row.Data.ForeColor != ListViewDataItemStyle.DefaultForeColor && this.row.Data.ForeColor != this.ForeColor)
          this.ForeColor = this.row.Data.ForeColor;
        else if (this.row.Data.ForeColor == ListViewDataItemStyle.DefaultForeColor)
        {
          int num2 = (int) this.ResetValue(VisualElement.ForeColorProperty, ValueResetFlags.Local);
        }
        if (this.row.Data.TextAlignment != this.TextAlignment)
          this.TextAlignment = this.row.Data.TextAlignment;
        else if (this.row.Data.TextAlignment == ListViewDataItemStyle.DefaultTextAlignment)
        {
          int num3 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
          int num4 = (int) this.SetDefaultValueOverride(LightVisualElement.TextAlignmentProperty, (object) ListViewDataItemStyle.DefaultTextAlignment);
        }
        if (this.row.Data.TextImageRelation != ListViewDataItemStyle.DefaultTextImageRelation && this.row.Data.TextImageRelation != this.TextImageRelation)
          this.TextImageRelation = this.row.Data.TextImageRelation;
        else if (this.row.Data.TextImageRelation == ListViewDataItemStyle.DefaultTextImageRelation)
        {
          int num3 = (int) this.ResetValue(LightVisualElement.TextImageRelationProperty, ValueResetFlags.Local);
          int num4 = (int) this.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) ListViewDataItemStyle.DefaultTextImageRelation);
        }
        if (this.row.Data.ImageAlignment != ListViewDataItemStyle.DefaultImageAlignment && this.row.Data.ImageAlignment != this.ImageAlignment)
        {
          this.ImageAlignment = this.row.Data.ImageAlignment;
        }
        else
        {
          if (this.row.Data.ImageAlignment != ListViewDataItemStyle.DefaultImageAlignment)
            return;
          int num3 = (int) this.ResetValue(LightVisualElement.ImageAlignmentProperty, ValueResetFlags.Local);
          int num4 = (int) this.SetDefaultValueOverride(LightVisualElement.ImageAlignmentProperty, (object) ListViewDataItemStyle.DefaultImageAlignment);
        }
      }
      else
      {
        if (this.GetValueSource(VisualElement.FontProperty) != ValueSource.Style)
        {
          int num1 = (int) this.ResetValue(VisualElement.FontProperty);
        }
        if (this.GetValueSource(VisualElement.ForeColorProperty) != ValueSource.Style)
        {
          int num2 = (int) this.ResetValue(VisualElement.ForeColorProperty);
        }
        if (this.GetValueSource(LightVisualElement.TextAlignmentProperty) != ValueSource.Style)
        {
          int num3 = (int) this.ResetValue(LightVisualElement.TextAlignmentProperty);
          int num4 = (int) this.SetDefaultValueOverride(LightVisualElement.TextAlignmentProperty, (object) ListViewDataItemStyle.DefaultTextAlignment);
        }
        if (this.GetValueSource(LightVisualElement.TextImageRelationProperty) != ValueSource.Style)
        {
          int num3 = (int) this.ResetValue(LightVisualElement.TextImageRelationProperty);
          int num4 = (int) this.SetDefaultValueOverride(LightVisualElement.TextImageRelationProperty, (object) ListViewDataItemStyle.DefaultTextImageRelation);
        }
        if (this.GetValueSource(LightVisualElement.ImageAlignmentProperty) == ValueSource.Style)
          return;
        int num5 = (int) this.ResetValue(LightVisualElement.ImageAlignmentProperty);
        int num6 = (int) this.SetDefaultValueOverride(LightVisualElement.ImageAlignmentProperty, (object) ListViewDataItemStyle.DefaultImageAlignment);
      }
    }
  }
}
