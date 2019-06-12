// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ImageEditorCanvasElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Localization;
using Telerik.WinControls.Paint;
using Telerik.WinControls.UI.ImageEditor;
using Telerik.WinControls.UI.Localization;

namespace Telerik.WinControls.UI
{
  public class ImageEditorCanvasElement : LightVisualElement
  {
    private RectangleF relativeCropRect = new RectangleF(0.1f, 0.1f, 0.8f, 0.8f);
    private Size cropHandlesSize = new Size(12, 12);
    private RadImageEditorElement imageEditorElement;
    private RadButtonElement acceptButton;
    private RadButtonElement cancelButton;
    private ResizeType resizeType;
    private bool isCropping;
    private bool isPanning;
    private bool isDrawing;
    private List<Point> drawPath;
    private ShapeInfo shapeInfo;
    protected bool movingCropRect;
    protected bool panning;
    protected Point mouseLastLocation;
    protected Point mouseDownLocation;
    protected Point mouseCurrentLocation;
    protected bool resizingCropRect;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.acceptButton = this.CreateAcceptButton();
      this.cancelButton = this.CreateCancelButton();
      this.Children.Add((RadElement) this.acceptButton);
      this.Children.Add((RadElement) this.cancelButton);
      this.HideAcceptAndCancelButtons();
    }

    private RadButtonElement CreateAcceptButton()
    {
      RadButtonElement radButtonElement = new RadButtonElement();
      radButtonElement.Padding = new Padding(5);
      radButtonElement.Click += (EventHandler) ((param0, param1) =>
      {
        if (!this.IsCropping)
          return;
        this.imageEditorElement.StopCrop(true);
      });
      return radButtonElement;
    }

    private RadButtonElement CreateCancelButton()
    {
      RadButtonElement radButtonElement = new RadButtonElement();
      radButtonElement.Padding = new Padding(5);
      radButtonElement.Click += (EventHandler) ((param0, param1) =>
      {
        if (!this.IsCropping)
          return;
        this.imageEditorElement.StopCrop(false);
      });
      return radButtonElement;
    }

    public ImageEditorCanvasElement(RadImageEditorElement imageEditorElement)
    {
      this.imageEditorElement = imageEditorElement;
    }

    public bool IsCropping
    {
      get
      {
        return this.isCropping;
      }
      protected set
      {
        this.isCropping = value;
      }
    }

    public bool IsPanning
    {
      get
      {
        return this.isPanning;
      }
      protected set
      {
        this.isPanning = value;
      }
    }

    public bool IsDrawing
    {
      get
      {
        return this.isDrawing;
      }
      protected set
      {
        this.isDrawing = value;
      }
    }

    protected RectangleF RelativeCropRect
    {
      get
      {
        return this.relativeCropRect;
      }
      set
      {
        this.relativeCropRect = value;
      }
    }

    public Size CropHandlesSize
    {
      get
      {
        return this.cropHandlesSize;
      }
      set
      {
        this.cropHandlesSize = value;
      }
    }

    public RadButtonElement AcceptButton
    {
      get
      {
        return this.acceptButton;
      }
    }

