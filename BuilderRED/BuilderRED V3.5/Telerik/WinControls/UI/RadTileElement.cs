// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadTileElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Layouts;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [ToolboxItem(false)]
  [Designer("Telerik.WinControls.UI.Design.RadTileElementDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class RadTileElement : LightVisualElement
  {
    private int rowSpan = 1;
    private int colSpan = 1;
    private int column;
    private int row;
    private Padding cellPadding;
    private SizeF currentPanOffset;
    private Point panBeginLocation;
    private Point lastMouseMove;

    static RadTileElement()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (RadTileElement));
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(0)]
    [Description("Gets the zero-based index of the column in which the tile should be arranged.")]
    public int Column
    {
      get
      {
        return this.column;
      }
      set
      {
        if (this.column == value)
          return;
        this.column = value;
        int num = (int) this.SetValue(GridLayout.ColumnIndexProperty, (object) value);
      }
    }

    [Description("Gets the zero-based index of the row in which the tile should be arranged.")]
    [Browsable(true)]
    [DefaultValue(0)]
    [Category("Appearance")]
    public int Row
    {
      get
      {
        return this.row;
      }
      set
      {
        if (this.row == value)
          return;
        this.row = value;
        int num = (int) this.SetValue(GridLayout.RowIndexProperty, (object) value);
      }
    }

    [Browsable(true)]
    [Description("Gets or sets the number of cells that the tile should occupy in a column.")]
    [DefaultValue(1)]
    [Category("Appearance")]
    public int RowSpan
    {
      get
      {
        return this.rowSpan;
      }
      set
      {
        if (this.rowSpan == value)
          return;
        this.rowSpan = value;
        int num = (int) this.SetValue(GridLayout.RowSpanProperty, (object) value);
      }
    }

    [Description("Gets or sets the number of cells that the tile should occupy in a row.")]
    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(1)]
    public int ColSpan
    {
      get
      {
        return this.colSpan;
      }
      set
      {
        if (this.colSpan == value)
          return;
        this.colSpan = value;
        int num = (int) this.SetValue(GridLayout.ColSpanProperty, (object) value);
      }
    }

    [DefaultValue(typeof (Padding), "0, 0, 0, 0")]
    [Description("Gets or sets the padding according to the currently occupied cell.")]
    [Browsable(true)]
    [Category("Appearance")]
    public Padding CellPadding
    {
      get
      {
        return TelerikDpiHelper.ScalePadding(this.cellPadding, this.DpiScaleFactor);
      }
      set
      {
        if (!(this.cellPadding != value))
          return;
        this.cellPadding = value;
        int num = (int) this.SetValue(GridLayout.CellPaddingProperty, (object) value);
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.PositionOffsetProperty && e.Property != RadElement.ScaleTransformProperty)
        return;
      this.ElementTree.RootElement.Invalidate();
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.DrawFill = true;
      this.DrawBorder = false;
      this.BackColor = Color.FromArgb((int) byte.MaxValue, 27, 72, 137);
      this.GradientStyle = GradientStyles.Solid;
      this.Font = new Font("Segoe UI Light", 16f, FontStyle.Regular, GraphicsUnit.Point);
      this.ForeColor = Color.White;
      this.AllowDrag = true;
      this.AllowDrop = true;
      this.TextImageRelation = TextImageRelation.ImageAboveText;
      this.BorderGradientStyle = GradientStyles.Solid;
      this.DesignTimeAllowDrag = this.DesignTimeAllowDrop = false;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      RadPanoramaElement tilePanelElement = this.FindTilePanelElement();
      if (tilePanelElement == null || e.Button != MouseButtons.Left || !tilePanelElement.AllowDragDrop)
        return;
      tilePanelElement.DragDropService.Start((object) this);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (e.Button == MouseButtons.Left)
      {
        if (Math.Abs(new Point(e.X - this.lastMouseMove.X, e.Y - this.lastMouseMove.Y).X) < SystemInformation.DragSize.Width)
          return;
        RadPanoramaElement tilePanelElement = this.FindTilePanelElement();
        if (tilePanelElement == null || tilePanelElement.AllowDragDrop)
          return;
        if (!this.Capture && e.Button == MouseButtons.Left)
        {
          this.Capture = true;
          tilePanelElement.ScrollService.MouseDown(e.Location);
        }
        else
        {
          if (!this.Capture)
            return;
          tilePanelElement.ScrollService.MouseMove(e.Location);
        }
      }
      else
        this.lastMouseMove = e.Location;
    }

    protected override void OnBubbleEvent(RadElement sender, RoutedEventArgs args)
    {
      MouseEventArgs originalEventArgs = args.OriginalEventArgs as MouseEventArgs;
      if (args.RoutedEvent == RadElement.MouseUpEvent && originalEventArgs != null)
      {
        RadPanoramaElement tilePanelElement = this.FindTilePanelElement();
        if (tilePanelElement != null && !tilePanelElement.AllowDragDrop && this.Capture)
        {
          tilePanelElement.ScrollService.MouseUp(originalEventArgs.Location);
          this.Capture = false;
          args.Canceled = true;
          return;
        }
      }
      base.OnBubbleEvent(sender, args);
    }

    protected override void OnPanGesture(PanGestureEventArgs args)
    {
      base.OnPanGesture(args);
      RadPanoramaElement tilePanelElement = this.FindTilePanelElement();
      if (tilePanelElement == null || !this.AllowDrag)
        return;
      RadDragDropService dragDropService = tilePanelElement.DragDropService;
      if (args.IsBegin)
      {
        this.currentPanOffset = SizeF.Empty;
        this.panBeginLocation = this.ElementTree.Control.PointToScreen(args.Location);
        this.ZIndex = 1000;
      }
      if (dragDropService.State == RadServiceState.Working)
      {
        dragDropService.DoMouseMove(this.ElementTree.Control.PointToScreen(args.Location));
      }
      else
      {
        tilePanelElement.ScrollView(args.Offset.Width, true);
        if (tilePanelElement.AllowDragDrop)
        {
          if (!args.IsInertia)
          {
            this.currentPanOffset.Width += (float) args.Offset.Width;
            this.currentPanOffset.Height += (float) args.Offset.Height;
          }
          if ((double) Math.Abs(this.currentPanOffset.Height) > (double) (this.Size.Height / this.RowSpan))
          {
            this.PositionOffset = SizeF.Empty;
            this.panBeginLocation.X += (int) this.currentPanOffset.Width;
            dragDropService.BeginDrag(this.panBeginLocation, (ISupportDrag) this);
            if (dragDropService.State == RadServiceState.Working)
              dragDropService.DoMouseMove(this.ElementTree.Control.PointToScreen(args.Location));
          }
          else
            this.PositionOffset = new SizeF(0.0f, 20f * this.currentPanOffset.Height / (float) (this.Size.Height / this.RowSpan));
        }
      }
      if (args.IsEnd)
      {
        if (dragDropService.State == RadServiceState.Working)
          dragDropService.EndDrag();
        this.PositionOffset = SizeF.Empty;
        int num = (int) this.ResetValue(RadElement.ZIndexProperty, ValueResetFlags.Local);
      }
      args.Handled = true;
    }

    protected RadPanoramaElement FindTilePanelElement()
    {
      for (RadElement parent = this.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is RadPanoramaElement)
          return parent as RadPanoramaElement;
      }
      return (RadPanoramaElement) null;
    }

    protected override bool ProcessDragOver(Point mousePosition, ISupportDrag dragObject)
    {
      return true;
    }

    protected override bool CanDragCore(Point dragStartPoint)
    {
      return this.ElementState == ElementState.Loaded;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.IsMouseDown = false;
    }

    protected override void OnAnimatedImageFrameChanged()
    {
      if (this.ElementTree != null)
        this.ElementTree.RootElement.Invalidate();
      base.OnAnimatedImageFrameChanged();
    }
  }
}
