// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PropertyGridTraverserState
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class PropertyGridTraverserState
  {
    private PropertyGridGroupItem group;
    private PropertyGridItemBase item;
    private int index;
    private int groupIndex;

    public PropertyGridTraverserState(
      PropertyGridGroupItem group,
      PropertyGridItemBase item,
      int index,
      int groupIndex)
    {
      this.group = group;
      this.item = item;
      this.index = index;
      this.groupIndex = groupIndex;
    }

    public PropertyGridGroupItem Group
    {
      get
      {
        return this.group;
      }
    }

    public PropertyGridItemBase Item
    {
      get
      {
        return this.item;
      }
    }

    public int Index
    {
      get
      {
        return this.index;
      }
    }

    public int GroupIndex
    {
      get
      {
        return this.groupIndex;
      }
    }
  }
}
