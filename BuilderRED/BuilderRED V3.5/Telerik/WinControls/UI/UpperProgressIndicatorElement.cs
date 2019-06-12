// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.UpperProgressIndicatorElement
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.ComponentModel;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class UpperProgressIndicatorElement : ProgressIndicatorElement
  {
    public static RadProperty AutoOpacityProperty = RadProperty.Register(nameof (AutoOpacity), typeof (bool), typeof (UpperProgressIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.AffectsDisplay));
    public static RadProperty AutoOpacityMinimumProperty = RadProperty.Register(nameof (AutoOpacityMinimum), typeof (double), typeof (UpperProgressIndicatorElement), (RadPropertyMetadata) new RadElementPropertyMetadata((object) 0.75, ElementPropertyOptions.AffectsDisplay));

    [Description("Gets or sets whether this progress indicatior will automatically control its opacity when close to or over the second progress indicator.")]
    [Category("Behavior")]
    public bool AutoOpacity
    {
      get
      {
        return (bool) this.GetValue(UpperProgressIndicatorElement.AutoOpacityProperty);
      }
      set
      {
        int num = (int) this.SetValue(UpperProgressIndicatorElement.AutoOpacityProperty, (object) value);
      }
    }

    [Description("Gets or sets the minimum opacity level this progress indicator will go to when over the second progress indicator when AutoOpacity property is set to true.")]
    [Category("Behavior")]
    public double AutoOpacityMinimum
    {
      get
      {
        return (double) this.GetValue(UpperProgressIndicatorElement.AutoOpacityMinimumProperty);
      }
      set
      {
        int num = (int) this.SetValue(UpperProgressIndicatorElement.AutoOpacityMinimumProperty, (object) value);
      }
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.DrawFill = true;
      this.BackColor = Color.Green;
    }
  }
}
