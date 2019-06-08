// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.ILayoutManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.Layouts
{
  public interface ILayoutManager
  {
    ILayoutQueue MeasureQueue { get; }

    ILayoutQueue ArrangeQueue { get; }

    List<EventHandler> LayoutEvents { get; }

    void RemoveElementFromQueues(RadElement e);

    void IncrementLayoutCalls();

    void DecrementLayoutCalls();

    void AddToSizeChangedChain(SizeChangedInfo info);

    void EnterArrange();

    void EnterMeasure();

    void ExitArrange();

    void ExitMeasure();

    bool IsUpdating { get; }

    void UpdateLayout();

    void InvokeUpdateLayoutAsync();
  }
}