    public RadButtonElement CancelButton
    {
      get
      {
        return this.cancelButton;
      }
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      if (this.IsCropping)
      {
        Rectangle cropRectangle = this.GetCropRectangle();
        SizeF scaleFactor = new SizeF(1f / this.DpiScaleFactor.Width, 1f / this.DpiScaleFactor.Height);
        this.AcceptButton.PositionOffset = TelerikDpiHelper.ScaleSizeF(new SizeF((float) cropRectangle.X + (float) cropRectangle.Width / 2f - (float) this.AcceptButton.Location.X - this.AcceptButton.DesiredSize.Width, (float) (cropRectangle.Y + cropRectangle.Height) + (float) this.CropHandlesSize.Height / 2f - (float) this.AcceptButton.Location.Y), scaleFactor);
        this.CancelButton.PositionOffset = TelerikDpiHelper.ScaleSizeF(new SizeF((float) cropRectangle.X + (float) cropRectangle.Width / 2f - (float) this.CancelButton.Location.X - this.CancelButton.DesiredSize.Width, (float) (cropRectangle.Y + cropRectangle.Height) + (float) this.CropHandlesSize.Height / 2f - (float) this.CancelButton.Location.Y), scaleFactor);
      }
      base.PaintElement(graphics, angle, scale);
      if (this.imageEditorElement.CurrentBitmap == null)
        return;
      Graphics underlayGraphics = (Graphics) graphics.UnderlayGraphics;
      Rectangle imageDrawRectangle = this.GetImageDrawRectangle();
      underlayGraphics.DrawImage((Image) this.imageEditorElement.CurrentBitmap, imageDrawRectangle);
      if (!this.IsCropping)
        return;
      RectangleF srcRect = new RectangleF((float) this.imageEditorElement.CurrentBitmap.Width * this.RelativeCropRect.X, (float) this.imageEditorElement.CurrentBitmap.Height * this.RelativeCropRect.Y, (float) this.imageEditorElement.CurrentBitmap.Width * this.RelativeCropRect.Width, (float) this.imageEditorElement.CurrentBitmap.Height * this.RelativeCropRect.Height);
      Rectangle cropRectangle1 = this.GetCropRectangle();
      SizeF sizeF1 = new SizeF((float) this.CropHandlesSize.Width / 2f, (float) this.CropHandlesSize.Height / 2f);
      graphics.FillRectangle(imageDrawRectangle, Color.FromArgb(100, Color.White));
      underlayGraphics.DrawImage((Image) this.imageEditorElement.CurrentBitmap, (RectangleF) cropRectangle1, srcRect, GraphicsUnit.Pixel);
      underlayGraphics.DrawRectangle(Pens.OrangeRed, cropRectangle1);
      underlayGraphics.FillRectangle(Brushes.OrangeRed, new RectangleF((float) cropRectangle1.X - sizeF1.Width, (float) cropRectangle1.Y - sizeF1.Height, (float) this.CropHandlesSize.Width, (float) this.CropHandlesSize.Height));
      underlayGraphics.FillRectangle(Brushes.OrangeRed, new RectangleF((float) cropRectangle1.Right - sizeF1.Width, (float) cropRectangle1.Y - sizeF1.Height, (float) this.CropHandlesSize.Width, (float) this.CropHandlesSize.Height));
      underlayGraphics.FillRectangle(Brushes.OrangeRed, new RectangleF((float) cropRectangle1.X - sizeF1.Width, (float) cropRectangle1.Bottom - sizeF1.Height, (float) this.CropHandlesSize.Width, (float) this.CropHandlesSize.Height));
      underlayGraphics.FillRectangle(Brushes.OrangeRed, new RectangleF((float) cropRectangle1.Right - sizeF1.Width, (float) cropRectangle1.Bottom - sizeF1.Height, (float) this.CropHandlesSize.Width, (float) this.CropHandlesSize.Height));
      string str1 = string.Format("X={0}, Y={1}", (object) (cropRectangle1.X - imageDrawRectangle.X), (object) (cropRectangle1.Y - imageDrawRectangle.Y));
      SizeF sizeF2 = underlayGraphics.MeasureString(str1, this.GetScaledFont(this.DpiScaleFactor.Height));
      underlayGraphics.DrawString(str1, this.GetScaledFont(this.DpiScaleFactor.Height), Brushes.OrangeRed, (float) cropRectangle1.X - sizeF1.Width, (float) cropRectangle1.Y - sizeF1.Height - sizeF2.Height);
      string str2 = string.Format("{0} x {1} px", (object) cropRectangle1.Width, (object) cropRectangle1.Height);
      SizeF sizeF3 = underlayGraphics.MeasureString(str2, this.GetScaledFont(this.DpiScaleFactor.Height));
      underlayGraphics.DrawString(str2, this.GetScaledFont(this.DpiScaleFactor.Height), Brushes.OrangeRed, (float) cropRectangle1.Right + sizeF1.Width - sizeF3.Width, (float) cropRectangle1.Bottom + sizeF1.Height);
    }

