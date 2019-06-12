// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadBarcode
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using Telerik.Licensing;
using Telerik.WinControls.Design;
using Telerik.WinControls.UI.Barcode.Symbology;

namespace Telerik.WinControls.UI
{
  [DefaultProperty("Value")]
  [LicenseProvider(typeof (TelerikLicenseProvider))]
  [TelerikToolboxCategory("Data Controls")]
  [ToolboxItem(true)]
  [Designer("Telerik.WinControls.UI.Design.RadBarcodeDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  [Description("Generates and displays a barcode using a provided encoding method (symbology) and a value.")]
  public class RadBarcode : RadControl
  {
    private RadBarcodeElement barcodeElement;

    protected override void CreateChildItems(RadElement parent)
    {
      base.CreateChildItems(parent);
      this.barcodeElement = this.CreateBarcodeElement();
      parent.Children.Add((RadElement) this.barcodeElement);
    }

    protected virtual RadBarcodeElement CreateBarcodeElement()
    {
      return new RadBarcodeElement();
    }

    public RadBarcodeElement BarcodeElement
    {
      get
      {
        return this.barcodeElement;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return new Size(203, 203);
      }
    }

    [DefaultValue(typeof (Color), "255, 0, 0, 0")]
    public override Color ForeColor
    {
      get
      {
        return this.BarcodeElement.ForeColor;
      }
      set
      {
        this.BarcodeElement.ForeColor = value;
      }
    }

    [DefaultValue(typeof (Color), "255, 255, 255, 255")]
    public override Color BackColor
    {
      get
      {
        return this.BarcodeElement.BackColor;
      }
      set
      {
        this.BarcodeElement.BackColor = value;
      }
    }

    [DefaultValue(null)]
    public string Value
    {
      get
      {
        return this.BarcodeElement.Value;
      }
      set
      {
        this.BarcodeElement.Value = value;
      }
    }

    [DefaultValue(null)]
    [Editor("Telerik.WinControls.UI.Design.BarcodeSymbologyEditor, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e", typeof (UITypeEditor))]
    [TypeConverter(typeof (BarcodeSymbologyConverter))]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public ISymbology Symbology
    {
      get
      {
        return this.BarcodeElement.Symbology;
      }
      set
      {
        this.BarcodeElement.Symbology = value;
      }
    }

    public Image ExportToImage()
    {
      return this.BarcodeElement.ExportToImage();
    }

    public Image ExportToImage(int width, int height)
    {
      return this.BarcodeElement.ExportToImage(width, height);
    }

    public void ExportToImage(Stream stream, Size size)
    {
      this.BarcodeElement.ExportToImage(stream, size);
    }

    public void ExportToImage(string filePath, Size size)
    {
      this.BarcodeElement.ExportToImage(filePath, size);
    }

    public void ExportToImage(Stream stream, Size size, ImageFormat imageFormat)
    {
      this.BarcodeElement.ExportToImage(stream, size, imageFormat);
    }

    public void ExportToImage(string filePath, Size size, ImageFormat imageFormat)
    {
      this.BarcodeElement.ExportToImage(filePath, size, imageFormat);
    }

    [Description("Occurs after the value of the barcode is chnaged.")]
    [Category("Action")]
    public event EventHandler ValueChanged
    {
      add
      {
        this.BarcodeElement.ValueChanged += value;
      }
      remove
      {
        this.BarcodeElement.ValueChanged -= value;
      }
    }

    [Description("Occurs before the value of the barcode is chnaged.")]
    [Category("Action")]
    public event BarcodeValueChangingEventHandler ValueChanging
    {
      add
      {
        this.BarcodeElement.ValueChanging += value;
      }
      remove
      {
        this.BarcodeElement.ValueChanging -= value;
      }
    }

    [Description("Occurs after the symbology of the barcode is chnaged.")]
    [Category("Action")]
    public event EventHandler SymbologyChanged
    {
      add
      {
        this.BarcodeElement.SymbologyChanged += value;
      }
      remove
      {
        this.BarcodeElement.SymbologyChanged -= value;
      }
    }

    [Description("Occurs before the symbology of the barcode is chnaged.")]
    [Category("Action")]
    public event BarcodeSymbologyChangingEventHandler SymbologyChanging
    {
      add
      {
        this.BarcodeElement.SymbologyChanging += value;
      }
      remove
      {
        this.BarcodeElement.SymbologyChanging -= value;
      }
    }
  }
}
