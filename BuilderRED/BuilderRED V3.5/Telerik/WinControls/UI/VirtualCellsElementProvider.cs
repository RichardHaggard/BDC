// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualCellsElementProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class VirtualCellsElementProvider : IVirtualizedElementProvider<int>
  {
    private List<VirtualGridCellElement> cachedCells = new List<VirtualGridCellElement>();
    private VirtualGridTableElement owner;

    public VirtualCellsElementProvider(VirtualGridTableElement owner)
    {
      this.owner = owner;
    }

    public VirtualGridCellElement CreateElement(
      int data,
      Type cellType,
      object context)
    {
      VirtualGridCreateCellEventArgs e = new VirtualGridCreateCellEventArgs(data, ((VirtualGridRowElement) context).Data, cellType, this.owner.ViewInfo);
      this.owner.GridElement.OnCreateCellElement(e);
      if (e.CellElement != null)
        return e.CellElement;
      if ((object) e.CellType != null)
        return (VirtualGridCellElement) Activator.CreateInstance(e.CellType);
      return (VirtualGridCellElement) null;
    }

    public IVirtualizedElement<int> GetElement(int data, object context)
    {
      VirtualGridCellElement virtualGridCellElement1 = (VirtualGridCellElement) null;
      for (int index = this.cachedCells.Count - 1; index >= 0; --index)
      {
        if (this.cachedCells[index].IsCompatible(data, context))
        {
          virtualGridCellElement1 = this.cachedCells[index];
          this.cachedCells.RemoveAt(index);
          break;
        }
      }
      if (virtualGridCellElement1 == null)
      {
        VirtualGridCellElement virtualGridCellElement2 = data != -1 ? (!(context is VirtualGridHeaderRowElement) ? (!(context is VirtualGridFilterRowElement) ? (!(context is VirtualGridNewRowElement) ? this.CreateElement(data, typeof (VirtualGridCellElement), context) : this.CreateElement(data, typeof (VirtualGridNewCellElement), context)) : this.CreateElement(data, typeof (VirtualGridFilterCellElement), context)) : this.CreateElement(data, typeof (VirtualGridHeaderCellElement), context)) : this.CreateElement(data, typeof (VirtualGridIndentCellElement), context);
        virtualGridCellElement2.Initialize(this.owner);
        virtualGridCellElement2.Attach(data, context);
        return (IVirtualizedElement<int>) virtualGridCellElement2;
      }
      virtualGridCellElement1.Attach(data, context);
      return (IVirtualizedElement<int>) virtualGridCellElement1;
    }

    public bool CacheElement(IVirtualizedElement<int> element)
    {
      if (element is VirtualGridHeaderCellElement)
        return false;
      VirtualGridCellElement virtualGridCellElement = element as VirtualGridCellElement;
      if (virtualGridCellElement == null || virtualGridCellElement.IsDisposed || (virtualGridCellElement.IsDisposing || this.cachedCells.Contains(virtualGridCellElement)))
        return false;
      this.cachedCells.Add(virtualGridCellElement);
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
      if (data == -1)
        return new SizeF((float) this.owner.IndentColumnWidth, 0.0f);
      return new SizeF((float) this.owner.GetColumnWidth(data), 0.0f);
    }

    public SizeF GetElementSize(IVirtualizedElement<int> element)
    {
      return this.GetElementSize(element.Data);
    }

    public SizeF DefaultElementSize
    {
      get
      {
        return new SizeF((float) this.owner.ColumnWidth, 0.0f);
      }
      set
      {
      }
    }

    public void ClearCache()
    {
      this.cachedCells.Clear();
    }
  }
}
