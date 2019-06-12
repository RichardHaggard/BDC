// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.RadDisplacementMapEffect
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Drawing
{
  public abstract class RadDisplacementMapEffect : RadEffect
  {
    public static readonly RadDisplacementMapEffect Empty = (RadDisplacementMapEffect) new RadDisplacementMapEffect.EmptyDisplacementMapEffect();

    public abstract float Scale { get; set; }

    public abstract RadDisplacementMapSelector XSelect { get; set; }

    public abstract RadDisplacementMapSelector YSelect { get; set; }

    private class EmptyDisplacementMapEffect : RadDisplacementMapEffect
    {
      public override float Scale
      {
        get
        {
          return 0.0f;
        }
        set
        {
        }
      }

      public override RadDisplacementMapSelector XSelect
      {
        get
        {
          return RadDisplacementMapSelector.R;
        }
        set
        {
        }
      }

      public override RadDisplacementMapSelector YSelect
      {
        get
        {
          return RadDisplacementMapSelector.G;
        }
        set
        {
        }
      }

      public override object RawEffect
      {
        get
        {
          return (object) null;
        }
      }
    }
  }
}
