// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IGanttViewDataProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public interface IGanttViewDataProvider
  {
    object DataSource { get; }

    string TaskDataMember { get; }

    string ChildMember { get; }

    string ParentMember { get; }

    string TitleMember { get; }

    string StartMember { get; }

    string EndMember { get; }

    string ProgressMember { get; }

    string LinkDataMember { get; }

    string LinkStartMember { get; }

    string LinkEndMember { get; }

    string LinkTypeMember { get; }

    List<GanttViewTextViewColumn> Columns { get; set; }
  }
}
