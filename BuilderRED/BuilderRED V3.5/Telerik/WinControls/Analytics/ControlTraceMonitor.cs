// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Analytics.ControlTraceMonitor
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Analytics
{
  public static class ControlTraceMonitor
  {
    public static ITraceMonitor AnalyticsMonitor { get; set; }

    public static void TrackAtomicFeature(string controlName, string traceEvent, object value)
    {
      if (ControlTraceMonitor.AnalyticsMonitor == null)
        return;
      string feature = string.Format("{0}.{1}", (object) controlName, (object) traceEvent);
      if (feature == null)
        return;
      string str = value == null ? (string) null : Convert.ToString(value);
      if (!string.IsNullOrEmpty(str))
        feature = string.Format("{0}.{1}", (object) feature, (object) str);
      ControlTraceMonitor.AnalyticsMonitor.TrackAtomicFeature(feature);
    }

    public static void TrackAtomicFeature(RadControl control, string traceEvent, object value)
    {
      string featureName = ControlTraceMonitor.GetFeatureName(control, traceEvent);
      if (featureName == null)
        return;
      string str = value == null ? (string) null : Convert.ToString(value);
      ControlTraceMonitor.AnalyticsMonitor.TrackAtomicFeature(!string.IsNullOrEmpty(str) ? string.Format("{0}.{1}", (object) featureName, (object) str) : string.Format("{0}.NULL", (object) featureName));
    }

    public static void TrackAtomicFeature(RadElement element, string traceEvent, object value)
    {
      if (element == null || element.ElementTree == null || element.ElementTree.Control == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(element.ElementTree.Control as RadControl, traceEvent, value);
    }

    public static void TrackAtomicFeature(RadElement element, string traceEvent)
    {
      if (element == null || element.ElementTree == null || element.ElementTree.Control == null)
        return;
      ControlTraceMonitor.TrackAtomicFeature(element.ElementTree.Control as RadControl, traceEvent);
    }

    public static void TrackAtomicFeature(RadControl control, string traceEvent)
    {
      string featureName = ControlTraceMonitor.GetFeatureName(control, traceEvent);
      if (featureName == null)
        return;
      ControlTraceMonitor.AnalyticsMonitor.TrackAtomicFeature(featureName);
    }

    public static void TrackFeatureStart(RadControl control, string traceEvent)
    {
      string featureName = ControlTraceMonitor.GetFeatureName(control, traceEvent);
      if (featureName == null)
        return;
      ControlTraceMonitor.AnalyticsMonitor.TrackFeatureStart(featureName);
    }

    public static void TrackFeatureStop(RadControl control, string traceEvent)
    {
      string featureName = ControlTraceMonitor.GetFeatureName(control, traceEvent);
      if (featureName == null)
        return;
      ControlTraceMonitor.AnalyticsMonitor.TrackFeatureEnd(featureName);
    }

    public static void TrackErrorCore(RadControl control, string traceEvent, Exception exception)
    {
      string featureName;
      if (exception == null || (featureName = ControlTraceMonitor.GetFeatureName(control, traceEvent)) == null)
        return;
      ControlTraceMonitor.AnalyticsMonitor.TrackError(featureName, exception);
    }

    public static void TrackValueCore(RadControl control, string traceEvent, long value)
    {
      string featureName = ControlTraceMonitor.GetFeatureName(control, traceEvent);
      if (featureName == null)
        return;
      ControlTraceMonitor.AnalyticsMonitor.TrackValue(featureName, value);
    }

    private static string GetFeatureName(RadControl control, string traceEvent)
    {
      string controlName;
      if (ControlTraceMonitor.AnalyticsMonitor == null || control == null || (string.IsNullOrEmpty(traceEvent) || !control.EnableAnalytics) || (control.IsInitializing || !control.IsHandleCreated || string.IsNullOrEmpty(controlName = ControlTraceMonitor.GetControlName(control))))
        return (string) null;
      if (string.IsNullOrEmpty(controlName))
        return string.Format("{0}.{1}", (object) control.GetType().Name, (object) traceEvent);
      return string.Format("{0}.{1}.{2}", (object) control.GetType().Name, (object) controlName, (object) traceEvent);
    }

    private static string GetControlName(RadControl control)
    {
      if (!string.IsNullOrEmpty(control.AnalyticsName))
        return control.AnalyticsName;
      return control.Name;
    }
  }
}
