// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.TrackBarArrowButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class TrackBarArrowButton : RadRepeatButtonElement
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register(nameof (IsVertical), typeof (bool), typeof (TrackBarArrowButton), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.AffectsTheme));

    static TrackBarArrowButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new TrackBarArrowStateManager(), typeof (TrackBarArrowButton));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.TextAlignment = ContentAlignment.MiddleCenter;
      this.MinSize = new Size(14, 14);
      this.StretchVertically = false;
      this.StretchHorizontally = false;
    }

    public bool IsVertical
    {
      get
      {
        return (bool) this.GetValue(TrackBarArrowButton.IsVerticalProperty);
      }
      set
      {
        int num = (int) this.SetValue(TrackBarArrowButton.IsVerticalProperty, (object) value);
      }
    }

    protected override void OnPropertyChanged(RadPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);
      if (e.Property != RadElement.IsMouseOverElementProperty && e.Property != RadElement.IsMouseDownProperty || this.Parent == null)
        return;
      this.Parent.Invalidate();
    }
  }
}
