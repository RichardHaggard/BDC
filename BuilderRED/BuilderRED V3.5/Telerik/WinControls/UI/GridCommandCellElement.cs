// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridCommandCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridCommandCellElement : GridDataCellElement
  {
    public static RoutedEvent OnCommandClick = RadElement.RegisterRoutedEvent(nameof (OnCommandClick), typeof (GridCommandCellElement));
    private RadButtonElement button;

    public GridCommandCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.button = new RadButtonElement();
      this.button.Click += new EventHandler(this.Button_Click);
      this.Children.Add((RadElement) this.button);
    }

    protected override void DisposeManagedResources()
    {
      if (this.button != null)
        this.button.Click -= new EventHandler(this.Button_Click);
      base.DisposeManagedResources();
      this.button = (RadButtonElement) null;
    }

    public override void Detach()
    {
      this.button.Image = (Image) null;
      base.Detach();
    }

    public override bool IsEditable
    {
      get
      {
        return false;
      }
    }

    public RadButtonElement CommandButton
    {
      get
      {
        return this.button;
      }
    }

    private void Button_Click(object sender, EventArgs e)
    {
      MouseEventArgs mouseEventArgs = e as MouseEventArgs;
      if (mouseEventArgs == null || mouseEventArgs.Button != MouseButtons.Left)
        return;
      this.RaiseBubbleEvent((RadElement) this, new RoutedEventArgs(e, GridCommandCellElement.OnCommandClick));
      GridViewCellEventArgs args = new GridViewCellEventArgs(this.RowInfo, this.ColumnInfo, this.GridViewElement.ActiveEditor);
      this.MasterTemplate.EventDispatcher.RaiseEvent<GridViewCellEventArgs>(EventDispatcher.CommandCellClick, (object) this, args);
    }

    protected override void OnColumnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnColumnPropertyChanged(e);
      GridViewCommandColumn columnInfo = (GridViewCommandColumn) this.ColumnInfo;
      if (e.Property == GridViewCommandColumn.ImageProperty)
        this.button.Image = columnInfo.Image;
      else if (e.Property == GridViewColumn.ImageLayoutProperty)
        this.button.ImagePrimitive.ImageLayout = columnInfo.ImageLayout;
      else if (e.Property == GridViewColumn.TextImageRelationProperty)
      {
        this.button.TextImageRelation = columnInfo.TextImageRelation;
      }
      else
      {
        if (e.Property != GridViewColumn.TextAlignmentProperty)
          return;
        this.button.TextAlignment = columnInfo.TextAlignment;
      }
    }

    protected override void UpdateInfoCore()
    {
      base.UpdateInfoCore();
      GridViewCommandColumn columnInfo = this.ColumnInfo as GridViewCommandColumn;
      if (columnInfo == null)
        return;
      this.button.Image = columnInfo.Image;
      this.button.ImageAlignment = columnInfo.ImageAlignment;
      this.button.TextImageRelation = columnInfo.TextImageRelation;
      this.button.TextAlignment = columnInfo.TextAlignment;
    }

    protected override void SetContentCore(object value)
    {
      GridViewCommandColumn columnInfo = this.ColumnInfo as GridViewCommandColumn;
      if (columnInfo == null)
        return;
      if (columnInfo.UseDefaultText)
        this.button.Text = columnInfo.DefaultText;
      else
        this.button.Text = value == null ? string.Empty : this.ApplyFormatString(value);
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      return data is GridViewCommandColumn;
    }
  }
}
