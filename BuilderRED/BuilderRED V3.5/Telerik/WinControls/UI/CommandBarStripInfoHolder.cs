// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CommandBarStripInfoHolder
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class CommandBarStripInfoHolder
  {
    private List<CommandBarStripElement> stripInfoList;

    public List<CommandBarStripElement> StripInfoList
    {
      get
      {
        return this.stripInfoList;
      }
    }

    public CommandBarStripInfoHolder()
    {
      this.stripInfoList = new List<CommandBarStripElement>();
    }

    public void AddStripInfo(CommandBarStripElement strip)
    {
      if (this.stripInfoList.Contains(strip))
        return;
      this.stripInfoList.Add(strip);
    }

    public void RemoveStripInfo(CommandBarStripElement strip)
    {
      this.stripInfoList.Remove(strip);
    }
  }
}
