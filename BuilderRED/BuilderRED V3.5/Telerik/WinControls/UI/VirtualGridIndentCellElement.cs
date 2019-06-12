// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridIndentCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class VirtualGridIndentCellElement : VirtualGridCellElement
  {
    private bool isWaiting;
    private bool hasError;
    private bool isRowInEditMode;
    private ExpanderItem expanderItem;

    public bool IsWaiting
    {
      get
      {
        return this.isWaiting;
      }
      set
      {
        this.isWaiting = value;
      }
    }

    public bool HasError
    {
      get
      {
        return this.hasError;
      }
      set
      {
        this.hasError = value;
      }
    }

    public bool IsRowInEditMode
    {
      get
      {
        return this.isRowInEditMode;
      }
      set
      {
        this.isRowInEditMode = value;
      }
    }

    public ExpanderItem ExpanderItem
    {
      get
      {
        return this.expanderItem;
      }
    }

    public bool ShowExpanderItem
    {
      get
      {
        return this.expanderItem.Visibility == ElementVisibility.Visible;
      }
      set
      {
        this.expanderItem.Visibility = value ? ElementVisibility.Visible : ElementVisibility.Collapsed;
      }
    }

    public VirtualGridIndentCellElement()
    {
      this.DrawText = false;
    }

    protected override void UpdateInfo(VirtualGridCellValueNeededEventArgs args)
    {
      base.UpdateInfo(args);
      this.UpdateImage();
      if (!this.ShowExpanderItem)
        return;
      this.expanderItem.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.expanderItem_RadPropertyChanged);
      this.expanderItem.Expanded = this.ViewInfo.IsRowExpanded(this.RowIndex);
      this.expanderItem.RadPropertyChanged += new RadPropertyChangedEventHandler(this.expanderItem_RadPropertyChanged);
    }

    protected override void OnRowElementPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnRowElementPropertyChanged(e);
      if (!(e.PropertyName == "IsInEditMode") && !(e.PropertyName == "RightToLeft"))
        return;
      this.UpdateImage();
    }

    protected virtual void UpdateImage()
    {
      this.IsWaiting = this.ViewInfo.IsRowWaiting(this.RowIndex);
      this.HasError = this.ViewInfo.RowHasError(this.RowIndex);
      this.IsRowInEditMode = this.RowElement.IsInEditMode;
      this.ToolTipText = string.Empty;
      this.ShowExpanderItem = this.RowElement.HasChildRows && !this.IsWaiting && !this.HasError;
      if (this.RowIndex < 0)
        return;
      if (this.HasError)
      {
        this.Image = this.TableElement.RowErrorImage;
        this.ToolTipText = this.ViewInfo.GetRowErrorText(this.RowIndex);
      }
      else if (this.IsRowInEditMode && this.TableElement.EditRowHeaderImage != null)
      {
        Bitmap bitmap = new Bitmap(this.TableElement.EditRowHeaderImage);
        if (this.RightToLeft)
          bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
        this.Image = (Image) bitmap;
      }
      else if (this.IsCurrentRow && this.TableElement.CurrentRowHeaderImage != null)
      {
        Bitmap bitmap = new Bitmap(this.TableElement.CurrentRowHeaderImage);
        if (this.RightToLeft)
          bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
        this.Image = (Image) bitmap;
      }
      else if (this.IsWaiting)
      {
        this.Image = this.TableElement.RowWaitingImage;
      }
      else
      {
        int num = (int) this.ResetValue(LightVisualElement.ImageProperty, ValueResetFlags.Local);
      }
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.expanderItem = (ExpanderItem) new VirtualGridExpanderItem();
      this.expanderItem.StretchHorizontally = this.expanderItem.StretchVertically = true;
      this.Children.Add((RadElement) this.expanderItem);
    }

    protected internal override void Attach(int data, object context, bool synchronize)
    {
      base.Attach(data, context, synchronize);
      this.expanderItem.RadPropertyChanged += new RadPropertyChangedEventHandler(this.expanderItem_RadPropertyChanged);
    }

    public override void Detach()
    {
      base.Detach();
      this.expanderItem.RadPropertyChanged -= new RadPropertyChangedEventHandler(this.expanderItem_RadPropertyChanged);
    }

    private void expanderItem_RadPropertyChanged(object sender, RadPropertyChangedEventArgs e)
    {
      if (e.Property != ExpanderItem.ExpandedProperty)
        return;
      if (this.expanderItem.Expanded)
      {
        if (this.TableElement.ExpandRow(this.RowIndex))
          return;
        this.expanderItem.SuspendPropertyNotifications();
        this.expanderItem.Expanded = false;
        this.expanderItem.ResumePropertyNotifications();
      }
      else
        this.TableElement.CollapseRow(this.RowIndex);
    }

    public override bool IsCompatible(int data, object context)
    {
      return data == -1;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return new SizeF((float) this.TableElement.IndentColumnWidth, this.RowIndex >= 0 ? (float) this.TableElement.RowsViewState.GetItemSize(this.RowIndex, true, false) : (float) this.TableElement.GetRowHeight(this.RowIndex));
    }
  }
}
