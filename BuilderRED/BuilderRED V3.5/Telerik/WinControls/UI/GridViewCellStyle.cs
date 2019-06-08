// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GridViewCellStyle
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GridViewCellStyle : RadObject
  {
    public static RadProperty ForeColorProperty = RadProperty.Register(nameof (ForeColor), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.WindowText));
    public static RadProperty FontProperty = RadProperty.Register(nameof (Font), typeof (Font), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) null));
    public static RadProperty CustomizeFillProperty = RadProperty.Register(nameof (CustomizeFill), typeof (bool), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty DrawFillProperty = RadProperty.Register(nameof (DrawFill), typeof (bool), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    public static RadProperty BackColorProperty = RadProperty.Register(nameof (BackColor), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDarkDark));
    public static RadProperty BackColor2Property = RadProperty.Register(nameof (BackColor2), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.Control));
    public static RadProperty BackColor3Property = RadProperty.Register(nameof (BackColor3), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark));
    public static RadProperty BackColor4Property = RadProperty.Register(nameof (BackColor4), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlLightLight));
    public static RadProperty GradientStyleProperty = RadProperty.Register(nameof (GradientStyle), typeof (GradientStyles), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GradientStyles.Solid));
    public static RadProperty GradientAngleProperty = RadProperty.Register(nameof (GradientAngle), typeof (GradientStyles), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 90f));
    public static RadProperty NumberOfColorsProperty = RadProperty.Register(nameof (NumberOfColors), typeof (int), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 2));
    public static RadProperty GradientPercentageProperty = RadProperty.Register(nameof (GradientPercentage), typeof (float), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.5f));
    public static RadProperty GradientPercentage2Property = RadProperty.Register(nameof (GradientPercentage2), typeof (float), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.66f));
    public static RadProperty CustomizeBorderProperty = RadProperty.Register(nameof (CustomizeBorder), typeof (bool), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false));
    public static RadProperty DrawBorderProperty = RadProperty.Register(nameof (DrawBorder), typeof (bool), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true));
    public static RadProperty BorderBoxStyleProperty = RadProperty.Register(nameof (BorderBoxStyle), typeof (BorderBoxStyle), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) BorderBoxStyle.SingleBorder));
    public static RadProperty BorderWidthProperty = RadProperty.Register(nameof (BorderWidth), typeof (float), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f));
    public static RadProperty BorderLeftWidthProperty = RadProperty.Register(nameof (BorderLeftWidth), typeof (float), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f));
    public static RadProperty BorderRightWidthProperty = RadProperty.Register(nameof (BorderRightWidth), typeof (float), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f));
    public static RadProperty BorderTopWidthProperty = RadProperty.Register(nameof (BorderTopWidth), typeof (float), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f));
    public static RadProperty BorderBottomWidthProperty = RadProperty.Register(nameof (BorderBottomWidth), typeof (float), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1f));
    public static RadProperty BorderGradientAngleProperty = RadProperty.Register(nameof (BorderGradientAngle), typeof (float), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 270f));
    public static RadProperty BorderGradientStyleProperty = RadProperty.Register(nameof (BorderGradientStyle), typeof (GradientStyles), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) GradientStyles.Linear));
    public static RadProperty BorderColorProperty = RadProperty.Register(nameof (BorderColor), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDarkDark));
    public static RadProperty BorderColor2Property = RadProperty.Register(nameof (BorderColor2), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark));
    public static RadProperty BorderColor3Property = RadProperty.Register(nameof (BorderColor3), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark));
    public static RadProperty BorderColor4Property = RadProperty.Register(nameof (BorderColor4), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlLightLight));
    public static RadProperty BorderInnerColorProperty = RadProperty.Register(nameof (BorderInnerColor), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlLightLight));
    public static RadProperty BorderInnerColor2Property = RadProperty.Register(nameof (BorderInnerColor2), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.Control));
    public static RadProperty BorderInnerColor3Property = RadProperty.Register(nameof (BorderInnerColor3), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark));
    public static RadProperty BorderInnerColor4Property = RadProperty.Register(nameof (BorderInnerColor4), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDarkDark));
    public static RadProperty BorderLeftColorProperty = RadProperty.Register(nameof (BorderLeftColor), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDarkDark));
    public static RadProperty BorderTopColorProperty = RadProperty.Register(nameof (BorderTopColor), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark));
    public static RadProperty BorderRightColorProperty = RadProperty.Register(nameof (BorderRightColor), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlDark));
    public static RadProperty BorderBottomColorProperty = RadProperty.Register(nameof (BorderBottomColor), typeof (Color), typeof (GridViewCellStyle), (RadPropertyMetadata) new RadElementPropertyMetadata((object) SystemColors.ControlLightLight));
    private GridViewCellInfo ownerCell;

    public Color ForeColor
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.ForeColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.ForeColorProperty, (object) value);
      }
    }

    public Font Font
    {
      get
      {
        return (Font) this.GetValue(GridViewCellStyle.FontProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.FontProperty, (object) value);
      }
    }

    public bool CustomizeFill
    {
      get
      {
        return (bool) this.GetValue(GridViewCellStyle.CustomizeFillProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.CustomizeFillProperty, (object) value);
      }
    }

    public bool DrawFill
    {
      get
      {
        return (bool) this.GetValue(GridViewCellStyle.DrawFillProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.DrawFillProperty, (object) value);
      }
    }

    public Color BackColor
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BackColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BackColorProperty, (object) value);
      }
    }

    public Color BackColor2
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BackColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BackColor2Property, (object) value);
      }
    }

    public Color BackColor3
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BackColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BackColor3Property, (object) value);
      }
    }

    public Color BackColor4
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BackColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BackColor4Property, (object) value);
      }
    }

    public GradientStyles GradientStyle
    {
      get
      {
        return (GradientStyles) this.GetValue(GridViewCellStyle.GradientStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.GradientStyleProperty, (object) value);
      }
    }

    public float GradientAngle
    {
      get
      {
        return (float) this.GetValue(GridViewCellStyle.GradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.GradientAngleProperty, (object) value);
      }
    }

    public int NumberOfColors
    {
      get
      {
        return (int) this.GetValue(GridViewCellStyle.NumberOfColorsProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.NumberOfColorsProperty, (object) value);
      }
    }

    public float GradientPercentage
    {
      get
      {
        return (float) this.GetValue(GridViewCellStyle.GradientPercentageProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.GradientPercentageProperty, (object) value);
      }
    }

    public float GradientPercentage2
    {
      get
      {
        return (float) this.GetValue(GridViewCellStyle.GradientPercentage2Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.GradientPercentage2Property, (object) value);
      }
    }

    public bool CustomizeBorder
    {
      get
      {
        return (bool) this.GetValue(GridViewCellStyle.CustomizeBorderProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.CustomizeBorderProperty, (object) value);
      }
    }

    public bool DrawBorder
    {
      get
      {
        return (bool) this.GetValue(GridViewCellStyle.DrawBorderProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.DrawBorderProperty, (object) value);
      }
    }

    public BorderBoxStyle BorderBoxStyle
    {
      get
      {
        return (BorderBoxStyle) this.GetValue(GridViewCellStyle.BorderBoxStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderBoxStyleProperty, (object) value);
      }
    }

    public float BorderWidth
    {
      get
      {
        return (float) this.GetValue(GridViewCellStyle.BorderWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderWidthProperty, (object) value);
      }
    }

    public float BorderLeftWidth
    {
      get
      {
        return (float) this.GetValue(GridViewCellStyle.BorderLeftWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderLeftWidthProperty, (object) value);
      }
    }

    public float BorderTopWidth
    {
      get
      {
        return (float) this.GetValue(GridViewCellStyle.BorderTopWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderTopWidthProperty, (object) value);
      }
    }

    public float BorderRightWidth
    {
      get
      {
        return (float) this.GetValue(GridViewCellStyle.BorderRightWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderRightWidthProperty, (object) value);
      }
    }

    public float BorderBottomWidth
    {
      get
      {
        return (float) this.GetValue(GridViewCellStyle.BorderBottomWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderBottomWidthProperty, (object) value);
      }
    }

    public float BorderGradientAngle
    {
      get
      {
        return (float) this.GetValue(GridViewCellStyle.BorderGradientAngleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderGradientAngleProperty, (object) value);
      }
    }

    public GradientStyles BorderGradientStyle
    {
      get
      {
        return (GradientStyles) this.GetValue(GridViewCellStyle.BorderGradientStyleProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderGradientStyleProperty, (object) value);
      }
    }

    public Color BorderColor
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderColorProperty, (object) value);
      }
    }

    public Color BorderColor2
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderColor2Property, (object) value);
      }
    }

    public Color BorderColor3
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderColor3Property, (object) value);
      }
    }

    public Color BorderColor4
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderColor4Property, (object) value);
      }
    }

    public Color BorderInnerColor
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderInnerColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderInnerColorProperty, (object) value);
      }
    }

    public Color BorderInnerColor2
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderInnerColor2Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderInnerColor2Property, (object) value);
      }
    }

    public Color BorderInnerColor3
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderInnerColor3Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderInnerColor3Property, (object) value);
      }
    }

    public Color BorderInnerColor4
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderInnerColor4Property);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderInnerColor4Property, (object) value);
      }
    }

    public Color BorderTopColor
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderTopColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderTopColorProperty, (object) value);
      }
    }

    public Color BorderBottomColor
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderBottomColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderBottomColorProperty, (object) value);
      }
    }

    public Color BorderLeftColor
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderLeftColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderLeftColorProperty, (object) value);
      }
    }

    public Color BorderRightColor
    {
      get
      {
        return (Color) this.GetValue(GridViewCellStyle.BorderRightColorProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridViewCellStyle.BorderRightColorProperty, (object) value);
      }
    }

    public GridViewCellStyle()
    {
    }

    public GridViewCellStyle(GridViewCellInfo ownerCell)
    {
      this.ownerCell = ownerCell;
    }

    public void Reset()
    {
      if (this.ownerCell == null)
        return;
      this.Dispose();
      this.ownerCell.Style = (GridViewCellStyle) null;
      this.ownerCell.RowInfo.InvalidateRow();
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      this.ownerCell.PersistCellInfo();
      this.ownerCell.RowInfo.InvalidateRow();
    }
  }
}
