// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.Drawing.RadGaussianBlurEffect
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

namespace Telerik.WinControls.Drawing
{
  public abstract class RadGaussianBlurEffect : RadEffect
  {
    public static readonly RadGaussianBlurEffect Empty = (RadGaussianBlurEffect) new RadGaussianBlurEffect.EmptyGaussianBlurEffect();

    public abstract RadGaussianBlurBorderMode Mode { get; set; }

    public abstract float Deviation { get; set; }

    private class EmptyGaussianBlurEffect : RadGaussianBlurEffect
    {
      public override RadGaussianBlurBorderMode Mode
      {
        get
        {
          return RadGaussianBlurBorderMode.Soft;
        }
        set
        {
        }
      }

      public override float Deviation
      {
        get
        {
          return 0.0f;
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
