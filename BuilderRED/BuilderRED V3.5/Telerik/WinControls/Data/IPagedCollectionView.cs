// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Data.IPagedCollectionView
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.Data
{
  public interface IPagedCollectionView
  {
    bool CanPage { get; }

    event EventHandler<EventArgs> PageChanged;

    event EventHandler<PageChangingEventArgs> PageChanging;

    bool MoveToFirstPage();

    bool MoveToLastPage();

    bool MoveToNextPage();

    bool MoveToPage(int pageIndex);

    bool MoveToPreviousPage();

    bool CanChangePage { get; }

    bool IsPageChanging { get; }

    int PageIndex { get; }

    int PageSize { get; set; }

    int ItemCount { get; }
  }
}
