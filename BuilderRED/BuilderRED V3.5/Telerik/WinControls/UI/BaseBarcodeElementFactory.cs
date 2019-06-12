// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseBarcodeElementFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class BaseBarcodeElementFactory : IElementFactory
  {
    private RadBarcodeElement barcodeElement;
    private List<BarcodeElementBase> elements;
    private RectangleF elementsBounds;

    public BaseBarcodeElementFactory(RadBarcodeElement barcodeElement)
    {
      this.elements = new List<BarcodeElementBase>();
      this.barcodeElement = barcodeElement;
    }

    public RadBarcodeElement BarcodeElement
    {
      get
      {
        return this.barcodeElement;
      }
    }

    public ReadOnlyCollection<BarcodeElementBase> Elements
    {
      get
      {
        return new ReadOnlyCollection<BarcodeElementBase>((IList<BarcodeElementBase>) this.elements);
      }
    }

    public RectangleF ElementsBounds
    {
      get
      {
        return this.elementsBounds;
      }
    }

    public void ClearElements()
    {
      this.elementsBounds = (RectangleF) Rectangle.Empty;
      this.elements.Clear();
    }

    public void CreateBarElement(RectangleF rect)
    {
      this.elements.Add((BarcodeElementBase) new BarElement(rect, this.BarcodeElement.ForeColor));
      this.elementsBounds = RectangleF.Union(this.elementsBounds, rect);
    }

    public void CreateTextElement(string text, RectangleF rect)
    {
      this.elementsBounds = RectangleF.Union(this.elementsBounds, rect);
      this.elements.Add((BarcodeElementBase) new TextElement(rect, text, this.BarcodeElement.ForeColor)
      {
        Font = this.BarcodeElement.GetScaledFont(this.BarcodeElement.DpiScaleFactor.Height)
      });
    }
  }
}
