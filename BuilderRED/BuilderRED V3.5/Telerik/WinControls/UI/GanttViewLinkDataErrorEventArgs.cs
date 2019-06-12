// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewLinkDataErrorEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GanttViewLinkDataErrorEventArgs : RadGanttViewLinkEventArgs
  {
    private string errorText;
    private object[] context;

    public GanttViewLinkDataErrorEventArgs(
      GanttViewLinkDataItem item,
      string errorText,
      params object[] context)
      : base(item)
    {
      this.errorText = errorText;
      this.context = context;
    }

    public string ErrorText
    {
      get
      {
        return this.errorText;
      }
    }

    public object[] Context
    {
      get
      {
        return this.context;
      }
    }
  }
}
