// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.RoutedEventBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls
{
  public class RoutedEventBehavior
  {
    private RaisedRoutedEvent raisedRoutedEvent;

    public RaisedRoutedEvent RaisedRoutedEvent
    {
      get
      {
        return this.raisedRoutedEvent;
      }
      set
      {
        this.raisedRoutedEvent = value;
      }
    }

    public RoutedEventBehavior(RaisedRoutedEvent raisedRoutedEvent)
    {
      this.raisedRoutedEvent = raisedRoutedEvent;
    }

    public virtual void OnEventOccured(RadElement sender, RadElement element, RoutedEventArgs args)
    {
    }

    public virtual void BehaviorRemoving(RadElement fromElement)
    {
    }
  }
}
