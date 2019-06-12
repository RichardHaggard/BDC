// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewEditManager
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.WinControls.Data;

namespace Telerik.WinControls.UI
{
  public class GridViewEditManager : IEditorManager, IDisposable, IGridViewEventListener
  {
    private List<Type> permanentEditors = new List<Type>();
    private Dictionary<Type, IInputEditor> cachedEditors = new Dictionary<Type, IInputEditor>();
    private bool allowEditMode = true;
    private int oldRowHeight = -1;
    private RadGridViewElement gridViewElement;
    private IInputEditor activeEditor;
    private bool closeEditorWhenValidationFails;
    private bool wasInEditMode;
    private bool endEditCore;
    private bool oldRowIsNewRow;
    private bool closeEditorWhenValidationFailsOriginalValue;
    private bool restoreCloseEditorWhenValidationFailsValue;
    private bool isValidating;
    private bool editModeInitialized;

    public GridViewEditManager(RadGridViewElement gridViewElement)
    {
      this.gridViewElement = gridViewElement;
      this.gridViewElement.Template.SynchronizationService.AddListener((IGridViewEventListener) this);
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public IInputEditor ActiveEditor
    {
      get
      {
        return this.activeEditor;
      }
      protected internal set
      {
        this.activeEditor = value;
        if (this.activeEditor == null || this.activeEditor.EditorManager != null)
          return;
        this.activeEditor.EditorManager = (IEditorManager) this;
      }
    }

    public RadGridViewElement GridViewElement
    {
      get
      {
        return this.gridViewElement;
      }
    }

    public bool IsInEditMode
    {
      get
      {
        return this.activeEditor != null;
      }
    }

    internal bool WasInEditMode
    {
      get
      {
        return this.wasInEditMode;
      }
      set
      {
        this.wasInEditMode = value;
      }
    }

    internal bool IsValidating
    {
      get
      {
        return this.isValidating;
      }
      set
      {
        this.isValidating = false;
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    internal bool AllowEditMode
    {
      get
      {
        return this.allowEditMode;
      }
      set
      {
        this.allowEditMode = value;
      }
    }

    public bool CloseEditorWhenValidationFails
    {
      get
      {
        return this.closeEditorWhenValidationFails;
      }
      set
      {
        this.closeEditorWhenValidationFails = value;
      }
    }

    public virtual bool BeginEdit()
    {
      GridCellElement currentCell = this.gridViewElement.CurrentView.CurrentCell;
      IEditableCell editableCell = currentCell as IEditableCell;
      if (this.IsInEditMode || editableCell == null || (!editableCell.IsEditable || currentCell == null) || (currentCell.RowInfo == null || currentCell.ViewInfo.ChildRows.Count == 0 && currentCell.RowInfo is GridViewDataRowInfo))
        return false;
      bool flag = this.IsPermanentEditor(currentCell.ColumnInfo.GetDefaultEditorType());
      this.editModeInitialized = false;
      IInputEditor inputEditor1 = flag ? editableCell.Editor : this.GetDefaultEditor((IEditorProvider) currentCell.ColumnInfo);
      if (inputEditor1 == null)
        return false;
      this.ActiveEditor = inputEditor1;
      GridViewCellCancelEventArgs args = new GridViewCellCancelEventArgs(currentCell, this.activeEditor);
      this.gridViewElement.Template.EventDispatcher.RaiseEvent<GridViewCellCancelEventArgs>(EventDispatcher.CellBeginEdit, (object) this, args);
      if (args.Cancel)
      {
        this.ActiveEditor = (IInputEditor) null;
        return false;
      }
      this.gridViewElement.CurrentView.EnsureCellVisible(currentCell.RowInfo, currentCell.ColumnInfo);
      this.gridViewElement.UpdateLayout();
      if (this.gridViewElement.CurrentCell == null || this.gridViewElement.CurrentCell.RowInfo == null)
        return false;
      if (currentCell != this.gridViewElement.CurrentCell || this.ActiveEditor == null)
      {
        currentCell = (GridCellElement) this.gridViewElement.CurrentCell;
        editableCell = currentCell as IEditableCell;
        IInputEditor inputEditor2 = this.IsPermanentEditor(currentCell.ColumnInfo.GetDefaultEditorType()) ? editableCell.Editor : this.GetDefaultEditor((IEditorProvider) currentCell.ColumnInfo);
        if (inputEditor2 == null)
          return false;
        this.ActiveEditor = inputEditor2;
      }
      if (currentCell.RowInfo is GridViewNewRowInfo)
        currentCell.RowInfo.CallOnBeginEdit();
      editableCell.AddEditor(this.activeEditor);
      BaseGridEditor activeEditor = this.ActiveEditor as BaseGridEditor;
      activeEditor?.SetIsInBeginEditMode(true);
      currentCell.UpdateLayout();
      if (this.gridViewElement.AutoSizeRows)
      {
        GridViewRowInfo rowInfo = currentCell.RowInfo;
        this.oldRowHeight = currentCell.RowInfo.Height;
        rowInfo.SuspendPropertyNotifications();
        rowInfo.Height = (int) currentCell.RowElement.DesiredSize.Height;
        rowInfo.ResumePropertyNotifications();
        currentCell.TableElement.ViewElement.UpdateRows();
      }
      this.InitializeEditor(this.activeEditor);
      activeEditor?.SetIsInBeginEditMode(false);
      this.activeEditor.BeginEdit();
      currentCell.RowElement.UpdateInfo();
      this.editModeInitialized = true;
      return true;
    }

    public virtual bool EndEdit()
    {
      bool flag = false;
      if (this.EndEditCore(true, false))
      {
        flag = true;
        GridViewRowInfo currentRow = this.gridViewElement.Template.CurrentRow;
        if (currentRow == null)
          return true;
        if (currentRow is GridViewNewRowInfo)
          flag = this.FinishEditingOperation();
        else if (currentRow.IsModified)
        {
          this.gridViewElement.Template.ListSource.BeginUpdate();
          flag = currentRow.CallOnEndEdit();
          this.gridViewElement.Template.ListSource.EndUpdate(false);
          currentRow.IsModified = false;
          if (currentRow.ViewTemplate.EnableCustomGrouping && currentRow.ViewTemplate.GroupDescriptors.Count > 0)
            currentRow.ViewTemplate.Refresh();
        }
      }
      return flag;
    }

    public virtual bool CloseEditor()
    {
      return this.EndEditCore(true, false);
    }

    public virtual bool CancelEdit()
    {
      if (!this.endEditCore)
        return this.EndEditCore(false, true);
      this.closeEditorWhenValidationFailsOriginalValue = this.CloseEditorWhenValidationFails;
      this.restoreCloseEditorWhenValidationFailsValue = true;
      this.CloseEditorWhenValidationFails = true;
      return true;
    }

    public virtual bool IsPermanentEditor(Type editorType)
    {
      if ((object) editorType != null)
        return this.permanentEditors.Contains(editorType);
      return false;
    }

    protected virtual void OnPositionChanging(PositionChangingEventArgs args)
    {
      GridViewRowInfo currentRow = this.gridViewElement.Template.CurrentRow;
      GridViewColumn currentColumn = currentRow?.ViewTemplate.CurrentColumn;
      this.oldRowIsNewRow = false;
      if (this.IsInEditMode)
      {
        if (!this.endEditCore)
          args.Cancel = !this.CloseEditor();
        this.wasInEditMode = this.GridViewElement.BeginEditMode != RadGridViewBeginEditMode.BeginEditProgrammatically;
      }
      else
      {
        object oldValue = currentRow == null || currentColumn == null ? (object) null : currentRow[currentColumn];
        object newValue = oldValue;
        if (this.RaiseCellValidatingEvent(currentRow, currentColumn, newValue, oldValue))
        {
          args.Cancel = true;
          return;
        }
        this.RaiseCellValidatedEvent(currentRow, currentColumn, newValue);
      }
      if (!args.Cancel && args.Row != currentRow)
      {
        args.Cancel = !this.FinishEditingOperation();
        this.oldRowIsNewRow = currentRow is GridViewNewRowInfo;
      }
      if (!args.Cancel)
        return;
      this.wasInEditMode = false;
    }

    protected virtual void OnPositionChanged(PositionChangedEventArgs args)
    {
      if (!this.allowEditMode)
      {
        this.wasInEditMode = false;
        this.allowEditMode = true;
      }
      if (!this.wasInEditMode)
        return;
      this.wasInEditMode = false;
      if (this.oldRowIsNewRow || args.Row == null || (args.Column == null || this.GridViewElement.CurrentView == null))
        return;
      this.GridViewElement.Template.SynchronizationService.DispatchEvent(new GridViewEvent((object) this, (object) this, new object[2]
      {
        (object) args.Row,
        (object) args.Column
      }, new GridViewEventInfo(KnownEvents.BeginEdit, GridEventType.Data, GridEventDispatchMode.Send)));
    }

    GridEventType IGridViewEventListener.DesiredEvents
    {
      get
      {
        return GridEventType.Data;
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
        return GridEventProcessMode.AllProcess;
      }
    }

    GridViewEventResult IGridViewEventListener.PreProcessEvent(
      GridViewEvent eventData)
    {
      if (eventData.Info.Id != KnownEvents.CurrentChanged)
        return (GridViewEventResult) null;
      if (eventData.Originator is RadDataView<GridViewRowInfo>)
      {
        if (this.IsInEditMode)
          return new GridViewEventResult(true, true);
        return (GridViewEventResult) null;
      }
      PositionChangingEventArgs args = new PositionChangingEventArgs(eventData.Arguments[0] as GridViewRowInfo, eventData.Arguments[1] as GridViewColumn);
      this.OnPositionChanging(args);
      if (args.Cancel)
        return new GridViewEventResult(false, true);
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.ProcessEvent(
      GridViewEvent eventData)
    {
      if (eventData.Info.Id != KnownEvents.CurrentChanged)
        return (GridViewEventResult) null;
      this.OnPositionChanged(new PositionChangedEventArgs(eventData.Arguments[0] as GridViewRowInfo, eventData.Arguments[1] as GridViewColumn));
      return (GridViewEventResult) null;
    }

    GridViewEventResult IGridViewEventListener.PostProcessEvent(
      GridViewEvent eventData)
    {
      if (eventData.Info.Id == KnownEvents.BeginEdit)
      {
        this.GridViewElement.CurrentView.EnsureCellVisible(eventData.Arguments[0] as GridViewRowInfo, eventData.Arguments[1] as GridViewColumn);
        this.GridViewElement.UpdateLayout();
        this.BeginEdit();
      }
      return (GridViewEventResult) null;
    }

    bool IGridViewEventListener.AnalyzeQueue(List<GridViewEvent> events)
    {
      return false;
    }

    public virtual IInputEditor GetDefaultEditor(IEditorProvider provider)
    {
      if (provider == null)
        return (IInputEditor) null;
      EditorRequiredEventArgs args = new EditorRequiredEventArgs(provider.GetDefaultEditorType());
      this.gridViewElement.Template.EventDispatcher.RaiseEvent<EditorRequiredEventArgs>(EventDispatcher.EditorRequired, (object) this, args);
      bool flag = this.IsPermanentEditor(args.EditorType);
      if ((object) args.EditorType == null || flag)
        return (IInputEditor) null;
      if (args.Editor is IInputEditor)
      {
        IInputEditor editor = args.Editor as IInputEditor;
        editor.EditorManager = (IEditorManager) this;
        return editor;
      }
      IInputEditor editor1 = (IInputEditor) null;
      if (!this.cachedEditors.TryGetValue(args.EditorType, out editor1))
      {
        editor1 = Activator.CreateInstance(args.EditorType) as IInputEditor;
      }
      else
      {
        RadItem editorElement = this.GetEditorElement(editor1);
        if (editorElement != null && editorElement.IsDisposed)
        {
          this.cachedEditors.Remove(args.EditorType);
          editor1 = Activator.CreateInstance(args.EditorType) as IInputEditor;
        }
      }
      if (editor1 != null && !this.cachedEditors.ContainsValue(editor1))
        this.cachedEditors.Add(args.EditorType, editor1);
      editor1.EditorManager = (IEditorManager) this;
      return editor1;
    }

    public void RegisterPermanentEditorType(Type editor)
    {
      if (this.permanentEditors.Contains(editor))
        return;
      this.permanentEditors.Add(editor);
    }

    public void Dispose()
    {
      this.gridViewElement.Template.SynchronizationService.RemoveListener((IGridViewEventListener) this);
      this.permanentEditors.Clear();
      foreach (KeyValuePair<Type, IInputEditor> cachedEditor in this.cachedEditors)
      {
        IInputEditor inputEditor = cachedEditor.Value;
        BaseGridEditor baseGridEditor = inputEditor as BaseGridEditor;
        (baseGridEditor == null ? inputEditor as IDisposable : (IDisposable) baseGridEditor.EditorElement)?.Dispose();
      }
      this.cachedEditors.Clear();
    }

    protected virtual void InitializeEditor(IInputEditor activeEditor)
    {
      GridCellElement currentCell = this.gridViewElement.CurrentView.CurrentCell;
      if (currentCell == null)
        return;
      ISupportInitialize supportInitialize = activeEditor as ISupportInitialize;
      supportInitialize?.BeginInit();
      EventDispatcher eventDispatcher = this.GridViewElement.Template.EventDispatcher;
      eventDispatcher.SuspendEvent(EventDispatcher.ValueChanging);
      eventDispatcher.SuspendEvent(EventDispatcher.ValueChanged);
      currentCell.ColumnInfo.InitializeEditor(activeEditor);
      activeEditor.Initialize((object) currentCell, currentCell.Value);
      eventDispatcher.ResumeEvent(EventDispatcher.ValueChanging);
      eventDispatcher.ResumeEvent(EventDispatcher.ValueChanged);
      GridViewCellEventArgs args = new GridViewCellEventArgs(currentCell.RowInfo, currentCell.ColumnInfo, activeEditor);
      this.gridViewElement.Template.EventDispatcher.RaiseEvent<GridViewCellEventArgs>(EventDispatcher.CellEditorInitialized, (object) this, args);
      if (TelerikHelper.IsMaterialTheme(this.gridViewElement.GridControl.ThemeName))
      {
        BaseInputEditor activeEditor1 = args.ActiveEditor as BaseInputEditor;
        if (activeEditor1 != null)
          activeEditor1.EditorElement.StretchVertically = true;
      }
      supportInitialize?.EndInit();
    }

    protected virtual bool EndEditCore(bool validate, bool cancel)
    {
      if (this.endEditCore || !this.IsInEditMode)
      {
        this.RestoreValidationFlag();
        return false;
      }
      this.endEditCore = true;
      GridCellElement currentCell = this.gridViewElement.CurrentView.CurrentCell;
      if (currentCell == null)
      {
        this.RestoreValidationFlag();
        return false;
      }
      GridViewDataColumn columnInfo = currentCell.ColumnInfo as GridViewDataColumn;
      RadItem activeEditor1 = this.activeEditor as RadItem;
      GridCellElement gridCellElement = (GridCellElement) null;
      if (activeEditor1 != null)
      {
        gridCellElement = activeEditor1.FindAncestor<GridCellElement>();
      }
      else
      {
        BaseGridEditor activeEditor2 = this.activeEditor as BaseGridEditor;
        if (activeEditor2 != null)
          gridCellElement = activeEditor2.EditorElement.FindAncestor<GridCellElement>();
      }
      if (gridCellElement != null && gridCellElement.ColumnInfo != null && gridCellElement.ColumnInfo != currentCell.ColumnInfo)
      {
        cancel = true;
        validate = false;
      }
      object result = this.activeEditor.Value;
      if (columnInfo is GridViewDecimalColumn && !RadDataConverter.Instance.EqualsNullValue(result, (IDataConversionInfoProvider) columnInfo) && (result != null && (object) result.GetType() != (object) columnInfo.DataType))
        RadDataConverter.Instance.TryFormat(result, columnInfo.DataType, (IDataConversionInfoProvider) columnInfo, out result);
      if (object.Equals(result, (object) string.Empty))
        result = (object) null;
      bool flag1 = this.activeEditor.IsModified && this.editModeInitialized;
      bool flag2 = true;
      object dataSource = this.GridViewElement.Template.DataSource;
      if (validate)
      {
        GridTableElement tableElement = currentCell.TableElement;
        tableElement.BeginUpdate();
        flag2 = !this.RaiseCellValidatingEvent(currentCell.RowInfo, currentCell.ColumnInfo, result, currentCell.Value);
        tableElement.EndUpdate(false);
      }
      if (!flag2 && !this.CloseEditorWhenValidationFails)
      {
        this.endEditCore = false;
        this.RestoreValidationFlag();
        BaseGridEditor activeEditor2 = this.activeEditor as BaseGridEditor;
        (activeEditor2 != null ? activeEditor2.EditorElement : this.activeEditor as RadElement)?.FindDescendant<RadTextBoxItem>()?.HostedControl.Focus();
        return flag2;
      }
      if (cancel || !flag2)
      {
        object obj = currentCell.Value;
        BaseGridEditor activeEditor2 = this.activeEditor as BaseGridEditor;
        if (activeEditor2 != null)
          obj = RadDataConverter.Instance.Format(obj, activeEditor2.DataType, (IDataConversionInfoProvider) columnInfo);
        this.activeEditor.Value = obj;
      }
      else if (!flag1 && columnInfo.DataSourceNullValue != null && RadDataConverter.Instance.EqualsNullValue(result, (IDataConversionInfoProvider) columnInfo))
        flag1 = true;
      this.activeEditor.EndEdit();
      if (this.gridViewElement.AutoSizeRows)
      {
        GridViewRowInfo rowInfo = currentCell.RowElement.RowInfo;
        rowInfo.SuspendPropertyNotifications();
        rowInfo.Height = this.oldRowHeight;
        rowInfo.ResumePropertyNotifications();
        this.oldRowHeight = -1;
      }
      if (currentCell != null)
      {
        ((IEditableCell) currentCell).RemoveEditor(this.activeEditor);
        if (!this.isValidating && currentCell.ElementTree != null && currentCell.ElementTree.Control.ContainsFocus)
          currentCell.GridViewElement.Focus();
      }
      if (currentCell.RowInfo is GridViewNewRowInfo && currentCell.Value == null && result == DBNull.Value)
        flag1 = false;
      int num1 = 0;
      if (currentCell.ViewTemplate != null && currentCell.ViewTemplate.DataSource != null)
        num1 = currentCell.ViewTemplate.DataSource.GetHashCode();
      if (flag1 && !cancel && flag2)
      {
        this.gridViewElement.SuspendLayout(true);
        currentCell.Value = result;
        validate = currentCell.ViewTemplate != null && (currentCell.ViewTemplate.DataSource != null && num1 == currentCell.ViewTemplate.DataSource.GetHashCode() || currentCell.RowInfo is GridViewNewRowInfo || flag1);
        if (currentCell != null && currentCell.RowInfo != null)
        {
          currentCell.RowInfo.IsModified = true;
        }
        else
        {
          this.ActiveEditor = (IInputEditor) null;
          this.endEditCore = false;
          this.RestoreValidationFlag();
          this.gridViewElement.ResumeLayout(true, true);
          return true;
        }
      }
      int num2 = 0;
      if (currentCell.ViewTemplate != null && currentCell.ViewTemplate.DataSource != null)
        num2 = currentCell.ViewTemplate.DataSource.GetHashCode();
      if (validate && num1 == num2)
        this.RaiseCellValidatedEvent(currentCell.RowInfo, currentCell.ColumnInfo, currentCell.MasterTemplate.VirtualMode ? result : currentCell.Value);
      IInputEditor activeEditor3 = this.ActiveEditor;
      this.ActiveEditor = (IInputEditor) null;
      if (currentCell.RowElement != null)
        currentCell.RowElement.UpdateInfo();
      this.endEditCore = false;
      if (flag1 && !cancel && flag2)
        this.gridViewElement.ResumeLayout(true, true);
      if (this.GridViewElement.Template.DataSource != dataSource)
        this.GridViewElement.TableElement.Update(GridUINotifyAction.Reset);
      GridViewCellEventArgs args = new GridViewCellEventArgs(currentCell.RowInfo, currentCell.ColumnInfo, activeEditor3);
      this.gridViewElement.Template.EventDispatcher.RaiseEvent<GridViewCellEventArgs>(EventDispatcher.CellEndEdit, (object) this, args);
      this.RestoreValidationFlag();
      return true;
    }

    private bool FinishEditingOperation()
    {
      bool flag1 = false;
      GridViewRowInfo currentRow = this.gridViewElement.Template.CurrentRow;
      GridViewNewRowInfo gridViewNewRowInfo = currentRow as GridViewNewRowInfo;
      if (gridViewNewRowInfo != null && gridViewNewRowInfo.Validated)
      {
        gridViewNewRowInfo.Validated = false;
        return true;
      }
      bool flag2 = gridViewNewRowInfo != null;
      if (currentRow != null && (currentRow.IsAttached || flag2) && this.RaiseRowValidatingEvent(currentRow))
        return flag1;
      if (currentRow != null && (currentRow.IsAttached || flag2))
        this.RaiseRowValidatedEvent(currentRow);
      bool flag3 = true;
      if (flag2 && !currentRow.IsModified && (gridViewNewRowInfo.ViewTemplate != null && gridViewNewRowInfo.ViewTemplate.ListSource != null))
        gridViewNewRowInfo.ViewTemplate.ListSource.CreateATransactionForEveryValueSetting = true;
      if (currentRow == null || !currentRow.IsModified)
        return flag3;
      this.GridViewElement.Template.SynchronizationService.RemoveListener((IGridViewEventListener) this);
      try
      {
        if (flag2)
          flag3 = currentRow.CallOnEndEdit();
        if (flag3)
          currentRow.IsModified = false;
        return flag3;
      }
      finally
      {
        this.GridViewElement.Template.SynchronizationService.AddListener((IGridViewEventListener) this);
      }
    }

    private void RestoreValidationFlag()
    {
      if (!this.restoreCloseEditorWhenValidationFailsValue)
        return;
      this.closeEditorWhenValidationFails = this.closeEditorWhenValidationFailsOriginalValue;
      this.restoreCloseEditorWhenValidationFailsValue = false;
    }

    private bool RaiseCellValidatingEvent(
      GridViewRowInfo row,
      GridViewColumn column,
      object newValue,
      object oldValue)
    {
      CellValidatingEventArgs args = new CellValidatingEventArgs(row, column, newValue, oldValue, this.activeEditor);
      this.gridViewElement.Template.EventDispatcher.RaiseEvent<CellValidatingEventArgs>(EventDispatcher.CellValidating, (object) this, args);
      return args.Cancel;
    }

    private void RaiseCellValidatedEvent(GridViewRowInfo row, GridViewColumn column, object value)
    {
      CellValidatedEventArgs args = new CellValidatedEventArgs(row, column, value);
      this.gridViewElement.Template.EventDispatcher.RaiseEvent<CellValidatedEventArgs>(EventDispatcher.CellValidated, (object) this, args);
    }

    internal bool RaiseRowValidatingEvent(GridViewRowInfo row)
    {
      RowValidatingEventArgs args = new RowValidatingEventArgs(row);
      this.gridViewElement.Template.EventDispatcher.RaiseEvent<RowValidatingEventArgs>(EventDispatcher.RowValidating, (object) this, args);
      return args.Cancel;
    }

    internal void RaiseRowValidatedEvent(GridViewRowInfo row)
    {
      RowValidatedEventArgs args = new RowValidatedEventArgs(row);
      this.gridViewElement.Template.EventDispatcher.RaiseEvent<RowValidatedEventArgs>(EventDispatcher.RowValidated, (object) this, args);
    }

    private RadItem GetEditorElement(IInputEditor editor)
    {
      RadItem radItem = editor as RadItem;
      if (radItem == null)
      {
        BaseGridEditor baseGridEditor = editor as BaseGridEditor;
        if (baseGridEditor != null)
        {
          radItem = baseGridEditor.EditorElement as RadItem;
        }
        else
        {
          BaseInputEditor baseInputEditor = editor as BaseInputEditor;
          if (baseInputEditor != null)
            radItem = baseInputEditor.EditorElement as RadItem;
        }
      }
      return radItem;
    }
  }
}
