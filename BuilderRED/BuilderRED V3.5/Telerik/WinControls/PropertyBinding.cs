// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PropertyBinding
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls
{
  public class PropertyBinding
  {
    private RadProperty boundProperty;
    private RadProperty sourceProperty;
    private WeakReference sourceObject;
    private PropertyBindingOptions options;

    public PropertyBinding(
      RadObject sourceObject,
      RadProperty boundProperty,
      RadProperty sourceProperty,
      PropertyBindingOptions options)
    {
      this.sourceObject = new WeakReference((object) sourceObject);
      this.boundProperty = boundProperty;
      this.sourceProperty = sourceProperty;
      this.options = options;
    }

    public object GetValue()
    {
      return this.SourceObject?.GetValue(this.sourceProperty);
    }

    public void UpdateSourceProperty(object newValue)
    {
      if ((this.options & PropertyBindingOptions.NoChangeNotify) == PropertyBindingOptions.NoChangeNotify || (this.options & PropertyBindingOptions.TwoWay) != PropertyBindingOptions.TwoWay)
        return;
      this.SourceObject?.OnTwoWayBoundPropertyChanged(this, newValue);
    }

    public RadObject SourceObject
    {
      get
      {
        if (!this.sourceObject.IsAlive)
          return (RadObject) null;
        RadObject target = (RadObject) this.sourceObject.Target;
        if (target.IsDisposed || target.IsDisposing)
          return (RadObject) null;
        return target;
      }
    }

    public bool IsSourceObjectAlive
    {
      get
      {
        return this.sourceObject.IsAlive;
      }
    }

    public PropertyBindingOptions BindingOptions
    {
      get
      {
        return this.options;
      }
    }

    public RadProperty BoundProperty
    {
      get
      {
        return this.boundProperty;
      }
    }

    public RadProperty SourceProperty
    {
      get
      {
        return this.sourceProperty;
      }
    }
  }
}
