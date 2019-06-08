// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyDisplayNameNeededEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class PropertyDisplayNameNeededEventArgs
  {
    public PropertyDisplayNameNeededEventArgs(string fieldName, string displayName)
    {
      this.FieldName = fieldName;
      this.DisplayName = displayName;
    }

    public string FieldName { get; private set; }

    public string DisplayName { get; set; }
  }
}