    protected virtual Rectangle GetImageDrawRectangle()
    {
      Size size = TelerikDpiHelper.ScaleSize(this.imageEditorElement.CurrentBitmap.Size, this.imageEditorElement.ZoomFactor);
      return new Rectangle(this.Size.Width > size.Width ? (this.Size.Width - size.Width) / 2 : 0, this.Size.Height > size.Height ? (this.Size.Height - size.Height) / 2 : 0, size.Width, size.Height);
    }

    protected virtual Rectangle GetCropRectangle()
    {
      Rectangle imageDrawRectangle = this.GetImageDrawRectangle();
      return new Rectangle((int) Math.Round((double) imageDrawRectangle.X + (double) imageDrawRectangle.Width * (double) this.RelativeCropRect.X, MidpointRounding.AwayFromZero), (int) Math.Round((double) imageDrawRectangle.Y + (double) imageDrawRectangle.Height * (double) this.RelativeCropRect.Y, MidpointRounding.AwayFromZero), (int) Math.Round((double) imageDrawRectangle.Width * (double) this.RelativeCropRect.Width, MidpointRounding.AwayFromZero), (int) Math.Round((double) imageDrawRectangle.Height * (double) this.RelativeCropRect.Height, MidpointRounding.AwayFromZero));
    }

    public virtual void StartCrop()
    {
      if (this.imageEditorElement.CurrentBitmap == null)
        return;
      this.StopAllOperations();
      this.IsCropping = true;
      this.RelativeCropRect = new RectangleF(0.1f, 0.1f, 0.8f, 0.8f);
      this.ShowAcceptAndCancelButtons();
    }

    public virtual Rectangle StopCrop()
    {
      this.HideAcceptAndCancelButtons();
      this.IsCropping = false;
      this.ResetMouseAndFlags();
      return new Rectangle((int) Math.Round((double) this.RelativeCropRect.X * (double) this.imageEditorElement.CurrentBitmap.Width, MidpointRounding.AwayFromZero), (int) Math.Round((double) this.RelativeCropRect.Y * (double) this.imageEditorElement.CurrentBitmap.Height, MidpointRounding.AwayFromZero), (int) Math.Round((double) this.RelativeCropRect.Width * (double) this.imageEditorElement.CurrentBitmap.Width, MidpointRounding.AwayFromZero), (int) Math.Round((double) this.RelativeCropRect.Height * (double) this.imageEditorElement.CurrentBitmap.Height, MidpointRounding.AwayFromZero));
    }

    public virtual void StartDrawing(ShapeInfo shapeInfo)
    {
      if (this.imageEditorElement.CurrentBitmap == null)
        return;
      this.StopAllOperations();
      this.IsDrawing = true;
      this.shapeInfo = shapeInfo;
      if (this.shapeInfo.ShapeType != ShapeType.Freeflow)
        return;
      this.drawPath = new List<Point>();
    }

    public virtual void StopDrawing()
    {
      this.IsDrawing = false;
    }

    public virtual void StartPan()
    {
      if (this.imageEditorElement.CurrentBitmap == null)
        return;
      this.StopAllOperations();
      this.IsPanning = true;
      this.ElementTree.Control.Cursor = Cursors.Hand;
    }

    public virtual void StopPan()
    {
      this.IsPanning = false;
      this.ElementTree.Control.Cursor = Cursors.Default;
    }

    public virtual void StopAllOperations()
    {
      if (this.IsPanning)
        this.StopPan();
      if (this.IsCropping)
        this.StopCrop();
      if (!this.IsDrawing)
        return;
      this.StopDrawing();
    }

    public virtual void ShowAcceptAndCancelButtons()
    {
      this.acceptButton.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CropAccept");
      this.cancelButton.Text = LocalizationProvider<ImageEditorLocalizationProvider>.CurrentProvider.GetLocalizedString("CropCancel");
      this.acceptButton.Visibility = ElementVisibility.Visible;
      this.cancelButton.Visibility = ElementVisibility.Visible;
    }

