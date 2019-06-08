// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Analytics.ITraceMonitor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Analytics
{
  public interface ITraceMonitor
  {
    void TrackAtomicFeature(string feature);

    void TrackFeatureStart(string feature);

    void TrackFeatureEnd(string feature);

    void TrackFeatureCancel(string feature);

    void TrackError(string feature, Exception exception);

    void TrackValue(string feature, long value);
  }
}
