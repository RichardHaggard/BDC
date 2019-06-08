// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridFilterCheckBoxCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;
using Telerik.WinControls.Data;
using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class GridFilterCheckBoxCellElement : GridFilterCellElement
  {
    private bool threeState = true;
    private RadCheckBoxEditor checkBoxEditor;

    public GridFilterCheckBoxCellElement(GridViewDataColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.checkBoxEditor = new RadCheckBoxEditor();
      this.checkBoxEditor.EditorElement.Visibility = ElementVisibility.Collapsed;
      this.checkBoxEditor.EditorElement.StretchHorizontally = false;
      this.Children.Add(this.checkBoxEditor.EditorElement);
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      if (data is GridViewCheckBoxColumn)
        return context is GridFilterRowElement;
      return false;
    }

    protected void InitializeCheckBoxEditor()
    {
      if (this.DataColumnInfo != null && this.DataColumnInfo.AllowFiltering)
      {
        if (this.ThreeState)
          this.checkBoxEditor.EditorElement.Visibility = ElementVisibility.Visible;
        else
          this.UpdateEditorVisibility(this.Descriptor.Operator);
      }
      else
        this.checkBoxEditor.EditorElement.Visibility = ElementVisibility.Collapsed;
      this.checkBoxEditor.ThreeState = this.CheckBoxColumnThreeState || this.threeState;
    }

    public override IInputEditor Editor
    {
      get
      {
        return (IInputEditor) this.checkBoxEditor;
      }
    }

    protected override Type ThemeEffectiveType
    {
      get
      {
        return typeof (GridFilterCellElement);
      }
    }

    protected bool CheckBoxColumnThreeState
    {
      get
      {
        GridViewCheckBoxColumn columnInfo = this.ColumnInfo as GridViewCheckBoxColumn;
        if (columnInfo == null)
          return false;
        return columnInfo.ThreeState;
      }
    }

    public virtual bool ThreeState
    {
      get
      {
        if (!this.CheckBoxColumnThreeState)
          return this.threeState;
        return false;
      }
      set
      {
        if (value == this.threeState)
          return;
        if (this.CheckBoxColumnThreeState && !value)
          throw new InvalidOperationException("You cannot set the ThreeState property of the cell to false, when its column has threestate.");
        this.threeState = value;
        this.SetContent();
        this.OnNotifyPropertyChanged(nameof (ThreeState));
      }
    }

    private RadElement EditorElement
    {
      get
      {
        return this.checkBoxEditor.EditorElement;
      }
    }

    public override bool IsEditable
    {
      get
      {
        if (this.ThreeState)
          return true;
        return base.IsEditable;
      }
    }

    protected override void ArrangeEditorElement(
      RadElement element,
      RectangleF editorRect,
      RectangleF clientRect)
    {
      editorRect.X = (float) (((double) clientRect.Width - (double) editorRect.Width) / 2.0 + 1.0);
      base.ArrangeEditorElement(element, editorRect, clientRect);
    }

    public override void Attach(GridViewColumn data, object context)
    {
      base.Attach(data, context);
      if (this.RowElement == null)
        return;
      this.GridViewElement.EditorManager.RegisterPermanentEditorType(typeof (RadCheckBoxEditor));
    }

    protected override void SetContentCore(object value)
    {
      this.InitializeCheckBoxEditor();
      if (!this.ThreeState && this.GridViewElement.EditorManager.ActiveEditor != null && this.IsCurrent)
        return;
      FilterOperator filterOperator = FilterOperator.None;
      if (this.Descriptor != null)
        filterOperator = this.Descriptor.Operator;
      if (filterOperator == FilterOperator.IsNotNull || filterOperator == FilterOperator.IsNull)
        value = (object) null;
      this.checkBoxEditor.BeginInit();
      object obj = RadDataConverter.Instance.Format(value, this.checkBoxEditor.DataType, (IDataConversionInfoProvider) (this.ColumnInfo as GridViewDataColumn));
      if (obj != null)
        this.checkBoxEditor.Value = obj;
      this.checkBoxEditor.EndInit();
    }

    public override object Value
    {
      get
      {
        return base.Value;
      }
      set
      {
        if (this.ThreeState)
        {
          if ((ToggleState) value == ToggleState.Indeterminate)
            value = (object) null;
          else if (this.Descriptor != null && this.Descriptor.Operator == FilterOperator.None)
            this.Descriptor.Operator = FilterOperator.IsEqualTo;
        }
        base.Value = value;
      }
    }

    protected override bool SetFilterDescriptor(FilterDescriptor descriptor)
    {
      if (this.ThreeState)
        return base.SetFilterDescriptor(descriptor);
      if (!base.SetFilterDescriptor(descriptor))
        return false;
      this.UpdateEditorVisibility(descriptor != null ? descriptor.Operator : FilterOperator.None);
      return true;
    }

    protected override bool SetFilterOperator(FilterOperator filterOperator)
    {
      if (this.ThreeState)
        return base.SetFilterOperator(filterOperator);
      if (!base.SetFilterOperator(filterOperator))
        return false;
      if (filterOperator != FilterOperator.None)
        this.RowInfo.Cells[this.ColumnInfo.Name].Value = this.Editor.Value;
      this.UpdateEditorVisibility(filterOperator);
      return true;
    }

    protected override void SetSelectedFilterOperatorText()
    {
      if (this.ThreeState)
        return;
      base.SetSelectedFilterOperatorText();
    }

    private void UpdateEditorVisibility(FilterOperator filterOperator)
    {
      if (filterOperator == FilterOperator.None || filterOperator == FilterOperator.IsNotNull || (filterOperator == FilterOperator.IsNull || !this.DataColumnInfo.AllowFiltering))
        this.EditorElement.Visibility = ElementVisibility.Collapsed;
      else
        this.EditorElement.Visibility = ElementVisibility.Visible;
    }

    protected override void OnColumnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnColumnPropertyChanged(e);
      if (e.Property != GridViewCheckBoxColumn.ThreeStateProperty && e.Property != GridViewDataColumn.AllowFilteringProperty)
        return;
      this.SetContent();
    }
  }
}
