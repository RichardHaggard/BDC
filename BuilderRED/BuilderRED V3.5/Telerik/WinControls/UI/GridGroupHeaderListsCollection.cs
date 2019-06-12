// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridGroupHeaderListsCollection
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public class GridGroupHeaderListsCollection : RadItemOwnerCollection
  {
    public GridGroupHeaderListsCollection(RadElement owner)
      : base(owner)
    {
    }

    public GridGroupHeaderList this[int index]
    {
      get
      {
        return (GridGroupHeaderList) null;
      }
    }

    public int IndexOf(GridViewTemplate template, int index, int count)
    {
      return -1;
    }

    public int IndexOf(GridViewTemplate template)
    {
      return -1;
    }

    public int IndexOf(GridViewTemplate template, int index)
    {
      return -1;
    }

    public void Move(int oldIndex, int newIndex)
    {
    }

    public void RemoveTail(int index)
    {
    }

    public void RemoveRange(int index, int count)
    {
    }
  }
}
