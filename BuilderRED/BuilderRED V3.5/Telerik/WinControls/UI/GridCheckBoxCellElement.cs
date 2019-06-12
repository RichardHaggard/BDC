// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridCheckBoxCellElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;

namespace Telerik.WinControls.UI
{
  public class GridCheckBoxCellElement : GridDataCellElement
  {
    private RadCheckBoxEditor checkBoxEditor;

    public GridCheckBoxCellElement(GridViewColumn column, GridRowElement row)
      : base(column, row)
    {
    }

    protected override void CreateChildElements()
    {
      base.CreateChildElements();
      this.checkBoxEditor = new RadCheckBoxEditor();
      this.Children.Add(this.checkBoxEditor.EditorElement);
    }

    public override void Initialize(GridViewColumn column, GridRowElement row)
    {
      this.SetThreeState(column);
      base.Initialize(column, row);
    }

    private void SetThreeState(GridViewColumn column)
    {
      GridViewCheckBoxColumn viewCheckBoxColumn = column as GridViewCheckBoxColumn;
      if (viewCheckBoxColumn == null)
        return;
      this.checkBoxEditor.ThreeState = viewCheckBoxColumn.ThreeState;
    }

    public override IInputEditor Editor
    {
      get
      {
        return (IInputEditor) this.checkBoxEditor;
      }
    }

    public override string ToolTipText
    {
      get
      {
        return base.ToolTipText;
      }
      set
      {
        base.ToolTipText = value;
        if (this.checkBoxEditor == null)
          return;
        this.checkBoxEditor.EditorElement.ToolTipText = value;
        if (this.checkBoxEditor.EditorElement.Children.Count <= 0)
          return;
        (this.checkBoxEditor.EditorElement.Children[0] as RadCheckmark).ToolTipText = value;
      }
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
      if (this.GridViewElement == null || this.GridViewElement.EditorManager.ActiveEditor != null && this.IsCurrent)
        return;
      this.checkBoxEditor.BeginInit();
      this.checkBoxEditor.Value = RadDataConverter.Instance.Format(value, this.checkBoxEditor.DataType, (IDataConversionInfoProvider) (this.ColumnInfo as GridViewDataColumn));
      this.checkBoxEditor.EndInit();
    }

    public override bool IsCompatible(GridViewColumn data, object context)
    {
      return data is GridViewCheckBoxColumn;
    }

    protected override void OnColumnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnColumnPropertyChanged(e);
      if (e.Property != GridViewCheckBoxColumn.ThreeStateProperty)
        return;
      this.SetThreeState(this.ColumnInfo);
      this.SetContent();
    }

    public override void AddEditor(IInputEditor editor)
    {
      base.AddEditor(editor);
      editor.ValueChanged += new EventHandler(this.editor_ValueChanged);
    }

    private void editor_ValueChanged(object sender, EventArgs e)
    {
      if (((GridViewCheckBoxColumn) this.ColumnInfo).EditMode != EditMode.OnValueChange || this.RowInfo is GridViewNewRowInfo)
        return;
      this.GridControl.EndEdit();
    }

    public override void RemoveEditor(IInputEditor editor)
    {
      editor.ValueChanged -= new EventHandler(this.editor_ValueChanged);
      base.RemoveEditor(editor);
    }
  }
}