    public virtual void HideAcceptAndCancelButtons()
    {
      this.acceptButton.Visibility = ElementVisibility.Collapsed;
      this.cancelButton.Visibility = ElementVisibility.Collapsed;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.Capture = true;
      this.mouseLastLocation = e.Location;
      this.mouseDownLocation = this.PointFromControl(e.Location);
      this.mouseCurrentLocation = this.mouseDownLocation;
      if (this.IsCropping)
      {
        Rectangle cropRectangle = this.GetCropRectangle();
        cropRectangle.Inflate(-this.CropHandlesSize.Width, -this.CropHandlesSize.Height);
        if (cropRectangle.Contains(this.mouseDownLocation))
        {
          this.movingCropRect = true;
          this.HideAcceptAndCancelButtons();
        }
        this.resizeType = this.GetResizeType(this.PointFromControl(e.Location));
        if (this.resizeType == ResizeType.None)
          return;
        this.resizingCropRect = true;
        this.HideAcceptAndCancelButtons();
      }
      else if (this.IsDrawing)
      {
        if (this.shapeInfo.ShapeType == ShapeType.Freeflow)
        {
          RectangleF imageDrawRectangle = (RectangleF) this.GetImageDrawRectangle();
          this.drawPath = new List<Point>();
          this.drawPath.Add(new Point((int) Math.Round((double) (((float) this.mouseCurrentLocation.X - imageDrawRectangle.X) / imageDrawRectangle.Width * (float) this.imageEditorElement.CurrentBitmap.Width), MidpointRounding.AwayFromZero), (int) Math.Round((double) (((float) this.mouseCurrentLocation.Y - imageDrawRectangle.Y) / imageDrawRectangle.Height * (float) this.imageEditorElement.CurrentBitmap.Height), MidpointRounding.AwayFromZero)));
        }
        this.imageEditorElement.DrawShape(this.GetShapePath(), this.shapeInfo.Fill, this.shapeInfo.BorderColor, this.shapeInfo.BorderThickness);
      }
      else
      {
        if (!this.IsPanning)
          return;
        this.panning = true;
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.movingCropRect)
        this.MoveCropRectangle(e.Location);
      else if (this.resizingCropRect)
        this.ResizeCropRect(e.Location);
      else if (this.IsCropping)
        this.ChangeResizeCursor(e.Location);
      else if (this.IsDrawing && (e.Button & MouseButtons.Left) == MouseButtons.Left)
      {
        this.mouseCurrentLocation = this.PointFromControl(e.Location);
        if (this.shapeInfo.ShapeType == ShapeType.Freeflow)
        {
          RectangleF imageDrawRectangle = (RectangleF) this.GetImageDrawRectangle();
          this.drawPath.Add(new Point((int) Math.Round((double) (((float) this.mouseCurrentLocation.X - imageDrawRectangle.X) / imageDrawRectangle.Width * (float) this.imageEditorElement.CurrentBitmap.Width), MidpointRounding.AwayFromZero), (int) Math.Round((double) (((float) this.mouseCurrentLocation.Y - imageDrawRectangle.Y) / imageDrawRectangle.Height * (float) this.imageEditorElement.CurrentBitmap.Height), MidpointRounding.AwayFromZero)));
        }
        this.imageEditorElement.DrawShape(this.GetShapePath(), this.shapeInfo.Fill, this.shapeInfo.BorderColor, this.shapeInfo.BorderThickness);
      }
      else
      {
        if (!this.IsPanning || !this.panning || (e.Button & MouseButtons.Left) != MouseButtons.Left)
          return;
        int xoffs = this.mouseLastLocation.X - e.Location.X;
        int yoffs = this.mouseLastLocation.Y - e.Location.Y;
        this.mouseLastLocation = e.Location;
        this.imageEditorElement.ScrollViewer.ScrollLayoutPanel.ScrollWith(xoffs, yoffs);
      }
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if ((Control.ModifierKeys & Keys.Control) != Keys.Control || e.Delta == 0)
        return;
      float num = this.imageEditorElement.ZoomElement.TrackBar.Value + (float) (e.Delta / 120);
      if ((double) num < (double) this.imageEditorElement.ZoomElement.TrackBar.Minimum)
        num = this.imageEditorElement.ZoomElement.TrackBar.Minimum;
      if ((double) num > (double) this.imageEditorElement.ZoomElement.TrackBar.Maximum)
        num = this.imageEditorElement.ZoomElement.TrackBar.Maximum;
      this.imageEditorElement.ZoomElement.TrackBar.Value = num;
    }

