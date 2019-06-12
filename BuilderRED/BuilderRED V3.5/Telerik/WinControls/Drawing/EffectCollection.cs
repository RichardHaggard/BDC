// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.EffectCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;
using Telerik.Collections.Generic;

namespace Telerik.WinControls.Drawing
{
  public class EffectCollection : NotifyCollection<RadEffect>
  {
    protected override void InsertItem(int index, RadEffect item)
    {
      if (item == null)
        return;
      base.InsertItem(index, item);
    }

    public void RemoveRange(IEnumerable<RadEffect> effects)
    {
      foreach (RadEffect effect in effects)
        this.Items.Remove(effect);
    }
  }
}
