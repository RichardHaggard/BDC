// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridContentElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class PropertyGridContentElement : LightVisualElement
  {
    public PropertyGridTableElement PropertyGridTableElement
    {
      get
      {
        return this.FindAncestor<PropertyGridTableElement>();
      }
    }

    public PropertyGridItemElementBase VisualItem
    {
      get
      {
        return this.FindAncestor<PropertyGridItemElementBase>();
      }
    }
  }
}