    protected virtual void ChangeResizeCursor(Point location)
    {
      switch (this.GetResizeType(this.PointFromControl(location)))
      {
        case ResizeType.TopLeft:
        case ResizeType.BottomRight:
          Cursor.Current = Cursors.SizeNWSE;
          break;
        case ResizeType.Left:
        case ResizeType.Right:
          Cursor.Current = Cursors.SizeWE;
          break;
        case ResizeType.BottomLeft:
        case ResizeType.TopRight:
          Cursor.Current = Cursors.SizeNESW;
          break;
        case ResizeType.Bottom:
        case ResizeType.Top:
          Cursor.Current = Cursors.SizeNS;
          break;
        default:
          Cursor.Current = Cursors.Default;
          break;
      }
    }

    protected virtual void MoveCropRectangle(Point location)
    {
      Rectangle imageDrawRectangle = this.GetImageDrawRectangle();
      Rectangle cropRectangle = this.GetCropRectangle();
      PointF pointF = new PointF((float) (cropRectangle.X + location.X - this.mouseLastLocation.X), (float) (cropRectangle.Y + location.Y - this.mouseLastLocation.Y));
      if ((double) pointF.X < (double) imageDrawRectangle.X)
        pointF.X = (float) imageDrawRectangle.X;
      if ((double) pointF.Y < (double) imageDrawRectangle.Y)
        pointF.Y = (float) imageDrawRectangle.Y;
      if ((double) pointF.X + (double) cropRectangle.Width > (double) imageDrawRectangle.Right)
        pointF.X = (float) (imageDrawRectangle.Right - cropRectangle.Width);
      if ((double) pointF.Y + (double) cropRectangle.Height > (double) imageDrawRectangle.Bottom)
        pointF.Y = (float) (imageDrawRectangle.Bottom - cropRectangle.Height);
      if (!((PointF) cropRectangle.Location != pointF))
        return;
      if ((double) pointF.X == (double) imageDrawRectangle.X)
        this.relativeCropRect.X = 0.0f;
      if ((double) pointF.Y == (double) imageDrawRectangle.Y)
        this.relativeCropRect.Y = 0.0f;
      this.relativeCropRect.X = (pointF.X - (float) imageDrawRectangle.X) / (float) imageDrawRectangle.Width;
      this.relativeCropRect.Y = (pointF.Y - (float) imageDrawRectangle.Y) / (float) imageDrawRectangle.Height;
      this.mouseLastLocation = location;
      this.Invalidate();
    }

