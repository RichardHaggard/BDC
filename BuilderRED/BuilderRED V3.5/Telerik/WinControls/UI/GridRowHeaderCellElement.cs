// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridRowHeaderCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Primitives;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class GridRowHeaderCellElement : GridVirtualizedCellElement
  {
    private bool errorInfoIsSet;
    private ImagePrimitive errorImage;

    public GridRowHeaderCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.Class = "RowHeaderCell";
      this.AllowRowReorder = true;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.errorImage = new ImagePrimitive();
      this.Children.Add((RadElement) this.errorImage);
    }

    protected override void UpdateInfoCore()
    {
      base.UpdateInfoCore();
      this.UpdateErrorInfo();
      this.UpdateImage();
    }

    protected override void BindRowProperties()
    {
      base.BindRowProperties();
      this.RowElement.PropertyChanged += new PropertyChangedEventHandler(this.RowElement_PropertyChanged);
    }

    protected override void UnbindRowProperties()
    {
      this.RowElement.PropertyChanged -= new PropertyChangedEventHandler(this.RowElement_PropertyChanged);
      base.UnbindRowProperties();
    }

    private void RowElement_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "IsCurrent"))
        return;
      this.UpdateInfo();
    }

    protected override void OnRowPropertyChanged(PropertyChangedEventArgs e)
    {
      base.OnRowPropertyChanged(e);
      if (!(e.PropertyName == "ErrorText"))
        return;
      this.UpdateErrorInfo();
      this.UpdateImage();
      this.errorImage.Visibility = ElementVisibility.Collapsed;
      int num = (int) this.errorImage.ResetValue(RadElement.VisibilityProperty, ValueResetFlags.Local);
    }

    private void Unpin_Click(object sender, EventArgs e)
    {
      this.PinRow(PinnedRowPosition.None);
    }

    private void PinAtTop_Click(object sender, EventArgs e)
    {
      this.PinRow(PinnedRowPosition.Top);
    }

    private void PinAtBottom_Click(object sender, EventArgs e)
    {
      this.PinRow(PinnedRowPosition.Bottom);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      this.Layout.Arrange(clientRectangle);
      foreach (RadElement child in this.Children)
      {
        if (child == this.errorImage)
          child.Arrange(new RectangleF((float) ((double) clientRectangle.Right - (double) this.errorImage.DesiredSize.Width - 2.0), clientRectangle.Top + (float) (((double) clientRectangle.Height - (double) this.errorImage.DesiredSize.Height) / 2.0), this.errorImage.DesiredSize.Width, this.errorImage.DesiredSize.Height));
        else
          this.ArrangeElement(child, finalSize);
      }
      return finalSize;
    }

    protected virtual void UpdateErrorInfo()
    {
      if (this.GridViewElement.ShowRowErrors)
      {
        string errorText = this.RowInfo.ErrorText;
        if (!string.IsNullOrEmpty(errorText))
        {
          this.ToolTipText = errorText;
          this.errorInfoIsSet = true;
          return;
        }
      }
      if (!this.errorInfoIsSet)
        return;
      this.ToolTipText = string.Empty;
      this.errorInfoIsSet = false;
    }

    protected virtual void UpdateImage()
    {
      if (!this.IsInValidState(true))
        return;
      Image image = (Image) null;
      if (this.RowInfo is GridViewNewRowInfo)
        image = this.TableElement.NewRowHeaderImage;
      bool flag = this.RowInfo.IsCurrent;
      if (this.GridViewElement.HideSelection && !this.ElementTree.Control.Focused && !this.ElementTree.Control.ContainsFocus)
        flag = false;
      if (flag)
        image = !this.GridViewElement.EditorManager.IsInEditMode ? this.TableElement.CurrentRowHeaderImage : this.TableElement.EditRowHeaderImage;
      if (this.RowInfo is GridViewSearchRowInfo)
        image = this.TableElement.SearchRowHeaderImage;
      if (image != null && this.ElementTree.Control.RightToLeft == RightToLeft.Yes)
      {
        image = (Image) image.Clone();
        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
      }
      this.errorImage.Image = !this.errorInfoIsSet ? (Image) null : this.TableElement.ErrorRowHeaderImage;
      this.Image = image;
    }

    public override RadDropDownMenu MergeMenus(
      IContextMenuManager contextMenuManager,
      params object[] parameters)
    {
      if (this.ViewTemplate.AllowRowHeaderContextMenu)
        return base.MergeMenus(contextMenuManager, parameters);
      return (RadDropDownMenu) null;
    }

    protected internal override void ShowContextMenu()
    {
      if (!this.ViewTemplate.AllowRowHeaderContextMenu)
        return;
      base.ShowContextMenu();
    }

    protected override void CreateContextMenuItems(RadDropDownMenu menu)
    {
      base.CreateContextMenuItems(menu);
      if (!(this.RowInfo is GridViewDataRowInfo))
        return;
      this.CreateRowPinningMenuItems(menu);
    }

    protected virtual void CreateRowPinningMenuItems(RadDropDownMenu contextMenu)
    {
      RadMenuItem radMenuItem1 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinMenuItem"));
      bool flag = this.ShouldShowPinMenuItems();
      RadMenuItem radMenuItem2 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("UnpinRowMenuItem"));
      radMenuItem2.Click += new EventHandler(this.Unpin_Click);
      radMenuItem2.IsChecked = this.RowInfo.PinPosition == PinnedRowPosition.None;
      radMenuItem1.Items.Add((RadItem) radMenuItem2);
      if (flag)
      {
        RadMenuItem radMenuItem3 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinAtTopMenuItem"));
        radMenuItem3.Click += new EventHandler(this.PinAtTop_Click);
        radMenuItem3.IsChecked = this.RowInfo.PinPosition == PinnedRowPosition.Top;
        radMenuItem1.Items.Add((RadItem) radMenuItem3);
        RadMenuItem radMenuItem4 = new RadMenuItem(LocalizationProvider<RadGridLocalizationProvider>.CurrentProvider.GetLocalizedString("PinAtBottomMenuItem"));
        radMenuItem4.Click += new EventHandler(this.PinAtBottom_Click);
        radMenuItem4.IsChecked = this.RowInfo.PinPosition == PinnedRowPosition.Bottom;
        radMenuItem1.Items.Add((RadItem) radMenuItem4);
      }
      contextMenu.Items.Add((RadItem) radMenuItem1);
    }

    private bool ShouldShowPinMenuItems()
    {
      bool flag = true;
      if (this.RowInfo != null && this.RowInfo.Group != null && this.RowInfo.Group.GroupRow != null)
      {
        flag = false;
        int num = 0;
        for (int index = 0; index < this.RowInfo.Group.GroupRow.ChildRows.Count; ++index)
        {
          if (!this.RowInfo.Group.GroupRow.ChildRows[index].IsPinned)
          {
            ++num;
            if (num > 1)
            {
              flag = true;
              break;
            }
          }
        }
      }
      return flag;
    }

    protected virtual void PinRow(PinnedRowPosition position)
    {
      if (this.MasterTemplate.MultiSelect)
      {
        foreach (GridViewRowInfo gridViewRowInfo in new List<GridViewRowInfo>(this.GetRowsForPin()))
          gridViewRowInfo.PinPosition = position;
      }
      else
        this.RowInfo.PinPosition = position;
    }

    private IEnumerable<GridViewRowInfo> GetRowsForPin()
    {
      MasterGridViewTemplate masterTemplate = this.MasterTemplate;
      if (this.MasterTemplate.SelectionMode == GridViewSelectionMode.FullRowSelect)
      {
        foreach (GridViewRowInfo selectedRow in (ReadOnlyCollection<GridViewRowInfo>) masterTemplate.SelectedRows)
          yield return selectedRow;
      }
      else
      {
        foreach (GridViewCellInfo selectedCell in (ReadOnlyCollection<GridViewCellInfo>) masterTemplate.SelectedCells)
          yield return selectedCell.RowInfo;
      }
    }
  }
}
