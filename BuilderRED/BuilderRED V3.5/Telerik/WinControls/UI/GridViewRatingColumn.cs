// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewRatingColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;

namespace Telerik.WinControls.UI
{
  public class GridViewRatingColumn : GridViewDataColumn
  {
    public static RadProperty MinimumProperty = RadProperty.Register(nameof (Minimum), typeof (double), typeof (GridViewRatingColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.0));
    public static RadProperty MaximumProperty = RadProperty.Register(nameof (Maximum), typeof (double), typeof (GridViewRatingColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 100.0));
    public static RadProperty ShouldPaintHoverProperty = RadProperty.Register(nameof (ShouldPaintHover), typeof (bool), typeof (GridViewRatingColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    public static RadProperty SelectionModeProperty = RadProperty.Register(nameof (SelectionMode), typeof (RatingSelectionMode), typeof (GridViewRatingColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RatingSelectionMode.Precise));
    public new static RadProperty ReadOnlyProperty = RadProperty.Register(nameof (ReadOnly), typeof (bool), typeof (GridViewRatingColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty DirectionProperty = RadProperty.Register(nameof (Direction), typeof (RatingDirection), typeof (GridViewRatingColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) RatingDirection.Standard));
    public static RadProperty ToolTipPrecisionProperty = RadProperty.Register(nameof (ToolTipPrecision), typeof (double), typeof (GridViewRatingColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.1));
    public static RadProperty PercentageRoundingProperty = RadProperty.Register(nameof (PercentageRounding), typeof (double), typeof (GridViewRatingColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.5));
    public static RadProperty ToolTipFormatStringProperty = RadProperty.Register(nameof (ToolTipFormatString), typeof (string), typeof (GridViewRatingColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) "{0:0.0}"));

    public GridViewRatingColumn()
    {
      this.InitProperties();
    }

    public GridViewRatingColumn(string fieldName)
      : base(fieldName)
    {
      this.InitProperties();
    }

    public GridViewRatingColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
      this.InitProperties();
    }

    private void InitProperties()
    {
      int num1 = (int) this.SetDefaultValueOverride(GridViewColumn.WidthProperty, (object) 102);
      int num2 = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeProperty, (object) typeof (double));
    }

    [DefaultValue(typeof (double))]
    public override Type DataType
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

    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets a value indicating the maximum value for the Rating.")]
    [Category("Data")]
    [DefaultValue(typeof (double), "100")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public double Maximum
    {
      get
      {
        return (double) this.GetValue(GridViewRatingColumn.MaximumProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewRatingColumn.MaximumProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Data")]
    [DefaultValue(typeof (double), "0")]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets a value indicating the minimum value for the Rating.")]
    public double Minimum
    {
      get
      {
        return (double) this.GetValue(GridViewRatingColumn.MinimumProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewRatingColumn.MinimumProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Description("Gets or sets whether the Hover layer should be applied.")]
    public bool ShouldPaintHover
    {
      get
      {
        return (bool) this.GetValue(GridViewRatingColumn.ShouldPaintHoverProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewRatingColumn.ShouldPaintHoverProperty, (object) value);
      }
    }

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [DefaultValue(RatingSelectionMode.Precise)]
    [Description("Gets or sets the selection mode of the rating control (full item, half item, precise selection).")]
    public RatingSelectionMode SelectionMode
    {
      get
      {
        return (RatingSelectionMode) this.GetValue(GridViewRatingColumn.SelectionModeProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewRatingColumn.SelectionModeProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets a value indicating whether the rating element in the cell is read-only. When rating element is read only user is not able to change the value with mouse.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public new bool ReadOnly
    {
      get
      {
        return (bool) this.GetValue(GridViewRatingColumn.ReadOnlyProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewRatingColumn.ReadOnlyProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [DefaultValue(RatingDirection.Standard)]
    [Description("Gets or sets the direction of rating element paint (Standard, Reversed).")]
    [Browsable(true)]
    public RatingDirection Direction
    {
      get
      {
        return (RatingDirection) this.GetValue(GridViewRatingColumn.DirectionProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewRatingColumn.DirectionProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("Gets or sets the tool tip precision.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(0.1)]
    public double ToolTipPrecision
    {
      get
      {
        return (double) this.GetValue(GridViewRatingColumn.ToolTipPrecisionProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewRatingColumn.ToolTipPrecisionProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Description("Gets or sets the percentage rounding.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(0.5)]
    [Browsable(true)]
    public double PercentageRounding
    {
      get
      {
        return (double) this.GetValue(GridViewRatingColumn.PercentageRoundingProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewRatingColumn.PercentageRoundingProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue("{0:0.0}")]
    [Description("Gets or sets the tool tip format string.")]
    public string ToolTipFormatString
    {
      get
      {
        return (string) this.GetValue(GridViewRatingColumn.ToolTipFormatStringProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewRatingColumn.ToolTipFormatStringProperty, (object) value);
      }
    }

    public override Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewDataRowInfo)
        return typeof (GridRatingCellElement);
      return base.GetCellType(row);
    }

    public override Type GetDefaultEditorType()
    {
      return typeof (GridSpinEditor);
    }
  }
}
