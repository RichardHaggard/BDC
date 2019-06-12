// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadCommandBarGrip
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Paint;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  public class RadCommandBarGrip : RadCommandBarVisualElement
  {
    private float dotSize = 2f;
    private float dotSpacing = 2f;
    private float shadowOffset = 0.1f;
    private int cachedNumberOfDots = 4;
    private PointF delta = PointF.Empty;
    private PointF oldMouseLocation = PointF.Empty;
    public static RoutedEvent BeginDraggingEvent = RadElement.RegisterRoutedEvent(nameof (BeginDraggingEvent), typeof (RadCommandBarGrip));
    public static RoutedEvent EndDraggingEvent = RadElement.RegisterRoutedEvent(nameof (EndDraggingEvent), typeof (RadCommandBarGrip));
    public static RoutedEvent DraggingEvent = RadElement.RegisterRoutedEvent(nameof (DraggingEvent), typeof (RadCommandBarGrip));
    public static RadProperty NumberOfDotsProperty = RadProperty.Register(nameof (NumberOfDots), typeof (int), typeof (RadCommandBarGrip), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 4, ElementPropertyOptions.AffectsDisplay));
    private const int defaultNumberOfDots = 4;
    private CommandBarStripElement owner;
    private float currentX;
    private float currentY;
    private bool isDragging;
    private Point oldMousePosition;

    static RadCommandBarGrip()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new ItemStateManagerFactory(), typeof (RadCommandBarGrip));
    }

    public RadCommandBarGrip(CommandBarStripElement owner)
    {
      this.owner = owner;
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.MinSize = new Size(5, 25);
      this.DrawBorder = false;
      this.DrawFill = false;
      this.NumberOfDots = 0;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.PaintDots(graphics, angle, scale);
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      this.OnMouseHover(e);
      Cursor.Current = Cursors.SizeAll;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      Cursor.Current = Cursors.SizeAll;
      if (!this.isDragging && e.Button == MouseButtons.Left && this.IsMouseDown && (Math.Abs(e.X - this.oldMousePosition.X) >= 1 || Math.Abs(e.Y - this.oldMousePosition.Y) >= 1))
        this.BeginDrag(e);
      if (!this.isDragging)
        return;
      this.Capture = true;
      this.ElementTree.Control.Capture = true;
      PointF pointF = this.Orientation == Orientation.Horizontal ? new PointF((float) e.X - this.currentX, (float) e.Y) : new PointF((float) e.X, (float) e.Y - this.currentY);
      pointF.X /= this.DpiScaleFactor.Width;
      pointF.Y /= this.DpiScaleFactor.Height;
      CommandBarRowElement parent = this.owner.Parent as CommandBarRowElement;
      if (parent != null && parent.RightToLeft && parent.Orientation == Orientation.Horizontal)
        pointF.X = ((float) parent.Size.Width - pointF.X) / this.DpiScaleFactor.Width;
      this.owner.DesiredLocation = pointF;
      this.delta = new PointF((pointF.X - this.oldMouseLocation.X) / this.DpiScaleFactor.Width, (pointF.Y - this.oldMouseLocation.Y) / this.DpiScaleFactor.Height);
      this.oldMouseLocation = pointF;
      this.OnDragging(e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.oldMousePosition = e.Location;
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (!this.isDragging)
        return;
      this.EndDrag();
    }

    public PointF Delta
    {
      get
      {
        return this.delta;
      }
    }

    public bool IsDrag
    {
      get
      {
        return this.isDragging;
      }
    }

    public override Orientation Orientation
    {
      get
      {
        return base.Orientation;
      }
      set
      {
        this.cachedOrientation = value;
        this.AngleTransform = value == Orientation.Horizontal ? 0.0f : 90f;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public CommandBarStripElement Owner
    {
      get
      {
        return this.owner;
      }
    }

    [DefaultValue(2f)]
    public float DotSize
    {
      get
      {
        return this.dotSize;
      }
      set
      {
        this.dotSize = value;
      }
    }

    [DefaultValue(2f)]
    public float DotSpacing
    {
      get
      {
        return this.dotSpacing;
      }
      set
      {
        this.dotSpacing = value;
      }
    }

    [DefaultValue(0.1f)]
    public float ShadowOffset
    {
      get
      {
        return this.shadowOffset;
      }
      set
      {
        this.shadowOffset = value;
      }
    }

    public virtual int NumberOfDots
    {
      get
      {
        return this.cachedNumberOfDots;
      }
      set
      {
        this.cachedNumberOfDots = value;
        int num = (int) this.SetValue(RadCommandBarGrip.NumberOfDotsProperty, (object) value);
      }
    }

    protected virtual bool OnBeginDragging(CancelEventArgs args)
    {
      if (!args.Cancel)
        this.RaiseBubbleEvent((RadElement) this.owner, new RoutedEventArgs((EventArgs) args, RadCommandBarGrip.BeginDraggingEvent));
      return args.Cancel;
    }

    protected virtual void OnEndDragging(EventArgs args)
    {
      this.RaiseBubbleEvent((RadElement) this, new RoutedEventArgs(args, RadCommandBarGrip.EndDraggingEvent));
    }

    protected virtual void OnDragging(MouseEventArgs args)
    {
      this.RaiseBubbleEvent((RadElement) this, new RoutedEventArgs((EventArgs) args, RadCommandBarGrip.DraggingEvent));
    }

    protected virtual void PaintDots(IGraphics g, float angle, SizeF scale)
    {
      if (this.NumberOfDots == 0)
        return;
      RectangleF BorderRectangle1 = new RectangleF((float) Math.Floor(((double) this.DesiredSize.Width - (double) this.DotSize) / 2.0), (float) Math.Floor(((double) this.DesiredSize.Height - ((double) this.NumberOfDots * (double) this.DotSize + (double) (this.NumberOfDots - 1) * (double) this.DotSpacing)) / 2.0), this.DotSize, this.DotSize);
      for (int index = 0; index < this.NumberOfDots; ++index)
      {
        RectangleF BorderRectangle2 = new RectangleF(BorderRectangle1.X + this.ShadowOffset, BorderRectangle1.Y + this.ShadowOffset, this.DotSize, this.DotSize);
        g.FillRectangle(BorderRectangle2, this.BackColor2);
        g.FillRectangle(BorderRectangle1, this.BackColor);
        BorderRectangle1.Y += this.DotSpacing + this.DotSize;
      }
    }

    protected internal void BeginDrag(MouseEventArgs e)
    {
      if (!this.Owner.EnableDragging || this.OnBeginDragging(new CancelEventArgs()))
        return;
      this.isDragging = true;
      this.Capture = true;
      this.isDragging = true;
      if (this.RightToLeft)
      {
        this.currentX = (float) (e.X - (this.owner.ControlBoundingRectangle.Location.X + this.owner.ControlBoundingRectangle.Width));
        this.currentY = (float) (e.Y - this.owner.ControlBoundingRectangle.Location.Y);
      }
      else
      {
        this.currentX = (float) (e.X - this.owner.ControlBoundingRectangle.Location.X);
        this.currentY = (float) (e.Y - this.owner.ControlBoundingRectangle.Location.Y);
      }
    }

    protected internal void EndDrag()
    {
      this.Capture = false;
      this.isDragging = false;
      this.OnEndDragging(new EventArgs());
    }
  }
}
