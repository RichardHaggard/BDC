// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PageViewItemSizeInfo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  internal class PageViewItemSizeInfo
  {
    public RadPageViewItem item;
    public bool verticalOrientation;
    public bool verticalAlign;
    public SizeF desiredSize;
    public SizeF minSize;
    public SizeF maxSize;
    public SizeF layoutSize;
    public SizeF currentSize;
    public RectangleF itemRectangle;
    public float desiredLength;
    public float minLength;
    public float maxLength;
    public float layoutLength;
    public float currentLength;
    public int marginLength;
    public int itemIndex;

    public PageViewItemSizeInfo(RadPageViewItem item, bool vertical)
    {
      this.item = item;
      this.verticalAlign = vertical;
      this.verticalOrientation = item.ContentOrientation == PageViewContentOrientation.Vertical90 || item.ContentOrientation == PageViewContentOrientation.Vertical270;
      this.desiredSize = item.DesiredSize;
      this.layoutSize = this.desiredSize;
      this.minSize = (SizeF) item.MinSize;
      this.maxSize = (SizeF) item.MaxSize;
      this.currentSize = item.CurrentSize;
      if (this.currentSize == (SizeF) Size.Empty)
        this.currentSize = this.desiredSize;
      this.desiredLength = this.GetLength(this.desiredSize);
      this.currentLength = this.GetLength(this.currentSize);
      this.layoutLength = this.desiredLength;
      this.minLength = this.verticalOrientation ? this.minSize.Height : this.minSize.Width;
      this.maxLength = this.verticalOrientation ? this.maxSize.Height : this.maxSize.Width;
      this.marginLength = this.GetItemMarginLength(item);
    }

    protected virtual int GetItemMarginLength(RadPageViewItem item)
    {
      if (!this.verticalAlign)
        return item.Margin.Horizontal;
      if (!item.AutoFlipMargin)
        return item.Margin.Vertical;
      return item.Margin.Horizontal;
    }

    public void SetLayoutSize(SizeF newSize)
    {
      this.layoutSize = newSize;
      this.layoutLength = this.GetLength(newSize);
    }

    public void SetCurrentSize(SizeF newSize)
    {
      this.currentSize = newSize;
      this.currentLength = this.GetLength(newSize);
    }

    public void ChangeLayoutLength(int newLength)
    {
      this.layoutLength = (float) newLength;
      if (this.verticalAlign)
        this.layoutSize.Height = (float) newLength;
      else
        this.layoutSize.Width = (float) newLength;
    }

    private float GetLength(SizeF size)
    {
      if (!this.verticalAlign)
        return size.Width;
      return size.Height;
    }
  }
}
