// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PropertyBoundObject
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class PropertyBoundObject
  {
    private WeakReference boundObject;
    private RadProperty boundProperty;

    public PropertyBoundObject(RadObject boundObject, RadProperty boundProperty)
    {
      this.boundObject = new WeakReference((object) boundObject, false);
      this.boundProperty = boundProperty;
    }

    public void UpdateValue()
    {
      RadObject radObject = this.Object;
      if (radObject == null)
        return;
      int num = (int) radObject.UpdateValue(this.boundProperty);
    }

    public RadProperty Property
    {
      get
      {
        return this.boundProperty;
      }
    }

    public RadObject Object
    {
      get
      {
        if (!this.boundObject.IsAlive)
          return (RadObject) null;
        RadObject target = (RadObject) this.boundObject.Target;
        if (target.IsDisposed || target.IsDisposing)
          return (RadObject) null;
        return target;
      }
    }

    public bool IsObjectAlive
    {
      get
      {
        return this.boundObject.IsAlive;
      }
    }
  }
}
