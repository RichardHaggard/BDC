// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TableSizePickerElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layout;

namespace Telerik.WinControls.UI
{
  internal class TableSizePickerElement : StackLayoutElement
  {
    private Size buttonSize = new Size(25, 25);
    private int selectedColumns = 1;
    private int selectedRows = 1;
    private WrapLayoutPanel wrapLayout;
    private LightVisualElement labelElement;
    private int columns;
    private int rows;
    private int maxRows;
    private int maxColumns;

    public event EventHandler<TableSizeSelectedEventArgs> SizeSelected;

    public Size ButtonSize
    {
      get
      {
        return this.buttonSize;
      }
      set
      {
        if (!(this.buttonSize != value))
          return;
        this.buttonSize = value;
        this.OnConstraintsChanged();
      }
    }

    public int SelectedColumns
    {
      get
      {
        return this.selectedColumns;
      }
      set
      {
        if (this.selectedColumns == value)
          return;
        this.selectedColumns = value;
        this.OnSizeSelected(new TableSizeSelectedEventArgs(this.rows, this.columns));
      }
    }

    public int SelectedRows
    {
      get
      {
        return this.selectedRows;
      }
      set
      {
        if (this.selectedRows == value)
          return;
        this.selectedRows = value;
        this.OnSizeSelected(new TableSizeSelectedEventArgs(this.rows, this.columns));
      }
    }

    public int MaxRows
    {
      get
      {
        return this.maxRows;
      }
      set
      {
        if (this.maxRows == value)
          return;
        this.maxRows = value;
        this.OnConstraintsChanged();
      }
    }

    public int MaxColumns
    {
      get
      {
        return this.maxColumns;
      }
      set
      {
        if (this.maxColumns == value)
          return;
        this.maxColumns = value;
        this.OnConstraintsChanged();
      }
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.ElementTree.Control != null && ((this.ElementTree.Control as RadControl).ThemeName == "TelerikMetroTouch" || ThemeResolutionService.ApplicationThemeName == "TelerikMetroTouch"))
        this.wrapLayout.MaxSize = new Size(this.maxColumns * 32, 0);
      return base.MeasureOverride(availableSize);
    }

    protected virtual void OnConstraintsChanged()
    {
      this.wrapLayout.Children.Clear();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RadPrintPreviewDialog));
      for (int index = 1; index <= this.maxRows * this.maxColumns; ++index)
      {
        CommandBarButton commandBarButton = new CommandBarButton();
        commandBarButton.NotifyParentOnMouseInput = true;
        commandBarButton.ShouldHandleMouseInput = false;
        commandBarButton.Shape = (ElementShape) null;
        commandBarButton.MinSize = this.buttonSize;
        commandBarButton.Image = (Image) componentResourceManager.GetObject("page-thumbnail");
        this.wrapLayout.Children.Add((RadElement) commandBarButton);
      }
      this.wrapLayout.MaxSize = new Size(this.maxColumns * this.buttonSize.Width, 0);
      this.ShowOriginalSelection();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      int val1 = 0;
      int val2 = 0;
      foreach (RadElement child in this.wrapLayout.Children)
      {
        if (child.ControlBoundingRectangle.X <= e.Location.X && child.ControlBoundingRectangle.Y <= e.Location.Y)
        {
          child.IsMouseOver = true;
          val1 = Math.Max(val1, val2);
        }
        else
          child.IsMouseOver = false;
        ++val2;
      }
      this.rows = val1 / this.maxColumns + 1;
      this.columns = val1 % this.maxColumns + 1;
      this.labelElement.Text = this.rows.ToString() + " x " + (object) this.columns;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.ShowOriginalSelection();
    }

    private void ShowOriginalSelection()
    {
      int num1 = 0;
      foreach (RadElement child in this.wrapLayout.Children)
      {
        int num2 = num1 / this.maxColumns + 1;
        int num3 = num1 % this.maxColumns + 1;
        child.IsMouseOver = num2 <= this.SelectedRows && num3 <= this.SelectedColumns;
        ++num1;
      }
      this.labelElement.Text = this.SelectedRows.ToString() + " x " + (object) this.SelectedColumns;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      this.selectedColumns = this.columns;
      this.selectedRows = this.rows;
      this.OnSizeSelected(new TableSizeSelectedEventArgs(this.rows, this.columns));
      base.OnMouseDown(e);
    }

    protected virtual void OnSizeSelected(TableSizeSelectedEventArgs args)
    {
      this.ShowOriginalSelection();
      if (this.SizeSelected == null)
        return;
      this.SizeSelected((object) this, args);
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.Orientation = Orientation.Vertical;
      this.wrapLayout = new WrapLayoutPanel();
      this.wrapLayout.MaxSize = new Size(130, 0);
      this.wrapLayout.StretchVertically = false;
      this.Children.Add((RadElement) this.wrapLayout);
      this.labelElement = new LightVisualElement();
      this.labelElement.StretchVertically = false;
      this.labelElement.DrawBorder = true;
      this.labelElement.BorderGradientStyle = GradientStyles.Solid;
      this.Children.Add((RadElement) this.labelElement);
    }
  }
}
