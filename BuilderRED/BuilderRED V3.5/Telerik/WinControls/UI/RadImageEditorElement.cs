// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadImageEditorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls.UI.ImageEditor;
using Telerik.WinControls.UI.ImageEditor.Dialogs;

namespace Telerik.WinControls.UI
{
  public class RadImageEditorElement : LightVisualElement
  {
    public static RadProperty CommandsElementWidthProperty = RadProperty.Register(nameof (CommandsElementWidth), typeof (int), typeof (RadImageEditorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 150, ElementPropertyOptions.AffectsLayout));
    public static RadProperty ZoomElementHeightProperty = RadProperty.Register(nameof (ZoomElementHeight), typeof (int), typeof (RadImageEditorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 50, ElementPropertyOptions.AffectsLayout));
    private int readBlockSize = 4096;
    private PixelFormat pixelFormat = PixelFormat.Format32bppArgb;
    private int bitmapStep = 4;
    private SizeF zoomFactor = new SizeF(1f, 1f);
    private double blurFactor = 1.0 / 16.0;
    private double[,] blurFilterMatrix = new double[3, 3]{ { 1.0, 2.0, 1.0 }, { 2.0, 4.0, 2.0 }, { 1.0, 2.0, 1.0 } };
    private double[,] sharpenFilterMatrix = new double[5, 5]{ { -1.0, -1.0, -1.0, -1.0, -1.0 }, { -1.0, 2.0, 2.0, 2.0, -1.0 }, { -1.0, 2.0, 16.0, 2.0, -1.0 }, { -1.0, 2.0, 2.0, 2.0, -1.0 }, { -1.0, -1.0, -1.0, -1.0, -1.0 } };
    private RadScrollViewer scrollViewer;
    private RadCanvasViewport canvasViewport;
    private ImageEditorCanvasElement canvasElement;
    private ImageEditorZoomElement zoomElement;
    private ImageEditorCommandsElement commandsElement;
    private ImageEditorDialogFactory dialogFactory;
    private ImageEditorBaseDialog openDialog;
    private int contentLength;
    private int totalBytesRead;
    private byte[] readBuffer;
    private bool cancellationPending;
    private bool asyncOperationInProgress;
    private MemoryStream tempDownloadStream;
    private AsyncOperation currentAsyncLoadOperation;
    private SendOrPostCallback loadCompletedDelegate;
    private SendOrPostCallback loadProgressDelegate;
    private int bitmapStride;
    private Stack<Bitmap> undoStack;
    private Stack<Bitmap> redoStack;
    private Bitmap currentBitmap;
    private Bitmap originalBitmap;
    private string bitmapPath;

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.commandsElement = this.CreateCommandsElement();
      this.scrollViewer = this.CreateScrollViewer();
      this.canvasViewport = this.CreateCanvasViewport();
      this.canvasElement = this.CreateCanvasElement();
      this.zoomElement = this.CreateZoomElement();
      this.scrollViewer.Viewport = (RadElement) this.canvasViewport;
      this.canvasViewport.Children.Add((RadElement) this.canvasElement);
      this.Children.Add((RadElement) this.commandsElement);
      this.Children.Add((RadElement) this.scrollViewer);
      this.Children.Add((RadElement) this.zoomElement);
    }

    protected virtual RadScrollViewer CreateScrollViewer()
    {
      return new RadScrollViewer() { UsePhysicalScrolling = true, ShowBorder = false, ShowFill = false };
    }

    protected virtual RadCanvasViewport CreateCanvasViewport()
    {
      return new RadCanvasViewport();
    }

    protected virtual ImageEditorCanvasElement CreateCanvasElement()
    {
      return new ImageEditorCanvasElement(this);
    }

    protected virtual ImageEditorCommandsElement CreateCommandsElement()
    {
      ImageEditorCommandsElement editorCommandsElement = new ImageEditorCommandsElement(this);
      editorCommandsElement.Margin = new Padding(10, 10, 0, 2);
      return editorCommandsElement;
    }

    protected virtual ImageEditorZoomElement CreateZoomElement()
    {
      ImageEditorZoomElement editorZoomElement = new ImageEditorZoomElement(this);
      editorZoomElement.Margin = new Padding(0, 0, 14, 0);
      return editorZoomElement;
    }

    public RadImageEditorElement()
    {
      this.undoStack = new Stack<Bitmap>();
      this.redoStack = new Stack<Bitmap>();
      this.dialogFactory = new ImageEditorDialogFactory();
      this.UpdateUndoRedoButtons();
    }

    public ImageEditorCommandsElement CommandsElement
    {
      get
      {
        return this.commandsElement;
      }
    }

    public ImageEditorCanvasElement CanvasElement
    {
      get
      {
        return this.canvasElement;
      }
    }

    public ImageEditorZoomElement ZoomElement
    {
      get
      {
        return this.zoomElement;
      }
    }

    public RadScrollViewer ScrollViewer
    {
      get
      {
        return this.scrollViewer;
      }
    }

    public Bitmap CurrentBitmap
    {
      get
      {
        return this.currentBitmap;
      }
      set
      {
        this.currentBitmap = value;
      }
    }

    protected virtual Bitmap OriginalBitmap
    {
      get
      {
        return this.originalBitmap;
      }
      set
      {
        this.originalBitmap = value;
      }
    }

    public string BitmapPath
    {
      get
      {
        return this.bitmapPath;
      }
      set
      {
        this.bitmapPath = value;
      }
    }

    public SizeF ZoomFactor
    {
      get
      {
        return this.zoomFactor;
      }
      set
      {
        if (this.zoomFactor == value)
          return;
        this.zoomFactor = value;
        this.InvalidateMeasure(true);
        this.UpdateLayout();
        this.Invalidate();
      }
    }

    public ImageEditorDialogFactory DialogFactory
    {
      get
      {
        return this.dialogFactory;
      }
      set
      {
        this.dialogFactory = value;
      }
    }

