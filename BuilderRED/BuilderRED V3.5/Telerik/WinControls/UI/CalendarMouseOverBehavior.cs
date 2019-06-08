// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.CalendarMouseOverBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class CalendarMouseOverBehavior : PropertyChangeBehavior
  {
    private bool popupShown;

    public CalendarMouseOverBehavior()
      : base(RadElement.IsMouseOverProperty)
    {
    }

    public override void OnPropertyChange(RadElement element, RadPropertyChangedEventArgs e)
    {
      if (!(bool) e.NewValue || this.popupShown)
        return;
      this.popupShown = true;
      ZoomPopup zoomPopup = new ZoomPopup(element, new SizeF(2f, 2f));
      zoomPopup.AnimationFrames = 10;
      zoomPopup.AnimationInterval = 20;
      zoomPopup.BeginInit();
      zoomPopup.Closed += (EventHandler) ((sender, ea) =>
      {
        this.popupShown = false;
        ThemeResolutionService.UnsubscribeFromThemeChanged((IThemeChangeListener) ((RadControl) sender).ElementTree);
      });
      zoomPopup.EndInit();
      zoomPopup.Show();
    }
  }
}
