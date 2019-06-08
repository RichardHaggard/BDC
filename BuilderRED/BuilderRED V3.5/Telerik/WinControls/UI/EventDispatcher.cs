// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.EventDispatcher
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;

namespace Telerik.WinControls.UI
{
  public class EventDispatcher
  {
    public static readonly EventDispatcher Empty = (EventDispatcher) new EventDispatcher.EmptyEventDispatcher();
    public static readonly object CellBeginEdit = new object();
    public static readonly object CellClick = new object();
    public static readonly object CellClipboardCopy = new object();
    public static readonly object CellClipboardPaste = new object();
    public static readonly object CellEditorInitialized = new object();
    public static readonly object CellEndEdit = new object();
    public static readonly object CellMouseMove = new object();
    public static readonly object CellPaint = new object();
    public static readonly object CellValidated = new object();
    public static readonly object CellValidating = new object();
    public static readonly object CellValueChanged = new object();
    public static readonly object CellValueNeeded = new object();
    public static readonly object CellValuePushed = new object();
    public static readonly object ChildViewExpanded = new object();
    public static readonly object ChildViewExpanding = new object();
    public static readonly object ColumnChooserItemElementCreating = new object();
    public static readonly object ColumnIndexChanged = new object();
    public static readonly object ColumnIndexChanging = new object();
    public static readonly object ColumnPropertyChanged = new object();
    public static readonly object ColumnWidthChanged = new object();
    public static readonly object ColumnWidthChanging = new object();
    public static readonly object CommandCellClick = new object();
    public static readonly object ConditionalFormattingFormShown = new object();
    public static readonly object ContextMenuOpening = new object();
    public static readonly object Copying = new object();
    public static readonly object CreateCompositeFilterDialog = new object();
    public static readonly object CreateRowInfo = new object();
    public static readonly object CurrentCellChanged = new object();
    public static readonly object CurrentColumnChanged = new object();
    public static readonly object CurrentRowChanged = new object();
    public static readonly object CurrentRowChanging = new object();
    public static readonly object CustomFiltering = new object();
    public static readonly object CustomGrouping = new object();
    public static readonly object CustomSorting = new object();
    public static readonly object DataBindingComplete = new object();
    public static readonly object DataError = new object();
    public static readonly object DefaultValuesNeeded = new object();
    public static readonly object EditorRequired = new object();
    public static readonly object ExpressionEditorFormCreated = new object();
    public static readonly object FilterChangedEvent = new object();
    public static readonly object FilterChangingEvent = new object();
    public static readonly object FilterExpressionChanged = new object();
    public static readonly object FilterPopupInitialized = new object();
    public static readonly object FilterPopupRequired = new object();
    public static readonly object GroupByChanged = new object();
    public static readonly object GroupByChanging = new object();
    public static readonly object GroupExpanded = new object();
    public static readonly object GroupExpanding = new object();
    public static readonly object GroupSummaryEvaluate = new object();
    public static readonly object HeaderCellToggleStateChanged = new object();
    public static readonly object HyperlinkOpened = new object();
    public static readonly object HyperlinkOpening = new object();
    public static readonly object InvalidateRow = new object();
    public static readonly object LayoutLoaded = new object();
    public static readonly object PageChanged = new object();
    public static readonly object PageChanging = new object();
    public static readonly object Pasting = new object();
    public static readonly object PositionChanged = new object();
    public static readonly object PositionChanging = new object();
    public static readonly object RowHeightChanged = new object();
    public static readonly object RowHeightChanging = new object();
    public static readonly object RowMouseMove = new object();
    public static readonly object RowPaint = new object();
    public static readonly object RowPropertyChanged = new object();
    public static readonly object RowsChanged = new object();
    public static readonly object RowsChanging = new object();
    public static readonly object RowSourceNeeded = new object();
    public static readonly object RowValidated = new object();
    public static readonly object RowValidating = new object();
    public static readonly object SelectionChanged = new object();
    public static readonly object SelectionChanging = new object();
    public static readonly object SortChangedEvent = new object();
    public static readonly object SortChangingEvent = new object();
    public static readonly object ToolTipTextNeeded = new object();
    public static readonly object UserAddedRow = new object();
    public static readonly object UserAddingRow = new object();
    public static readonly object UserChangedCurrentRow = new object();
    public static readonly object UserDeletedRow = new object();
    public static readonly object UserDeletingRow = new object();
    public static readonly object ValueChanged = new object();
    public static readonly object ValueChanging = new object();
    public static readonly object ViewColumnsChanged = new object();
    private Dictionary<object, Delegate> events = new Dictionary<object, Delegate>(128);
    private bool isSuspended;
    private List<object> suspendedEvents;

