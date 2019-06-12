// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ColumnChooserItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class ColumnChooserItem : ColumnChooserVisualElement
  {
    private GridViewColumn columnInfo;
    private IRadServiceProvider serviceProvider;
    private GridViewColumnGroup group;

    public ColumnChooserItem()
    {
    }

    static ColumnChooserItem()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (ColumnChooserItem));
    }

    public ColumnChooserItem(GridViewColumn column, IRadServiceProvider serviceProvider)
    {
      this.columnInfo = column;
      this.DisableHTMLRendering = column.DisableHTMLRendering;
      this.Text = this.columnInfo.HeaderText;
      this.serviceProvider = serviceProvider;
      this.WireEvents();
    }

    public ColumnChooserItem(GridViewColumnGroup group, IRadServiceProvider serviceProvider)
    {
      this.group = group;
      this.Text = group.Text;
      this.serviceProvider = serviceProvider;
      this.WireEvents();
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.AllowDrag = true;
      this.AllowDrop = true;
      this.Class = "ColumnChooserItemElement";
      this.BypassLayoutPolicies = true;
      this.StretchHorizontally = true;
      this.MinSize = new Size(0, 18);
    }

    protected override void DisposeManagedResources()
    {
      this.UnwireEvents();
      this.columnInfo = (GridViewColumn) null;
      base.DisposeManagedResources();
    }

    public GridViewColumn ColumnInfo
    {
      get
      {
        return this.columnInfo;
      }
    }

    public void WireEvents()
    {
      if (this.columnInfo != null)
        this.columnInfo.PropertyChanged += new PropertyChangedEventHandler(this.DataColumn_PropertyChanged);
      if (this.group == null)
        return;
      this.group.PropertyChanged += new PropertyChangedEventHandler(this.group_PropertyChanged);
    }

    public void UnwireEvents()
    {
      if (this.columnInfo != null)
        this.columnInfo.PropertyChanged -= new PropertyChangedEventHandler(this.DataColumn_PropertyChanged);
      if (this.group == null)
        return;
      this.group.PropertyChanged -= new PropertyChangedEventHandler(this.group_PropertyChanged);
    }

    protected override object GetDragContextCore()
    {
      if (this.columnInfo != null)
        return (object) this.columnInfo;
      return (object) this.group;
    }

    protected override Image GetDragHintCore()
    {
      return (Image) RadGridViewDragDropService.GetDragImageHint(this.TextAlignment, base.GetDragHintCore() as Bitmap, this.Layout.RightPart.Bounds, 100);
    }

    protected override bool ProcessDragOver(Point currentMouseLocation, ISupportDrag dragObject)
    {
      GridViewColumn dataContext = dragObject.GetDataContext() as GridViewColumn;
      if (dataContext != null && dataContext.OwnerTemplate != null && dataContext.OwnerTemplate.MasterTemplate != this.ColumnInfo.OwnerTemplate.MasterTemplate)
        return false;
      return base.ProcessDragOver(currentMouseLocation, dragObject);
    }

    private void DataColumn_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      switch (e.PropertyName)
      {
        case "HeaderText":
          this.Text = this.columnInfo.HeaderText;
          break;
      }
    }

    private void group_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Text"))
        return;
      this.Text = this.group.Text;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left || this.serviceProvider == null)
        return;
      this.serviceProvider.GetService<RadDragDropService>().Start((object) this);
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      if (this.ColumnInfo != null)
      {
        if (this.ColumnInfo.OwnerTemplate != null)
        {
          ColumnGroupsViewDefinition viewDefinition = this.ColumnInfo.OwnerTemplate.ViewDefinition as ColumnGroupsViewDefinition;
          if (viewDefinition != null && viewDefinition.FindGroup(this.ColumnInfo) == null)
            return;
        }
        this.ColumnInfo.IsVisible = true;
      }
      if (this.group == null)
        return;
      this.group.IsVisible = true;
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      RadDragDropService service = this.serviceProvider.GetService<RadDragDropService>();
      if (args.IsBegin)
      {
        int num = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) true);
        service.BeginDrag(this.ElementTree.Control.PointToScreen(args.Location), (ISupportDrag) this);
      }
      if (service.State == RadServiceState.Working)
        service.DoMouseMove(this.ElementTree.Control.PointToScreen(args.Location));
      if (args.IsEnd)
      {
        service.EndDrag();
        int num1 = (int) this.SetValue(RadElement.IsMouseDownProperty, (object) false);
        int num2 = (int) this.ResetValue(RadElement.IsMouseDownProperty, ValueResetFlags.Local);
      }
      args.Handled = true;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF = base.MeasureOverride(availableSize);
      sizeF.Height = Math.Max(sizeF.Height, (float) this.MinSize.Height);
      return sizeF;
    }
  }
}
