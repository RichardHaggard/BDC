// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.Gauges.LinearGaugeSingleLabel
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.UI.Gauges
{
  [Description("Present additional information for the RadLinearGauge, e.g. current value")]
  [Designer("Telerik.WinControls.UI.Design.LinearSingleLabelDesigner, Telerik.WinControls.UI.Design, Version=2018.3.1016.20, Culture=neutral, PublicKeyToken=5bb2a467cbec794e")]
  public class LinearGaugeSingleLabel : GaugeVisualElement, IDrawing
  {
    private float labelFontSize = 8f;
    private string labelFormat = "#,##0.#";
    private SizeF locationPercentage = new SizeF(0.0f, 0.0f);
    private string labelText = "Text";
    private RadLinearGaugeElement owner;
    private bool bindValue;

    public string LabelText
    {
      get
      {
        return this.labelText;
      }
      set
      {
        if (value == this.labelText)
          return;
        this.labelText = value;
        this.OnNotifyPropertyChanged(nameof (LabelText));
      }
    }

    [DefaultValue(false)]
    [Description("Indicates whether the single label's text is bound to the gauge's Value.")]
    public bool BindValue
    {
      get
      {
        return this.bindValue;
      }
      set
      {
        this.bindValue = value;
      }
    }

    [Description("Controls the label's location (x, y) according to the center point. LocationPercentage accepts values withing the range [(-1,-1), (1,1)].")]
    [DefaultValue(typeof (SizeF), "0,0")]
    public SizeF LocationPercentage
    {
      get
      {
        return this.locationPercentage;
      }
      set
      {
        if (value == this.locationPercentage)
          return;
        this.locationPercentage = value;
        this.OnNotifyPropertyChanged(nameof (LocationPercentage));
      }
    }

    [Description("Specifies the label size.")]
    [DefaultValue(8f)]
    public float LabelFontSize
    {
      get
      {
        return this.labelFontSize;
      }
      set
      {
        if ((double) value == (double) this.labelFontSize)
          return;
        this.labelFontSize = value;
        this.OnNotifyPropertyChanged(nameof (LabelFontSize));
      }
    }

    [DefaultValue("#,##0.#")]
    [Description("Specifies the label format. By default, it is set to #,##0.#.")]
    public string LabelFormat
    {
      get
      {
        return this.labelFormat;
      }
      set
      {
        if (value == this.labelFormat)
          return;
        this.labelFormat = value;
        this.OnNotifyPropertyChanged(nameof (LabelFormat));
      }
    }

    public override GraphicsPath Path
    {
      get
      {
        GraphicsPath graphicsPath = new GraphicsPath();
        float emSize = this.Font.Size;
        if (this.AutoSize)
          emSize = this.owner.GaugeOtherSizeLength * (this.labelFontSize / 100f);
        using (Font font = new Font(this.Font.FontFamily, emSize, this.Font.Style))
        {
          using (StringFormat stringFormat = new StringFormat())
          {
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            string labelText = this.labelText;
            if (this.BindValue)
              labelText = this.owner.Value.ToString(this.labelFormat);
            PointF pt = new PointF(this.owner.GaugeCenter.X + (float) ((double) this.LocationPercentage.Width * (double) this.owner.GaugeSize.Width / 2.0), this.owner.GaugeCenter.Y + (float) ((double) this.locationPercentage.Height * (double) this.owner.GaugeLength / 2.0));
            if (this.owner.Vertical)
              pt = new PointF(this.owner.GaugeCenter.Y + (float) ((double) this.locationPercentage.Height * (double) this.owner.GaugeSize.Width / 2.0), this.owner.GaugeCenter.X + (float) ((double) this.LocationPercentage.Height * (double) this.owner.GaugeLength / 2.0));
            SizeF size = (SizeF) TextRenderer.MeasureText(labelText, font);
            RectangleF rect = new RectangleF(PointF.Subtract(pt, new SizeF(size.Width / 2f, size.Height / 2f)), size);
            graphicsPath.StartFigure();
            graphicsPath.AddRectangle(rect);
          }
        }
        return graphicsPath;
      }
    }

    public void Paint(Graphics graphics, Rectangle boundingRectangle)
    {
      float emSize = this.Font.Size;
      if (this.AutoSize)
        emSize = this.owner.GaugeOtherSizeLength * (this.labelFontSize / 100f);
      if ((double) emSize <= 0.0)
        return;
      using (Font font = new Font(this.Font.FontFamily, emSize, this.Font.Style))
      {
        using (StringFormat format = new StringFormat())
        {
          format.Alignment = StringAlignment.Center;
          format.LineAlignment = StringAlignment.Center;
          string labelText = this.labelText;
          if (this.BindValue)
            labelText = this.owner.Value.ToString(this.labelFormat);
          PointF point = new PointF(this.owner.GaugeCenter.X + (float) ((double) this.LocationPercentage.Width * (double) this.owner.GaugeSize.Width / 2.0), this.owner.GaugeCenter.Y + (float) ((double) this.locationPercentage.Height * (double) this.owner.GaugeLength / 2.0));
          if (this.owner.Vertical)
            point = new PointF(this.owner.GaugeCenter.Y + (float) ((double) this.locationPercentage.Height * (double) this.owner.GaugeSize.Width / 2.0), this.owner.GaugeCenter.X + (float) ((double) this.LocationPercentage.Height * (double) this.owner.GaugeLength / 2.0));
          using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
            graphics.DrawString(labelText, font, (Brush) solidBrush, point, format);
        }
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawText = false;
    }

    protected override void OnNotifyPropertyChanged(string propertyName)
    {
      base.OnNotifyPropertyChanged(propertyName);
      if (propertyName != "Shape")
        this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
      this.Invalidate();
    }

    protected override void OnParentChanged(RadElement previousParent)
    {
      base.OnParentChanged(previousParent);
      if (this.Parent == null)
        return;
      this.owner = (RadLinearGaugeElement) this.Parent;
      this.owner.ValueChanged -= new EventHandler(this.owner_ValueChanged);
      this.owner.ValueChanged += new EventHandler(this.owner_ValueChanged);
      this.Shape = (ElementShape) new GaugeVisualElement.GaugeShape((GaugeVisualElement) this);
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      this.Paint(((RadGdiGraphics) graphics).Graphics, this.BoundingRectangle);
    }

    private void owner_ValueChanged(object sender, EventArgs e)
    {
      this.Invalidate();
    }
  }
}
