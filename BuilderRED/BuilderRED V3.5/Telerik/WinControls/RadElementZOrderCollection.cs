// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RadElementZOrderCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls
{
  internal class RadElementZOrderCollection
  {
    private Dictionary<RadElement, int> cachedIndices = new Dictionary<RadElement, int>();
    private List<RadElement> list;
    private RadElement owner;
    private int layoutableCount;

    public RadElementZOrderCollection(RadElement owner)
    {
      this.owner = owner;
      this.list = new List<RadElement>();
    }

    internal void Add(RadElement element)
    {
      this.list.Insert(this.FindInsertIndex(element), element);
      if (element.Visibility == ElementVisibility.Collapsed)
        return;
      ++this.layoutableCount;
    }

    internal void Remove(RadElement element)
    {
      this.RemoveAt(this.list.IndexOf(element));
    }

    internal void OnElementSet(RadElement element)
    {
      if (this.list.IndexOf(element) >= 0)
        this.OnElementZIndexChanged(element);
      else
        this.Add(element);
    }

    private void RemoveAt(int index)
    {
      if (this.list[index].Visibility != ElementVisibility.Collapsed)
        --this.layoutableCount;
      this.list.RemoveAt(index);
    }

    internal void Reset()
    {
      this.list.Clear();
      this.list.AddRange((IEnumerable<RadElement>) this.owner.Children);
      this.list.Sort(new Comparison<RadElement>(this.CompareElements));
    }

    internal void OnElementZIndexChanged(RadElement element)
    {
      this.Remove(element);
      this.Add(element);
    }

    internal void OnElementVisibilityChanged(RadElement element)
    {
      if (element.Visibility != ElementVisibility.Collapsed)
        ++this.layoutableCount;
      else
        --this.layoutableCount;
    }

    internal void Clear()
    {
      this.list.Clear();
      this.layoutableCount = 0;
      this.ResetCachedIndices();
    }

    public List<RadElement> Elements
    {
      get
      {
        return this.list;
      }
    }

    public void SendToBack(RadElement element)
    {
      if (!this.list.Contains(element))
        return;
      int zindex = this.list[0].ZIndex;
      element.ZIndex = zindex - 1;
    }

    public void BringToFront(RadElement element)
    {
      if (!this.list.Contains(element))
        return;
      int zindex = this.list[this.list.Count - 1].ZIndex;
      element.ZIndex = zindex + 1;
    }

    private int FindInsertIndex(RadElement element)
    {
      int num1 = this.list.Count;
      if (num1 == 0)
        return 0;
      int num2 = 0;
      do
      {
        int index = num2 + num1 >> 1;
        RadElement el2 = this.list[index];
        switch (this.CompareElements(element, el2))
        {
          case -1:
            num1 = index;
            break;
          case 1:
            num2 = index + 1;
            break;
        }
      }
      while (num2 < num1);
      return num2;
    }

    private int CompareElements(RadElement el1, RadElement el2)
    {
      int num1 = el1.ZIndex;
      int num2 = el2.ZIndex;
      if (num1 == num2)
      {
        num1 = this.GetIndexInOwnerChildren(el1);
        num2 = this.GetIndexInOwnerChildren(el2);
      }
      return num1.CompareTo(num2);
    }

    internal void ResetCachedIndices()
    {
      this.cachedIndices.Clear();
    }

    private int GetIndexInOwnerChildren(RadElement element)
    {
      if (this.cachedIndices.Count == 0)
      {
        for (int index = 0; index < this.owner.Children.Count; ++index)
          this.cachedIndices.Add(this.owner.Children[index], index);
      }
      if (this.cachedIndices.ContainsKey(element))
        return this.cachedIndices[element];
      return this.owner.Children.IndexOf(element);
    }

    public int LayoutableCount
    {
      get
      {
        return this.layoutableCount;
      }
    }
  }
}
