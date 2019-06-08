// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.IContextMenuProvider
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.UI
{
  public interface IContextMenuProvider
  {
    RadDropDownMenu ContextMenu { get; set; }

    RadDropDownMenu MergeMenus(
      RadDropDownMenu contextMenu,
      params object[] parameters);

    RadDropDownMenu MergeMenus(
      IContextMenuProvider contextMenuProvider,
      params object[] parameters);

    RadDropDownMenu MergeMenus(
      IContextMenuManager contextMenuManager,
      params object[] parameters);
  }
}
