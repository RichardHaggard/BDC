// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseVirtualizedElementProvider`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public abstract class BaseVirtualizedElementProvider<T> : IVirtualizedElementProvider<T>
  {
    private SizeF elementSize = new SizeF(20f, 20f);
    private List<IVirtualizedElement<T>> cachedElements = new List<IVirtualizedElement<T>>();

    public SizeF DefaultElementSize
    {
      get
      {
        return this.elementSize;
      }
      set
      {
        this.elementSize = value;
      }
    }

    public abstract IVirtualizedElement<T> CreateElement(T data, object context);

    public virtual IVirtualizedElement<T> GetElementFromCache(
      T data,
      object context)
    {
      foreach (IVirtualizedElement<T> element in new List<IVirtualizedElement<T>>((IEnumerable<IVirtualizedElement<T>>) this.cachedElements))
      {
        DisposableObject disposableObject = element as DisposableObject;
        if (disposableObject != null && (disposableObject.IsDisposed || disposableObject.IsDisposing))
          this.cachedElements.Remove(element);
        else if (this.IsCompatible(element, data, context))
        {
          this.cachedElements.Remove(element);
          this.PreInitializeCachedElement(element, context);
          return element;
        }
      }
      return (IVirtualizedElement<T>) null;
    }

    public virtual IVirtualizedElement<T> GetElement(T data, object context)
    {
      return this.GetElementFromCache(data, context) ?? this.CreateElement(data, context);
    }

    protected virtual void PreInitializeCachedElement(
      IVirtualizedElement<T> element,
      object context)
    {
    }

    public virtual bool CacheElement(IVirtualizedElement<T> element)
    {
      this.cachedElements.Add(element);
      return true;
    }

    public virtual bool ShouldUpdate(IVirtualizedElement<T> element, T data, object context)
    {
      if (element.Data.Equals((object) data))
        return !this.IsCompatible(element, data, context);
      return true;
    }

    public virtual bool IsCompatible(IVirtualizedElement<T> element, T data, object context)
    {
      return element.IsCompatible(data, context);
    }

    public virtual SizeF GetElementSize(T item)
    {
      return this.elementSize;
    }

    public virtual SizeF GetElementSize(IVirtualizedElement<T> element)
    {
      return this.GetElementSize(element.Data);
    }

    public virtual void ClearCache()
    {
      foreach (IVirtualizedElement<T> cachedElement in this.cachedElements)
        (cachedElement as IDisposable)?.Dispose();
      this.cachedElements.Clear();
    }

    public virtual int CachedElementsCount
    {
      get
      {
        return this.cachedElements.Count;
      }
    }
  }
}
