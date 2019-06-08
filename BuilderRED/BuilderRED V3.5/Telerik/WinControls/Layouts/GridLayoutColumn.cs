// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.GridLayoutColumn
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Layouts
{
  public class GridLayoutColumn : GridLayoutElement
  {
    public static readonly RadProperty ProportionalWidthWeightProperty = RadProperty.Register(nameof (ProportionalWidthWeight), typeof (int), typeof (GridLayoutColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1));
    public static readonly RadProperty FixedWidthProperty = RadProperty.Register(nameof (FixedWidth), typeof (float), typeof (GridLayoutColumn), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0));
    private float width = 20f;

    public int ProportionalWidthWeight
    {
      get
      {
        return (int) this.GetValue(GridLayoutColumn.ProportionalWidthWeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridLayoutColumn.ProportionalWidthWeightProperty, (object) value);
      }
    }

    public float FixedWidth
    {
      get
      {
        return (float) this.GetValue(GridLayoutColumn.FixedWidthProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridLayoutColumn.FixedWidthProperty, (object) value);
      }
    }

    public float Width
    {
      get
      {
        return this.width;
      }
      set
      {
        this.width = value;
      }
    }
  }
}
