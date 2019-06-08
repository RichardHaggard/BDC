// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Export.IExportGridTo
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace Telerik.WinControls.Export
{
  public interface IExportGridTo
  {
    SummariesOption SummariesExportOption { set; }

    HiddenOption HiddenColumnOption { set; }

    HiddenOption HiddenRowOption { set; }

    RadGridView RadGridViewToExport { set; }

    string FileExtension { set; }

    void RunExport(string fileName);
  }
}
