// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCommandColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.WinControls.Primitives;

namespace Telerik.WinControls.UI
{
  public class GridViewCommandColumn : GridViewDataColumn
  {
    public static RadProperty DefaultTextProperty = RadProperty.Register(nameof (DefaultText), typeof (string), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ""));
    public static RadProperty UseDefaultTextProperty = RadProperty.Register(nameof (UseDefaultText), typeof (bool), typeof (GridViewColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty ImageProperty = RadProperty.Register(nameof (Image), typeof (Image), typeof (GridViewCommandColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null));
    public static RadProperty ImageAlignmentProperty = RadProperty.Register(nameof (ImageAlignment), typeof (ContentAlignment), typeof (GridViewCommandColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) ContentAlignment.MiddleLeft));

    public GridViewCommandColumn()
    {
    }

    public GridViewCommandColumn(string fieldName)
      : base(fieldName)
    {
    }

    public GridViewCommandColumn(string uniqueName, string fieldName)
      : base(uniqueName, fieldName)
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Description("This property is overriden to always return false since grouping by this column is not allowed.")]
    [Browsable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
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

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [DefaultValue(false)]
    public override bool AllowFiltering
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Browsable(true)]
    [DefaultValue(false)]
    public override bool AllowSearching
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(true)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("This property is overriden to always return true since Command column cannot be edited.")]
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

    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Category("Behavior")]
    [DefaultValue("")]
    [Localizable(true)]
    [Description("Gets or sets the default text displayed on the button cell.")]
    public virtual string DefaultText
    {
      get
      {
        return (string) this.GetValue(GridViewCommandColumn.DefaultTextProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCommandColumn.DefaultTextProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets a value indicating whether the default text or the value as text will appear on the button displayed by the cell.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(false)]
    public bool UseDefaultText
    {
      get
      {
        return (bool) this.GetValue(GridViewCommandColumn.UseDefaultTextProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCommandColumn.UseDefaultTextProperty, (object) value);
      }
    }

    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the image displayed on the button cell.")]
    [TypeConverter(typeof (ImageTypeConverter))]
    [Browsable(true)]
    public Image Image
    {
      get
      {
        return (Image) this.GetValue(GridViewCommandColumn.ImageProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCommandColumn.ImageProperty, (object) value);
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the image layout of the image inside the cells.")]
    public virtual ContentAlignment ImageAlignment
    {
      get
      {
        return (ContentAlignment) this.GetValue(GridViewCommandColumn.ImageAlignmentProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCommandColumn.ImageAlignmentProperty, (object) value);
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
        return typeof (GridCommandCellElement);
      return base.GetCellType(row);
    }
  }
}
