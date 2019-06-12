// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.PopupEditorNotificationData
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;
using Telerik.WinControls.UI.Data;

namespace Telerik.WinControls.UI
{
  public class PopupEditorNotificationData
  {
    public PopupEditorNotificationData.Context notificationContext;
    public Telerik.WinControls.UI.Data.ValueChangedEventArgs valueChangedEventArgs;
    public PositionChangingCancelEventArgs positionChangingCancelEventArgs;
    public Telerik.WinControls.UI.Data.PositionChangedEventArgs positionChangedEventArgs;
    public ListItemDataBindingEventArgs listItemDataBindingEventArgs;
    public ListItemDataBoundEventArgs listItemDataBoundEventArgs;
    public CreatingVisualListItemEventArgs creatingVisualListItemEventArgs;
    public KeyPressEventArgs keyPressEventArgs;
    public MouseEventArgs mouseEventArgs;
    public SortStyleChangedEventArgs sortStyleChanged;
    public VisualItemFormattingEventArgs visualItemFormatting;
    public KeyEventArgs keyEventArgs;

    public PopupEditorNotificationData()
    {
      this.notificationContext = PopupEditorNotificationData.Context.None;
      this.valueChangedEventArgs = (Telerik.WinControls.UI.Data.ValueChangedEventArgs) null;
      this.positionChangingCancelEventArgs = (PositionChangingCancelEventArgs) null;
      this.positionChangedEventArgs = (Telerik.WinControls.UI.Data.PositionChangedEventArgs) null;
      this.listItemDataBindingEventArgs = (ListItemDataBindingEventArgs) null;
      this.listItemDataBoundEventArgs = (ListItemDataBoundEventArgs) null;
      this.creatingVisualListItemEventArgs = (CreatingVisualListItemEventArgs) null;
      this.keyPressEventArgs = (KeyPressEventArgs) null;
      this.mouseEventArgs = (MouseEventArgs) null;
      this.sortStyleChanged = (SortStyleChangedEventArgs) null;
      this.visualItemFormatting = (VisualItemFormattingEventArgs) null;
      this.keyEventArgs = (KeyEventArgs) null;
    }

    public PopupEditorNotificationData(Telerik.WinControls.UI.Data.ValueChangedEventArgs valueChangedEventArgs)
      : this()
    {
      this.notificationContext = PopupEditorNotificationData.Context.SelectedValueChanged;
      this.valueChangedEventArgs = valueChangedEventArgs;
    }

    public PopupEditorNotificationData(
      PositionChangingCancelEventArgs positionChangingCancelEventArgs)
      : this()
    {
      this.notificationContext = PopupEditorNotificationData.Context.SelectedIndexChanging;
      this.positionChangingCancelEventArgs = positionChangingCancelEventArgs;
    }

    public PopupEditorNotificationData(Telerik.WinControls.UI.Data.PositionChangedEventArgs positionChangedEventArgs)
      : this()
    {
      this.notificationContext = PopupEditorNotificationData.Context.SelectedIndexChanged;
      this.positionChangedEventArgs = positionChangedEventArgs;
    }

    public PopupEditorNotificationData(
      ListItemDataBindingEventArgs listItemDataBindingEventArgs)
      : this()
    {
      this.notificationContext = PopupEditorNotificationData.Context.ListItemDataBinding;
      this.listItemDataBindingEventArgs = listItemDataBindingEventArgs;
    }

    public PopupEditorNotificationData(
      ListItemDataBoundEventArgs listItemDataBoundEventArgs)
      : this()
    {
      this.notificationContext = PopupEditorNotificationData.Context.ListItemDataBound;
      this.listItemDataBoundEventArgs = listItemDataBoundEventArgs;
    }

    public PopupEditorNotificationData(
      CreatingVisualListItemEventArgs creatingVisualListItemEventArgs)
    {
      this.notificationContext = PopupEditorNotificationData.Context.CreatingVisualItem;
      this.creatingVisualListItemEventArgs = creatingVisualListItemEventArgs;
    }

    public PopupEditorNotificationData(KeyPressEventArgs keyPressEventArgs)
      : this()
    {
      this.notificationContext = PopupEditorNotificationData.Context.KeyPress;
      this.keyPressEventArgs = keyPressEventArgs;
    }

    public PopupEditorNotificationData(MouseEventArgs mouseEventArgs)
    {
      this.notificationContext = PopupEditorNotificationData.Context.MouseEvent;
      this.mouseEventArgs = mouseEventArgs;
    }

    public PopupEditorNotificationData(SortStyleChangedEventArgs sortStyleChanged)
    {
      this.notificationContext = PopupEditorNotificationData.Context.SortStyleChanged;
      this.sortStyleChanged = sortStyleChanged;
    }

    public PopupEditorNotificationData(VisualItemFormattingEventArgs visualItemFormatting)
    {
      this.notificationContext = PopupEditorNotificationData.Context.VisualItemFormatting;
      this.visualItemFormatting = visualItemFormatting;
    }

    public PopupEditorNotificationData(KeyEventArgs keyEventArgs)
    {
      this.notificationContext = PopupEditorNotificationData.Context.KeyUpKeyDownPress;
      this.keyEventArgs = keyEventArgs;
    }

    public enum Context
    {
      None,
      SelectedIndexChanged,
      SelectedIndexChanging,
      SelectedValueChanged,
      ListItemDataBinding,
      ListItemDataBound,
      CreatingVisualItem,
      KeyPress,
      TextChanged,
      MouseEvent,
      SortStyleChanged,
      VisualItemFormatting,
      MouseWheel,
      TextBoxDoubleClick,
      MouseUpOnEditorElement,
      DisplayMemberChanged,
      ValueMemberChanged,
      F4Press,
      Esc,
      KeyUpKeyDownPress,
      ItemsChanged,
      ItemsClear,
      KeyDown,
      KeyUp,
      Clipboard,
    }
  }
}
