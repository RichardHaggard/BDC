// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RangeSelectorArrowButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Drawing;
using Telerik.WinControls.Styles;

namespace Telerik.WinControls.UI
{
  public class RangeSelectorArrowButton : RadRepeatButtonElement
  {
    public static RadProperty IsVerticalProperty = RadProperty.Register(nameof (IsVertical), typeof (bool), typeof (RangeSelectorArrowButton), (RadPropertyMetadata) new RadElementPropertyMetadata((object) false, ElementPropertyOptions.InvalidatesLayout | ElementPropertyOptions.AffectsDisplay | ElementPropertyOptions.AffectsTheme));

    static RangeSelectorArrowButton()
    {
      ItemStateManagerFactoryRegistry.AddStateManagerFactory((ItemStateManagerFactoryBase) new RangeSelectorArrowButtonStateManager(), typeof (RangeSelectorArrowButton));
    }

    protected override void InitializeFields()
    {
      base.InitializeFields();
      this.ImageAlignment = ContentAlignment.MiddleCenter;
      this.MinSize = new Size(14, 14);
      this.StretchVertically = false;
      this.StretchHorizontally = false;
      this.ZIndex = 3;
    }

    public bool IsVertical
    {
      get
      {
        return (bool) this.GetValue(RangeSelectorArrowButton.IsVerticalProperty);
      }
      set
      {
        int num = (int) this.SetValue(RangeSelectorArrowButton.IsVerticalProperty, (object) value);
      }
    }
  }
}
