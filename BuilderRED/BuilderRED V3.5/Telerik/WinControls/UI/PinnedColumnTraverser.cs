// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PinnedColumnTraverser
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class PinnedColumnTraverser : ItemsTraverser<GridViewColumn>
  {
    private bool filterByPinPosition = true;
    private PinnedColumnPosition pinPosition;

    public PinnedColumnTraverser(IList<GridViewColumn> collection, PinnedColumnPosition pinPosition)
      : base(collection)
    {
      this.pinPosition = pinPosition;
    }

    public PinnedColumnPosition PinPosition
    {
      get
      {
        return this.pinPosition;
      }
      set
      {
        this.pinPosition = value;
      }
    }

    public bool FilterByPinPosition
    {
      get
      {
        return this.filterByPinPosition;
      }
      set
      {
        this.filterByPinPosition = value;
      }
    }

    protected override bool OnItemsNavigating(GridViewColumn current)
    {
      if (!this.filterByPinPosition)
        base.OnItemsNavigating(current);
      if (current.IsVisible)
        return current.PinPosition != this.pinPosition;
      return true;
    }
  }
}
