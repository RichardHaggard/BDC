// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewContextMenuManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class GridViewContextMenuManager : IContextMenuManager, IGridViewEventListener, IDisposable
  {
    private RadGridViewElement rootElement;
    private RadDropDownMenu contextMenu;

    public GridViewContextMenuManager(RadGridViewElement rootElement)
    {
      this.rootElement = rootElement;
      this.rootElement.Template.SynchronizationService.AddListener((IGridViewEventListener) this);
    }

    public virtual RadGridViewElement RootElement
    {
      get
      {
        return this.rootElement;
      }
    }

    public virtual RadDropDownMenu ContextMenu
    {
      get
      {
        return this.contextMenu;
      }
    }

    public virtual bool ShowContextMenu(IContextMenuProvider contextMenuProvider, Point location)
    {
      if (this.rootElement == null)
        return false;
      RadDropDownMenu radDropDownMenu = contextMenuProvider.ContextMenu ?? contextMenuProvider.MergeMenus((IContextMenuManager) this);
      GridViewContextMenuManager.UpdateMenuTheme((GridVisualElement) this.rootElement, radDropDownMenu);
      ContextMenuOpeningEventArgs args = new ContextMenuOpeningEventArgs(contextMenuProvider, radDropDownMenu);
      this.rootElement.Template.EventDispatcher.RaiseEvent<ContextMenuOpeningEventArgs>(EventDispatcher.ContextMenuOpening, (object) contextMenuProvider, args);
      RadDropDownMenu contextMenu = args.ContextMenu;
      if (args.Cancel || contextMenu == null || contextMenu.Items.Count == 0)
        return false;
      if (this.rootElement.RightToLeft)
      {
        contextMenu.RightToLeft = RightToLeft.Yes;
      }
      else
      {
        contextMenu.RightToLeft = RightToLeft.No;
        contextMenu.HorizontalPopupAlignment = HorizontalPopupAlignment.LeftToLeft;
      }
      contextMenu.Show(this.rootElement.ElementTree.Control, location);
      this.contextMenu = contextMenu;
      this.contextMenu.PopupClosed += new RadPopupClosedEventHandler(this.ContextMenu_PopupClosed);
      return true;
    }

    private void ContextMenu_PopupClosed(object sender, RadPopupClosedEventArgs args)
    {
      RadDropDownMenu radDropDownMenu = sender as RadDropDownMenu;
      if (radDropDownMenu == null)
        return;
      radDropDownMenu.PopupClosed -= new RadPopupClosedEventHandler(this.ContextMenu_PopupClosed);
      this.contextMenu = (RadDropDownMenu) null;
    }

    public virtual bool ShowContextMenu(IContextMenuProvider contextMenuProvider)
    {
      Point client = this.rootElement.ElementTree.Control.PointToClient(Control.MousePosition);
      return this.ShowContextMenu(contextMenuProvider, client);
    }

    public virtual void HideContextMenu()
    {
      if (this.contextMenu == null)
        return;
      this.contextMenu.ClosePopup(RadPopupCloseReason.CloseCalled);
    }

    internal static void UpdateMenuTheme(GridVisualElement gridElement, RadDropDownMenu menu)
    {
      if (menu == null)
        return;
      string str = gridElement.ElementTree.ThemeName;
      if (string.IsNullOrEmpty(str))
        str = "ControlDefault";
      menu.ThemeName = str;
    }

    GridEventType IGridViewEventListener.DesiredEvents
    {
      get
      {
        return GridEventType.UI;
      }
    }

    EventListenerPriority IGridViewEventListener.Priority
    {
      get
      {
        return EventListenerPriority.Normal;
      }
    }

    GridEventProcessMode IGridViewEventListener.DesiredProcessMode
    {
      get
      {
        return GridEventProcessMode.Process;
      }
    }

    GridViewEventResult IGridViewEventListener.PreProcessEvent(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.ProcessEvent(
      GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.CurrentChanged)
        return this.ProcessCurrentChanged(eventData);
      return (GridViewEventResult) null;
    }

    protected virtual GridViewEventResult ProcessCurrentChanged(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.PostProcessEvent(
      GridViewEvent eventData)
    {
      return (GridViewEventResult) null;
    }

    bool IGridViewEventListener.AnalyzeQueue(List<GridViewEvent> events)
    {
      return false;
    }

    public void Dispose()
    {
      if (this.rootElement != null)
      {
        this.rootElement.Template.SynchronizationService.RemoveListener((IGridViewEventListener) this);
        this.rootElement = (RadGridViewElement) null;
      }
      if (this.contextMenu == null)
        return;
      this.contextMenu.Dispose();
    }
  }
}