    protected virtual void ResizeCropRect(Point location)
    {
      if (this.resizeType == ResizeType.None)
        return;
      RectangleF imageDrawRectangle = (RectangleF) this.GetImageDrawRectangle();
      RectangleF rectangleF = (RectangleF) this.GetCropRectangle();
      PointF pointF1 = (PointF) this.PointFromControl(location);
      PointF pointF2 = new PointF((float) (location.X - this.mouseLastLocation.X), (float) (location.Y - this.mouseLastLocation.Y));
      this.mouseLastLocation = location;
      switch (this.resizeType)
      {
        case ResizeType.TopLeft:
          if ((double) rectangleF.X + (double) pointF2.X < (double) imageDrawRectangle.X)
            pointF2.X = imageDrawRectangle.X - rectangleF.X;
          if ((double) rectangleF.Y + (double) pointF2.Y < (double) imageDrawRectangle.Y)
            pointF2.Y = imageDrawRectangle.Y - rectangleF.Y;
          if ((double) rectangleF.X + (double) pointF2.X > (double) rectangleF.Right - 1.0)
            pointF2.X = rectangleF.Right - 1f - rectangleF.X;
          if ((double) rectangleF.Y + (double) pointF2.Y > (double) rectangleF.Bottom - 1.0)
            pointF2.Y = rectangleF.Bottom - 1f - rectangleF.Y;
          rectangleF = new RectangleF(rectangleF.X + pointF2.X, rectangleF.Y + pointF2.Y, rectangleF.Width - pointF2.X, rectangleF.Height - pointF2.Y);
          break;
        case ResizeType.Left:
          if ((double) rectangleF.X + (double) pointF2.X < (double) imageDrawRectangle.X)
            pointF2.X = imageDrawRectangle.X - rectangleF.X;
          if ((double) rectangleF.X + (double) pointF2.X > (double) rectangleF.Right - 1.0)
            pointF2.X = rectangleF.Right - 1f - rectangleF.X;
          rectangleF = new RectangleF(rectangleF.X + pointF2.X, rectangleF.Y, rectangleF.Width - pointF2.X, rectangleF.Height);
          break;
        case ResizeType.BottomLeft:
          if ((double) rectangleF.X + (double) pointF2.X < (double) imageDrawRectangle.X)
            pointF2.X = imageDrawRectangle.X - rectangleF.X;
          if ((double) rectangleF.Bottom + (double) pointF2.Y > (double) imageDrawRectangle.Bottom)
            pointF2.Y = imageDrawRectangle.Bottom - rectangleF.Bottom;
          if ((double) rectangleF.X + (double) pointF2.X > (double) rectangleF.Right - 1.0)
            pointF2.X = rectangleF.Right - 1f - rectangleF.X;
          rectangleF = new RectangleF(rectangleF.X + pointF2.X, rectangleF.Y, rectangleF.Width - pointF2.X, rectangleF.Height + pointF2.Y);
          break;
        case ResizeType.Bottom:
          if ((double) rectangleF.Bottom + (double) pointF2.Y > (double) imageDrawRectangle.Bottom)
            pointF2.Y = imageDrawRectangle.Bottom - rectangleF.Bottom;
          rectangleF = new RectangleF(rectangleF.X, rectangleF.Y, rectangleF.Width, rectangleF.Height + pointF2.Y);
          break;
        case ResizeType.BottomRight:
          if ((double) rectangleF.Right + (double) pointF2.X > (double) imageDrawRectangle.Right)
            pointF2.X = imageDrawRectangle.Right - rectangleF.Right;
          if ((double) rectangleF.Bottom + (double) pointF2.Y > (double) imageDrawRectangle.Bottom)
            pointF2.Y = imageDrawRectangle.Bottom - rectangleF.Bottom;
          rectangleF = new RectangleF(rectangleF.X, rectangleF.Y, rectangleF.Width + pointF2.X, rectangleF.Height + pointF2.Y);
          break;
        case ResizeType.Right:
          if ((double) rectangleF.Right + (double) pointF2.X > (double) imageDrawRectangle.Right)
            pointF2.X = imageDrawRectangle.Right - rectangleF.Right;
          rectangleF = new RectangleF(rectangleF.X, rectangleF.Y, rectangleF.Width + pointF2.X, rectangleF.Height);
          break;
        case ResizeType.TopRight:
          if ((double) rectangleF.Right + (double) pointF2.X > (double) imageDrawRectangle.Right)
            pointF2.X = imageDrawRectangle.Right - rectangleF.Right;
          if ((double) rectangleF.Y + (double) pointF2.Y < (double) imageDrawRectangle.Y)
            pointF2.Y = imageDrawRectangle.Y - rectangleF.Y;
          if ((double) rectangleF.Y + (double) pointF2.Y > (double) rectangleF.Bottom - 1.0)
            pointF2.Y = rectangleF.Bottom - 1f - rectangleF.Y;
          rectangleF = new RectangleF(rectangleF.X, rectangleF.Y + pointF2.Y, rectangleF.Width + pointF2.X, rectangleF.Height - pointF2.Y);
          break;
        case ResizeType.Top:
          if ((double) rectangleF.Y + (double) pointF2.Y < (double) imageDrawRectangle.Y)
            pointF2.Y = imageDrawRectangle.Y - rectangleF.Y;
          if ((double) rectangleF.Y + (double) pointF2.Y > (double) rectangleF.Bottom - 1.0)
            pointF2.Y = rectangleF.Bottom - 1f - rectangleF.Y;
          rectangleF = new RectangleF(rectangleF.X, rectangleF.Y + pointF2.Y, rectangleF.Width, rectangleF.Height - pointF2.Y);
          break;
      }
      if ((double) rectangleF.Width < 1.0)
        rectangleF.Width = 1f;
      if ((double) rectangleF.Height < 1.0)
        rectangleF.Height = 1f;
      this.relativeCropRect.X = (rectangleF.X - imageDrawRectangle.X) / imageDrawRectangle.Width;
      this.relativeCropRect.Y = (rectangleF.Y - imageDrawRectangle.Y) / imageDrawRectangle.Height;
      this.relativeCropRect.Width = rectangleF.Width / imageDrawRectangle.Width;
      this.relativeCropRect.Height = rectangleF.Height / imageDrawRectangle.Height;
      this.Invalidate();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.Capture = false;
      if (this.IsCropping)
      {
        if (this.movingCropRect || this.resizingCropRect)
          this.ShowAcceptAndCancelButtons();
        this.ResetMouseAndFlags();
      }
      else if (this.IsDrawing)
      {
        GraphicsPath shapePath = this.GetShapePath();
        if (this.shapeInfo.ShapeType == ShapeType.Freeflow)
        {
          RectangleF imageDrawRectangle = (RectangleF) this.GetImageDrawRectangle();
          this.drawPath.Add(new Point((int) Math.Round((double) (((float) this.mouseCurrentLocation.X - imageDrawRectangle.X) / imageDrawRectangle.Width * (float) this.imageEditorElement.CurrentBitmap.Width), MidpointRounding.AwayFromZero), (int) Math.Round((double) (((float) this.mouseCurrentLocation.Y - imageDrawRectangle.Y) / imageDrawRectangle.Height * (float) this.imageEditorElement.CurrentBitmap.Height), MidpointRounding.AwayFromZero)));
        }
        this.imageEditorElement.DrawShape(shapePath, this.shapeInfo.Fill, this.shapeInfo.BorderColor, this.shapeInfo.BorderThickness);
        this.imageEditorElement.SaveState();
      }
      else
      {
        if (!this.IsPanning)
          return;
        this.panning = false;
      }
    }