    public bool IsSuspended
    {
      get
      {
        return this.isSuspended;
      }
    }

    public void SuspendNotifications()
    {
      this.isSuspended = true;
    }

    public void ResumeNotifications()
    {
      this.isSuspended = false;
    }

    public void SuspendEvent(object key)
    {
      if (this.suspendedEvents == null)
        this.suspendedEvents = new List<object>();
      if (this.suspendedEvents.Contains(key))
        return;
      this.suspendedEvents.Add(key);
    }

    public void ResumeEvent(object key)
    {
      if (this.suspendedEvents == null)
        return;
      this.suspendedEvents.Remove(key);
    }

    public virtual void RaiseEvent<T>(object eventKey, object sender, T args) where T : EventArgs
    {
      if (this.isSuspended || this.suspendedEvents != null && this.suspendedEvents.Contains(eventKey))
        return;
      Delegate @delegate = (Delegate) null;
      this.events.TryGetValue(eventKey, out @delegate);
      if ((object) @delegate == null)
        return;
      EventHandler<T> eventHandler = @delegate as EventHandler<T>;
      if (eventHandler == null)
        throw new InvalidCastException(string.Format("The eventlistener is from type '{0}', but is raised as type '{1}'.", (object) @delegate.GetType(), (object) typeof (EventHandler<T>)));
      eventHandler(sender, args);
    }

    public virtual void AddListener<T>(object eventKey, EventHandler<T> eventRaisingMethod) where T : EventArgs
    {
      Delegate @delegate = (Delegate) null;
      this.events.TryGetValue(eventKey, out @delegate);
      if ((object) @delegate != null)
      {
        EventHandler<T> eventHandler1 = @delegate as EventHandler<T>;
        if (eventHandler1 == null)
          throw new InvalidCastException(string.Format("The eventlistener is from type '{0}', but is raised as type '{1}'.", (object) eventHandler1.GetType(), (object) typeof (EventHandler<T>)));
        EventHandler<T> eventHandler2 = eventHandler1 + eventRaisingMethod;
        this.events[eventKey] = (Delegate) eventHandler2;
      }
      else
        this.events.Add(eventKey, (Delegate) eventRaisingMethod);
    }

    public virtual void RemoveListener<T>(object eventKey, EventHandler<T> eventRaisingMethod) where T : EventArgs
    {
      Delegate @delegate = (Delegate) null;
      this.events.TryGetValue(eventKey, out @delegate);
      if ((object) @delegate == null)
        return;
      EventHandler<T> eventHandler1 = @delegate as EventHandler<T>;
      if (eventHandler1 == null)
        throw new InvalidCastException(string.Format("The eventlistener is from type '{0}', but is raised as type '{1}'.", (object) eventHandler1.GetType(), (object) typeof (EventHandler<T>)));
      EventHandler<T> eventHandler2 = eventHandler1 - eventRaisingMethod;
      this.events[eventKey] = (Delegate) eventHandler2;
    }

    public void RemoveListenersByKey(object eventKey)
    {
      if (!this.events.ContainsKey(eventKey))
        return;
      this.events.Remove(eventKey);
    }

    public void ClearListeners()
    {
      this.events.Clear();
    }

    private class EmptyEventDispatcher : EventDispatcher
    {
      public override void RaiseEvent<T>(object eventKey, object sender, T args)
      {
      }

      public override void RemoveListener<T>(object eventKey, EventHandler<T> eventRaisingMethod)
      {
      }

      public override void AddListener<T>(object eventKey, EventHandler<T> eventRaisingMethod)
      {
      }
    }
  }
}
