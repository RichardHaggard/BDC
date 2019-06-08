// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.VirtualGridTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class VirtualGridTraverser : ITraverser<int>, IEnumerator<int>, IDisposable, IEnumerator, IEnumerable
  {
    private int? currentPosition = new int?();
    private VirtualGridTableViewState viewState;

    public VirtualGridTraverser(VirtualGridTableViewState viewState)
    {
      this.viewState = viewState;
    }

    public object Position
    {
      get
      {
        return (object) this.currentPosition;
      }
      set
      {
        this.currentPosition = (int?) value;
        int? currentPosition1 = this.currentPosition;
        if ((currentPosition1.GetValueOrDefault() >= 0 ? 0 : (currentPosition1.HasValue ? 1 : 0)) != 0)
          this.currentPosition = new int?();
        if (!this.viewState.EnablePaging)
          return;
        int? currentPosition2 = this.currentPosition;
        int num = this.viewState.PageIndex * this.viewState.PageSize;
        if ((currentPosition2.GetValueOrDefault() >= num ? 0 : (currentPosition2.HasValue ? 1 : 0)) == 0)
          return;
        this.currentPosition = new int?();
      }
    }

    public bool MovePrevious()
    {
      if (!this.currentPosition.HasValue)
        return false;
      int num1 = 0;
      if (this.viewState.EnablePaging)
        num1 = this.viewState.PageIndex * this.viewState.PageSize;
      int? currentPosition1;
      int num2;
      do
      {
        VirtualGridTraverser virtualGridTraverser = this;
        int? currentPosition2 = virtualGridTraverser.currentPosition;
        virtualGridTraverser.currentPosition = currentPosition2.HasValue ? new int?(currentPosition2.GetValueOrDefault() - 1) : new int?();
        if (this.viewState.IsPinned(this.currentPosition.Value))
        {
          currentPosition1 = this.currentPosition;
          num2 = num1;
        }
        else
          break;
      }
      while ((currentPosition1.GetValueOrDefault() >= num2 ? 0 : (currentPosition1.HasValue ? 1 : 0)) != 0);
      int? currentPosition3 = this.currentPosition;
      int num3 = num1;
      if ((currentPosition3.GetValueOrDefault() >= num3 ? 0 : (currentPosition3.HasValue ? 1 : 0)) != 0)
        this.currentPosition = new int?();
      return true;
    }

    public bool MoveToEnd()
    {
      this.Position = (object) (this.viewState.ItemCount - 1);
      return true;
    }

    public int Current
    {
      get
      {
        return this.currentPosition ?? -1;
      }
    }

    public void Dispose()
    {
    }

    object IEnumerator.Current
    {
      get
      {
        return this.Position;
      }
    }

    public bool MoveNext()
    {
      int val1 = this.viewState.ItemCount - 1;
      if (this.viewState.EnablePaging)
        val1 = Math.Min(val1, (this.viewState.PageIndex + 1) * this.viewState.PageSize - 1);
      if (!this.currentPosition.HasValue)
      {
        if (val1 < 0)
          return false;
        this.currentPosition = new int?(0);
        if (this.viewState.EnablePaging)
          this.currentPosition = new int?(this.viewState.PageIndex * this.viewState.PageSize);
        int? currentPosition1;
        VirtualGridTraverser virtualGridTraverser;
        for (; this.viewState.IsPinned(this.currentPosition.Value); virtualGridTraverser.currentPosition = currentPosition1.HasValue ? new int?(currentPosition1.GetValueOrDefault() + 1) : new int?())
        {
          int? currentPosition2 = this.currentPosition;
          int num = val1;
          if ((currentPosition2.GetValueOrDefault() > num ? 0 : (currentPosition2.HasValue ? 1 : 0)) != 0)
          {
            virtualGridTraverser = this;
            currentPosition1 = virtualGridTraverser.currentPosition;
          }
          else
            break;
        }
        int? currentPosition3 = this.currentPosition;
        int num1 = val1;
        if ((currentPosition3.GetValueOrDefault() <= num1 ? 0 : (currentPosition3.HasValue ? 1 : 0)) == 0)
          return true;
        this.currentPosition = new int?(val1);
        return false;
      }
      int? currentPosition4 = this.currentPosition;
      int num2 = val1;
      if ((currentPosition4.GetValueOrDefault() >= num2 ? 0 : (currentPosition4.HasValue ? 1 : 0)) == 0)
        return false;
      int? currentPosition5;
      int num3;
      do
      {
        VirtualGridTraverser virtualGridTraverser = this;
        int? currentPosition1 = virtualGridTraverser.currentPosition;
        virtualGridTraverser.currentPosition = currentPosition1.HasValue ? new int?(currentPosition1.GetValueOrDefault() + 1) : new int?();
        if (this.viewState.IsPinned(this.currentPosition.Value))
        {
          currentPosition5 = this.currentPosition;
          num3 = val1;
        }
        else
          break;
      }
      while ((currentPosition5.GetValueOrDefault() >= num3 ? 0 : (currentPosition5.HasValue ? 1 : 0)) != 0);
      return !this.viewState.IsPinned(this.currentPosition.Value);
    }

    public void Reset()
    {
      this.currentPosition = new int?();
    }

    public IEnumerator GetEnumerator()
    {
      return (IEnumerator) new VirtualGridTraverser(this.viewState)
      {
        Position = this.Position
      };
    }
  }
}