    protected virtual void ResetMouseAndFlags()
    {
      this.resizeType = ResizeType.None;
      this.resizingCropRect = false;
      this.movingCropRect = false;
    }

    protected virtual ResizeType GetResizeType(Point location)
    {
      Rectangle cropRectangle = this.GetCropRectangle();
      Rectangle rectangle = cropRectangle;
      cropRectangle.Inflate(this.CropHandlesSize.Width / 2, this.CropHandlesSize.Height / 2);
      rectangle.Inflate(-this.CropHandlesSize.Width / 2, -this.CropHandlesSize.Height / 2);
      if (!cropRectangle.Contains(location) || rectangle.Contains(location))
        return ResizeType.None;
      if (new Rectangle(rectangle.X, cropRectangle.Y, rectangle.Width, this.CropHandlesSize.Height).Contains(location))
        return ResizeType.Top;
      if (new Rectangle(rectangle.X, rectangle.Bottom, rectangle.Width, this.CropHandlesSize.Height).Contains(location))
        return ResizeType.Bottom;
      if (new Rectangle(cropRectangle.X, rectangle.Y, this.CropHandlesSize.Width, rectangle.Height).Contains(location))
        return ResizeType.Left;
      if (new Rectangle(rectangle.Right, rectangle.Y, this.CropHandlesSize.Width, rectangle.Height).Contains(location))
        return ResizeType.Right;
      Point point = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
      if (location.X < point.X && location.Y > point.Y)
        return ResizeType.BottomLeft;
      if (location.X > point.X && location.Y < point.Y)
        return ResizeType.TopRight;
      if (location.X < point.X && location.Y < point.Y)
        return ResizeType.TopLeft;
      return location.X > point.X && location.Y > point.Y ? ResizeType.BottomRight : ResizeType.None;
    }

