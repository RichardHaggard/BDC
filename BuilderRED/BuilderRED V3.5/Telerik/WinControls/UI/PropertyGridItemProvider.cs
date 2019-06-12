// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridItemProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public abstract class PropertyGridItemProvider : IDisposable
  {
    private PropertyGridTableElement propertyGridElement;
    private int update;

    public PropertyGridItemProvider(PropertyGridTableElement propertyGridElement)
    {
      this.propertyGridElement = propertyGridElement;
    }

    public abstract IList<PropertyGridItem> GetItems();

    public virtual void SetCurrent(PropertyGridItem item)
    {
    }

    public PropertyGridTableElement RadPropertyGridElement
    {
      get
      {
        return this.propertyGridElement;
      }
    }

    public virtual void Reset()
    {
    }

    public virtual void Dispose()
    {
    }

    public virtual void SuspendUpdate()
    {
      ++this.update;
    }

    public virtual void ResumeUpdate()
    {
      --this.update;
    }

    public bool IsSuspended
    {
      get
      {
        return this.update > 0;
      }
    }
  }
}
