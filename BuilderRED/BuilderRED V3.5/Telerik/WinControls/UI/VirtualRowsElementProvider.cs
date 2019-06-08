// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualRowsElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class VirtualRowsElementProvider : IVirtualizedElementProvider<int>
  {
    private List<VirtualGridRowElement> cachedRows = new List<VirtualGridRowElement>();
    private VirtualGridTableElement owner;

    public VirtualRowsElementProvider(VirtualGridTableElement owner)
    {
      this.owner = owner;
    }

    public VirtualGridRowElement CreateElement(
      int data,
      Type rowType,
      object context)
    {
      VirtualGridCreateRowEventArgs e = new VirtualGridCreateRowEventArgs(data, rowType, this.owner.ViewInfo);
      this.owner.GridElement.OnCreateRowElement(e);
      if (e.RowElement != null)
        return e.RowElement;
      if ((object) e.RowType != null)
        return (VirtualGridRowElement) Activator.CreateInstance(e.RowType);
      return (VirtualGridRowElement) null;
    }

    public IVirtualizedElement<int> GetElement(int data, object context)
    {
      bool flag = this.owner.IsRowExpanded(data);
      IVirtualizedElement<int> virtualizedElement = (IVirtualizedElement<int>) null;
      for (int index = this.cachedRows.Count - 1; index >= 0; --index)
      {
        if ((!flag && !this.cachedRows[index].IsChildViewInitialized || flag && this.cachedRows[index].IsChildViewInitialized) && this.cachedRows[index].IsCompatible(data, context))
        {
          virtualizedElement = (IVirtualizedElement<int>) this.cachedRows[index];
          this.cachedRows.RemoveAt(index);
          break;
        }
      }
      if (virtualizedElement == null && this.cachedRows.Count > 10)
      {
        for (int index = this.cachedRows.Count - 1; index >= 0; --index)
        {
          if (this.cachedRows[index].IsCompatible(data, context))
          {
            virtualizedElement = (IVirtualizedElement<int>) this.cachedRows[index];
            this.cachedRows.RemoveAt(index);
            break;
          }
        }
      }
      if (virtualizedElement == null)
      {
        VirtualGridRowElement element;
        switch (data)
        {
          case -3:
            element = this.CreateElement(data, typeof (VirtualGridFilterRowElement), context);
            element.Initialize(this.owner);
            element.Attach(-3, context);
            break;
          case -2:
            element = this.CreateElement(data, typeof (VirtualGridNewRowElement), context);
            element.Initialize(this.owner);
            element.Attach(-2, context);
            break;
          case -1:
            element = this.CreateElement(data, typeof (VirtualGridHeaderRowElement), context);
            element.Initialize(this.owner);
            element.Attach(-1, context);
            break;
          default:
            element = this.CreateElement(data, typeof (VirtualGridRowElement), context);
            element.Initialize(this.owner);
            break;
        }
        return (IVirtualizedElement<int>) element;
      }
      virtualizedElement.Attach(data, context);
      return virtualizedElement;
    }

    public bool CacheElement(IVirtualizedElement<int> element)
    {
      VirtualGridRowElement virtualGridRowElement = element as VirtualGridRowElement;
      if (virtualGridRowElement == null || virtualGridRowElement.IsDisposed || (virtualGridRowElement.IsDisposing || this.cachedRows.Contains(virtualGridRowElement)))
        return false;
      this.cachedRows.Add(virtualGridRowElement);
      return true;
    }

    public bool ShouldUpdate(IVirtualizedElement<int> element, int data, object context)
    {
      return element.Data != data;
    }

    public bool IsCompatible(IVirtualizedElement<int> element, int data, object context)
    {
      return element.IsCompatible(data, context);
    }

    public SizeF GetElementSize(int data)
    {
      return new SizeF(0.0f, (float) this.owner.GetRowHeight(data));
    }

    public SizeF GetElementSize(IVirtualizedElement<int> element)
    {
      return this.GetElementSize(element.Data);
    }

    public SizeF DefaultElementSize
    {
      get
      {
        return new SizeF(0.0f, (float) this.owner.RowHeight);
      }
      set
      {
      }
    }

    public void ClearCache()
    {
      this.cachedRows.Clear();
    }

    public bool TryGetElementWithChildView(
      int data,
      object context,
      out IVirtualizedElement<int> element)
    {
      for (int index = this.cachedRows.Count - 1; index >= 0; --index)
      {
        if (this.cachedRows[index].IsChildViewInitialized && this.cachedRows[index].IsCompatible(data, context))
        {
          element = (IVirtualizedElement<int>) this.cachedRows[index];
          this.cachedRows.RemoveAt(index);
          element.Attach(data, context);
          return true;
        }
      }
      element = (IVirtualizedElement<int>) null;
      return false;
    }

    public bool TryGetElementWithoutChildView(
      int data,
      object context,
      out IVirtualizedElement<int> element)
    {
      for (int index = this.cachedRows.Count - 1; index >= 0; --index)
      {
        if (!this.cachedRows[index].IsChildViewInitialized && this.cachedRows[index].IsCompatible(data, context))
        {
          element = (IVirtualizedElement<int>) this.cachedRows[index];
          this.cachedRows.RemoveAt(index);
          element.Attach(data, context);
          return true;
        }
      }
      element = (IVirtualizedElement<int>) null;
      return false;
    }
  }
}
