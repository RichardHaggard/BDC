// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ITraverser`1
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public interface ITraverser<T> : IEnumerator<T>, IDisposable, IEnumerator, IEnumerable
  {
    object Position { get; set; }

    bool MovePrevious();

    bool MoveToEnd();
  }
}
