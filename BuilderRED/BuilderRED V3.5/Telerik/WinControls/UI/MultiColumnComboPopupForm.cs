// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.MultiColumnComboPopupForm
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class MultiColumnComboPopupForm : RadEditorPopupControlBase
  {
    private GridViewHostItem editorElement;
    private bool autoFilter;
    internal bool closed;

    static MultiColumnComboPopupForm()
    {
      RuntimeHelpers.RunClassConstructor(typeof (RadMultiColumnComboBoxElement).TypeHandle);
    }

    public MultiColumnComboPopupForm(PopupEditorBaseElement owner)
      : base((RadItem) owner)
    {
      this.FadeAnimationType = FadeAnimationType.FadeOut;
    }

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.InitializeEditorElement();
      if (this.editorElement != null)
        this.SizingGripDockLayout.Children.Add((RadElement) this.editorElement);
      parent.Children[0].Children[1].Visibility = ElementVisibility.Visible;
      this.SizingGripDockLayout.LastChildFill = true;
    }

    protected override void Dispose(bool disposing)
    {
      this.UnwireEvents();
      base.Dispose(disposing);
    }

    protected virtual void InitializeEditorElement()
    {
      this.editorElement = new GridViewHostItem();
      this.EditorControl.ShowGroupPanel = false;
      this.EditorControl.ReadOnly = true;
      this.EditorControl.MasterTemplate.AllowAddNewRow = false;
      this.EditorControl.MasterTemplate.EnableGrouping = false;
      this.EditorControl.MasterTemplate.EnableFiltering = false;
      this.EditorControl.MasterTemplate.ShowFilteringRow = false;
      this.EditorControl.MasterTemplate.AllowColumnChooser = false;
      this.EditorControl.MasterTemplate.AllowCellContextMenu = false;
      this.WireEvents();
    }

    protected void WireEvents()
    {
      if (this.editorElement == null)
        return;
      this.EditorControl.Rows.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Rows_CollectionChanged);
      this.EditorControl.SortChanged += new GridViewCollectionChangedEventHandler(this.EditorControl_SortChanged);
      this.EditorControl.ContextMenuOpening += new ContextMenuOpeningEventHandler(this.EditorControl_ContextMenuOpening);
    }

    protected void UnwireEvents()
    {
      if (this.editorElement == null || this.EditorControl.Disposing || this.EditorControl.IsDisposed)
        return;
      this.EditorControl.Rows.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.Rows_CollectionChanged);
      this.EditorControl.SortChanged -= new GridViewCollectionChangedEventHandler(this.EditorControl_SortChanged);
      this.EditorControl.ContextMenuOpening -= new ContextMenuOpeningEventHandler(this.EditorControl_ContextMenuOpening);
    }

    protected override CreateParams CreateParams
    {
      get
      {
        CreateParams createParams = base.CreateParams;
        createParams.Style |= 33554432;
        return createParams;
      }
    }

    public IGridView EditorElement
    {
      get
      {
        return (IGridView) this.editorElement.HostedGridView.TableElement;
      }
    }

    [DefaultValue(false)]
    public virtual bool AutoFilter
    {
      get
      {
        return this.autoFilter;
      }
      set
      {
        if (this.autoFilter == value)
          return;
        this.autoFilter = value;
        this.OnNotifyPropertyChanged(nameof (AutoFilter));
      }
    }

    [Browsable(false)]
    public virtual RadMultiColumnComboBoxElement OwnerComboItem
    {
      get
      {
        return (RadMultiColumnComboBoxElement) this.OwnerElement;
      }
    }

    public RadGridView EditorControl
    {
      get
      {
        return (RadGridView) this.editorElement.HostedGridView;
      }
    }

    public GridViewRowInfo FindItem(string startsWith)
    {
      List<GridViewRowInfo> allItems = this.FindAllItems(startsWith);
      if (allItems.Count > 0)
        return allItems[0];
      return (GridViewRowInfo) null;
    }

    public List<GridViewRowInfo> FindAllItems(string startsWith)
    {
      List<GridViewRowInfo> gridViewRowInfoList = new List<GridViewRowInfo>();
      if (!string.IsNullOrEmpty(startsWith) && !string.IsNullOrEmpty(this.OwnerComboItem.DisplayMember))
      {
        GridViewDataColumn column = this.EditorControl.Columns[this.OwnerComboItem.DisplayMember];
        if (column != null)
        {
          for (int index = 0; index < this.EditorControl.Rows.Count; ++index)
          {
            string text = this.GetText(this.EditorControl.Rows[index].Cells[column.Name].Value);
            if (!string.IsNullOrEmpty(text) && text.StartsWith(startsWith, !this.EditorControl.MasterTemplate.CaseSensitive, CultureInfo.InvariantCulture))
              gridViewRowInfoList.Add(this.EditorControl.Rows[index]);
          }
        }
      }
      return gridViewRowInfoList;
    }

    public GridViewRowInfo FindItemExact(string text)
    {
      GridViewDataColumn displayColumn = this.OwnerComboItem.DisplayColumn;
      if (displayColumn == null)
        return (GridViewRowInfo) null;
      return this.FindItemExact(text, displayColumn.FieldName);
    }

    public GridViewRowInfo FindItemExact(string text, string field)
    {
      GridViewDataColumn[] columnByFieldName = this.EditorControl.Columns.GetColumnByFieldName(field);
      if (columnByFieldName.Length > 0)
      {
        for (int index = 0; index < this.EditorControl.Rows.Count; ++index)
        {
          string text1 = this.GetText(this.EditorControl.Rows[index].Cells[columnByFieldName[0].Name].Value);
          if (!string.IsNullOrEmpty(text1) && text1.Equals(text, this.EditorControl.MasterTemplate.CaseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
            return this.EditorControl.Rows[index];
        }
      }
      return (GridViewRowInfo) null;
    }

    public override void ShowPopup(Rectangle alignmentRectangle)
    {
      base.ShowPopup(alignmentRectangle);
      int num = this.EditorControl.TableElement.VScrollBar.Value;
      if (this.OwnerComboItem.EditorControl.CurrentRow == null)
      {
        this.EditorControl.TableElement.VScrollBar.Value = num;
      }
      else
      {
        this.EditorControl.TableElement.ScrollToRow(this.OwnerComboItem.EditorControl.CurrentRow);
        if (this.AnimationEnabled && ThemeResolutionService.AllowAnimations)
          return;
        Size nonAnimatedSize = this.NonAnimatedSize;
        this.AutoSize = false;
        this.Size = Size.Empty;
        this.Size = nonAnimatedSize;
        if (this.ShouldRestoreAutoSize)
          this.AutoSize = true;
        this.Invalidate();
      }
    }

    public override bool CanClosePopup(RadPopupCloseReason reason)
    {
      if (reason == RadPopupCloseReason.Mouse && this.OwnerComboItem != null && (!this.OwnerComboItem.IsDisposed && !this.OwnerComboItem.IsDisposing))
      {
        Point client = this.OwnerComboItem.ElementTree.Control.PointToClient(Control.MousePosition);
        Rectangle boundingRectangle = this.OwnerComboItem.ArrowButton.ControlBoundingRectangle;
        RadElement elementAtPoint = this.OwnerComboItem.EditorControl.ElementTree.GetElementAtPoint(this.OwnerComboItem.EditorControl.ElementTree.Control.PointToClient(Control.MousePosition));
        if (Control.MouseButtons == MouseButtons.Left && boundingRectangle.Contains(client) || this.OwnerComboItem.DropDownStyle == RadDropDownStyle.DropDownList && this.OwnerComboItem.ControlBoundingRectangle.Contains(client) || (elementAtPoint is GridSearchCellButtonElement || !this.OwnerComboItem.EditorControl.ReadOnly && elementAtPoint is RadCheckmark))
          return false;
      }
      return true;
    }

    public override bool OnKeyDown(Keys keyData)
    {
      switch (keyData)
      {
        case Keys.Back:
        case Keys.Return:
          return false;
        case Keys.Escape:
          this.OwnerComboItem.ProcessEscKey(new KeyEventArgs(keyData));
          break;
      }
      return base.OnKeyDown(keyData);
    }

    public override bool OnMouseWheel(Control target, int delta)
    {
      this.RootElement.InvalidateMeasure(true);
      this.editorElement.CallDoMouseWheel(new MouseEventArgs(MouseButtons.None, 0, 0, 0, delta));
      return true;
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName == "ThemeName")
        this.EditorControl.ThemeName = this.ThemeName;
      if (!(propertyName == "AutoFilter"))
        return;
      this.EditorControl.EnableFiltering = this.AutoFilter;
    }

    protected override void WndProc(ref Message msg)
    {
      if (msg.Msg == 513 && this.OwnerComboItem != null)
        this.OwnerComboItem.KeyboardCommandIssued = false;
      base.WndProc(ref msg);
    }

    private void EditorControl_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
    {
      e.ContextMenu = (RadDropDownMenu) null;
    }

    private void EditorControl_SortChanged(object sender, GridViewCollectionChangedEventArgs e)
    {
      this.OwnerComboItem.CallOnSortedChanged(new EventArgs());
    }

    private void Rows_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      switch (e.Action)
      {
        case NotifyCollectionChangedAction.Remove:
          if (this.EditorControl.SelectedRows.Count < 1)
            this.EditorControl.CurrentRow = (GridViewRowInfo) null;
          for (int index = 0; index < e.NewItems.Count; ++index)
          {
            if (this.EditorControl.CurrentRow == e.NewItems[index])
            {
              this.EditorControl.CurrentRow = (GridViewRowInfo) null;
              break;
            }
          }
          break;
        case NotifyCollectionChangedAction.Reset:
          this.EditorControl.CurrentRow = (GridViewRowInfo) null;
          break;
      }
    }

    protected override void OnPopupOpened()
    {
      base.OnPopupOpened();
      this.closed = false;
      if (this.OwnerComboItem.EditorControl.CurrentRow == null)
        return;
      this.EditorControl.TableElement.ScrollToRow(this.OwnerComboItem.EditorControl.CurrentRow);
    }

    protected override void OnPopupClosed(RadPopupClosedEventArgs args)
    {
      base.OnPopupClosed(args);
      this.closed = true;
    }

    private string GetText(object item)
    {
      GridCellElement gridCellElement = item as GridCellElement;
      if (gridCellElement != null)
        return gridCellElement.Text;
      if (item != null)
        return Convert.ToString(item);
      return (string) null;
    }
  }
}
