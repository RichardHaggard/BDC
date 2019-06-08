// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Layouts.GridLayoutRow
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Layouts
{
  public class GridLayoutRow : GridLayoutElement
  {
    public static readonly RadProperty ProportionalHeightWeightProperty = RadProperty.Register(nameof (ProportionalHeightWeight), typeof (int), typeof (GridLayoutRow), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 1));
    public static readonly RadProperty FixedHeightProperty = RadProperty.Register(nameof (FixedHeight), typeof (float), typeof (GridLayoutRow), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0));
    private float height;

    public int ProportionalHeightWeight
    {
      get
      {
        return (int) this.GetValue(GridLayoutRow.ProportionalHeightWeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridLayoutRow.ProportionalHeightWeightProperty, (object) value);
      }
    }

    public float FixedHeight
    {
      get
      {
        return (float) this.GetValue(GridLayoutRow.FixedHeightProperty);
      }
      set
      {
        int num = (int) this.SetValue(GridLayoutRow.FixedHeightProperty, (object) value);
      }
    }

    public float Height
    {
      get
      {
        return this.height;
      }
      set
      {
        this.height = value;
      }
    }
  }
}