    protected virtual GraphicsPath GetShapePath()
    {
      RectangleF imageDrawRectangle = (RectangleF) this.GetImageDrawRectangle();
      float num1 = (float) this.imageEditorElement.CurrentBitmap.Width * (((float) this.mouseDownLocation.X - imageDrawRectangle.X) / imageDrawRectangle.Width);
      float num2 = (float) this.imageEditorElement.CurrentBitmap.Height * (((float) this.mouseDownLocation.Y - imageDrawRectangle.Y) / imageDrawRectangle.Height);
      float num3 = (float) this.imageEditorElement.CurrentBitmap.Width * (((float) this.mouseCurrentLocation.X - imageDrawRectangle.X) / imageDrawRectangle.Width);
      float num4 = (float) this.imageEditorElement.CurrentBitmap.Height * (((float) this.mouseCurrentLocation.Y - imageDrawRectangle.Y) / imageDrawRectangle.Height);
      float num5 = Math.Abs(num1 - num3);
      float num6 = Math.Abs(num2 - num4);
      GraphicsPath graphicsPath = new GraphicsPath();
      switch (this.shapeInfo.ShapeType)
      {
        case ShapeType.Freeflow:
          graphicsPath.AddLines(this.drawPath.ToArray());
          break;
        case ShapeType.Rectangle:
          float x1 = Math.Min(num1, num3);
          float y1 = Math.Min(num2, num4);
          graphicsPath.AddRectangle(new RectangleF(x1, y1, num5, num6));
          break;
        case ShapeType.Square:
          float num7 = Math.Min(num5, num6);
          float x2;
          float y2;
          if ((double) num3 > (double) num1 && (double) num4 > (double) num2)
          {
            x2 = num1;
            y2 = num2;
          }
          else if ((double) num3 < (double) num1 && (double) num4 < (double) num2)
          {
            x2 = num1 - num7;
            y2 = num2 - num7;
          }
          else if ((double) num3 > (double) num1 && (double) num4 < (double) num2)
          {
            x2 = num1;
            y2 = num2 - num7;
          }
          else
          {
            x2 = num1 - num7;
            y2 = num2;
          }
          graphicsPath.AddRectangle(new RectangleF(x2, y2, num7, num7));
          break;
        case ShapeType.Ellipse:
          float x3 = Math.Min(num1, num3);
          float y3 = Math.Min(num2, num4);
          graphicsPath.AddEllipse(new RectangleF(x3, y3, num5, num6));
          break;
        case ShapeType.Circle:
          float num8 = Math.Min(num5, num6);
          float x4;
          float y4;
          if ((double) num3 > (double) num1 && (double) num4 > (double) num2)
          {
            x4 = num1;
            y4 = num2;
          }
          else if ((double) num3 < (double) num1 && (double) num4 < (double) num2)
          {
            x4 = num1 - num8;
            y4 = num2 - num8;
          }
          else if ((double) num3 > (double) num1 && (double) num4 < (double) num2)
          {
            x4 = num1;
            y4 = num2 - num8;
          }
          else
          {
            x4 = num1 - num8;
            y4 = num2;
          }
          graphicsPath.AddEllipse(new RectangleF(x4, y4, num8, num8));
          break;
        case ShapeType.Line:
          graphicsPath.AddLine(num1, num2, num3, num4);
          break;
      }
      return graphicsPath;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.imageEditorElement != null && this.imageEditorElement.CurrentBitmap != null)
        return (SizeF) TelerikDpiHelper.ScaleSize(this.imageEditorElement.CurrentBitmap.Size, this.imageEditorElement.ZoomFactor);
      return base.MeasureOverride(availableSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      this.acceptButton.Arrange(new RectangleF(0.0f, 0.0f, this.acceptButton.DesiredSize.Width, this.acceptButton.DesiredSize.Height));
      this.cancelButton.Arrange(new RectangleF(this.acceptButton.DesiredSize.Width, 0.0f, this.cancelButton.DesiredSize.Width, this.cancelButton.DesiredSize.Height));
      return sizeF;
    }
  }
}
