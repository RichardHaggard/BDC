// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.GalleryMouseOverBehavior
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System;
using System.Drawing;

namespace Telerik.WinControls.UI
{
  public class GalleryMouseOverBehavior : PropertyChangeBehavior
  {
    private bool popupShown;
    private RadGalleryElement gallery;
    private ZoomPopup popup;

    public GalleryMouseOverBehavior(RadGalleryElement gallery)
      : base(RadElement.IsMouseOverProperty)
    {
      this.gallery = gallery;
    }

    public bool IsPopupShown
    {
      get
      {
        return this.popupShown;
      }
    }

    public void ClosePopup()
    {
      if (this.popup == null || !this.popup.Visible)
        return;
      this.popup.Hide();
    }

    public override void OnPropertyChange(RadElement element, RadPropertyChangedEventArgs e)
    {
      if (!(bool) e.NewValue || this.IsPopupShown)
        return;
      this.popupShown = true;
      this.popup = new ZoomPopup(element, new SizeF(1.5f, 1.5f), false);
      this.popup.BeginInit();
      this.popup.Closed += (EventHandler) ((sender, ea) =>
      {
        this.popupShown = false;
        this.popup.Dispose();
        if (element.Parent == null)
          return;
        element.Parent.UpdateLayout();
      });
      this.popup.Clicked += (EventHandler) ((sender, ea) => this.gallery.CloseDropDown());
      this.popup.EndInit();
      this.popup.Show();
    }
  }
}
