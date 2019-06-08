// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridHyperlinkCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridHyperlinkCellElement : GridDataCellElement
  {
    private GridHyperlinkCellContentElement contentElement;
    private Point clickLocation;

    public GridHyperlinkCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
      this.MouseLeave += new EventHandler(this.OnGridHyperlinkCellElementMouseLeave);
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      base.Initialize(column, row);
      GridViewHyperlinkCellInfo cell = this.RowInfo.Cells[this.ColumnInfo.Name] as GridViewHyperlinkCellInfo;
      if (cell == null)
        return;
      this.contentElement.Visited = cell.Visited;
    }

    protected override void DisposeManagedResources()
    {
      this.MouseLeave -= new EventHandler(this.OnGridHyperlinkCellElementMouseLeave);
      base.DisposeManagedResources();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawText = false;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.contentElement = new GridHyperlinkCellContentElement();
      this.Children.Add((RadElement) this.contentElement);
      int num = (int) this.contentElement.BindProperty(LightVisualElement.TextAlignmentProperty, (RadObject) this, LightVisualElement.TextAlignmentProperty, PropertyBindingOptions.OneWay);
    }

    public GridHyperlinkCellContentElement ContentElement
    {
      get
      {
        return this.contentElement;
      }
      set
      {
        this.contentElement = value;
      }
    }

    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
        this.contentElement.Text = value;
      }
    }

    public override Image Image
    {
      get
      {
        return this.contentElement.Image;
      }
      set
      {
        base.Image = (Image) null;
        this.contentElement.Image = value;
      }
    }

    protected override void SetContentCore(object value)
    {
      this.contentElement.AutoEllipsis = this.ColumnInfo.AutoEllipsis;
      this.contentElement.TextWrap = this.ColumnInfo.WrapText;
      this.contentElement.Alignment = this.ColumnInfo.TextAlignment;
      base.SetContentCore(value);
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      return data is GridViewHyperlinkColumn;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left)
        return;
      this.clickLocation = e.Location;
      this.contentElement.IsMouseDown = true;
      GridViewHyperlinkColumn columnInfo = this.ColumnInfo as GridViewHyperlinkColumn;
      if (columnInfo == null || columnInfo.HyperlinkOpenAction != HyperlinkOpenAction.SingleClick)
        return;
      RadElement elementAtPoint = GridVisualElement.GetElementAtPoint<RadElement>((RadElementTree) this.GridViewElement.ElementTree, this.clickLocation);
      if (columnInfo.HyperlinkOpenArea != HyperlinkOpenArea.Cell && !(elementAtPoint is GridHyperlinkCellContentElement))
        return;
      this.ProcessHyperlink();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.IsMouseDown = false;
      this.contentElement.IsMouseDown = false;
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      GridViewHyperlinkColumn columnInfo = this.ColumnInfo as GridViewHyperlinkColumn;
      if (columnInfo == null || columnInfo.HyperlinkOpenAction != HyperlinkOpenAction.DoubleClick)
        return;
      RadElement elementAtPoint = GridVisualElement.GetElementAtPoint<RadElement>((RadElementTree) this.GridViewElement.ElementTree, this.clickLocation);
      if (columnInfo.HyperlinkOpenArea != HyperlinkOpenArea.Cell && !(elementAtPoint is GridHyperlinkCellContentElement))
        return;
      this.ProcessHyperlink();
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      base.ArrangeOverride(finalSize);
      SizeF sizeF = finalSize;
      if (this.SelfReferenceLayout != null)
        sizeF.Width -= (float) (int) this.SelfReferenceLayout.StackLayoutElement.DesiredSize.Width;
      float x = 0.0f;
      float y = 0.0f;
      Padding borderThickness = this.GetBorderThickness(true);
      switch (this.contentElement.Alignment)
      {
        case ContentAlignment.TopLeft:
          x = (float) (this.Padding.Left + this.contentElement.Margin.Left + borderThickness.Left);
          y = (float) (this.Padding.Top + this.contentElement.Margin.Top + borderThickness.Top);
          break;
        case ContentAlignment.TopCenter:
          x = (float) (((double) sizeF.Width - (double) this.contentElement.DesiredSize.Width) / 2.0);
          y = (float) (this.Padding.Top + this.contentElement.Margin.Top + borderThickness.Top);
          break;
        case ContentAlignment.TopRight:
          x = sizeF.Width - this.contentElement.DesiredSize.Width - (float) this.Padding.Right - (float) this.contentElement.Margin.Right - (float) borderThickness.Right;
          y = (float) (this.Padding.Top + this.contentElement.Margin.Top + borderThickness.Top);
          break;
        case ContentAlignment.MiddleLeft:
          x = (float) (this.Padding.Left + this.contentElement.Margin.Left + borderThickness.Left);
          y = (float) (((double) sizeF.Height - (double) this.contentElement.DesiredSize.Height) / 2.0);
          break;
        case ContentAlignment.MiddleCenter:
          x = (float) (((double) sizeF.Width - (double) this.contentElement.DesiredSize.Width) / 2.0);
          y = (float) (((double) sizeF.Height - (double) this.contentElement.DesiredSize.Height) / 2.0);
          break;
        case ContentAlignment.MiddleRight:
          x = sizeF.Width - this.contentElement.DesiredSize.Width - (float) this.Padding.Right - (float) this.contentElement.Margin.Right - (float) borderThickness.Right;
          y = (float) (((double) sizeF.Height - (double) this.contentElement.DesiredSize.Height) / 2.0);
          break;
        case ContentAlignment.BottomLeft:
          x = (float) (this.Padding.Left + this.contentElement.Margin.Left + borderThickness.Left);
          y = (float) (this.Padding.Bottom + this.contentElement.Margin.Bottom + borderThickness.Bottom);
          break;
        case ContentAlignment.BottomCenter:
          x = (float) (((double) sizeF.Width - (double) this.contentElement.DesiredSize.Width) / 2.0);
          y = (float) (this.Padding.Bottom + this.contentElement.Margin.Bottom + borderThickness.Bottom);
          break;
        case ContentAlignment.BottomRight:
          x = sizeF.Width - this.contentElement.DesiredSize.Width - (float) this.Padding.Right - (float) this.contentElement.Margin.Right - (float) borderThickness.Right;
          y = (float) (this.Padding.Bottom + this.contentElement.Margin.Bottom + borderThickness.Bottom);
          break;
      }
      if (this.SelfReferenceLayout != null && !this.RightToLeft)
        x += (float) (int) this.SelfReferenceLayout.StackLayoutElement.DesiredSize.Width;
      this.contentElement.Arrange(new RectangleF(x, y, this.contentElement.DesiredSize.Width, this.contentElement.DesiredSize.Height));
      return finalSize;
    }

    protected virtual void ProcessHyperlink()
    {
      string hyperlink = (string) null;
      if (this.Value != null)
        hyperlink = this.Value.ToString();
      HyperlinkOpeningEventArgs args1 = new HyperlinkOpeningEventArgs(hyperlink, this.RowInfo, this.ColumnInfo);
      this.ViewTemplate.EventDispatcher.RaiseEvent<HyperlinkOpeningEventArgs>(EventDispatcher.HyperlinkOpening, (object) this.ViewTemplate, args1);
      if (args1.Cancel)
        return;
      this.contentElement.Visited = true;
      GridViewHyperlinkCellInfo cell = this.RowInfo.Cells[this.ColumnInfo.Name] as GridViewHyperlinkCellInfo;
      if (cell != null)
        cell.Visited = this.contentElement.Visited;
      string error = (string) null;
      try
      {
        this.ProcessHyperlinkCore(hyperlink);
      }
      catch (Exception ex)
      {
        error = ex.Message;
      }
      HyperlinkOpenedEventArgs args2 = new HyperlinkOpenedEventArgs(hyperlink, this.RowInfo, this.ColumnInfo, error);
      this.ViewTemplate.EventDispatcher.RaiseEvent<HyperlinkOpenedEventArgs>(EventDispatcher.HyperlinkOpened, (object) this.ViewTemplate, args2);
    }

    protected virtual void ProcessHyperlinkCore(string hyperlink)
    {
      if (string.IsNullOrEmpty(hyperlink))
        return;
      Process.Start(hyperlink);
    }

    private void OnGridHyperlinkCellElementMouseLeave(object sender, EventArgs e)
    {
      this.IsMouseDown = false;
      this.contentElement.IsMouseDown = false;
    }

    protected override void OnColumnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnColumnPropertyChanged(e);
      if (e.Property.Name == "AutoEllipsis")
        this.contentElement.AutoEllipsis = this.ColumnInfo.AutoEllipsis;
      if (e.Property.Name == "WrapText")
        this.contentElement.TextWrap = this.ColumnInfo.WrapText;
      if (!(e.Property.Name == "TextAlignment"))
        return;
      this.contentElement.Alignment = this.ColumnInfo.TextAlignment;
    }
  }
}
