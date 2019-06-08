// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewImageColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridViewImageColumn : GridViewDataColumn
  {
    public static RadProperty ImageAlignmentProperty = RadProperty.Register(nameof (ImageAlignment), typeof (ContentAlignment), typeof (GridViewImageColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleCenter));

    public GridViewImageColumn()
    {
      int num = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeProperty, (object) typeof (Image));
    }

    public GridViewImageColumn(string fieldName)
      : base(fieldName)
    {
      int num = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeProperty, (object) typeof (Image));
    }

    public GridViewImageColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
      int num = (int) this.SetDefaultValueOverride(GridViewDataColumn.DataTypeProperty, (object) typeof (Image));
    }

    [DefaultValue(false)]
    [Description("This property is overriden to always return false since grouping by this column is not allowed.")]
    public override bool AllowGroup
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [DefaultValue(true)]
    [Description("This property is overriden to always return true since this column cannot be edited.")]
    public override bool ReadOnly
    {
      get
      {
        return true;
      }
      set
      {
      }
    }

    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(ContentAlignment.MiddleCenter)]
    [Description("Gets or sets the image alignment of the image inside the cells.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public virtual ContentAlignment ImageAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(GridViewImageColumn.ImageAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewImageColumn.ImageAlignmentProperty, (object) value);
      }
    }

    [DefaultValue(typeof (Image))]
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

    [Description("Grouping by this type of column cannot be performed. That method returns string.empty")]
    public override string GetDefaultGroupByExpression()
    {
      return string.Empty;
    }

    public override Type GetCellType(GridViewRowInfo row)
    {
      if (row is GridViewDataRowInfo || row is GridViewNewRowInfo)
        return typeof (GridImageCellElement);
      return base.GetCellType(row);
    }
  }
}
