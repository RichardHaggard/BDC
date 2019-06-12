// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.BaseVirtualizedContainer`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Drawing;
using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public abstract class BaseVirtualizedContainer<T> : LayoutPanel
  {
    private IVirtualizedElementProvider<T> elementProvider;
    private IEnumerable dataProvider;

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ClipDrawing = true;
    }

    public IVirtualizedElementProvider<T> ElementProvider
    {
      get
      {
        return this.elementProvider;
      }
      set
      {
        if (this.elementProvider == value)
          return;
        this.elementProvider = value;
        this.Children.Clear();
      }
    }

    public IEnumerable DataProvider
    {
      get
      {
        return this.dataProvider;
      }
      set
      {
        if (this.dataProvider == value)
          return;
        this.dataProvider = value;
        this.InvalidateMeasure();
      }
    }

    protected bool DataProviderIsEmpty
    {
      get
      {
        return this.DataProvider.GetEnumerator() == null;
      }
    }

    public virtual void UpdateItems()
    {
      foreach (RadElement child in this.Children)
        child.InvalidateMeasure();
      this.InvalidateMeasure();
    }

    protected override SizeF MeasureOverride(SizeF availableSize)
    {
      if (this.BeginMeasure(availableSize))
        this.MeasureElements();
      return this.EndMeasure();
    }

    protected virtual void MeasureElements()
    {
      int position = 0;
      foreach (T data in this.DataProvider)
      {
        if (this.IsItemVisible(data))
        {
          IVirtualizedElement<T> element = this.UpdateElement(position, data);
          if (element != null)
          {
            ++position;
            if (!this.MeasureElement(element))
              break;
          }
        }
      }
      while (position < this.Children.Count)
        this.RemoveElement(position);
    }

    protected virtual bool BeginMeasure(SizeF availableSize)
    {
      if (this.DataProvider != null && this.ElementProvider != null)
        return !this.DataProviderIsEmpty;
      return false;
    }

    protected abstract bool MeasureElement(IVirtualizedElement<T> element);

    protected abstract SizeF EndMeasure();

    protected virtual bool IsItemVisible(T data)
    {
      return true;
    }

    protected virtual object GetElementContext()
    {
      return (object) null;
    }

    protected virtual void RemoveElement(int position)
    {
      IVirtualizedElement<T> child = (IVirtualizedElement<T>) this.Children[position];
      this.ElementProvider.CacheElement(child);
      this.InvalidateMeasureOnRemove = false;
      this.Children.RemoveAt(position);
      this.InvalidateMeasureOnRemove = true;
      child.Detach();
    }

    protected virtual void InsertElement(int position, IVirtualizedElement<T> element, T data)
    {
      if (element == null)
        return;
      ((RadElement) element).Visibility = ElementVisibility.Visible;
      this.Children.checkForAlreadyAddedItems = false;
      this.Children.Insert(position, (RadElement) element);
      this.Children.checkForAlreadyAddedItems = true;
      element.Attach(data, this.GetElementContext());
    }

    protected virtual int FindCompatibleElement(int position, T data)
    {
      return -1;
    }

    protected virtual IVirtualizedElement<T> UpdateElement(int position, T data)
    {
      object elementContext = this.GetElementContext();
      IVirtualizedElement<T> element;
      if (position < this.Children.Count)
      {
        element = (IVirtualizedElement<T>) this.Children[position];
        if (this.ElementProvider.ShouldUpdate(element, data, elementContext))
        {
          if (position < this.Children.Count - 1)
          {
            IVirtualizedElement<T> child = (IVirtualizedElement<T>) this.Children[position + 1];
            if (child.Data.Equals((object) data))
            {
              this.Children.Move(position + 1, position);
              child.Synchronize();
              return child;
            }
          }
          if (this.ElementProvider.IsCompatible(element, data, elementContext))
          {
            element.Detach();
            element.Attach(data, elementContext);
          }
          else
          {
            int compatibleElement = this.FindCompatibleElement(position, data);
            if (compatibleElement > position)
            {
              this.Children.Move(compatibleElement, position);
              element = (IVirtualizedElement<T>) this.Children[position];
              element.Synchronize();
            }
            else
            {
              element = this.ElementProvider.GetElement(data, elementContext);
              this.InsertElement(position, element, data);
            }
          }
        }
      }
      else
      {
        element = this.ElementProvider.GetElement(data, elementContext);
        this.InsertElement(position, element, data);
      }
      return element;
    }
  }
}
