// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadBarcodeElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using Telerik.WinControls.Paint;
using Telerik.WinControls.UI.Barcode.Symbology;

namespace Telerik.WinControls.UI
{
  public class RadBarcodeElement : LightVisualElement
  {
    protected SizeF previousDpi = new SizeF(1f, 1f);
    private IElementFactory elementFactory;
    private ISymbology symbology;
    private string value;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DefaultSize = new Size(200, 100);
      this.BackColor = Color.White;
      this.ForeColor = Color.Black;
    }

    public RadBarcodeElement()
    {
      this.elementFactory = (IElementFactory) new BaseBarcodeElementFactory(this);
    }

    public IElementFactory ElementFactory
    {
      get
      {
        return this.elementFactory;
      }
      set
      {
        this.elementFactory = value;
        this.Update();
      }
    }

    [Editor("Telerik.WinControls.UI.Design.BarcodeSymbologyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [TypeConverter(typeof (BarcodeSymbologyConverter))]
    [DefaultValue(null)]
    public ISymbology Symbology
    {
      get
      {
        return this.symbology;
      }
      set
      {
        if (this.symbology == value)
          return;
        BarcodeSymbologyChangingEventArgs e = new BarcodeSymbologyChangingEventArgs(this.symbology, value);
        this.OnSymbologyChanging(e);
        if (e.Cancel)
          return;
        if (this.symbology != null)
          this.symbology.PropertyChanged -= new PropertyChangedEventHandler(this.Symbology_PropertyChanged);
        if (this.IsDesignMode)
          this.value = (string) null;
        this.symbology = value;
        this.Update();
        if (this.symbology != null)
          this.symbology.PropertyChanged += new PropertyChangedEventHandler(this.Symbology_PropertyChanged);
        this.OnSymbologyChanged(EventArgs.Empty);
      }
    }

    [DefaultValue(null)]
    public string Value
    {
      get
      {
        return this.value;
      }
      set
      {
        BarcodeValueChangingEventArgs e = new BarcodeValueChangingEventArgs(this.value, value);
        this.OnValueChanging(e);
        if (e.Cancel)
          return;
        this.value = value;
        this.Update();
        this.OnValueChanged(EventArgs.Empty);
      }
    }

    protected virtual void Update()
    {
      if (string.IsNullOrEmpty(this.Value) || this.Symbology == null)
      {
        this.ElementFactory.ClearElements();
        this.Invalidate();
      }
      else
      {
        (this.Symbology as Symbology1D)?.ProcessValue(this.Value);
        PDF417 symbology = this.Symbology as PDF417;
        symbology?.PopulateMatrix(this.value, symbology.ECLevel, symbology.EncodingMode, symbology.Columns, symbology.Rows);
        (this.Symbology as QRCode)?.EncodeData(this.value);
        this.InvalidateMeasure();
        this.InvalidateArrange();
        this.UpdateLayout();
        this.Invalidate();
      }
    }

    [Category("Action")]
    [Description("Occurs after the value of the barcode is chnaged.")]
    public event EventHandler ValueChanged;

    protected virtual void OnValueChanged(EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, e);
    }

    [Category("Action")]
    [Description("Occurs before the value of the barcode is chnaged.")]
    public event BarcodeValueChangingEventHandler ValueChanging;

    protected virtual void OnValueChanging(BarcodeValueChangingEventArgs e)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, e);
    }

    [Description("Occurs after the symbology of the barcode is chnaged.")]
    [Category("Action")]
    public event EventHandler SymbologyChanged;

    protected virtual void OnSymbologyChanged(EventArgs e)
    {
      if (this.SymbologyChanged == null)
        return;
      this.SymbologyChanged((object) this, e);
    }

    [Category("Action")]
    [Description("Occurs before the symbology of the barcode is chnaged.")]
    public event BarcodeSymbologyChangingEventHandler SymbologyChanging;

    protected virtual void OnSymbologyChanging(BarcodeSymbologyChangingEventArgs e)
    {
      if (this.SymbologyChanging == null)
        return;
      this.SymbologyChanging((object) this, e);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.PaintElements(graphics, this.Symbology, this.ElementFactory);
    }

    protected virtual void PaintElements(
      IGraphics graphics,
      ISymbology symbology,
      IElementFactory elementFactory)
    {
      if (symbology is PDF417)
        graphics.TranslateTransform((float) (((double) this.Bounds.Width - (double) elementFactory.ElementsBounds.Width) / 2.0), (float) (((double) this.Bounds.Height - (double) elementFactory.ElementsBounds.Height) / 2.0));
      else if (symbology is QRCode)
        TelerikDpiHelper.ScaleInt(4, this.DpiScaleFactor);
      (graphics.UnderlayGraphics as Graphics).Clear(this.BackColor);
      foreach (BarcodeElementBase element in elementFactory.Elements)
        element.PaintElement(graphics.UnderlayGraphics as Graphics);
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      SizeF sizeF1 = base.MeasureOverride(availableSize);
      if (this.Value == null)
        return sizeF1;
      SizeF sizeF2 = this.MeasureSymbology(availableSize);
      if (float.IsInfinity(sizeF2.Width))
        sizeF2.Width = TelerikDpiHelper.ScaleFloat((float) this.DefaultSize.Width, this.DpiScaleFactor);
      if (float.IsInfinity(sizeF2.Height))
        sizeF2.Height = TelerikDpiHelper.ScaleFloat((float) this.DefaultSize.Height, this.DpiScaleFactor);
      if (float.IsInfinity(availableSize.Width) || float.IsInfinity(availableSize.Height))
        return sizeF2;
      return availableSize;
    }

    protected virtual SizeF MeasureSymbology(SizeF availableSize)
    {
      Symbology1D symbology1 = this.Symbology as Symbology1D;
      if (symbology1 != null)
        return symbology1.MeasureContent((IMeasureContext) new MeasureContext(this.GetScaledFont(this.DpiScaleFactor.Height)), availableSize);
      PDF417 symbology2 = this.Symbology as PDF417;
      if (symbology2 != null)
        return new SizeF((float) (symbology2.DataMatrix.GetLength(1) * symbology2.Module), (float) (symbology2.DataMatrix.GetLength(0) * symbology2.Module));
      QRCode symbology3 = this.Symbology as QRCode;
      if (symbology3 != null)
        return new SizeF((float) (symbology3.BinaryMatrix.GetLength(1) * symbology3.Module), (float) (symbology3.BinaryMatrix.GetLength(0) * symbology3.Module));
      return SizeF.Empty;
    }

    protected override SizeF ArrangeOverride(SizeF finalSize)
    {
      SizeF sizeF = base.ArrangeOverride(finalSize);
      if (this.Value == null || this.Symbology == null)
        return sizeF;
      RectangleF clientRectangle = this.GetClientRectangle(finalSize);
      (this.Symbology as Symbology1D)?.ArrangeContent((IMeasureContext) new MeasureContext(this.GetScaledFont(this.DpiScaleFactor.Height)), clientRectangle);
      this.ElementFactory.ClearElements();
      this.Symbology.CreateElements(this.ElementFactory, Rectangle.Round(clientRectangle));
      return sizeF;
    }

    public override void DpiScaleChanged(SizeF scaleFactor)
    {
      base.DpiScaleChanged(scaleFactor);
      Symbology1D symbology1 = this.Symbology as Symbology1D;
      if (symbology1 != null)
        symbology1.Module = (int) Math.Round((double) symbology1.Module * ((double) this.DpiScaleFactor.Width / (double) this.previousDpi.Width));
      PDF417 symbology2 = this.Symbology as PDF417;
      if (symbology2 != null)
        symbology2.Module = (int) Math.Round((double) symbology2.Module * ((double) this.DpiScaleFactor.Width / (double) this.previousDpi.Width), MidpointRounding.AwayFromZero);
      QRCode symbology3 = this.Symbology as QRCode;
      if (symbology3 != null)
        symbology3.Module = (int) Math.Round((double) symbology3.Module * ((double) this.DpiScaleFactor.Width / (double) this.previousDpi.Width), MidpointRounding.AwayFromZero);
      this.previousDpi = scaleFactor;
    }

    public Image ExportToImage()
    {
      return this.ExportToImage(this.Bounds.Width, this.Bounds.Height);
    }

    public Image ExportToImage(int width, int height)
    {
      Rectangle bounds = new Rectangle(0, 0, width, height);
      Bitmap bitmap = new Bitmap(width, height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        graphics.Clear(this.BackColor);
        this.MeasureSymbology((SizeF) bounds.Size);
        (this.Symbology as Symbology1D)?.ArrangeContent((IMeasureContext) new MeasureContext(this.GetScaledFont(this.DpiScaleFactor.Height)), (RectangleF) bounds);
        BaseBarcodeElementFactory barcodeElementFactory = new BaseBarcodeElementFactory(this);
        this.Symbology.CreateElements((IElementFactory) barcodeElementFactory, bounds);
        this.PaintElements((IGraphics) new RadGdiGraphics(graphics), this.Symbology, (IElementFactory) barcodeElementFactory);
      }
      return (Image) bitmap;
    }

    public void ExportToImage(Stream stream, Size size)
    {
      this.ExportToImage(stream, size, ImageFormat.Png);
    }

    public void ExportToImage(string filePath, Size size)
    {
      using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        this.ExportToImage((Stream) fileStream, size, ImageFormat.Png);
    }

    public void ExportToImage(string filePath, Size size, ImageFormat imageFormat)
    {
      using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        this.ExportToImage((Stream) fileStream, size, imageFormat);
    }

    public void ExportToImage(Stream stream, Size size, ImageFormat imageFormat)
    {
      this.ExportToImage(size.Width, size.Height).Save(stream, imageFormat);
    }

    private void Symbology_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.OnSymbologyPropertyChanged(sender, e);
    }

    protected virtual void OnSymbologyPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      this.Update();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (!(e.Property.Name == "ForeColor"))
        return;
      this.Update();
    }
  }
}
