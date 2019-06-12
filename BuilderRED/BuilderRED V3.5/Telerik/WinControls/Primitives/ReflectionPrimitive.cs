// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Primitives.ReflectionPrimitive
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Telerik.WinControls.Paint;

namespace Telerik.WinControls.Primitives
{
  public class ReflectionPrimitive : BasePrimitive, IDisposable
  {
    private RadElement ownerElement;
    private Bitmap reflectionBitmap;
    private bool reflectionSourceBitmapChanged;
    private Bitmap reflectionSourceBitmap;
    private double itemReflectionPercentage;

    public ReflectionPrimitive(RadElement ownerElement)
    {
      this.OwnerElement = ownerElement;
    }

    public static Bitmap CopyBitmap(Bitmap srcBitmap, Rectangle section)
    {
      Bitmap bitmap = new Bitmap(section.Width, section.Height);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      if (srcBitmap == null)
        srcBitmap = new Bitmap(section.Width, section.Height);
      graphics.DrawImage((Image) srcBitmap, 0, 0, section, GraphicsUnit.Pixel);
      graphics.Dispose();
      return bitmap;
    }

    protected override void PaintElement(IGraphics graphics, float angle, SizeF scale)
    {
      base.PaintElement(graphics, angle, scale);
      if (this.ownerElement == null)
        return;
      if (this.reflectionSourceBitmapChanged)
      {
        if (this.reflectionSourceBitmap == null || (int) Math.Round((double) this.reflectionSourceBitmap.Height * this.itemReflectionPercentage) == 0)
          return;
        this.reflectionSourceBitmap = this.ownerElement.GetAsTransformedBitmap(Brushes.Transparent, this.OwnerElement.AngleTransform, this.OwnerElement.ScaleTransform);
        if (this.reflectionSourceBitmap == null)
          return;
        this.reflectionSourceBitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
        if (this.reflectionBitmap != null)
        {
          this.reflectionBitmap.Dispose();
          this.reflectionBitmap = (Bitmap) null;
        }
        Rectangle boundingRectangle = this.ownerElement.ControlBoundingRectangle;
        Size size = new Size(Math.Max(1, boundingRectangle.Size.Width), Math.Max(1, (int) Math.Round((double) boundingRectangle.Size.Height * this.ItemReflectionPercentage)));
        if (size.Width == 0 || size.Height == 0)
          return;
        this.reflectionBitmap = ReflectionPrimitive.CopyBitmap(this.reflectionSourceBitmap, this.ownerElement.Shape == null ? new Rectangle(0, 0, size.Width, size.Height) : new Rectangle(1, 1, size.Width - 1, size.Height - 1));
        ReflectionPrimitive.ApplyReflectionGradientFade(this.reflectionBitmap, 1.0);
        this.UpdateReflectionImage(this.reflectionBitmap);
        this.reflectionSourceBitmapChanged = false;
      }
      if (this.reflectionBitmap == null)
        return;
      graphics.DrawBitmap((Image) this.reflectionBitmap, 0, 0);
    }

    private static void ApplyReflectionGradientFade(Bitmap bitmap, double initialOpacity)
    {
      Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
      BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
      float num = 130f * (float) initialOpacity;
      try
      {
        for (int index1 = 0; index1 <= bitmapdata.Height - 1; ++index1)
        {
          for (int index2 = 0; index2 <= bitmapdata.Width - 1; ++index2)
          {
            Color baseColor = Color.FromArgb(Marshal.ReadInt32(bitmapdata.Scan0, bitmapdata.Stride * index1 + 4 * index2));
            int alpha = (int) ((double) baseColor.A / (double) byte.MaxValue * ((double) num - (double) num * ((double) index1 / (double) rect.Height)));
            Marshal.WriteInt32(bitmapdata.Scan0, bitmapdata.Stride * index1 + 4 * index2, Color.FromArgb(alpha, baseColor).ToArgb());
          }
        }
      }
      finally
      {
        bitmap.UnlockBits(bitmapdata);
      }
    }

    public void UpdateReflectionImage(Bitmap itemBitmap)
    {
      this.reflectionSourceBitmapChanged = true;
      if (this.reflectionSourceBitmap != null)
      {
        this.reflectionSourceBitmap.Dispose();
        this.reflectionSourceBitmap = (Bitmap) null;
      }
      this.reflectionSourceBitmap = itemBitmap;
    }

    protected override void DisposeManagedResources()
    {
      if (this.reflectionBitmap != null)
        this.reflectionBitmap.Dispose();
      if (this.reflectionSourceBitmap != null)
        this.reflectionSourceBitmap.Dispose();
      if (this.ownerElement != null)
        this.ownerElement.ElementPainted -= new PaintEventHandler(this.HostedItem_ElementPainted);
      base.DisposeManagedResources();
    }

    protected override void OnBeginDispose()
    {
      base.OnBeginDispose();
      if (this.ElementTree == null)
        return;
      ((RadControl) this.ElementTree.Control).ElementInvalidated -= new EventHandler(this.CarouselContentItem_ElementInvalidated);
    }

    protected override void OnElementTreeChanged(ComponentThemableElementTree previousTree)
    {
      base.OnElementTreeChanged(previousTree);
      if (previousTree != null)
        ((RadControl) previousTree.Control).ElementInvalidated -= new EventHandler(this.CarouselContentItem_ElementInvalidated);
      if (this.ElementTree == null)
        return;
      ((RadControl) this.ElementTree.Control).ElementInvalidated += new EventHandler(this.CarouselContentItem_ElementInvalidated);
    }

    public double ItemReflectionPercentage
    {
      get
      {
        return this.itemReflectionPercentage;
      }
      set
      {
        this.itemReflectionPercentage = value;
      }
    }

    public void CarouselContentItem_ElementInvalidated(object sender, EventArgs e)
    {
      if (this.OwnerElement == null)
        return;
      for (RadElement radElement = sender as RadElement; radElement != null; radElement = radElement.Parent)
      {
        if (radElement == this.OwnerElement)
        {
          this.Invalidate();
          break;
        }
      }
    }

    public RadElement OwnerElement
    {
      get
      {
        return this.ownerElement;
      }
      set
      {
        if (this.ownerElement != null)
          this.ownerElement.ElementPainted -= new PaintEventHandler(this.HostedItem_ElementPainted);
        if (this.ownerElement == value)
          return;
        this.ownerElement = value;
        if (this.ownerElement == null)
          return;
        this.ownerElement.ElementPainted += new PaintEventHandler(this.HostedItem_ElementPainted);
      }
    }

    private void HostedItem_ElementPainted(object sender, PaintEventArgs e)
    {
      Rectangle boundingRectangle = this.ownerElement.ControlBoundingRectangle;
      Size size = new Size(Math.Max(1, boundingRectangle.Size.Width), Math.Max(1, (int) Math.Round((double) boundingRectangle.Height * this.ItemReflectionPercentage)));
      this.UpdateReflectionImage(new Bitmap(size.Width, size.Height));
    }
  }
}
