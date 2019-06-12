// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.ContextMenuOpeningEventArgs
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class ContextMenuOpeningEventArgs : CancelEventArgs
  {
    private RadDropDownMenu contextMenu;
    private IContextMenuProvider provider;

    public ContextMenuOpeningEventArgs(IContextMenuProvider provider, RadDropDownMenu contextMenu)
    {
      this.contextMenu = contextMenu;
      this.provider = provider;
    }

    public ContextMenuOpeningEventArgs(
      IContextMenuProvider provider,
      RadDropDownMenu contextMenu,
      bool cancel)
      : base(cancel)
    {
      this.contextMenu = contextMenu;
      this.provider = provider;
    }

    public RadDropDownMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
      set
      {
        this.contextMenu = value;
      }
    }

    public IContextMenuProvider ContextMenuProvider
    {
      get
      {
        return this.provider;
      }
    }
  }
}
