// Decompiled with JetBrains decompiler
// Type: Telerik.WinControls.UI.RadGalleryPopupButton
// Assembly: BuilderRED, Version=3.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C1B998B6-FDC9-4BE9-BF3E-DE300E192916
// Assembly location: C:\Program Files (x86)\ERDC-CERL\SMS BUILDER RED\BuilderRED.exe

using System.Windows.Forms;

namespace Telerik.WinControls.UI
{
  public class RadGalleryPopupButton : RadImageButtonElement
  {
    private RadGalleryElement galleryElement;

    public RadGalleryPopupButton(RadGalleryElement galleryElement)
    {
      this.galleryElement = galleryElement;
    }

    public RadGalleryElement GalleryElement
    {
      get
      {
        return this.galleryElement;
      }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.galleryElement.GalleryDropDown.IsVisible && this.IsFocused && e.KeyCode == Keys.Return)
        this.galleryElement.GalleryDropDown.ClickFocusedItem();
      else
        base.OnKeyDown(e);
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (this.galleryElement.GalleryDropDown.IsVisible && this.IsFocused && e.KeyCode == Keys.Space)
        this.galleryElement.GalleryDropDown.ClickFocusedItem();
      else
        base.OnKeyUp(e);
    }
  }
}
