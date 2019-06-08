// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TemplateDataView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections;
using System.Collections.Generic;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class TemplateDataView : RadDataView<GridViewRowInfo>
  {
    public TemplateDataView(IEnumerable<GridViewRowInfo> rows)
      : base(rows)
    {
    }

    protected override void ProcessCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      if (args.Action == NotifyCollectionChangedAction.Remove && this.HasGroup)
        this.RemoveRowsFromGroup(args.NewItems);
      base.ProcessCollectionChanged(args);
    }

    private void RemoveRowsFromGroup(IList list)
    {
    }
  }
}
