// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCheckBoxColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.Design;

namespace Telerik.WinControls.UI
{
  public class GridViewCheckBoxColumn : GridViewDataColumn
  {
    public static RadProperty ThreeStateProperty = RadProperty.Register(nameof (ThreeState), typeof (bool), typeof (GridViewCheckBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty EnableHeaderCheckBoxProperty = RadProperty.Register(nameof (EnableHeaderCheckBox), typeof (bool), typeof (GridViewCheckBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty ShouldCheckDataRowsProperty = RadProperty.Register(nameof (ShouldCheckDataRows), typeof (bool), typeof (GridViewCheckBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    public static RadProperty CheckedProperty = RadProperty.Register(nameof (Checked), typeof (Telerik.WinControls.Enumerations.ToggleState), typeof (GridViewCheckBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) Telerik.WinControls.Enumerations.ToggleState.Off));
    public static RadProperty HeaderCheckBoxPositionProperty = RadProperty.Register(nameof (HeaderCheckBoxPosition), typeof (HorizontalAlignment), typeof (GridViewCheckBoxColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) HorizontalAlignment.Center));
    public static RadProperty HeaderCheckBoxAlignmentProperty = RadProperty.Register(nameof (HeaderCheckBoxAlignment), typeof (ContentAlignment), typeof (RadCheckBoxElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout | ElementPropertyOptions.AffectsMeasure | ElementPropertyOptions.AffectsDisplay));

    public GridViewCheckBoxColumn()
    {
      this.InitProperties();
    }

    public GridViewCheckBoxColumn(string fieldName)
      : base(fieldName)
    {
      this.InitProperties();
    }

    public GridViewCheckBoxColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
      this.InitProperties();
    }

    protected override int GetDefaultMinWidth()
    {
      return 20;
    }

    private void InitProperties()
    {
      int num1 = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeProperty, (object) typeof (bool));
      int num2 = (int) this.SetDefaultValueOverride(GridViewColumn.MinWidthProperty, (object) 20);
      int num3 = (int) this.SetDefaultValueOverride(GridViewColumn.ShowEditorProperty, (object) true);
      int num4 = (int) this.SetDefaultValueOverride(GridViewCheckBoxColumn.EnableHeaderCheckBoxProperty, (object) false);
      this.EditMode = EditMode.OnValidate;
    }

    public override IInputEditor GetDefaultEditor()
    {
      return (IInputEditor) new RadCheckBoxEditor();
    }

    public override void InitializeEditor(IInputEditor editor)
    {
    }

    public override System.Type GetDefaultEditorType()
    {
      return typeof (RadCheckBoxEditor);
    }

    public override System.Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewFilteringRowInfo)
        return typeof (GridFilterCheckBoxCellElement);
      if (row is GridViewTableHeaderRowInfo)
        return typeof (GridCheckBoxHeaderCellElement);
      if (row is GridViewDataRowInfo || row is GridViewNewRowInfo)
        return typeof (GridCheckBoxCellElement);
      return base.GetCellType(row);
    }

    [RadPropertyDefaultValue("CheckAlignment", typeof (RadCheckBoxElement))]
    [Description("Gets or sets a value indicating the alignment of the check box to the text.")]
    public ContentAlignment HeaderCheckBoxAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(GridViewCheckBoxColumn.HeaderCheckBoxAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCheckBoxColumn.HeaderCheckBoxAlignmentProperty, (object) value);
      }
    }

    [Description("Gets or sets a value indicating the position of the CheckBoxElement along with the text.")]
    [DefaultValue(HorizontalAlignment.Center)]
    [Browsable(true)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public HorizontalAlignment HeaderCheckBoxPosition
    {
      get
      {
        return (HorizontalAlignment) this.GetValue(GridViewCheckBoxColumn.HeaderCheckBoxPositionProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCheckBoxColumn.HeaderCheckBoxPositionProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether to use a three state checkbox.")]
    [Category("Data")]
    public bool ThreeState
    {
      get
      {
        return (bool) this.GetValue(GridViewCheckBoxColumn.ThreeStateProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCheckBoxColumn.ThreeStateProperty, (object) value);
      }
    }

    [DefaultValue(typeof (bool))]
    public override System.Type DataType
    {
      get
      {
        return base.DataType;
      }
      set
      {
        base.DataType = value;
      }
    }

    [Description("Gets or sets a value indicating whether to show embedded CheckBox in header cell.")]
    [DefaultValue(false)]
    public bool EnableHeaderCheckBox
    {
      get
      {
        return (bool) this.GetValue(GridViewCheckBoxColumn.EnableHeaderCheckBoxProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCheckBoxColumn.EnableHeaderCheckBoxProperty, (object) value);
      }
    }

    [Description("Gets a value indicating whether the CheckBox in header cell checked.")]
    [DefaultValue(Telerik.WinControls.Enumerations.ToggleState.Off)]
    public Telerik.WinControls.Enumerations.ToggleState Checked
    {
      get
      {
        return (Telerik.WinControls.Enumerations.ToggleState) this.GetValue(GridViewCheckBoxColumn.CheckedProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCheckBoxColumn.CheckedProperty, (object) value);
      }
    }

    [DefaultValue(true)]
    [Description("This property determines whether the CheckBox in the header cell will be synced with the data cells. When true, the header check box will check the data cells and vice versa")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ShouldCheckDataRows
    {
      get
      {
        return (bool) this.GetValue(GridViewCheckBoxColumn.ShouldCheckDataRowsProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCheckBoxColumn.ShouldCheckDataRowsProperty, (object) value);
      }
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description("This property determines whether changing a value of a check box will immediately be send to the cell (OnValueChange) or when the current cell is changed or the grid is being validated (OnCellChangeOrValidating)")]
    [DefaultValue(EditMode.OnValidate)]
    public EditMode EditMode { get; set; }
  }
}
