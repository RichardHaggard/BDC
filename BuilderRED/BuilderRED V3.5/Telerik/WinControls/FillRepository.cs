// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.FillRepository
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls
{
  public class FillRepository : IDisposable
  {
    public Dictionary<Size, Dictionary<int, Bitmap>> PaintBuffers;
    private bool disableBitmapCache;

    internal FillRepository()
    {
      this.PaintBuffers = new Dictionary<Size, Dictionary<int, Bitmap>>();
    }

    public bool DisableBitmapCache
    {
      get
      {
        return this.disableBitmapCache;
      }
      set
      {
        this.disableBitmapCache = value;
      }
    }

    public Dictionary<int, Bitmap> GetBuffersBySize(Size size)
    {
      if (this.DisableBitmapCache)
        return (Dictionary<int, Bitmap>) null;
      if (!this.PaintBuffers.ContainsKey(size))
        return (Dictionary<int, Bitmap>) null;
      return this.PaintBuffers[size];
    }

    public Bitmap GetBitmapBySizeAndHash(Size size, int hash)
    {
      if (this.DisableBitmapCache)
        return (Bitmap) null;
      Dictionary<int, Bitmap> buffersBySize = this.GetBuffersBySize(size);
      if (buffersBySize == null)
        return (Bitmap) null;
      Bitmap bitmap;
      buffersBySize.TryGetValue(hash, out bitmap);
      return bitmap;
    }

    public void RemoveBitmapsBySize(Size size)
    {
      if (size == Size.Empty)
        return;
      Dictionary<int, Bitmap> buffersBySize = this.GetBuffersBySize(size);
      if (buffersBySize == null)
        return;
      foreach (KeyValuePair<int, Bitmap> keyValuePair in buffersBySize)
        keyValuePair.Value.Dispose();
      this.PaintBuffers.Remove(size);
    }

    public void AddNewBitmap(Size size, int hash, Bitmap bitmap)
    {
      if (this.DisableBitmapCache || size == Size.Empty)
        return;
      Dictionary<int, Bitmap> dictionary;
      this.PaintBuffers.TryGetValue(size, out dictionary);
      if (dictionary == null)
      {
        dictionary = new Dictionary<int, Bitmap>();
        this.PaintBuffers.Add(size, dictionary);
      }
      dictionary[hash] = bitmap;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      foreach (KeyValuePair<Size, Dictionary<int, Bitmap>> paintBuffer in this.PaintBuffers)
      {
        foreach (KeyValuePair<int, Bitmap> keyValuePair in paintBuffer.Value)
          keyValuePair.Value.Dispose();
      }
      this.PaintBuffers.Clear();
    }

    ~FillRepository()
    {
      this.Dispose(false);
    }
  }
}
