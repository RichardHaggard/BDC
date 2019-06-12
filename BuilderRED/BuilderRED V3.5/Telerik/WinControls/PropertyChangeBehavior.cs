// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.PropertyChangeBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class PropertyChangeBehavior
  {
    private RadProperty _property;

    public PropertyChangeBehavior(RadProperty executeBehaviorPropertyTrigger)
    {
      this._property = executeBehaviorPropertyTrigger;
    }

    public RadProperty Property
    {
      get
      {
        return this._property;
      }
      set
      {
        this._property = value;
      }
    }

    public virtual void OnPropertyChange(RadElement element, RadPropertyChangedEventArgs e)
    {
    }

    public virtual void BehaviorRemoving(RadElement element)
    {
    }
  }
}
