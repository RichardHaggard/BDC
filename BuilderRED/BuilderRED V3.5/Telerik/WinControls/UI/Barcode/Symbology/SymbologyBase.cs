// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Barcode.Symbology.SymbologyBase
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI.Barcode.Symbology
{
  public abstract class SymbologyBase : ISymbology, INotifyPropertyChanged
  {
    private StringAlignment textAlign = StringAlignment.Center;
    private StringAlignment lineAlign = StringAlignment.Far;
    private int module = 1;
    private bool showText;
    private bool stretch;
    private bool checksum;
    private string value;

    [DefaultValue(StringAlignment.Center)]
    public StringAlignment TextAlign
    {
      get
      {
        return this.textAlign;
      }
      set
      {
        if (this.textAlign == value)
          return;
        this.textAlign = value;
        this.OnPropertyChanged(nameof (TextAlign));
      }
    }

    [DefaultValue(StringAlignment.Far)]
    public StringAlignment LineAlign
    {
      get
      {
        return this.lineAlign;
      }
      set
      {
        if (this.lineAlign == value)
          return;
        this.lineAlign = value;
        this.OnPropertyChanged(nameof (LineAlign));
      }
    }

    [DefaultValue(false)]
    public bool ShowText
    {
      get
      {
        return this.showText;
      }
      set
      {
        if (this.showText == value)
          return;
        this.showText = value;
        this.OnPropertyChanged(nameof (ShowText));
      }
    }

    [DefaultValue(false)]
    public bool Stretch
    {
      get
      {
        return this.stretch;
      }
      set
      {
        if (this.stretch == value)
          return;
        this.stretch = value;
        this.OnPropertyChanged(nameof (Stretch));
      }
    }

    [DefaultValue(false)]
    public bool Checksum
    {
      get
      {
        return this.checksum;
      }
      set
      {
        if (this.checksum == value)
          return;
        this.checksum = value;
        this.OnPropertyChanged(nameof (Checksum));
      }
    }

    [DefaultValue(1)]
    public int Module
    {
      get
      {
        return this.module;
      }
      set
      {
        if (this.module == value)
          return;
        this.module = value;
        this.OnPropertyChanged(nameof (Module));
      }
    }

    [DefaultValue(null)]
    public string Value
    {
      get
      {
        return this.value;
      }
    }

    public void CreateElements(IElementFactory factory, Rectangle bounds)
    {
      this.CreateBarsOverride(factory);
      if (!this.ShowText)
        return;
      this.CreateTextElementsOverride(factory);
    }

    protected abstract void CreateBarsOverride(IElementFactory factory);

    protected abstract void CreateTextElementsOverride(IElementFactory factory);

    public virtual void ProcessValue(string value)
    {
      this.value = value;
      this.ValidateValue(value);
    }

    protected virtual void ValidateValue(string value)
    {
    }

    public virtual SizeF MeasureContent(IMeasureContext context, SizeF size)
    {
      return size;
    }

    public virtual void ArrangeContent(IMeasureContext context, RectangleF bounds)
    {
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }
  }
}
