// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.IDataItem
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Data
{
  public interface IDataItem
  {
    object DataBoundItem { get; set; }

    int FieldCount { get; }

    object this[string name] { get; set; }

    object this[int index] { get; set; }

    int IndexOf(string name);
  }
}
