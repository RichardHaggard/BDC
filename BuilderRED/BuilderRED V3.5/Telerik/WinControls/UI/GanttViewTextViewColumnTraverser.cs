﻿// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GanttViewTextViewColumnTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class GanttViewTextViewColumnTraverser : ItemsTraverser<GanttViewTextViewColumn>
  {
    public GanttViewTextViewColumnTraverser(IList<GanttViewTextViewColumn> columns)
      : base(columns)
    {
    }

    protected override bool OnItemsNavigating(GanttViewTextViewColumn current)
    {
      if (!base.OnItemsNavigating(current))
        return !current.Visible;
      return true;
    }
  }
}
