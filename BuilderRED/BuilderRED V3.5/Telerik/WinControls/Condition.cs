// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Condition
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls
{
  public abstract class Condition
  {
    public abstract bool Evaluate(RadObject target);

    public List<RadProperty> AffectedProperties
    {
      get
      {
        List<RadProperty> inList = new List<RadProperty>();
        this.FillAffectedProperties(inList);
        return inList;
      }
    }

    public List<RaisedRoutedEvent> AffectedEvents
    {
      get
      {
        List<RaisedRoutedEvent> inList = new List<RaisedRoutedEvent>();
        this.FillAffectedEvents(inList);
        return inList;
      }
    }

    protected virtual void FillAffectedProperties(List<RadProperty> inList)
    {
    }

    protected virtual void FillAffectedEvents(List<RaisedRoutedEvent> inList)
    {
    }
  }
}
