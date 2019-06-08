// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CollapsibleAdapterFactory
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Layouts;

namespace Telerik.WinControls.UI
{
  public class CollapsibleAdapterFactory
  {
    public static CollapsibleElement CreateAdapter(RadElement item)
    {
      if (item is CollapsibleElement)
        return (CollapsibleElement) item;
      if (item is RadButtonElement && !(item is ActionButtonElement) && item.Visibility == ElementVisibility.Visible)
        return (CollapsibleElement) new CollapsableButtonAdapter((RadButtonElement) item);
      return (CollapsibleElement) null;
    }
  }
}