    public int CommandsElementWidth
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadImageEditorElement.CommandsElementWidthProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadImageEditorElement.CommandsElementWidthProperty, (object) value);
      }
    }

    public int ZoomElementHeight
    {
      get
      {
        return TelerikDpiHelper.ScaleInt((int) this.GetValue(RadImageEditorElement.ZoomElementHeightProperty), this.DpiScaleFactor);
      }
      set
      {
        int num = (int) this.SetValue(RadImageEditorElement.ZoomElementHeightProperty, (object) value);
      }
    }

    protected Stack<Bitmap> UndoStack
    {
      get
      {
        return this.undoStack;
      }
    }

    protected Stack<Bitmap> RedoStack
    {
      get
      {
        return this.redoStack;
      }
    }

    public virtual void LoadAsync()
    {
      if (TelerikHelper.StringIsNullOrWhiteSpace(this.BitmapPath))
        throw new InvalidOperationException("The BitmapPath property is not set to a valid value.");
      if (this.asyncOperationInProgress)
        return;
      this.asyncOperationInProgress = true;
      this.currentAsyncLoadOperation = AsyncOperationManager.CreateOperation((object) null);
      if (this.loadCompletedDelegate == null)
      {
        this.loadCompletedDelegate = new SendOrPostCallback(this.LoadCompletedDelegate);
        this.loadProgressDelegate = new SendOrPostCallback(this.LoadProgressDelegate);
        this.readBuffer = new byte[this.readBlockSize];
      }
      this.cancellationPending = false;
      this.contentLength = -1;
      this.tempDownloadStream = new MemoryStream();
      Uri requestUri;
      try
      {
        requestUri = new Uri(this.BitmapPath);
      }
      catch (UriFormatException ex)
      {
        requestUri = new Uri(Path.GetFullPath(this.BitmapPath));
      }
      new WaitCallback(this.BeginGetResponseDelegate).BeginInvoke((object) WebRequest.Create(requestUri), (AsyncCallback) null, (object) null);
    }

    public virtual void LoadAsync(string url)
    {
      this.BitmapPath = url;
      this.LoadAsync();
    }

    public virtual void CancelAsync()
    {
      this.cancellationPending = true;
    }

    public virtual void OpenImage(Bitmap image)
    {
      this.SetNewBitmap(new Bitmap((Image) image), true);
      this.OnImageLoaded(new AsyncCompletedEventArgs((Exception) null, false, (object) null));
    }

    public virtual void OpenImage(string fileName)
    {
      this.CanvasElement.StopAllOperations();
      this.SetNewBitmap(new Bitmap(Image.FromFile(fileName)), true);
      this.BitmapPath = fileName;
      this.Invalidate();
      this.OnImageLoaded(new AsyncCompletedEventArgs((Exception) null, false, (object) null));
    }

    public virtual void OpenImage()
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.RestoreDirectory = true;
      openFileDialog.FilterIndex = 1;
      openFileDialog.Filter = "All image files|*.bmp; *.gif; *.ico; *.jpg; *.jpeg; *.jpe; *.jif; *.jfif; *.jfi; *.png; *.tiff; *.tif|Bitmap images|*.bmp|GIF images|*.gif|Icon images|*.ico|JPEG images|*.jpg; *.jpeg; *.jpe; *.jif; *.jfif; *.jfi|PNG images|*.png|TIFF images|*.tiff; *.tif";
      if (DialogResult.OK != openFileDialog.ShowDialog())
        return;
      this.OpenImage(openFileDialog.FileName);
    }

    public virtual void SaveImage()
    {
      if (this.CurrentBitmap == null)
        return;
      if (string.IsNullOrEmpty(this.BitmapPath))
      {
        this.SaveImageAs();
      }
      else
      {
        try
        {
          this.CurrentBitmap.Save(this.BitmapPath);
        }
        catch (Exception ex)
        {
          this.OnOperationError(new ErrorEventArgs(ex));
        }
      }
    }

    public virtual void SaveImage(string path)
    {
      this.BitmapPath = path;
      this.SaveImage();
    }

    public virtual void SaveImage(string path, ImageFormat imageFormat)
    {
      this.BitmapPath = path;
      this.CurrentBitmap.Save(path, imageFormat);
    }

    public virtual void SaveImage(Stream stream)
    {
      this.SaveImage(stream, ImageFormat.Bmp);
    }

    public virtual void SaveImage(Stream stream, ImageFormat imageFormat)
    {
      if (this.CurrentBitmap == null)
        return;
      this.CurrentBitmap.Save(stream, imageFormat);
    }

    public virtual void SaveImageAs()
    {
      if (this.CurrentBitmap == null)
        return;
      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.RestoreDirectory = true;
      saveFileDialog.FilterIndex = 1;
      saveFileDialog.Filter = "All image files|*.bmp; *.gif; *.ico; *.jpg; *.jpeg; *.jpe; *.jif; *.jfif; *.jfi; *.png; *.tiff; *.tif|Bitmap images|*.bmp|GIF images|*.gif|Icon images|*.ico|JPEG images|*.jpg; *.jpeg; *.jpe; *.jif; *.jfif; *.jfi|PNG images|*.png|TIFF images|*.tiff; *.tif";
      if (DialogResult.OK != saveFileDialog.ShowDialog())
        return;
      this.SaveImage(saveFileDialog.FileName);
    }

    public virtual void Undo()
    {
      if (this.UndoStack == null || this.UndoStack.Count == 0)
      {
        this.UpdateUndoRedoButtons();
      }
      else
      {
        this.RedoStack.Push((Bitmap) this.CurrentBitmap.Clone());
        if (this.CurrentBitmap != null)
          this.CurrentBitmap.Dispose();
        this.CurrentBitmap = this.UndoStack.Pop();
        if (this.originalBitmap != null)
          this.originalBitmap.Dispose();
        this.originalBitmap = (Bitmap) this.CurrentBitmap.Clone();
        this.UpdateUndoRedoButtons();
        this.Invalidate();
      }
    }

    public virtual void Redo()
    {
      if (this.RedoStack == null || this.RedoStack.Count == 0)
      {
        this.UpdateUndoRedoButtons();
      }
      else
      {
        this.UndoStack.Push((Bitmap) this.CurrentBitmap.Clone());
        if (this.CurrentBitmap != null)
          this.CurrentBitmap.Dispose();
        this.CurrentBitmap = this.RedoStack.Pop();
        if (this.originalBitmap != null)
          this.originalBitmap.Dispose();
        this.originalBitmap = (Bitmap) this.CurrentBitmap.Clone();
        this.UpdateUndoRedoButtons();
        this.Invalidate();
      }
    }

    public virtual void Resize(int width, int height)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        Rectangle destRect = new Rectangle(0, 0, width, height);
        Bitmap bitmap = new Bitmap(width, height);
        bitmap.SetResolution(this.OriginalBitmap.HorizontalResolution, this.OriginalBitmap.VerticalResolution);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.CompositingMode = CompositingMode.SourceCopy;
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.SmoothingMode = SmoothingMode.HighQuality;
          graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
          using (ImageAttributes imageAttr = new ImageAttributes())
          {
            imageAttr.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage((Image) this.OriginalBitmap, destRect, 0, 0, this.OriginalBitmap.Width, this.OriginalBitmap.Height, GraphicsUnit.Pixel, imageAttr);
          }
        }
        this.CurrentBitmap = (Bitmap) bitmap.Clone();
        bitmap.Dispose();
        BitmapData bitmapdata = this.CurrentBitmap.LockBits(new Rectangle(0, 0, this.CurrentBitmap.Width, this.CurrentBitmap.Height), ImageLockMode.ReadOnly, this.pixelFormat);
        this.bitmapStride = bitmapdata.Stride;
        this.bitmapStep = Image.GetPixelFormatSize(this.pixelFormat) / 8;
        this.CurrentBitmap.UnlockBits(bitmapdata);
        this.Invalidate();
      }
    }

    public virtual void ResizeCanvas(
      int width,
      int height,
      ContentAlignment imageAlignemnt,
      Color background)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        Size size = this.originalBitmap.Size;
        Rectangle destRect = new Rectangle(0, 0, this.originalBitmap.Width, this.originalBitmap.Height);
        switch (imageAlignemnt)
        {
          case ContentAlignment.TopCenter:
            destRect.X = (width - size.Width) / 2;
            break;
          case ContentAlignment.TopRight:
            destRect.X = width - size.Width;
            break;
          case ContentAlignment.MiddleLeft:
            destRect.Y = (height - size.Height) / 2;
            break;
          case ContentAlignment.MiddleCenter:
            destRect.X = (width - size.Width) / 2;
            destRect.Y = (height - size.Height) / 2;
            break;
          case ContentAlignment.MiddleRight:
            destRect.X = width - size.Width;
            destRect.Y = (height - size.Height) / 2;
            break;
          case ContentAlignment.BottomLeft:
            destRect.Y = height - size.Height;
            break;
          case ContentAlignment.BottomCenter:
            destRect.X = (width - size.Width) / 2;
            destRect.Y = height - size.Height;
            break;
          case ContentAlignment.BottomRight:
            destRect.X = width - size.Width;
            destRect.Y = height - size.Height;
            break;
        }
        Bitmap bitmap = new Bitmap(width, height);
        bitmap.SetResolution(this.originalBitmap.HorizontalResolution, this.originalBitmap.VerticalResolution);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.CompositingMode = CompositingMode.SourceCopy;
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.SmoothingMode = SmoothingMode.HighQuality;
          graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
          using (ImageAttributes imageAttr = new ImageAttributes())
          {
            imageAttr.SetWrapMode(WrapMode.TileFlipXY);
            graphics.Clear(background);
            graphics.DrawImage((Image) this.originalBitmap, destRect, 0, 0, this.originalBitmap.Width, this.originalBitmap.Height, GraphicsUnit.Pixel, imageAttr);
          }
        }
        this.CurrentBitmap = (Bitmap) bitmap.Clone();
        bitmap.Dispose();
        BitmapData bitmapdata = this.CurrentBitmap.LockBits(new Rectangle(0, 0, this.CurrentBitmap.Width, this.CurrentBitmap.Height), ImageLockMode.ReadOnly, this.pixelFormat);
        this.bitmapStride = bitmapdata.Stride;
        this.bitmapStep = Image.GetPixelFormatSize(this.pixelFormat) / 8;
        this.CurrentBitmap.UnlockBits(bitmapdata);
        this.Invalidate();
      }
    }

    public virtual void RotateFlip(RotateFlipType rotateFlipType)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        Bitmap bitmap = (Bitmap) this.originalBitmap.Clone();
        bitmap.RotateFlip(rotateFlipType);
        this.CurrentBitmap = (Bitmap) bitmap.Clone();
        bitmap.Dispose();
        BitmapData bitmapdata = this.CurrentBitmap.LockBits(new Rectangle(0, 0, this.CurrentBitmap.Width, this.CurrentBitmap.Height), ImageLockMode.ReadOnly, this.pixelFormat);
        this.bitmapStride = bitmapdata.Stride;
        this.bitmapStep = Image.GetPixelFormatSize(this.pixelFormat) / 8;
        this.CurrentBitmap.UnlockBits(bitmapdata);
        this.Invalidate();
      }
    }

    public virtual void RoundCorners(
      int cornerRadius,
      Color background,
      int borderThickness,
      Color borderColor)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        Rectangle rectangle = new Rectangle(0, 0, this.originalBitmap.Width, this.originalBitmap.Height);
        Bitmap bitmap = new Bitmap(this.originalBitmap.Width, this.originalBitmap.Height);
        bitmap.SetResolution(this.originalBitmap.HorizontalResolution, this.originalBitmap.VerticalResolution);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.CompositingMode = CompositingMode.SourceCopy;
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.SmoothingMode = SmoothingMode.HighQuality;
          graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
          graphics.Clear(background);
          Region roundRectRgn1 = Telerik.WinControls.NativeMethods.CreateRoundRectRgn(rectangle, cornerRadius);
          graphics.Clip = roundRectRgn1;
          using (SolidBrush solidBrush = new SolidBrush(borderColor))
            graphics.FillRegion((Brush) solidBrush, roundRectRgn1);
          graphics.ResetClip();
          rectangle.X += borderThickness;
          rectangle.Y += borderThickness;
          rectangle.Width -= borderThickness;
          rectangle.Height -= borderThickness;
          Region roundRectRgn2 = Telerik.WinControls.NativeMethods.CreateRoundRectRgn(rectangle, cornerRadius);
          graphics.Clip = roundRectRgn2;
          graphics.DrawImage((Image) this.originalBitmap, rectangle, 0, 0, this.originalBitmap.Width, this.originalBitmap.Height, GraphicsUnit.Pixel);
          graphics.ResetClip();
        }
        this.CurrentBitmap = (Bitmap) bitmap.Clone();
        bitmap.Dispose();
        this.Invalidate();
      }
    }

    public virtual void StartCrop()
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        this.CloseOpenDialog();
        this.canvasElement.StartCrop();
      }
    }

    public virtual void StopCrop(bool commit)
    {
      Rectangle cropRect = this.canvasElement.StopCrop();
      if (!commit)
        return;
      this.Crop(cropRect);
    }

    public virtual void Crop(Rectangle cropRect)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        Bitmap bitmap = new Bitmap(cropRect.Width, cropRect.Height);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
          graphics.DrawImage((Image) this.originalBitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), cropRect, GraphicsUnit.Pixel);
        this.CurrentBitmap = (Bitmap) bitmap.Clone();
        BitmapData bitmapdata = this.CurrentBitmap.LockBits(new Rectangle(0, 0, this.CurrentBitmap.Width, this.CurrentBitmap.Height), ImageLockMode.ReadOnly, this.pixelFormat);
        this.bitmapStride = bitmapdata.Stride;
        this.bitmapStep = Image.GetPixelFormatSize(this.pixelFormat) / 8;
        this.CurrentBitmap.UnlockBits(bitmapdata);
        this.SaveState();
      }
    }

    public virtual void StartPan()
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        this.CloseOpenDialog();
        this.CanvasElement.StartPan();
      }
    }

    public virtual void StopPan()
    {
      this.CanvasElement.StopPan();
    }

    public virtual void DrawString(
      string text,
      int fontSize,
      Color foreColor,
      int x,
      int y,
      int angle)
    {
      if (this.CurrentBitmap == null)
        this.OpenImage();
      else if (string.IsNullOrEmpty(text))
      {
        this.CurrentBitmap = (Bitmap) this.originalBitmap.Clone();
        this.Invalidate();
      }
      else
      {
        Rectangle destRect = new Rectangle(0, 0, this.originalBitmap.Width, this.originalBitmap.Height);
        Bitmap bitmap = new Bitmap(this.originalBitmap.Width, this.originalBitmap.Height);
        bitmap.SetResolution(this.originalBitmap.HorizontalResolution, this.originalBitmap.VerticalResolution);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.CompositingMode = CompositingMode.SourceCopy;
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.SmoothingMode = SmoothingMode.HighQuality;
          graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
          graphics.DrawImage((Image) this.originalBitmap, destRect, 0, 0, this.originalBitmap.Width, this.originalBitmap.Height, GraphicsUnit.Pixel);
          using (SolidBrush solidBrush = new SolidBrush(foreColor))
          {
            using (Font font = new Font(this.Font.FontFamily, (float) fontSize))
            {
              graphics.CompositingMode = CompositingMode.SourceOver;
              graphics.TranslateTransform((float) x, (float) y);
              graphics.RotateTransform((float) angle);
              graphics.DrawString(text, font, (Brush) solidBrush, 0.0f, 0.0f);
              graphics.RotateTransform((float) -angle);
              graphics.TranslateTransform((float) -x, (float) -y);
            }
          }
        }
        this.CurrentBitmap = (Bitmap) bitmap.Clone();
        bitmap.Dispose();
        this.Invalidate();
      }
    }

    public virtual void StartDrawing(ShapeInfo shapeInfo)
    {
      if (this.CurrentBitmap == null)
        this.OpenImage();
      else
        this.CanvasElement.StartDrawing(shapeInfo);
    }

    public virtual void DrawShape(
      GraphicsPath shape,
      Color fill,
      Color stroke,
      int borderThickness)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        Rectangle destRect = new Rectangle(0, 0, this.originalBitmap.Width, this.originalBitmap.Height);
        Bitmap bitmap = new Bitmap(this.originalBitmap.Width, this.originalBitmap.Height);
        bitmap.SetResolution(this.originalBitmap.HorizontalResolution, this.originalBitmap.VerticalResolution);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        {
          graphics.CompositingMode = CompositingMode.SourceCopy;
          graphics.CompositingQuality = CompositingQuality.HighQuality;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.SmoothingMode = SmoothingMode.HighQuality;
          graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
          graphics.DrawImage((Image) this.originalBitmap, destRect, 0, 0, this.originalBitmap.Width, this.originalBitmap.Height, GraphicsUnit.Pixel);
          using (SolidBrush solidBrush = new SolidBrush(fill))
          {
            using (Pen pen = new Pen(stroke, (float) borderThickness))
            {
              graphics.CompositingMode = CompositingMode.SourceOver;
              graphics.FillPath((Brush) solidBrush, shape);
              graphics.DrawPath(pen, shape);
            }
          }
        }
        this.CurrentBitmap = (Bitmap) bitmap.Clone();
        bitmap.Dispose();
        this.Invalidate();
      }
    }

    public virtual void StopDrawing()
    {
      this.CanvasElement.StopDrawing();
    }

    public virtual void SetHue(int hue)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        hue = Math.Min(Math.Max(0, hue), 360);
        if (hue == 0)
        {
          this.CurrentBitmap = (Bitmap) this.originalBitmap.Clone();
          this.Invalidate();
        }
        else
        {
          byte[] imageArray = this.GetImageArray();
          for (int index = this.bitmapStep - 1; index < imageArray.Length; index += this.bitmapStep)
          {
            Color color = Color.FromArgb((int) imageArray[index], (int) imageArray[index - 3], (int) imageArray[index - 2], (int) imageArray[index - 1]);
            float hue1 = color.GetHue();
            float saturation = color.GetSaturation();
            float brightness = color.GetBrightness();
            color = ImageHelper.HslToRgb(((double) hue1 + (double) hue) % 360.0 / 360.0, (double) saturation, (double) brightness, (double) color.A);
            imageArray[index - 3] = color.R;
            imageArray[index - 2] = color.G;
            imageArray[index - 1] = color.B;
            imageArray[index] = color.A;
          }
          this.SetImageArray(ref imageArray);
          this.Invalidate();
        }
      }
    }

    public virtual void SetSaturation(int saturation)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        saturation = Math.Min(Math.Max(-100, saturation), 100);
        if (saturation == 0)
        {
          this.CurrentBitmap = (Bitmap) this.originalBitmap.Clone();
          this.Invalidate();
        }
        else
        {
          byte[] imageArray = this.GetImageArray();
          for (int index = this.bitmapStep - 1; index < imageArray.Length; index += this.bitmapStep)
          {
            Color color = Color.FromArgb((int) imageArray[index], (int) imageArray[index - 3], (int) imageArray[index - 2], (int) imageArray[index - 1]);
            float hue = color.GetHue();
            float saturation1 = color.GetSaturation();
            float brightness = color.GetBrightness();
            float num = Math.Min(Math.Max(saturation1 + (float) saturation / 100f, 0.0f), 1f);
            color = ImageHelper.HslToRgb((double) hue / 360.0, (double) num, (double) brightness, (double) color.A);
            imageArray[index - 3] = color.R;
            imageArray[index - 2] = color.G;
            imageArray[index - 1] = color.B;
            imageArray[index] = color.A;
          }
          this.SetImageArray(ref imageArray);
          this.Invalidate();
        }
      }
    }

    public virtual void SetContrastAndBrightness(int contrast, int brightness)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        contrast = Math.Min(Math.Max(-100, contrast), 100);
        brightness = Math.Min(Math.Max(-100, brightness), 100);
        if (contrast == 0 && brightness == 0)
        {
          this.CurrentBitmap = (Bitmap) this.originalBitmap.Clone();
          this.Invalidate();
        }
        else
        {
          byte[] imageArray = this.GetImageArray();
          double num1 = Math.Pow((100.0 + (double) contrast) / 100.0, 2.0);
          int num2 = (int) byte.MaxValue * brightness / 100;
          for (int index = this.bitmapStep - 1; index < imageArray.Length; index += this.bitmapStep)
          {
            int num3 = (int) imageArray[index - 3];
            int num4 = (int) imageArray[index - 2];
            int num5 = (int) imageArray[index - 1];
            double num6 = (((double) num3 / (double) byte.MaxValue - 0.5) * num1 + 0.5) * (double) byte.MaxValue;
            if (num6 < 0.0)
              num6 = 0.0;
            if (num6 > (double) byte.MaxValue)
              num6 = (double) byte.MaxValue;
            double num7 = (((double) num4 / (double) byte.MaxValue - 0.5) * num1 + 0.5) * (double) byte.MaxValue;
            if (num7 < 0.0)
              num7 = 0.0;
            if (num7 > (double) byte.MaxValue)
              num7 = (double) byte.MaxValue;
            double num8 = (((double) num5 / (double) byte.MaxValue - 0.5) * num1 + 0.5) * (double) byte.MaxValue;
            if (num8 < 0.0)
              num8 = 0.0;
            if (num8 > (double) byte.MaxValue)
              num8 = (double) byte.MaxValue;
            imageArray[index - 3] = (byte) Math.Min(Math.Max(0.0, num6 + (double) num2), (double) byte.MaxValue);
            imageArray[index - 2] = (byte) Math.Min(Math.Max(0.0, num7 + (double) num2), (double) byte.MaxValue);
            imageArray[index - 1] = (byte) Math.Min(Math.Max(0.0, num8 + (double) num2), (double) byte.MaxValue);
          }
          this.SetImageArray(ref imageArray);
          this.Invalidate();
        }
      }
    }

    public virtual void InvertColors()
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        byte[] imageArray = this.GetImageArray();
        for (int index = this.bitmapStep - 1; index < imageArray.Length; index += this.bitmapStep)
        {
          imageArray[index - 3] = (byte) ((uint) byte.MaxValue - (uint) imageArray[index - 3]);
          imageArray[index - 2] = (byte) ((uint) byte.MaxValue - (uint) imageArray[index - 2]);
          imageArray[index - 1] = (byte) ((uint) byte.MaxValue - (uint) imageArray[index - 1]);
        }
        this.SetImageArray(ref imageArray);
        this.Invalidate();
      }
    }

    public virtual void Sharpen(double strength)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        strength = Math.Min(Math.Max(0.0, strength), 100.0);
        if (strength == 0.0)
        {
          this.CurrentBitmap = (Bitmap) this.originalBitmap.Clone();
          this.Invalidate();
        }
        else
        {
          double num1 = 1.0 - strength;
          double num2 = strength / 16.0;
          int length = this.sharpenFilterMatrix.GetLength(0);
          int num3 = length / 2;
          int width = this.originalBitmap.Width;
          int height = this.originalBitmap.Height;
          Color[,] colorArray = new Color[width, height];
          byte[] imageArray = this.GetImageArray();
          for (int index1 = num3; index1 < width - num3; ++index1)
          {
            for (int index2 = num3; index2 < height - num3; ++index2)
            {
              double num4 = 0.0;
              double num5 = 0.0;
              double num6 = 0.0;
              for (int index3 = 0; index3 < length; ++index3)
              {
                for (int index4 = 0; index4 < length; ++index4)
                {
                  int num7 = (index1 - num3 + index3 + width) % width;
                  int index5 = (index2 - num3 + index4 + height) % height * this.bitmapStride + this.bitmapStep * num7;
                  num4 += (double) imageArray[index5 + 2] * this.sharpenFilterMatrix[index3, index4];
                  num5 += (double) imageArray[index5 + 1] * this.sharpenFilterMatrix[index3, index4];
                  num6 += (double) imageArray[index5] * this.sharpenFilterMatrix[index3, index4];
                }
                int index6 = index2 * this.bitmapStride + this.bitmapStep * index1;
                int red = Math.Min(Math.Max((int) (num2 * num4 + num1 * (double) imageArray[index6 + 2]), 0), (int) byte.MaxValue);
                int green = Math.Min(Math.Max((int) (num2 * num5 + num1 * (double) imageArray[index6 + 1]), 0), (int) byte.MaxValue);
                int blue = Math.Min(Math.Max((int) (num2 * num6 + num1 * (double) imageArray[index6]), 0), (int) byte.MaxValue);
                colorArray[index1, index2] = Color.FromArgb(red, green, blue);
              }
            }
          }
          for (int index1 = num3; index1 < width - num3; ++index1)
          {
            for (int index2 = num3; index2 < height - num3; ++index2)
            {
              int index3 = index2 * this.bitmapStride + this.bitmapStep * index1;
              imageArray[index3 + 2] = colorArray[index1, index2].R;
              imageArray[index3 + 1] = colorArray[index1, index2].G;
              imageArray[index3] = colorArray[index1, index2].B;
            }
          }
          this.SetImageArray(ref imageArray);
          this.Invalidate();
        }
      }
    }

    public virtual void Blur(int bias)
    {
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        bias = Math.Min(Math.Max(0, bias), 100);
        if (bias == 0)
        {
          this.CurrentBitmap = (Bitmap) this.originalBitmap.Clone();
          this.Invalidate();
        }
        else
        {
          int width = this.originalBitmap.Width;
          int height = this.originalBitmap.Height;
          byte[] rgbValues = this.GetImageArray();
          byte[] numArray = new byte[rgbValues.Length];
          for (int index1 = 0; index1 < bias; ++index1)
          {
            int length = this.blurFilterMatrix.GetLength(1);
            this.blurFilterMatrix.GetLength(0);
            int num1 = (length - 1) / 2;
            for (int index2 = num1; index2 < height - num1; ++index2)
            {
              for (int index3 = num1; index3 < width - num1; ++index3)
              {
                double num2 = 0.0;
                double num3 = 0.0;
                double num4 = 0.0;
                double num5 = 0.0;
                int index4 = index2 * this.bitmapStride + index3 * this.bitmapStep;
                for (int index5 = -num1; index5 <= num1; ++index5)
                {
                  for (int index6 = -num1; index6 <= num1; ++index6)
                  {
                    int index7 = index4 + index6 * this.bitmapStep + index5 * this.bitmapStride;
                    num2 += (double) rgbValues[index7 + 3] * this.blurFilterMatrix[index5 + num1, index6 + num1];
                    num3 += (double) rgbValues[index7 + 2] * this.blurFilterMatrix[index5 + num1, index6 + num1];
                    num4 += (double) rgbValues[index7 + 1] * this.blurFilterMatrix[index5 + num1, index6 + num1];
                    num5 += (double) rgbValues[index7] * this.blurFilterMatrix[index5 + num1, index6 + num1];
                  }
                }
                double val2_1 = this.blurFactor * num2;
                double val2_2 = this.blurFactor * num3;
                double val2_3 = this.blurFactor * num4;
                double val2_4 = this.blurFactor * num5;
                double num6 = Math.Min(Math.Max(0.0, val2_1), (double) byte.MaxValue);
                double num7 = Math.Min(Math.Max(0.0, val2_2), (double) byte.MaxValue);
                double num8 = Math.Min(Math.Max(0.0, val2_3), (double) byte.MaxValue);
                double num9 = Math.Min(Math.Max(0.0, val2_4), (double) byte.MaxValue);
                numArray[index4] = (byte) num9;
                numArray[index4 + 1] = (byte) num8;
                numArray[index4 + 2] = (byte) num7;
                numArray[index4 + 3] = (byte) num6;
              }
            }
            rgbValues = numArray;
          }
          this.SetImageArray(ref rgbValues);
          this.Invalidate();
        }
      }
    }

    public virtual void ShowResizeDialog()
    {
      this.ShowDialog(typeof (ResizeDialog));
    }

    public virtual void ShowCanvasResizeDialog()
    {
      this.ShowDialog(typeof (ResizeCanvasDialog));
    }

    public virtual void ShowRoundCornersDialog()
    {
      this.ShowDialog(typeof (RoundCornersDialog));
    }

    public virtual void ShowDrawTextDialog()
    {
      this.ShowDialog(typeof (DrawTextDialog));
    }

    public virtual void ShowSaturationDialog()
    {
      this.ShowDialog(typeof (SaturationDialog));
    }

    public virtual void ShowContrastDialog()
    {
      this.ShowDialog(typeof (ContrastDialog));
    }

    public virtual void ShowSharpenDialog()
    {
      this.ShowDialog(typeof (SharpenDialog));
    }

    public virtual void ShowBlurDialog()
    {
      this.ShowDialog(typeof (BlurDialog));
    }

    public virtual void ShowHueDialog()
    {
      this.ShowDialog(typeof (HueShiftDialog));
    }

    public virtual void ShowDrawShapeDialog()
    {
      this.ShowTopMostDialog(typeof (DrawShapeDialog));
    }

    public virtual void ShowDrawDialog()
    {
      this.ShowTopMostDialog(typeof (DrawDialog));
    }

    protected virtual void ShowDialog(System.Type dialogType)
    {
      this.CloseOpenDialog();
      this.CanvasElement.StopAllOperations();
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        if (this.DialogFactory.CreateDialog(dialogType, this).ShowDialog() != DialogResult.OK)
          return;
        this.SaveState();
      }
    }

    protected virtual void ShowTopMostDialog(System.Type dialogType)
    {
      this.CloseOpenDialog();
      if (this.CurrentBitmap == null)
      {
        this.OpenImage();
      }
      else
      {
        ImageEditorBaseDialog dialog = this.DialogFactory.CreateDialog(dialogType, this);
        dialog.TopMost = true;
        dialog.Show();
        dialog.FormClosed += (FormClosedEventHandler) ((param0, param1) =>
        {
          this.StopDrawing();
          this.openDialog = (ImageEditorBaseDialog) null;
        });
        this.openDialog = dialog;
      }
    }

    public void CloseOpenDialog()
    {
      if (this.openDialog == null)
        return;
      this.openDialog.Close();
      this.openDialog = (ImageEditorBaseDialog) null;
    }

    protected virtual void SetNewBitmap(Bitmap value, bool disposeOldState)
    {
      if (disposeOldState)
        this.DisposeBitmaps();
      this.CurrentBitmap = value;
      this.originalBitmap = (Bitmap) this.CurrentBitmap.Clone();
      BitmapData bitmapdata = this.CurrentBitmap.LockBits(new Rectangle(0, 0, this.CurrentBitmap.Width, this.CurrentBitmap.Height), ImageLockMode.ReadOnly, this.pixelFormat);
      this.bitmapStride = bitmapdata.Stride;
      this.bitmapStep = Image.GetPixelFormatSize(this.pixelFormat) / 8;
      this.CurrentBitmap.UnlockBits(bitmapdata);
      this.InvalidateMeasure(true);
      this.UpdateLayout();
    }

    protected virtual byte[] GetImageArray()
    {
      BitmapData bitmapData = this.originalBitmap.LockBits(new Rectangle(0, 0, this.originalBitmap.Width, this.originalBitmap.Height), ImageLockMode.ReadOnly, this.pixelFormat);
      int numberOfBytes = this.GetNumberOfBytes(bitmapData);
      byte[] destination = new byte[numberOfBytes];
      Marshal.Copy(bitmapData.Scan0, destination, 0, numberOfBytes);
      this.originalBitmap.UnlockBits(bitmapData);
      return destination;
    }

    protected virtual void SetImageArray(ref byte[] rgbValues)
    {
      BitmapData bitmapData = this.CurrentBitmap.LockBits(new Rectangle(0, 0, this.originalBitmap.Width, this.originalBitmap.Height), ImageLockMode.WriteOnly, this.pixelFormat);
      Marshal.Copy(rgbValues, 0, bitmapData.Scan0, this.GetNumberOfBytes(bitmapData));
      this.CurrentBitmap.UnlockBits(bitmapData);
    }

    protected virtual int GetNumberOfBytes(BitmapData data)
    {
      int num1 = Math.Abs(data.Stride) * data.Height;
      int num2 = Image.GetPixelFormatSize(this.pixelFormat) / 8;
      int num3 = num1 % num2;
      if (num3 != 0)
        num1 += num2 - num3;
      return num1;
    }

    public virtual void SaveState()
    {
      if (this.CurrentBitmap == null)
        return;
      this.UndoStack.Push((Bitmap) this.originalBitmap.Clone());
      this.UpdateUndoRedoButtons();
      if (this.originalBitmap != null)
        this.originalBitmap.Dispose();
      this.originalBitmap = (Bitmap) this.CurrentBitmap.Clone();
    }

    protected override void DisposeManagedResources()
    {
      this.DisposeBitmaps();
      this.CloseOpenDialog();
      base.DisposeManagedResources();
    }

    protected virtual void DisposeBitmaps()
    {
      if (this.originalBitmap != null)
        this.originalBitmap.Dispose();
      if (this.CurrentBitmap != null)
        this.CurrentBitmap.Dispose();
      while (this.UndoStack != null && this.UndoStack.Count > 0)
        this.UndoStack.Pop().Dispose();
      while (this.RedoStack != null && this.RedoStack.Count > 0)
        this.RedoStack.Pop().Dispose();
    }

    protected virtual void UpdateUndoRedoButtons()
    {
      this.CommandsElement.UndoButton.Enabled = this.UndoStack != null && this.UndoStack.Count > 0;
      this.CommandsElement.RedoButton.Enabled = this.RedoStack != null && this.RedoStack.Count > 0;
    }

    private void BeginGetResponseDelegate(object arg)
    {
      WebRequest webRequest = (WebRequest) arg;
      webRequest.BeginGetResponse(new AsyncCallback(this.GetResponseCallback), (object) webRequest);
    }

    private void GetResponseCallback(IAsyncResult result)
    {
      if (this.cancellationPending)
      {
        this.PostCompleted((Exception) null, true);
      }
      else
      {
        try
        {
          WebResponse response = ((WebRequest) result.AsyncState).EndGetResponse(result);
          this.totalBytesRead = 0;
          this.contentLength = (int) response.ContentLength;
          Stream responseStream = response.GetResponseStream();
          responseStream.BeginRead(this.readBuffer, 0, this.readBlockSize, new AsyncCallback(this.ReadCallBack), (object) responseStream);
        }
        catch (Exception ex)
        {
          this.PostCompleted(ex, false);
        }
      }
    }

    private void ReadCallBack(IAsyncResult result)
    {
      if (this.cancellationPending)
      {
        this.PostCompleted((Exception) null, true);
      }
      else
      {
        Stream stream1 = (Stream) result.AsyncState;
        try
        {
          int count = stream1.EndRead(result);
          if (count > 0)
          {
            this.totalBytesRead += count;
            this.tempDownloadStream.Write(this.readBuffer, 0, count);
            stream1.BeginRead(this.readBuffer, 0, this.readBlockSize, new AsyncCallback(this.ReadCallBack), (object) stream1);
            if (this.contentLength == -1)
              return;
            int progressPercentage = (int) (100.0 * ((double) this.totalBytesRead / (double) this.contentLength));
            if (this.currentAsyncLoadOperation == null)
              return;
            this.currentAsyncLoadOperation.Post(this.loadProgressDelegate, (object) new ProgressChangedEventArgs(progressPercentage, (object) null));
          }
          else
          {
            this.tempDownloadStream.Seek(0L, SeekOrigin.Begin);
            if (this.currentAsyncLoadOperation != null)
              this.currentAsyncLoadOperation.Post(this.loadProgressDelegate, (object) new ProgressChangedEventArgs(100, (object) null));
            this.PostCompleted((Exception) null, false);
            Stream stream2 = stream1;
            stream1 = (Stream) null;
            stream2.Close();
          }
        }
        catch (Exception ex)
        {
          this.PostCompleted(ex, false);
          stream1?.Close();
        }
      }
    }

    private void LoadCompletedDelegate(object arg)
    {
      AsyncCompletedEventArgs e = (AsyncCompletedEventArgs) arg;
      Bitmap bitmap = (Bitmap) null;
      if (!e.Cancelled)
      {
        if (e.Error == null)
        {
          try
          {
            bitmap = new Bitmap(Image.FromStream((Stream) this.tempDownloadStream));
          }
          catch (Exception ex)
          {
            e = new AsyncCompletedEventArgs(ex, false, (object) null);
          }
        }
      }
      if (!e.Cancelled)
        this.SetNewBitmap(bitmap, true);
      this.tempDownloadStream = (MemoryStream) null;
      this.cancellationPending = false;
      this.asyncOperationInProgress = false;
      this.OnImageLoaded(e);
    }

    private void LoadProgressDelegate(object arg)
    {
      this.OnImageLoadAsyncProgressChanged((ProgressChangedEventArgs) arg);
    }

    private void PostCompleted(Exception error, bool cancelled)
    {
      AsyncOperation asyncLoadOperation = this.currentAsyncLoadOperation;
      this.currentAsyncLoadOperation = (AsyncOperation) null;
      asyncLoadOperation?.PostOperationCompleted(this.loadCompletedDelegate, (object) new AsyncCompletedEventArgs(error, cancelled, (object) null));
    }

    public event AsyncCompletedEventHandler ImageLoaded;

    protected virtual void OnImageLoaded(AsyncCompletedEventArgs e)
    {
      if (this.ImageLoaded == null)
        return;
      this.ImageLoaded((object) this, e);
    }

    public event ProgressChangedEventHandler ImageLoadAsyncProgressChanged;

    protected virtual void OnImageLoadAsyncProgressChanged(ProgressChangedEventArgs e)
    {
      if (this.ImageLoadAsyncProgressChanged == null)
        return;
      this.ImageLoadAsyncProgressChanged((object) this, e);
    }

    public event ErrorEventHandler OperationError;

    private void OnOperationError(ErrorEventArgs e)
    {
      if (this.OperationError == null)
        return;
      this.OperationError((object) this, e);
    }

    protected internal virtual bool ProcessMouseDown(MouseEventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessMouseUp(MouseEventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessMouseClick(MouseEventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessDoubleClick(MouseEventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessMouseEnter(EventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessMouseLeave(EventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessMouseMove(MouseEventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessMouseHover(EventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessMouseWheel(MouseEventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessKeyPress(KeyPressEventArgs e)
    {
      return false;
    }

    protected internal virtual bool ProcessKeyDown(KeyEventArgs e)
    {
      if (e.Control)
      {
        if ((e.KeyData & Keys.Z) == Keys.Z)
        {
          this.Undo();
          return true;
        }
        if ((e.KeyData & Keys.Y) == Keys.Y)
        {
          this.Redo();
          return true;
        }
      }
      return false;
    }

    protected internal virtual bool ProcessKeyUp(KeyEventArgs e)
    {
      return false;
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      return base.MeasureOverride(availableSize);
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      RectangleF finalRect1 = new RectangleF(clientRectangle.X, clientRectangle.Y, (float) this.CommandsElementWidth, clientRectangle.Height);
      RectangleF finalRect2 = new RectangleF(clientRectangle.X + (float) this.CommandsElementWidth, clientRectangle.Y, clientRectangle.Width - (float) this.CommandsElementWidth, clientRectangle.Height - (float) this.ZoomElementHeight);
      RectangleF finalRect3 = new RectangleF(clientRectangle.X + (float) this.CommandsElementWidth, clientRectangle.Bottom - (float) this.ZoomElementHeight, clientRectangle.Width - (float) this.CommandsElementWidth, (float) this.ZoomElementHeight);
      this.commandsElement.Arrange(finalRect1);
      this.scrollViewer.Arrange(finalRect2);
      this.zoomElement.Arrange(finalRect3);
      return new SizeF(finalRect1.Size.Width + finalRect2.Size.Width, finalRect2.Height + finalRect3.Height);
    }
  }
}
