// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadMenuCheckmark
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using Telerik.WinControls.Enumerations;

namespace Telerik.WinControls.UI
{
  public class RadMenuCheckmark : RadCheckmark
  {
    public static RadProperty ShowAlwaysBorderProperty = RadProperty.Register(nameof (ShowAlwaysBorder), typeof (bool), typeof (RadMenuCheckmark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));
    public static RadProperty ShowAlwaysFillProperty = RadProperty.Register("ShowAlwaysFill", typeof (bool), typeof (RadMenuCheckmark), (RadPropertyMetadata) new RadElementPropertyMetadata((object) true, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsLayout));

    public bool ShowAlwaysBorder
    {
      get
      {
        return (bool) this.GetValue(RadMenuCheckmark.ShowAlwaysBorderProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuCheckmark.ShowAlwaysBorderProperty, (object) value);
        this.SetCheckState();
      }
    }

    public bool ShowAlwaysBackground
    {
      get
      {
        return (bool) this.GetValue(RadMenuCheckmark.ShowAlwaysFillProperty);
      }
      set
      {
        int num = (int) this.SetValue(RadMenuCheckmark.ShowAlwaysFillProperty, (object) value);
        this.SetCheckState();
      }
    }

    protected override void SetCheckState()
    {
      if (this.CheckState == ToggleState.On || this.CheckState == ToggleState.Indeterminate)
      {
        if (this.ShowAlwaysBorder)
          this.Border.Visibility = ElementVisibility.Visible;
        if (this.ShowAlwaysBackground)
          this.Fill.Visibility = ElementVisibility.Visible;
        if (this.CheckElement == null || this.ImageElement != null && !this.ImageElement.IsEmpty)
          return;
        this.CheckElement.Visibility = ElementVisibility.Visible;
      }
      else
      {
        this.Border.Visibility = ElementVisibility.Collapsed;
        this.Fill.Visibility = ElementVisibility.Collapsed;
        if (this.CheckElement == null)
          return;
        this.CheckElement.Visibility = ElementVisibility.Collapsed;
      }
    }
  }
}
